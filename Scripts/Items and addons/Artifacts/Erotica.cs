// Created with UO Armor Generator
// Created On: 2/8/2010 4:55:45 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class Erotica : FemaleStuddedChest, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 3; } }
        public override int BaseColdResistance{ get{ return 12; } }
        public override int BaseFireResistance{ get{ return 12; } }
        public override int BaseEnergyResistance{ get{ return 13; } }
        public override int BasePoisonResistance{ get{ return 18; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public Erotica()
        {
            Name = "Erotica";
            Hue = 25;
            Attributes.BonusStr = 5;
            Attributes.BonusInt = 5;
            Attributes.BonusDex = 5;
            Attributes.AttackChance = 10;
            Attributes.DefendChance = 10;
            Attributes.Luck = 205;
            Attributes.SpellDamage = 5;
            ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 4;
            Attributes.LowerManaCost = 8;
            Attributes.LowerRegCost = 20;
        }

        public Erotica(Serial serial) : base( serial )
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
