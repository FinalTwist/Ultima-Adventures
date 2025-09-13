using System;
using Server;

namespace Server.Items
{
	public class DDRelicFur : Item
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicFur() : base( 0x11F4 )
		{
			Weight = 40;
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();

			ItemID = Utility.RandomList( 0x11F4, 0x11F5, 0x11F6, 0x11F7, 0x11F8, 0x11F9, 0x11FA, 0x11FB );

			Hue = Utility.RandomNeutralHue();

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

			string sType = "beaver";
			switch ( Utility.RandomMinMax( 0, 38 ) ) 
			{
				case 0: 	sType = "beaver"; 			break;
				case 1: 	sType = "ermine"; 			break;
				case 2:		sType = "fox"; 				break;
				case 3: 	sType = "marten"; 			break;
				case 4: 	sType = "mink"; 			break;
				case 5: 	sType = "muskrat";			break;
				case 6: 	sType = "sable"; 			break;
				case 7: 	sType = "bear"; 			break;
				case 8: 	sType = "deer"; 			break;
				case 9: 	sType = "rabbit"; 			break;
				case 10:	sType = "yeti";				Hue = 1150;	break;
				case 11:	sType = "dire bear";		break;
				case 12: 	sType = "polar bear";		Hue = 1150;	break;
				case 13:	sType = "black wolf";		Hue = 1899; break;
				case 14:	sType = "badger";			break;
				case 15:	sType = "mammoth";			break;
				case 16: 	sType = "mastadon";			break;
				case 17:	sType = "buffalo";			break;
				case 18:	sType = "camel";			break;
				case 19:	sType = "cheetah";			break;
				case 20:	sType = "leopard";			break;
				case 21:	sType = "lion";				break;
				case 22:	sType = "panther";			Hue = 1899; break;
				case 23:	sType = "lynx";				break;
				case 24:	sType = "cougar";			break;
				case 25:	sType = "sabretooth tiger";	break;
				case 26:	sType = "tiger";			break;
				case 27:	sType = "goat";				Hue = 1150;	break;
				case 28:	sType = "griffin";			break;
				case 29:	sType = "hippogriff";		break;
				case 30:	sType = "hyena";			break;
				case 31:	sType = "jackal";			break;
				case 32:	sType = "wolf";				break;
				case 33:	sType = "otter";			break;
				case 34:	sType = "kodiak bear";		Hue = 1899;	break;
				case 35:	sType = "unicorn";			Hue = 1150;	break;
				case 36:	sType = "pegasus";			Hue = 1150;	break;
				case 37:	sType = "weasel";			break;
				case 38:	sType = "wolverine";		break;
			}

			Name = sLook + " bundle of " + sType + " fur";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public DDRelicFur(Serial serial) : base(serial)
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