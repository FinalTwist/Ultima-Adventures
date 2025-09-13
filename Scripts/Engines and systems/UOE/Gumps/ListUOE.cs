using System;
using System.IO;
using System.Media;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Targets;
using Server.Targeting;

namespace Server.Gumps
{
	public class ListUOE : Gump
	{
        	private Mobile mob_m;

		public Mobile m_Mob
		{ 
			get{ return mob_m; } 
			set{ mob_m = value; } 
		}

        	private Item tool_i;
	
		public Item i_Tool
		{ 
			get{ return tool_i; } 
			set{ tool_i = value; } 
		}

		public ListUOE( Mobile m, int p ) : base( 0, 0 )
		{
			PlayerMobile pm = m as PlayerMobile;

			if (pm == null || pm.Backpack == null)
    				return;

            		m_Mob = pm;
			
			Item check = pm.Backpack.FindItemByType(typeof(UOETool) );

			if ( check == null )
			{
				pm.SendMessage( pm.Name + ", Contact Draco, System Error : Check Failed {0}/{1}", check );

				return;
			}

			UOETool dd = check as UOETool;

			i_Tool = dd;

			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;

			this.X = dd.x_List;
			this.Y = dd.y_List;

			this.AddPage(0);

			this.AddBackground(0, 0, 100, 318, dd.s_Style);

			this.AddLabel(34, 6, dd.Hue_T, @"ID List");
			this.AddLabel(6, 30, dd.c_Font, @"1");
			this.AddLabel(6, 55, dd.c_Font, @"2");
			this.AddLabel(6, 81, dd.c_Font, @"3");
			this.AddLabel(6, 106, dd.c_Font, @"4");
			this.AddLabel(6, 131, dd.c_Font, @"5");
			this.AddLabel(6, 157, dd.c_Font, @"6");
			this.AddLabel(6, 182, dd.c_Font, @"7");
			this.AddLabel(6, 207, dd.c_Font, @"8");
			this.AddLabel(6, 233, dd.c_Font, @"9");
			this.AddLabel(6, 258, dd.c_Font, @"0");

			this.AddTextEntry(20, 30, 70, 20, dd.Hue_G, 1, @"" + dd.List1);
			this.AddTextEntry(20, 55, 70, 20, dd.Hue_G, 2, @"" + dd.List2);
			this.AddTextEntry(20, 80, 70, 20, dd.Hue_G, 3, @"" + dd.List3);
			this.AddTextEntry(20, 106, 70, 20, dd.Hue_G, 4, @"" + dd.List4);
			this.AddTextEntry(20, 131, 70, 20, dd.Hue_G, 5, @"" + dd.List5);
			this.AddTextEntry(20, 156, 70, 20, dd.Hue_G, 6, @"" + dd.List6);
			this.AddTextEntry(20, 181, 70, 20, dd.Hue_G, 7, @"" + dd.List7);
			this.AddTextEntry(20, 207, 70, 20, dd.Hue_G, 8, @"" + dd.List8);
			this.AddTextEntry(20, 232, 70, 20, dd.Hue_G, 9, @"" + dd.List9);
			this.AddTextEntry(20, 257, 70, 20, dd.Hue_G, 10, @"" + dd.List0);

			this.AddButton(23, 285, 247, 248, 1, GumpButtonType.Reply, 0);
		}

        	public override void OnResponse(NetState ns, RelayInfo info)
        	{
			Mobile mob_m = ns.Mobile;

			PlayerMobile pm = mob_m as PlayerMobile;

			UOETool dd = i_Tool as UOETool;

			if ( pm == null || dd == null )
				return;

			int si;

			TextRelay entry1 = info.GetTextEntry(1);
			string text1 = (entry1 == null ? "" : entry1.Text.Trim());
			bool r1 = Int32.TryParse( text1, out si );
			if ( r1 != false ){dd.List1 = si;}

			TextRelay entry2 = info.GetTextEntry(2);
			string text2 = (entry2 == null ? "" : entry2.Text.Trim());
			bool r2 = Int32.TryParse( text2, out si );
			if ( r2 != false ){dd.List2 = si;}

			TextRelay entry3 = info.GetTextEntry(3);
			string text3 = (entry3 == null ? "" : entry3.Text.Trim());
			bool r3 = Int32.TryParse( text3, out si );
			if ( r3 != false ){dd.List3 = si;}

			TextRelay entry4 = info.GetTextEntry(4);
			string text4 = (entry4 == null ? "" : entry4.Text.Trim());
			bool r4 = Int32.TryParse( text4, out si );
			if ( r4 != false ){dd.List4 = si;}

			TextRelay entry5 = info.GetTextEntry(5);
			string text5 = (entry5 == null ? "" : entry5.Text.Trim());
			bool r5 = Int32.TryParse( text5, out si );
			if ( r5 != false ){dd.List5 = si;}

			TextRelay entry6 = info.GetTextEntry(6);
			string text6 = (entry6 == null ? "" : entry6.Text.Trim());
			bool r6 = Int32.TryParse( text6, out si );
			if ( r6 != false ){dd.List6 = si;}

			TextRelay entry7 = info.GetTextEntry(7);
			string text7 = (entry7 == null ? "" : entry7.Text.Trim());
			bool r7 = Int32.TryParse( text7, out si );
			if ( r7 != false ){dd.List7 = si;}

			TextRelay entry8 = info.GetTextEntry(8);
			string text8 = (entry8 == null ? "" : entry8.Text.Trim());
			bool r8 = Int32.TryParse( text8, out si );
			if ( r8 != false ){dd.List8 = si;}

			TextRelay entry9 = info.GetTextEntry(9);
			string text9 = (entry9 == null ? "" : entry9.Text.Trim());
			bool r9 = Int32.TryParse( text9, out si );
			if ( r9 != false ){dd.List9 = si;}

			TextRelay entry0 = info.GetTextEntry(0);
			string text0 = (entry0 == null ? "" : entry0.Text.Trim());
			bool r0 = Int32.TryParse( text0, out si );
			if ( r0 != false ){dd.List0 = si;}

      			switch(info.ButtonID)
            		{
                		case 0:
				{
					pm.SendMessage( pm.Name + ", Thanks for using the UO Editor!" );

            				dd.SendSYSBCK( pm, dd );

					break;
				}
                		case 1:
				{
					dd.ListT = true;					

					pm.SendMessage( pm.Name + ", Tile ID's Loaded!" );

					dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);
				
					break;
				}
			}
		}
	}
}