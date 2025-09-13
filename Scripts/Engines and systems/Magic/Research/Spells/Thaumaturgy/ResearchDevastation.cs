using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Research
{
	public class ResearchDevastation : ResearchSpell
	{
		public override int spellIndex { get { return 62; } }
		public int CirclePower = 8;
		public static int spellID = 62;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				233,
				9042
			);

		public ResearchDevastation( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.SendMessage( "Choose a focal point for this spell, if you dare!" );
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( Server.Misc.Worlds.NoApocalypse( Caster.Location, Caster.Map ) )
			{
				Caster.SendMessage( "You don't think it is wise to cast this here." ); 
				return;
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				if ( p is Item )
					p = ((Item)p).GetWorldLocation();

				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;

				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 20 );

					foreach ( Mobile m in eable )
					{
						if ( Caster.Region == m.Region && Caster != m && Caster.InLOS( m ) && m.Blessed == false && Caster.CanBeHarmful( m, true ) )
						{
							targets.Add( m );

							if ( m.Mounted )
							{
								IMount mount = m.Mount;

								if( mount != null )
									mount.Rider = null;

								if ( mount is BaseCreature )
								{
									if ( ((Mobile)mount).Blessed == false && Caster.CanBeHarmful( ((Mobile)mount), true ) )
										targets.Add( ((Mobile)mount) );
								}
							}
						}
					}

					eable.Free();
				}

				if ( targets.Count > 0 )
				{
					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = targets[i];

						Point3D blast = new Point3D( ( m.X ), ( m.Y ), m.Z+10 );
						Effects.SendLocationEffect( blast, m.Map, 0x2A4E, 30, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
						m.PlaySound( 0x029 );

						Point3D blast1 = new Point3D( ( m.X ), ( m.Y ), m.Z+5 );
						Point3D blast2 = new Point3D( ( m.X-1 ), ( m.Y ), m.Z+5 );
						Point3D blast3 = new Point3D( ( m.X+1 ), ( m.Y ), m.Z+5 );
						Point3D blast4 = new Point3D( ( m.X ), ( m.Y-1 ), m.Z+5 );
						Point3D blast5 = new Point3D( ( m.X ), ( m.Y+1 ), m.Z+5 );

						if ( Utility.RandomBool() ){ Effects.SendLocationEffect( blast1, m.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 ); }
						if ( Utility.RandomBool() ){ Effects.SendLocationEffect( blast2, m.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 ); }
						if ( Utility.RandomBool() ){ Effects.SendLocationEffect( blast3, m.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 ); }
						if ( Utility.RandomBool() ){ Effects.SendLocationEffect( blast4, m.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 ); }
						if ( Utility.RandomBool() ){ Effects.SendLocationEffect( blast5, m.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 ); }

						Effects.PlaySound( m.Location, m.Map, 0x307 );

						Caster.DoHarmful( m );

						if ( m is PlayerMobile ){ m.Damage( 10000, Caster ); Caster.Criminal = true; }
						else if ( m is BaseCreature )
						{
							if ( m is BaseVendor || m is BasePerson )
							{
								Caster.Criminal = true;
								Caster.Kills = Caster.Kills + 1;
							}

							BaseCreature bc = (BaseCreature)m;
							if ( bc.Controlled ){ m.Damage( 10000, Caster ); }
							else { m.Delete(); }
						}
					}

					TimeSpan duration = TimeSpan.FromSeconds( 3.0 );
					new DeathTimer( Caster, duration ).Start();
					Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, true );
					KarmaMod( Caster, ((int)RequiredSkill+RequiredMana) );
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private ResearchDevastation m_Owner;

			public InternalTarget( ResearchDevastation owner ) : base( Core.ML ? 10 : 12, true, TargetFlags.None )
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

		public class DeathTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public DeathTimer( Mobile suicide, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = suicide;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					Point3D bolt = new Point3D( ( m_m.X ), ( m_m.Y ), m_m.Z+10 );
					Effects.SendLocationEffect( bolt, m_m.Map, 0x2A4E, 30, 10, Server.Items.CharacterDatabase.GetMySpellHue( m_m, 0 ), 0 );
					m_m.PlaySound( 0x029 );
					m_m.Damage( 10000, m_m );
					Stop();
				}
			}
		}
	}
}