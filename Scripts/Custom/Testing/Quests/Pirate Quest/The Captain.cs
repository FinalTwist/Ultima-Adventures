using System;
using System.Collections;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class TheCaptain : Mobile
    {

        [Constructable]
        public TheCaptain()
        {
            Name = "The Captain";
            Body = 400;
            BaseSoundID = 0;
            Hue = 33785;
            CantWalk = true;
            Blessed = true;

            AddItem(new Server.Items.Boots(1108));
            AddItem(new Server.Items.Shirt(939));
            AddItem(new Server.Items.ShortPants(1018));
            AddItem(new Server.Items.TricorneHat(1108));
        }

        public TheCaptain(Serial serial) : base(serial)
        {
        }
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new PirateEntry(from, this));
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

        }

        public class PirateEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public PirateEntry(Mobile from, Mobile giver) : base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }

            public override void OnClick()
            {


                if (!(m_Mobile is PlayerMobile))
                    return;

                PlayerMobile mobile = (PlayerMobile)m_Mobile;

                {
                    if (!mobile.HasGump(typeof(PirateGump)))
                    {
                        mobile.SendGump(new PirateGump(mobile));

                    }
                }
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;

            if (mobile != null)
            {
                if (dropped is CaptainsCutlass)
                {


                    dropped.Delete();

                    mobile.SendMessage("Many thanks to ye for returning my cutlass!.");
                    mobile.AddToBackpack(new PirateCutlass());

                    return true;

                }

                else
                {
                    SayTo(from, "That be not my cutlass.");
                }
            }
            return false;


        }

    }
}
