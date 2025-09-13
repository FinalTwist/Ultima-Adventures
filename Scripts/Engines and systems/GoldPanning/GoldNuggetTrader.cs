/* Created by Hammerhand & Milva */

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
    public class GoldNuggetTrader : Mobile
    {
        private static bool m_Talked; // flag to prevent spam 

        string[] kfcsay = new string[] // things to say while greeting 
      { 
         "I trade gold nuggets!!", 
         "a medium nugget for 10 small ones",
         "a large nugget for 5 medium ones",
         "a gold brick for 3 large nuggets",
         "a bankcheck for 2 gold bricks",
         "unattended macroing is for whimps!",
	     "You look like a monkey and you smell like one too",
	     "So you're lookin' ta get rich quick eh!?!",
	     "Don't give me that look, give me that Gold!",
	     "Do I look like I know where to find the gold?",
	     "I'll tell you where to pan if you tell me your wife's number...",
	     "The shallower the better... That's what she said!",
	     "Panning in deep water will get you killed!",
         "What? You want to know where to get the tools of the trade?",
         "Seek out a miner that sells stuff n' you'll find the tool you need!",
         "I'm a gold panner! I use a gold pan!"
      };
        public virtual bool IsInvulnerable { get { return true; } }
        [Constructable]
        public GoldNuggetTrader()
        {
            InitStats(31, 41, 51);
            Name = "Harry Johnson";
            Title = "an old dude gold panner";
            Body = 400;
            Female = false;
            Frozen = true;
            SpeechHue = 33;
            Hue = Utility.RandomSkinHue();

            VirtualArmor = 44;

            Item hair = new Item(0x203C);
            hair.Hue = 2555;
            hair.Layer = Layer.Hair;
            hair.Movable = false;
            AddItem(hair);

            Item beard = new Item(0x204C);
            beard.Hue = 2555;
            beard.Layer = Layer.FacialHair;
            beard.Movable = false;
            AddItem(beard);

            AddItem(new Server.Items.LongPants(Utility.RandomNeutralHue()));
            AddItem(new Server.Items.Boots(Utility.RandomNeutralHue()));
            AddItem(new Server.Items.FancyShirt(Utility.RandomNeutralHue()));
            AddItem(new Server.Items.FloppyHat(Utility.RandomNeutralHue()));

            Container pack = new Backpack();
            pack.DropItem(new Gold(10, 100));
            pack.Movable = false;
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false)
            {
                if (m.InRange(this, 4))
                {
                    m_Talked = true;
                    SayRandom(kfcsay, this);
                    this.Move(GetDirectionTo(m.Location));
                    // Start timer to prevent spam 
                    SpamTimer t = new SpamTimer();
                    t.Start();
                }
            }
        }

        private class SpamTimer : Timer
        {
            public SpamTimer()
                : base(TimeSpan.FromSeconds(4))
            {
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }

        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
        }

        public virtual void CheckSpeech(Mobile from, string speech)
        {
        }


        public GoldNuggetTrader(Serial serial)
            : base(serial)
        {
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {

            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;

            if (mobile != null)
            {

                if (dropped is SmallGoldNugget)
                {
                    if (dropped.Amount != 10)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 10 of the same type.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    mobile.AddToBackpack(new MediumGoldNugget());
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Here is a Medium Gold Nugget", mobile.NetState);
                    return true;
                }
                else if (dropped is MediumGoldNugget)
                {
                    if (dropped.Amount != 5)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 5 of the same type.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    mobile.AddToBackpack(new LargeGoldNugget());
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Here is a Large Gold Nugget", mobile.NetState);
                    return true;
                }
                else if (dropped is LargeGoldNugget)
                {
                    if (dropped.Amount != 3)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 3 of the same type.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    mobile.AddToBackpack(new GoldBrick());
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Here is a GoldBrick", mobile.NetState);
                    return true;
                }
                else if (dropped is GoldBrick)
                {
                    if (dropped.Amount != 2)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 2 of the same type.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    mobile.AddToBackpack(new BankCheck(10000));
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Here is a BankCheck", mobile.NetState);
                    return true;
                }

                else
                {
                    from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Can I help you with something?", from.NetState);
                    return false;
                }
            }
            else
            {
                from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "What am I to do with this?", from.NetState);
                return false;
            }
        }
    }
}
