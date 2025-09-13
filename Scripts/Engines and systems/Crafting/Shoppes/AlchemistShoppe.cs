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
	[Flipable( 0x3CE1, 0x3CE2 )]
	public class AlchemistShoppe : BaseShoppe
	{
		[Constructable]
		public AlchemistShoppe()
		{
			Name = "Alchemist Work Shoppe";
			ItemID = Utility.RandomList( 0x3CE1, 0x3CE2 );
			ShoppeName = Name;
			ShelfTitle = "ALCHEMIST WORK SHOPPE";
			ShelfItem = 0x3CE1;
			ShelfSkill = 1;
			ShelfGuild = NpcGuild.AlchemistsGuild;
			ShelfTools = "Mortars and Pestles";
			ShelfResources = "Reagents";
			ShelfSound = 0x240;
		}

		public static string MakeThisTask()
		{
			string task = null;

			switch( Utility.RandomMinMax( 1, 4 ) )
			{
				case 1: task = "Brew"; break;
				case 2: task = "Create"; break;
				case 3: task = "Concoct"; break;
				case 4: task = "Boil"; break;
			}

			switch( Utility.RandomMinMax( 1, 5 ) )
			{
				case 1: task = task + " an elixir"; break;
				case 2: task = task + " a potion"; break;
				case 3: task = task + " a draught"; break;
				case 4: task = task + " a mixture"; break;
				case 5: task = task + " a philter"; break;
			}

			switch( Utility.RandomMinMax( 1, 4 ) )
			{
				case 1: task = task + " with "; break;
				case 2: task = task + " using "; break;
				case 3: task = task + " mixing "; break;
				case 4: task = task + " combining "; break;
			}

			string sWord = "";

			if ( Utility.RandomMinMax( 1, 4 ) > 1 )
			{
				string[] sWord1 = new string[] {"ant", "animal", "bat", "bear", "beetle", "boar", "brownie", "bugbear", "basilisk", "bull", "froglok", "cat", "centaur", "chimera", "cow", "crocodile", "cyclops", "dark elf", "demon", "devil", "doppelganger", "dragon", "drake", "dryad", "dwarf", "elf", "ettin", "frog", "gargoyle", "ghoul", "giant", "gnoll", "gnome", "goblin", "gorilla", "gremlin", "griffin", "hag", "hobbit", "harpy", "hippogriff", "hobgoblin", "horse", "hydra", "imp", "kobold", "kraken", "leprechaun", "lizard", "lizard man", "medusa", "human", "minotaur", "mouse", "naga", "nightmare", "nixie", "ogre", "orc", "pixie", "pegasus", "phoenix", "giant lizard", "rat", "giant snake", "satyr", "scorpion", "serpent", "shark", "snake", "sphinx", "giant spider", "spider", "sylvan", "sprite", "succubus", "sylvan", "titan", "toad", "troglodite", "troll", "unicorn", "vampire", "weasel", "werebear", "wererat", "werewolf", "werecat", "wolf", "worm", "wyrm", "wyvern", "yeti", "zombie"};
					string sName1 = sWord1[Utility.RandomMinMax( 0, (sWord1.Length-1) )];
				string[] sWord2 = new string[] {"bile", "blood", "bone dust", "essence", "extract", "eyes", "hair/skin", "herbs", "juice", "oil", "powder", "salt", "sauce", "scent", "serum", "spice", "spit", "tears", "teeth", "urine"};
					string sName2 = sWord2[Utility.RandomMinMax( 0, (sWord2.Length-1) )];
				sWord = sName1 + " " + sName2;
			}
			else
			{
				string[] sWords = new string[] {"ants", "slime", "bat whiskers", "bees", "black cat hair", "black salt", "bloodworms", "cat whiskers", "centipedes", "coffin shavings", "crystal moonbeams", "cyclops eyelashes", "dragon scales", "efreet dust", "elemental dust", "eye of newt", "fairy dust", "fairy wings", "fire giant ash", "gelatinous goo", "genie smoke", "ghoul skin flakes", "graveyard dirt", "slime", "hell hound ash", "leeches", "lich dust", "love honey", "mosquitoes", "mummy spice", "mystic dust", "ochre jelly", "phoenix ash", "pixie dust", "pixie wings", "ritual powder", "sea serpent salt", "serpent scales", "snake scales", "sorcerer sand", "sprite wings", "tree leaves", "tree root", "tree sap", "vampire ash", "viper essence", "wasps", "wisp dust", "witch hazel", "worms", "zombie flesh"};
					sWord = sWords[Utility.RandomMinMax( 0, (sWords.Length-1) )];
			}

			string[] sTypes = new string[] {"black pearl","bloodmoss","garlic","ginseng","mandrake root","nightshade","spider silk","sulfurous ash","bat wing","grave dust","daemon blood","pig iron","nox crystal","silver serpent venom","dragon blood","enchanted seaweed","dragon teeth","golden serpent venom","lich dust","demon claws","unicorn horns","demigod blood","ghostly dust","eyes of toads","fairy eggs","gargoyle ears","beetle shells","moon crystals","pixie skulls","red lotus","sea salt","silver widows","swamp berries","brimstone","butterfly wings"};
				string sType = sTypes[Utility.RandomMinMax( 0, (sTypes.Length-1) )];

			switch( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: task = task + sWord + " and " + sType + " into a vial of "; break;
				case 1: task = task + sWord + " and " + sType + " into a bottle of "; break;
				case 2: task = task + sWord + " and " + sType + " into a flask of "; break;
				case 3: task = task + sWord + " and " + sType + " into a jar of "; break;
			}

			string[] sMixs = new string[] {"Acidic", "Summoning", "Scrying", "Obscure", "Iron", "Ghoulish", "Enfeebling", "Altered", "Secret", "Obscuring", "Irresistible", "Gibbering", "Enlarged", "Confusing", "Analyzing", "Sympathetic", "Secure", "Permanent", "Keen", "Glittering", "Ethereal", "Contacting", "Animal", "Telekinetic", "Seeming", "Persistent", "Lawful", "Evil", "Continual", "Animated", "Telepathic", "Shadow", "Phantasmal", "Legendary", "Good", "Expeditious", "Control", "Antimagic", "Teleporting", "Shattering", "Phantom", "Lesser", "Grasping", "Explosive", "Crushing", "Arcane", "Temporal", "Shocking", "Phasing", "Levitating", "Greater", "Fabricated", "Cursed", "Articulated", "Tiny", "Shouting", "Planar", "Limited", "Guarding", "Faithful", "Dancing", "Binding", "Transmuting", "Shrinking", "Poisonous", "Lucubrating", "Fearful", "Dazzling", "Black", "Undead", "Silent", "Polymorphing", "Magical", "Hallucinatory", "Delayed", "Blinding", "Undetectable", "Slow", "Prismatic", "Magnificent", "Hideous", "Fire", "Demanding", "Blinking", "Unseen", "Solid", "Programmed", "Major", "Holding", "Flaming", "Dimensional", "Vampiric", "Soul", "Projected", "Mass", "Horrid", "Discern", "Burning", "Vanishing", "Spectral", "Mending", "Hypnotic", "Floating", "Disintegrating", "Cat", "Protective", "Mind", "Ice", "Flying", "Disruptive", "Chain", "Spidery", "Prying", "Minor", "Illusionary", "Force", "Dominating", "Changing", "Warding", "Stinking", "Pyrotechnic", "Mirrored", "Improved", "Forceful", "Dreaming", "Chaotic", "Water", "Stone", "Rainbow", "Misdirected", "Incendiary", "Freezing", "Elemental", "Charming", "Watery", "Misleading", "Instant", "Gaseous", "Emotional", "Chilling", "Weird", "Storming", "Resilient", "Mnemonic", "Interposing", "Gentle", "Enduring", "Whispering", "Suggestive", "Reverse", "Moving", "Invisible", "Ghostly", "Energy", "Clenched", "Climbing", "Comprehending", "Colorful", "True", "False"};
				string sMix = sMixs[Utility.RandomMinMax( 0, (sMixs.Length-1) )];

			string[] sEffects = new string[] {"Acid", "Tentacles", "Sigil", "Plane", "Legend", "Gravity", "Emotion", "Chest", "Alarm", "Terrain", "Simulacrum", "Poison", "Lightning", "Grease", "Endurance", "Circle", "Anchor", "Thoughts", "Skin", "Polymorph", "Lights", "Growth", "Enervation", "Clairvoyance", "Animal", "Time", "Sleep", "Prestidigitation", "Location", "Guards", "Enfeeblement", "Clone", "Antipathy", "Tongues", "Soul", "Projection", "Lock", "Hand", "Enhancer", "Cloud", "Arcana", "Touch", "Sound", "Pyrotechnics", "Lore", "Haste", "Etherealness", "Cold", "Armor", "Transformation", "Spells", "Refuge", "Lucubration", "Hat", "Evil", "Color", "Arrows", "Trap", "Sphere", "Repulsion", "Magic", "Hound", "Evocation", "Confusion", "Aura", "Trick", "Spider", "Resistance", "Mansion", "Hypnotism", "Eye", "Conjuration", "Banishment", "Turning", "Spray", "Retreat", "Mask", "Ice", "Fall", "Contagion", "Banshee", "Undead", "Stasis", "Rope", "Maze", "Image", "Fear", "Creation", "Bear", "Vanish", "Statue", "Runes", "Message", "Imprisonment", "Feather", "Curse", "Binding", "Veil", "Steed", "Scare", "Meteor", "Insanity", "Field", "Dance", "Vision", "Stone", "Screen", "Mind", "Invisibility", "Fireball", "Darkness", "Blindness", "Vocation", "Storm", "Script", "Mirage", "Invulnerability", "Flame", "Daylight", "Blink", "Wail", "Strength", "Scrying", "Misdirection", "Iron", "Flesh", "Dead", "Blur", "Walk", "Strike", "Seeing", "Missile", "Item", "Fog", "Deafness", "Body", "Wall", "Stun", "Self", "Mist", "Jar", "Force", "Death", "Bolt", "Wards", "Suggestion", "Sending", "Monster", "Jaunt", "Foresight", "Demand", "Bond", "Water", "Summons", "Servant", "Mouth", "Jump", "Form", "Disjunction", "Breathing", "Weapon", "Sunburst", "Shadow", "Mud", "Kill", "Freedom", "Disk", "Burning", "Weather", "Swarm", "Shape", "Nightmare", "Killer", "Frost", "Dismissal", "Cage", "Web", "Symbol", "Shelter", "Object", "Knock", "Gate", "Displacement", "Chain", "Wilting", "Sympathy", "Shield", "Page", "Languages", "Good", "Door", "Chaos", "Wind", "Telekinesis", "Shift", "Pattern", "Laughter", "Grace", "Drain", "Charm", "Wish", "Teleport", "Shout", "Person", "Law", "Grasp", "Dream", "Elements", "Edge", "Earth", "Dust"};
				string sEffect = sEffects[Utility.RandomMinMax( 0, (sEffects.Length-1) )];

			task = task + sMix + " "  + sEffect;

			return task;
		}

		public AlchemistShoppe( Serial serial ) : base( serial )
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