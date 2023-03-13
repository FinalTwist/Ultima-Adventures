using System;
using Server;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;
using Server.Prompts;
using Server.Gumps;

namespace Server.Gumps 
{
    public class SkillTitleGump : Gump
    {
        public SkillTitleGump ( Mobile from ) : base ( 25,25 )
        {
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

			AddHtml( 175, 86, 500, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>CHOOSE YOUR SKILL TITLE</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 175, 118, 500, 21, @"<BODY><BIG><BASEFONT Color=#FFA200>" + from.Name + " the " + GetPlayerInfo.GetSkillTitle( from ) + "</BASEFONT><BIG></BODY>", (bool)false, (bool)false);

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
			int skillTitle = DB.CharacterSkill;

			int x = 100;
			int z = 140;
			int y = 115;
			int b = 3609;
			int i = 1;
			string color = "#FCFF00";

			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Alchemy</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Anatomy</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Animal Lore</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Animal Taming</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Archery</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Arms Lore</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Begging</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Blacksmithing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Bushido</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Camping</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Carpentry</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Cartography</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Chivalry</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Cooking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Detect Hidden</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;

			x = 350;
			z = 390;
			y = 115;
			b = 3609;

			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Discordance</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Eval Intelligence</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Fencing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Fishing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Fletching</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Focus</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Forensics</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Healing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Herding</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Hiding</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Inscription</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Item ID</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Lockpicking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Lumberjacking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Mace Fighting</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;

			x = 600;
			z = 640;
			y = 115;
			b = 3609;

			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Magery</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Resist Spells</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Meditation</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Mining</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Musicianship</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Necromancy</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Ninjitsu</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Parrying</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Peacemaking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Poisoning</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Provocation</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Remove Trap</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Snooping</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Spirit Speak</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Stealing</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;

			x = 850;
			z = 890;
			y = 115;
			b = 3609;

			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Stealth</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Swordsmanship</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Tactics</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Tailoring</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Taste ID</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Tinkering</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Tracking</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Veterinary</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;
			if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Wrestling</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i++;

			y = y + 80;

			if ( skillTitle == 0 ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
				AddButton(x, y, b, b, 99, GumpButtonType.Reply, 0);
				AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Auto Title</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			if ( from.StatCap > 275 )
			{
				if ( skillTitle == i ){ b = 4018; color = "#FFA200"; } else { b = 3609; color = "#FCFF00"; } y = y + 40;
					AddButton(x, y, b, b, i, GumpButtonType.Reply, 0);
					AddHtml( z, y, 150, 21, @"<BODY><BASEFONT Color=" + color + "><BIG>Titan of Ether</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
        }
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

			if ( info.ButtonID == 99 ){ DB.CharacterSkill = 0; from.SendSound( 0x4A ); }
			else if ( info.ButtonID > 0 ){ DB.CharacterSkill = info.ButtonID; from.SendSound( 0x4A ); }

			if ( info.ButtonID > 0 ){ from.SendGump( new SkillTitleGump( from ) ); }
			else { from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) ); }
		}
    }
}