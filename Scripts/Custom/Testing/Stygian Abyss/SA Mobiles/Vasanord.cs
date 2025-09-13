using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a plant corpse" )]
	public class Vasanord : BaseCreature
	{
		[Constructable]
		public Vasanord() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.6, 1.2 )
		{
			Name = "Vasanord";
			Body = 780;

			SetStr( 805, 869 );
			SetDex( 51, 64 );
			SetInt( 38, 48 );

			SetHits( 553, 626 );
			SetMana( 0 );
			SetStam( 51, 64 );

			SetDamage( 10, 23 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 30, 50 );
			SetResistance( ResistanceType.Fire, 20, 40 );
			SetResistance( ResistanceType.Cold, 20, 50 );
			SetResistance( ResistanceType.Poison, 100, 100 );
			SetResistance( ResistanceType.Energy, 20, 50 );

			SetSkill( SkillName.MagicResist, 72.8, 77.7 );
			SetSkill( SkillName.Tactics, 50.7, 99.6 );
			SetSkill( SkillName.Anatomy, 6.5, 17.1 );
			SetSkill( SkillName.EvalInt, 92.5, 106.2 );
			SetSkill( SkillName.Magery, 95.5, 106.9 );
			SetSkill( SkillName.Wrestling, 53.6, 98.6 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 28;

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new Board( 10 ) );
			else
				PackItem( new Log( 10 ) );
				PackItem( new DaemonBone( 30 ) );

			PackReg( 3 );
			PackItem( new Engines.Plants.Seed() );
			PackItem( new Engines.Plants.Seed() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
		}

		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public Vasanord( Serial serial ) : base( serial )
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


		/*public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( this.Hits > (this.HitsMax / 4) )
			{
				if ( 0.25 >= Utility.RandomDouble() )
					//SpawnBogling( attacker );
			}
			else if ( 0.25 >= Utility.RandomDouble() )
			{
				//EatBoglings();
			}
		}*/
	}
}