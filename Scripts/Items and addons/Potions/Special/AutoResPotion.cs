using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Prompts;
using Server.Network;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;
using System.Globalization;

namespace Server.Items
{
    public class AutoResPotion : Item
    {
        private static Dictionary<Mobile, AutoResPotion> m_ResList;
        private static readonly object _lock = new object();

        private int m_Charges;
        private Mobile m_Consumer;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; InvalidateProperties(); }
        }

        private Timer m_Timer;
        private static TimeSpan m_Delay = TimeSpan.FromSeconds(30.0); /*TimeSpan.Zero*/

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan Delay { get { return m_Delay; } set { m_Delay = value; } }

        public static void Initialize()
        {
            EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_Death);
			EventSink.Login += new LoginEventHandler(e => 
                {
                    PlayerMobile owner = e.Mobile as PlayerMobile;
                    if (owner == null) return;

                    AutoResPotion arp;
                    if (m_ResList == null || !m_ResList.ContainsKey(owner) || !m_ResList.TryGetValue(owner, out arp)) return;

                    arp.StartTimer();
                }
            );
        }

        [Constructable]
        public AutoResPotion() : this(1)
        { }

        [Constructable]
        public AutoResPotion(int charges) : base(0x0E0F)
        {
            m_Charges = charges;
            Name = "Potion Of Rebirth";
            LootType = LootType.Blessed;
            Weight = 1.0;
            Hue = 0x494;
        }

        public AutoResPotion(Serial serial) : base(serial)
        {
        }

        public static bool IsProtected(Mobile from)
        {
            return m_ResList != null && m_ResList.ContainsKey(from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.Alive)
                return;

            if (m_ResList == null)
            {
                lock(_lock) 
                {
                    if (m_ResList == null) 
                    {
                        m_ResList = new Dictionary<Mobile, AutoResPotion>();
                    }
                }
            }

            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your backpack to use.");
                return;
            }

            const string AlreadyWatched = "The spirits watch you already.";
            if (from is PlayerMobile)
            {
                if (((PlayerMobile)from).SoulBound)
                {
                    from.SendMessage("Strange... the potion does nothing to you.");
                    return;
                }
                else if (from != null && this != null)
                {
                    string message;
                    lock(_lock)
                    {
                        if (m_ResList.ContainsKey(from))
                        {
                            message = AlreadyWatched;
                        }
                        else if (!m_ResList.ContainsValue(this))
                        {
                            m_ResList.Add(from, this);
                            m_Consumer = from;
                            message = "You feel the spirits watching you, awaiting to send you back to your body.";
                            InvalidateProperties();
                        }
                        else
                        {
                            message = "The spirits of this potion are watching another";
                        }
                    }

                    from.SendMessage(message);
                }
                else
                    from.SendMessage(AlreadyWatched);
            }
        }

        private static void EventSink_Death(PlayerDeathEventArgs e)
        {
            PlayerMobile owner = e.Mobile as PlayerMobile;

            if (owner != null && !owner.Deleted)
            {
                if (owner.Alive)
                    return;

                if (m_ResList != null)
                {
                    lock (_lock)
                    {
                        if (m_ResList != null && m_ResList.ContainsKey(owner))
                        {
                            AutoResPotion arp = m_ResList[owner];
                            if (arp == null || arp.Deleted)
                            {
                                m_ResList.Remove(owner);
                                return;
                            }

                            arp.StartTimer();
                        }
                    }
                }
            }
        }

        private void StartTimer()
        {
            Mobile owner = m_Consumer;
            if (Deleted || owner.Alive) { return; }
            
            if (m_Timer != null)
                m_Timer.Stop();

            BuffInfo.AddBuff( owner, new BuffInfo( BuffIcon.GiftOfLife, 1015222,TimeSpan.FromSeconds(30), owner, String.Format("You will resurrect within 30 seconds of your death"),true ) );
            m_Timer = Timer.DelayCall(m_Delay, new TimerStateCallback(Resurrect_OnTick), new object[] { owner, this });
        }

        private static void Resurrect_OnTick(object state)
        {
            object[] states = (object[])state;
            PlayerMobile owner = (PlayerMobile)states[0];
            AutoResPotion arp = (AutoResPotion)states[1];
            if (owner != null && !owner.Deleted && arp != null && !arp.Deleted)
            {
                if (owner.Alive || arp.m_Charges < 1)
                    return;

                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                var confirmationGump = new ConfirmationGump(
                    owner, 
                    cultInfo.ToTitleCase(arp.Name),
                    "The spirits offer their aid.<br>Do you accept?", 
                    arp.DoUse,
                    arp.StartTimer
                );
                owner.SendGump(confirmationGump);
            }
        }

        private void DoUse()
        {
            Mobile owner = m_Consumer;
            if (Deleted || owner == null || owner.Alive) { return; }
            
            owner.SendMessage("You died under the watch of the spirits, they have offered you another chance at life.");

            owner.Resurrect();
            Server.Misc.Death.Penalty( owner, false );
            BuffInfo.RemoveBuff(owner, BuffIcon.GiftOfLife);
            if (m_Timer != null)
                m_Timer.Stop();
            if (m_ResList != null) 
                m_ResList.Remove(owner);

            m_Charges--;
            InvalidateProperties();

            if (m_Charges < 1)
                Delete();
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add(1070722, "Drink Anytime Before Death");
            list.Add(1070722, "Resurrect 30 Seconds Later");
            if (m_Consumer != null)
            {
                list.Add(1049644, "This potion protects: " + m_Consumer.Name);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write((TimeSpan)m_Delay);
            writer.Write((int)m_Charges);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            switch (version)
            {
                case 0:
                    {
                        m_Delay = reader.ReadTimeSpan();
                        m_Charges = reader.ReadInt();
                    }
                    break;
            }
            Hue = 0x494;
        }
    }
}
