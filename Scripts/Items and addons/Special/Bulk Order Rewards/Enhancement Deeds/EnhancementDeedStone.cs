using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Custom
{
    public class EDStone : Item
    {
        private static Dictionary<int, int> m_Enhanced;

        //**************** MAKE CHANGES STARTING HERE ****************************
        private static int m_MaxEnhancements = 20;
        //**************** MAKE CHANGES ENDING HERE ****************************

        /// <summary>
        /// Collection of values where <int,int> represents the Serial value and Number of enhancements
        /// for each Mobile that used an EnhancementDeed and each Item that was Enhanced by a Deed.
        /// </summary>
        public static Dictionary<int, int> Enhanced { get { return m_Enhanced; } set { m_Enhanced = value; } }
        
        //**************** MAKE CHANGES STARTING HERE ****************************
        private static int m_AosRegenHitsMax = 100;
        private static int m_AosRegenStamMax = 100;
        private static int m_AosRegenManaMax = 100;
        private static int m_AosDefendChanceMax = 100;
        private static int m_AosAttackChanceMax = 100;
        private static int m_AosBonusStrMax = 100;
        private static int m_AosBonusDexMax = 100;
        private static int m_AosBonusIntMax = 100;
        private static int m_AosBonusHitsMax = 100;
        private static int m_AosBonusStamMax = 100;
        private static int m_AosBonusManaMax = 100;
        private static int m_AosWeaponDamageMax = 100;
        private static int m_AosWeaponSpeedMax = 100;
        private static int m_AosSpellDamageMax = 100;
        private static int m_AosCastRecoveryMax = 100;
        private static int m_AosCastSpeedMax = 100;
        private static int m_AosLowerManaCostMax = 100;
        private static int m_AosLowerRegCostMax = 100;
        private static int m_AosReflectPhysicalMax = 100;
        private static int m_AosEnhancePotionsMax = 100;
        private static int m_AosLuckMax = 100;
        private static int m_AosSpellChannelingMax = 1;
        private static int m_AosNightSightMax = 1;

        private static int m_WeaponLowerStatReqMax = 100;
        private static int m_WeaponSelfRepairMax = 100;
        private static int m_WeaponHitLeechHitsMax = 100;
        private static int m_WeaponHitLeechStamMax = 100;
        private static int m_WeaponHitLeechManaMax = 100;
        private static int m_WeaponHitLowerAttackMax = 100;
        private static int m_WeaponHitLowerDefendMax = 100;
        private static int m_WeaponHitMagicArrowMax = 100;
        private static int m_WeaponHitHarmMax = 100;
        private static int m_WeaponHitFireballMax = 100;
        private static int m_WeaponHitLightningMax = 100;
        private static int m_WeaponHitDispelMax = 100;
        private static int m_WeaponHitColdAreaMax = 100;
        private static int m_WeaponHitFireAreaMax = 100;
        private static int m_WeaponHitPoisonAreaMax = 100;
        private static int m_WeaponHitEnergyAreaMax = 100;
        private static int m_WeaponHitPhysicalAreaMax = 100;
        private static int m_WeaponResistPhysicalBonusMax = 100;
        private static int m_WeaponResistFireBonusMax = 100;
        private static int m_WeaponResistColdBonusMax = 100;
        private static int m_WeaponResistPoisonBonusMax = 100;
        private static int m_WeaponResistEnergyBonusMax = 100;
        private static int m_WeaponUseBestSkillMax = 1;
        private static int m_WeaponMageWeaponMax = 1;
        private static int m_WeaponDurabilityBonusMax = 100;

        private static int m_ArmorLowerStatReqMax = 100;
        private static int m_ArmorSelfRepairMax = 100;
        private static int m_ArmorMageArmorMax = 1;
        private static int m_ArmorDurabilityBonusMax = 100;

        private static int m_ElementPhysicalMax = 70;
        private static int m_ElementFireMax = 70;
        private static int m_ElementColdMax = 70;
        private static int m_ElementPoisonMax = 70;
        private static int m_ElementEnergyMax = 70;

        private static int m_SkillMax = 100;
        //**************** MAKE CHANGES ENDING HERE ****************************

        #region Getters/Setters

        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosRegenHitsMax { get { return m_AosRegenHitsMax; } set { m_AosRegenHitsMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosRegenStamMax { get { return m_AosRegenStamMax; } set { m_AosRegenStamMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosRegenManaMax { get { return m_AosRegenManaMax; } set { m_AosRegenManaMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosDefendChanceMax { get { return m_AosDefendChanceMax; } set { m_AosDefendChanceMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosAttackChanceMax { get { return m_AosAttackChanceMax; } set { m_AosAttackChanceMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosBonusStrMax { get { return m_AosBonusStrMax; } set { m_AosBonusStrMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosBonusDexMax { get { return m_AosBonusDexMax; } set { m_AosBonusDexMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosBonusIntMax { get { return m_AosBonusIntMax; } set { m_AosBonusIntMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosBonusHitsMax { get { return m_AosBonusHitsMax; } set { m_AosBonusHitsMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosBonusStamMax { get { return m_AosBonusStamMax; } set { m_AosBonusStamMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosBonusManaMax { get { return m_AosBonusManaMax; } set { m_AosBonusManaMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosWeaponDamageMax { get { return m_AosWeaponDamageMax; } set { m_AosWeaponDamageMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosWeaponSpeedMax { get { return m_AosWeaponSpeedMax; } set { m_AosWeaponSpeedMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosSpellDamageMax { get { return m_AosSpellDamageMax; } set { m_AosSpellDamageMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosCastRecoveryMax { get { return m_AosCastRecoveryMax; } set { m_AosCastRecoveryMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosCastSpeedMax { get { return m_AosCastSpeedMax; } set { m_AosCastSpeedMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosLowerManaCostMax { get { return m_AosLowerManaCostMax; } set { m_AosLowerManaCostMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosLowerRegCostMax { get { return m_AosLowerRegCostMax; } set { m_AosLowerRegCostMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosReflectPhysicalMax { get { return m_AosReflectPhysicalMax; } set { m_AosReflectPhysicalMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosEnhancePotionsMax { get { return m_AosEnhancePotionsMax; } set { m_AosEnhancePotionsMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosLuckMax { get { return m_AosLuckMax; } set { m_AosLuckMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosSpellChannelingMax { get { return m_AosSpellChannelingMax; } set { m_AosSpellChannelingMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int AosNightSightMax { get { return m_AosNightSightMax; } set { m_AosNightSightMax = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponLowerStatReqMax { get { return m_WeaponLowerStatReqMax; } set { m_WeaponLowerStatReqMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponSelfRepairMax { get { return m_WeaponSelfRepairMax; } set { m_WeaponSelfRepairMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitLeechHitsMax { get { return m_WeaponHitLeechHitsMax; } set { m_WeaponHitLeechHitsMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitLeechStamMax { get { return m_WeaponHitLeechStamMax; } set { m_WeaponHitLeechStamMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitLeechManaMax { get { return m_WeaponHitLeechManaMax; } set { m_WeaponHitLeechManaMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitLowerAttackMax { get { return m_WeaponHitLowerAttackMax; } set { m_WeaponHitLowerAttackMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitLowerDefendMax { get { return m_WeaponHitLowerDefendMax; } set { m_WeaponHitLowerDefendMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitMagicArrowMax { get { return m_WeaponHitMagicArrowMax; } set { m_WeaponHitMagicArrowMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitHarmMax { get { return m_WeaponHitHarmMax; } set { m_WeaponHitHarmMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitFireballMax { get { return m_WeaponHitFireballMax; } set { m_WeaponHitFireballMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitLightningMax { get { return m_WeaponHitLightningMax; } set { m_WeaponHitLightningMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitDispelMax { get { return m_WeaponHitDispelMax; } set { m_WeaponHitDispelMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitColdAreaMax { get { return m_WeaponHitColdAreaMax; } set { m_WeaponHitColdAreaMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitFireAreaMax { get { return m_WeaponHitFireAreaMax; } set { m_WeaponHitFireAreaMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitPoisonAreaMax { get { return m_WeaponHitPoisonAreaMax; } set { m_WeaponHitPoisonAreaMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitEnergyAreaMax { get { return m_WeaponHitEnergyAreaMax; } set { m_WeaponHitEnergyAreaMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponHitPhysicalAreaMax { get { return m_WeaponHitPhysicalAreaMax; } set { m_WeaponHitPhysicalAreaMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponResistPhysicalBonusMax { get { return m_WeaponResistPhysicalBonusMax; } set { m_WeaponResistPhysicalBonusMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponResistFireBonusMax { get { return m_WeaponResistFireBonusMax; } set { m_WeaponResistFireBonusMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponResistColdBonusMax { get { return m_WeaponResistColdBonusMax; } set { m_WeaponResistColdBonusMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponResistPoisonBonusMax { get { return m_WeaponResistPoisonBonusMax; } set { m_WeaponResistPoisonBonusMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponResistEnergyBonusMax { get { return m_WeaponResistEnergyBonusMax; } set { m_WeaponResistEnergyBonusMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponUseBestSkillMax { get { return m_WeaponUseBestSkillMax; } set { m_WeaponUseBestSkillMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponMageWeaponMax { get { return m_WeaponMageWeaponMax; } set { m_WeaponMageWeaponMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int WeaponDurabilityBonusMax { get { return m_WeaponDurabilityBonusMax; } set { m_WeaponDurabilityBonusMax = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public static int ArmorLowerStatReqMax { get { return m_ArmorLowerStatReqMax; } set { m_ArmorLowerStatReqMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int ArmorSelfRepairMax { get { return m_ArmorSelfRepairMax; } set { m_ArmorSelfRepairMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int ArmorMageArmorMax { get { return m_ArmorMageArmorMax; } set { m_ArmorMageArmorMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int ArmorDurabilityBonusMax { get { return m_ArmorDurabilityBonusMax; } set { m_ArmorDurabilityBonusMax = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public static int ElementPhysicalMax { get { return m_ElementPhysicalMax; } set { m_ElementPhysicalMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int ElementFireMax { get { return m_ElementFireMax; } set { m_ElementFireMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int ElementColdMax { get { return m_ElementColdMax; } set { m_ElementColdMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int ElementPoisonMax { get { return m_ElementPoisonMax; } set { m_ElementPoisonMax = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public static int ElementEnergyMax { get { return m_ElementEnergyMax; } set { m_ElementEnergyMax = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public static int SkillMax { get { return m_SkillMax; } set { m_SkillMax = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public static int MaxEnhancements { get { return m_MaxEnhancements; } set { m_MaxEnhancements = value; } }

        #endregion

        public override string DefaultName
        {
            get { return "an Enhancement Deed Stone"; }
        }

        [Constructable]
        public EDStone()
            : base(0xED4)
        {
            Movable = false;
            Hue = 0x105;

            try
            {
                if (m_Enhanced.Count < 1)
                {
                    m_Enhanced = new Dictionary<int, int>();
                    m_Enhanced.Add(0, 0);
                }
            }
            catch
            {
                m_Enhanced = new Dictionary<int, int>();
                m_Enhanced.Add(0, 0);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
        }

        public EDStone(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_MaxEnhancements);

            #region Serialize Max Values

            writer.Write((int)m_AosRegenHitsMax);
            writer.Write((int)m_AosRegenStamMax);
            writer.Write((int)m_AosRegenManaMax);
            writer.Write((int)m_AosDefendChanceMax);
            writer.Write((int)m_AosAttackChanceMax);
            writer.Write((int)m_AosBonusStrMax);
            writer.Write((int)m_AosBonusDexMax);
            writer.Write((int)m_AosBonusIntMax);
            writer.Write((int)m_AosBonusHitsMax);
            writer.Write((int)m_AosBonusStamMax);
            writer.Write((int)m_AosBonusManaMax);
            writer.Write((int)m_AosWeaponDamageMax);
            writer.Write((int)m_AosWeaponSpeedMax);
            writer.Write((int)m_AosSpellDamageMax);
            writer.Write((int)m_AosCastRecoveryMax);
            writer.Write((int)m_AosCastSpeedMax);
            writer.Write((int)m_AosLowerManaCostMax);
            writer.Write((int)m_AosLowerRegCostMax);
            writer.Write((int)m_AosReflectPhysicalMax);
            writer.Write((int)m_AosEnhancePotionsMax);
            writer.Write((int)m_AosLuckMax);
            writer.Write((int)m_AosSpellChannelingMax);
            writer.Write((int)m_AosNightSightMax);

            writer.Write((int)m_WeaponLowerStatReqMax);
            writer.Write((int)m_WeaponSelfRepairMax);
            writer.Write((int)m_WeaponHitLeechHitsMax);
            writer.Write((int)m_WeaponHitLeechStamMax);
            writer.Write((int)m_WeaponHitLeechManaMax);
            writer.Write((int)m_WeaponHitLowerAttackMax);
            writer.Write((int)m_WeaponHitLowerDefendMax);
            writer.Write((int)m_WeaponHitMagicArrowMax);
            writer.Write((int)m_WeaponHitHarmMax);
            writer.Write((int)m_WeaponHitFireballMax);
            writer.Write((int)m_WeaponHitLightningMax);
            writer.Write((int)m_WeaponHitDispelMax);
            writer.Write((int)m_WeaponHitColdAreaMax);
            writer.Write((int)m_WeaponHitFireAreaMax);
            writer.Write((int)m_WeaponHitPoisonAreaMax);
            writer.Write((int)m_WeaponHitEnergyAreaMax);
            writer.Write((int)m_WeaponHitPhysicalAreaMax);
            writer.Write((int)m_WeaponResistPhysicalBonusMax);
            writer.Write((int)m_WeaponResistFireBonusMax);
            writer.Write((int)m_WeaponResistColdBonusMax);
            writer.Write((int)m_WeaponResistPoisonBonusMax);
            writer.Write((int)m_WeaponResistEnergyBonusMax);
            writer.Write((int)m_WeaponUseBestSkillMax);
            writer.Write((int)m_WeaponMageWeaponMax);
            writer.Write((int)m_WeaponDurabilityBonusMax);

            writer.Write((int)m_ArmorLowerStatReqMax);
            writer.Write((int)m_ArmorSelfRepairMax);
            writer.Write((int)m_ArmorMageArmorMax);
            writer.Write((int)m_ArmorDurabilityBonusMax);

            writer.Write((int)m_ElementPhysicalMax);
            writer.Write((int)m_ElementFireMax);
            writer.Write((int)m_ElementColdMax);
            writer.Write((int)m_ElementPoisonMax);
            writer.Write((int)m_ElementEnergyMax);

            writer.Write((int)m_SkillMax);

            #endregion


            if (m_Enhanced.Count < 1)
                writer.Write((int)0);
            else
            {
                writer.Write((int)m_Enhanced.Count);
                foreach (KeyValuePair<int, int> kvp in m_Enhanced)
                {
                    writer.Write((int)kvp.Key);
                    writer.Write((int)kvp.Value);
                }
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_MaxEnhancements = reader.ReadInt();

            #region Deserialize Max Values

            m_AosRegenHitsMax = reader.ReadInt();
            m_AosRegenStamMax = reader.ReadInt();
            m_AosRegenManaMax = reader.ReadInt();
            m_AosDefendChanceMax = reader.ReadInt();
            m_AosAttackChanceMax = reader.ReadInt();
            m_AosBonusStrMax = reader.ReadInt();
            m_AosBonusDexMax = reader.ReadInt();
            m_AosBonusIntMax = reader.ReadInt();
            m_AosBonusHitsMax = reader.ReadInt();
            m_AosBonusStamMax = reader.ReadInt();
            m_AosBonusManaMax = reader.ReadInt();
            m_AosWeaponDamageMax = reader.ReadInt();
            m_AosWeaponSpeedMax = reader.ReadInt();
            m_AosSpellDamageMax = reader.ReadInt();
            m_AosCastRecoveryMax = reader.ReadInt();
            m_AosCastSpeedMax = reader.ReadInt();
            m_AosLowerManaCostMax = reader.ReadInt();
            m_AosLowerRegCostMax = reader.ReadInt();
            m_AosReflectPhysicalMax = reader.ReadInt();
            m_AosEnhancePotionsMax = reader.ReadInt();
            m_AosLuckMax = reader.ReadInt();
            m_AosSpellChannelingMax = reader.ReadInt();
            m_AosNightSightMax = reader.ReadInt();

            m_WeaponLowerStatReqMax = reader.ReadInt();
            m_WeaponSelfRepairMax = reader.ReadInt();
            m_WeaponHitLeechHitsMax = reader.ReadInt();
            m_WeaponHitLeechStamMax = reader.ReadInt();
            m_WeaponHitLeechManaMax = reader.ReadInt();
            m_WeaponHitLowerAttackMax = reader.ReadInt();
            m_WeaponHitLowerDefendMax = reader.ReadInt();
            m_WeaponHitMagicArrowMax = reader.ReadInt();
            m_WeaponHitHarmMax = reader.ReadInt();
            m_WeaponHitFireballMax = reader.ReadInt();
            m_WeaponHitLightningMax = reader.ReadInt();
            m_WeaponHitDispelMax = reader.ReadInt();
            m_WeaponHitColdAreaMax = reader.ReadInt();
            m_WeaponHitFireAreaMax = reader.ReadInt();
            m_WeaponHitPoisonAreaMax = reader.ReadInt();
            m_WeaponHitEnergyAreaMax = reader.ReadInt();
            m_WeaponHitPhysicalAreaMax = reader.ReadInt();
            m_WeaponResistPhysicalBonusMax = reader.ReadInt();
            m_WeaponResistFireBonusMax = reader.ReadInt();
            m_WeaponResistColdBonusMax = reader.ReadInt();
            m_WeaponResistPoisonBonusMax = reader.ReadInt();
            m_WeaponResistEnergyBonusMax = reader.ReadInt();
            m_WeaponUseBestSkillMax = reader.ReadInt();
            m_WeaponMageWeaponMax = reader.ReadInt();
            m_WeaponDurabilityBonusMax = reader.ReadInt();

            m_ArmorLowerStatReqMax = reader.ReadInt();
            m_ArmorSelfRepairMax = reader.ReadInt();
            m_ArmorMageArmorMax = reader.ReadInt();
            m_ArmorDurabilityBonusMax = reader.ReadInt();

            m_ElementPhysicalMax = reader.ReadInt();
            m_ElementFireMax = reader.ReadInt();
            m_ElementColdMax = reader.ReadInt();
            m_ElementPoisonMax = reader.ReadInt();
            m_ElementEnergyMax = reader.ReadInt();

            m_SkillMax = reader.ReadInt();

            #endregion

            m_Enhanced = new Dictionary<int, int>();

            int count = reader.ReadInt();
            if (count < 1)
                m_Enhanced.Add(0, 0);
            else
                for (int x = 0; x < count; x++)
                    m_Enhanced.Add(reader.ReadInt(), reader.ReadInt());
        }
    }
}