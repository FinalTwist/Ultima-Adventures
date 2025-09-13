using System;


namespace Custom.Jerbal.Jako
{
    class JakoREneAttribute : JakoBaseAttribute
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
            return (uint)bc.EnergyResistance;
        }

        protected override void SetStat(Server.Mobiles.BaseCreature bc, uint toThis)
        {
            bc.SetResistance(Server.ResistanceType.Energy, (int)toThis);
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
            return "Energy Resistance";
        }
    }
}
