using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an air elemental corpse" )]
	public class SummonedAirElementalGreater : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override int BreathPhysicalDamage{ get{ return 100; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x654; } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 51 ); }

		[Constructable]
		public SummonedAirElementalGreater () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an air elemental";
			Body = 273;
			Hue = 0x4001;
			BaseSoundID = 655;

			SetStr( 300 );
			SetDex( 300 );
			SetInt( 200 );

			SetHits( 250 );
			SetStam( 150 );

			SetDamage( 12, 18 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 45, 55 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 80, 90 );

			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.EvalInt, 80.0 );
			SetSkill( SkillName.Magery, 80.0 );
			SetSkill( SkillName.MagicResist, 70.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0 );

			VirtualArmor = 40;
			ControlSlots = 2;
		}

		public SummonedAirElementalGreater( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 655;
		}
	}
}