using System;
using Server;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;
using Server.Gumps;

namespace Server.Gumps 
{
    public class MusicPlaylist : Gump
    {
        public MusicPlaylist ( Mobile from ) : base ( 25,25 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(600, 0, 155);
			AddImage(0, 300, 155);
			AddImage(300, 300, 155);
			AddImage(600, 300, 155);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(598, 2, 129);
			AddImage(2, 298, 129);
			AddImage(302, 298, 129);
			AddImage(598, 298, 129);
			AddImage(7, 6, 133);
			AddImage(235, 45, 132);
			AddImage(418, 45, 132);
			AddItem(86, 87, 3763);
			AddImage(684, 0, 155);
			AddImage(684, 300, 155);
			AddImage(600, 582, 155);
			AddImage(0, 582, 155);
			AddImage(300, 582, 155);
			AddImage(684, 582, 155);
			AddImage(682, 2, 129);
			AddImage(682, 298, 129);
			AddImage(2, 580, 129);
			AddImage(300, 580, 129);
			AddImage(598, 580, 129);
			AddImage(682, 580, 129);
			AddImage(564, 641, 136);
			AddImage(264, 812, 130);
			AddImage(166, 812, 130);
			AddImage(7, 770, 139);
			AddImage(611, 45, 132);
			AddImage(755, 6, 134);
			AddItem(14, 691, 2189);
			AddItem(14, 690, 2190);
			AddItem(162, 60, 3738);

			MusicPlaylistFunctions.InitializePlaylist( from );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
			string MySettings = DB.MusicPlaylist;

			int button1 = 3609;
			int button2 = 3609;
			int button3 = 3609;
			int button4 = 3609;
			int button5 = 3609;
			int button6 = 3609;
			int button7 = 3609;
			int button8 = 3609;
			int button9 = 3609;
			int button10 = 3609;
			int button11 = 3609;
			int button12 = 3609;
			int button13 = 3609;
			int button14 = 3609;
			int button15 = 3609;
			int button16 = 3609;
			int button17 = 3609;
			int button18 = 3609;
			int button19 = 3609;
			int button20 = 3609;
			int button21 = 3609;
			int button22 = 3609;
			int button23 = 3609;
			int button24 = 3609;
			int button25 = 3609;
			int button26 = 3609;
			int button27 = 3609;
			int button28 = 3609;
			int button29 = 3609;
			int button30 = 3609;
			int button31 = 3609;
			int button32 = 3609;
			int button33 = 3609;
			int button34 = 3609;
			int button35 = 3609;
			int button36 = 3609;
			int button37 = 3609;
			int button38 = 3609;
			int button39 = 3609;
			int button40 = 3609;
			int button41 = 3609;
			int button42 = 3609;
			int button43 = 3609;
			int button44 = 3609;
			int button45 = 3609;
			int button46 = 3609;
			int button47 = 3609;
			int button48 = 3609;
			int button49 = 3609;
			int button50 = 3609;
			int button51 = 3609;
			int button52 = 3609;
			int button53 = 3609;
			int button54 = 3609;
			int button55 = 3609;
			int button56 = 3609;
			int button57 = 3609;
			int button58 = 3609;
			int button59 = 3609;

			string[] eachSong = MySettings.Split('#');
			int nLine = 1;
			foreach (string eachSongs in eachSong)
			{
				if ( nLine == 1 && eachSongs == "1"){ button1 = 4018; }
				if ( nLine == 2 && eachSongs == "1"){ button2 = 4018; }
				if ( nLine == 3 && eachSongs == "1"){ button3 = 4018; }
				if ( nLine == 4 && eachSongs == "1"){ button4 = 4018; }
				if ( nLine == 5 && eachSongs == "1"){ button5 = 4018; }
				if ( nLine == 6 && eachSongs == "1"){ button6 = 4018; }
				if ( nLine == 7 && eachSongs == "1"){ button7 = 4018; }
				if ( nLine == 8 && eachSongs == "1"){ button8 = 4018; }
				if ( nLine == 9 && eachSongs == "1"){ button9 = 4018; }
				if ( nLine == 10 && eachSongs == "1"){ button10 = 4018; }
				if ( nLine == 11 && eachSongs == "1"){ button11 = 4018; }
				if ( nLine == 12 && eachSongs == "1"){ button12 = 4018; }
				if ( nLine == 13 && eachSongs == "1"){ button13 = 4018; }
				if ( nLine == 14 && eachSongs == "1"){ button14 = 4018; }
				if ( nLine == 15 && eachSongs == "1"){ button15 = 4018; }
				if ( nLine == 16 && eachSongs == "1"){ button16 = 4018; }
				if ( nLine == 17 && eachSongs == "1"){ button17 = 4018; }
				if ( nLine == 18 && eachSongs == "1"){ button18 = 4018; }
				if ( nLine == 19 && eachSongs == "1"){ button19 = 4018; }
				if ( nLine == 20 && eachSongs == "1"){ button20 = 4018; }
				if ( nLine == 21 && eachSongs == "1"){ button21 = 4018; }
				if ( nLine == 22 && eachSongs == "1"){ button22 = 4018; }
				if ( nLine == 23 && eachSongs == "1"){ button23 = 4018; }
				if ( nLine == 24 && eachSongs == "1"){ button24 = 4018; }
				if ( nLine == 25 && eachSongs == "1"){ button25 = 4018; }
				if ( nLine == 26 && eachSongs == "1"){ button26 = 4018; }
				if ( nLine == 27 && eachSongs == "1"){ button27 = 4018; }
				if ( nLine == 28 && eachSongs == "1"){ button28 = 4018; }
				if ( nLine == 29 && eachSongs == "1"){ button29 = 4018; }
				if ( nLine == 30 && eachSongs == "1"){ button30 = 4018; }
				if ( nLine == 31 && eachSongs == "1"){ button31 = 4018; }
				if ( nLine == 32 && eachSongs == "1"){ button32 = 4018; }
				if ( nLine == 33 && eachSongs == "1"){ button33 = 4018; }
				if ( nLine == 34 && eachSongs == "1"){ button34 = 4018; }
				if ( nLine == 35 && eachSongs == "1"){ button35 = 4018; }
				if ( nLine == 36 && eachSongs == "1"){ button36 = 4018; }
				if ( nLine == 37 && eachSongs == "1"){ button37 = 4018; }
				if ( nLine == 38 && eachSongs == "1"){ button38 = 4018; }
				if ( nLine == 39 && eachSongs == "1"){ button39 = 4018; }
				if ( nLine == 40 && eachSongs == "1"){ button40 = 4018; }
				if ( nLine == 41 && eachSongs == "1"){ button41 = 4018; }
				if ( nLine == 42 && eachSongs == "1"){ button42 = 4018; }
				if ( nLine == 43 && eachSongs == "1"){ button43 = 4018; }
				if ( nLine == 44 && eachSongs == "1"){ button44 = 4018; }
				if ( nLine == 45 && eachSongs == "1"){ button45 = 4018; }
				if ( nLine == 46 && eachSongs == "1"){ button46 = 4018; }
				if ( nLine == 47 && eachSongs == "1"){ button47 = 4018; }
				if ( nLine == 48 && eachSongs == "1"){ button48 = 4018; }
				if ( nLine == 49 && eachSongs == "1"){ button49 = 4018; }
				if ( nLine == 50 && eachSongs == "1"){ button50 = 4018; }
				if ( nLine == 51 && eachSongs == "1"){ button51 = 4018; }
				if ( nLine == 52 && eachSongs == "1"){ button52 = 4018; }
				if ( nLine == 53 && eachSongs == "1"){ button53 = 4018; }
				if ( nLine == 54 && eachSongs == "1"){ button54 = 4018; }
				if ( nLine == 55 && eachSongs == "1"){ button55 = 4018; }
				if ( nLine == 56 && eachSongs == "1"){ button56 = 4018; }
				if ( nLine == 57 && eachSongs == "1"){ button57 = 4018; }
				if ( nLine == 58 && eachSongs == "1"){ button58 = 4018; }
				if ( nLine == 59 && eachSongs == "1"){ button59 = 4018; }

				nLine++;
			}

			AddHtml( 218, 65, 250, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>MUSIC PLAYLIST</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(560, 65, button59, button59, 59, GumpButtonType.Reply, 0);
			AddHtml( 600, 65, 200, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Playlist Enabled</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(95, 128, button1, button1, 1, GumpButtonType.Reply, 0);
			AddButton(135, 131, 2117, 2117, 101, GumpButtonType.Reply, 0);
			AddHtml( 160, 128, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Britain*</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 168, button2, button2, 2, GumpButtonType.Reply, 0);
			AddButton(135, 171, 2117, 2117, 102, GumpButtonType.Reply, 0);
			AddHtml( 160, 168, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Buccaneer's Den</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 208, button3, button3, 3, GumpButtonType.Reply, 0);
			AddButton(135, 211, 2117, 2117, 103, GumpButtonType.Reply, 0);
			AddHtml( 160, 208, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Castle British*</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 248, button4, button4, 4, GumpButtonType.Reply, 0);
			AddButton(135, 251, 2117, 2117, 104, GumpButtonType.Reply, 0);
			AddHtml( 160, 248, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Castle of Knowledge*</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 288, button5, button5, 5, GumpButtonType.Reply, 0);
			AddButton(135, 291, 2117, 2117, 105, GumpButtonType.Reply, 0);
			AddHtml( 160, 288, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Death Gulch</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 328, button6, button6, 6, GumpButtonType.Reply, 0);
			AddButton(135, 331, 2117, 2117, 106, GumpButtonType.Reply, 0);
			AddHtml( 160, 328, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Devil Guard</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 368, button7, button7, 7, GumpButtonType.Reply, 0);
			AddButton(135, 371, 2117, 2117, 107, GumpButtonType.Reply, 0);
			AddHtml( 160, 368, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Elidor</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 408, button8, button8, 8, GumpButtonType.Reply, 0);
			AddButton(135, 411, 2117, 2117, 108, GumpButtonType.Reply, 0);
			AddHtml( 160, 408, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Fawn</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 448, button9, button9, 9, GumpButtonType.Reply, 0);
			AddButton(135, 451, 2117, 2117, 109, GumpButtonType.Reply, 0);
			AddHtml( 160, 448, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Grey</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 488, button10, button10, 10, GumpButtonType.Reply, 0);
			AddButton(135, 491, 2117, 2117, 110, GumpButtonType.Reply, 0);
			AddHtml( 160, 488, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Luna</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 528, button11, button11, 11, GumpButtonType.Reply, 0);
			AddButton(135, 531, 2117, 2117, 111, GumpButtonType.Reply, 0);
			AddHtml( 160, 528, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Montor</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 568, button12, button12, 12, GumpButtonType.Reply, 0);
			AddButton(135, 571, 2117, 2117, 112, GumpButtonType.Reply, 0);
			AddHtml( 160, 568, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Moon</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 608, button13, button13, 13, GumpButtonType.Reply, 0);
			AddButton(135, 611, 2117, 2117, 113, GumpButtonType.Reply, 0);
			AddHtml( 160, 608, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Renika</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 648, button14, button14, 14, GumpButtonType.Reply, 0);
			AddButton(135, 651, 2117, 2117, 114, GumpButtonType.Reply, 0);
			AddHtml( 160, 648, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Skara Brae</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 688, button15, button15, 15, GumpButtonType.Reply, 0);
			AddButton(135, 691, 2117, 2117, 115, GumpButtonType.Reply, 0);
			AddHtml( 160, 688, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Time Lord</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(322, 128, button31, button31, 31, GumpButtonType.Reply, 0);
			AddButton(362, 131, 2117, 2117, 131, GumpButtonType.Reply, 0);
			AddHtml( 387, 128, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Clues</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 168, button32, button32, 32, GumpButtonType.Reply, 0);
			AddButton(362, 171, 2117, 2117, 132, GumpButtonType.Reply, 0);
			AddHtml( 387, 168, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Covetous</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 208, button33, button33, 33, GumpButtonType.Reply, 0);
			AddButton(362, 211, 2117, 2117, 133, GumpButtonType.Reply, 0);
			AddHtml( 387, 208, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dardin's Pit</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 248, button34, button34, 34, GumpButtonType.Reply, 0);
			AddButton(362, 251, 2117, 2117, 134, GumpButtonType.Reply, 0);
			AddHtml( 387, 248, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Deceit</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 288, button35, button35, 35, GumpButtonType.Reply, 0);
			AddButton(362, 291, 2117, 2117, 135, GumpButtonType.Reply, 0);
			AddHtml( 387, 288, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Despise</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 328, button36, button36, 36, GumpButtonType.Reply, 0);
			AddButton(362, 331, 2117, 2117, 136, GumpButtonType.Reply, 0);
			AddHtml( 387, 328, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Destard</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 368, button37, button37, 37, GumpButtonType.Reply, 0);
			AddButton(362, 371, 2117, 2117, 137, GumpButtonType.Reply, 0);
			AddHtml( 387, 368, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Doom</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 408, button38, button38, 38, GumpButtonType.Reply, 0);
			AddButton(362, 411, 2117, 2117, 138, GumpButtonType.Reply, 0);
			AddHtml( 387, 408, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Exodus</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 448, button39, button39, 39, GumpButtonType.Reply, 0);
			AddButton(362, 451, 2117, 2117, 139, GumpButtonType.Reply, 0);
			AddHtml( 387, 448, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Fires of Hell</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 488, button40, button40, 40, GumpButtonType.Reply, 0);
			AddButton(362, 491, 2117, 2117, 140, GumpButtonType.Reply, 0);
			AddHtml( 387, 488, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hythloth</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 528, button41, button41, 41, GumpButtonType.Reply, 0);
			AddButton(362, 531, 2117, 2117, 141, GumpButtonType.Reply, 0);
			AddHtml( 387, 528, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mines of Morinia</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 568, button42, button42, 42, GumpButtonType.Reply, 0);
			AddButton(362, 571, 2117, 2117, 142, GumpButtonType.Reply, 0);
			AddHtml( 387, 568, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Perinian Depths</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 608, button43, button43, 43, GumpButtonType.Reply, 0);
			AddButton(362, 611, 2117, 2117, 143, GumpButtonType.Reply, 0);
			AddHtml( 387, 608, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Shame</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 648, button44, button44, 44, GumpButtonType.Reply, 0);
			AddButton(362, 651, 2117, 2117, 144, GumpButtonType.Reply, 0);
			AddHtml( 387, 648, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Time Awaits</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 688, button45, button45, 45, GumpButtonType.Reply, 0);
			AddButton(362, 691, 2117, 2117, 145, GumpButtonType.Reply, 0);
			AddHtml( 387, 688, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wrong</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(547, 128, button16, button16, 16, GumpButtonType.Reply, 0);
			AddButton(587, 131, 2117, 2117, 116, GumpButtonType.Reply, 0);
			AddHtml( 612, 128, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wizard Den</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 168, button17, button17, 17, GumpButtonType.Reply, 0);
			AddButton(587, 171, 2117, 2117, 117, GumpButtonType.Reply, 0);
			AddHtml( 612, 168, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yew</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 208, button18, button18, 18, GumpButtonType.Reply, 0);
			AddButton(587, 211, 2117, 2117, 118, GumpButtonType.Reply, 0);
			AddHtml( 612, 208, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Adventure</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 248, button19, button19, 19, GumpButtonType.Reply, 0);
			AddButton(587, 251, 2117, 2117, 119, GumpButtonType.Reply, 0);
			AddHtml( 612, 248, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Expedition</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 288, button20, button20, 20, GumpButtonType.Reply, 0);
			AddButton(587, 291, 2117, 2117, 120, GumpButtonType.Reply, 0);
			AddHtml( 612, 288, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Explore</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 328, button21, button21, 21, GumpButtonType.Reply, 0);
			AddButton(587, 331, 2117, 2117, 121, GumpButtonType.Reply, 0);
			AddHtml( 612, 328, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hunting</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 368, button22, button22, 22, GumpButtonType.Reply, 0);
			AddButton(587, 371, 2117, 2117, 122, GumpButtonType.Reply, 0);
			AddHtml( 612, 368, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Odyssey</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 408, button23, button23, 23, GumpButtonType.Reply, 0);
			AddButton(587, 411, 2117, 2117, 123, GumpButtonType.Reply, 0);
			AddHtml( 612, 408, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Quest</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 448, button24, button24, 24, GumpButtonType.Reply, 0);
			AddButton(587, 451, 2117, 2117, 124, GumpButtonType.Reply, 0);
			AddHtml( 612, 448, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Roaming</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 488, button25, button25, 25, GumpButtonType.Reply, 0);
			AddButton(587, 491, 2117, 2117, 125, GumpButtonType.Reply, 0);
			AddHtml( 612, 488, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Scouting</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 528, button26, button26, 26, GumpButtonType.Reply, 0);
			AddButton(587, 531, 2117, 2117, 126, GumpButtonType.Reply, 0);
			AddHtml( 612, 528, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Searching</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 568, button27, button27, 27, GumpButtonType.Reply, 0);
			AddButton(587, 571, 2117, 2117, 127, GumpButtonType.Reply, 0);
			AddHtml( 612, 568, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Seeking</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 608, button28, button28, 28, GumpButtonType.Reply, 0);
			AddButton(587, 611, 2117, 2117, 128, GumpButtonType.Reply, 0);
			AddHtml( 612, 608, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Traveling</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 648, button29, button29, 29, GumpButtonType.Reply, 0);
			AddButton(587, 651, 2117, 2117, 129, GumpButtonType.Reply, 0);
			AddHtml( 612, 648, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wandering</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 688, button30, button30, 30, GumpButtonType.Reply, 0);
			AddButton(587, 691, 2117, 2117, 130, GumpButtonType.Reply, 0);
			AddHtml( 612, 688, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Catacombs</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(776, 128, button46, button46, 46, GumpButtonType.Reply, 0);
			AddButton(816, 131, 2117, 2117, 146, GumpButtonType.Reply, 0);
			AddHtml( 841, 128, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Docks</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 168, button47, button47, 47, GumpButtonType.Reply, 0);
			AddButton(816, 171, 2117, 2117, 147, GumpButtonType.Reply, 0);
			AddHtml( 841, 168, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pirates</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 208, button48, button48, 48, GumpButtonType.Reply, 0);
			AddButton(816, 211, 2117, 2117, 148, GumpButtonType.Reply, 0);
			AddHtml( 841, 208, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sailing</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 248, button49, button49, 49, GumpButtonType.Reply, 0);
			AddButton(816, 251, 2117, 2117, 149, GumpButtonType.Reply, 0);
			AddHtml( 841, 248, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cave</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 288, button50, button50, 50, GumpButtonType.Reply, 0);
			AddButton(816, 291, 2117, 2117, 150, GumpButtonType.Reply, 0);
			AddHtml( 841, 288, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Grotto</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 328, button51, button51, 51, GumpButtonType.Reply, 0);
			AddButton(816, 331, 2117, 2117, 151, GumpButtonType.Reply, 0);
			AddHtml( 841, 328, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mines</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 368, button52, button52, 52, GumpButtonType.Reply, 0);
			AddButton(816, 371, 2117, 2117, 152, GumpButtonType.Reply, 0);
			AddHtml( 841, 368, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Alehouse</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 408, button53, button53, 53, GumpButtonType.Reply, 0);
			AddButton(816, 411, 2117, 2117, 153, GumpButtonType.Reply, 0);
			AddHtml( 841, 408, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 448, button54, button54, 54, GumpButtonType.Reply, 0);
			AddButton(816, 451, 2117, 2117, 154, GumpButtonType.Reply, 0);
			AddHtml( 841, 448, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Guild</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 488, button55, button55, 55, GumpButtonType.Reply, 0);
			AddButton(816, 491, 2117, 2117, 155, GumpButtonType.Reply, 0);
			AddHtml( 841, 488, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Inn</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 528, button56, button56, 56, GumpButtonType.Reply, 0);
			AddButton(816, 531, 2117, 2117, 156, GumpButtonType.Reply, 0);
			AddHtml( 841, 528, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Lodge</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 568, button57, button57, 57, GumpButtonType.Reply, 0);
			AddButton(816, 571, 2117, 2117, 157, GumpButtonType.Reply, 0);
			AddHtml( 841, 568, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pub</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 608, button58, button58, 58, GumpButtonType.Reply, 0);
			AddButton(816, 611, 2117, 2117, 158, GumpButtonType.Reply, 0);
			AddHtml( 841, 608, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tavern</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 1 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 1 ); }
			else if ( info.ButtonID == 2 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 2 ); }
			else if ( info.ButtonID == 3 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 3 ); }
			else if ( info.ButtonID == 4 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 4 ); }
			else if ( info.ButtonID == 5 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 5 ); }
			else if ( info.ButtonID == 6 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 6 ); }
			else if ( info.ButtonID == 7 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 7 ); }
			else if ( info.ButtonID == 8 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 8 ); }
			else if ( info.ButtonID == 9 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 9 ); }
			else if ( info.ButtonID == 10 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 10 ); }
			else if ( info.ButtonID == 11 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 11 ); }
			else if ( info.ButtonID == 12 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 12 ); }
			else if ( info.ButtonID == 13 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 13 ); }
			else if ( info.ButtonID == 14 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 14 ); }
			else if ( info.ButtonID == 15 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 15 ); }
			else if ( info.ButtonID == 16 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 16 ); }
			else if ( info.ButtonID == 17 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 17 ); }
			else if ( info.ButtonID == 18 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 18 ); }
			else if ( info.ButtonID == 19 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 19 ); }
			else if ( info.ButtonID == 20 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 20 ); }
			else if ( info.ButtonID == 21 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 21 ); }
			else if ( info.ButtonID == 22 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 22 ); }
			else if ( info.ButtonID == 23 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 23 ); }
			else if ( info.ButtonID == 24 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 24 ); }
			else if ( info.ButtonID == 25 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 25 ); }
			else if ( info.ButtonID == 26 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 26 ); }
			else if ( info.ButtonID == 27 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 27 ); }
			else if ( info.ButtonID == 28 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 28 ); }
			else if ( info.ButtonID == 29 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 29 ); }
			else if ( info.ButtonID == 30 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 30 ); }
			else if ( info.ButtonID == 31 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 31 ); }
			else if ( info.ButtonID == 32 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 32 ); }
			else if ( info.ButtonID == 33 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 33 ); }
			else if ( info.ButtonID == 34 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 34 ); }
			else if ( info.ButtonID == 35 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 35 ); }
			else if ( info.ButtonID == 36 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 36 ); }
			else if ( info.ButtonID == 37 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 37 ); }
			else if ( info.ButtonID == 38 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 38 ); }
			else if ( info.ButtonID == 39 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 39 ); }
			else if ( info.ButtonID == 40 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 40 ); }
			else if ( info.ButtonID == 41 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 41 ); }
			else if ( info.ButtonID == 42 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 42 ); }
			else if ( info.ButtonID == 43 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 43 ); }
			else if ( info.ButtonID == 44 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 44 ); }
			else if ( info.ButtonID == 45 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 45 ); }
			else if ( info.ButtonID == 46 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 46 ); }
			else if ( info.ButtonID == 47 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 47 ); }
			else if ( info.ButtonID == 48 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 48 ); }
			else if ( info.ButtonID == 49 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 49 ); }
			else if ( info.ButtonID == 50 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 50 ); }
			else if ( info.ButtonID == 51 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 51 ); }
			else if ( info.ButtonID == 52 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 52 ); }
			else if ( info.ButtonID == 53 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 53 ); }
			else if ( info.ButtonID == 54 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 54 ); }
			else if ( info.ButtonID == 55 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 55 ); }
			else if ( info.ButtonID == 56 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 56 ); }
			else if ( info.ButtonID == 57 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 57 ); }
			else if ( info.ButtonID == 58 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 58 ); }
			else if ( info.ButtonID == 59 ){ MusicPlaylistFunctions.UpdatePlaylist( from, 59 ); }
			else if ( info.ButtonID > 100 )
			{
				Server.Misc.MusicPlaylistFunctions.PlayMusicFile( from, (info.ButtonID-100) );
			}

			if ( info.ButtonID < 1 ){ from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) ); }
			else { from.SendGump( new MusicPlaylist( from ) ); }
		}
    }
}

namespace Server.Misc
{
    class MusicPlaylistFunctions
    {
		public static void UpdatePlaylist( Mobile m, int nChange )
		{
			m.SendSound( 0x4A ); 

			MusicPlaylistFunctions.InitializePlaylist( m );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string PlaylistSetting = DB.MusicPlaylist;

			string[] eachSetting = PlaylistSetting.Split('#');
			int nLine = 1;
			string newSettings = "";

			foreach (string eachSettings in eachSetting)
			{
				if ( nLine == nChange )
				{
					string sChange = "0";
					if ( eachSettings == "0" ){ sChange = "1"; }
					newSettings = newSettings + sChange + "#";
				}
				else if ( nLine > 59 )
				{
				}
				else
				{
					newSettings = newSettings + eachSettings + "#";
				}
				nLine++;
			}

			DB.MusicPlaylist = newSettings; 
		}

		public static void InitializePlaylist( Mobile m )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB.MusicPlaylist == null ){ DB.MusicPlaylist = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
		}

		public static void PickRandomSong( Mobile m )
		{
			MusicPlaylistFunctions.InitializePlaylist( m );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string PlaylistSetting = DB.MusicPlaylist;

			string[] eachSetting = PlaylistSetting.Split('#');
			int c = 0;
			int x = 1;
			//string newSettings = "";

			ArrayList songs = new ArrayList();
			foreach (string eachSettings in eachSetting)
			{
				if ( eachSettings == "1" && x < 59 ){ songs.Add( x ); c++; } x++;
			}

			int o = Utility.RandomMinMax( 0, c );

			for ( int i = 0; i < songs.Count; ++i )
			{
				int tune = Convert.ToInt32(songs[ i ]);

				if ( i == o )
				{
					Server.Misc.MusicPlaylistFunctions.PlayMusicFile( m, tune );
				}
			}
		}

		public static void PlayMusicFile( Mobile from, int song )
		{
			MusicName toPlay = MusicName.Cave;

			switch ( song )
			{
				case 1: { toPlay = MusicName.Cave; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 2: { toPlay = MusicName.Cave2; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 3: { toPlay = MusicName.Cave3; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 4: { toPlay = MusicName.City_Britain; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 5: { toPlay = MusicName.City_Castl; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 6: { toPlay = MusicName.City_Darkmoor; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 7: { toPlay = MusicName.City_Gen; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 8: { toPlay = MusicName.City_Gen2; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 9: { toPlay = MusicName.City_Gen5; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 10: { toPlay = MusicName.City_Gen6; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 11: { toPlay = MusicName.City_Gen7; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 12: { toPlay = MusicName.City_Gen8; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 13: { toPlay = MusicName.City_Gen9; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 14: { toPlay = MusicName.City_Gen10; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 15: { toPlay = MusicName.City_Gen11; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 16: { toPlay = MusicName.City_Gen12; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 17: { toPlay = MusicName.City_Gen13; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 18: { toPlay = MusicName.City_Gen14; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 19: { toPlay = MusicName.City_Gen15; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 20: { toPlay = MusicName.City_Gen16; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 21: { toPlay = MusicName.City_Gen17; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 22: { toPlay = MusicName.City_Good; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 23: { toPlay = MusicName.City_Good2; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 24: { toPlay = MusicName.City_Good4; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 25: { toPlay = MusicName.City_Good5; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 26: { toPlay = MusicName.City_Good6; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 27: { toPlay = MusicName.City_Good7; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 28: { toPlay = MusicName.City_Lod; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 29: { toPlay = MusicName.City_Lodoria; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 30: { toPlay = MusicName.City_Montor; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 31: { toPlay = MusicName.City_Night; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 32: { toPlay = MusicName.City_RavenDark; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 33: { toPlay = MusicName.City_Sarth; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 34: { toPlay = MusicName.City_Sarth2; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 35: { toPlay = MusicName.City_Sarth3; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 36: { toPlay = MusicName.City_Sav; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 37: { toPlay = MusicName.City_Yew; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 38: { toPlay = MusicName.Danger; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 39: { toPlay = MusicName.Death3; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 40: { toPlay = MusicName.Dun_Doom; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 41: { toPlay = MusicName.Dun_Gen; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 42: { toPlay = MusicName.Dun_Gen2; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 43: { toPlay = MusicName.Dun_Gen3; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 44: { toPlay = MusicName.Dun_Gen5; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 45: { toPlay = MusicName.Dun_Gen6; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 46: { toPlay = MusicName.Dun_Gen7; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 47: { toPlay = MusicName.Dun_Gen8; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 48: { toPlay = MusicName.Dun_Gen9; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 49: { toPlay = MusicName.Dun_Gen10; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 50: { toPlay = MusicName.Dun_Gen11; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 51: { toPlay = MusicName.Dun_Gen12; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 52: { toPlay = MusicName.Dun_Time; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 53: { toPlay = MusicName.Inn; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 54: { toPlay = MusicName.Inn2; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 55: { toPlay = MusicName.Inn3; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 56: { toPlay = MusicName.Inn4; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 57: { toPlay = MusicName.Inn5; from.Send(PlayMusic.GetInstance(toPlay)); break; }
				case 58: { toPlay = MusicName.Inn6; from.Send(PlayMusic.GetInstance(toPlay)); break; }
			}
		}

		public static int GetPlaylistSetting( Mobile m, int nSetting )
		{
			PlayerMobile pm = (PlayerMobile)m;
			string sSetting = "0";

			MusicPlaylistFunctions.InitializePlaylist( m );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string PlaylistSetting = DB.MusicPlaylist;

			string[] eachSetting = PlaylistSetting.Split('#');
			int nLine = 1;

			foreach (string eachSettings in eachSetting)
			{
				if ( nLine == nSetting ){ sSetting = eachSettings; }
				nLine++;
			}

			int nValue = Convert.ToInt32(sSetting);

			return nValue;
		}
	}
}