using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class SabretoothBearRiding : BaseMount
	{
		public override bool CanChew { get{return true;}}
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public SabretoothBearRiding() : this( "a sabreclaw bear" )
		{
		}

		[Constructable]
		public SabretoothBearRiding( string name ) : base( name, 34, MountBody(34), AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)		
		
				{
			Hue = 0x54B;
			BaseSoundID = 0xA3;
Name = "a greater sabreclaw bear";
			SetStr( 226, 355 );
			SetDex( 121, 245 );
			SetInt( 16, 40 );

			SetHits( 176, 293 );
			SetMana( 0 );

			SetDamage( 14, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 65 );
			SetResistance( ResistanceType.Cold, 35, 55 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );

			Fame = 1900;
			Karma = 0;

			VirtualArmor = 35;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 79.1;
		}
		else
		{
			Hue = 0x54B;
			BaseSoundID = 0xA3;

			SetStr( 226, 255 );
			SetDex( 121, 145 );
			SetInt( 16, 40 );

			SetHits( 176, 193 );
			SetMana( 0 );

			SetDamage( 14, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );

			Fame = 1500;
			Karma = 0;

			VirtualArmor = 35;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 69.1;
		}
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }

		public override int GetAngerSound()
		{
			return 0x518;
		}

		public override int GetIdleSound()
		{
			return 0x517;
		}

		public override int GetAttackSound()
		{
			return 0x516;
		}

		public override int GetHurtSound()
		{
			return 0x519;
		}

		public override int GetDeathSound()
		{
			return 0x515;
		}

		public SabretoothBearRiding( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			ItemID = MountBody(34);
		}
	}
}
