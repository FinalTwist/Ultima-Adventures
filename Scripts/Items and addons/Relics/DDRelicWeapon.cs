using System;
using Server;

namespace Server.Items
{
	public class DDRelicWeapon : Item
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
		public DDRelicWeapon() : base( 0x48B0 )
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
			
			string sDecon = "";
			switch ( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0:	sDecon = ", decorative";		break;
				case 1:	sDecon = ", ceremonial";		break;
				case 2:	sDecon = ", ornamental";		break;
			}

			switch ( Utility.RandomMinMax( 0, 9 ) ) 
			{
				case 0: ItemID = 0x48B0; RelicFlipID1 = 0x48B0; RelicFlipID2 = 0x48B1; Name = sLook + sDecon + " axe"; break;
				case 1: ItemID = 0x48B2; RelicFlipID1 = 0x48B2; RelicFlipID2 = 0x48B3; Name = sLook + sDecon + " axe"; break;
				case 2: ItemID = 0x48BA; RelicFlipID1 = 0x48BA; RelicFlipID2 = 0x48BB; Name = sLook + sDecon + " sword"; break;
				case 3: ItemID = 0x48BC; RelicFlipID1 = 0x48BC; RelicFlipID2 = 0x48BD; Name = sLook + sDecon + " dagger"; break;
				case 4: ItemID = 0x48BE; RelicFlipID1 = 0x48BE; RelicFlipID2 = 0x48BF; Name = sLook + sDecon + " trident"; break;
				case 5: ItemID = 0x48C0; RelicFlipID1 = 0x48C0; RelicFlipID2 = 0x48C1; Name = sLook + sDecon + " war hammer"; break;
				case 6: ItemID = 0x48C6; RelicFlipID1 = 0x48C6; RelicFlipID2 = 0x48C7; Name = sLook + sDecon + " scythe"; break;
				case 7: ItemID = 0x48C8; RelicFlipID1 = 0x48C8; RelicFlipID2 = 0x48C9; Name = sLook + sDecon + " pike"; break;
				case 8: ItemID = 0x48CA; RelicFlipID1 = 0x48CA; RelicFlipID2 = 0x48CB; Name = sLook + sDecon + " lance"; break;
				case 9: ItemID = 0x48D0; RelicFlipID1 = 0x48D0; RelicFlipID2 = 0x48D1; Name = sLook + sDecon + " swords"; break;
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

		public static void MakeOriental( Item item )
		{
			DDRelicWeapon relic = (DDRelicWeapon)item;

			string sLook = "wakizashi";
			switch ( Utility.RandomMinMax( 0, 13 ) )
			{
				case 0:	sLook = "wakizashi";	break;
				case 1:	sLook = "sword";		break;
				case 2:	sLook = "katana";		break;
				case 3:	sLook = "tanto";		break;
				case 4:	sLook = "chokuto";		break;
				case 5:	sLook = "tsurugi";		break;
				case 6:	sLook = "tachi";		break;
				case 7:	sLook = "odachi";		break;
				case 8:	sLook = "jokoto";		break;
				case 9:	sLook = "koto";			break;
				case 10:sLook = "shinto";		break;
				case 11:sLook = "shinshinto";	break;
				case 12:sLook = "gendaito";		break;
				case 13:sLook = "shinsakuto";	break;
			}

			string OwnerName = Server.Misc.RandomThings.GetRandomOrientalName();
			string OwnerTitle = Server.LootPackEntry.MagicItemAdj( "end", true, false, item.ItemID );
			
			relic.Name = sLook + " of " + OwnerName + " " + OwnerTitle; 

			switch ( Utility.RandomMinMax( 0, 9 ) ) 
			{
				case 0: relic.ItemID = 0x2851; relic.RelicFlipID1 = 0x2851; relic.RelicFlipID2 = 0x2852; break;
				case 1: relic.ItemID = 0x2853; relic.RelicFlipID1 = 0x2853; relic.RelicFlipID2 = 0x2854; break;
				case 2: relic.ItemID = 0x2855; relic.RelicFlipID1 = 0x2855; relic.RelicFlipID2 = 0x2856; break;
				case 3: relic.ItemID = 0x291C; relic.RelicFlipID1 = 0x291C; relic.RelicFlipID2 = 0x291D; break;
				case 4: relic.ItemID = 0x291E; relic.RelicFlipID1 = 0x291E; relic.RelicFlipID2 = 0x291F; break;
				case 5: relic.ItemID = 0x2A2A; relic.RelicFlipID1 = 0x2A2A; relic.RelicFlipID2 = 0x2A2A; break;
				case 6: relic.ItemID = 0x2A45; relic.RelicFlipID1 = 0x2A45; relic.RelicFlipID2 = 0x2A46; break;
				case 7: relic.ItemID = 0x2A47; relic.RelicFlipID1 = 0x2A47; relic.RelicFlipID2 = 0x2A48; break;
				case 8: relic.ItemID = 0x2A49; relic.RelicFlipID1 = 0x2A49; relic.RelicFlipID2 = 0x2A4A; break;
				case 9: relic.ItemID = 0x2A4B; relic.RelicFlipID1 = 0x2A4B; relic.RelicFlipID2 = 0x2A4C; break;
			}
		}

		public DDRelicWeapon(Serial serial) : base(serial)
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