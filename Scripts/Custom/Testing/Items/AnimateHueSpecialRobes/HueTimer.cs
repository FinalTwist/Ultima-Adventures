using System;

namespace Server.AnimateHue
{
    public class HueTimer : Timer
    {
        public HueTimer() : base(TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(250))
        {
        }

        protected override void OnTick()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item.Name == "Special Robe")
                {
                    SpecialRobe sr = item as SpecialRobe;

                    sr.AnimateHue();
                }

                if (item.Name == "Special Floor")
                {
                    SpecialFloor sf = item as SpecialFloor;

                    sf.AnimateHue();
                }
            }
        }
    }
}
