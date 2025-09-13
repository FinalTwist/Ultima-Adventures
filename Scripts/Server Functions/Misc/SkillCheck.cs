using System;
using Server;
using Server.Mobiles;
using Server.Regions;

using Server.Items;

namespace Server.Misc
{
	public class SkillCheck
	{
		private static readonly bool AntiMacroCode = MyServerSettings.NoMacroing();		// Change this to false to disable anti-macro code
		public static TimeSpan AntiMacroExpire = TimeSpan.FromMinutes( 5.0 ); 			// How long do we remember targets/locations?
		public const int Allowance = 3;													// How many times may we use the same location/target for gain
		private const int LocationSize = 5; 											// The size of each location, make this smaller so players don't have to move as far
		private static bool[] UseAntiMacro = new bool[]
		{
			// true if this skill uses the anti-macro code, false if it does not
			false,// Alchemy = 0,
			false,// Anatomy = 1,
			false,// AnimalLore = 2,
			false,// ItemID = 3,
			false,// ArmsLore = 4,
			false,// Parry = 5,
			false,// Begging = 6,
			false,// Blacksmith = 7,
			false,// Fletching = 8,
			false,// Peacemaking = 9,
			false,// Camping = 10,
			false,// Carpentry = 11,
			false,// Cartography = 12,
			false,// Cooking = 13,
			false,// DetectHidden = 14,
			false,// Discordance = 15,
			false,// EvalInt = 16,
			false,// Healing = 17,
			false,// Fishing = 18,
			false,// Forensics = 19,
			false,// Herding = 20,
			true,// Hiding = 21,
			false,// Provocation = 22,
			false,// Inscribe = 23,
			false,// Lockpicking = 24,
			false,// Magery = 25,
			false,// MagicResist = 26,
			false,// Tactics = 27,
			false,// Snooping = 28,
			true,// Musicianship = 29,
			false,// Poisoning = 30,
			false,// Archery = 31,
			true,// SpiritSpeak = 32,
			false,// Stealing = 33,
			false,// Tailoring = 34,
			false,// AnimalTaming = 35,
			false,// TasteID = 36,
			false,// Tinkering = 37,
			false,// Tracking = 38,
			false,// Veterinary = 39,
			true,// Swords = 40,
			true,// Macing = 41,
			true,// Fencing = 42,
			true,// Wrestling = 43,
			false,// Lumberjacking = 44,
			false,// Mining = 45,
			false,// Meditation = 46,
			false,// Stealth = 47,
			false,// RemoveTrap = 48,
			false,// Necromancy = 49,
			true,// Focus = 50,
			false,// Chivalry = 51
			false,// Bushido = 52
			false,//Ninjitsu = 53
			false // Spellweaving
		};

		public static void Initialize()
		{
			Mobile.SkillCheckLocationHandler = new SkillCheckLocationHandler( Mobile_SkillCheckLocation );
			Mobile.SkillCheckDirectLocationHandler = new SkillCheckDirectLocationHandler( Mobile_SkillCheckDirectLocation );

			Mobile.SkillCheckTargetHandler = new SkillCheckTargetHandler( Mobile_SkillCheckTarget );
			Mobile.SkillCheckDirectTargetHandler = new SkillCheckDirectTargetHandler( Mobile_SkillCheckDirectTarget );
		}

		public static bool Mobile_SkillCheckLocation( Mobile from, SkillName skillName, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			double value = skill.Value;

			if ( value < minSkill )
				return false; // Too difficult
			else if ( value >= maxSkill )
				return true; // No challenge

			double chance = (value - minSkill) / (maxSkill - minSkill);

			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

		public static bool Mobile_SkillCheckDirectLocation( Mobile from, SkillName skillName, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			if ( chance < 0.0 )
				return false; // Too difficult
			else if ( chance >= 1.0 )
				return true; // No challenge

			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

		public static bool CheckPuritainSuccess( Mobile from, Skill skill, object amObj, double chance )
		{
			double first = 0;
			double second = 0;
			double third = 0;

			SkillName skillName = skill.SkillName;
			
			//mental skills rely mainly on mental exhaustion
			if (
				skillName == SkillName.DetectHidden &&
				skillName == SkillName.Meditation &&
				skillName == SkillName.TasteID &&
				skillName == SkillName.ItemID &&
				skillName == SkillName.Anatomy &&
				skillName == SkillName.AnimalLore &&
				skillName == SkillName.Tactics &&
				skillName == SkillName.Tracking &&
				skillName == SkillName.EvalInt &&
				skillName == SkillName.Magery &&
				skillName == SkillName.Necromancy &&
				skillName == SkillName.Chivalry &&
				skillName == SkillName.Inscribe &&
				skillName == SkillName.Alchemy &&
				skillName == SkillName.ArmsLore )
			{
				second = ((PlayerMobile)from).Agility();
				third = ((PlayerMobile)from).Encumbrance();
				first = ((PlayerMobile)from).MentalExhaustion();
			}
				
			//these rely mainly on agility
			else if ( 
				skillName == SkillName.Lockpicking &&
				skillName == SkillName.RemoveTrap &&
				skillName == SkillName.Musicianship &&	
				skillName == SkillName.Musicianship &&
				skillName == SkillName.Hiding &&
				skillName == SkillName.Provocation &&
				skillName == SkillName.Discordance &&
				skillName == SkillName.Peacemaking &&
				skillName == SkillName.Stealing &&
				skillName == SkillName.Stealth &&
				skillName == SkillName.Ninjitsu &&
				skillName == SkillName.Bushido &&
				skillName == SkillName.Tinkering &&
				skillName == SkillName.Herding &&
				skillName == SkillName.Cooking &&
				skillName == SkillName.Fletching &&
				skillName == SkillName.Carpentry &&
				skillName == SkillName.Camping &&
				skillName == SkillName.Tailoring )
			{
				first = ((PlayerMobile)from).Agility();
				second = ((PlayerMobile)from).Encumbrance();
				third = ((PlayerMobile)from).MentalExhaustion();
			}

			//these rely mainly on encumbrance
			else if (
				skillName == SkillName.Mining &&		
				skillName == SkillName.Parry &&
				skillName == SkillName.Swords &&
				skillName == SkillName.Blacksmith &&
				skillName == SkillName.Fencing &&
				skillName == SkillName.Macing &&
				skillName == SkillName.Lumberjacking )
			{
				second = ((PlayerMobile)from).Agility();
				first = ((PlayerMobile)from).Encumbrance();
				third = ((PlayerMobile)from).MentalExhaustion();
			}
				
			//basic principle:  a score of 2 is failure.  We will add the three scores based on a probability of being checked (100%, 50%, 25%) and check that against a random variable from 0 to 2.
			double checkvalue = Utility.RandomDouble() *2;
			
			double score = first;
			if (Utility.RandomBool())
				score += second;
			if (Utility.RandomDouble() < 0.25)
				score += third;

			//lets do the check
			if (score < checkvalue)
				return true;

			//they fail.
			return false;

		}
		
		public static bool CheckSkill( Mobile from, Skill skill, object amObj, double chance )
		{
			if (from is PlayerMobile && AdventuresFunctions.IsPuritain((object)from) )
				return CheckPuritainSuccess(from, skill, amObj, chance);
			
			SkillName skillName = skill.SkillName;

			if ( from.Skills.Cap == 0 )
				return false;

			double gainer = 2;

			if ( from is PlayerMobile )
			{
				if ( IsGuildSkill( from, skillName ) == true ) // WIZARD WANTS GUILD MEMBERS TO GAIN QUICKER
				{
					switch( Utility.RandomMinMax( 0, 5 ) )
					{
						case 0: gainer = 1.80; break;
						case 1: gainer = 1.75; break;
						case 2: gainer = 1.70; break;
						case 3: gainer = 1.65; break;
						case 4: gainer = 1.60; break;
						case 5: gainer = 1.55; break;
					}
				}
			}

			bool success = ( chance >= Utility.RandomDouble() );
			double gc = (double)(skill.Cap - skill.Base) / skill.Cap;
			gc /= gainer;

			gc += ( 1.0 - chance ) * ( success ? 0.5 : (Core.AOS ? 0.0 : 0.2) );
			gc /= gainer;

			gc *= skill.Info.GainFactor;

			if ( gc < 0.01 )
				gc = 0.01;

			if ( from is BaseCreature && ((BaseCreature)from).Controlled )
				gc *= 1.45;

			if (from is PlayerMobile)
			{

				if (((PlayerMobile)from).Avatar)
					gc *= 1.75;
				else 
					gc *= 0.75;
					
				if (from.Hunger <= 3)
					gc /= 1.5;
				else if (from.Hunger <= 10)
					gc /= 1.25;
				else if (from.Hunger <= 20)
					gc *= 1.10;
				else if (from.Hunger <= 25)
					gc *= 1.25;
				else if (from.Hunger <= 30)
					gc *= 1.50;
				else if (from.Hunger <= 40)
					gc *= 2.00;
					
				double gcModifier = 1.00;
				if ( ((PlayerMobile)from).SoulBound) {
					Phylactery phylactery = ((PlayerMobile)from).FindPhylactery();
					if (phylactery != null) {
						gcModifier += phylactery.CalculateSkillGainModifier();
					}
				}
				gc *= gcModifier;

				Region reg = Region.Find( from.Location, from.Map );

				if ( from.Map != null && from.Map == Map.Trammel && from.X <= 2586 && from.X <= 2766 && from.Y >= 1402 && from.Y <= 1595 && from is PlayerMobile && from.Guild != null && !from.Guild.Disbanded && Insensitive.Contains( from.Guild.Name, "the church of justice" ) )
				{					
					gc *= 1.25;
				}

				if ( from is PlayerMobile )
				{
					if ( ((PlayerMobile)from).SoulBound && Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) == "the Basement")
						gc *= 4;
					if ( ((PlayerMobile)from).FastGain > 1 )
						gc *= ((PlayerMobile)from).FastGain;
				}


				if ( reg.IsPartOf( typeof( BardDungeonRegion ) ) || reg.IsPartOf( typeof( DungeonRegion ) ) )
				{
					double bonus = (Misc.MyServerSettings.GetDifficultyLevel( from.Location, from.Map ) / 5);

					if (
						skillName == SkillName.DetectHidden &&
						skillName == SkillName.Meditation &&
						skillName == SkillName.TasteID &&
						skillName == SkillName.ItemID &&
						skillName == SkillName.Anatomy &&
						skillName == SkillName.Lockpicking &&
						skillName == SkillName.RemoveTrap &&
						skillName == SkillName.Tactics &&
						skillName == SkillName.Musicianship &&
						skillName == SkillName.AnimalLore &&
						skillName == SkillName.Musicianship &&
						skillName == SkillName.Mining &&
						skillName == SkillName.Tracking &&
						skillName == SkillName.Parry &&
						skillName == SkillName.EvalInt &&
						skillName == SkillName.Hiding
					)
						bonus = 1+ (bonus/3);					 

					else if (
						skillName == SkillName.Swords &&
						skillName == SkillName.Fencing &&
						skillName == SkillName.Macing &&
						skillName == SkillName.Magery &&
						skillName == SkillName.Necromancy &&
						skillName == SkillName.Provocation &&
						skillName == SkillName.Discordance &&
						skillName == SkillName.Peacemaking &&
						skillName == SkillName.Chivalry &&
						skillName == SkillName.Stealing &&
						skillName == SkillName.Stealth &&
						skillName == SkillName.Ninjitsu &&
						skillName == SkillName.Bushido &&
						skillName == SkillName.Archery
					)
						bonus = 1+ bonus;	

					else /*if (
						skillName == SkillName.Inscribe &&
						skillName == SkillName.Alchemy &&
						skillName == SkillName.Blacksmith &&
						skillName == SkillName.Tinkering &&
						skillName == SkillName.Herding &&
						skillName == SkillName.Cooking &&
						skillName == SkillName.ArmsLore &&
						skillName == SkillName.Fletching &&
						skillName == SkillName.Carpentry &&
						skillName == SkillName.Lumberjacking &&
						skillName == SkillName.Camping &&
						skillName == SkillName.Tailoring
					)*/
						bonus = 1;

					 if (bonus >= 1)
					 	gc *= bonus;
				}
			}

			if ( from.Alive && ( ( gc >= Utility.RandomDouble() && AllowGain( from, skill, amObj ) ) || skill.Base < 10.0 ) )
			{
				// CAN ONLY GAIN FISHING SKILL ON A BOAT AFTER REACHING 60
				if ( Worlds.IsOnBoat( from ) == false && skill.SkillName == SkillName.Fishing && from.Skills[SkillName.Fishing].Base >= 60 )
				{
					from.SendMessage("You will only get better at fishing if you do it from a boat.");
				}
				else
				{
					Gain( from, skill );
				}
			}

			if (!success && from.Player && ((PlayerMobile)from).THC > 0)
			{
				PlayerMobile pm = (PlayerMobile)from;
				if (Utility.RandomDouble() < (0.08 * ((double)pm.THC / 60)))
				{
					success = true;
					from.SendMessage("Your skill and focus seems a little ... higher.");
				}
			}

			return success;
		}

		public static bool Mobile_SkillCheckTarget( Mobile from, SkillName skillName, object target, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			double value = skill.Value;

			if ( value < minSkill )
				return false; // Too difficult
			else if ( value >= maxSkill )
				return true; // No challenge

			double chance = (value - minSkill) / (maxSkill - minSkill);

			return CheckSkill( from, skill, target, chance );
		}

		public static bool Mobile_SkillCheckDirectTarget( Mobile from, SkillName skillName, object target, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			if ( chance < 0.0 )
				return false; // Too difficult
			else if ( chance >= 1.0 )
				return true; // No challenge

			return CheckSkill( from, skill, target, chance );
		}

		public static bool IsGuildSkill( Mobile from, SkillName skillName )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( pm.NpcGuild == NpcGuild.MagesGuild )
			{
				if ( skillName == SkillName.EvalInt ){ return true; }
				else if ( skillName == SkillName.Magery ){ return true; }
				else if ( skillName == SkillName.Meditation ){ return true; }
				else if ( skillName == SkillName.Inscribe ){ return true; }
				else if ( skillName == SkillName.Alchemy ){ return true; }				
			}
			else if ( pm.NpcGuild == NpcGuild.WarriorsGuild )
			{
				if ( skillName == SkillName.Fencing ){ return true; }
				else if ( skillName == SkillName.Macing ){ return true; }
				else if ( skillName == SkillName.Parry ){ return true; }
				else if ( skillName == SkillName.Swords ){ return true; }
				else if ( skillName == SkillName.Tactics ){ return true; }
				else if ( skillName == SkillName.Healing ){ return true; }
				else if ( skillName == SkillName.Anatomy ){ return true; }
				else if ( skillName == SkillName.Chivalry ){ return true; }
			}
			else if ( pm.NpcGuild == NpcGuild.ThievesGuild )
			{
				if ( skillName == SkillName.Hiding ){ return true; }
				else if ( skillName == SkillName.Lockpicking ){ return true; }
				else if ( skillName == SkillName.Snooping ){ return true; }
				else if ( skillName == SkillName.Stealing ){ return true; }
				else if ( skillName == SkillName.Stealth ){ return true; }
				else if ( skillName == SkillName.DetectHidden ){ return true; }
				else if ( skillName == SkillName.Ninjitsu ){ return true; }
			}
			else if ( pm.NpcGuild == NpcGuild.BardsGuild )
			{
				if ( skillName == SkillName.Discordance ){ return true; }
				else if ( skillName == SkillName.Musicianship ){ return true; }
				else if ( skillName == SkillName.Peacemaking ){ return true; }
				else if ( skillName == SkillName.Provocation ){ return true; }
			}
			else if ( pm.NpcGuild == NpcGuild.BlacksmithsGuild )
			{
				if ( skillName == SkillName.Blacksmith ){ return true; }
				else if ( skillName == SkillName.ArmsLore ){ return true; }
				else if ( skillName == SkillName.Mining ){ return true; }
				//else if ( skillName == SkillName.Lumberjacking ){ return true; }
				//else if ( skillName == SkillName.Fletching ){ return true; }
				//else if ( skillName == SkillName.Carpentry ){ return true; }
				else if ( skillName == SkillName.Tinkering ){ return true; }
			}
			else if ( pm.NpcGuild == NpcGuild.NecromancersGuild )
			{
				if ( skillName == SkillName.Forensics ){ return true; }
				else if ( skillName == SkillName.Necromancy ){ return true; }
				else if ( skillName == SkillName.SpiritSpeak ){ return true; }
				else if ( skillName == SkillName.Alchemy ){ return true; }
				else if ( skillName == SkillName.Inscribe ){ return true; }				
			}
			else if ( pm.NpcGuild == NpcGuild.DruidsGuild )
			{
				if ( skillName == SkillName.AnimalLore ){ return true; }
				else if ( skillName == SkillName.AnimalTaming ){ return true; }
				else if ( skillName == SkillName.Herding ){ return true; }
				else if ( skillName == SkillName.Veterinary ){ return true; }
				else if ( skillName == SkillName.Cooking ){ return true; }
				else if ( skillName == SkillName.Camping ){ return true; }
				else if ( skillName == SkillName.Tracking ){ return true; }				
			}
			else if ( pm.NpcGuild == NpcGuild.CartographersGuild )
				{
				if ( skillName == SkillName.Cartography ){ return true; }
				else if ( skillName == SkillName.RemoveTrap ){ return true; }
				else if ( skillName == SkillName.Lockpicking ){ return true; }
				//else if ( skillName == SkillName.Mining ){ return true; }
				else if ( skillName == SkillName.Fishing ){ return true; }		
			}
			else if ( pm.NpcGuild == NpcGuild.AssassinsGuild )
			{
				if ( skillName == SkillName.Fencing ){ return true; }
				else if ( skillName == SkillName.Hiding ){ return true; }
				else if ( skillName == SkillName.Poisoning ){ return true; }
				else if ( skillName == SkillName.Stealth ){ return true; }
				else if ( skillName == SkillName.Archery ){ return true; }				
				else if ( skillName == SkillName.Alchemy ){ return true; }
				else if ( skillName == SkillName.Bushido ){ return true; }
				}
			else if ( pm.NpcGuild == NpcGuild.MerchantsGuild )
			{
				if ( skillName == SkillName.ItemID ){ return true; }
				else if ( skillName == SkillName.ArmsLore ){ return true; }
				else if ( skillName == SkillName.TasteID ){ return true; }
			}				
			else if ( pm.NpcGuild == NpcGuild.TailorsGuild )
			{
				if ( skillName == SkillName.Tailoring ){ return true; }
				//else if ( skillName == SkillName.Mining ){ return true; }
				//else if ( skillName == SkillName.Lumberjacking ){ return true; }
				//else if ( skillName == SkillName.Fletching ){ return true; }
				//else if ( skillName == SkillName.Carpentry ){ return true; }
				else if ( skillName == SkillName.Tinkering ){ return true; }				
			}				
			else if ( pm.NpcGuild == NpcGuild.CarpentersGuild )
			{
				if ( skillName == SkillName.Carpentry ){ return true; }
				else if ( skillName == SkillName.Lumberjacking ){ return true; }
				//else if ( skillName == SkillName.Blacksmith ){ return true; }
				//else if ( skillName == SkillName.Mining ){ return true; }
				else if ( skillName == SkillName.Fletching ){ return true; }
				else if ( skillName == SkillName.Tinkering ){ return true; }
				//else if ( skillName == SkillName.Tailoring ){ return true; }
			}				
			else if ( pm.NpcGuild == NpcGuild.CulinariansGuild )
			{
				if ( skillName == SkillName.Cooking ){ return true; }
				else if ( skillName == SkillName.TasteID ){ return true; }
				else if ( skillName == SkillName.Tinkering ){ return true; }
			}				
			else if ( pm.NpcGuild == NpcGuild.TinkersGuild )
			{
				if ( skillName == SkillName.Tinkering ){ return true; }
				//else if ( skillName == SkillName.Mining ){ return true; }
				//else if ( skillName == SkillName.Lumberjacking ){ return true; }
				else if ( skillName == SkillName.Fletching ){ return true; }
				else if ( skillName == SkillName.Carpentry ){ return true; }
				else if ( skillName == SkillName.Tailoring ){ return true; }
				
			}				
			else if ( pm.NpcGuild == NpcGuild.ArchersGuild )
			{
				if ( skillName == SkillName.Archery ){ return true; }
				else if ( skillName == SkillName.Fletching ){ return true; }
				else if ( skillName == SkillName.Tactics ){ return true; }
				else if ( skillName == SkillName.Chivalry ){ return true; }
			}				
			else if ( pm.NpcGuild == NpcGuild.AlchemistsGuild )
			{
				if ( skillName == SkillName.Alchemy ){ return true; }
				else if ( skillName == SkillName.Cooking ){ return true; }
				else if ( skillName == SkillName.TasteID ){ return true; }
			}			
			else if ( pm.NpcGuild == NpcGuild.RangersGuild )
			{
				if ( skillName == SkillName.Camping ){ return true; }
				else if ( skillName == SkillName.Tracking ){ return true; }
			}
			else if ( pm.NpcGuild == NpcGuild.LibrariansGuild )
			{
				if ( skillName == SkillName.ItemID ){ return true; }
				else if ( skillName == SkillName.Inscribe ){ return true; }
			}
			else if ( pm.NpcGuild == NpcGuild.FishermensGuild )
			{
				if ( skillName == SkillName.Fishing ){ return true; }
			}
			else if ( pm.NpcGuild == NpcGuild.HealersGuild )
			{
				if ( skillName == SkillName.Anatomy ){ return true; }
				else if ( skillName == SkillName.Healing ){ return true; }
				else if ( skillName == SkillName.Veterinary ){ return true; }
			}
			else if ( pm.NpcGuild == NpcGuild.MinersGuild )
			{
				if ( skillName == SkillName.Mining ){ return true; }
				else if ( skillName == SkillName.ArmsLore ){ return true; }
				else if ( skillName == SkillName.Blacksmith ){ return true; }
				//else if ( skillName == SkillName.Lumberjacking ){ return true; }
				//else if ( skillName == SkillName.Fletching ){ return true; }
				//else if ( skillName == SkillName.Carpentry ){ return true; }
				else if ( skillName == SkillName.Tinkering ){ return true; }
			}			
			return false;
		}




		private static bool AllowGain( Mobile from, Skill skill, object obj )
		{

			if ( AntiMacroCode && from is PlayerMobile && UseAntiMacro[skill.Info.SkillID] )
				return ((PlayerMobile)from).AntiMacroCheck( skill, obj );
			else
				return true;
		}

		public enum Stat { Str, Dex, Int }

		public static void Gain( Mobile from, Skill skill )
		{
			if ( from.Region.IsPartOf( typeof( Regions.Jail ) ) )
				return;

			if ( from is BaseCreature && ((BaseCreature)from).IsDeadPet )
				return;

			if ( skill.SkillName == SkillName.Focus && from is BaseCreature )
				return;

			if ( skill.Base < skill.Cap && skill.Lock == SkillLock.Up )
			{
				int toGain = 1;
				int harder = 1;


				if (AdventuresFunctions.IsPuritain((object)from))
				{

					if ( skill.Base <= 25.0 )
						toGain = Utility.RandomMinMax(4, 6);

					else if ( skill.Base <= 55.0 )
						toGain = Utility.RandomMinMax(3, 4);

					else if ( skill.Base <= 70.0 )
						toGain = Utility.RandomMinMax(1, 3);

					else if ( skill.Base <= 80.0 )
						toGain = 1;

					else if ( skill.Base <= 85.0 )
						{
							harder = Utility.RandomMinMax( 0, 2 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
					else if ( skill.Base <= 90.0 )
						{
							harder = Utility.RandomMinMax( 0, 3 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
					else if ( skill.Base <= 95.0 )
						{
							harder = Utility.RandomMinMax( 0, 4 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
					else if ( skill.Base <= 100.0 )
						{
							harder = Utility.RandomMinMax( 0, 5 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
					else if ( skill.Base <= 105 )
						{
							harder = Utility.RandomMinMax( 0, 6 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}	

					else if ( skill.Base <= 110 )
						{
							harder = Utility.RandomMinMax( 0, 7 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}	

					else if ( skill.Base >= 110.1 )
						{
							harder = Utility.RandomMinMax( 0, 8 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}	

					if ( toGain > 0 && (skill.SkillName == SkillName.Focus || skill.SkillName == SkillName.Meditation))
						{
							harder = Utility.RandomMinMax( 0, 2 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
				}
				else
				{

					if ( skill.Base <= 25.0 )
						toGain = Utility.RandomMinMax(3, 5);

					else if ( skill.Base <= 55.0 )
						toGain = Utility.RandomMinMax(2, 3);

					else if ( skill.Base <= 70.0 )
						toGain = Utility.RandomMinMax(1, 2);

					else if ( skill.Base <= 80.0 )
						toGain = 1;

					else if ( skill.Base <= 85.0 )
						{
							harder = Utility.RandomMinMax( 0, 1 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
					else if ( skill.Base <= 90.0 )
						{
							harder = Utility.RandomMinMax( 0, 2 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
					else if ( skill.Base <= 95.0 )
						{
							harder = Utility.RandomMinMax( 0, 3 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
					else if ( skill.Base <= 100.0 )
						{
							harder = Utility.RandomMinMax( 0, 4 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
					else if ( skill.Base <= 105 )
						{
							harder = Utility.RandomMinMax( 0, 5 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}	

					else if ( skill.Base <= 110 )
						{
							harder = Utility.RandomMinMax( 0, 6 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}	

					else if ( skill.Base >= 110.1 )
						{
							harder = Utility.RandomMinMax( 0, 7 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}	

					if ( toGain > 0 && (skill.SkillName == SkillName.Focus || skill.SkillName == SkillName.Meditation))
						{
							harder = Utility.RandomMinMax( 0, 2 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
				}				


				Skills skills = from.Skills;

				if ( from.Player && ( skills.Total / skills.Cap ) >= Utility.RandomDouble() )//( skills.Total >= skills.Cap )
				{
					for ( int i = 0; i < skills.Length; ++i )
					{
						Skill toLower = skills[i];

						if ( toLower != skill && toLower.Lock == SkillLock.Down && toLower.BaseFixedPoint >= toGain )
						{
							toLower.BaseFixedPoint -= toGain;
							break;
						}
					}
				}

				#region Scroll of Alacrity
				PlayerMobile pm = from as PlayerMobile;

				if ( from is PlayerMobile )
					if (pm != null && skill.SkillName == pm.AcceleratedSkill && pm.AcceleratedStart > DateTime.UtcNow)
					toGain *= Utility.RandomMinMax(2, 4);
					#endregion

				if (from is PlayerMobile && skill.BaseFixedPoint < 1000 && (toGain + skill.BaseFixedPoint >= 1000))
					LoggingFunctions.LogGM( from, skill );

				if ( !from.Player || (skills.Total + toGain) <= skills.Cap )
				{
					skill.BaseFixedPoint += toGain;

					if ( skill.SkillName == SkillName.Focus && Utility.RandomMinMax( 1, 20 ) == 1 )
						{ Server.Gumps.SkillListingGump.RefreshSkillList( from ); }

					else if ( skill.SkillName == SkillName.Meditation && Utility.RandomMinMax( 1, 20 ) == 1 )
						{ Server.Gumps.SkillListingGump.RefreshSkillList( from ); }

					else
						{ Server.Gumps.SkillListingGump.RefreshSkillList( from ); }
				}
			}

			if ( skill.Lock == SkillLock.Up )
			{
				SkillInfo info = skill.Info;

				if ( from.StrLock == StatLockType.Up && (info.StrGain / 33.3) > Utility.RandomDouble() )
					GainStat( from, Stat.Str );
				else if ( from.DexLock == StatLockType.Up && (info.DexGain / 33.3) > Utility.RandomDouble() )
					GainStat( from, Stat.Dex );
				else if ( from.IntLock == StatLockType.Up && (info.IntGain / 33.3) > Utility.RandomDouble() )
					GainStat( from, Stat.Int );
			}
		}

		public static bool CanLower( Mobile from, Stat stat )
		{
			switch ( stat )
			{
				case Stat.Str: return ( from.StrLock == StatLockType.Down && from.RawStr > 10 );
				case Stat.Dex: return ( from.DexLock == StatLockType.Down && from.RawDex > 10 );
				case Stat.Int: return ( from.IntLock == StatLockType.Down && from.RawInt > 10 );
			}

			return false;
		}

		public static bool CanRaise( Mobile from, Stat stat )
		{
			if ( !(from is BaseCreature && ((BaseCreature)from).Controlled) )
			{
				if ( from.RawStatTotal >= from.StatCap )
					return false;
			}

			if ( from.StatCap > 275 )
			{
				switch ( stat )
				{
					case Stat.Str: return ( from.StrLock == StatLockType.Up && from.RawStr < 175 );
					case Stat.Dex: return ( from.DexLock == StatLockType.Up && from.RawDex < 175 );
					case Stat.Int: return ( from.IntLock == StatLockType.Up && from.RawInt < 175 );
				}
			}
			else
			{
				switch ( stat )
				{
					case Stat.Str: return ( from.StrLock == StatLockType.Up && from.RawStr < 150 );
					case Stat.Dex: return ( from.DexLock == StatLockType.Up && from.RawDex < 150 );
					case Stat.Int: return ( from.IntLock == StatLockType.Up && from.RawInt < 150 );
				}
			}

			return false;
		}

		public static void IncreaseStat( Mobile from, Stat stat, bool atrophy )
		{
			atrophy = atrophy || (from.RawStatTotal >= from.StatCap);

			switch ( stat )
			{
				case Stat.Str:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Dex ) && (from.RawDex < from.RawInt || !CanLower( from, Stat.Int )) )
							--from.RawDex;
						else if ( CanLower( from, Stat.Int ) )
							--from.RawInt;
					}

					if ( CanRaise( from, Stat.Str ) )
						++from.RawStr;

					break;
				}
				case Stat.Dex:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Str ) && (from.RawStr < from.RawInt || !CanLower( from, Stat.Int )) )
							--from.RawStr;
						else if ( CanLower( from, Stat.Int ) )
							--from.RawInt;
					}

					if ( CanRaise( from, Stat.Dex ) )
						++from.RawDex;

					break;
				}
				case Stat.Int:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Str ) && (from.RawStr < from.RawDex || !CanLower( from, Stat.Dex )) )
							--from.RawStr;
						else if ( CanLower( from, Stat.Dex ) )
							--from.RawDex;
					}

					if ( CanRaise( from, Stat.Int ) )
						++from.RawInt;

					break;
				}
			}
		}

		private static double m_DefaultStatGainDelay = 15.0;
		private static TimeSpan m_StatGainDelay = TimeSpan.FromMinutes(m_DefaultStatGainDelay);
		private static TimeSpan m_PetStatGainDelay = TimeSpan.FromMinutes( 5.0 );

		public static void GainStat( Mobile from, Stat stat )
		{
			if (from is PlayerMobile)
			{
				if ( ((PlayerMobile)from).SoulBound) {
					Phylactery phylactery = ((PlayerMobile)from).FindPhylactery();
					if (phylactery != null) {
						m_StatGainDelay = TimeSpan.FromMinutes(phylactery.CalculateStatGainModifier(m_DefaultStatGainDelay));
					}
				}
				if ( ((PlayerMobile)from).FastGain > 2)
					m_StatGainDelay = TimeSpan.FromMinutes( ( m_DefaultStatGainDelay / 2 ));
			}
		
			switch( stat )
			{
				case Stat.Str:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastStrGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}
					else if( (from.LastStrGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastStrGain = DateTime.UtcNow;
					break;
				}
				case Stat.Dex:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastDexGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}
					else if( (from.LastDexGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastDexGain = DateTime.UtcNow;
					break;
				}
				case Stat.Int:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastIntGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}

					else if( (from.LastIntGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastIntGain = DateTime.UtcNow;
					break;
				}
			}

			bool atrophy = ( (from.RawStatTotal / (double)from.StatCap) >= Utility.RandomDouble() );

			IncreaseStat( from, stat, atrophy );
		}
	}
}
