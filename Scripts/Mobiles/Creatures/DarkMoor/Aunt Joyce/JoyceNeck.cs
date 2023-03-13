// Scripted by James4245
using System;
using Server;

namespace Server.Items
{
    public class JoyceNeck : LeatherGorget
    {
        public override int ArtifactRarity { get { return 551; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public JoyceNeck()
        {
            Weight = 10.0;
            Name = "a Crazy Cat Lady's Neck Massager";
            Hue = 2469;

            //Attributes.AttackChance = nn;
            //Attributes.BonusDex = nn;
            //Attributes.BonusHits = nn;
            //Attributes.BonusInt = nn;
            //Attributes.BonusMana = nn;
            //Attributes.BonusStam = nn;
            //Attributes.BonusStr = nn;
            Attributes.CastRecovery = 10;
            Attributes.CastSpeed = 20;
            Attributes.DefendChance = 17;
            //Attributes.EnhancePotions = nn;
            Attributes.LowerManaCost = 10;
            Attributes.LowerRegCost = 15;
            //Attributes.Luck = nn;
            //Attributes.Nightsight = 1;
            Attributes.ReflectPhysical = 27;
            //Attributes.RegenHits = nn;
            //Attributes.RegenMana = nn;
            //Attributes.RegenStam = nn;
            Attributes.SpellChanneling = 1;
            //Attributes.SpellDamage = nn;
            //Attributes.WeaponDamage = nn;
            //Attributes.WeaponSpeed = nn;

            //ArmorAttributes.DurabilityBonus = nn;
            //ArmorAttributes.LowerStatReq = nn;
            ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 155;

            ColdBonus = 8;
            //DexBonus = nn;
            //DexRequirement = nn;
            EnergyBonus = 14;
            FireBonus = 17;
            //IntBonus = nn;
            //IntRequirement = 40;
            PhysicalBonus = 28;
            PoisonBonus = 9;
            //StrBonus = nn;
            StrRequirement = 60;

            LootType = LootType.Cursed;

        }

        public JoyceNeck(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}