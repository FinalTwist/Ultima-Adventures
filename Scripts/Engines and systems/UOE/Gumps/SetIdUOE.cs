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
	public class SetIdUOE : Gump
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

		public SetIdUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.X = dd.x_SetId;
			this.Y = dd.y_SetId;

			this.AddPage(0);

			this.AddBackground(0, 0, 115, 36, dd.s_Style);

			this.AddLabel(22, 7, dd.c_Font, @"Set");
			this.AddLabel(92, 8, dd.c_Font, @"ID");
			
			if ( dd.StcT == true )
				this.AddTextEntry(47, 7, 40, 20, dd.Hue_G, 1, @"" + dd.StcID);
			else
				this.AddTextEntry(47, 7, 40, 20, dd.Hue_G, 1, @"" + dd.LndID);

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

			if ( dd.StcT == true )
			{
				TextRelay entry1 = info.GetTextEntry(1);
				string text1 = (entry1 == null ? "" : entry1.Text.Trim());
				bool r1 = Int32.TryParse( text1, out si );
				if ( r1 != false ){dd.StcID = si;}
			}

			if ( dd.LndT == true )
			{
				TextRelay entry1 = info.GetTextEntry(1);
				string text1 = (entry1 == null ? "" : entry1.Text.Trim());
				bool r1 = Int32.TryParse( text1, out si );
				if ( r1 != false ){dd.LndID = si;}
			}

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
					bool IDCheck = dd.IDCKUOE( pm, dd );

					if ( IDCheck == false )
					{
						pm.SendMessage( pm.Name + ", You entered improper values in the ID field!" );

						dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd7);
				
						break;
					}

					if ( dd.StcT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m setStaticId {1}", CommandSystem.Prefix, dd.StcID ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}setStaticId {1}", CommandSystem.Prefix, dd.StcID ) );  

            					dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd5);

						break;
					}

					if ( dd.LndT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m setLandId {1}", CommandSystem.Prefix, dd.LndID ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}setLandId {1}", CommandSystem.Prefix, dd.LndID ) );
 
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