using System;
using System.Collections;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Misc;

namespace Server.Gumps
{
    public class NameAlterGump : Gump
    {
        public NameAlterGump(Mobile from) : base(25, 25)
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 151);
			AddImage(300, 0, 151);
			AddImage(0, 120, 151);
			AddImage(300, 120, 151);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 118, 129);
			AddImage(298, 118, 129);
			AddImage(8, 7, 145);
			AddImage(32, 353, 130);
			AddImage(188, 182, 136);
			AddImage(168, 16, 130);
			AddImage(257, 16, 130);
			AddImage(552, 11, 143);
			AddImage(16, 348, 159);
			AddTextEntry(126, 238, 267, 20, 1511, 1, @"Type here...", 16);
			AddHtml( 175, 37, 349, 180, @"<BODY><BASEFONT Color=#FBFBFB><BIG>One of the main rules of this game is to have a unique fantasy name for your character. Don't name your character TacoChamp123 or Batman, for example. If you feel your name is appropriate, then retype your name here. Otherwise, delete the text below and enter a new name for yourself that is no longer than 16 characters.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(90, 238, 4005, 4005, 1, GumpButtonType.Reply, 0);
        }

        private string GetString(RelayInfo info, int id)
        {
            TextRelay t = info.GetTextEntry(id);
            return (t == null ? null : t.Text.Trim());
        }

        public override void OnResponse( NetState sender, RelayInfo info )
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
                from.SendMessage(0X22, "You may enter a name.");
                from.SendGump(new NameAlterGump(from));
            }

            if (name != "")
            {
                if (!NameVerification.Validate(name, 2, 16, true, false, true, 1, NameVerification.SpaceOnly))
                {
                    from.SendMessage(0X22, "That name is unacceptable or already taken.");
                    return;
                }
                else if ( CharacterCreation.CheckDupe(from, name) )
                {
                    from.SendMessage(0X22, "Your name is now {0}.", name);
                    from.Name = name;
                    from.CantWalk = false;
                    return;
                }
                else if ( CharacterCreation.CheckDupe(from, name) )
                {
                    from.SendMessage(0X22, "That name is unacceptable or already taken.");
                    return;
                }
            }
            else
            {
                from.SendMessage(0X22, "You must enter a name.");
            }

            from.SendGump(new NameAlterGump(from));
        }
    }
}