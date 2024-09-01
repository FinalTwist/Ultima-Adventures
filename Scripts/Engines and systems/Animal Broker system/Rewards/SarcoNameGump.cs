using System;
using System.Collections;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Misc;
using Server.Items;

namespace Server.Gumps
{
    public class SarcoNameGump : Gump
    {
        const string DEFAULT_NAME = "Type here...";
        private Item m_gate;

        public SarcoNameGump(Mobile from, Item gate) : base(100, 100)
        {
            Closable = true;
            Disposable = false;
            Dragable = true;
            Resizable = false;
            m_gate = gate;

            AddPage(0);

            AddBackground(137, 119, 334, 195, 9250);
            AddBackground(221, 264, 171, 29, 3000);
            AddHtml(153, 135, 304, 113, @"Enter a name for this sarcophagus below.", (bool)true, (bool)false);

            AddLabel(153, 270, 0, @"New Name:");
            AddTextEntry(224, 268, 163, 21, 0, 1, DEFAULT_NAME, 16); // 16 Character Limit
            AddButton(395, 267, 4023, 4024, 1, GumpButtonType.Reply, 0); // Okay
        }

        private string GetString(RelayInfo info, int id)
        {
            TextRelay t = info.GetTextEntry(id);
            return (t == null ? null : t.Text.Trim());
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 0) return;

            Mobile from = sender.Mobile;
            if (from == null || m_gate == null)
            {
                return;
            }

            string name = GetString(info, 1);
            name = name != null ? name.Trim() : null;
            if (!string.IsNullOrWhiteSpace(name) && name != DEFAULT_NAME)
            {
                from.SendMessage(0X22, "The sarcophagus is now called {0}.", name);
                m_gate.Name = name;
                return;
            }
            else
            {
                from.SendMessage(0X22, "You may enter a name.");
            }

            from.SendGump(new SarcoNameGump(from, m_gate));
        }
    }
}
