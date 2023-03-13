using System;
using Server;

namespace Server.Items
{
	public class DDRelicVase : Item
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicVase() : base( 0x44F1 )
		{
			Weight = 20;
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			switch ( Utility.RandomMinMax( 0, 8 ) ) 
			{
				case 0: ItemID = 0x44F1; break;
				case 1: ItemID = 0xB46; break;
				case 2: ItemID = 0x44EF; break;
				case 3: ItemID = 0xB48; break;
				case 4: ItemID = 0xB45; Weight = 40; RelicGoldValue = Utility.RandomMinMax( 20, 150 ); break;
				case 5: ItemID = 0xB47; Weight = 40; RelicGoldValue = Utility.RandomMinMax( 20, 150 ); break;
				case 6: ItemID = 0x42B2; Weight = 40; RelicGoldValue = Utility.RandomMinMax( 20, 150 ); break;
				case 7: ItemID = 0x42B3; Weight = 40; RelicGoldValue = Utility.RandomMinMax( 20, 150 ); break;
				case 8: ItemID = 0x44F0; Weight = 40; RelicGoldValue = Utility.RandomMinMax( 20, 150 ); break;
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
			Name = sLook + " vase";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public DDRelicVase(Serial serial) : base(serial)
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