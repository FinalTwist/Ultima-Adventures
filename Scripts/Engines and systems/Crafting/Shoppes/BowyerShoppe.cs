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
	[Flipable( 0x3CE9, 0x3CEA )]
	public class BowyerShoppe : BaseShoppe
	{
		[Constructable]
		public BowyerShoppe()
		{
			Name = "Bowyer Work Shoppe";
			ItemID = Utility.RandomList( 0x3CE9, 0x3CEA );
			ShoppeName = Name;
			ShelfTitle = "BOWYER WORK SHOPPE";
			ShelfItem = 0x3CE9;
			ShelfSkill = 20;
			ShelfGuild = NpcGuild.ArchersGuild;
			ShelfTools = "Fletching Tools";
			ShelfResources = "Boards or Logs";
			ShelfSound = 0x55;
		}

		public static string MakeThisTask()
		{
			string task = null;

			switch( Utility.RandomMinMax( 1, 10 ) )
			{
				case 1: task = "Repair"; break;
				case 2: task = "Fix"; break;
				case 3: task = "Enhance"; break;
				case 4: task = "Modify"; break;
				case 5: task = "Restring"; break;
				case 6: task = "Engrave"; break;
				case 7: task = "Adjust"; break;
				case 8: task = "Improve"; break;
				case 9: task = "Align"; break;
				case 10: task = "Balance"; break;
			}

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item item = null;

				switch( Utility.RandomMinMax( 1, 7 ) )
				{
					case 1: item = new Bow(); break;
					case 2: item = new Crossbow(); break;
					case 3: item = new HeavyCrossbow(); break;
					case 4: item = new RepeatingCrossbow(); break;
					case 5: item = new CompositeBow(); break;
					case 6: item = new MagicalShortbow(); break;
					case 7: item = new ElvenCompositeLongbow(); break;
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

				switch( Utility.RandomMinMax( 1, 8 ) )
				{
					case 1: task = task + "bow"; break;
					case 2: task = task + "crossbow"; break;
					case 3: task = task + "longbow"; break;
					case 4: task = task + "shortbow"; break;
					case 5: task = task + "repeating crossbow"; break;
					case 6: task = task + "heavy crossbow"; break;
					case 7: task = task + "composite longbow"; break;
					case 8: task = task + "composite shortbow"; break;
				}
			}

			return task;
		}

		public BowyerShoppe( Serial serial ) : base( serial )
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