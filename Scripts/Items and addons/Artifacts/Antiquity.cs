// Created with UO Armor Generator
// Created On: 2/8/2010 4:45:54 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class Antiquity : PlateGloves, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 8; } }
        public override int BaseColdResistance{ get{ return 18; } }
        public override int BaseFireResistance{ get{ return 13; } }
        public override int BaseEnergyResistance{ get{ return 1; } }
        public override int BasePoisonResistance{ get{ return 4; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public Antiquity()
        {
            Name = "Antiquity";
            Hue = 2431;
            Attributes.NightSight = 1;
            Attributes.BonusStr = 5;
            Attributes.BonusInt = 5;
            Attributes.AttackChance = 15;
            Attributes.DefendChance = 5;
            Attributes.SpellDamage = 5;
            ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 4;
            Attributes.CastRecovery = 3;
        }

        public Antiquity(Serial serial) : base( serial )
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
