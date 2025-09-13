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
	public class SettingUOE : Gump
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

		public SettingUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.X = dd.x_Setting;
			this.Y = dd.y_Setting;

			this.AddPage(0);

			this.AddBackground(0, 0, 100, 258, dd.s_Style);

			this.AddLabel(22, 5, dd.Hue_T, @"Settings");

			this.AddLabel(28, 32, dd.c_Font, @"Style BG");
			this.AddButton(9, 35, 1209, 1210, 1, GumpButtonType.Reply, 0);

			this.AddLabel(3, 55, dd.c_Font, @"Font Hue");
			this.AddTextEntry(58, 54, 39, 20, dd.Hue_G, 1, @"" + dd.Hue_T);
			this.AddLabel(3, 75, dd.c_Font, @"Font Hue");
			this.AddTextEntry(58, 74, 39, 20, dd.Hue_G, 2, @"" + dd.c_Font);
			this.AddLabel(3, 95, dd.c_Font, @"Font Hue");
			this.AddTextEntry(58, 94, 39, 20, dd.Hue_G, 3, @"" + dd.Hue_G);

			if ( dd.SndOn == true )
			{
				this.AddButton(9, 125, 1210, 1209, 2, GumpButtonType.Reply, 0);
				this.AddLabel(28, 122, 1153, @"Sound On");
			}
			else
			{
				this.AddButton(9, 125, 1209, 1210, 2, GumpButtonType.Reply, 0);
				this.AddLabel(28, 122, dd.c_Font, @"Sound Off");
			}

			this.AddButton(18, 149, 238, 240, 3, GumpButtonType.Reply, 0);
			this.AddButton(18, 180, 2033, 2032, 4, GumpButtonType.Reply, 0);

			this.AddButton(3, 234, 1209, 1210, 5, GumpButtonType.Reply, 0);
			this.AddLabel(3, 209, dd.c_Font, @"Version : 2.9.7");
			this.AddImage(21, 236, 2514);
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
			if ( r1 != false ){dd.Hue_T = si;}   

			TextRelay entry2 = info.GetTextEntry(2);
			string text2 = (entry2 == null ? "" : entry2.Text.Trim());
			bool r2 = Int32.TryParse( text2, out si );
			if ( r2 != false ){dd.c_Font = si;} 

			TextRelay entry3 = info.GetTextEntry(3);
			string text3 = (entry3 == null ? "" : entry3.Text.Trim());
			bool r3 = Int32.TryParse( text3, out si );
			if ( r3 != false ){dd.Hue_G = si;}   

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
            				if ( pm.HasGump( typeof(PickUOE) ) )
                				pm.CloseGump( typeof(PickUOE) );
					pm.SendGump( new PickUOE( pm, dd.p_Page ) );

            				if ( pm.HasGump( typeof(SettingUOE) ) )
                				pm.CloseGump( typeof(SettingUOE) );
					pm.SendGump( new SettingUOE( pm, dd.p_Page ) );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;
				}
                		case 2:
				{
					if ( dd.SndOn == false )
						dd.SndOn = true;
					else
						dd.SndOn = false;

            				dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;
				}
                		case 3:
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

					pm.SendMessage( pm.Name + ", Font Color Loaded!" );

            				dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;
				}
                		case 4:
				{
					pm.SendGump( new HelpUOE( pm, dd.p_Page ) );
					pm.SendGump( new SettingUOE( pm, dd.p_Page ) );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;
				}
                		case 5:
				{
					string UpdateBR = "http://www.golddraco13.com/UO/UOEUD.swf";

					pm.LaunchBrowser(UpdateBR);

            				dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;
				}
			}
		}
	}
}