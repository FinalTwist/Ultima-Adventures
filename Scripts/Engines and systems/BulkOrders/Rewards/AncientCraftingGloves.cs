using Server.Engines.Craft;

namespace Server.Items
{
    public class AncientCraftingGloves : BaseArmor, IMainHandGloves
    {
        private int m_Bonus;
        private int m_Charges;
        private SkillName m_Skill;
        private SkillMod m_SkillMod;

        [Constructable]
        public AncientCraftingGloves(SkillName skill, int bonus) : this(skill, bonus, 20)
        {
        }

        [Constructable]
        public AncientCraftingGloves(SkillName skill, int bonus, int charges) : base(0x13C6)
        {
            m_Skill = skill;
            m_Bonus = bonus;
            m_Charges = charges;

            Weight = 1.0;
            Hue = 0x482;
            Layer = Layer.OneHanded;
            Name = "Ancient " + skill.ToString() + " Gloves";
        }

        public AncientCraftingGloves(Serial serial) : base(serial)
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

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get
            {
                return m_Charges;
            }
            set
            {
                m_Charges = value;
                InvalidateProperties();
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

        public bool ConsumeCharge(Mobile from, CraftSystem craftSystem)
        {
            if (craftSystem.MainSkill != m_Skill) return false;
            if (Charges < 1) return false;

            if (--Charges < 1)
            {
                from.SendMessage("The magic in the gloves runs out and they crumble into dust.");
                Delete();
            }

            return true;
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
                        m_Charges = reader.ReadInt();
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


            list.Add(1075217, Charges.ToString()); // ~1_val~ charges remaining
            if (m_Bonus != 0) list.Add(1060451, "#{0}\t{1}", 1044060 + (int)m_Skill, m_Bonus);
        }

        public override void OnAdded(IEntity parent)
        {
            base.OnAdded(parent);

            if (m_Bonus != 0 && parent is Mobile && 0 < Charges)
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
            writer.Write((int)m_Charges);
        }
    }
}