using System;	
using Server;	
using System.Collections;	
using Server.Misc;	
using Server.Items;	
using Server.Network;	
using Server.Commands;	
using Server.Commands.Generic;	
using Server.Mobiles;	
using Server.Accounting;	

namespace Server.Misc
{
    class TavernPatrons
    {
		public static void RemoveSomeGear( Mobile m, bool helm )
		{
			if ( m.FindItemOnLayer( Layer.OneHanded ) != null ) { m.FindItemOnLayer( Layer.OneHanded ).Delete(); }
			if ( m.FindItemOnLayer( Layer.TwoHanded ) != null ) { m.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
			if ( m.FindItemOnLayer( Layer.FirstValid ) != null && m.FindItemOnLayer( Layer.FirstValid ) is BaseShield ) { m.FindItemOnLayer( Layer.FirstValid ).Delete(); }
			if ( m.FindItemOnLayer( Layer.FirstValid ) != null && m.FindItemOnLayer( Layer.FirstValid ) is BaseWeapon ) { m.FindItemOnLayer( Layer.FirstValid ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Helm ) != null && helm ) { if ( m.FindItemOnLayer( Layer.Helm ) is BaseArmor ){ m.FindItemOnLayer( Layer.Helm ).Delete(); } }
		}

		public static string GetTitle()
		{
			string sTitle = "";	
			string myTitle = "";	

			int otitle = Utility.RandomMinMax( 1, 33 );	
			if (otitle == 1){sTitle = "of the North";}
			else if (otitle == 2){sTitle = "of the South";}
			else if (otitle == 3){sTitle = "of the East";}
			else if (otitle == 4){sTitle = "of the West";}
			else if (otitle == 5){sTitle = "of the City";}
			else if (otitle == 6){sTitle = "of the Hills";}
			else if (otitle == 7){sTitle = "of the Mountains";}
			else if (otitle == 8){sTitle = "of the Plains";}
			else if (otitle == 9){sTitle = "of the Woods";}
			else if (otitle == 10){sTitle = "of the Light";}
			else if (otitle == 11){sTitle = "of the Dark";}
			else if (otitle == 12){sTitle = "of the Night";}
			else if (otitle == 13){sTitle = "of the Sea";}
			else if (otitle == 14){sTitle = "of the Desert";}
			else if (otitle == 15){sTitle = "of the Order";}
			else if (otitle == 16){sTitle = "of the Forest";}
			else if (otitle == 17){sTitle = "of the Snow";}
			else if (otitle == 18){sTitle = "of the Coast";}
			else if (otitle == 19){sTitle = "of the Arid Wastes";}
			else if (otitle == 20){sTitle = "of the Beetling Brow";}
			else if (otitle == 21){sTitle = "of the Cyclopean City";}
			else if (otitle == 22){sTitle = "of the Dread Wilds";}
			else if (otitle == 23){sTitle = "of the Eerie Eyes";}
			else if (otitle == 24){sTitle = "of the Foetid Swamp";}
			else if (otitle == 25){sTitle = "of the Forgotten City";}
			else if (otitle == 26){sTitle = "of the Haunted Heath";}
			else if (otitle == 27){sTitle = "of the Hidden Valley";}
			else if (otitle == 28){sTitle = "of the Howling Hills";}
			else if (otitle == 29){sTitle = "of the Jagged Peaks";}
			else if (otitle == 30){sTitle = "of the Menacing Mien";}
			else if (otitle == 31){sTitle = "of the Savage Isle";}
			else if (otitle == 32){sTitle = "of the Tangled Woods";}
			else {sTitle = "of the Watchful Eyes";}

			string sColor = "Red";	
			switch( Utility.RandomMinMax( 0, 9 ) )
			{
				case 0: sColor = "Black"; break;	
				case 1: sColor = "Blue"; break;	
				case 2: sColor = "Gray"; break;	
				case 3: sColor = "Green"; break;	
				case 4: sColor = "Red"; break;	
				case 5: sColor = "Brown"; break;	
				case 6: sColor = "Orange"; break;	
				case 7: sColor = "Yellow"; break;	
				case 8: sColor = "Purple"; break;	
				case 9: sColor = "White"; break;	
			}

			string gColor = "Gold";	
			switch( Utility.RandomMinMax( 0, 11 ) )
			{
				case 0: gColor = "Gold"; break;	
				case 1: gColor = "Silver"; break;	
				case 2: gColor = "Arcane"; break;	
				case 3: gColor = "Iron"; break;	
				case 4: gColor = "Steel"; break;	
				case 5: gColor = "Emerald"; break;	
				case 6: gColor = "Ruby"; break;	
				case 7: gColor = "Bronze"; break;	
				case 8: gColor = "Jade"; break;	
				case 9: gColor = "Sapphire"; break;	
				case 10: gColor = "Copper"; break;	
				case 11: gColor = "Royal"; break;	
			}

			string kKiller = "Giants";	
			switch( Utility.RandomMinMax( 0, 12 ) )
			{
				case 0: kKiller = "Giants"; break;	
				case 1: kKiller = "Dragons"; break;	
				case 2: kKiller = "Ogres"; break;	
				case 3: kKiller = "Trolls"; break;	
				case 4: kKiller = "Demons"; break;	
				case 5: kKiller = "Devils"; break;	
				case 6: kKiller = "Drow"; break;	
				case 7: kKiller = "Orcs"; break;	
				case 8: kKiller = "Minotaurs"; break;	
				case 9: kKiller = "Monsters"; break;	
				case 10: kKiller = "Undead"; break;	
				case 11: kKiller = "Serpents"; break;	
				case 12: kKiller = "Vampires"; break;	
			}

			string mKiller = "Giant";	
			switch( Utility.RandomMinMax( 0, 12 ) )
			{
				case 0: mKiller = "Giant"; break;	
				case 1: mKiller = "Dragon"; break;	
				case 2: mKiller = "Ogre"; break;	
				case 3: mKiller = "Troll"; break;	
				case 4: mKiller = "Demon"; break;	
				case 5: mKiller = "Devil"; break;	
				case 6: mKiller = "Drow"; break;	
				case 7: mKiller = "Orc"; break;	
				case 8: mKiller = "Minotaur"; break;	
				case 9: mKiller = "Monster"; break;	
				case 10: mKiller = "Undead"; break;	
				case 11: mKiller = "Serpent"; break;	
				case 12: mKiller = "Vampire"; break;	
			}

			string aKiller = "Slayer";	
			switch( Utility.RandomMinMax( 0, 4 ) )
			{
				case 0: aKiller = "Slayer"; break;	
				case 1: aKiller = "Killer"; break;	
				case 2: aKiller = "Butcher"; break;	
				case 3: aKiller = "Executioner"; break;	
				case 4: aKiller = "Hunter"; break;	
			}

			switch ( Utility.RandomMinMax( 0, 107 ) )
			{
				case 0: myTitle = "from Above"; break;	
				case 1: myTitle = "from Afar"; break;	
				case 2: myTitle = "from Below"; break;	
				case 3: myTitle = "of the " + sColor + " Cloak"; break;	
				case 4: myTitle = "of the " + sColor + " Robe"; break;	
				case 5: myTitle = "of the " + sColor + " Order"; break;	
				case 6: myTitle = "of the " + gColor + " Shield"; break;	
				case 7: myTitle = "of the " + gColor + " Sword"; break;	
				case 8: myTitle = "of the " + gColor + " Helm"; break;	
				case 9: myTitle = sTitle; break;	
				case 10: myTitle = sTitle; break;	
				case 11: myTitle = sTitle; break;	
				case 12: myTitle = sTitle; break;	
				case 13: myTitle = sTitle; break;	
				case 14: myTitle = sTitle; break;	
				case 15: myTitle = sTitle; break;	
				case 16: myTitle = sTitle; break;	
				case 17: myTitle = sTitle; break;	
				case 18: myTitle = sTitle; break;	
				case 19: myTitle = sTitle; break;	
				case 20: myTitle = sTitle; break;	
				case 21: myTitle = sTitle; break;	
				case 22: myTitle = "the " + sColor; break;	
				case 23: myTitle = "the Adept"; break;	
				case 24: myTitle = "the Nomad"; break;	
				case 25: myTitle = "the Antiquarian"; break;	
				case 26: myTitle = "the Arcane"; break;	
				case 27: myTitle = "the Archaic"; break;	
				case 28: myTitle = "the Barbarian"; break;	
				case 29: myTitle = "the Batrachian"; break;	
				case 30: myTitle = "the Battler"; break;	
				case 31: myTitle = "the Bilious"; break;	
				case 32: myTitle = "the Bold"; break;	
				case 33: myTitle = "the Fearless"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Brave";} break;	
				case 34: myTitle = "the Savage"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Civilized";} break;	
				case 35: myTitle = "the Collector"; break;	
				case 36: myTitle = "the Cryptic"; break;	
				case 37: myTitle = "the Curious"; break;	
				case 38: myTitle = "the Dandy"; break;	
				case 39: myTitle = "the Daring"; break;	
				case 40: myTitle = "the Decadent"; break;	
				case 41: myTitle = "the Delver"; break;	
				case 42: myTitle = "the Distant"; break;	
				case 43: myTitle = "the Eldritch"; break;	
				case 44: myTitle = "the Exotic"; break;	
				case 45: myTitle = "the Explorer"; break;	
				case 46: myTitle = "the Fair"; break;	
				case 47: myTitle = "the Strong"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Weak";} break;	
				case 48: myTitle = "the Fickle"; break;	
				case 49:
						int iDice = Utility.RandomMinMax( 1, 10 );	
						if (iDice == 1){myTitle = "the First";}
						else if (iDice == 2){myTitle = "the Second";}
						else if (iDice == 3){myTitle = "the Third";}
						else if (iDice == 4){myTitle = "the Fourth";}
						else if (iDice == 5){myTitle = "the Fifth";}
						else if (iDice == 6){myTitle = "the Sixth";}
						else if (iDice == 7){myTitle = "the Seventh";}
						else if (iDice == 8){myTitle = "the Eighth";}
						else if (iDice == 9){myTitle = "the Ninth";}
						else {myTitle = "the Tenth";}
						break;	
				case 50: myTitle = "the Foul"; break;	
				case 51: myTitle = "the Furtive"; break;	
				case 52: myTitle = "the Gambler"; break;	
				case 53: myTitle = "the Ghastly"; break;	
				case 54: myTitle = "the Gibbous"; break;	
				case 55: myTitle = "the Great"; break;	
				case 56: myTitle = "the Grizzled"; break;	
				case 57: myTitle = "the Gruff"; break;	
				case 58: myTitle = "the Spiritual"; break;	
				case 59: myTitle = "the Haunted"; break;	
				case 60: myTitle = "the Calm"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Frantic";} break;	
				case 61:
						int iDice2 = Utility.RandomMinMax( 1, 4 );	
						if (iDice2 == 1){myTitle = "the Hooded";}
						else if (iDice2 == 2){myTitle = "the Cloaked";}
						else if (iDice2 == 3){myTitle = "the Cowled";}
						else {myTitle = "the Robed";}
						break;	
				case 62: myTitle = "the Hunter"; break;	
				case 63: myTitle = "the Imposing"; break;	
				case 64: myTitle = "the Irreverent"; break;	
				case 65: myTitle = "the Loathsome"; break;	
				case 66:
						int iDice3 = Utility.RandomMinMax( 1, 3 );	
						if (iDice3 == 1){myTitle = "the Quiet";}
						else if (iDice3 == 2){myTitle = "the Silent";}
						else {myTitle = "the Loud";}
						break;	
				case 67: myTitle = "the Lovely"; break;	
				case 68: myTitle = "the Mantled"; break;	
				case 69: myTitle = "the Masked"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Veiled";} break;	
				case 70: myTitle = "the Merciful"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Merciless";} break;	
				case 71: myTitle = "the Mercurial"; break;	
				case 72: myTitle = "the Mighty"; break;	
				case 73: myTitle = "the Morose"; break;	
				case 74: myTitle = "the Mutable"; break;	
				case 75: myTitle = "the Mysterious"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Unknown";} break;	
				case 76: myTitle = "the Obscure"; break;	
				case 77: myTitle = "the Old"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Young";} break;	
				case 78: myTitle = "the Ominous"; break;	
				case 79: myTitle = "the Peculiar"; break;	
				case 80: myTitle = "the Perceptive"; break;	
				case 81: myTitle = "the Pious"; break;	
				case 82: myTitle = "the Quick"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Slow";} break;	
				case 83: myTitle = "the Ragged"; break;	
				case 84: myTitle = "the Ready"; break;	
				case 85: myTitle = "the Rough"; break;	
				case 86: myTitle = "the Rugose"; break;	
				case 87: myTitle = "the Scarred"; break;	
				case 88: myTitle = "the Searcher"; break;	
				case 89: myTitle = "the Shadowy"; break;	
				case 90: myTitle = "the Short"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Tall";} break;	
				case 91: myTitle = "the Steady"; break;	
				case 92: myTitle = "the Uncanny"; break;	
				case 93: myTitle = "the Unexpected"; break;	
				case 94: myTitle = "the Unknowable"; break;	
				case 95: myTitle = "the Verbose"; break;	
				case 96: myTitle = "the Vigorous"; break;	
				case 97: myTitle = "the Traveler"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Wanderer";} break;	
				case 98: myTitle = "the Wary"; break;	
				case 99: myTitle = "the Weird"; break;	
				case 100: myTitle = "the Steady"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Unready";} break;	
				case 101: myTitle = "the Gentle"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Cruel";} break;	
				case 102: myTitle = "the Lost"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Exiled";} break;	
				case 103: myTitle = "the Careless"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Clumsy";} break;	
				case 104: myTitle = "the Hopeful"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Trustful";} break;	
				case 105: myTitle = "the Angry"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Timid";} break;	
				case 106: myTitle = "the " + aKiller + " of " + kKiller; break;	
				case 107: myTitle = "the " + mKiller + " " + aKiller; break;	
			}
			return myTitle;	
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static string GetRareLocation( Mobile speaker, bool toPlayer, bool MixTogether )
		{
			string what = "";	
			string where = "";	
			string say = QuestCharacters.RandomWords() + " killed them, I just know it.";	

			int rare = Utility.RandomMinMax( 1, 11 );	

			if ( rare == 1 )
			{
				what = "Exodus";	
				foreach ( Mobile mob in World.Mobiles.Values )
				if ( mob is Exodus )
				{
					where = Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location );	
				}
			}
			else if ( rare == 2 )
			{
				what = "Jormungandr";	
				foreach ( Mobile mob in World.Mobiles.Values )
				if ( mob is Jormungandr )
				{
					where = Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location );	

					if ( where == "the Bottle World of Kuldar" ){ 		where = "the waters of the Kuldar Sea"; }
					else if ( where == "the Land of Ambrosia" ){ 		where = "the waters of the Ambrosia Lakes"; }
					else if ( where == "the Island of Umber Veil" ){ 	where = "the waters of the Umber Sea"; }
					else if ( where == "the Land of Lodoria" ){ 		where = "the waters of the Lodoria Ocean"; }
					else if ( where == "the Underworld" ){ 				where = "the waters of Carthax Lake"; }
					else if ( where == "the Serpent Island" ){ 			where = "the waters of the Serpent Seas"; }
					else if ( where == "the Isles of Dread" ){ 			where = "the waters of the Dreadful Sea"; }
					else if ( where == "the Savaged Empire" ){ 			where = "the waters of the Savage Seas"; }
					else if ( where == "the Land of Sosaria" ){ 		where = "the waters of the Sosaria Ocean"; }
				}
			}
			else
			{
				foreach ( Item target in World.Items.Values )
				if ( target is FlamesBase || target is BaneBase || target is PaganBase || target is RunesBase )
				{
					if ( target is FlamesBase )
					{
						if ( rare == 2 ){ what = "the Book of Truth"; 				FlamesBase targ2 = (FlamesBase)target; if ( targ2.ItemType == 1){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
						else if ( rare == 3 ){ what = "the Bell of Courage"; 		FlamesBase targ3 = (FlamesBase)target; if ( targ3.ItemType == 2){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
						else if ( rare == 4 ){ what = "the Candle of Love"; 		FlamesBase targ4 = (FlamesBase)target; if ( targ4.ItemType == 3){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
					}
					else if ( target is BaneBase )
					{
						if ( rare == 5 ){ what = "the Scales of Ethicality"; 		BaneBase targ5 = (BaneBase)target; if ( targ5.ItemType == 1){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
						else if ( rare == 6 ){ what = "the Orb of Logic"; 			BaneBase targ6 = (BaneBase)target; if ( targ6.ItemType == 2){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
						else if ( rare == 7 ){ what = "the Lantern of Discipline"; 	BaneBase targ7 = (BaneBase)target; if ( targ7.ItemType == 3){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
					}
					else if ( target is PaganBase )
					{
						if ( rare == 8 ){ what = "the Breath of Air"; 				PaganBase targ8 = (PaganBase)target; if ( targ8.ItemType == 1){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
						else if ( rare == 9 ){ what = "the Tongue of Flame"; 		PaganBase targ9 = (PaganBase)target; if ( targ9.ItemType == 2){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
						else if ( rare == 10 ){ what = "the Heart of Earth"; 		PaganBase targ10 = (PaganBase)target; if ( targ10.ItemType == 3){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
						else if ( rare == 11 ){ what = "the Tear of the Seas"; 		PaganBase targ11 = (PaganBase)target; if ( targ11.ItemType == 4){ where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location ); } }
					}
					else if ( target is RunesBase )
					{
						what = "the Chest of Virtue"; 								where = Server.Misc.Worlds.GetRegionName( target.Map, target.Location );	
					}
				}
			}

			if ( rare != 2 && where != "" && Utility.RandomBool() ) // CITIZENS LIE HALF THE TIME
			{
				if ( Utility.RandomBool() ){ where = RandomThings.MadeUpDungeon(); }
				else { where = QuestCharacters.SomePlace( null ); }
			}

			if ( where != "" )
			{
				if ( MixTogether )
				{
					say = "";	
					switch( Utility.RandomMinMax( 0, 2 ) )
					{
						case 0: say = "where one can find " + what + " in " + where + ""; break;	
						case 1: say = "where one would need to go to " + where + " if they are going to find " + what + ""; break;	
						case 2: say = "that someone can probably find " + what + " if they search " + where + ""; break;	
					}
				}
				else if ( toPlayer )
				{
					say = "";	
					switch( Utility.RandomMinMax( 0, 2 ) )
					{
						case 0: say = "I learned where one can find " + what + ". They would need to head to " + where + "."; break;	
						case 1: say = "One would need to go to " + where + " if they are going to find " + what + "."; break;	
						case 2: say = "The " + RandomThings.GetRandomJob() + " in " + RandomThings.GetRandomCity() + " told me that someone can probably find " + what + " if they search " + where + "."; break;	
					}
				}
				else if ( speaker is SherryTheMouse )
				{
					say = "";	
					switch( Utility.RandomMinMax( 0, 2 ) )
					{
						case 0: say = "Lord British would tell me stories about " + what + ", and how it was in " + where + "."; break;	
						case 1: say = "Someone in the castle went to " + where + " and saw " + what + "."; break;	
						case 2: say = "I heard " + QuestCharacters.RandomWords() + " tell Lord British that " + what + " was said to be in " + where + "."; break;	
					}
				}
				else
				{
					say = "";	
					switch( Utility.RandomMinMax( 0, 2 ) )
					{
						case 0: say = "I finally learned where we can find " + what + ". We need to head to " + where + "."; break;	
						case 1: say = "We need to go to " + where + " if we are going to find " + what + "."; break;	
						case 2: say = "The " + RandomThings.GetRandomJob() + " in " + RandomThings.GetRandomCity() + " told me that we can probably find " + what + " if we search " + where + "."; break;	
					}
				}
			}

			return say;	
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static string GetEvilTitle()
		{
			string sTitle = "";	
			string myTitle = "";	

			int otitle = Utility.RandomMinMax( 1, 33 );	
			if (otitle == 1){sTitle = "of the Dark";}
			else if (otitle == 2){sTitle = "of the Vile";}
			else if (otitle == 3){sTitle = "of the Grave";}
			else if (otitle == 4){sTitle = "of the Dead";}
			else if (otitle == 5){sTitle = "of the Cemetery";}
			else if (otitle == 6){sTitle = "of the Dark Tower";}
			else if (otitle == 7){sTitle = "of the Fires Below";}
			else if (otitle == 8){sTitle = "of the Swamps";}
			else if (otitle == 9){sTitle = "of the Hideous";}
			else if (otitle == 10){sTitle = "of the Foul";}
			else if (otitle == 11){sTitle = "of the Dark";}
			else if (otitle == 12){sTitle = "of the Night";}
			else if (otitle == 13){sTitle = "of the Baneful";}
			else if (otitle == 14){sTitle = "of the Maleficent";}
			else if (otitle == 15){sTitle = "of the Wrathful";}
			else if (otitle == 16){sTitle = "of the Tomb";}
			else if (otitle == 17){sTitle = "of the Catacombs";}
			else if (otitle == 18){sTitle = "of the Crypts";}
			else if (otitle == 19){sTitle = "of the Dead Lands";}
			else if (otitle == 20){sTitle = "of the Necropolis";}
			else if (otitle == 21){sTitle = "of the Vampire's Tomb";}
			else if (otitle == 22){sTitle = "of the Haunted Wilds";}
			else if (otitle == 23){sTitle = "of the Eerie Eyes";}
			else if (otitle == 24){sTitle = "of the Foetid Swamp";}
			else if (otitle == 25){sTitle = "of the Destroyed City";}
			else if (otitle == 26){sTitle = "of the Haunted Heath";}
			else if (otitle == 27){sTitle = "of the Dark Mansion";}
			else if (otitle == 28){sTitle = "of the Howling Hills";}
			else if (otitle == 29){sTitle = "of the Hellish Wastes";}
			else if (otitle == 30){sTitle = "of the Menacing Mien";}
			else if (otitle == 31){sTitle = "of the Savage Lands";}
			else if (otitle == 32){sTitle = "of the Evil Woods";}
			else {sTitle = "of the Hateful Eyes";}

			string sColor = "Wicked";	
			switch( Utility.RandomMinMax( 0, 9 ) )
			{
				case 0: sColor = "Wicked"; break;	
				case 1: sColor = "Vile"; break;	
				case 2: sColor = "Malevolent"; break;	
				case 3: sColor = "Hateful"; break;	
				case 4: sColor = "Bloody"; break;	
				case 5: sColor = "Nefarious"; break;	
				case 6: sColor = "Heinous"; break;	
				case 7: sColor = "Evil"; break;	
				case 8: sColor = "Wicked"; break;	
				case 9: sColor = "Vicious"; break;	
			}

			switch ( Utility.RandomMinMax( 0, 46 ) )
			{
				case 0: myTitle = "from the Wastes"; break;	
				case 1: myTitle = "from the Grave"; break;	
				case 2: myTitle = "from the Deep"; break;	
				case 3: myTitle = "of the " + sColor + " Cloak"; break;	
				case 4: myTitle = "of the " + sColor + " Robe"; break;	
				case 5: myTitle = "of the " + sColor + " Order"; break;	
				case 6: myTitle = "of the " + sColor + " Hood"; break;	
				case 7: myTitle = "of the " + sColor + " Society"; break;	
				case 8: myTitle = "of the " + sColor + " Mask"; break;	
				case 9: myTitle = sTitle; break;	
				case 10: myTitle = sTitle; break;	
				case 11: myTitle = sTitle; break;	
				case 12: myTitle = sTitle; break;	
				case 13: myTitle = sTitle; break;	
				case 14: myTitle = sTitle; break;	
				case 15: myTitle = "of the " + sColor + " Lich"; break;	
				case 16: myTitle = "of the " + sColor + " Ghost"; break;	
				case 17: myTitle = "of the " + sColor + " Daemon"; break;	
				case 18: myTitle = "of the " + sColor + " Castle"; break;	
				case 19: myTitle = "of the " + sColor + " Skull"; break;	
				case 20: myTitle = "of the " + sColor + " Grave"; break;	
				case 21: myTitle = "of the " + sColor + " House"; break;	
				case 22: myTitle = "the " + sColor; break;	
				case 23: myTitle = "the Necromancer"; break;	
				case 24: myTitle = "the Warlock"; break;	
				case 25: myTitle = "the Witch"; break;	
				case 26: myTitle = "the Undertaker"; break;	
				case 27: myTitle = "the Torturer"; break;	
				case 28: myTitle = "the Dread Lord"; break;	
				case 29: myTitle = "the Death Knight"; break;	
				case 30: myTitle = "the Thief"; break;	
				case 31: myTitle = "the Assassin"; break;	
				case 32: myTitle = "the Rogue"; break;	
				case 33: myTitle = "the Diabolist"; break;	
				case 34: myTitle = "the Savage"; break;	
				case 35: myTitle = "the Foul"; break;	
				case 36: myTitle = "the Ghastly"; break;	
				case 37: myTitle = "the Haunted"; break;	
				case 38: myTitle = "the Frantic"; break;	
				case 39: myTitle = "the Loathsome"; break;	
				case 40: myTitle = "the Angry"; break;	
				case 41: myTitle = "of the " + sColor + " Cowl"; break;	
				case 42: myTitle = "of the " + sColor + " Eye"; break;	
				case 43: myTitle = "of the " + sColor + " Hat"; break;	
				case 44: myTitle = "of the " + sColor + " Glove"; break;	
				case 45: myTitle = "of the " + sColor + " Veil"; break;	
				case 46: myTitle = "of the " + sColor + " Shroud"; break;	
			}
			return myTitle;	
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static string CommonTalk( string sWords, string city, string dungeon, Mobile from, string adventurer, bool useAll )
		{
			string misc = "";	

			int max = 169; if ( !useAll ){ max = max + 40; }
			switch( Utility.RandomMinMax( 0, max ) )
			{
				case 0: sWords = "a bright white shrine in Sosaria that leads to the moon"; break;	
				case 1: sWords = "a castle of evil mages ruled by an even more vile archmage"; break;	
				case 2: sWords = "a cave in the Lodor swamp that is home to scaly humanoids"; break;	
				case 3: sWords = "a cave of pixies and a crazy druid in the Savaged Empire"; break;	
				case 4: sWords = "a cave west of Lodoria, that is filled with serpents, ettins, and trolls"; break;	
				case 5: sWords = "a crypt beneath the cave of souls"; break;	
				case 6: sWords = "a deep dungeon of dead in the Lodor cold lands"; break;	
				case 7: sWords = "a demigoddess of blood, and she has returned"; break;	
				case 8: sWords = "a demonic lord corrupting the core of Dungeon Ankh"; break;	
				case 9: sWords = "a den of ancient gargoyles in the swamps of the Savaged Empire"; break;	
				case 10: sWords = "a den of cave bears in the bottom of Dardin's Pit"; break;	
				case 11: sWords = "a den of dragons north of the Village of Whisper"; break;	
				case 12: sWords = "a den of harpies on the island in Lodor"; break;	
				case 13: sWords = "a Dungeon of Doom in the southwest region of Sosaria"; break;	
				case 14: sWords = "a dungeon of ork sorcerers plotting against us"; break;	
				case 15: sWords = "a frozen palace where the ice queen dwells"; break;	
				case 16: sWords = "a ghost haunting those ruins in the Savaged Empire"; break;	
				case 17: sWords = "a gigantic squid living in the bottom of the Flooded Temple"; break;	
				case 18: sWords = "a great many secrets that can be learned in Dungeon Clues"; break;	
				case 19: sWords = "a group of devil worshipers in Dungeon Vile"; break;	
				case 20: sWords = "a group of ophidians worshiping in the Serpent Sanctum"; break;	
				case 21: sWords = "a horde of demons in Dungeon Torment"; break;	
				case 22: sWords = "a horrible secret within the mountains of Umber Veil"; break;	
				case 23: sWords = "a lair of vampires that exists north of the white shrine"; break;	
				case 24: sWords = "a large group of ship builders in Lodor"; break;	
				case 25: sWords = "a magic mirror in Dungeon Fire"; break;	
				case 26: sWords = "a magical hedge maze created centuries ago"; break;	
				case 27: sWords = "a magical portal in the bottom of the Pharaohs tomb in Sosaria"; break;	
				case 28: sWords = "a magical portal in the Savaged Empire"; break;	
				case 29: sWords = "a magical seal, keeping the lich king from escaping"; break;	
				case 30: sWords = "a pack of minotaurs guarding that old hedge maze"; break;	
				case 31: sWords = "a Pharaohs tomb in the desert of Sosaria"; break;	
				case 32: sWords = "a pool of vile liquid in the bottom of Dungeon Wicked"; break;	
				case 33: sWords = "a powerful lich roaming within a tower in Sosaria, with a magical mirror"; break;	
				case 34: sWords = "a primitive orc fort near the old graveyard in the Savaged Empire"; break;	
				case 35: sWords = "a race of serpent men in dungeon scorn"; break;	
				case 36: sWords = "a secret entrance in the old cemetery of the Savaged Empire"; break;	
				case 37: sWords = "a secret passage in the Umber Veil castle"; break;	
				case 38: sWords = "a storm giant in a castle out on the Savaged Empire sea"; break;	
				case 39: sWords = "a twisted pass in the Savaged Empire with undead druids about"; break;	
				case 40: sWords = "a valley of cyclops in the Savaged Empire"; break;	
				case 41: sWords = "an underground passage that connects the northern and central islands of Lodoria"; break;	
				case 42: sWords = "an abandoned mine north of Grey in Sosaria"; break;	
				case 43: sWords = "an altar in the Savaged Empire where those are sacrificed to the dragon king"; break;	
				case 44: sWords = "an ancient blood cult in the Isles of Dread"; break;	
				case 45: sWords = "an ancient crypt where the gargoyles once buried their dead"; break;	
				case 46: sWords = "an ancient dark elf city deep beneath Lodor"; break;	
				case 47: sWords = "an ancient evil beneath the mystical hedge maze"; break;	
				case 48: sWords = "an ancient lair in the Lodor caves, where wizards and elementals dwell"; break;	
				case 49: sWords = "an ancient lich that has an island fortress in the Savaged Empire"; break;	
				case 50: sWords = "an ancient prison hidden in the desert sands of the Serpent Island"; break;	
				case 51: sWords = "an ancient wyrm flying around northern Lodor"; break;	
				case 52: sWords = "an ancient wyrm sleeping below Dungeon Hate"; break;	
				case 53: sWords = "an elven pass that leads to great craftsmen"; break;	
				case 54: sWords = "an infestation of rats and snakes in Dungeon Wrath"; break;	
				case 55: sWords = "an island in the Savaged Empire with blue scaled drakes"; break;	
				case 56: sWords = "an old ruined building in Sosaria, with treasure in the basement"; break;	
				case 57: sWords = "an ork prophecy that speaks of their god returning to rule"; break;	
				case 58: sWords = "a Savaged Empire lighthouse with a secret beneath it"; break;	
				case 59: sWords = "ancient crypts deep below the Savaged Empire"; break;	
				case 60: sWords = "a cave in Lodoria that only rangers or explorers could traverse"; break;	
				case 61: sWords = "bandits within a stronghold in northern Sosaria"; break;	
				case 62: sWords = "Castle Exodus lying in ruins since the stranger destroyed it"; break;	
				case 63: sWords = "catacombs under the city of Lodoria"; break;	
				case 64: sWords = "cauldrons full of potions in those dungeons"; break;	
				case 65: sWords = "drakkul summoning demons in the ice caves of Lodor"; break;	
				case 66: sWords = "demons being unleashed below the desert sands of Lodor"; break;	
				case 67: sWords = "a dungeon called Deceit that is home to a very powerful lich"; break;	
				case 68: sWords = "evil humans in an ancient temple in the Lodor mountains"; break;	
				case 69: sWords = "fire beetles nesting in the Cave of Fire"; break;	
				case 70: sWords = "many different elementals guarding the Tomb of the Fallen Wizard"; break;	
				case 71: sWords = "men made of ice that the ice queen summons"; break;	
				case 72: sWords = "mines in the Savaged Empire controlled by ratmen"; break;	
				case 73: sWords = "mines the barbarians dig at, in the northern part of the Isles of Dread"; break;	
				case 74: sWords = "powerful cursed creatures roaming the Serpent Island"; break;	
				case 75: sWords = "scrolls of power, but they could only be used at shrines in Ambrosia"; break;	
				case 76: sWords = "small settlements of primitive tribes in the Isles of Dread"; break;	
				case 77: sWords = "some of the most poisonous creatures in Dungeon Bane"; break;	
				case 78: sWords = "some old ruins in Sosaria, where ratmen now live beneath it"; break;	
				case 79: sWords = "a City of Mistas that was supposedly swallowed by the sea centuries ago"; break;	
				case 80: sWords = "dark elves summoning demons in dungeon destard"; break;	
				case 81: sWords = "mystical stones that elves have that can color anything"; break;	
				case 82: sWords = "a cemetery in Lodoria with a hidden secret"; break;	
				case 83: sWords = "a swamp in Sosaria with an ancient temple where a lich awaits the prophecy"; break;	
				case 84: sWords = "vile spider creatures in a castle in the jungles of Lodor"; break;	
				case 85: sWords = "an ancient relic buried in a grave in Umber Veil"; break;	
				case 86: sWords = "a powerful spellbook in a ruined wizard home"; break;	
				case 87: sWords = "a friendly dragon living beneath the Sosarian ice islands"; break;	
				case 88: sWords = "an abandoned logger home in Sosaria, with something beneath the floor boards"; break;	
				case 89: sWords = "bandits having a royal prisoner held in the northern part of Sosaria"; break;	
				case 90: sWords = "a tower in Sosaria where a lich holds a powerful staff"; break;	
				case 91: sWords = "a skull of Mondain that is deep below Castle Exodus"; break;	
				case 92: sWords = "this lighthouse keeper in Sosaria selling powerful artifacts found on the shore"; break;	
				case 93: sWords = "a lich in the Sosaria swamp carrying a marvelous artifact"; break;	
				case 94: sWords = "chests full of treasure in those magic pools"; break;	
				case 95: sWords = "a powerful troll lord in the bottom of Dardin's Pit"; break;	
				case 96: sWords = "a demon king dwelling in dungeon doom that grants wishes"; break;	
				case 97: sWords = "a pair of mystical boots that let you walk on lava"; break;	
				case 98: sWords = "really good ore in the Mines of Morinia"; break;	
				case 99: sWords = "this time lord that is sending people to the past or future"; break;	
				case 100: sWords = "a secret passage in the tomb below the Lodoria cemetery"; break;	
				case 101: sWords = "a broken wall in the British family tomb"; break;	
				case 102: sWords = "a group of ogres and ettins that have been burning farmland south of the Town of Moon"; break;	
				case 103: sWords = "a grave being dug up in the Village of Grey"; break;	
				case 104: sWords = "a volcanic dragon in southern Lodor"; break;	
				case 105: sWords = "a master vampire on an island in Lodor"; break;	
				case 106: sWords = "only necromancers and death knights living on that dead island in Lodor"; break;	
				case 107: sWords = "a town called Skara Brae that wasn't really destroyed by a wizard"; break;	
				case 108: sWords = "a wizard named Mangar who built a tower somewhere in Sosaria"; break;	
				case 109: sWords = "some stranger who brought an end to Exodus"; break;	
				case 110: sWords = "someone escaping from Skara Brae"; break;	
				case 111: sWords = "a vault of the Black Knight that is too big to explore"; break;	
				case 112: sWords = "the Undermountain being able to be reached through the lizardmen caves"; break;	
				case 113: sWords = "someone that touched a crystal ball in Mangar's tower and vanished"; break;	
				case 114: sWords = "an empty oak shelf that really is a door to the Thieves Guild"; break;	
				case 115: sWords = "a Black Magic Guild hidden around here"; break;	
				case 116: sWords = "the Black Knight having an entire city trapped in a bottle"; break;	
				case 117: sWords = "a wizard named Vordo who was able to make an entire island disappear"; break;	
				case 118: sWords = "a lost race of Zuluu that could ride the legendary dragyns"; break;	
				case 119: sWords = "the dragyns, that were once offspring of wyrms"; break;	
				case 120: sWords = "dragon like creatures with scales of gems"; break;	
				case 121: sWords = "an island appearing by the hands of Poseidon"; break;	
				case 122: sWords = "a thief escaping from the cell in the castle of Lord British"; break;	
				case 123: sWords = "some forgotten halls below the castle of Lord British"; break;	
				case 124: sWords = "some cultists bringing Kazibal back from the dead"; break;	
				case 125: sWords = "an ancient evil dwelling below Castle British"; break;	
				case 126: sWords = "a necromancer appearing out of the ever burning fire in Sosaria"; break;	
				case 127: sWords = "someone buried with great treasure in the graveyard in " + city; break;	
				case 128: sWords = "a demilich dwelling below " + city; break;	
				case 129: sWords = "some " + RandomThings.GetRandomJob() + " selling artifacts in " + city; break;	
				case 130: sWords = "someone who killed the " + RandomThings.GetRandomJob() + " in " + city; break;	
				case 131: sWords = "a clan of orcs that slowly mutated over the centuries"; break;	
				case 132: sWords = "sailors exploring a reef in the Isles of Dread"; break;	
				case 133: sWords = "some necromancers practicing black magic deep below the castle"; break;	
				case 134: sWords = "a brass tower appearing in Umber Veil"; break;	
				case 135: sWords = "an orc tribe that discovered the lost silver mines"; break;	
				case 136: sWords = "an abandoned castle of Stonegate, because all inside were slain"; break;	
				case 137: sWords = "some Shadowlords that took over the castle of Stonegate"; break;	
				case 138: sWords = "a cyclops warlord seeking silver to forge weapons for his army"; break;	
				case 139: sWords = "an evil knight that has the skull of Mondain"; break;	
				case 140: sWords = "a vile wizard that has the gem of immortality"; break;	
				case 141: sWords = "an ancient book of magic buried in " + dungeon; break;	
				case 142: sWords = "a wizard that sails the Isles of Dread, selling rare spells"; break;	
				case 143: sWords = "a blacksmith in " + city + " that makes weapons out of mithril"; break;	
				case 144: sWords = "Zorn living in " + dungeon; break;	
				case 145: sWords = "a black sword resting in " + dungeon; break;	
				case 146: sWords = "some " + adventurer + " that was killed by a cyclops' eye"; break;	
				case 147: sWords = "some " + adventurer + " that had a tinker in " + city + " make a golem with a dark core"; break;	
				case 148: sWords = "titans that drop lightning from the sky"; break;	
				case 149: sWords = "some " + adventurer + " that was killed by elemental grues"; break;	
				case 150: sWords = "an ancient wyrm guarding the way to the Hidden Valley"; break;	
				case 151: sWords = "a mad wizard acting as a high priest of Kazibal"; break;	
				case 152: sWords = "an island mansion where Azerok is said to still live"; break;	
				case 153: sWords = "a hidden cave below the Forgotten Lighthouse"; break;	
				case 154: sWords = GetRareLocation( from, false, true ); if ( from is HouseVisitor ){ sWords = "an artifact merchant in " + city + ""; } break;	
				case 155: sWords = "a chatty mouse in the castle that likes cheese"; break;	
				case 156: sWords = "a moonstone that can summon a moongate from almost anywhere"; break;	
				case 157: sWords = "a group of miners saying that Morinia is one of the best mines for ore"; break;	
				case 158: sWords = "some crystals being in the Morinia mines"; break;	
				case 159: sWords = "a legendary miner that dug up dwarven ore"; break;	
				case 160: sWords = "a legendary lumberjack that chopped up elven wood"; break;	
				case 161: sWords = "some " + RandomThings.GetRandomJob() + " solving the mystery of the Skull Gate"; break;	
				case 162: sWords = "some " + RandomThings.GetRandomJob() + " solving the mystery of the Serpent Pillars"; break;	
				case 163: 
					misc = "tomb";	
					switch( Utility.RandomMinMax( 0, 4 ) )
					{
						case 1: misc = "crypt"; break;	
						case 2: misc = "treasure"; break;	
						case 3: misc = "artifact"; break;	
						case 4: misc = "remains"; break;	
					}
					sWords = "a " + misc + " of " + RandomThings.GetRandomName() + " in " + dungeon + ""; break;	
				case 164:
					misc = "map";	
					switch( Utility.RandomMinMax( 0, 4 ) )
					{
						case 1: misc = "tablet"; break;	
						case 2: misc = "scroll"; break;	
						case 3: misc = "book"; break;	
						case 4: misc = "clue"; break;	
					}
					sWords = "a " + misc + " that leads to " + dungeon + ""; break;	
				case 165:
					misc = "map";	
					switch( Utility.RandomMinMax( 0, 4 ) )
					{
						case 1: misc = "tablet"; break;	
						case 2: misc = "scroll"; break;	
						case 3: misc = "book"; break;	
						case 4: misc = "clue"; break;	
					}
					string misc2 = "gold";	
					switch( Utility.RandomMinMax( 0, 4 ) )
					{
						case 1: misc = "treasure"; break;	
						case 2: misc = "gems"; break;	
						case 3: misc = "jewels"; break;	
						case 4: misc = "riches"; break;	
					}
					sWords = "a " + misc + " that leads to the " + misc2 + " of " + RandomThings.GetRandomName() + ""; break;	
				case 166: 
					misc = "n artifact";	
					switch( Utility.RandomMinMax( 0, 4 ) )
					{
						case 1: misc = "relic"; break;	
						case 2: misc = "magic item"; break;	
						case 3: misc = "n ancient artifact"; break;	
						case 4: misc = "n ancient relic"; break;	
					}
					sWords = "a" + misc + " called " + QuestCharacters.QuestItems( false ) + " lost in " + dungeon + ""; break;	
				case 167: 
					misc = "destroyed";	
					switch( Utility.RandomMinMax( 0, 3 ) )
					{
						case 1: misc = "ruined"; break;	
						case 2: misc = "devastated"; break;	
						case 3: misc = "lost"; break;	
					}
					sWords = "legends of " + RandomThings.MadeUpCity() + " being " + misc + " during " + RandomThings.GetRandomDisaster() + ""; break;	
				case 168: 
					misc = "joined";	
					switch( Utility.RandomMinMax( 0, 4 ) )
					{
						case 1: misc = "left"; break;	
						case 2: misc = "betrayed"; break;	
						case 3: misc = "destroyed"; break;	
						case 4: misc = "started"; break;	
					}
					sWords = "a " + RandomThings.GetBoyGirlJob( Utility.RandomMinMax( 0, 1 ) ) + " that " + misc + " " + RandomThings.GetRandomSociety() + ""; break;	
				case 169: 
					misc = "robbed";	
					switch( Utility.RandomMinMax( 0, 5 ) )
					{
						case 1: misc = "killed"; break;	
						case 2: misc = "lost"; break;	
						case 3: misc = "slain"; break;	
						case 4: misc = "arrested"; break;	
						case 5: misc = "kidnapped"; break;	
					}
					sWords = "a " + RandomThings.GetBoyGirlJob( Utility.RandomMinMax( 0, 1 ) ) + " that was " + misc + " on the way to " + RandomThings.MadeUpCity() + ""; break;	
			}
			return sWords;	
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static string Adventurer()
		{
			string sAdventurer = "bandit";	
			switch( Utility.RandomMinMax( 0, 56 ) )
			{
				case 0: sAdventurer = "adventurer"; break;	
				case 1: sAdventurer = "bandit"; break;	
				case 2: sAdventurer = "barbarian"; break;	
				case 3: sAdventurer = "bard"; break;	
				case 4: sAdventurer = "baron"; break;	
				case 5: sAdventurer = "baroness"; break;	
				case 6: sAdventurer = "cavalier"; break;	
				case 7: sAdventurer = "cleric"; break;	
				case 8: sAdventurer = "conjurer"; break;	
				case 9: sAdventurer = "defender"; break;	
				case 10: sAdventurer = "diviner"; break;	
				case 11: sAdventurer = "enchanter"; break;	
				case 12: sAdventurer = "enchantress"; break;	
				case 13: sAdventurer = "explorer"; break;	
				case 14: sAdventurer = "fighter"; break;	
				case 15: sAdventurer = "gladiator"; break;	
				case 16: sAdventurer = "heretic"; break;	
				case 17: sAdventurer = "hunter"; break;	
				case 18: sAdventurer = "illusionist"; break;	
				case 19: sAdventurer = "invoker"; break;	
				case 20: sAdventurer = "king"; break;	
				case 21: sAdventurer = "knight"; break;	
				case 22: sAdventurer = "lady"; break;	
				case 23: sAdventurer = "lord"; break;	
				case 24: sAdventurer = "mage"; break;	
				case 25: sAdventurer = "magician"; break;	
				case 26: sAdventurer = "mercenary"; break;	
				case 27: sAdventurer = "minstrel"; break;	
				case 28: sAdventurer = "monk"; break;	
				case 29: sAdventurer = "mystic"; break;	
				case 30: sAdventurer = "necromancer"; break;	
				case 31: sAdventurer = "outlaw"; break;	
				case 32: sAdventurer = "paladin"; break;	
				case 33: sAdventurer = "priest"; break;	
				case 34: sAdventurer = "priestess"; break;	
				case 35: sAdventurer = "prince"; break;	
				case 36: sAdventurer = "princess"; break;	
				case 37: sAdventurer = "prophet"; break;	
				case 38: sAdventurer = "queen"; break;	
				case 39: sAdventurer = "ranger"; break;	
				case 40: sAdventurer = "rogue"; break;	
				case 41: sAdventurer = "sage"; break;	
				case 42: sAdventurer = "scout"; break;	
				case 43: sAdventurer = "seeker"; break;	
				case 44: sAdventurer = "seer"; break;	
				case 45: sAdventurer = "shaman"; break;	
				case 46: sAdventurer = "slayer"; break;	
				case 47: sAdventurer = "sorcerer"; break;	
				case 48: sAdventurer = "sorceress"; break;	
				case 49: sAdventurer = "summoner"; break;	
				case 50: sAdventurer = "templar"; break;	
				case 51: sAdventurer = "thief"; break;	
				case 52: sAdventurer = "traveler"; break;	
				case 53: sAdventurer = "warlock"; break;	
				case 54: sAdventurer = "warrior"; break;	
				case 55: sAdventurer = "witch"; break;	
				case 56: sAdventurer = "wizard"; break;	
			}
			return sAdventurer;	
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void GetChatter( Mobile patron )
		{
			string cVal = "";	
			string cDun = "";	
			string act = "";	

			string sThey = "Samson";	
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ sThey = NameList.RandomName( "female" ); } else { sThey = NameList.RandomName( "male" ); }

			string city = RandomThings.GetRandomCity();	
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ city = RandomThings.MadeUpCity(); }

			string dungeon = QuestCharacters.SomePlace( "tavern" );	
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ dungeon = RandomThings.MadeUpDungeon(); }

			string sAdventurer = Adventurer();	

			string sMoney = "gold";	
			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: sMoney = "silver"; break;	
				case 1: sMoney = "copper"; break;	
				case 2: sMoney = "jewels"; break;	
			}

			string sDebt = "from that card game";	
			switch( Utility.RandomMinMax( 0, 19 ) )
			{
				case 0: sDebt = "from that bet"; break;	
				case 1: sDebt = "for that artifact"; break;	
				case 2: sDebt = "from that card game"; break;	
				case 3: sDebt = "from that dart game"; break;	
				case 4: sDebt = "for that horse"; break;	
				case 5: sDebt = "for that potion"; break;	
				case 6: sDebt = "for that weapon"; break;	
				case 7: sDebt = "for that armor"; break;	
				case 8: sDebt = "for releasing him"; break;	
				case 9: sDebt = "for finding that item"; break;	
				case 10: sDebt = "for solving that riddle"; break;	
				case 11: sDebt = "for digging up that treasure"; break;	
				case 12: sDebt = "for that gem"; break;	
				case 13: sDebt = "for that wand"; break;	
				case 14: sDebt = "for that staff"; break;	
				case 15: sDebt = "for fixing that thing"; break;	
				case 16: sDebt = "for killing that monster"; break;	
				case 17: sDebt = "for stealing that thing"; break;	
				case 18: sDebt = "for hiding them in my house"; break;	
				case 19: sDebt = "for that map"; break;	
			}

			int relic = Utility.RandomMinMax( 1, 59 );	

			int CommonTalkingCount = 47;	
			string sSpeech = "We are supposed to wait for " + sThey + ".";	
			switch( Utility.RandomMinMax( 1, CommonTalkingCount ) )
			{
				case 1: sSpeech = "We are supposed to wait for " + sThey + "."; break;	
				case 2: sSpeech = sThey + " lives somewhere near " + city + "."; break;	
				case 3: sSpeech = "We will go find " + sThey + " tomorrow."; break;	
				case 4: sSpeech = "We need to find a bank and split this loot we have."; break;	
				case 5: sSpeech = sThey + " still owes me " + Utility.RandomMinMax( 5, 200 ) + " " + sMoney + " " + sDebt + "."; break;	
				case 6:
					cVal = "sleeping";	
					switch( Utility.RandomMinMax( 0, 8 ) )
					{
						case 1: cVal = "drinking"; break;	
						case 2: cVal = "eating"; break;	
						case 3: cVal = "distracted"; break;	
						case 4: cVal = "searching"; break;	
						case 5: cVal = "lost"; break;	
						case 6: cVal = "gone"; break;	
						case 7: cVal = "exploring"; break;	
						case 8: cVal = "drunk"; break;	
					}
					sSpeech = "I think " + sThey + " stole it while we were " + cVal + "."; break;	
				case 7: sSpeech = sThey + " will bring it here when they find it."; break;	
				case 8:
					cVal = "Do you know";	
					switch( Utility.RandomMinMax( 0, 9 ) )
					{
						case 1: cVal = "Where did you meet"; break;	
						case 2: cVal = "Where did you see"; break;	
						case 3: cVal = "When did you meet"; break;	
						case 4: cVal = "When did you see"; break;	
						case 5: cVal = "When have you last heard from"; break;	
						case 6: cVal = "When did you kill"; break;	
						case 7: cVal = "Where did you kill"; break;	
						case 8: cVal = "When will I meet"; break;	
						case 9: cVal = "When will we meet"; break;	
					}
					sSpeech = sSpeech = " " + sThey + "?"; break;	
				case 9: sSpeech = sThey + " sold " + QuestCharacters.QuestItems( false ) + " for " + Utility.RandomMinMax( 5, 200 ) + " " + sMoney + "."; break;	
				case 10: sSpeech = "I paid " + sThey + " " + Utility.RandomMinMax( 5, 200 ) + " " + sMoney + " for " + QuestCharacters.QuestItems( false ) + "."; break;	
				case 11:
					cVal = "destroyed";	
					switch( Utility.RandomMinMax( 0, 6 ) )
					{
						case 1: cVal = "sold"; break;	
						case 2: cVal = "lost"; break;	
						case 3: cVal = "found"; break;	
						case 4: cVal = "discovered"; break;	
						case 5: cVal = "traded"; break;	
						case 6: cVal = "stole"; break;	
					}
					sSpeech = sThey + " " + cVal + " " + QuestCharacters.QuestItems( false ) + "."; break;	
				case 12:
					cVal = "robbed";	
					switch( Utility.RandomMinMax( 0, 6 ) )
					{
						case 1: cVal = "assassinated"; break;	
						case 2: cVal = "betrayed"; break;	
						case 3: cVal = "captured"; break;	
						case 4: cVal = "fooled"; break;	
						case 5: cVal = "killed"; break;	
						case 6: cVal = "swindled"; break;	
					}
					sSpeech = sThey + " " + cVal + " them, I just know it."; break;	
				case 13:
					cVal = "bought that from";	
					switch( Utility.RandomMinMax( 0, 8 ) )
					{
						case 1: cVal = "stole that from"; break;	
						case 2: cVal = "sold that to"; break;	
						case 3: cVal = "met with"; break;	
						case 4: cVal = "kidnapped"; break;	
						case 5: cVal = "robbed"; break;	
						case 6: cVal = "works for"; break;	
						case 7: cVal = "lives with"; break;	
						case 8: cVal = "owes " + Utility.RandomMinMax( 5, 200 ) + " gold to"; break;	
					}
					sSpeech = sThey + " " + cVal + " a " + RandomThings.GetRandomJob() + " in " + city + "."; break;	
				case 14:
					act = "robbed";	
					switch( Utility.RandomMinMax( 0, 9 ) )
					{
						case 1: act = "assassinated"; break;	
						case 2: act = "betrayed"; break;	
						case 3: act = "captured"; break;	
						case 4: act = "met"; break;	
						case 5: act = "killed"; break;	
						case 6: act = "left"; break;	
						case 7: act = "followed"; break;	
						case 8: act = "served"; break;	
						case 9: act = "arrested"; break;	
					}
					cVal = NameList.RandomName( "female" );	
					if ( Utility.RandomBool() ){ cVal = NameList.RandomName( "male" ); }
					string scene = city;	
					if ( Utility.RandomBool() ){ scene = dungeon; }
					sSpeech = sThey + " " + act + " " + cVal + " in " + scene + "."; break;	
				case 15:
					cVal = "executed";	
					switch( Utility.RandomMinMax( 0, 8 ) )
					{
						case 1: cVal = "jailed"; break;	
						case 2: cVal = "arrested"; break;	
						case 3: cVal = "captured"; break;	
						case 4: cVal = "banished"; break;	
						case 5: cVal = "rewarded"; break;	
						case 6: cVal = "celebrated"; break;	
						case 7: cVal = "promoted"; break;	
						case 8: cVal = "released"; break;	
					}
					sSpeech = sThey + " was " + cVal + " for killing that " + RandomThings.GetRandomJob() + " in " + city + "."; break;	
				case 16: sSpeech = "I heard " + sThey + " became a " + RandomThings.GetRandomJob() + " in " + city + "."; break;	
				case 17: sSpeech = "I need to see the " + RandomThings.GetRandomJob() + " before we travel on."; break;	
				case 18: sSpeech = sThey + " retired and became a " + RandomThings.GetRandomJob() + " in " + city + "."; break;	
				case 19: sSpeech = sThey + " was imprisoned for stealing from the " + RandomThings.GetRandomJob() + " in " + city + "."; break;	
				case 20:
					string item20 = Server.Items.SomeRandomNote.GetSpecialItem( relic, 1 );		if ( patron is HouseVisitor ){ item20 = QuestCharacters.QuestItems( false ); }
					string place20 = Server.Items.SomeRandomNote.GetSpecialItem( relic, 0 );	if ( patron is HouseVisitor ){ place20 = dungeon; }
					sSpeech = "I finally learned how we can get the " + item20 + ". We need to assemble the others and meet at " + place20 + "."; break;	
				case 21:
					string item21 = Server.Items.SomeRandomNote.GetSpecialItem( relic, 1 );		if ( patron is HouseVisitor ){ item21 = QuestCharacters.QuestItems( false ); }
					string place21 = Server.Items.SomeRandomNote.GetSpecialItem( relic, 0 );	if ( patron is HouseVisitor ){ place21 = dungeon; }
					sSpeech = "We need to go to " + place21 + " if we are going to obtain the " + item21 + " for " + QuestCharacters.RandomWords() + "."; break;	
				case 22:
					string item22 = Server.Items.SomeRandomNote.GetSpecialItem( relic, 1 );		if ( patron is HouseVisitor ){ item22 = QuestCharacters.QuestItems( false ); }
					string place22 = Server.Items.SomeRandomNote.GetSpecialItem( relic, 0 );	if ( patron is HouseVisitor ){ place22 = dungeon; }
					sSpeech = "The " + RandomThings.GetRandomJob() + " in " + city + " told me that we can probably get the " + item22 + " if we search " + place22 + "."; break;
				case 23: sSpeech = GetRareLocation( patron, false, false ); if ( patron is HouseVisitor ){ sSpeech = "We need to go to " + city + " if we are going to find the " + QuestCharacters.QuestItems( false ) + "."; } break;	
				case 24: sSpeech = sThey + " has been selling body parts to the black magic guild."; break;	
				case 25: sSpeech = sThey + " sold that monster skull to the necromancers for " + Utility.RandomMinMax( 50, 200 ) + " gold."; break;	
				case 26: sSpeech = "We will search for the " + Server.Misc.RandomThings.GetRandomColorName( 0 ) + " " + RandomThings.GetRandomThing( 0 ) + " tomorrow."; break;	
				case 27: sSpeech = "The " + RandomThings.GetRandomJob() + " in " + RandomThings.MadeUpCity() + " is looking for some help with " + RandomThings.GetRandomMonsters() + "."; break;	
				case 28: sSpeech = RandomThings.GetRandomShipName( "", 0 ) + " sank off the coast of the " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom() + "."; break;	
				case 29:
					cVal = RandomThings.MadeUpDungeon();	
					switch( Utility.RandomMinMax( 0, 1 ) )
					{
						case 1: cVal = RandomThings.MadeUpCity(); break;	
					}
					sSpeech = "I found a map that leads to " + cVal + "."; break;	
				case 30:
					cVal = "attack";	
					switch( Utility.RandomMinMax( 0, 5 ) )
					{
						case 1: cVal = "destroy"; break;	
						case 2: cVal = "invade"; break;	
						case 3: cVal = "war with"; break;	
						case 4: cVal = "be defeated by"; break;	
						case 5: cVal = "be attacked by"; break;	
					}
					sSpeech = "The " + RandomThings.GetRandomTroops() + " are going to " + cVal + " the " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom() + "."; break;	
				case 31:
					cVal = "tower";	
					switch( Utility.RandomMinMax( 0, 5 ) )
					{
						case 1: cVal = "castle"; break;	
						case 2: cVal = "mansion"; break;	
						case 3: cVal = "keep"; break;	
						case 4: cVal = "home"; break;	
						case 5: cVal = "cabin"; break;	
					}
					sSpeech = "We should build that " + cVal + " in the " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom() + "."; break;	
				case 32:
					cVal = RandomThings.MadeUpDungeon();	
					switch( Utility.RandomMinMax( 0, 1 ) )
					{
						case 1: cVal = RandomThings.MadeUpCity(); break;	
					}
					sSpeech = "We need to get to " + cVal + " before " + sThey + " does."; break;	
				case 33: sSpeech = "The " + RandomThings.GetRandomJob() + " in " + RandomThings.MadeUpCity() + " has " + QuestCharacters.QuestItems( false ) + " for sale."; break;	
				case 34: sSpeech = "The " + RandomThings.GetRandomNoble() + " is offering gold to rid the " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom() + " of " + RandomThings.GetRandomAttackers() + "."; break;	
				case 35:
					cVal = RandomThings.MadeUpDungeon();	
					switch( Utility.RandomMinMax( 0, 1 ) )
					{
						case 1: cVal = QuestCharacters.SomePlace( "" ); break;	
					}
					sSpeech = "I think we got the most treasure out of " + cVal + "."; break;	
				case 36:
					cVal = "robbed";	
					switch( Utility.RandomMinMax( 0, 9 ) )
					{
						case 1: cVal = "assassinated"; break;	
						case 2: cVal = "met"; break;	
						case 3: cVal = "spied on"; break;	
						case 4: cVal = "betrayed"; break;	
						case 5: cVal = "swore allegiance to"; break;	
						case 6: cVal = "serves"; break;	
						case 7: cVal = "was jailed by"; break;	
						case 8: cVal = "was killed by"; break;	
						case 9: cVal = "killed"; break;	
					}
					sSpeech = sThey + " " + cVal + " the " + RandomThings.GetRandomNoble() + " in " + RandomThings.MadeUpCity() + "."; break;	
				case 37: sSpeech = "Some " + RandomThings.GetRandomNoble() + " will pay us " + RandomThings.GetRandomCoinReward() + " gold if we find them " + QuestCharacters.QuestItems( false ) + "."; break;
				case 38: sSpeech = "There is a bounty of  " + RandomThings.GetRandomCoinReward() + " gold for " + sThey + " the " + RandomThings.GetBoyGirlJob( Utility.RandomMinMax( 0, 1 ) ) + "."; break;	
				case 39: sSpeech = "The " + RandomThings.GetBoyGirlJob( Utility.RandomMinMax( 0, 1 ) ) + " said for great treasure we need to go to " + RandomThings.MadeUpDungeon() + "."; break;	
				case 40:
					cVal = "hid";	
					switch( Utility.RandomMinMax( 0, 6 ) )
					{
						case 1: cVal = "lost"; break;	
						case 2: cVal = "left"; break;	
						case 3: cVal = "hidden"; break;	
						case 4: cVal = "found"; break;	
						case 5: cVal = "discovered"; break;	
						case 6: cVal = "created"; break;	
					}
					sSpeech = sThey + " " + cVal + " " + QuestCharacters.QuestItems( false ) + " deep in " + RandomThings.MadeUpDungeon() + "."; break;	
				case 41:
					cVal = RandomThings.MadeUpDungeon();	
					string portal = "mirror";	
					if ( Utility.RandomBool() ){ cVal = QuestCharacters.SomePlace( "" ); }
					if ( Utility.RandomBool() ){ portal = "portal"; }
					sSpeech = sThey + " found a magic " + portal + " that led to " + cVal + "."; break;	
				case 42:
					cVal = "all of their coins turned to lead";	
					switch( Utility.RandomMinMax( 0, 13 ) )
					{
						case 1: cVal = "all of their meager coins turned to gold"; break;	
						case 2: cVal = "they became much stronger"; break;	
						case 3: cVal = "they became much quicker"; break;	
						case 4: cVal = "they became more intelligent"; break;	
						case 5: cVal = "they became much weaker"; break;	
						case 6: cVal = "they became much less nimble"; break;	
						case 7: cVal = "they lost their mind"; break;	
						case 8: cVal = "water elementals spewed forth"; break;	
						case 9: cVal = "they saw a great treasure box within it"; break;	
						case 10: cVal = "they died from the poison"; break;	
						case 11: cVal = "they were magically healed"; break;	
						case 12: cVal = "they were cured of the poison"; break;	
						case 13: cVal = "their " + Server.Items.SomeRandomNote.GetSpecialItem( relic, 1 ) + " vanished"; break;	
					}
					cDun = RandomThings.MadeUpDungeon();	
					if ( Utility.RandomBool() ){ cDun = QuestCharacters.SomePlace( "" ); }
					sSpeech = sThey + " drank from a strange pool in " + cDun + " and " + cVal + "."; break;	
				case 43:
					cVal = "a pit trap";	
					switch( Utility.RandomMinMax( 0, 8 ) )
					{
						case 1: cVal = "a spike trap"; break;	
						case 2: cVal = "a flame trap"; break;	
						case 3: cVal = "an explosion trap"; break;	
						case 4: cVal = "a poison gas trap"; break;	
						case 5: cVal = "an exploding mushroom"; break;	
						case 6: cVal = "a saw blade trap"; break;	
						case 7: cVal = "a fiery stone face trap"; break;	
						case 8: cVal = "a magic trap"; break;	
					}
					cDun = RandomThings.MadeUpDungeon();	
					if ( Utility.RandomBool() ){ cDun = QuestCharacters.SomePlace( "" ); }
					sSpeech = sThey + " died in " + cDun + " from " + cVal + "."; break;	
				case 44:
					cVal = "has fallen to";	
					switch( Utility.RandomMinMax( 0, 8 ) )
					{
						case 1: cVal = "was attacked by"; break;	
						case 2: cVal = "was invaded by"; break;	
						case 3: cVal = "was destroyed by"; break;	
						case 4: cVal = "was defeated by"; break;	
						case 5: cVal = "has surrendered to"; break;	
						case 6: cVal = "won against"; break;	
						case 7: cVal = "has defeated"; break;	
						case 8: cVal = "has slain the army of"; break;	
					}
					sSpeech = "The " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom() + " " + cVal + " the " + RandomThings.GetRandomTroops() + "."; break;	
				case 45:
					cVal = "killed";	
					switch( Utility.RandomMinMax( 0, 5 ) )
					{
						case 1: cVal = "slain"; break;	
						case 2: cVal = "defeated"; break;	
						case 3: cVal = "almost killed"; break;	
						case 4: cVal = "almost slain"; break;	
						case 5: cVal = "almost defeated"; break;	
					}
					sSpeech = sThey + " was " + cVal + " by " + RandomThings.GetRandomMonsters() + " in " + RandomThings.MadeUpDungeon() + "."; break;
				case 46:
					string dIrc = "Let me tell you";
						if ( Utility.RandomBool() ){ dIrc = "Tell me"; }

					cVal = "tale";	
					switch( Utility.RandomMinMax( 0, 4 ) )
					{
						case 1: cVal = "story"; break;	
						case 2: cVal = "fable"; break;	
						case 3: cVal = "legend"; break;	
						case 4: cVal = "myth"; break;	
					}

					sSpeech = dIrc + " the " + cVal + " of " + QuestCharacters.QuestItems( false ) + ".";

					switch( Utility.RandomMinMax( 0, 5 ) )
					{
						case 1: sSpeech = dIrc + " the " + cVal + " of the " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom() + "."; break;	
						case 2: sSpeech = dIrc + " the " + cVal + " of " + RandomThings.MadeUpDungeon() + "."; break;	
						case 3: sSpeech = dIrc + " the " + cVal + " of the " + RandomThings.GetRandomJobTitle(0) + " and the " + RandomThings.GetRandomThing(0) + "."; break;	
						case 4: sSpeech = dIrc + " the " + cVal + " of " + RandomThings.GetRandomColorName(0) + " " + RandomThings.GetRandomThing(0) + "."; break;	
						case 5: sSpeech = dIrc + " the " + cVal + " of the " + RandomThings.GetRandomJobTitle(0) + " and the " + RandomThings.GetRandomCreature() + "."; break;	
					}
					break;
				case 47:
					cVal = "searching for";	
					switch( Utility.RandomMinMax( 0, 3 ) )
					{
						case 1: cVal = "looking for"; break;	
						case 2: cVal = "trying to find"; break;	
						case 3: cVal = "trying to locate"; break;	
					}

					string goal = "the Codex of Ultimate Wisdom";	
					switch( Utility.RandomMinMax( 0, 24 ) )
					{
						case 1: goal = "the Dark Core of Exodus";	 	break;	
						case 2: goal = QuestCharacters.QuestItems( false );	break;	
						case 3: goal = "the Staff of Five Parts";	break;	
						case 4: goal = "Mangar the Dark";	break;	
						case 5: goal = "the Runes of Virtue";	break;	
						case 6: goal = "the Book of Truth";	break;	
						case 7: goal = "the Bell of Courage";	break;	
						case 8: goal = "the Candle of Love";	break;	
						case 9: goal = "the Scales of Ethicality";	break;	
						case 10: goal = "the Orb of Logic";	break;	
						case 11: goal = "the Lantern of Discipline";	break;	
						case 12: goal = "the Breath of Air";	break;	
						case 13: goal = "the Tongue of Flame";	break;	
						case 14: goal = "the Heart of Earth";	break;	
						case 15: goal = "the Tear of the Seas";	break;	
						case 16: goal = "the Statue of Gygax";	break;	
						case 17: goal = "the Skull of Baron Almric";	break;	
						case 18: goal = "the Shard of Cowardice";	break;	
						case 19: goal = "the Shard of Falsehood";	break;	
						case 20: goal = "the Shard of Hatred";	break;	
						case 21: goal = "the Gem of Immortality";	break;	
						case 22: goal = "the Manual of Golems";	break;	
						case 23: goal = "Frankenstein's Journal";	break;	
						case 24: goal = "the Vortex Cube";	break;	
					}

					string fate = "died";	
					switch( Utility.RandomMinMax( 0, 6 ) )
					{
						case 1: fate = "went missing";	 			break;	
						case 2: fate = "has been";	 				break;	
						case 3: fate = "almost died";	 			break;	
						case 4: fate = "never returned while";	 	break;	
						case 5: fate = "vanished";	 				break;	
						case 6: fate = "perished";	 				break;	
					}

					sSpeech = sThey + " " + fate + " " + cVal + " " + goal + ".";

					break;
			}

			string sGossip = sSpeech;	

			switch( Utility.RandomMinMax( 1, ( 11 + CommonTalkingCount ) ) )
			{
				case 1: sGossip = "Another ale over here!"; break;	
				case 2: sGossip = "More wine!"; break;	
				case 3: sGossip = "Can I get another mug over here?"; break;	
				case 4: sGossip = "What does it take to get a good drink in this place?"; break;	
				case 5: sGossip = sThey + " said this is the best place to drink."; break;	
				case 6: sGossip = sThey + " lives around here somewhere."; break;	
				case 7: sGossip = "Raise a mug to " + sThey + ", as we will not forget them."; break;	
				case 8: sGossip = "We should eat while we are here."; break;	
				case 9: sGossip = "this is some good wine."; break;	
				case 10: sGossip = "I never had ale quite like this."; break;	
				case 11: sGossip = "I am starting to think they water down the drinks."; break;	
			}

			string sTent = sSpeech;	
			switch( Utility.RandomMinMax( 1, ( 5 + CommonTalkingCount ) ) )
			{
				case 1: sTent = sThey + " said this is the safest place to camp."; break;	
				case 2: sTent = "Raise a mug to " + sThey + ", as we will not forget them."; break;	
				case 3: sTent = "We should eat while we are resting here."; break;	
				case 4: sTent = "this is some good wine you brought."; break;	
				case 5: sTent = "I never had ale quite like this."; break;	
			}

			string sCitizen = sSpeech;	
			switch( Utility.RandomMinMax( 1, ( 2 + CommonTalkingCount ) ) )
			{
				case 1: sCitizen = sThey + " said this is the safest place to stay."; break;	
				case 2: sCitizen = sThey + " lives somewhere near " + city + "."; break;	
			}

			string sHappen = "A friend of mine died"; string sEnd = ".";	
			switch( Utility.RandomMinMax( 0, 35 ) )
			{
				case 0: sHappen = "A friend of mine was lost in"; sEnd = "."; break;	
				case 1: sHappen = "A friend of mine died in"; sEnd = "."; break;	
				case 2: sHappen = "I lost that weapon in"; sEnd = "."; break;	
				case 3: sHappen = "Have you ever been to"; sEnd = "?"; break;	
				case 4: sHappen = "Have you ever heard of"; sEnd = "?"; break;	
				case 5: sHappen = "When did you go to"; sEnd = "?"; break;	
				case 6: sHappen = "How did you get to"; sEnd = "?"; break;	
				case 7: sHappen = "Why did you go to"; sEnd = "?"; break;	
				case 8: sHappen = "What did you find in"; sEnd = "?"; break;	
				case 9: sHappen = "You found that in"; sEnd = "?"; break;	
				case 10: sHappen = "They died in"; sEnd = "."; break;	
				case 11: sHappen = "I have never been to"; sEnd = "."; break;	
				case 12: sHappen = "That artifact came from"; sEnd = "."; break;	
				case 13: sHappen = "They got lost in"; sEnd = "."; break;	
				case 14: sHappen = "They vanished in"; sEnd = "."; break;	
				case 15: sHappen = "I almost didn't make it out of"; sEnd = "."; break;	
				case 16: sHappen = "They didn't make it out of"; sEnd = "."; break;	
				case 17: sHappen = "I lost that magic item in"; sEnd = "."; break;	
				case 18: sHappen = "Did you lose it in"; sEnd = "?"; break;	
				case 19: sHappen = "We should go search in"; sEnd = "."; break;	
				case 20: sHappen = "We should go explore in"; sEnd = "."; break;	
				case 21: sHappen = "Tonight we will go to"; sEnd = "."; break;	
				case 22: sHappen = sThey + " was lost in"; sEnd = "."; break;	
				case 23: sHappen = sThey + " died in"; sEnd = "."; break;	
				case 24: sHappen = sThey + " lost that weapon in"; sEnd = "."; break;	
				case 25: sHappen = "When did " + sThey + " go to"; sEnd = "?"; break;	
				case 26: sHappen = "How did " + sThey + " get to"; sEnd = "?"; break;	
				case 27: sHappen = "Why did " + sThey + " go to"; sEnd = "?"; break;	
				case 28: sHappen = "What did " + sThey + " find in"; sEnd = "?"; break;	
				case 29: sHappen = sThey + " found that in"; sEnd = "?"; break;	
				case 30: sHappen = sThey + " has never been to"; sEnd = "."; break;	
				case 31: sHappen = sThey + " vanished in"; sEnd = "."; break;	
				case 32: sHappen = sThey + " almost didn't make it out of"; sEnd = "."; break;	
				case 33: sHappen = sThey + " didn't make it out of"; sEnd = "."; break;	
				case 34: sHappen = sThey + " lost that magic item in"; sEnd = "."; break;	
				case 35: sHappen = "Did " + sThey + " lose it in"; sEnd = "?"; break;	
			}

			string sEvent = sHappen + " " + dungeon + sEnd;	

			string sWords = CommonTalk( "", city, dungeon, patron, sAdventurer, false );	

			int LogReader = 0;	
			if ( sWords == "" )
			{
				sWords = Server.Misc.LoggingFunctions.LogSpeak();	
				LogReader = 1;	
				if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ sWords = Server.Misc.LoggingFunctions.LogSpeakQuest(); LogReader = 2; }
			}

			string sJob = sThey;	
			switch( Utility.RandomMinMax( 0, 86 ) )
			{
				case 0: sJob = "An adventurer"; break;	
				case 1: sJob = "A bandit"; break;	
				case 2: sJob = "A barbarian"; break;	
				case 3: sJob = "A bard"; break;	
				case 4: sJob = "A baron"; break;	
				case 5: sJob = "A baroness"; break;	
				case 6: sJob = "A cavalier"; break;	
				case 7: sJob = "A cleric"; break;	
				case 8: sJob = "A conjurer"; break;	
				case 9: sJob = "A defender"; break;	
				case 10: sJob = "A diviner"; break;	
				case 11: sJob = "An enchanter"; break;	
				case 12: sJob = "A enchantress"; break;	
				case 13: sJob = "An explorer"; break;	
				case 14: sJob = "A fighter"; break;	
				case 15: sJob = "A gladiator"; break;	
				case 16: sJob = "A heretic"; break;	
				case 17: sJob = "A hunter"; break;	
				case 18: sJob = "An illusionist"; break;	
				case 19: sJob = "An invoker"; break;	
				case 20: sJob = "A king"; break;	
				case 21: sJob = "A knight"; break;	
				case 22: sJob = "A lady"; break;	
				case 23: sJob = "A lord"; break;	
				case 24: sJob = "A mage"; break;	
				case 25: sJob = "A magician"; break;	
				case 26: sJob = "A mercenary"; break;	
				case 27: sJob = "A minstrel"; break;	
				case 28: sJob = "A monk"; break;	
				case 29: sJob = "A mystic"; break;	
				case 30: sJob = "A necromancer"; break;	
				case 31: sJob = "An outlaw"; break;	
				case 32: sJob = "A paladin"; break;	
				case 33: sJob = "A priest"; break;	
				case 34: sJob = "A priestess"; break;	
				case 35: sJob = "A prince"; break;	
				case 36: sJob = "A princess"; break;	
				case 37: sJob = "A prophet"; break;	
				case 38: sJob = "A queen"; break;	
				case 39: sJob = "A ranger"; break;	
				case 40: sJob = "A rogue"; break;	
				case 41: sJob = "A sage"; break;	
				case 42: sJob = "A scout"; break;	
				case 43: sJob = "A seeker"; break;	
				case 44: sJob = "A seer"; break;	
				case 45: sJob = "A shaman"; break;	
				case 46: sJob = "A slayer"; break;	
				case 47: sJob = "A sorcerer"; break;	
				case 48: sJob = "A sorceress"; break;	
				case 49: sJob = "A summoner"; break;	
				case 50: sJob = "A templar"; break;	
				case 51: sJob = "A thief"; break;	
				case 52: sJob = "A traveler"; break;	
				case 53: sJob = "A warlock"; break;	
				case 54: sJob = "A warrior"; break;	
				case 55: sJob = "A witch"; break;	
				case 56: sJob = "A wizard"; break;	
			}

			string sBuild1 = "I found"; string sBuild2 = ".";	

			if ( LogReader == 1 )
			{
				switch( Utility.RandomMinMax( 0, 11 ) )
				{
					case 0: sBuild1 = sJob + " heard of"; sBuild2 = "."; break;	
					case 1: sBuild1 = sJob + " tells of"; sBuild2 = "."; break;	
					case 2: sBuild1 = sJob + " is spreading rumours about"; sBuild2 = "."; break;	
					case 3: sBuild1 = sJob + " tells tales of"; sBuild2 = "."; break;	
					case 4: sBuild1 = sJob + " mentioned something about"; sBuild2 = "."; break;	
					case 5: sBuild1 = sJob + " heard rumours about"; sBuild2 = "."; break;	
					case 6: sBuild1 = "I found"; sBuild2 = "."; break;	
					case 7: sBuild1 = "I heard rumours about"; sBuild2 = "."; break;	
					case 8: sBuild1 = "I heard a story about"; sBuild2 = "."; break;	
					case 9: sBuild1 = "I overheard someone tell of"; sBuild2 = "."; break;	
					case 10: sBuild1 = "Were you saying something about"; sBuild2 = "?"; break;	
					case 11: sBuild1 = "Where did I hear about"; sBuild2 = "?"; break;	
				}
			}
			else if ( LogReader == 0 )
			{
				switch( Utility.RandomMinMax( 0, 13 ) )
				{
					case 0: sBuild1 = sJob + " found"; sBuild2 = "."; break;	
					case 1: sBuild1 = sJob + " tells of"; sBuild2 = "."; break;	
					case 2: sBuild1 = sJob + " is spreading rumours about"; sBuild2 = "."; break;	
					case 3: sBuild1 = sJob + " tells tales of"; sBuild2 = "."; break;	
					case 4: sBuild1 = sJob + " mentioned that there was"; sBuild2 = "."; break;	
					case 5: sBuild1 = sJob + " heard rumours about"; sBuild2 = "."; break;	
					case 6: sBuild1 = "I found"; sBuild2 = "."; break;	
					case 7: sBuild1 = "I heard rumours about"; sBuild2 = "."; break;	
					case 8: sBuild1 = "I heard a story about"; sBuild2 = "."; break;	
					case 9: sBuild1 = "I overheard someone tell of"; sBuild2 = "."; break;	
					case 10: sBuild1 = "Were you saying that there is"; sBuild2 = "?"; break;	
					case 11: sBuild1 = "Where did I hear that there is"; sBuild2 = "?"; break;	
					case 12: sBuild1 = "Are you telling me that there is"; sBuild2 = "?"; break;	
					case 13: sBuild1 = "Do you mean to say that there is"; sBuild2 = "?"; break;	
				}
			}

			string sPhrase = sBuild1 + " " + sWords + sBuild2;	

			if ( LogReader == 2 )
			{
				sPhrase = sWords + ".";	
			}

			Region reg = Region.Find( patron.Location, patron.Map );	

			int iWillSay = Utility.RandomMinMax( 1, 8 );	

			if ( iWillSay < 3 )
			{
				switch( Utility.RandomMinMax( 1, 39 ) )
				{
					case 1: patron.PlaySound( patron.Female ? 778 : 1049 ); patron.Say( "*ah!*" ); break;	
					case 2: patron.PlaySound( patron.Female ? 779 : 1050 ); patron.Say( "Ah ha!" ); break;	
					case 3: patron.PlaySound( patron.Female ? 780 : 1051 ); patron.Say( "*applauds*" ); break;	
					case 4: patron.PlaySound( patron.Female ? 781 : 1052 ); patron.Say( "*blows nose*" );	break;	
					case 5: patron.PlaySound( patron.Female ? 786 : 1057 ); patron.Say( "*cough*" ); break;	
					case 6: patron.PlaySound( patron.Female ? 782 : 1053 ); patron.Say( "*burp*" ); break;	
					case 7: patron.PlaySound( patron.Female ? 784 : 1055 ); patron.Say( "*clears throat*" ); break;	
					case 8: patron.PlaySound( patron.Female ? 785 : 1056 ); patron.Say( "*cough*" ); break;	
					case 9: patron.PlaySound( patron.Female ? 787 : 1058 ); patron.Say( "*cries*" ); break;	
					case 10: patron.PlaySound( patron.Female ? 792 : 1064 ); patron.Say( "*farts*" ); break;	
					case 11: patron.PlaySound( patron.Female ? 793 : 1065 ); patron.Say( "*gasp*" ); break;	
					case 12: patron.PlaySound( patron.Female ? 794 : 1066 ); patron.Say( "*giggles*" ); break;	
					case 13: patron.PlaySound( patron.Female ? 0x31B : 0x42B ); patron.Say( "*groans*" ); break;	
					case 14: patron.PlaySound( patron.Female ? 0x338 : 0x44A ); patron.Say( "*growls*" ); break;	
					case 15: patron.PlaySound( patron.Female ? 797 : 1069 ); patron.Say( "Hey!" ); break;	
					case 16: patron.PlaySound( patron.Female ? 798 : 1070 ); patron.Say( "*hiccup*" ); break;	
					case 17: patron.PlaySound( patron.Female ? 799 : 1071 ); patron.Say( "Huh?" ); break;	
					case 18: patron.PlaySound( patron.Female ? 801 : 1073 ); patron.Say( "*laughs*" ); break;	
					case 19: patron.PlaySound( patron.Female ? 802 : 1074 ); patron.Say( "No!" ); break;	
					case 20: patron.PlaySound( patron.Female ? 803 : 1075 ); patron.Say( "Oh!" ); break;	
					case 21: patron.PlaySound( patron.Female ? 811 : 1085 ); patron.Say( "Oooh." ); break;	
					case 22: patron.PlaySound( patron.Female ? 812 : 1086 ); patron.Say( "Oops!" ); break;	
					case 23: patron.PlaySound( patron.Female ? 0x32E : 0x440 ); patron.Say( "Ahhhh!" ); break;	
					case 24: patron.PlaySound( patron.Female ? 815 : 1089 ); patron.Say( "Shhh!" ); break;	
					case 25: patron.PlaySound( patron.Female ? 816 : 1090 ); patron.Say( "*sigh*" ); break;	
					case 26: patron.PlaySound( patron.Female ? 817 : 1091 ); patron.Say( "Ahh-choo!" ); break;	
					case 27: patron.PlaySound( patron.Female ? 818 : 1092 ); patron.Say( "*sniff*" ); break;	
					case 28: patron.PlaySound( patron.Female ? 819 : 1093 ); patron.Say( "*snore*" ); break;	
					case 29: patron.PlaySound( patron.Female ? 820 : 1094 ); patron.Say( "*spits*" ); break;	
					case 30: patron.PlaySound( patron.Female ? 821 : 1095 ); patron.Say( "*whistles*" ); break;	
					case 31: patron.PlaySound( patron.Female ? 783 : 1054 ); patron.Say( "Woohoo!" ); break;	
					case 32: patron.PlaySound( patron.Female ? 822 : 1096 ); patron.Say( "*yawns*" ); break;	
					case 33: patron.PlaySound( patron.Female ? 823 : 1097 ); patron.Say( "Yea!" ); break;	
					case 34: patron.PlaySound( patron.Female ? 0x31C : 0x42C ); patron.Say( "*yells*" ); break;	
					case 35: patron.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) ); break;	
					case 36: patron.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) ); break;	
					case 37: patron.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) ); break;	
					case 38: patron.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) ); break;	
					case 39: patron.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) ); break;	
				}
			}
			else if ( iWillSay < 5 ){ patron.Say( sPhrase ); }
			else if ( iWillSay < 7 ){ patron.Say( sEvent ); }
			else if ( reg.Name == "the Dungeon Room" || reg.Name == "the Camping Tent" ) { patron.Say( sTent ); }
			else if ( !( patron is TavernPatronNorth || patron is TavernPatronSouth || patron is TavernPatronEast || patron is TavernPatronWest ) ) { patron.Say( sCitizen ); }
			else { patron.Say( sGossip ); }
		}
	}
}