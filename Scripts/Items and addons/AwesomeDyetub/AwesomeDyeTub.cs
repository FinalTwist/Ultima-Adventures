using Server.Gumps;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using System;

namespace Server.Items
{
    public class AwesomeDyeTub : DyeTub
    {
        public bool m_IsCycling;
        public int m_HueStop;
        public int m_HueStart;
        public Timer m_Timer;
        public double m_HueDelay;
        [CommandProperty(AccessLevel.GameMaster)]
        public new virtual bool AllowRunebooks
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostRunebookDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public new virtual bool AllowFurniture
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostFurnitureDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public new virtual bool AllowStatuettes
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostStatuettesDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public new virtual bool AllowLeather
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostLeatherDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool AllowMetal
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostMetalDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool AllowBackpack
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostBackpackDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool AllowContainer
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostContainerDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool AllowEverything
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostMiscDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public new virtual bool AllowDyables
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int CostDyablesDye
        {
            get; set;
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public override int Hue
        {
            get
            { return base.Hue; }
            set
            {
                if (DyedHue != value)
                    DyedHue = value;
                base.Hue = value;
            }
        }
        [CommandProperty(AccessLevel.Player)]
        public double HueDelay { get { return m_HueDelay; } set { m_HueDelay = value; } }
        
        [Constructable]
        public AwesomeDyeTub()
        {
            Name = "a bubbling tub";

            m_HueDelay = 0.3;
            m_HueStart = 0;
            m_HueStop = 900;
            m_IsCycling = false;
            CostDyablesDye = 5000;
        }

        public AwesomeDyeTub(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(AllowBackpack);
            writer.Write(AllowContainer);
            writer.Write(AllowDyables);
            writer.Write(AllowEverything);
            writer.Write(AllowFurniture);
            writer.Write(AllowLeather);
            writer.Write(AllowMetal);
            writer.Write(AllowRunebooks);
            writer.Write(AllowStatuettes);

            writer.Write(CostBackpackDye);
            writer.Write(CostContainerDye);
            writer.Write(CostDyablesDye);
            writer.Write(CostFurnitureDye);
            writer.Write(CostLeatherDye);
            writer.Write(CostMetalDye);
            writer.Write(CostMiscDye);
            writer.Write(CostRunebookDye);
            writer.Write(CostStatuettesDye);

            writer.Write((double)m_HueDelay);
            writer.Write((int)m_HueStart);
            writer.Write((int)m_HueStop);
            writer.Write((bool)m_IsCycling);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            AllowBackpack = reader.ReadBool();
            AllowContainer = reader.ReadBool();
            AllowDyables = reader.ReadBool();
            AllowEverything = reader.ReadBool();
            AllowFurniture = reader.ReadBool();
            AllowLeather = reader.ReadBool();
            AllowMetal = reader.ReadBool();
            AllowRunebooks = reader.ReadBool();
            AllowStatuettes = reader.ReadBool();

            CostBackpackDye = reader.ReadInt();
            CostContainerDye = reader.ReadInt();
            CostDyablesDye = reader.ReadInt();
            CostFurnitureDye = reader.ReadInt();
            CostLeatherDye = reader.ReadInt();
            CostMetalDye = reader.ReadInt();
            CostMiscDye = reader.ReadInt();
            CostRunebookDye = reader.ReadInt();
            CostStatuettesDye = reader.ReadInt();

            m_HueDelay = reader.ReadDouble();
            m_HueStart = reader.ReadInt();
            m_HueStop = reader.ReadInt();
            m_IsCycling = reader.ReadBool();

            if (m_IsCycling)
            {
                DoHueTimer();
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(this.GetWorldLocation(), 2) && from.AccessLevel < AccessLevel.GameMaster)
            {
                from.SendMessage("The dyetub is too far away...");
            }
            else
            {
                from.CloseGump(typeof(AwesomeDyeTubGump));
                from.SendGump(new AwesomeDyeTubGump(from, this));
                from.SendMessage(240, "Hey! " + from.Name + "! Use of this dyetub will cost you 5000 gold.");
            }
        }

        public void DoHueTimer()
        {
            TimeSpan next = TimeSpan.FromSeconds(this.m_HueDelay);
            m_Timer = new InternalTimer(this, next);
            m_Timer.Start();
        }
        private class InternalTimer : Timer
        {
            private AwesomeDyeTub m_item;

            private static TimeSpan TwoMinutes = TimeSpan.FromMinutes(2.0);
            private static TimeSpan ThirtySeconds = TimeSpan.FromSeconds(30.0);
            private static TimeSpan TenSeconds = TimeSpan.FromSeconds(10.0);
            private static TimeSpan OneSecond = TimeSpan.FromSeconds(1.0);

            public InternalTimer(AwesomeDyeTub item, TimeSpan next) : base(next)
            {
                m_item = item;

                //This section of assigning timer priority came from the RunUO Distro script:
                if (next >= TwoMinutes)
                    Priority = TimerPriority.OneMinute;
                else if (next >= ThirtySeconds)
                    Priority = TimerPriority.FiveSeconds;
                else if (next >= TenSeconds)
                    Priority = TimerPriority.OneSecond;
                else if (next >= OneSecond)
                    Priority = TimerPriority.TwoFiftyMS;
                else
                    Priority = TimerPriority.TwentyFiveMS;
            }
            protected override void OnTick()
            {
                if (m_item.m_IsCycling)
                {
                    if (m_item.Hue < m_item.m_HueStart)
                    {
                        m_item.Hue = m_item.m_HueStart;
                        m_item.DoHueTimer();
                        m_item.Name = "Dyetub Hue # " + m_item.Hue;
                    }
                    else if (m_item.Hue < m_item.m_HueStop)
                    {
                        m_item.Hue++;
                        m_item.DoHueTimer();
                        m_item.Name = "Dyetub Hue # " + m_item.Hue;
                    }
                    else
                    {
                        if (m_item.m_IsCycling)
                        {
                            m_item.m_IsCycling = false;
                        }

                        if (m_item.Hue != 0 && m_item.Redyable)
                        {
                            m_item.Hue = 0;
                            m_item.Name = "Dyetub Hue # " + m_item.Hue;
                        }

                        if (m_item.ItemID != 0xFAB)
                            m_item.ItemID = 0xFAB;
                    }
                }
            }
        }

        public class AwesomeDyeTubGump : Gump
        {
            AwesomeDyeTub m_CD;

            public static int NegProtection(string numberstring)
            {
                int ns = Utility.ToInt32(numberstring);

                if (ns < 0)
                    return 0;
                else
                    return ns;
            }
            public static int ButtonID(AwesomeDyeTub cd)
            {
                if (cd.m_IsCycling)
                    return 2116;
                else
                    return 2113;
            }
            public static bool CheckRange(Mobile from, AwesomeDyeTub cd)
            {
                if (!from.InRange(cd.GetWorldLocation(), 2))
                {
                    from.SendMessage("The dyetub is too far away...");
                    DoExit(from, cd);
                    return false;
                }
                else
                    return true;
            }

            public AwesomeDyeTubGump(Mobile from, AwesomeDyeTub cd) : base(0, 0)
            {
                m_CD = cd;

                this.Closable = false;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;

                this.AddPage(0);

                this.AddBackground(71, 78, 180, 92, 2620);
                this.AddBackground(71, 162, 144, 84, 2620);
                this.AddBackground(139, 238, 76, 35, 2620);

                this.AddLabel(80, 90, 2416, @"Current Hue:");
		if (cd.Redyable)
		{
		    this.AddButton(226, 90, 250, 251, (int)Buttons.CurHuePlus, GumpButtonType.Reply, 0);
		    this.AddButton(166, 90, 252, 253, (int)Buttons.CurHueMinus, GumpButtonType.Reply, 0);
		    this.AddTextEntry(187, 90, 31, 20, 2416, 1, @"" + m_CD.Hue);
		}

                this.AddButton(77, 116, 2124, 248, (int)Buttons.ApplyHue, GumpButtonType.Reply, 0);
                this.AddLabel(135, 116, 2416, @"to Target");

		this.AddLabel(80, 138, 2416, @"Preview Item");
		this.AddButton(162, 138, 2515, 2515, (int)Buttons.Preview, GumpButtonType.Reply, 0);

		if (cd.Redyable)
		{
		    this.AddButton(77, 173, ButtonID(m_CD), ButtonID(m_CD), (int)Buttons.StopStartHueCycle, GumpButtonType.Reply, 0);
		}

		this.AddLabel(cd.Redyable ? 137 : 79, 174, 2416, cd.Redyable ? @"Cycle Hues" : @"Unchangeable Hue");

		if (cd.Redyable)
		{
		    this.AddLabel(80, 195, 2416, @"Start Hue");
		    this.AddTextEntry(155, 195, 33, 20, 2416, 2, @"" + m_CD.m_HueStart);//Start hue
		    this.AddLabel(80, 215, 2416, @"Stop Hue");
		    this.AddTextEntry(155, 215, 33, 20, 2416, 3, @"" + m_CD.m_HueStop);//End hue
		}

		this.AddButton(73, 237, 1028, 1026, (int)Buttons.Exit, GumpButtonType.Reply, 0);

		if (cd.Redyable)
		{
		    this.AddLabel(145, 245, 2416, @"Get Hue");
		    this.AddButton(191, 249, 5300, 5300, (int)Buttons.GetHue, GumpButtonType.Reply, 0);
		}
		else
		{
		    this.AddLabel(145, 245, 2416, @"----------");
		}

            }

            public enum Buttons
            {
                Exit,
                CurHuePlus,
                CurHueMinus,
                ApplyHue,
                Preview,
                StopStartHueCycle,
                GetHue,
            }

            public override void OnResponse(NetState state, RelayInfo info)
            {
                if (m_CD == null)
                    return;

                Mobile from = state.Mobile;

                #region TextRelays
		if (m_CD.Redyable)
		{
		    TextRelay entry1 = info.GetTextEntry(1);
		    int CurHue = NegProtection(entry1 == null ? "" : entry1.Text.Trim());
		    if (CurHue > 2999)
			from.SendMessage("There are no hues above 2999.");
		    else if (!(CurHue == m_CD.Hue) && !m_CD.m_IsCycling)
			m_CD.Hue = CurHue;

		    TextRelay entry2 = info.GetTextEntry(2);
		    int HueStart = NegProtection(entry2 == null ? "" : entry2.Text.Trim());
		    if (!(HueStart == m_CD.m_HueStart))
			m_CD.m_HueStart = HueStart;

		    TextRelay entry3 = info.GetTextEntry(3);
		    int HueStop = NegProtection(entry3 == null ? "" : entry3.Text.Trim());
		    if (HueStop > 2999)
			from.SendMessage("There are no hues above 2999.");
		    else if (!(HueStop == m_CD.m_HueStop))
			m_CD.m_HueStop = HueStop;
		}
                #endregion

                #region switch ( info.ButtonID )
                switch (info.ButtonID)
                {
                    case (int)Buttons.Exit: DoExit(from, m_CD); break;
                    case (int)Buttons.CurHuePlus: DoCurHuePlus(from, m_CD); break;
                    case (int)Buttons.CurHueMinus: DoCurHueMinus(from, m_CD); break;
                    case (int)Buttons.ApplyHue: from.Target = new DoApplyHue(m_CD); break;
                    case (int)Buttons.Preview: from.Target = new DoPreview(m_CD); break;
                    case (int)Buttons.StopStartHueCycle: DoStopStartHueCycle(from, m_CD); break;
                    case (int)Buttons.GetHue: from.Target = new DoGetHue(m_CD); break;
                }
                #endregion
            }

            public static void DoExit(Mobile from, AwesomeDyeTub cd)
            {
                if (cd.m_IsCycling)
                {
                    cd.m_IsCycling = false;
                    from.EndAction(typeof(AwesomeDyeTub));
                }

                if (cd.Hue != 0 && cd.Redyable)
                {
                    cd.Hue = 0;
                    cd.Name = "Dyetub Hue # " + cd.Hue;
                }

                if (cd.ItemID != 0xFAB)
                    cd.ItemID = 0xFAB;

		if (!cd.Redyable)
		{
		    cd.m_HueStart = cd.Hue;
		    cd.m_HueStop = cd.Hue;
		}

                from.CloseGump(typeof(AwesomeDyeTubGump));
            }
            public void DoCurHuePlus(Mobile from, AwesomeDyeTub cd)
            {
                if (!CheckRange(from, cd))
                    return;

                if (cd.Hue > 2998)
                {
                    from.SendMessage("There are no hues above 2999.");
                }
                else if (!cd.m_IsCycling)
                {
                    cd.Hue++;
                }
                else
                {
                    cd.m_IsCycling = false;
                    from.EndAction(typeof(AwesomeDyeTub));

                    cd.Hue++;
                }

                cd.Name = "Dyetub Hue # " + cd.Hue;
                from.CloseGump(typeof(AwesomeDyeTubGump));
                from.SendGump(new AwesomeDyeTubGump(from, cd));
            }
            public void DoCurHueMinus(Mobile from, AwesomeDyeTub cd)
            {
                if (!CheckRange(from, m_CD))
                    return;

                if (!cd.m_IsCycling)
                {
                    if (cd.Hue >= 1)
                        cd.Hue--;
                }
                else
                {
                    cd.m_IsCycling = false;
                    from.EndAction(typeof(AwesomeDyeTub));

                    if (cd.Hue >= 1)
                        cd.Hue--;
                }

                cd.Name = "Dyetub Hue # " + cd.Hue;
                from.CloseGump(typeof(AwesomeDyeTubGump));
                from.SendGump(new AwesomeDyeTubGump(from, cd));
            }
            public class DoApplyHue : Target
            {
                Item item = null;
                AwesomeDyeTub m_CD;

                private void Dye(Mobile from, Item item, int cost)
                {
                    if (from.AccessLevel >= AccessLevel.Counselor || TakePayment(from, m_CD, cost))
                    {
                        item.Hue = m_CD.DyedHue;
                        from.PlaySound(0x23E);
                    }
                }
                public DoApplyHue(AwesomeDyeTub cd) : base(4, false, TargetFlags.None)
                {
                    m_CD = cd;
                }
                protected override void OnTarget(Mobile from, object targeted)
                {
                    if (!CheckRange(from, m_CD))
                        return;

		    if (!(targeted is Item))
		    {
                        from.SendLocalizedMessage(m_CD.FailMessage);
			return;
		    }

                    Item item = (Item)targeted;

                    if (item is IDyable && !(item is Container) && (m_CD.AllowDyables || m_CD.AllowEverything))
                    {
                        if (!from.InRange(m_CD.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                            from.SendLocalizedMessage(500446); // That is too far away.
                        else if (item.Parent is Mobile)
                            from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
                        else if (((IDyable)item).Dye(from, m_CD))
                        {
                            Dye(from, item, m_CD.CostDyablesDye);
                        }
                    }
                    else if ((FurnitureAttribute.Check(item) || (item is PotionKeg)) && (m_CD.AllowFurniture || m_CD.AllowEverything))
                    {
                        if (!from.InRange(m_CD.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else
                        {
                            bool okay = (item.IsChildOf(from.Backpack));

                            if (!okay)
                            {
                                if (item.Parent == null)
                                {
                                    BaseHouse house = BaseHouse.FindHouseAt(item);

                                    if (house == null || (!house.IsLockedDown(item) && !house.IsSecure(item)))
                                        from.SendLocalizedMessage(501022); // Furniture must be locked down to paint it.
                                    else if (!house.IsCoOwner(from))
                                        from.SendLocalizedMessage(501023); // You must be the owner to use this item.
                                    else
                                        okay = true;
                                }
                                else
                                {
                                    from.SendLocalizedMessage(1048135); // The furniture must be in your backpack to be painted.
                                }
                            }
                            if (okay)
                            {
                                Dye(from, item, m_CD.CostFurnitureDye);
                            }
                        }
                    }
                    else if ((item is Runebook || item is RecallRune) && (m_CD.AllowRunebooks || m_CD.AllowEverything))
                    {
                        if (!from.InRange(m_CD.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else if (!item.Movable)
                        {
                            from.SendLocalizedMessage(1049776); // You cannot dye runes or runebooks that are locked down.
                        }
                        else
                        {
                            Dye(from, item, m_CD.CostRunebookDye);
                        }
                    }
                    else if (item is MonsterStatuette && (m_CD.AllowStatuettes || m_CD.AllowEverything))
                    {
                        if (!from.InRange(m_CD.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else if (!item.Movable)
                        {
                            from.SendLocalizedMessage(1049779); // You cannot dye statuettes that are locked down.
                        }
                        else
                        {
                            Dye(from, item, m_CD.CostStatuettesDye);
                        }
                    }
                    else if (item is BaseArmor)
                    {
                        BaseArmor ba = item as BaseArmor;
                        if (((ba.MaterialType == ArmorMaterialType.Leather || ba.MaterialType == ArmorMaterialType.Studded) || item is ElvenBoots ||  m_CD.AllowEverything))
                        {
                            if (!from.InRange(m_CD.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                            {
                                from.SendLocalizedMessage(500446); // That is too far away.
                            }
                            else if (!item.Movable)
                            {
                                from.SendLocalizedMessage(1042419); // You may not dye leather items which are locked down.
                            }
                            else if (item.Parent is Mobile)
                            {
                                from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
                            }
                            else
                            {
                                Dye(from, item, m_CD.CostLeatherDye);
                            }
                        }
                        else if ((ba.MaterialType == ArmorMaterialType.Ringmail || ba.MaterialType == ArmorMaterialType.Chainmail || ba.MaterialType == ArmorMaterialType.Plate) && (m_CD.AllowMetal || m_CD.AllowEverything))
                        {
                            if (!from.InRange(m_CD.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                            {
                                from.SendLocalizedMessage(500446); // That is too far away.
                            }
                            else if (!item.Movable)
                            {
                                from.SendLocalizedMessage(1010093); // You may not dye items which are locked down.
                            }
                            else if (item.Parent is Mobile)
                            {
                                from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
                            }
                            else
                            {
                                Dye(from, item, m_CD.CostMetalDye);
                            }
                        }
                    }
                    else if (item is Container)
                    {
                        Container c = item as Container;
                        if (c is Backpack && c.Parent == from && (m_CD.AllowBackpack || m_CD.AllowEverything))
                        {
                            Dye(from, item, m_CD.CostBackpackDye);
                        }
                        else if (!from.InRange(m_CD.GetWorldLocation(), 1) || !from.InRange(c.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else if (!c.Movable)
                        {
                            from.SendLocalizedMessage(1010093); // You may not dye items which are locked down.
                        }
                        else if (m_CD.AllowContainer || m_CD.AllowEverything)
                        {
                            Dye(from, item, m_CD.CostContainerDye);
                        }
                    }
                    else if (m_CD.AllowEverything)
                    {
                        if (!from.InRange(m_CD.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else if (!item.Movable)
                        {
                            from.SendLocalizedMessage(1010093); // You may not dye items which are locked down.
                        }
                        else if (item.Parent is Mobile)
                        {
                            from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
                        }
                        else
                        {
                            Dye(from, item, m_CD.CostMiscDye);
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(m_CD.FailMessage);
                    }
                    from.CloseGump(typeof(AwesomeDyeTubGump));
                    from.SendGump(new AwesomeDyeTubGump(from, m_CD));
                }
            }
            public class DoPreview : Target
            {
                Item i = null;
                AwesomeDyeTub m_CD;

                public DoPreview(AwesomeDyeTub cd) : base(4, false, TargetFlags.None)
                {
                    m_CD = cd;
                }
                protected override void OnTarget(Mobile from, object targeted)
                {
                    if (!CheckRange(from, m_CD))
                        return;

                    if (targeted is Item)
                    {
                        i = (Item)targeted;
                        if (i == m_CD)
                        {
                            m_CD.ItemID = 0xFAB;
                        }
                        else if (from.AccessLevel > AccessLevel.Player)
                        {
                            m_CD.ItemID = i.ItemID;
                        }
                        else if (i.Parent == from && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            m_CD.ItemID = i.ItemID;
                        }
                        else
                            from.SendMessage("That is not in your posession.");
                    }
                    else
                        from.SendMessage("You can only preview items.");

                    from.CloseGump(typeof(AwesomeDyeTubGump));
                    from.SendGump(new AwesomeDyeTubGump(from, m_CD));
                }
            }
            public void DoStopStartHueCycle(Mobile from, AwesomeDyeTub cd)
            {
                if (!CheckRange(from, cd))
                    return;

                if (!cd.m_IsCycling)
                {
                    cd.m_IsCycling = true;
                    cd.DoHueTimer();
                }
                else
                {
                    cd.m_IsCycling = false;
                    from.EndAction(typeof(AwesomeDyeTub));
                }

                from.CloseGump(typeof(AwesomeDyeTubGump));
                from.SendGump(new AwesomeDyeTubGump(from, m_CD));
            }
            public class DoGetHue : Target
            {
                Item i = null;
                Mobile mob = null;
                AwesomeDyeTub m_CD;

                public DoGetHue(AwesomeDyeTub cd) : base(4, false, TargetFlags.None)
                {
                    m_CD = cd;
                }
                protected override void OnTarget(Mobile from, object targeted)
                {
                    if (targeted is Item)
                    {
                        i = (Item)targeted;
                        if (i.Hue != 0)
                        {
                            m_CD.Hue = i.Hue;
                            from.SendMessage("Hue # " + i.Hue + " assigned to dyetub.");
                        }
                        else
                            from.SendMessage("That is just a natural zero hue, sorry!");
                    }
                    else if (targeted is Mobile)
                    {
                        mob = (Mobile)targeted;
                        if (mob.Hue != 0)
                        {
                            m_CD.Hue = mob.Hue;
                            from.SendMessage("Hue # " + mob.Hue + " assigned to dyetub.");
                        }
                        else
                            from.SendMessage("That is just a natural zero hue, sorry!");
                    }
                    else
                        from.SendMessage("Items or Mobiles only please.");

                    from.CloseGump(typeof(AwesomeDyeTubGump));
                    from.SendGump(new AwesomeDyeTubGump(from, m_CD));
                }
            }

            #region HasCost functions
            public static Type PayType()
            {
                return typeof(Gold);
            }
            public static string PayTypeName()
            {
                return "gold";
            }
            public static void AlertPlayerOfCost(Mobile from, int cost)
            {
                if (from.AccessLevel < AccessLevel.GameMaster)
                {
                    from.SendMessage(240, "Hey! " + from.Name + "! Use of this dyetub will cost you " + cost + " " + PayTypeName()+".");
                    from.SendMessage(269, "Hit escape to exit this transaction or target again to confirm.");
                }
            }
            public static bool TakePayment(Mobile from, AwesomeDyeTub cd, int cost)
            {

				int i_Bank = Banker.GetBalance( from );
				int i_Total = cost;
							
				Container bank = from.FindBankNoCreate();
				
				if ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), i_Total ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), i_Total ) ) )
					return true;
                else
                {
                    from.SendMessage("Begging thy pardon, but thou can not afford that.");
                    from.SendMessage("The cost for this item is "+cost+" "+PayTypeName()+".");
                }
				/*
                Container bp = from.Backpack;
                if (from.AccessLevel >= AccessLevel.Counselor)
                    return true;
                if(bp != null)
                {
                    if (bp.ConsumeTotal(PayType(), cost))
                    {
                        return true;
                    }
                    else
                    {
                        from.SendMessage("Begging thy pardon, but thou can not afford that.");
                        from.SendMessage("The cost for this item is "+cost+" "+PayTypeName()+".");
                    }
                }*/
                return false;            
            }
            #endregion
        }
    }
}
