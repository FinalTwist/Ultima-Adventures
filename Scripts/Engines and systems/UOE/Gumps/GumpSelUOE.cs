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
    	public class GumpSelUOE : Gump
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

        	public GumpSelUOE( Mobile m, int p ) : base( 0, 0 )
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
			this.Dragable=false;
			this.Resizable=false;

			this.X = dd.x_Sel;
			this.Y = dd.y_Sel;

			AddPage(0);

			AddBackground(0, 0, 278, 51, dd.s_Style);

			AddLabel(7, 5, dd.Hue_T, @"GUMP");
			AddLabel(12, 25, dd.Hue_T, @"POS");

			AddLabel(65, 5, dd.c_Font, @"" + dd.GmpN);
			AddButton(70, 25, 5538, 5537, 1, GumpButtonType.Reply, 0); //Left
			AddButton(110, 25, 5541, 5542, 2, GumpButtonType.Reply, 0); //Right

			AddLabel(150, 5, dd.c_Font, @"X");
			AddTextEntry(175, 5, 42, 20, dd.Hue_G, 1, @"" + dd.GmpX);
			AddLabel(150, 25, dd.c_Font, @"Y");
			AddTextEntry(175, 25, 42, 20, dd.Hue_G, 2, @"" + dd.GmpY);

			AddLabel(235, 5, dd.c_Font, @"Set");
			AddButton(239, 26, 2117, 2118, 3, GumpButtonType.Reply, 0);
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
			if ( r1 != false ){dd.GmpX = si;}

			TextRelay entry2 = info.GetTextEntry(2);
			string text2 = (entry2 == null ? "" : entry2.Text.Trim());
			bool r2 = Int32.TryParse( text2, out si );
			if ( r2 != false ){dd.GmpY = si;}

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
					dd.CntGN--;

					dd.GumpNameUOE( pm, dd );

					break;
				}
                		case 2:
				{
					dd.CntGN++;

					dd.GumpNameUOE( pm, dd );

					break;
				}
                		case 3:
				{
					if ( dd.GmpX < 0 || dd.GmpX > 2000 )
					{
						pm.SendMessage( pm.Name + ", You entered in an improper X Value!");
						
            					dd.SendSYSBCK( pm, dd );

						break;
					}

					if ( dd.GmpY < 0 || dd.GmpY > 1100 )
					{
						pm.SendMessage( pm.Name + ", You entered in an improper Y Value!");
						
            					dd.SendSYSBCK( pm, dd );

						break;
					}

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					dd.SetGumpNameUOE( pm, dd );

					break;
				}
			}
        	}
    	}
}