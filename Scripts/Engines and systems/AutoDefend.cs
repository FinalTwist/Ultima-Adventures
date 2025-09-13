// AutoDefend v1.1.2
// Author: Felladrin
// Started: 2013-10-14
// Updated: 2016-01-03

using System.Collections.Generic;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Mobiles;

namespace Felladrin.Automations
{
    public static class AutoDefend
    {
        public static class Config
        {
            public static bool Enabled = true;           // Is this system enabled?
            public static bool CommandEnabled = true;    // Should we enable the command to allow players to enable/disable this system for their account?
        }

        public static void Initialize()
        {
            if (Config.Enabled)
            {
                EventSink.AggressiveAction += OnAggressiveAction;
                EventSink.Login += OnLogin;
                EventSink.Logout += OnLogout;

                if (Config.CommandEnabled)
                    CommandSystem.Register("AutoDefend", AccessLevel.Player, new CommandEventHandler(OnCommand));
            }
        }

        static HashSet<int> DisabledPlayers = new HashSet<int>();

        public static void OnAggressiveAction(AggressiveActionEventArgs e)
        {
            if (e.Aggressed.Player && e.Aggressor != e.Aggressed.Combatant && !DisabledPlayers.Contains(e.Aggressed.Serial.Value))
            {
                if (e.Aggressed.Combatant == null)
                {
                    e.Aggressed.Warmode = true;
                    e.Aggressed.Combatant = e.Aggressor;
                }
                else if (e.Aggressor.GetDistanceToSqrt(e.Aggressed) < e.Aggressed.Combatant.GetDistanceToSqrt(e.Aggressed))
                {
                    e.Aggressed.Warmode = true;
                    e.Aggressed.Combatant = e.Aggressor;
                }
            }
        }

        public static void OnLogin(LoginEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;
            Account acc = pm.Account as Account;

            if (acc.GetTag("AutoDefend") == "Disabled")
            {
                DisabledPlayers.Add(pm.Serial.Value);
                pm.SendMessage("Auto-Defend is Disabled for your account.");
            }
            else
            {
                pm.SendMessage("Auto-Defend is Enabled for your account.");
            }
        }

        public static void OnLogout(LogoutEventArgs e)
        {
            DisabledPlayers.Remove(e.Mobile.Serial.Value);
        }

        [Usage("AutoDefend")]
        [Description("Enables or disables the auto-defend feature.")]
        static void OnCommand(CommandEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;
            Account acc = pm.Account as Account;

            if (acc.GetTag("AutoDefend") == null || acc.GetTag("AutoDefend") == "Enabled")
            {
                DisabledPlayers.Add(pm.Serial.Value);
                acc.SetTag("AutoDefend", "Disabled");
                pm.SendMessage(38, "You have disabled the auto-defend feature for your account.");
            }
            else
            {
                DisabledPlayers.Remove(pm.Serial.Value);
                acc.SetTag("AutoDefend", "Enabled");
                pm.SendMessage(68, "You have enabled the auto-defend feature for your account.");
            }
        }
    }
}