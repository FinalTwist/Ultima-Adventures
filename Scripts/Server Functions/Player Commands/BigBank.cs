using Server.Mobiles;
using Server.Items;
using Server.Commands;

namespace Server.Scripts.Commands
{
    public class BigBank
    {
        public static void Initialize()
        {
            CommandSystem.Register("bigbank", AccessLevel.Player, new CommandEventHandler(BigBank_OnCommand));
        }

        public static void BigBank_OnCommand(CommandEventArgs e)
        {
            PlayerMobile player = e.Mobile as PlayerMobile;
            BankBox bank = player.FindBankNoCreate();
            if (player != null && bank != null)
            {
                if (bank.ItemID == 0xE7C)
                {
                    bank.ItemID = 0x2A9E;
                    bank.UpdateContainerData();
                    player.SendMessage("Your bank is now large.");
                }
                else
                {
                    bank.ItemID = 0xE7C;
                    bank.UpdateContainerData();
                    player.SendMessage("Your bank is now small.");
                }
            }
        }
    }
}