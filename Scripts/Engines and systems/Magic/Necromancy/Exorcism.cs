using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;

namespace Server.Spells.Necromancy
{
	public class ExorcismSpell : NecromancerSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Exorcism", "Ort Corp Grav",
				203,
				9031,
				Reagent.NoxCrystal,
				Reagent.GraveDust
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		public override double RequiredSkill { get { return 80.0; } }
		public override int RequiredMana { get { return 40; } }

		public ExorcismSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
			Caster.SendMessage( "Which demon or undead do you wish to exorcise from this realm?" );
		}

		public override bool CheckCast(Mobile caster)
		{
			if( Caster.Skills.SpiritSpeak.Value < 100.0 )
			{
				Caster.SendLocalizedMessage( 1072112 ); // You must have GM Spirit Speak to use this spell
				return false;
			}

			return base.CheckCast( caster );
		}

		public void Target( Mobile m )
		{
			SlayerEntry undead = SlayerGroup.GetEntryByName( SlayerName.Silver );
			SlayerEntry exorcism = SlayerGroup.GetEntryByName( SlayerName.Exorcism );

			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( m is BaseCreature )
			{
				BaseCreature bc = m as BaseCreature;

				if (!undead.Slays(m) && !exorcism.Slays(m))
				{
					Caster.SendMessage( "This spell cannot be used on this type of creature." );
				}
				else if( bc.IsBonded )
				{
					Caster.SendMessage("This spell cannot banish such a creature!");
				}
				else if ( exorcism.Slays(m) && !bc.IsDispellable )
				{
					m.Say("Your pitiful spell amuses me, mortal!");
					double damage;
					damage = GetNewAosDamage(48, 1, 5, Caster);
					Caster.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
					Caster.PlaySound(0x208);
					SpellHelper.Damage(this, Caster, damage, 0, 100, 0, 0, 0);
				}
				else if ( m.Fame >= 23000 )
				{
					m.Say("Your magic is puny in comparison to my power!");
					double damage;
					damage = GetNewAosDamage(48, 1, 5, Caster);
					Caster.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
					Caster.PlaySound(0x208);
					SpellHelper.Damage(this, Caster, damage, 0, 100, 0, 0, 0);
				}
				else if (CheckHSequence(m))
				{
					int exChance = (int)(m.Fame/200)+10;
					if ( Caster.Skills[SkillName.Necromancy].Value >= Utility.RandomMinMax( 1, exChance ) )
					{
						if (undead.Slays(m))
						{
							m.Say("No! You cannot banish me! I will return from the Underworld!");
						}
						else
						{
							m.Say("No! You cannot kill that which is dead! I will return!");
						}

						SpellHelper.Turn(Caster, m);
						m.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
						m.PlaySound(0x208);
						new InternalTimer(m).Start();
					}
					else
					{
						Caster.SendMessage( "You fail at your exorcism, but did cause some damage." );
						double damage;
						damage = GetNewAosDamage(48, 1, 5, Caster);
						Caster.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
						Caster.PlaySound(0x208);
						SpellHelper.Damage(this, Caster, damage, 0, 100, 0, 0, 0);
					}
				}
			}
			FinishSequence();
		}

		private class InternalTimer : Timer
		{
			Mobile m_Owner;

			public InternalTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 1.5 ) )
			{
				m_Owner = owner;
			}

			protected override void OnTick()
			{
				if ( m_Owner != null) 
				{
                    if( m_Owner.CheckAlive() )
					m_Owner.Delete();
				}
			}
		}

		private class InternalTarget : Target
		{
			private ExorcismSpell m_Owner;
			public InternalTarget( ExorcismSpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( m_Owner !=null && o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}