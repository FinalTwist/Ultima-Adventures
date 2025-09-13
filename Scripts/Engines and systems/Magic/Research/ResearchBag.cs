using System;
using Server;
using Server.Network;
using Server.Multis;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Accounting;
using System.Collections;
using System.Collections.Generic;
using Server.Commands;

namespace Server.Items
{
	[Flipable(0x4C53, 0x4C54)]
	public class ResearchBag : Item
	{
		[Constructable]
		public ResearchBag() : base( 0x4C53 )
		{
			Name = "research pack";
			Weight = 1.0;
			Hue = 0xABE;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( BagOwner != null ){ list.Add( 1049644, "Belongs to " + BagOwner.Name + "" ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "This must be in your pack to do any research." );
			}
			else if ( from.Skills[SkillName.Inscribe].Value < 30 )
			{
				from.SendMessage( "You cannot understand anything that is in this bag." );
			}
			else if ( BagOwner != from )
			{
				from.SendMessage( "This bag doesn't belong to you so you throw it out." );
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

						if ( m == BagOwner )
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
			else
			{
				BagPage = 1;
				from.PlaySound( 0x48 );
				from.CloseGump( typeof( ResearchGump ) );
				from.SendGump( new ResearchGump( this ) );
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is BlankScroll )
			{
				if ( BagScrolls > 500 )
				{
					from.SendMessage( "This pack can only hold 500 blank scrolls." );
				}
				else
				{
					from.PlaySound( 0x48 );
					int need = 500 - BagScrolls;
					int have = dropped.Amount;

					if ( need >= have ){ BagScrolls = BagScrolls + have; dropped.Delete(); }
					else { BagScrolls = 500; dropped.Amount = dropped.Amount - need; }

					from.SendMessage( "The blank scrolls have been added to your pack." );

					if( from.HasGump( typeof(ResearchGump)) )
					{
						from.CloseGump( typeof(ResearchGump) );
						from.SendGump( new ResearchGump( this ) );
					}
				}
			}
			else if ( dropped is ScribesPen )
			{
				BaseTool tool = (BaseTool)dropped;

				if ( BagQuills > 500 )
				{
					from.SendMessage( "This pack can only hold 500 quills." );
				}
				else
				{
					from.PlaySound( 0x48 );
					BagQuills = BagQuills + tool.UsesRemaining;
					dropped.Delete();
					if ( BagQuills > 500 ){ BagQuills = 500; }

					from.SendMessage( "The quills have been added to your pack." );

					if( from.HasGump( typeof(ResearchGump)) )
					{
						from.CloseGump( typeof(ResearchGump) );
						from.SendGump( new ResearchGump( this ) );
					}
				}
			}

			return false;
		}

        public static void InvokeCommand( string c, Mobile from )
        {
            CommandSystem.Handle(from, String.Format("{0}{1}", CommandSystem.Prefix, c));
        }

		public class ResearchGump : Gump
		{
			private ResearchBag m_Bag;
			private int m_Page;

			public ResearchGump( ResearchBag bag ) : base( 25, 25 )
			{
				m_Bag = bag;
				m_Page = bag.BagPage;
					if ( !Research.GetRunes( bag, 10 ) && m_Page != 12 && m_Page != 2 ){ m_Page = bag.BagPage = 2; }

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
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(0, 415, 155);
				AddImage(300, 415, 155);
				AddImage(600, 415, 155);
				AddImage(2, 298, 129);
				AddImage(299, 298, 129);
				AddImage(598, 298, 129);
				AddImage(2, 413, 129);
				AddImage(301, 413, 129);
				AddImage(598, 413, 129);
				AddImage(8, 7, 133);
				AddImage(236, 46, 132);
				AddImage(498, 46, 132);
				AddImage(678, 7, 134);
				AddImage(7, 470, 142);
				AddImage(330, 679, 140);
				AddImage(579, 679, 140);
				AddImage(857, 680, 143);

				AddItem(5, 374, 3636);
				AddItem(18, 404, 8273);
				AddItem(14, 446, 10282);
				AddHtml( 48, 375, 42, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + (bag.BagScrolls).ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 48, 412, 42, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + (bag.BagQuills).ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 48, 449, 42, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + (bag.BagInk).ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( ( m_Page < 10 || m_Page > 20 ) && Research.GetRunes( bag, 10 ) ) /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					int button_top_menu = 3609; if ( m_Page == 1 ){ button_top_menu = 4011; }
					AddButton(180, 75, button_top_menu, button_top_menu, 1, GumpButtonType.Reply, 0);
					AddItem(216, 78, 10174);
					button_top_menu = 3609; if ( m_Page == 2 ){ button_top_menu = 4011; }
					AddButton(280, 75, button_top_menu, button_top_menu, 2, GumpButtonType.Reply, 0);
					AddItem(316, 74, 19516);
					button_top_menu = 3609; if ( m_Page == 3 || ( m_Page > 20 && m_Page < 30 ) ){ button_top_menu = 4011; }
					AddButton(380, 75, button_top_menu, button_top_menu, 3, GumpButtonType.Reply, 0);
					AddItem(405, 74, 3834);
					button_top_menu = 3609; if ( m_Page == 4 ){ button_top_menu = 4011; }
					AddButton(480, 75, button_top_menu, button_top_menu, 4, GumpButtonType.Reply, 0);
					AddItem(505, 74, 8787);
					button_top_menu = 3609; if ( m_Page == 5 || ( m_Page > 30 && m_Page < 40 ) ){ button_top_menu = 4011; }
					AddButton(580, 75, button_top_menu, button_top_menu, 5, GumpButtonType.Reply, 0);
					AddItem(603, 72, 17087);
					if ( Research.GetResearch( bag, 1 ) )
					{
						button_top_menu = 3609; if ( m_Page == 6 || ( m_Page > 40 && m_Page < 50 ) ){ button_top_menu = 4011; }
						AddButton(680, 75, button_top_menu, button_top_menu, 6, GumpButtonType.Reply, 0);
						AddItem(718, 72, 19541);
					}
				}

				AddHtml( 10, 14, 870, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>SPELL RESEARCH</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( m_Page == 1 ) // MAIN SCREEN /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>MAIN PACK</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(828, 75, 3610, 3610, 11, GumpButtonType.Reply, 0); // HELP BUTTON

					AddItem(104, 160, 3636);
					AddHtml( 145, 163, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Scrolls</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 239, 163, 53, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + (bag.BagScrolls).ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddItem(417, 148, 8273);
					AddHtml( 444, 163, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Quills</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 538, 163, 53, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + (bag.BagQuills).ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddItem(698, 159, 10282);
					AddHtml( 727, 163, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ink</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 821, 163, 53, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + (bag.BagInk).ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					string msg = "Your pack is fully stocked with octopus ink.";
					if ( bag.BagInk < 500 ){ msg = "More octopus ink is rumored to be at " + bag.BagInkLocation + " in " + bag.BagInkWorld + "."; }

					AddHtml( 100, 207, 778, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + msg + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddImage(126, 253, 130);
					AddImage(394, 253, 130);
					AddImage(541, 253, 130);
					AddImage(96, 248, 159);
					AddImage(843, 248, 143);

					if ( Research.GetResearch( bag, 1 ) )
					{
						AddButton(121, 304, 4005, 4005, 1201, GumpButtonType.Reply, 0);
						AddHtml( 161, 304, 204, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Research Spell Bar I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(436, 304, 4005, 4005, 1211, GumpButtonType.Reply, 0);
						AddHtml( 478, 304, 147, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(693, 304, 4017, 4017, 1221, GumpButtonType.Reply, 0);
						AddHtml( 734, 304, 147, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

						AddButton(121, 344, 4005, 4005, 1202, GumpButtonType.Reply, 0);
						AddHtml( 161, 344, 204, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Research Spell Bar II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(436, 344, 4005, 4005, 1212, GumpButtonType.Reply, 0);
						AddHtml( 478, 344, 147, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(693, 344, 4017, 4017, 1222, GumpButtonType.Reply, 0);
						AddHtml( 734, 344, 147, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

						AddButton(121, 384, 4005, 4005, 1203, GumpButtonType.Reply, 0);
						AddHtml( 161, 384, 204, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Research Spell Bar III</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(436, 384, 4005, 4005, 1213, GumpButtonType.Reply, 0);
						AddHtml( 478, 384, 147, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(693, 384, 4017, 4017, 1223, GumpButtonType.Reply, 0);
						AddHtml( 734, 384, 147, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

						AddButton(121, 424, 4005, 4005, 1204, GumpButtonType.Reply, 0);
						AddHtml( 161, 424, 204, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Research Spell Bar IV</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(436, 424, 4005, 4005, 1214, GumpButtonType.Reply, 0);
						AddHtml( 478, 424, 147, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(693, 424, 4017, 4017, 1224, GumpButtonType.Reply, 0);
						AddHtml( 734, 424, 147, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}

					AddImage(590, 587, 11317);
					AddImage(421, 589, 11323);
					AddImage(835, 498, 11326);
					AddImage(786, 594, 11329);
					AddImage(747, 521, 11336);
					AddImage(86, 518, 11338);
					AddImage(256, 562, 11318);
				}
				if ( m_Page == 2 ) // RUNES FOUND /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					int icon = 0;
					int next = 1;
					string Rune = "";
					string rune = "";
					string missing = "";
					int r = 0;

					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(145, 146, icon);
					AddHtml( 192, 156, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(330, 146, icon);
					AddHtml( 377, 156, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(515, 146, icon);
					AddHtml( 562, 156, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(700, 146, icon);
					AddHtml( 747, 156, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(145, 226, icon);
					AddHtml( 192, 236, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(330, 226, icon);
					AddHtml( 377, 236, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(515, 226, icon);
					AddHtml( 562, 236, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(700, 226, icon);
					AddHtml( 747, 236, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(145, 306, icon);
					AddHtml( 192, 316, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(330, 306, icon);
					AddHtml( 377, 316, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
/*					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(515, 306, icon);
					AddHtml( 562, 316, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(700, 306, icon);
					AddHtml( 747, 316, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(145, 386, icon);
					AddHtml( 192, 396, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(330, 386, icon);
					AddHtml( 377, 396, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(515, 386, icon);
					AddHtml( 562, 396, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(700, 386, icon);
					AddHtml( 747, 396, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(145, 466, icon);
					AddHtml( 192, 476, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(330, 466, icon);
					AddHtml( 377, 476, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(515, 466, icon);
					AddHtml( 562, 476, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(700, 466, icon);
					AddHtml( 747, 476, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(145, 546, icon);
					AddHtml( 192, 556, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(330, 546, icon);
					AddHtml( 377, 556, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(515, 546, icon);
					AddHtml( 562, 556, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(700, 546, icon);
					AddHtml( 747, 556, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(330, 626, icon);
					AddHtml( 377, 636, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					r++;	Rune = Research.RuneName( r, 1 );	rune = Research.RuneName( r, 0 );	icon = 11277; if ( Research.GetRunes( bag, r ) ){ icon = Research.RuneIndex( rune, 1 ); } else if ( next > 0 ){ icon = 11278; next = 0; missing = Rune; }
					AddImage(515, 626, icon);
					AddHtml( 562, 636, 50, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Rune + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
*/
					string msg = "You have found all of the Cubes of Power.";
					if ( missing != "" ){ msg = "The Cube of " + missing + " is said to be in " + bag.RuneLocation + " in " + bag.RuneWorld + "."; }
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + msg + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(828, 75, 3610, 3610, 12, GumpButtonType.Reply, 0); // HELP BUTTON
				}
				if ( m_Page == 3 || ( m_Page > 20 && m_Page < 30 ) ) // MAGERY RESEARCH
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>MAGERY RESEARCH</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					int mcircleIcon = 3609; if ( m_Page == 21 || m_Page == 3 ){ mcircleIcon = 4017; }
					AddButton(105, 150, mcircleIcon, mcircleIcon, 21, GumpButtonType.Reply, 0);
					AddHtml( 145, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>1st Circle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					if ( Research.GetWizardry( bag, 8 ) )
					{
						mcircleIcon = 3609; if ( m_Page == 22 ){ mcircleIcon = 4017; }
						AddButton(325, 150, mcircleIcon, mcircleIcon, 22, GumpButtonType.Reply, 0);
						AddHtml( 365, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>2nd Circle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}
					if ( Research.GetWizardry( bag, 16 ) )
					{
						mcircleIcon = 3609; if ( m_Page == 23 ){ mcircleIcon = 4017; }
						AddButton(545, 150, mcircleIcon, mcircleIcon, 23, GumpButtonType.Reply, 0);
						AddHtml( 585, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>3rd Circle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}
					if ( Research.GetWizardry( bag, 24 ) )
					{
						mcircleIcon = 3609; if ( m_Page == 24 ){ mcircleIcon = 4017; }
						AddButton(765, 150, mcircleIcon, mcircleIcon, 24, GumpButtonType.Reply, 0);
						AddHtml( 805, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>4th Circle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}
					if ( Research.GetWizardry( bag, 32 ) )
					{
						mcircleIcon = 3609; if ( m_Page == 25 ){ mcircleIcon = 4017; }
						AddButton(105, 190, mcircleIcon, mcircleIcon, 25, GumpButtonType.Reply, 0);
						AddHtml( 145, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>5th Circle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}
					if ( Research.GetWizardry( bag, 40 ) )
					{
						mcircleIcon = 3609; if ( m_Page == 26 ){ mcircleIcon = 4017; }
						AddButton(325, 190, mcircleIcon, mcircleIcon, 26, GumpButtonType.Reply, 0);
						AddHtml( 365, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>6th Circle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}
					if ( Research.GetWizardry( bag, 48 ) )
					{
						mcircleIcon = 3609; if ( m_Page == 27 ){ mcircleIcon = 4017; }
						AddButton(545, 190, mcircleIcon, mcircleIcon, 27, GumpButtonType.Reply, 0);
						AddHtml( 585, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>7th Circle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}
					if ( Research.GetWizardry( bag, 56 ) )
					{
						mcircleIcon = 3609; if ( m_Page == 28 ){ mcircleIcon = 4017; }
						AddButton(765, 190, mcircleIcon, mcircleIcon, 28, GumpButtonType.Reply, 0);
						AddHtml( 805, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>8th Circle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}

					AddImage(134, 241, 130);
					AddImage(387, 241, 130);
					AddImage(548, 241, 130);
					AddImage(850, 235, 143);
					AddImage(103, 236, 159);

					int CandleIcon = 11322;

					string spellCircle = "first";
					int spellName = 1;
						if ( m_Page == 22 ){ spellName = 9; spellCircle = "second"; }
						else if ( m_Page == 23 ){ spellName = 17; spellCircle = "third"; }
						else if ( m_Page == 24 ){ spellName = 25; spellCircle = "fourth"; }
						else if ( m_Page == 25 ){ spellName = 33; spellCircle = "fifth"; }
						else if ( m_Page == 26 ){ spellName = 41; spellCircle = "sixth"; }
						else if ( m_Page == 27 ){ spellName = 49; spellCircle = "seventh"; }
						else if ( m_Page == 28 ){ spellName = 57; spellCircle = "eighth"; }

					if ( Research.GetWizardry( bag, spellName ) )
					{
						AddButton(115, 340, 4005, 4005, 400+spellName, GumpButtonType.Reply, 0);
						AddHtml( 195, 340, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(155, 340, 4011, 4011, 300+spellName, GumpButtonType.Reply, 0);
					} spellName++;
					if ( Research.GetWizardry( bag, spellName ) )
					{
						AddButton(115, 390, 4005, 4005, 400+spellName, GumpButtonType.Reply, 0);
						AddHtml( 195, 390, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(155, 390, 4011, 4011, 300+spellName, GumpButtonType.Reply, 0);
					} spellName++;
					if ( Research.GetWizardry( bag, spellName ) )
					{
						AddButton(115, 440, 4005, 4005, 400+spellName, GumpButtonType.Reply, 0);
						AddHtml( 195, 440, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(155, 440, 4011, 4011, 300+spellName, GumpButtonType.Reply, 0);
					} spellName++;
					if ( Research.GetWizardry( bag, spellName ) )
					{
						AddButton(115, 490, 4005, 4005, 400+spellName, GumpButtonType.Reply, 0);
						AddHtml( 195, 490, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(155, 490, 4011, 4011, 300+spellName, GumpButtonType.Reply, 0);
					} spellName++;
					if ( Research.GetWizardry( bag, spellName ) )
					{
						AddButton(670, 340, 4005, 4005, 400+spellName, GumpButtonType.Reply, 0);
						AddHtml( 750, 340, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(710, 340, 4011, 4011, 300+spellName, GumpButtonType.Reply, 0);
					} spellName++;
					if ( Research.GetWizardry( bag, spellName ) )
					{
						AddButton(670, 390, 4005, 4005, 400+spellName, GumpButtonType.Reply, 0);
						AddHtml( 750, 390, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(710, 390, 4011, 4011, 300+spellName, GumpButtonType.Reply, 0);
					} spellName++;
					if ( Research.GetWizardry( bag, spellName ) )
					{
						AddButton(670, 440, 4005, 4005, 400+spellName, GumpButtonType.Reply, 0);
						AddHtml( 750, 440, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(710, 440, 4011, 4011, 300+spellName, GumpButtonType.Reply, 0);
					} spellName++;
					if ( Research.GetWizardry( bag, spellName ) )
					{
						CandleIcon = 11323;
						AddButton(670, 490, 4005, 4005, 400+spellName, GumpButtonType.Reply, 0);
						AddHtml( 750, 490, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(710, 490, 4011, 4011, 300+spellName, GumpButtonType.Reply, 0);
					} spellName++;

					AddImage(478, 388, 11317);
					AddImage(116, 522, CandleIcon);
					AddImage(769, 510, 11327);
					AddImage(432, 351, 11326);
					AddImage(731, 594, CandleIcon);
					AddImage(263, 535, 11331);
					AddImage(519, 579, 11333);
					AddImage(378, 576, 11334);
					AddImage(633, 527, 11325);

					string msg = "You have researched all of the " + spellCircle + " circle magery spells.";
					string msgColor = "FFA200";
					string nextSpell = Research.NextWizardry( bag );

					if ( bag.BagMessage > 0 )
					{
						msgColor = "15E650"; if ( bag.BagMessage == 1 ){ msgColor = "F25A5A"; }
						msg = bag.BagMsgString;	
					}
					else if ( nextSpell != "" )
					{
						msg = "To learn the " + nextSpell + " spell you need to find " + bag.SpellsMageItem + " at " + bag.SpellsMageLocation + " in " + bag.SpellsMageWorld + ".";
					}

					AddHtml( 111, 278, 760, 45, @"<BODY><BASEFONT Color=#" + msgColor + "><BIG>" + msg + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(828, 75, 3610, 3610, 13, GumpButtonType.Reply, 0); // HELP BUTTON
				}
				if ( m_Page == 4 ) // NECROMANCY RESEARCH /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>NECROMANCY RESEARCH</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					int CandleIcon = 11322;
					int spellName = 1;

					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 235, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(155, 235, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 235, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 265, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(155, 265, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 265, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 295, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(155, 295, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 295, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 325, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(154, 325, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 325, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 355, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(155, 355, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 355, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 385, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(155, 385, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 385, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 415, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(155, 415, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 415, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 445, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(155, 445, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 445, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(115, 475, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(155, 475, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 195, 475, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(620, 235, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(660, 235, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 700, 235, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(620, 265, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(660, 265, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 700, 265, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(620, 295, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(660, 295, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 700, 295, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(620, 325, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(660, 325, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 700, 325, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(620, 355, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(660, 355, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 700, 355, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(620, 385, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(660, 385, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 700, 385, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						AddButton(620, 415, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(660, 415, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 700, 415, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;
					if ( Research.GetNecromancy( bag, spellName ) )
					{
						CandleIcon = 11323;
						AddButton(620, 445, 4005, 4005, 400+spellName+64, GumpButtonType.Reply, 0);
						AddButton(660, 445, 4011, 4011, 300+spellName+64, GumpButtonType.Reply, 0);
						AddHtml( 700, 445, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spellName+64, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName++;

					string msg = "You have researched all of the necromancy spells.";
					string msgColor = "FFA200";
					string nextSpell = Research.NextNecromancy( bag );

					if ( bag.BagMessage > 0 )
					{
						msgColor = "15E650"; if ( bag.BagMessage == 1 ){ msgColor = "F25A5A"; }
						msg = bag.BagMsgString;	
					}
					else if ( nextSpell != "" )
					{
						msg = "To learn the " + nextSpell + " spell you need to find " + bag.SpellsNecroItem + " at " + bag.SpellsNecroLocation + " in " + bag.SpellsNecroWorld + ".";
					}

					AddHtml( 113, 160, 760, 45, @"<BODY><BASEFONT Color=#" + msgColor + "><BIG>" + msg + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddImage(454, 254, 11319);
					AddImage(85, 537, CandleIcon);
					AddImage(229, 539, 11328);
					AddImage(469, 381, 11326);
					AddImage(684, 594, CandleIcon);
					AddImage(780, 537, 11337);
					AddImage(457, 537, 11336);
					AddImage(567, 571, 11339);
					AddImage(342, 585, 11324);

					AddButton(828, 75, 3610, 3610, 14, GumpButtonType.Reply, 0); // HELP BUTTON
				}
				if ( m_Page == 5 || ( m_Page > 30 && m_Page < 40 ) ) // ANCIENT RESEARCH //////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>ANCIENT SPELL RESEARCH</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					int mcircleIcon = 3609; if ( m_Page == 31 || m_Page == 5 ){ mcircleIcon = 4017; }
					AddButton(105, 150, mcircleIcon, mcircleIcon, 31, GumpButtonType.Reply, 0);
					AddHtml( 185, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Conjuration</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(144, 143, Int32.Parse( Research.SpellInformation( 1, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 32 ){ mcircleIcon = 4017; }
					AddButton(325-27, 150, mcircleIcon, mcircleIcon, 32, GumpButtonType.Reply, 0);
					AddHtml( 405-27, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Death</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(364-27, 143, Int32.Parse( Research.SpellInformation( 2, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 33 ){ mcircleIcon = 4017; }
					AddButton(545-39, 150, mcircleIcon, mcircleIcon, 33, GumpButtonType.Reply, 0);
					AddHtml( 625-39, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Enchanting</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(584-39, 143, Int32.Parse( Research.SpellInformation( 3, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 34 ){ mcircleIcon = 4017; }
					AddButton(765-61, 150, mcircleIcon, mcircleIcon, 34, GumpButtonType.Reply, 0);
					AddHtml( 845-61, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sorcery</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(804-61, 143, Int32.Parse( Research.SpellInformation( 4, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 35 ){ mcircleIcon = 4017; }
					AddButton(105, 190, mcircleIcon, mcircleIcon, 35, GumpButtonType.Reply, 0);
					AddHtml( 185, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Summoning</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(144, 183, Int32.Parse( Research.SpellInformation( 5, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 36 ){ mcircleIcon = 4017; }
					AddButton(325-27, 190, mcircleIcon, mcircleIcon, 36, GumpButtonType.Reply, 0);
					AddHtml( 405-27, 190, 95, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Thaumaturgy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(364-27, 183, Int32.Parse( Research.SpellInformation( 6, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 37 ){ mcircleIcon = 4017; }
					AddButton(545-39, 190, mcircleIcon, mcircleIcon, 37, GumpButtonType.Reply, 0);
					AddHtml( 625-39, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Theurgy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(584-39, 183, Int32.Parse( Research.SpellInformation( 7, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 38 ){ mcircleIcon = 4017; }
					AddButton(765-61, 190, mcircleIcon, mcircleIcon, 38, GumpButtonType.Reply, 0);
					AddHtml( 845-61, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wizardry</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(804-61, 183, Int32.Parse( Research.SpellInformation( 8, 10 ) ) );

					AddImage(134, 241, 130);
					AddImage(387, 241, 130);
					AddImage(548, 241, 130);
					AddImage(850, 235, 143);
					AddImage(103, 236, 159);

					int CandleIcon = 11322;

					string spellCircle = "conjuration";
					int spellName = 1;
						if ( m_Page == 32 ){ spellName = 2; spellCircle = "death"; }
						else if ( m_Page == 33 ){ spellName = 3; spellCircle = "enchanting"; }
						else if ( m_Page == 34 ){ spellName = 4; spellCircle = "sorcery"; }
						else if ( m_Page == 35 ){ spellName = 5; spellCircle = "summoning"; }
						else if ( m_Page == 36 ){ spellName = 6; spellCircle = "thaumaturgy"; }
						else if ( m_Page == 37 ){ spellName = 7; spellCircle = "theurgy"; }
						else if ( m_Page == 38 ){ spellName = 8; spellCircle = "wizardry"; }

					int BookIcon = Int32.Parse( Research.SpellInformation( spellName, 9 ) );

					if ( Research.GetResearch( bag, spellName ) )
					{
						AddButton(115, 340, 4005, 4005, 600+spellName, GumpButtonType.Reply, 0);
						AddHtml( 195, 340, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(155, 340, 4011, 4011, 500+spellName, GumpButtonType.Reply, 0);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						AddButton(115, 390, 4005, 4005, 600+spellName, GumpButtonType.Reply, 0);
						AddHtml( 195, 390, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(155, 390, 4011, 4011, 500+spellName, GumpButtonType.Reply, 0);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						AddButton(115, 440, 4005, 4005, 600+spellName, GumpButtonType.Reply, 0);
						AddHtml( 195, 440, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(155, 440, 4011, 4011, 500+spellName, GumpButtonType.Reply, 0);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						AddButton(115, 490, 4005, 4005, 600+spellName, GumpButtonType.Reply, 0);
						AddHtml( 195, 490, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(155, 490, 4011, 4011, 500+spellName, GumpButtonType.Reply, 0);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						AddButton(670, 340, 4005, 4005, 600+spellName, GumpButtonType.Reply, 0);
						AddHtml( 750, 340, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(710, 340, 4011, 4011, 500+spellName, GumpButtonType.Reply, 0);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						AddButton(670, 390, 4005, 4005, 600+spellName, GumpButtonType.Reply, 0);
						AddHtml( 750, 390, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(710, 390, 4011, 4011, 500+spellName, GumpButtonType.Reply, 0);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						AddButton(670, 440, 4005, 4005, 600+spellName, GumpButtonType.Reply, 0);
						AddHtml( 750, 440, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(710, 440, 4011, 4011, 500+spellName, GumpButtonType.Reply, 0);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						CandleIcon = 11323;
						AddButton(670, 490, 4005, 4005, 600+spellName, GumpButtonType.Reply, 0);
						AddHtml( 750, 490, 176, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(710, 490, 4011, 4011, 500+spellName, GumpButtonType.Reply, 0);
					} spellName+=8;

					AddImage(399, 391, BookIcon);
					AddImage(475, 379, 11326);
					AddImage(100, 540, 11328);
					AddImage(780, 593, 11329);
					AddImage(676, 589, CandleIcon);
					AddImage(251, 547, CandleIcon);
					AddImage(355, 592, 11324);
					AddImage(580, 581, 11333);
					AddImage(480, 584, 11331);

					string msg = "You have researched all of the ancient " + spellCircle + " magic.";
					string msgColor = "FFA200";
					string nextSpell = Research.NextResearch( bag );

					if ( bag.BagMessage > 0 )
					{
						msgColor = "15E650"; if ( bag.BagMessage == 1 ){ msgColor = "F25A5A"; }
						msg = bag.BagMsgString;	
					}
					else if ( nextSpell != "" )
					{
						msg = "To learn the magic of " + nextSpell + " you need to find " + bag.ResearchItem + " at " + bag.ResearchLocation + " in " + bag.ResearchWorld + ".";
					}

					AddHtml( 111, 278, 760, 45, @"<BODY><BASEFONT Color=#" + msgColor + "><BIG>" + msg + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(828, 75, 3610, 3610, 15, GumpButtonType.Reply, 0); // HELP BUTTON
				}
				if ( m_Page == 6 || ( m_Page > 40 && m_Page < 50 ) ) // ANCIENT PREPARED SPELLS ///////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>PREPARED ANCIENT SPELLS</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					int mcircleIcon = 3609; if ( m_Page == 41 || m_Page == 6 ){ mcircleIcon = 4017; }
					AddButton(105, 150, mcircleIcon, mcircleIcon, 41, GumpButtonType.Reply, 0);
					AddHtml( 185, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Conjuration</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(144, 143, Int32.Parse( Research.SpellInformation( 1, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 42 ){ mcircleIcon = 4017; }
					AddButton(325-27, 150, mcircleIcon, mcircleIcon, 42, GumpButtonType.Reply, 0);
					AddHtml( 405-27, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Death</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(364-27, 143, Int32.Parse( Research.SpellInformation( 2, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 43 ){ mcircleIcon = 4017; }
					AddButton(545-39, 150, mcircleIcon, mcircleIcon, 43, GumpButtonType.Reply, 0);
					AddHtml( 625-39, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Enchanting</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(584-39, 143, Int32.Parse( Research.SpellInformation( 3, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 44 ){ mcircleIcon = 4017; }
					AddButton(765-61, 150, mcircleIcon, mcircleIcon, 44, GumpButtonType.Reply, 0);
					AddHtml( 845-61, 150, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sorcery</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(804-61, 143, Int32.Parse( Research.SpellInformation( 4, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 45 ){ mcircleIcon = 4017; }
					AddButton(105, 190, mcircleIcon, mcircleIcon, 45, GumpButtonType.Reply, 0);
					AddHtml( 185, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Summoning</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(144, 183, Int32.Parse( Research.SpellInformation( 5, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 46 ){ mcircleIcon = 4017; }
					AddButton(325-27, 190, mcircleIcon, mcircleIcon, 46, GumpButtonType.Reply, 0);
					AddHtml( 405-27, 190, 95, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Thaumaturgy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(364-27, 183, Int32.Parse( Research.SpellInformation( 6, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 47 ){ mcircleIcon = 4017; }
					AddButton(545-39, 190, mcircleIcon, mcircleIcon, 47, GumpButtonType.Reply, 0);
					AddHtml( 625-39, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Theurgy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(584-39, 183, Int32.Parse( Research.SpellInformation( 7, 10 ) ) );

					mcircleIcon = 3609; if ( m_Page == 48 ){ mcircleIcon = 4017; }
					AddButton(765-61, 190, mcircleIcon, mcircleIcon, 48, GumpButtonType.Reply, 0);
					AddHtml( 845-61, 190, 85, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wizardry</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddImage(804-61, 183, Int32.Parse( Research.SpellInformation( 8, 10 ) ) );

					if ( bag.BagMessage > 0 )
					{
						string msgColor = "15E650"; if ( bag.BagMessage == 1 ){ msgColor = "F25A5A"; }
						string msg = bag.BagMsgString;	
						AddHtml( 103, 236, 760, 45, @"<BODY><BASEFONT Color=#" + msgColor + "><BIG>" + msg + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}
					else
					{
						AddImage(134, 241, 130);
						AddImage(387, 241, 130);
						AddImage(548, 241, 130);
						AddImage(103, 236, 159);
						AddImage(850, 236, 143);
					}

					int CandleIcon = 11322;

					int spellName = 1;
						if ( m_Page == 42 ){ spellName = 2; }
						else if ( m_Page == 43 ){ spellName = 3; }
						else if ( m_Page == 44 ){ spellName = 4; }
						else if ( m_Page == 45 ){ spellName = 5; }
						else if ( m_Page == 46 ){ spellName = 6; }
						else if ( m_Page == 47 ){ spellName = 7; }
						else if ( m_Page == 48 ){ spellName = 8; }

					int BookIcon = Int32.Parse( Research.SpellInformation( spellName, 9 ) );
					int SmallIcon = Int32.Parse( Research.SpellInformation( spellName, 10 ) );
					int IsPrepared = 0;
					string PrepareColor = "#F25A5A";

					if ( Research.GetResearch( bag, spellName ) )
					{
						PrepareColor = "#FBFBFB"; IsPrepared = Research.GetPrepared( bag, spellName ); if ( IsPrepared > 10 ){ PrepareColor = "#15E650"; } else if (IsPrepared > 0 ){ PrepareColor = "#F25A5A"; }
						AddImage(120, 305, Int32.Parse( Research.SpellInformation( spellName, 11 ) ) );
						AddButton(170, 315, 4011, 4011, 700+spellName, GumpButtonType.Reply, 0);
						AddButton(210, 315, 4014, 4014, 800+spellName, GumpButtonType.Reply, 0);
						AddHtml( 120, 275, 250, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 250, 315, 60, 20, @"<BODY><BASEFONT Color=" + PrepareColor + "><BIG>" + IsPrepared + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						PrepareColor = "#FBFBFB"; IsPrepared = Research.GetPrepared( bag, spellName ); if ( IsPrepared > 10 ){ PrepareColor = "#15E650"; } else if (IsPrepared > 0 ){ PrepareColor = "#F25A5A"; }
						AddImage(120, 395, Int32.Parse( Research.SpellInformation( spellName, 11 ) ) );
						AddButton(170, 405, 4011, 4011, 700+spellName, GumpButtonType.Reply, 0);
						AddButton(210, 405, 4014, 4014, 800+spellName, GumpButtonType.Reply, 0);
						AddHtml( 120, 365, 250, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 250, 405, 60, 20, @"<BODY><BASEFONT Color=" + PrepareColor + "><BIG>" + IsPrepared + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						PrepareColor = "#FBFBFB"; IsPrepared = Research.GetPrepared( bag, spellName ); if ( IsPrepared > 10 ){ PrepareColor = "#15E650"; } else if (IsPrepared > 0 ){ PrepareColor = "#F25A5A"; }
						AddImage(120, 485, Int32.Parse( Research.SpellInformation( spellName, 11 ) ) );
						AddButton(170, 495, 4011, 4011, 700+spellName, GumpButtonType.Reply, 0);
						AddButton(210, 495, 4014, 4014, 800+spellName, GumpButtonType.Reply, 0);
						AddHtml( 120, 455, 250, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 250, 495, 60, 20, @"<BODY><BASEFONT Color=" + PrepareColor + "><BIG>" + IsPrepared + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						PrepareColor = "#FBFBFB"; IsPrepared = Research.GetPrepared( bag, spellName ); if ( IsPrepared > 10 ){ PrepareColor = "#15E650"; } else if (IsPrepared > 0 ){ PrepareColor = "#F25A5A"; }
						AddImage(120, 575, Int32.Parse( Research.SpellInformation( spellName, 11 ) ) );
						AddButton(170, 585, 4011, 4011, 700+spellName, GumpButtonType.Reply, 0);
						AddButton(210, 585, 4014, 4014, 800+spellName, GumpButtonType.Reply, 0);
						AddHtml( 120, 545, 250, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 250, 585, 60, 20, @"<BODY><BASEFONT Color=" + PrepareColor + "><BIG>" + IsPrepared + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName+=8;

					int shft = 50;

					if ( Research.GetResearch( bag, spellName ) )
					{
						PrepareColor = "#FBFBFB"; IsPrepared = Research.GetPrepared( bag, spellName ); if ( IsPrepared > 10 ){ PrepareColor = "#15E650"; } else if (IsPrepared > 0 ){ PrepareColor = "#F25A5A"; }
						AddImage(620+shft, 305, Int32.Parse( Research.SpellInformation( spellName, 11 ) ) );
						AddButton(670+shft, 315, 4011, 4011, 700+spellName, GumpButtonType.Reply, 0);
						AddButton(710+shft, 315, 4014, 4014, 800+spellName, GumpButtonType.Reply, 0);
						AddHtml( 620+shft, 275, 250, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 750+shft, 315, 60, 20, @"<BODY><BASEFONT Color=" + PrepareColor + "><BIG>" + IsPrepared + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						PrepareColor = "#FBFBFB"; IsPrepared = Research.GetPrepared( bag, spellName ); if ( IsPrepared > 10 ){ PrepareColor = "#15E650"; } else if (IsPrepared > 0 ){ PrepareColor = "#F25A5A"; }
						AddImage(620+shft, 395, Int32.Parse( Research.SpellInformation( spellName, 11 ) ) );
						AddButton(670+shft, 405, 4011, 4011, 700+spellName, GumpButtonType.Reply, 0);
						AddButton(710+shft, 405, 4014, 4014, 800+spellName, GumpButtonType.Reply, 0);
						AddHtml( 620+shft, 365, 250, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 750+shft, 405, 60, 20, @"<BODY><BASEFONT Color=" + PrepareColor + "><BIG>" + IsPrepared + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						PrepareColor = "#FBFBFB"; IsPrepared = Research.GetPrepared( bag, spellName ); if ( IsPrepared > 10 ){ PrepareColor = "#15E650"; } else if (IsPrepared > 0 ){ PrepareColor = "#F25A5A"; }
						AddImage(620+shft, 485, Int32.Parse( Research.SpellInformation( spellName, 11 ) ) );
						AddButton(670+shft, 495, 4011, 4011, 700+spellName, GumpButtonType.Reply, 0);
						AddButton(710+shft, 495, 4014, 4014, 800+spellName, GumpButtonType.Reply, 0);
						AddHtml( 620+shft, 455, 250, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 750+shft, 495, 60, 20, @"<BODY><BASEFONT Color=" + PrepareColor + "><BIG>" + IsPrepared + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName+=8;
					if ( Research.GetResearch( bag, spellName ) )
					{
						PrepareColor = "#FBFBFB"; IsPrepared = Research.GetPrepared( bag, spellName ); if ( IsPrepared > 10 ){ PrepareColor = "#15E650"; } else if (IsPrepared > 0 ){ PrepareColor = "#F25A5A"; }
						CandleIcon = 11323;
						AddImage(620+shft, 575, Int32.Parse( Research.SpellInformation( spellName, 11 ) ) );
						AddButton(670+shft, 585, 4011, 4011, 700+spellName, GumpButtonType.Reply, 0);
						AddButton(710+shft, 585, 4014, 4014, 800+spellName, GumpButtonType.Reply, 0);
						AddHtml( 620+shft, 545, 250, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spellName, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 750+shft, 585, 60, 20, @"<BODY><BASEFONT Color=" + PrepareColor + "><BIG>" + IsPrepared + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					} spellName+=8;

					AddImage(362, 575, CandleIcon);
					AddImage(491, 574, 11332);
					AddImage(500, 280, CandleIcon);
					AddImage(371, 287, BookIcon);
					AddImage(502, 453, 11326);
					AddImage(495, 385, 11327);
					AddImage(365, 384, 11338);
					AddImage(355, 489, 11330);
					AddImage(819, 641, SmallIcon);

					AddButton(828, 75, 3610, 3610, 16, GumpButtonType.Reply, 0); // HELP BUTTON
				}
				else if ( m_Page == 11 ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>SPELL RESEARCH</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 115, 147, 756, 380, @"<BODY><BASEFONT Color=#FFA200><BIG>Spell research is something that the most dedicated of wizards pursue. It requires the accumulation of knowledge of those that came before. Mages and Necromancers once practiced this strange magic, that was thought to be lost through the passage of time. No matter the research goal, one would need to find the cubes of power in order to scribe spells. These cubes are also required to cast any spell that was used in ancient times. Once all of the cubes of power are found, the true research can begin.<br><br>Some perform research to write commonly used spells, but cannot find the original scroll that contains the vital information needed to cast it. This research has a scribe searching for the pages of tomes that contain the information to write the spell to parchment. Creating scrolls in this manner require the use of octopus ink, which is scarce since the last octopus was seen centuries ago as the kraken were said to have wiped them out. The more difficult the spell, the more ink that is required to scribe the scroll. Your research will indicate where more octopus ink can be obtained. The better your skills in alchemy, cooking, and taste identification the more use you will find from the collected ink. Modern spells can be learned in the areas of magery and necromancy. You can research each area simultaneously if you choose, but each area will require you to learn the magic in a specific order as the learned spell will help the researcher learn more of the next spell until all are discovered.<br><br>The main goal of spell research is for a spell caster to learn the ancient magic that has been long forgotten. This ancient magic consists of 64 different spells in 8 different schools of magic. These spells were once used by mages and necromancers alike, where those that reached the status of archmage benefited the most from the power these spells unleash. The magic cannot be written in books, but can only be scribed to individual scrolls that the caster can then read from. When read, the scroll crumples to dust. If one has attributes of lower reagent use, the scroll may not crumble and can be used again but that is not guaranteed especially for very powerful spells. Spells are cast with those skilled in either magery or necromancy, whichever is higher. The effectiveness of the spells is dependent on the combination of magery, necromancy, spirit speaking, and the evaluating of intellect. If you are simply skilled in only a couple of these skills, then the spells will have only an average effect. It is those that pursue all four categories of wizardry that will gain the most benefit. When ancient spells are performed, it helps a researcher practice inscription, magery, necromancy, spirit speaking, and intellect evaluation at the same time. This is why ancient spell research interests archmages, as they have achieved the level of grandmaster in both areas of magic. Some ancient magic has similarities to spells used today, as is to be expected that some of the knowledge survived the ages. So very few spells will be similar to current magery spells, and even fewer spells that are similar to modern necromancer spells. Although they are similar, the ancient spell usually proves to be much more powerful.<br><br>This bag will hold all of your research, as well as blank scrolls, quills, and octopus ink for a maximum quantity of 500 each. You can drop scribe pens and blank scrolls onto the bag to replenish those materials. Discovered ink will be placed in your bag when found. It will also hold all of the cubes of power as well as any ancient magic you put to parchment. This bag will hold 500 scrolls of each ancient magic spell. The bag is yours and no one else can look inside it or use the magic you researched within it. If you lose the bag, you should find a scribe immediately and give them another 500 gold where maybe you will have your research returned. If not, you will have to begin your research all over again with an empty bag.<br><br>There are a few different ways to cast the ancient magic. The first is within the bag from the prepared spell section. There is an arrow icon next to each spell that can cast it for you. The other is spell bars for much easier convenience, and they can be configured in the main section of this bag and only if you learned at least 1 of the ancient spells. You are able to have 4 different spell bars for ancient magic, and you can customize each in a variety of ways. The last way to cast these spells is by a typed command, which allows you to make macros for spell casting if you want. Each of these commands are listed below:<br><br>[CastAcidElemental<br><br>[CastAerialServant<br><br>[CastAirWalk<br><br>[CastAnimateBones<br><br>[CastAvalanche<br><br>[CastBanishDaemon<br><br>[CastBloodElemental<br><br>[CastCallDestruction<br><br>[CastCauseFear<br><br>[CastCharm<br><br>[CastClone<br><br>[CastConflagration<br><br>[CastConfusionBlast<br><br>[CastConjure<br><br>[CastCreateFire<br><br>[CastCreateGold<br><br>[CastCreateGolem<br><br>[CastDeathSpeak<br><br>[CastDeathVortex<br><br>[CastDevastation<br><br>[CastDivination<br><br>[CastElectricalElemental<br><br>[CastEnchant<br><br>[CastEndureCold<br><br>[CastEndureHeat<br><br>[CastEtherealTravel<br><br>[CastExplosion<br><br>[CastExtinguish<br><br>[CastFadefromSight<br><br>[CastFlameBolt<br><br>[CastFrostField<br><br>[CastFrostStrike<br><br>[CastGasCloud<br><br>[CastGemElemental<br><br>[CastGrantPeace<br><br>[CastHailStorm<br><br>[CastHealingTouch<br><br>[CastIceElemental<br><br>[CastIcicle<br><br>[CastIgnite<br><br>[CastIntervention<br><br>[CastInvokeDevil<br><br>[CastMagicSteed<br><br>[CastMaskofDeath<br><br>[CastMassDeath<br><br>[CastMassMight<br><br>[CastMassSleep<br><br>[CastMeteorShower<br><br>[CastMudElemental<br><br>[CastOpenGround<br><br>[CastPoisonElemental<br><br>[CastRestoration<br><br>[CastRingofFire<br><br>[CastRockFlesh<br><br>[CastSeeTruth<br><br>[CastSleep<br><br>[CastSleepField<br><br>[CastSneak<br><br>[CastSnowBall<br><br>[CastSpawnCreature<br><br>[CastSwarm<br><br>[CastWeedElemental<br><br>[CastWithstandDeath<br><br>[CastWizardEye<br><br><br><br><br><br></BIG></BASEFONT></BODY>", (bool)false, (bool)true);

					AddImage(86, 532, 11324);
					AddImage(431, 591, 11326);
					AddImage(341, 590, 11316);
					AddImage(777, 573, 11325);
					AddImage(576, 581, 11323);
					AddButton(848, 75, 4017, 4017, 0, GumpButtonType.Reply, 0); // EXIT BUTTON
				}
				else if ( m_Page == 12 ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>CUBES OF POWER</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 115, 147, 756, 380, @"<BODY><BASEFONT Color=#FFA200><BIG>In order to pursue any type of spell research, you must first collect all of the Cubes of Power. There are 26 cubes in total, and they have been lost throughout the land of Sosaria centuries ago. When you begin your search for the cubes, you will have a clue on where the first cube is rumored to be. Once you find this cube, you will learn the whereabouts to another cube. As you find these cubes, they will appear in your pack. Each cube has a word of power engraved in the top, and you need to use these cubes to scribe the magic to scrolls. Once all of the cubes have been found, you can further your research into areas such as wizardry, necromancy, or the spells that were once used by long dead mages. Search the dungeon that the clue provides, and seek the runic pedestal with the small chest on top. If you search the contents, you may find the cube you seek.<br><br>Every cube you find, you will also receive a souvenir that is a replica of the cube you found. These can be used to decorate your home and display your goals in the realm of spell research. Each cube will have a soft glow to them, and their symbols are carved on the top.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddImage(86, 532, 11324);
					AddImage(431, 591, 11326);
					AddImage(341, 590, 11316);
					AddImage(777, 573, 11325);
					AddImage(576, 581, 11323);
					AddButton(848, 75, 4017, 4017, 0, GumpButtonType.Reply, 0); // EXIT BUTTON
				}
				else if ( m_Page == 13 ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>MAGERY RESEARCH</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 115, 147, 756, 380, @"<BODY><BASEFONT Color=#FFA200><BIG>Magery spell research is done in a linear order from the least difficult spell to the most difficult. As you collect the wisdom of wizards that lived long ago, you will gain additional knowledge to construct the next series of spells in the field of magery. There are 64 magery spells in total, and the wisdom of those that originally created them are lost throught the many lands. When you begin your search for these tomes, you will have a clue on where the first book is rumored to be. Once you find this book, you will learn the whereabouts to another book. As you find these tomes, the information on a particular spell will appear in your bag. Search the dungeon that the clue provides, and seek the runic pedestal with the small chest on top. If you search the contents, you may find the book you seek.<br><br>As you learn these spells, you can begin to scribe them. Each spell listed has a button to attempt to scribe the scroll (arrow button) or a button that displays the information about the spell (scroll button). Look over the information to see what you will need to attempt to scribe the scroll. All scrolls require a certain inscription skill and mana to attempt, but also a blank scroll, quill, and reagents. The more difficult the spell, the more octopus ink you will need to scribe the words onto parchment. You can also attempt to scribe the scroll from the information window by pressing the scroll button. If you fail in your attempt, there is a chance that some of the materials will be lost. Scribes that pursue this type of research are those that cannot find a scroll for a particular spell, so this research aids them toward obtaining the spell.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddImage(86, 532, 11324);
					AddImage(431, 591, 11326);
					AddImage(341, 590, 11316);
					AddImage(777, 573, 11325);
					AddImage(576, 581, 11323);
					AddButton(848, 75, 4017, 4017, 0, GumpButtonType.Reply, 0); // EXIT BUTTON
				}
				else if ( m_Page == 14 ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>NECROMANCY RESEARCH</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 115, 147, 756, 380, @"<BODY><BASEFONT Color=#FFA200><BIG>Necromancy spell research is done in a linear order from the least difficult spell to the most difficult. As you collect the wisdom of necromancers that lived long ago, you will gain additional knowledge to construct the next series of spells in the field of necromancy. There are 17 necromancy spells in total, and the wisdom of those that originally created them are lost throught the many lands. When you begin your search for these tomes, you will have a clue on where the first book is rumored to be. Once you find this book, you will learn the whereabouts to another book. As you find these tomes, the information on a particular spell will appear in your bag. Search the dungeon that the clue provides, and seek the runic pedestal with the small chest on top. If you search the contents, you may find the book you seek.<br><br>As you learn these spells, you can begin to scribe them. Each spell listed has a button to attempt to scribe the scroll (arrow button) or a button that displays the information about the spell (scroll button). Look over the information to see what you will need to attempt to scribe the scroll. All scrolls require a certain inscription skill and mana to attempt, but also a blank scroll, quill, and reagents. The more difficult the spell, the more octopus ink you will need to scribe the words onto parchment. You can also attempt to scribe the scroll from the information window by pressing the scroll button. If you fail in your attempt, there is a chance that some of the materials will be lost. Scribes that pursue this type of research are those that cannot find a scroll for a particular spell, so this research aids them toward obtaining the spell.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddImage(86, 532, 11324);
					AddImage(431, 591, 11326);
					AddImage(341, 590, 11316);
					AddImage(777, 573, 11325);
					AddImage(576, 581, 11323);
					AddButton(848, 75, 4017, 4017, 0, GumpButtonType.Reply, 0); // EXIT BUTTON
				}
				else if ( m_Page == 15 ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>ANCIENT SPELL RESEARCH</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 115, 147, 756, 380, @"<BODY><BASEFONT Color=#FFA200><BIG>Ancient spell research is done in a linear order from the least difficult spell to the most difficult where each school of magic is given a turn in the phases of discovery. This means that you will find the information for the easiest conjuration spell first. You will then need to find the information for the easiest death spell next. When you find the easiest spells for each of the 8 schools of magic, the rotation will begin again for the next least difficult spell for each school. This progression needs to be followed until all 64 spells are learned. The wizards that once used these spells died centuries ago, and the written tomes they possessed was lost throught the many lands. When you begin your search for these tomes, you will have a clue on where the first book is rumored to be. Once you find this book, you will learn the whereabouts to another book. As you find these tomes, the information on a particular spell will appear in your bag. Search the dungeon that the clue provides, and seek the runic pedestal with the small chest on top. If you search the contents, you may find the book you seek.<br><br>As you learn these spells, you can begin to scribe them if you have the skills, mana, and resources to do so. Once scribed, the scroll will remain in your bag until you choose to cast it. As you cast these spells, the scribed scrolls will be depleted. Those enhanced with lower reagent qualities, may be able to keep the scrolls from crumbling to dust upon casting but that is not guaranteed. You can learn more about casting these spells on the main screen's Help section or the prepared spells section. Each spell listed has a button to attempt to scribe the scroll (arrow button) or a button that displays the information about the spell (scroll button). Look over the information to see what you will need to attempt to scribe the scroll. All scrolls require a certain inscription skill and mana to attempt, but also a blank scroll, quill, and reagents. You can also attempt to scribe the scroll from the information window by pressing the scroll button if you choose to only scribe a single scroll. You can also press the other button to scribe as many scrolls as you have reagents, quills, and blank scrolls. When you scribe many at once, you only need the required mana as though you were scribing a single scroll, but the resources multiply toward the total quantity that is to be created. Lower reagent attributes do not work toward reagents needing to scribe these spells. If you fail in your attempt, there is a chance that some of the materials will be lost.<br><br><br><br></BIG></BASEFONT></BODY>", (bool)false, (bool)true);

					AddImage(86, 532, 11324);
					AddImage(431, 591, 11326);
					AddImage(341, 590, 11316);
					AddImage(777, 573, 11325);
					AddImage(576, 581, 11323);
					AddButton(848, 75, 4017, 4017, 0, GumpButtonType.Reply, 0); // EXIT BUTTON
				}
				else if ( m_Page == 16 ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				{
					AddHtml( 104, 117, 778, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>PREPARED ANCIENT SPELLS</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 115, 147, 756, 380, @"<BODY><BASEFONT Color=#FFA200><BIG>As you learn these spells, you can begin to scribe them if you have the skills, mana, and resources to do so. Once scribed, the scroll will remain in your bag until you choose to cast it. As you cast these spells, the scribed scrolls will be depleted. Those enhanced with lower reagent qualities, may be able to keep the scrolls from crumbling to dust upon casting but that is not guaranteed. To cast a spell from this window, select the arrow button next to the spell icon. Each spell listed has a scroll button that displays the information about the spell. Look over the information to see what you will need to attempt to scribe the scroll. All scrolls require a certain inscription skill and mana to attempt, but also a blank scroll, quill, and reagents. You can attempt to scribe the scroll from the information window by pressing the scroll button if you choose to only scribe a single scroll. You can also press the other button to scribe as many scrolls as you have reagents, quills, and blank scrolls. When you scribe many at once, you only need the required mana as though you were scribing a single scroll, but the resources multiply toward the total quantity that is to be created. Lower reagent attributes do not work toward reagents needing to scribe these spells. If you fail in your attempt, there is a chance that some of the materials will be lost.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddImage(86, 532, 11324);
					AddImage(431, 591, 11326);
					AddImage(341, 590, 11316);
					AddImage(777, 573, 11325);
					AddImage(576, 581, 11323);
					AddButton(848, 75, 4017, 4017, 0, GumpButtonType.Reply, 0); // EXIT BUTTON
				}

				bag.BagMsgString = "";
				bag.BagMessage = 0;
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile;

				from.SendSound( 0x4A );
				from.CloseGump( typeof( ResearchGump ) );

				// 1 - 9 : TOP SELECTION MENU
				// 11 - 19 : HELP BUTTONS
				// 21 - 29 : MAGERY CIRCLE CHOICES
				
				if ( m_Bag.BagPage > 10 && m_Bag.BagPage < 20 )
				{
					m_Bag.BagPage = m_Bag.BagPage-10;
					from.SendGump( new ResearchGump( m_Bag ) );
				}
				else if ( info.ButtonID > 0 && info.ButtonID < 100 )
				{
					m_Bag.BagPage = info.ButtonID;
					from.SendGump( new ResearchGump( m_Bag ) );
				}
				else if ( info.ButtonID > 300 && info.ButtonID < 400 )
				{
					from.SendGump( new SpellInformation( m_Bag, from, m_Bag.BagPage, (info.ButtonID-300), 0 ) );
				}
				else if ( info.ButtonID > 400 && info.ButtonID < 500 )
				{
					Research.CreateNormalSpell( m_Bag, from, (info.ButtonID-400) );
					from.SendGump( new ResearchGump( m_Bag ) );
				}
				else if ( info.ButtonID > 500 && info.ButtonID < 600 )
				{
					from.SendGump( new SpellInformation( m_Bag, from, m_Bag.BagPage, (info.ButtonID-500), 1 ) );
				}
				else if ( info.ButtonID > 600 && info.ButtonID < 700 )
				{
					Research.CreateResearchSpell( m_Bag, from, (info.ButtonID-600) );
					from.SendGump( new ResearchGump( m_Bag ) );
				}
				else if ( info.ButtonID > 700 && info.ButtonID < 800 )
				{
					from.SendGump( new SpellInformation( m_Bag, from, m_Bag.BagPage, (info.ButtonID-700), 1 ) );
				}
				else if ( info.ButtonID > 800 && info.ButtonID < 900 )
				{
					Research.CastSpell( from, (info.ButtonID-800) );
				}
				else if ( info.ButtonID > 1200 )
				{
					if ( info.ButtonID == 1201 ){ from.CloseGump( typeof( SetupBarsResearch1 ) ); from.SendGump( new SetupBarsResearch1( from, 1 ) ); }
					else if ( info.ButtonID == 1202 ){ from.CloseGump( typeof( SetupBarsResearch2 ) ); from.SendGump( new SetupBarsResearch2( from, 1 ) ); }
					else if ( info.ButtonID == 1203 ){ from.CloseGump( typeof( SetupBarsResearch3 ) ); from.SendGump( new SetupBarsResearch3( from, 1 ) ); }
					else if ( info.ButtonID == 1204 ){ from.CloseGump( typeof( SetupBarsResearch4 ) ); from.SendGump( new SetupBarsResearch4( from, 1 ) ); }

					else if ( info.ButtonID == 1211 ){ InvokeCommand( "researchtool1", from ); }
					else if ( info.ButtonID == 1212 ){ InvokeCommand( "researchtool2", from ); }
					else if ( info.ButtonID == 1213 ){ InvokeCommand( "researchtool3", from ); }
					else if ( info.ButtonID == 1214 ){ InvokeCommand( "researchtool4", from ); }

					else if ( info.ButtonID == 1221 ){ InvokeCommand( "researchclose1", from ); }
					else if ( info.ButtonID == 1222 ){ InvokeCommand( "researchclose2", from ); }
					else if ( info.ButtonID == 1223 ){ InvokeCommand( "researchclose3", from ); }
					else if ( info.ButtonID == 1224 ){ InvokeCommand( "researchclose4", from ); }

					if ( info.ButtonID > 1204 ){ from.SendGump( new ResearchGump( m_Bag ) ); }
				}
			}
		}

		public Mobile BagOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Bag_Owner { get{ return BagOwner; } set{ BagOwner = value; } }

		public int BagInk;
		[CommandProperty(AccessLevel.Owner)]
		public int Bag_Ink { get { return BagInk; } set { BagInk = value; InvalidateProperties(); } }

		public int BagScrolls;
		[CommandProperty(AccessLevel.Owner)]
		public int Bag_Scrolls { get { return BagScrolls; } set { BagScrolls = value; InvalidateProperties(); } }

		public int BagQuills;
		[CommandProperty(AccessLevel.Owner)]
		public int Bag_Quills { get { return BagQuills; } set { BagQuills = value; InvalidateProperties(); } }

		public int BagPage;
		[CommandProperty(AccessLevel.Owner)]
		public int Bag_Page { get { return BagPage; } set { BagPage = value; InvalidateProperties(); } }

		public string BagInkLocation;
		[CommandProperty(AccessLevel.Owner)]
		public string Bag_InkLocation { get { return BagInkLocation; } set { BagInkLocation = value; InvalidateProperties(); } }

		public string BagInkWorld;
		[CommandProperty(AccessLevel.Owner)]
		public string Bag_InkWorld { get { return BagInkWorld; } set { BagInkWorld = value; InvalidateProperties(); } }

		public int BagMessage;
		[CommandProperty(AccessLevel.Owner)]
		public int Bag_Message { get { return BagMessage; } set { BagMessage = value; InvalidateProperties(); } }

		public string BagMsgString;
		[CommandProperty(AccessLevel.Owner)]
		public string Bag_MsgString { get { return BagMsgString; } set { BagMsgString = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string SpellsMagery;
		[CommandProperty(AccessLevel.Owner)]
		public string Spells_Magery { get { return SpellsMagery; } set { SpellsMagery = value; InvalidateProperties(); } }

		public string SpellsMageLocation;
		[CommandProperty(AccessLevel.Owner)]
		public string Spells_MageLocation { get { return SpellsMageLocation; } set { SpellsMageLocation = value; InvalidateProperties(); } }

		public string SpellsMageWorld;
		[CommandProperty(AccessLevel.Owner)]
		public string Spells_MageWorld { get { return SpellsMageWorld; } set { SpellsMageWorld = value; InvalidateProperties(); } }

		public string SpellsMageItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Spells_MageItem { get { return SpellsMageItem; } set { SpellsMageItem = value; InvalidateProperties(); } }

		public string SpellsNecromancy;
		[CommandProperty(AccessLevel.Owner)]
		public string Spells_Necromancy { get { return SpellsNecromancy; } set { SpellsNecromancy = value; InvalidateProperties(); } }

		public string SpellsNecroLocation;
		[CommandProperty(AccessLevel.Owner)]
		public string Spells_NecroLocation { get { return SpellsNecroLocation; } set { SpellsNecroLocation = value; InvalidateProperties(); } }

		public string SpellsNecroWorld;
		[CommandProperty(AccessLevel.Owner)]
		public string Spells_NecroWorld { get { return SpellsNecroWorld; } set { SpellsNecroWorld = value; InvalidateProperties(); } }

		public string SpellsNecroItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Spells_NecroItem { get { return SpellsNecroItem; } set { SpellsNecroItem = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string RuneFound;
		[CommandProperty(AccessLevel.Owner)]
		public string Rune_Found { get { return RuneFound; } set { RuneFound = value; InvalidateProperties(); } }

		public string RuneLocation;
		[CommandProperty(AccessLevel.Owner)]
		public string Rune_Location { get { return RuneLocation; } set { RuneLocation = value; InvalidateProperties(); } }

		public string RuneWorld;
		[CommandProperty(AccessLevel.Owner)]
		public string Rune_World { get { return RuneWorld; } set { RuneWorld = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string ResearchSpells;
		[CommandProperty(AccessLevel.Owner)]
		public string Research_Spells { get { return ResearchSpells; } set { ResearchSpells = value; InvalidateProperties(); } }

		public string ResearchLocation;
		[CommandProperty(AccessLevel.Owner)]
		public string Research_Location { get { return ResearchLocation; } set { ResearchLocation = value; InvalidateProperties(); } }

		public string ResearchWorld;
		[CommandProperty(AccessLevel.Owner)]
		public string Research_World { get { return ResearchWorld; } set { ResearchWorld = value; InvalidateProperties(); } }

		public string ResearchItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Research_Item { get { return ResearchItem; } set { ResearchItem = value; InvalidateProperties(); } }

		public string ResearchPrep1;
		[CommandProperty(AccessLevel.Owner)]
		public string Research_Prep1 { get { return ResearchPrep1; } set { ResearchPrep1 = value; InvalidateProperties(); } }

		public string ResearchPrep2;
		[CommandProperty(AccessLevel.Owner)]
		public string Research_Prep2 { get { return ResearchPrep2; } set { ResearchPrep2 = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string BarsCast1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Bars_Cast1 { get { return BarsCast1; } set { BarsCast1 = value; InvalidateProperties(); } }

		public string BarsCast2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Bars_Cast2 { get { return BarsCast2; } set { BarsCast2 = value; InvalidateProperties(); } }

		public string BarsCast3;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Bars_Cast3 { get { return BarsCast3; } set { BarsCast3 = value; InvalidateProperties(); } }

		public string BarsCast4;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Bars_Cast4 { get { return BarsCast4; } set { BarsCast4 = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public ResearchBag(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);

            writer.Write( (Mobile)BagOwner );
            writer.Write( BagInk );
            writer.Write( BagScrolls );
            writer.Write( BagQuills );
            writer.Write( BagPage );
            writer.Write( BagInkLocation );
            writer.Write( BagInkWorld );
		    writer.Write( BagMessage );
		    writer.Write( BagMsgString );

            writer.Write( SpellsMagery );
            writer.Write( SpellsMageLocation );
            writer.Write( SpellsMageWorld );
            writer.Write( SpellsMageItem );
            writer.Write( SpellsNecromancy );
            writer.Write( SpellsNecroLocation );
            writer.Write( SpellsNecroWorld );
            writer.Write( SpellsNecroItem );

            writer.Write( RuneFound );
            writer.Write( RuneLocation );
            writer.Write( RuneWorld );

            writer.Write( ResearchSpells );
            writer.Write( ResearchLocation );
            writer.Write( ResearchWorld );
            writer.Write( ResearchItem );
            writer.Write( ResearchPrep1 );
            writer.Write( ResearchPrep2 );

            writer.Write( BarsCast1 );
            writer.Write( BarsCast2 );
            writer.Write( BarsCast3 );
            writer.Write( BarsCast4 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			BagOwner = reader.ReadMobile();
			BagInk = reader.ReadInt();
			BagScrolls = reader.ReadInt();
			BagQuills = reader.ReadInt();
			BagPage = reader.ReadInt();
			BagInkLocation = reader.ReadString();
			BagInkWorld = reader.ReadString();
		    BagMessage = reader.ReadInt();
		    BagMsgString = reader.ReadString();

			SpellsMagery = reader.ReadString();
			SpellsMageLocation = reader.ReadString();
			SpellsMageWorld = reader.ReadString();
			SpellsMageItem = reader.ReadString();
			SpellsNecromancy = reader.ReadString();
			SpellsNecroLocation = reader.ReadString();
			SpellsNecroWorld = reader.ReadString();
			SpellsNecroItem = reader.ReadString();

			RuneFound = reader.ReadString();
			RuneLocation = reader.ReadString();
			RuneWorld = reader.ReadString();

			ResearchSpells = reader.ReadString();
			ResearchLocation = reader.ReadString();
			ResearchWorld = reader.ReadString();
			ResearchItem = reader.ReadString();
			ResearchPrep1 = reader.ReadString();
			ResearchPrep2 = reader.ReadString();

			BarsCast1 = reader.ReadString();
			BarsCast2 = reader.ReadString();
			BarsCast3 = reader.ReadString();
			BarsCast4 = reader.ReadString();
		}

		private class SpellInformation : Gump
		{
			private ResearchBag m_Bag;
			private Mobile m_Scribe;
			private int m_Page;
			private int m_Spell;

			public SpellInformation( ResearchBag bag, Mobile from, int page, int spell, int area ) : base( 25, 25 )
			{
				m_Bag = bag;
				m_Scribe = from;
				m_Page = page;
				m_Spell = spell;
				m_Bag.BagPage = m_Page;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);

				if ( area > 0 )
				{
					AddImage(300, 187, 152);
					AddImage(0, 187, 152);
					AddImage(0, 0, 152);
					AddImage(300, 0, 152);
					AddImage(2, 2, 163);
					AddImage(302, 2, 163);
					AddImage(119, 2, 163);
					AddImage(8, 7, 139);
					AddImage(223, 48, 130);
					AddImage(276, 48, 130);
					AddImage(556, 43, 143);
					AddImage(2, 189, 163);
					AddImage(302, 189, 163);
					AddImage(134, 189, 163);
					AddImage(15, 84, Int32.Parse( Research.SpellInformation( spell, 11 ) ) );

					string cubes = Research.SpellInformation( spell, 4 );
					if ( cubes.Length > 0 )
					{
						string[] cube = cubes.Split(' ');
						int box = 0;
						foreach (string rune in cube)
						{
							box++;

							if ( box == 1 ){ AddImage(68, 88, Research.RuneIndex( rune, 1 ) ); }
							else if ( box == 2 ){ AddImage(109, 88, Research.RuneIndex( rune, 1 ) ); }
							else if ( box == 3 ){ AddImage(150, 88, Research.RuneIndex( rune, 1 ) ); }
							else if ( box == 4 ){ AddImage(191, 88, Research.RuneIndex( rune, 1 ) ); }
						}
					}

					AddImage( 555, 82, Int32.Parse( Research.SpellInformation( spell, 10 ) ) );
					AddImage( 12, 394, Int32.Parse( Research.SpellInformation( spell, 9 ) ) );

					AddHtml( 231, 19, 317, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spell, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 18, 142, 564, 230, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + Research.SpellInformation( spell, 3 ) + " School of Magic<br><br>" + Research.SpellInformation( spell, 6 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddHtml( 252, 71, 74, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Mana:</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 336, 71, 74, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spell, 7 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 252, 105, 74, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Skill:</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 336, 105, 74, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.SpellInformation( spell, 8 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddItem(468, 70, 3827);
					AddHtml( 512, 69, 30, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>1</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddItem(481, 97, 8273);
					AddHtml( 512, 105, 30, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>1</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					string reagents = Research.SpellInformation( spell, 5 );
						if ( reagents.Contains(", ") ){ reagents = reagents.Replace(", ", "<br>"); }

					AddHtml( 348, 391, 234, 80, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + reagents + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 155, 394, 120, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Scribe One</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 155, 440, 120, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Scribe Most</BIG></BASEFONT></BODY>", (bool)false, (bool)false);


					AddButton(111, 394, 4011, 4011, 500+spell, GumpButtonType.Reply, 0);
					AddButton(111, 440, 4029, 4029, 600+spell, GumpButtonType.Reply, 0);
					AddButton(558, 10, 4017, 4017, 0, GumpButtonType.Reply, 0);
				}
				else
				{
					int BookIcon = 11317;
						if ( spell > 64 ){ BookIcon = 11319; }

					int Border = 153;
						if ( spell > 64 ){ Border = 154; }

					AddImage(0, 0, Border);
					AddImage(293, 0, Border);
					AddImage(2, 2, 163);
					AddImage(295, 2, 163);
					AddImage(258, 262, 140);
					AddImage(6, 53, 142);
					AddImage(549, 264, 143);
					AddImage(491, 171, BookIcon);

					AddHtml( 90, 20, 236, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spell, 2 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddItem(348, 11, 8273);
					AddHtml( 372, 17, 13, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>1</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddItem(400, 18, 3827);
					AddHtml( 441, 17, 13, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>1</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddItem(478, 13, 10282);
					AddHtml( 502, 17, 13, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spell, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddHtmlLocalized( 88, 57, 488, 102, Int32.Parse( Research.ScrollInformation( spell, 6 ) ), 0x7FFF, false, false );

					AddHtml( 88, 172, 52, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Skill:</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 148, 172, 35, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spell, 5 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddHtml( 214, 172, 52, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Mana:</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 274, 172, 35, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Research.ScrollInformation( spell, 4 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					string reagents = Research.ScrollInformation( spell, 3 );
						if ( reagents.Contains(", ") ){ reagents = reagents.Replace(", ", "<br>"); }

					AddHtml( 339, 171, 134, 95, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + reagents + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(545, 18, 4017, 4017, 0, GumpButtonType.Reply, 0);
					AddButton(276, 211, 4011, 4011, spell, GumpButtonType.Reply, 0);
				}
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile;
				from.SendSound( 0x4A );

				if ( info.ButtonID > 500 && info.ButtonID < 600 )
				{
					Research.CreateResearchSpell( m_Bag, from, info.ButtonID-500 );
					from.CloseGump( typeof( ResearchGump ) );
					from.CloseGump( typeof( SpellInformation ) );
					from.SendGump( new ResearchGump( m_Bag ) );
				}
				else if ( info.ButtonID > 600 && info.ButtonID < 700 )
				{
					Research.CreateManySpells( m_Bag, from, info.ButtonID-600 );
					from.CloseGump( typeof( ResearchGump ) );
					from.CloseGump( typeof( SpellInformation ) );
					from.SendGump( new ResearchGump( m_Bag ) );
				}
				else if ( info.ButtonID > 0 )
				{
					Research.CreateNormalSpell( m_Bag, from, info.ButtonID );
					from.CloseGump( typeof( ResearchGump ) );
					from.CloseGump( typeof( SpellInformation ) );
					from.SendGump( new ResearchGump( m_Bag ) );
				}
				else
				{
					from.CloseGump( typeof( ResearchGump ) );
					from.CloseGump( typeof( SpellInformation ) );
					from.SendGump( new ResearchGump( m_Bag ) );

				}
			}
		}
	}
}