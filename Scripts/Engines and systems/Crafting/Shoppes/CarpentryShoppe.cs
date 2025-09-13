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
	[Flipable( 0x3CFB, 0x3CFC )]
	public class CarpentryShoppe : BaseShoppe
	{
		[Constructable]
		public CarpentryShoppe()
		{
			Name = "Carpentry Work Shoppe";
			ItemID = Utility.RandomList( 0x3CFB, 0x3CFC );
			ShoppeName = Name;
			ShelfTitle = "CARPENTRY WORK SHOPPE";
			ShelfItem = 0x3CFB;
			ShelfSkill = 11;
			ShelfGuild = NpcGuild.CarpentersGuild;
			ShelfTools = "Carpentry Tools";
			ShelfResources = "Boards or Logs";
			ShelfSound = 0x23D;
		}

		public static string MakeThisTask()
		{
			string task = null;

			switch( Utility.RandomMinMax( 1, 10 ) )
			{
				case 1: task = "Repair"; break;
				case 2: task = "Fix"; break;
				case 3: task = "Sand"; break;
				case 4: task = "Modify"; break;
				case 5: task = "Polish"; break;
				case 6: task = "Engrave"; break;
				case 7: task = "Adjust"; break;
				case 8: task = "Improve"; break;
				case 9: task = "Oil"; break;
				case 10: task = "Refinish"; break;
			}

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item item = null;

				switch( Utility.RandomMinMax( 1, 14 ) )
				{
					case 1: item = new WildStaff(); break;
					case 2: item = new ShepherdsCrook(); break;
					case 3: item = new QuarterStaff(); break;
					case 4: item = new GnarledStaff(); break;
					case 5: item = new WoodenShield(); break;
					case 6: item = new Bokuto(); break;
					case 7: item = new Fukiya(); break;
					case 8: item = new Tetsubo(); break;
					case 9: item = new WoodenPlateArms(); break;
					case 10: item = new WoodenPlateHelm(); break;
					case 11: item = new WoodenPlateGloves(); break;
					case 12: item = new WoodenPlateGorget(); break;
					case 13: item = new WoodenPlateLegs(); break;
					case 14: item = new WoodenPlateChest(); break;
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

				task = task + " their " + name;
			}
			else
			{
				string[] sWoods = new string[] {" ", " ash wood ", " cherry wood ", " ebony wood ", " golden oak ", " hickory ", " mahogany ", " oak ", " pine ", " ghost wood ", " rosewood ", " walnut wood ", " petrified wood ", " diftwood ", " elven wood " };
					string sWood = sWoods[Utility.RandomMinMax( 0, (sWoods.Length-1) )];

				task = task + " their" + sWood;

				switch( Utility.RandomMinMax( 1, 20 ) )
				{
					case 1: task = task + "foot stool"; break;
					case 2: task = task + "stool"; break;
					case 3: task = task + "chair"; break;
					case 4: task = task + "bench"; break;
					case 5: task = task + "throne"; break;
					case 6: task = task + "nightstand"; break;
					case 7: task = task + "writing table"; break;
					case 8: task = task + "table"; break;
					case 9: task = task + "box"; break;
					case 10: task = task + "crate"; break;
					case 11: task = task + "chest"; break;
					case 12: task = task + "armoire"; break;
					case 13: task = task + "bookcase"; break;
					case 14: task = task + "shelf"; break;
					case 15: task = task + "drawers"; break;
					case 16: task = task + "foot locker"; break;
					case 17: task = task + "cabinet"; break;
					case 18: task = task + "barrel"; break;
					case 19: task = task + "tub"; break;
					case 20: task = task + "bed"; break;
				}
			}

			return task;
		}

		public CarpentryShoppe( Serial serial ) : base( serial )
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