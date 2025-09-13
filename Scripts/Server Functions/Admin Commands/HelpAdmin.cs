using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Gumps;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Items 
{
    class HelpAdmin
    {
		public static void Initialize()
		{
            CommandSystem.Register( "helpadmin", AccessLevel.Administrator, new CommandEventHandler( MyHelpAdmin_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "helpadmin" )]
		[Description( "Opens Help Gump." )]
		public static void MyHelpAdmin_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpeechGump ) );
			if ( ! from.HasGump( typeof( SpeechGump ) ) )
			{
					string sText = ""
						+ "If this is the first time you started this server, or you want to do an overall refresh, run the command below...<br><br>"

						+ "[buildworld - This will decorate and spawn the world, along with generating gardens and stealable artifacts.<br><br>"

						+ "This does not alter any players or their belongings. Once you do this, then the world is ready for the players. There is a unique name system implemented that will force players to have a unique name. There are also 3 task managers in Lord British's castle. One runs every hour, another runs every 3 hours, and the other runs once per day.<br><br>"

						+ "Every hour, the task manager will change the appearances of the shrines. It will also remove any dungeon chests that have been opened. Lastly, it will delete the hidden traps/chests so they may then respawn in random locations again. This is done with a region spawner.<br><br>"

						+ "Every 3 hours, the task manager will replant the gardens in the world and mix up the hostile and mystical creatures that roam the land.<br><br>"

						+ "Every day, the task manager will delete the wandering healers, sea creatures, and tavern patrons. This is so the sea creatures and healers can respawn in new random locations. The tavern patrons change to new characters to give the illusion that new patrons arrived at the tavern.<br><br>"

						+ "The server allows players to macro the gathering of resources, and crafting of items. If you want to disallow this behavior, then set the AllowMacroResources to false in MyServerSettings.cs and restart the server. Players will then be presented with a captcha after randomly determined times. This captcha, that they have to respond to, will help avoid unattended macroing.<br><br>"

						+ "<br><br>"

						+ "There is a message of the day you can make use of. You simply need to edit the News.txt file in the Info folder whenever you want to notify players of anything. You can view the message of the day by typing...<br><br>"
						+ "[motd"

						+ "<br><br>"

						+ "There is a logging system built into the game that tracks where a player goes, what they kill, when they die, what traps they spring, and some other minor activities. They can turn this option off for more privacy. The town criers will use these logs to shout out various things that the players are doing in the game. One can talk to a town crier and learn about all the events. The logs are kept in the Info folder.<br><br>"

						+ "<br><br>"

						+ "There is a MyServerSettings.cs file that allows you to change some parameters of how difficult the game is. The gold monsters and containers drop is cut down to 25%, but can be changed in this file. Also, each dungeon has a difficulty level set here. Some dungeons are harder than others, but the loot is better. Be wary when changing these settings as these have been tested to work well for solo players, which is what this server was designed around.<br><br>"

						+ "<br><br>"

						+ "The game will save if you log out in an inn, tavern, home, etc. If you plan to run this in a multi-player fashion, you may want to disable this feature. To do this, find the MyServerSettings.cs file and change the SaveOnCharacterLogout to false. Either way, the game saves itself every 30 minutes.<br><br>"

						+ "<br><br>"

						+ "Here are some other commands that may interest you...<br><br>"

						+ "[adddoor - Opens the window for you to add a door.<br><br>"
						+ "[addongen - Lets you make an addon and deed for something you created in the game.<br><br>"
						+ "[addtobank - Lets you add an item to many players' bank boxes at once.<br><br>"
						+ "[addtrap - Opens the window for you to add a trap. Make sure you are standing in the spot where you want the trap before selecting one.<br><br>"
						+ "[admin - Opens the window for general server administration.<br><br>"
						+ "[findboat - Lets you find all the boats in the world.<br><br>"
						+ "[scan - Quickly cycle through the players logged in and teleport to them.<br><br>"
						+ "[searchimage - Allows you to find images in UO and then add them to the world.<br><br>"
						+ "[staex - Allows you to export items into a decoration file.<br><br>"
						+ "[townhouses - Lets you build in-game player housing, and also find all the houses built in the world.<br><br>"

						+ "<br><br>"

						+ "This server is set to give players 1,000 skill points to spend. This helps for solo play and lets the player round out a decent character...while still staying within the bounds of mastering a class. Their mana, stamina, and hit points are double the associated ability...this also helps with the solo play aspect. Rejuvenation type potions, skills, and magics have been modified to accommodate these changes."

					+ "";

				from.SendGump(new SpeechGump( "Help For Admins", sText ));
			}
        }
    }
}