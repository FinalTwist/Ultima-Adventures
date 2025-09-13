namespace Server.Items
{
    public class HeavyWeightingStone : DamageIncreaseWeightingStoneBase
    {
        protected override int MaxDamageBonus { get { return 65; } }

        public HeavyWeightingStone(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public HeavyWeightingStone() : this(5)
        {
        }

        [Constructable]
        public HeavyWeightingStone(int uses) : base(uses)
        {
            Name = "Heavy Weighting Stone";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Skills[SkillName.Blacksmith].Value < 80)
            {
                from.SendMessage(32, "You need at least 80 Blacksmithing to use this");
                return;
            }

            base.OnDoubleClick(from);
        }

        protected override int GetBonus(Mobile from)
        {
            return Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value / 20));
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