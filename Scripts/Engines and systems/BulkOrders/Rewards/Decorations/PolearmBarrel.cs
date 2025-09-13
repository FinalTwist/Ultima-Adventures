namespace Server.Items
{
    public class PolearmBarrel : Container
    {
        [Constructable]
        public PolearmBarrel() : base(Utility.RandomList(0x3B48, 0x3B49, 0x3B4A, 0x3B4E))
        {
            Name = "A polearm barrel";
        }

        public PolearmBarrel(Serial serial) : base(serial)
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