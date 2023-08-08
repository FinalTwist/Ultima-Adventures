using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.Threading;
using Server.OneTime.Events;
using Server.Engines.PartySystem;

namespace Server.Spells.Song
{
	public abstract class ModifierSong : Song
	{
		private ArrayList m_Targets;
		private SongBook m_Book;
		private bool m_isHkCheck;
		private bool m_DamageOverTime;
		private ResistanceMod m_Modification;
		private int m_Duration;
		private int m_ModType;
		private int m_ModLevel;
		private int m_ModAmount;
		private string m_ExpiryMessage;
		private int m_TargetOriginalResistance;
		private ResistanceMod m_mod;
		
		public ResistanceMod Mod {
			get { return m_mod; }
			set { m_mod = value;}
		}
		
		public bool IsDamageOverTime {
			get { return m_DamageOverTime; }
			set { m_DamageOverTime = value;}
		}

		public bool IsHkCheck {
			get { return m_isHkCheck; }
			set { m_isHkCheck = value;}
		}

		public int ModAmount { // used in ModTypes 3
			get { return m_ModAmount; }
			set { m_ModAmount = value;}
		}

		public int ModLevel { // used in ModTypes 3
			get { return m_ModLevel; }
			set { m_ModLevel = value;}
		}
		public string ExpiryMessage {
			get { return m_ExpiryMessage; }
			set { m_ExpiryMessage = value;}
		}

		public int ModType { // 1 Buff; 2 Debuff; 3 Hit stacking; 4 StatMod
			get{ return m_ModType; }
            set{ m_ModType = value; }
		}

		public ArrayList Targets {
			get{ return m_Targets; }
            set{ m_Targets = value; }
		}

		public ResistanceMod DummyModification {
		 	get{ return m_Modification; }
            set{ m_Modification = value; }
		}


		public ResistanceMod Modification {
		 	get{ return m_Modification; }
            set{ m_Modification = value; }
		}

        public int Duration
        {
            get{ return m_Duration; }
            set{ m_Duration = value; }
        }

        public int TargetOriginalResistance
        {
            get{ return m_TargetOriginalResistance; }
            set{ m_TargetOriginalResistance = value; }
        }

        public SongBook SongBook
        {
            get{ return m_Book; }
            set{ m_Book = value; }
        }
        

        public ModifierSong( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info ) 
		{ 
			ExpiryMessage = "The effect of the musical spell wears off.";
			Duration = 0;
			Targets = new ArrayList();
			IsHkCheck = false;
			ModLevel = 6;
			IsDamageOverTime = false;
		}

		public void AddSongEffect(SongEffect songEffect) {
			List<SongEffect> songEffects = ((PlayerMobile)Caster).SongEffects;
			songEffects.Add(songEffect);
			((PlayerMobile)Caster).SongEffects = songEffects;
		}

		public bool SongIsAlreadyOnTarget(Mobile target) {
			List<SongEffect> songEffects = ((PlayerMobile)Caster).SongEffects;
			SongEffect songEffect = songEffects.Find(effect => target.Serial == effect.MobileSerial && effect.Song.GetType() == this.GetType() );
			if (songEffect != null) {
				Caster.SendMessage("This song is already in effect on the target");
			}
			return (songEffect != null);
		}


		public void GetElligbleBeneficials() {
			PlayerMobile player = (PlayerMobile)Caster;
			Targets.Add(Caster);
			// only buff nearby companions
			foreach ( Mobile m in player.getNearbyPets( 3 ) )
			{
				Targets.Add( m );
			}
			Party party = (Party)player.Party;
			if (party != null) {
				// only buff nearby party members
				foreach ( Mobile m in player.GetMobilesInRange( 3 ) )
				{
					foreach (PartyMemberInfo member in party.Members) {
						if (member.Mobile.Serial == m.Serial && m.Serial != Caster.Serial) {
							Targets.Add( m );
						}	
					}
				}
			}

			// remove any targets already effected by this spell
			List<SongEffect> songEffects = ((PlayerMobile)Caster).SongEffects;
			for ( int i = 0; i < Targets.Count; ++i )
				{
					Mobile mobile = (Mobile)Targets[i];
					if (songEffects != null) {	
						foreach (SongEffect effect in songEffects) {
							Serial serial = effect.MobileSerial;
							Song song = effect.Song;
							if (mobile.Serial == serial && song.GetType() == this.GetType()) {
								Targets.Remove(mobile);
							}
						}
					}
				}
			}

		public void DetermineModLevelBySkill(int musicSkill) {
		 if (musicSkill > 120 && musicSkill < 240)
            {
                ModLevel = 7;
            }
            else if (musicSkill < 360)
            { 
                ModLevel = 8;
            }
            else if (musicSkill < 480)
            { 
                ModLevel = 9;
            }
            else if (musicSkill >= 480)
            {
                ModLevel = 10;
            }
		}

		public virtual bool CheckSlayer( BaseInstrument instrument, Mobile defender )
		{
			SlayerEntry atkSlayer = SlayerGroup.GetEntryByName( instrument.Slayer );
			SlayerEntry atkSlayer2 = SlayerGroup.GetEntryByName( instrument.Slayer2 );
			if ( atkSlayer != null && atkSlayer.Slays( defender )  || atkSlayer2 != null && atkSlayer2.Slays( defender ) )
				return true;

			return false;
		}

		public bool HasSongBook() {
            return (SongBook != null);
		}

		public bool CanCast() {
		  //get songbook instrument
            
            if ( !BaseInstrument.CheckMusicianship( Caster ) )
			{
				Caster.SendLocalizedMessage( 500612 ); // You play poorly, and there is no effect.
			} else if (SongBook.Instrument == null || !(Caster.InRange(SongBook.Instrument.GetWorldLocation(), 1)))
            {
                Caster.SendMessage("Your instrument is missing! You can select another from your song book.");
            } else if (SongBook.Instrument == null || !(Caster.InRange(SongBook.Instrument.GetWorldLocation(), 1)))
            {
                Caster.SendMessage("You need an instrument to play that song!");
            } else if (Targets.Count == 0) {
            	Caster.SendMessage("There are no eligible targets for your song!");
            } else if (!IsHkCheck && CheckSequence())
			{
				return true;
			} else if (IsHkCheck) { // is a HkCheck being performed, which calls CheckSequence anyway
				return true;
			}
			return false;
		}

		public void RemoveSongEffect(Mobile mobile) {
			List<SongEffect> songEffects = ((PlayerMobile)Caster).SongEffects;
			if (songEffects != null) {	
				foreach (SongEffect effect in songEffects.ToArray()) {
					Serial serial = effect.MobileSerial;
					Song song = effect.Song;
					if (mobile.Serial == serial && song.GetType() == this.GetType()) {
						songEffects.Remove(effect);
					}
				}
			}
			((PlayerMobile)Caster).SongEffects = songEffects;
			
			if (m_mod != null)
				mobile.RemoveResistanceMod(m_mod);
		}

		public int CalculateDurationByFame(Mobile mobile, bool inverse) {
			int fameModifier = this.CalculateFameProbability(mobile);
			int duration = (int)MyServerSettings.PlayerLevelMod(100-fameModifier, mobile);
			if (inverse) {
				duration = (int)MyServerSettings.PlayerLevelMod(0+fameModifier, mobile);
			}

			if (duration < MyServerSettings.BardSongMinDurationCap()) {
				duration = MyServerSettings.BardSongMinDurationCap(); 
			} 
			return duration;
		}

		public int CalculateDurationByMagicResist(Mobile mobile) {
			int magicResist = (int)mobile.Skills[SkillName.MagicResist].Value;
			int duration =  (int)MyServerSettings.PlayerLevelMod((MusicSkill(Caster)/10), mobile);
			if (magicResist > 0) {
				duration = (int)MyServerSettings.PlayerLevelMod(MusicSkill(Caster)/(int)mobile.Skills[SkillName.MagicResist].Value, mobile);
			}
			if (duration < MyServerSettings.BardSongMinDurationCap()) {
				duration = MyServerSettings.BardSongMinDurationCap(); 
			} 
			return duration;
		}

		public bool FailedFameCheck(Mobile mobile) {
			Random rand = new Random(); 
			// get dynamic fame check success modifier
			// the higher the probability the greater chance of failure
			int probabilityToFail = this.CalculateFameProbability(mobile);
			return (rand.Next(1,101) <= probabilityToFail);
		} 

		public int CalculateFameProbability(Mobile mobile) {
			Random rand = new Random(); 
			int totalSkill = MusicSkill( Caster );
			// for a maxed character with any mob that has 20k+ fame, add a random difficulty modifier
			int fameModifier = (int)(mobile.Fame/totalSkill);
			int max = 20;
			fameModifier += rand.Next(1,max); 	
			return fameModifier;
		}

		// public void LowerMobileResistance(EditableMobile mobile, int amount) {
		// 	if (ModResistanceType != null) {
		// 		TargetOriginalResistance = mobile.GetResistance(ModResistanceType);
		// 		if (amount > TargetOriginalResistance) {
		// 			amount = TargetOriginalResistance;
		// 		}
		// 		int newAmount = TargetOriginalResistance - amount;
		//     	mobile.SetResistance(ModResistanceType, newAmount);
		//     	if (mobile is BaseCreature) {
		//     		Caster.SendMessage("Your song reduces " + mobile.Name + "'s resistances by " + amount.ToString() );	
		//     	} else {
		//     		Caster.SendMessage("Your song reduces the target's resistances by " + amount.ToString() );	
		//     	}
		    	
		// 	}	
		// }

        public void SecondTimerTick(object sender, EventArgs e)
        {
			if (Targets.Count > 0) {
				switch (ModType) {
				case 3: // increase hit points
				case 6: // increase mana
					if (ModLevel != null) {
						if (Duration > 0) { // still some rounds to go
							for ( int i = 0; i < Targets.Count; ++i )
							{
								Mobile mobile = (Mobile)Targets[i];
								int amount = MyServerSettings.PlayerLevelMod(ModLevel, mobile);
								if (ModType == 3) {
									mobile.Hits += amount;
									// dont let the Caster benefit from the mana boost
								} else if (ModType == 6 && Caster.Serial != mobile.Serial) {
									mobile.Mana += amount;
								}
								
								if (Duration == 1) {
									mobile.SendMessage(ExpiryMessage);
									// remove them on the last tick
									this.RemoveSongEffect(mobile);
								}
							}
							Duration--;
						} else {
							OneTimeSecEvent.SecTimerTick -= SecondTimerTick;
						}
					}
					return;
				break;
				default: // catch 1, 2 and 4
					 if (Duration == 0)
			            {
			                for ( int i = 0; i < Targets.Count; ++i )
							{
								Mobile mobile = (Mobile)Targets[i];
								if (ModType == 1 || ModType == 2) {
									mobile.RemoveResistanceMod(Modification);
								} else if (ModType == 5) {
									((BaseCreature)mobile).Owners.Remove(Caster);
									((BaseCreature)mobile).Summoned = false;
									((BaseCreature)mobile).SetControlMaster( null );		
								}
								this.RemoveSongEffect(mobile);
								if (Caster.Serial != mobile.Serial) {
									mobile.SendMessage(ExpiryMessage);
								}	
							}
							Caster.SendMessage(ExpiryMessage);
							OneTimeSecEvent.SecTimerTick -= SecondTimerTick;
			            } else {
			            	Duration--;
			            }
			            return;
				break;
				}
			} else {
				Caster.SendMessage( "Your spell has no effect on its target and the mana is lost" );	
			}
        	OneTimeSecEvent.SecTimerTick -= SecondTimerTick;
        }
    }
}
