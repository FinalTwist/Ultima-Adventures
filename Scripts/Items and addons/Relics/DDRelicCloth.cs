using System;
using Server;

namespace Server.Items
{
	public class DDRelicCloth : Item
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicCloth() : base( 0x175D )
		{
			Weight = 30;
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			ItemID = Utility.RandomList( 0x175D, 0x175E, 0x175F, 0x1760, 0x1761, 0x1762, 0x1763, 0x1764, 0x1765 );
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

			string sType = "silk ";
			switch ( Utility.RandomMinMax( 0, 5 ) ) 
			{
				case 0: sType = "silk "; break;
				case 1: sType = "cotton "; break;
				case 2: sType = "hemp "; break;
				case 3: sType = "wool "; break;
				case 4: sType = ""; break;
				case 5: sType = ""; break;
			}

			Name = sLook + " bundle of " + sType + "cloth";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public DDRelicCloth(Serial serial) : base(serial)
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