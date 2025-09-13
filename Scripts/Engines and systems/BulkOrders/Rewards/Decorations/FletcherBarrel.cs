namespace Server.Items
{
    public class FletcherBarrel : Container
    {
        [Constructable]
        public FletcherBarrel() : base(Utility.RandomList(0x3B45, 0x3B46))
        {
            Name = "A fletcher's barrel";
        }

        public FletcherBarrel(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}