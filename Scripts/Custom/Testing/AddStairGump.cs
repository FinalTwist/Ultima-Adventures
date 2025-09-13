using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Network;

namespace Server.Gumps
{
    public class AddStairGump : Gump
    {
        public static StairInfo[] m_Types = new StairInfo[]
        {
            #region WoodWalls
            new StairInfo(1006),new StairInfo(1007),new StairInfo(1008),new StairInfo(1009),new StairInfo(1010),
            new StairInfo(1012),new StairInfo(1014),new StairInfo(1016),new StairInfo(1017),new StairInfo(1801),
            new StairInfo(1802),new StairInfo(1803),new StairInfo(1804),new StairInfo(1805),new StairInfo(1807),
            new StairInfo(1809),new StairInfo(1811),new StairInfo(1812),new StairInfo(1822),new StairInfo(1823),
            new StairInfo(1825),new StairInfo(1826),new StairInfo(1827),new StairInfo(1828),new StairInfo(1829),
            new StairInfo(1831),new StairInfo(1833),new StairInfo(1835),new StairInfo(1836),new StairInfo(1846),
            new StairInfo(1847),new StairInfo(1848),new StairInfo(1849),new StairInfo(1850),new StairInfo(1851),
            new StairInfo(1852),new StairInfo(1854),new StairInfo(1856),new StairInfo(1861),new StairInfo(1862),
            new StairInfo(1865),new StairInfo(1867),new StairInfo(1869),new StairInfo(1872),new StairInfo(1873),
            new StairInfo(1874),new StairInfo(1875),new StairInfo(1876),new StairInfo(1878),new StairInfo(1880),
            new StairInfo(1882),new StairInfo(1883),new StairInfo(1900),new StairInfo(1901),new StairInfo(1902),
            new StairInfo(1903),new StairInfo(1904),new StairInfo(1906),new StairInfo(1908),new StairInfo(1910),
            new StairInfo(1911),new StairInfo(1928),new StairInfo(1929),new StairInfo(1930),new StairInfo(1931),
            new StairInfo(1932),new StairInfo(1934),new StairInfo(1936),new StairInfo(1938),new StairInfo(1939),
            new StairInfo(1955),new StairInfo(1956),new StairInfo(1957),new StairInfo(1958),new StairInfo(1959),
            new StairInfo(1961),new StairInfo(1963),new StairInfo(1978),new StairInfo(1979),new StairInfo(1980),
            new StairInfo(1991)
            #endregion

        };


        private readonly int m_Type;

        public AddStairGump() : this(-1)
        {
        }
        public AddStairGump( int type ) : base( 0, 0 )
		{
            int pagewidth = 575;
            int pageheight = 180;
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);

            AddTheBackground(575, 180);

            int pages = m_Types.Length / 10 + 1;
            for ( int i = 0; i < m_Types.Length; ++i )
            {
                int page = i / 10 + 1;
                int xpos = i % 10 ;

                if ( xpos == 0 )
                {
                    AddPage(page);
                    AddHtmlLocalized(15, 15, 60, 20, 1042971, String.Format("{0}", page), 0x7FFF, false, false); // #

                    AddHtmlLocalized(20, 38, 60, 20, 1043353, 0x7FFF, false, false); // Next
                    if ( page < pages )
                        AddButton(15, 55, 1687, 1688, 0, GumpButtonType.Page, page + 1);
                    else
                        AddButton(15, 55, 1687, 1688, 0, GumpButtonType.Page, 1);

                    AddHtmlLocalized(20, 93, 60, 20, 1011393, 0x7FFF, false, false); // Back
                    if ( page > 1 )
                        AddButton(15, 110, 1689, 1690, 0, GumpButtonType.Page, page - 1);
                    else
                        AddButton(15, 110, 1689, 1690, 0, GumpButtonType.Page, pages);
                }

                if ( m_Types[i].m_BaseID == 0 )
                    continue;

                int x = (xpos + 1) * 50;
                AddButton(25 + x, 20, 2117, 2211853, i + 1, GumpButtonType.Reply, m_Types[i].m_BaseID);
                AddItem(15 + x, 40, m_Types[i].m_BaseID);
            }
        }

        public static void Initialize()
        {
            CommandSystem.Register("AddStair", AccessLevel.GameMaster, new CommandEventHandler(AddStair_OnCommand));
        }

        [Usage("AddStair")]
        [Description("Displays a menu from which you can interactively add Stairs.")]
        public static void AddStair_OnCommand( CommandEventArgs e )
        {
            e.Mobile.SendGump(new AddStairGump());
        }

        public void AddTheBackground( int width, int height )
        {
            this.AddBackground(0, 0, width - 00, height - 00, 1755);
        }

        public override void OnResponse( NetState sender, RelayInfo info )
        {
            Mobile from = sender.Mobile;
            int button = info.ButtonID - 1;

            if ( button < 0 )
                return;

            CommandSystem.Handle(from, String.Format("{0}M Add Static {1}", CommandSystem.Prefix, m_Types[button].m_BaseID));
            from.SendGump(new AddStairGump());
        }
        public class StairInfo
        {
            public int m_BaseID;
            public StairInfo( int baseID )
            {
                m_BaseID = baseID;
            }
        }
    }
}
