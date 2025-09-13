/* Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
    public class CertainDoom : CrescentBlade
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.DoubleStrike; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }

        public override int ArtifactRarity{ get{ return 46; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        public override int DefHitSound { get { return 0x23B; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int AosMinDamage { get { return 18; } }
        public override int AosMaxDamage { get { return 25; } }
	public override float MlSpeed{ get{ return 3.00f; } }

        [Constructable]
        public CertainDoom()
        {
            Name = "Certain Doom";
            Hue = 546;

            LootType = LootType.Blessed;

            Slayer = SlayerName.Exorcism;

            Attributes.SpellChanneling = 1;
            Attributes.NightSight = 1;
            Attributes.RegenHits = 2;
            Attributes.RegenStam = 2;
            Attributes.AttackChance = 15;
            Attributes.DefendChance = 10;
            Attributes.WeaponDamage = 60;
            Attributes.WeaponSpeed = 40;
            Attributes.ReflectPhysical = 5;
            WeaponAttributes.ResistPhysicalBonus = 15;
            WeaponAttributes.ResistColdBonus = 8;
            WeaponAttributes.ResistFireBonus = 12;
            WeaponAttributes.ResistEnergyBonus = 6;
            WeaponAttributes.ResistPoisonBonus = 9;
            WeaponAttributes.SelfRepair = 35;
            Attributes.CastSpeed = 2;
            Attributes.CastRecovery = 3;
            WeaponAttributes.HitLowerAttack = 50;
            WeaponAttributes.HitLowerDefend = 42;
            WeaponAttributes.HitFireball = 55;
            WeaponAttributes.HitLightning = 46;
        }

        public CertainDoom(Serial serial) : base( serial )
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
    } 
} 
