using System;
using System.Collections;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Misc;
using Server.Items;

namespace Server.Gumps
{
    public class ContainerNameGump : Gump
    {
        const string DEFAULT_NAME = "Type here...";
        private BaseContainer m_gate;

        public ContainerNameGump(Mobile from, BaseContainer gate) : base(100, 100)
        {
            Closable = true;
            Disposable = false;
            Dragable = true;
            Resizable = false;
            m_gate = gate;

            AddPage(0);

            AddBackground(137, 119, 334, 210, 9250);
            AddHtml(153, 135, 304, 113, @"Enter a name for this container below.", (bool)true, (bool)false);
			AddHtml(153, 170, 304, 113, @"The name will be overwritten!", (bool)true, (bool)false);

            AddBackground(221, 285, 171, 29, 3000);
            AddLabel(153, 290, 0, @"New Name:");
            AddTextEntry(224, 290, 163, 21, 0, 1, DEFAULT_NAME, 16); // 16 Character Limit
            AddButton(395, 290, 4023, 4024, 1, GumpButtonType.Reply, 0); // Okay
        }

        private string GetString(RelayInfo info, int id)
        {
            TextRelay t = info.GetTextEntry(id);
            return (t == null ? null : t.Text.Trim());
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            if (from == null || m_gate == null || info.ButtonID != 1)
            {
                return;
            }

            string name = GetString(info, 1);
            if (!string.IsNullOrWhiteSpace(name) && name != DEFAULT_NAME)
            {
                    from.SendMessage(0X22, "The container is now called {0}.", name);
                    m_gate.Name = name;
                    return;
            }
            else
            {
                from.SendMessage(0X22, "You may enter a name.");
            }

            from.SendGump(new ContainerNameGump(from, m_gate));
        }
    }
}