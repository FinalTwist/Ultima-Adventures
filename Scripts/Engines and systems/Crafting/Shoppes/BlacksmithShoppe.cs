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
	[Flipable( 0x3CF7, 0x3CF8 )]
	public class BlacksmithShoppe : BaseShoppe
	{
		[Constructable]
		public BlacksmithShoppe()
		{
			Name = "Blacksmith Work Shoppe";
			ItemID = Utility.RandomList( 0x3CF7, 0x3CF8 );
			ShoppeName = Name;
			ShelfTitle = "BLACKSMITH WORK SHOPPE";
			ShelfItem = 0x3CF7;
			ShelfSkill = 8;
			ShelfGuild = NpcGuild.BlacksmithsGuild;
			ShelfTools = "Smithing Tools";
			ShelfResources = "Ingots";
			ShelfSound = 0x541;
		}

		public static string MakeThisTask()
		{
			string task = null;

			switch( Utility.RandomMinMax( 1, 10 ) )
			{
				case 1: task = "Repair"; break;
				case 2: task = "Fix"; break;
				case 3: task = "Buff"; break;
				case 4: task = "Modify"; break;
				case 5: task = "Polish"; break;
				case 6: task = "Engrave"; break;
				case 7: task = "Adjust"; break;
				case 8: task = "Improve"; break;
				case 9: task = "Smooth the dents from"; break;
				case 10: task = "Remove the dents from"; break;
			}

			Item item = null;

			switch( Utility.RandomMinMax( 1, 79 ) )
			{
				case 1: item = new AssassinSpike(); break;
				case 2: item = new Axe(); break;
				case 3: item = new Bardiche(); break;
				case 4: item = new Bascinet(); break;
				case 5: item = new BattleAxe(); break;
				case 6: item = new BoneHarvester(); break;
				case 7: item = new Broadsword(); break;
				case 8: item = new BronzeShield(); break;
				case 9: item = new Buckler(); break;
				case 10: item = new ButcherKnife(); break;
				case 11: item = new ChainChest(); break;
				case 12: item = new ChainCoif(); break;
				case 13: item = new ChainLegs(); break;
				case 14: item = new ChampionShield(); break;
				case 15: item = new Cleaver(); break;
				case 16: item = new CloseHelm(); break;
				case 17: item = new CloseHelm(); break;
				case 18: item = new CrescentBlade(); break;
				case 19: item = new CrestedShield(); break;
				case 20: item = new Cutlass(); break;
				case 21: item = new Dagger(); break;
				case 22: item = new DarkShield(); break;
				case 23: item = new DiamondMace(); break;
				case 24: item = new DoubleAxe(); break;
				case 25: item = new DoubleBladedStaff(); break;
				case 26: item = new DreadHelm(); break;
				case 27: item = new ElvenMachete(); break;
				case 28: item = new ElvenShield(); break;
				case 29: item = new ElvenSpellblade(); break;
				case 30: item = new ExecutionersAxe(); break;
				case 31: item = new FemalePlateChest(); break;
				case 32: item = new GuardsmanShield(); break;
				case 33: item = new Halberd(); break;
				case 34: item = new HammerPick(); break;
				case 35: item = new HeaterShield(); break;
				case 36: item = new Helmet(); break;
				case 37: item = new Helmet(); break;
				case 38: item = new JeweledShield(); break;
				case 39: item = new Katana(); break;
				case 40: item = new Kryss(); break;
				case 41: item = new Lance(); break;
				case 42: item = new LargeBattleAxe(); break;
				case 43: item = new Leafblade(); break;
				case 44: item = new Longsword(); break;
				case 45: item = new Mace(); break;
				case 46: item = new Maul(); break;
				case 47: item = new MetalKiteShield(); break;
				case 48: item = new MetalShield(); break;
				case 49: item = new NorseHelm(); break;
				case 50: item = new NorseHelm(); break;
				case 51: item = new OrnateAxe(); break;
				case 52: item = new Pickaxe(); break;
				case 53: item = new Pike(); break;
				case 54: item = new Pitchfork(); break;
				case 55: item = new PlateArms(); break;
				case 56: item = new PlateChest(); break;
				case 57: item = new PlateGloves(); break;
				case 58: item = new PlateGorget(); break;
				case 59: item = new PlateHelm(); break;
				case 60: item = new PlateHelm(); break;
				case 61: item = new PlateLegs(); break;
				case 62: item = new RadiantScimitar(); break;
				case 63: item = new RingmailArms(); break;
				case 64: item = new RingmailChest(); break;
				case 65: item = new RingmailGloves(); break;
				case 66: item = new RingmailLegs(); break;
				case 67: item = new RuneBlade(); break;
				case 68: item = new Scimitar(); break;
				case 69: item = new Scythe(); break;
				case 70: item = new ShortSpear(); break;
				case 71: item = new SkinningKnife(); break;
				case 72: item = new Spear(); break;
				case 73: item = new ThinLongsword(); break;
				case 74: item = new TwoHandedAxe(); break;
				case 75: item = new VikingSword(); break;
				case 76: item = new WarAxe(); break;
				case 77: item = new WarCleaver(); break;
				case 78: item = new WarHammer(); break;
				case 79: item = new WarMace(); break;
			}

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
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

				task = task + " their " + name;
			}
			else
			{
				string[] sMetals = new string[] { "iron ", "dull copper ", "shadow iron ", "copper ", "bronze ", "gold ", "agapite ", "verite ", "valorite ", "nepturite ", "obsidian ", "steel ", "brass ", "mithril ", "xormite ", "dwarven " };
					string sMetal = sMetals[Utility.RandomMinMax( 0, (sMetals.Length-1) )];

				string name = "item";
				if ( item.Name != null && item.Name != "" ){ name = item.Name.ToLower(); }
				if ( name == "item" ){ name = MorphingItem.AddSpacesToSentence( (item.GetType()).Name ).ToLower(); }

				task = task + " their " + sMetal + name;
			}

			item.Delete();

			return task;
		}

		public BlacksmithShoppe( Serial serial ) : base( serial )
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