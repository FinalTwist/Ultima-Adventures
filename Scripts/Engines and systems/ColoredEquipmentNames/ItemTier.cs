using System;
using Server;
using Server.Items;

namespace ItemNameHue
{
    public class ItemTier
    {
	private static bool IncreasePriceBasedOnNumberOfProps = true; // if true, items with many beneficial props will sell for more money
	private static int AttrsMod1Or2Props = 1; // price multiplier if the item has 1-2 beneficial props
	private static int AttrsMod3Or4Props = 2;
	private static int AttrsMod5Or6Props = 5;
	private static int AttrsMod7Or8Props = 10;
	private static int AttrsMod9OrMoreProps = 20; // price multiplier if the item has 9+ beneficial props
	private static int AttrsIntensityThreshold = 25; // threshold for attribute intensity % to count toward the number of beneficial props (0 = any intensity, otherwise needs to be greater than the percentage specified)
	private static int IntensityPercentile = 20; // for each N% intensity, give a payout bonus equal to intensity multiplied by the multiplier below
	private static int IntensityMultiplier = 2; // for each N% intensity, give an additional intensity multiplier

	private static int PriceCutOnMaxDurability25 = 90; // %
	private static int PriceCutOnMaxDurability20 = 75; // %
	private static int PriceCutOnMaxDurability15 = 50; // %
	private static int PriceCutOnMaxDurability10 = 25; // %
	private static int PriceCutOnMaxDurability5 = 5; // %
	private static int PriceCutOnMaxDurability3 = 1; // %

	// The lists below must correspond to the enum definitions in AOS.cs. The number of elements
	// must strictly correspond to the number of elements in the AOS enums, or the game will crash.
	private static int[] AosAttributeIntensities = {
	    10, // RegenHits
	    10, // RegenStam
	    10, // RegenMana
	    25, // DefendChance
	    25, // AttackChance
	    25, // BonusStr
	    25, // BonusDex
	    25, // BonusInt
	    25, // BonusHits
	    25, // BonusStam
	    25, // BonusMana
	    50, // WeaponDamage
	    50, // WeaponSpeed
	    50, // SpellDamage
	    3, // CastRecovery
	    3, // CastSpeed
	    25, // LowerManaCost
	    25, // LowerRegCost
	    50, // ReflectPhysical
	    50, // EnhancePotions,
	    150, // Luck
	    1, // SpellChanneling
	    1 // NightSight
	};

	private static int[] AosWeaponAttributeIntensities = {
	    50, // LowerStatReq
	    5, // SelfRepair
	    50, // HitLeechHits
	    50, // HitLeechStam
	    50, // HitLeechMana
	    50, // HitLowerAttack
	    50, // HitLowerDefend
	    50, // HitMagicArrow
	    50, // HitHarm
	    50, // HitFireball
	    50, // HitLightning
	    50, // HitDispel
	    50, // HitColdArea
	    50, // HitFireArea
	    50, // HitPoisonArea
	    50, // HitEnergyArea
	    50, // HitPhysicalArea
	    15, // ResistPhysicalBonus
	    15, // ResistFireBonus
	    15, // ResistColdBonus
	    15, // ResistPoisonBonus
	    15, // ResistEnergyBonus
	    1, // UseBestSkill
	    1, // MageWeapon
	    100 // DurabilityBonus
	};

	private static int[] AosArmorAttributeIntensities = {
	    50, // LowerStatReq
	    5, // SelfRepair
	    1, // MageArmor
	    100 // DurabilityBonus
	};

	private static int[] AosElementAttributeIntensities = {
	    1100, // Physical - avoid overvaluing it since most stuff is at 100% physical
	    100, // Fire
	    100, // Cold
	    100, // Poison
	    100, // Energy
	    100, // Chaos
	    100, // Direct
	};

	private static int MaxSkillIntensity = 15; // FIXME: 12?
	private static int MaxResistanceIntensity = 25;
	private static int ResistanceIntensityCountsAsProp = 90; // %

	private enum IntensityMode
	{
	    AosAttribute,
	    AosWeaponAttribute,
	    AosArmorAttribute,
	    AosElementAttribute,
	    SkillBonus,
	    ResistanceBonus,
	    RunicToolProperties
	}

	private static void AddSkillBonuses(double skill1, double skill2, double skill3, double skill4, double skill5, ref int attrsMod, ref int props)
	{
	    int NormalizedSkillBonus1 = (int)skill1 * 100 / MaxSkillIntensity;
	    int NormalizedSkillBonus2 = (int)skill2 * 100 / MaxSkillIntensity;
	    int NormalizedSkillBonus3 = (int)skill3 * 100 / MaxSkillIntensity;
	    int NormalizedSkillBonus4 = (int)skill4 * 100 / MaxSkillIntensity;
	    int NormalizedSkillBonus5 = (int)skill5 * 100 / MaxSkillIntensity;

	    if(NormalizedSkillBonus1 > 0 && NormalizedSkillBonus1 >= AttrsIntensityThreshold) ++props;
	    if(NormalizedSkillBonus2 > 0 && NormalizedSkillBonus2 >= AttrsIntensityThreshold) ++props;
	    if(NormalizedSkillBonus3 > 0 && NormalizedSkillBonus3 >= AttrsIntensityThreshold) ++props;
	    if(NormalizedSkillBonus4 > 0 && NormalizedSkillBonus4 >= AttrsIntensityThreshold) ++props;
	    if(NormalizedSkillBonus5 > 0 && NormalizedSkillBonus5 >= AttrsIntensityThreshold) ++props;

	    attrsMod += (int)skill1 * (NormalizedSkillBonus1 / 2);
	    attrsMod += (int)skill2 * (NormalizedSkillBonus2 / 2);
	    attrsMod += (int)skill3 * (NormalizedSkillBonus3 / 2);
	    attrsMod += (int)skill4 * (NormalizedSkillBonus4 / 2);
	    attrsMod += (int)skill5 * (NormalizedSkillBonus5 / 2);
	}

	private static void AddResistanceBonuses(int physical, int fire, int cold, int poison, int energy, ref int attrsMod, ref int props)
	{
	    int NormalizedPhysicalResistance = physical * 100 / MaxResistanceIntensity;
	    int NormalizedFireResistance = fire * 100 / MaxResistanceIntensity;
	    int NormalizedColdResistance = cold * 100 / MaxResistanceIntensity;
	    int NormalizedPoisonResistance = poison * 100 / MaxResistanceIntensity;
	    int NormalizedEnergyResistance = energy * 100 / MaxResistanceIntensity;

	    if (NormalizedPhysicalResistance >= ResistanceIntensityCountsAsProp) ++props;
	    if (NormalizedFireResistance >= ResistanceIntensityCountsAsProp) ++props;
	    if (NormalizedColdResistance >= ResistanceIntensityCountsAsProp) ++props;
	    if (NormalizedPoisonResistance >= ResistanceIntensityCountsAsProp) ++props;
	    if (NormalizedEnergyResistance >= ResistanceIntensityCountsAsProp) ++props;

	    attrsMod += physical * (NormalizedPhysicalResistance / 10);
	    attrsMod += fire * (NormalizedFireResistance / 10);
	    attrsMod += cold * (NormalizedColdResistance / 10);
	    attrsMod += poison * (NormalizedPoisonResistance / 10);
	    attrsMod += energy * (NormalizedEnergyResistance / 10);
	}

	private static void ScalePriceOnDurability(Item item, ref int price)
	{
	    int cur_dur = 0;
	    int max_dur = 0;

	    if (item is BaseWeapon)
	    {
		cur_dur = ((BaseWeapon)item).HitPoints;
		max_dur = ((BaseWeapon)item).MaxHitPoints;
	    }
	    else if (item is BaseArmor)
	    {
		cur_dur = ((BaseArmor)item).HitPoints;
		max_dur = ((BaseArmor)item).MaxHitPoints;
	    }
	    else if (item is BaseClothing)
	    {
		cur_dur = ((BaseClothing)item).HitPoints;
		max_dur = ((BaseClothing)item).MaxHitPoints;
	    }
	    else if (item is BaseShield)
	    {
		cur_dur = ((BaseShield)item).HitPoints;
		max_dur = ((BaseShield)item).MaxHitPoints;
	    }
	    else if (item is BaseJewel)
	    {
		cur_dur = ((BaseJewel)item).HitPoints;
		max_dur = ((BaseJewel)item).MaxHitPoints;
	    }

	    if (cur_dur > 0 && max_dur > 0)
	    {
		if (max_dur <= 3)
		    price = price * PriceCutOnMaxDurability3 / 100;
		if (max_dur <= 5)
		    price = price * PriceCutOnMaxDurability5 / 100;
		if (max_dur <= 10)
		    price = price * PriceCutOnMaxDurability10 / 100;
		if (max_dur <= 15)
		    price = price * PriceCutOnMaxDurability15 / 100;
		if (max_dur <= 20)
		    price = price * PriceCutOnMaxDurability20 / 100;
		else if (max_dur <= 25)
		    price = price * PriceCutOnMaxDurability25 / 100;
	    }
	}

	// BaseWeapon
	private static void AddNormalizedBonuses(BaseWeapon bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    int id = 0;

	    if (mode == IntensityMode.AosAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
		{
		    int MaxIntensity = AosAttributeIntensities[id++];
		    int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
		    if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxIntensity > 1 )
			attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    } 
	    else if (mode == IntensityMode.AosWeaponAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosWeaponAttribute ) ) ) 
		{
		    int MaxWeaponIntensity = AosWeaponAttributeIntensities[id++];
		    int NormalizedWeaponAttribute = bw.WeaponAttributes[ (AosWeaponAttribute)i ] * 100 / MaxWeaponIntensity;
		    if ( NormalizedWeaponAttribute > 0 && NormalizedWeaponAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxWeaponIntensity > 1 )
			attrsMod += (int)(NormalizedWeaponAttribute * ( (double)NormalizedWeaponAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedWeaponAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    }
	    else if (mode == IntensityMode.AosElementAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosElementAttribute ) ) ) 
		{
		    int MaxElemIntensity = AosElementAttributeIntensities[id++];
		    int NormalizedElementalAttribute = bw.AosElementDamages[ (AosElementAttribute)i ] * 100 / MaxElemIntensity;
		    if ( NormalizedElementalAttribute > 0 && NormalizedElementalAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxElemIntensity > 1 )
			attrsMod += (int)(NormalizedElementalAttribute * ( (double)NormalizedElementalAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedElementalAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    }
	    else if (mode == IntensityMode.SkillBonus)
	    {
		AddSkillBonuses(bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
			bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
	    }
	    else
	    {
		Console.WriteLine("Unexpected mode for weapon: " + mode);
	    }
	}

	// BaseArmor
	private static void AddNormalizedBonuses(BaseArmor bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    int id = 0;

	    if (mode == IntensityMode.AosAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
		{
		    int MaxIntensity = AosAttributeIntensities[id++];
		    int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
		    if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxIntensity > 1 )
			attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    } 
	    else if (mode == IntensityMode.AosArmorAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) ) 
		{
		    int MaxArmorIntensity = AosArmorAttributeIntensities[id++];
		    int NormalizedArmorAttribute = bw.ArmorAttributes[ (AosArmorAttribute)i ] * 100 / MaxArmorIntensity;
		    if ( NormalizedArmorAttribute > 0 && NormalizedArmorAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxArmorIntensity > 1 )
			attrsMod += (int)(NormalizedArmorAttribute * ( (double)NormalizedArmorAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedArmorAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    }
	    else if (mode == IntensityMode.SkillBonus)
	    {
		AddSkillBonuses(bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
			bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
	    }
	    else if (mode == IntensityMode.ResistanceBonus)
	    {
		AddResistanceBonuses(bw.PhysicalBonus, bw.FireBonus, bw.ColdBonus, bw.PoisonBonus, bw.EnergyBonus,
			ref attrsMod, ref props);
	    }
	    else
	    {
		Console.WriteLine("Unexpected mode for armor: " + mode);
	    }
	}

	// BaseShield
	private static void AddNormalizedBonuses(BaseShield bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    int id = 0;

	    if (mode == IntensityMode.AosAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
		{
		    int MaxIntensity = AosAttributeIntensities[id++];
		    int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
		    if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxIntensity > 1 )
			attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    } 
	    else if (mode == IntensityMode.AosArmorAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) ) 
		{
		    int MaxArmorIntensity = AosArmorAttributeIntensities[id++];
		    int NormalizedArmorAttribute = bw.ArmorAttributes[ (AosArmorAttribute)i ] * 100 / MaxArmorIntensity;
		    if ( NormalizedArmorAttribute > 0 && NormalizedArmorAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxArmorIntensity > 1 )
			attrsMod += (int)(NormalizedArmorAttribute * ( (double)NormalizedArmorAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedArmorAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    }
	    else if (mode == IntensityMode.SkillBonus)
	    {
		AddSkillBonuses(bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
			bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
	    }
	    else if (mode == IntensityMode.ResistanceBonus)
	    {
		AddResistanceBonuses(bw.PhysicalBonus, bw.FireBonus, bw.ColdBonus, bw.PoisonBonus, bw.EnergyBonus,
			ref attrsMod, ref props);
	    }
	    else
	    {
		Console.WriteLine("Unexpected mode for shield: " + mode);
	    }
	}

	// BaseClothing
	private static void AddNormalizedBonuses(BaseClothing bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    int id = 0;

	    if (mode == IntensityMode.AosAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
		{
		    int MaxIntensity = AosAttributeIntensities[id++];
		    int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
		    if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxIntensity > 1 )
			attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    } 
	    else if (mode == IntensityMode.AosArmorAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) ) 
		{
		    int MaxArmorIntensity = AosArmorAttributeIntensities[id++];
		    int NormalizedArmorAttribute = bw.ClothingAttributes[ (AosArmorAttribute)i ] * 100 / MaxArmorIntensity;
		    if ( NormalizedArmorAttribute > 0 && NormalizedArmorAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxArmorIntensity > 1 )
			attrsMod += (int)(NormalizedArmorAttribute * ( (double)NormalizedArmorAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedArmorAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    }
	    else if (mode == IntensityMode.SkillBonus)
	    {
		AddSkillBonuses(bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
			bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
	    }
	    else
	    {
		Console.WriteLine("Unexpected mode for clothing: " + mode);
	    }
	}

	// BaseJewel
	private static void AddNormalizedBonuses(BaseJewel bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    int id = 0;

	    if (mode == IntensityMode.AosAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
		{
		    int MaxIntensity = AosAttributeIntensities[id++];
		    int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
		    if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxIntensity > 1 )
			attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    } 
	    else if (mode == IntensityMode.AosElementAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosElementAttribute ) ) ) 
		{
		    int MaxElemIntensity = AosElementAttributeIntensities[id++];
		    int NormalizedElementalAttribute = bw.Resistances[ (AosElementAttribute)i ] * 100 / MaxElemIntensity;
		    if ( NormalizedElementalAttribute > 0 && NormalizedElementalAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxElemIntensity > 1 )
			attrsMod += (int)(NormalizedElementalAttribute * ( (double)NormalizedElementalAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedElementalAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    }
	    else if (mode == IntensityMode.SkillBonus)
	    {
		AddSkillBonuses(bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
			bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
	    }
	    else if (mode == IntensityMode.ResistanceBonus)
	    {
		AddResistanceBonuses(bw.PhysicalResistance, bw.FireResistance, bw.ColdResistance, bw.PoisonResistance, bw.EnergyResistance,
			ref attrsMod, ref props);
	    }
	    else
	    {
		Console.WriteLine("Unexpected mode for jewel: " + mode);
	    }
	}

	// BaseQuiver
	private static void AddNormalizedBonuses(BaseQuiver bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    int id = 0;

	    if (mode == IntensityMode.AosAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
		{
		    int MaxIntensity = AosAttributeIntensities[id++];
		    int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
		    if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxIntensity > 1 )
			attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    } 
	    else
	    {
		Console.WriteLine("Unexpected mode for quiver: " + mode);
	    }
	}

	// BaseInstrument
	private static void AddNormalizedBonuses(BaseInstrument bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    int id = 0;

	    if (mode == IntensityMode.AosAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
		{
		    int MaxIntensity = AosAttributeIntensities[id++];
		    int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
		    if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxIntensity > 1 )
			attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    } 
	    else if (mode == IntensityMode.SkillBonus)
	    {
		AddSkillBonuses(bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
			bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
	    }
	    else if (mode == IntensityMode.ResistanceBonus)
	    {
		AddResistanceBonuses(bw.PhysicalResistance, bw.FireResistance, bw.ColdResistance, bw.PoisonResistance, bw.EnergyResistance,
			ref attrsMod, ref props);
	    }
	    else
	    {
		Console.WriteLine("Unexpected mode for instrument: " + mode);
	    }
	}

	// Spellbook
	private static void AddNormalizedBonuses(Spellbook bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    int id = 0;

	    if (mode == IntensityMode.AosAttribute)
	    {
		foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
		{
		    int MaxIntensity = AosAttributeIntensities[id++];
		    int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
		    if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

		    if ( MaxIntensity > 1 )
			attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
		    else if ( NormalizedAttribute > 0 )
			attrsMod += Utility.RandomMinMax(50, 100);
		}
	    } 
	    else if (mode == IntensityMode.SkillBonus)
	    {
		AddSkillBonuses(bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
			bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
	    }
	    else if (mode == IntensityMode.ResistanceBonus)
	    {
		AddResistanceBonuses(bw.PhysicalResistance, bw.FireResistance, bw.ColdResistance, bw.PoisonResistance, bw.EnergyResistance,
			ref attrsMod, ref props);
	    }
	    else
	    {
		Console.WriteLine("Unexpected mode for spellbook: " + mode);
	    }
	}

	// BaseRunicTool
	private static void AddNormalizedBonuses(BaseRunicTool bw, IntensityMode mode, ref int attrsMod, ref int props)
	{
	    if (mode == IntensityMode.RunicToolProperties)
	    {
		attrsMod += 1000;

		switch ( bw.Resource )
		{
		    case CraftResource.DullCopper: attrsMod = (int)( attrsMod * 1.25 ); break;
		    case CraftResource.ShadowIron: attrsMod = (int)( attrsMod * 1.5 ); break;
		    case CraftResource.Copper: attrsMod = (int)( attrsMod * 1.75 ); break;
		    case CraftResource.Bronze: attrsMod = (int)( attrsMod * 2 ); break;
		    case CraftResource.Gold: attrsMod = (int)( attrsMod * 2.25 ); break;
		    case CraftResource.Agapite: attrsMod = (int)( attrsMod * 2.50 ); break;
		    case CraftResource.Verite: attrsMod = (int)( attrsMod * 2.75 ); break;
		    case CraftResource.Valorite: attrsMod = (int)( attrsMod * 3 ); break;
		    case CraftResource.Nepturite: attrsMod = (int)( attrsMod * 3.10 ); break;
		    case CraftResource.Obsidian: attrsMod = (int)( attrsMod * 3.10 ); break;
		    case CraftResource.Steel: attrsMod = (int)( attrsMod * 3.25 ); break;
		    case CraftResource.Brass: attrsMod = (int)( attrsMod * 3.5 ); break;
		    case CraftResource.Mithril: attrsMod = (int)( attrsMod * 3.75 ); break;
		    case CraftResource.Xormite: attrsMod = (int)( attrsMod * 3.75 ); break;
		    case CraftResource.Dwarven: attrsMod = (int)( attrsMod * 7.50 ); break;
		    case CraftResource.SpinedLeather: attrsMod = (int)( attrsMod * 1.5 ); break;
		    case CraftResource.HornedLeather: attrsMod = (int)( attrsMod * 1.75 ); break;
		    case CraftResource.BarbedLeather: attrsMod = (int)( attrsMod * 2.0 ); break;
		    case CraftResource.NecroticLeather: attrsMod = (int)( attrsMod * 2.25 ); break;
		    case CraftResource.VolcanicLeather: attrsMod = (int)( attrsMod * 2.5 ); break;
		    case CraftResource.FrozenLeather: attrsMod = (int)( attrsMod * 2.75 ); break;
		    case CraftResource.GoliathLeather: attrsMod = (int)( attrsMod * 3.0 ); break;
		    case CraftResource.DraconicLeather: attrsMod = (int)( attrsMod * 3.25 ); break;
		    case CraftResource.HellishLeather: attrsMod = (int)( attrsMod * 3.5 ); break;
		    case CraftResource.DinosaurLeather: attrsMod = (int)( attrsMod * 3.75 ); break;
		    case CraftResource.AlienLeather: attrsMod = (int)( attrsMod * 3.75 ); break;
		    case CraftResource.RedScales: attrsMod = (int)( attrsMod * 1.25 ); break;
		    case CraftResource.YellowScales: attrsMod = (int)( attrsMod * 1.25 ); break;
		    case CraftResource.BlackScales: attrsMod = (int)( attrsMod * 1.5 ); break;
		    case CraftResource.GreenScales: attrsMod = (int)( attrsMod * 1.5 ); break;
		    case CraftResource.WhiteScales: attrsMod = (int)( attrsMod * 1.5 ); break;
		    case CraftResource.BlueScales: attrsMod = (int)( attrsMod * 1.5 ); break;
		    case CraftResource.AshTree: attrsMod = (int)( attrsMod * 1.25 ); break;
		    case CraftResource.CherryTree: attrsMod = (int)( attrsMod * 1.45 ); break;
		    case CraftResource.EbonyTree: attrsMod = (int)( attrsMod * 1.65 ); break;
		    case CraftResource.GoldenOakTree: attrsMod = (int)( attrsMod * 1.85 ); break;
		    case CraftResource.HickoryTree: attrsMod = (int)( attrsMod * 2.05 ); break;
		    case CraftResource.MahoganyTree: attrsMod = (int)( attrsMod * 2.25 ); break;
		    case CraftResource.DriftwoodTree: attrsMod = (int)( attrsMod * 2.25 ); break;
		    case CraftResource.OakTree: attrsMod = (int)( attrsMod * 2.45 ); break;
		    case CraftResource.PineTree: attrsMod = (int)( attrsMod * 2.65 ); break;
		    case CraftResource.GhostTree: attrsMod = (int)( attrsMod * 2.65 ); break;
		    case CraftResource.RosewoodTree: attrsMod = (int)( attrsMod * 2.85 ); break;
		    case CraftResource.WalnutTree: attrsMod = (int)( attrsMod * 3 ); break;
		    case CraftResource.ElvenTree: attrsMod = (int)( attrsMod * 6 ); break;
		    case CraftResource.PetrifiedTree: attrsMod = (int)( attrsMod * 3.25 ); break;
		}

		attrsMod -= (50 - bw.UsesRemaining) * 30;
		if (attrsMod < 0)
		    attrsMod = 0;
	    }
	    else
	    {
		Console.WriteLine("Unexpected mode for runic tool: " + mode);
	    }
	}

	public static void GetItemTier( Item ii, out int attrsValue, out int valuableProps )
	{
	    if (ii == null) {
		attrsValue = 0;
		valuableProps = 0;
		return;
	    }

	    int attrsMod = 0;
	    int props = 0;

	    if (ii is BaseWeapon)
	    {
		BaseWeapon bw = ii as BaseWeapon;

		AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.AosWeaponAttribute, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.AosElementAttribute, ref attrsMod, ref props);

		AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);

		if(bw.Slayer != SlayerName.None) ++props;
		if(bw.Slayer2 != SlayerName.None) ++props;

		if (bw.Slayer != SlayerName.None) 
		{
		    attrsMod += 100;
		    props++;
		}
		if (bw.Slayer2 != SlayerName.None) 
		{
		    attrsMod += 100;
		    props++;
		}

		if (props >= 3 && (bw.WeaponAttributes.MageWeapon > 0 || bw.Attributes.SpellChanneling > 0))
		    attrsMod = (int)((double)attrsMod * 1.3);
	    }
	    else if (ii is BaseArmor)
	    {
		BaseArmor bw = ii as BaseArmor;

		AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.AosArmorAttribute, ref attrsMod, ref props);

		AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);

		if (props >= 3 && bw.ArmorAttributes.MageArmor > 0 || bw.Attributes.SpellChanneling > 0)
		    attrsMod = (int)((double)attrsMod * 1.3);

	    }
	    else if (ii is BaseClothing)
	    {
		BaseClothing bw = ii as BaseClothing;

		AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.AosArmorAttribute, ref attrsMod, ref props);

		AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);

		if (props >= 3 && bw.ClothingAttributes.MageArmor > 0 || bw.Attributes.SpellChanneling > 0)
		    attrsMod = (int)((double)attrsMod * 1.3);
	    }
	    else if (ii is BaseJewel)
	    {
		BaseJewel bw = ii as BaseJewel;

		AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.AosElementAttribute, ref attrsMod, ref props);

		AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);
	    }
	    else if (ii is BaseShield)
	    {
		BaseShield bw = ii as BaseShield;

		AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.AosArmorAttribute, ref attrsMod, ref props);

		AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);

		if (props >= 3 && bw.ArmorAttributes.MageArmor > 0 || bw.Attributes.SpellChanneling > 0)
		    attrsMod = (int)((double)attrsMod * 1.3);
	    }
	    else if (ii is BaseQuiver)
	    {
		BaseQuiver bw = ii as BaseQuiver;

		AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
	    }
	    else if (ii is BaseInstrument)
	    {
		BaseInstrument bw = ii as BaseInstrument;

		AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);

		AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);
	    }
	    else if (ii is BaseRunicTool)
	    {
		BaseRunicTool bw = ii as BaseRunicTool;

		AddNormalizedBonuses(bw, IntensityMode.RunicToolProperties, ref attrsMod, ref props);
	    }
	    else if (ii is Spellbook)
	    {
		Spellbook bw = ii as Spellbook;

		AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);

		AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
		AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);

		if(bw.Slayer != SlayerName.None) ++props;
		if(bw.Slayer2 != SlayerName.None) ++props;

		if (bw.SpellCount > 0)
		{
		    attrsMod += bw.SpellCount * 20; // TODO: make the higher circle spells cost more
		}
		if (bw.Slayer != SlayerName.None)
		{
		    attrsMod += 100;
		}
		if (bw.Slayer2 != SlayerName.None)
		{
		    attrsMod += 100;
		}
	    }

	    if (IncreasePriceBasedOnNumberOfProps)
	    {
		if (props == 1 || props == 2) { attrsMod *= AttrsMod1Or2Props; }
		else if (props == 3 || props == 4) { attrsMod *= AttrsMod3Or4Props; }
		else if (props == 5 || props == 6) { attrsMod *= AttrsMod5Or6Props; }
		else if (props == 7 || props == 8) { attrsMod *= AttrsMod7Or8Props; }
		else if (props >= 9) { attrsMod *= AttrsMod9OrMoreProps; }
	    }

	    attrsValue = attrsMod;
	    valuableProps = props;
	}
    }
}
