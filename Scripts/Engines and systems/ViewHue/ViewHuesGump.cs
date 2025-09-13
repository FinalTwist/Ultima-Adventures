/*************
 *  X --->   *
 * Y         *
 *           *
 * |         *
 * |         *
 * V         *
 *************/
//Espcevan

using System;
using System.Collections;
using System.IO;
using Server;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Targeting;

namespace Server.Gumps
{
	public class ViewHuesGump : Gump
	{
        private Mobile m_From;
		private readonly int m_newstart;
        private int m_itemID;
        private int Total_HUES = 3000;

        string mSetHue = "M Set Hue";
        string SetHue = "Set Hue";


		public ViewHuesGump( Mobile from, int index , int itemid) : base( 50, 40 )
		{
            int PageHeight = 485;
            int PageWidth = 522;
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;

            int start = index;

			m_From = from;
			m_newstart = index;
            m_itemID = itemid;

			AddPage( 0 );

			AddBackground( 0, 0, 521, 485, 1755 );
            AddHtml(32, 13, 150, 20,
                @"<basefont color =  #478cf7><center><h1>View Hues", ( bool ) false, ( bool ) false);

            //-----------------------------------------------------------------------
            AddBackground(21, ( PageHeight - 65 ), 482, 50, 0x2454);

            AddLabel( 345, 11, 294, "Back");
            AddButton( 375, 14, 0x15E3, 0x15E7, 10001, GumpButtonType.Reply, 0 );//Back
			AddButton( 395, 14, 0x15E1, 0x15E5, 10002, GumpButtonType.Reply, 0 );//Next
            AddLabel( 415, 11, 294, "Next");

            AddButton(470, 13, 0x657, 0x657, 10050, GumpButtonType.Reply, 0);//MiniVersion
            AddButton(490, 15, 3, 4, 10003, GumpButtonType.Reply, 0);//Exit


            //-----------------------------------------------------------------------
            AddLabel(65, ( PageHeight - 65 ), 294, "Item ID");
            AddButton(50, (PageHeight -41), 0x4B9, 0x4BA, 10004, GumpButtonType.Reply, 0);//set itemid
            AddImage(70, ( PageHeight - 45 ), 0x98C);
            AddTextEntry(76, ( PageHeight - 44 ), 60, 20, 10, 10, String.Format( "{0}", m_itemID));
            //-----------------------------------------------------------------------
            AddLabel(160, ( PageHeight - 65 ), 294, "GoTo Hue");
            AddButton( 145, ( PageHeight - 41 ), 2224, 2224, 10000, GumpButtonType.Reply, 0 );//goto page
            AddImage(165, ( PageHeight -  45), 0x98C);
            AddTextEntry(171, ( PageHeight - 44 ), 60, 20, 10, 20, "");
            //-----------------------------------------------------------------------
            AddCheck(250, ( PageHeight - 50 ), 0x9CE, 0x9CF, false, 5000);
            AddLabel(266, ( PageHeight - 50 ), 294, " [M Set Hue");
            //-----------------------------------------------------------------------

            


            for ( int i = 0; i < 5; ++i )
            {
                AddLabel(30, 65 + ( i * 75 ), 1152, String.Format("{0}", index + ( i * 6 )));
                for ( int j = 0; j < 6; ++j )
                {

                    AddButton(68 + ( j * 75 ), 48 + ( i * 75 ), 2351, 2353, index + ( i * 6 ) + ( j ), GumpButtonType.Reply, 0);
                    AddItem(70 + ( j * 75 ), 50 + ( i * 75 ), m_itemID, index + ( i * 6 ) + ( j ));
                    //AddHtml(71 + ( j * 75 ), 95 + ( i * 75 ), 50, 20, String.Format(@"<center><basefont color = #f0f0f0>{0}", index + ( i * 6 ) + ( j )),
                    //( bool ) false, ( bool ) false);
                    AddLabel(80 + ( j * 75 ), 95 + ( i * 75 ), index + ( i * 6 ) + ( j ) - 1, String.Format(@"{0}", index + ( i * 6 ) + ( j )));
                }
            }
        }

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			int newstart = 1;
			int buttonID = info.ButtonID;
            int itemID = m_itemID;

            if ( info == null || state == null || from == null )
            {
                return;
            }

            switch ( info.ButtonID )
            {
                case 10050:
                    {
                        from.CloseGump(typeof(ViewHuesGump));
                        from.SendGump(new ViewHueGump(from, 0));
                        break;
                    }
                case 10004:
                    {
                        from.CloseGump(typeof(ViewHuesGump));
                        TextRelay entry = info.GetTextEntry( 10 );

                        try { itemID = Convert.ToInt32(entry.Text); }
                        catch { }
                        if ( itemID < 1 )
                            itemID = 4011;
                        else if (itemID > -0)
                        {
                            m_itemID = itemID;
                        }

                        from.SendGump(new ViewHuesGump(from, newstart, m_itemID));
                        break;
                    }
                case 10003:
                    {
                        from.CloseGump(typeof(ViewHuesGump));
                        break;
                    }
                case 10002:
                    {
                        if ( ( m_newstart + 30 ) > Total_HUES )
                        {
                            from.CloseGump(typeof(ViewHuesGump));
                            from.SendGump(new ViewHuesGump(from, 2971, m_itemID));
                        }
                        else
                        {
                            from.CloseGump(typeof(ViewHuesGump));
                            from.SendGump(new ViewHuesGump(from, m_newstart + 30, m_itemID));
                        }
                        break;
                    }

                case 10001:
                    {
                        if ( ( m_newstart - 30 ) < 1 )
                        {
                            from.CloseGump(typeof(ViewHuesGump));
                            from.SendGump(new ViewHuesGump(from, 1, m_itemID));
                        }
                        else
                        {
                            from.CloseGump(typeof(ViewHuesGump));
                            from.SendGump(new ViewHuesGump(from, m_newstart - 30, m_itemID));
                        }

                        break;
                    }
                case 10000:
                    {
                        from.CloseGump(typeof(ViewHuesGump));
                        TextRelay entry = info.GetTextEntry( 20 );

                        try { newstart = Convert.ToInt32(entry.Text); }
                        catch { }
                        if ( newstart < 1 )
                            newstart = 1;
                        else if ( newstart > 2971 )
                            newstart = 2971;

                        from.SendGump(new ViewHuesGump(from, newstart, m_itemID));
                        break;
                    }
                default:
                    {
                        if ( buttonID <= 0 )
                        {
                            from.SendGump(new ViewHuesGump(from, m_newstart, m_itemID));
                        }
                        else
                        {
                            if (info.IsSwitched(5000))
                            {
                                CommandSystem.Handle(from, String.Format("{0}{1} {2}", CommandSystem.Prefix, mSetHue, buttonID));
                                from.SendGump(new ViewHuesGump(from, m_newstart, m_itemID));
                                info.IsSwitched(5000);
                            }
                            else
                            {
                                CommandSystem.Handle(from, String.Format("{0}{1} {2}", CommandSystem.Prefix, SetHue, buttonID));
                                from.SendGump(new ViewHuesGump(from, m_newstart, m_itemID));
                            }
                        }
                        break;
                    }
            }
		}
	}
}
