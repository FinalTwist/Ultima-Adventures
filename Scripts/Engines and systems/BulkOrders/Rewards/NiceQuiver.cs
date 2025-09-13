namespace Server.Items
{
    public class NiceQuiver : BaseQuiver
    {
        [Constructable]
        public NiceQuiver() : base()
        {
            Name = "Nice Quiver";
            WeightReduction = 100;
            DamageIncrease = 10;
            LowerAmmoCost = 90;
        }

        public NiceQuiver(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}