using System;
using Server;
using System.Collections;
using Server.Misc;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using Server.Targeting;
using System.Collections.Generic;
using Server.Items;
using Server.Spells.Fifth;
using Knives.TownHouses;

namespace Server.Misc
{
    class MyServerSettings
    {
		public static string ServerName()
		{
			// THE NAME OF YOUR SERVER
			// DON'T MAKE THE NAME LONGER THAN THE CHARACTERS BELOW OR IT GETS CUT OFF
			return "Ultima Adventures";
		}

		public static string FilesPath()
		{
            // THE SERVER REQUIRES SOME CLIENT FILES TO FUNCTION
            // THESE REQUIRED FILES SHOULD BE INCLUDED IN THE "Files" FOLDER OF THE PACKAGE
            // SET THE BELOW PATH TO WHERE THIS FOLDER IS LOCATED

            return @"C:\Ultima-Adventures\Files";
        }

		public static bool AllowSaveFunction()
		{
			// THIS ALLOWS PLAYERS TO SAVE THE SERVER WITH THE [save COMMAND AND THAT COMMAND IS THEN LISTED IN THEIR HELP SECTION
			// WARNING: MUTLTIPLE SAVE COMMANDS, HAPPENING AT ONCE, CAN CORRUPT THE SAVED GAME FILES
			// THIS IS RECOMMENDED FOR SINGLE PLAYER GAMES...WHERE YOU PERHAPS SET THE ServerSaveMinutes TO A REALLY HIGH VALUE TO AVOID INTERFERENCE
			// IF YOU PLAY THIS GAME ON A PORTABLE USB DRIVE, SERVER SAVE TIMES CAN BE UP TO 60 SECONDS SO TURNING OFF THE AUTO SAVE
			// AND USING THIS OPTION CAN AVOID THE SUDDEN WAIT TIMES FOR SAVES IN THOSE SCENARIOS AS THE PLAYER CAN CHOOSE WHEN TO DO IT
			return false;
		}

		public static bool ClientVersion()
		{
			return true;
		}

		public static double ServerSaveMinutes() // HOW MANY MINUTES BETWEEN AUTOMATIC SERVER SAVES
		{
			return 20.0;
		}

		public static bool SaveOnCharacterLogout()
		{
			// THIS IS HELPFUL IN SINGLE PLAYER MODE, WHERE THE GAME WILL SAVE AS SOON AS YOU LOG OUT YOUR CHARACTER
			return false;
		}

		public static bool PersistentBlackjack()
		{
			// IF YOU HAVE CUSTOM SETTINGS FOR YOUR BLACKJACK TABLES
			// THEN SETTING THIS TO true WILL KEEP YOUR TABLES IN PLACE
			// EVEN AFTER YOU DO A [buildworld COMMAND
			return true;
		}

		public static bool OpenBasements()
		{
			return true;
		}

		public static int FloorTrapTrigger()
		{
			// THERE ARE MANY HIDDEN TRAPS ON THE FLOOR, BUT THE PERCENT CHANCE
			// IS SET BELOW THAT THEY WILL TRIGGER WHEN WALKED OVER BY PLAYERS
			// 20% IS THE DEFAULT...WHERE 0 IS NEVER AND 100 IS ALWAYS
			return 35;
		}

		public static int GetUnidentifiedChance()
		{
			// CHANCE THAT ITEMS ARE UNIDENTIFIED
			// IF YOU SET THIS VERY LOW, THEN ITEM IDENTIFICATION STARTS TO BECOME A USELESS SKILL
			return 40;
		}

		public static bool NoMacroing()
		{
			// SOME SKILLS ARE MEANT TO BE WORKED ACTIVELY BY THE PLAYER
			// THIS SETS THE TONE FOR GAME DIFFICULTY AND CHARACTER DEVELOPMENT
			// SETTING THE BELOW TO FALSE WILL IGNORE THIS FEATURE OF THE GAME
			return false;
		}

		public static int GetTimeBetweenQuests()
		{
			return 1; // MINUTES
		}

		public static int GetTimeBetweenArtifactQuests()
		{
			return 2880; // MINUTES
		}

		public static int GetGoldCutRate() 
		{
			return GetGoldCutRate(null, null);
		}

		public static int GetGoldCutRate( Mobile m, Item i )
		{
			// THIS AFFECTS MONEY ELEMENTS SUCH AS...
			// MONSTER DROPS
			// CHEST DROPS
			// CARGO
			// MUSEUM SEARCHES
			// SHOPPE PROFITS
			// SOME QUESTS


				bool evilloc = false;

				if (m != null && m.Map != null)
				{
					if ( m.Map == Map.Ilshenar && m.X <= 1007 && m.Y <= 1280) // darkmoor
						evilloc = true;

					Region reg = Region.Find( m.Location, m.Map );
					if ( reg.IsPartOf( typeof( NecromancerRegion ) ) || reg.IsPartOf( typeof(UmbraRegion) ) || (Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "The Pit") )
						evilloc = true;
				}

				if (i != null && i.Map != null)
				{
					if ( i.Map == Map.Ilshenar && i.X <= 1007 && i.Y <= 1280) // darkmoor
						evilloc = true;
					Region reg = Region.Find( i.Location, i.Map );
					if ( reg.IsPartOf( typeof( NecromancerRegion ) ) || reg.IsPartOf( typeof(UmbraRegion) ) || (Server.Misc.Worlds.GetRegionName( i.Map, i.Location ) == "The Pit") )
						evilloc = true;	
				}
				
				int cut = 50;

				if ( evilloc )
				{
						if ((AetherGlobe.BalanceLevel / 100000) >= 0.85)
							cut = 55;			
						if ((AetherGlobe.BalanceLevel / 100000) >= 0.55)
							cut = 44;
						else if ((AetherGlobe.BalanceLevel / 100000) >= 0.4)
							cut = 34;
						else if ((AetherGlobe.BalanceLevel / 100000) >= 0.20)
							cut = 27;		
						else if ((AetherGlobe.BalanceLevel / 100000) >= 0.00)
							cut = 20;
				}	
				else
				{
						if ((AetherGlobe.BalanceLevel / 100000) >= 0.85)
							cut = 20;			
						if ((AetherGlobe.BalanceLevel / 100000) >= 0.55)
							cut = 27;
						else if ((AetherGlobe.BalanceLevel / 100000) >= 0.4)
							cut = 34;
						else if ((AetherGlobe.BalanceLevel / 100000) >= 0.20)
							cut = 44;		
						else if ((AetherGlobe.BalanceLevel / 100000) >= 0.00)
							cut = 55;						
				}	

				if (AdventuresFunctions.IsPuritain((object)m))
					cut /= 2;		
			
			return cut;

		}

		public static bool CreaturesDetectHidden()
		{
			// IF TRUE, ALL CREATURES WILL HAVE SOME FORM OF DETECT HIDDEN SKILL
			// THAT IS BASED ON THEIR CREATURE LEVEL. THIS MAKES THE STEALTHY
			// THIEVES HAVE MORE OF A CHALLENGE WHEN THEY ARE TOMB RAIDING
			return true;
		}

		public static bool NoMountsInCertainRegions()
		{
			// PLAYER MOUNTS GET STABLED WHEN THEY GO IN CERTAIN AREAS LIKE DUNGEONS OR CAVES
			// THEY WILL REMOUNT THEM WHEN THEY LEAVE THESE AREAS
			// SET TO false IF YOU DO NOT WANT TO LIMIT WHERE THEY TAKE MOUNTS
			// KEEP IN MIND THAT HAVING NO MOUNTS IN DUNGEONS DOES INCREASE THE DIFFICULTY
			return true;
		}

		public static bool AllowAlienChoice()
		{
			// THERE IS A PLAY STYLE WHERE ONE CAN CHOOSE TO ENTER A TRANSPORTER AND BE A CHARACTER THAT CRASHD
			// HERE FROM ANOTHER WORLD. THIS GIVES THEM THE ABILITY TO GRANDMASTER 40 SKILLS BUT THEY BEGIN THE
			// THE GAME WITH NO GOLD OR ANY SKILLS. THEY WILL ALSO SUFFER:
			// 4X GUILD FEES
			// 3X RESURRECTION FEES
			// SUFFER DOUBLE STAT/FAME/KARMA/SKILL LOSS ON DEATH WITH NO TRIBUTE
			// SUFFER NORMAL STAT/FAME/KARMA/SKILL LOSS ON DEATH WITH TRIBUTE
			return false;
		}

		public static bool AnnounceTrapSaves()
		{
			// IF SET TO TRUE, THEN CHARACTERS WILL HAVE AN ANNOUNCEMENT WHEN THEY MAKE A SAVING THROW
			// AGAINST A PARTICULAR HIDDEN FLOOR TRAP. OTHERWISE, THEY WILL NEVER KNOW THEY AVOIDED IT.
			return true;
		}

		public static bool IdentifyItemsOnlyInPack()
		{
			// IF SET TO TRUE, THEN CHARACTERS HAVE TO PUT UNIDENTIFIED ITEMS IN THEIR PACK TO IDENTIFY THEM
			// IF SET TO FALSE, THEN CHARACTERS CAN IDENITIFY ITEMS THEY ARE ABLE TO DOUBLE CLICK
			// THIS OPTION IS ONLY PROVIDED IF YOU NEED THE SECURITY OF MULTI-PLAYER ENVIRONMENTS
			return false;
		}

		public static int SpellDamageIncreaseVsMonsters()
		{
			return 300;
		}

		public static int SpellDamageIncreaseVsPlayers()
		{
			return 100;
		}

		public static int SoulForceCap() {
			return 50000;
		}

		public static int SoulForceMin() {
			return 1000;
		}

		public static int FameCap() {
			return 15000;
		}

		public static int StatBonusCap() {
			return 115;
		}

		public static int KarmaMax() {
			return 15000;
		}

		public static int KarmaMin() {
			return -15000;
		}

		public static int BardSongMinDurationCap() {
			return 5;
		}

		public static bool RunRoutinesAtStartup()
		{
			// THE SERVER HAS SOME SELF-CLEANING AND SELF-SUSTAINING SCRIPTS IT RUNS EVERY HOUR, 3 HOURS, & 24 HOURS
			// IF YOU RUN A 24x7 SERVER, YOU CAN SET THE BELOW TO false SINCE YOUR SERVER WILL RUN THESE AT THOSE TIMES
			// IF YOU PLAY SINGLE PLAYER, AND YOU TURN THE SERVER ON/OFF AS REQUIRED, THEN SET THIS TO true SO THESE ROUTINES AT LEAST RUN FOR YOU
			return true;
		}

		public static int QuestRewardModifier(Mobile m, Item i)
		{
			// FOR ASSSASSIN, THIEF, FISHING, & STANDARD QUESTS
			// 100 PERCENT IS STANDARD

			int mod = 100; // PERCENT
			if (m != null)
				mod = GetGoldCutRate(m, null);
			
			else if (i != null)
				mod = GetGoldCutRate(null, i);

			if ( mod < 0 )
				mod = 0;

			return mod;
		}

		public static int PlayerLevelMod( int value, Mobile m )
		{
			// THIS MULTIPLIES AGAINST THE RAW STAT TO GIVE THE RETURNING HIT POINTS, MANA, OR STAMINA
			// SO SETTING THIS TO 2.0 WOULD GIVE THE CHARACTER HITS POINTS EQUAL TO THEIR STRENGTH x 2
			// THIS ALSO AFFECTS BENEFICIAL SPELLS AND POTIONS THAT RESTORE HEALTH, STAMINA, AND MANA

			double mod = 1.0;
				if ( m is PlayerMobile ){ mod = 1.25; } // ONLY CHANGE THIS VALUE

			value = (int)( value * mod );
				if ( value < 0 ){ value = 1; }

			return value;
		}

		public static int WyrmBody()
		{
			return 723; // THIS IS WHAT WYRMS LOOK LIKE IN THE GAME...IF YOU WANT A DIFFERENT APPEARANCE THEN CHANGE THIS VALUE
		}

		public static void AdditionalHitPoints( BaseCreature bc, int DungeonDifficulty )
		{
			// THIS SECTION INCREASES THE HIT POINTS OF MONSTERS WITHOUT INCREASING THEIR OTHER ATTRIBUTES
			// CONSIDER USING THIS IF YOU WANT TO INCREASE MONSTER DIFFICULTY, AS INCREASING OTHER ATTRIBUTES
			// COULD LEAD TO MONSTERS THAT ARE IMPOSSIBLE TO BEAT.

			// IF YOU DO NOT WANT ANY HIT POINT INCREASES, THEN UNCOMMENT THE HitPercents LINE BELOW.

			// IF YOU WANT TO INCREASE THE HIT POINTS, THEN TWEAK THE SETTINGS BELOW. THERE ARE ONLY 0-4 DIFFICULTY LEVELS FOR DUNGEONS.
			// IF YOU INCREASE A CREATURE HIT POINTS, ANY BREATH/SPECIAL ATTACKS WILL CAUSE MORE DAMAGE WHEN THEY ARE AT FULL HEALTH.

			double HitPercents = 15;																			// LEVEL 0 & 1 DUNGEONS
			if ( DungeonDifficulty == 1 ){ HitPercents = Utility.RandomMinMax( 30, 50 )*0.01; }				// LEVEL 2 DUNGEONS
			else if ( DungeonDifficulty == 2 ){ HitPercents = Utility.RandomMinMax( 55, 85 )*0.01; }
			else if ( DungeonDifficulty == 3 ){ HitPercents = Utility.RandomMinMax( 90, 110 )*0.01; }		// LEVEL 3 DUNGEONS
			else if ( DungeonDifficulty > 3 ){ HitPercents = Utility.RandomMinMax( 115, 175 )*0.01; }			// LEVEL 4 DUNGEONS

			if (((Mobile)bc).Karma < 0)
			{
				if ((AetherGlobe.BalanceLevel / 100000) >= 0.85)
					HitPercents *= 1.35;			
				if ((AetherGlobe.BalanceLevel / 100000) >= 0.55)
					HitPercents *= 1.15;
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.4)
					HitPercents += 0;
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.20)
					HitPercents *= .85;		
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.00)
					HitPercents *= .75;			
			}
			else if (((Mobile)bc).Karma > 0)
			{
				if ((AetherGlobe.BalanceLevel / 100000) >= 0.85)
					HitPercents *= .75;			
				if ((AetherGlobe.BalanceLevel / 100000) >= 0.55)
					HitPercents *= .85;
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.4)
					HitPercents += 0;
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.20)
					HitPercents *= 1.15;		
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.00)
					HitPercents *= 1.35;	
			}				

			// HitPercents = 0;	

			int hits = (int)(bc.HitsMax + (bc.HitsMax*HitPercents) );
			bc.SetHits( hits );
			bc.Hits = bc.HitsMax;
		}

		public static void LessHitPoints( BaseCreature bc, int DungeonDifficulty )
		{
			// THIS SECTION INCREASES THE HIT POINTS OF MONSTERS WITHOUT INCREASING THEIR OTHER ATTRIBUTES
			// CONSIDER USING THIS IF YOU WANT TO INCREASE MONSTER DIFFICULTY, AS INCREASING OTHER ATTRIBUTES
			// COULD LEAD TO MONSTERS THAT ARE IMPOSSIBLE TO BEAT.

			// IF YOU DO NOT WANT ANY HIT POINT INCREASES, THEN UNCOMMENT THE HitPercents LINE BELOW.

			// IF YOU WANT TO INCREASE THE HIT POINTS, THEN TWEAK THE SETTINGS BELOW. THERE ARE ONLY 0-4 DIFFICULTY LEVELS FOR DUNGEONS.
			// IF YOU INCREASE A CREATURE HIT POINTS, ANY BREATH/SPECIAL ATTACKS WILL CAUSE MORE DAMAGE WHEN THEY ARE AT FULL HEALTH.

			double HitPercents = 15;																			// LEVEL 0 & 1 DUNGEONS
			if ( DungeonDifficulty == 1 ){ HitPercents = Utility.RandomMinMax( 30, 50 )*0.01; }				// LEVEL 2 DUNGEONS
			else if ( DungeonDifficulty == 2 ){ HitPercents = Utility.RandomMinMax( 55, 85 )*0.01; }
			else if ( DungeonDifficulty == 3 ){ HitPercents = Utility.RandomMinMax( 90, 110 )*0.01; }		// LEVEL 3 DUNGEONS
			else if ( DungeonDifficulty > 3 ){ HitPercents = Utility.RandomMinMax( 115, 175 )*0.01; }			// LEVEL 4 DUNGEONS

			if (((Mobile)bc).Karma < 0)
			{
				if ((AetherGlobe.BalanceLevel / 100000) >= 0.85)
					HitPercents *= 1.35;			
				if ((AetherGlobe.BalanceLevel / 100000) >= 0.55)
					HitPercents *= 1.15;
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.4)
					HitPercents += 0;
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.20)
					HitPercents *= .85;		
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.00)
					HitPercents *= .75;			
			}
			else if (((Mobile)bc).Karma > 0)
			{
				if ((AetherGlobe.BalanceLevel / 100000) >= 0.85)
					HitPercents *= .75;			
				if ((AetherGlobe.BalanceLevel / 100000) >= 0.55)
					HitPercents *= .85;
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.4)
					HitPercents += 0;
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.20)
					HitPercents *= 1.15;		
				else if ((AetherGlobe.BalanceLevel / 100000) >= 0.00)
					HitPercents *= 1.35;	
			}				

			// HitPercents = 0;	

			int hits = (int)(bc.HitsMax / (1+(HitPercents / 1.5)) );
			bc.SetHits( hits );
			bc.Hits = bc.HitsMax;
		}

		public static bool GoodVsEvil()
		{
			// THERE ARE CREATURES WITH PURPLE NAMES THAT FIGHT EVIL. THIS MEANS CRIMINALS, MURDERERS, OR THOSE WITH NEGATIVE KARMA
			// THEY WILL ATTACH OTHER PLAYER CHARACTER BUT SETTING THIS VALUE TO TRUE WILL ALSO HAVE THEM ATTACK EVIL MONSTERS THEY ARE NEAR
			// THOSE THAT ATTACK EVIL ATTACK THOSE WITH -2500 KARMA OR LOWER
			// IF YOU FIND THAT THESE GOODY-GOODY CREATURES ARE FIGHTING OTHER CREATURES TOO MUCH IN YOUR VIRTUAL WORLD, SET THIS TO FALSE
			// CREATURES THAT KILL EACH OTHER IN THESE SIMULATED CONFLICTS WILL HAVE THE CORPSES DELETE UPON DEATH SO PLAYERS DO NOT BENEFIT FROM FREE TREASURE
			// NO MATTER THIS SETTING, SUCH GOODY-GOODY CREATURES WILL ATTACK OTHER PLAYER CHARACTERS THAT FALL WITHIN THE CRITERIA NOTED
			return true;
		}

		public static bool FastFriends( Mobile m )					// IF TRUE, FOLLOWERS WILL ATTEMPT TO STAY WITH YOU IF YOU ARE RUNNING FAST
		{															// OTHERWISE THEY HAVE THEIR OWN DEFAULT SPEEDS
			if ( m is BaseCreature && ((BaseCreature)m).ControlMaster != null ){ return true; } // THIS VALUE YOU WOULD CHANGE
			return true;
		}

		public static bool FriendsAvoidHeels() // IF true, FOLLOWERS WILL HAVE A MORE RANDOM PATTERN WHEN FOLLOWING YOU, INSTEAD OF ALWAYS STACKED ON TOP OF EACH OTHER
		{
			return true;
		}

		public static bool FriendsGuardFriends() // IF true, FOLLOWERS WILL NOT ONLY GUARD YOU...BUT ALSO YOUR OTHER FOLLOWERS AND ATTACK THOSE THAT YOUR GROUP ATTACKS
		{
			return true;
		}

		public static int BoatDecay() // HOW MANY DAYS A BOAT WILL LAST BEFORE IT DECAYS, WHERE USING IT REFRESHES THE TIME
		{ 
			return 30;
		}

		public static double HomeDecay() // HOW MANY DAYS A HOUSE WILL LAST BEFORE IT DECAYS, WHERE USING IT REFRESHES THE TIME
		{
			return 60.0;
		}

		public static bool HousesDecay(object house) // DO HOUSES DECAY IN YOUR GAME AT ALL?
		{
			if (house is TownHouse)
				return true;
			
			return false;
		}

		public static int HousesPerAccount() // HOW MANY HOUSES CAN ONE ACCOUNT HAVE, WHERE -1 IS NOT LIMIT
		{
			return -1;
		}

		public static bool EnableDungeonSoundEffects() // DO THE DUNGEONS HAVE RANDOM SOUND EFFECTS?
		{
			return true;	
		}
		
		public static bool EnableAmbientSoundEffects() // DO THE DUNGEONS HAVE RANDOM SOUND EFFECTS?
		{
			return true;	
		}
		// ******************************************************************************************************************************************
		// THE OPTIONS IN THIS SECTION ARE MEANT TO HELP SIMULATE AN ECOMONY IN THE WORLD WHERE VENDORS SELL AND/OR BUY SOME THINGS BUT NOT OTHERS.
		// THIS CHANGES ON A SCHEDULE AND THEN RANDOMIZES WHAT THEY BUY/SELL EACH TIME THE SCHEDULE TRIGGERS. SOME EXAMPLES OF THIS CONFIGURATION IS
		// A VENDOR IN BRITAIN MAY NOT WANT TO BUY YOUR LEATHER HIDES, BUT THE TANNER IN MONTOR JUST MIGHT SO YOUR CHARACTER MAY WANT TO MAKE THE
		// JOURNEY THERE. THERE ARE ALSO ITEMS THAT ARE MEANT TO BE HARD TO FIND (LIKE RUNEBOOKS) SO YOU MAY HAVE TO VISIT SEVERAL VILLAGES BEFORE
		// YOU FIND A VENDOR THAT SELLS ONE. AGAIN, THIS IS TO CULTIVATE WORLD TRAVEL AND EXPLORATION.

		public static bool SellChance() // CHANCE A VENDOR SELLS A REGULAR ITEM. SET "chance" HIGHER FOR MORE OFTEN
		{
			int chance = 50;	if ( chance >= Utility.RandomMinMax( 1, 100 ) ){ return true; }
			return false;
		}

		public static bool SellCommonChance() // CHANCE A VENDOR SELLS A REALLY COMMON ITEM. SET "chance" HIGHER FOR MORE OFTEN
		{
			int chance = 80;	if ( chance >= Utility.RandomMinMax( 1, 100 ) ){ return true; }
			return false;
		}

		public static bool SellRareChance() // CHANCE A VENDOR SELLS A RARE ITEM. SET "chance" HIGHER FOR MORE OFTEN
		{
			int chance = 25;	if ( chance >= Utility.RandomMinMax( 1, 100 ) ){ return true; }
			return false;
		}

		public static bool SellVeryRareChance() // CHANCE A VENDOR SELLS A VERY RARE ITEM. SET "chance" HIGHER FOR MORE OFTEN
		{
			int chance = 5;	if ( chance >= Utility.RandomMinMax( 1, 100 ) ){ return true; }
			return false;
		}

		public static bool BuyChance() // CHANCE A VENDOR BUYS A REGULAR ITEM. SET "chance" HIGHER FOR MORE OFTEN
		{
			int chance = 70;	if ( chance >= Utility.RandomMinMax( 1, 100 ) ){ return true; }
			return false;
		}

		public static bool BuyCommonChance() // CHANCE A VENDOR BUYS A COMMON ITEM. SET "chance" HIGHER FOR MORE OFTEN
		{
			int chance = 90;	if ( chance >= Utility.RandomMinMax( 1, 100 ) ){ return true; }
			return false;
		}

		public static bool BuyRareChance() // CHANCE A VENDOR BUYS A RARE ITEM. SET "chance" HIGHER FOR MORE OFTEN
		{
			int chance = 40;	if ( chance >= Utility.RandomMinMax( 1, 100 ) ){ return true; }
			return false;
		}
		public static bool BuyVeryRareChance() // CHANCE A VENDOR BUYS A RARE ITEM. SET "chance" HIGHER FOR MORE OFTEN
		{
			int chance = 5;	if ( chance >= Utility.RandomMinMax( 1, 100 ) ){ return true; }
			return false;
		}

		public static bool NewMounts() // If enabled, will keep the mount body and will be extended for more mounts later
		{
			return true;
		}
		
//final edits
// ******************************************************************************************************************************************
		
		public static bool buysellcontext() // do you want vendors to have clickable context menus for buy/sell? (comment one OR the other)
		{
			//return true; 
			return false;
		}
		
		public static int decayrate() // note, itemdecay was disabled in this release.  if you want itemdecay, run runuo.exe.itemdecay instead and set the value here to the amount of time you want for item decay .  
		{
			return 1;
		}
		
		public static int skillcap() // This server can accomodate unlimited skillcap.  What should a NEW CHARACTER's skillcap be?
		{
			return 25000;
		}
		
		public static int skillcapbarbaric() // Skill cap for Barbaric characters
		{
			return 100000;
		}

		public static int skillcapwanted() // Skill cap for Wanted characters
		{
			return 100000;
		}

		public static int skillcapalien() // Skill cap for Alien characters
		{
			return 100000;
		}

		public static int newstatcap() // This server can accomodate unlimited statcap.  What should a NEW CHARACTER's statcap be?
		{
			return 250;
		}
		
		public static bool powerscrolllevel() // this sets whether you need to use a +5 powerscroll before using a +10, and +10 before using a +15, etc.
		{
			return true; 
			//return false;
		}		
	
// Final added customizable caps for certain aos attributes
		public static int RegenHitsCap() 
		{
			return 35;
		}	

		public static int RegenManaCap() 
		{
			return 35;
		}	

		public static int RegenStamCap() 
		{
			return 35;
		}	

		public static int SpellDamage() 
		{
			return 300;
		}

		public static int ReflectDamageCap() {
			return 100;
		}

		public static int LowerReagentCostCap() {
			return 100;
		}

		public static int LowerManaCostCap() {
			return 90;
		}

		public static int EnhancePotionCap(Mobile m) 
		{
			if (m is PlayerMobile && ((PlayerMobile)m).Alchemist())
				return 100;
			
			return 50;
		}

		public static int LuckCap() {
			return 4000;
		}

		public static double BandageSpeedMin() {
			return 3.0;
		}

		public static double MinimumSwingDelaySeconds() {
			return 1.25;
		}

		public static int SwingSpeedCap() {
			return 100;
		}

		public static int HitChanceCap() {
			return 45;
		}

		public static int DefendChanceCap() {
			return 45;
		}

		public static int CastSpeedCap() {
			return 2;
		}

		public static int PhylacterySkillStatGainCap() {
			return 14;
		}

		public static int CastRecoveryCap() {
			return 6;
		}

		public static int DamageIncreaseCap() {
			return 300;
		}

		public static int RealSpellDamageCap() {
			if (SpellDamage() > SpellDamageIncreaseVsMonsters() && SpellDamageIncreaseVsMonsters() > 0) {
				return SpellDamageIncreaseVsMonsters();
			} else {
				return SpellDamage();
			}
		}

		public static int WeaponSpeedCap() {
			return 100;
		}
	
		public static int curseincrease() // base for balancelevel calculation - higher means quicker increases, lower for less
		{
			return 3000;
		}
		
		public static double DamageToPets()
		{
			return 1.0;
		}
		
		public static int CriticalToPets()
		{
			return 0;
		}
		
		//gadget2013 edits
		// ******************************************************************************************************************************************
		// This section defines whether the simulated Player Vendor system is active or not, as well as defines
		// its properties. Simulated Player Vendor system allows to use player vendors in solo mode. The "virtual"
		// bot sales will be simulated at certain time periods. Recent sales can be viewed (up to 5 by default) by
		// talking and naming the vendor by name and saying the word "report", e.g. if the vendor's name is Calder,
		// saying "Calder report" will make the given vendor named Calder report on recent sales, if there were any.

		public static bool EnableSimulatedPVSales( Mobile vendor ) // If enabled, Player Vendors will try to randomly "sell" items to virtual bot adventurers every 10 minutes (by default), see additional settings in Mobiles/PlayerVendor.cs.
		{
			
			Region reg = Region.Find( vendor.Location, vendor.Map );

			if (reg.IsPartOf(typeof(SafeRegion) ) || reg.IsPartOf(typeof(PublicRegion)))
			    return true;
			
		    	return false;
		}

		public static bool EnableRandomPVAdvertisements( Mobile vendor ) // If enabled, Player Vendors will randomly shout advertisement lines based on sold item names and descriptions
		{
		    	Region reg = Region.Find( vendor.Location, vendor.Map );

			if (reg.IsPartOf(typeof(SafeRegion) ) || reg.IsPartOf(typeof(PublicRegion)))
			    return true;
			
		    	return false;
		}

		public static bool PVChargeForServicePerUODay( Mobile vendor ) // If enabled, Player Vendors will charge a certain amount of money (dependent on the total price of the items for sale) for their services every UO day
		{
		    	Region reg = Region.Find( vendor.Location, vendor.Map );

			if (reg.IsPartOf(typeof(SafeRegion) ) || reg.IsPartOf(typeof(PublicRegion)))
			    return true;
			
		    	return false;
		}

		public static bool AllowFreePVPlacement() // If enabled, Player Vendors can be placed anywhere on the map except in other player's houses and in private houses
		{
		    return true;
		}
		// ******************************************************************************************************************************************


		// ******************************************************************************************************************************************

		public static int GetDifficultyLevel( Point3D loc, Map map ) // THESE ARE DUNGEON DIFFICULTY LEVELS FROM 0 (NEWBIE) TO 1 (NORMAL) UP TO 4 (DEADLY)
		{
			int Heat = -5;

			Region reg = Region.Find( loc, map );

			if ( map == Map.Felucca )
			{
				if ( reg.IsPartOf( "the Lodoria Sewers" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "the Lizardman Cave" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Ratmen Cave" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Crypt" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "Dungeon Wrong" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Volcanic Cave" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "Terathan Keep" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Shame" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Ice Fiend Lair" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Frozen Hells" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "Dungeon Hythloth" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "the Mind Flayer City" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the City of Embers" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "Dungeon Destard" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Despise" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Deceit" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Covetous" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Lodoria Catacombs" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Halls of Undermountain" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Vault of the Black Knight" ) ){ Heat = 4; } // -- IN SERPENT ISLAND
				else if ( reg.IsPartOf( "the Crypts of Dracula" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Castle of Dracula" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "Stonegate Castle" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Ancient Elven Mine" ) ){ Heat = 4; }

				else if ( reg.IsPartOf( "Morgaelin's Inferno" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Zealan Tombs" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Temple of Osirus" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "Argentrock Castle" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Daemon's Crag" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Hall of the Mountain King" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Depths of Carthax Lake" ) ){ Heat = 4; }

				else if ( reg.IsPartOf( "the Montor Sewers" ) ){ Heat = 0; }

				else if ( reg.IsPartOf( "Mangar's Tower" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "Mangar's Chamber" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Kylearan's Tower" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "Harkyn's Castle" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "the Catacombs" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "the Lower Catacombs" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "the Sewers" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Cellar" ) ){ Heat = 0; }

				else if ( reg.IsPartOf( "the Sanctum of Saltmarsh" ) ){ Heat = 3; }
			}
			else if ( map == Map.Trammel )
			{
				if ( reg.IsPartOf( "the Ancient Pyramid" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Mausoleum" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "Dungeon Clues" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "Dardin's Pit" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "Frostwall Caverns" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "Dungeon Abandon" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "Dungeon Exodus" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Fires of Hell" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Frozen Dungeon" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Mines of Morinia" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Perinian Depths" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Ratmen Lair" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Dungeon of Time Awaits" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "Castle Exodus" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Cave of Banished Mages" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the City of the Dead" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "the Dragon's Maw" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Cave of the Zuluu" ) ){ Heat = 0; }
				else if ( reg.IsPartOf( "the Tower of Brass" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "the Caverns of Poseidon" ) ){ Heat = 1; }

				else if ( reg.IsPartOf( "the Accursed Maze" ) ){ Heat = -1; }
				else if ( reg.IsPartOf( "the Chamber of Bane" ) ){ Heat = -1; }
				else if ( reg.IsPartOf( "Coldhall Depths" ) ){ Heat = -1; }
				else if ( reg.IsPartOf( "the Dark Sanctum" ) ){ Heat = -1; }
				else if ( reg.IsPartOf( "the Forgotten Tombs" ) ){ Heat = -1; }
				else if ( reg.IsPartOf( "the Magma Vaults" ) ){ Heat = -1; }
				else if ( reg.IsPartOf( "the Shrouded Grave" ) ){ Heat = -1; }

				else if ( reg.IsPartOf( "the Ruins of the Black Blade" ) ){ Heat = 2; }

				else if ( reg.IsPartOf( "Steamfire Cave" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Kuldara Sewers" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Crypts of Kuldar" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Vordo's Castle" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Vordo's Dungeon" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Vordo's Castle Grounds" ) ){ Heat = 3; }

			}
			else if ( map == Map.Malas )
			{
				if ( reg.IsPartOf( "the Ancient Prison" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Cave of Fire" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Cave of Souls" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Ankh" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Bane" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Hate" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Scorn" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "Dungeon Torment" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "Dungeon Vile" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Wicked" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "Dungeon Wrath" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Flooded Temple" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Gargoyle Crypts" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Serpent Sanctum" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Tomb of the Fallen Wizard" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Castle of the Black Knight" ) ){ Heat = 4; }
				
			}
			else if ( map == Map.Tokuno )
			{
				if ( reg.IsPartOf( "the Altar of the Blood God" ) ){ Heat = 2; }
			}
			else if ( map == Map.Ilshenar )
			{
				if ( loc.X < 1007 && loc.Y < 1279 )
				{
					// DarkMoor
					Heat = 1;
				}
				else if ( reg.IsPartOf( "Widow's Lament" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "Doom" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "Doom Gauntlet" ) ){ Heat = 4; }
				else 
				{
					if ( reg.IsPartOf( "the Glacial Scar" ) ){ Heat = 2; }
					else if ( Server.Misc.Worlds.GetRegionName( map, loc ) == "the Underworld" ){ Heat = 3; }
					else if ( reg.IsPartOf( typeof( DungeonRegion ) ) ){ Heat = 4; }
				}
			}
			else if ( map == Map.TerMur )
			{
				if ( reg.IsPartOf( "the Blood Temple" ) ){ Heat = 3; } // -- IN ISLES OF DREAD
				else if ( reg.IsPartOf( "the Tombs" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Corrupt Pass" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Crypt" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Great Pyramid" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Altar of the Dragon King" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Ice Queen Fortress" ) ){ Heat = 3; } // -- IN ISLES OF DREAD
				else if ( reg.IsPartOf( "Dungeon of the Lich King" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "Dungeon of the Mad Archmage" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Halls of Ogrimar" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Ratmen Mines" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "Dungeon Rock" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Sakkhra Tunnel" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Spider Cave" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Storm Giant Lair" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Cave of the Ancient Wyrm" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Isle of the Lich" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Castle of the Mad Archmage" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Mage Mansion" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Island of the Storm Giant" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Orc Fort" ) ){ Heat = 2; }
				else if ( reg.IsPartOf( "the Hedge Maze" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Pixie Cave" ) ){ Heat = 1; }
				else if ( reg.IsPartOf( "the Forgotten Halls" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Undersea Castle" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Tomb of Kazibal" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Catacombs of Azerok" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Azure Castle" ) ){ Heat = 3; }
				else if ( reg.IsPartOf( "the Scurvy Reef" ) ){ Heat = 3; }

				else if ( reg.IsPartOf( "the Ancient Crash Site" ) ){ Heat = 4; }
				else if ( reg.IsPartOf( "the Ancient Sky Ship" ) ){ Heat = 4; }
			}

			return Heat;
		}
	}
}
