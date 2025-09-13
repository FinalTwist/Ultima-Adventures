namespace Server.Items
{
    public class RoughSharpeningStone : DamageIncreaseSharpeningStoneBase
    {
        protected override int MaxDamageBonus { get { return 60; } }

        public RoughSharpeningStone(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public RoughSharpeningStone() : this(5)
        {
        }

        [Constructable]
        public RoughSharpeningStone(int uses) : base(uses)
        {
            Name = "Rough Sharpening Stone";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Skills[SkillName.Blacksmith].Value < 60)
            {
                from.SendMessage(32, "You need at least 60 Blacksmithing to use this");
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