namespace Server.Items
{
    public class DenseBowString : DamageIncreaseBowStringBase
    {
        protected override int MaxDamageBonus { get { return 70; } }

        public DenseBowString(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public DenseBowString() : this(5)
        {
        }

        [Constructable]
        public DenseBowString(int uses) : base(uses)
        {
            Name = "Mighty Bow String";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Skills[SkillName.Fletching].Value < 100.0)
            {
                from.SendMessage(32, "You need at least 100 Fletching to use this");
                return;
            }

            base.OnDoubleClick(from);
        }

        protected override int GetBonus(Mobile from)
        {
            return Utility.Random((int)(from.Skills[SkillName.Fletching].Value / 10));
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