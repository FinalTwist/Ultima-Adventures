using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Misc;
using Server.Regions;
using System.Collections;
using Server.Accounting;

namespace Server
{
    public class CodexGump : Gump
    {
        private CodexWisdom m_CodexWisdom;

        public CodexGump(CodexWisdom codex): base(25, 25)
        {
            m_CodexWisdom = codex;

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 153);
			AddImage(300, 0, 153);
			AddImage(600, 0, 153);
			AddImage(900, 0, 153);
			AddImage(0, 300, 153);
			AddImage(300, 300, 153);
			AddImage(600, 300, 153);
			AddImage(900, 300, 153);
			AddImage(2, 2, 129);
			AddImage(300, 2, 129);
			AddImage(600, 2, 129);
			AddImage(898, 2, 129);
			AddImage(2, 298, 129);
			AddImage(300, 298, 129);
			AddImage(600, 298, 129);
			AddImage(898, 298, 129);
			AddImage(0, 600, 153);
			AddImage(300, 600, 153);
			AddImage(600, 600, 153);
			AddImage(900, 600, 153);
			AddImage(2, 598, 129);
			AddImage(300, 598, 129);
			AddImage(600, 598, 129);
			AddImage(898, 598, 129);
			AddImage(6, 7, 145);
			AddImage(8, 664, 128);
			AddImage(962, 8, 138);
			AddImage(9, 333, 137);
			AddImage(853, 419, 144);
			AddImage(773, 399, 129);
			AddImage(52, 516, 156);
			AddImage(1052, 549, 156);
			AddImage(662, 47, 132);
			AddImage(410, 835, 130);
			AddImage(548, 794, 134);
			AddImage(784, 863, 140);
			AddImage(868, 654, 147);
			AddImage(754, 865, 159);
			AddImage(368, 47, 132);
			AddImage(212, 47, 132);
			AddImage(170, 8, 139);
			AddImage(166, 7, 156);
			AddImage(204, 9, 156);
			AddImage(184, 9, 156);

			string title = "CHOOSE TWO SKILLS TO STUDY";
			if ( m_CodexWisdom.SkillFirst > 0 && m_CodexWisdom.SkillSecond > 0 ){ title = "CHOOSE A SKILL TO FORGET SO YOU CAN LEARN A DIFFERENT ONE"; }
			else if ( m_CodexWisdom.SkillFirst == 0 && m_CodexWisdom.SkillSecond > 0 ){ title = "CHOOSE ONE SKILL TO STUDY OR ONE TO FORGET SO YOU CAN LEARN A DIFFERENT ONE"; }
			else if ( m_CodexWisdom.SkillFirst > 0 && m_CodexWisdom.SkillSecond == 0 ){ title = "CHOOSE ONE SKILL TO STUDY OR ONE TO FORGET SO YOU CAN LEARN A DIFFERENT ONE"; }

			AddHtml( 175, 86, 900, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + title + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 175, 118, 900, 21, @"<BODY><BIG><BASEFONT Color=#FFA200>If you change your skills and leave the chamber, the lenses will vanish and you may need to find others.</BASEFONT><BIG></BODY>", (bool)false, (bool)false);

			int Skill1 = m_CodexWisdom.SkillFirst;
			int Skill2 = m_CodexWisdom.SkillSecond;

			int x = 100;
			int z = 140;
			int y = 115;
			int b = 3609;
			int i = 1;
			string color = "#FCFF00";

			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Alchemy</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Anatomy</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Animal Lore</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Animal Taming</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Archery</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Arms Lore</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Begging</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Blacksmithing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Bushido</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Camping</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Carpentry</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Cartography</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Chivalry</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Cooking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Detect Hidden</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;

			x = 350;
			z = 390;
			y = 115;
			b = 3609;

			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Discordance</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Eval Intelligence</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Fencing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Fishing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Fletching</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Focus</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Forensics</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Healing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Herding</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Hiding</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Inscription</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Item ID</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Lockpicking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Lumberjacking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Mace Fighting</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;

			x = 600;
			z = 640;
			y = 115;
			b = 3609;

			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Magery</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Resist Spells</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Meditation</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Mining</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Musicianship</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Necromancy</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Ninjitsu</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Parrying</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Peacemaking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Poisoning</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Provocation</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Remove Trap</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Snooping</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Spirit Speak</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Stealing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;

			x = 850;
			z = 890;
			y = 115;
			b = 3609;

			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Stealth</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Swordsmanship</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Tactics</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Tailoring</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Taste ID</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Tinkering</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Tracking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Veterinary</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( Skill1 == i || Skill2 == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Wrestling</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;

			AddImage(900, 492, 50344);
			AddItem(1018, 621, 4643);
			AddItem(1018, 731, 4643);
			AddItem(1013, 618, 1517);
			AddItem(1013, 728, 1518);
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.SendSound( 0x55 );

			if ( info.ButtonID > 0 )
			{
				bool updateBook = true;
				if ( info.ButtonID == m_CodexWisdom.SkillFirst ){ m_CodexWisdom.SkillFirst = 0; }
				else if ( info.ButtonID == m_CodexWisdom.SkillSecond ){ m_CodexWisdom.SkillSecond = 0; }
				else if ( m_CodexWisdom.SkillFirst == 0 ){ m_CodexWisdom.SkillFirst = info.ButtonID; }
				else if ( m_CodexWisdom.SkillSecond == 0 ){ m_CodexWisdom.SkillSecond = info.ButtonID; }
				else { updateBook = false; }

				if ( updateBook ){ Server.CodexWisdom.UpdateCodex( m_CodexWisdom ); }

				from.SendGump( new CodexGump( m_CodexWisdom ) );
			}
		}
    }

    public class LenseGump : Gump
    {
        private CodexWisdom m_CodexWisdom;

        public LenseGump(CodexWisdom codex, int status): base(25, 25)
        {
            m_CodexWisdom = codex;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(1, 0, 154);
			AddImage(300, 0, 154);
			AddImage(2, 2, 163);
			AddImage(302, 2, 163);
			AddImage(159, 2, 163);
			AddImage(167, 235, 130);
			AddImage(8, 64, 128);
			AddImage(451, 230, 143);
			AddItem(11, 14, 2076);

			string phrase = "You will need the Concave and Convex Lenses to read the Codex.";
			if ( status > 0 ){ phrase = "This can only be studied within the Chamber of the Codex."; }

			AddHtml( 77, 9, 509, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + phrase + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(490, 216, 4643);
			AddItem(546, 216, 4643);

			if ( m_CodexWisdom.HasConcaveLense > 0 )
			{
				AddHtml( 77, 40, 509, 43, @"<BODY><BASEFONT Color=#FCFF00><BIG>You have the Concave Lense.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(485, 213, 1517);
			}
			else
			{
				AddHtml( 77, 40, 509, 43, @"<BODY><BASEFONT Color=#FCFF00><BIG>Naxatilor " + Server.Items.VortexCube.GargoyleLocation( "Naxatilor" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			if ( m_CodexWisdom.HasConvexLense > 0 )
			{
				AddHtml( 77, 90, 509, 43, @"<BODY><BASEFONT Color=#FFA200><BIG>You have the Convex Lense.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(541, 213, 1518);
			}
			else
			{
				AddHtml( 77, 90, 509, 43, @"<BODY><BASEFONT Color=#FFA200><BIG>Lor-wis-lem " + Server.Items.VortexCube.GargoyleLocation( "Lor-wis-lem" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			from.SendSound( 0x55 );
		}
    }

    public class CodexWisdom : MagicTalisman
    {
		public Mobile CodexOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Codex_Owner { get{ return CodexOwner; } set{ CodexOwner = value; } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasConvexLense;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_ConvexLense { get { return HasConvexLense; } set { HasConvexLense = value; InvalidateProperties(); } }

		public int HasConcaveLense;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_ConcaveLense { get { return HasConcaveLense; } set { HasConcaveLense = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int SkillFirst;
		[CommandProperty(AccessLevel.Owner)]
		public int Skill_First { get { return SkillFirst; } set { SkillFirst = value; InvalidateProperties(); } }

		public int SkillSecond;
		[CommandProperty(AccessLevel.Owner)]
		public int Skill_Second { get { return SkillSecond; } set { SkillSecond = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int PreviousFirst;
		[CommandProperty(AccessLevel.Owner)]
		public int Previous_First { get { return PreviousFirst; } set { PreviousFirst = value; InvalidateProperties(); } }

		public int PreviousSecond;
		[CommandProperty(AccessLevel.Owner)]
		public int Previous_Second { get { return PreviousSecond; } set { PreviousSecond = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public override bool DisplayLootType{ get{ return false; } }

        [Constructable]
        public CodexWisdom()
        {
			ItemID = 0x081C;
            Weight = 1.0;
			Hue = 0;
            Name = "Codex of Ultimate Wisdom";
            LootType = LootType.Blessed;

			Attributes.BonusInt = 25;
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( CodexOwner != null ){ list.Add( 1049644, "Belongs to " + CodexOwner.Name + "" ); }
        }

		public override bool OnEquip( Mobile from )
		{
			if ( CodexOwner != from )
			{
				from.SendMessage( "This Codex does not belong to you!" );
				return false;
			}

			return base.OnEquip( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				Region reg = Region.Find( from.Location, from.Map );
				Item tome = from.FindItemOnLayer( Layer.Talisman );

				if ( !IsChildOf( from.Backpack ) && tome != this )
				{
					from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
				}
				else if ( CodexOwner != from )
				{
					from.SendMessage( "This Codex does not belong to you so it vanishes!" );
					bool remove = true;
					foreach ( Account a in Accounts.GetAccounts() )
					{
						if (a == null)
							break;

						int index = 0;

						for (int i = 0; i < a.Length; ++i)
						{
							Mobile m = a[i];

							if (m == null)
								continue;

							if ( m == CodexOwner )
							{
								m.AddToBackpack( this );
								remove = false;
							}

							++index;
						}
					}
					if ( remove )
					{
						this.Delete();
					}
				}
				else if ( HasConvexLense == 0 || HasConcaveLense == 0 )
				{
					from.SendSound( 0x55 );
					from.CloseGump( typeof( LenseGump ) );
					from.SendGump( new LenseGump( this, 0 ) );
				}
				else if ( !reg.IsPartOf( "the Chamber of the Codex" ) )
				{
					from.SendSound( 0x55 );
					from.CloseGump( typeof( LenseGump ) );
					from.SendGump( new LenseGump( this, 1 ) );
				}
				else
				{
					from.SendSound( 0x55 );
					from.CloseGump( typeof( CodexGump ) );
					from.SendGump( new CodexGump( this ) );
				}
			}
		}

		public static void UpdateCodex( CodexWisdom book )
		{
			if ( book.SkillFirst > 0 ){		book.SkillBonuses.SetValues(0, GetCodexSkill( book.SkillFirst ), 100); } else { book.SkillBonuses.SetValues(0, SkillName.Alchemy, 0); }
			if ( book.SkillSecond > 0 ){	book.SkillBonuses.SetValues(1, GetCodexSkill( book.SkillSecond ), 100); } else { book.SkillBonuses.SetValues(1, SkillName.Alchemy, 0); }

			book.InvalidateProperties();
		}

		private static SkillName GetCodexSkill( int learn )
		{
			SkillName skill = SkillName.Alchemy;

			if ( learn > 0 )
			{
				if ( learn == 1 ){ skill = SkillName.Alchemy; }
				else if ( learn == 2 ){ skill = SkillName.Anatomy; }
				else if ( learn == 3 ){ skill = SkillName.AnimalLore; }
				else if ( learn == 4 ){ skill = SkillName.AnimalTaming; }
				else if ( learn == 5 ){ skill = SkillName.Archery; }
				else if ( learn == 6 ){ skill = SkillName.ArmsLore; }
				else if ( learn == 7 ){ skill = SkillName.Begging; }
				else if ( learn == 8 ){ skill = SkillName.Blacksmith; }
				else if ( learn == 9 ){ skill = SkillName.Bushido; }
				else if ( learn == 10 ){ skill = SkillName.Camping; }
				else if ( learn == 11 ){ skill = SkillName.Carpentry; }
				else if ( learn == 12 ){ skill = SkillName.Cartography; }
				else if ( learn == 13 ){ skill = SkillName.Chivalry; }
				else if ( learn == 14 ){ skill = SkillName.Cooking; }
				else if ( learn == 15 ){ skill = SkillName.DetectHidden; }
				else if ( learn == 16 ){ skill = SkillName.Discordance; }
				else if ( learn == 17 ){ skill = SkillName.EvalInt; }
				else if ( learn == 18 ){ skill = SkillName.Fencing; }
				else if ( learn == 19 ){ skill = SkillName.Fishing; }
				else if ( learn == 20 ){ skill = SkillName.Fletching; }
				else if ( learn == 21 ){ skill = SkillName.Focus; }
				else if ( learn == 22 ){ skill = SkillName.Forensics; }
				else if ( learn == 23 ){ skill = SkillName.Healing; }
				else if ( learn == 24 ){ skill = SkillName.Herding; }
				else if ( learn == 25 ){ skill = SkillName.Hiding; }
				else if ( learn == 26 ){ skill = SkillName.Inscribe; }
				else if ( learn == 27 ){ skill = SkillName.ItemID; }
				else if ( learn == 28 ){ skill = SkillName.Lockpicking; }
				else if ( learn == 29 ){ skill = SkillName.Lumberjacking; }
				else if ( learn == 30 ){ skill = SkillName.Macing; }
				else if ( learn == 31 ){ skill = SkillName.Magery; }
				else if ( learn == 32 ){ skill = SkillName.MagicResist; }
				else if ( learn == 33 ){ skill = SkillName.Meditation; }
				else if ( learn == 34 ){ skill = SkillName.Mining; }
				else if ( learn == 35 ){ skill = SkillName.Musicianship; }
				else if ( learn == 36 ){ skill = SkillName.Necromancy; }
				else if ( learn == 37 ){ skill = SkillName.Ninjitsu; }
				else if ( learn == 38 ){ skill = SkillName.Parry; }
				else if ( learn == 39 ){ skill = SkillName.Peacemaking; }
				else if ( learn == 40 ){ skill = SkillName.Poisoning; }
				else if ( learn == 41 ){ skill = SkillName.Provocation; }
				else if ( learn == 42 ){ skill = SkillName.RemoveTrap; }
				else if ( learn == 43 ){ skill = SkillName.Snooping; }
				else if ( learn == 44 ){ skill = SkillName.SpiritSpeak; }
				else if ( learn == 45 ){ skill = SkillName.Stealing; }
				else if ( learn == 46 ){ skill = SkillName.Stealth; }
				else if ( learn == 47 ){ skill = SkillName.Swords; }
				else if ( learn == 48 ){ skill = SkillName.Tactics; }
				else if ( learn == 49 ){ skill = SkillName.Tailoring; }
				else if ( learn == 50 ){ skill = SkillName.TasteID; }
				else if ( learn == 51 ){ skill = SkillName.Tinkering; }
				else if ( learn == 52 ){ skill = SkillName.Tracking; }
				else if ( learn == 53 ){ skill = SkillName.Veterinary; }
				else if ( learn == 54 ){ skill = SkillName.Wrestling; }
			}

			return skill;
		}

        public CodexWisdom(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1); // version 

			writer.Write( (Mobile)CodexOwner);

            writer.Write( HasConvexLense );
            writer.Write( HasConcaveLense );

            writer.Write( SkillFirst );
            writer.Write( SkillSecond );

            writer.Write( PreviousFirst );
            writer.Write( PreviousSecond );
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

			CodexOwner = reader.ReadMobile();

			HasConvexLense = reader.ReadInt();
			HasConcaveLense = reader.ReadInt();

			SkillFirst = reader.ReadInt();
			SkillSecond = reader.ReadInt();

			PreviousFirst = reader.ReadInt();
			PreviousSecond = reader.ReadInt();

			ItemID = 0x081C;
            Weight = 1.0;
            Name = "Codex of Ultimate Wisdom";
            LootType = LootType.Blessed;
        }
    }
}