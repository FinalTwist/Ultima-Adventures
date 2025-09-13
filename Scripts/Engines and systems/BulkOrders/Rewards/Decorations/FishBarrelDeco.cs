namespace Server.Items
{
    public class FishBarrelDeco : Item
    {
        [Constructable]
        public FishBarrelDeco() : base(0x4CCF)
        {
            Weight = 100.0;
            Name = "Decorative Fish Barrel";
        }

        public FishBarrelDeco(Serial serial) : base(serial)
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