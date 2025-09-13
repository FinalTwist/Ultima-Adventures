// Created with UO Weapon Generator
// Created On: 2/9/2010 1:09:53 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class Valicious : LargeBattleAxe, ITokunoDyable
    {
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
        public override int ArtifactRarity{ get{ return 18; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public Valicious()
        {
            Name = "Valicious";
            Hue = 2075;
            Attributes.RegenStam = 3;
            WeaponAttributes.HitLeechStam = 15;
            Attributes.AttackChance = 5;
            Attributes.WeaponDamage = 25;
            Attributes.WeaponSpeed = 5;
            WeaponAttributes.HitPhysicalArea = 100;
            WeaponAttributes.SelfRepair = 4;
            WeaponAttributes.HitLowerDefend = 5;
            WeaponAttributes.HitDispel = 25;
        }

        public Valicious(Serial serial) : base( serial )
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
