using System.Collections.Generic;
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;


namespace Server.Mobiles
{
    [CorpseName("Corpse of Archambeau")]
    public class Archambeau : BaseCreature
    {
        public virtual bool IsInvulnerable { get { return true; } }

        [Constructable]
        public Archambeau()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Archambeau";
            Title = "Guard of Knowledge";
            Body = 400;
            CantWalk = true;
            Hue = 33770;
            CantWalk = true;

            //int hairHue = 0;
            Blessed = true;

            AddItem(new LongHair(0));
            AddItem(new Vandyke(0));

            AddItem(new Cloak(1882));
            AddItem(new Sandals(1530));
            AddItem(new Robe(1530));

            Backpack backpack = new Backpack();
            backpack.Hue = 1530;
            backpack.Movable = false;
            AddItem(backpack);

        }

        public Archambeau(Serial serial)
            : base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new ArchambeauEntry(from, this));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public class ArchambeauEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public ArchambeauEntry(Mobile from, Mobile giver)
                : base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            base.HandlesOnSpeech(from);
            return true;
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        { 
                   Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

            if (mobile != null)
            {
                
                 
                  if (dropped is aeParchment3)

                {
                        dropped.Delete();
                        mobile.SendGump(new ElementQuestGump9(from));
                        return true;
                    }
                    else
                    {
                        from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Have we met before ?", from.NetState);
                        return false;
                    }
                }
                else
                {
                    from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "What am I to do with this ?", from.NetState);
                    return false;
                }

            }

        }
    }

                    
                   
