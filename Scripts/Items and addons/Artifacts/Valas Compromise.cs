// Created with UO Armor Generator
// Created On: 2/8/2010 4:49:24 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class ValasCompromise : ChainLegs, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 10; } }
        public override int BaseColdResistance{ get{ return 10; } }
        public override int BaseFireResistance{ get{ return 10; } }
        public override int BaseEnergyResistance{ get{ return 10; } }
        public override int BasePoisonResistance{ get{ return 10; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public ValasCompromise()
        {
            Name = "Valas Compromise";
            Hue = 1756;
            Attributes.BonusDex = 3;
            Attributes.AttackChance = 3;
            Attributes.DefendChance = 3;
            Attributes.WeaponDamage = 3;
            Attributes.WeaponSpeed = 3;
            ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 3;
        }

        public ValasCompromise(Serial serial) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 0 );
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    } // End Class
} // End Namespace
