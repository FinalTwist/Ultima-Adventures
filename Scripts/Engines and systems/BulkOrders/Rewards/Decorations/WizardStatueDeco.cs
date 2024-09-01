namespace Server.Items
{
    public class WizardStatueDeco : Item
    {
        [Constructable]
        public WizardStatueDeco() : base(0x5078)
        {
            Weight = 100.0;
            Name = "A wizard statue";
        }

        public WizardStatueDeco(Serial serial) : base(serial)
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