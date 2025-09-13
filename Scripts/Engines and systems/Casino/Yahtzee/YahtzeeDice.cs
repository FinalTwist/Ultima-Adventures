// This Abstract class is used to group items into the confectionary Category.
// All items crafted within the pottery system are of the BaseConfectionaryItem type.
// supports: increased addability for [add menu.

using System;
using Server.Network;
using Server;
using Server.Items;


namespace Capt.MiniGames
{
    public class YahtzeeDice : Item
    {
        public override void OnDoubleClick(Mobile from)
        {
            base.OnDoubleClick(from);
            //if (!(IsChildOf(from.Backpack))) // Make sure its in their pack
            //{
            //    from.SendMessage("This must be in your backpack for you to use!");
            //}
            //else
            //{
                from.SendGump(new YahtzeeGump(from));
                this.Consume();
            //}
        }
        [Constructable]
        public YahtzeeDice() : base(4007)
        {
            Name = "Yahtzee dice";
            Hue = 1150;
        }

        public YahtzeeDice(Serial serial)
            : base(serial)
        {
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            switch (version)
            {
                case 0:
                    {
                        break;
                    }
            }
        }
    }
}