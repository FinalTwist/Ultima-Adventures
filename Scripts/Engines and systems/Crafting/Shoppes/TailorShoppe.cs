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
	[Flipable( 0x3CF9, 0x3CFA )]
	public class TailorShoppe : BaseShoppe
	{
		[Constructable]
		public TailorShoppe()
		{
			Name = "Tailor Work Shoppe";
			ItemID = Utility.RandomList( 0x3CF9, 0x3CFA );
			ShoppeName = Name;
			ShelfTitle = "TAILOR WORK SHOPPE";
			ShelfItem = 0x3CF9;
			ShelfSkill = 49;
			ShelfGuild = NpcGuild.TailorsGuild;
			ShelfTools = "Sewing Kits";
			ShelfResources = "Cloth";
			ShelfSound = 0x248;
		}

		public static string MakeThisTask()
		{
			string task = null;

			switch( Utility.RandomMinMax( 1, 9 ) )
			{
				case 1: task = "Repair"; break;
				case 2: task = "Fix"; break;
				case 3: task = "Enhance"; break;
				case 4: task = "Modify"; break;
				case 5: task = "Resize"; break;
				case 6: task = "Embroider"; break;
				case 7: task = "Stitch"; break;
				case 8: task = "Patch"; break;
				case 9: task = "Alter"; break;
			}

			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
			{
				Item item = null;

				switch( Utility.RandomMinMax( 1, 6 ) )
				{
					case 1: item = new MagicRobe(); break;
					case 2: item = new MagicCloak(); break;
					case 3: item = new MagicBelt(); break;
					case 4: item = new MagicBoots(); break;
					case 5: item = new MagicHat(); break;
					case 6: item = new MagicSash(); break;
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
				string[] sCloths = new string[] { " brocade ", " cotton ", " ermine ", " silk ", " wool ", " fur ", " spider silk ", " cloth ", " lace ", " leather ", " felt ", " hemp ", " linen ", " quilted " };
					string sCloth = sCloths[Utility.RandomMinMax( 0, (sCloths.Length-1) )];

				task = task + " their" + sCloth;

				switch( Utility.RandomMinMax( 1, 17 ) )
				{
					case 1: task = task + "shirt"; break;
					case 2: task = task + "short pants"; break;
					case 3: task = task + "long pants"; break;
					case 4: task = task + "fancy dress"; break;
					case 5: task = task + "plain dress"; break;
					case 6: task = task + "kilt"; break;
					case 7: task = task + "half apron"; break;
					case 8: task = task + "loin cloth"; break;
					case 9: task = task + "cloak"; break;
					case 10: task = task + "doublet"; break;
					case 11: task = task + "tunic"; break;
					case 12: task = task + "floppy hat"; break;
					case 13: task = task + "wizard hat"; break;
					case 14: task = task + "witch hat"; break;
					case 15: task = task + "robe"; break;
					case 16: task = task + "breeches"; break;
					case 17: task = task + "stockings"; break;
				}
			}

			return task;
		}

		public TailorShoppe( Serial serial ) : base( serial )
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