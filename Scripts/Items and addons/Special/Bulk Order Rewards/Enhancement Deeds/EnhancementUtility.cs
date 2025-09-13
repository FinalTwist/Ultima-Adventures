using System;
using Server;

namespace Server.Custom
{
    public class EnhancementUtility
    {
        public static bool EDStoneExists()
        {
            foreach (Item i in World.Items.Values)
                if (i is EDStone) return true;
            return false;
        }

        public static void Increment(Mobile mobile, Item item)
        {
            if (EDStone.Enhanced.ContainsKey(mobile.Serial.Value))
                EDStone.Enhanced[mobile.Serial.Value]++;
            else
                EDStone.Enhanced.Add(mobile.Serial.Value, 1);

            if (EDStone.Enhanced.ContainsKey(item.Serial.Value))
            {
                int itemcount = EDStone.Enhanced[item.Serial.Value];
                EDStone.Enhanced[item.Serial.Value]++;
                if (itemcount > 15)
                {
                    if (itemcount > 20)
                    {
                        mobile.SendMessage("The item has been destroyed due to over-enhancement!");
                        item.Delete();
                    }
                    else
                        mobile.SendMessage("WARNING! Item enhanced {0} times. Item will be destroyed if it is enhanced {1} more time{2}."
                            , itemcount, 21 - itemcount, 21 - itemcount == 1 ? "" : "s");
                }
            }
            else
                EDStone.Enhanced.Add(item.Serial.Value, 1);
        }

        public static int GetValue(AosArmorAttribute attribute)
        {
            switch (attribute)
            {
                //**************** MAKE CHANGES STARTING HERE ****************************
                case AosArmorAttribute.DurabilityBonus: { return Utility.RandomMinMax(20, 50); }
                case AosArmorAttribute.LowerStatReq: { return Utility.RandomMinMax(5, 15); }
                case AosArmorAttribute.MageArmor: { return 1; }
                case AosArmorAttribute.SelfRepair: { return Utility.RandomMinMax(1, 2); }
                //**************** MAKE CHANGES ENDING HERE ****************************
            }
            return 0;
        }

        public static int GetValue(AosAttribute attribute)
        {
            switch (attribute)
            {
                //**************** MAKE CHANGES STARTING HERE ****************************
                case AosAttribute.RegenHits: { return Utility.RandomMinMax(5, 15); }
                case AosAttribute.RegenStam: { return Utility.RandomMinMax(5, 15); }
                case AosAttribute.RegenMana: { return Utility.RandomMinMax(5, 15); }
                case AosAttribute.DefendChance: { return Utility.RandomMinMax(5, 10); }
                case AosAttribute.AttackChance: { return Utility.RandomMinMax(5, 10); }
                case AosAttribute.BonusStr: { return Utility.RandomMinMax(2, 10); }
                case AosAttribute.BonusDex: { return Utility.RandomMinMax(2, 10); }
                case AosAttribute.BonusInt: { return Utility.RandomMinMax(2, 10); }
                case AosAttribute.BonusHits: { return Utility.RandomMinMax(5, 20); }
                case AosAttribute.BonusStam: { return Utility.RandomMinMax(10, 20); }
                case AosAttribute.BonusMana: { return Utility.RandomMinMax(10, 20); }
                case AosAttribute.WeaponDamage: { return Utility.RandomMinMax(5, 20); }
                case AosAttribute.WeaponSpeed: { return Utility.RandomMinMax(5, 20); }
                case AosAttribute.SpellDamage: { return Utility.RandomMinMax(10, 20); }
                case AosAttribute.CastRecovery: { return Utility.RandomMinMax(1, 2); }
                case AosAttribute.CastSpeed: { return Utility.RandomMinMax(1, 2); }
                case AosAttribute.LowerManaCost: { return Utility.RandomMinMax(4, 20); }
                case AosAttribute.LowerRegCost: { return Utility.RandomMinMax(5, 20); }
                case AosAttribute.ReflectPhysical: { return Utility.RandomMinMax(5, 20); }
                case AosAttribute.EnhancePotions: { return Utility.RandomMinMax(5, 20); }
                case AosAttribute.Luck: { return Utility.RandomMinMax(10, 200); }
                case AosAttribute.SpellChanneling: { return 1; }
                case AosAttribute.NightSight: { return 1; }
                //**************** MAKE CHANGES ENDING HERE ****************************
            }
            return 0;
        }

        public static int GetValue(AosWeaponAttribute attribute)
        {
            switch (attribute)
            {
                //**************** MAKE CHANGES STARTING HERE ****************************
                case AosWeaponAttribute.HitLeechHits: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitLeechStam: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitLeechMana: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitLowerAttack: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitLowerDefend: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitMagicArrow: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitHarm: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitFireball: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitLightning: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitDispel: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.HitColdArea: { return Utility.RandomMinMax(2, 10); }
                case AosWeaponAttribute.HitFireArea: { return Utility.RandomMinMax(2, 10); }
                case AosWeaponAttribute.HitPoisonArea: { return Utility.RandomMinMax(2, 10); }
                case AosWeaponAttribute.HitEnergyArea: { return Utility.RandomMinMax(2, 10); }
                case AosWeaponAttribute.HitPhysicalArea: { return Utility.RandomMinMax(2, 10); }
                case AosWeaponAttribute.ResistPhysicalBonus: { return Utility.RandomMinMax(10, 50); }
                case AosWeaponAttribute.ResistFireBonus: { return Utility.RandomMinMax(10, 50); }
                case AosWeaponAttribute.ResistColdBonus: { return Utility.RandomMinMax(10, 50); }
                case AosWeaponAttribute.ResistPoisonBonus: { return Utility.RandomMinMax(10, 50); }
                case AosWeaponAttribute.ResistEnergyBonus: { return Utility.RandomMinMax(10, 50); }
                case AosWeaponAttribute.UseBestSkill: { return 1; }
                case AosWeaponAttribute.MageWeapon: { return 1; }
                case AosWeaponAttribute.DurabilityBonus: { return Utility.RandomMinMax(20, 50); }
                case AosWeaponAttribute.LowerStatReq: { return Utility.RandomMinMax(5, 20); }
                case AosWeaponAttribute.SelfRepair: { return Utility.RandomMinMax(1, 2); }
                //**************** MAKE CHANGES ENDING HERE ****************************
            }
            return 0;
        }

        public static int GetValue(AosElementAttribute attribute)
        {
            switch (attribute)
            {
                //**************** MAKE CHANGES STARTING HERE ****************************
                case AosElementAttribute.Cold: { return Utility.RandomMinMax(5, 25); }
                case AosElementAttribute.Energy: { return Utility.RandomMinMax(5, 25); }
                case AosElementAttribute.Fire: { return Utility.RandomMinMax(5, 25); }
                case AosElementAttribute.Physical: { return Utility.RandomMinMax(5, 25); }
                case AosElementAttribute.Poison: { return Utility.RandomMinMax(5, 25); }
                //**************** MAKE CHANGES ENDING HERE ****************************
            }
            return 0;
        }

        public static int GetValue(SkillName attribute)
        {
            switch (attribute)
            {
                //**************** MAKE CHANGES STARTING HERE ****************************
                default: { return 10; }
                //**************** MAKE CHANGES ENDING HERE ****************************
            }
            return 0;
        }

        public static int GetMaxValue(AosArmorAttribute attribute)
        {
            if (!EDStoneExists()) return -1;
            switch (attribute)
            {
                case AosArmorAttribute.DurabilityBonus: { return EDStone.ArmorDurabilityBonusMax; }
                case AosArmorAttribute.LowerStatReq: { return EDStone.ArmorLowerStatReqMax; }
                case AosArmorAttribute.MageArmor: { return EDStone.ArmorMageArmorMax; }
                case AosArmorAttribute.SelfRepair: { return EDStone.ArmorSelfRepairMax; }
            }
            return 0;
        }

        public static int GetMaxValue(AosAttribute attribute)
        {
            if (!EDStoneExists()) return -1;
            switch (attribute)
            {
                case AosAttribute.RegenHits: { return EDStone.AosRegenHitsMax; }
                case AosAttribute.RegenStam: { return EDStone.AosRegenStamMax; }
                case AosAttribute.RegenMana: { return EDStone.AosRegenManaMax; }
                case AosAttribute.DefendChance: { return EDStone.AosDefendChanceMax; }
                case AosAttribute.AttackChance: { return EDStone.AosAttackChanceMax; }
                case AosAttribute.BonusStr: { return EDStone.AosBonusStrMax; }
                case AosAttribute.BonusDex: { return EDStone.AosBonusDexMax; }
                case AosAttribute.BonusInt: { return EDStone.AosBonusIntMax; }
                case AosAttribute.BonusHits: { return EDStone.AosBonusHitsMax; }
                case AosAttribute.BonusStam: { return EDStone.AosBonusStamMax; }
                case AosAttribute.BonusMana: { return EDStone.AosBonusManaMax; }
                case AosAttribute.WeaponDamage: { return EDStone.AosWeaponDamageMax; }
                case AosAttribute.WeaponSpeed: { return EDStone.AosWeaponSpeedMax; }
                case AosAttribute.SpellDamage: { return EDStone.AosSpellDamageMax; }
                case AosAttribute.CastRecovery: { return EDStone.AosCastRecoveryMax; }
                case AosAttribute.CastSpeed: { return EDStone.AosCastSpeedMax; }
                case AosAttribute.LowerManaCost: { return EDStone.AosLowerManaCostMax; }
                case AosAttribute.LowerRegCost: { return EDStone.AosLowerRegCostMax; }
                case AosAttribute.ReflectPhysical: { return EDStone.AosReflectPhysicalMax; }
                case AosAttribute.EnhancePotions: { return EDStone.AosEnhancePotionsMax; }
                case AosAttribute.Luck: { return EDStone.AosLuckMax; }
                case AosAttribute.SpellChanneling: { return EDStone.AosSpellChannelingMax; }
                case AosAttribute.NightSight: { return EDStone.AosNightSightMax; }
            }
            return 0;
        }

        public static int GetMaxValue(AosWeaponAttribute attribute)
        {
            if (!EDStoneExists()) return -1;
            switch (attribute)
            {
                case AosWeaponAttribute.HitLeechHits: { return EDStone.WeaponHitLeechHitsMax; }
                case AosWeaponAttribute.HitLeechStam: { return EDStone.WeaponHitLeechStamMax; }
                case AosWeaponAttribute.HitLeechMana: { return EDStone.WeaponHitLeechManaMax; }
                case AosWeaponAttribute.HitLowerAttack: { return EDStone.WeaponHitLowerAttackMax; }
                case AosWeaponAttribute.HitLowerDefend: { return EDStone.WeaponHitLowerDefendMax; }
                case AosWeaponAttribute.HitMagicArrow: { return EDStone.WeaponHitMagicArrowMax; }
                case AosWeaponAttribute.HitHarm: { return EDStone.WeaponHitHarmMax; }
                case AosWeaponAttribute.HitFireball: { return EDStone.WeaponHitFireballMax; }
                case AosWeaponAttribute.HitLightning: { return EDStone.WeaponHitLightningMax; }
                case AosWeaponAttribute.HitDispel: { return EDStone.WeaponHitDispelMax; }
                case AosWeaponAttribute.HitColdArea: { return EDStone.WeaponHitColdAreaMax; }
                case AosWeaponAttribute.HitFireArea: { return EDStone.WeaponHitFireAreaMax; }
                case AosWeaponAttribute.HitPoisonArea: { return EDStone.WeaponHitPoisonAreaMax; }
                case AosWeaponAttribute.HitEnergyArea: { return EDStone.WeaponHitEnergyAreaMax; }
                case AosWeaponAttribute.HitPhysicalArea: { return EDStone.WeaponHitPhysicalAreaMax; }
                case AosWeaponAttribute.ResistPhysicalBonus: { return EDStone.WeaponResistPhysicalBonusMax; }
                case AosWeaponAttribute.ResistFireBonus: { return EDStone.WeaponResistFireBonusMax; }
                case AosWeaponAttribute.ResistColdBonus: { return EDStone.WeaponResistColdBonusMax; }
                case AosWeaponAttribute.ResistPoisonBonus: { return EDStone.WeaponResistPoisonBonusMax; }
                case AosWeaponAttribute.ResistEnergyBonus: { return EDStone.WeaponResistEnergyBonusMax; }
                case AosWeaponAttribute.UseBestSkill: { return EDStone.WeaponUseBestSkillMax; }
                case AosWeaponAttribute.MageWeapon: { return EDStone.WeaponMageWeaponMax; }
                case AosWeaponAttribute.DurabilityBonus: { return EDStone.WeaponDurabilityBonusMax; }
                case AosWeaponAttribute.LowerStatReq: { return EDStone.WeaponLowerStatReqMax; }
                case AosWeaponAttribute.SelfRepair: { return EDStone.WeaponSelfRepairMax; }
            }
            return 0;
        }

        public static int GetMaxValue(AosElementAttribute attribute)
        {
            if (!EDStoneExists()) return -1;
            switch (attribute)
            {
                case AosElementAttribute.Cold: { return EDStone.ElementColdMax; }
                case AosElementAttribute.Energy: { return EDStone.ElementEnergyMax; }
                case AosElementAttribute.Fire: { return EDStone.ElementFireMax; }
                case AosElementAttribute.Physical: { return EDStone.ElementPhysicalMax; }
                case AosElementAttribute.Poison: { return EDStone.ElementPoisonMax; }
            }
            return 0;
        }

        public static int GetMaxValue(SkillName attribute)
        {
            if (!EDStoneExists()) return -1;
            switch (attribute)
            {
                default: { return EDStone.SkillMax; }
            }
            return 0;
        }
    }
}
