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
	public class AltUOE : Gump
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

		public AltUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.X = dd.x_Alt;
			this.Y = dd.y_Alt;

			this.AddPage(0);

			this.AddBackground(0, 0, 46, 165, dd.s_Style);

			this.AddLabel(15, 10, dd.c_Font, @"Z+");
			this.AddButton(15, 38, 250, 251, 1, GumpButtonType.Reply, 0);
			this.AddTextEntry(10, 74, 25, 20, dd.Hue_G, 1, @"" + dd.A_Val);
			this.AddButton(15, 105, 252, 253, 2, GumpButtonType.Reply, 0);
			this.AddLabel(15, 136, dd.c_Font, @"Z-");
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
			if ( r1 != false ){dd.A_Val = si;}      

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
					bool MapAlt = dd.MapAltUOE( pm, dd );

					if ( MapAlt == false )
					{
						pm.SendMessage( pm.Name + ", You entered improper value in the Z field!" );

						dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd7);
				
						break;
					}
					
					if ( dd.StcT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m incStaticAlt {1}", CommandSystem.Prefix, dd.A_Val ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}incStaticAlt {1}", CommandSystem.Prefix, dd.A_Val ) );

            					dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd5);
					
						break;
					}

					if ( dd.LndT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m incLandAlt {1}", CommandSystem.Prefix, dd.A_Val ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}incLandAlt {1}", CommandSystem.Prefix, dd.A_Val ) );

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
					bool MapAlt = dd.MapAltUOE( pm, dd );

					if ( MapAlt == false )
					{
						pm.SendMessage( pm.Name + ", You entered improper value in the Z field!" );

						dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd7);
				
						break;
					}
					
					if ( dd.StcT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m incStaticAlt -{1}", CommandSystem.Prefix, dd.A_Val ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}incStaticAlt -{1}", CommandSystem.Prefix, dd.A_Val ) );

            					dd.SendSYSBCK( pm, dd );

						if ( dd.SndOn == true )
							pm.PlaySound(dd.Snd5);
					
						break;
					}

					if ( dd.LndT == true )
					{
						if ( dd.MultiT == true )
							CommandSystem.Handle( pm, String.Format( "{0}m incLandAlt -{1}", CommandSystem.Prefix, dd.A_Val ) );
						else
							CommandSystem.Handle( pm, String.Format( "{0}incLandAlt -{1}", CommandSystem.Prefix, dd.A_Val ) );

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