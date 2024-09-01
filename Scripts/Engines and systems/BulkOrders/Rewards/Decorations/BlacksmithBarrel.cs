namespace Server.Items
{
    public class BlacksmithBarrel : Container
    {
        [Constructable]
        public BlacksmithBarrel() : base(Utility.RandomList(0x4D05, 0x4D06))
        {
            Name = "A blacksmith's barrel";
        }

        public BlacksmithBarrel(Serial serial) : base(serial)
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