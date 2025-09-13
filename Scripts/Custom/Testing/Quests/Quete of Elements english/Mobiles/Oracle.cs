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
    [CorpseName("Corpse of Oracle")]
    public class Oracle : BaseCreature
    {
        //public override bool IsInvulnerable { get { return true; } }

        [Constructable]
        public Oracle()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Oracle";
            Title = "I was, I'm, I'll be";
            Body = 400;
            CantWalk = true;
            Hue = 33770;
            CantWalk = true;

            Blessed = true;

            
            AddItem(new LongHair(0));
            

            AddItem(new Cloak(1153));
            AddItem(new Sandals(1153));
            AddItem(new Robe(1153));
            AddItem(new Bandana(1153));

            GoldNecklace goldnecklace = new GoldNecklace();
            goldnecklace.Hue = 0;
            goldnecklace.Movable = false;
            AddItem(goldnecklace);

            GoldRing goldring = new GoldRing();
            goldring.Hue = 0;
            goldring.Movable = false;
            AddItem(goldring);

            Backpack backpack = new Backpack();
            backpack.Hue = 1530;
            backpack.Movable = false;
            AddItem(backpack);

        }

        public Oracle(Serial serial)
            : base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new OracleEntry(from, this));
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

        public class OracleEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public OracleEntry(Mobile from, Mobile giver)
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

        public override void OnSpeech(SpeechEventArgs e)
        {

            bool isMatch = false;

            Mobile from = e.Mobile;

            string keyword = this.Name + " Hail";
 

                if (keyword != null && e.Speech.ToLower().IndexOf(keyword.ToLower()) >= 0)
            {
                isMatch = true;

                if (!isMatch)
                    return;

                from.SendGump(new ElementQuestGump(from));
                e.Handled = true;
            }
           
            base.OnSpeech(e);
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        { 
                   Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

            if (mobile != null)
            {
                if (dropped is ElementFeu)
                {
                        dropped.Delete();
                        mobile.SendGump(new ElementQuestGump2(from));
                        mobile.AddToBackpack(new eParchment1());
                        return true;
                    }
                  else if (dropped is ElementAir)
                {
                        dropped.Delete();
                        mobile.SendGump(new ElementQuestGump4(from));
                        mobile.AddToBackpack(new eParchment2());
                        return true;
                    }
                    else if (dropped is ElementEau)
                {
                        dropped.Delete();
                        mobile.SendGump(new ElementQuestGump6(from));
                        mobile.AddToBackpack(new aeParchment1());
                        return true;
                    }
                  else if (dropped is ElementTerre)
                {
                        dropped.Delete();
                        mobile.SendGump(new ElementQuestGump8(from));
                        mobile.AddToBackpack(new aeParchment2());
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

                    
                   
