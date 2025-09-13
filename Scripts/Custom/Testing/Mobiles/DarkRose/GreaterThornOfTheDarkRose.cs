using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class GreaterDarkRoseThorn : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 25; } }
		public override int AosSpeed{ get{ return 53; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 15; } }
		public override int OldMaxDamage{ get{ return 29; } }
		public override int OldSpeed{ get{ return 45; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 200; } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public GreaterDarkRoseThorn() : base( 0x26CE )
		{

            Name = "Greater Thorn of the Dark Rose";
            Hue = 2949;

            Attributes.SpellChanneling = 1;
            Attributes.NightSight = 1;
            Attributes.BonusStr = 6;
            Attributes.BonusDex = 12;
            Attributes.RegenStam = 5;

            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLeechStam = 15;
            WeaponAttributes.HitLeechMana = 18;

            Attributes.AttackChance = 10;
            Attributes.DefendChance = 15;
            Attributes.WeaponDamage = 40;
            Attributes.WeaponSpeed = 35;
            Attributes.ReflectPhysical = 10;
                        
		}

        public GreaterDarkRoseThorn(Serial serial)
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