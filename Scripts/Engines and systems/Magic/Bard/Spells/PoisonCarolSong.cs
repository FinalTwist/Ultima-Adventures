using System;
using System.Collections;
using System.Collections.Generic;
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
	public class PoisonCarolSong : ModifierSong
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Poison Carol", "*plays a poison carol*",
				//SpellCircle.First,
				//212,9041
				-1
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 5 ); } }
		public override double RequiredSkill{ get{ return 50.0; } }
		public override int RequiredMana{ get{ return 20; } }
		public PoisonCarolSong( Mobile caster, Item scroll) : base( caster, scroll, m_Info )
		{
			ModType	= 1;
			ExpiryMessage = "The effect of the poison carol wears off.";
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
					Duration = ((int)(Caster.Skills[SkillName.Musicianship].Value/2) + (int)this.CalculateDurationByFame(Caster, true));
					int modAmount = MyServerSettings.PlayerLevelMod( (int)(MusicSkill( Caster ) / 25), Caster );

					if (Caster is PlayerMobile && ((PlayerMobile)Caster).Troubadour())
					{
						modAmount = MyServerSettings.PlayerLevelMod( (int)(MusicSkill( Caster ) / 15), Caster );
						Duration *= (int)((double)Duration * 1.3);
					}

					for ( int i = 0; i < Targets.Count; ++i )
					{
						Mobile mobile = (Mobile)Targets[i];
						if ( mobile.PoisonResistance < 70 && (mobile.PoisonResistance + modAmount) > 70)
							modAmount = 70 - mobile.PoisonResistance;
						if (mobile.PoisonResistance < 70)
						{
							mobile.SendMessage( "Your resistance to poison has increased." );	
							mobile.FixedParticles( 0x373A, 10, 15, 5012, 0x238, 3, EffectLayer.Waist );
							Modification =  new ResistanceMod( ResistanceType.Poison, modAmount );
							mobile.AddResistanceMod( Modification );
							Mod = Modification;
							SongEffect songEffect = new SongEffect(mobile.Serial, this);
							base.AddSongEffect(songEffect);
						}
						
					}
					OneTimeSecEvent.SecTimerTick += SecondTimerTick;
	        	}
	        	BardFunctions.UseBardInstrument( SongBook.Instrument, sings, Caster );
				FinishSequence();
            }
		}
	}
}
