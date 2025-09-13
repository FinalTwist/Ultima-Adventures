using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public enum GiftAttributeCategory
	{
		Misc = 0x00000001,
		Melee = 0x00000002,
		Magic = 0x00000004,
		Stats = 0x00000008,
		Resists = 0x00000010,
		Hits = 0x00000020
	}

	public class GiftAttributes
	{
		public static GiftAttributeInfo[] m_Attributes = new GiftAttributeInfo[]
		{
			new GiftAttributeInfo( AosAttribute.RegenHits, "Regen Hits", GiftAttributeCategory.Stats, 5, 5 ),
			new GiftAttributeInfo( AosAttribute.RegenStam, "Regen Stamina", GiftAttributeCategory.Stats, 5, 5 ),
			new GiftAttributeInfo( AosAttribute.RegenMana, "Regen Mana", GiftAttributeCategory.Stats, 5, 5 ),
			new GiftAttributeInfo( AosAttribute.DefendChance, "Defence Chance Increase", GiftAttributeCategory.Melee, 8, 15 ),
			new GiftAttributeInfo( AosAttribute.AttackChance, "Hit Chance Increase", GiftAttributeCategory.Melee, 10, 15 ),
			new GiftAttributeInfo( AosAttribute.BonusStr, "Bonus Strength", GiftAttributeCategory.Stats, 10, 10 ),
			new GiftAttributeInfo( AosAttribute.BonusDex, "Bonus Dex", GiftAttributeCategory.Stats, 10, 10 ),
			new GiftAttributeInfo( AosAttribute.BonusInt, "Bonus Int", GiftAttributeCategory.Stats, 10, 10 ),
			new GiftAttributeInfo( AosAttribute.BonusHits, "Bonus Hits", GiftAttributeCategory.Stats, 5, 20 ),
			new GiftAttributeInfo( AosAttribute.BonusStam, "Bonus Stamina", GiftAttributeCategory.Stats, 5, 20 ),
			new GiftAttributeInfo( AosAttribute.BonusMana, "Bonus Mana", GiftAttributeCategory.Stats, 5, 20 ),
			new GiftAttributeInfo( AosAttribute.WeaponDamage, "Damage Increase", GiftAttributeCategory.Melee, 5, 50 ),
			new GiftAttributeInfo( AosAttribute.WeaponSpeed, "Swing Speed Increase", GiftAttributeCategory.Melee, 6, 40 ),
			new GiftAttributeInfo( AosAttribute.SpellDamage, "Spell Damage", GiftAttributeCategory.Magic, 4, 25 ),
			new GiftAttributeInfo( AosAttribute.CastRecovery, "Faster Cast Recovery", GiftAttributeCategory.Magic, 20, 4 ),
			new GiftAttributeInfo( AosAttribute.CastSpeed, "Faster Casting", GiftAttributeCategory.Magic, 20, 4 ),
			new GiftAttributeInfo( AosAttribute.LowerManaCost, "Lower Mana Cost", GiftAttributeCategory.Magic, 5, 50 ),
			new GiftAttributeInfo( AosAttribute.LowerRegCost, "Lower Reagent Cost", GiftAttributeCategory.Magic, 5, 50 ),
			new GiftAttributeInfo( AosAttribute.ReflectPhysical, "Reflect Physical Damage", GiftAttributeCategory.Melee, 2, 50 ),
			new GiftAttributeInfo( AosAttribute.EnhancePotions, "Enhance Potions", GiftAttributeCategory.Magic, 2, 25 ),
			new GiftAttributeInfo( AosAttribute.Luck, "Luck", GiftAttributeCategory.Misc, 2, 500 ),
			new GiftAttributeInfo( AosAttribute.SpellChanneling, "Spell Channeling", GiftAttributeCategory.Magic, 15, 1 ),
			new GiftAttributeInfo( AosAttribute.NightSight, "Nightsight", GiftAttributeCategory.Misc, 6, 1 )
		};

        //Weapon Specific
		public static WeaponGiftAttributeInfo[] m_WeaponAttributes = new WeaponGiftAttributeInfo[]
		{
			new WeaponGiftAttributeInfo( AosWeaponAttribute.LowerStatReq, "Lower Stat Requirement", GiftAttributeCategory.Stats, 2, 100 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.SelfRepair, "Self Repair", GiftAttributeCategory.Misc, 2, 10 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitLeechHits, "Hit Life Leech", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitLeechStam, "Hit Stamina Leech", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitLeechMana, "Hit Mana Leech", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitLowerAttack, "Hit Lower Attack", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitLowerDefend, "Hit Lower Defence", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitMagicArrow, "Hit Magic Arrow", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitHarm, "Hit Harm", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitFireball, "Hit Fireball", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitLightning, "Hit Lightning", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitDispel, "Hit Dispel", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitColdArea, "Hit Cold Area", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitFireArea, "Hit Fire Area", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitPoisonArea, "Hit Poison Area", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitEnergyArea, "Hit Energy Area", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.HitPhysicalArea, "Hit Physical Area", GiftAttributeCategory.Hits, 3, 50 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.ResistPhysicalBonus, "Resist Physical Bonus", GiftAttributeCategory.Resists, 5, 20 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.ResistFireBonus, "Resist Fire Bonus", GiftAttributeCategory.Resists, 5, 20 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.ResistColdBonus, "Resist Cold Bonus", GiftAttributeCategory.Resists, 5, 20 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.ResistPoisonBonus, "Resist Poison Bonus", GiftAttributeCategory.Resists, 5, 20 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.ResistEnergyBonus, "Resist Energy Bonus", GiftAttributeCategory.Resists, 5, 20 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.UseBestSkill, "Use Best Weapon Skill", GiftAttributeCategory.Misc, 10, 1 ),
			new WeaponGiftAttributeInfo( AosWeaponAttribute.MageWeapon, "Mage Weapon", GiftAttributeCategory.Magic, 5, 1 ),
            new WeaponGiftAttributeInfo( AosWeaponAttribute.DurabilityBonus, "Durability Bonus", GiftAttributeCategory.Misc, 1, 255 )
		};

		//Armor specific attributes
        public static ArmorGiftAttributeInfo[] m_ArmorAttributes = new ArmorGiftAttributeInfo[]
		{
            new ArmorGiftAttributeInfo( AosArmorAttribute.LowerStatReq, "Lower Stat Requirement", GiftAttributeCategory.Stats, 2, 100 ),
            new ArmorGiftAttributeInfo( AosArmorAttribute.SelfRepair, "Self Repair", GiftAttributeCategory.Misc, 2, 5 ),
            new ArmorGiftAttributeInfo( AosArmorAttribute.MageArmor, "Mage Armor", GiftAttributeCategory.Magic, 5, 1 ),
            new ArmorGiftAttributeInfo( AosArmorAttribute.DurabilityBonus, "Durability Bonus", GiftAttributeCategory.Misc, 1, 255 )
        };

        //Armor specific
        public static GiftResistanceTypeInfo[] m_ResistanceTypes = new GiftResistanceTypeInfo[]
		{
            new GiftResistanceTypeInfo( ResistanceType.Physical, "Physical Resistance", GiftAttributeCategory.Resists, 2, 20 ),
            new GiftResistanceTypeInfo( ResistanceType.Fire, "Fire Resistance", GiftAttributeCategory.Resists, 2, 20 ),
            new GiftResistanceTypeInfo( ResistanceType.Cold, "Cold Resistance", GiftAttributeCategory.Resists, 2, 20 ),
            new GiftResistanceTypeInfo( ResistanceType.Poison, "Poison Resistance", GiftAttributeCategory.Resists, 2, 20 ),
            new GiftResistanceTypeInfo( ResistanceType.Energy, "Energy Resistance", GiftAttributeCategory.Resists, 2, 20 )
        };

        //Jewel & Clothing Specific Resists
        public static ElementGiftAttributeInfo[] m_ElementAttributes = new ElementGiftAttributeInfo[]
		{
            new ElementGiftAttributeInfo( AosElementAttribute.Physical, "Physical Resistance", GiftAttributeCategory.Resists, 2, 20 ),
            new ElementGiftAttributeInfo( AosElementAttribute.Fire, "Fire Resistance", GiftAttributeCategory.Resists, 2, 20 ),
            new ElementGiftAttributeInfo( AosElementAttribute.Cold, "Cold Resistance", GiftAttributeCategory.Resists, 2, 20 ),
            new ElementGiftAttributeInfo( AosElementAttribute.Poison, "Poison Resistance", GiftAttributeCategory.Resists, 2, 20 ),
            new ElementGiftAttributeInfo( AosElementAttribute.Energy, "Energy Resistance", GiftAttributeCategory.Resists, 2, 20 )
        };
	}

	#region " Info Classes "

	public class GiftAttributeInfo
	{
		public AosAttribute m_Attribute;
		public string m_Name;
		public GiftAttributeCategory m_Category;
		public int m_XP;
		public int m_MaxValue;

		public GiftAttributeInfo( AosAttribute attribute, string name, GiftAttributeCategory category, int xp, int maxvalue )
		{
			m_Attribute = attribute;
			m_Name = name;
			m_Category = category;
			m_XP = xp;
			m_MaxValue = maxvalue;
		}
	}

	public class WeaponGiftAttributeInfo
	{
		public AosWeaponAttribute m_Attribute;
		public string m_Name;
		public GiftAttributeCategory m_Category;
		public int m_XP;
		public int m_MaxValue;

		public WeaponGiftAttributeInfo( AosWeaponAttribute attribute, string name, GiftAttributeCategory category, int xp, int maxvalue )
		{
			m_Attribute = attribute;
			m_Name = name;
			m_Category = category;
			m_XP = xp;
			m_MaxValue = maxvalue;
		}
	}

    public class ArmorGiftAttributeInfo
    {
        public AosArmorAttribute m_Attribute;
        public string m_Name;
        public GiftAttributeCategory m_Category;
        public int m_XP;
        public int m_MaxValue;

        public ArmorGiftAttributeInfo(AosArmorAttribute attribute, string name, GiftAttributeCategory category, int xp, int maxvalue)
        {
            m_Attribute = attribute;
            m_Name = name;
            m_Category = category;
            m_XP = xp;
            m_MaxValue = maxvalue;
        }
    }

    public class GiftResistanceTypeInfo
    {
        public ResistanceType m_Attribute;
        public string m_Name;
        public GiftAttributeCategory m_Category;
        public int m_XP;
        public int m_MaxValue;

        public GiftResistanceTypeInfo(ResistanceType attribute, string name, GiftAttributeCategory category, int xp, int maxvalue)
        {
            m_Attribute = attribute;
            m_Name = name;
            m_Category = category;
            m_XP = xp;
            m_MaxValue = maxvalue;
        }
    }

    public class ElementGiftAttributeInfo
    {
        public AosElementAttribute m_Attribute;
        public string m_Name;
        public GiftAttributeCategory m_Category;
        public int m_XP;
        public int m_MaxValue;

        public ElementGiftAttributeInfo(AosElementAttribute attribute, string name, GiftAttributeCategory category, int xp, int maxvalue)
        {
            m_Attribute = attribute;
            m_Name = name;
            m_Category = category;
            m_XP = xp;
            m_MaxValue = maxvalue;
        }
    }
	#endregion
}