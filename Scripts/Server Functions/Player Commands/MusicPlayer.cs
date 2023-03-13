using System;
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
    class Musical
    {
        public static void Initialize()
        {
            CommandSystem.Register("musical", AccessLevel.Player, new CommandEventHandler(OnTogglePrivateTime));
        }

        [Usage("musical")]
        [Description("Enables or disables the type of music played in dungeons.")]
        private static void OnTogglePrivateTime(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string tunes = DB.CharMusical;

            if ( tunes == "Forest" )
            {
				DB.CharMusical = "Dungeon";
                m.SendMessage(68, "Your dungeon music preference has been set to normal.");
            }
            else
            {
				DB.CharMusical = "Forest";
                m.SendMessage(68, "Your dungeon music preference has been set to casual.");
            }
        }
    }
    public class MusicPlayer : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "music", AccessLevel.Player, new CommandEventHandler( MyStats_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "music" )]
		[Description( "Opens the music player." )]
		public static void MyStats_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( MusicPlayer ) );
			from.SendGump( new MusicPlayer( from, 0 ) );
        }
   
        public MusicPlayer ( Mobile from, int origin ) : base ( 25,25 )
        {
			m_Origin = origin;

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

			AddHtml( 218, 65, 250, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>MUSIC PLAYER</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(95, 128, 4005, 4005, 1, GumpButtonType.Reply, 0);
			AddHtml( 135, 128, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Britain*</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 168, 4005, 4005, 2, GumpButtonType.Reply, 0);
			AddHtml( 135, 168, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Buccaneer's Den</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 208, 4005, 4005, 3, GumpButtonType.Reply, 0);
			AddHtml( 135, 208, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Castle British*</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 248, 4005, 4005, 4, GumpButtonType.Reply, 0);
			AddHtml( 135, 248, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Castle of Knowledge*</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 288, 4005, 4005, 5, GumpButtonType.Reply, 0);
			AddHtml( 135, 288, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Death Gulch</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 328, 4005, 4005, 6, GumpButtonType.Reply, 0);
			AddHtml( 135, 328, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Devil Guard</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 368, 4005, 4005, 7, GumpButtonType.Reply, 0);
			AddHtml( 135, 368, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Elidor</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 408, 4005, 4005, 8, GumpButtonType.Reply, 0);
			AddHtml( 135, 408, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Fawn</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 448, 4005, 4005, 9, GumpButtonType.Reply, 0);
			AddHtml( 135, 448, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Grey</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 488, 4005, 4005, 10, GumpButtonType.Reply, 0);
			AddHtml( 135, 488, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Luna</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 528, 4005, 4005, 11, GumpButtonType.Reply, 0);
			AddHtml( 135, 528, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Montor</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 568, 4005, 4005, 12, GumpButtonType.Reply, 0);
			AddHtml( 135, 568, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Moon</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 608, 4005, 4005, 13, GumpButtonType.Reply, 0);
			AddHtml( 135, 608, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Renika</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 648, 4005, 4005, 14, GumpButtonType.Reply, 0);
			AddHtml( 135, 648, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Skara Brae</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(95, 688, 4005, 4005, 15, GumpButtonType.Reply, 0);
			AddHtml( 135, 688, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Time Lord</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(322, 128, 4005, 4005, 31, GumpButtonType.Reply, 0);
			AddHtml( 362, 128, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Clues</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 168, 4005, 4005, 32, GumpButtonType.Reply, 0);
			AddHtml( 362, 168, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Covetous</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 208, 4005, 4005, 33, GumpButtonType.Reply, 0);
			AddHtml( 362, 208, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dardin's Pit</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 248, 4005, 4005, 34, GumpButtonType.Reply, 0);
			AddHtml( 362, 248, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Deceit</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 288, 4005, 4005, 35, GumpButtonType.Reply, 0);
			AddHtml( 362, 288, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Despise</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 328, 4005, 4005, 36, GumpButtonType.Reply, 0);
			AddHtml( 362, 328, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Destard</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 368, 4005, 4005, 37, GumpButtonType.Reply, 0);
			AddHtml( 362, 368, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Doom</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 408, 4005, 4005, 38, GumpButtonType.Reply, 0);
			AddHtml( 362, 408, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Exodus</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 448, 4005, 4005, 39, GumpButtonType.Reply, 0);
			AddHtml( 362, 448, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Fires of Hell</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 488, 4005, 4005, 40, GumpButtonType.Reply, 0);
			AddHtml( 362, 488, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hythloth</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 528, 4005, 4005, 41, GumpButtonType.Reply, 0);
			AddHtml( 362, 528, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mines of Morinia</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 568, 4005, 4005, 42, GumpButtonType.Reply, 0);
			AddHtml( 362, 568, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Perinian Depths</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 608, 4005, 4005, 43, GumpButtonType.Reply, 0);
			AddHtml( 362, 608, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Shame</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 648, 4005, 4005, 44, GumpButtonType.Reply, 0);
			AddHtml( 362, 648, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Time Awaits</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(322, 688, 4005, 4005, 45, GumpButtonType.Reply, 0);
			AddHtml( 362, 688, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wrong</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(547, 128, 4005, 4005, 16, GumpButtonType.Reply, 0);
			AddHtml( 587, 128, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wizard Den</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 168, 4005, 4005, 17, GumpButtonType.Reply, 0);
			AddHtml( 587, 168, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yew</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 208, 4005, 4005, 18, GumpButtonType.Reply, 0);
			AddHtml( 587, 208, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Adventure</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 248, 4005, 4005, 19, GumpButtonType.Reply, 0);
			AddHtml( 587, 248, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Expedition</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 288, 4005, 4005, 20, GumpButtonType.Reply, 0);
			AddHtml( 587, 288, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Explore</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 328, 4005, 4005, 21, GumpButtonType.Reply, 0);
			AddHtml( 587, 328, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hunting</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 368, 4005, 4005, 22, GumpButtonType.Reply, 0);
			AddHtml( 587, 368, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Odyssey</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 408, 4005, 4005, 23, GumpButtonType.Reply, 0);
			AddHtml( 587, 408, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Quest</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 448, 4005, 4005, 24, GumpButtonType.Reply, 0);
			AddHtml( 587, 448, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Roaming</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 488, 4005, 4005, 25, GumpButtonType.Reply, 0);
			AddHtml( 587, 488, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Scouting</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 528, 4005, 4005, 26, GumpButtonType.Reply, 0);
			AddHtml( 587, 528, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Searching</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 568, 4005, 4005, 27, GumpButtonType.Reply, 0);
			AddHtml( 587, 568, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Seeking</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 608, 4005, 4005, 28, GumpButtonType.Reply, 0);
			AddHtml( 587, 608, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Traveling</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 648, 4005, 4005, 29, GumpButtonType.Reply, 0);
			AddHtml( 587, 648, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wandering</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(547, 688, 4005, 4005, 30, GumpButtonType.Reply, 0);
			AddHtml( 587, 688, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Catacombs</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(776, 128, 4005, 4005, 46, GumpButtonType.Reply, 0);
			AddHtml( 816, 128, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Docks</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 168, 4005, 4005, 47, GumpButtonType.Reply, 0);
			AddHtml( 816, 168, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pirates</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 208, 4005, 4005, 48, GumpButtonType.Reply, 0);
			AddHtml( 816, 208, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sailing</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 248, 4005, 4005, 49, GumpButtonType.Reply, 0);
			AddHtml( 816, 248, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cave</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 288, 4005, 4005, 50, GumpButtonType.Reply, 0);
			AddHtml( 816, 288, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Grotto</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 328, 4005, 4005, 51, GumpButtonType.Reply, 0);
			AddHtml( 816, 328, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mines</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 368, 4005, 4005, 52, GumpButtonType.Reply, 0);
			AddHtml( 816, 368, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Alehouse</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 408, 4005, 4005, 53, GumpButtonType.Reply, 0);
			AddHtml( 816, 408, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 448, 4005, 4005, 54, GumpButtonType.Reply, 0);
			AddHtml( 816, 448, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Guild</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 488, 4005, 4005, 55, GumpButtonType.Reply, 0);
			AddHtml( 816, 488, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Inn</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 528, 4005, 4005, 56, GumpButtonType.Reply, 0);
			AddHtml( 816, 528, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Lodge</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 568, 4005, 4005, 57, GumpButtonType.Reply, 0);
			AddHtml( 816, 568, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pub</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(776, 608, 4005, 4005, 58, GumpButtonType.Reply, 0);
			AddHtml( 816, 608, 154, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tavern</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID > 0 ){ Server.Misc.MusicPlaylistFunctions.PlayMusicFile( from, info.ButtonID ); from.SendGump( new MusicPlayer( from, m_Origin ) ); }

			if ( m_Origin > 0 && info.ButtonID < 1 ){ from.SendGump( new Server.Engines.Help.HelpGump( from, 1 ) ); }
		}
    }
}