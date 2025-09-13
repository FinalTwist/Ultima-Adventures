using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0x3D03, 0x3D04 )]
	public class TinkerShoppe : BaseShoppe
	{
		[Constructable]
		public TinkerShoppe()
		{
			Name = "Tinker Work Shoppe";
			ItemID = Utility.RandomList( 0x3D03, 0x3D04 );
			ShoppeName = Name;
			ShelfTitle = "TINKER WORK SHOPPE";
			ShelfItem = 0x3D03;
			ShelfSkill = 51;
			ShelfGuild = NpcGuild.TinkersGuild;
			ShelfTools = "Tinker Tools";
			ShelfResources = "Ingots";
			ShelfSound = 0x542;
		}

		public static string MakeThisTask()
		{
			string task = null;

			switch( Utility.RandomMinMax( 1, 6 ) )
			{
				case 1: task = "Repair"; break;
				case 2: task = "Fix"; break;
				case 3: task = "Enhance"; break;
				case 4: task = "Modify"; break;
				case 5: task = "Resize"; break;
				case 6: task = "Alter"; break;
			}

			string forr = "their";
			switch( Utility.RandomMinMax( 0, 15 ) )
			{
				case 0: forr = "their"; 				break;
				case 1: forr = "their friend's"; 		break;
				case 2: forr = "this"; 					break;
				case 3: forr = "their father's"; 		break;
				case 4: forr = "their mother's"; 		break;
				case 5: forr = "their brother's"; 		break;
				case 6: forr = "their sister's"; 		break;
				case 7: forr = "their uncle's"; 		break;
				case 8: forr = "their aunt's"; 			break;
				case 9: forr = "their cousin's"; 		break;
				case 10: forr = "their grandparent's"; 	break;
			}

			if ( Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				Item item = null;

				switch( Utility.RandomMinMax( 1, 7 ) )
				{
					case 1: item = new MagicLantern(); break;
					case 2: item = new MagicCandle(); break;
					case 3: item = new MagicJewelryRing(); break;
					case 4: item = new MagicJewelryNecklace(); break;
					case 5: item = new MagicJewelryEarrings(); break;
					case 6: item = new MagicJewelryBracelet(); break;
					case 7: item = new MagicJewelryCirclet(); break;
				}

				bool evil = false;
				bool orient = false;

				switch( Utility.RandomMinMax( 1, 8 ) )
				{
					case 1: evil = true; break;
					case 2: orient = true; break;
				}

				string sAdjective = "unusual";
				string eAdjective = "might";

				sAdjective = Server.LootPackEntry.MagicItemAdj( "start", orient, evil, item.ItemID );
				eAdjective = Server.LootPackEntry.MagicItemAdj( "end", orient, evil, item.ItemID );

				string name = "item";
				string xName = ContainerFunctions.GetOwner( "property" );

				if ( item.Name != null && item.Name != "" ){ name = item.Name.ToLower(); }
				if ( name == "item" ){ name = MorphingItem.AddSpacesToSentence( (item.GetType()).Name ).ToLower(); }

				switch( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: name = sAdjective + " " + name + " of " + xName; 	break;
					case 1: name = name + " of " + xName; 						break;
					case 2: name = sAdjective + " " + name; 					break;
					case 3: name = sAdjective + " " + name + " of " + xName; 	break;
					case 4: name = name + " of " + xName; 						break;
					case 5: name = sAdjective + " " + name; 					break;
				}

				item.Delete();

				task = task + " " + forr + " " + name;
			}
			else
			{
				string[] sTinks = new string[] { " jointing plane ", " moulding plane ", " smoothing plane ", " clock ", " axle ", " rolling pin ", " scissors ", " mortar pestle ", " scorp ", " draw knife ", " sewing kit ", " garden tool ", " herbalist cauldron ", " saw ", " dovetail saw ", " froe ", " shovel ", " hammer ", " tongs ", " inshave ", " pickaxe ", " lockpick ", " skillet ", " flour sifter ", " fletcher tools ", " mapmakers pen ", " scribes pen ", " skinning knife ", " surgeons knife ", " mixing cauldron ", " waxing pot ", " sextant ", " spoon ", " plate ", " forkleft ", " cleaver ", " knife ", " goblet ", " mug ", " candle ", " scales ", " key ", " key ring ", " globe ", " spyglass ", " lantern ", " heating stand ", " amulet ", " bracelet ", " ring ", " earrings ", " potion keg " };
					string sTink = sTinks[Utility.RandomMinMax( 0, (sTinks.Length-1) )];

				task = task + " " + forr + sTink;
			}

			return task;
		}

		public TinkerShoppe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}