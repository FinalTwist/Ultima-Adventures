// Created with UO Weapon Generator
// Created On: 2/9/2010 1:16:07 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class BloodTrail : WarHammer
    {
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }
        public override int ArtifactRarity{ get{ return 18; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public BloodTrail()
        {
            Name = "Bloody Trail";
            Hue = 39;
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLeechHits = 5;
            Attributes.WeaponDamage = 25;
            Attributes.WeaponSpeed = 50;
            Attributes.Luck = 120;
            WeaponAttributes.HitPhysicalArea = 100;
            Attributes.LowerManaCost = 7;
            WeaponAttributes.HitLowerDefend = 10;
        }

        public BloodTrail(Serial serial) : base( serial )
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
