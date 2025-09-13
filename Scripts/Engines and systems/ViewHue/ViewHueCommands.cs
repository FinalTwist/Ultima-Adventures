using Server.Commands;
using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using System.Reflection;
using Server.Misc;
using System.Diagnostics;

namespace Server.Commands
{
    public class ViewHueCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Hue", AccessLevel.GameMaster, new CommandEventHandler(ViewHue_OnCommand));
            CommandSystem.Register("Hues", AccessLevel.GameMaster, new CommandEventHandler(ViewHues_OnCommand));
        }

        [Usage("Hue")]
        [Description("View Hues in a Gump List")]
        private static void ViewHue_OnCommand( CommandEventArgs e )
        {
            e.Mobile.SendGump(new ViewHueGump(e.Mobile, 0));
        }

        [Usage("Hues")]
        [Description("View Hues Gump")]
        public static void ViewHues_OnCommand( CommandEventArgs e )
        {
            e.Mobile.SendGump(new ViewHuesGump(e.Mobile, 1, 4011));
        }
    }
}
