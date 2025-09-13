using Server.Items;

namespace Server.AnimateHue
{
    public class SpecialFloor : BaseFloor
    {
        private int BaseHue;
        private int MaxHue;

        [Constructable]
        public SpecialFloor(int hue, int run) : base(0x519, 4)
        {
            Name = "Special Floor";
            Hue = hue;

            BaseHue = hue;
            MaxHue = (hue + run);
        }

        public SpecialFloor(Serial serial) : base(serial)
        {
        }

        public void AnimateHue()
        {
            if (Hue < MaxHue)
            {
                Hue++;
            }
            else
            {
                Hue = BaseHue;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(BaseHue);
            writer.Write(MaxHue);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            BaseHue = reader.ReadInt();
            MaxHue = reader.ReadInt();
        }
    }
}
