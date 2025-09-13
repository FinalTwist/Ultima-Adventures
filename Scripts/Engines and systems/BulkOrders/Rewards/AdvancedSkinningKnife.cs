namespace Server.Items
{
    public class AdvancedSkinningKnife : SkinningKnife, IUsesRemaining
    {
        private int m_YieldBonus;
        private int m_UsesRemaining;

        [Constructable]
        public AdvancedSkinningKnife(int yieldBonus) : this(yieldBonus, 50)
        {
        }

        [Constructable]
        public AdvancedSkinningKnife(int yieldBonus, int uses) : base()
        {
            Name = "Advanced Skinning Knife";
            YieldBonus = yieldBonus;
            UsesRemaining = uses;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int YieldBonus
        {
            get { return m_YieldBonus; }
            set { m_YieldBonus = value; InvalidateProperties(); }
        }


        public bool ShowUsesRemaining
        {
            get { return true; }
            set { }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get { return m_UsesRemaining; }
            set { m_UsesRemaining = value; InvalidateProperties(); }
        }

        public AdvancedSkinningKnife(Serial serial) : base(serial)
        {
        }

        public override void AppendChildProperties(ObjectPropertyList list)
        {
            base.AppendChildProperties(list);

            list.Add("Automatically skins killed enemies");

            if (0 < m_YieldBonus)
                list.Add("Increases carving yields by {0}%", m_YieldBonus);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)m_YieldBonus);
            writer.Write((int)m_UsesRemaining);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_YieldBonus = reader.ReadInt();
            m_UsesRemaining = reader.ReadInt();
        }
    }
}
