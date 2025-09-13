//------------------------------------------------------------------------------
///  <summary>
///   
///  </summary>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Globalization;
//------------------------------------------------------------------------------
using Server;
using Server.Misc;
using Server.Items;
using Server.Regions;
using Server.Mobiles;
using Server.Targeting;
using Server.Accounting;
using Server.Network;
using Server.Engines.XmlSpawner2;
//------------------------------------------------------------------------------
namespace Server.Misc {
//------------------------------------------------------------------------------
//  Encounter Timer: determines the pace at which random encounters occur...
//--------------------------------------------------------------------------
public class EncounterTimer : Timer
{
    string              m_RegionType;
    float               m_Interval;

    public EncounterTimer( string regionType, float interval ) : 
        base( 
            TimeSpan.FromSeconds( RandomEncounterEngine.Delay ), 
            TimeSpan.FromSeconds( interval ) 
            )
    {
        m_RegionType = regionType;
        m_Interval = interval;
    }

    protected override void OnTick()
    {
        RandomEncounterEngine.GenerateEncounters( m_RegionType ); 
    }
}
//--------------------------------------------------------------------------
//  Reinitialize Timer: checks periodically to see if the config file has
//     been written; reinitializes system if it has.
//--------------------------------------------------------------------------
public class ReinitializeTimer : Timer
{
    private DateTime        m_lastWriteTime;

    public ReinitializeTimer( ) : base( TimeSpan.FromSeconds( 0 ), TimeSpan.FromSeconds( 15 ) )
    { 
        m_lastWriteTime = File.GetLastWriteTime( RandomEncounterEngine.EncountersFile );
    }

    protected override void OnTick()
    {
        DateTime        checkTime = File.GetLastWriteTime( RandomEncounterEngine.EncountersFile );

        if( checkTime > m_lastWriteTime )
        {
            if (RandomEncounterEngine.Debug)
                Console.WriteLine("RandomEncounters: rereading encounters file");
            
            RandomEncounterEngine.Stop();

            RandomEncounterEngine.Initialize();

            m_lastWriteTime = checkTime;
        }
    }
}
//--------------------------------------------------------------------------
//  DeleteTimer -- exists to spin down encounters so that the world
//    doesn't overflow
//--------------------------------------------------------------------------
public class DeleteTimer : Timer
{
    public DeleteTimer( 
        float delay,
        float stride
        ) : 
        base( 
            TimeSpan.FromSeconds( delay  ), 
            TimeSpan.FromSeconds( stride ) 
            )
    {
    }

    protected override void OnTick()
    {
        if( RandomEncounterEngine.Debug) Console.Write( "\n****RandomEncounters: Running Cleaner... " );

        ArrayList  removeList = new ArrayList();

        foreach( XmlAttachment attachment in XmlAttach.AllAttachments.Values )
        {
            if( attachment is XmlDateCount )
            {
                XmlDateCount    xmlDate = (XmlDateCount) attachment;

                long            then = xmlDate.Date.Ticks;
                long            now = DateTime.UtcNow.Ticks;
                long            elapsed = ( now - then ) / 10000000;

                // the timer doesn't sweep encounters that aren't in the cleanup window

                if( elapsed < RandomEncounterEngine.Cleanup ) 
                {
                    //Console.WriteLine("Object too new {0} for cleanup; skipping", elapsed );
                    continue;
                }

                // now we'll inspect the actual encounter object

                object o = xmlDate.AttachedTo;

                if( o == null )
                    // can happen because xmlattach uses lazy evaluation to 
                    // cleanup the deletes
                {
                    continue; 
                }

                //Console.WriteLine("Object {0} has been around for {1} ", o, elapsed);

                if( o is Item )
                {
                    Item i = (Item) o;

                    if( !i.Movable ) 
                    {
                        if( MaybeRemove( xmlDate, i ) ) removeList.Add( new object[2]{ xmlDate, i } );
                    }

                    object parent = i.Parent;

                    //  check up the containment hierarchy...

                    while( true ) 
                    {
                        if( parent == null ) 
                        {
                            if( MaybeRemove( xmlDate, i ) ) removeList.Add( new object[2]{ xmlDate, i } );

                            break;
                        }

                        if( parent is Item ) parent = ((Item)parent).Parent;

                        else if( parent is Mobile ) 
                        {
                            // once owned picked up, it will NEVER be cleaned

                            removeList.Add( new object[2]{ xmlDate, null } );

                            break;
                        }
                    }
                }
                else if( o is Mobile )
                {
                    if( o is BaseCreature )
                    {
                        BaseCreature c = (BaseCreature) o;

                        if (c.Owners.Count > 0) 
                        {
                            // once tamed, it will NEVER be cleaned
                            //Console.WriteLine("Detaching tamed creature: "+o);

                            removeList.Add( new object[2]{ xmlDate, null } );

                            continue;
                        }
                    }


                    Mobile m = (Mobile) o;

                    if( m.Combatant == null ) 
                    {
                        if( MaybeRemove( xmlDate, m ) ) 
                        {
                            removeList.Add( new object[2]{ xmlDate, m } );
                        }
                    }
                }
            }

            //------------------------------------------------------------------
            //  Below following code is a small optimization to keep a big sweep
            //  from causing a lag spike. Basically, it will offset the sweep work
            //  in time in the server, giving the server 1/10th of a second breathers
            //  in the sweep work.
            //------------------------------------------------------------------
            //  not quite right because of the possibility of first 250 attachmnts
            //  being not xmldates.... would never do the work.

            //if( nIterations > 250 ) 
            //{
            //    DeleteTimer workoff = new DeleteTimer( .10F, 0 );
            //    workoff.Start();
            //    break;
            //}
        }

        int nDetached = 0;
        int nDeleted = 0;

        foreach( object[] arr in removeList )
        {
            XmlDateCount xmlDate = (XmlDateCount) arr[0];
            object  o       = arr[1];

            if( o != null )
            {
                if( o is Item )
                { 
                    Item i = (Item) o;

                    nDeleted++;

                    i.Delete();
                }

                if( o is Mobile )

// Edit 1
			if ( !(o is BaseCreature) )
		                { 
                   		 Mobile m = (Mobile) o;
  	         		 nDeleted++;
		    		 m.Delete();
				}
// Edit 2
                    if ( o is BaseCreature )
                    {
                        BaseCreature c = (BaseCreature) o;

                        if ( !(c.Controlled = true && c.ControlMaster is PlayerMobile ) )
		                { 
                   		 Mobile m = (Mobile) o;
  	         		 nDeleted++;
		    		 m.Delete();
				}
                    }

                
                XmlAttach.Defrag(o);
            }

            nDetached++;
            xmlDate.Delete();
            XmlAttach.AllAttachments.Remove( xmlDate.Serial.Value );
        }

        if( RandomEncounterEngine.Debug) Console.WriteLine( " detached = {0}; deleted = {1}", nDetached, nDeleted );

        if( Interval.Ticks == 0 ) Stop();
    }

    private bool MaybeRemove( XmlDateCount xmlDate, object o )
    {
        if( RandomEncounterEngine.CleanupGrace == 0 ) return true;

        Point3D             loc     = o is Mobile ? ((Mobile)o).Location : ((Item)o).Location;

        IPooledEnumerable   eable   = null; 

        if( o is Mobile) 
        {
            Mobile m = (Mobile) o;

            if( m.Map == null ) return true;

            eable = m.Map.GetClientsInRange( loc, 18 );
        }

        else
        {
            Item i = (Item) o;

            if( i.Map == null ) return true;

            eable = i.Map.GetClientsInRange( loc, 18 );
        }

        foreach( NetState state in eable )
        {
            if( state.Mobile != null && state.Mobile.AccessLevel > AccessLevel.Player ) continue;

            if( xmlDate.Count < RandomEncounterEngine.CleanupGrace ) 
            {
                xmlDate.Count += 1;
                return false;
            }
        }

        return true;
    }
}
//public class DeleteEncounterTimer : Timer
//{
//    ArrayList           m_Objects;
//
//    public DeleteEncounterTimer( float cleanup, ArrayList objects ) : 
//        base( 
//            TimeSpan.FromSeconds( cleanup ), 
//            TimeSpan.FromSeconds( 0 ) 
//            )
//    {
//        m_Objects = objects;
//    }
//
//    protected override void OnTick()
//    {
//        ArrayList       saveList = new ArrayList();
//
//        foreach( object o in m_Objects )
//        {
//            if( o is Mobile )
//            {
//                Mobile m = (Mobile) o;
//
//                if (o is BaseCreature)
//                {
//                    BaseCreature c = (BaseCreature) o;
//
//                    if (c.Owners.Count > 0) 
//                    {
//                        //Console.WriteLine("Skipping deletion of mobile {0} because it is tame.", m);
//                        continue;
//                    }
//                }
//
//                if( m.Combatant == null ) m.Delete();
//                else saveList.Add( o );
//            }
//            else if( o is Item )
//            {
//                Item i = (Item) o;
//                i.Delete();
//            }
//        }
//
//        if (saveList.Count == 0) Stop();
//
//        else m_Objects = saveList;
//    }
//}
//------------------------------------------------------------------------------
} // namespace Server.Misc
//------------------------------------------------------------------------------
