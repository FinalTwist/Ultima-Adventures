using Server;
using System;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Accounting;



namespace Server.Items
{
    public class ShowerWater : Item
    {

        [Constructable]
        public ShowerWater()
            : base(14138)
        {
            Movable = false;
            Name = "Water";
        }

		public override bool OnMoveOver( Mobile m )
        {
			m.SendMessage( 48, "You feel clean and fresh as you step into the shower water." );
			
			return true;
        }
        public ShowerWater(Serial serial)
            : base(serial) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
    }  	        
} 
