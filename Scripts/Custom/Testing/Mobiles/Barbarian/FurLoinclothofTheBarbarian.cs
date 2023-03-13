//Made By Brad Poe
using System;
using Server.Items;

namespace Server.Items
{
    public class FurLoinclothofTheBarbarian : BaseArmor
    {
        public override int ArtifactRarity { get { return 21; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public FurLoinclothofTheBarbarian() : base(0x230B)
        {
            Weight = 6.0;
            Name = "Fur Loincloth of The Barbarian";
            Hue = 2101;

            ArmorAttributes.SelfRepair = 15;
            Attributes.WeaponDamage =25;
            Attributes.BonusHits = 20;
            ArmorAttributes.MageArmor = 1;

            PhysicalBonus = 35;
            FireBonus = 15;
            ColdBonus = 15;
            PoisonBonus = 15;
            EnergyBonus = 15;
        }

        public FurLoinclothofTheBarbarian(Serial serial) : base(serial)
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


