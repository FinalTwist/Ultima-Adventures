/*  Author:         Rilian aka Taryen
 *  Date:           1/2/2011
 *  File Name:      KeywordDoor.cs
 *  Description:    A door that is opened once a keyword or phrase is said. The word or phrase can be 
 *              modified as well as many other aspects of the door's behavior.
*/

using System;

using Server;
using Server.Spells;

namespace Server.Items
{
    public enum DoorSet
    {
        StrongWoodDoor,
        BarredMetalDoor,
        BarredMetalDoor2,
        DarkWoodDoor,
        DarkWoodGate,
        IronGate,
        IronGateShort,
        LightWoodDoor,
        LightWoodGate,
        MediumWoodDoor,
        MetalDoor,
        MetalDoor2,
        RattanDoor,
        SecretDungeonDoor,
        SecretWoodenDoor,
        SecretLightWoodDoor,
        SecretStoneDoor1,
        SecretStoneDoor2,
        SecretStoneDoor3
    }

    public class KeywordDoor : BaseDoor
    {
        #region Properties
        private bool m_Active, m_Creatures, m_CombatCheck, m_QuickClose;
        private DoorSet m_Doors;
        private string m_Substring;
        private int m_Keyword;
        private int m_Range;
        private int m_CloseDelay;
        private int m_FaceValue;

        private Timer m_Timer;
        private static TimeSpan CombatDelay = TimeSpan.FromSeconds(10.0);

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active
        {
            get { return m_Active; }
            set { m_Active = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Creatures
        {
            get { return m_Creatures; }
            set { m_Creatures = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CombatCheck
        {
            get { return m_CombatCheck; }
            set { m_CombatCheck = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int CloseDelay
        {
            get { return m_CloseDelay; }
            set { m_CloseDelay = value; m_Timer = new InternalTimer(this, m_CloseDelay); InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DoorSet Doors
        {
            get { return m_Doors; }
            set { m_Doors = value; ChangeDoorSet(); InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Keyword
        {
            get { return m_Keyword; }
            set { m_Keyword = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Range
        {
            get { return m_Range; }
            set { m_Range = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Substring
        {
            get { return m_Substring; }
            set { m_Substring = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool QuickClose
        {
            get { return m_QuickClose; }
            set { m_QuickClose = value; InvalidateProperties(); }
        }
        #endregion

        public override bool HandlesOnSpeech { get { return true; } }

        [Constructable]
        public KeywordDoor(DoorFacing facing)
            : base(0x6E5 + (2 * (int)facing), 0x6E6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset(facing))
        {
            this.Locked = true;
            m_Active = true;
            m_Keyword = -1;
            m_Substring = "Open";
            m_CloseDelay = 10;
            m_FaceValue = (int)facing;
            m_Range = 3;

            m_Timer = new InternalTimer(this, m_CloseDelay);
        }

        public KeywordDoor(Serial serial)
            : base(serial)
        {
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && m_Active)
            {
                Mobile m = e.Mobile;

                if (this.Open)
                    return;

                if (!Creatures && !m.Player)
                    return;

                if (m_CombatCheck && SpellHelper.CheckCombat(m))
                {
                    m.SendMessage(1154, "I cannot open to those in battle.");
                    e.Handled = true;
                    return;
                }

                if (!m.InRange(GetWorldLocation(), m_Range))
                    return;

                bool isMatch = false;

                if (m_Keyword >= 0 && e.HasKeyword(m_Keyword))
                    isMatch = true;
                else if (m_Substring != null && e.Speech.Contains(m_Substring))
                    isMatch = true;

                if (!isMatch)
                    return;

                e.Handled = true;
                this.Open = true;
                m.SendMessage(1154, "The door has been opened.");

                if(m_QuickClose)
                    m_Timer.Start();
            }
        }

        #region Door Sets
        public void ChangeDoorSet()
        {
            switch (Doors)
            {
                case DoorSet.IronGateShort :
                    this.ClosedID = 0x84c + (2 * m_FaceValue);
                    this.OpenedID = 0x84d + (2 * m_FaceValue);
                    this.OpenedSound = 0xEC;
                    this.ClosedSound = 0xF3;
                    break;
                case DoorSet.IronGate :
                    this.ClosedID = 0x824 + (2 * m_FaceValue);
                    this.OpenedID = 0x825 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEC;
                    this.ClosedSound = 0xF3;
                    break;
                case DoorSet.LightWoodGate :
                    this.ClosedID = 0x839 + (2 * m_FaceValue);
                    this.OpenedID = 0x83A + (2 * m_FaceValue);
                    this.OpenedSound = 0xEB;
                    this.ClosedSound = 0xF2;
                    break;
                case DoorSet.DarkWoodGate :
                    this.ClosedID = 0x866 + (2 * m_FaceValue);
                    this.OpenedID = 0x867 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEB;
                    this.ClosedSound = 0xF2;
                    break;
                case DoorSet.MetalDoor :
                    this.ClosedID = 0x675 + (2 * m_FaceValue);
                    this.OpenedID = 0x676 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEC;
                    this.ClosedSound = 0xF3;
                    break;
                case DoorSet.BarredMetalDoor :
                    this.ClosedID = 0x685 + (2 * m_FaceValue);
                    this.OpenedID = 0x686 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEC;
                    this.ClosedSound = 0xF3;
                    break;
                case DoorSet.BarredMetalDoor2 :
                    this.ClosedID = 0x1FED + (2 * m_FaceValue);
                    this.OpenedID = 0x1FEE + (2 * m_FaceValue);
                    this.OpenedSound = 0xEC;
                    this.ClosedSound = 0xF3;
                    break;
                case DoorSet.RattanDoor :
                    this.ClosedID = 0x695 + (2 * m_FaceValue);
                    this.OpenedID = 0x696 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEB;
                    this.ClosedSound = 0xF2;
                    break;
                case DoorSet.DarkWoodDoor :
                    this.ClosedID = 0x6A5 + (2 * m_FaceValue);
                    this.OpenedID = 0x6A6 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEA;
                    this.ClosedSound = 0xF1;
                    break;
                case DoorSet.MediumWoodDoor :
                    this.ClosedID = 0x6B5 + (2 * m_FaceValue);
                    this.OpenedID = 0x6B6 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEA;
                    this.ClosedSound = 0xF1;
                    break;
                case DoorSet.MetalDoor2 :
                    this.ClosedID = 0x6C5 + (2 * m_FaceValue);
                    this.OpenedID = 0x6C6 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEC;
                    this.ClosedSound = 0xF3;
                    break;
                case DoorSet.LightWoodDoor :
                    this.ClosedID = 0x6D5 + (2 * m_FaceValue);
                    this.OpenedID = 0x6D6 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEA;
                    this.ClosedSound = 0xF1;
                    break;
                case DoorSet.StrongWoodDoor :
                    this.ClosedID = 0x6E5 + (2 * m_FaceValue);
                    this.OpenedID = 0x6E6 + (2 * m_FaceValue);
                    this.OpenedSound = 0xEA;
                    this.ClosedSound = 0xF1;
                    break;
                case DoorSet.SecretStoneDoor1 :
                    this.ClosedID = 0xE8 + (2 * m_FaceValue);
                    this.OpenedID = 0xE9 + (2 * m_FaceValue);
                    this.OpenedSound = 0xED;
                    this.ClosedSound = 0xF4;
                    break;
                case DoorSet.SecretDungeonDoor :
                    this.ClosedID = 0x314 + (2 * m_FaceValue);
                    this.OpenedID = 0x315 + (2 * m_FaceValue);
                    this.OpenedSound = 0xED;
                    this.ClosedSound = 0xF4;
                    break;
                case DoorSet.SecretStoneDoor2 :
                    this.ClosedID = 0x324 + (2 * m_FaceValue);
                    this.OpenedID = 0x325 + (2 * m_FaceValue);
                    this.OpenedSound = 0xED;
                    this.ClosedSound = 0xF4;
                    break;
                case DoorSet.SecretWoodenDoor :
                    this.ClosedID = 0x334 + (2 * m_FaceValue);
                    this.OpenedID = 0x335 + (2 * m_FaceValue);
                    this.OpenedSound = 0xED;
                    this.ClosedSound = 0xF4;
                    break;
                case DoorSet.SecretLightWoodDoor :
                    this.ClosedID = 0x344 + (2 * m_FaceValue);
                    this.OpenedID = 0x345 + (2 * m_FaceValue);
                    this.OpenedSound = 0xED;
                    this.ClosedSound = 0xF4;
                    break;
                case DoorSet.SecretStoneDoor3 :
                    this.ClosedID = 0x354 + (2 * m_FaceValue);
                    this.OpenedID = 0x355 + (2 * m_FaceValue);
                    this.OpenedSound = 0xED;
                    this.ClosedSound = 0xF4;
                    break;
            }

            if (this.Open)
                this.ItemID = this.OpenedID;
            else
                this.ItemID = this.ClosedID;
        }
        #endregion

        private class InternalTimer : Timer
        {
            private BaseDoor m_Door;

            public InternalTimer(BaseDoor door, int CloseDelay) : base(TimeSpan.FromSeconds(20.0), TimeSpan.FromSeconds(10.0))
            {
                this.Interval = TimeSpan.FromSeconds(0.0);
                this.Delay = TimeSpan.FromSeconds(CloseDelay);
                Priority = TimerPriority.OneSecond;
                m_Door = door;
            }

            protected override void OnTick()
            {
                if (m_Door.Open && m_Door.IsFreeToClose())
                {
                    m_Door.Open = false;
                    this.Stop();
                }
            }
        }

        #region Serialization
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(m_Active);
            writer.Write(m_Creatures);
            writer.Write(m_CombatCheck);
            writer.Write(m_CloseDelay);
            writer.Write((int)m_Doors);
            writer.Write(m_FaceValue);
            writer.Write(m_Keyword);
            writer.Write(m_Range);
            writer.Write(m_Substring);
            writer.Write(m_QuickClose);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Active = reader.ReadBool();
                        m_Creatures = reader.ReadBool();
                        m_CombatCheck = reader.ReadBool();
                        m_CloseDelay = reader.ReadInt();
                        m_Doors = (DoorSet)reader.ReadInt();
                        m_FaceValue = reader.ReadInt();
                        m_Keyword = reader.ReadInt();
                        m_Range = reader.ReadInt();
                        m_Substring = reader.ReadString();
                        m_QuickClose = reader.ReadBool();

                        m_Timer = new InternalTimer(this, m_CloseDelay);

                        if (this.Open)
                            m_Timer.Start();

                        break;
                    }
            }
        }
        #endregion
    }
}