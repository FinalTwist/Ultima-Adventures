namespace Server.Items
{
    public class HeavyBowString : DamageIncreaseBowStringBase
    {
        protected override int MaxDamageBonus { get { return 65; } }

        public HeavyBowString(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public HeavyBowString() : this(5)
        {
        }

        [Constructable]
        public HeavyBowString(int uses) : base(uses)
        {
            Name = "Sturdy Bow String";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Skills[SkillName.Fletching].Value < 80)
            {
                from.SendMessage(32, "You need at least 80 Fletching to use this");
                return;
            }

            base.OnDoubleClick(from);
        }

        protected override int GetBonus(Mobile from)
        {
            return Utility.Random((int)(from.Skills[SkillName.Fletching].Value / 20));
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