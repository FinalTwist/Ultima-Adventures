using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class LesserDarkRoseThorn : BaseSword
	{
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ConcussionBlow; } }

        public override int AosStrengthReq { get { return 35; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 16; } }
        public override int AosSpeed { get { return 30; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 10; } }
		public override int OldMaxDamage{ get{ return 20; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

		[Constructable]
        public LesserDarkRoseThorn()
            : base(0xF61)
		{

            Name = "Lesser Thorn of the Dark Rose";
            Hue = 2024;

            Attributes.SpellChanneling = 1;
            Attributes.NightSight = 1;
            Attributes.BonusStr = 3;
            Attributes.BonusDex = 5;
            Attributes.RegenStam = 3;

            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLeechStam = 10;
            WeaponAttributes.HitLeechMana = 12;

            Attributes.AttackChance = 5;
            Attributes.DefendChance = 10;
            Attributes.WeaponDamage = 25;
            Attributes.WeaponSpeed = 30;
            Attributes.ReflectPhysical = 10;
                        
		}

        public LesserDarkRoseThorn(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}