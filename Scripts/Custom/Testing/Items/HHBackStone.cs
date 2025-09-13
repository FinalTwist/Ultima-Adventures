/*
   (Please don't remove this header)
   (this is my first script :) )
    Name: Back Stone 
    Description: Back to Britan once overy hour (like Hearthstone in wow)
    Version: 1.0
    Author: squid (squid(at)stormwind.it -- http://heavenandhell.xsquid.net)
    Usage: "[Add HHBackStone"
    
*/

using System; 
using Server; 
using Server.Mobiles;
namespace Server.Items 
{ 
	public class HHBackStone : Item
	{ 
    private DateTime m_lastused = DateTime.UtcNow.AddHours(-1);
    
		private TimeSpan m_delay = TimeSpan.FromSeconds( 3600 );
    
    //In English -- "You must wait one hour to use the stone!";
    private string s_ucantuse = "You must wait one hour to use the stone!";
     //Name of stone --;
    private string s_backstonename = "Back Stone";
    //_Message after teleportation
    private string m_teleported = "You open your eyes, and find yourself in a city.";
    
    

		[Constructable]
		public HHBackStone() : base( 7956 ) 
		{
			Weight = 1.0; 
      Hue = 232;
			Name = s_backstonename;
		} 

		public HHBackStone( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
      writer.Write( (DateTime) m_lastused);
      
		} 
       
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
      m_lastused = reader.ReadDateTime();
		}

		public override void OnDoubleClick( Mobile from ) 
		{ 
      if ( m_lastused + m_delay > DateTime.UtcNow ) 
      {
        TimeSpan difference;
        string diff_string;
        difference = DateTime.UtcNow - m_lastused;
        difference = m_delay - difference;
        from.SendMessage(22, s_ucantuse + " >> [" + difference + "]");
        return;
      }
			else
				m_lastused = DateTime.UtcNow;

			if ( IsChildOf( from.Backpack ) || from.InRange( this, 2 ) && from.CanSee( this ) )
			{
				//Teleport
        Point3D m_loc = new Point3D(1430,1703,9);
        BaseCreature.TeleportPets( from, m_loc, Map.Trammel, true );
			  from.MoveToWorld( m_loc, Map.Trammel );
        from.SendMessage(22, m_teleported);
			}
			else 
			{ 
				from.SendLocalizedMessage( 500446 ); // That is too far away. 
			} 

		}
	} 
}