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
	public class PosUOE : Gump
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

		public PosUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.X = dd.x_Pos;
			this.Y = dd.y_Pos;

			this.AddPage(0);

			this.AddBackground(0, 0, 100, 129, dd.s_Style);

			this.AddLabel(22, 7, dd.Hue_T, @"POS Tile");

			this.AddLabel(20, 43, dd.c_Font, @"W");
			this.AddButton(4, 32, 1209, 1210, 1, GumpButtonType.Reply, 0);

			this.AddLabel(70, 43, dd.c_Font, @"N");
			this.AddButton(81, 32, 1209, 1210, 2, GumpButtonType.Reply, 0);

			this.AddLabel(20, 93, dd.c_Font, @"S");
			this.AddButton(4, 110, 1209, 1210, 3, GumpButtonType.Reply, 0);

			this.AddLabel(70, 93, dd.c_Font, @"E");
			this.AddButton(81, 110, 1209, 1210, 4, GumpButtonType.Reply, 0);

			this.AddTextEntry(45, 70, 30, 20, dd.Hue_G, 1, @"" + dd.M_Val);
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
			if ( r1 != false ){dd.M_Val = si;}

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
					bool MovCK = dd.MovCKUOE( pm, dd );

					if ( MovCK == false )
					{
						pm.SendMessage( pm.Name + ", You can only enter 1-15 for the value!");

						dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd7);

						break;
					}

					if ( dd.StcT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m IncSX -{1}", CommandSystem.Prefix, dd.M_Val ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}IncSX -{1}", CommandSystem.Prefix, dd.M_Val ) );

            					dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd5);
					
						break;
					}

            				dd.SendSYSBCK( pm, dd );

					break;
				}
                		case 2:
				{
					bool MovCK = dd.MovCKUOE( pm, dd );

					if ( MovCK == false )
					{
						pm.SendMessage( pm.Name + ", You can only enter 1-15 for the value!");

						dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd7);

						break;
					}

					if ( dd.StcT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m IncSY -{1}", CommandSystem.Prefix, dd.M_Val ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}IncSY -{1}", CommandSystem.Prefix, dd.M_Val ) );

            					dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd5);
					
						break;
					}

            				dd.SendSYSBCK( pm, dd );

					break;
				}
                		case 3:
				{
					bool MovCK = dd.MovCKUOE( pm, dd );

					if ( MovCK == false )
					{
						pm.SendMessage( pm.Name + ", You can only enter 1-15 for the value!");

						dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd7);

						break;
					}

					if ( dd.StcT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m IncSY {1}", CommandSystem.Prefix, dd.M_Val ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}IncSY {1}", CommandSystem.Prefix, dd.M_Val ) );

            					dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd5);
					
						break;
					}

            				dd.SendSYSBCK( pm, dd );

					break;
				}
                		case 4:
				{
					bool MovCK = dd.MovCKUOE( pm, dd );

					if ( MovCK == false )
					{
						pm.SendMessage( pm.Name + ", You can only enter 1-15 for the value!");

						dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd7);

						break;
					}

					if ( dd.StcT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m IncSX {1}", CommandSystem.Prefix, dd.M_Val ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}IncSX {1}", CommandSystem.Prefix, dd.M_Val ) );

            					dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd5);
					
						break;
					}

            				dd.SendSYSBCK( pm, dd );

					break;
				}
			}
		}
	}
}