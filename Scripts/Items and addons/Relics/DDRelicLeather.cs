using System;
using Server;

namespace Server.Items
{
	public class DDRelicLeather : Item
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicLeather() : base( 0x106B )
		{
			Weight = 40;
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();

			ItemID = Utility.RandomList( 0x106B, 0x106A, 0x1069, 0x107C, 0x107B, 0x107A, 0x1079, 0x1078 );

			Hue = Utility.RandomMinMax( 2401, 2430 );

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

			string sType = "animal";
			switch ( Utility.RandomMinMax( 0, 20 ) ) 
			{
				case 0:	sType = "deer";	break;
				case 1:	sType = "wolf";	break;
				case 2:	sType = "dinosaur";	break;
				case 3:	sType = "dragon";	break;
				case 4:	sType = "crocodile";	break;
				case 5:	sType = "lizard";	break;
				case 6:	sType = "serpent";	break;
				case 7:	sType = "bear";	break;
				case 8:	sType = "lion";	break;
				case 9:	sType = "mammoth";	break;
				case 10:sType = "manticore";	break;
				case 11:sType = "rhinoceros";	break;
				case 12:sType = "sabretooth";	break;
				case 13:sType = "basilisk";	break;
				case 14:sType = "gargoyle";	break;
				case 15:sType = "unicorn";	break;
				case 16:sType = "pegasus";	break;
				case 17:sType = "demon";	break;
				case 18:sType = "griffin";	break;
				case 19:sType = "alligator";	break;
				case 20:sType = "snake";	break;
			}
			if ( (ItemID == 0x1079) || (ItemID == 0x1078) ){ Name = sLook + " bundle of " + sType + " leather"; }
			else { Name = sLook + " stretched hide of " + sType + " leather"; }
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public DDRelicLeather(Serial serial) : base(serial)
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