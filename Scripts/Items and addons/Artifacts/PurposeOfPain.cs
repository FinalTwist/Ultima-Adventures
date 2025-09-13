// Created with UO Weapon Generator
// Created On: 2/9/2010 1:35:37 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class PurposeOfPain : Longsword, ITokunoDyable
    {
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.BleedAttack; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }
        public override int ArtifactRarity{ get{ return 18; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public PurposeOfPain()
        {
            Name = "Purpose Of Pain";
            Hue = 2041;
            //Slayer = SlayerName.None;
            Attributes.BonusStr = 5;
            WeaponAttributes.UseBestSkill = 1;
            Attributes.AttackChance = 15;
            Attributes.WeaponDamage = 50;
            WeaponAttributes.HitPhysicalArea = 100;
            WeaponAttributes.SelfRepair = 4;
            WeaponAttributes.HitHarm = 10;
        }

        public PurposeOfPain(Serial serial) : base( serial )
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
