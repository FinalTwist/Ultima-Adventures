// Created with UO Weapon Generator
// Created On: 2/9/2010 1:12:38 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class StandStill : Spear, ITokunoDyable
    {
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
        public override int ArtifactRarity{ get{ return 18; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public StandStill()
        {
            Name = "Stand Still";
            Hue = 863;
            Attributes.SpellChanneling = 1;
            WeaponAttributes.HitLeechMana = 5;
            Attributes.AttackChance = 15;
            Attributes.WeaponSpeed = 15;
            WeaponAttributes.HitPhysicalArea = 100;
        }

        public StandStill(Serial serial) : base( serial )
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
