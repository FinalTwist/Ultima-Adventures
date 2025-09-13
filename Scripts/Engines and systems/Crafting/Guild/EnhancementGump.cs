using System;
using Server;
using Server.Items;

namespace Server.Gumps
{
    public class EnhancementGump : Gump
    {
        private GuildCraftingProcess Process;

        public EnhancementGump(GuildCraftingProcess process) : base(40, 40)
        {
            bool MoreAttributesAllowed = true;

            Process = process;

            if (Process.CurrentAttributeCount >= Process.MaxAttrCount)
                MoreAttributesAllowed = false;

            AddBackground(0, 0, 620, 390, 9200);
            AddImageTiled(8, 10, 604, 24, 2624);
            AddImageTiled(8, 38, 300, 345, 2624);
            AddImageTiled(312, 38, 300, 345, 2624);
            AddAlphaRegion(8, 10, 604, 373);

            AddLabel(224, 12, 0x481, "Equipment Enhancement");

            AddLabel(15, 40, 0x481, "Attributes");
            AddLabel(184, 40, 0x481, "Gold");
            AddLabel(273, 40, 0x481, "Use");

            AddLabel(319, 40, 0x481, "Attributes");
            AddLabel(488, 40, 0x481, "Gold");
            AddLabel(577, 40, 0x481, "Use");

            int column = 0;
            int row = 0;

            for ( int i = 0; i < AttributeHandler.Definitions.Count; i++)
            {
                AttributeHandler handler = AttributeHandler.Definitions[i];

                if (handler.IsUpgradable(Process.ItemToUpgrade))
                {
                    int currentValue = handler.Upgrade(Process.ItemToUpgrade, true);

                    if (currentValue > 0 || MoreAttributesAllowed)
                    {
                        if (row > 11)
                        {
                            row = 0;
                            column = 1;
                        }

                        AddLabel(15 + (304 * column), 65 + (25 * row), 0x481, handler.Description);
                        AddLabel(184 + (304 * column), 65 + (25 * row), 0x481, Process.GetCostToUpgrade(handler).ToString());
                        AddButton(270 + (304 * column), 62 + (25 * row), 4023, 4024, 1000 + i, GumpButtonType.Reply, 0);

                        row++;
                    }
                }
            }
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
            if (info.ButtonID >= 1000)
            {
                AttributeHandler handler = AttributeHandler.Definitions[info.ButtonID - 1000];
                Process.BeginUpgrade(handler);
            }
        }
    }
}
