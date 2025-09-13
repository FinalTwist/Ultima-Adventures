namespace Server.Items
{
    public class DwarvenStatueDeco : Item
    {
        [Constructable]
        public DwarvenStatueDeco() : base(0x5075)
        {
            Weight = 100.0;
            Name = "A dwarven statue";
        }

        public DwarvenStatueDeco(Serial serial) : base(serial)
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