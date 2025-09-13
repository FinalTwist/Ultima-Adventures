using System;
using Server;
using Server.Items;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a dissipated vortex" )]
	public class EvilEnergyVortex : BaseCreature
	{
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool DeleteCorpseOnDeath { get { return true; } }

		[Constructable]
		public EvilEnergyVortex() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an energy vortex";
			Body = 13;
			Hue = 0x490;
			BaseSoundID = 655;

			Timer.DelayCall( TimeSpan.FromSeconds( (double)(Utility.RandomMinMax( 30, 240 )) ), new TimerCallback( Delete ) );

			SetStr( 200 );
			SetDex( 200 );
			SetInt( 100 );

			SetHits( ( Core.SE ) ? 140 : 70 );
			SetStam( 250 );
			SetMana( 0 );

			SetDamage( 14, 17 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Energy, 100 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 90, 100 );

			SetSkill( SkillName.MagicResist, 99.9 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 40;

			AddItem( new LightSource() );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override int GetAngerSound()
		{
			return 0x15;
		}

		public override int GetAttackSound()
		{
			return 0x28;
		}

		public EvilEnergyVortex( Serial serial ) : base( serial )
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