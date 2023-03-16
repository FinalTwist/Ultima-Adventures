using System;
using System.Collections;
using Server.Misc;

namespace Server.Items
{
	public enum CraftResource
	{
		None = 0,
		Iron = 1,
		DullCopper,
		ShadowIron,
		Copper,
		Bronze,
		Gold,
		Agapite,
		Verite,
		Valorite,
		Nepturite,
		Obsidian,
		Steel,
		Brass,
		Mithril,
		Xormite,
		Dwarven,

		RegularLeather = 101,
		SpinedLeather, 
		HornedLeather, 
		BarbedLeather, 
		NecroticLeather, 
		VolcanicLeather, 
		FrozenLeather, 
		GoliathLeather, 
		DraconicLeather, 
		HellishLeather, 
		DinosaurLeather,
		AlienLeather,

		RedScales = 201,
		YellowScales,
		BlackScales,
		GreenScales,
		WhiteScales,
		BlueScales,
		DinosaurScales,

		RegularWood = 301,
		AshTree,
		CherryTree,
		EbonyTree,
		GoldenOakTree,
		HickoryTree,
		MahoganyTree,
		OakTree,
		PineTree,
		GhostTree,
		RosewoodTree,
		WalnutTree,
		PetrifiedTree,
		DriftwoodTree,
		ElvenTree
	}

	public enum CraftResourceType
	{
		None,
		Metal,
		Leather,
		Scales,
		Wood
	}

	public class CraftAttributeInfo
	{
		private int m_WeaponFireDamage;
		private int m_WeaponColdDamage;
		private int m_WeaponPoisonDamage;
		private int m_WeaponEnergyDamage;
		private int m_WeaponChaosDamage;
		private int m_WeaponDirectDamage;
		private int m_WeaponDurability;
		private int m_WeaponLuck;
		private int m_WeaponGoldIncrease;
		private int m_WeaponLowerRequirements;

		private int m_ArmorPhysicalResist;
		private int m_ArmorFireResist;
		private int m_ArmorColdResist;
		private int m_ArmorPoisonResist;
		private int m_ArmorEnergyResist;
		private int m_ArmorDurability;
		private int m_ArmorLuck;
		private int m_ArmorGoldIncrease;
		private int m_ArmorLowerRequirements;

		private int m_RunicMinAttributes;
		private int m_RunicMaxAttributes;
		private int m_RunicMinIntensity;
		private int m_RunicMaxIntensity;

		public int WeaponFireDamage{ get{ return m_WeaponFireDamage; } set{ m_WeaponFireDamage = value; } }
		public int WeaponColdDamage{ get{ return m_WeaponColdDamage; } set{ m_WeaponColdDamage = value; } }
		public int WeaponPoisonDamage{ get{ return m_WeaponPoisonDamage; } set{ m_WeaponPoisonDamage = value; } }
		public int WeaponEnergyDamage{ get{ return m_WeaponEnergyDamage; } set{ m_WeaponEnergyDamage = value; } }
		public int WeaponChaosDamage{ get{ return m_WeaponChaosDamage; } set{ m_WeaponChaosDamage = value; } }
		public int WeaponDirectDamage{ get{ return m_WeaponDirectDamage; } set{ m_WeaponDirectDamage = value; } }
		public int WeaponDurability{ get{ return m_WeaponDurability; } set{ m_WeaponDurability = value; } }
		public int WeaponLuck{ get{ return m_WeaponLuck; } set{ m_WeaponLuck = value; } }
		public int WeaponGoldIncrease{ get{ return m_WeaponGoldIncrease; } set{ m_WeaponGoldIncrease = value; } }
		public int WeaponLowerRequirements{ get{ return m_WeaponLowerRequirements; } set{ m_WeaponLowerRequirements = value; } }

		public int ArmorPhysicalResist{ get{ return m_ArmorPhysicalResist; } set{ m_ArmorPhysicalResist = value; } }
		public int ArmorFireResist{ get{ return m_ArmorFireResist; } set{ m_ArmorFireResist = value; } }
		public int ArmorColdResist{ get{ return m_ArmorColdResist; } set{ m_ArmorColdResist = value; } }
		public int ArmorPoisonResist{ get{ return m_ArmorPoisonResist; } set{ m_ArmorPoisonResist = value; } }
		public int ArmorEnergyResist{ get{ return m_ArmorEnergyResist; } set{ m_ArmorEnergyResist = value; } }
		public int ArmorDurability{ get{ return m_ArmorDurability; } set{ m_ArmorDurability = value; } }
		public int ArmorLuck{ get{ return m_ArmorLuck; } set{ m_ArmorLuck = value; } }
		public int ArmorGoldIncrease{ get{ return m_ArmorGoldIncrease; } set{ m_ArmorGoldIncrease = value; } }
		public int ArmorLowerRequirements{ get{ return m_ArmorLowerRequirements; } set{ m_ArmorLowerRequirements = value; } }

		public int RunicMinAttributes{ get{ return m_RunicMinAttributes; } set{ m_RunicMinAttributes = value; } }
		public int RunicMaxAttributes{ get{ return m_RunicMaxAttributes; } set{ m_RunicMaxAttributes = value; } }
		public int RunicMinIntensity{ get{ return m_RunicMinIntensity; } set{ m_RunicMinIntensity = value; } }
		public int RunicMaxIntensity{ get{ return m_RunicMaxIntensity; } set{ m_RunicMaxIntensity = value; } }

		public CraftAttributeInfo()
		{
		}

		public static readonly CraftAttributeInfo Blank;
		public static readonly CraftAttributeInfo DullCopper, ShadowIron, Copper, Bronze, Golden, Agapite, Verite, Valorite, Nepturite, Obsidian, Steel, Brass, Mithril, Xormite, Dwarven;
		public static readonly CraftAttributeInfo Spined, Horned, Barbed, Necrotic, Volcanic, Frozen, Goliath, Draconic, Hellish, Dinosaur, Alien;
		public static readonly CraftAttributeInfo RedScales, YellowScales, BlackScales, GreenScales, WhiteScales, BlueScales, DinosaurScales;
		public static readonly CraftAttributeInfo AshTree, CherryTree, EbonyTree, GoldenOakTree, HickoryTree, MahoganyTree, OakTree, PineTree, GhostTree, RosewoodTree, WalnutTree, PetrifiedTree, DriftwoodTree, ElvenTree;

		static CraftAttributeInfo()
		{
			Blank = new CraftAttributeInfo();

			CraftAttributeInfo dullCopper = DullCopper = new CraftAttributeInfo();

			dullCopper.ArmorPhysicalResist = 2; // 7
			dullCopper.ArmorEnergyResist = 5;
			dullCopper.ArmorDurability = 30;
			dullCopper.ArmorLowerRequirements = 20;
			dullCopper.WeaponDurability = 30;
			dullCopper.WeaponLowerRequirements = 50;
			dullCopper.RunicMinAttributes = 1;
			dullCopper.RunicMaxAttributes = 2;
				dullCopper.RunicMinIntensity = 40;
				dullCopper.RunicMaxIntensity = 60;

			CraftAttributeInfo shadowIron = ShadowIron = new CraftAttributeInfo();

			shadowIron.ArmorPhysicalResist = 2; // 9
			shadowIron.ArmorPoisonResist = 2;
			shadowIron.ArmorEnergyResist = 5;
			shadowIron.ArmorDurability = 10;
			shadowIron.WeaponColdDamage = 20;
			shadowIron.WeaponDurability = 50;
			shadowIron.RunicMinAttributes = 1;
			shadowIron.RunicMaxAttributes = 3;
				shadowIron.RunicMinIntensity = 45;
				shadowIron.RunicMaxIntensity = 70;


			CraftAttributeInfo copper = Copper = new CraftAttributeInfo();

			copper.ArmorPhysicalResist = 3; //11
			copper.ArmorFireResist = 2;
			copper.ArmorPoisonResist = 4;
			copper.ArmorEnergyResist = 2;
			copper.WeaponPoisonDamage = 10;
			copper.WeaponEnergyDamage = 20;
			copper.RunicMinAttributes = 2;
			copper.RunicMaxAttributes = 3;
				copper.RunicMinIntensity = 50;
				copper.RunicMaxIntensity = 75;

			CraftAttributeInfo bronze = Bronze = new CraftAttributeInfo();

			bronze.ArmorPhysicalResist = 3; //13
			bronze.ArmorColdResist = 5;
			bronze.ArmorPoisonResist = 2;
			bronze.ArmorEnergyResist = 3;
			bronze.WeaponFireDamage = 40;
			bronze.RunicMinAttributes = 2;
			bronze.RunicMaxAttributes = 4;
				bronze.RunicMinIntensity = 55;
				bronze.RunicMaxIntensity = 80;

			CraftAttributeInfo golden = Golden = new CraftAttributeInfo();

			golden.ArmorPhysicalResist = 4; //15
			golden.ArmorFireResist = 5;
			golden.ArmorColdResist = 2;
			golden.ArmorEnergyResist = 4;
			golden.ArmorLuck = 40;
			golden.ArmorLowerRequirements = 30;
			golden.WeaponLuck = 40;
			golden.WeaponLowerRequirements = 50;
			golden.RunicMinAttributes = 3;
			golden.RunicMaxAttributes = 6;
				golden.RunicMinIntensity = 60;
				golden.RunicMaxIntensity = 90;

			CraftAttributeInfo agapite = Agapite = new CraftAttributeInfo();

			agapite.ArmorPhysicalResist = 5; //17
			agapite.ArmorFireResist = 3;
			agapite.ArmorColdResist = 3;
			agapite.ArmorPoisonResist = 3;
			agapite.ArmorEnergyResist = 3;
			agapite.WeaponColdDamage = 30;
			agapite.WeaponEnergyDamage = 20;
			agapite.RunicMinAttributes = 3;
			agapite.RunicMaxAttributes = 7;

				agapite.RunicMinIntensity = 65;
				agapite.RunicMaxIntensity = 90;

			CraftAttributeInfo verite = Verite = new CraftAttributeInfo();

			verite.ArmorPhysicalResist = 5; //19
			verite.ArmorFireResist = 4;
			verite.ArmorColdResist = 4;
			verite.ArmorPoisonResist = 5;
			verite.ArmorEnergyResist = 1;
			verite.WeaponPoisonDamage = 40;
			verite.WeaponEnergyDamage = 20;
			verite.RunicMinAttributes = 4;
			verite.RunicMaxAttributes = 8;
				verite.RunicMinIntensity = 80;
				verite.RunicMaxIntensity = 100;

			CraftAttributeInfo valorite = Valorite = new CraftAttributeInfo();

			valorite.ArmorPhysicalResist = 6; //21
			valorite.ArmorColdResist = 5;
			valorite.ArmorPoisonResist = 5;
			valorite.ArmorEnergyResist = 5;
			valorite.ArmorDurability = 15;
			valorite.WeaponFireDamage = 10;
			valorite.WeaponColdDamage = 20;
			valorite.WeaponPoisonDamage = 10;
			valorite.WeaponEnergyDamage = 20;
			valorite.RunicMinAttributes = 5;
			valorite.RunicMaxAttributes = 9;
				valorite.RunicMinIntensity = 90;
				valorite.RunicMaxIntensity = 100;

			CraftAttributeInfo nepturite = Nepturite = new CraftAttributeInfo();

			nepturite.ArmorPhysicalResist = 6; //24
			nepturite.ArmorColdResist = 8;
			nepturite.ArmorPoisonResist = 10;
			nepturite.ArmorDurability = 20;
			nepturite.WeaponColdDamage = 25;
			nepturite.WeaponPoisonDamage = 25;
			nepturite.RunicMinAttributes = 5;
			nepturite.RunicMaxAttributes = 6;
				nepturite.RunicMinIntensity = 80;
				nepturite.RunicMaxIntensity = 100;

			CraftAttributeInfo obsidian = Obsidian = new CraftAttributeInfo();

			obsidian.ArmorPhysicalResist = 7; //27
			obsidian.ArmorColdResist = 4;
			obsidian.ArmorPoisonResist = 4;
			obsidian.ArmorFireResist = 12;
			obsidian.ArmorEnergyResist = 9;
			obsidian.ArmorDurability = 25;
			obsidian.WeaponFireDamage = 20;
			obsidian.WeaponEnergyDamage = 10;
			obsidian.RunicMinAttributes = 5;
			obsidian.RunicMaxAttributes = 6;
				obsidian.RunicMinIntensity = 85;
				obsidian.RunicMaxIntensity = 100;

			CraftAttributeInfo steel = Steel = new CraftAttributeInfo();

			steel.ArmorPhysicalResist = 5;  //13
			steel.ArmorColdResist = 3;
			steel.ArmorPoisonResist = 3;
			steel.ArmorEnergyResist = 2;
			steel.ArmorDurability = 10;
			steel.RunicMinAttributes = 5;
			steel.RunicMaxAttributes = 7;
			steel.WeaponDurability = 10;
			steel.WeaponLowerRequirements = 25;
			steel.ArmorLowerRequirements = 25;
				steel.RunicMinIntensity = 85;
				steel.RunicMaxIntensity = 100;

			CraftAttributeInfo brass = Brass = new CraftAttributeInfo();

			brass.ArmorPhysicalResist = 6; //17
			brass.ArmorColdResist = 4;
			brass.ArmorPoisonResist = 4;
			brass.ArmorEnergyResist = 3;
			brass.ArmorDurability = 10;
			brass.WeaponFireDamage = 20;
			brass.WeaponEnergyDamage = 20;
			brass.RunicMinAttributes = 5;
			brass.RunicMaxAttributes = 7;
			brass.WeaponDurability = 10;
			brass.WeaponLowerRequirements = 45;
			brass.ArmorLowerRequirements = 45;
				brass.RunicMinIntensity = 90;
				brass.RunicMaxIntensity = 100;

			CraftAttributeInfo mithril = Mithril = new CraftAttributeInfo();

			mithril.ArmorPhysicalResist = 15; //30
			mithril.ArmorColdResist = 15;
			mithril.ArmorLuck = 100;
			mithril.WeaponLuck = 100;
			mithril.ArmorPoisonResist = 9;
			mithril.ArmorEnergyResist = 9;
			mithril.ArmorDurability = 30;
			mithril.WeaponEnergyDamage = 30;
			mithril.RunicMinAttributes = 6;
			mithril.RunicMaxAttributes = 7;
			mithril.WeaponDurability = 30;
			mithril.WeaponLowerRequirements = 75;
			mithril.ArmorLowerRequirements = 75;
				mithril.RunicMinIntensity = 90;
				mithril.RunicMaxIntensity = 100;

			CraftAttributeInfo dwarven = Dwarven = new CraftAttributeInfo();

			dwarven.ArmorPhysicalResist = 16; //33
			dwarven.ArmorFireResist = 10;
			dwarven.ArmorColdResist = 7;
			dwarven.ArmorDurability = 35;
			dwarven.RunicMinAttributes = 6;
			dwarven.RunicMaxAttributes = 7;
			dwarven.WeaponDurability = 35;
				dwarven.RunicMinIntensity = 100;
				dwarven.RunicMaxIntensity = 120;

			CraftAttributeInfo xormite = Xormite = new CraftAttributeInfo();

			xormite.ArmorPhysicalResist = 15; //54
			xormite.ArmorColdResist = 8;
			xormite.ArmorPoisonResist = 8;
			xormite.ArmorFireResist = 8;
			xormite.ArmorEnergyResist = 15;
			xormite.ArmorDurability = 20;
			xormite.WeaponDirectDamage = 30;
			xormite.RunicMinAttributes = 6;
			xormite.RunicMaxAttributes = 8;
			xormite.WeaponDurability = 20;
			xormite.WeaponLowerRequirements = 75;
			xormite.ArmorLowerRequirements = 75;
				xormite.RunicMinIntensity = 100;
				xormite.RunicMaxIntensity = 150;


			CraftAttributeInfo spined = Spined = new CraftAttributeInfo();

			spined.ArmorDurability = 5;
			spined.WeaponDurability = 5;
			spined.ArmorPhysicalResist = 0; // 5
			spined.ArmorColdResist = 2;
			spined.ArmorPoisonResist = 2;
			spined.ArmorEnergyResist = 1;
			spined.ArmorLuck = 40;
			spined.WeaponLuck = 40;
			spined.WeaponPoisonDamage = 20;
			spined.RunicMinAttributes = 1;
			spined.RunicMaxAttributes = 2;
				spined.RunicMinIntensity = 40;
				spined.RunicMaxIntensity = 60;


			CraftAttributeInfo horned = Horned = new CraftAttributeInfo();

			horned.ArmorDurability = 10;
			horned.WeaponDurability = 10;
			horned.ArmorPhysicalResist = 1; //7
			horned.ArmorColdResist = 3;
			horned.ArmorPoisonResist = 3;
			horned.RunicMinAttributes = 2;
			horned.RunicMaxAttributes = 4;
				horned.RunicMinIntensity = 60;
				horned.RunicMaxIntensity = 90;

			CraftAttributeInfo barbed = Barbed = new CraftAttributeInfo();

			barbed.ArmorDurability = 20;
			barbed.WeaponDurability = 20;
			barbed.ArmorPhysicalResist = 1; //9
			barbed.ArmorFireResist = 3;
			barbed.ArmorColdResist = 3;
			barbed.ArmorPoisonResist = 2;
			barbed.WeaponPoisonDamage = 70;
			barbed.RunicMinAttributes = 4;
			barbed.RunicMaxAttributes = 8;
				barbed.RunicMinIntensity = 90;
				barbed.RunicMaxIntensity = 100;

			CraftAttributeInfo necrotic = Necrotic = new CraftAttributeInfo();

			necrotic.ArmorDurability = 5;
			necrotic.WeaponDurability = 5;
			necrotic.ArmorPhysicalResist = 1; //10
			necrotic.ArmorColdResist = 4;
			necrotic.ArmorPoisonResist = 3;
			necrotic.ArmorEnergyResist = 2;
			necrotic.WeaponFireDamage = 50;
			necrotic.RunicMinAttributes = 4;
			necrotic.RunicMaxAttributes = 6;
				necrotic.RunicMinIntensity = 80;
				necrotic.RunicMaxIntensity = 100;

			CraftAttributeInfo volcanic = Volcanic = new CraftAttributeInfo();

			volcanic.ArmorDurability = 15;
			volcanic.WeaponDurability = 15;
			volcanic.ArmorPhysicalResist = 2; //11
			volcanic.ArmorFireResist = 6;
			volcanic.ArmorPoisonResist = 3;
			volcanic.WeaponFireDamage = 50;
			volcanic.RunicMinAttributes = 4;
			volcanic.RunicMaxAttributes = 6;
				volcanic.RunicMinIntensity = 90;
				volcanic.RunicMaxIntensity = 100;

			CraftAttributeInfo frozen = Frozen = new CraftAttributeInfo();

			frozen.ArmorDurability = 10;
			frozen.WeaponDurability = 10;
			frozen.ArmorPhysicalResist = 2; //12
			frozen.ArmorColdResist = 6;
			frozen.ArmorEnergyResist = 4;
			frozen.WeaponColdDamage = 50;
			frozen.RunicMinAttributes = 4;
			frozen.RunicMaxAttributes = 6;
				frozen.RunicMinIntensity = 90;
				frozen.RunicMaxIntensity = 100;

			CraftAttributeInfo goliath = Goliath = new CraftAttributeInfo();

			goliath.ArmorDurability = 25;
			goliath.WeaponDurability = 25;
			goliath.ArmorPhysicalResist = 4; //13
			goliath.ArmorFireResist = 3;
			goliath.ArmorColdResist = 3;
			goliath.ArmorEnergyResist = 3;
			goliath.WeaponEnergyDamage = 25;
			goliath.RunicMinAttributes = 4;
			goliath.RunicMaxAttributes = 6;
				goliath.RunicMinIntensity = 90;
				goliath.RunicMaxIntensity = 100;

			CraftAttributeInfo draconic = Draconic = new CraftAttributeInfo();

			draconic.ArmorDurability = 25;
			draconic.WeaponDurability = 25;
			draconic.ArmorPhysicalResist = 2; //14
			draconic.ArmorFireResist = 6;
			draconic.ArmorPoisonResist = 3;
			draconic.ArmorEnergyResist = 3;
			draconic.WeaponFireDamage = 25;
			draconic.RunicMinAttributes = 4;
			draconic.RunicMaxAttributes = 6;
				draconic.RunicMinIntensity = 90;
				draconic.RunicMaxIntensity = 100;

			CraftAttributeInfo hellish = Hellish = new CraftAttributeInfo();

			hellish.ArmorDurability = 10;
			hellish.WeaponDurability = 10;
			hellish.ArmorPhysicalResist = 3; //15
			hellish.ArmorColdResist = 4;
			hellish.ArmorPoisonResist = 2;
			hellish.ArmorEnergyResist = 6;
			hellish.WeaponFireDamage = 50;
			hellish.RunicMinAttributes = 4;
			hellish.RunicMaxAttributes = 6;
				hellish.RunicMinIntensity = 90;
				hellish.RunicMaxIntensity = 100;

			CraftAttributeInfo dinosaur = Dinosaur = new CraftAttributeInfo();

			dinosaur.ArmorDurability = 40;
			dinosaur.WeaponDurability = 40;
			dinosaur.ArmorPhysicalResist = 3; //16
			dinosaur.ArmorFireResist = 4;
			dinosaur.ArmorColdResist = 4;
			dinosaur.ArmorPoisonResist = 5;
			dinosaur.RunicMinAttributes = 5;
			dinosaur.RunicMaxAttributes = 7;
				dinosaur.RunicMinIntensity = 90;
				dinosaur.RunicMaxIntensity = 100;

			CraftAttributeInfo alien = Alien = new CraftAttributeInfo();

			alien.ArmorDurability = 30;
			alien.WeaponDurability = 30;
			alien.ArmorPhysicalResist = 5; //17
			alien.ArmorFireResist = 7;
			alien.ArmorColdResist = 7;
			alien.ArmorPoisonResist = 6;
			alien.ArmorEnergyResist = 7;
			alien.RunicMinAttributes = 6;
			alien.RunicMaxAttributes = 8;
				alien.RunicMinIntensity = 100;
				alien.RunicMaxIntensity = 150;

			CraftAttributeInfo red = RedScales = new CraftAttributeInfo();

			red.ArmorPhysicalResist = 5; //25
			red.ArmorFireResist = 8;
			red.ArmorColdResist = 8;
			red.ArmorPoisonResist = 2;
			red.ArmorEnergyResist = 2;

			CraftAttributeInfo yellow = YellowScales = new CraftAttributeInfo();

			yellow.ArmorPhysicalResist = 5; //25
			yellow.ArmorFireResist = 4;
			yellow.ArmorColdResist = 4;
			yellow.ArmorPoisonResist = 4;
			yellow.ArmorEnergyResist = 4;
            yellow.ArmorGoldIncrease = 4;

            CraftAttributeInfo black = BlackScales = new CraftAttributeInfo();

			black.ArmorPhysicalResist = 6; //30
			black.ArmorFireResist = 3;
			black.ArmorColdResist = 3;
			black.ArmorPoisonResist = 9;
			black.ArmorEnergyResist = 9;

			CraftAttributeInfo green = GreenScales = new CraftAttributeInfo();

			green.ArmorPhysicalResist = 6; //30
			green.ArmorFireResist = 3;
			green.ArmorColdResist = 9;
			green.ArmorPoisonResist = 9;
			green.ArmorEnergyResist = 3;

			CraftAttributeInfo white = WhiteScales = new CraftAttributeInfo();

			white.ArmorPhysicalResist = 6; //30
			white.ArmorFireResist = 9;
			white.ArmorColdResist = 3;
			white.ArmorPoisonResist = 3;
			white.ArmorEnergyResist = 9;

			CraftAttributeInfo blue = BlueScales = new CraftAttributeInfo();

			blue.ArmorPhysicalResist = 9; //30
			blue.ArmorFireResist = 9;
			blue.ArmorColdResist = 3;
			blue.ArmorPoisonResist = 3;
			blue.ArmorEnergyResist = 6;

			CraftAttributeInfo dino = DinosaurScales = new CraftAttributeInfo();

			dino.ArmorPhysicalResist = 11; //35
			dino.ArmorFireResist = 6;
			dino.ArmorColdResist = 6;
			dino.ArmorPoisonResist = 6;
			dino.ArmorEnergyResist = 6;

			CraftAttributeInfo ashtree = AshTree = new CraftAttributeInfo();

			ashtree.ArmorPhysicalResist = 1; //5
			ashtree.ArmorFireResist = 1;
			ashtree.ArmorColdResist = 1;
			ashtree.ArmorPoisonResist = 1;
			ashtree.ArmorEnergyResist = 1;
			ashtree.WeaponFireDamage = 5;
			ashtree.WeaponColdDamage = 5;
			ashtree.WeaponPoisonDamage = 5;
			ashtree.WeaponEnergyDamage = 5;
			ashtree.RunicMinAttributes = 1;
			ashtree.RunicMaxAttributes = 2;
				ashtree.RunicMinIntensity = 45;
				ashtree.RunicMaxIntensity = 60;

			CraftAttributeInfo cherrytree = CherryTree = new CraftAttributeInfo();

			cherrytree.ArmorPhysicalResist = 1; //7
			cherrytree.ArmorFireResist = 2;
			cherrytree.ArmorColdResist = 1;
			cherrytree.ArmorPoisonResist = 2;
			cherrytree.ArmorEnergyResist = 1;
			cherrytree.WeaponPoisonDamage = 10;
			cherrytree.WeaponEnergyDamage = 20;
			cherrytree.RunicMinAttributes = 1;
			cherrytree.RunicMaxAttributes = 2;
				cherrytree.RunicMinIntensity = 50;
				cherrytree.RunicMaxIntensity = 80;

			CraftAttributeInfo ebonytree = EbonyTree = new CraftAttributeInfo();

			ebonytree.ArmorPhysicalResist = 3; //8
			ebonytree.ArmorFireResist = 3;
			ebonytree.ArmorEnergyResist = 2;
			ebonytree.ArmorDurability = 20;
			ebonytree.WeaponColdDamage = 20;
			ebonytree.WeaponDurability = 20;
			ebonytree.RunicMinAttributes = 2;
			ebonytree.RunicMaxAttributes = 2;
				ebonytree.RunicMinIntensity = 50;
				ebonytree.RunicMaxIntensity = 80;

			CraftAttributeInfo goldenoaktree = GoldenOakTree = new CraftAttributeInfo();

			goldenoaktree.ArmorPhysicalResist = 3; //10
			goldenoaktree.ArmorFireResist = 2;
			goldenoaktree.ArmorColdResist = 3;
			goldenoaktree.ArmorEnergyResist = 2;
			goldenoaktree.ArmorLuck = 40;
			goldenoaktree.ArmorLowerRequirements = 30;
			goldenoaktree.WeaponLuck = 40;
			goldenoaktree.WeaponLowerRequirements = 50;
			goldenoaktree.RunicMinAttributes = 2;
			goldenoaktree.RunicMaxAttributes = 3;
				goldenoaktree.RunicMinIntensity = 60;
				goldenoaktree.RunicMaxIntensity = 80;

			CraftAttributeInfo hickorytree = HickoryTree = new CraftAttributeInfo();

			hickorytree.ArmorPhysicalResist = 5; //12
			goldenoaktree.ArmorColdResist = 7;
			hickorytree.ArmorDurability = 20;
			hickorytree.ArmorLowerRequirements = 20;
			hickorytree.WeaponDurability = 20;
			hickorytree.WeaponLowerRequirements = 50;
			hickorytree.RunicMinAttributes = 2;
			hickorytree.RunicMaxAttributes = 3;
				hickorytree.RunicMinIntensity = 60;
				hickorytree.RunicMaxIntensity = 80;

			CraftAttributeInfo mahoganytree = MahoganyTree = new CraftAttributeInfo();

			mahoganytree.ArmorPhysicalResist = 7; //15
			mahoganytree.ArmorFireResist = 3;
			mahoganytree.ArmorPoisonResist = 3;
			mahoganytree.ArmorEnergyResist = 2;
			mahoganytree.WeaponPoisonDamage = 10;
			mahoganytree.WeaponEnergyDamage = 20;
			mahoganytree.RunicMinAttributes = 2;
			mahoganytree.RunicMaxAttributes = 3;
				mahoganytree.RunicMinIntensity = 60;
				mahoganytree.RunicMaxIntensity = 80;

			CraftAttributeInfo oaktree = OakTree = new CraftAttributeInfo();

			oaktree.ArmorPhysicalResist = 3; //16
			oaktree.ArmorColdResist = 5;
			oaktree.ArmorPoisonResist = 5;
			oaktree.ArmorEnergyResist = 3;
			oaktree.WeaponFireDamage = 40;
			oaktree.RunicMinAttributes = 3;
			oaktree.RunicMaxAttributes = 3;
				oaktree.RunicMinIntensity = 70;
				oaktree.RunicMaxIntensity = 90;

			CraftAttributeInfo pinetree = PineTree = new CraftAttributeInfo();

			pinetree.ArmorPhysicalResist = 3; //19
			pinetree.ArmorFireResist = 3;
			pinetree.ArmorColdResist = 3;
			pinetree.ArmorPoisonResist = 5;
			pinetree.ArmorEnergyResist = 5;
			pinetree.WeaponColdDamage = 30;
			pinetree.WeaponEnergyDamage = 20;
			pinetree.RunicMinAttributes = 3;
			pinetree.RunicMaxAttributes = 4;
				pinetree.RunicMinIntensity = 70;
				pinetree.RunicMaxIntensity = 90;

			CraftAttributeInfo rosewoodtree = RosewoodTree = new CraftAttributeInfo();

			rosewoodtree.ArmorPhysicalResist = 5; //24
			rosewoodtree.ArmorFireResist = 5;
			rosewoodtree.ArmorColdResist = 3;
			rosewoodtree.ArmorPoisonResist = 8;
			rosewoodtree.ArmorEnergyResist = 3;
			rosewoodtree.WeaponPoisonDamage = 40;
			rosewoodtree.WeaponEnergyDamage = 20;
			rosewoodtree.RunicMinAttributes = 3;
			rosewoodtree.RunicMaxAttributes = 5;
				rosewoodtree.RunicMinIntensity = 70;
				rosewoodtree.RunicMaxIntensity = 90;

			CraftAttributeInfo walnuttree = WalnutTree = new CraftAttributeInfo(); 

			walnuttree.ArmorPhysicalResist = 10;//23
			walnuttree.ArmorColdResist = 5;
			walnuttree.ArmorPoisonResist = 4;
			walnuttree.ArmorEnergyResist = 4;
			walnuttree.ArmorDurability = 10;
			walnuttree.WeaponFireDamage = 10;
			walnuttree.WeaponColdDamage = 20;
			walnuttree.WeaponPoisonDamage = 10;
			walnuttree.WeaponEnergyDamage = 20;
			walnuttree.RunicMinAttributes = 4;
			walnuttree.RunicMaxAttributes = 5;
				walnuttree.RunicMinIntensity = 70;
				walnuttree.RunicMaxIntensity = 100;


			CraftAttributeInfo driftwoodtree = DriftwoodTree = new CraftAttributeInfo(); //4

			driftwoodtree.ArmorPhysicalResist = 9; //33
			driftwoodtree.ArmorColdResist = 8;
			driftwoodtree.ArmorPoisonResist = 10;
			driftwoodtree.ArmorEnergyResist = 5;
			driftwoodtree.ArmorDurability = 5;
			driftwoodtree.WeaponFireDamage = 10;
			driftwoodtree.WeaponColdDamage = 10;
			driftwoodtree.WeaponPoisonDamage = 20;
			driftwoodtree.WeaponEnergyDamage = 10;
			driftwoodtree.RunicMinAttributes = 5;
			driftwoodtree.RunicMaxAttributes = 6;
				driftwoodtree.RunicMinIntensity = 90;
				driftwoodtree.RunicMaxIntensity = 100;
				
				
			CraftAttributeInfo ghosttree = GhostTree = new CraftAttributeInfo(); //3

			ghosttree.ArmorPhysicalResist = 9; //20
			ghosttree.ArmorFireResist = 5;
			ghosttree.ArmorColdResist = 9;
			ghosttree.ArmorPoisonResist = 9;
			ghosttree.ArmorEnergyResist = 6;
			ghosttree.WeaponColdDamage = 25;
			ghosttree.WeaponEnergyDamage = 25;
			ghosttree.RunicMinAttributes = 3;
			ghosttree.RunicMaxAttributes = 4;
				ghosttree.RunicMinIntensity = 70;
				ghosttree.RunicMaxIntensity = 90;
				
				
			CraftAttributeInfo petrifiedtree = PetrifiedTree = new CraftAttributeInfo(); //2

			petrifiedtree.ArmorPhysicalResist = 10; //30
			petrifiedtree.ArmorColdResist = 5;
			petrifiedtree.ArmorPoisonResist = 5;
			petrifiedtree.ArmorEnergyResist = 10;
			petrifiedtree.ArmorDurability = 30;
			petrifiedtree.WeaponColdDamage = 25;
			petrifiedtree.RunicMinAttributes = 5;
			petrifiedtree.RunicMaxAttributes = 5;
				petrifiedtree.RunicMinIntensity = 80;
				petrifiedtree.RunicMaxIntensity = 100;


			CraftAttributeInfo elventree = ElvenTree = new CraftAttributeInfo(); //1

			elventree.ArmorPhysicalResist = 10;   //39
			elventree.ArmorFireResist = 3;			
			elventree.ArmorPoisonResist = 11;
			elventree.ArmorEnergyResist = 15;
			elventree.ArmorDurability = 25;
			elventree.ArmorLuck = 100;
			elventree.WeaponLuck = 100;
			elventree.WeaponFireDamage = 0;
			elventree.WeaponColdDamage = 0;
			elventree.WeaponPoisonDamage = 0;
			elventree.WeaponEnergyDamage = 0;
			elventree.RunicMinAttributes = 6;
			elventree.RunicMaxAttributes = 8;
				elventree.RunicMinIntensity = 100;
				elventree.RunicMaxIntensity = 150;
		}
	}

	public class CraftResourceInfo
	{
		private int m_Hue;
		private int m_Number;
		private string m_Name;
		private CraftAttributeInfo m_AttributeInfo;
		private CraftResource m_Resource;
		private Type[] m_ResourceTypes;

		public int Hue{ get{ return m_Hue; } }
		public int Number{ get{ return m_Number; } }
		public string Name{ get{ return m_Name; } }
		public CraftAttributeInfo AttributeInfo{ get{ return m_AttributeInfo; } }
		public CraftResource Resource{ get{ return m_Resource; } }
		public Type[] ResourceTypes{ get{ return m_ResourceTypes; } }

		public CraftResourceInfo( int hue, int number, string name, CraftAttributeInfo attributeInfo, CraftResource resource, params Type[] resourceTypes )
		{
			m_Hue = hue;
			m_Number = number;
			m_Name = name;
			m_AttributeInfo = attributeInfo;
			m_Resource = resource;
			m_ResourceTypes = resourceTypes;

			for ( int i = 0; i < resourceTypes.Length; ++i )
				CraftResources.RegisterType( resourceTypes[i], resource );
		}
	}

	public class CraftResources
	{
		private static CraftResourceInfo[] m_MetalInfo = new CraftResourceInfo[]
			{
				new CraftResourceInfo( 0x000, 													1053109, "Iron",		CraftAttributeInfo.Blank,		CraftResource.Iron,				typeof( IronIngot ),		typeof( IronOre ),			typeof( Granite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "dull copper", "", 0 ), 	1053108, "Dull Copper",	CraftAttributeInfo.DullCopper,	CraftResource.DullCopper,		typeof( DullCopperIngot ),	typeof( DullCopperOre ),	typeof( DullCopperGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ), 	1053107, "Shadow Iron",	CraftAttributeInfo.ShadowIron,	CraftResource.ShadowIron,		typeof( ShadowIronIngot ),	typeof( ShadowIronOre ),	typeof( ShadowIronGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "copper", "", 0 ), 		1053106, "Copper",		CraftAttributeInfo.Copper,		CraftResource.Copper,			typeof( CopperIngot ),		typeof( CopperOre ),		typeof( CopperGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "bronze", "", 0 ), 		1053105, "Bronze",		CraftAttributeInfo.Bronze,		CraftResource.Bronze,			typeof( BronzeIngot ),		typeof( BronzeOre ),		typeof( BronzeGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "gold", "", 0 ), 			1053104, "Gold",		CraftAttributeInfo.Golden,		CraftResource.Gold,				typeof( GoldIngot ),		typeof( GoldOre ),			typeof( GoldGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "agapite", "", 0 ), 		1053103, "Agapite",		CraftAttributeInfo.Agapite,		CraftResource.Agapite,			typeof( AgapiteIngot ),		typeof( AgapiteOre ),		typeof( AgapiteGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "verite", "", 0 ), 		1053102, "Verite",		CraftAttributeInfo.Verite,		CraftResource.Verite,			typeof( VeriteIngot ),		typeof( VeriteOre ),		typeof( VeriteGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "valorite", "", 0 ), 		1053101, "Valorite",	CraftAttributeInfo.Valorite,	CraftResource.Valorite,			typeof( ValoriteIngot ),	typeof( ValoriteOre ),		typeof( ValoriteGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "nepturite", "", 0 ), 	1036175, "Nepturite",	CraftAttributeInfo.Nepturite,	CraftResource.Nepturite,		typeof( NepturiteIngot ),	typeof( NepturiteOre ),		typeof( NepturiteGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "obsidian", "", 0 ), 		1036165, "Obsidian",	CraftAttributeInfo.Obsidian,	CraftResource.Obsidian,			typeof( ObsidianIngot ),	typeof( ObsidianOre ),		typeof( ObsidianGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "steel", "", 0 ), 		1036146, "Steel",		CraftAttributeInfo.Steel,		CraftResource.Steel,			typeof( SteelIngot ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "brass", "", 0 ), 		1036154, "Brass",		CraftAttributeInfo.Brass,		CraftResource.Brass,			typeof( BrassIngot ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "mithril", "", 0 ), 		1036139, "Mithril",		CraftAttributeInfo.Mithril,		CraftResource.Mithril,			typeof( MithrilIngot ),		typeof( MithrilOre ),		typeof( MithrilGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "xormite", "", 0 ), 		1034439, "Xormite",		CraftAttributeInfo.Xormite,		CraftResource.Xormite,			typeof( XormiteIngot ),		typeof( XormiteOre ),		typeof( XormiteGranite ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "dwarven", "", 0 ), 		1036183, "Dwarven",		CraftAttributeInfo.Dwarven,		CraftResource.Dwarven,			typeof( DwarvenIngot ),		typeof( DwarvenOre ),		typeof( DwarvenGranite ) )
			};

		private static CraftResourceInfo[] m_ScaleInfo = new CraftResourceInfo[]
			{
				new CraftResourceInfo( 0x66D, 1053129, "Red Scales",		CraftAttributeInfo.RedScales,		CraftResource.RedScales,		typeof( RedScales ) ),
				new CraftResourceInfo( 0x8A8, 1053130, "Yellow Scales",		CraftAttributeInfo.YellowScales,	CraftResource.YellowScales,		typeof( YellowScales ) ),
				new CraftResourceInfo( 0x455, 1053131, "Black Scales",		CraftAttributeInfo.BlackScales,		CraftResource.BlackScales,		typeof( BlackScales ) ),
				new CraftResourceInfo( 0x851, 1053132, "Green Scales",		CraftAttributeInfo.GreenScales,		CraftResource.GreenScales,		typeof( GreenScales ) ),
				new CraftResourceInfo( 0x8FD, 1053133, "White Scales",		CraftAttributeInfo.WhiteScales,		CraftResource.WhiteScales,		typeof( WhiteScales ) ),
				new CraftResourceInfo( 0x8B0, 1053134, "Blue Scales",		CraftAttributeInfo.BlueScales,		CraftResource.BlueScales,		typeof( BlueScales ) ),
				new CraftResourceInfo( 0x430, 1054016, "Dinosaur Scales",	CraftAttributeInfo.DinosaurScales,	CraftResource.DinosaurScales,	typeof( DinosaurScales ) )
			};

		private static CraftResourceInfo[] m_AOSLeatherInfo = new CraftResourceInfo[]
			{
				new CraftResourceInfo( 0x000, 												1049353, "Normal",		CraftAttributeInfo.Blank,		CraftResource.RegularLeather,	typeof( Leather ),			typeof( Hides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "deep sea", "", 0 ), 	1049354, "Deep Sea",	CraftAttributeInfo.Spined,		CraftResource.SpinedLeather,	typeof( SpinedLeather ),	typeof( SpinedHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "lizard", "", 0 ), 	1049355, "Lizard",		CraftAttributeInfo.Horned,		CraftResource.HornedLeather,	typeof( HornedLeather ),	typeof( HornedHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "serpent", "", 0 ), 	1049356, "Serpent",		CraftAttributeInfo.Barbed,		CraftResource.BarbedLeather,	typeof( BarbedLeather ),	typeof( BarbedHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "necrotic", "", 0 ), 	1034404, "Necrotic",	CraftAttributeInfo.Necrotic,	CraftResource.NecroticLeather,	typeof( NecroticLeather ),	typeof( NecroticHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "volcanic", "", 0 ), 	1034415, "Volcanic",	CraftAttributeInfo.Volcanic,	CraftResource.VolcanicLeather,	typeof( VolcanicLeather ),	typeof( VolcanicHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "frozen", "", 0 ), 	1034426, "Frozen",		CraftAttributeInfo.Frozen,		CraftResource.FrozenLeather,	typeof( FrozenLeather ),	typeof( FrozenHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "goliath", "", 0 ), 	1034371, "Goliath",		CraftAttributeInfo.Goliath,		CraftResource.GoliathLeather,	typeof( GoliathLeather ),	typeof( GoliathHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "draconic", "", 0 ), 	1034382, "Draconic",	CraftAttributeInfo.Draconic,	CraftResource.DraconicLeather,	typeof( DraconicLeather ),	typeof( DraconicHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "hellish", "", 0 ), 	1034393, "Hellish",		CraftAttributeInfo.Hellish,		CraftResource.HellishLeather,	typeof( HellishLeather ),	typeof( HellishHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "dinosaur", "", 0 ), 	1036105, "Dinosaur",	CraftAttributeInfo.Dinosaur,	CraftResource.DinosaurLeather,	typeof( DinosaurLeather ),	typeof( DinosaurHides ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "alien", "", 0 ), 	1034445, "Alien",		CraftAttributeInfo.Alien,		CraftResource.AlienLeather,		typeof( AlienLeather ),		typeof( AlienHides ) )
			};

		private static CraftResourceInfo[] m_WoodInfo = new CraftResourceInfo[]
			{
				new CraftResourceInfo( 0x000, 													1011542,	"Normal",		CraftAttributeInfo.Blank,			CraftResource.RegularWood,		typeof( Log ),			typeof( Board ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "ash", "", 0 ),			1095399,	"Ash",			CraftAttributeInfo.AshTree,			CraftResource.AshTree,			typeof( AshLog ),		typeof( AshBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "cherry", "", 0 ),		1095400,	"Cherry",		CraftAttributeInfo.CherryTree,		CraftResource.CherryTree,		typeof( CherryLog ),	typeof( CherryBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "ebony", "", 0 ),			1095401,	"Ebony",		CraftAttributeInfo.EbonyTree,		CraftResource.EbonyTree,		typeof( EbonyLog ),		typeof( EbonyBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "golden oak", "", 0 ),	1095402,	"Golden Oak",	CraftAttributeInfo.GoldenOakTree,	CraftResource.GoldenOakTree,	typeof( GoldenOakLog ),	typeof( GoldenOakBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "hickory", "", 0 ),		1095403,	"Hickory",		CraftAttributeInfo.HickoryTree,		CraftResource.HickoryTree,		typeof( HickoryLog ),	typeof( HickoryBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "mahogany", "", 0 ),		1095404,	"Mahogany",		CraftAttributeInfo.MahoganyTree,	CraftResource.MahoganyTree,		typeof( MahoganyLog ),	typeof( MahoganyBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "oak", "", 0 ),			1095405,	"Oak",			CraftAttributeInfo.OakTree,			CraftResource.OakTree,			typeof( OakLog ),		typeof( OakBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "pine", "", 0 ),			1095406,	"Pine",			CraftAttributeInfo.PineTree,		CraftResource.PineTree,			typeof( PineLog ),		typeof( PineBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "ghostwood", "", 0 ),		1095513,	"Ghostwood",	CraftAttributeInfo.GhostTree,		CraftResource.GhostTree,		typeof( GhostLog ),		typeof( GhostBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "rosewood", "", 0 ),		1095407,	"Rosewood",		CraftAttributeInfo.RosewoodTree,	CraftResource.RosewoodTree,		typeof( RosewoodLog ),	typeof( RosewoodBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "walnut", "", 0 ),		1095408,	"Walnut",		CraftAttributeInfo.WalnutTree,		CraftResource.WalnutTree,		typeof( WalnutLog ),	typeof( WalnutBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "petrified", "", 0 ),		1095534,	"Petrified",	CraftAttributeInfo.PetrifiedTree,	CraftResource.PetrifiedTree,	typeof( PetrifiedLog ),	typeof( PetrifiedBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "driftwood", "", 0 ),		1095510,	"Driftwood",	CraftAttributeInfo.DriftwoodTree,	CraftResource.DriftwoodTree,	typeof( DriftwoodLog ),	typeof( DriftwoodBoard ) ),
				new CraftResourceInfo( MaterialInfo.GetMaterialColor( "elven", "", 0 ),			1095537,	"Elven",		CraftAttributeInfo.ElvenTree,		CraftResource.ElvenTree,		typeof( ElvenLog ),		typeof( ElvenBoard ) )
			};

		/// <summary>
		/// Returns true if '<paramref name="resource"/>' is None, Iron, RegularLeather or RegularWood. False if otherwise.
		/// </summary>
		public static bool IsStandard( CraftResource resource )
		{
			return ( resource == CraftResource.None || resource == CraftResource.Iron || resource == CraftResource.RegularLeather || resource == CraftResource.RegularWood );
		}

		private static Hashtable m_TypeTable;

		/// <summary>
		/// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="CraftResources.GetFromType"/>
		/// </summary>
		public static void RegisterType( Type resourceType, CraftResource resource )
		{
			if ( m_TypeTable == null )
				m_TypeTable = new Hashtable();

			m_TypeTable[resourceType] = resource;
		}

		/// <summary>
		/// Returns the <see cref="CraftResource"/> value for which '<paramref name="resourceType"/>' uses -or- CraftResource.None if an unregistered type was specified.
		/// </summary>
		public static CraftResource GetFromType( Type resourceType )
		{
			if ( m_TypeTable == null )
				return CraftResource.None;

			object obj = m_TypeTable[resourceType];

			if ( !(obj is CraftResource) )
				return CraftResource.None;

			return (CraftResource)obj;
		}

		/// <summary>
		/// Returns a <see cref="CraftResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
		/// </summary>
		public static CraftResourceInfo GetInfo( CraftResource resource )
		{
			CraftResourceInfo[] list = null;

			switch ( GetType( resource ) )
			{
				case CraftResourceType.Metal: list = m_MetalInfo; break;
				case CraftResourceType.Leather: list = m_AOSLeatherInfo; break;
				case CraftResourceType.Scales: list = m_ScaleInfo; break;
				case CraftResourceType.Wood: list = m_WoodInfo; break;
			}

			if ( list != null )
			{
				int index = GetIndex( resource );

				if ( index >= 0 && index < list.Length )
					return list[index];
			}

			return null;
		}

		/// <summary>
		/// Returns a <see cref="CraftResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
		/// </summary>
		public static CraftResourceType GetType( CraftResource resource )
		{
			if ( resource >= CraftResource.Iron && resource <= CraftResource.Valorite )
				return CraftResourceType.Metal;

			if ( resource == CraftResource.Steel || resource == CraftResource.Brass || resource == CraftResource.Mithril || resource == CraftResource.Dwarven || resource == CraftResource.Xormite || resource == CraftResource.Obsidian || resource == CraftResource.Nepturite )
				return CraftResourceType.Metal;

			if ( resource >= CraftResource.RegularLeather && resource <= CraftResource.BarbedLeather )
				return CraftResourceType.Leather;

			if ( 	resource == CraftResource.NecroticLeather || 
					resource == CraftResource.VolcanicLeather || 
					resource == CraftResource.FrozenLeather || 
					resource == CraftResource.GoliathLeather || 
					resource == CraftResource.DraconicLeather || 
					resource == CraftResource.HellishLeather || 
					resource == CraftResource.DinosaurLeather || 
					resource == CraftResource.AlienLeather )
				return CraftResourceType.Leather;

			if ( resource >= CraftResource.RedScales && resource <= CraftResource.BlueScales )
				return CraftResourceType.Scales;

			if ( resource == CraftResource.DinosaurScales )
				return CraftResourceType.Scales;

			if (	resource == CraftResource.RegularWood || 
					resource == CraftResource.AshTree || 
					resource == CraftResource.CherryTree || 
					resource == CraftResource.EbonyTree || 
					resource == CraftResource.GoldenOakTree || 
					resource == CraftResource.HickoryTree || 
					resource == CraftResource.MahoganyTree || 
					resource == CraftResource.OakTree || 
					resource == CraftResource.PineTree || 
					resource == CraftResource.GhostTree || 
					resource == CraftResource.RosewoodTree || 
					resource == CraftResource.WalnutTree || 
					resource == CraftResource.DriftwoodTree || 
					resource == CraftResource.ElvenTree || 
					resource == CraftResource.PetrifiedTree )
				return CraftResourceType.Wood;

			return CraftResourceType.None;
		}

		/// <summary>
		/// Returns the first <see cref="CraftResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
		/// </summary>
		public static CraftResource GetStart( CraftResource resource )
		{
			switch ( GetType( resource ) )
			{
				case CraftResourceType.Metal: return CraftResource.Iron;
				case CraftResourceType.Leather: return CraftResource.RegularLeather;
				case CraftResourceType.Scales: return CraftResource.RedScales;
				case CraftResourceType.Wood: return CraftResource.RegularWood;
			}

			return CraftResource.None;
		}

		/// <summary>
		/// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
		/// </summary>
		public static int GetIndex( CraftResource resource )
		{
			CraftResource start = GetStart( resource );

			if ( start == CraftResource.None )
				return 0;

			return (int)(resource - start);
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
		/// </summary>
		public static int GetLocalizationNumber( CraftResource resource )
		{
			CraftResourceInfo info = GetInfo( resource );

			return ( info == null ? 0 : info.Number );
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
		/// </summary>
		public static int GetHue( CraftResource resource )
		{
			CraftResourceInfo info = GetInfo( resource );

			return ( info == null ? 0 : info.Hue );
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
		/// </summary>
		public static string GetName( CraftResource resource )
		{
			CraftResourceInfo info = GetInfo( resource );

			return ( info == null ? String.Empty : info.Name );
		}

		/// <summary>
		/// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>' -or- CraftResource.None if unable to convert.
		/// </summary>
		public static CraftResource GetFromOreInfo( OreInfo info )
		{
			if ( info.Name.IndexOf( "Deep Sea" ) >= 0 )
				return CraftResource.SpinedLeather;
			else if ( info.Name.IndexOf( "Lizard" ) >= 0 )
				return CraftResource.HornedLeather;
			else if ( info.Name.IndexOf( "Serpent" ) >= 0 )
				return CraftResource.BarbedLeather;
			else if ( info.Name.IndexOf( "Necrotic" ) >= 0 )
				return CraftResource.NecroticLeather;
			else if ( info.Name.IndexOf( "Volcanic" ) >= 0 )
				return CraftResource.VolcanicLeather;
			else if ( info.Name.IndexOf( "Frozen" ) >= 0 )
				return CraftResource.FrozenLeather;
			else if ( info.Name.IndexOf( "Goliath" ) >= 0 )
				return CraftResource.GoliathLeather;
			else if ( info.Name.IndexOf( "Draconic" ) >= 0 )
				return CraftResource.DraconicLeather;
			else if ( info.Name.IndexOf( "Hellish" ) >= 0 )
				return CraftResource.HellishLeather;
			else if ( info.Name.IndexOf( "Dinosaur" ) >= 0 )
				return CraftResource.DinosaurLeather;
			else if ( info.Name.IndexOf( "Alien" ) >= 0 )
				return CraftResource.AlienLeather;
			else if ( info.Name.IndexOf( "Leather" ) >= 0 )
				return CraftResource.RegularLeather;

			if ( info.Level == 0 )
				return CraftResource.Iron;
			else if ( info.Level == 1 )
				return CraftResource.DullCopper;
			else if ( info.Level == 2 )
				return CraftResource.ShadowIron;
			else if ( info.Level == 3 )
				return CraftResource.Copper;
			else if ( info.Level == 4 )
				return CraftResource.Bronze;
			else if ( info.Level == 5 )
				return CraftResource.Gold;
			else if ( info.Level == 6 )
				return CraftResource.Agapite;
			else if ( info.Level == 7 )
				return CraftResource.Verite;
			else if ( info.Level == 8 )
				return CraftResource.Valorite;
			else if ( info.Level == 9 )
				return CraftResource.Nepturite;
			else if ( info.Level == 10 )
				return CraftResource.Obsidian;
			else if ( info.Level == 11 )
				return CraftResource.Steel;
			else if ( info.Level == 12 )
				return CraftResource.Brass;
			else if ( info.Level == 13 )
				return CraftResource.Mithril;
			else if ( info.Level == 14 )
				return CraftResource.Xormite;
			else if ( info.Level == 15 )
				return CraftResource.Dwarven;

			return CraftResource.None;
		}

		/// <summary>
		/// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>', using '<paramref name="material"/>' to help resolve leather OreInfo instances.
		/// </summary>
		public static CraftResource GetFromOreInfo( OreInfo info, ArmorMaterialType material )
		{
			if ( material == ArmorMaterialType.Studded || 
					material == ArmorMaterialType.Leather || 
					material == ArmorMaterialType.Spined || 
					material == ArmorMaterialType.Necrotic || 
					material == ArmorMaterialType.Volcanic || 
					material == ArmorMaterialType.Frozen || 
					material == ArmorMaterialType.Goliath || 
					material == ArmorMaterialType.Draconic || 
					material == ArmorMaterialType.Hellish || 
					material == ArmorMaterialType.Horned || 
					material == ArmorMaterialType.Barbed || 
					material == ArmorMaterialType.Dinosaur || 
					material == ArmorMaterialType.Alien )
			{
				if ( info.Level == 0 )
					return CraftResource.RegularLeather;
				else if ( info.Level == 1 )
					return CraftResource.SpinedLeather;
				else if ( info.Level == 2 )
					return CraftResource.HornedLeather;
				else if ( info.Level == 3 )
					return CraftResource.BarbedLeather;
				else if ( info.Level == 4 )
					return CraftResource.NecroticLeather;
				else if ( info.Level == 5 )
					return CraftResource.VolcanicLeather;
				else if ( info.Level == 6 )
					return CraftResource.FrozenLeather;
				else if ( info.Level == 7 )
					return CraftResource.GoliathLeather;
				else if ( info.Level == 8 )
					return CraftResource.DraconicLeather;
				else if ( info.Level == 9 )
					return CraftResource.HellishLeather;
				else if ( info.Level == 10 )
					return CraftResource.DinosaurLeather;
				else if ( info.Level == 11 )
					return CraftResource.AlienLeather;

				return CraftResource.None;
			}

			return GetFromOreInfo( info );
		}
	}

	// NOTE: This class is only for compatability with very old RunUO versions.
	// No changes to it should be required for custom resources.
	public class OreInfo
	{
		public static readonly OreInfo Iron			= new OreInfo( 0, 0x000, "Iron" );
		public static readonly OreInfo DullCopper	= new OreInfo( 1, MaterialInfo.GetMaterialColor( "dull copper", "", 0 ), "Dull Copper" );
		public static readonly OreInfo ShadowIron	= new OreInfo( 2, MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ), "Shadow Iron" );
		public static readonly OreInfo Copper		= new OreInfo( 3, MaterialInfo.GetMaterialColor( "copper", "classic", 0 ), "Copper" );
		public static readonly OreInfo Bronze		= new OreInfo( 4, MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ), "Bronze" );
		public static readonly OreInfo Gold			= new OreInfo( 5, MaterialInfo.GetMaterialColor( "gold", "classic", 0 ), "Gold" );
		public static readonly OreInfo Agapite		= new OreInfo( 6, MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ), "Agapite" );
		public static readonly OreInfo Verite		= new OreInfo( 7, MaterialInfo.GetMaterialColor( "verite", "classic", 0 ), "Verite" );
		public static readonly OreInfo Valorite		= new OreInfo( 8, MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ), "Valorite" );
		public static readonly OreInfo Nepturite	= new OreInfo( 9, MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ), "Nepturite" );
		public static readonly OreInfo Obsidian		= new OreInfo( 10, MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ), "Obsidian" );
		public static readonly OreInfo Mithril		= new OreInfo( 11, MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ), "Mithril" );
		public static readonly OreInfo Xormite		= new OreInfo( 12, MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ), "Xormite" );
		public static readonly OreInfo Dwarven		= new OreInfo( 13, MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ), "Dwarven" );

		private int m_Level;
		private int m_Hue;
		private string m_Name;

		public OreInfo( int level, int hue, string name )
		{
			m_Level = level;
			m_Hue = hue;
			m_Name = name;
		}

		public int Level
		{
			get
			{
				return m_Level;
			}
		}

		public int Hue
		{
			get
			{
				return m_Hue;
			}
		}

		public string Name
		{
			get
			{
				return m_Name;
			}
		}
	}
}
