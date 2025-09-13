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
	[Flipable( 0x3BF4, 0x3BF3 )]
	public class HerbalistShoppe : BaseShoppe
	{
		[Constructable]
		public HerbalistShoppe()
		{
			Name = "Herbalist Work Shoppe";
			ItemID = Utility.RandomList( 0x3BF4, 0x3BF3 );
			ShoppeName = Name;
			ShelfTitle = "HERBALIST WORK SHOPPE";
			ShelfItem = 0x3BF4;
			ShelfSkill = 55;
			ShelfGuild = NpcGuild.DruidsGuild;
			ShelfTools = "Garden Shears";
			ShelfResources = "Herbs";
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

			string[] sWords = new string[] {"abcess root", "acacia", "aconite", "acorn", "adder's tongue", "adderwort", "adrue", "agar-agar", "agaric", "agrimony", "alder", "alfalfa", "alkanet root", "almond powder", "aloe", "amaranth", "ammoniacum", "angelica", "anise", "arbutus", "areca nut", "arenaria rubra", "arrach", "asafetida", "asarabacca", "ash bark", "ash leaves", "asparagus juice", "asparagus root", "atichoke juice", "avens", "bael", "balm leaves", "balm of gilead", "balmony ", "balsam weed", "barley", "basil", "bay leaf", "beet", "belladonna", "benne", "benzoin", "berberis", "betel nut", "beth root", "bilberry", "birch", "birthwort", "bistort", "bitter aloe", "bitter herb", "bitter root", "black cherry root", "black currant", "black willow", "blackberry", "blueberry", "boneset", "borage", "box leaves", "bryony", "bugle", "burdock", "butterbur", "cabbage juice", "calotopis", "camphor", "caraway", "cardamom", "carrot juice", "carrot seeds", "castor oil bush", "catnip", "cayenne", "celery", "chamomile", "chaulmoogra oil", "cherry gum", "chervil", "chives", "cinnamon", "cleavers", "clover", "cloves", "club moss", "cockleburr", "colewort", "comfrey root", "coriander", "couchgrass", "cucumber", "cumin seed", "dandelion", "dead men's bells", "deadly nightshade", "devil's dung", "dewberry", "digitalis  ", "dill", "dwale", "ergot", "eyebright", "fairy bells", "fairy cap", "fairy fingers", "felonwort", "felwort", "fennel", "fenugreek", "fig", "figwort", "fireweed", "flag lily", "fluellin", "fox tail", "foxglove", "friar's cap", "garden burnet", "garlic", "gelsemium", "gentian", "geranium", "ginger", "ginseng", "goat's rue", "goosefoot", "goosegrass", "grape juice", "gum asafetida", "gum benzoin", "gum camphor", "hartstongue", "hawthorn", "hazelwort", "hedge mustard", "hellebore", "herb bennet", "honeysuckle", "horehound", "horseradish", "huckleberry", "hurtleberry", "hyssop", "ipecac", "irish moss", "jambul seed", "jewel weed", "juniper berry", "jurubera", "kelp", "knight's spur", "lamb's tail", "larkspur", "leek", "lily-of-the-valley", "lotus", "lucerne", "lycopodium", "madweed", "mallow", "mandragora", "marigold", "marjoram", "masterwort", "mayflower", "mistletoe", "monkshood", "mudar bark", "muira-puama", "mustard", "nutmeg", "nux vomica", "onion", "oregano", "paprika", "parsley", "parsnip", "peach seed", "pepper", "peppermint", "pitcher plant", "plantain", "poison flag", "poison nut", "pomegranate", "poppy", "pumpkin seed", "pussy willow", "quince", "radish", "raspberry", "red cockscomb", "rhubarb", "ripple grass", "rose", "rosemary", "rye", "saffron", "sage", "sandwort", "sarsaparilla", "scarlet berry", "scopolis", "scrofula", "scullcap", "seawrack", "senna", "sesame", "snake head", "spearmint", "spikenard", "stickwort", "strawberry", "summer savory", "sweet geranium", "sweet root", "tamarind", "tansy", "tarragon", "tea", "thoughtwort", "throatwort", "thyme", "turmeric", "water flag", "water lily", "watercress", "waybread", "white birch", "white bryony", "whortleberry", "wild nard", "wild woodbine", "wolfsbane", "woody nightshade", "wound-wort"};
				string sWord = sWords[Utility.RandomMinMax( 0, (sWords.Length-1) )];

			string[] sTypes = new string[] {"dragon berry", "winter berry", "earth stem", "tangle leaf", "eldritch leaf", "lotus petal", "life root", "snake weed", "white mushroom", "dark toadstool", "purple fungus", "frog bed leaf", "lilly flower petal", "deep water stem", "desert root", "cactus sponge", "vampire thorn", "forest hair", "fey seed", "druidic blade"};
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

		public HerbalistShoppe( Serial serial ) : base( serial )
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