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
	public class HueUOE : Gump
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

		public HueUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.X = dd.x_Hue;
			this.Y = dd.y_Hue;

			this.AddPage(0);

			this.AddBackground(0, 0, 129, 34, dd.s_Style);

			this.AddLabel(24, 7, dd.c_Font, @"Set Hue");

			this.AddTextEntry(80, 7, 40, 20, dd.Hue_G, 1, @"" + dd.Hue_S);
			this.AddButton(7, 10, 1209, 1210, 1, GumpButtonType.Reply, 0);
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
			if ( r1 != false ){dd.Hue_S = si;}   

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
					bool HueCK = dd.HueCKUOE( pm, dd );

					if ( HueCK == false )
					{
						pm.SendMessage( pm.Name + ", You can only enter 1-3000 for the value!");

						dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd7);

						break;
					}

					if ( dd.StcT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m SetStaticHue {1}", CommandSystem.Prefix, dd.Hue_S ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}SetStaticHue {1}", CommandSystem.Prefix, dd.Hue_S ) );

						dd.SendSYSBCK( pm, dd );

						pm.PlaySound(dd.Snd8);
				
						break;
					}
					dd.SendSYSBCK( pm, dd );
				
					break;
				}
			}
		}
	}
}