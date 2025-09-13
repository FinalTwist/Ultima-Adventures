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
    	public class GridUOE : Gump
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

        	public GridUOE( Mobile m, int p ) : base( 0, 0 )
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

			AddPage(0);

			AddImage(0, 77, 4502);
			AddImage(0, 177, 4502);
			AddImage(0, 277, 4502);
			AddImage(0, 377, 4502);
			AddImage(0, 477, 4502);
			AddImage(0, 577, 4502);
			AddImage(0, 677, 4502);
			AddImage(0, 777, 4502);
			AddImage(0, 877, 4502);
			AddImage(0, 977, 4502);
			AddImage(0, 1077, 4502);

			AddImage(77, 0, 4504);
			AddImage(177, 0, 4504);
			AddImage(277, 0, 4504);
			AddImage(377, 0, 4504);
			AddImage(477, 0, 4504);
			AddImage(577, 0, 4504);
			AddImage(677, 0, 4504);
			AddImage(777, 0, 4504);
			AddImage(877, 0, 4504);
			AddImage(977, 0, 4504);
			AddImage(1077, 0, 4504);
			AddImage(1177, 0, 4504);
			AddImage(1277, 0, 4504);
			AddImage(1377, 0, 4504);
			AddImage(1477, 0, 4504);
			AddImage(1577, 0, 4504);
			AddImage(1677, 0, 4504);
			AddImage(1777, 0, 4504);
			AddImage(1877, 0, 4504);
			AddImage(1977, 0, 4504);

			AddLabel(49, 91, dd.c_Font, @"100");
			AddLabel(90, 49, dd.c_Font, @"100");
			AddLabel(49, 191, dd.c_Font, @"200");
			AddLabel(190, 49, dd.c_Font, @"200");
			AddLabel(49, 291, dd.c_Font, @"300");
			AddLabel(290, 49, dd.c_Font, @"300");
			AddLabel(49, 391, dd.c_Font, @"400");
			AddLabel(390, 49, dd.c_Font, @"400");
			AddLabel(49, 491, dd.c_Font, @"500");
			AddLabel(490, 49, dd.c_Font, @"500");
			AddLabel(49, 591, dd.c_Font, @"600");
			AddLabel(590, 49, dd.c_Font, @"600");
			AddLabel(49, 691, dd.c_Font, @"700");
			AddLabel(690, 49, dd.c_Font, @"700");
			AddLabel(49, 791, dd.c_Font, @"800");
			AddLabel(790, 49, dd.c_Font, @"800");
			AddLabel(49, 891, dd.c_Font, @"900");
			AddLabel(890, 49, dd.c_Font, @"900");
			AddLabel(49, 991, dd.c_Font, @"1000");
			AddLabel(990, 49, dd.c_Font, @"1000");
			AddLabel(49, 1091, dd.c_Font, @"1100");
			AddLabel(1090, 49, dd.c_Font, @"1100");

			AddLabel(1190, 49, dd.c_Font, @"1200");
			AddLabel(1290, 49, dd.c_Font, @"1300");
			AddLabel(1390, 49, dd.c_Font, @"1400");
			AddLabel(1490, 49, dd.c_Font, @"1500");
			AddLabel(1590, 49, dd.c_Font, @"1600");
			AddLabel(1690, 49, dd.c_Font, @"1700");
			AddLabel(1790, 49, dd.c_Font, @"1800");
			AddLabel(1890, 49, dd.c_Font, @"1900");
			AddLabel(1990, 49, dd.c_Font, @"2000");
        	}

        	public override void OnResponse(NetState sender, RelayInfo info)
        	{
            		Mobile from = sender.Mobile;

            		switch(info.ButtonID)
            		{
                		case 0:
				{
					break;
				}
			}
	 	}
    	}
}