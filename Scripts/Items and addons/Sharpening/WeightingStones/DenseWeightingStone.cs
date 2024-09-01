namespace Server.Items
{
    public class DenseWeightingStone : DamageIncreaseWeightingStoneBase
    {
        protected override int MaxDamageBonus { get { return 70; } }

        public DenseWeightingStone(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public DenseWeightingStone() : this(5)
        {
        }

        [Constructable]
        public DenseWeightingStone(int uses) : base(uses)
        {
            Name = "Dense Weighting Stone";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Skills[SkillName.Blacksmith].Value < 100.0)
            {
                from.SendMessage(32, "You need at least 100 Blacksmithing to use this");
                return;
            }

            base.OnDoubleClick(from);
        }

        protected override int GetBonus(Mobile from)
        {
            return Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value / 10));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            if (IsLegacyItem) return;

            int version = reader.ReadInt();
        }
    }
}