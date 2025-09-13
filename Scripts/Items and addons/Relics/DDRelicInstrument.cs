using System;
using Server;

namespace Server.Items
{
	public class DDRelicInstrument : Item
	{
		public int RelicGoldValue;
		public int RelicFlipID1;
		public int RelicFlipID2;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Relic_FlipID1 { get { return RelicFlipID1; } set { RelicFlipID1 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Relic_FlipID2 { get { return RelicFlipID2; } set { RelicFlipID2 = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicInstrument() : base( 0x41FD )
		{
			Weight = 10;
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

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
			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0:	sDecon = "decorative";		break;
				case 1:	sDecon = "ceremonial";		break;
				case 2:	sDecon = "ornamental";		break;
			}

			switch ( Utility.RandomMinMax( 0, 1 ) ) 
			{
				case 0: ItemID = 0x41FD; RelicFlipID1 = 0x41FD; RelicFlipID2 = 0x41FC; Name = sLook + ", " + sDecon + " harp"; break;
				case 1: ItemID = 0x420C; RelicFlipID1 = 0x420C; RelicFlipID2 = 0x420D; Name = sLook + ", " + sDecon + " lute"; break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "This can be identified to determine its value." );
				from.SendMessage( "This must be in your backpack to flip." );
			}
			else
			{
				if ( this.ItemID == RelicFlipID1 ){ this.ItemID = RelicFlipID2; } else { this.ItemID = RelicFlipID1; }
			}
		}

		public DDRelicInstrument(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( RelicGoldValue );
            writer.Write( RelicFlipID1 );
            writer.Write( RelicFlipID2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
            RelicFlipID1 = reader.ReadInt();
            RelicFlipID2 = reader.ReadInt();
		}
	}
}