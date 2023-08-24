using System;
using Server;
using System.Collections;
using Server.Misc;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Items;

namespace Server.Misc
{
    class GetPlayerInfo
    {
		public static string GetSkillTitle( Mobile m )
		{
			bool isOriental = Server.Misc.GetPlayerInfo.OrientalPlay( m );
			bool isEvil = Server.Misc.GetPlayerInfo.EvilPlay( m );
			int isBarbaric = Server.Misc.GetPlayerInfo.BarbaricPlay( m );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB.CharacterSkill == 55 )
			{
				return "Titan of Ether";
			}
			else if ( m.SkillsTotal > 0 )
			{
				Skill highest = GetShowingSkill( m, DB );

				if ( highest != null )
				{
					if ( highest.Value < 0.1 )
					{
						return "Village Idiot";
					}
					else
					{
						string skillLevel = null;
						if ( highest.Value < 29.1 ){ skillLevel = "Aspiring"; }
						else { skillLevel = GetSkillLevel( highest ); }

						string skillTitle = highest.Info.Title;

						/* DEFAULT TITLES FOR SKILLS...
						Alchemy				Alchemist
						Anatomy				Biologist
						Animal Lore			Naturalist
						Animal Taming		Tamer
						Archery				Archer
						Arms Lore			Weapon Master
						Begging				Beggar
						Blacksmithy			Blacksmith
						Bowcraft/Fletching	Bowyer
						Bushido				Samurai
						Camping				Explorer
						Carpentry			Carpenter
						Cartography			Cartographer
						Chivalry			Paladin
						Cooking				Chef
						Detecting Hidden	Scout
						Discordance			Demoralizer
						Evaluating Int		Scholar
						Fencing				Fencer
						Fishing				Fisherman
						Focus				Driven
						Forensic Eval		Detective
						Healing				Healer
						Herding				Shepherd
						Hiding				Shade
						Imbuing				Artificer
						Inscription			Scribe
						Item ID				Merchant
						Lockpicking			Infiltrator
						Lumberjacking		Lumberjack
						Mace Fighting		Armsman
						Magery				Mage
						Meditation			Stoic
						Mining				Miner
						Musicianship		Bard
						Mysticism			Mystic
						Necromancy			Necromancer
						Ninjitsu			Ninja
						Parrying			Duelist
						Peacemaking			Pacifier
						Poisoning			Assassin
						Provocation			Rouser
						Remove Trap			Trap Specialist
						Resisting Spells	Warder
						Snooping			Spy
						Spellweaving		Arcanist
						Spirit Speak		Medium
						Stealing			Pickpocket
						Stealth				Rogue
						Swordsmanship		Swordsman
						Tactics				Tactician
						Tailoring			Tailor
						Taste ID			Praegustator
						Throwing			Bladeweaver
						Tinkering			Tinker
						Tracking			Ranger
						Veterinary			Veterinarian
						Wrestling			Wrestler
						*/

						// WIZARD CHANGED SOME SKILL TITLES

						if ( isBarbaric > 0 && 
							( skillTitle.Contains("Alchemist") || 
							skillTitle.Contains("Naturalist") || 
							skillTitle.Contains("Tamer") || 
							skillTitle.Contains("Archer") || 
							skillTitle.Contains("Explorer") || 
							skillTitle.Contains("Paladin") || 
							skillTitle.Contains("Fencer") || 
							skillTitle.Contains("Healer") || 
							skillTitle.Contains("Shepherd") || 
							skillTitle.Contains("Armsman") || 
							skillTitle.Contains("Mage") || 
							skillTitle.Contains("Bard") || 
							skillTitle.Contains("Necromancer") || 
							skillTitle.Contains("Fishing") || 
							skillTitle.Contains("Ranger") || 
							skillTitle.Contains("Duelist") || 
							skillTitle.Contains("Swordsman") || 
							skillTitle.Contains("Weapon Master") || 
							skillTitle.Contains("Tactician") || 
							skillTitle.Contains("Veterinarian") )
						)
						{
							if ( skillTitle.Contains("Alchemist") && m.Female ){ skillTitle = skillTitle.Replace("Alchemist", "Medicine Woman"); }
							else if ( skillTitle.Contains("Alchemist") ){ skillTitle = skillTitle.Replace("Alchemist", "Medicine Man"); }
							else if ( skillTitle.Contains("Naturalist") ){ skillTitle = skillTitle.Replace("Naturalist", "Beastmaster"); }
							else if ( skillTitle.Contains("Tamer") ){ skillTitle = skillTitle.Replace("Tamer", "Beastmaster"); }
							else if ( skillTitle.Contains("Shepherd") ){ skillTitle = skillTitle.Replace("Shepherd", "Beastmaster"); }
							else if ( skillTitle.Contains("Fishing") )
							{
								skillTitle = skillTitle.Replace("Fishing", "Atlantean");
								if ( m.Skills[SkillName.Fishing].Value >= 100 ){ skillTitle = skillTitle.Replace("Atlantean", "Atlantean Captain"); }
							}
							else if ( skillTitle.Contains("Veterinarian") ){ skillTitle = skillTitle.Replace("Veterinarian", "Beastmaster"); }
							else if ( skillTitle.Contains("Explorer") ){ skillTitle = skillTitle.Replace("Explorer", "Wanderer"); }
							else if ( skillTitle.Contains("Paladin") )
							{
								if ( m.Karma < 0 ){ skillTitle = skillTitle.Replace("Paladin", "Death Knight"); }
								else if ( isBarbaric > 1 ){ skillTitle = skillTitle.Replace("Paladin", "Valkyrie"); }
								else { skillTitle = skillTitle.Replace("Paladin", "Chieftain"); }
							}
							else if ( skillTitle.Contains("Tactician") ){ skillTitle = skillTitle.Replace("Tactician", "Warlord"); }
							else if ( skillTitle.Contains("Duelist") ){ skillTitle = skillTitle.Replace("Duelist", "Defender"); }
							else if ( skillTitle.Contains("Necromancer") ){ skillTitle = skillTitle.Replace("Necromancer", "Witch Doctor"); }
							else if ( skillTitle.Contains("Bard") ){ skillTitle = skillTitle.Replace("Bard", "Chronicler"); }
							else if ( skillTitle.Contains("Mage") ){ skillTitle = skillTitle.Replace("Mage", "Shaman"); }
							else if ( skillTitle.Contains("Healer") && m.Female ){ skillTitle = skillTitle.Replace("Healer", "Medicine Woman"); }
							else if ( skillTitle.Contains("Healer") ){ skillTitle = skillTitle.Replace("Healer", "Medicine Man"); }
							else if ( skillTitle.Contains("Archer") && isBarbaric > 1 ){ skillTitle = skillTitle.Replace("Archer", "Amazon"); }
							else if ( skillTitle.Contains("Fencer") && isBarbaric > 1 ){ skillTitle = skillTitle.Replace("Fencer", "Amazon"); }
							else if ( skillTitle.Contains("Armsman") && isBarbaric > 1 ){ skillTitle = skillTitle.Replace("Armsman", "Amazon"); }
							else if ( skillTitle.Contains("Swordsman") && isBarbaric > 1 ){ skillTitle = skillTitle.Replace("Swordsman", "Amazon"); }
							else if ( skillTitle.Contains("Archer") ){ skillTitle = skillTitle.Replace("Archer", "Barbarian"); }
							else if ( skillTitle.Contains("Fencer") ){ skillTitle = skillTitle.Replace("Fencer", "Barbarian"); }
							else if ( skillTitle.Contains("Armsman") ){ skillTitle = skillTitle.Replace("Armsman", "Barbarian"); }
							else if ( skillTitle.Contains("Swordsman") ){ skillTitle = skillTitle.Replace("Swordsman", "Barbarian"); }
							else if ( skillTitle.Contains("Ranger") ){ skillTitle = skillTitle.Replace("Ranger", "Hunter"); }
							else if ( skillTitle.Contains("Weapon Master") ){ skillTitle = skillTitle.Replace("Weapon Master", "Gladiator"); }
						}
						else if ( !isOriental && skillTitle.Contains("Mage") && m.Skills[SkillName.Magery].Base >= 100 && m.Skills[SkillName.Necromancy].Base >= 100 ){ skillTitle = skillTitle.Replace("Mage", "Archmage"); }
						else if ( !isOriental && skillTitle.Contains("Necromancer") && m.Skills[SkillName.Magery].Base >= 100 && m.Skills[SkillName.Necromancy].Base >= 100 ){ skillTitle = skillTitle.Replace("Necromancer", "Archmage"); }

						else if ( ( skillTitle.Contains("Wrestler") ) && isMonk(m) )
						{
							skillTitle = skillTitle.Replace("Wrestler", "Monk");
							if ( m.Skills[SkillName.Magery].Base >= 50 || m.Skills[SkillName.Necromancy].Base >= 50 ){ skillTitle = skillTitle.Replace("Monk", "Mystic"); }
						}
						else if ( ( skillTitle.Contains("Scholar") ) && isSyth(m,false) )
						{
							skillTitle = skillTitle.Replace("Scholar", "Syth");
						}
						else if ( ( skillTitle.Contains("Scholar") ) && isJedi(m,false) )
						{
							skillTitle = skillTitle.Replace("Scholar", "Jedi");
						}
						
					// Jedi  titles.
						else if ( ( skillTitle.Contains("Paladin") ) && isJedi(m,false) ) 
						
			{
				string jedi = "Jedi Youngling";
				
			if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ jedi = "Jedi Padawan";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 0 || m.Karma >= 0 ){ jedi = "Jedi Service Corps";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 0 || m.Karma >= 0 ){ jedi = "Jedi Knight";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 100 || m.Karma >= 5000 ){ jedi = "Jedi Consular";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
				
				skillTitle = skillTitle.Replace("Paladin", jedi);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ jedi = "Jedi Guardian";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ jedi = "Jedi Sentinel";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 120 || m.Karma >= 7500 ){ jedi = "Jedi Master";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
			
			if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ jedi = "Jedi Council Member";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
				if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ jedi = "Master of the Order";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
				if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ jedi = "Grand Master";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 0 || m.Karma >= 0 ){ jedi = "Jedi Troll";}   //   { skillTitle = skillTitle.Replace("Scholar", "Jedi Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", jedi);
			
			
			}
			
			////syth titles   titles are a work in progress. and karma and skills need editing . same for jedi
			
			
						else if ( ( skillTitle.Contains("Paladin") ) && isSyth(m,false) ) 
						
			{
				string syth = "Syth Hopeful";
				
			if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ syth = "Syth Hopefu";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 0 || m.Karma >= 0 ){ syth = "Syth Hopefu";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 0 || m.Karma >= 0 ){ syth = "Syth Hopefu";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 100 || m.Karma >= 5000 ){ syth = "Syth Hopefu";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
				
				skillTitle = skillTitle.Replace("Paladin", syth);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ syth = "Syth Hopefu";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ syth = "Syth Hopefu";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 120 || m.Karma >= 7500 ){ syth = "Syth Master";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
			
			if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ syth = "syth Council Member";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
				if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ syth = "Master of the Order";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
				if ( m.Skills[SkillName.Chivalry].Base >= 10 || m.Karma >= 500 ){ syth = "Grand Master";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
			if ( m.Skills[SkillName.Chivalry].Base >= 0 || m.Karma >= 0 ){ syth = "Syth Troll";}   //   { skillTitle = skillTitle.Replace("Scholar", "syth Knight"); }
			
			skillTitle = skillTitle.Replace("Paladin", syth);
			
			
			}
			
			
			
			
			
			
						
						else if ( skillTitle.Contains("Naturalist") )
						{ 
							if ( m.Karma < 0 ){ skillTitle = skillTitle.Replace("Naturalist", "Industrialist"); }
						}
						else if ( skillTitle.Contains("Beggar") && isJester(m) ){ skillTitle = skillTitle.Replace("Beggar", "Jester"); }
						else if ( skillTitle.Contains("Scholar") && isJester(m) ){ skillTitle = skillTitle.Replace("Scholar", "Joker"); }
						else if ( skillTitle.Contains("Samurai") && m.Karma < 0 ){ skillTitle = skillTitle.Replace("Samurai", "Ronin"); }
						else if ( skillTitle.Contains("Ninja") && m.Karma < 0 ){ skillTitle = skillTitle.Replace("Ninja", "Yakuza"); }
						else if ( skillTitle.Contains("Mage") && isOriental == true ){ skillTitle = skillTitle.Replace("Mage", "Wu Jen"); }
						else if ( skillTitle.Contains("Swordsman") && isOriental == true ){ skillTitle = skillTitle.Replace("Swordsman", "Kensai"); }
						else if ( skillTitle.Contains("Healer") && isOriental == true ){ skillTitle = skillTitle.Replace("Healer", "Shukenja"); }
						else if ( skillTitle.Contains("Necromancer") && isOriental == true ){ skillTitle = skillTitle.Replace("Necromancer", "Fangshi"); }
						else if ( skillTitle.Contains("Alchemist") && isOriental == true ){ skillTitle = skillTitle.Replace("Alchemist", "Waidan"); }
						else if ( skillTitle.Contains("Medium") && isOriental == true ){ skillTitle = skillTitle.Replace("Medium", "Neidan"); }
						else if ( skillTitle.Contains("Archer") && isOriental == true ){ skillTitle = skillTitle.Replace("Archer", "Kyudo"); }
						else if ( skillTitle.Contains("Fencer") && isOriental == true ){ skillTitle = skillTitle.Replace("Fencer", "Yuki Ota"); }
						else if ( skillTitle.Contains("Armsman") && isOriental == true ){ skillTitle = skillTitle.Replace("Armsman", "Mace Fighter"); }
						else if ( skillTitle.Contains("Tactician") && isOriental == true ){ skillTitle = skillTitle.Replace("Tactician", "Sakushi"); }
						else if ( skillTitle.Contains("Paladin") && isOriental == true ){ skillTitle = skillTitle.Replace("Paladin", "Youxia"); }

						else if ( ( skillTitle.Contains("Healer") || skillTitle.Contains("Medium") ) && m.Karma >= 2500 && m.Skills[SkillName.Healing].Base >= 50 && m.Skills[SkillName.SpiritSpeak].Base >= 50 )
						{
							skillTitle = skillTitle.Replace("Medium", "Priest");
							skillTitle = skillTitle.Replace("Healer", "Priest");

							if ( isOriental == true ){ skillTitle = skillTitle.Replace("Priest", "Buddhist"); }
						}

						else if ( skillTitle.Contains("Wrestler") && isOriental == true ){ skillTitle = skillTitle.Replace("Wrestler", "Karateka"); }
						else if ( skillTitle.Contains("Detective") ){ skillTitle = skillTitle.Replace("Detective", "Undertaker"); }
						else if ( skillTitle.Contains("Shade") ){ skillTitle = skillTitle.Replace("Shade", "Skulker"); }
						else if ( skillTitle.Contains("Rogue") ){ skillTitle = skillTitle.Replace("Rogue", "Sneak"); }
						//else if ( skillTitle.Contains("Infiltrator") ){ skillTitle = skillTitle.Replace("Infiltrator", "Lockpicker"); }
						else if ( skillTitle.Contains("Mage") && isEvil == true && m.Body == 0x191 ){ skillTitle = skillTitle.Replace("Mage", "Enchantress"); }
						else if ( skillTitle.Contains("Mage") && isEvil == true ){ skillTitle = skillTitle.Replace("Mage", "Warlock"); }
						else if ( skillTitle.Contains("Mage") ){ skillTitle = skillTitle.Replace("Mage", "Wizard"); }
						else if ( skillTitle.Contains("Paladin") ){ skillTitle = skillTitle.Replace("Paladin", "Knight"); }
						else if ( skillTitle.Contains("Tamer") )
						{ 
							if ( m.Karma < -2500 ){ skillTitle = skillTitle.Replace("Naturalist", "Slaver"); }
							else { skillTitle = skillTitle.Replace("Tamer", "Beastmaster"); }
						}
						else if ( skillTitle.Contains("Pickpocket") ){ skillTitle = skillTitle.Replace("Pickpocket", "Thief"); }
						else if ( skillTitle.Contains("Fisherman") ){ skillTitle = skillTitle.Replace("Fisherman", "Sailor"); }
						else if ( skillTitle.Contains("Stoic") ){ skillTitle = skillTitle.Replace("Stoic", "Meditator"); }
						//else if ( skillTitle.Contains("Armsman") ){ skillTitle = skillTitle.Replace("Armsman", "Mace Fighter"); }
						else if ( skillTitle.Contains("Wrestler") ){ skillTitle = skillTitle.Replace("Wrestler", "Brawler"); }
						//else if ( skillTitle.Contains("Praegustator") ){ skillTitle = skillTitle.Replace("Praegustator", "Food Taster"); }
						else if ( skillTitle.Contains("Warder") ){ skillTitle = skillTitle.Replace("Warder", "Spell Warder"); }

						if ( isBarbaric == 0 )
						{
							if ( skillTitle.Contains("Wizard") && m.Body == 0x191 ){ skillTitle = skillTitle.Replace("Wizard", "Sorceress"); }
							if ( skillTitle.Contains("Necromancer") && m.Body == 0x191 ){ skillTitle = skillTitle.Replace("Necromancer", "Witch"); }
							if ( skillTitle.Contains("Knight") && m.Karma < 0 ){ skillTitle = skillTitle.Replace("Knight", "Death Knight"); }
							if ( skillTitle.Contains("Healer") && m.Karma < 0 ){ skillTitle = skillTitle.Replace("Healer", "Mortician"); }
						}

						if ( skillTitle.Contains("Sailor") && m.Karma < 0 ){ skillTitle = skillTitle.Replace("Sailor", "Pirate"); }
						if ( skillTitle.Contains("Sailor") && m.Skills[SkillName.Fishing].Value >= 100 ){ skillTitle = skillTitle.Replace("Sailor", "Ship Captain"); }
						if ( skillTitle.Contains("Pirate") && m.Skills[SkillName.Fishing].Value >= 100 ){ skillTitle = skillTitle.Replace("Pirate", "Pirate Captain"); }

						if ( m.Female && isBarbaric == 0 && skillTitle.EndsWith( "man" ) )
							skillTitle = skillTitle.Substring( 0, skillTitle.Length - 3 ) + "woman";

						return String.Concat( skillLevel, " ", skillTitle );
					}
				}
			}

			return "Village Idiot";
		}

		public static bool isMonk ( Mobile m )
		{
			int points = 0;

			Spellbook book = Spellbook.FindMystic( m );
			if ( book is MysticSpellbook )
			{
				MysticSpellbook tome = (MysticSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Mystic.MysticSpell.MonkNotIllegal( m ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}

		public static bool isFromSpace( Mobile m )
		{
			if ( m.Skills.Cap >= 40000 )
				return true;

			return false;
		}

		public static bool isSyth ( Mobile m, bool checkSword )
		{
			int points = 0;

			Spellbook book = Spellbook.FindSyth( m );
			if ( book is SythSpellbook )
			{
				SythSpellbook tome = (SythSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Syth.SythSpell.SythNotIllegal( m, checkSword ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}

		public static bool isJedi ( Mobile m, bool checkSword )
		{
			int points = 0;

			Spellbook book = Spellbook.FindJedi( m );
			if ( book is JediSpellbook )
			{
				JediSpellbook tome = (JediSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Jedi.JediSpell.JediNotIllegal( m, checkSword ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}
		

		
		public static bool isJester ( Mobile from )
		{
			int points = 0;

			if ( from is PlayerMobile && from != null )
			{
				foreach( Item i in from.Backpack.FindItemsByType( typeof( BagOfTricks ), true ) )
				{
					if ( i != null ){ points = 1; }
				}

				if ( from.Skills[SkillName.Begging].Value > 10 || from.Skills[SkillName.EvalInt].Value > 10 )
				{
					points++;
				}

				if ( from.FindItemOnLayer( Layer.OuterTorso ) != null )
				{
					Item robe = from.FindItemOnLayer( Layer.OuterTorso );
					if ( robe.ItemID == 0x1f9f || robe.ItemID == 0x1fa0 || robe.ItemID == 0x4C16 || robe.ItemID == 0x4C17 || robe.ItemID == 0x2B6B || robe.ItemID == 0x3162 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.MiddleTorso ) != null )
				{
					Item shirt = from.FindItemOnLayer( Layer.MiddleTorso );
					if ( shirt.ItemID == 0x1f9f || shirt.ItemID == 0x1fa0 || shirt.ItemID == 0x4C16 || shirt.ItemID == 0x4C17 || shirt.ItemID == 0x2B6B || shirt.ItemID == 0x3162 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.Helm ) != null )
				{
					Item hat = from.FindItemOnLayer( Layer.Helm );
					if ( hat.ItemID == 0x171C || hat.ItemID == 0x4C15 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.Shoes ) != null )
				{
					Item feet = from.FindItemOnLayer( Layer.Shoes );
					if ( feet.ItemID == 0x4C27 )
						points++;
				}
			}

			if ( points > 2 )
				return true;

			return false;
		}

		private static Skill GetHighestSkill( Mobile m )
		{
			Skills skills = m.Skills;

			if ( !Core.AOS )
				return skills.Highest;

			Skill highest = m.Skills[SkillName.Wrestling];

			for ( int i = 0; i < m.Skills.Length; ++i )
			{
				Skill check = m.Skills[i];

				if ( highest == null || check.Value > highest.Value )
					highest = check;
				else if ( highest != null && highest.Lock != SkillLock.Up && check.Lock == SkillLock.Up && check.Value == highest.Value )
					highest = check;
			}

			return highest;
		}

		private static string[,] m_Levels = new string[,]
			{
				{ "Neophyte",		"Neophyte",		"Neophyte"		},
				{ "Novice",			"Novice",		"Novice"		},
				{ "Apprentice",		"Apprentice",	"Apprentice"	},
				{ "Journeyman",		"Journeyman",	"Journeyman"	},
				{ "Expert",			"Expert",		"Expert"		},
				{ "Adept",			"Adept",		"Adept"			},
				{ "Master",			"Master",		"Master"		},
				{ "Grandmaster",	"Grandmaster",	"Grandmaster"	},
				{ "Elder",			"Tatsujin",		"Shinobi"		},
				{ "Legendary",		"Kengo",		"Ka-ge"			}
			};

		private static string GetSkillLevel( Skill skill )
		{
			return m_Levels[GetTableIndex( skill ), GetTableType( skill )];
		}

		private static int GetTableType( Skill skill )
		{
			switch ( skill.SkillName )
			{
				default: return 0;
				case SkillName.Bushido: return 1;
				case SkillName.Ninjitsu: return 2;
			}
		}

		private static int GetTableIndex( Skill skill )
		{
			int fp = 0; // Math.Min( skill.BaseFixedPoint, 1200 );

			if ( skill.Value >= 120 ){ fp = 9; }
			else if ( skill.Value >= 110 ){ fp = 8; }
			else if ( skill.Value >= 100 ){ fp = 7; }
			else if ( skill.Value >= 90 ){ fp = 6; }
			else if ( skill.Value >= 80 ){ fp = 5; }
			else if ( skill.Value >= 70 ){ fp = 4; }
			else if ( skill.Value >= 60 ){ fp = 3; }
			else if ( skill.Value >= 50 ){ fp = 2; }
			else if ( skill.Value >= 40 ){ fp = 1; }
			else { fp = 0; }

			return fp;

			// return (fp - 300) / 100;
		}

		private static Skill GetShowingSkill( Mobile m, CharacterDatabase DB )
		{
			Skill skill = GetHighestSkill( m );

			if ( DB != null )
			{
				int NskillShow = DB.CharacterSkill;

				if ( NskillShow > 0 )
				{
					if ( NskillShow == 1 ){ skill = m.Skills[SkillName.Alchemy]; }
					else if ( NskillShow == 2 ){ skill = m.Skills[SkillName.Anatomy]; }
					else if ( NskillShow == 3 ){ skill = m.Skills[SkillName.AnimalLore]; }
					else if ( NskillShow == 4 ){ skill = m.Skills[SkillName.AnimalTaming]; }
					else if ( NskillShow == 5 ){ skill = m.Skills[SkillName.Archery]; }
					else if ( NskillShow == 6 ){ skill = m.Skills[SkillName.ArmsLore]; }
					else if ( NskillShow == 7 ){ skill = m.Skills[SkillName.Begging]; }
					else if ( NskillShow == 8 ){ skill = m.Skills[SkillName.Blacksmith]; }
					else if ( NskillShow == 9 ){ skill = m.Skills[SkillName.Bushido]; }
					else if ( NskillShow == 10 ){ skill = m.Skills[SkillName.Camping]; }
					else if ( NskillShow == 11 ){ skill = m.Skills[SkillName.Carpentry]; }
					else if ( NskillShow == 12 ){ skill = m.Skills[SkillName.Cartography]; }
					else if ( NskillShow == 13 ){ skill = m.Skills[SkillName.Chivalry]; }
					else if ( NskillShow == 14 ){ skill = m.Skills[SkillName.Cooking]; }
					else if ( NskillShow == 15 ){ skill = m.Skills[SkillName.DetectHidden]; }
					else if ( NskillShow == 16 ){ skill = m.Skills[SkillName.Discordance]; }
					else if ( NskillShow == 17 ){ skill = m.Skills[SkillName.EvalInt]; }
					else if ( NskillShow == 18 ){ skill = m.Skills[SkillName.Fencing]; }
					else if ( NskillShow == 19 ){ skill = m.Skills[SkillName.Fishing]; }
					else if ( NskillShow == 20 ){ skill = m.Skills[SkillName.Fletching]; }
					else if ( NskillShow == 21 ){ skill = m.Skills[SkillName.Focus]; }
					else if ( NskillShow == 22 ){ skill = m.Skills[SkillName.Forensics]; }
					else if ( NskillShow == 23 ){ skill = m.Skills[SkillName.Healing]; }
					else if ( NskillShow == 24 ){ skill = m.Skills[SkillName.Herding]; }
					else if ( NskillShow == 25 ){ skill = m.Skills[SkillName.Hiding]; }
					else if ( NskillShow == 26 ){ skill = m.Skills[SkillName.Inscribe]; }
					else if ( NskillShow == 27 ){ skill = m.Skills[SkillName.ItemID]; }
					else if ( NskillShow == 28 ){ skill = m.Skills[SkillName.Lockpicking]; }
					else if ( NskillShow == 29 ){ skill = m.Skills[SkillName.Lumberjacking]; }
					else if ( NskillShow == 30 ){ skill = m.Skills[SkillName.Macing]; }
					else if ( NskillShow == 31 ){ skill = m.Skills[SkillName.Magery]; }
					else if ( NskillShow == 32 ){ skill = m.Skills[SkillName.MagicResist]; }
					else if ( NskillShow == 33 ){ skill = m.Skills[SkillName.Meditation]; }
					else if ( NskillShow == 34 ){ skill = m.Skills[SkillName.Mining]; }
					else if ( NskillShow == 35 ){ skill = m.Skills[SkillName.Musicianship]; }
					else if ( NskillShow == 36 ){ skill = m.Skills[SkillName.Necromancy]; }
					else if ( NskillShow == 37 ){ skill = m.Skills[SkillName.Ninjitsu]; }
					else if ( NskillShow == 38 ){ skill = m.Skills[SkillName.Parry]; }
					else if ( NskillShow == 39 ){ skill = m.Skills[SkillName.Peacemaking]; }
					else if ( NskillShow == 40 ){ skill = m.Skills[SkillName.Poisoning]; }
					else if ( NskillShow == 41 ){ skill = m.Skills[SkillName.Provocation]; }
					else if ( NskillShow == 42 ){ skill = m.Skills[SkillName.RemoveTrap]; }
					else if ( NskillShow == 43 ){ skill = m.Skills[SkillName.Snooping]; }
					else if ( NskillShow == 44 ){ skill = m.Skills[SkillName.SpiritSpeak]; }
					else if ( NskillShow == 45 ){ skill = m.Skills[SkillName.Stealing]; }
					else if ( NskillShow == 46 ){ skill = m.Skills[SkillName.Stealth]; }
					else if ( NskillShow == 47 ){ skill = m.Skills[SkillName.Swords]; }
					else if ( NskillShow == 48 ){ skill = m.Skills[SkillName.Tactics]; }
					else if ( NskillShow == 49 ){ skill = m.Skills[SkillName.Tailoring]; }
					else if ( NskillShow == 50 ){ skill = m.Skills[SkillName.TasteID]; }
					else if ( NskillShow == 51 ){ skill = m.Skills[SkillName.Tinkering]; }
					else if ( NskillShow == 52 ){ skill = m.Skills[SkillName.Tracking]; }
					else if ( NskillShow == 53 ){ skill = m.Skills[SkillName.Veterinary]; }
					else if ( NskillShow == 54 ){ skill = m.Skills[SkillName.Wrestling]; }
					else { skill = GetHighestSkill( m ); }
				}
			}

			return skill;
		}

		public static string GetNPCGuild( Mobile m )
		{
			string GuildTitle = "";

			if ( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				if ( pm.Profession == 1 ){ GuildTitle = " & Fugitive"; }
				else if ( pm.NpcGuild == NpcGuild.MagesGuild ){ GuildTitle = " of the Wizards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.WarriorsGuild ){ GuildTitle = " of the Warriors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ThievesGuild ){ GuildTitle = " of the Thieves Guild"; }
				else if ( pm.NpcGuild == NpcGuild.RangersGuild ){ GuildTitle = " of the Rangers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.HealersGuild ){ GuildTitle = " of the Healers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MinersGuild ){ GuildTitle = " of the Miners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MerchantsGuild ){ GuildTitle = " of the Merchants Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TinkersGuild ){ GuildTitle = " of the Tinkers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TailorsGuild ){ GuildTitle = " of the Tailors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.FishermensGuild ){ GuildTitle = " of the Mariners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BardsGuild ){ GuildTitle = " of the Bards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildTitle = " of the Blacksmiths Guild"; }
				else if ( pm.NpcGuild == NpcGuild.NecromancersGuild ){ GuildTitle = " of the Black Magic Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildTitle = " of the Alchemists Guild"; }
				else if ( pm.NpcGuild == NpcGuild.DruidsGuild ){ GuildTitle = " of the Druids Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ArchersGuild ){ GuildTitle = " of the Archers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CarpentersGuild ){ GuildTitle = " of the Carpenters Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CartographersGuild ){ GuildTitle = " of the Cartographers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.LibrariansGuild ){ GuildTitle = " of the Librarians Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CulinariansGuild ){ GuildTitle = " of the Culinary Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AssassinsGuild ){ GuildTitle = " of the Assassins Guild"; }
			}
			else if ( m is BaseVendor )
			{
				BaseVendor pm = (BaseVendor)m;

				if ( pm.NpcGuild == NpcGuild.MagesGuild ){ GuildTitle = "Wizards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.WarriorsGuild ){ GuildTitle = "Warriors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ThievesGuild ){ GuildTitle = "Thieves Guild"; }
				else if ( pm.NpcGuild == NpcGuild.RangersGuild ){ GuildTitle = "Rangers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.HealersGuild ){ GuildTitle = "Healers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MinersGuild ){ GuildTitle = "Miners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MerchantsGuild ){ GuildTitle = "Merchants Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TinkersGuild ){ GuildTitle = "Tinkers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TailorsGuild ){ GuildTitle = "Tailors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.FishermensGuild ){ GuildTitle = "Mariners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BardsGuild ){ GuildTitle = "Bards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildTitle = "Blacksmiths Guild"; }
				else if ( pm.NpcGuild == NpcGuild.NecromancersGuild ){ GuildTitle = "Black Magic Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildTitle = "Alchemists Guild"; }
				else if ( pm.NpcGuild == NpcGuild.DruidsGuild ){ GuildTitle = "Druids Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ArchersGuild ){ GuildTitle = "Archers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CarpentersGuild ){ GuildTitle = "Carpenters Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CartographersGuild ){ GuildTitle = "Cartographers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.LibrariansGuild ){ GuildTitle = "Librarians Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CulinariansGuild ){ GuildTitle = "Culinary Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AssassinsGuild ){ GuildTitle = "Assassins Guild"; }
			}
			return GuildTitle;
		}

		public static string GetStatusGuild( Mobile m )
		{
			string GuildTitle = "";

			if ( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				if ( pm.Profession == 1 ){ GuildTitle = "The Fugitive"; }
				else if ( pm.NpcGuild == NpcGuild.MagesGuild ){ GuildTitle = "The Wizards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.WarriorsGuild ){ GuildTitle = "The Warriors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ThievesGuild ){ GuildTitle = "The Thieves Guild"; }
				else if ( pm.NpcGuild == NpcGuild.RangersGuild ){ GuildTitle = "The Rangers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.HealersGuild ){ GuildTitle = "The Healers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MinersGuild ){ GuildTitle = "The Miners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MerchantsGuild ){ GuildTitle = "The Merchants Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TinkersGuild ){ GuildTitle = "The Tinkers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TailorsGuild ){ GuildTitle = "The Tailors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.FishermensGuild ){ GuildTitle = "The Mariners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BardsGuild ){ GuildTitle = "The Bards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildTitle = "The Blacksmiths Guild"; }
				else if ( pm.NpcGuild == NpcGuild.NecromancersGuild ){ GuildTitle = "The Black Magic Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildTitle = "The Alchemists Guild"; }
				else if ( pm.NpcGuild == NpcGuild.DruidsGuild ){ GuildTitle = "The Druids Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ArchersGuild ){ GuildTitle = "The Archers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CarpentersGuild ){ GuildTitle = "The Carpenters Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CartographersGuild ){ GuildTitle = "The Cartographers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.LibrariansGuild ){ GuildTitle = "The Librarians Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CulinariansGuild ){ GuildTitle = "The Culinary Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AssassinsGuild ){ GuildTitle = "The Assassins Guild"; }
			}
			return GuildTitle;
		}

		public static int GetPlayerLevel( Mobile m )
		{
			int fame = m.Fame;
				if ( fame > 15000){ fame = 15000; }

			int karma = m.Karma;
				if ( karma < 0 ){ karma = m.Karma * -1; }
				if ( karma > 15000){ karma = 15000; }

			int skills = m.Skills.Total;
				if ( skills > 10000){ skills = 10000; }
				skills = (int)( 1.5 * skills );			// UP TO 15,000

			int stats = m.RawStr + m.RawDex + m.RawInt;
				//if ( stats > 250){ stats = 250; }
				stats = 60 * stats;						// UP TO 15,000

			int level = (int)( ( fame + karma + skills + stats ) / 600 );
				level = (int)( ( level - 10 ) * 1.12 );

			if ( level < 1 ){ level = 1; }
			if ( level > 100 ){ level = 100; }

			return level;
		}

		public static int GetPlayerDifficulty( Mobile m )
		{
			int difficulty = 0;
			int level = GetPlayerLevel( m );

			if ( level >=95 ){ difficulty = 4; }
			else if ( level >=75 ){ difficulty = 3; }
			else if ( level >=50 ){ difficulty = 2; }
			else if ( level >=25 ){ difficulty = 1; }

			return difficulty;
		}

		public static int GetResurrectCost( Mobile m )
		{
			int fame = (int)((double)m.Fame / 3);
				if ( fame > 5000){ fame = 5000; }
			int karma = (int)((double)Math.Abs(m.Karma) / 3);
				if ( karma > 5000){ karma = 5000; }

			int skills = (int)((double)m.Skills.Total / 3);
				if ( skills > 25000){ skills = 25000; }		

			int stats = m.RawStr + m.RawDex + m.RawInt;
				if ( stats > 250){ stats = 250; }
				stats = 50 * stats;					

			int level = (int)( ( fame + karma + skills + stats ) / 1000 );
				level = (int)( ( level - 10 ) * 1.12 );

			if ( level < 1 ){ level = 1; }
			if ( level > 150 ){ level = 150; }

			level = ( level * 100 );

			if ( m.Skills.Total <= 5000 ){ level = 0; }
			if ( ( m.RawStr + m.RawDex + m.RawInt ) <= 100 ){ level = 0; }

			if (m is PlayerMobile)
			{
				if (((PlayerMobile)m).Profession == 1 ){ level = (int)(level * 1.25); }
				if ( !((PlayerMobile)m).Avatar) {level = (int)(level * 2);}
			
			}

			return level;
		}

		public static string GetTodaysDate()
		{
			string sYear = DateTime.UtcNow.Year.ToString();
			string sMonth = DateTime.UtcNow.Month.ToString();
				string sMonthName = "January";
				if ( sMonth == "2" ){ sMonthName = "February"; }
				else if ( sMonth == "3" ){ sMonthName = "March"; }
				else if ( sMonth == "4" ){ sMonthName = "April"; }
				else if ( sMonth == "5" ){ sMonthName = "May"; }
				else if ( sMonth == "6" ){ sMonthName = "June"; }
				else if ( sMonth == "7" ){ sMonthName = "July"; }
				else if ( sMonth == "8" ){ sMonthName = "August"; }
				else if ( sMonth == "9" ){ sMonthName = "September"; }
				else if ( sMonth == "10" ){ sMonthName = "October"; }
				else if ( sMonth == "11" ){ sMonthName = "November"; }
				else if ( sMonth == "12" ){ sMonthName = "December"; }
			string sDay = DateTime.UtcNow.Day.ToString();
			string sHour = DateTime.UtcNow.Hour.ToString();
			string sMinute = DateTime.UtcNow.Minute.ToString();
			string sSecond = DateTime.UtcNow.Second.ToString();

			if ( sHour.Length == 1 ){ sHour = "0" + sHour; }
			if ( sMinute.Length == 1 ){ sMinute = "0" + sMinute; }
			if ( sSecond.Length == 1 ){ sSecond = "0" + sSecond; }

			string sDateString = sMonthName + " " + sDay + ", " + sYear + " at " + sHour + ":" + sMinute;

			return sDateString;
		}

		public static bool LuckyPlayer( int luck, Mobile from )
		{
			if (AdventuresFunctions.IsPuritain((object)from))
				luck = 0;

			if (luck > 8000) //how is this even possible... but it is apparently
				luck = 8000;

			int realluck = Utility.RandomMinMax(1,2000) + (int)( (double)luck /2);	//max of 6000, however likely to be 4000 in most cases maxed

			double balancetweak = 1;
			double balance = (double)AetherGlobe.BalanceLevel; 

			if (from is PlayerMobile )		
			{
				PlayerMobile pm = (PlayerMobile)from;

				//adjusting for player alignment plus or minus 25% effect based on balance
				if (pm.BalanceStatus < 0 || pm.Karma < 0)
					balance = balance - 50000; 
				else if ( pm.BalanceStatus > 0 || pm.Karma > 0 )
					balance = 50000 - balance; 

				balance = balance / 200000; 
				balancetweak += balance;
            }

            double characterTypeModifier = 1.0;
            if (from is PlayerMobile && ((PlayerMobile)from).BalanceStatus == 0) //observer, has penalty 20%
                characterTypeModifier = 0.80;

            realluck = (int)((double)realluck * balancetweak * characterTypeModifier);

            if ( realluck <= 0 ) //sanity check
				return false;

			if ( realluck > MyServerSettings.LuckCap() ) //luck cap is 4k here, so 4k max now
			realluck = MyServerSettings.LuckCap();

			int clover = (int)((double)realluck * 0.05); // RETURNS A MAX OF 200

			if (  clover >= Utility.RandomMinMax( 1, 1000 ) ) //at max its 20%, this is 10% at mid 
			{
				from.SendMessage( "You feel lucky!" );
				return true;
			}

			//note:  even with 0 luck attribute the original amount of realluck allows for a minimum value so it is possible to be lucky without luck attribute

			if (from is PlayerMobile)
				((PlayerMobile)from).CheckRest();
			
			if ( from is PlayerMobile && ((PlayerMobile)from).GetFlag(PlayerFlag.WellRested) && Utility.RandomDouble() < 0.10 ) 
			{
				from.SendMessage( "You feel lucky!" );
				return true;
			}
				
			return false;
		}

		public static bool LuckyKiller( int luck, Mobile from )
		{
			if (LuckyPlayer(luck, from))
				return true;

			return false;
		}

		public static bool EvilPlayer( Mobile m )
		{
			if ( m is BaseCreature )
				m = ((BaseCreature)m).GetMaster();

			if ( m is PlayerMobile )
			{
				if ( m.AccessLevel > AccessLevel.Player )
					return true;

				if ( m.Skills[SkillName.Necromancy].Base >= 50.0 && m.Karma < 0 ) // NECROMANCERS
					return true;

				if ( m.Skills[SkillName.Forensics].Base >= 80.0 && m.Karma < 0 ) // UNDERTAKERS
					return true;

				if ( m.Skills[SkillName.Chivalry].Base >= 50.0 && m.Karma <= -5000 ) // DEATH KNIGHTS
					return true;

				if ( m.Skills[SkillName.EvalInt].Base >= 50.0 && m.Skills[SkillName.Swords].Base >= 50.0 && m.Karma <= -5000 && Server.Misc.GetPlayerInfo.isSyth(m,false) ) // SYTH
					return true;

				if (((PlayerMobile)m).BalanceStatus <0 ) // pledged to evil
					return true;
			}

			return false;
		}
		
		/* make this perhaps?
		
		 public static bool GoodPlayer( Mobile m )
		{
			if ( m is BaseCreature )
				m = ((BaseCreature)m).GetMaster();

			if ( m is PlayerMobile )
			{
				if ( m.AccessLevel > AccessLevel.Player )
					return true;

				//if ( m.Skills[SkillName.Necromancy].Base >= 50.0 && m.Karma < 0 ) // NECROMANCERS
					return true;

			//	if ( m.Skills[SkillName.Forensics].Base >= 80.0 && m.Karma < 0 ) // UNDERTAKERS
					return true;

				//if ( m.Skills[SkillName.Chivalry].Base >= 50.0 && m.Karma <= -5000 ) // DEATH KNIGHTS
					return true;

				if ( m.Skills[SkillName.EvalInt].Base >= 50.0 && m.Skills[SkillName.Swords].Base >= 50.0 && m.Karma >= 0 && Server.Misc.GetPlayerInfo.isJedi(m,false) ) //Jedi
					return true;

				if (((PlayerMobile)m).BalanceStatus >0 ) // pledged to good
					return true;
			}

			return false;
		}
		
		*/
		

		public static int LuckyPlayerArtifacts( int luck, Mobile from)
		{

			if (AdventuresFunctions.IsPuritain((object)from) || luck > 8000)
				luck = 0;

			int realluck = Utility.RandomMinMax(1,2000) + (int)( (double)luck /2);	

			double balancetweak = 1;
			double balance = (double)AetherGlobe.BalanceLevel;

			if (from is PlayerMobile && ((PlayerMobile)from).Avatar)		
			{
				PlayerMobile pm = (PlayerMobile)from;
				if (pm.Karma < 0)
					balance = 100000 - balance;

				balance = (balance - 50000) / 200000;
				balancetweak += balance;

			}
			
			realluck = (int)((double)realluck * balancetweak);

			if ( realluck <= 0 )
				return 0;

			if ( realluck > MyServerSettings.LuckCap() )
				realluck = MyServerSettings.LuckCap();

			int clover = (int)((double)realluck * 0.005); // RETURNS A MAX OF 20

			return clover;
		}

		public static bool OrientalPlay( Mobile m )
		{
			if (m == null)
				return false;

			if ( m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if (DB != null && DB.CharacterOriental == 1 )
					return true;
			}
			else if ( m is BaseCreature )
			{
				Mobile killer = m.LastKiller;
				if ( killer != null && killer is BaseCreature)
				{
					BaseCreature bc_killer = (BaseCreature)killer;
					if(bc_killer.Summoned)
					{
						if(bc_killer.SummonMaster != null)
							killer = bc_killer.SummonMaster;
					}
					else if(bc_killer.Controlled)
					{
						if(bc_killer.ControlMaster != null)
							killer=bc_killer.ControlMaster;
					}
					else if(bc_killer.BardProvoked)
					{
						if(bc_killer.BardMaster != null)
							killer=bc_killer.BardMaster;
					}
				}

				if ( killer != null && killer is PlayerMobile )
				{
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( killer );

					if (DB != null && DB.CharacterOriental == 1 )
						return true;
				}
				else
				{
					Mobile hitter = m.FindMostRecentDamager(true);
					if (hitter != null && hitter is BaseCreature)
					{
						BaseCreature bc_killer = (BaseCreature)hitter;
						if(bc_killer.Summoned)
						{
							if(bc_killer.SummonMaster != null)
								hitter = bc_killer.SummonMaster;
						}
						else if(bc_killer.Controlled)
						{
							if(bc_killer.ControlMaster != null)
								hitter=bc_killer.ControlMaster;
						}
						else if(bc_killer.BardProvoked)
						{
							if(bc_killer.BardMaster != null)
								hitter=bc_killer.BardMaster;
						}
					}

					if ( hitter != null && hitter is PlayerMobile )
					{
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( hitter );

						if ( DB != null && DB.CharacterOriental == 1 )
							return true;
					}
				}
			}

			return false;
		}

		public static int BarbaricPlay( Mobile m )
		{
			if ( m != null && m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB != null && DB.CharacterBarbaric > 0 )
					return DB.CharacterBarbaric;
			}

			return 0;
		}

		public static bool EvilPlay( Mobile m )
		{
			if ( m != null && m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if (DB != null && DB.CharacterEvil == 1 )
					return true;
			}
			else if ( m != null && m is BaseCreature )
			{
				Mobile killer = m.LastKiller;
				if (killer is BaseCreature)
				{
					BaseCreature bc_killer = (BaseCreature)killer;
					if(bc_killer.Summoned)
					{
						if(bc_killer.SummonMaster != null)
							killer = bc_killer.SummonMaster;
					}
					else if(bc_killer.Controlled)
					{
						if(bc_killer.ControlMaster != null)
							killer=bc_killer.ControlMaster;
					}
					else if(bc_killer.BardProvoked)
					{
						if(bc_killer.BardMaster != null)
							killer=bc_killer.BardMaster;
					}
				}

				if ( killer != null && killer is PlayerMobile )
				{
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( killer );

					if ( DB != null && DB.CharacterEvil == 1 )
						return true;
				}
				else
				{
					Mobile hitter = m.FindMostRecentDamager(true);
					if (hitter is BaseCreature)
					{
						BaseCreature bc_killer = (BaseCreature)hitter;
						if(bc_killer.Summoned)
						{
							if(bc_killer.SummonMaster != null)
								hitter = bc_killer.SummonMaster;
						}
						else if(bc_killer.Controlled)
						{
							if(bc_killer.ControlMaster != null)
								hitter=bc_killer.ControlMaster;
						}
						else if(bc_killer.BardProvoked)
						{
							if(bc_killer.BardMaster != null)
								hitter=bc_killer.BardMaster;
						}
					}

					if ( hitter != null && hitter is PlayerMobile )
					{
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( hitter );

						if ( DB != null && DB.CharacterEvil == 1 )
							return true;
					}
				}
			}

			return false;
		}

		public static int GetWealth( Mobile from, int pack )
		{
			int wealth = 0;

			Container bank = from.FindBankNoCreate();
				if ( pack > 0 ){ bank = from.Backpack; }

			if ( bank != null )
			{
				Item[] gold = bank.FindItemsByType( typeof( Gold ) );
				Item[] checks = bank.FindItemsByType( typeof( BankCheck ) );
				Item[] silver = bank.FindItemsByType( typeof( DDSilver ) );
				Item[] copper = bank.FindItemsByType( typeof( DDCopper ) );
				Item[] xormite = bank.FindItemsByType( typeof( DDXormite ) );
				Item[] jewels = bank.FindItemsByType( typeof( DDJewels ) );
				Item[] crystals = bank.FindItemsByType( typeof( Crystals ) );
				Item[] gems = bank.FindItemsByType( typeof( DDGemstones ) );
				Item[] nuggets = bank.FindItemsByType( typeof( DDGoldNuggets ) );

				for ( int i = 0; i < gold.Length; ++i )
					wealth += gold[i].Amount;

				for ( int i = 0; i < checks.Length; ++i )
					wealth += ((BankCheck)checks[i]).Worth;

				for ( int i = 0; i < silver.Length; ++i )
					wealth += (int)Math.Floor((decimal)(silver[i].Amount / 5));

				for ( int i = 0; i < copper.Length; ++i )
					wealth += (int)Math.Floor((decimal)(copper[i].Amount / 10));

				for ( int i = 0; i < xormite.Length; ++i )
					wealth += (xormite[i].Amount)*3;

				for ( int i = 0; i < crystals.Length; ++i )
					wealth += (crystals[i].Amount)*5;

				for ( int i = 0; i < jewels.Length; ++i )
					wealth += (jewels[i].Amount)*2;

				for ( int i = 0; i < gems.Length; ++i )
					wealth += (gems[i].Amount)*2;

				for ( int i = 0; i < nuggets.Length; ++i )
					wealth += (nuggets[i].Amount);
			}

			return wealth;
		}
	}
}
