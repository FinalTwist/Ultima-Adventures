// Created with UO Armor Generator
// Created On: 2/9/2010 12:30:43 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class ThickNeck : StuddedGorget, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 22; } }
        public override int BaseColdResistance{ get{ return 4; } }
        public override int BaseFireResistance{ get{ return 18; } }
        public override int BaseEnergyResistance{ get{ return 17; } }
        public override int BasePoisonResistance{ get{ return 16; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public ThickNeck()
        {
            Name = "Thick Neck";
            Hue = 2224;
            StrRequirement = 110;
            Attributes.BonusStr = 5;
            Attributes.DefendChance = 15;
            ArmorAttributes.SelfRepair = 4;
            
        }

        public ThickNeck(Serial serial) : base( serial )
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
