using System.Collections;
using System.Collections.Generic;
using Server;
using System;
using Server.Mobiles;
namespace Custom.Jerbal.Jako
{
    #region Enums
    [Flags]
    public enum JakoAttributesEnum
    {
        None = 0x00000000,
        Hits = 0x00000001,
        Stam = 0x00000002,
        Mana = 0x00000004,
        BonusPhysResist = 0x00000008,
        BonusFireResist = 0x00000010,
        BonusColdResist = 0x00000020,
        BonusEnerResist = 0x00000040,
        BonusPoisResist = 0x00000080
    }
    #endregion

    public class JakoAttributes
    {
        private Hashtable m_attributes = new Hashtable();
        public JakoAttributes()
        {
            m_attributes.Add(JakoAttributesEnum.Hits, new JakoHitsAttribute());
            m_attributes.Add(JakoAttributesEnum.Stam, new JakoStamAttribute());
            m_attributes.Add(JakoAttributesEnum.Mana, new JakoManaAttribute());
            m_attributes.Add(JakoAttributesEnum.BonusPhysResist, new JakoRPhyAttribute());
            m_attributes.Add(JakoAttributesEnum.BonusFireResist, new JakoRFirAttribute());
            m_attributes.Add(JakoAttributesEnum.BonusColdResist, new JakoRColAttribute());
            m_attributes.Add(JakoAttributesEnum.BonusEnerResist, new JakoREneAttribute());
            m_attributes.Add(JakoAttributesEnum.BonusPoisResist, new JakoRPoiAttribute());
        }


        public JakoBaseAttribute GetAttribute(JakoAttributesEnum value)
        {
            if (m_attributes.ContainsKey(value))
                return (JakoBaseAttribute)m_attributes[value];
            return null;
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public virtual JakoBaseAttribute Hits
        {
            get
            {
                return GetAttribute(JakoAttributesEnum.Hits);
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual JakoBaseAttribute Stam
        {
            get
            {
                return GetAttribute(JakoAttributesEnum.Stam);
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual JakoBaseAttribute Mana
        {
            get
            {
                return GetAttribute(JakoAttributesEnum.Mana);
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual JakoBaseAttribute PhysicalResist
        {
            get
            {
                return GetAttribute(JakoAttributesEnum.BonusPhysResist);
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual JakoBaseAttribute FireResist
        {
            get
            {
                return GetAttribute(JakoAttributesEnum.BonusFireResist);
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual JakoBaseAttribute ColdResist
        {
            get
            {
                return GetAttribute(JakoAttributesEnum.BonusColdResist);
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual JakoBaseAttribute EnergyResist
        {
            get
            {
                return GetAttribute(JakoAttributesEnum.BonusEnerResist);
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual JakoBaseAttribute PoisonResist
        {
            get
            {
                return GetAttribute(JakoAttributesEnum.BonusPoisResist);
            }
        }


        /// <summary>
        /// Increase the given attribute byThis.</summary>
        /// <paramref name="att"/> Attribute by JakoAtributesEnum.</param>
        /// <param name="bc"/> The Creature we are increasing the stats for.</param>
        /// <param name="byThis"/> The int amount of the increase/decrease.</param>
        public void IncBonus(JakoAttributesEnum att, BaseCreature bc, int byThis, bool special)
        {
            GetAttribute(att).IncBonus(bc, (uint)byThis, special);
        }

        /// <summary>
        /// Sets the specified Bonus and the mobile's attribute accordingly.</summary>
        /// <paramref name="att"/> Attribute by JakoAttributes.</param>
        /// <param name="bc"/> The Creature we are increasing the stats for.</param>
        /// <paramref name="value"/> How much to increase or decrease the current attribute by.</param>
        public void SetBonus(JakoAttributesEnum att, BaseCreature bc, int value, bool special)
        {
            GetAttribute(att).SetBonus(bc, (uint)value, special);
        }


        #region Serial Functions/Flags
        private static void SetSaveFlag(ref JakoAttributesEnum flags, JakoAttributesEnum toSet, bool setIf)
        {
            if (setIf)
                flags |= toSet;
        }

        private static bool GetSaveFlag(JakoAttributesEnum flags, JakoAttributesEnum toGet)
        {
            return ((flags & toGet) != 0);
        }

        public virtual void Serialize(GenericWriter writer)
        {

            writer.Write(1);//Version

            JakoAttributesEnum flags = JakoAttributesEnum.None;
            SetSaveFlag(ref flags, JakoAttributesEnum.Hits, GetAttribute(JakoAttributesEnum.Hits).IncreasedBy != 0);
            SetSaveFlag(ref flags, JakoAttributesEnum.Stam, GetAttribute(JakoAttributesEnum.Stam).IncreasedBy != 0);
            SetSaveFlag(ref flags, JakoAttributesEnum.Mana, GetAttribute(JakoAttributesEnum.Mana).IncreasedBy != 0);
            SetSaveFlag(ref flags, JakoAttributesEnum.BonusPhysResist, GetAttribute(JakoAttributesEnum.BonusPhysResist).IncreasedBy != 0);
            SetSaveFlag(ref flags, JakoAttributesEnum.BonusFireResist, GetAttribute(JakoAttributesEnum.BonusFireResist).IncreasedBy != 0);
            SetSaveFlag(ref flags, JakoAttributesEnum.BonusColdResist, GetAttribute(JakoAttributesEnum.BonusColdResist).IncreasedBy != 0);
            SetSaveFlag(ref flags, JakoAttributesEnum.BonusEnerResist, GetAttribute(JakoAttributesEnum.BonusEnerResist).IncreasedBy != 0);
            SetSaveFlag(ref flags, JakoAttributesEnum.BonusPoisResist, GetAttribute(JakoAttributesEnum.BonusPoisResist).IncreasedBy != 0);
            writer.WriteEncodedInt((int)flags);

            if (GetSaveFlag(flags, JakoAttributesEnum.Hits))
                GetAttribute(JakoAttributesEnum.Hits).Serialize(writer);

            if (GetSaveFlag(flags, JakoAttributesEnum.Stam))
                GetAttribute(JakoAttributesEnum.Stam).Serialize(writer);

            if (GetSaveFlag(flags, JakoAttributesEnum.Mana))
                GetAttribute(JakoAttributesEnum.Mana).Serialize(writer);

            if (GetSaveFlag(flags, JakoAttributesEnum.BonusPhysResist))
                GetAttribute(JakoAttributesEnum.BonusPhysResist).Serialize(writer);

            if (GetSaveFlag(flags, JakoAttributesEnum.BonusFireResist))
                GetAttribute(JakoAttributesEnum.BonusFireResist).Serialize(writer);

            if (GetSaveFlag(flags, JakoAttributesEnum.BonusColdResist))
                GetAttribute(JakoAttributesEnum.BonusColdResist).Serialize(writer);

            if (GetSaveFlag(flags, JakoAttributesEnum.BonusEnerResist))
                GetAttribute(JakoAttributesEnum.BonusEnerResist).Serialize(writer);

            if (GetSaveFlag(flags, JakoAttributesEnum.BonusPoisResist))
                GetAttribute(JakoAttributesEnum.BonusPoisResist).Serialize(writer);


        }

        public virtual void Deserialize(GenericReader reader)
        {
            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        JakoAttributesEnum flags = (JakoAttributesEnum)reader.ReadEncodedInt();
                        if (GetSaveFlag(flags, JakoAttributesEnum.Hits))
                            GetAttribute(JakoAttributesEnum.Hits).Deserialize(reader);

                        if (GetSaveFlag(flags, JakoAttributesEnum.Stam))
                            GetAttribute(JakoAttributesEnum.Stam).Deserialize(reader);

                        if (GetSaveFlag(flags, JakoAttributesEnum.Mana))
                            GetAttribute(JakoAttributesEnum.Mana).Deserialize(reader);

                        if (GetSaveFlag(flags, JakoAttributesEnum.BonusPhysResist))
                            GetAttribute(JakoAttributesEnum.BonusPhysResist).Deserialize(reader);

                        if (GetSaveFlag(flags, JakoAttributesEnum.BonusFireResist))
                            GetAttribute(JakoAttributesEnum.BonusFireResist).Deserialize(reader);

                        if (GetSaveFlag(flags, JakoAttributesEnum.BonusColdResist))
                            GetAttribute(JakoAttributesEnum.BonusColdResist).Deserialize(reader);

                        if (GetSaveFlag(flags, JakoAttributesEnum.BonusEnerResist))
                            GetAttribute(JakoAttributesEnum.BonusEnerResist).Deserialize(reader);

                        if (GetSaveFlag(flags, JakoAttributesEnum.BonusPoisResist))
                            GetAttribute(JakoAttributesEnum.BonusPoisResist).Deserialize(reader);
                        break;
                    }

            }
        }
        #endregion

        public override string ToString()
        {
            return "...";
        }
    }
}