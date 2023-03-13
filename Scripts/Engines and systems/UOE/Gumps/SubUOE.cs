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
	public class SubUOE : Gump
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

		public SubUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.X = dd.x_Sub;
			this.Y = dd.y_Sub;

			this.AddPage(0);

			this.AddBackground(0, 0, 101, 48, dd.s_Style);

			if ( dd.StcT == false )
				this.AddLabel(27, 2, dd.c_Font, @"Static Tile");
			else
				this.AddLabel(27, 2, 1153, @"Static Tile");

			this.AddButton(5, 6, 1209, 1210, 1, GumpButtonType.Reply, 0);

			if ( dd.LndT == false )
				this.AddLabel(28, 23, dd.c_Font, @"Land Tile");
			else
				this.AddLabel(28, 23, 1153, @"Land Tile");

			this.AddButton(5, 27, 1209, 1210, 2, GumpButtonType.Reply, 0);
		}

        	public override void OnResponse(NetState ns, RelayInfo info)
        	{
			Mobile mob_m = ns.Mobile;

			PlayerMobile pm = mob_m as PlayerMobile;

			UOETool dd = i_Tool as UOETool;

			if ( pm == null || dd == null )
				return;

			dd.x_Sub = this.X;
			dd.y_Sub = this.Y;

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
					if ( dd.StcT == false )
					{
						dd.StcT = true;
						dd.LndT = false;
					}
					else
						dd.StcT = false;

            				dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;
				}
				case 2:
				{
					if ( dd.LndT == false )
					{
						dd.LndT = true;
						dd.StcT = false;
					}
					else
						dd.LndT = false;

            				dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;
				}
			}
		}
	}
}