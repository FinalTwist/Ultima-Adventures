using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using Server.Multis;

namespace Server.Misc
{
    class GravediggerCommand
    {
	public static void Initialize()
	{
	    CommandSystem.Register( "gravedigger", AccessLevel.Player, new CommandEventHandler( Gravedigger_Command ) );
	}
	public static void Register( string command, AccessLevel access, CommandEventHandler handler )
	{
	    CommandSystem.Register(command, access, handler);
	}

	[Usage( "gravedigger" )]
	[Description( "Summons the gravedigger to get your body" )]
	public static void Gravedigger_Command( CommandEventArgs e )
	{
	    Mobile from = e.Mobile;

	    if (!from.Alive)
	    {
		from.SendMessage("You are dead and cannot do that!");
		return;
	    }

	    Map map = from.Map;

	    if ( map == null )
		return;

	    GraveDigger mSp = new GraveDigger();
	    mSp.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);

	    mSp.Say ("Greetings, Sire.");
	    mSp.Say ("I will fetch your corpse for a fee of 40,000 gold (coins)");
	    mSp.Say ("Hurry, I am busy and will only stay here a few minutes.");

	}


    }

}

namespace Server.Mobiles
{
    [CorpseName( "A gravedigger corpse" )]
    public class GraveDigger : BaseCreature
    {
	public virtual bool IsInvulnerable{ get{ return true; } }

	[Constructable]
	public GraveDigger() : base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
	{
	    Name = "Squeiyk the Gravedigger";
	    Body = 42;
	    BaseSoundID = 0xCC;
	    Hidden = false;
	    CantWalk = true;
	    Timer.DelayCall( TimeSpan.FromMinutes( 5.0 ), new TimerCallback( Delete ) );
	    Blessed = true;

	}

	public GraveDigger(Serial serial) : base(serial)
	{
	}

	public override bool HandlesOnSpeech( Mobile from ) 
	{ 
	    return true; 
	} 

	public override void OnSpeech( SpeechEventArgs e ) 
	{
	    if( e.Mobile.InRange( this, 4 ))
	    {
			if ( ( e.Speech.ToLower() == "fetch" ) )
			{
				e.Mobile.SendMessage("Yes sire, as you command.");
				GetMyCorpse(e.Mobile);
				this.Delete();
			}
	    }
	    base.OnSpeech( e ); 

	}

	public override void Serialize(GenericWriter writer)
	{
	    base.Serialize(writer);
	    writer.Write((int) 0);
	}

	public override void Deserialize(GenericReader reader)
	{
	    base.Deserialize(reader);
	    int version = reader.ReadInt();
	    Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( Delete ) );
	}

	public static void GetMyCorpse( Mobile from )
	{

	    if (!from.Alive)
	    {
			from.SendMessage("But you are dead sire, I cannot help thee");
			return;
	    }

		string world = Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y );
		Region region = Region.Find( from.Location, from.Map );

	    Map map = from.Map;

	    if ( map == null )
			return;

	    int distchk = 0;

	    ArrayList bodies = new ArrayList();
	    ArrayList empty = new ArrayList();

	    foreach ( Item body in World.Items.Values ) 
		if ( body is Corpse )
		{
		    Corpse cadaver = (Corpse)body;

		    int carrying = body.GetTotal( TotalType.Items );
		    
		    if (cadaver.Map == null)
		    	continue;
			
			BaseHouse house = BaseHouse.FindHouseAt( body.Location, body.Map, 16 );
			
			string bodyworld = Worlds.GetMyWorld( cadaver.Map, cadaver.Location, cadaver.X, cadaver.Y );
			Region bodyregion = Region.Find( cadaver.Location, cadaver.Map );

		    if ( house == null && cadaver.Owner == from && carrying > 0 && ( bodyworld == world || bodyregion.IsPartOf(world) || region.IsPartOf(bodyworld) ) )
		    {
				distchk++;
				bodies.Add( body ); 
		    }
		}

	    if ( distchk == 0 ){ from.SendMessage("But Sire, You have no nearby corpse in these lands!"); }
	    else
	    {

			int i_Bank;
			i_Bank = Banker.GetBalance( from );				
			Container bank = from.FindBankNoCreate();				
			if ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), 40000 ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), 40000 ) ) )
			{
				for ( int h = 0; h < bodies.Count; ++h )
				{
					Corpse theBody = ( Corpse )bodies[ h ];
					theBody.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
				}
			}
			else
			{
				from.SendMessage("I only work for gold, Sire.");
				from.SendMessage("Make sure you have 40,000 gold (coins) in your pack or bank.");
			}
	    }
	}


    }
}
