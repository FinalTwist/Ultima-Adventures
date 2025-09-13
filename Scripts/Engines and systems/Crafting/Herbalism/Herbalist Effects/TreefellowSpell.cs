using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server;
using Server.Items;
using System.Collections;

namespace Server.Spells.Herbalist
{
	public class TreefellowSpell : HerbalistSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override int HerbalistSpellCircle{ get{ return 4; } }
		public override double CastDelay{ get{ return 5.0; } }
      	public override double RequiredSkill{ get{ return 75.0; } }
      	public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
		
		public TreefellowSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( (Caster.Followers + (Core.SE ? 2 : 1)) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			else if ( Caster.Skills[CastSkill].Value < RequiredSkill )
			{
				Caster.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You lack the understanding to use this fertilizer.", Caster.NetState);
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

				if ( Core.AOS )
					duration = TimeSpan.FromSeconds( 90.0 );
				else
					duration = TimeSpan.FromSeconds( Utility.Random( 80, 40 ) );

				BaseCreature.Summon( new SummonedTreefellow(), false, Caster, new Point3D( p ), 0x212, duration );
				Caster.PlaySound( 0x19 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private TreefellowSpell m_Owner;

			public InternalTarget( TreefellowSpell owner ) : base( 12, true, TargetFlags.None )
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

namespace Server.Mobiles
{
	[CorpseName( "a treant corpse" )]
	public class SummonedTreefellow : BaseCreature
	{
		public override bool DeleteCorpseOnDeath { get { return Summoned; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public override double DispelDifficulty { get { return 80.0; } }
		public override double DispelFocus { get { return 20.0; } }

		public override double GetFightModeRanking( Mobile m, FightMode acqType, bool bPlayerOnly )
		{
			return ( m.Int + m.Skills[SkillName.AnimalLore].Value ) / Math.Max( GetDistanceToSqrt( m ), 1.0 );
		}

		[Constructable]
		public SummonedTreefellow() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a treant";
			Body = 309;
			BaseSoundID = 442;

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
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 40;
			ControlSlots = ( Core.SE ) ? 2 : 1;
		}

		public override bool BleedImmune{ get{ return true; } }
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
					//TODO: Confim if it's the dispel with all the pretty effects or just a Deletion of it.
					Dispel( ( (Mobile) spirtsOrVortexes[index] ) );
					spirtsOrVortexes.RemoveAt( index );
				}
			}

			base.OnThink();
		}

		public SummonedTreefellow( Serial serial ) : base( serial )
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
			if ( BaseSoundID == 263 )
				BaseSoundID = 0;
		}
	}
}