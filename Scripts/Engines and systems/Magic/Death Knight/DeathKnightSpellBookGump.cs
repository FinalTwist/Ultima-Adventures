using System; 
using System.Collections; 
using Server; 
using Server.Items; 
using Server.Misc; 
using Server.Network; 
using Server.Spells; 
using Server.Spells.DeathKnight; 
using Server.Prompts; 

namespace Server.Gumps 
{ 
	public class DeathKnightSpellbookGump : Gump 
	{
		private DeathKnightSpellbook m_Book; 

		public bool HasSpell(Mobile from, int spellID)
		{
			return (m_Book.HasSpell(spellID));
		}

		public DeathKnightSpellbookGump( Mobile from, DeathKnightSpellbook book, int page ) : base( 100, 100 ) 
		{
			m_Book = book; 

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(41, 42, 1100);

			int PriorPage = page - 1;
				if ( PriorPage < 1 ){ PriorPage = 19; }
			int NextPage = page + 1;

			string sGrave = "";

			AddButton(115, 54, 1057, 1057, PriorPage, GumpButtonType.Reply, 0);
			AddButton(521, 54, 1058, 1058, NextPage, GumpButtonType.Reply, 0);

			AddHtml( 172, 61, 345, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Death Magic                 Death Magic</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			string name, map, dungeon;
			if ( page == 1 )
			{
				int SpellsInBook = 14;
				int SafetyCatch = 0;
				int SpellsListed = 749;
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

						if ( SpellsListed == 750 ){ SpellName = "Banish"; }
						else if ( SpellsListed == 751 ){ SpellName = "Demonic Touch"; }
						else if ( SpellsListed == 752 ){ SpellName = "Devil Pact"; }
						else if ( SpellsListed == 753 ){ SpellName = "Grim Reaper"; }
						else if ( SpellsListed == 754 ){ SpellName = "Hag Hand"; }
						else if ( SpellsListed == 755 ){ SpellName = "Hellfire"; }
						else if ( SpellsListed == 756 ){ SpellName = "Lucifer's Bolt"; }
						else if ( SpellsListed == 757 ){ SpellName = "Orb of Orcus"; }
						else if ( SpellsListed == 758 ){ SpellName = "Shield of Hate"; }
						else if ( SpellsListed == 759 ){ SpellName = "Soul Reaper"; }
						else if ( SpellsListed == 760 ){ SpellName = "Strength of Steel"; }
						else if ( SpellsListed == 761 ){ SpellName = "Strike"; }
						else if ( SpellsListed == 762 ){ SpellName = "Succubus Skin"; }
						else if ( SpellsListed == 763 ){ SpellName = "Wrath"; }

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
				name = "Saint Kargoth";
				map = "Land of Sosaria";
				dungeon = "Ancient Pyramid";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Trammel );
				AddHtml( 128, 99, 201, 223, "<BODY><BASEFONT Color=#111111><BIG>Banish<BR><BR>Souls: 56<BR><BR>Skill: 40<BR><BR>Mana: 36<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Banish summoned creatures back to their realm, demons back to hell, or elementals back to their plane of existence.<br><br>Command Word: [Banish</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x5010, 2405);
			}
			else if ( page == 3 )
			{
				name = "Lord Monduiz Dephaar";
				map = "Land of Sosaria";
				dungeon = "Dungeon Clues";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Trammel );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Demonic Touch<BR><BR>Souls: 21<BR><BR>Skill: 15<BR><BR>Mana: 16<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The death knight's target is healed by demonic forces for a significant amount.<br><br>Command Word: [DemonicTouch</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x5009, 2405);
			}
			else if ( page == 4 )
			{
				name = "Lady Kath of Naelex";
				map = "Land of Sosaria";
				dungeon = "Dungeon Abandon";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Trammel );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Devil Pact<BR><BR>Souls: 98<BR><BR>Skill: 90<BR><BR>Mana: 60<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Summons the devil to battle with the death knight.<br><br>Command Word: [DevilPact</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x5005, 2405);
			}
			else if ( page == 5 )
			{
				name = "Prince Myrhal of Rax";
				map = "Land of Sosaria";
				dungeon = "Fires of Hell";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Trammel );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Grim Reaper<BR><BR>Souls: 42<BR><BR>Skill: 30<BR><BR>Mana: 28<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The next target hit becomes marked by the grim reaper. All damage dealt to it is increased, but the death knight takes extra damage from other kinds of creatures.<br><br>Command Word: [GrimReaper</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x402, 2405);
			}
			else if ( page == 6 )
			{
				name = "Sir Maeril of Naelax";
				map = "Land of Sosaria";
				dungeon = "Dungeon Exodus";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Trammel );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Hag Hand<BR><BR>Souls: 7<BR><BR>Skill: 5<BR><BR>Mana: 8<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Your hand holds the powers of a hag, where it can remove curses from items and others.<br><br>Command Word: [HagHand</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x5002, 2405);
			}
			else if ( page == 7 )
			{
				name = "Sir Farian of Lirtham";
				map = "Land of Ambrosia";
				dungeon = "City of the Dead";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Trammel );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Hellfire<BR><BR>Souls: 84<BR><BR>Skill: 70<BR><BR>Mana: 52<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The death knights's enemy is scorched by a hellfire that continues to burn the enemy for a short duration.<br><br>Command Word: [Hellfire</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x3E9, 2405);
			}
			else if ( page == 8 )
			{
				name = "Lord Androma of Gara";
				map = "Island of Umber Veil";
				dungeon = "the Mausoleum";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Trammel );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Lucifer's Bolt<BR><BR>Souls: 35<BR><BR>Skill: 25<BR><BR>Mana: 24<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Calls down a bolt of energy from Lucifer himself, and temporarily stuns the enemy.<br><br>Command Word: [LucifersBolt</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x5DC0, 2405);
			}
			else if ( page == 9 )
			{
				name = "Sir Oslan Knarren";
				map = "Land of Lodoria";
				dungeon = "Dungeon Despise";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Felucca );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Orb of Orcus<BR><BR>Souls: 200<BR><BR>Skill: 80<BR><BR>Mana: 56<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The forces of Orcus surround the knight and refelec a certain amount of magical effects back at the caster.<br><br>Command Word: [OrbOfOrcus</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x1B, 2405);
			}
			else if ( page == 10 )
			{
				name = "Sir Rezinar of Haxx";
				map = "Land of Lodoria";
				dungeon = "Dungeon Deceit";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Felucca );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Shield of Hate<BR><BR>Souls: 77<BR><BR>Skill: 60<BR><BR>Mana: 48<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Channels hatred to form a barrier around the target, shielding them from physical harm.<br><br>Command Word: [ShieldOfHate</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x3EE, 2405);
			}
			else if ( page == 11 )
			{
				name = "Lord Thyrian of Naelax";
				map = "Land of Lodoria";
				dungeon = "Dungeon Wrong";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Felucca );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Soul Reaper<BR><BR>Souls: 63<BR><BR>Skill: 45<BR><BR>Mana: 40<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Drains the enemy of their soul, reducing their mana for a short period of time.<br><br>Command Word: [SoulReaper</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x5006, 2405);
			}
			else if ( page == 12 )
			{
				name = "Sir Minar of Darmen";
				map = "Land of Lodoria";
				dungeon = "Lodoria Catacombs";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Felucca );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Strength of Steel<BR><BR>Souls: 28<BR><BR>Skill: 20<BR><BR>Mana: 20<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Greatly increases the target's strength for a short period.<br><br>Command Word: [StrengthOfSteel</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x2B, 2405);
			}
			else if ( page == 13 )
			{
				name = "Duke Urkar of Torquann";
				map = "Land of Lodoria";
				dungeon = "Dungeon Shame";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Felucca );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Strike<BR><BR>Souls: 14<BR><BR>Skill: 10<BR><BR>Mana: 12<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The death knight's enemy is damaged by a demonic energy from the nine hells.<br><br>Command Word: [Strike</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x12, 2405);
			}
			else if ( page == 14 )
			{
				name = "Sir Luren the Boar";
				map = "Land of Lodoria";
				dungeon = "the City of Embers";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Felucca );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Succubus Skin<BR><BR>Souls: 49<BR><BR>Skill: 35<BR><BR>Mana: 32<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The death knight's target has their skin regenerate health over time.<br><br>Command Word: [SuccubusSkin</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x500C, 2405);
			}
			else if ( page == 15 )
			{
				name = "Lord Khayven of Rax";
				map = "Land of Lodoria";
				dungeon = "Dungeon Hythloth";
				sGrave = Worlds.GetAreaEntrance( dungeon, Map.Felucca );
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Wrath<BR><BR>Souls: 70<BR><BR>Skill: 50<BR><BR>Mana: 44<BR><BR>" + name + "<BR>" + map + "<BR>" + dungeon + "<BR>" + sGrave + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>The death knight unleashes the forces of hell unto his nearby enemies, causing much damage. <br><br>Command Word: [Wrath</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(280, 102, 0x2E, 2405);
			}

			else if ( page == 16 )
			{
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>In order to learn the ways of the Death Knight, you must master the art of Chivalry while spreading evil deeds throughout the land, avoiding Karmic influences. One must seek out the 14 Disciple Knights of Kas, and learn the power they each mastered. Find their resting places, speak their names, and claim their</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>skulls which contains the knowledge they had. Placing the skulls onto this book will increase its spell potential, but be quick about it. Anyone that calls forth their skull will cause it to appear no matter where it is in the land, taking it from another that may possess it. You will need the power of souls to use such magic.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else if ( page == 17 )
			{
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Find humanoid creatures like brigands, orcs, titans, goblins, or trolls...those that carry gold, and slay them while holding the lantern in your left hand. Although their gold will turn to dust, your lantern will increase in power that will drain as you use this magic. You do not need to hold the lantern while unleashing this power,</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>but only when collecting souls. The lantern does not need to be in your possession either, as death magic will claim the souls from the lantern wherever it is. Magic from lower reagent properties can affect the amount of souls needed to invoke the magic. Although most magic relies on your Chivalry skill alone, there</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else if ( page == 18 )
			{
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>are also some elements that will have greater effect the lower your Karma is. Go forth Death Knight, and bring our order back to this world.<br><br>Magic Toolbars: Here are the commands you can use (include the bracket) to manage magic toolbars that might help you play better.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>[deathspell1 - Opens the 1st death knight spell bar editor.<br>[deathspell2 - Opens the 2nd death knight spell bar editor.<br><br><br>[deathtool1 - Opens the 1st death knight spell bar.<br>[deathtool2 - Opens the 2nd death knight spell bar.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else if ( page == 19 )
			{
				AddHtml( 128, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>[deathclose1 - Closes the 1st death knight spell bar.<br>[deathclose2 - Closes the 2nd death knight spell bar.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 361, 99, 201, 223, @"<BODY><BASEFONT Color=#111111><BIG>Beware, Death Knight. Powerful Death Knights are often not tolerated in the city streets and may be attacked on site.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
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
				from.SendGump( new DeathKnightSpellbookGump( from, m_Book, page ) );
			}
			else if ( info.ButtonID > 700 )
			{
				if ( info.ButtonID == 750 ){ new BanishSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 751 ){ new DemonicTouchSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 752 ){ new DevilPactSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 753 ){ new GrimReaperSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 754 ){ new HagHandSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 755 ){ new HellfireSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 756 ){ new LucifersBoltSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 757 ){ new OrbOfOrcusSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 758 ){ new ShieldOfHateSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 759 ){ new SoulReaperSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 760 ){ new StrengthOfSteelSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 761 ){ new StrikeSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 762 ){ new SuccubusSkinSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 763 ){ new WrathSpell( from, null ).Cast(); }

				from.SendGump( new DeathKnightSpellbookGump( from, m_Book, 1 ) );
			}
		}
	}
}