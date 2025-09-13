namespace Server.Items
{
    public class ToolBarrel : Container
    {
        [Constructable]
        public ToolBarrel() : base(Utility.RandomList(0x3B44, 0x3B50, 0x3B4C, 0x3B4D))
        {
            Name = "A tool barrel";
        }

        public ToolBarrel(Serial serial) : base(serial)
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