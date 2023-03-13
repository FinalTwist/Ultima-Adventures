// Created with UO Weapon Generator
// Created On: 2/9/2010 1:28:54 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class BowOfHarps : RepeatingCrossbow, ITokunoDyable
    {
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.MovingShot; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.BleedAttack; } }
        public override int ArtifactRarity{ get{ return 18; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public BowOfHarps()
        {
            Name = "Bow Of Harps";
            Hue = 403;
            Attributes.SpellChanneling = 1;
            Attributes.BonusDex = 5;
            WeaponAttributes.HitLeechMana = 5;
            Attributes.AttackChance = 15;
            Attributes.WeaponDamage = 15;
            Attributes.Luck = 120;
            WeaponAttributes.HitPhysicalArea = 100;
            WeaponAttributes.SelfRepair = 4;
            WeaponAttributes.HitLowerAttack = 10;
            WeaponAttributes.HitDispel = 30;
        }

        public BowOfHarps(Serial serial) : base( serial )
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
