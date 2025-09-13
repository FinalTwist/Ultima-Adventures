// Created with UO Weapon Generator
// Created On: 2/9/2010 1:24:32 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class Kamadon : Kama, ITokunoDyable
    {
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }
        public override int ArtifactRarity{ get{ return 18; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public Kamadon()
        {
            Name = "Kamadon";
            Hue = 443;
            Slayer = SlayerName.ElementalBan;
            Attributes.SpellChanneling = 1;
            WeaponAttributes.HitLeechHits = 5;
            Attributes.AttackChance = 15;
            Attributes.WeaponSpeed = 25;
            Attributes.Luck = 300;
            Attributes.SpellDamage = 5;
            WeaponAttributes.HitPhysicalArea = 100;
            Attributes.CastRecovery = 3;
            WeaponAttributes.HitLowerAttack = 10;
            WeaponAttributes.HitLightning = 10;
        }

        public Kamadon(Serial serial) : base( serial )
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
