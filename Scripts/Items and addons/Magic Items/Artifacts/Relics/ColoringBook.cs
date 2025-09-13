using System;
using Server; 
using Server.Network;
using System.Collections; 
using Server.Items;
using Server.Misc;
using Server.Gumps;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class ColoringBook : Item
	{
		public string MagicColor;

		[CommandProperty(AccessLevel.Owner)]
		public string Magic_Color { get { return MagicColor; } set { MagicColor = value; InvalidateProperties(); } }

		public int MagicPage;

		[CommandProperty(AccessLevel.Owner)]
		public int Magic_Page { get { return MagicPage; } set { MagicPage = value; InvalidateProperties(); } }

		[Constructable]
		public ColoringBook() : base( 0xEFA )
		{
			Weight = 1.0;
			Hue = 0xB94;
			Name = "Book of Prismatic Magic";
			MagicPage = 0;
			MagicColor = "";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to read." );
				return;
			}
			else 
			{
				from.SendSound( 0x55 );
				from.CloseGump( typeof( ColoringBookGump ) );
				from.SendGump( new ColoringBookGump( from, this ) );
			}
		}

		private class ColorTarget : Target
		{
			private ColoringBook m_Book;

			public ColorTarget( ColoringBook book ) : base( 1, false, TargetFlags.None )
			{
				m_Book = book;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item iColor = targeted as Item;

					if ( !iColor.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only color items in your pack." );
					}
					else if ( iColor.IsChildOf( from.Backpack ) )
					{
						from.RevealingAction();
						from.PlaySound( 0x1FA );
						iColor.Hue = m_Book.Hue;
						from.SendMessage( "You magically change the color." );
					}
					else
					{
						from.SendMessage( "You cannot color that!" );
					}
				}
				else
				{
					from.SendMessage( "You cannot color that!" );
				}
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact" );
            if ( MagicColor != "" ){ list.Add( 1049644, MagicColor ); }
        }

		public class ColoringBookGump : Gump
		{
			private ColoringBook m_Book;

			public ColoringBookGump( Mobile from, ColoringBook book ): base( 100, 100 )
			{
				m_Book = book;
				int page = m_Book.MagicPage;

				int NumberOfColors = 577; // SEE LISTING BELOW AND MAKE SURE IT MATCHES THE AMOUNT
				decimal PageCount = NumberOfColors / 16;
				int TotalBookPages = ( 100000 ) + ( (int)Math.Ceiling( PageCount ) );

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);

				int subColor = page * 16;

				int showColor1 = subColor + 1;
				int showColor2 = subColor + 2;
				int showColor3 = subColor + 3;
				int showColor4 = subColor + 4;
				int showColor5 = subColor + 5;
				int showColor6 = subColor + 6;
				int showColor7 = subColor + 7;
				int showColor8 = subColor + 8;
				int showColor9 = subColor + 9;
				int showColor10 = subColor + 10;
				int showColor11 = subColor + 11;
				int showColor12 = subColor + 12;
				int showColor13 = subColor + 13;
				int showColor14 = subColor + 14;
				int showColor15 = subColor + 15;
				int showColor16 = subColor + 16;

				int page_prev = ( 100000 + page ) - 1;
					if ( page_prev < 100000 ){ page_prev = TotalBookPages; }
				int page_next = ( 100000 + page ) + 1;
					if ( page_next > TotalBookPages ){ page_next = 100000; }

				AddImage(20, 42, 1054);

				AddHtml( 171, 65, 159, 26, @"<BODY><BASEFONT Color=#111111><BIG>Prismatic Magic</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 479, 65, 159, 26, @"<BODY><BASEFONT Color=#111111><BIG>Prismatic Magic</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(74, 59, 1055, 1055, page_prev, GumpButtonType.Reply, 0);
				AddButton(605, 58, 1056, 1056, page_next, GumpButtonType.Reply, 0);

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 134, 112, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 134, 152, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 134, 192, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor3 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 134, 232, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor4 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 134, 272, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor5 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 134, 312, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor6 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 134, 352, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor7 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 134, 392, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor8 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetColorListForBook( showColor1 ) != "" ){ AddItem(280, 107, 5988, GetColorNumberForBook( showColor1 )); AddButton(91, 111, 9720, 9720, showColor1, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor2 ) != "" ){ AddItem(280, 147, 5988, GetColorNumberForBook( showColor2 )); AddButton(91, 151, 9720, 9720, showColor2, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor3 ) != "" ){ AddItem(280, 187, 5988, GetColorNumberForBook( showColor3 )); AddButton(91, 191, 9720, 9720, showColor3, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor4 ) != "" ){ AddItem(280, 227, 5988, GetColorNumberForBook( showColor4 )); AddButton(91, 231, 9720, 9720, showColor4, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor5 ) != "" ){ AddItem(280, 267, 5988, GetColorNumberForBook( showColor5 )); AddButton(91, 271, 9720, 9720, showColor5, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor6 ) != "" ){ AddItem(280, 307, 5988, GetColorNumberForBook( showColor6 )); AddButton(91, 311, 9720, 9720, showColor6, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor7 ) != "" ){ AddItem(280, 347, 5988, GetColorNumberForBook( showColor7 )); AddButton(91, 351, 9720, 9720, showColor7, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor8 ) != "" ){ AddItem(280, 387, 5988, GetColorNumberForBook( showColor8 )); AddButton(91, 391, 9720, 9720, showColor8, GumpButtonType.Reply, 0); }

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 443, 112, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor9 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 152, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor10 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 192, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor11 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 232, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor12 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 272, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor13 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 312, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor14 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 352, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor15 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 392, 157, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + GetColorListForBook( showColor16 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetColorListForBook( showColor9 ) != "" ){ AddItem(597, 107, 5988, GetColorNumberForBook( showColor9 )); AddButton(400, 111, 9720, 9720, showColor9, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor10 ) != "" ){ AddItem(597, 147, 5988, GetColorNumberForBook( showColor10 )); AddButton(400, 151, 9720, 9720, showColor10, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor11 ) != "" ){ AddItem(597, 187, 5988, GetColorNumberForBook( showColor11 )); AddButton(400, 191, 9720, 9720, showColor11, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor12 ) != "" ){ AddItem(597, 227, 5988, GetColorNumberForBook( showColor12 )); AddButton(400, 231, 9720, 9720, showColor12, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor13 ) != "" ){ AddItem(597, 267, 5988, GetColorNumberForBook( showColor13 )); AddButton(400, 271, 9720, 9720, showColor13, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor14 ) != "" ){ AddItem(597, 307, 5988, GetColorNumberForBook( showColor14 )); AddButton(400, 311, 9720, 9720, showColor14, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor15 ) != "" ){ AddItem(597, 347, 5988, GetColorNumberForBook( showColor15 )); AddButton(400, 351, 9720, 9720, showColor15, GumpButtonType.Reply, 0); }
				if ( GetColorListForBook( showColor16 ) != "" ){ AddItem(597, 387, 5988, GetColorNumberForBook( showColor16 )); AddButton(400, 391, 9720, 9720, showColor16, GumpButtonType.Reply, 0); }
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile; 

				if ( info.ButtonID >= 100000 )
				{
					from.SendSound( 0x55 );
					int page = info.ButtonID - 100000;
					m_Book.MagicPage = page;
					from.SendGump( new ColoringBookGump( from, m_Book ) );
				}
				else
				{
					int TheColor = GetColorNumberForBook( info.ButtonID );

					if ( TheColor > 0 )
					{
						m_Book.MagicColor = GetColorListForBook( info.ButtonID );
						m_Book.InvalidateProperties();
						from.SendMessage( "What would you like to magically color?" );
						m_Book.Hue = TheColor;
						from.SendGump( new ColoringBookGump( from, m_Book ) );
						from.Target = new ColorTarget( m_Book );
					}
				}
			}
		}

		public ColoringBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
            writer.Write( MagicColor );
            writer.Write( MagicPage );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			MagicColor = reader.ReadString();
			MagicPage = reader.ReadInt();
		}

		public static string GetColorListForBook( int colorz )
		{
			string color = "";
			int clrz = 1;

			if ( colorz == clrz) { color="Blue #1"; } clrz++;
			if ( colorz == clrz) { color="Blue #2"; } clrz++;
			if ( colorz == clrz) { color="Blue #3"; } clrz++;
			if ( colorz == clrz) { color="Blue #4"; } clrz++;
			if ( colorz == clrz) { color="Blue #5"; } clrz++;
			if ( colorz == clrz) { color="Blue #6"; } clrz++;
			if ( colorz == clrz) { color="Blue #7"; } clrz++;
			if ( colorz == clrz) { color="Blue #8"; } clrz++;
			if ( colorz == clrz) { color="Blue #9"; } clrz++;
			if ( colorz == clrz) { color="Blue #10"; } clrz++;
			if ( colorz == clrz) { color="Blue #11"; } clrz++;
			if ( colorz == clrz) { color="Blue #12"; } clrz++;
			if ( colorz == clrz) { color="Blue #13"; } clrz++;
			if ( colorz == clrz) { color="Blue #14"; } clrz++;
			if ( colorz == clrz) { color="Blue #15"; } clrz++;
			if ( colorz == clrz) { color="Blue #16"; } clrz++;
			if ( colorz == clrz) { color="Blue #17"; } clrz++;
			if ( colorz == clrz) { color="Blue #18"; } clrz++;
			if ( colorz == clrz) { color="Blue #19"; } clrz++;
			if ( colorz == clrz) { color="Blue #20"; } clrz++;
			if ( colorz == clrz) { color="Blue #21"; } clrz++;
			if ( colorz == clrz) { color="Blue #22"; } clrz++;
			if ( colorz == clrz) { color="Blue #23"; } clrz++;
			if ( colorz == clrz) { color="Blue #24"; } clrz++;
			if ( colorz == clrz) { color="Blue #25"; } clrz++;
			if ( colorz == clrz) { color="Blue #26"; } clrz++;
			if ( colorz == clrz) { color="Blue #27"; } clrz++;
			if ( colorz == clrz) { color="Blue #28"; } clrz++;
			if ( colorz == clrz) { color="Blue #29"; } clrz++;
			if ( colorz == clrz) { color="Blue #30"; } clrz++;
			if ( colorz == clrz) { color="Blue #31"; } clrz++;
			if ( colorz == clrz) { color="Blue #32"; } clrz++;
			if ( colorz == clrz) { color="Blue #33"; } clrz++;
			if ( colorz == clrz) { color="Blue #34"; } clrz++;
			if ( colorz == clrz) { color="Blue #35"; } clrz++;
			if ( colorz == clrz) { color="Blue #36"; } clrz++;
			if ( colorz == clrz) { color="Blue #37"; } clrz++;
			if ( colorz == clrz) { color="Blue #38"; } clrz++;
			if ( colorz == clrz) { color="Blue #39"; } clrz++;
			if ( colorz == clrz) { color="Blue #40"; } clrz++;
			if ( colorz == clrz) { color="Blue #41"; } clrz++;
			if ( colorz == clrz) { color="Blue #42"; } clrz++;
			if ( colorz == clrz) { color="Blue #43"; } clrz++;
			if ( colorz == clrz) { color="Blue #44"; } clrz++;
			if ( colorz == clrz) { color="Blue #45"; } clrz++;
			if ( colorz == clrz) { color="Blue #46"; } clrz++;
			if ( colorz == clrz) { color="Blue #47"; } clrz++;
			if ( colorz == clrz) { color="Blue #48"; } clrz++;
			if ( colorz == clrz) { color="Blue #49"; } clrz++;
			if ( colorz == clrz) { color="Blue #50"; } clrz++;
			if ( colorz == clrz) { color="Blue #51"; } clrz++;
			if ( colorz == clrz) { color="Blue #52"; } clrz++;
			if ( colorz == clrz) { color="Blue #53"; } clrz++;
			if ( colorz == clrz) { color="Blue #54"; } clrz++;
			if ( colorz == clrz) { color="Green #1"; } clrz++;
			if ( colorz == clrz) { color="Green #2"; } clrz++;
			if ( colorz == clrz) { color="Green #3"; } clrz++;
			if ( colorz == clrz) { color="Green #4"; } clrz++;
			if ( colorz == clrz) { color="Green #5"; } clrz++;
			if ( colorz == clrz) { color="Green #6"; } clrz++;
			if ( colorz == clrz) { color="Green #7"; } clrz++;
			if ( colorz == clrz) { color="Green #8"; } clrz++;
			if ( colorz == clrz) { color="Green #9"; } clrz++;
			if ( colorz == clrz) { color="Green #10"; } clrz++;
			if ( colorz == clrz) { color="Green #11"; } clrz++;
			if ( colorz == clrz) { color="Green #12"; } clrz++;
			if ( colorz == clrz) { color="Green #13"; } clrz++;
			if ( colorz == clrz) { color="Green #14"; } clrz++;
			if ( colorz == clrz) { color="Green #15"; } clrz++;
			if ( colorz == clrz) { color="Green #16"; } clrz++;
			if ( colorz == clrz) { color="Green #17"; } clrz++;
			if ( colorz == clrz) { color="Green #18"; } clrz++;
			if ( colorz == clrz) { color="Green #19"; } clrz++;
			if ( colorz == clrz) { color="Green #20"; } clrz++;
			if ( colorz == clrz) { color="Green #21"; } clrz++;
			if ( colorz == clrz) { color="Green #22"; } clrz++;
			if ( colorz == clrz) { color="Green #23"; } clrz++;
			if ( colorz == clrz) { color="Green #24"; } clrz++;
			if ( colorz == clrz) { color="Green #25"; } clrz++;
			if ( colorz == clrz) { color="Green #26"; } clrz++;
			if ( colorz == clrz) { color="Green #27"; } clrz++;
			if ( colorz == clrz) { color="Green #28"; } clrz++;
			if ( colorz == clrz) { color="Green #29"; } clrz++;
			if ( colorz == clrz) { color="Green #30"; } clrz++;
			if ( colorz == clrz) { color="Green #31"; } clrz++;
			if ( colorz == clrz) { color="Green #32"; } clrz++;
			if ( colorz == clrz) { color="Green #33"; } clrz++;
			if ( colorz == clrz) { color="Green #34"; } clrz++;
			if ( colorz == clrz) { color="Green #35"; } clrz++;
			if ( colorz == clrz) { color="Green #36"; } clrz++;
			if ( colorz == clrz) { color="Green #37"; } clrz++;
			if ( colorz == clrz) { color="Green #38"; } clrz++;
			if ( colorz == clrz) { color="Green #39"; } clrz++;
			if ( colorz == clrz) { color="Green #40"; } clrz++;
			if ( colorz == clrz) { color="Green #41"; } clrz++;
			if ( colorz == clrz) { color="Green #42"; } clrz++;
			if ( colorz == clrz) { color="Green #43"; } clrz++;
			if ( colorz == clrz) { color="Green #44"; } clrz++;
			if ( colorz == clrz) { color="Green #45"; } clrz++;
			if ( colorz == clrz) { color="Green #46"; } clrz++;
			if ( colorz == clrz) { color="Green #47"; } clrz++;
			if ( colorz == clrz) { color="Green #48"; } clrz++;
			if ( colorz == clrz) { color="Green #49"; } clrz++;
			if ( colorz == clrz) { color="Green #50"; } clrz++;
			if ( colorz == clrz) { color="Green #51"; } clrz++;
			if ( colorz == clrz) { color="Green #52"; } clrz++;
			if ( colorz == clrz) { color="Green #53"; } clrz++;
			if ( colorz == clrz) { color="Green #54"; } clrz++;
			if ( colorz == clrz) { color="Orange #1"; } clrz++;
			if ( colorz == clrz) { color="Orange #2"; } clrz++;
			if ( colorz == clrz) { color="Orange #3"; } clrz++;
			if ( colorz == clrz) { color="Orange #4"; } clrz++;
			if ( colorz == clrz) { color="Orange #5"; } clrz++;
			if ( colorz == clrz) { color="Orange #6"; } clrz++;
			if ( colorz == clrz) { color="Orange #7"; } clrz++;
			if ( colorz == clrz) { color="Orange #8"; } clrz++;
			if ( colorz == clrz) { color="Orange #9"; } clrz++;
			if ( colorz == clrz) { color="Orange #10"; } clrz++;
			if ( colorz == clrz) { color="Orange #11"; } clrz++;
			if ( colorz == clrz) { color="Orange #12"; } clrz++;
			if ( colorz == clrz) { color="Orange #13"; } clrz++;
			if ( colorz == clrz) { color="Orange #14"; } clrz++;
			if ( colorz == clrz) { color="Orange #15"; } clrz++;
			if ( colorz == clrz) { color="Orange #16"; } clrz++;
			if ( colorz == clrz) { color="Orange #17"; } clrz++;
			if ( colorz == clrz) { color="Orange #18"; } clrz++;
			if ( colorz == clrz) { color="Orange #19"; } clrz++;
			if ( colorz == clrz) { color="Orange #20"; } clrz++;
			if ( colorz == clrz) { color="Orange #21"; } clrz++;
			if ( colorz == clrz) { color="Orange #22"; } clrz++;
			if ( colorz == clrz) { color="Orange #23"; } clrz++;
			if ( colorz == clrz) { color="Orange #24"; } clrz++;
			if ( colorz == clrz) { color="Orange #25"; } clrz++;
			if ( colorz == clrz) { color="Orange #26"; } clrz++;
			if ( colorz == clrz) { color="Orange #27"; } clrz++;
			if ( colorz == clrz) { color="Orange #28"; } clrz++;
			if ( colorz == clrz) { color="Orange #29"; } clrz++;
			if ( colorz == clrz) { color="Orange #30"; } clrz++;
			if ( colorz == clrz) { color="Orange #31"; } clrz++;
			if ( colorz == clrz) { color="Orange #32"; } clrz++;
			if ( colorz == clrz) { color="Orange #33"; } clrz++;
			if ( colorz == clrz) { color="Orange #34"; } clrz++;
			if ( colorz == clrz) { color="Orange #35"; } clrz++;
			if ( colorz == clrz) { color="Orange #36"; } clrz++;
			if ( colorz == clrz) { color="Orange #37"; } clrz++;
			if ( colorz == clrz) { color="Orange #38"; } clrz++;
			if ( colorz == clrz) { color="Orange #39"; } clrz++;
			if ( colorz == clrz) { color="Orange #40"; } clrz++;
			if ( colorz == clrz) { color="Orange #41"; } clrz++;
			if ( colorz == clrz) { color="Orange #42"; } clrz++;
			if ( colorz == clrz) { color="Orange #43"; } clrz++;
			if ( colorz == clrz) { color="Orange #44"; } clrz++;
			if ( colorz == clrz) { color="Orange #45"; } clrz++;
			if ( colorz == clrz) { color="Orange #46"; } clrz++;
			if ( colorz == clrz) { color="Orange #47"; } clrz++;
			if ( colorz == clrz) { color="Orange #48"; } clrz++;
			if ( colorz == clrz) { color="Orange #49"; } clrz++;
			if ( colorz == clrz) { color="Orange #50"; } clrz++;
			if ( colorz == clrz) { color="Orange #51"; } clrz++;
			if ( colorz == clrz) { color="Orange #52"; } clrz++;
			if ( colorz == clrz) { color="Orange #53"; } clrz++;
			if ( colorz == clrz) { color="Orange #54"; } clrz++;
			if ( colorz == clrz) { color="Pink #1"; } clrz++;
			if ( colorz == clrz) { color="Pink #2"; } clrz++;
			if ( colorz == clrz) { color="Pink #3"; } clrz++;
			if ( colorz == clrz) { color="Pink #4"; } clrz++;
			if ( colorz == clrz) { color="Pink #5"; } clrz++;
			if ( colorz == clrz) { color="Pink #6"; } clrz++;
			if ( colorz == clrz) { color="Pink #7"; } clrz++;
			if ( colorz == clrz) { color="Pink #8"; } clrz++;
			if ( colorz == clrz) { color="Pink #9"; } clrz++;
			if ( colorz == clrz) { color="Pink #10"; } clrz++;
			if ( colorz == clrz) { color="Pink #11"; } clrz++;
			if ( colorz == clrz) { color="Pink #12"; } clrz++;
			if ( colorz == clrz) { color="Pink #13"; } clrz++;
			if ( colorz == clrz) { color="Pink #14"; } clrz++;
			if ( colorz == clrz) { color="Pink #15"; } clrz++;
			if ( colorz == clrz) { color="Pink #16"; } clrz++;
			if ( colorz == clrz) { color="Pink #17"; } clrz++;
			if ( colorz == clrz) { color="Pink #18"; } clrz++;
			if ( colorz == clrz) { color="Pink #19"; } clrz++;
			if ( colorz == clrz) { color="Pink #20"; } clrz++;
			if ( colorz == clrz) { color="Pink #21"; } clrz++;
			if ( colorz == clrz) { color="Pink #22"; } clrz++;
			if ( colorz == clrz) { color="Pink #23"; } clrz++;
			if ( colorz == clrz) { color="Pink #24"; } clrz++;
			if ( colorz == clrz) { color="Pink #25"; } clrz++;
			if ( colorz == clrz) { color="Pink #26"; } clrz++;
			if ( colorz == clrz) { color="Pink #27"; } clrz++;
			if ( colorz == clrz) { color="Pink #28"; } clrz++;
			if ( colorz == clrz) { color="Pink #29"; } clrz++;
			if ( colorz == clrz) { color="Pink #30"; } clrz++;
			if ( colorz == clrz) { color="Pink #31"; } clrz++;
			if ( colorz == clrz) { color="Pink #32"; } clrz++;
			if ( colorz == clrz) { color="Pink #33"; } clrz++;
			if ( colorz == clrz) { color="Pink #34"; } clrz++;
			if ( colorz == clrz) { color="Pink #35"; } clrz++;
			if ( colorz == clrz) { color="Pink #36"; } clrz++;
			if ( colorz == clrz) { color="Pink #37"; } clrz++;
			if ( colorz == clrz) { color="Pink #38"; } clrz++;
			if ( colorz == clrz) { color="Pink #39"; } clrz++;
			if ( colorz == clrz) { color="Pink #40"; } clrz++;
			if ( colorz == clrz) { color="Pink #41"; } clrz++;
			if ( colorz == clrz) { color="Pink #42"; } clrz++;
			if ( colorz == clrz) { color="Pink #43"; } clrz++;
			if ( colorz == clrz) { color="Pink #44"; } clrz++;
			if ( colorz == clrz) { color="Pink #45"; } clrz++;
			if ( colorz == clrz) { color="Pink #46"; } clrz++;
			if ( colorz == clrz) { color="Pink #47"; } clrz++;
			if ( colorz == clrz) { color="Pink #48"; } clrz++;
			if ( colorz == clrz) { color="Pink #49"; } clrz++;
			if ( colorz == clrz) { color="Pink #50"; } clrz++;
			if ( colorz == clrz) { color="Pink #51"; } clrz++;
			if ( colorz == clrz) { color="Pink #52"; } clrz++;
			if ( colorz == clrz) { color="Pink #53"; } clrz++;
			if ( colorz == clrz) { color="Pink #54"; } clrz++;
			if ( colorz == clrz) { color="Red #1"; } clrz++;
			if ( colorz == clrz) { color="Red #2"; } clrz++;
			if ( colorz == clrz) { color="Red #3"; } clrz++;
			if ( colorz == clrz) { color="Red #4"; } clrz++;
			if ( colorz == clrz) { color="Red #5"; } clrz++;
			if ( colorz == clrz) { color="Red #6"; } clrz++;
			if ( colorz == clrz) { color="Red #7"; } clrz++;
			if ( colorz == clrz) { color="Red #8"; } clrz++;
			if ( colorz == clrz) { color="Red #9"; } clrz++;
			if ( colorz == clrz) { color="Red #10"; } clrz++;
			if ( colorz == clrz) { color="Red #11"; } clrz++;
			if ( colorz == clrz) { color="Red #12"; } clrz++;
			if ( colorz == clrz) { color="Red #13"; } clrz++;
			if ( colorz == clrz) { color="Red #14"; } clrz++;
			if ( colorz == clrz) { color="Red #15"; } clrz++;
			if ( colorz == clrz) { color="Red #16"; } clrz++;
			if ( colorz == clrz) { color="Red #17"; } clrz++;
			if ( colorz == clrz) { color="Red #18"; } clrz++;
			if ( colorz == clrz) { color="Red #19"; } clrz++;
			if ( colorz == clrz) { color="Red #20"; } clrz++;
			if ( colorz == clrz) { color="Red #21"; } clrz++;
			if ( colorz == clrz) { color="Red #22"; } clrz++;
			if ( colorz == clrz) { color="Red #23"; } clrz++;
			if ( colorz == clrz) { color="Red #24"; } clrz++;
			if ( colorz == clrz) { color="Red #25"; } clrz++;
			if ( colorz == clrz) { color="Red #26"; } clrz++;
			if ( colorz == clrz) { color="Red #27"; } clrz++;
			if ( colorz == clrz) { color="Red #28"; } clrz++;
			if ( colorz == clrz) { color="Red #29"; } clrz++;
			if ( colorz == clrz) { color="Red #30"; } clrz++;
			if ( colorz == clrz) { color="Red #31"; } clrz++;
			if ( colorz == clrz) { color="Red #32"; } clrz++;
			if ( colorz == clrz) { color="Red #33"; } clrz++;
			if ( colorz == clrz) { color="Red #34"; } clrz++;
			if ( colorz == clrz) { color="Red #35"; } clrz++;
			if ( colorz == clrz) { color="Red #36"; } clrz++;
			if ( colorz == clrz) { color="Red #37"; } clrz++;
			if ( colorz == clrz) { color="Red #38"; } clrz++;
			if ( colorz == clrz) { color="Red #39"; } clrz++;
			if ( colorz == clrz) { color="Red #40"; } clrz++;
			if ( colorz == clrz) { color="Red #41"; } clrz++;
			if ( colorz == clrz) { color="Red #42"; } clrz++;
			if ( colorz == clrz) { color="Red #43"; } clrz++;
			if ( colorz == clrz) { color="Red #44"; } clrz++;
			if ( colorz == clrz) { color="Red #45"; } clrz++;
			if ( colorz == clrz) { color="Red #46"; } clrz++;
			if ( colorz == clrz) { color="Red #47"; } clrz++;
			if ( colorz == clrz) { color="Red #48"; } clrz++;
			if ( colorz == clrz) { color="Red #49"; } clrz++;
			if ( colorz == clrz) { color="Red #50"; } clrz++;
			if ( colorz == clrz) { color="Red #51"; } clrz++;
			if ( colorz == clrz) { color="Red #52"; } clrz++;
			if ( colorz == clrz) { color="Red #53"; } clrz++;
			if ( colorz == clrz) { color="Red #54"; } clrz++;
			if ( colorz == clrz) { color="Yellow #1"; } clrz++;
			if ( colorz == clrz) { color="Yellow #2"; } clrz++;
			if ( colorz == clrz) { color="Yellow #3"; } clrz++;
			if ( colorz == clrz) { color="Yellow #4"; } clrz++;
			if ( colorz == clrz) { color="Yellow #5"; } clrz++;
			if ( colorz == clrz) { color="Yellow #6"; } clrz++;
			if ( colorz == clrz) { color="Yellow #7"; } clrz++;
			if ( colorz == clrz) { color="Yellow #8"; } clrz++;
			if ( colorz == clrz) { color="Yellow #9"; } clrz++;
			if ( colorz == clrz) { color="Yellow #10"; } clrz++;
			if ( colorz == clrz) { color="Yellow #11"; } clrz++;
			if ( colorz == clrz) { color="Yellow #12"; } clrz++;
			if ( colorz == clrz) { color="Yellow #13"; } clrz++;
			if ( colorz == clrz) { color="Yellow #14"; } clrz++;
			if ( colorz == clrz) { color="Yellow #15"; } clrz++;
			if ( colorz == clrz) { color="Yellow #16"; } clrz++;
			if ( colorz == clrz) { color="Yellow #17"; } clrz++;
			if ( colorz == clrz) { color="Yellow #18"; } clrz++;
			if ( colorz == clrz) { color="Yellow #19"; } clrz++;
			if ( colorz == clrz) { color="Yellow #20"; } clrz++;
			if ( colorz == clrz) { color="Yellow #21"; } clrz++;
			if ( colorz == clrz) { color="Yellow #22"; } clrz++;
			if ( colorz == clrz) { color="Yellow #23"; } clrz++;
			if ( colorz == clrz) { color="Yellow #24"; } clrz++;
			if ( colorz == clrz) { color="Yellow #25"; } clrz++;
			if ( colorz == clrz) { color="Yellow #26"; } clrz++;
			if ( colorz == clrz) { color="Yellow #27"; } clrz++;
			if ( colorz == clrz) { color="Yellow #28"; } clrz++;
			if ( colorz == clrz) { color="Yellow #29"; } clrz++;
			if ( colorz == clrz) { color="Yellow #30"; } clrz++;
			if ( colorz == clrz) { color="Yellow #31"; } clrz++;
			if ( colorz == clrz) { color="Yellow #32"; } clrz++;
			if ( colorz == clrz) { color="Yellow #33"; } clrz++;
			if ( colorz == clrz) { color="Yellow #34"; } clrz++;
			if ( colorz == clrz) { color="Yellow #35"; } clrz++;
			if ( colorz == clrz) { color="Yellow #36"; } clrz++;
			if ( colorz == clrz) { color="Yellow #37"; } clrz++;
			if ( colorz == clrz) { color="Yellow #38"; } clrz++;
			if ( colorz == clrz) { color="Yellow #39"; } clrz++;
			if ( colorz == clrz) { color="Yellow #40"; } clrz++;
			if ( colorz == clrz) { color="Yellow #41"; } clrz++;
			if ( colorz == clrz) { color="Yellow #42"; } clrz++;
			if ( colorz == clrz) { color="Yellow #43"; } clrz++;
			if ( colorz == clrz) { color="Yellow #44"; } clrz++;
			if ( colorz == clrz) { color="Yellow #45"; } clrz++;
			if ( colorz == clrz) { color="Yellow #46"; } clrz++;
			if ( colorz == clrz) { color="Yellow #47"; } clrz++;
			if ( colorz == clrz) { color="Yellow #48"; } clrz++;
			if ( colorz == clrz) { color="Yellow #49"; } clrz++;
			if ( colorz == clrz) { color="Yellow #50"; } clrz++;
			if ( colorz == clrz) { color="Yellow #51"; } clrz++;
			if ( colorz == clrz) { color="Yellow #52"; } clrz++;
			if ( colorz == clrz) { color="Yellow #53"; } clrz++;
			if ( colorz == clrz) { color="Yellow #54"; } clrz++;
			if ( colorz == clrz) { color="Neutral #1"; } clrz++;
			if ( colorz == clrz) { color="Neutral #2"; } clrz++;
			if ( colorz == clrz) { color="Neutral #3"; } clrz++;
			if ( colorz == clrz) { color="Neutral #4"; } clrz++;
			if ( colorz == clrz) { color="Neutral #5"; } clrz++;
			if ( colorz == clrz) { color="Neutral #6"; } clrz++;
			if ( colorz == clrz) { color="Neutral #7"; } clrz++;
			if ( colorz == clrz) { color="Neutral #8"; } clrz++;
			if ( colorz == clrz) { color="Neutral #9"; } clrz++;
			if ( colorz == clrz) { color="Neutral #10"; } clrz++;
			if ( colorz == clrz) { color="Neutral #11"; } clrz++;
			if ( colorz == clrz) { color="Neutral #12"; } clrz++;
			if ( colorz == clrz) { color="Neutral #13"; } clrz++;
			if ( colorz == clrz) { color="Neutral #14"; } clrz++;
			if ( colorz == clrz) { color="Neutral #15"; } clrz++;
			if ( colorz == clrz) { color="Neutral #16"; } clrz++;
			if ( colorz == clrz) { color="Neutral #17"; } clrz++;
			if ( colorz == clrz) { color="Neutral #18"; } clrz++;
			if ( colorz == clrz) { color="Neutral #19"; } clrz++;
			if ( colorz == clrz) { color="Neutral #20"; } clrz++;
			if ( colorz == clrz) { color="Neutral #21"; } clrz++;
			if ( colorz == clrz) { color="Neutral #22"; } clrz++;
			if ( colorz == clrz) { color="Neutral #23"; } clrz++;
			if ( colorz == clrz) { color="Neutral #24"; } clrz++;
			if ( colorz == clrz) { color="Neutral #25"; } clrz++;
			if ( colorz == clrz) { color="Neutral #26"; } clrz++;
			if ( colorz == clrz) { color="Neutral #27"; } clrz++;
			if ( colorz == clrz) { color="Neutral #28"; } clrz++;
			if ( colorz == clrz) { color="Neutral #29"; } clrz++;
			if ( colorz == clrz) { color="Neutral #30"; } clrz++;
			if ( colorz == clrz) { color="Neutral #31"; } clrz++;
			if ( colorz == clrz) { color="Neutral #32"; } clrz++;
			if ( colorz == clrz) { color="Neutral #33"; } clrz++;
			if ( colorz == clrz) { color="Neutral #34"; } clrz++;
			if ( colorz == clrz) { color="Neutral #35"; } clrz++;
			if ( colorz == clrz) { color="Neutral #36"; } clrz++;
			if ( colorz == clrz) { color="Neutral #37"; } clrz++;
			if ( colorz == clrz) { color="Neutral #38"; } clrz++;
			if ( colorz == clrz) { color="Neutral #39"; } clrz++;
			if ( colorz == clrz) { color="Neutral #40"; } clrz++;
			if ( colorz == clrz) { color="Neutral #41"; } clrz++;
			if ( colorz == clrz) { color="Neutral #42"; } clrz++;
			if ( colorz == clrz) { color="Neutral #43"; } clrz++;
			if ( colorz == clrz) { color="Neutral #44"; } clrz++;
			if ( colorz == clrz) { color="Neutral #45"; } clrz++;
			if ( colorz == clrz) { color="Neutral #46"; } clrz++;
			if ( colorz == clrz) { color="Neutral #47"; } clrz++;
			if ( colorz == clrz) { color="Neutral #48"; } clrz++;
			if ( colorz == clrz) { color="Neutral #49"; } clrz++;
			if ( colorz == clrz) { color="Neutral #50"; } clrz++;
			if ( colorz == clrz) { color="Neutral #51"; } clrz++;
			if ( colorz == clrz) { color="Neutral #52"; } clrz++;
			if ( colorz == clrz) { color="Neutral #53"; } clrz++;
			if ( colorz == clrz) { color="Neutral #54"; } clrz++;
			if ( colorz == clrz) { color="Neutral #55"; } clrz++;
			if ( colorz == clrz) { color="Neutral #56"; } clrz++;
			if ( colorz == clrz) { color="Neutral #57"; } clrz++;
			if ( colorz == clrz) { color="Neutral #58"; } clrz++;
			if ( colorz == clrz) { color="Neutral #59"; } clrz++;
			if ( colorz == clrz) { color="Neutral #60"; } clrz++;
			if ( colorz == clrz) { color="Neutral #61"; } clrz++;
			if ( colorz == clrz) { color="Neutral #62"; } clrz++;
			if ( colorz == clrz) { color="Neutral #63"; } clrz++;
			if ( colorz == clrz) { color="Neutral #64"; } clrz++;
			if ( colorz == clrz) { color="Neutral #65"; } clrz++;
			if ( colorz == clrz) { color="Neutral #66"; } clrz++;
			if ( colorz == clrz) { color="Neutral #67"; } clrz++;
			if ( colorz == clrz) { color="Neutral #68"; } clrz++;
			if ( colorz == clrz) { color="Neutral #69"; } clrz++;
			if ( colorz == clrz) { color="Neutral #70"; } clrz++;
			if ( colorz == clrz) { color="Neutral #71"; } clrz++;
			if ( colorz == clrz) { color="Neutral #72"; } clrz++;
			if ( colorz == clrz) { color="Neutral #73"; } clrz++;
			if ( colorz == clrz) { color="Neutral #74"; } clrz++;
			if ( colorz == clrz) { color="Neutral #75"; } clrz++;
			if ( colorz == clrz) { color="Neutral #76"; } clrz++;
			if ( colorz == clrz) { color="Neutral #77"; } clrz++;
			if ( colorz == clrz) { color="Neutral #78"; } clrz++;
			if ( colorz == clrz) { color="Neutral #79"; } clrz++;
			if ( colorz == clrz) { color="Neutral #80"; } clrz++;
			if ( colorz == clrz) { color="Neutral #81"; } clrz++;
			if ( colorz == clrz) { color="Neutral #82"; } clrz++;
			if ( colorz == clrz) { color="Neutral #83"; } clrz++;
			if ( colorz == clrz) { color="Neutral #84"; } clrz++;
			if ( colorz == clrz) { color="Neutral #85"; } clrz++;
			if ( colorz == clrz) { color="Neutral #86"; } clrz++;
			if ( colorz == clrz) { color="Neutral #87"; } clrz++;
			if ( colorz == clrz) { color="Neutral #88"; } clrz++;
			if ( colorz == clrz) { color="Neutral #89"; } clrz++;
			if ( colorz == clrz) { color="Neutral #90"; } clrz++;
			if ( colorz == clrz) { color="Neutral #91"; } clrz++;
			if ( colorz == clrz) { color="Neutral #92"; } clrz++;
			if ( colorz == clrz) { color="Neutral #93"; } clrz++;
			if ( colorz == clrz) { color="Neutral #94"; } clrz++;
			if ( colorz == clrz) { color="Neutral #95"; } clrz++;
			if ( colorz == clrz) { color="Neutral #96"; } clrz++;
			if ( colorz == clrz) { color="Neutral #97"; } clrz++;
			if ( colorz == clrz) { color="Neutral #98"; } clrz++;
			if ( colorz == clrz) { color="Neutral #99"; } clrz++;
			if ( colorz == clrz) { color="Neutral #100"; } clrz++;
			if ( colorz == clrz) { color="Neutral #101"; } clrz++;
			if ( colorz == clrz) { color="Neutral #102"; } clrz++;
			if ( colorz == clrz) { color="Neutral #103"; } clrz++;
			if ( colorz == clrz) { color="Neutral #104"; } clrz++;
			if ( colorz == clrz) { color="Neutral #105"; } clrz++;
			if ( colorz == clrz) { color="Neutral #106"; } clrz++;
			if ( colorz == clrz) { color="Neutral #107"; } clrz++;
			if ( colorz == clrz) { color="Neutral #108"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #1"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #2"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #3"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #4"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #5"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #6"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #7"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #8"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #9"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #10"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #11"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #12"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #13"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #14"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #15"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #16"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #17"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #18"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #19"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #20"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #21"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #22"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #23"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #24"; } clrz++;
			if ( colorz == clrz) { color="Gargoyle #25"; } clrz++;
			if ( colorz == clrz) { color="Serpent #1"; } clrz++;
			if ( colorz == clrz) { color="Serpent #2"; } clrz++;
			if ( colorz == clrz) { color="Serpent #3"; } clrz++;
			if ( colorz == clrz) { color="Serpent #4"; } clrz++;
			if ( colorz == clrz) { color="Serpent #5"; } clrz++;
			if ( colorz == clrz) { color="Serpent #6"; } clrz++;
			if ( colorz == clrz) { color="Serpent #7"; } clrz++;
			if ( colorz == clrz) { color="Serpent #8"; } clrz++;
			if ( colorz == clrz) { color="Serpent #9"; } clrz++;
			if ( colorz == clrz) { color="Serpent #10"; } clrz++;
			if ( colorz == clrz) { color="Serpent #11"; } clrz++;
			if ( colorz == clrz) { color="Serpent #12"; } clrz++;
			if ( colorz == clrz) { color="Serpent #13"; } clrz++;
			if ( colorz == clrz) { color="Serpent #14"; } clrz++;
			if ( colorz == clrz) { color="Serpent #15"; } clrz++;
			if ( colorz == clrz) { color="Serpent #16"; } clrz++;
			if ( colorz == clrz) { color="Serpent #17"; } clrz++;
			if ( colorz == clrz) { color="Avian #1"; } clrz++;
			if ( colorz == clrz) { color="Avian #2"; } clrz++;
			if ( colorz == clrz) { color="Avian #3"; } clrz++;
			if ( colorz == clrz) { color="Avian #4"; } clrz++;
			if ( colorz == clrz) { color="Avian #5"; } clrz++;
			if ( colorz == clrz) { color="Avian #6"; } clrz++;
			if ( colorz == clrz) { color="Avian #7"; } clrz++;
			if ( colorz == clrz) { color="Avian #8"; } clrz++;
			if ( colorz == clrz) { color="Avian #9"; } clrz++;
			if ( colorz == clrz) { color="Avian #10"; } clrz++;
			if ( colorz == clrz) { color="Avian #11"; } clrz++;
			if ( colorz == clrz) { color="Avian #12"; } clrz++;
			if ( colorz == clrz) { color="Avian #13"; } clrz++;
			if ( colorz == clrz) { color="Avian #14"; } clrz++;
			if ( colorz == clrz) { color="Avian #15"; } clrz++;
			if ( colorz == clrz) { color="Avian #16"; } clrz++;
			if ( colorz == clrz) { color="Avian #17"; } clrz++;
			if ( colorz == clrz) { color="Avian #18"; } clrz++;
			if ( colorz == clrz) { color="Avian #19"; } clrz++;
			if ( colorz == clrz) { color="Avian #20"; } clrz++;
			if ( colorz == clrz) { color="Avian #21"; } clrz++;
			if ( colorz == clrz) { color="Avian #22"; } clrz++;
			if ( colorz == clrz) { color="Avian #23"; } clrz++;
			if ( colorz == clrz) { color="Avian #24"; } clrz++;
			if ( colorz == clrz) { color="Avian #25"; } clrz++;
			if ( colorz == clrz) { color="Avian #26"; } clrz++;
			if ( colorz == clrz) { color="Avian #27"; } clrz++;
			if ( colorz == clrz) { color="Avian #28"; } clrz++;
			if ( colorz == clrz) { color="Avian #29"; } clrz++;
			if ( colorz == clrz) { color="Avian #30"; } clrz++;
			if ( colorz == clrz) { color="Slime #1"; } clrz++;
			if ( colorz == clrz) { color="Slime #2"; } clrz++;
			if ( colorz == clrz) { color="Slime #3"; } clrz++;
			if ( colorz == clrz) { color="Slime #4"; } clrz++;
			if ( colorz == clrz) { color="Slime #5"; } clrz++;
			if ( colorz == clrz) { color="Slime #6"; } clrz++;
			if ( colorz == clrz) { color="Slime #7"; } clrz++;
			if ( colorz == clrz) { color="Slime #8"; } clrz++;
			if ( colorz == clrz) { color="Slime #9"; } clrz++;
			if ( colorz == clrz) { color="Slime #10"; } clrz++;
			if ( colorz == clrz) { color="Slime #11"; } clrz++;
			if ( colorz == clrz) { color="Slime #12"; } clrz++;
			if ( colorz == clrz) { color="Slime #13"; } clrz++;
			if ( colorz == clrz) { color="Slime #14"; } clrz++;
			if ( colorz == clrz) { color="Slime #15"; } clrz++;
			if ( colorz == clrz) { color="Slime #16"; } clrz++;
			if ( colorz == clrz) { color="Slime #17"; } clrz++;
			if ( colorz == clrz) { color="Slime #18"; } clrz++;
			if ( colorz == clrz) { color="Slime #19"; } clrz++;
			if ( colorz == clrz) { color="Slime #20"; } clrz++;
			if ( colorz == clrz) { color="Slime #21"; } clrz++;
			if ( colorz == clrz) { color="Slime #22"; } clrz++;
			if ( colorz == clrz) { color="Slime #23"; } clrz++;
			if ( colorz == clrz) { color="Slime #24"; } clrz++;
			if ( colorz == clrz) { color="Animal #1"; } clrz++;
			if ( colorz == clrz) { color="Animal #2"; } clrz++;
			if ( colorz == clrz) { color="Animal #3"; } clrz++;
			if ( colorz == clrz) { color="Animal #4"; } clrz++;
			if ( colorz == clrz) { color="Animal #5"; } clrz++;
			if ( colorz == clrz) { color="Animal #6"; } clrz++;
			if ( colorz == clrz) { color="Animal #7"; } clrz++;
			if ( colorz == clrz) { color="Animal #8"; } clrz++;
			if ( colorz == clrz) { color="Animal #9"; } clrz++;
			if ( colorz == clrz) { color="Animal #10"; } clrz++;
			if ( colorz == clrz) { color="Animal #11"; } clrz++;
			if ( colorz == clrz) { color="Animal #12"; } clrz++;
			if ( colorz == clrz) { color="Animal #13"; } clrz++;
			if ( colorz == clrz) { color="Animal #14"; } clrz++;
			if ( colorz == clrz) { color="Animal #15"; } clrz++;
			if ( colorz == clrz) { color="Animal #16"; } clrz++;
			if ( colorz == clrz) { color="Animal #17"; } clrz++;
			if ( colorz == clrz) { color="Animal #18"; } clrz++;
			if ( colorz == clrz) { color="Metal #1"; } clrz++;
			if ( colorz == clrz) { color="Metal #2"; } clrz++;
			if ( colorz == clrz) { color="Metal #3"; } clrz++;
			if ( colorz == clrz) { color="Metal #4"; } clrz++;
			if ( colorz == clrz) { color="Metal #5"; } clrz++;
			if ( colorz == clrz) { color="Metal #6"; } clrz++;
			if ( colorz == clrz) { color="Metal #7"; } clrz++;
			if ( colorz == clrz) { color="Metal #8"; } clrz++;
			if ( colorz == clrz) { color="Metal #9"; } clrz++;
			if ( colorz == clrz) { color="Metal #10"; } clrz++;
			if ( colorz == clrz) { color="Metal #11"; } clrz++;
			if ( colorz == clrz) { color="Metal #12"; } clrz++;
			if ( colorz == clrz) { color="Metal #13"; } clrz++;
			if ( colorz == clrz) { color="Metal #14"; } clrz++;
			if ( colorz == clrz) { color="Metal #15"; } clrz++;
			if ( colorz == clrz) { color="Metal #16"; } clrz++;
			if ( colorz == clrz) { color="Metal #17"; } clrz++;
			if ( colorz == clrz) { color="Metal #18"; } clrz++;
			if ( colorz == clrz) { color="Metal #19"; } clrz++;
			if ( colorz == clrz) { color="Metal #20"; } clrz++;
			if ( colorz == clrz) { color="Metal #21"; } clrz++;
			if ( colorz == clrz) { color="Metal #22"; } clrz++;
			if ( colorz == clrz) { color="Metal #23"; } clrz++;
			if ( colorz == clrz) { color="Metal #24"; } clrz++;
			if ( colorz == clrz) { color="Metal #25"; } clrz++;
			if ( colorz == clrz) { color="Metal #26"; } clrz++;
			if ( colorz == clrz) { color="Metal #27"; } clrz++;
			if ( colorz == clrz) { color="Metal #28"; } clrz++;
			if ( colorz == clrz) { color="Metal #29"; } clrz++;
			if ( colorz == clrz) { color="Metal #30"; } clrz++;
			if ( colorz == clrz) { color="Silver #1"; } clrz++;

			return color;
		}

		public static int GetColorNumberForBook( int colorz )
		{
			int color = 0;
			int clrz = 1;

			if ( colorz == clrz) { color = 0x515; } clrz++;
			if ( colorz == clrz) { color = 0x516; } clrz++;
			if ( colorz == clrz) { color = 0x517; } clrz++;
			if ( colorz == clrz) { color = 0x518; } clrz++;
			if ( colorz == clrz) { color = 0x519; } clrz++;
			if ( colorz == clrz) { color = 0x51A; } clrz++;
			if ( colorz == clrz) { color = 0x51B; } clrz++;
			if ( colorz == clrz) { color = 0x51C; } clrz++;
			if ( colorz == clrz) { color = 0x51D; } clrz++;
			if ( colorz == clrz) { color = 0x51E; } clrz++;
			if ( colorz == clrz) { color = 0x51F; } clrz++;
			if ( colorz == clrz) { color = 0x520; } clrz++;
			if ( colorz == clrz) { color = 0x521; } clrz++;
			if ( colorz == clrz) { color = 0x522; } clrz++;
			if ( colorz == clrz) { color = 0x523; } clrz++;
			if ( colorz == clrz) { color = 0x524; } clrz++;
			if ( colorz == clrz) { color = 0x525; } clrz++;
			if ( colorz == clrz) { color = 0x526; } clrz++;
			if ( colorz == clrz) { color = 0x527; } clrz++;
			if ( colorz == clrz) { color = 0x528; } clrz++;
			if ( colorz == clrz) { color = 0x529; } clrz++;
			if ( colorz == clrz) { color = 0x52A; } clrz++;
			if ( colorz == clrz) { color = 0x52B; } clrz++;
			if ( colorz == clrz) { color = 0x52C; } clrz++;
			if ( colorz == clrz) { color = 0x52D; } clrz++;
			if ( colorz == clrz) { color = 0x52E; } clrz++;
			if ( colorz == clrz) { color = 0x52F; } clrz++;
			if ( colorz == clrz) { color = 0x530; } clrz++;
			if ( colorz == clrz) { color = 0x531; } clrz++;
			if ( colorz == clrz) { color = 0x532; } clrz++;
			if ( colorz == clrz) { color = 0x533; } clrz++;
			if ( colorz == clrz) { color = 0x534; } clrz++;
			if ( colorz == clrz) { color = 0x535; } clrz++;
			if ( colorz == clrz) { color = 0x536; } clrz++;
			if ( colorz == clrz) { color = 0x537; } clrz++;
			if ( colorz == clrz) { color = 0x538; } clrz++;
			if ( colorz == clrz) { color = 0x539; } clrz++;
			if ( colorz == clrz) { color = 0x53A; } clrz++;
			if ( colorz == clrz) { color = 0x53B; } clrz++;
			if ( colorz == clrz) { color = 0x53C; } clrz++;
			if ( colorz == clrz) { color = 0x53D; } clrz++;
			if ( colorz == clrz) { color = 0x53E; } clrz++;
			if ( colorz == clrz) { color = 0x53F; } clrz++;
			if ( colorz == clrz) { color = 0x540; } clrz++;
			if ( colorz == clrz) { color = 0x541; } clrz++;
			if ( colorz == clrz) { color = 0x542; } clrz++;
			if ( colorz == clrz) { color = 0x543; } clrz++;
			if ( colorz == clrz) { color = 0x544; } clrz++;
			if ( colorz == clrz) { color = 0x545; } clrz++;
			if ( colorz == clrz) { color = 0x546; } clrz++;
			if ( colorz == clrz) { color = 0x547; } clrz++;
			if ( colorz == clrz) { color = 0x548; } clrz++;
			if ( colorz == clrz) { color = 0x549; } clrz++;
			if ( colorz == clrz) { color = 0x54A; } clrz++;
			if ( colorz == clrz) { color = 0x579; } clrz++;
			if ( colorz == clrz) { color = 0x57A; } clrz++;
			if ( colorz == clrz) { color = 0x57B; } clrz++;
			if ( colorz == clrz) { color = 0x57C; } clrz++;
			if ( colorz == clrz) { color = 0x57D; } clrz++;
			if ( colorz == clrz) { color = 0x57E; } clrz++;
			if ( colorz == clrz) { color = 0x57F; } clrz++;
			if ( colorz == clrz) { color = 0x580; } clrz++;
			if ( colorz == clrz) { color = 0x581; } clrz++;
			if ( colorz == clrz) { color = 0x582; } clrz++;
			if ( colorz == clrz) { color = 0x583; } clrz++;
			if ( colorz == clrz) { color = 0x584; } clrz++;
			if ( colorz == clrz) { color = 0x585; } clrz++;
			if ( colorz == clrz) { color = 0x586; } clrz++;
			if ( colorz == clrz) { color = 0x587; } clrz++;
			if ( colorz == clrz) { color = 0x588; } clrz++;
			if ( colorz == clrz) { color = 0x589; } clrz++;
			if ( colorz == clrz) { color = 0x58A; } clrz++;
			if ( colorz == clrz) { color = 0x58B; } clrz++;
			if ( colorz == clrz) { color = 0x58C; } clrz++;
			if ( colorz == clrz) { color = 0x58D; } clrz++;
			if ( colorz == clrz) { color = 0x58E; } clrz++;
			if ( colorz == clrz) { color = 0x58F; } clrz++;
			if ( colorz == clrz) { color = 0x590; } clrz++;
			if ( colorz == clrz) { color = 0x591; } clrz++;
			if ( colorz == clrz) { color = 0x592; } clrz++;
			if ( colorz == clrz) { color = 0x593; } clrz++;
			if ( colorz == clrz) { color = 0x594; } clrz++;
			if ( colorz == clrz) { color = 0x595; } clrz++;
			if ( colorz == clrz) { color = 0x596; } clrz++;
			if ( colorz == clrz) { color = 0x597; } clrz++;
			if ( colorz == clrz) { color = 0x598; } clrz++;
			if ( colorz == clrz) { color = 0x599; } clrz++;
			if ( colorz == clrz) { color = 0x59A; } clrz++;
			if ( colorz == clrz) { color = 0x59B; } clrz++;
			if ( colorz == clrz) { color = 0x59C; } clrz++;
			if ( colorz == clrz) { color = 0x59D; } clrz++;
			if ( colorz == clrz) { color = 0x59E; } clrz++;
			if ( colorz == clrz) { color = 0x59F; } clrz++;
			if ( colorz == clrz) { color = 0x5A0; } clrz++;
			if ( colorz == clrz) { color = 0x5A1; } clrz++;
			if ( colorz == clrz) { color = 0x5A2; } clrz++;
			if ( colorz == clrz) { color = 0x5A3; } clrz++;
			if ( colorz == clrz) { color = 0x5A4; } clrz++;
			if ( colorz == clrz) { color = 0x5A5; } clrz++;
			if ( colorz == clrz) { color = 0x5A6; } clrz++;
			if ( colorz == clrz) { color = 0x5A7; } clrz++;
			if ( colorz == clrz) { color = 0x5A8; } clrz++;
			if ( colorz == clrz) { color = 0x5A9; } clrz++;
			if ( colorz == clrz) { color = 0x5AA; } clrz++;
			if ( colorz == clrz) { color = 0x5AB; } clrz++;
			if ( colorz == clrz) { color = 0x5AC; } clrz++;
			if ( colorz == clrz) { color = 0x5AD; } clrz++;
			if ( colorz == clrz) { color = 0x5AE; } clrz++;
			if ( colorz == clrz) { color = 0x5DD; } clrz++;
			if ( colorz == clrz) { color = 0x5DE; } clrz++;
			if ( colorz == clrz) { color = 0x5DF; } clrz++;
			if ( colorz == clrz) { color = 0x5E0; } clrz++;
			if ( colorz == clrz) { color = 0x5E1; } clrz++;
			if ( colorz == clrz) { color = 0x5E2; } clrz++;
			if ( colorz == clrz) { color = 0x5E3; } clrz++;
			if ( colorz == clrz) { color = 0x5E4; } clrz++;
			if ( colorz == clrz) { color = 0x5E5; } clrz++;
			if ( colorz == clrz) { color = 0x5E6; } clrz++;
			if ( colorz == clrz) { color = 0x5E7; } clrz++;
			if ( colorz == clrz) { color = 0x5E8; } clrz++;
			if ( colorz == clrz) { color = 0x5E9; } clrz++;
			if ( colorz == clrz) { color = 0x5EA; } clrz++;
			if ( colorz == clrz) { color = 0x5EB; } clrz++;
			if ( colorz == clrz) { color = 0x5EC; } clrz++;
			if ( colorz == clrz) { color = 0x5ED; } clrz++;
			if ( colorz == clrz) { color = 0x5EE; } clrz++;
			if ( colorz == clrz) { color = 0x5EF; } clrz++;
			if ( colorz == clrz) { color = 0x5F0; } clrz++;
			if ( colorz == clrz) { color = 0x5F1; } clrz++;
			if ( colorz == clrz) { color = 0x5F2; } clrz++;
			if ( colorz == clrz) { color = 0x5F3; } clrz++;
			if ( colorz == clrz) { color = 0x5F4; } clrz++;
			if ( colorz == clrz) { color = 0x5F5; } clrz++;
			if ( colorz == clrz) { color = 0x5F6; } clrz++;
			if ( colorz == clrz) { color = 0x5F7; } clrz++;
			if ( colorz == clrz) { color = 0x5F8; } clrz++;
			if ( colorz == clrz) { color = 0x5F9; } clrz++;
			if ( colorz == clrz) { color = 0x5FA; } clrz++;
			if ( colorz == clrz) { color = 0x5FB; } clrz++;
			if ( colorz == clrz) { color = 0x5FC; } clrz++;
			if ( colorz == clrz) { color = 0x5FD; } clrz++;
			if ( colorz == clrz) { color = 0x5FE; } clrz++;
			if ( colorz == clrz) { color = 0x5FF; } clrz++;
			if ( colorz == clrz) { color = 0x600; } clrz++;
			if ( colorz == clrz) { color = 0x601; } clrz++;
			if ( colorz == clrz) { color = 0x602; } clrz++;
			if ( colorz == clrz) { color = 0x603; } clrz++;
			if ( colorz == clrz) { color = 0x604; } clrz++;
			if ( colorz == clrz) { color = 0x605; } clrz++;
			if ( colorz == clrz) { color = 0x606; } clrz++;
			if ( colorz == clrz) { color = 0x607; } clrz++;
			if ( colorz == clrz) { color = 0x608; } clrz++;
			if ( colorz == clrz) { color = 0x609; } clrz++;
			if ( colorz == clrz) { color = 0x60A; } clrz++;
			if ( colorz == clrz) { color = 0x60B; } clrz++;
			if ( colorz == clrz) { color = 0x60C; } clrz++;
			if ( colorz == clrz) { color = 0x60D; } clrz++;
			if ( colorz == clrz) { color = 0x60E; } clrz++;
			if ( colorz == clrz) { color = 0x60F; } clrz++;
			if ( colorz == clrz) { color = 0x610; } clrz++;
			if ( colorz == clrz) { color = 0x611; } clrz++;
			if ( colorz == clrz) { color = 0x612; } clrz++;
			if ( colorz == clrz) { color = 0x4B1; } clrz++;
			if ( colorz == clrz) { color = 0x4B2; } clrz++;
			if ( colorz == clrz) { color = 0x4B3; } clrz++;
			if ( colorz == clrz) { color = 0x4B4; } clrz++;
			if ( colorz == clrz) { color = 0x4B5; } clrz++;
			if ( colorz == clrz) { color = 0x4B6; } clrz++;
			if ( colorz == clrz) { color = 0x4B7; } clrz++;
			if ( colorz == clrz) { color = 0x4B8; } clrz++;
			if ( colorz == clrz) { color = 0x4B9; } clrz++;
			if ( colorz == clrz) { color = 0x4BA; } clrz++;
			if ( colorz == clrz) { color = 0x4BB; } clrz++;
			if ( colorz == clrz) { color = 0x4BC; } clrz++;
			if ( colorz == clrz) { color = 0x4BD; } clrz++;
			if ( colorz == clrz) { color = 0x4BE; } clrz++;
			if ( colorz == clrz) { color = 0x4BF; } clrz++;
			if ( colorz == clrz) { color = 0x4C0; } clrz++;
			if ( colorz == clrz) { color = 0x4C1; } clrz++;
			if ( colorz == clrz) { color = 0x4C2; } clrz++;
			if ( colorz == clrz) { color = 0x4C3; } clrz++;
			if ( colorz == clrz) { color = 0x4C4; } clrz++;
			if ( colorz == clrz) { color = 0x4C5; } clrz++;
			if ( colorz == clrz) { color = 0x4C6; } clrz++;
			if ( colorz == clrz) { color = 0x4C7; } clrz++;
			if ( colorz == clrz) { color = 0x4C8; } clrz++;
			if ( colorz == clrz) { color = 0x4C9; } clrz++;
			if ( colorz == clrz) { color = 0x4CA; } clrz++;
			if ( colorz == clrz) { color = 0x4CB; } clrz++;
			if ( colorz == clrz) { color = 0x4CC; } clrz++;
			if ( colorz == clrz) { color = 0x4CD; } clrz++;
			if ( colorz == clrz) { color = 0x4CE; } clrz++;
			if ( colorz == clrz) { color = 0x4CF; } clrz++;
			if ( colorz == clrz) { color = 0x4D0; } clrz++;
			if ( colorz == clrz) { color = 0x4D1; } clrz++;
			if ( colorz == clrz) { color = 0x4D2; } clrz++;
			if ( colorz == clrz) { color = 0x4D3; } clrz++;
			if ( colorz == clrz) { color = 0x4D4; } clrz++;
			if ( colorz == clrz) { color = 0x4D5; } clrz++;
			if ( colorz == clrz) { color = 0x4D6; } clrz++;
			if ( colorz == clrz) { color = 0x4D7; } clrz++;
			if ( colorz == clrz) { color = 0x4D8; } clrz++;
			if ( colorz == clrz) { color = 0x4D9; } clrz++;
			if ( colorz == clrz) { color = 0x4DA; } clrz++;
			if ( colorz == clrz) { color = 0x4DB; } clrz++;
			if ( colorz == clrz) { color = 0x4DC; } clrz++;
			if ( colorz == clrz) { color = 0x4DD; } clrz++;
			if ( colorz == clrz) { color = 0x4DE; } clrz++;
			if ( colorz == clrz) { color = 0x4DF; } clrz++;
			if ( colorz == clrz) { color = 0x4E0; } clrz++;
			if ( colorz == clrz) { color = 0x4E1; } clrz++;
			if ( colorz == clrz) { color = 0x4E2; } clrz++;
			if ( colorz == clrz) { color = 0x4E3; } clrz++;
			if ( colorz == clrz) { color = 0x4E4; } clrz++;
			if ( colorz == clrz) { color = 0x4E5; } clrz++;
			if ( colorz == clrz) { color = 0x4E6; } clrz++;
			if ( colorz == clrz) { color = 0x641; } clrz++;
			if ( colorz == clrz) { color = 0x642; } clrz++;
			if ( colorz == clrz) { color = 0x643; } clrz++;
			if ( colorz == clrz) { color = 0x644; } clrz++;
			if ( colorz == clrz) { color = 0x645; } clrz++;
			if ( colorz == clrz) { color = 0x646; } clrz++;
			if ( colorz == clrz) { color = 0x647; } clrz++;
			if ( colorz == clrz) { color = 0x648; } clrz++;
			if ( colorz == clrz) { color = 0x649; } clrz++;
			if ( colorz == clrz) { color = 0x64A; } clrz++;
			if ( colorz == clrz) { color = 0x64B; } clrz++;
			if ( colorz == clrz) { color = 0x64C; } clrz++;
			if ( colorz == clrz) { color = 0x64D; } clrz++;
			if ( colorz == clrz) { color = 0x64E; } clrz++;
			if ( colorz == clrz) { color = 0x64F; } clrz++;
			if ( colorz == clrz) { color = 0x650; } clrz++;
			if ( colorz == clrz) { color = 0x651; } clrz++;
			if ( colorz == clrz) { color = 0x652; } clrz++;
			if ( colorz == clrz) { color = 0x653; } clrz++;
			if ( colorz == clrz) { color = 0x654; } clrz++;
			if ( colorz == clrz) { color = 0x655; } clrz++;
			if ( colorz == clrz) { color = 0x656; } clrz++;
			if ( colorz == clrz) { color = 0x657; } clrz++;
			if ( colorz == clrz) { color = 0x658; } clrz++;
			if ( colorz == clrz) { color = 0x659; } clrz++;
			if ( colorz == clrz) { color = 0x65A; } clrz++;
			if ( colorz == clrz) { color = 0x65B; } clrz++;
			if ( colorz == clrz) { color = 0x65C; } clrz++;
			if ( colorz == clrz) { color = 0x65D; } clrz++;
			if ( colorz == clrz) { color = 0x65E; } clrz++;
			if ( colorz == clrz) { color = 0x65F; } clrz++;
			if ( colorz == clrz) { color = 0x660; } clrz++;
			if ( colorz == clrz) { color = 0x661; } clrz++;
			if ( colorz == clrz) { color = 0x662; } clrz++;
			if ( colorz == clrz) { color = 0x663; } clrz++;
			if ( colorz == clrz) { color = 0x664; } clrz++;
			if ( colorz == clrz) { color = 0x665; } clrz++;
			if ( colorz == clrz) { color = 0x666; } clrz++;
			if ( colorz == clrz) { color = 0x667; } clrz++;
			if ( colorz == clrz) { color = 0x668; } clrz++;
			if ( colorz == clrz) { color = 0x669; } clrz++;
			if ( colorz == clrz) { color = 0x66A; } clrz++;
			if ( colorz == clrz) { color = 0x66B; } clrz++;
			if ( colorz == clrz) { color = 0x66C; } clrz++;
			if ( colorz == clrz) { color = 0x66D; } clrz++;
			if ( colorz == clrz) { color = 0x66E; } clrz++;
			if ( colorz == clrz) { color = 0x66F; } clrz++;
			if ( colorz == clrz) { color = 0x670; } clrz++;
			if ( colorz == clrz) { color = 0x671; } clrz++;
			if ( colorz == clrz) { color = 0x672; } clrz++;
			if ( colorz == clrz) { color = 0x673; } clrz++;
			if ( colorz == clrz) { color = 0x674; } clrz++;
			if ( colorz == clrz) { color = 0x675; } clrz++;
			if ( colorz == clrz) { color = 0x676; } clrz++;
			if ( colorz == clrz) { color = 0x6A5; } clrz++;
			if ( colorz == clrz) { color = 0x6A6; } clrz++;
			if ( colorz == clrz) { color = 0x6A7; } clrz++;
			if ( colorz == clrz) { color = 0x6A8; } clrz++;
			if ( colorz == clrz) { color = 0x6A9; } clrz++;
			if ( colorz == clrz) { color = 0x6AA; } clrz++;
			if ( colorz == clrz) { color = 0x6AB; } clrz++;
			if ( colorz == clrz) { color = 0x6AC; } clrz++;
			if ( colorz == clrz) { color = 0x6AD; } clrz++;
			if ( colorz == clrz) { color = 0x6AE; } clrz++;
			if ( colorz == clrz) { color = 0x6AF; } clrz++;
			if ( colorz == clrz) { color = 0x6B0; } clrz++;
			if ( colorz == clrz) { color = 0x6B1; } clrz++;
			if ( colorz == clrz) { color = 0x6B2; } clrz++;
			if ( colorz == clrz) { color = 0x6B3; } clrz++;
			if ( colorz == clrz) { color = 0x6B4; } clrz++;
			if ( colorz == clrz) { color = 0x6B5; } clrz++;
			if ( colorz == clrz) { color = 0x6B6; } clrz++;
			if ( colorz == clrz) { color = 0x6B7; } clrz++;
			if ( colorz == clrz) { color = 0x6B8; } clrz++;
			if ( colorz == clrz) { color = 0x6B9; } clrz++;
			if ( colorz == clrz) { color = 0x6BA; } clrz++;
			if ( colorz == clrz) { color = 0x6BB; } clrz++;
			if ( colorz == clrz) { color = 0x6BC; } clrz++;
			if ( colorz == clrz) { color = 0x6BD; } clrz++;
			if ( colorz == clrz) { color = 0x6BE; } clrz++;
			if ( colorz == clrz) { color = 0x6BF; } clrz++;
			if ( colorz == clrz) { color = 0x6C0; } clrz++;
			if ( colorz == clrz) { color = 0x6C1; } clrz++;
			if ( colorz == clrz) { color = 0x6C2; } clrz++;
			if ( colorz == clrz) { color = 0x6C3; } clrz++;
			if ( colorz == clrz) { color = 0x6C4; } clrz++;
			if ( colorz == clrz) { color = 0x6C5; } clrz++;
			if ( colorz == clrz) { color = 0x6C6; } clrz++;
			if ( colorz == clrz) { color = 0x6C7; } clrz++;
			if ( colorz == clrz) { color = 0x6C8; } clrz++;
			if ( colorz == clrz) { color = 0x6C9; } clrz++;
			if ( colorz == clrz) { color = 0x6CA; } clrz++;
			if ( colorz == clrz) { color = 0x6CB; } clrz++;
			if ( colorz == clrz) { color = 0x6CC; } clrz++;
			if ( colorz == clrz) { color = 0x6CD; } clrz++;
			if ( colorz == clrz) { color = 0x6CE; } clrz++;
			if ( colorz == clrz) { color = 0x6CF; } clrz++;
			if ( colorz == clrz) { color = 0x6D0; } clrz++;
			if ( colorz == clrz) { color = 0x6D1; } clrz++;
			if ( colorz == clrz) { color = 0x6D2; } clrz++;
			if ( colorz == clrz) { color = 0x6D3; } clrz++;
			if ( colorz == clrz) { color = 0x6D4; } clrz++;
			if ( colorz == clrz) { color = 0x6D5; } clrz++;
			if ( colorz == clrz) { color = 0x6D6; } clrz++;
			if ( colorz == clrz) { color = 0x6D7; } clrz++;
			if ( colorz == clrz) { color = 0x6D8; } clrz++;
			if ( colorz == clrz) { color = 0x6D9; } clrz++;
			if ( colorz == clrz) { color = 0x6DA; } clrz++;
			if ( colorz == clrz) { color = 0x709; } clrz++;
			if ( colorz == clrz) { color = 0x70A; } clrz++;
			if ( colorz == clrz) { color = 0x70B; } clrz++;
			if ( colorz == clrz) { color = 0x70C; } clrz++;
			if ( colorz == clrz) { color = 0x70D; } clrz++;
			if ( colorz == clrz) { color = 0x70E; } clrz++;
			if ( colorz == clrz) { color = 0x70F; } clrz++;
			if ( colorz == clrz) { color = 0x710; } clrz++;
			if ( colorz == clrz) { color = 0x711; } clrz++;
			if ( colorz == clrz) { color = 0x712; } clrz++;
			if ( colorz == clrz) { color = 0x713; } clrz++;
			if ( colorz == clrz) { color = 0x714; } clrz++;
			if ( colorz == clrz) { color = 0x715; } clrz++;
			if ( colorz == clrz) { color = 0x716; } clrz++;
			if ( colorz == clrz) { color = 0x717; } clrz++;
			if ( colorz == clrz) { color = 0x718; } clrz++;
			if ( colorz == clrz) { color = 0x719; } clrz++;
			if ( colorz == clrz) { color = 0x71A; } clrz++;
			if ( colorz == clrz) { color = 0x71B; } clrz++;
			if ( colorz == clrz) { color = 0x71C; } clrz++;
			if ( colorz == clrz) { color = 0x71D; } clrz++;
			if ( colorz == clrz) { color = 0x71E; } clrz++;
			if ( colorz == clrz) { color = 0x71F; } clrz++;
			if ( colorz == clrz) { color = 0x720; } clrz++;
			if ( colorz == clrz) { color = 0x721; } clrz++;
			if ( colorz == clrz) { color = 0x722; } clrz++;
			if ( colorz == clrz) { color = 0x723; } clrz++;
			if ( colorz == clrz) { color = 0x724; } clrz++;
			if ( colorz == clrz) { color = 0x725; } clrz++;
			if ( colorz == clrz) { color = 0x726; } clrz++;
			if ( colorz == clrz) { color = 0x727; } clrz++;
			if ( colorz == clrz) { color = 0x728; } clrz++;
			if ( colorz == clrz) { color = 0x729; } clrz++;
			if ( colorz == clrz) { color = 0x72A; } clrz++;
			if ( colorz == clrz) { color = 0x72B; } clrz++;
			if ( colorz == clrz) { color = 0x72C; } clrz++;
			if ( colorz == clrz) { color = 0x72D; } clrz++;
			if ( colorz == clrz) { color = 0x72E; } clrz++;
			if ( colorz == clrz) { color = 0x72F; } clrz++;
			if ( colorz == clrz) { color = 0x730; } clrz++;
			if ( colorz == clrz) { color = 0x731; } clrz++;
			if ( colorz == clrz) { color = 0x732; } clrz++;
			if ( colorz == clrz) { color = 0x733; } clrz++;
			if ( colorz == clrz) { color = 0x734; } clrz++;
			if ( colorz == clrz) { color = 0x735; } clrz++;
			if ( colorz == clrz) { color = 0x736; } clrz++;
			if ( colorz == clrz) { color = 0x737; } clrz++;
			if ( colorz == clrz) { color = 0x738; } clrz++;
			if ( colorz == clrz) { color = 0x739; } clrz++;
			if ( colorz == clrz) { color = 0x73A; } clrz++;
			if ( colorz == clrz) { color = 0x73B; } clrz++;
			if ( colorz == clrz) { color = 0x73C; } clrz++;
			if ( colorz == clrz) { color = 0x73D; } clrz++;
			if ( colorz == clrz) { color = 0x73E; } clrz++;
			if ( colorz == clrz) { color = 0x73F; } clrz++;
			if ( colorz == clrz) { color = 0x740; } clrz++;
			if ( colorz == clrz) { color = 0x741; } clrz++;
			if ( colorz == clrz) { color = 0x742; } clrz++;
			if ( colorz == clrz) { color = 0x743; } clrz++;
			if ( colorz == clrz) { color = 0x744; } clrz++;
			if ( colorz == clrz) { color = 0x745; } clrz++;
			if ( colorz == clrz) { color = 0x746; } clrz++;
			if ( colorz == clrz) { color = 0x747; } clrz++;
			if ( colorz == clrz) { color = 0x748; } clrz++;
			if ( colorz == clrz) { color = 0x749; } clrz++;
			if ( colorz == clrz) { color = 0x74A; } clrz++;
			if ( colorz == clrz) { color = 0x74B; } clrz++;
			if ( colorz == clrz) { color = 0x74C; } clrz++;
			if ( colorz == clrz) { color = 0x74D; } clrz++;
			if ( colorz == clrz) { color = 0x74E; } clrz++;
			if ( colorz == clrz) { color = 0x74F; } clrz++;
			if ( colorz == clrz) { color = 0x750; } clrz++;
			if ( colorz == clrz) { color = 0x751; } clrz++;
			if ( colorz == clrz) { color = 0x752; } clrz++;
			if ( colorz == clrz) { color = 0x753; } clrz++;
			if ( colorz == clrz) { color = 0x754; } clrz++;
			if ( colorz == clrz) { color = 0x755; } clrz++;
			if ( colorz == clrz) { color = 0x756; } clrz++;
			if ( colorz == clrz) { color = 0x757; } clrz++;
			if ( colorz == clrz) { color = 0x758; } clrz++;
			if ( colorz == clrz) { color = 0x759; } clrz++;
			if ( colorz == clrz) { color = 0x75A; } clrz++;
			if ( colorz == clrz) { color = 0x75B; } clrz++;
			if ( colorz == clrz) { color = 0x75C; } clrz++;
			if ( colorz == clrz) { color = 0x75D; } clrz++;
			if ( colorz == clrz) { color = 0x75E; } clrz++;
			if ( colorz == clrz) { color = 0x75F; } clrz++;
			if ( colorz == clrz) { color = 0x760; } clrz++;
			if ( colorz == clrz) { color = 0x761; } clrz++;
			if ( colorz == clrz) { color = 0x762; } clrz++;
			if ( colorz == clrz) { color = 0x763; } clrz++;
			if ( colorz == clrz) { color = 0x764; } clrz++;
			if ( colorz == clrz) { color = 0x765; } clrz++;
			if ( colorz == clrz) { color = 0x766; } clrz++;
			if ( colorz == clrz) { color = 0x767; } clrz++;
			if ( colorz == clrz) { color = 0x768; } clrz++;
			if ( colorz == clrz) { color = 0x769; } clrz++;
			if ( colorz == clrz) { color = 0x76A; } clrz++;
			if ( colorz == clrz) { color = 0x76B; } clrz++;
			if ( colorz == clrz) { color = 0x76C; } clrz++;
			if ( colorz == clrz) { color = 0x76D; } clrz++;
			if ( colorz == clrz) { color = 0x76E; } clrz++;
			if ( colorz == clrz) { color = 0x76F; } clrz++;
			if ( colorz == clrz) { color = 0x770; } clrz++;
			if ( colorz == clrz) { color = 0x771; } clrz++;
			if ( colorz == clrz) { color = 0x772; } clrz++;
			if ( colorz == clrz) { color = 0x773; } clrz++;
			if ( colorz == clrz) { color = 0x774; } clrz++;
			if ( colorz == clrz) { color = 0x6DB; } clrz++;
			if ( colorz == clrz) { color = 0x6DC; } clrz++;
			if ( colorz == clrz) { color = 0x6DD; } clrz++;
			if ( colorz == clrz) { color = 0x6DE; } clrz++;
			if ( colorz == clrz) { color = 0x6DF; } clrz++;
			if ( colorz == clrz) { color = 0x6E0; } clrz++;
			if ( colorz == clrz) { color = 0x6E1; } clrz++;
			if ( colorz == clrz) { color = 0x6E2; } clrz++;
			if ( colorz == clrz) { color = 0x6E3; } clrz++;
			if ( colorz == clrz) { color = 0x6E4; } clrz++;
			if ( colorz == clrz) { color = 0x6E5; } clrz++;
			if ( colorz == clrz) { color = 0x6E6; } clrz++;
			if ( colorz == clrz) { color = 0x6E7; } clrz++;
			if ( colorz == clrz) { color = 0x6E8; } clrz++;
			if ( colorz == clrz) { color = 0x6E9; } clrz++;
			if ( colorz == clrz) { color = 0x6EA; } clrz++;
			if ( colorz == clrz) { color = 0x6EB; } clrz++;
			if ( colorz == clrz) { color = 0x6EC; } clrz++;
			if ( colorz == clrz) { color = 0x6ED; } clrz++;
			if ( colorz == clrz) { color = 0x6EE; } clrz++;
			if ( colorz == clrz) { color = 0x6EF; } clrz++;
			if ( colorz == clrz) { color = 0x6F0; } clrz++;
			if ( colorz == clrz) { color = 0x6F1; } clrz++;
			if ( colorz == clrz) { color = 0x6F2; } clrz++;
			if ( colorz == clrz) { color = 0x6F3; } clrz++;
			if ( colorz == clrz) { color = 0x7D1; } clrz++;
			if ( colorz == clrz) { color = 0x7D2; } clrz++;
			if ( colorz == clrz) { color = 0x7D3; } clrz++;
			if ( colorz == clrz) { color = 0x7D4; } clrz++;
			if ( colorz == clrz) { color = 0x7D5; } clrz++;
			if ( colorz == clrz) { color = 0x7D6; } clrz++;
			if ( colorz == clrz) { color = 0x7D7; } clrz++;
			if ( colorz == clrz) { color = 0x7D8; } clrz++;
			if ( colorz == clrz) { color = 0x7D9; } clrz++;
			if ( colorz == clrz) { color = 0x7DA; } clrz++;
			if ( colorz == clrz) { color = 0x7DB; } clrz++;
			if ( colorz == clrz) { color = 0x7DC; } clrz++;
			if ( colorz == clrz) { color = 0x7DD; } clrz++;
			if ( colorz == clrz) { color = 0x7DE; } clrz++;
			if ( colorz == clrz) { color = 0x7DF; } clrz++;
			if ( colorz == clrz) { color = 0x7E1; } clrz++;
			if ( colorz == clrz) { color = 0x7E2; } clrz++;
			if ( colorz == clrz) { color = 0x835; } clrz++;
			if ( colorz == clrz) { color = 0x836; } clrz++;
			if ( colorz == clrz) { color = 0x837; } clrz++;
			if ( colorz == clrz) { color = 0x838; } clrz++;
			if ( colorz == clrz) { color = 0x839; } clrz++;
			if ( colorz == clrz) { color = 0x83A; } clrz++;
			if ( colorz == clrz) { color = 0x83B; } clrz++;
			if ( colorz == clrz) { color = 0x83C; } clrz++;
			if ( colorz == clrz) { color = 0x83D; } clrz++;
			if ( colorz == clrz) { color = 0x83E; } clrz++;
			if ( colorz == clrz) { color = 0x83F; } clrz++;
			if ( colorz == clrz) { color = 0x840; } clrz++;
			if ( colorz == clrz) { color = 0x841; } clrz++;
			if ( colorz == clrz) { color = 0x842; } clrz++;
			if ( colorz == clrz) { color = 0x843; } clrz++;
			if ( colorz == clrz) { color = 0x844; } clrz++;
			if ( colorz == clrz) { color = 0x845; } clrz++;
			if ( colorz == clrz) { color = 0x846; } clrz++;
			if ( colorz == clrz) { color = 0x847; } clrz++;
			if ( colorz == clrz) { color = 0x848; } clrz++;
			if ( colorz == clrz) { color = 0x849; } clrz++;
			if ( colorz == clrz) { color = 0x84A; } clrz++;
			if ( colorz == clrz) { color = 0x84B; } clrz++;
			if ( colorz == clrz) { color = 0x84C; } clrz++;
			if ( colorz == clrz) { color = 0x84D; } clrz++;
			if ( colorz == clrz) { color = 0x84E; } clrz++;
			if ( colorz == clrz) { color = 0x84F; } clrz++;
			if ( colorz == clrz) { color = 0x850; } clrz++;
			if ( colorz == clrz) { color = 0x851; } clrz++;
			if ( colorz == clrz) { color = 0x852; } clrz++;
			if ( colorz == clrz) { color = 0x899; } clrz++;
			if ( colorz == clrz) { color = 0x89A; } clrz++;
			if ( colorz == clrz) { color = 0x89B; } clrz++;
			if ( colorz == clrz) { color = 0x89C; } clrz++;
			if ( colorz == clrz) { color = 0x89D; } clrz++;
			if ( colorz == clrz) { color = 0x89E; } clrz++;
			if ( colorz == clrz) { color = 0x89F; } clrz++;
			if ( colorz == clrz) { color = 0x8A0; } clrz++;
			if ( colorz == clrz) { color = 0x8A1; } clrz++;
			if ( colorz == clrz) { color = 0x8A2; } clrz++;
			if ( colorz == clrz) { color = 0x8A3; } clrz++;
			if ( colorz == clrz) { color = 0x8A4; } clrz++;
			if ( colorz == clrz) { color = 0x8A5; } clrz++;
			if ( colorz == clrz) { color = 0x8A6; } clrz++;
			if ( colorz == clrz) { color = 0x8A7; } clrz++;
			if ( colorz == clrz) { color = 0x8A8; } clrz++;
			if ( colorz == clrz) { color = 0x8A9; } clrz++;
			if ( colorz == clrz) { color = 0x8AA; } clrz++;
			if ( colorz == clrz) { color = 0x8AB; } clrz++;
			if ( colorz == clrz) { color = 0x8AC; } clrz++;
			if ( colorz == clrz) { color = 0x8AD; } clrz++;
			if ( colorz == clrz) { color = 0x8AE; } clrz++;
			if ( colorz == clrz) { color = 0x8AF; } clrz++;
			if ( colorz == clrz) { color = 0x8B0; } clrz++;
			if ( colorz == clrz) { color = 0x8FD; } clrz++;
			if ( colorz == clrz) { color = 0x8FE; } clrz++;
			if ( colorz == clrz) { color = 0x8FF; } clrz++;
			if ( colorz == clrz) { color = 0x900; } clrz++;
			if ( colorz == clrz) { color = 0x901; } clrz++;
			if ( colorz == clrz) { color = 0x902; } clrz++;
			if ( colorz == clrz) { color = 0x903; } clrz++;
			if ( colorz == clrz) { color = 0x904; } clrz++;
			if ( colorz == clrz) { color = 0x905; } clrz++;
			if ( colorz == clrz) { color = 0x906; } clrz++;
			if ( colorz == clrz) { color = 0x907; } clrz++;
			if ( colorz == clrz) { color = 0x908; } clrz++;
			if ( colorz == clrz) { color = 0x909; } clrz++;
			if ( colorz == clrz) { color = 0x90A; } clrz++;
			if ( colorz == clrz) { color = 0x90B; } clrz++;
			if ( colorz == clrz) { color = 0x90C; } clrz++;
			if ( colorz == clrz) { color = 0x90D; } clrz++;
			if ( colorz == clrz) { color = 0x90E; } clrz++;
			if ( colorz == clrz) { color = 0x961; } clrz++;
			if ( colorz == clrz) { color = 0x962; } clrz++;
			if ( colorz == clrz) { color = 0x963; } clrz++;
			if ( colorz == clrz) { color = 0x964; } clrz++;
			if ( colorz == clrz) { color = 0x965; } clrz++;
			if ( colorz == clrz) { color = 0x966; } clrz++;
			if ( colorz == clrz) { color = 0x967; } clrz++;
			if ( colorz == clrz) { color = 0x968; } clrz++;
			if ( colorz == clrz) { color = 0x969; } clrz++;
			if ( colorz == clrz) { color = 0x96A; } clrz++;
			if ( colorz == clrz) { color = 0x96B; } clrz++;
			if ( colorz == clrz) { color = 0x96C; } clrz++;
			if ( colorz == clrz) { color = 0x96D; } clrz++;
			if ( colorz == clrz) { color = 0x96E; } clrz++;
			if ( colorz == clrz) { color = 0x96F; } clrz++;
			if ( colorz == clrz) { color = 0x970; } clrz++;
			if ( colorz == clrz) { color = 0x971; } clrz++;
			if ( colorz == clrz) { color = 0x972; } clrz++;
			if ( colorz == clrz) { color = 0x973; } clrz++;
			if ( colorz == clrz) { color = 0x974; } clrz++;
			if ( colorz == clrz) { color = 0x975; } clrz++;
			if ( colorz == clrz) { color = 0x976; } clrz++;
			if ( colorz == clrz) { color = 0x977; } clrz++;
			if ( colorz == clrz) { color = 0x978; } clrz++;
			if ( colorz == clrz) { color = 0x979; } clrz++;
			if ( colorz == clrz) { color = 0x97A; } clrz++;
			if ( colorz == clrz) { color = 0x97B; } clrz++;
			if ( colorz == clrz) { color = 0x97C; } clrz++;
			if ( colorz == clrz) { color = 0x97D; } clrz++;
			if ( colorz == clrz) { color = 0x97E; } clrz++;
			if ( colorz == clrz) { color = 0x430; } clrz++;

			return color;
		}
	}
}