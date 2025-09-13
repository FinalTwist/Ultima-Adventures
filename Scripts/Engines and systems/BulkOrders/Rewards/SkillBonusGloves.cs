namespace Server.Items
{
    public class SkillBonusGloves : BaseArmor
    {
        private int m_Bonus;
        private SkillName m_Skill;
        private SkillMod m_SkillMod;

        [Constructable]
        public SkillBonusGloves(SkillName skill, int bonus) : base(0x13C6)
        {
            m_Skill = skill;
            m_Bonus = bonus;

            Weight = 1.0;
            Layer = Layer.Gloves;
            Name = "Gloves of " + skill.ToString();
        }

        public SkillBonusGloves(Serial serial) : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bonus
        {
            get
            {
                return m_Bonus;
            }
            set
            {
                m_Bonus = value;
                InvalidateProperties();

                if (m_Bonus == 0)
                {
                    if (m_SkillMod != null)
                        m_SkillMod.Remove();

                    m_SkillMod = null;
                }
                else if (m_SkillMod == null && Parent is Mobile)
                {
                    m_SkillMod = new DefaultSkillMod(m_Skill, true, m_Bonus);
                    ((Mobile)Parent).AddSkillMod(m_SkillMod);
                }
                else if (m_SkillMod != null)
                {
                    m_SkillMod.Value = m_Bonus;
                }
            }
        }

        public override ArmorMaterialType MaterialType
        { get { return ArmorMaterialType.Leather; } }

        public SkillName Skill
        {
            get
            {
                return m_Skill;
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Skill = (SkillName)reader.ReadInt();
                        m_Bonus = reader.ReadInt();
                        break;
                    }
            }

            if (m_Bonus != 0 && Parent is Mobile)
            {
                if (m_SkillMod != null)
                    m_SkillMod.Remove();

                m_SkillMod = new DefaultSkillMod(m_Skill, true, m_Bonus);
                ((Mobile)Parent).AddSkillMod(m_SkillMod);
            }
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);

            if (m_Bonus != 0) list.Add(1060451, "#{0}\t{1}", 1044060 + (int)m_Skill, m_Bonus);
        }

        public override void OnAdded(IEntity parent)
        {
            base.OnAdded(parent);

            if (m_Bonus != 0 && parent is Mobile)
            {
                if (m_SkillMod != null)
                    m_SkillMod.Remove();

                m_SkillMod = new DefaultSkillMod(m_Skill, true, m_Bonus);
                ((Mobile)parent).AddSkillMod(m_SkillMod);
            }
        }

        public override void OnRemoved(IEntity parent)
        {
            base.OnRemoved(parent);

            if (m_SkillMod != null)
                m_SkillMod.Remove();

            m_SkillMod = null;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_Skill);
            writer.Write((int)m_Bonus);
        }
    }
}