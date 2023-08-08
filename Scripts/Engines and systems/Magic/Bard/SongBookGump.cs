using System; 
using System.Collections; 
using Server; 
using Server.Items; 
using Server.Network; 
using Server.Spells;
using Server.Misc; 
using Server.Spells.Song; 
using Server.Prompts;
using Server.Targeting; 
 
namespace Server.Gumps 
{ 
	public class SongBookGump : Gump 
	{ 
		private SongBook m_Book; 

		int gth = 0x0; // FONT COLOR FOR LABELS

		private void AddBackground() 
		{
			AddPage( 0 ); 
			AddImage(47, 50, 5000); 
		}

		public bool HasSpell( Mobile from, int spellID ) 
		{ 
			return ( m_Book != null && m_Book.HasSpell( spellID ) ); 
		} 
       
		public SongBookGump( Mobile from, SongBook book, int page ) : base( 0, 0 ) 
		{ 
			m_Book = book;
			AddBackground();
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;

			int PriorPage = page - 1;
				if ( PriorPage < 1 ){ PriorPage = 18; }
			int NextPage = page + 1;

			AddButton(97, 59, 2205, 2205, PriorPage, GumpButtonType.Reply, 0);
			AddButton(368, 58, 2206, 2206, NextPage, GumpButtonType.Reply, 0);

			if ( page == 1 )
			{
				AddHtml( 281, 68, 97, 19, @"<BASEFONT Color=#363329><BIG>Instrument</BIG></BASEFONT>", (bool)false, (bool)false);
				AddButton(264, 69, 55, 55, 99, GumpButtonType.Reply, 0);

				AddHtml( 122, 64, 97, 36, @"<BASEFONT Color=#363329><BIG><CENTER>Bardic<br>Songs</CENTER></BIG></BASEFONT>", (bool)false, (bool)false);

				int lcbt = 108; 
				int lcpg = 103;

				if (HasSpell( from, 351) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 351, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Army's Paeon");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 352) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 352, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Enchanting Etude");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 353) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 353, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Energy Carol");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 354) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 354, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Energy Threnody");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 355) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 355, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Fire Carol");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 356) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 356, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Fire Threnody");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 357) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 357, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Foe Requiem");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 358) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 358, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Ice Carol");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 359) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 359, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Ice Threnody");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 360) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 360, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Knight's Minne");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 361) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 361, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Mage's Ballad");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 362) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 362, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Magic Finale");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 363) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 363, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Poison Carol");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 364) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 364, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Poison Threnody");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 365) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 365, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Shepherd's Dance");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (HasSpell( from, 366) ) 
				{
					AddButton(lcpg, lcbt, 9762, 9762, 366, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Sinewy Etude");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
				if (false && HasSpell( from, 367) ) 
				{
					// TODO: This needs to go onto the second page. It's currently on top of "Ice Threnody".
					AddButton(lcpg, lcbt, 9762, 9762, 367, GumpButtonType.Reply, 0);
					AddLabel(lcpg+20, lcbt-2, gth, @"Dominate Creature");
					lcbt = lcbt + 18; if (lcbt > 234){lcbt = 108; lcpg = 258;}
				}
			}

			else if ( page == 2 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Army's Paeon<br><br>An area of effect that regenerates your party's health slowly.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 60<br><br>Mana: 30<br><br>[ArmysPaeon</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x404);
			} 
			else if ( page == 3 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Enchanting Etude<br><br>An area of effect that raises the intelligence of your party.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 65<br><br>Mana: 40<br><br>[EnchantingEtude</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x405);
			} 
			else if ( page == 4 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Energy Carol<br><br>An area of effect that raises the energy resistance of your party.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 50<br><br>Mana: 20<br><br>[EnergyCarol</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x406);
			} 
			else if ( page == 5 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Energy Threnody<br><br>Lowers the energy resistance of your target.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 65<br><br>Mana: 40<br><br>[EnergyThrenody</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x407);
			} 
			else if ( page == 6 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Fire Carol<br><br>An area of effect that raises the fire resistance of your party.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 50<br><br>Mana: 20<br><br>[FireCarol</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x408);
			} 
			else if ( page == 7 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Fire Threnody<br><br>Lowers the fire resistance of your target.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 65<br><br>Mana: 40<br><br>[FireThrenody</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x409);
			} 
			else if ( page == 8 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Foe Requiem<br><br>Damages your target with a burst of sonic energy.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 65<br><br>Mana: 30<br><br>[FoeRequiem</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x40A);
			} 
			else if ( page == 9 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Ice Carol<br><br>An area of effect that raises the cold resistance of your party.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 50<br><br>Mana: 20<br><br>[IceCarol</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x40B);
			} 
			else if ( page == 10 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Ice Threnody<br><br>Lowers the ice resistance of your target.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 65<br><br>Mana: 40<br><br>[IceThrenody</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x40C);
			} 
			else if ( page == 11 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Knight's Minne<br><br>An area of effect that raises the physical resist of your party.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 50<br><br>Mana: 20<br><br>[KnightsMinne</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x40D);
			} 
			else if ( page == 12 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Mage's Ballad<br><br>Sacrifices a large amount of the bard's mana to regenerate your party's mana slowly.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 55<br><br>Mana: 100<br><br>[MagesBallad</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x40E);
			} 
			else if ( page == 13 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Magic Finale<br><br>An area of effect that dispels all summoned creatures around you and damages enemies you have provoked.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 70<br><br>Mana: 55<br><br>[MagicFinale</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x410);
			} 
			else if ( page == 14 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Poison Carol<br><br>An area of effect that raises the poison resistance of your party.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 50<br><br>Mana: 20<br><br>[PoisonCarol</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x411);
			} 
			else if ( page == 15 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Poison Threnody<br><br>Lowers the poison resistance of your target.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 65<br><br>Mana: 40<br><br>[PoisonThrenody</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x412);
			} 
			else if ( page == 16 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Shepherd's Dance<br><br>An area of effect that raises the dexterity of your party.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 65<br><br>Mana: 40<br><br>[ShephardsDance</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x413);
			} 
			else if ( page == 17 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Sinewy Etude<br><br>An area of effect that raises the strength of your party.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 60<br><br>Mana: 20<br><br>[SinewyEtude</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x414);
			} 
			else if ( false && page == 18 )
			{
			AddHtml( 105, 86, 133, 163, @"<BASEFONT Color=#363329><BIG>Dominate Creature<br><br>An incredibly powerful bard song that combines the Provocation skill to completely control a creature for a limited amount of time.</BIG></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 262, 89, 132, 120, @"<BASEFONT Color=#363329><BIG>Skill: 80<br><br>Mana: 70<br><br>[DominateCreature</BIG></BASEFONT>", (bool)false, (bool)false);
			AddImage(308, 197, 0x5005);
			} 
		} 
       
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( info.ButtonID == 99 )
			{
				from.SendMessage( "Click your instrument of bardic choice." );
				from.Target = new InternalTarget( m_Book );
			}
			else if ( info.ButtonID < 300 && info.ButtonID > 0 )
			{
				from.SendSound( 0x55 );
				int page = info.ButtonID;
				if ( page < 1 ){ page = 18; }
				if ( page > 18 ){ page = 1; }
				from.SendGump( new SongBookGump( from, m_Book, page ) );
			}
			else if ( m_Book.Instrument != null && !(from.InRange( m_Book.Instrument.GetWorldLocation(), 1 )) )
			{
				from.SendMessage( "Your chosen instrument must be in your pack!" );
			}
			else if ( info.ButtonID > 300 )
			{
				if ( m_Book.Instrument == null )
				{
					from.SendMessage( "You need an instrument to play that song!" );
					from.SendMessage( "Select your instrument of bardic choice." );
					from.Target = new InternalTarget( m_Book );
				}
				else if ( info.ButtonID == 351 ){ new ArmysPaeonSong( from, null ).Cast(); }
				else if ( info.ButtonID == 352 ){ new EnchantingEtudeSong( from, null ).Cast(); }
				else if ( info.ButtonID == 353 ){ new EnergyCarolSong( from, null ).Cast(); }
				else if ( info.ButtonID == 354 ){ new EnergyThrenodySong( from, null ).Cast(); }
				else if ( info.ButtonID == 355 ){ new FireCarolSong( from, null ).Cast(); }
				else if ( info.ButtonID == 356 ){ new FireThrenodySong( from, null ).Cast(); }
				else if ( info.ButtonID == 357 ){ new FoeRequiemSong( from, null ).Cast(); }
				else if ( info.ButtonID == 358 ){ new IceCarolSong( from, null ).Cast(); }
				else if ( info.ButtonID == 359 ){ new IceThrenodySong( from, null ).Cast(); }
				else if ( info.ButtonID == 360 ){ new KnightsMinneSong( from, null ).Cast(); }
				else if ( info.ButtonID == 361 ){ new MagesBalladSong( from, null ).Cast(); }
				else if ( info.ButtonID == 362 ){ new MagicFinaleSong( from, null ).Cast(); }
				else if ( info.ButtonID == 363 ){ new PoisonCarolSong( from, null ).Cast(); }
				else if ( info.ButtonID == 364 ){ new PoisonThrenodySong( from, null ).Cast(); }
				else if ( info.ButtonID == 365 ){ new SheepfoeMamboSong( from, null ).Cast(); }
				else if ( info.ButtonID == 366 ){ new SinewyEtudeSong( from, null ).Cast(); }
				else if ( info.ButtonID == 367 ){ new DominateCreatureSong( from, null ).Cast(); }
			}
		} 

		private class InternalTarget : Target
		{
			private SongBook Book;

			public InternalTarget( SongBook book ) : base( 1, false, TargetFlags.None ) 
			{
				Book = book;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is BaseInstrument )
				{
					Book.Instrument = (BaseInstrument)target;
					from.SendMessage( "You set your instrument to play for these songs." );
				}
				else
				{
					from.SendMessage( "That is not an instrument you can play!" );
				}
			}
		}
	} 
}
