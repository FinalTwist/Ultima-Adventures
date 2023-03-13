using System;
using System.Collections;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Misc;

namespace Server.Gumps
{
    public class NameChangeGump : Gump
    {
        public NameChangeGump(Mobile from) : base(100, 100)
        {
            Closable = false;
            Disposable = false;
            Dragable = true;
            Resizable = false;

            AddPage(0);

            AddHtml(153, 135, 304, 113, @"The name you've chosen is currently in use and is no longer available. You must choose a different name before you're able to continue. So delete the text below and enter a new fantasy appropriate name.", (bool)true, (bool)false);
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
            if (from == null)
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
                from.SendGump(new NameChangeGump(from));
            }

            if (name != "")
            {
                if (!NameVerification.Validate(name, 2, 16, true, false, true, 1, NameVerification.SpaceOnly))
                {
                    from.SendMessage(0X22, "That name is unacceptable or already taken.");
                    from.SendGump(new NameChangeGump(from));
                    return;
                }
                if (CharacterCreation.CheckDupe(from, name))
                {
                    from.SendMessage(0X22, "Your name is now {0}.", name);
                    from.Name = name;
                    from.CantWalk = false;
                    return;
                }
            }
            else
            {
                from.SendMessage(0X22, "You must enter a name.");
            }

            from.SendGump(new NameChangeGump(from));
        }
    }
}