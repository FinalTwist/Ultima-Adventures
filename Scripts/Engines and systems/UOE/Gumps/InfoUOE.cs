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
	public class InfoUOE : Gump
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

		public InfoUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.X = dd.x_Info;
			this.Y = dd.y_Info;

			this.AddPage(0);

			this.AddBackground(0, 0, 282, 70, dd.s_Style);

			this.AddLabel(7, 5, dd.Hue_T, @"Tile Info");

			this.AddLabel(20, 25, dd.c_Font, @"Get");
			this.AddButton(25, 47, 1209, 1210, 1, GumpButtonType.Reply, 0);

			this.AddLabel(60, 5, dd.c_Font, @"Name : " + dd.TempN);
			this.AddLabel(210, 5, dd.c_Font, @"ID : " + dd.TempID);
			this.AddLabel(60, 25, dd.c_Font, @"Loc : X : " + dd.TempX);
			this.AddLabel(160, 25, dd.c_Font, @"Y : " + dd.TempY);
			this.AddLabel(225, 25, dd.c_Font, @"Z : " + dd.TempZ);

			if ( dd.StcT == true )
				this.AddLabel(70, 45, dd.c_Font, @"Hue : " + dd.TempH);

			this.AddButton(216, 45, 240, 239, 2, GumpButtonType.Reply, 0);
		}

        	public override void OnResponse(NetState ns, RelayInfo info)
        	{
			Mobile mob_m = ns.Mobile;

			PlayerMobile pm = mob_m as PlayerMobile;

			UOETool dd = i_Tool as UOETool;

			if ( pm == null || dd == null )
				return;  

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
					pm.Target = new UOETarget();

					dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;	
				}
                		case 2:
				{
					if ( dd.StcT == true )
					{
						dd.StcID = dd.TempID;
						dd.StcX = dd.TempX;
						dd.StcY = dd.TempY;
						dd.StcZ = dd.TempZ;
						dd.Hue_S = dd.TempH;
					}

					if ( dd.LndT == true )
					{
						dd.LndID = dd.TempID;
						dd.LndX = dd.TempX;
						dd.LndY = dd.TempY;
						dd.LndZ = dd.TempZ;
					}

					dd.SendSYSBCK( pm, dd );

					if ( dd.SndOn == true )
						pm.PlaySound(dd.Snd5);

					break;	
				}
			}
		}
	}
}