namespace Server.Items
{
    public class StumpDeco : Item
    {
        [Constructable]
        public StumpDeco() : base(Utility.RandomList(0x0E56, 0x0E57, 0x0E58, 0x0E59))
        {
            Name = "Stump";
        }

        public StumpDeco(Serial serial) : base(serial)
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