using System;
using Server;
using Server.Items;
using Server.Engines.Plants;
using Server.Regions;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a fallen tree" )]
	public class DeadReaper : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public DeadReaper() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dead wood tree";
			Body = Utility.RandomList( 285, 301 );
			BaseSoundID = 442;
			Hue = 0x497;

			SetStr( 66, 215 );
			SetDex( 66, 75 );
			SetInt( 101, 250 );

			SetHits( 40, 129 );
			SetStam( 0 );

			SetDamage( 9, 11 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.1, 125.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;

			PackItem( new EbonyLog( Utility.RandomMinMax( 10, 20 ) ) );

			switch ( Utility.Random( 7 ) )
			{
				case 0: PackItem( new BatWing( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 1: PackItem( new NoxCrystal( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 2: PackItem( new GraveDust( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 3: PackItem( new PigIron( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 4: PackItem( new DaemonBlood( Utility.RandomMinMax( 1, 10 ) ) ); break;
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool BleedImmune{ get{ return true; } }

		public override bool IsEnemy( Mobile m )
		{
			Region reg = Region.Find( m.Location, m.Map );

			if ( reg.IsPartOf( typeof( NecromancerRegion ) ) && GetPlayerInfo.EvilPlayer( m ) )
				return false;

			if ( m is BaseCreature )
				return false;

			return true;
		}

		public DeadReaper( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}