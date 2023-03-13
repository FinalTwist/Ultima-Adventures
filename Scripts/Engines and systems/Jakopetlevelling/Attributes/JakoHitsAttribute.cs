using System;

namespace Custom.Jerbal.Jako
{
    public class JakoHitsAttribute : JakoBaseAttribute
    {

        public JakoHitsAttribute()
        {

        }

        public override uint Cap
        {
            get { return (uint)15000; }
        }

        public override double CapScale
        {
            get { return 3; }
        }

        public override uint GetStat(Server.Mobiles.BaseCreature bc)
        {
            return (uint)bc.HitsMax;
        }

        protected override void SetStat(Server.Mobiles.BaseCreature bc, uint toThis)
        {
            bc.HitsMaxSeed = (int)toThis;
        }

        public override uint AttributesGiven
        {
            get { return (uint)1; }
        }

        public override uint PointsTaken
        {
            get { return (uint)1; }
        }

        public override string ToString()
        {
            return "Hits";
        }
    }
}
