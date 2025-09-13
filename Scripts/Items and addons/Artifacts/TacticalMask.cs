// Created with UO Armor Generator
// Created On: 2/9/2010 12:24:05 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class TacticalMask : PlateMempo, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 14; } }
        public override int BaseColdResistance{ get{ return 14; } }
        public override int BaseFireResistance{ get{ return 8; } }
        public override int BaseEnergyResistance{ get{ return 15; } }
        public override int BasePoisonResistance{ get{ return 15; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public TacticalMask()
        {
            Name = "Tactical Mask";
            Hue = 2075;
            StrRequirement = 85;
            Attributes.NightSight = 1;
            Attributes.BonusDex = 5;
            Attributes.DefendChance = 15;
            Attributes.WeaponSpeed = 10;
            ArmorAttributes.SelfRepair = 3;
            ArmorAttributes.LowerStatReq = 5;
            Attributes.CastRecovery = 3;
            SkillBonuses.SetValues( 0, SkillName.Tactics, 5.0 );
            SkillBonuses.SetValues( 1, SkillName.Swords, 5.0 );
            SkillBonuses.SetValues( 2, SkillName.Fencing, 5.0 );
            SkillBonuses.SetValues( 3, SkillName.Macing, 5.0 );
        }

        public TacticalMask(Serial serial) : base( serial )
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
