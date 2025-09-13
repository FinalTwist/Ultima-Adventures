// Created with UO Armor Generator
// Created On: 2/8/2010 5:09:59 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class HandsofTabulature : LeatherGloves, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 3; } }
        public override int BaseColdResistance{ get{ return 4; } }
        public override int BaseFireResistance{ get{ return 14; } }
        public override int BaseEnergyResistance{ get{ return 3; } }
        public override int BasePoisonResistance{ get{ return 6; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public HandsofTabulature()
        {
            Name = "Hands of Tabulature";
            Hue = 1165;
            Attributes.DefendChance = 4;
            ArmorAttributes.MageArmor = 1;
            SkillBonuses.SetValues( 0, SkillName.Musicianship, 5.0 );
            SkillBonuses.SetValues( 1, SkillName.Provocation, 5.0 );
            SkillBonuses.SetValues( 2, SkillName.Peacemaking, 5.0 );
            SkillBonuses.SetValues( 3, SkillName.Discordance, 5.0 );
        }

        public HandsofTabulature(Serial serial) : base( serial )
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
