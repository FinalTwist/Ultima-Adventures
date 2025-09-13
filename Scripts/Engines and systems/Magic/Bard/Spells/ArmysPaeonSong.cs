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
	public class ArmysPaeonSong : ModifierSong
	{
		
		private static SpellInfo m_Info = new SpellInfo(
			"Army's Paeon", "*plays an army's paeon*",
			//SpellCircle.First,
			//212,
			//9041
			-1
			);
	
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 5 ); } }
		public override double RequiredSkill{ get{ return 60.0; } }
		public override int RequiredMana{ get{ return 30; } }
	 
		public ArmysPaeonSong( Mobile caster, Item scroll) : base( caster, scroll, m_Info )
		{
            ModType = 3;
            ExpiryMessage = "The effect of the army's paeon wears off";
            ModLevel = 6;
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
	                Duration = (int)( Caster.Skills[SkillName.Musicianship].Value * .16 );
	                this.DetermineModLevelBySkill(MusicSkill( Caster ));
	                for ( int i = 0; i < Targets.Count; ++i )
	                {
	                    Mobile mobile = (Mobile)Targets[i];
	                    mobile.SendMessage( "You feel the rush of blood return to you." );   
	                    mobile.FixedParticles( 0x373A, 10, 15, 5012, 0x14, 3, EffectLayer.Waist );       
	                    mobile.PlaySound(0x1F2);   
						SongEffect songEffect = new SongEffect(mobile.Serial, this);
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
