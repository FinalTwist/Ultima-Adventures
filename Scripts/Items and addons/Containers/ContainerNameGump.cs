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
        private BaseContainer m_gate;

        public ContainerNameGump(Mobile from, BaseContainer gate) : base(100, 100)
        {
            Closable = false;
            Disposable = false;
            Dragable = true;
            Resizable = false;
            m_gate = gate;

            AddPage(0);

            AddHtml(153, 135, 304, 113, @"Enter a name for this container below.", (bool)true, (bool)false);
			AddHtml(153, 170, 304, 113, @"The name will be overwritten!", (bool)true, (bool)false);
            AddBackground(137, 119, 334, 195, 9250);
            AddBackground(221, 264, 171, 29, 3000);

            AddLabel(153, 270, 0, @"New Name:");
            AddTextEntry(224, 268, 163, 21, 0, 1, @"Type here...", 16); // 16 Character Limit
            AddButton(395, 267, 4023, 4024, 1, GumpButtonType.Reply, 0); // Okay
        }

        private string GetString(RelayInfo info, int id)
        {
            TextRelay t = info.GetTextEntry(id);
            return (t == null ? null : t.Text.Trim());
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            if (from == null || m_gate == null)
            {
                return;
            }

            string name = GetString(info, 1);
            if (name != null)
            {
                name = name.Trim();
            }
            else
            {
                from.SendMessage(0X22, "You must enter a name.");
                from.SendGump(new ContainerNameGump(from, m_gate));
            }

            if (name != "")
            {
                    from.SendMessage(0X22, "The container is now called {0}.", name);
                    m_gate.Name = name;
                    return;
            }
            else
            {
                from.SendMessage(0X22, "You must enter a name.");
            }

            from.SendGump(new ContainerNameGump(from, m_gate));
        }
    }
}