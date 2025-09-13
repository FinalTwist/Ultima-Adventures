// Created with UO Weapon Generator
// Created On: 2/9/2010 1:32:01 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class ChildOfDeath : Scythe, ITokunoDyable
    {
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
        public override int ArtifactRarity{ get{ return 18; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public ChildOfDeath()
        {
            Name = "Child Of Death";
            Hue = 1157;
            Slayer = SlayerName.None;
            Attributes.RegenHits = 3;
            WeaponAttributes.HitLeechMana = 5;
            Attributes.WeaponDamage = 40;
            Attributes.WeaponSpeed = 10;
            Attributes.Luck = 120;
            WeaponAttributes.HitPhysicalArea = 100;
            WeaponAttributes.SelfRepair = 4;
            Attributes.CastSpeed = 1;
            Attributes.CastRecovery = 1;
            WeaponAttributes.HitLowerDefend = 10;
            WeaponAttributes.HitFireball = 10;
        }

        public ChildOfDeath(Serial serial) : base( serial )
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
