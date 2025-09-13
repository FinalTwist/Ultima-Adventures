/* Created by Hammerhand */

using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class CloakoftheFireLord : Cloak
	{
                public override int ArtifactRarity{ get{ return 2106; } }
                public override int BaseFireResistance { get { return 20; } }

		[Constructable]
		public CloakoftheFireLord()
		{
			 
			Name = "Cloak of the FireLord";
            Hue = 1360;
			Attributes.Luck = 100;

            		
		 	Attributes.RegenHits = 10;
		 	Attributes.RegenStam = 7;				
			Attributes.RegenMana = 7;
		}
		
		public override bool OnEquip(Mobile m) 
	      { 

			if ( m.Mounted )
			{
				m.SendMessage( "You cant do that while mounted" );
				return false;
			}


			m.NameMod = m.Name+" The FireLord";
		        m.BodyMod = 76;
			m.HueMod = 1360;
               Attributes.BonusStr = 15;
			m.DisplayGuildTitle = false;	
			m.SendMessage( "The cloak has transformed you into a FireLord!" );
            m.PlaySound( 484 );
			return base.OnEquip(m);
				

		}
		
		public override void OnRemoved(IEntity parent) 
	      { 
		if (parent is Mobile) 
	        { 
	         Mobile m = (Mobile)parent; 
		   m.NameMod = null;
                   m.BodyMod = 0;
           m.HueMod = -1;
               Attributes.BonusStr = 0;
		   m.SendMessage( "Your back to your old self." );
                   m.PlaySound( 484 );		   
		   m.DisplayGuildTitle = true;
		  }

	         base.OnRemoved(parent); 
      	}

        public CloakoftheFireLord(Serial serial): base(serial)
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
 
		}
	}
}