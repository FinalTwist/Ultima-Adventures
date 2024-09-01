using Server.Items;

namespace Server.Commands
{
    public class CompleteQuestCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("CompleteQuest", AccessLevel.GameMaster, new CommandEventHandler(OnCommand));
        }

        [Usage("CompleteQuest")]
        [Description("Completes the quest for the targeted item.")]
        public static void OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            e.Mobile.BeginTarget(-1, false, Server.Targeting.TargetFlags.None, new TargetCallback(OnTarget));
            e.Mobile.SendMessage("Target the item you wish to complete the Quest for.");
        }

        public static void OnTarget(Mobile from, object targeted)
        {
            var museumBook = targeted as MuseumBook;
            if (museumBook != null)
            {
                var player = museumBook.Art_Owner;

                // Force-set any required values
                museumBook.Rumor_Dungeon = Misc.Worlds.GetRegionName(player.Map, player.Location);

                var i = 0;
                while (++i < 50)
                {
                    if (MuseumBook.FoundItem(player, museumBook.Rumor_Goal)) return;
                }

                from.SendMessage("Failed to complete quest.");
                return;
            }
        }
    }
}
