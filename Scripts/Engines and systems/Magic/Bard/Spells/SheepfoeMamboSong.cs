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
using Server.OneTime.Events;

namespace Server.Spells.Song
{
	public class SheepfoeMamboSong : ModifierSong
	{
		
		private static SpellInfo m_Info = new SpellInfo(
				"Shepherd's Dance", "*plays a shepherd's dance*",
				//SpellCircle.First,
				//212,9041
				-1
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2 ); } }
		public override double RequiredSkill{ get{ return 65.0; } }
		public override int RequiredMana{ get{ return 40; } }
		
		public SheepfoeMamboSong( Mobile caster, Item scroll) : base( caster, scroll, m_Info )
		{
			ModType = 4;
            ExpiryMessage = "The effect of the shepherd's dance wears off";
		}

        public override void OnCast()
        {
			Spellbook book = Spellbook.Find(Caster, -1, SpellbookType.Song);		
			if (book != null) {
				SongBook = (SongBook)book;
	    		bool sings = false;
	            base.GetElligbleBeneficials();
	        	if (this.CanCast()) {
	        		sings = true;
					int amount = (int)(Caster.Skills[SkillName.Musicianship].Value / 10);
					Duration = ((int)(Caster.Skills[SkillName.Musicianship].Value/2) + (int)this.CalculateDurationByFame(Caster, true));
					
					if (Caster is PlayerMobile && ((PlayerMobile)Caster).Troubadour())
					{
						amount = (int)(Caster.Skills[SkillName.Musicianship].Value / 5);
						Duration *= (int)((double)Duration * 1.3);
					}
					
					string dex = "dex";
					for ( int i = 0; i < Targets.Count; ++i )
					{
						Mobile m = (Mobile)Targets[i];						
						StatMod mod = new StatMod( StatType.Dex, dex, amount, TimeSpan.FromSeconds( (double)Duration ) );
						m.AddStatMod( mod );
						m.FixedParticles( 0x375A, 10, 15, 5017, 0x224, 3, EffectLayer.Waist );
						SongEffect songEffect = new SongEffect(m.Serial, this);
						base.AddSongEffect(songEffect);
					}
					OneTimeSecEvent.SecTimerTick += SecondTimerTick;
	        	}
				BardFunctions.UseBardInstrument( SongBook.Instrument, sings, Caster );
				FinishSequence();
			}
		}
	}
}
