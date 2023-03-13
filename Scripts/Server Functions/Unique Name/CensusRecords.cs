using System;
using Server;
using Server.Misc;
using Server.Network;
using System.Text;
using System.IO;
using System.Threading;
using Server.Gumps;
using System.Collections;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	[Flipable(0xFBD, 0xFBE)]
	public class CensusRecords : Item
	{
		[Constructable]
		public CensusRecords( ) : base( 0xFBD )
		{
			Weight = 1.0;
			Name = "Census Records";
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( Name == "Census Records" ){ e.SendGump( new CensusGump( e, true ) ); } else { e.SendGump( new CensusGump( e, false ) ); }
		}

		public CensusRecords(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}

namespace Server.Gumps
{
    public class CensusGump : Gump
    {
        public CensusGump(Mobile from, bool legal) : base(25, 25)
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			string text = "These are the census records for the many lands, and the sages have compiled a list of names of its citizens. Your name is on this list as well. If you want to change your name, you can do it within this book.";

			if ( !legal ){ text = "These are the forged census records for the many lands, and the thieves guild has compiled a list of names of its citizens. Your name is on this list as well. If you want to change your name, you can do it within this book."; } 

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
			AddHtml( 175, 37, 349, 180, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + text + " So if you have an idea for a new fantasy appropriate name, and are willing to spend 2,000 gold, then delete the text below and retype it. A new name can be no longer than 16 characters.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
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

			Container pack = from.Backpack;

            string name = GetString(info, 1);
            if (name != null)
            {
                name = name.Trim();
            }

			if ( name == "Type here..." )
			{
			}
            else if (name != "")
            {
                if (!NameVerification.Validate(name, 2, 16, true, false, true, 1, NameVerification.SpaceOnly))
                {
                    from.SendMessage(0X22, "That name is unacceptable or already taken.");
                    return;
                }
                else if ( CharacterCreation.CheckDupe(from, name) && pack.ConsumeTotal(typeof(Gold), 2000) )
                {
                    from.SendMessage(0X22, "Your name is now {0}.", name);
                    from.Name = name;
                    from.CantWalk = false;
                    return;
                }
                else if ( CharacterCreation.CheckDupe(from, name) && !(pack.ConsumeTotal(typeof(Gold), 2000)) )
                {
                    from.SendMessage(0X22, "You do not have enough gold!");
                    return;
                }
                else
                {
                    from.SendMessage(0X22, "That name is unacceptable or already taken.");
                    return;
                }
            }
            else
            {
                from.SendMessage(0X22, "You must enter a name.");
            }
        }
    }
}