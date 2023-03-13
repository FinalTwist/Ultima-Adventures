using System;
using Server;

namespace Server.Items
{
	public class Pillows : Item
	{
		public int PillowFlipID1;
		public int PillowFlipID2;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Pillow_FlipID1 { get { return PillowFlipID1; } set { PillowFlipID1 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Pillow_FlipID2 { get { return PillowFlipID2; } set { PillowFlipID2 = value; InvalidateProperties(); } }

		[Constructable]
		public Pillows() : base( 0x1397 )
		{
			Weight = 2;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			switch ( Utility.RandomMinMax( 0, 5 ) ) 
			{
				case 0: ItemID = 0x1397; PillowFlipID1 = 0x1397; PillowFlipID2 = 0x13A6; break;
				case 1: ItemID = 0x13A4; PillowFlipID1 = 0x13A4; PillowFlipID2 = 0x13A5; break;
				case 2: ItemID = 0x13A7; PillowFlipID1 = 0x13A7; PillowFlipID2 = 0x13A8; break;
				case 3: ItemID = 0x13A9; PillowFlipID1 = 0x13A9; PillowFlipID2 = 0x13AA; break;
				case 4: ItemID = 0x13AC; PillowFlipID1 = 0x13AC; PillowFlipID2 = 0x13AC; break;
				case 5: ItemID = 0x13AD; PillowFlipID1 = 0x13AD; PillowFlipID2 = 0x13AE; break;
			}

			Name = "pillow";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.ItemID == PillowFlipID1 ){ this.ItemID = PillowFlipID2; } else { this.ItemID = PillowFlipID1; }
		}

		public Pillows(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( PillowFlipID1 );
            writer.Write( PillowFlipID2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            PillowFlipID1 = reader.ReadInt();
            PillowFlipID2 = reader.ReadInt();
		}
	}
}