using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Network;

namespace Server.Gumps
{
    public class AddWallGump : Gump
    {
        public static WallInfo[] m_Types = new WallInfo[]
        {
            #region WoodWalls
            new WallInfo(10),new WallInfo(7),new WallInfo(12),new WallInfo(6),
            new WallInfo(13),new WallInfo(8),new WallInfo(11),new WallInfo(9),
            new WallInfo(14),new WallInfo(15),new WallInfo(18),new WallInfo(16),
            new WallInfo(17),new WallInfo(19),new WallInfo(22),new WallInfo(20),
            new WallInfo(21),new WallInfo(23),new WallInfo(171),new WallInfo(168),
            new WallInfo(173),new WallInfo(166),new WallInfo(172),new WallInfo(167),
            new WallInfo(170),new WallInfo(169),new WallInfo(186),new WallInfo(9472),
            new WallInfo(9479),new WallInfo(9478),new WallInfo(9473),new WallInfo(185),
            new WallInfo(178),new WallInfo(175),new WallInfo(181),new WallInfo(174),
            new WallInfo(180),new WallInfo(176),new WallInfo(179),new WallInfo(177),
            new WallInfo(188),new WallInfo(187),new WallInfo(948),new WallInfo(947),
            new WallInfo(949),new WallInfo(950),new WallInfo(191),new WallInfo(189),
            new WallInfo(190),new WallInfo(192),new WallInfo(9367),new WallInfo(9365),
            new WallInfo(9366),new WallInfo(9368),new WallInfo(9363),new WallInfo(9361),
            new WallInfo(9362),new WallInfo(9364),new WallInfo(9359),new WallInfo(9357),
            new WallInfo(9358),new WallInfo(9360),new WallInfo(11585),new WallInfo(11546),
            new WallInfo(11584),new WallInfo(11584),new WallInfo(11584),new WallInfo(11549),
            new WallInfo(11587),new WallInfo(11589),new WallInfo(11588),new WallInfo(11586),
            new WallInfo(11583),new WallInfo(11545),new WallInfo(11582),new WallInfo(11548),
            new WallInfo(11581),new WallInfo(11544),new WallInfo(11580),new WallInfo(11547),
            #endregion

            #region StoneWalls
            new WallInfo(26),new WallInfo(27),new WallInfo(28),new WallInfo(29),new WallInfo(30),
            new WallInfo(31),new WallInfo(32),new WallInfo(33),new WallInfo(34),new WallInfo(35),
            new WallInfo(36),new WallInfo(37),new WallInfo(38),new WallInfo(39),new WallInfo(199),
            new WallInfo(200),new WallInfo(201),new WallInfo(202),new WallInfo(203),new WallInfo(204),
            new WallInfo(220),new WallInfo(221),new WallInfo(222),new WallInfo(223),new WallInfo(344),
            new WallInfo(345),new WallInfo(346),new WallInfo(347),new WallInfo(348),new WallInfo(349),
            new WallInfo(350),new WallInfo(351),new WallInfo(352),new WallInfo(353),new WallInfo(354),
            new WallInfo(355),new WallInfo(356),new WallInfo(357),new WallInfo(358),new WallInfo(359),
            new WallInfo(360),new WallInfo(361),new WallInfo(362),new WallInfo(363),new WallInfo(463),
            new WallInfo(464),new WallInfo(465),new WallInfo(466),new WallInfo(467),new WallInfo(468),
            new WallInfo(488),new WallInfo(489),new WallInfo(490),new WallInfo(491),new WallInfo(511),
            new WallInfo(512),new WallInfo(513),new WallInfo(514),new WallInfo(515),new WallInfo(516),
            new WallInfo(517),new WallInfo(518),new WallInfo(519),new WallInfo(522),new WallInfo(588),
            new WallInfo(589),new WallInfo(590),new WallInfo(591),new WallInfo(592),new WallInfo(593),
            new WallInfo(594),new WallInfo(595),new WallInfo(596),new WallInfo(597),new WallInfo(598),
            new WallInfo(599),new WallInfo(600),new WallInfo(601),new WallInfo(602),new WallInfo(603),
            new WallInfo(958),new WallInfo(960),new WallInfo(961),new WallInfo(962),new WallInfo(967),
            new WallInfo(968),new WallInfo(969),new WallInfo(970),new WallInfo(971),new WallInfo(972),
            new WallInfo(973),new WallInfo(974),new WallInfo(975),new WallInfo(976),new WallInfo(977),
            new WallInfo(978),new WallInfo(979),new WallInfo(980),new WallInfo(981),new WallInfo(982),
            new WallInfo(983),new WallInfo(984),new WallInfo(990),new WallInfo(991),new WallInfo(992),
            new WallInfo(993),new WallInfo(994),new WallInfo(9519),new WallInfo(9520),new WallInfo(9526),
            new WallInfo(9527),new WallInfo(10576),new WallInfo(10577),new WallInfo(10578),new WallInfo(10579),
            new WallInfo(10580),new WallInfo(10581),new WallInfo(10582),new WallInfo(10583),new WallInfo(10584),
            new WallInfo(10585),new WallInfo(10586),new WallInfo(10587),new WallInfo(10660),new WallInfo(10661),
            new WallInfo(10662),new WallInfo(10663),new WallInfo(10664),new WallInfo(10665),new WallInfo(10666),
            new WallInfo(10667),new WallInfo(10668),new WallInfo(10669),new WallInfo(10670),new WallInfo(10671),
            new WallInfo(10672),new WallInfo(10673),new WallInfo(10674),new WallInfo(10675),new WallInfo(10676),
            new WallInfo(10677),
            #endregion

            #region MarbleWalls
            new WallInfo(248),new WallInfo(249),new WallInfo(250),new WallInfo(251),new WallInfo(252),
            new WallInfo(253),new WallInfo(254),new WallInfo(255),new WallInfo(256),new WallInfo(257),
            new WallInfo(258),new WallInfo(259),new WallInfo(260),new WallInfo(261),new WallInfo(262),
            new WallInfo(263),new WallInfo(264),new WallInfo(265),new WallInfo(266),new WallInfo(267),
            new WallInfo(268),new WallInfo(269),new WallInfo(270),new WallInfo(271),new WallInfo(272),
            new WallInfo(273),new WallInfo(279),new WallInfo(280),new WallInfo(281),new WallInfo(282),
            new WallInfo(657),new WallInfo(658),new WallInfo(659),new WallInfo(660),new WallInfo(661),
            new WallInfo(662),new WallInfo(663),new WallInfo(664),new WallInfo(665),new WallInfo(666),
            new WallInfo(667),new WallInfo(668),new WallInfo(669),new WallInfo(670),new WallInfo(671),
            new WallInfo(672),new WallInfo(673),new WallInfo(674),new WallInfo(675),new WallInfo(676),
            new WallInfo(685),new WallInfo(686),new WallInfo(693),new WallInfo(694),new WallInfo(695),
            new WallInfo(696),new WallInfo(697),new WallInfo(698),new WallInfo(699),new WallInfo(700),
            new WallInfo(1090),new WallInfo(1091),new WallInfo(1092),new WallInfo(1093),new WallInfo(1104),
            new WallInfo(1105),new WallInfo(1106),new WallInfo(1107),new WallInfo(9484),new WallInfo(9485),
            new WallInfo(9490),new WallInfo(9491),new WallInfo(9496),new WallInfo(9497),new WallInfo(9502),
            new WallInfo(9503),new WallInfo(9508),new WallInfo(9509),new WallInfo(9514),new WallInfo(9515),
            new WallInfo(9532),new WallInfo(9533),new WallInfo(9940),new WallInfo(9941),
            #endregion

            #region PlasterWalls
            new WallInfo(295),new WallInfo(296),new WallInfo(297),new WallInfo(298),new WallInfo(299),
            new WallInfo(300),new WallInfo(301),new WallInfo(302),new WallInfo(303),new WallInfo(304),
            new WallInfo(305),new WallInfo(306),new WallInfo(307),new WallInfo(308),new WallInfo(309),
            new WallInfo(310),new WallInfo(311),new WallInfo(312),new WallInfo(313),new WallInfo(314),
            new WallInfo(315),new WallInfo(332),new WallInfo(334),new WallInfo(335),new WallInfo(336),
            new WallInfo(338),new WallInfo(339),new WallInfo(340),new WallInfo(341),new WallInfo(342),
            new WallInfo(343),new WallInfo(895),new WallInfo(896),new WallInfo(897),new WallInfo(898),
            new WallInfo(899),new WallInfo(900),new WallInfo(901),new WallInfo(902),new WallInfo(903),
            new WallInfo(904),new WallInfo(905),new WallInfo(906),new WallInfo(907),new WallInfo(908),
            new WallInfo(909),new WallInfo(910),new WallInfo(911),new WallInfo(912),new WallInfo(913),
            new WallInfo(914),new WallInfo(915),new WallInfo(916),new WallInfo(9349),new WallInfo(9350),
            new WallInfo(9351),new WallInfo(9354),new WallInfo(9371),new WallInfo(9372),new WallInfo(9373),
            new WallInfo(9374),new WallInfo(9375),new WallInfo(9376),new WallInfo(9377),new WallInfo(9378),
            new WallInfo(9379),new WallInfo(9380),new WallInfo(9381),new WallInfo(9382),new WallInfo(9383),
            new WallInfo(9384),new WallInfo(9385),new WallInfo(9386),new WallInfo(10721),new WallInfo(10722),
            new WallInfo(10723),new WallInfo(10724),new WallInfo(10726),new WallInfo(10727),new WallInfo(10728),
            new WallInfo(10729),new WallInfo(10730),new WallInfo(10731),new WallInfo(10732),new WallInfo(10733),
            new WallInfo(10734),new WallInfo(10735),new WallInfo(10736),new WallInfo(10737),new WallInfo(10738),
            new WallInfo(10739),new WallInfo(10740),new WallInfo(10741),new WallInfo(10742),new WallInfo(10743),
            new WallInfo(10744),new WallInfo(10745),new WallInfo(10746),new WallInfo(10747),new WallInfo(10748),
            new WallInfo(10800),new WallInfo(10801),new WallInfo(10802),new WallInfo(10803),new WallInfo(10804),
            new WallInfo(10806),new WallInfo(10807),new WallInfo(10808),new WallInfo(10809),new WallInfo(10810),
            #endregion

            #region ElvenWalls
            new WallInfo(11130),new WallInfo(11131),new WallInfo(11132),new WallInfo(11133),new WallInfo(11134),
            new WallInfo(11135),new WallInfo(11136),new WallInfo(11181),new WallInfo(11182),new WallInfo(11183),
            new WallInfo(11184),new WallInfo(11185),new WallInfo(11186),new WallInfo(11193),new WallInfo(11194),
            new WallInfo(11195),new WallInfo(11196),new WallInfo(11197),new WallInfo(11198),new WallInfo(11199),
            new WallInfo(11200),new WallInfo(11201),new WallInfo(11202),new WallInfo(11207),new WallInfo(11208),
            new WallInfo(11209),new WallInfo(11210),new WallInfo(11211),new WallInfo(11212),new WallInfo(11503),
            new WallInfo(11504),new WallInfo(11505),new WallInfo(11506),new WallInfo(11507),new WallInfo(11508),
            new WallInfo(11509),new WallInfo(11510),new WallInfo(11511),new WallInfo(11512),new WallInfo(11715),
            new WallInfo(11716),new WallInfo(11717),new WallInfo(11718),new WallInfo(11719),new WallInfo(11720),
            new WallInfo(11727),new WallInfo(11728),new WallInfo(11729),
            #endregion

            #region OtherWalls
            new WallInfo(51),new WallInfo(52),new WallInfo(53),new WallInfo(54),new WallInfo(55),new WallInfo(56),
            new WallInfo(57),new WallInfo(58),new WallInfo(59),new WallInfo(60),new WallInfo(61),new WallInfo(62),
            new WallInfo(63),new WallInfo(64),new WallInfo(65),new WallInfo(66),new WallInfo(67),new WallInfo(68),
            new WallInfo(87),new WallInfo(88),new WallInfo(89),new WallInfo(90),new WallInfo(91),new WallInfo(92),
            new WallInfo(93),new WallInfo(94),new WallInfo(95),new WallInfo(96),new WallInfo(97),new WallInfo(98),
            new WallInfo(99),new WallInfo(100),new WallInfo(101),new WallInfo(102),new WallInfo(105),new WallInfo(106),
            new WallInfo(107),new WallInfo(108),new WallInfo(144),new WallInfo(145),new WallInfo(146),new WallInfo(147),
            new WallInfo(148),new WallInfo(149),new WallInfo(150),new WallInfo(151),new WallInfo(152),new WallInfo(153),
            new WallInfo(154),new WallInfo(155),new WallInfo(156),new WallInfo(157),new WallInfo(158),new WallInfo(159),
            new WallInfo(160),new WallInfo(161),new WallInfo(419),new WallInfo(421),new WallInfo(422),new WallInfo(423),
            new WallInfo(424),new WallInfo(425),new WallInfo(426),new WallInfo(427),new WallInfo(428),new WallInfo(429),
            new WallInfo(430),new WallInfo(431),new WallInfo(432),new WallInfo(438),new WallInfo(439),new WallInfo(440),
            new WallInfo(441),new WallInfo(444),new WallInfo(445),new WallInfo(446),new WallInfo(447),new WallInfo(448),
            new WallInfo(449),new WallInfo(450),new WallInfo(451),new WallInfo(452),new WallInfo(453),new WallInfo(454),
            new WallInfo(455),new WallInfo(545),new WallInfo(546),new WallInfo(547),new WallInfo(550),new WallInfo(551),
            new WallInfo(552),new WallInfo(553),new WallInfo(1057),new WallInfo(1058),new WallInfo(1059),new WallInfo(1060),
            new WallInfo(1061),new WallInfo(1072),new WallInfo(8538),new WallInfo(8539),new WallInfo(8540),new WallInfo(9343),
            new WallInfo(9344),new WallInfo(9345),new WallInfo(9346),new WallInfo(9347),new WallInfo(9348),new WallInfo(9352),
            new WallInfo(9353),new WallInfo(9460),new WallInfo(9461),new WallInfo(9466),new WallInfo(9467),new WallInfo(10003),
            new WallInfo(10004),new WallInfo(10005),new WallInfo(10010),new WallInfo(10011),new WallInfo(10012),new WallInfo(10013),
            new WallInfo(10014),new WallInfo(10015),new WallInfo(10067),new WallInfo(10068),new WallInfo(10069),new WallInfo(10074),
            new WallInfo(10075),new WallInfo(10076),new WallInfo(10077),new WallInfo(10078),new WallInfo(10079),new WallInfo(10080),
            new WallInfo(10081),new WallInfo(10082),new WallInfo(10552),new WallInfo(10553),new WallInfo(10554),new WallInfo(10555),
            new WallInfo(10556),new WallInfo(10557),new WallInfo(10558),new WallInfo(10559),new WallInfo(10560),new WallInfo(10561),
            new WallInfo(10562),new WallInfo(10563),new WallInfo(10564),new WallInfo(10565),new WallInfo(10566),new WallInfo(10567),
            new WallInfo(10568),new WallInfo(10569),new WallInfo(10570),new WallInfo(10571),new WallInfo(10572),new WallInfo(10573),
            new WallInfo(10574),new WallInfo(10575),new WallInfo(10642),new WallInfo(10643),new WallInfo(10644),new WallInfo(10645),
            new WallInfo(10646),new WallInfo(10647),new WallInfo(10648),new WallInfo(10649),new WallInfo(10650),new WallInfo(10651),
            new WallInfo(10652),new WallInfo(10653),new WallInfo(10656),new WallInfo(10657),new WallInfo(10658),new WallInfo(10659),
            new WallInfo(10678),new WallInfo(10679),new WallInfo(10680),new WallInfo(10681),new WallInfo(10682),new WallInfo(10683),
            new WallInfo(10684),new WallInfo(10685),new WallInfo(10686),new WallInfo(10687),new WallInfo(13783),new WallInfo(13784),
            new WallInfo(13785),new WallInfo(13786),new WallInfo(13787),new WallInfo(13788),new WallInfo(13789),new WallInfo(13790),
            new WallInfo(13791),new WallInfo(13792),new WallInfo(13793),new WallInfo(13794),new WallInfo(13795),new WallInfo(13796),
            new WallInfo(13797),new WallInfo(13798),new WallInfo(13843),new WallInfo(13844),new WallInfo(13845),new WallInfo(13846),
            new WallInfo(13847),new WallInfo(13848),new WallInfo(13849),new WallInfo(13878),new WallInfo(13879),new WallInfo(13880),
            new WallInfo(13881),new WallInfo(13882),new WallInfo(13883),new WallInfo(13884),new WallInfo(13885),new WallInfo(13895),
            new WallInfo(13896),new WallInfo(13897),new WallInfo(13898)
            #endregion
        };


        private readonly int m_Type;

        public AddWallGump() : this(-1)
        {
        }
        public AddWallGump( int type ) : base( 0, 0 )
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
                AddButton(25 + x, 20, 2117, 2118, i + 1, GumpButtonType.Reply, m_Types[i].m_BaseID);
                AddItem(15 + x, 40, m_Types[i].m_BaseID);
            }
        }

        public static void Initialize()
        {
            CommandSystem.Register("AddWall", AccessLevel.GameMaster, new CommandEventHandler(AddWall_OnCommand));
        }

        [Usage("AddWall")]
        [Description("Displays a menu from which you can interactively add Walls.")]
        public static void AddWall_OnCommand( CommandEventArgs e )
        {
            e.Mobile.SendGump(new AddWallGump());
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

            CommandSystem.Handle(from, String.Format("{0}Tile Static {1}", CommandSystem.Prefix, m_Types[button].m_BaseID));
            from.SendGump(new AddWallGump());
        }
        public class WallInfo
        {
            public int m_BaseID;
            public WallInfo( int baseID )
            {
                m_BaseID = baseID;
            }
        }
    }
}
