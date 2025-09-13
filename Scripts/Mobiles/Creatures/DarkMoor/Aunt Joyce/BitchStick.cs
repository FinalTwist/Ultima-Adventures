// Created with UO Weapon Generator
// Created On: 12/30/2015 11:48:13 PM
// By: dave

using System;
using Server;

namespace Server.Items
{
    public class BitchStick : QuarterStaff
    {
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.CrushingBlow; } }

        [Constructable]
        public BitchStick()
        {
            Name = "The Bitch Stick";
            Attributes.SpellChanneling = 1;
            Attributes.NightSight = 1;
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLeechMana = 34;
            Attributes.AttackChance = 15;
            Attributes.DefendChance = 15;
            Attributes.WeaponDamage = 75;
            Attributes.WeaponSpeed = 75;
            WeaponAttributes.SelfRepair = 2;
            Attributes.CastSpeed = 4;
            Attributes.CastRecovery = 4;
            Attributes.LowerManaCost = 45;
            Attributes.LowerRegCost = 45;
            WeaponAttributes.HitHarm = 45;
            WeaponAttributes.HitFireball = 45;
            WeaponAttributes.HitLightning = 45;
        }

        public BitchStick(Serial serial) : base( serial )
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
