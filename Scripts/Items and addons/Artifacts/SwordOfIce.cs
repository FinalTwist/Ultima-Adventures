using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1401, 0x1400 )]
	public class SwordOfIce : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 10; } }
		public override int AosMaxDamage{ get{ return 12; } }
		//public override int AosSpeed{ get{ return 53; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 3; } }
		public override int OldMaxDamage{ get{ return 28; } }
		//public override int OldSpeed{ get{ return 53; } }

		public override int DefHitSound{ get{ return 0x23C; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public SwordOfIce() : base( 0x1401 )
		{
			Weight = 2.0;
			Name = "Sword Of Ice";
            Hue = 1152;
            
            HitPoints = Utility.RandomMinMax(100, 125);
			MaxHitPoints = 150;
              
            WeaponAttributes.HitColdArea = 75;
            WeaponAttributes.HitLeechMana = 45;
            WeaponAttributes.UseBestSkill = 1;
            Attributes.WeaponDamage = 45;
            Attributes.WeaponSpeed = 30;
              
            Slayer = SlayerName.ElementalBan;
		}

		public SwordOfIce( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}
