// Created with UO Armor Generator
// Created On: 2/8/2010 4:42:17 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class Revenge : BoneHelm, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 17; } }
        public override int BaseColdResistance{ get{ return 4; } }
        public override int BaseFireResistance{ get{ return 4; } }
        public override int BaseEnergyResistance{ get{ return 7; } }
        public override int BasePoisonResistance{ get{ return 3; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public Revenge()
        {
            Name = "Revenge";
            Hue = 39;
            Attributes.NightSight = 1;
            Attributes.BonusDex = 10;
            Attributes.RegenHits = 3;
            Attributes.RegenStam = 3;
            Attributes.AttackChance = 10;
            Attributes.DefendChance = 10;
            ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 4;
            Attributes.CastSpeed = 1;
            Attributes.LowerManaCost = 2;
        }

        public Revenge(Serial serial) : base( serial )
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
