namespace Server.Items
{
    public class LlamaShrubDeco : Item
    {
        [Constructable]
        public LlamaShrubDeco() : base(0x4930)
        {
            Weight = 25.0;
            Name = "A shrub";
        }

        public LlamaShrubDeco(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}