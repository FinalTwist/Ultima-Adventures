using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a dinosaur corpse" )]
	public class Iguanodon : BaseMount
	{
		[Constructable]
		public Iguanodon() : this( "an iguanodon" )
		{
		}

		[Constructable]
		public Iguanodon( string name ) : base( name, 0x31A, 0x3EBD, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			BaseSoundID = 206;
			Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );
Name = "a greater iguanodon";
			SetStr( 301, 500 );
			SetDex( 66, 185 );
			SetInt( 25, 45 );

			SetHits( 221, 380 );

			SetDamage( 9, 25 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );

			SetResistance( ResistanceType.Physical, 35, 50 );
			SetResistance( ResistanceType.Fire, 20, 40 );
			SetResistance( ResistanceType.Cold, 20, 50 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Anatomy, 45.1, 55.0 );
			SetSkill( SkillName.MagicResist, 45.1, 55.0 );
			SetSkill( SkillName.Tactics, 45.1, 55.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );

			Fame = 5000;
			Karma = -4000;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 99.9;
		}
		else
		{
			BaseSoundID = 206;
			Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );

			SetStr( 301, 400 );
			SetDex( 66, 85 );
			SetInt( 25, 45 );

			SetHits( 221, 280 );

			SetDamage( 9, 16 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Anatomy, 45.1, 55.0 );
			SetSkill( SkillName.MagicResist, 45.1, 55.0 );
			SetSkill( SkillName.Tactics, 45.1, 55.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );

			Fame = 4000;
			Karma = -4000;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 93.9;
		}
	}

		public override int GetIdleSound()
		{
			return 0x2CE;
		}

		public override int GetDeathSound()
		{
			return 0x2CC;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override int GetAttackSound()
		{
			return 0x2C8;
		}

		public override double GetControlChance( Mobile m, bool useBaseSkill )
		{
			return 1.0;
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Scales{ get{ return 5; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Dinosaur; } }
		public override bool CanAngerOnTame { get { return true; } }

		public Iguanodon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( BaseSoundID == -1 )
				BaseSoundID = 0x16A;
		}
	}
}