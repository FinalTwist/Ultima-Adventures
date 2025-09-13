using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Spells.Fourth;
using Server.Spells.Seventh;
using Server.Spells.Chivalry;
using Server.Spells.Herbalist;
using Server.Spells.Undead;
using Server.Spells.Mystic;
using Server.Spells.Research;
using Server.Prompts;
using Server.Mobiles;

namespace Server.Gumps
{
	public class RunebookGump : Gump
	{
		private int m_Page;
		private Runebook m_Book;

		public Runebook Book{ get{ return m_Book; } }

		public int GetMapHue( Map map )
		{
			if ( map == Map.Trammel ) // WIZARD CHANGED COLORS
				return 901;
			else if ( map == Map.Felucca )
				return 318;
			else if ( map == Map.Ilshenar )
				return 199;
			else if ( map == Map.Malas )
				return 33;
			else if ( map == Map.Tokuno )
				return 342;
			else if ( map == Map.TerMur )
				return 367;

			return 0;
		}

		public string GetEntryHue( Map map )
		{
			if ( map == Map.Trammel ) // WIZARD CHANGED COLORS
				return "#111111";
			else if ( map == Map.Felucca )
				return "#B74FC0";
			else if ( map == Map.Ilshenar )
				return "#173F7F";
			else if ( map == Map.Malas )
				return "#D02C52";
			else if ( map == Map.Tokuno )
				return "#BD6B29";
			else if ( map == Map.TerMur )
				return "#466545";

			return "#111111";
		}

		public string GetName( string name )
		{
			if ( name == null || (name = name.Trim()).Length <= 0 )
				return "Marked Location";

			return name;
		}

		private void AddBackground( )
		{
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			// Background image
			AddImage(0, 0, 1054);

			// Charges
			AddHtml( 136, 26, 143, 20, @"<BODY><BASEFONT Color=#111111><H3>Charges: " + m_Book.CurCharges.ToString() + "/" + m_Book.MaxCharges.ToString() + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		private void AddNavigation()
		{
			int g = 86;
			int h = 90;
			// Page Buttons
			int btn0 = 9781; if ( m_Page == 1 ){ btn0 = 9780; } AddButton(23, g, btn0, btn0, 0, GumpButtonType.Page, 1); g=g+30;
			int btn1 = 9781; if ( m_Page == 2 ){ btn1 = 9780; } AddButton(23, g, btn1, btn1, 0, GumpButtonType.Page, 2); g=g+30;
			int btn2 = 9781; if ( m_Page == 3 ){ btn2 = 9780; } AddButton(23, g, btn2, btn2, 0, GumpButtonType.Page, 3); g=g+30;
			int btn3 = 9781; if ( m_Page == 4 ){ btn3 = 9780; } AddButton(23, g, btn3, btn3, 0, GumpButtonType.Page, 4); g=g+30;
			int btn4 = 9781; if ( m_Page == 5 ){ btn4 = 9780; } AddButton(23, g, btn4, btn4, 0, GumpButtonType.Page, 5); g=g+30;
			int btn5 = 9781; if ( m_Page == 6 ){ btn5 = 9780; } AddButton(23, g, btn5, btn5, 0, GumpButtonType.Page, 6); g=g+30;
			int btn6 = 9781; if ( m_Page == 7 ){ btn6 = 9780; } AddButton(23, g, btn6, btn6, 0, GumpButtonType.Page, 7); g=g+30;
			int btn7 = 9781; if ( m_Page == 8 ){ btn7 = 9780; } AddButton(23, g, btn7, btn7, 0, GumpButtonType.Page, 8); g=g+30;
			int btn8 = 9781; if ( m_Page == 9 ){ btn8 = 9780; } AddButton(23, g, btn8, btn8, 0, GumpButtonType.Page, 9); g=g+30;
			int btn9 = 9781; if ( m_Page == 10 ){ btn9 = 9780; } AddButton(23, g, btn9, btn9, 0, GumpButtonType.Page, 10);
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>1</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>2</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>3</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>4</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>5</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>6</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>7</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>8</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 40, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>9</H3></BASEFONT></BODY>", (bool)false, (bool)false); h=h+30;
			AddHtml( 38, h, 20, 20, @"<BODY><BASEFONT Color=#111111><H3>10</H3></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		private void AddIndex()
		{
			// Index
			AddPage( 1 );
			m_Page = 1;
			AddNavigation();

			// Rename button
			AddHtml( 450, 26, 126, 20, @"<BODY><BASEFONT Color=#111111><H3>Rename Book</H3></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(430, 28, 30008, 30008, 1, GumpButtonType.Reply, 0);

			// List of entries
			List<RunebookEntry> entries = m_Book.Entries;

			int row = 1;
			for ( int i = 0; i < 16; ++i )
			{
				string desc;
				string hue;
				string world;
				int mod = 10;

				if ( i < entries.Count )
				{
					desc = GetName( entries[i].Description );
					hue = GetEntryHue( entries[i].Map );
					world = Server.Misc.Worlds.GetMyWorld( entries[i].Map, entries[i].Location, entries[i].Location.X, entries[i].Location.Y );

					if ( row == 1 )
					{
						AddHtml( 115, 70-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 135, 90-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(95, 70-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 2 )
					{
						AddHtml( 115, 110-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 135, 130-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(95, 110-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 3 )
					{
						AddHtml( 115, 150-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 135, 170-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(95, 150-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 4 )
					{
						AddHtml( 115, 190-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 135, 210-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(95, 190-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 5 )
					{
						AddHtml( 115, 230-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 135, 250-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(95, 230-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 6 )
					{
						AddHtml( 115, 270-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 135, 290-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(95, 270-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 7 )
					{
						AddHtml( 115, 310-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 135, 330-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(95, 310-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 8 )
					{
						AddHtml( 115, 350-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 135, 370-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(95, 350-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 9 )
					{
						AddHtml( 410, 70-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 430, 90-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(390, 70-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 10 )
					{
						AddHtml( 410, 110-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 430, 130-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(390, 110-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 11 )
					{
						AddHtml( 410, 150-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 430, 170-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(390, 150-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 12 )
					{
						AddHtml( 410, 190-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 430, 210-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(390, 190-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 13 )
					{
						AddHtml( 410, 230-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 430, 250-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(390, 230-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 14 )
					{
						AddHtml( 410, 270-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 430, 290-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(390, 270-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 15 )
					{
						AddHtml( 410, 310-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 430, 330-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(390, 310-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
						row++;
					}
					else if ( row == 16 )
					{
						AddHtml( 410, 350-mod, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 430, 370-mod, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + world + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(390, 350-mod, 30008, 30008, (2 + (i * 6) + 0), GumpButtonType.Reply, 0);
					}
				}
			}

			// Turn page button
			AddButton(586, 16, 1056, 1056, 0, GumpButtonType.Page, 2);
		}

		private void AddInstructions()
		{
			AddHtml( 73, 70, 257, 299, @"<BODY><BASEFONT Color=#111111><H3>Rune Books are designed to help reduce the total number of carried runes and to assist rune libraries.<br><br>General Features:<br><br>- A rune book can hold a total of 16 locations.<br><br>- One of these locations can be set as the 'default' location.<br><br>- Casting the recall, gate, ethereal travel, or sacred journey spell on the rune book will treat the book like a rune marked with the default location.<br><br>- Using the astral travel skill on the rune book will treat the book like a rune marked with the default location.<br><br>- Using potions of nature fury, mushroom gateway, demonic fire, and black gate on the rune book will treat the book like a rune marked with the default location.<br><br>- Books can have charges that will allow you to recall to locations in the book without using spells, skills, or potions.<br><br>- Rune Books can be recharged with recall, gate, or astral travel scrolls. They can also be recharged with potions of nature fury, mushroom gateway, demonic fire, and black gate. Dragging such items onto the book will add one charge (up to its maximum).<br><br>- Books cannot be recharged while open.<br><br>- Dragging a rune onto a book will add that location to the book.<br><br>- You can name the rune book by opening the book and selecting 'Rename Book'. After selecting that, simply type in the name of the book and hit enter.</H3></BASEFONT></BODY>", (bool)false, (bool)true);
			AddHtml( 380, 70, 257, 299, @"<BODY><BASEFONT Color=#111111><H3>Using the Rune Book:<br><br>- On the top right of the first index page is an option to rename the book.<br><br>- Casting recall, ethereal travel, or sacred journey directly on the rune book will transport you to the location set as the 'default'.<br><br>- Using the astral travel skill directly on the rune book will transport you to the location set as the 'default'.<br><br>- Using potions of nature fury and demonic fire directly on the rune book will transport you to the location set as the 'default'.<br><br>- Casting gate directly on the rune book will open a gate with the destination to the 'default' location of the book.<br><br>- Using potions of mushroom gateway and black gate directly on the rune book will open a gate with the destination to the 'default' location of the book.<br><br>- To access the non-default locations, you will be able to open the book by double clicking on it.<br><br>- When open, the book will display two index pages with 8 locations on each page.<br><br>- Each page will have the current number of charges listed on the top left side.<br><br>- Each location entry will have a button that will use a charge and transport you to that location. If the book has no charges left, you will not be able to do this.<br><br>- The index pages will display the first 18 characters from the marked rune’s name.<br><br>- The side of the book has book markers. Clicking these numbers will bring you to that page.<br><br>- After each use (success or failure) the rune book needs a few seconds to recharge.<br><br>Rune Book Pages:<br><br>Each rune page will contain buttons that...<br><br>- will use a charge and recall to that location.<br><br>- will set that location as the book's default location.<br><br>- will remove the rune from the book.<br><br>- will cast the recall spell if you know it.<br><br>- will cast the gate travel spell if you know it.<br><br>- will cast the sacred journey spell if you know it.<br><br>- will use a nature passage potion if you have one.<br><br>- will use a mushroom gateway potion if you have one.<br><br>- will use a demonic fire potion if you have one.<br><br>- will use a black gate potion if you have one.<br><br>- will use the astral travel ability if you know it.<br><br>- will use the etheral travel spell if you have it prepared.</H3></BASEFONT></BODY>", (bool)false, (bool)true);

			string title = "Runebook";
				if ( m_Book.Description != null && m_Book.Description != "" ){ title = m_Book.Description; }
			AddHtml( 377, 26, 196, 20, @"<BODY><BASEFONT Color=#111111><H3>" + title + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		private void AddDetails( int index, int half )
		{
			string title = "Runebook";
				if ( m_Book.Description != null && m_Book.Description != "" ){ title = m_Book.Description; }
			AddHtml( 377, 26, 196, 20, @"<BODY><BASEFONT Color=#111111><H3>" + title + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);

			string desc;
			string hue;
			int filled = 0;
			string Sextants = "";
			int defButtonID = 0;

			if ( index < m_Book.Entries.Count )
			{
				RunebookEntry e = (RunebookEntry)m_Book.Entries[index];

				desc = GetName( e.Description );
				hue = GetEntryHue( e.Map );
				filled = 1;

				// Location labels
				int xLong = 0, yLat = 0;
				int xMins = 0, yMins = 0;
				bool xEast = false, ySouth = false;

				if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
				{
					Sextants = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
				}

				// Set as default button
				defButtonID = e != m_Book.Default ? 11410 : 11400;
			}
			else
			{
				desc = "Empty";
				hue = "#111111";
				filled = 0;
			}

			int t = 10;
			int v = 140;
			int w = 7;
			if ( half == 1 ){ v = 440; }

			AddHtml( v-20, 62, 217, 20, @"<BODY><BASEFONT Color=" + hue + "><H3>" + desc + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
			if ( filled > 0 )
			{
				AddButton(v-40, 65, 30008, 30008, 2 + (index * 6) + 0, GumpButtonType.Reply, 0);
				AddHtml( v-25, 84, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>" + Sextants + "</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 115-w, defButtonID, defButtonID, 2 + (index * 6) + 2, GumpButtonType.Reply, 0);
				AddHtml( v, 115-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Set As Default</H3></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( v, 140-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Remove Rune</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 140-w, 30008, 30008, 2 + (index * 6) + 1, GumpButtonType.Reply, 0);

				AddHtml( v, 165-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Recall</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 165-w, 30008, 30008, 2 + (index * 6) + 3, GumpButtonType.Reply, 0);

				AddHtml( v, 190-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Gate</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 190-w, 30008, 30008, 2 + (index * 6) + 4, GumpButtonType.Reply, 0);

				AddHtml( v, 215-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Sacred Journey</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 215-w, 30008, 30008, 2 + (index * 6) + 5, GumpButtonType.Reply, 0);

				AddHtml( v, 240-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Nature Passage</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 240-w, 30008, 30008, 602 + (index * 6) + 1, GumpButtonType.Reply, 0);

				AddHtml( v, 265-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Mushroom Gateway</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 265-w, 30008, 30008, 602 + (index * 6) + 2, GumpButtonType.Reply, 0);

				AddHtml( v, 290-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Demonic Fire</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 290-w, 30008, 30008, 602 + (index * 6) + 3, GumpButtonType.Reply, 0);

				AddHtml( v, 315-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Black Gate</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 315-w, 30008, 30008, 602 + (index * 6) + 4, GumpButtonType.Reply, 0);

				AddHtml( v, 340-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Astral Travel</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 340-w, 30008, 30008, 602 + (index * 6) + 5, GumpButtonType.Reply, 0);

				AddHtml( v, 365-t, 217, 20, @"<BODY><BASEFONT Color=#111111><H3>Ethereal Travel</H3></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(v-20, 365-w, 30008, 30008, 702 + (index * 6) + 1, GumpButtonType.Reply, 0);
			}
		}

		public RunebookGump( Mobile from, Runebook book ) : base( 50, 50 )
		{
			m_Book = book;

			AddBackground();
			AddIndex();

			for ( int page = 0; page < 9; ++page )
			{
				AddPage( 2 + page );
				m_Page = 2 + page;
				AddNavigation();

				AddButton(53, 16, 1055, 1055, 0, GumpButtonType.Page, 1 + page);

				if ( page < 8 )
					AddButton(586, 16, 1056, 1056, 0, GumpButtonType.Page, 3 + page);

				if ( page < 8 )
				{
					for ( int half = 0; half < 2; ++half )
						AddDetails( (page * 2) + half, half );
				}
				else if ( page > 7 )
				{
					AddInstructions();
				}
			}
		}

		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );

			return ( book != null && book.HasSpell( spellID ) );
		}

		private class InternalPrompt : Prompt
		{
			private Runebook m_Book;

			public InternalPrompt( Runebook book )
			{
				m_Book = book;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( m_Book.Deleted || !from.InRange( m_Book.GetWorldLocation(), (Core.ML ? 3 : 1) ) )
					return;

				if ( m_Book.CheckAccess( from ) )
				{
					m_Book.Description = Utility.FixHtml( text.Trim() );

					from.CloseGump( typeof( RunebookGump ) );
					from.SendGump( new RunebookGump( from, m_Book ) );

					from.SendMessage( "The book's title has been changed." );
				}
				else
				{
					m_Book.Openers.Remove( from );
					
					from.SendLocalizedMessage( 502416 ); // That cannot be done while the book is locked down.
				}
			}

			public override void OnCancel( Mobile from )
			{
				from.SendLocalizedMessage( 502415 ); // Request cancelled.

				if ( !m_Book.Deleted && from.InRange( m_Book.GetWorldLocation(), (Core.ML ? 3 : 1) ) )
				{
					from.CloseGump( typeof( RunebookGump ) );
					from.SendGump( new RunebookGump( from, m_Book ) );
				}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			from.SendSound( 0x55 );

			if ( m_Book.Deleted || !from.InRange( m_Book.GetWorldLocation(), (Core.ML ? 3 : 1) ) || !Multis.DesignContext.Check( from ) )
			{
				m_Book.Openers.Remove( from );
				return;
			}

			int buttonID = info.ButtonID;

			if ( buttonID == 1 ) // Rename book
			{
				if ( !m_Book.IsLockedDown || from.AccessLevel >= AccessLevel.GameMaster )
				{
					from.SendLocalizedMessage( 502414 ); // Please enter a title for the runebook:
					from.Prompt = new InternalPrompt( m_Book );
				}
				else
				{
					m_Book.Openers.Remove( from );
					
					from.SendLocalizedMessage( 502413, null, 0x35 ); // That cannot be done while the book is locked down.
				}
			}
			else if ( buttonID > 600 && buttonID < 700 )
			{
				buttonID -= 602;

				int index = buttonID / 6;
				int type = buttonID % 6;

				if ( index >= 0 && index < m_Book.Entries.Count )
				{
					RunebookEntry e = (RunebookEntry)m_Book.Entries[index];

					switch ( type )
					{
						case 1: // Nature Passage
						{
							if ( from.Backpack.FindItemByType( typeof ( NaturesPassagePotion ) ) == null )
							{
								from.SendMessage( "You do not have that potion!" );
							}
							else
							{
								int xLong = 0, yLat = 0;
								int xMins = 0, yMins = 0;
								bool xEast = false, ySouth = false;

								if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
								{
									string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
									from.SendMessage( location );
								}

								m_Book.OnTravel();
								new NaturesPassageSpell( from, null, e, null ).Cast();
								from.SendMessage( "You empty a jar in the attempt." );
								from.AddToBackpack( new Jar() );
								(from.Backpack.FindItemByType( typeof ( NaturesPassagePotion ) )).Consume();
							}
							break;
						}
						case 2: // Mushroom Gateway
						{
							if ( from.Backpack.FindItemByType( typeof ( MushroomGatewayPotion ) ) == null )
							{
								from.SendMessage( "You do not have that potion!" );
							}
							else
							{
								int xLong = 0, yLat = 0;
								int xMins = 0, yMins = 0;
								bool xEast = false, ySouth = false;

								if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
								{
									string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
									from.SendMessage( location );
								}

								m_Book.OnTravel();
								new MushroomGatewaySpell( from, null, e ).Cast();
								from.SendMessage( "You empty a jar in the attempt." );
								from.AddToBackpack( new Jar() );
								(from.Backpack.FindItemByType( typeof ( MushroomGatewayPotion ) )).Consume();
							}
							break;
						}
						case 3: // Demonic Fire
						{
							if ( from.Backpack.FindItemByType( typeof ( HellsGateScroll ) ) == null )
							{
								from.SendMessage( "You do not have that potion!" );
							}
							else
							{
								int xLong = 0, yLat = 0;
								int xMins = 0, yMins = 0;
								bool xEast = false, ySouth = false;

								if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
								{
									string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
									from.SendMessage( location );
								}

								m_Book.OnTravel();
								new HellsGateSpell( from, null, e, null ).Cast();
								from.SendMessage( "You empty a jar in the attempt." );
								from.AddToBackpack( new Jar() );
								(from.Backpack.FindItemByType( typeof ( HellsGateScroll ) )).Consume();
							}
							break;
						}
						case 4: // Black Gate
						{
							if ( from.Backpack.FindItemByType( typeof ( GraveyardGatewayScroll ) ) == null )
							{
								from.SendMessage( "You do not have that potion!" );
							}
							else
							{
								int xLong = 0, yLat = 0;
								int xMins = 0, yMins = 0;
								bool xEast = false, ySouth = false;

								if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
								{
									string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
									from.SendMessage( location );
								}

								m_Book.OnTravel();
								new UndeadGraveyardGatewaySpell( from, null, e ).Cast();
								from.SendMessage( "You empty a jar in the attempt." );
								from.AddToBackpack( new Jar() );
								(from.Backpack.FindItemByType( typeof ( GraveyardGatewayScroll ) )).Consume();
							}
							break;
						}
						case 5: // Astral Travel
						{
							if ( HasSpell( from, 251 ) )
							{
								int xLong = 0, yLat = 0;
								int xMins = 0, yMins = 0;
								bool xEast = false, ySouth = false;

								if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
								{
									string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
									from.SendMessage( location );
								}

								m_Book.OnTravel();
								new AstralTravel( from, null, e, null ).Cast();
							}
							else
							{
								from.SendMessage( "You do not have that skill!" );
							}
							
							m_Book.Openers.Remove( from );

							break;
						}
					}
				}
			}
			else if ( buttonID > 700 )
			{
				buttonID -= 702;

				int index = buttonID / 6;
				int type = buttonID % 6;

				if ( index >= 0 && index < m_Book.Entries.Count )
				{
					RunebookEntry e = (RunebookEntry)m_Book.Entries[index];

					switch ( type )
					{
						case 1: // Ethereal Travel
						{
							int xLong = 0, yLat = 0;
							int xMins = 0, yMins = 0;
							bool xEast = false, ySouth = false;

							if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
							{
								string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
								from.SendMessage( location );
							}

							m_Book.OnTravel();
							new ResearchEtherealTravel( from, null, e, null ).Cast();
							
							m_Book.Openers.Remove( from );

							break;
						}
					}
				}
				else
					m_Book.Openers.Remove( from );
			}
			else
			{
				buttonID -= 2;

				int index = buttonID / 6;
				int type = buttonID % 6;

				if ( index >= 0 && index < m_Book.Entries.Count )
				{
					RunebookEntry e = (RunebookEntry)m_Book.Entries[index];

					switch ( type )
					{
						case 0: // Use charges
						{
							if ( m_Book.CurCharges <= 0 )
							{
								from.CloseGump( typeof( RunebookGump ) );
								from.SendGump( new RunebookGump( from, m_Book ) );

								from.SendLocalizedMessage( 502412 ); // There are no charges left on that item.
							}
							else
							{
								int xLong = 0, yLat = 0;
								int xMins = 0, yMins = 0;
								bool xEast = false, ySouth = false;

								if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
								{
									string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
									from.SendMessage( location );
								}

								m_Book.OnTravel();
								new RecallSpell( from, m_Book, e, m_Book ).Cast();
								
								m_Book.Openers.Remove( from );
							}

							break;
						}
						case 1: // Drop rune
						{
							if ( !m_Book.IsLockedDown || from.AccessLevel >= AccessLevel.GameMaster )
							{
								m_Book.DropRune( from, e, index );

								from.CloseGump( typeof( RunebookGump ) );
								if ( !Core.ML )
									from.SendGump( new RunebookGump( from, m_Book ) );
							}
							else
							{
								m_Book.Openers.Remove( from );
								
								from.SendLocalizedMessage( 502413, null, 0x35 ); // That cannot be done while the book is locked down.
							}

							break;
						}
						case 2: // Set default
						{
							if ( m_Book.CheckAccess( from ) )
							{
								m_Book.Default = e;

								from.CloseGump( typeof( RunebookGump ) );
								from.SendGump( new RunebookGump( from, m_Book ) );

								from.SendLocalizedMessage( 502417 ); // New default location set.
							}

							break;
						}
						case 3: // Recall
						{
							if ( HasSpell( from, 31 ) )
							{
								int xLong = 0, yLat = 0;
								int xMins = 0, yMins = 0;
								bool xEast = false, ySouth = false;

								if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
								{
									string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
									from.SendMessage( location );
								}

								m_Book.OnTravel();
								new RecallSpell( from, null, e, null ).Cast();
							}
							else
							{
								from.SendLocalizedMessage( 500015 ); // You do not have that spell!
							}
							
							m_Book.Openers.Remove( from );

							break;
						}
						case 4: // Gate
						{
							if ( HasSpell( from, 51 ) )
							{
								int xLong = 0, yLat = 0;
								int xMins = 0, yMins = 0;
								bool xEast = false, ySouth = false;

								if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
								{
									string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
									from.SendMessage( location );
								}

								m_Book.OnTravel();
								new GateTravelSpell( from, null, e ).Cast();
							}
							else
							{
								from.SendLocalizedMessage( 500015 ); // You do not have that spell!
							}
							
							m_Book.Openers.Remove( from );

							break;
						}
						case 5: // Sacred Journey
						{
							if ( Core.AOS )
							{
								if ( HasSpell( from, 209 ) )
								{
									int xLong = 0, yLat = 0;
									int xMins = 0, yMins = 0;
									bool xEast = false, ySouth = false;

									if ( Sextant.Format( e.Location, e.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
									{
										string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
										from.SendMessage( location );
									}

									m_Book.OnTravel();
									new SacredJourneySpell( from, null, e, null ).Cast();
								}
								else
								{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
								}
							}
							
							m_Book.Openers.Remove( from );

							break;
						}
					}
				}
				else
					m_Book.Openers.Remove( from );
			}
		}
	}
}