// Created by Malice_Molaka
// For script support contact Malice at Malice_Molaka@hotmail.com
using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
namespace Server.Mobiles
{
    [CorpseName("Dante's Corpse")]
    public class Dante : Mobile
    {
        public virtual bool IsInvulnerable { get { return true; } }
        [Constructable]
        public Dante()
        {

            // STR/DEX/INT
            InitStats(31, 41, 51);

            // name
            Name = "Dante";

            Body = 0x190;

            // immortal and frozen to-the-spot features below:
            Blessed = true;
            CantWalk = false;

            // Adding a backpack
            Container pack = new Backpack();
            pack.DropItem(new Gold(250, 300));
            pack.Movable = false;
            AddItem(pack);


            LeatherChest Chest = new LeatherChest();
            Chest.Movable = false;
            AddItem(Chest);

            LeatherGorget Neck = new LeatherGorget();
            Neck.Movable = false;
            AddItem(Neck);

            LeatherArms Arms = new LeatherArms();
            Arms.Movable = false;
            AddItem(Arms);

            LeatherLegs Legs = new LeatherLegs();
            Legs.Movable = false;
            AddItem(Legs);

            LeatherGloves Gloves = new LeatherGloves();
            Gloves.Movable = false;
            AddItem(Gloves);

            FloppyHat Helm = new FloppyHat();
            Helm.Movable = false;
            AddItem(Helm);
        }

        public Dante(Serial serial) : base(serial) { }
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        { base.GetContextMenuEntries(from, list); list.Add(new DanteEntry(from, this)); }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
        public class DanteEntry : ContextMenuEntry
        {
            private Mobile m_Mobile; private Mobile m_Giver;
            public DanteEntry(Mobile from, Mobile giver) : base(6146, 3) { m_Mobile = from; m_Giver = giver; }
            public override void OnClick()
            {
                if (!(m_Mobile is PlayerMobile)) return;
                PlayerMobile mobile = (PlayerMobile)m_Mobile;
                {

                    // gump name
                    if (!mobile.HasGump(typeof(DanteQuestGump)))
                    {
                        mobile.SendGump(new DanteQuestGump(mobile));
                    }
                }
            }
        }
        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            Mobile m = from; PlayerMobile mobile = m as PlayerMobile;
            if (mobile != null)
            {

                // item to be dropped
                if (dropped is DantesInks)
                {
                    if (dropped.Amount != 5)
                    { this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "That is not the right amount!", mobile.NetState); return false; }
                    dropped.Delete();

                    // the reward
                    mobile.AddToBackpack(new Gold(2000));
                    {
                        if (1 > Utility.RandomDouble()) // 1 = 100% = chance to drop 
                            switch (Utility.Random(3))
                            {

                                case 0: mobile.AddToBackpack(new DantesEarrings()); break;
                                case 1: mobile.AddToBackpack(new DantesRing()); break;
                                case 2: mobile.AddToBackpack(new DantesBracelet()); break;
                            }

                        return true;
                    }
                    // thanks message
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Hurry Back!! I have many pages to write.", mobile.NetState);


                    return true;
                }
                else if (dropped is Whip) { this.PrivateOverheadMessage(MessageType.Regular, 1153, 1054071, mobile.NetState); return false; } else { this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState); }
            }
            return false;
        }
    }
}
