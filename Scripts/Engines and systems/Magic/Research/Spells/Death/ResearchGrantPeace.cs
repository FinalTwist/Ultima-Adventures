using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;

namespace Server.Spells.Research
{
	public class ResearchGrantPeace : ResearchSpell
	{
		public override int spellIndex { get { return 18; } }
		public int CirclePower = 4;
		public static int spellID = 18;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				203,
				9031
			);

		public ResearchGrantPeace( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
			Caster.SendMessage( "Which undead creature do you wish to banish?" );
		}

		public void Target( Mobile m )
		{
			SlayerEntry undead = SlayerGroup.GetEntryByName( SlayerName.Silver );

			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( m is BaseCreature )
			{
				BaseCreature bc = m as BaseCreature;

				double damage = DamagingSkill( Caster )-30;
					if ( damage > 200 ){ damage = 200.0; }
					if ( damage < 50 ){ damage = 50.0; }

				if (!undead.Slays(m))
				{
					Caster.SendMessage( "This spell cannot be used on this type of creature." );
				}
				else if( bc.IsBonded )
				{
					Caster.SendMessage("This spell cannot affect such a creature!");
				}
				else if ( undead.Slays(m) && !bc.IsDispellable )
				{
					m.Say("Your pitiful spell amuses me, mortal!");
					Caster.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
					Caster.PlaySound(0x208);
					SpellHelper.Damage(this, Caster, damage, 0, 0, 0, 0, 100);
				}
				else if ( m.Fame >= 23000 )
				{
					m.Say("Your magic is puny in comparison to my power!");
					Caster.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
					Caster.PlaySound(0x208);
					SpellHelper.Damage(this, Caster, damage, 0, 0, 0, 0, 100);
				}
				else if (CheckHSequence(m))
				{
					int exChance = (int)(m.Fame/200)+10;
					if ( DamagingSkill( Caster ) >= Utility.RandomMinMax( 1, exChance ) )
					{
						m.Say("No! You cannot send me toward the light! Death is not eternal!");
						SpellHelper.Turn(Caster, m);
						m.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
						m.PlaySound(0x208);
						new InternalTimer(m).Start();
					}
					else
					{
						Caster.SendMessage( "You fail at your attempt, but did cause some energy damage." );
						Caster.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
						Caster.PlaySound(0x208);
						SpellHelper.Damage(this, Caster, damage, 0, 0, 0, 0, 100);
					}
				}
				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false );
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
			private ResearchGrantPeace m_Owner;
			public InternalTarget( ResearchGrantPeace owner ) : base( 12, false, TargetFlags.Harmful )
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