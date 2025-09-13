// Created with UO Armor Generator
// Created On: 2/9/2010 12:34:10 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class SatanicHelm : DragonHelm, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 18; } }
        public override int BaseColdResistance{ get{ return 2; } }
        public override int BaseFireResistance{ get{ return 33; } }
        public override int BaseEnergyResistance{ get{ return 14; } }
        public override int BasePoisonResistance{ get{ return 6; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public SatanicHelm()
        {
            Name = "Satanic Helm";
            Hue = 1157;
            StrRequirement = 100;
            Attributes.RegenStam = 3;
            Attributes.DefendChance = 5;
            Attributes.WeaponDamage = 5;
            ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 4;
            
        }

        public SatanicHelm(Serial serial) : base( serial )
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
