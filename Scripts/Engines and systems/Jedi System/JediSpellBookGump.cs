using System;
using Server; 
using System.Collections;
using System.Globalization;
using Server.Items; 
using Server.Misc; 
using Server.Network; 
using Server.Spells; 
using Server.Spells.Jedi; 
using Server.Prompts;

namespace Server.Gumps 
{ 
	public class JediSpellbookGump : Gump 
	{
		private JediSpellbook m_Book; 

		public bool HasSpell( int spellID )
		{
			return (m_Book.HasSpell(spellID));
		}

		public JediSpellbookGump( Mobile from, JediSpellbook book, int page ) : base( 25, 25 ) 
		{
			m_Book = book; 

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 30521);

			if ( page == 1 )
			{
				AddImage(517, 40, 11435);

				AddHtml( 50, 40, 437, 20, @"<BODY><BASEFONT Color=#308EB3>DATACRON OF JEDI WISDOM</BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(443, 40, 3610, 3610, 2, GumpButtonType.Reply, 0);

				AddItem(43, 81, 16314);
				AddItem(60, 131, 9698);
				AddItem(51, 181, 0x3003, 0xB96);
				AddHtml( 110, 85, 70, 20, @"<BODY><BASEFONT Color=#308EB3>Skill:</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 110, 135, 70, 20, @"<BODY><BASEFONT Color=#308EB3>Mana:</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 110, 185, 70, 20, @"<BODY><BASEFONT Color=#308EB3>Crystals:</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 210, 85, 55, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Jedi.JediSpell.GetJediSkillMax( from)  + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 210, 135, 55, 20, @"<BODY><BASEFONT Color=#00FF06>" + from.ManaMax + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 210, 185, 55, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Jedi.JediSpell.GetCrystals( from ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(305, 122, 0x2162);
				AddHtml( 360, 135, 70, 20, @"<BODY><BASEFONT Color=#308EB3>Power:</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 460, 135, 55, 20, @"<BODY><BASEFONT Color=#00FF06>" + ((int)(Server.Spells.Jedi.JediSpell.GetJediDamage(from))) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				if ( Server.Misc.GetPlayerInfo.isJedi ( from, true ) ){ AddHtml( 310, 85, 183, 20, @"<BODY><BASEFONT Color=#00FF06>You Are One With The Jedi</BASEFONT></BODY>", (bool)false, (bool)false); }
				else { AddHtml( 310, 85, 183, 20, @"<BODY><BASEFONT Color=#308EB3>You Are Not A Jedi!</BASEFONT></BODY>", (bool)false, (bool)false); }

				AddHtml( 105, 236, 246, 20, @"<BODY><BASEFONT Color=#308EB3>Open Horizontal Quick Bar</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(60, 236, 4005, 4005, 3, GumpButtonType.Reply, 0);
				AddHtml( 105, 276, 246, 20, @"<BODY><BASEFONT Color=#308EB3>Open Vertical Quick Bar</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(60, 276, 4005, 4005, 4, GumpButtonType.Reply, 0);

				int name_show = 3609; if ( m_Book.names > 0 ){ name_show = 4017; }

				AddHtml( 105, 316, 246, 20, @"<BODY><BASEFONT Color=#308EB3>Vertical Bar Names</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(60, 316, name_show, name_show, 5, GumpButtonType.Reply, 0);

				AddHtml( 350, 236, 246, 20, @"<BODY><BASEFONT Color=#308EB3>Close Quick Bars</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 236, 4005, 4005, 9, GumpButtonType.Reply, 0);

				AddHtml( 350, 316, 246, 20, @"<BODY><BASEFONT Color=#308EB3>Construct Laser Sword</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 316, 4011, 4011, 6, GumpButtonType.Reply, 0);

				string wordColor = "#308EB3";

				wordColor = "#308EB3";
				if ( HasSpell( from, 280 ) ){ wordColor = "#00FF06"; AddButton(100, 380, 4005, 4005, 380, GumpButtonType.Reply, 0); }
				else { AddImage(100, 380, 4005 ); }
				AddItem(60, 377, 65498);
				AddHtml( 145, 380, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 280, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 380, 4011, 4011, 280, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 281 ) ){ wordColor = "#00FF06"; AddButton(100, 430, 4005, 4005, 381, GumpButtonType.Reply, 0); }
				else { AddImage(100, 430, 4005 ); }
				AddItem(60, 427, 65498);
				AddHtml( 145, 430, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 281, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 430, 4011, 4011, 281, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 282 ) ){ wordColor = "#00FF06"; AddButton(100, 480, 4005, 4005, 382, GumpButtonType.Reply, 0); }
				else { AddImage(100, 480, 4005 ); }
				AddItem(60, 477, 65498);
				AddHtml( 145, 480, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 282, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 480, 4011, 4011, 282, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 283 ) ){ wordColor = "#00FF06"; AddButton(100, 530, 4005, 4005, 383, GumpButtonType.Reply, 0); }
				else { AddImage(100, 530, 4005 ); }
				AddItem(60, 527, 65498);
				AddHtml( 145, 530, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 283, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 530, 4011, 4011, 283, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 284 ) ){ wordColor = "#00FF06"; AddButton(100, 580, 4005, 4005, 384, GumpButtonType.Reply, 0); }
				else { AddImage(100, 580, 4005 ); }
				AddItem(60, 577, 65498);
				AddHtml( 145, 580, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 284, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 580, 4011, 4011, 284, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 285 ) ){ wordColor = "#00FF06"; AddButton(470, 380, 4005, 4005, 385, GumpButtonType.Reply, 0); }
				else { AddImage(470, 380, 4005 ); }
				AddItem(430, 377, 65498);
				AddHtml( 515, 380, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 285, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 380, 4011, 4011, 285, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 286 ) ){ wordColor = "#00FF06"; AddButton(470, 430, 4005, 4005, 386, GumpButtonType.Reply, 0); }
				else { AddImage(470, 430, 4005 ); }
				AddItem(430, 427, 65498);
				AddHtml( 515, 430, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 286, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 430, 4011, 4011, 286, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 287 ) ){ wordColor = "#00FF06"; AddButton(470, 480, 4005, 4005, 387, GumpButtonType.Reply, 0); }
				else { AddImage(470, 480, 4005 ); }
				AddItem(430, 477, 65498);
				AddHtml( 515, 480, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 287, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 480, 4011, 4011, 287, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 288 ) ){ wordColor = "#00FF06"; AddButton(470, 530, 4005, 4005, 388, GumpButtonType.Reply, 0); }
				else { AddImage(470, 530, 4005 ); }
				AddItem(430, 527, 65498);
				AddHtml( 515, 530, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 288, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 530, 4011, 4011, 288, GumpButtonType.Reply, 0);

				wordColor = "#308EB3"; 
				if ( HasSpell( from, 289 ) ){ wordColor = "#00FF06"; AddButton(470, 580, 4005, 4005, 389, GumpButtonType.Reply, 0); }
				else { AddImage(470, 580, 4005 ); }
				AddItem(430, 577, 65498);
				AddHtml( 515, 580, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Jedi.JediSpell.SpellInfo( 289, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 580, 4011, 4011, 289, GumpButtonType.Reply, 0);
			}
			else if ( page == 2 )
			{
				AddHtml( 50, 40, 437, 20, @"<BODY><BASEFONT Color=#308EB3>DATACRON OF JEDI WISDOM</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 57, 77, 652, 528, @"<BODY><BASEFONT Color=#00FF06>You can hear the voice of Zoda the Jedi Master from within your own mind. He tells you that if you are worthy of the path, you can one day become a powerful Jedi yourself. A Jedi pursues their goals with their psychic abilities and their sword. Any other type of weapon is virtually useless to a Jedi. <br><br>He continues to explain that the knowledge this datacron contained was given by ten members of the Jedi order. You will need to find their resting places to obtain each of the Jedi powers once wielded by the Jedi. Each power is described within the datacron, and the location of where an aspiring Jedi may find the graves. Find the grave, speak the true name of the one resting there, and gain the wisdom of the Jedi.<br><br>To be one with the Jedi, you need to have positive karma. The greater your karma, the more light you possess and thus your powers will have a potential of greater effect. To use a Jedi power, one would need an equal amount of skill in both evaluating intelligence and sword fighting. Tactics can help a Jedi not only fight better, but tactics (along with sword fighting) can also increase the effectiveness of Jedi powers. There are certain criteria to be a Jedi. They must always wield a sword and wear clothing or trinkets named after our order. These could be either trinkets, robes, hoods, cowls, or shields. Only one piece of clothing or trinket need be worn at a time. Without a datacron of their own, a Jedi cannot be. Since datacrons cannot be created on this world, you will have to embrace Zoda's. Keep it with you at all times or you will not be one with the Jedi. If you find clothing, or trinkets that you wish to keep, place them onto the datacron and it will transform the item into something appropriate for a Jedi.<br><br>A Jedi cannot rely solely on the power of the mind, as we need the energy from karan crystals to power our datacrons and give us the force we need to use our power. We could not find karan crystals on this world, as they are normally from the Jedi home world. The ship I came here on did have a quadrit catalizer, however, that can cystalize the molecular structure of gold into karan crystals. I left this device with Jacen in Britain, so he may be able to help you turn gold coins into these crystals. If you find this datacron many years beyond his passing, perhaps he had a child that also became a priest and may still be in Britain. It is up to you to seek them out if this is true, and see if you can give them gold coins they will turn into these crystals for you. We also discovered that demonic creatures have an element of karan crystals within their bones. This was by accident, when I slain a daemon terrorizing a family. It was a discovery that helped the Jedi to exist here, and you must know this if you pursue this life. If the device no longer exists, then you must vanquish the demons of the land and use the shards from their bones to undo the evil they wrought. There is a magical property on this world with lower reagent qualities. These are helpful for a Jedi in regards to syrcarak crystals, but only half as such as wizards are granted.<br><br>If you want to have the grand title of 'Jedi', then make sure your skill title is set to that of evaluating intelligence and that you also follow the Jedi ways. If you also pursue the passage of becoming a grandmaster in this world's noble acts of chivalry, then you can be a 'Jedi Knight'.<br><br>Go forth, and bring peace to this world. Remain in the light and beware the darkness!<br><br>You can use these powers by a typed command, which allows you to make macros for using these if you want. Each of these commands are listed below:<br><br>[ForceGrip<br><br>[MindsEye<br><br>[Mirage<br><br>[ThrowSabre<br><br>[Celerity<br><br>[PsychicAura<br><br>[Deflection<br><br>[SoothingTouch<br><br>[StasisField<br><br>[Replicate <br><br><br><br></BASEFONT></BODY>", (bool)false, (bool)true);
				AddButton(691, 38, 4017, 4017, 1, GumpButtonType.Reply, 0);
				AddItem(634, 34, 65498);
			}
			else if ( page == 6 )
			{
				AddHtml( 50, 40, 437, 20, @"<BODY><BASEFONT Color=#308EB3>DATACRON OF JEDI WISDOM</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(691, 38, 4017, 4017, 1, GumpButtonType.Reply, 0);
				AddImage(52, 78, 11435);

				AddItem(273, 80, 7153, 0x7A9);

				if ( m_Book.steel > 0 ){ AddHtml( 325, 81, 190, 20, @"<BODY><BASEFONT Color=#00FF06>You Have Durasteel</BASEFONT></BODY>", (bool)false, (bool)false); }
				else { AddHtml( 325, 81, 190, 20, @"<BODY><BASEFONT Color=#308EB3>You Need Durasteel!</BASEFONT></BODY>", (bool)false, (bool)false); }

				int jewel = 0xB38;
				if ( m_Book.gem > 0 ){ jewel = m_Book.gem; AddHtml( 325, 126, 190, 20, @"<BODY><BASEFONT Color=#00FF06>You Have A Gem</BASEFONT></BODY>", (bool)false, (bool)false); }
				else { AddHtml( 325, 126, 190, 20, @"<BODY><BASEFONT Color=#308EB3>You Need A Gem!</BASEFONT></BODY>", (bool)false, (bool)false); }
				AddItem(272, 131, 11421, jewel);

				AddHtml( 278, 221, 436, 382, @"<BODY><BASEFONT Color=#00FF06>When a Jedi has reached the level of grandmaster in intelligence evaluation, sword fighting, and tactics they can construct their own laser sword. In order to do this, they will need to find an ordinary gem of their choice. These are gems like rubies, emeralds, or sapphires. The gem will set the color for the blade of the laser sword after it is constructed and emit the light needed for the blade, where this gem is put into this datacron. They will also need a piece of durasteel, which is not a metal born of this world. If they find a piece of durasteel metal armor or weapon, they could use that for the handle of the sword as this datacron will melt it down into the needed ingots. They can replace gems at any time before construction if they want to choose a different color for the blade. They will also need 10,000 gold in their pack for additional materials. The construction will require a Jedi to mediate deeply to power the creation of the blade, where 15,000 karma will be required. They will also need 15,000 points of fame. Both elements of the Jedi will be set to zero after the creation of the laser sword, where the Jedi will be greatly weakened in their powers until their wisdom grows strong again.<br><br>The laser sword will begin as something ordinary, but as long as it is used it will grow in power as victories are achieved over the many fearsome foes of the lands. This sword will never need to be repaired. If a Jedi meets an untimely end, they will have it in their possession when they return to the living. Certain traps that affect equipped items will have no adverse effects on this. Creatures, that attempt to ruin items, will fail in the attempt. If a Jedi is careless with the sword, and leave it lying about, then fate will speak for what may happen to it. This sword will gain levels as it achieves victory over a Jedi’s adversaries. When the sword gains a level, a Jedi can single click on it and select 'Status' to give the sword more power. Be careful adding powers, as one cannot change any attributes once they select them. A Jedi can use regular dye tubs on these, making them any color they choose if the gem they selected does not meet their expectations. They must also be in Zoda's tomb to construct this laser sword, where they will have their choice of a regular or a double bladed weapon.</BASEFONT></BODY>", (bool)false, (bool)true);

				AddItem(132, 416, 16314);
				AddItem(136, 525, 11499);

				Region reg = Region.Find( from.Location, from.Map );

				if ( !Server.Misc.GetPlayerInfo.isJedi ( from, false ) || from.Skills[SkillName.EvalInt].Value < 100 || from.Skills[SkillName.Tactics].Value < 100 || from.Skills[SkillName.Swords].Value < 100 || from.Fame < 15000 || from.Karma < 15000 )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#308EB3>You lack the attributes as a Jedi to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( !( reg.IsPartOf( "the Tomb of Zoda the Jedi Master" ) ) )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#308EB3>You need to be at Zoda's tomb to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Book.steel < 1 )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#308EB3>You need a piece of durasteel to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Book.gem < 1 )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#308EB3>You need an ordinary gem to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( GetWealth( from ) < 10000 )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#308EB3>You need 10,000 gold to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else
				{
					AddButton(97, 426, 4005, 4005, 7, GumpButtonType.Reply, 0);
					AddButton(97, 538, 4005, 4005, 8, GumpButtonType.Reply, 0);
				}
			}
			else if ( page >= 280 && page < 290 )
			{
				AddHtml( 50, 40, 437, 20, @"<BODY><BASEFONT Color=#308EB3>DATACRON OF JEDI WISDOM</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(691, 38, 4017, 4017, 1, GumpButtonType.Reply, 0);

				AddImage(510, 43, 11433);
				AddItem(307, 82, 65498);
				AddHtml( 114, 83, 154, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Jedi.JediSpell.SpellInfo( page, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 57, 286, 652, 186, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Jedi.JediSpell.SpellInfo( page, 0 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				int prev = m_Book.page - 1; if ( prev < 280 ){ prev = 289; }
				int next = m_Book.page + 1; if ( next > 289 ){ next = 280; }

				AddImage(56, 73, ( Int32.Parse( Server.Spells.Jedi.JediSpell.SpellInfo( page, 11 ) ) ), 2825);

				AddButton(45, 586, 4014, 4014, prev, GumpButtonType.Reply, 0);
				AddButton(691, 586, 4005, 4005, next, GumpButtonType.Reply, 0);

				AddItem(46, 132, 16314);
					AddHtml( 115, 135, 70, 20, @"<BODY><BASEFONT Color=#308EB3>Skill:</BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 210, 135, 54, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Jedi.JediSpell.SpellInfo( page, 2 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(63, 182, 9698);
					AddHtml( 115, 185, 70, 20, @"<BODY><BASEFONT Color=#308EB3>Mana:</BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 210, 185, 54, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Jedi.JediSpell.SpellInfo( page, 3 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(54, 232, 0x3003, 0xB96);
					AddHtml( 115, 235, 70, 20, @"<BODY><BASEFONT Color=#308EB3>Crystals:</BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 210, 235, 54, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Jedi.JediSpell.SpellInfo( page, 10 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				if ( HasSpell( from, page ) ){ AddHtml( 352, 82, 154, 20, @"<BODY><BASEFONT Color=#00FF06>Learned</BASEFONT></BODY>", (bool)false, (bool)false); }
				else
				{
					AddHtml( 352, 82, 154, 20, @"<BODY><BASEFONT Color=#308EB3>Not Learned</BASEFONT></BODY>", (bool)false, (bool)false);

					string hidden = "You can find the Jedi Holocron of " + Server.Spells.Jedi.JediSpell.SpellInfo( page, 1 ) + " at " + Server.Spells.Jedi.JediSpell.SpellInfo( page, 6 ) + " in the " + Server.Spells.Jedi.JediSpell.SpellInfo( page, 7 ) + ".";
					hidden = hidden + " It is safely in the grave of a Jedi Master known as " + Server.Spells.Jedi.JediSpell.SpellInfo( page, 8 ) + ", and it can be revealed by speaking their true name of '" + Server.Spells.Jedi.JediSpell.SpellInfo( page, 4 ) + "'.<br><br>";

					AddHtml( 57, 480, 652, 90, @"<BODY><BASEFONT Color=#308EB3>" + hidden + "</BASEFONT></BODY>", (bool)false, (bool)false);
				}

				if ( Server.Misc.GetPlayerInfo.isJedi ( from, true ) )
				{
					if ( Server.Spells.Jedi.JediSpell.GetJediSkill( from, ( Int32.Parse( Server.Spells.Jedi.JediSpell.SpellInfo( page, 2 ) ) ) ) )
					{
						AddHtml( 352, 226, 300, 20, @"<BODY><BASEFONT Color=#00FF06>You Are One With The Jedi</BASEFONT></BODY>", (bool)false, (bool)false);
					}
					else
					{
						AddHtml( 352, 226, 300, 20, @"<BODY><BASEFONT Color=#308EB3>Your Jedi Powers Are Not Strong Enough!</BASEFONT></BODY>", (bool)false, (bool)false);
					}
				}
				else { AddHtml( 352, 226, 300, 20, @"<BODY><BASEFONT Color=#308EB3>You Are Not A Jedi!</BASEFONT></BODY>", (bool)false, (bool)false); }
			}
		}

		public static int GetWealth( Mobile from )
		{
			int wealth = 0;

			Container pack = from.Backpack;

			if ( pack != null )
			{
				Item[] gold = pack.FindItemsByType( typeof( Gold ) );

				for ( int i = 0; i < gold.Length; ++i )
					wealth += gold[i].Amount;
			}

			return wealth;
		}

		public static string SwordName( string item, Mobile from )
		{
			string OwnerName = from.Name;
			string sAdjective = CultureInfo.CurrentCulture.TextInfo.ToTitleCase( LootPackEntry.MagicItemAdj( "start", false, false, 0 ) );
			string name = item;

			if ( OwnerName.EndsWith( "s" ) )
			{
				OwnerName = OwnerName + "'";
			}
			else
			{
				OwnerName = OwnerName + "'s";
			}

			int FirstLast = 0;
			if ( Utility.RandomBool() ){ FirstLast = 1; }

			if ( FirstLast == 0 ) // FIRST COMES ADJECTIVE
			{
				name = "the " + sAdjective + " " + item + " of " + from.Name;
			}
			else // FIRST COMES OWNER
			{
				name = OwnerName + " " + sAdjective + " " + item;
			}

			return name;
		}

		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );

			return ( book != null && book.HasSpell( spellID ) );
		}

		public override void OnResponse( NetState state, RelayInfo info ) 
		{
			Mobile from = state.Mobile; 

			if ( info.ButtonID >= 280 && info.ButtonID < 290 )
			{
				m_Book.page = info.ButtonID;
				from.SendSound( 0x54B );
				int page = info.ButtonID;
				from.SendGump( new JediSpellbookGump( from, m_Book, page ) );
			}
			else if ( info.ButtonID >= 380 )
			{
				m_Book.page = 1;
				int spell = info.ButtonID - 100;
				if ( spell == 280 && HasSpell( from, 280 ) ){ new ForceGrip( from, null ).Cast(); }
				else if ( spell == 281 && HasSpell( from, 281 ) ){ new MindsEye( from, null ).Cast(); }
				else if ( spell == 282 && HasSpell( from, 282 ) ){ new Mirage( from, null ).Cast(); }
				else if ( spell == 283 && HasSpell( from, 283 ) ){ new ThrowSabre( from, null ).Cast(); }
				else if ( spell == 284 && HasSpell( from, 284 ) ){ new Celerity( from, null ).Cast(); }
				else if ( spell == 285 && HasSpell( from, 285 ) ){ new PsychicAura( from, null ).Cast(); }
				else if ( spell == 286 && HasSpell( from, 286 ) ){ new Deflection( from, null ).Cast(); }
				else if ( spell == 287 && HasSpell( from, 287 ) ){ new SoothingTouch( from, null ).Cast(); }
				else if ( spell == 288 && HasSpell( from, 288 ) ){ new StasisField( from, null ).Cast(); }
				else if ( spell == 289 && HasSpell( from, 289 ) ){ new Replicate( from, null ).Cast(); }

				from.SendGump( new JediSpellbookGump( from, m_Book, 1 ) );
				from.SendSound( 0x54B );
			}
			else if ( info.ButtonID == 2 )
			{
				m_Book.page = 2;
				from.SendGump( new JediSpellbookGump( from, m_Book, 2 ) );
				from.SendSound( 0x54B );
			}
			else if ( info.ButtonID == 3 ){ from.SendSound( 0x54D ); from.CloseGump( typeof( Server.Items.JediSpellbook.PowerColumn ) ); from.CloseGump( typeof( Server.Items.JediSpellbook.PowerRow ) ); from.SendGump( new Server.Items.JediSpellbook.PowerRow( from, m_Book ) ); }
			else if ( info.ButtonID == 4 ){ from.SendSound( 0x54D ); from.CloseGump( typeof( Server.Items.JediSpellbook.PowerColumn ) ); from.CloseGump( typeof( Server.Items.JediSpellbook.PowerRow ) ); from.SendGump( new Server.Items.JediSpellbook.PowerColumn( from, m_Book ) ); }
			else if ( info.ButtonID == 5 )
			{
				if ( m_Book.names == 1 ){ m_Book.names = 0; } else { m_Book.names = 1; }
				from.SendGump( new JediSpellbookGump( from, m_Book, 1 ) );
				from.SendSound( 0x54B );
			}
			else if ( info.ButtonID == 6 )
			{
				m_Book.page = 6;
				from.SendGump( new JediSpellbookGump( from, m_Book, 6 ) );
				from.SendSound( 0x54B );
			}
			else if ( info.ButtonID == 7 || info.ButtonID == 8 )
			{
				Container pack = from.Backpack;
				if (pack.ConsumeTotal(typeof(Gold), 10000))
				{
					Item sword = new LevelLaserSword();
						if ( info.ButtonID == 8 ){ sword.Delete(); sword = new LevelDoubleLaserSword(); }
					sword.Hue = m_Book.gem;
					from.Fame = 0;
					from.Karma = 0;
					m_Book.gem = 0;
					m_Book.steel = 0;
					sword.Name = SwordName( sword.Name, from );
					from.AddToBackpack ( sword );
					LoggingFunctions.LogCreatedJedi( from, sword.Name );
					from.SendMessage( "You can construct your own laser sword.");
					from.FixedParticles( 0x373A, 9, 32, 5030, 0xB41, 0, EffectLayer.Waist );
					from.PlaySound( 0x5C9 );
				}
			}
			else if ( info.ButtonID == 9 ){ from.SendSound( 0x54D ); from.CloseGump( typeof( Server.Items.JediSpellbook.PowerColumn ) ); from.CloseGump( typeof( Server.Items.JediSpellbook.PowerRow ) );  }
			else if ( m_Book.page > 1 )
			{
				m_Book.page = 1;
				from.SendGump( new JediSpellbookGump( from, m_Book, 1 ) );
				from.SendSound( 0x54B );
			}
			else
			{
				from.SendSound( 0x54D );
			}
		}
	}
}