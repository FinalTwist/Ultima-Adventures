using Server.Diagnostics;
using System;
using System.IO;

namespace Server.Commands
{
    public static class DumpCommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("DumpPackets", AccessLevel.Player, new CommandEventHandler(DumpPackets_OnCommand)); // Warning: This is temporarily exposed to players
        }

        [Usage("DumpPackets")]
        [Description("Generates a log file with information about the last packets sent to each Account.")]
        public static void DumpPackets_OnCommand(CommandEventArgs e)
        {
            string source = e.Mobile != null && e.Mobile.Account != null ? e.Mobile.Account.Username : "Unknown user";
            DumpPackets(source);
        }

        public static void DumpPackets(string source)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("packet-history.log", true))
                {
                    sw.WriteLine("### {0} requested a packet dump at {1:f} UTC ({2}) ###", source, DateTime.UtcNow, Core.TickCount);
                    BaseProfile.WriteAll(sw, PacketHistoryProfile.Profiles);
                    sw.WriteLine();
                }
            }
            catch
            {
            }
        }
    }
}
