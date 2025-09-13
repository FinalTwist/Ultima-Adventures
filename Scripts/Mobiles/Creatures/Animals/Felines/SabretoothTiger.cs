using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a feline corpse" )]
	public class SabretoothTiger : BaseCreature
	{
		public override bool CanChew { get{return true;}}
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public SabretoothTiger() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if(Utility.RandomDouble() > 0.85)
		{
			Name = "a greater sabretooth tiger";
			Body = 340;
			BaseSoundID = 0x462;
			Hue = 0x54F;

			SetStr( 500 );
			SetDex( 400 );
			SetInt( 120 );

			SetMana( 0 );

			SetDamage( 25, 45 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 45 );
			SetResistance( ResistanceType.Cold, 60, 90 );
			SetResistance( ResistanceType.Poison, 15, 35 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 3700;
			Karma = 0;

			VirtualArmor = 50;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 95.1;
		}
		else
		{
			Name = "a sabretooth tiger";
			Body = 340;
			BaseSoundID = 0x462;
			Hue = 0x54F;

			SetStr( 400 );
			SetDex( 300 );
			SetInt( 120 );

			SetMana( 0 );

			SetDamage( 25, 35 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 3000;
			Karma = 0;

			VirtualArmor = 50;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 90.1;
		}
	}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public SabretoothTiger( Serial serial ) : base( serial )
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
		}
	}
}