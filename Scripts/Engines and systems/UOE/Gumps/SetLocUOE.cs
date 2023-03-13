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
	public class SetLocUOE : Gump
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

		public SetLocUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.X = dd.x_SetLoc;
			this.Y = dd.y_SetLoc;

			this.AddPage(0);

			this.AddBackground(0, 0, 220, 34, dd.s_Style);

			this.AddLabel(25, 7, dd.c_Font, @"Set");
			this.AddLabel(80, 8, dd.c_Font, @"X");
			this.AddLabel(145, 8, dd.c_Font, @"Y");
			this.AddLabel(200, 8, dd.c_Font, @"Z");

			this.AddTextEntry(50, 7, 40, 20, dd.Hue_G, 1, @"" + dd.StcX);
			this.AddTextEntry(100, 7, 40, 20, dd.Hue_G, 2, @"" + dd.StcY);
			this.AddTextEntry(160, 7, 40, 20, dd.Hue_G, 3, @"" + dd.StcZ);

			this.AddButton(5, 10, 1209, 1210, 1, GumpButtonType.Reply, 0);
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
			if ( r1 != false ){dd.StcX = si;}

			TextRelay entry2 = info.GetTextEntry(2);
			string text2 = (entry2 == null ? "" : entry2.Text.Trim());
			bool r2 = Int32.TryParse( text2, out si );
			if ( r2 != false ){dd.StcY = si;}

			TextRelay entry3 = info.GetTextEntry(3);
			string text3 = (entry3 == null ? "" : entry3.Text.Trim());
			bool r3 = Int32.TryParse( text3, out si );
			if ( r3 != false ){dd.StcZ = si;}

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
					//ToDo del static then add static to new loc?
					pm.SendMessage( pm.Name + ", Not in yet, Coming Soon!" );

            				dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;
				}
			}
		}
	}
}