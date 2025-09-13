using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class SeaShell : Item
	{
		[Constructable]
		public SeaShell() : base( 0xFC4 )
		{
			Weight = 1;
			Name = "sea shell";

			switch ( Utility.RandomMinMax( 0, 8 ) ) 
			{
				case 0: ItemID = 0xFC4; Name = "sea shell"; break;
				case 1: ItemID = 0xFC5; Name = "sea shells"; break;
				case 2: ItemID = 0xFC6; Name = "sea shells"; break;
				case 3: ItemID = 0xFC7; Name = "sea shell"; break;
				case 4: ItemID = 0xFC8; Name = "sea shell"; break;
				case 5: ItemID = 0xFC9; Name = "sea shells"; break;
				case 6: ItemID = 0xFCA; Name = "sea shells"; break;
				case 7: ItemID = 0xFCB; Name = "sea shell"; break;
				case 8: ItemID = 0xFCC; Name = "sea shell"; break;
			}
		}

		public SeaShell(Serial serial) : base(serial)
		{
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
		}
	}
}