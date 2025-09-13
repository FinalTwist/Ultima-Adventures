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
	public class EnchantingEtudeSong : ModifierSong
	{
	
		private static SpellInfo m_Info = new SpellInfo(
			"Enchanting Etude", "*plays an enchanting etude*",
			//SpellCircle.First,
			//212,
			//9041
			-1
			);
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2 ); } }
		public override double RequiredSkill{ get{ return 65.0; } }
		public override int RequiredMana{ get{ return 40; } }

		public EnchantingEtudeSong( Mobile caster, Item scroll) : base( caster, scroll, m_Info )
		{
			ModType = 4;
            ExpiryMessage = "The effect of the enchanting etude wears off";
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

					string statType = "int";
					for ( int i = 0; i < Targets.Count; ++i )
					{
						Mobile m = (Mobile)Targets[i];                    
						StatMod mod = new StatMod( StatType.Int, statType, amount, TimeSpan.FromSeconds( (double)Duration ) );					
						m.AddStatMod( mod );
						SongEffect songEffect = new SongEffect(m.Serial, this);
						m.FixedParticles( 0x375A, 10, 15, 5017, 0x1F8, 3, EffectLayer.Waist );
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
