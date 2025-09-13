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
    	public class HelpUOE : Gump
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

        	public HelpUOE( Mobile m, int p ) : base( 0, 0 )
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

			this.AddPage(0);
			this.AddBackground(0, 0, 796, 564, dd.s_Style);
			this.AddLabel(358, 14, dd.Hue_T, @"Help Menu");
			this.AddHtml( 14, 46, 767, 475, @"

Welcome to Ultima Live Editor, built to compliment Ultima Live System built by Praxiiz!

Understanding our build version : Currently 2.9.7

2 = Second build of Ultima Live Editor!
9.7 = Current Ultima Online Version Supported!

Understanding the Editor!

The editor is broken into individual gumps that control seperate systems within the Ultma Live System!

Future plans are to incorperate complimentry systems!

Please reply with wishes/wants/changes/bugs to ServUO Forums (This Systems Post)


Controls :


Add Gump : 

On this menu you can add a land or static ID to a Location set in the XYZ, if XYZ left 0, you'll get a target curser just with add ID, but if XYZ set, then the ID will be added at that location!

Multi Command Supported on ID only!

Hue Linkable if On!


Alt Gump

On this gump you can lower or raise the land or static tile, you have to set the distance to move in the center box between the up and down arrows!

Multi Command Supported!


Delete Gump

Just as the name applies, but this will only work on static tiles!

Multi Command Supported!


Gump Selection Gump

This is a gump repositioning gump, use this to set the XY position of any gump in the editor, as you change the gump name to the gump menu you want to move, a grid will appear, use this grid to get a ruff number, X goes Left to Right and Y is Top to Bottom!


Hue Gump

This is to set the hue for Static tiles, this can be used with the add tool to preset a statics hue on add!

Multi Command Supported!


Info Gump

This is a menu to gain information on a land or static tile, use the get button to load the menu, if you want to transfer the info to the rest of your menus, hit apply, this will populate all menus with the menus information.


Main Gump

This is where you activate the system, this is also where you close the system, to close the main gump, make sure the systems closed, then double click the tool to close!


Move Gump

This does just as it says, right now it only moves a static tile, future plans to add land tiles too!


Multi Gump

When toggled on will add the multi command to other commands within the system!


Pick Gump

This is a sub gump for the settings gump, you can use this gump to set a background for your gumps!


Pos Gump

This gump allows you to move static tiles on the XY, enter a number in the middle then use the directional arrows to move a static.

Multi Command Supported!


Reset Gump

If the system isn't working right or you think you messed up the menus, hit this to reset the system, everything reset to default!


Set ID Gump

As the gump implies, enter a static or land ID, hit enter!

Multi Command Supported!


Setting Gump

This is where you can set the Background, Text Hues (Title, General, Entry), Turn Sound on/off, Check for updates, Find Help!


Sub Gump

This is the tile submission menu, this defines what type of tiles you want to work on, at the moment you can either pick statics or land, in the future there is planned on more types like itme/mobs added here.


Well thats all the controls open in this release, if you peak in the scripts you'll see a sneak peak of whats coming in V3.9.X


GoldDraco13
			", (bool)true, (bool)true);

			AddButton(359, 530, 247, 248, 1, GumpButtonType.Reply, 0);
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
					dd.SendSYSBCK( pm, dd );

					break;
				}
                		case 1:
				{	
					dd.SendSYSBCK( pm, dd );

					break;
				}
			}
        	}
    	}
}