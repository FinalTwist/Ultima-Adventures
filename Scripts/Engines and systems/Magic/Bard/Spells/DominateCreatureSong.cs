using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.OneTime.Events;
using System.Collections;

namespace Server.Spells.Song
{
	public class DominateCreatureSong : ModifierSong
	{
		
		private static SpellInfo m_Info = new SpellInfo(
				"Dominate Creature", "*plays an oppressive melody*",
				//SpellCircle.First,
				//212,9041
				-1
			);
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 5 ); } }
		public override double RequiredSkill{ get{ return 80.0; } }
		public override int RequiredMana{ get{ return 70; } }
		
		public DominateCreatureSong( Mobile caster, Item scroll) : base( caster, scroll, m_Info )
		{	
			ModType = 5;
			ExpiryMessage = "The effect of the oppressive melody wears off.";
			IsHkCheck = true;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{	
			Spellbook book = Spellbook.Find(Caster, -1, SpellbookType.Song);		
	
			if (book != null) {
				SongBook = (SongBook)book;
				bool sings = false;
				Targets = new ArrayList(){ m };		
				if ( !Caster.CanSee( m ) )
				{
					Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
				} 
				else if ( CheckHSequence( m ) && this.CanCast() )
				{ 
					Mobile source = Caster;
					if ( m is Mobile )
					{
						BaseCreature creature = (BaseCreature)m;
						double diff = (SongBook.Instrument.GetDifficultyFor(Caster, creature ) * 0.5) - 5.0;
						double music = Caster.Skills[SkillName.Musicianship].Value;

						if ( music > 100.0 )
							diff -= (music - 100.0) * 0.5;

						 if ( creature.Controlled )
						{
							Caster.SendLocalizedMessage( 501590 ); // They are too loyal to their master to be dominated.
						}
						else if (creature.BardImmune || (creature.IsParagon && BaseInstrument.GetBaseDifficulty( creature ) >= 160.0) )
						{
							Caster.SendMessage( "You have no chance of dominating this creature." );
						} 
						else if ( !Caster.CheckTargetSkill( SkillName.Provocation, creature, diff-25.0, diff+25.0 ) || (FailedFameCheck(m))) 
						{	
							base.DoFizzle();
						} else {
							sings = true;

						    SpellHelper.Turn( source, m );
						    Targets = new ArrayList(){ m };

						    m.FixedParticles( 0x374A, 10, 30, 5013, 0x14, 2, EffectLayer.Waist );
						    bool IsSlayer = false;
						    if ( m is BaseCreature ){ 
						    	IsSlayer = base.CheckSlayer( SongBook.Instrument, m );
							}
			
							Duration = this.CalculateDurationByFame(m, false);
						    if ( IsSlayer == true )
						    {
								Duration = (int)(Duration*1.25);
						    }

							m.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502799, Caster.NetState ); // It seems to accept you as master.
							creature.Owners.Add( Caster );
							creature.SetControlMaster( Caster );
							creature.Summoned = true;
						    OneTimeSecEvent.SecTimerTick += SecondTimerTick;
						}
					}
				}
				BardFunctions.UseBardInstrument( SongBook.Instrument, sings, Caster );
				FinishSequence();	
			}	
		}

		private class InternalTarget : Target
		{
			private DominateCreatureSong m_Owner;

			public InternalTarget( DominateCreatureSong owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
					m_Owner.Target( (Mobile)targeted );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}