// Created with UO Armor Generator
// Created On: 2/9/2010 12:39:20 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class GlovesOfTheHardWorker : PlateGloves, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 6; } }
        public override int BaseColdResistance{ get{ return 8; } }
        public override int BaseFireResistance{ get{ return 4; } }
        public override int BaseEnergyResistance{ get{ return 7; } }
        public override int BasePoisonResistance{ get{ return 3; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public GlovesOfTheHardWorker()
        {
            Name = "Gloves Of The Hard Worker";
            Hue = 2431;
            StrRequirement = 85;
            Attributes.BonusStr = 5;
            Attributes.BonusDex = 5;
            ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 5;
            SkillBonuses.SetValues( 0, SkillName.Blacksmith, 10.0 );
            SkillBonuses.SetValues( 1, SkillName.Mining, 10.0 );
            SkillBonuses.SetValues( 2, SkillName.Tinkering, 5.0 );
            
        }

        public GlovesOfTheHardWorker(Serial serial) : base( serial )
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
