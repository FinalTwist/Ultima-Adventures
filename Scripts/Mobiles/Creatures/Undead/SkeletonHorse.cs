using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class SkeletonHorse : BaseMount
	{
		[Constructable]
		public SkeletonHorse() : this( "a skeleton horse" )
		{
		}

		[Constructable]
		public SkeletonHorse( string name ) : base( name, 793, 0x3EBB, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0xA8;

			SetStr( 396, 425 );
			SetDex( 76, 95 );
			SetInt( 76, 115 );

			SetHits( 198, 215 );

			SetDamage( 10, 16 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 40 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 10.4, 50.0 );
			SetSkill( SkillName.Magery, 10.4, 50.0 );
			SetSkill( SkillName.MagicResist, 85.3, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 80.5, 92.5 );

			Fame = 12000;
			Karma = -12000;

			VirtualArmor = 50;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 85.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Potions );
		}

		public override int GetAngerSound()
		{
			if ( !Controlled )
				return 0x16A;

			return base.GetAngerSound();
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }
		public override bool BleedImmune{ get{ return true; } }

		public SkeletonHorse( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 0x16A )
				BaseSoundID = 0xA8;
		}
	}
}