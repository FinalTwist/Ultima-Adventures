using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using System;
using Server;
using System.Text;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
	[Flipable(0x1E5E, 0x1E5F)]
    public class AdminBoard : Item
    {
        [Constructable]
        public AdminBoard() : base( 0x1E5E )
        {
            Name = "News From The Nobles";
			Hue = 0x4A7;
        }

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 6 ) )
			{
				e.CloseGump( typeof( AdminBoardGump ) );
				e.SendGump( new AdminBoardGump( e ) );
				e.SendSound( 0x55 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public class AdminBoardGump : Gump
		{
			public AdminBoardGump( Mobile from ): base( 25, 25 )
			{
				int face = GetBoardAvatar( from, from.Map, from.Location, from.X, from.Y );
				string title = GetBoardName( face );
				title = title + "<br>Recent Messages from Throughout the Land<br>Select an Article Below to Read";

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 151);
				AddImage(300, 0, 151);
				AddImage(0, 300, 151);
				AddImage(300, 300, 151);
				AddImage(600, 0, 151);
				AddImage(600, 300, 151);
				AddImage(2, 2, 129);
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(301, 298, 129);
				AddImage(598, 298, 129);
				AddImage(580, 46, 132);
				AddImage(237, 46, 132);
				AddImage(870, 57, 160);
				AddImage(870, 264, 160);
				AddImage(55, 267, 160);
				AddImage(474, 46, 132);
				AddImage(762, 59, face);
				AddImage(7, 7, 133);
				AddImage(866, 559, 157);
				AddImage(51, 559, 157);
				AddImage(812, 320, 131);
				AddImage(782, 515, 159);
				AddItem(796, 214, 7774);
				AddHtml( 173, 66, 567, 60, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + title + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int i = 90;

				string message10 = Server.Misc.LoggingFunctions.LogArticles( 10, 1 );
				if ( message10 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 10, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message10 + " - " + Server.Misc.LoggingFunctions.LogArticles( 10, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message9 = Server.Misc.LoggingFunctions.LogArticles( 9, 1 );
				if ( message9 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 9, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message9 + " - " + Server.Misc.LoggingFunctions.LogArticles( 9, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message8 = Server.Misc.LoggingFunctions.LogArticles( 8, 1 );
				if ( message8 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 8, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message8 + " - " + Server.Misc.LoggingFunctions.LogArticles( 8, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message7 = Server.Misc.LoggingFunctions.LogArticles( 7, 1 );
				if ( message7 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 7, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message7 + " - " + Server.Misc.LoggingFunctions.LogArticles( 7, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message6 = Server.Misc.LoggingFunctions.LogArticles( 6, 1 );
				if ( message6 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 6, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message6 + " - " + Server.Misc.LoggingFunctions.LogArticles( 6, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message5 = Server.Misc.LoggingFunctions.LogArticles( 5, 1 );
				if ( message5 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 5, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message5 + " - " + Server.Misc.LoggingFunctions.LogArticles( 5, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message4 = Server.Misc.LoggingFunctions.LogArticles( 4, 1 );
				if ( message4 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 4, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message4 + " - " + Server.Misc.LoggingFunctions.LogArticles( 4, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message3 = Server.Misc.LoggingFunctions.LogArticles( 3, 1 );
				if ( message3 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 3, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message3 + " - " + Server.Misc.LoggingFunctions.LogArticles( 3, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message2 = Server.Misc.LoggingFunctions.LogArticles( 2, 1 );
				if ( message2 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 2, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message2 + " - " + Server.Misc.LoggingFunctions.LogArticles( 2, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				string message1 = Server.Misc.LoggingFunctions.LogArticles( 1, 1 );
				if ( message1 != "" )
				{
					i = i+45;
					AddButton(105, i, 4005, 4005, 1, GumpButtonType.Reply, 0);
					AddHtml( 142, i, 603, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message1 + " - " + Server.Misc.LoggingFunctions.LogArticles( 1, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile; 

				from.SendSound( 0x55 );

				if ( info.ButtonID > 0 )
				{
					from.SendGump( new BoardMessage( from, info.ButtonID ) );
				}
			}
		}

		public class BoardMessage : Gump
		{
			public BoardMessage( Mobile from, int message ): base( 25, 25 )
			{
				int face = GetBoardAvatar( from, from.Map, from.Location, from.X, from.Y );
				string title = GetBoardName( face );
				title = title + "<br>" + Server.Misc.LoggingFunctions.LogArticles( message, 1 ) + "<br>" + Server.Misc.LoggingFunctions.LogArticles( message, 2 ) + "";

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 151);
				AddImage(300, 0, 151);
				AddImage(0, 300, 151);
				AddImage(300, 300, 151);
				AddImage(600, 0, 151);
				AddImage(600, 300, 151);
				AddImage(2, 2, 129);
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(301, 298, 129);
				AddImage(598, 298, 129);
				AddImage(580, 46, 132);
				AddImage(237, 46, 132);
				AddImage(870, 57, 160);
				AddImage(870, 264, 160);
				AddImage(55, 267, 160);
				AddImage(474, 46, 132);
				AddImage(762, 59, face);
				AddImage(7, 7, 133);
				AddImage(866, 559, 157);
				AddImage(51, 559, 157);
				AddImage(812, 320, 131);
				AddImage(782, 515, 159);
				AddItem(796, 214, 7774);
				AddHtml( 173, 66, 567, 60, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + title + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 108, 142, 637, 437, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Server.Misc.LoggingFunctions.LogArticles( message, 3 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile; 
				from.SendSound( 0x55 );
				from.SendGump( new AdminBoardGump( from ) );
			}
		}

		public static int GetBoardAvatar( Mobile m, Map map, Point3D location, int x, int y )
		{
			int face = 0x478;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			bool canLeave = true;
			int mX = 0;
			int mY = 0;
			int mZ = 0;
			Map mWorld = null;

			string sPublicDoor = DB.CharacterPublicDoor;
			if ( sPublicDoor != null )
			{
				if ( sPublicDoor.Length > 0 )
				{
					string[] sPublicDoors = sPublicDoor.Split('#');
					int nEntry = 1;
					foreach (string exits in sPublicDoors)
					{
						if ( nEntry == 1 ){ mX = Convert.ToInt32(exits); }
						else if ( nEntry == 2 ){ mY = Convert.ToInt32(exits); }
						else if ( nEntry == 3 ){ mZ = Convert.ToInt32(exits); }
						else if ( nEntry == 4 ){ try { mWorld = Map.Parse( exits ); } catch{} if ( mWorld == null ){ mWorld = Map.Trammel; } }
						nEntry++;
					}

					location = new Point3D( mX, mY, mZ );
					map = mWorld;
					x = mX;
					y = mY;
				}
			}

			string world = Worlds.GetMyWorld( map, location, x, y );
			Region reg = Region.Find( location, map );

			if ( world == "the Bottle World of Kuldar" ){ face = 0x479; }
			else if ( world == "the Land of Lodoria" ){ face = 0x4DC; }
			else if ( world == "the Serpent Island" ){ face = 0x46A; }
			else if ( world == "the Isles of Dread" ){ face = 0x469; }
			else if ( world == "the Savaged Empire" ){ face = 0x468; }
			else if ( world == "the Island of Umber Veil" ){ face = 0x4DD; }
			else if ( world == "the Moon of Luna" ){ face = 0x47A; }
			else if ( world == "the Underworld" ){ face = 0x4DE; }

			return face;
		}

		public static string GetBoardName( int face )
		{
			string name = "Lord British";

			if ( face == 0x479 ){ name = "Lord Blackthorn"; }
			else if ( face == 0x4DC ){ name = "Arandur the Elven Prince"; }
			else if ( face == 0x46A ){ name = "Lord Draxinusom"; }
			else if ( face == 0x469 ){ name = "Gorn the Barbarian"; }
			else if ( face == 0x468 ){ name = "Vorgarag the Ork Lord"; }
			else if ( face == 0x4DD ){ name = "Dupre the Paladin"; }
			else if ( face == 0x47A ){ name = "Kalana the Oracle"; }
			else if ( face == 0x4DE ){ name = "Xavier the Theurgist"; }

			return name;
		}

        public AdminBoard( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );
        }
    }
}