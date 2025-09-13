namespace Server.Items
{
    public class HeavySharpeningStone : DamageIncreaseSharpeningStoneBase
    {
        protected override int MaxDamageBonus { get { return 65; } }

        public HeavySharpeningStone(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public HeavySharpeningStone() : this(5)
        {
        }

        [Constructable]
        public HeavySharpeningStone(int uses) : base(uses)
        {
            Name = "Heavy Sharpening Stone";
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