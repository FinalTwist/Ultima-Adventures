using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Magical
{
	public class SummonSnakesSpell : MagicalSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"", "", 
				266,
				9040,
				false
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }
		public override double RequiredSkill{ get{ return 0.0; } }
		public override int RequiredMana{ get{ return 1; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
		
		public SummonSnakesSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			Map map = Caster.Map;
			SpellHelper.GetSurfaceTop( ref p );

			if ( map == null || !map.CanSpawnMobile( p.X, p.Y, p.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				TimeSpan duration;
				duration = TimeSpan.FromSeconds( 120 );
				BaseCreature.Summon( new SummonSnakes(), false, Caster, new Point3D( p ), 0x212, duration );
			}
			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private SummonSnakesSpell m_Owner;

			public InternalTarget( SummonSnakesSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetOutOfLOS( Mobile from, object o )
			{
				from.SendLocalizedMessage( 501943 ); // Target cannot be seen. Try again.
				from.Target = new InternalTarget( m_Owner );
				from.Target.BeginTimeout( from, TimeoutTime - DateTime.UtcNow );
				m_Owner = null;
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_Owner != null )
					m_Owner.FinishSequence();
			}
		}
	}
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Server.Mobiles
{
	[CorpseName( "a serpent corpse" )]
	public class SummonSnakes : BaseCreature
	{
		public override bool DeleteCorpseOnDeath { get { return Summoned; } }
		public override double DispelDifficulty { get { return 900.0; } }
		public override double DispelFocus { get { return 900.0; } }

		public override double GetFightModeRanking( Mobile m, FightMode acqType, bool bPlayerOnly )
		{
			return 200 / Math.Max( GetDistanceToSqrt( m ), 1.0 );
		}

		[Constructable]
		public SummonSnakes() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a mystical serpent";

			Body = 0x15;
			Hue = 0x48F;
			BaseSoundID = 0xDB;

			SetStr( 150 );
			SetDex( 150 );
			SetInt( 100 );

			SetHits( 150 );
			SetStam( 250 );
			SetMana( 0 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 80 );

			SetResistance( ResistanceType.Physical, 40 );
			SetResistance( ResistanceType.Fire, 20 );
			SetResistance( ResistanceType.Cold, 20 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 10 );

			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 40;
			ControlSlots = 2;
		}

		public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override void OnThink()
		{
			if ( Core.SE && Summoned )
			{
				ArrayList spirtsOrVortexes = new ArrayList();

				foreach ( Mobile m in GetMobilesInRange( 5 ) )
				{
					if ( m is SummonSnakes || m is BladeSpirits || m is GasCloud || m is DeathVortex || m is SummonedTreefellow || m is EnergyVortex || m is SummonDragon )
					{
						if ( ( (BaseCreature) m ).Summoned )
							spirtsOrVortexes.Add( m );
					}
				}

				while ( spirtsOrVortexes.Count > 6 )
				{
					int index = Utility.Random( spirtsOrVortexes.Count );
					Dispel( ( (Mobile) spirtsOrVortexes[index] ) );
					spirtsOrVortexes.RemoveAt( index );
				}
			}

			base.OnThink();
		}


		public SummonSnakes( Serial serial ) : base( serial )
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
		}
	}
}