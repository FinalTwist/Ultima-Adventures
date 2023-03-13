using System;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Targeting;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Regions;
using Server.Spells.Sixth;

namespace Server.Spells.Jedi
{
    public class MindsEye : JediSpell
	{
		public override int spellIndex { get { return 281; } }
		public int CirclePower = 1;
		public static int spellID = 281;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 4 ) ),
				206,
				0
			);

        public MindsEye(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckSequence() )
			{
				IPooledEnumerable TitemsInRange = Caster.Map.GetItemsInRange( new Point3D( p ), 1 + (int)(Caster.Skills[SkillName.EvalInt].Value / 20.0) );
				string sTrap;
				foreach ( Item item in TitemsInRange )
				{
					if ( item is BaseTrap )
					{
						BaseTrap trap = (BaseTrap) item;

						if ( trap is FireColumnTrap ){ sTrap = "(fire column trap)"; }
						else if ( trap is FlameSpurtTrap ){ sTrap = "(fire spurt trap)"; }
						else if ( trap is GasTrap ){ sTrap = "(poison gas trap)"; }
						else if ( trap is GiantSpikeTrap ){ sTrap = "(giant spike trap)"; }
						else if ( trap is MushroomTrap ){ sTrap = "(mushroom trap)"; }
						else if ( trap is SawTrap ){ sTrap = "(saw blade trap)"; }
						else if ( trap is SpikeTrap ){ sTrap = "(spike trap)"; }
						else if ( trap is StoneFaceTrap ){ sTrap = "(stone face trap)"; }
						else { sTrap = ""; }

						Effects.SendLocationParticles( EffectItem.Create( item.Location, item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 0xB41, 0, 5024, 0 );
						Effects.PlaySound( item.Location, item.Map, 0x1FA );
						Caster.SendMessage( "There is a trap nearby! " + sTrap + "" );
					}
					else if ( item is HiddenTrap )
					{
						Effects.SendLocationParticles( EffectItem.Create( item.Location, item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 0xB41, 0, 5024, 0 );
						Effects.PlaySound( item.Location, item.Map, 0x1FA );
						Caster.SendMessage( "There is a hidden floor trap somewhere nearby!" );
					}
					else if ( item is HiddenChest )
					{
						Caster.SendMessage( "Your mind sees a vision of something nearby." );
						string where = Server.Misc.Worlds.GetRegionName( Caster.Map, Caster.Location );

						int money = Utility.RandomMinMax( 50, 100 );

						int level = (int)(GetJediDamage( Caster ) / 50);
							if (level < 1){level = 1;}
							if (level > 6){level = 6;}

						switch( Utility.RandomMinMax( 1, level ) )
						{
							case 1: level = 1; break;
							case 2: level = 2; break;
							case 3: level = 3; break;
							case 4: level = 4; break;
							case 5: level = 5; break;
							case 6: level = 6; break;
						}

						if ( Utility.RandomMinMax( 1, 3 ) > 1 )
						{
							// DO NOTHING BECAUSE THE DETECT HIDDEN SKILL IS MUCH BETTER
						}
						else if ( level > 4 )
						{
							if ( level == 5 ){ level = 1; }
							else { level = 2; }

							HiddenBox mBox = new HiddenBox( level, where, Caster );

							Point3D loc = item.Location;
							mBox.MoveToWorld( loc, Caster.Map );
							Effects.SendLocationParticles( EffectItem.Create( mBox.Location, mBox.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 0xB41, 0, 5024, 0 );
							Effects.PlaySound( mBox.Location, mBox.Map, 0x1FA );
						}
						else
						{
							Gold coins = new Gold( ( money * level ) );

							Point3D loc = item.Location;
							coins.MoveToWorld( loc, Caster.Map );
							Effects.SendLocationParticles( EffectItem.Create( coins.Location, coins.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 0xB41, 0, 5024, 0 );
							Effects.PlaySound( coins.Location, coins.Map, 0x1FA );
						}

						item.Delete();
					}
				}
				TitemsInRange.Free(); /////////////////////////////////////////////////////////////////////////////

				SpellHelper.Turn( Caster, p );

				SpellHelper.GetSurfaceTop( ref p );

				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;

				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 1 + (int)(GetJediDamage( Caster ) / 20.0) );

					foreach ( Mobile m in eable )
					{
						if ( m.Hidden && (m.AccessLevel == AccessLevel.Player || Caster.AccessLevel > m.AccessLevel) && CheckDifficulty( Caster, m ) )
							targets.Add( m );
					}

					eable.Free();
				}

				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = targets[i];

					m.RevealingAction();

					m.FixedParticles( 0x375A, 9, 20, 5049, 0xB41, 0, EffectLayer.Head );
					m.PlaySound( 0x1FD );
				}
			}

			FinishSequence();
		}

		// Reveal uses magery and detect hidden vs. hide and stealth 
		private static bool CheckDifficulty( Mobile from, Mobile m )
		{
			if ( InvisibilitySpell.HasTimer( m ) )
				return true;

			int magery = (int)(GetJediDamage( from ));
			int detectHidden = (int)(from.Skills[SkillName.EvalInt].Fixed);

			int hiding = m.Skills[SkillName.Hiding].Fixed;
			int stealth = m.Skills[SkillName.Stealth].Fixed;
			int divisor = hiding + stealth;

			int chance;
			if ( divisor > 0 )
				chance = 50 * (magery + detectHidden) / divisor;
			else
				chance = 100;

			return chance > Utility.Random( 100 );
		}

		private class InternalTarget : Target
		{
			private MindsEye m_Owner;

			public InternalTarget( MindsEye owner ) : base( Core.ML ? 10 : 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}