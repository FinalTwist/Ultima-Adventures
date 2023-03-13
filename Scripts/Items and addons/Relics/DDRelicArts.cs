using System;
using Server;

namespace Server.Items
{
	public class DDRelicArts : Item
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicArts() : base( 0x4210 )
		{
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			RelicGoldValue = Server.Misc.RelicItems.RelicValue();

			string sType = "";
			switch ( Utility.RandomMinMax( 0, 2 ) ) 
			{
				case 0: ItemID = Utility.RandomList( 0x9CB, 0x9B3, 0x9BF, 0x9CB ); sType = " goblet"; Weight = 5; break;
				case 1: ItemID = Utility.RandomList( 0x42BE, 0x15F8, 0x15FD, 0x1603, 0x1604 ); sType = " bowl"; Weight = 20; break;
				case 2: ItemID = Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ); sType = " scepter"; Weight = 10; break;
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

			string sDecon = "decorative";
			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	sDecon = ", decorative";		break;
				case 1:	sDecon = ", ornamental";		break;
				case 2:	sDecon = "";		break;
				case 3:	sDecon = "";		break;
			}

			Name = sLook + sDecon + sType;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public DDRelicArts(Serial serial) : base(serial)
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