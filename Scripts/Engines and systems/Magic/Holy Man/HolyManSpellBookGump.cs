using System; 
using System.Collections; 
using Server; 
using Server.Items; 
using Server.Misc; 
using Server.Network; 
using Server.Spells; 
using Server.Spells.HolyMan; 
using Server.Prompts; 

namespace Server.Gumps 
{ 
	public class HolyManSpellbookGump : Gump 
	{
		private HolyManSpellbook m_Book; 

		public bool HasSpell(Mobile from, int spellID)
		{
			return (m_Book.HasSpell(spellID));
		}

		public HolyManSpellbookGump( Mobile from, HolyManSpellbook book, int page ) : base( 100, 100 ) 
		{
			m_Book = book; 

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(41, 42, 2404);

			int PriorPage = page - 1;
				if ( PriorPage < 1 ){ PriorPage = 19; }
			int NextPage = page + 1;

			string sGrave = "";

			AddButton(115, 54, 1057, 1057, PriorPage, GumpButtonType.Reply, 0);
			AddButton(521, 54, 1058, 1058, NextPage, GumpButtonType.Reply, 0);

			AddHtml( 172, 61, 345, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Prayer Book                 Prayer Book</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			if ( page == 1 )
			{
				int SpellsInBook = 14;
				int SafetyCatch = 0;
				int SpellsListed = 769;
				string SpellName = "";

				int nHTMLx = 146;
				int nHTMLy = 108;

				int nBUTTONx = 124;
				int nBUTTONy = 112;

				while ( SpellsInBook > 0 )
				{
					SpellsListed++;
					SafetyCatch++;

					if ( this.HasSpell( from, SpellsListed) )
					{
						SpellsInBook--;

						if ( SpellsListed == 770 ){ SpellName = "Banish"; }
						else if ( SpellsListed == 771 ){ SpellName = "Dampen Spirit"; }
						else if ( SpellsListed == 772 ){ SpellName = "Enchant"; }
						else if ( SpellsListed == 773 ){ SpellName = "Hammer of Faith"; }
						else if ( SpellsListed == 774 ){ SpellName = "Heavenly Light"; }
						else if ( SpellsListed == 775 ){ SpellName = "Nourish"; }
						else if ( SpellsListed == 776 ){ SpellName = "Purge"; }
						else if ( SpellsListed == 777 ){ SpellName = "Rebirth"; }
						else if ( SpellsListed == 778 ){ SpellName = "Sacred Boon"; }
						else if ( SpellsListed == 779 ){ SpellName = "Sanctify"; }
						else if ( SpellsListed == 780 ){ SpellName = "Seance"; }
						else if ( SpellsListed == 781 ){ SpellName = "Smite"; }
						else if ( SpellsListed == 782 ){ SpellName = "Touch of Life"; }
						else if ( SpellsListed == 783 ){ SpellName = "Trial by Fire"; }

						AddHtml( nHTMLx, nHTMLy, 182, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + SpellName + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(nBUTTONx, nBUTTONy, 30008, 30008, SpellsListed, GumpButtonType.Reply, 0);

						nHTMLy = nHTMLy + 30;
						if ( SpellsInBook == 7 ){ nHTMLx = 382; nHTMLy = 108; }

						nBUTTONy = nBUTTONy + 30;
						if ( SpellsInBook == 7 ){ nBUTTONx = 360; nBUTTONy = 112; }
					}

					if ( SafetyCatch > 14 ){ SpellsInBook = 0; }
				}
			}
			else if ( page == 2 )
			{
				AddHtml( 128, 99, 201, 223, @"Banish<BR><BR>Piety: 120<BR><BR>Skill: 60<BR><BR>Mana: 30<BR><BR>Patriarch Morden rests south of the Village of Springvale<BR><BR>  Mantra: exilium</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Sends demons and the dead back to the realms of hell.<br><br>Command Word: [BanishEvil</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x965, 1071);
			}
			else if ( page == 3 )
			{
				AddHtml( 128, 99, 201, 223, @"Dampen Spirit<BR><BR>Piety: 140<BR><BR>Skill: 70<BR><BR>Mana: 35<BR><BR>Archbishop Halyrn rests by the Village of Whisper<BR><BR>  Mantra: accipe spiritum</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Absorbs mana from others and bestows it to the priest.<br><br>Command Word: [DampenSpirit</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x966, 1071);
			}
			else if ( page == 4 )
			{
				AddHtml( 128, 99, 201, 223, @"Enchant<BR><BR>Piety: 180<BR><BR>Skill: 90<BR><BR>Mana: 45<BR><BR>Bishop Leantre rests in the Kuldar Cemetery<BR><BR>  Mantra: fascinare</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Temporarily imbues a weapon with holy powers.<br><br>Command Word: [Enchant</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x967, 1071);
			}
			else if ( page == 5 )
			{
				AddHtml( 128, 99, 201, 223, @"Hammer of Faith<BR><BR>Piety: 100<BR><BR>Skill: 50<BR><BR>Mana: 25<BR><BR>Deacon Wilems rests in the City of Elidor<BR><BR>  Mantra: malleo fidei</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Temporarily summons a hammer from the gods.<br><br>Command Word: [HammerOfFaith</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x968, 1071);
			}
			else if ( page == 6 )
			{
				AddHtml( 128, 99, 201, 223, @"Heavenly Light<BR><BR>Piety: 20<BR><BR>Skill: 10<BR><BR>Mana: 5<BR><BR>Drumat the Apostle rests by the City of Britain<BR><BR>  Mantra: caelesti lumine</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Destroys the darkness, allowing for one to see better.<br><br>Command Word: [HeavenlyLight</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x969, 1071);
			}
			else if ( page == 7 )
			{
				AddHtml( 128, 99, 201, 223, @"Nourish<BR><BR>Piety: 20<BR><BR>Skill: 10<BR><BR>Mana: 5<BR><BR>Vincent the Priest rests by the Town of Moon<BR><BR>  Mantra: famem prohibere</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The priest is able to help those that are starving or thirsty.<br><br>Command Word: [Nourish</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x96A, 1071);
			}
			else if ( page == 8 )
			{
				AddHtml( 128, 99, 201, 223, @"Purge<BR><BR>Piety: 80<BR><BR>Skill: 40<BR><BR>Mana: 20<BR><BR>Abigayl the Preacher rests near the Church of the Divine in the Town of Renika<BR>  Mantra: deiectionem</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Removes curses and other ailing effects.<br><br>Command Word: [Purge</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x96B, 1071);
			}
			else if ( page == 9 )
			{
				AddHtml( 128, 99, 201, 223, @"Rebirth<BR><BR>Piety: 400<BR><BR>Skill: 80<BR><BR>Mana: 40<BR><BR>Cardinal Greggs rests near the Greensky Village<BR><BR>  Mantra: reditus vitae</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Brings one back to life, or summons an orb to resurrect the priest later on.<br><br>Command Word: [Rebirth</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x96C, 1071);
			}
			else if ( page == 10 )
			{
				AddHtml( 128, 99, 201, 223, @"Sacred Boon<BR><BR>Piety: 40<BR><BR>Skill: 20<BR><BR>Mana: 10<BR><BR>Father Michal rests by the Village of Grey<BR><BR>  Mantra: sacrum munus</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Surrounds one with a holy aura that heals wounds much quicker.<br><br>Command Word: [SacredBoon</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x96E, 1071);
			}
			else if ( page == 11 )
			{
				AddHtml( 128, 99, 201, 223, @"Sanctify<BR><BR>Piety: 60<BR><BR>Skill: 30<BR><BR>Mana: 15<BR><BR>Sister Tiana rests south of the City of Montor<BR><BR>  Mantra: benedicite</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The gods grant the priest greater strength, speed, and intelligence.<br><br>Command Word: [Sanctify</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x96D, 1071);
			}
			else if ( page == 12 )
			{
				AddHtml( 128, 99, 201, 223, @"Seance<BR><BR>Piety: 120<BR><BR>Skill: 60<BR><BR>Mana: 30<BR><BR>Brother Kurklan rests near the Village of Islegem<BR><BR>  Mantra: spiritus mundi</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Allows the priest to enter the realm of the dead, avoiding any harm.<br><br>Command Word: [Seance</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x96F, 1071);
			}
			else if ( page == 13 )
			{
				AddHtml( 128, 99, 201, 223, @"Smite<BR><BR>Piety: 80<BR><BR>Skill: 40<BR><BR>Mana: 20<BR><BR>Edwin the Pope rests in the Lodoria Cemetery<BR><BR>  Mantra: percutiat</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Calls down a bolt from the heavens, doing double damage to demons and undead.<br><br>Command Word: [Smite</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x970, 1071);
			}
			else if ( page == 14 )
			{
				AddHtml( 128, 99, 201, 223, @"Touch of Life<BR><BR>Piety: 40<BR><BR>Skill: 20<BR><BR>Mana: 10<BR><BR>Xephyn the Monk rests near the Town of Devil Guard<BR><BR>  Mantra: tactus vitae</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Restores health and stamina to the weary.<br><br>Command Word: [TouchOfLife</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x971, 1071);
			}
			else if ( page == 15 )
			{
				AddHtml( 128, 99, 201, 223, @"Trial by Fire<BR><BR>Piety: 500<BR><BR>Skill: 30<BR><BR>Mana: 15<BR><BR>Chancellor Davis rests on an island near the Village of Fawn<BR><BR>  Mantra: igne iudicii</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Engulfs the priest in holy flames, reflecting magic back at the caster.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x972, 1071);
			}

			else if ( page == 16 )
			{
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>In order to learn the ways of the light, you must pursue proficiency in healing and speaking with spirits. One must seek out the graves of 14 priests, which are spread throughout the lands. Find their resting places, speak their mantra, and claim theirholy symbols which contains the power</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>granted from the gods. Placing the symbols onto this book will add the prayer, but be quick about it. Anyone that calls forth their symbols will cause it to appear no matter where it is in the land, taking it from another that may possess it. You will need to banish evil to use such prayers.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else if ( page == 17 )
			{
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Find creatures like demons and the undead...those that carry gold, and slay them while holding the symbol where trinkets go. Although their gold will vanish, your symbol will increase in piety that will deplete as you use these prayers. You do not need to hold the symbol while praying,</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>but only when dispatching such evil. The symbol does not need to be in your possession either, as prayers will use the piety wherever it is. Magic from lower reagent properties can affect the amount of piety needed to invoke the prayer. Although most prayers rely on your Spirit Speak skill alone, there</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else if ( page == 18 )
			{
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>are also some elements that will have greater effect based on your Healing skill. Go forth Priest, and rid the world of evil.<br><br>Magic Toolbars: Here are the commands you can use (include the bracket) to manage magic toolbars that might help you play better.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>[holyspell1 - Opens the 1st priest spell bar editor.<br>[holyspell2 - Opens the 2nd priest spell bar editor.<br><br><br>[holytool1 - Opens the 1st priest spell bar.<br>[holytool2 - Opens the 2nd priest spell bar.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else if ( page == 19 )
			{
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>[holyclose1 - Closes the 1st priest spell bar.<br>[holyclose2 - Closes the 2nd priest spell bar.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public override void OnResponse( NetState state, RelayInfo info ) 
		{
			Mobile from = state.Mobile; 

			if ( info.ButtonID < 700 && info.ButtonID > 0 )
			{
				from.SendSound( 0x55 );
				int page = info.ButtonID;
				if ( page < 1 ){ page = 19; }
				if ( page > 19 ){ page = 1; }
				from.SendGump( new HolyManSpellbookGump( from, m_Book, page ) );
			}
			else if ( info.ButtonID > 700 )
			{
				if ( info.ButtonID == 770 ){ new BanishEvilSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 771 ){ new DampenSpiritSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 772 ){ new EnchantSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 773 ){ new HammerOfFaithSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 774 ){ new HeavenlyLightSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 775 ){ new NourishSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 776 ){ new PurgeSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 777 ){ new RebirthSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 778 ){ new SacredBoonSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 779 ){ new SanctifySpell( from, null ).Cast(); }
				else if ( info.ButtonID == 780 ){ new SeanceSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 781 ){ new SmiteSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 782 ){ new TouchOfLifeSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 783 ){ new TrialByFireSpell( from, null ).Cast(); }

				from.SendGump( new HolyManSpellbookGump( from, m_Book, 1 ) );
			}
		}
	}
}