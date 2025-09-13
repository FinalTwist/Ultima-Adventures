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
	[Flipable( 0x3CF1, 0x3CF2 )]
	public class BakerShoppe : BaseShoppe
	{
		[Constructable]
		public BakerShoppe()
		{
			Name = "Baker Work Shoppe";
			ItemID = Utility.RandomList( 0x3CF1, 0x3CF2 );
			ShoppeName = Name;
			ShelfTitle = "BAKER WORK SHOPPE";
			ShelfItem = 0x3CF1;
			ShelfSkill = 14;
			ShelfGuild = NpcGuild.CulinariansGuild;
			ShelfTools = "Pans or Rolling Pins";
			ShelfResources = "Dough";
			ShelfSound = 0x054;
		}

		public static string MakeThisTask()
		{
			string task = null;

			switch( Utility.RandomMinMax( 1, 3 ) )
			{
				case 1: task = "Make"; break;
				case 2: task = "Cook"; break;
				case 3: task = "Bake"; break;
			}

			switch( Utility.RandomMinMax( 1, 10 ) )
			{
				case 1: task = task + " a batch"; break;
				case 2: task = task + " some"; break;
				case 3: task = task + " a pack"; break;
				case 4: task = task + " a pile"; break;
				case 5: task = task + " a sack"; break;
				case 6: task = task + " a box"; break;
				case 7: task = task + " a crate"; break;
				case 8: task = task + " a bundle"; break;
				case 9: task = task + " a bunch"; break;
				case 10: task = task + " a pan"; break;
			}

			string[] sTastes = new string[] { "aged", "bitter", "bittersweet", "bland", "burnt", "buttery", "chalky", "cheesy", "chewy", "chocolaty", "citrusy", "cool", "creamy", "crispy", "crumbly", "crunchy", "crusty", "doughy", "dry", "earthy", "eggy", "fatty", "fermented", "fiery", "fishy", "fizzy", "flakey", "flat", "flavorful", "fresh", "fried", "fruity", "garlicky", "gelatinous", "gingery", "glazed", "grainy", "greasy", "gooey", "gritty", "harsh", "hearty", "heavy", "herbal", "hot", "icy", "infused", "juicy", "lean", "light", "lemony", "malty", "mashed", "meaty", "mellow", "mild", "minty", "moist", "mushy", "nutty", "oily", "oniony", "overripe", "pasty", "peppery", "pickled", "plain", "powdery", "raw", "refreshing", "rich", "ripe", "roasted", "robust", "rubbery", "runny", "salty", "sauteed", "savory", "seared", "seasoned", "sharp", "silky", "slimy", "smokey", "smothered", "smooth", "soggy", "soupy", "sour", "spicy", "spongy", "stale", "sticky", "stale", "stringy", "strong", "sugary", "sweet", "syrupy", "tangy", "tart", "tasteless", "tender", "toasted", "tough", "unflavored", "unseasoned", "velvety", "vinegary", "watery", "whipped", "woody", "yeasty", "zesty", "zingy", "amazing", "appealing", "appetizing", "delectable", "delicious", "delightful", "divine", "enjoyable", "enticing", "excellent", "exquisite", "extraordinary", "fantastic", "heavenly", "luscious", "marvelous", "mouthwatering", "palatable", "pleasant", "pleasing", "satisfying", "scrumptious", "superb", "tantalizing", "tasty", "terrific", "wonderful", "yummy" };
				string sTaste = sTastes[Utility.RandomMinMax( 0, (sTastes.Length-1) )];

			string[] sFoods = new string[] { "biscuits", "bread", "bagels", "rolls", "buns", "muffins", "brownies", "cakes", "cookies", "crackers", "custards", "pastries", "pies", "roasts", "tarts" };
				string sFood = sFoods[Utility.RandomMinMax( 0, (sFoods.Length-1) )];

			string[] sWeirds = new string[] { "ant", "ape", "baboon", "badger", "basilisk", "bear", "beaver", "beetle", "beholder", "boar", "brownie", "buffalo", "bull", "camel", "centaur", "centipede", "chimera", "cockatrice", "crocodile", "deer", "demon", "devil", "dinosaur", "djinni", "dog", "dragon", "dryad", "dwarf", "eagle", "efreet", "elemental", "elephant", "elf", "ettin", "frog", "fungi", "gargoyle", "ghast", "ghost", "ghoul", "giant", "gnoll", "gnome", "goat", "goblin", "golem", "gorgon", "griffon", "hag", "halfling", "harpy", "hell hound", "hippogriff", "hippopotamus", "hobgoblin", "horse", "hydra", "hyena", "imp", "jackal", "jaguar", "ki-rin", "kobold", "leopard", "leprechaun", "lich", "lion", "lizard", "lizardman", "lycanthrope", "lynx", "mammoth", "manticore", "mastodon", "medusa", "minotaur", "mule", "mummy", "naga", "nightmare", "ogre", "orc", "owl", "pegasus", "pixie", "porcupine", "ram", "rat", "reaper", "rhinoceros", "roc", "satyr", "scorpion", "serpent", "shadow", "skeleton", "skunk", "snake", "spectre", "sphinx", "spider", "sprite", "stag", "tiger", "titan", "toad", "troglodyte", "troll", "unicorn", "vampire", "weasel", "wight", "wisp", "wolf", "wolverine", "worm", "wraith", "wyvern", "yeti", "zombie", "zorn" };
				string sWeird = sWeirds[Utility.RandomMinMax( 0, (sWeirds.Length-1) )];

			if ( Utility.RandomMinMax( 1, 3 ) > 1 ){ sWeird = ""; } else { sWeird = " " + sWeird; }  

			task = task + " of " + sTaste + sWeird + " " + sFood;

			return task;
		}

		public BakerShoppe( Serial serial ) : base( serial )
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