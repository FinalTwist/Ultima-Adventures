using System;
using System.Collections;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a blade spirit corpse" )]
	public class EvilBladeSpirits : BaseCreature
	{
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool DeleteCorpseOnDeath { get { return true; } }

		[Constructable]
		public EvilBladeSpirits() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a blade spirit";
			Body = 574;
			Timer.DelayCall( TimeSpan.FromSeconds( (double)(Utility.RandomMinMax( 30, 240 )) ), new TimerCallback( Delete ) );

			SetStr( 150 );
			SetDex( 150 );
			SetInt( 100 );

			SetHits( ( Core.SE ) ? 160 : 80 );
			SetStam( 250 );
			SetMana( 0 );

			SetDamage( 10, 14 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 70.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.Wrestling, 90.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 40;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override int GetAngerSound()
		{
			return 0x23A;
		}

		public override int GetAttackSound()
		{
			return 0x3B8;
		}

		public override int GetHurtSound()
		{
			return 0x23A;
		}

		public EvilBladeSpirits( Serial serial ) : base( serial )
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
			Timer.DelayCall( TimeSpan.FromSeconds( 30.0 ), new TimerCallback( Delete ) );
		}
	}
}