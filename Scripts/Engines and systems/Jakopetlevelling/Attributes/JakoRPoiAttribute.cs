using System;

namespace Custom.Jerbal.Jako
{
    class JakoRPoiAttribute : JakoBaseAttribute
    {
        public override uint Cap
        {
            get { return (uint)85; }
        }

        public override double CapScale
        {
            get { return 3; }
        }

        public override uint GetStat(Server.Mobiles.BaseCreature bc)
        {
            return (uint)bc.PoisonResistance;
        }

        protected override void SetStat(Server.Mobiles.BaseCreature bc, uint toThis)
        {
            bc.SetResistance(Server.ResistanceType.Poison, (int)toThis);
        }

        public override uint AttributesGiven
        {
            get { return (uint)2; }
        }

        public override uint PointsTaken
        {
            get { return (uint)5; }
        }

        public override string ToString()
        {
            return "Poison Resistance";
        }
    }
}
