using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Gumps;
using Server.Spells;
using Server.Misc;

namespace Server.Spells.Song
{
	public class MagicFinaleSong : Song
	{
		
		private static SpellInfo m_Info = new SpellInfo(
				"Magic Finale", "*plays a magic finale*",
				-1
			);

		private SongBook m_Book;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 5 ); } }
		public override double RequiredSkill{ get{ return 70.0; } }
		public override int RequiredMana{ get{ return 55; } }
		
		public MagicFinaleSong( Mobile caster, Item scroll) : base( caster, scroll, m_Info )
		{
		}

        public override void OnCast()
        {
            //get songbook instrument
            Spellbook book = Spellbook.Find(Caster, -1, SpellbookType.Song);
            if (book == null)
            {
                return;
            }
            m_Book = (SongBook)book;
            if (m_Book.Instrument == null || !(Caster.InRange(m_Book.Instrument.GetWorldLocation(), 1)))
            {
                Caster.SendMessage("Your instrument is missing! You can select another from your song book.");
                return;
            }

			bool sings = false;
 
			if( CheckSequence() )
			{
				sings = true;
 
				ArrayList targets = new ArrayList();

				foreach ( Mobile m in Caster.GetMobilesInRange( BaseInstrument.GetBardRange( Caster, SkillName.Provocation ) ) )
				{
					if ( m is BaseCreature )
					{
						targets.Add( m );
					}
				}
				
				Caster.FixedParticles( 0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist );
				
				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = (Mobile)targets[i];
					
					if ((m is BaseCreature && ((BaseCreature)m).Summoned) || ((BaseCreature)m).ControlSlots == 666)
					{
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );

						m.Delete();
					}
					else if (m is BaseCreature && ((BaseCreature)m).BardProvoked && Caster is PlayerMobile && ((PlayerMobile)Caster).Troubadour())
					{
						BaseCreature mob = (BaseCreature)m;
						if (mob.Combatant != null && mob.Combatant is BaseCreature)
						{
							//troubadour bonus
							BaseCreature target = (BaseCreature)mob.Combatant;

							double bonus = 5 + ((mob.Skills[SkillName.Wrestling].Value + mob.Skills[SkillName.Tactics].Value) / 90) + (mob.Dex / 100);
								int min = (int)((double)mob.DamageMin * bonus * (1+ (mob.Str/50)));
								int max = (int)((double)mob.DamageMax * bonus * (1+ (mob.Str/50)));

								int dmg = AdventuresFunctions.DiminishingReturns( Utility.RandomMinMax(min, max) , 300, 10 );

								AOS.Damage( target, mob, dmg, true, 100, 0, 0, 0, 0);
						}


					}
				}
			}

			BardFunctions.UseBardInstrument( m_Book.Instrument, sings, Caster );
			FinishSequence();
		}
	}
}

