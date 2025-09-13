using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a plant corpse" )]
	public class Anlorvaglem : BaseCreature
	{
		[Constructable]
		public Anlorvaglem() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.6, 1.2 )
		{
			Name = "Anlorvaglem";
			Body = 780;

			SetStr( 1104, 1104 );
			SetDex( 1076, 1076 );
			SetInt( 1107, 1107 );

			SetHits( 3205, 3205 );
			SetMana( 1107, 1107 );
			SetStam( 1076, 1076 );

			SetDamage( 11, 13 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 30, 36 );
			SetResistance( ResistanceType.Fire, 40, 43 );
			SetResistance( ResistanceType.Cold, 50, 58 );
			SetResistance( ResistanceType.Poison, 100, 100 );
			SetResistance( ResistanceType.Energy, 35, 40 );

			SetSkill( SkillName.MagicResist, 62.8, 66.8 );
			SetSkill( SkillName.Tactics, 90.7, 94.1 );
			SetSkill( SkillName.Anatomy, 20.5, 27.2 );
			SetSkill( SkillName.Wrestling, 53.6, 58.8 );

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

		public Anlorvaglem( Serial serial ) : base( serial )
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