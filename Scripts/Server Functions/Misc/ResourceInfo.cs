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
		SpinedLeather, // Deep Sea
		HornedLeather, // Lizard
		BarbedLeather, // Serpent
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

            DullCopper = new CraftAttributeInfo
            {
				// 7
                ArmorPhysicalResist = 2,
                ArmorEnergyResist = 5,
                ArmorDurability = 30,
                ArmorLowerRequirements = 20,

                WeaponDurability = 30,
                WeaponLowerRequirements = 50,
            };

            ShadowIron = new CraftAttributeInfo
            {
                // 9
                ArmorPhysicalResist = 2,
                ArmorPoisonResist = 2,
                ArmorEnergyResist = 5,
                ArmorDurability = 10,

                WeaponColdDamage = 20,
                WeaponDurability = 50,
            };

            Copper = new CraftAttributeInfo
            {
				// 11
                ArmorPhysicalResist = 3,
                ArmorFireResist = 2,
                ArmorPoisonResist = 4,
                ArmorEnergyResist = 2,

                WeaponPoisonDamage = 10,
                WeaponEnergyDamage = 20,
            };

            Bronze = new CraftAttributeInfo
            {
				// 13
                ArmorPhysicalResist = 3,
                ArmorColdResist = 5,
                ArmorPoisonResist = 2,
                ArmorEnergyResist = 3,

                WeaponFireDamage = 40,
            };

            Golden = new CraftAttributeInfo
            {
				// 15
                ArmorPhysicalResist = 4,
                ArmorFireResist = 5,
                ArmorColdResist = 2,
                ArmorEnergyResist = 4,
                ArmorLuck = 40,
                ArmorLowerRequirements = 30,

                WeaponLuck = 40,
                WeaponLowerRequirements = 50,
            };

            Agapite = new CraftAttributeInfo
            {
				// 17
                ArmorPhysicalResist = 5,
                ArmorFireResist = 3,
                ArmorColdResist = 3,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 3,

                WeaponColdDamage = 30,
                WeaponEnergyDamage = 20,
            };

            Verite = new CraftAttributeInfo
            {
				// 19
                ArmorPhysicalResist = 5,
                ArmorFireResist = 4,
                ArmorColdResist = 4,
                ArmorPoisonResist = 5,
                ArmorEnergyResist = 1,

                WeaponPoisonDamage = 40,
                WeaponEnergyDamage = 20,
            };

            Valorite = new CraftAttributeInfo
            {
                ArmorPhysicalResist = 6, //21
                ArmorColdResist = 5,
                ArmorPoisonResist = 5,
                ArmorEnergyResist = 5,
                ArmorDurability = 15,

                WeaponFireDamage = 10,
                WeaponColdDamage = 20,
                WeaponPoisonDamage = 10,
                WeaponEnergyDamage = 20,
            };

            Nepturite = new CraftAttributeInfo
            {
				// 24
                ArmorPhysicalResist = 6,
                ArmorColdResist = 8,
                ArmorPoisonResist = 10,
                ArmorDurability = 20,

                WeaponColdDamage = 25,
                WeaponPoisonDamage = 25,
            };

            Obsidian = new CraftAttributeInfo
            {
				// 27
                ArmorPhysicalResist = 7,
                ArmorColdResist = 4,
                ArmorPoisonResist = 4,
                ArmorFireResist = 12,
                ArmorEnergyResist = 9,
                ArmorDurability = 25,

                WeaponFireDamage = 20,
                WeaponEnergyDamage = 10,
            };

            Steel = new CraftAttributeInfo
            {
				// 13
                ArmorPhysicalResist = 5,
                ArmorColdResist = 3,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 2,
                ArmorDurability = 10,
                ArmorLowerRequirements = 25,

                WeaponDurability = 10,
                WeaponLowerRequirements = 25,
            };

            Brass = new CraftAttributeInfo
            {
				// 17
                ArmorPhysicalResist = 6,
                ArmorColdResist = 4,
                ArmorPoisonResist = 4,
                ArmorEnergyResist = 3,
                ArmorDurability = 10,
                ArmorLowerRequirements = 45,

                WeaponFireDamage = 20,
                WeaponEnergyDamage = 20,
                WeaponDurability = 10,
                WeaponLowerRequirements = 45,
            };

            Mithril = new CraftAttributeInfo
            {
				// 48
                ArmorPhysicalResist = 15,
                ArmorColdResist = 15,
                ArmorPoisonResist = 9,
                ArmorEnergyResist = 9,
                ArmorLuck = 100,
                ArmorDurability = 30,
                ArmorLowerRequirements = 75,

                WeaponLuck = 100,
                WeaponEnergyDamage = 30,
                WeaponDurability = 30,
                WeaponLowerRequirements = 75,
            };

            Dwarven = new CraftAttributeInfo
            {
				// 33
                ArmorPhysicalResist = 16,
                ArmorFireResist = 10,
                ArmorColdResist = 7,
                ArmorDurability = 35,

                WeaponDurability = 35,
            };

            Xormite = new CraftAttributeInfo
            {
				// 54
                ArmorPhysicalResist = 15,
                ArmorColdResist = 8,
                ArmorPoisonResist = 8,
                ArmorFireResist = 8,
                ArmorEnergyResist = 15,
                ArmorDurability = 20,
                ArmorLowerRequirements = 75,

                WeaponDirectDamage = 30,
                WeaponDurability = 20,
                WeaponLowerRequirements = 75,
            };

            Spined = new CraftAttributeInfo // Deep sea
            {
				// 5
                ArmorPhysicalResist = 0,
                ArmorColdResist = 2,
                ArmorPoisonResist = 2,
                ArmorEnergyResist = 1,
                ArmorLuck = 40,
                ArmorDurability = 5,

                WeaponDurability = 5,
                WeaponLuck = 40,
                WeaponPoisonDamage = 20,
            };

            Horned = new CraftAttributeInfo // Lizard
            {
				// 7
                ArmorPhysicalResist = 1,
                ArmorColdResist = 3,
                ArmorPoisonResist = 3,
                ArmorDurability = 10,

                WeaponDurability = 10,
            };

            Barbed = new CraftAttributeInfo // Serpent
            {
				// 9
                ArmorPhysicalResist = 1,
                ArmorFireResist = 3,
                ArmorColdResist = 3,
                ArmorPoisonResist = 2,
                ArmorDurability = 20,

                WeaponDurability = 20,
                WeaponPoisonDamage = 70,
            };

            Necrotic = new CraftAttributeInfo
            {
				// 10
                ArmorPhysicalResist = 1,
                ArmorColdResist = 4,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 2,
                ArmorDurability = 5,

                WeaponDurability = 5,
                WeaponFireDamage = 50,
            };

            Volcanic = new CraftAttributeInfo
            {
				// 11
                ArmorPhysicalResist = 2,
                ArmorFireResist = 6,
                ArmorPoisonResist = 3,
                ArmorDurability = 15,

                WeaponDurability = 15,
                WeaponFireDamage = 50,
            };

            Frozen = new CraftAttributeInfo
            {
				// 12
                ArmorPhysicalResist = 2,
                ArmorColdResist = 6,
                ArmorEnergyResist = 4,
                ArmorDurability = 10,

                WeaponDurability = 10,
                WeaponColdDamage = 50,
            };

            Goliath = new CraftAttributeInfo
            {
				// 13
                ArmorPhysicalResist = 4,
                ArmorFireResist = 3,
                ArmorColdResist = 3,
                ArmorEnergyResist = 3,
                ArmorDurability = 25,

                WeaponDurability = 25,
                WeaponEnergyDamage = 25,
            };

            Draconic = new CraftAttributeInfo
            {
				// 14
                ArmorPhysicalResist = 2,
                ArmorFireResist = 6,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 3,
                ArmorDurability = 25,

                WeaponDurability = 25,
                WeaponFireDamage = 25,
            };

            Hellish = new CraftAttributeInfo
            {
				// 15
                ArmorPhysicalResist = 3,
                ArmorColdResist = 4,
                ArmorPoisonResist = 2,
                ArmorEnergyResist = 6,
                ArmorDurability = 10,

                WeaponDurability = 10,
                WeaponFireDamage = 50,
            };

            Dinosaur = new CraftAttributeInfo
            {
				// 16
                ArmorPhysicalResist = 3,
                ArmorFireResist = 4,
                ArmorColdResist = 4,
                ArmorPoisonResist = 5,
                ArmorDurability = 40,

                WeaponDurability = 40,
            };

            Alien = new CraftAttributeInfo
            {
				// 32
                ArmorPhysicalResist = 5, 
                ArmorFireResist = 7,
                ArmorColdResist = 7,
                ArmorPoisonResist = 6,
                ArmorEnergyResist = 7,
                ArmorDurability = 30,

                WeaponDurability = 30,
            };

            RedScales = new CraftAttributeInfo
            {
				// 25
                ArmorPhysicalResist = 5,
                ArmorFireResist = 8,
                ArmorColdResist = 8,
                ArmorPoisonResist = 2,
                ArmorEnergyResist = 2
            };

            YellowScales = new CraftAttributeInfo
            {
				// 21
                ArmorPhysicalResist = 5,
                ArmorFireResist = 4,
                ArmorColdResist = 4,
                ArmorPoisonResist = 4,
                ArmorEnergyResist = 4,
                ArmorGoldIncrease = 4
            };

            BlackScales = new CraftAttributeInfo
            {
				// 30
                ArmorPhysicalResist = 6,
                ArmorFireResist = 3,
                ArmorColdResist = 3,
                ArmorPoisonResist = 9,
                ArmorEnergyResist = 9
            };

            GreenScales = new CraftAttributeInfo
            {
				// 30
                ArmorPhysicalResist = 6,
                ArmorFireResist = 3,
                ArmorColdResist = 9,
                ArmorPoisonResist = 9,
                ArmorEnergyResist = 3
            };

            WhiteScales = new CraftAttributeInfo
            {
				// 30
                ArmorPhysicalResist = 6,
                ArmorFireResist = 9,
                ArmorColdResist = 3,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 9
            };

            BlueScales = new CraftAttributeInfo
            {
				// 30
                ArmorPhysicalResist = 9,
                ArmorFireResist = 9,
                ArmorColdResist = 3,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 6
            };

            DinosaurScales = new CraftAttributeInfo
            {
				// 35
                ArmorPhysicalResist = 11,
                ArmorFireResist = 6,
                ArmorColdResist = 6,
                ArmorPoisonResist = 6,
                ArmorEnergyResist = 6
            };

            AshTree = new CraftAttributeInfo
            {
				// 5
                ArmorPhysicalResist = 1,
                ArmorFireResist = 1,
                ArmorColdResist = 1,
                ArmorPoisonResist = 1,
                ArmorEnergyResist = 1,

                WeaponFireDamage = 5,
                WeaponColdDamage = 5,
                WeaponPoisonDamage = 5,
                WeaponEnergyDamage = 5,
            };

            CherryTree = new CraftAttributeInfo
            {
                // 7
                ArmorPhysicalResist = 1,
                ArmorFireResist = 2,
                ArmorColdResist = 1,
                ArmorPoisonResist = 2,
                ArmorEnergyResist = 1,

                WeaponPoisonDamage = 10,
                WeaponEnergyDamage = 20,
            };

            EbonyTree = new CraftAttributeInfo
            {
                // 8
                ArmorPhysicalResist = 3,
                ArmorFireResist = 3,
                ArmorEnergyResist = 2,
                ArmorDurability = 20,

                WeaponColdDamage = 20,
                WeaponDurability = 20,
            };

            GoldenOakTree = new CraftAttributeInfo
            {
                // 10
                ArmorPhysicalResist = 3, 
                ArmorFireResist = 2,
                ArmorColdResist = 3,
                ArmorEnergyResist = 2,
                ArmorLuck = 40,
                ArmorLowerRequirements = 30,

                WeaponLuck = 40,
                WeaponLowerRequirements = 50,
            };

            HickoryTree = new CraftAttributeInfo
            {
                // 12
                ArmorPhysicalResist = 5,
                ArmorColdResist = 7,
                ArmorDurability = 20,
                ArmorLowerRequirements = 20,

                WeaponDurability = 20,
                WeaponLowerRequirements = 50,
            };

            MahoganyTree = new CraftAttributeInfo
            {
                // 15
                ArmorPhysicalResist = 7,
                ArmorFireResist = 3,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 2,

                WeaponPoisonDamage = 10,
                WeaponEnergyDamage = 20,
            };

            OakTree = new CraftAttributeInfo
            {
                // 16
                ArmorPhysicalResist = 3,
                ArmorColdResist = 5,
                ArmorPoisonResist = 5,
                ArmorEnergyResist = 3,

                WeaponFireDamage = 40,
            };

            PineTree = new CraftAttributeInfo
            {
                // 19
                ArmorPhysicalResist = 3,
                ArmorFireResist = 3,
                ArmorColdResist = 3,
                ArmorPoisonResist = 5,
                ArmorEnergyResist = 5,

                WeaponColdDamage = 30,
                WeaponEnergyDamage = 20,
            };

            RosewoodTree = new CraftAttributeInfo
            {
                // 24
                ArmorPhysicalResist = 5,
                ArmorFireResist = 5,
                ArmorColdResist = 3,
                ArmorPoisonResist = 8,
                ArmorEnergyResist = 3,

                WeaponPoisonDamage = 40,
                WeaponEnergyDamage = 20,
            };

            WalnutTree = new CraftAttributeInfo
            {
                // 23
                ArmorPhysicalResist = 10,
                ArmorColdResist = 5,
                ArmorPoisonResist = 4,
                ArmorEnergyResist = 4,
                ArmorDurability = 10,

                WeaponFireDamage = 10,
                WeaponColdDamage = 20,
                WeaponPoisonDamage = 10,
                WeaponEnergyDamage = 20,
            };

            DriftwoodTree = new CraftAttributeInfo
            {
                // 32
                ArmorPhysicalResist = 9,
                ArmorColdResist = 8,
                ArmorPoisonResist = 10,
                ArmorEnergyResist = 5,
                ArmorDurability = 5,

                WeaponFireDamage = 10,
                WeaponColdDamage = 10,
                WeaponPoisonDamage = 20,
                WeaponEnergyDamage = 10,
            };

            GhostTree = new CraftAttributeInfo
            {
                // 38
                ArmorPhysicalResist = 9,
                ArmorFireResist = 5,
                ArmorColdResist = 9,
                ArmorPoisonResist = 9,
                ArmorEnergyResist = 6,

                WeaponColdDamage = 25,
                WeaponEnergyDamage = 25,
            };

            PetrifiedTree = new CraftAttributeInfo
            {
                // 30
                ArmorPhysicalResist = 10,
                ArmorColdResist = 5,
                ArmorPoisonResist = 5,
                ArmorEnergyResist = 10,
                ArmorDurability = 30,

                WeaponColdDamage = 25,
            };

            ElvenTree = new CraftAttributeInfo
            {
                // 39
                ArmorPhysicalResist = 10,
                ArmorFireResist = 3,
                ArmorPoisonResist = 11,
                ArmorEnergyResist = 15,
                ArmorDurability = 25,
                ArmorLuck = 100,

                WeaponLuck = 100,
            };

            // Runics
            // SetRunicAttributes(minProps, maxProps, minIntensity, maxIntensity, ...resources... );
            SetRunicAttributes(1, 1, 60,  70,   DullCopper,  Spined,    AshTree);
            SetRunicAttributes(1, 2, 70,  80,   ShadowIron,  Horned,    CherryTree);
            SetRunicAttributes(2, 3, 70,  80,   Copper,      Barbed,    EbonyTree);
            SetRunicAttributes(2, 4, 70,  80,   Bronze,      Necrotic,  GoldenOakTree);
            SetRunicAttributes(3, 4, 70,  80,   Golden,      Volcanic,  HickoryTree);
            SetRunicAttributes(3, 5, 80,  90,   Agapite,     Frozen,    MahoganyTree);
            SetRunicAttributes(4, 6, 80,  100,  Verite,      Goliath,   OakTree);
            SetRunicAttributes(5, 9, 90,  100,  Valorite,    Draconic,  PineTree);

			// Warning: These runics have not been balanced:
            SetRunicAttributes(5, 6, 80,  100,  Nepturite,   null,      GhostTree);
            SetRunicAttributes(5, 6, 85,  100,  Obsidian,    null,      RosewoodTree);
            SetRunicAttributes(5, 7, 85,  100,  Steel,       Hellish,   WalnutTree);
            SetRunicAttributes(5, 7, 90,  100,  Brass,       null,      PetrifiedTree);
            SetRunicAttributes(6, 7, 90,  100,  Mithril,     Dinosaur,  DriftwoodTree);
            SetRunicAttributes(6, 7, 100, 120,  Xormite,     null,      null);
            SetRunicAttributes(6, 8, 100, 150,  Dwarven,     Alien,     ElvenTree);
        }

		private static void SetRunicAttributes(int runicMinAttributes, int runicMaxAttributes, int runicMinIntensity, int runicMaxIntensity, params CraftAttributeInfo[] attributes)
        {
			foreach (var attribute in attributes)
			{
				if (attribute == null) continue; // To help align things above

				attribute.RunicMinAttributes = runicMinAttributes;
				attribute.RunicMaxAttributes = runicMaxAttributes;
				attribute.RunicMinIntensity = runicMinIntensity;
				attribute.RunicMaxIntensity = runicMaxIntensity;
            }
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

		public static double GetValueMultiplier(CraftResource resource)
		{
			switch ( resource )
			{
				case CraftResource.DullCopper:      return 1.25;
				case CraftResource.ShadowIron:      return 1.5;
				case CraftResource.Copper:          return 1.75;
				case CraftResource.Bronze:          return 2;
				case CraftResource.Gold:            return 2.25;
				case CraftResource.Agapite:         return 2.50;
				case CraftResource.Verite:          return 2.75;
				case CraftResource.Valorite:        return 3;
				case CraftResource.Nepturite:       return 3.10;
				case CraftResource.Obsidian:        return 3.10;
				case CraftResource.Steel:           return 3.25;
				case CraftResource.Brass:           return 3.5;
				case CraftResource.Mithril:         return 3.75;
				case CraftResource.Xormite:         return 3.75;
				case CraftResource.Dwarven:         return 7.50;

				case CraftResource.SpinedLeather:   return 1.5;
				case CraftResource.HornedLeather:   return 1.75;
				case CraftResource.BarbedLeather:   return 2.0;
				case CraftResource.NecroticLeather: return 2.25;
				case CraftResource.VolcanicLeather: return 2.5;
				case CraftResource.FrozenLeather:   return 2.75;
				case CraftResource.GoliathLeather:  return 3.0;
				case CraftResource.DraconicLeather: return 3.25;
				case CraftResource.HellishLeather:  return 3.5;
				case CraftResource.DinosaurLeather: return 3.75;
				case CraftResource.AlienLeather:    return 3.75;

				case CraftResource.RedScales:       return 1.25;
				case CraftResource.YellowScales:    return 1.25;
				case CraftResource.BlackScales:     return 1.5;
				case CraftResource.GreenScales:     return 1.5;
				case CraftResource.WhiteScales:     return 1.5;
				case CraftResource.BlueScales:      return 1.5;

				case CraftResource.AshTree:         return 1.25;
				case CraftResource.CherryTree:      return 1.45;
				case CraftResource.EbonyTree:       return 1.65;
				case CraftResource.GoldenOakTree:   return 1.85;
				case CraftResource.HickoryTree:     return 2.05;
				case CraftResource.MahoganyTree:    return 2.25;
				case CraftResource.DriftwoodTree:   return 2.25;
				case CraftResource.OakTree:         return 2.45;
				case CraftResource.PineTree:        return 2.65;
				case CraftResource.GhostTree:       return 2.65;
				case CraftResource.RosewoodTree:    return 2.85;
				case CraftResource.WalnutTree:      return 3;
				case CraftResource.PetrifiedTree:   return 3.25;
				case CraftResource.ElvenTree:       return 6;
			}
			
			return 1;
		}

		public static double GetMetalProcessDifficulty(CraftResource resource)
		{
			switch ( resource )
			{
				case CraftResource.DullCopper: return 65.0;
				case CraftResource.ShadowIron: return 70.0;
				case CraftResource.Copper: return 75.0;
				case CraftResource.Bronze: return 80.0;
				case CraftResource.Gold: return 85.0;
				case CraftResource.Agapite: return 90.0;
				case CraftResource.Verite: return 95.0;
				case CraftResource.Valorite: return 99.0;
				case CraftResource.Nepturite: return 99.0;
				case CraftResource.Obsidian: return 99.0;
				case CraftResource.Steel: return 99.0;
				case CraftResource.Brass: return 105.0;
				case CraftResource.Mithril: return 99.0;
				case CraftResource.Xormite: return 99.0;
				case CraftResource.Dwarven: return 101.0;
				default: return 50.0;
			}
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
