#region References

using System;
using System.Collections;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

#endregion

namespace Server.Gumps
{
    public class ViewHueGump : Gump
    {

        private ArrayList Rewards;

        public int y_inc = 20;
        private int x_creditoffset = 75;
        private int x_pointsoffset = 280;
        private int maxItemsPerPage = 15;
        private readonly int viewpage;

        public const int TOTAL_HUES = 3000;

        Mobile caller;
        public ViewHueGump( Mobile from, int page ) : base(0, 0)
        {
            viewpage = page;

            int gotopageheight = 500;
            int pagewidth = 353;
            int height = maxItemsPerPage * y_inc + 150;
            int width = x_pointsoffset + 45;

            PlayerMobile pm = from as PlayerMobile;

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(35, 67, 190, 460, 1755);
            //AddBackground(35, 67, 190, 460, 9200);
            //AddBackground(42, 93, 175, 342, 1755);

            AddHtml(46, 72, 150, 20,
                @"<basefont color = #478cf7><center><h1>View Hue", ( bool ) false, ( bool ) false);
            AddButton(200, 76, 3, 4, ( int ) Buttons.CloseBtn, GumpButtonType.Reply, 0);

            AddLabel(60, 100, 294, "Hue");

            int y = 100;
            for ( int i = 0; i < TOTAL_HUES; i++ )
            {
                if ( i / maxItemsPerPage != viewpage )
                {
                    continue;
                }

                y += 20;

                int texthue = i;
                string color = Convert.ToString(texthue + 1);

                AddLabel(60, y + 3, texthue, color);

                AddItem(85, y, 0xFAB, texthue + 1);

                //The Button That Hues
                AddButton(pagewidth - 205, y, 0x992, 0x993, i + 4000, GumpButtonType.Reply, 0);
            }

            AddHtml(85, 435, 200, 20, String.Format(
                @"<basefont color =  #478cf7 >Page: {0}/{1}"
                , viewpage + 1, 3000 / maxItemsPerPage), ( bool ) false, ( bool ) false);
            // page up and down buttons
            AddButton(65, 437, 0x15E0, 0x15E4, 13, GumpButtonType.Reply, 0);
            AddButton(50, 437, 0x15E2, 0x15E6, 12, GumpButtonType.Reply, 0);

            AddHtml(50, 455, 160, 60,
                @"<basefont color = #094175><center>Enter a hue number to jump directly to it's page", false, false);
            AddAlphaRegion(80, gotopageheight - 4, 65, 20);
            AddTextEntry(80, gotopageheight - 4, 65, 20, 294, 14, @"1");
            // GoTo Hue Button
            AddButton(150, gotopageheight - 2, 2224, 2224, 15, GumpButtonType.Reply, 0);

            AddButton(195, 438, 0x4B9, 0x4BA, 99, GumpButtonType.Reply, 0);//Hue 0 Button
            AddHtml(185, 435, 50, 20, @"<basefont color = #478cf7 >0",
                ( bool ) false, ( bool ) false);


        }

        public enum Buttons
        {
            CloseBtn,
        }

        public override void OnResponse( NetState sender, RelayInfo info )
        {
            Mobile from = sender.Mobile;


            if ( info == null || sender == null || sender.Mobile == null )
            {
                return;
            }

            switch ( info.ButtonID )
            {
                case ( int ) Buttons.CloseBtn:
                    {

                        break;
                    }

                case 12:
                    // page up
                    int nitems = 0;
                    nitems = 5000;

                    int page = viewpage + 1;
                    if ( page > nitems / maxItemsPerPage )
                    {
                        page = nitems / maxItemsPerPage;
                    }
                    from.SendGump(new ViewHueGump(from, page));
                    break;
                case 13:
                    // page down
                    page = viewpage - 1;
                    if ( page < 0 )
                    {
                        page = 0;
                    }
                    from.SendGump(new ViewHueGump(from, page));
                    break;
                case 15:
                    {
                        // go to hue page
                        int hueToFind = 0;

                        string huestring = info.GetTextEntry(14).Text;

                        try
                        {
                            hueToFind = Convert.ToInt32(huestring, 10);
                        }
                        catch
                        {
                            hueToFind = 1;
                        }

                        if ( hueToFind < 1 )
                        {
                            hueToFind = 1;
                        }

                        if ( hueToFind > TOTAL_HUES )
                        {
                            hueToFind = TOTAL_HUES;
                        }

                        if ( hueToFind == null )
                        {
                            hueToFind = 1;
                        }

                        int pageHueFound = (hueToFind - 1) / maxItemsPerPage;

                        from.SendGump(new ViewHueGump(from, pageHueFound));

                        break;
                    }
                case 99:
                    {

                        int hueselection = 0;
                        int pageHueFound = 0;

                        CommandSystem.Handle(from, String.Format("{0}Set Hue {1}", CommandSystem.Prefix, hueselection));

                        from.SendGump(new ViewHueGump(from, viewpage));
                        break;
                    }
                default:
                    {
                        if ( info.ButtonID >= 4000 && info.ButtonID < 7001 )
                        {
                            int hueselection = info.ButtonID - 3999;
                            int pageHueFound = (hueselection) / maxItemsPerPage;


                            CommandSystem.Handle(from, String.Format("{0}Set Hue {1}", CommandSystem.Prefix, hueselection));

                            from.SendGump(new ViewHueGump(from, viewpage));
                            break;
                        }
                        break;
                    }

            }
        }
    }
}
