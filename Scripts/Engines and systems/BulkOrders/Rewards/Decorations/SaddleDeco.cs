namespace Server.Items
{
    public class SaddleDeco : Item
    {
        [Constructable]
        public SaddleDeco() : base(Utility.RandomList(0x0F37, 0x0F38))
        {
            Name = "Saddle";
        }

        public SaddleDeco(Serial serial) : base(serial)
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