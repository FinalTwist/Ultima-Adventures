/*
 * Character Change script by PsYiOn (AKA Admin Aphrodite) http://www.psyion.info - James@psyion.info. Thanks to 
 * * Cheetah2003 for the original script.
 * * Version 1.1 
 * * Added new gump with more character info.
 * * Fixed bug of wrong character being logged in.
 */
using System;
using System.Collections.Generic;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Commands
{
    class commandChangeCharacter
    {

        public static void Initialize()
        {
            CommandSystem.Register("ChangeCharacter", AccessLevel.Player, new CommandEventHandler(ChangeCharacter_OnCommand));
        }

        public static void ChangeCharacter_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            NetState ns = from.NetState;

			if (ns.Account.Count == 1)
            {
                from.SendMessage("You are unable to change characters when you have only one character.");
                return;
            }

            if (from.GetLogoutDelay() > TimeSpan.Zero)
            {
                from.SendMessage("You are unable to change characters at present. Make sure you are not in combat and that you are in a safe logout location.");
                return;
            }

            if (e.ArgString.Length == 0)// If not char is specified
            {
                from.CloseAllGumps();

                // Return player to character select screen.
                //ns.BlockAllPackets = true;
                //from.NetState = null;
                //ns.BlockAllPackets = false;
                ns.Mobile.SendGump(new gumpChangeCharacter(ns, from));

                Console.WriteLine("Client: {0}: Returning to character select. [{1}]",
                    ns.ToString(),
                    ns.Account.Username);

                return;
            }
        }
    }
}
