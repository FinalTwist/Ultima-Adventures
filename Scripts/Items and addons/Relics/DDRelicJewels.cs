using System;
using Server;

namespace Server.Items
{
	public class DDRelicJewels : Item
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicJewels() : base( 0x4210 )
		{
			Weight = 5;
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			string sType = "";
			switch ( Utility.RandomMinMax( 0, 8 ) ) 
			{
				case 0: ItemID = 0x4210; sType = "necklace"; break;
				case 1: ItemID = 0x4210; sType = "amulet"; break;
				case 2: ItemID = 0x4210; sType = "medallion"; break;
				case 3: ItemID = 0x4212; sType = "ring"; break;
				case 4: ItemID = 0x4213; sType = "set of earrings"; break;
				case 5: ItemID = 0x4212; sType = "ring"; break;
				case 6: ItemID = 0x4213; sType = "set of earrings"; break;
				case 7: ItemID = 0x4212; sType = "ring"; break;
				case 8: ItemID = 0x4213; sType = "pair of earrings"; break;
			}

			string sLook = "a rare";
			switch ( Utility.RandomMinMax( 0, 18 ) )
			{
				case 0:	sLook = "a rare";	break;
				case 1:	sLook = "a nice";	break;
				case 2:	sLook = "a pretty";	break;
				case 3:	sLook = "a superb";	break;
				case 4:	sLook = "a delightful";	break;
				case 5:	sLook = "an elegant";	break;
				case 6:	sLook = "an exquisite";	break;
				case 7:	sLook = "a fine";	break;
				case 8:	sLook = "a gorgeous";	break;
				case 9:	sLook = "a lovely";	break;
				case 10:sLook = "a magnificent";	break;
				case 11:sLook = "a marvelous";	break;
				case 12:sLook = "a splendid";	break;
				case 13:sLook = "a wonderful";	break;
				case 14:sLook = "an extraordinary";	break;
				case 15:sLook = "a strange";	break;
				case 16:sLook = "an odd";	break;
				case 17:sLook = "a unique";	break;
				case 18:sLook = "an unusual";	break;
			}
			Name = sLook + " " + sType;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public DDRelicJewels(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( RelicGoldValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
		}
	}
}