using System;
using Server.Items;

namespace Server.AnimateHue
{
    class SpecialRobe : Robe
    {
        private int BaseHue;
        private int MaxHue;

        private bool Special = false;
        private readonly int SpcHue = 2058;
        private readonly int SpcHueMax = 2067;
        private bool Forward = true;

        [Constructable]
        public SpecialRobe()
        {
            Name = "Special Robe";
            Hue = GetHue(0);
            BaseHue = Hue;
        }

        [Constructable]
        public SpecialRobe(int hue)
        {
            Name = "Special Robe";
            Hue = GetHue(hue);
            BaseHue = Hue;
        }

        public SpecialRobe(Serial serial) : base(serial)
        {
        }

        private int GetHue(int hueNum)
        {
            Random rnd = new Random();
            int getRnd = rnd.Next(0, 209);

            if (hueNum != 0)
            {
                getRnd = hueNum;
            }

            if (getRnd < 201)
            {
                if (getRnd == 0)
                {
                    MaxHue = 6;
                    return 2;
                }
                else
                {
                    MaxHue = ((getRnd * 5) + 6);
                    return ((getRnd * 5) + 2);
                }
            }
            else
            {
                if (getRnd == 201)
                {
                    MaxHue = 1260;
                    return 1255;
                }
                else if (getRnd == 202)
                {
                    MaxHue = 1266;
                    return 1261;
                }
                else if (getRnd == 203)
                {
                    MaxHue = 1272;
                    return 1267;
                }
                else if (getRnd == 204)
                {
                    MaxHue = 1278;
                    return 1273;
                }
                else if (getRnd == 205)
                {
                    MaxHue = 1360;
                    return 1355;
                }
                else if (getRnd == 206)
                {
                    MaxHue = 1366;
                    return 1361;
                }
                else if (getRnd == 207)
                {
                    MaxHue = 1372;
                    return 1367;
                }
                else if (getRnd == 208)
                {
                    MaxHue = 1378;
                    return 1373;
                }
                else
                {
                    Special = true;
                    return SpcHue;
                }
            }
        }

        public void AnimateHue()
        {
            if (Special)
            {
                Hue = GetSpcHue();
            }
            else
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
        }

        private int GetSpcHue()
        {
            int getHue = Hue;

            if (Hue <= SpcHue)
            {
                Forward = true;
                getHue = SpcHue;
            }

            if (Hue >= SpcHueMax)
            {
                Forward = false;
                getHue = SpcHueMax;
            }
            
            if (Forward)
            {
                getHue = (getHue + 1);
            }
            else
            {
                getHue = (getHue - 1);
            }

            return getHue;
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(BaseHue);
            writer.Write(MaxHue);
            writer.Write(Special);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            BaseHue = reader.ReadInt();
            MaxHue = reader.ReadInt();
            Special = reader.ReadBool();
        }
    }
}
