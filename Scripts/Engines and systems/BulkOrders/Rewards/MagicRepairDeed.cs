using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class MagicRepairDeed : Item, IRepairDeed
    {
        private int m_SkillBonus;

        public MagicRepairDeed(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public MagicRepairDeed() : this(0)
        {
        }

        [Constructable]
        public MagicRepairDeed(int skillBonus) : base(0x14F0)
        {
            Name = "Magical repair deed";
            SkillBonus = skillBonus;
            Hue = 0x1BC;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double SkillBonus
        {
            get { return m_SkillBonus; }
            set { m_SkillBonus = (int)Math.Max(Math.Min(value, 25), 0); InvalidateProperties(); }
        }

        public double SkillLevel
        {
            get { return 100 + SkillBonus; }
        }

        public bool Check(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
                from.SendLocalizedMessage(1047012); // The contract must be in your backpack to use it.
            else
                return true;

            return false;
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_SkillBonus = reader.ReadInt();
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Usable anywhere");
            list.Add("Skill level: {0}", (int)SkillLevel);
        }

        public override void OnDoubleClick(Mobile from)
        {
            Repair.Do(from, this);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(m_SkillBonus);
        }
    }
}