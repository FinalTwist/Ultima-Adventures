using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
    class RelicItems
    {
		public static string IdentifyRelicValue( Mobile examiner, Mobile customer, Item item )
		{
			int gold = 0;
			string buyer = "";
			string phrase = "";

			if ( item is DDRelicInstrument ){ gold = ((DDRelicInstrument)item).RelicGoldValue; buyer = "a bard"; }
			else if ( item is DDRelicAlchemy ){ gold = ((DDRelicAlchemy)item).RelicGoldValue; buyer = "an alchemist"; }
			else if ( item is DDRelicArts ){ gold = ((DDRelicArts)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicBanner ){ gold = ((DDRelicBanner)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicBearRugsAddon ){ gold = ((DDRelicBearRugsAddon)item).RelicGoldValue; buyer = "a fur trader"; }
			else if ( item is DDRelicBook ){ gold = ((DDRelicBook)item).RelicGoldValue; buyer = "a scribe"; }
			else if ( item is DDRelicClock1 ){ gold = ((DDRelicClock1)item).RelicGoldValue; buyer = "a tinker"; }
			else if ( item is DDRelicClock2 ){ gold = ((DDRelicClock2)item).RelicGoldValue; buyer = "a tinker"; }
			else if ( item is DDRelicClock3 ){ gold = ((DDRelicClock3)item).RelicGoldValue; buyer = "a tinker"; }
			else if ( item is DDRelicCloth ){ gold = ((DDRelicCloth)item).RelicGoldValue; buyer = "a tailor"; }
			else if ( item is DDRelicCoins ){ gold = ((DDRelicCoins)item).RelicGoldValue; buyer = "a minter"; }
			else if ( item is DDRelicDrink ){ gold = ((DDRelicDrink)item).RelicGoldValue; buyer = "a tavern keeper"; }
			else if ( item is DDRelicFur ){ gold = ((DDRelicFur)item).RelicGoldValue; buyer = "a fur trader"; }
			else if ( item is DDRelicGem ){ gold = ((DDRelicGem)item).RelicGoldValue; buyer = "a jeweler"; }
			else if ( item is DDRelicJewels ){ gold = ((DDRelicJewels)item).RelicGoldValue; buyer = "a jeweler"; }
			else if ( item is DDRelicLight1 ){ gold = ((DDRelicLight1)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicLight2 ){ gold = ((DDRelicLight2)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicLight3 ){ gold = ((DDRelicLight3)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicOrbs ){ gold = ((DDRelicOrbs)item).RelicGoldValue; buyer = "a mage"; }
			else if ( item is DDRelicPainting ){ gold = ((DDRelicPainting)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicReagent ){ gold = ((DDRelicReagent)item).RelicGoldValue; buyer = "an alchemist"; }
			else if ( item is DDRelicRugAddonDeed ){ gold = ((DDRelicRugAddonDeed)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicScrolls ){ gold = ((DDRelicScrolls)item).RelicGoldValue; buyer = "a mage"; }
			else if ( item is DDRelicStatue ){ gold = ((DDRelicStatue)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicTablet ){ gold = ((DDRelicTablet)item).RelicGoldValue; buyer = "a sage"; }
			else if ( item is DDRelicVase ){ gold = ((DDRelicVase)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is DDRelicLeather ){ gold = ((DDRelicLeather)item).RelicGoldValue; buyer = "a leather worker"; }
			else if ( item is DDRelicGrave ){ gold = ((DDRelicGrave)item).RelicGoldValue; buyer = "a necromancer"; }
			else if ( item is EmptyCanopicJar ){ gold = ((EmptyCanopicJar)item).RelicGoldValue; buyer = "an art collector"; }
			else if ( item is HighSeasRelic ){ gold = ((HighSeasRelic)item).RelicGoldValue; buyer = "a shipwright"; }
			else if ( item is StatueGygaxAddonDeed ){ gold = ((StatueGygaxAddonDeed)item).RelicGoldValue; buyer = "an art collector"; }

			if ( item is DDRelicBook || item is DDRelicTablet || item is DDRelicScrolls )
			{
				gold = (int)( gold * ( 1 + ( customer.Skills[SkillName.Inscribe].Value * 0.01 ) ) );
			}

			if ( buyer != "" && examiner == customer )
			{
				phrase = "m is worth " + gold.ToString() + " gold to " + buyer + ".";
			}
			else if ( buyer != "" )
			{
				phrase = "You could give this to " + buyer + " for " + gold.ToString() + " gold.";
			}

			return phrase;
		}

		public static string IdentifyArmsRelicValue( Mobile examiner, Mobile customer, Item item )
		{
			int gold = 0;
			string buyer = "";
			string phrase = "";

			if ( item is DDRelicArmor ){ gold = ((DDRelicArmor)item).RelicGoldValue; buyer = "an armorer"; }
			else if ( item is DDRelicWeapon ){ gold = ((DDRelicWeapon)item).RelicGoldValue; buyer = "a weaponsmith"; }

			if ( buyer != "" && examiner == customer )
			{
				phrase = "m is worth " + gold.ToString() + " gold to " + buyer + ".";
			}
			else if ( buyer != "" )
			{
				phrase = "You could give this to " + buyer + " for " + gold.ToString() + " gold.";
			}

			return phrase;
		}

		public static bool IsRelicItem( Item item )
		{
			if (	item is DDRelicAlchemy
				 || item is DDRelicArmor
				 || item is DDRelicArts
				 || item is DDRelicBanner
				 || item is DDRelicBearRugsAddon
				 || item is DDRelicBook
				 || item is DDRelicClock1
				 || item is DDRelicClock2
				 || item is DDRelicClock3
				 || item is DDRelicCloth
				 || item is DDRelicCoins
				 || item is DDRelicDrink
				 || item is DDRelicFur
				 || item is DDRelicGem
				 || item is DDRelicInstrument
				 || item is DDRelicJewels
				 || item is DDRelicLight1
				 || item is DDRelicLight2
				 || item is DDRelicLight3
				 || item is DDRelicOrbs
				 || item is DDRelicPainting
				 || item is DDRelicReagent
				 || item is DDRelicRugAddonDeed
				 || item is DDRelicScrolls
				 || item is DDRelicStatue
				 || item is DDRelicTablet
				 || item is DDRelicVase
				 || item is DDRelicWeapon
				 || item is DDRelicLeather 
				 || item is DDRelicGrave 
				 || item is HighSeasRelic 
				 || item is EmptyCanopicJar 
				 || item is StatueGygaxAddonDeed 
				)
				{ return true; }

			return false;
		}

		public static int RelicValue()
		{
			return Utility.RandomMinMax( 80, 500 );
		}

		public static int RelicValue( Item relics, Mobile m )
		{
			int RelicValue = 0;
			bool IsHenchman = false;
			if ( m is HenchmanMonster || m is HenchmanFighter || m is HenchmanArcher || m is HenchmanWizard ){ IsHenchman = true; }

			if ( relics is DDRelicVase && ( IsHenchman || m is VarietyDealer ) ){ DDRelicVase relic = (DDRelicVase)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicPainting && ( IsHenchman || m is VarietyDealer ) ){ DDRelicPainting relic = (DDRelicPainting)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is EmptyCanopicJar && ( IsHenchman || m is VarietyDealer ) ){ EmptyCanopicJar relic = (EmptyCanopicJar)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicBanner && ( IsHenchman || m is VarietyDealer ) ){ DDRelicBanner relic = (DDRelicBanner)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicGrave && ( IsHenchman || m is Necromancer || m is NecromancerGuildmaster || m is NecroMage || m is Witches ) ){ DDRelicGrave relic = (DDRelicGrave)relics; RelicValue = relic.RelicGoldValue; } // NECROMANCERS
			else if ( relics is DDRelicPainting && ( IsHenchman || m is Artist) ){ DDRelicPainting relic = (DDRelicPainting)relics; RelicValue = relic.RelicGoldValue*2; } // ARIST PAYS DOUBLE FOR PAINTINGS
			else if ( relics is DDRelicBanner && relics.Name.Contains("painting") && ( IsHenchman || m is Artist) ){ DDRelicBanner relic = (DDRelicBanner)relics; RelicValue = relic.RelicGoldValue*2; } // ARIST PAYS DOUBLE FOR PAINTINGS
			else if ( relics is DDRelicLight1 && ( IsHenchman || m is VarietyDealer ) ){ DDRelicLight1 relic = (DDRelicLight1)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicLight2 && ( IsHenchman || m is VarietyDealer ) ){ DDRelicLight2 relic = (DDRelicLight2)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicLight3 && ( IsHenchman || m is VarietyDealer ) ){ DDRelicLight3 relic = (DDRelicLight3)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicArts && ( IsHenchman || m is VarietyDealer ) ){ DDRelicArts relic = (DDRelicArts)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicStatue && ( IsHenchman || m is VarietyDealer ) ){ DDRelicStatue relic = (DDRelicStatue)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicRugAddonDeed && ( IsHenchman || m is VarietyDealer ) ){ DDRelicRugAddonDeed relic = (DDRelicRugAddonDeed)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR
			else if ( relics is DDRelicWeapon && ( IsHenchman || m is Weaponsmith ) ){ DDRelicWeapon relic = (DDRelicWeapon)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicArmor && ( IsHenchman || m is Armorer ) ){ DDRelicArmor relic = (DDRelicArmor)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicJewels && ( IsHenchman || m is Jeweler ) ){ DDRelicJewels relic = (DDRelicJewels)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicGem && ( IsHenchman || m is Jeweler ) ){ DDRelicGem relic = (DDRelicGem)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicInstrument && ( IsHenchman || m is Bard ) ){ DDRelicInstrument relic = (DDRelicInstrument)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicScrolls && ( IsHenchman || m is Mage ) ){ DDRelicScrolls relic = (DDRelicScrolls)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicClock1 && ( IsHenchman || m is Tinker ) ){ DDRelicClock1 relic = (DDRelicClock1)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicClock2 && ( IsHenchman || m is Tinker ) ){ DDRelicClock2 relic = (DDRelicClock2)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicClock3 && ( IsHenchman || m is Tinker ) ){ DDRelicClock3 relic = (DDRelicClock3)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicCloth && ( IsHenchman || m is Tailor ) ){ DDRelicCloth relic = (DDRelicCloth)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicFur && ( IsHenchman || m is Furtrader ) ){ DDRelicFur relic = (DDRelicFur)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicLeather && ( IsHenchman || m is LeatherWorker ) ){ DDRelicLeather relic = (DDRelicLeather)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicDrink && ( IsHenchman || m is TavernKeeper ) ){ DDRelicDrink relic = (DDRelicDrink)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicReagent && ( IsHenchman || m is Alchemist ) ){ DDRelicReagent relic = (DDRelicReagent)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicAlchemy && ( IsHenchman || m is Alchemist ) ){ DDRelicAlchemy relic = (DDRelicAlchemy)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicReagent && ( IsHenchman || m is Witches ) ){ DDRelicReagent relic = (DDRelicReagent)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicAlchemy && ( IsHenchman || m is Witches ) ){ DDRelicAlchemy relic = (DDRelicAlchemy)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is ObsidianStone && ( IsHenchman || m is StoneCrafter ) ){ ObsidianStone relic = (ObsidianStone)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicCoins && ( IsHenchman || m is Minter ) ){ DDRelicCoins relic = (DDRelicCoins)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicOrbs && ( IsHenchman || m is Mage ) ){ DDRelicOrbs relic = (DDRelicOrbs)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is HighSeasRelic && ( IsHenchman || m is Shipwright ) ){ HighSeasRelic relic = (HighSeasRelic)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicBook && ( IsHenchman || m is Scribe ) ){ DDRelicBook relic = (DDRelicBook)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicTablet && ( IsHenchman || m is Sage ) ){ DDRelicTablet relic = (DDRelicTablet)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DDRelicBearRugsAddonDeed && ( IsHenchman || m is Furtrader ) ){ DDRelicBearRugsAddonDeed relic = (DDRelicBearRugsAddonDeed)relics; RelicValue = relic.RelicGoldValue; }
			else if ( relics is DynamicBook && ( IsHenchman || m is Scribe ) ){ RelicValue = Utility.RandomMinMax( 20, 100 ); }
			else if ( relics is DynamicBook && ( IsHenchman || m is Sage ) ){ RelicValue = Utility.RandomMinMax( 20, 100 ); }
			else if ( relics is StatueGygaxAddonDeed && ( IsHenchman || m is VarietyDealer ) ){ StatueGygaxAddonDeed relic = (StatueGygaxAddonDeed)relics; RelicValue = relic.RelicGoldValue; } // ART COLLECTOR

			if ( relics is DDRelicBook || relics is DDRelicTablet || relics is DDRelicScrolls )
			{
				RelicValue = (int)( RelicValue * ( 1 + ( m.Skills[SkillName.Inscribe].Value * 0.01 ) ) );
			}

			return RelicValue;
		}
	}
}