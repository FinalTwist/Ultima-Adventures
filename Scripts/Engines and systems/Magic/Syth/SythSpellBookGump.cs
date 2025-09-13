using System;
using Server; 
using System.Collections;
using System.Globalization;
using Server.Items; 
using Server.Misc; 
using Server.Network; 
using Server.Spells; 
using Server.Spells.Syth; 
using Server.Prompts;

namespace Server.Gumps 
{ 
	public class SythSpellbookGump : Gump 
	{
		private SythSpellbook m_Book; 

		public bool HasSpell( int spellID )
		{
			return (m_Book.HasSpell(spellID));
		}

		public SythSpellbookGump( Mobile from, SythSpellbook book, int page ) : base( 25, 25 ) 
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
				AddImage(517, 40, 11428);

				AddHtml( 50, 40, 437, 20, @"<BODY><BASEFONT Color=#FF0000>DATACRON OF SYTH KNOWLEDGE</BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(443, 40, 3610, 3610, 2, GumpButtonType.Reply, 0);

				AddItem(43, 81, 16314);
				AddItem(60, 131, 9698);
				AddItem(51, 181, 0x3003, 0x869);
				AddHtml( 110, 85, 70, 20, @"<BODY><BASEFONT Color=#FF0000>Skill:</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 110, 135, 70, 20, @"<BODY><BASEFONT Color=#FF0000>Mana:</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 110, 185, 70, 20, @"<BODY><BASEFONT Color=#FF0000>Crystals:</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 210, 85, 55, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Syth.SythSpell.GetSythSkillMax( from)  + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 210, 135, 55, 20, @"<BODY><BASEFONT Color=#00FF06>" + from.ManaMax + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 210, 185, 55, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Syth.SythSpell.GetCrystals( from ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(305, 122, 8547);
				AddHtml( 360, 135, 70, 20, @"<BODY><BASEFONT Color=#FF0000>Power:</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 460, 135, 55, 20, @"<BODY><BASEFONT Color=#00FF06>" + ((int)(Server.Spells.Syth.SythSpell.GetSythDamage(from))) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				if ( Server.Misc.GetPlayerInfo.isSyth ( from, true ) ){ AddHtml( 310, 85, 183, 20, @"<BODY><BASEFONT Color=#00FF06>You Are One With The Syth</BASEFONT></BODY>", (bool)false, (bool)false); }
				else { AddHtml( 310, 85, 183, 20, @"<BODY><BASEFONT Color=#FF0000>You Are Not A Syth!</BASEFONT></BODY>", (bool)false, (bool)false); }

				AddHtml( 105, 236, 246, 20, @"<BODY><BASEFONT Color=#FF0000>Open Horizontal Quick Bar</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(60, 236, 4005, 4005, 3, GumpButtonType.Reply, 0);
				AddHtml( 105, 276, 246, 20, @"<BODY><BASEFONT Color=#FF0000>Open Vertical Quick Bar</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(60, 276, 4005, 4005, 4, GumpButtonType.Reply, 0);

				int name_show = 3609; if ( m_Book.names > 0 ){ name_show = 4017; }

				AddHtml( 105, 316, 246, 20, @"<BODY><BASEFONT Color=#FF0000>Vertical Bar Names</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(60, 316, name_show, name_show, 5, GumpButtonType.Reply, 0);

				AddHtml( 350, 236, 246, 20, @"<BODY><BASEFONT Color=#FF0000>Close Quick Bars</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 236, 4005, 4005, 9, GumpButtonType.Reply, 0);

				AddHtml( 350, 316, 246, 20, @"<BODY><BASEFONT Color=#FF0000>Construct Laser Sword</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 316, 4011, 4011, 6, GumpButtonType.Reply, 0);

				string wordColor = "#FF0000";

				wordColor = "#FF0000";
				if ( HasSpell( from, 270 ) ){ wordColor = "#00FF06"; AddButton(100, 380, 4005, 4005, 370, GumpButtonType.Reply, 0); }
				else { AddImage(100, 380, 4005 ); }
				AddItem(60, 377, 19679);
				AddHtml( 145, 380, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 270, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 380, 4011, 4011, 270, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 271 ) ){ wordColor = "#00FF06"; AddButton(100, 430, 4005, 4005, 371, GumpButtonType.Reply, 0); }
				else { AddImage(100, 430, 4005 ); }
				AddItem(60, 427, 19679);
				AddHtml( 145, 430, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 271, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 430, 4011, 4011, 271, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 272 ) ){ wordColor = "#00FF06"; AddButton(100, 480, 4005, 4005, 372, GumpButtonType.Reply, 0); }
				else { AddImage(100, 480, 4005 ); }
				AddItem(60, 477, 19679);
				AddHtml( 145, 480, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 272, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 480, 4011, 4011, 272, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 273 ) ){ wordColor = "#00FF06"; AddButton(100, 530, 4005, 4005, 373, GumpButtonType.Reply, 0); }
				else { AddImage(100, 530, 4005 ); }
				AddItem(60, 527, 19679);
				AddHtml( 145, 530, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 273, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 530, 4011, 4011, 273, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 274 ) ){ wordColor = "#00FF06"; AddButton(100, 580, 4005, 4005, 374, GumpButtonType.Reply, 0); }
				else { AddImage(100, 580, 4005 ); }
				AddItem(60, 577, 19679);
				AddHtml( 145, 580, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 274, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(305, 580, 4011, 4011, 274, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 275 ) ){ wordColor = "#00FF06"; AddButton(470, 380, 4005, 4005, 375, GumpButtonType.Reply, 0); }
				else { AddImage(470, 380, 4005 ); }
				AddItem(430, 377, 19679);
				AddHtml( 515, 380, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 275, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 380, 4011, 4011, 275, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 276 ) ){ wordColor = "#00FF06"; AddButton(470, 430, 4005, 4005, 376, GumpButtonType.Reply, 0); }
				else { AddImage(470, 430, 4005 ); }
				AddItem(430, 427, 19679);
				AddHtml( 515, 430, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 276, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 430, 4011, 4011, 276, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 277 ) ){ wordColor = "#00FF06"; AddButton(470, 480, 4005, 4005, 377, GumpButtonType.Reply, 0); }
				else { AddImage(470, 480, 4005 ); }
				AddItem(430, 477, 19679);
				AddHtml( 515, 480, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 277, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 480, 4011, 4011, 277, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 278 ) ){ wordColor = "#00FF06"; AddButton(470, 530, 4005, 4005, 378, GumpButtonType.Reply, 0); }
				else { AddImage(470, 530, 4005 ); }
				AddItem(430, 527, 19679);
				AddHtml( 515, 530, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 278, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 530, 4011, 4011, 278, GumpButtonType.Reply, 0);

				wordColor = "#FF0000"; 
				if ( HasSpell( from, 279 ) ){ wordColor = "#00FF06"; AddButton(470, 580, 4005, 4005, 379, GumpButtonType.Reply, 0); }
				else { AddImage(470, 580, 4005 ); }
				AddItem(430, 577, 19679);
				AddHtml( 515, 580, 154, 20, @"<BODY><BASEFONT Color=" + wordColor + ">" + Server.Spells.Syth.SythSpell.SpellInfo( 279, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(675, 580, 4011, 4011, 279, GumpButtonType.Reply, 0);
			}
			else if ( page == 2 )
			{
				AddHtml( 50, 40, 437, 20, @"<BODY><BASEFONT Color=#FF0000>DATACRON OF SYTH KNOWLEDGE</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 57, 77, 652, 528, @"<BODY><BASEFONT Color=#00FF06>You can hear the voice of Malek the Syth Lord from within your own mind. He tells you that if you are worthy of the path, you can one day become a powerful Syth yourself. A Syth pursues their goals with their psychic abilities and their sword. Any other type of weapon is virtually useless to a Syth. <br><br>He continues to explain that the knowledge this datacron contained was stolen by ten traitors of the Syth order. You will need to find their tombs to obtain each of the Syth powers once wielded by Malak. Each power is described within the datacron, and the location of where an aspiring Syth may find the tomb. Find the tomb, speak the words in the ancient Syth language, and claim your prize.<br><br>To be one with the Syth, you need to have negative karma. The lower your karma, the more rage you possess and thus your powers will have a potential of greater effect. To use a Syth power, one would need an equal amount of skill in both evaluating intelligence and sword fighting. Tactics can help a Syth not only fight better, but tactics (along with sword fighting) can also increase the effectiveness of Syth powers. There are certain criteria to be a Syth. They must always wield a sword and wear clothing or trinkets named after our order. These could be either trinkets, robes, hoods, cowls, or shields. Metallic helmets also can be that of the Syth, and thus count as well. Only one piece of clothing, trinket, or helm need be worn at a time. Without a datacron of their own, a Syth cannot be. Since datacrons cannot be created on this primitive world, you will have to settle for mine. Keep it with you at all times or you will not be one with the Syth. If you find clothing, trinkets, or helms that you wish to keep, place them onto the datacron and it will transform the item into something appropriate for a Syth. <br><br>A Syth cannot rely solely on the power of the mind, as we need the energy we can drain from syrcarak crystals to power our datacrons and give us the force we need to unleash our power. We could not find syrcarak crystals on this world, as they are normally from the Syth home world. We did discover, however, that demonic creatures have an element of syrcarak crystals within their bones. This was discovered by Asajj Ventress by accident, when she cleaved a balron in two. It was a discovery that allowed the Syth to exist here, and you must know this if you pursue this life. With this new found knowledge, we spoke with the demon in the Black Magic Guild. They offered to exchange a syrcarak crystal for every five gold we give him. The necromancers call these crystals hell shards, but no matter the name, we need them. Seek this demon out, and give him the gold he craves. Then you will have the crystals for this datacron and your journey can begin. Otherwise, you must hunt down the demons of the land and claim what you need from their corpses. There is a magical property on this world with lower reagent qualities. These are helpful for a Syth in regards to hell shards, but only half as such for wizards.<br><br>If you want to have the grand title of 'Syth', then make sure your skill title is set to that of evaluating intelligence and that you also follow the Syth ways. Syth are looked upon as a scourge of this world, and many believe us to be death knights at times. Their minds cannot comprehend the origin of our power and that they do not come from spiritual demons or gods. It is the raw power of the raging mind that gives the Syth strength. Nevertheless, we Syth seem to be embraced by the Black Magic Guild and have also been welcome on Dracula’s island to build a home and live. Once a Syth becomes powerful, city guards will attempt to slay you and villages will not welcome you into their borders. We did find two settlements that paid us no attention, and one was a small city of Umbra.<br><br>Go forth, and bring order to this world. Remain in the shadows but rule from the darkness!<br><br>You can use these powers by a typed command, which allows you to make macros for using these if you want. Each of these commands are listed below:<br><br>[Psychokinesis<br><br>[DeathGrip<br><br>[Projection<br><br>[ThrowSword<br><br>[SythSpeed<br><br>[SythLightning<br><br>[Absorption<br><br>[PsychicBlast<br><br>[DrainLife<br><br>[CloneBody <br><br><br><br></BASEFONT></BODY>", (bool)false, (bool)true);
				AddButton(691, 38, 4017, 4017, 1, GumpButtonType.Reply, 0);
				AddItem(634, 34, 19680);
			}
			else if ( page == 6 )
			{
				AddHtml( 50, 40, 437, 20, @"<BODY><BASEFONT Color=#FF0000>DATACRON OF SYTH KNOWLEDGE</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(691, 38, 4017, 4017, 1, GumpButtonType.Reply, 0);
				AddImage(52, 78, 11428);

				AddItem(273, 80, 7153, 0x7A9);

				if ( m_Book.steel > 0 ){ AddHtml( 325, 81, 190, 20, @"<BODY><BASEFONT Color=#00FF06>You Have Durasteel</BASEFONT></BODY>", (bool)false, (bool)false); }
				else { AddHtml( 325, 81, 190, 20, @"<BODY><BASEFONT Color=#FF0000>You Need Durasteel!</BASEFONT></BODY>", (bool)false, (bool)false); }

				int jewel = 0xB38;
				if ( m_Book.gem > 0 ){ jewel = m_Book.gem; AddHtml( 325, 126, 190, 20, @"<BODY><BASEFONT Color=#00FF06>You Have A Gem</BASEFONT></BODY>", (bool)false, (bool)false); }
				else { AddHtml( 325, 126, 190, 20, @"<BODY><BASEFONT Color=#FF0000>You Need A Gem!</BASEFONT></BODY>", (bool)false, (bool)false); }
				AddItem(272, 131, 11421, jewel);

				AddHtml( 278, 221, 436, 382, @"<BODY><BASEFONT Color=#00FF06>When a Syth has reached the level of grandmaster in intelligence evaluation, sword fighting, and tactics they can construct their own laser sword. In order to do this, they will need to find an ordinary gem of their choice. These are gems like rubies, emeralds, or sapphires. The gem will set the color for the blade of the laser sword after it is constructed and emit the light needed for the blade, where this gem is put into the datacron. They will also need a piece of durasteel, which is not a metal born of this world. If they find a piece of durasteel metal armor or weapon, they could use that for the handle of the sword as the datacron will melt it down into the needed ingots. They can replace gems at any time before construction if they want to choose a different color for the blade. They will also need 10,000 gold in their pack for additional materials. The construction will require a Syth to dig deep within their hatred and anger to power the creation of the blade, where -15,000 karma will be required. They will also need 15,000 points of fame. Both elements of the Syth will be set to zero after the creation of the laser sword, where the Syth will be greatly weakened in their powers until their fury grows strong again.<br><br>The laser sword will begin as something ordinary, but as long as it is used it will grow in power as victories are achieved over the many fearsome foes of the lands. This sword will never need to be repaired. If a Syth meets an untimely end, they will have it in their possession when they return to the living. Certain traps that affect equipped items will have no adverse effects on this. Creatures, that attempt to ruin items, will fail in the attempt. If a Syth is careless with the sword, and leave it lying about, then fate will speak for what may happen to it. This sword will gain levels as it achieves victory over a Syth’s adversaries. When the sword gains a level, a Syth can single click on it and select 'Status' to give the sword more power. Be careful adding powers, as one cannot change any attributes once they select them. A Syth can use regular dye tubs on these, making them any color they choose if the gem they selected does not meet their expectations. They must also be in Malak's tomb to construct this laser sword, where they will have their choice of a regular or a double bladed weapon.</BASEFONT></BODY>", (bool)false, (bool)true);

				AddItem(132, 416, 16314);
				AddItem(136, 525, 11499);

				Region reg = Region.Find( from.Location, from.Map );

				if ( !Server.Misc.GetPlayerInfo.isSyth ( from, false ) || from.Skills[SkillName.EvalInt].Value < 100 || from.Skills[SkillName.Tactics].Value < 100 || from.Skills[SkillName.Swords].Value < 100 || from.Fame < MyServerSettings.FameCap() || from.Karma > MyServerSettings.KarmaMin() )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#FF0000>You lack the attributes as a Syth to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( !( reg.IsPartOf( "the Tomb of Malak the Syth Lord" ) ) )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#FF0000>You need to be at Malak's tomb to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Book.steel < 1 )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#FF0000>You need a piece of durasteel to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Book.gem < 1 )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#FF0000>You need an ordinary gem to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( GetWealth( from ) < 10000 )
				{ 
					AddHtml( 281, 182, 430, 20, @"<BODY><BASEFONT Color=#FF0000>You need 10,000 gold to construct a laser sword.</BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else
				{
					AddButton(97, 426, 4005, 4005, 7, GumpButtonType.Reply, 0);
					AddButton(97, 538, 4005, 4005, 8, GumpButtonType.Reply, 0);
				}
			}
			else if ( page >= 270 && page < 280 )
			{
				AddHtml( 50, 40, 437, 20, @"<BODY><BASEFONT Color=#FF0000>DATACRON OF SYTH KNOWLEDGE</BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(691, 38, 4017, 4017, 1, GumpButtonType.Reply, 0);

				AddImage(517, 43, 11426);
				AddItem(307, 82, 19679);
				AddHtml( 114, 83, 154, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Syth.SythSpell.SpellInfo( page, 1 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 57, 286, 652, 186, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Syth.SythSpell.SpellInfo( page, 0 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				int prev = m_Book.page - 1; if ( prev < 270 ){ prev = 279; }
				int next = m_Book.page + 1; if ( next > 279 ){ next = 270; }

				AddImage(56, 73, ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( page, 11 ) ) ), 0x22);

				AddButton(45, 586, 4014, 4014, prev, GumpButtonType.Reply, 0);
				AddButton(691, 586, 4005, 4005, next, GumpButtonType.Reply, 0);

				AddItem(46, 132, 16314);
					AddHtml( 115, 135, 70, 20, @"<BODY><BASEFONT Color=#FF0000>Skill:</BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 210, 135, 54, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Syth.SythSpell.SpellInfo( page, 2 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(63, 182, 9698);
					AddHtml( 115, 185, 70, 20, @"<BODY><BASEFONT Color=#FF0000>Mana:</BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 210, 185, 54, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Syth.SythSpell.SpellInfo( page, 3 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(54, 232, 0x3003, 0x869);
					AddHtml( 115, 235, 70, 20, @"<BODY><BASEFONT Color=#FF0000>Crystals:</BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 210, 235, 54, 20, @"<BODY><BASEFONT Color=#00FF06>" + Server.Spells.Syth.SythSpell.SpellInfo( page, 10 ) + "</BASEFONT></BODY>", (bool)false, (bool)false);

				if ( HasSpell( from, page ) ){ AddHtml( 352, 82, 154, 20, @"<BODY><BASEFONT Color=#00FF06>Learned</BASEFONT></BODY>", (bool)false, (bool)false); }
				else
				{
					AddHtml( 352, 82, 154, 20, @"<BODY><BASEFONT Color=#FF0000>Not Learned</BASEFONT></BODY>", (bool)false, (bool)false);

					string hidden = "You can find the Syth Mysticron of " + Server.Spells.Syth.SythSpell.SpellInfo( page, 1 ) + " at " + Server.Spells.Syth.SythSpell.SpellInfo( page, 6 ) + " in the " + Server.Spells.Syth.SythSpell.SpellInfo( page, 7 ) + ".";
					hidden = hidden + " It is hidden in the tomb of Syth Lord known as " + Server.Spells.Syth.SythSpell.SpellInfo( page, 8 ) + ", and it can be revealed by speaking the words '" + Server.Spells.Syth.SythSpell.SpellInfo( page, 4 ) + "'. The last known coordinates of the tomb's outer entrance is:<br><br>";

					string map = Server.Spells.Syth.SythSpell.SpellInfo( page, 5 );
					Map land = Map.Trammel;
						if ( map == "Felucca" ){ land = Map.Felucca; }

					hidden = hidden + Server.Misc.Worlds.GetAreaEntrance( "" + Server.Spells.Syth.SythSpell.SpellInfo( page, 6 ) + "", land );

					AddHtml( 57, 480, 652, 90, @"<BODY><BASEFONT Color=#FF0000>" + hidden + "</BASEFONT></BODY>", (bool)false, (bool)false);
				}

				if ( Server.Misc.GetPlayerInfo.isSyth ( from, true ) )
				{
					if ( Server.Spells.Syth.SythSpell.GetSythSkill( from, ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( page, 2 ) ) ) ) )
					{
						AddHtml( 352, 226, 300, 20, @"<BODY><BASEFONT Color=#00FF06>You Are One With The Syth</BASEFONT></BODY>", (bool)false, (bool)false);
					}
					else
					{
						AddHtml( 352, 226, 300, 20, @"<BODY><BASEFONT Color=#FF0000>Your Syth Powers Are Not Strong Enough!</BASEFONT></BODY>", (bool)false, (bool)false);
					}
				}
				else { AddHtml( 352, 226, 300, 20, @"<BODY><BASEFONT Color=#FF0000>You Are Not A Syth!</BASEFONT></BODY>", (bool)false, (bool)false); }
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

			if ( info.ButtonID >= 270 && info.ButtonID < 280 )
			{
				m_Book.page = info.ButtonID;
				from.SendSound( 0x54B );
				int page = info.ButtonID;
				from.SendGump( new SythSpellbookGump( from, m_Book, page ) );
			}
			else if ( info.ButtonID >= 370 )
			{
				m_Book.page = 1;
				int spell = info.ButtonID - 100;
				if ( spell == 270 && HasSpell( from, 270 ) ){ new Psychokinesis( from, null ).Cast(); }
				else if ( spell == 271 && HasSpell( from, 271 ) ){ new DeathGrip( from, null ).Cast(); }
				else if ( spell == 272 && HasSpell( from, 272 ) ){ new Projection( from, null ).Cast(); }
				else if ( spell == 273 && HasSpell( from, 273 ) ){ new ThrowSword( from, null ).Cast(); }
				else if ( spell == 274 && HasSpell( from, 274 ) ){ new SythSpeed( from, null ).Cast(); }
				else if ( spell == 275 && HasSpell( from, 275 ) ){ new SythLightning( from, null ).Cast(); }
				else if ( spell == 276 && HasSpell( from, 276 ) ){ new Absorption( from, null ).Cast(); }
				else if ( spell == 277 && HasSpell( from, 277 ) ){ new PsychicBlast( from, null ).Cast(); }
				else if ( spell == 278 && HasSpell( from, 278 ) ){ new DrainLife( from, null ).Cast(); }
				else if ( spell == 279 && HasSpell( from, 279 ) ){ new CloneBody( from, null ).Cast(); }

				from.SendGump( new SythSpellbookGump( from, m_Book, 1 ) );
				from.SendSound( 0x54B );
			}
			else if ( info.ButtonID == 2 )
			{
				m_Book.page = 2;
				from.SendGump( new SythSpellbookGump( from, m_Book, 2 ) );
				from.SendSound( 0x54B );
			}
			else if ( info.ButtonID == 3 ){ from.SendSound( 0x54D ); from.CloseGump( typeof( Server.Items.SythSpellbook.PowerColumn ) ); from.CloseGump( typeof( Server.Items.SythSpellbook.PowerRow ) ); from.SendGump( new Server.Items.SythSpellbook.PowerRow( from, m_Book ) ); }
			else if ( info.ButtonID == 4 ){ from.SendSound( 0x54D ); from.CloseGump( typeof( Server.Items.SythSpellbook.PowerColumn ) ); from.CloseGump( typeof( Server.Items.SythSpellbook.PowerRow ) ); from.SendGump( new Server.Items.SythSpellbook.PowerColumn( from, m_Book ) ); }
			else if ( info.ButtonID == 5 )
			{
				if ( m_Book.names == 1 ){ m_Book.names = 0; } else { m_Book.names = 1; }
				from.SendGump( new SythSpellbookGump( from, m_Book, 1 ) );
				from.SendSound( 0x54B );
			}
			else if ( info.ButtonID == 6 )
			{
				m_Book.page = 6;
				from.SendGump( new SythSpellbookGump( from, m_Book, 6 ) );
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
					LoggingFunctions.LogCreatedSyth( from, sword.Name );
					from.SendMessage( "You can constructed your own laser sword.");
					Point3D blast = new Point3D( ( from.X ), ( from.Y ), from.Z+10 );
					Effects.SendLocationEffect( blast, from.Map, 0x2A4E, 30, 10, 0xB00, 0 );
					from.PlaySound( 0x653 );
				}
			}
			else if ( info.ButtonID == 9 ){ from.SendSound( 0x54D ); from.CloseGump( typeof( Server.Items.SythSpellbook.PowerColumn ) ); from.CloseGump( typeof( Server.Items.SythSpellbook.PowerRow ) );  }
			else if ( m_Book.page > 1 )
			{
				m_Book.page = 1;
				from.SendGump( new SythSpellbookGump( from, m_Book, 1 ) );
				from.SendSound( 0x54B );
			}
			else
			{
				from.SendSound( 0x54D );
			}
		}
	}
}