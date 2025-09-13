using System; 
using Server;
using Server.Items;

namespace Server.Items
{
    public class CasinoToken : Item   // Free plays on Casino games that are aware of Tokens
    {
        [Constructable]
        public CasinoToken() : this(10) { }

        [Constructable]
        public CasinoToken(int NumCredits) : this(NumCredits, 56) { }

        [Constructable]
        public CasinoToken(int NumCredits, int CreditHue)
            : base(10922)
        {
            Light = LightType.Empty;
            Stackable = true;
            Weight = 0.02;
            Hue = CreditHue;
            Name = "Casino Token";
            Amount = NumCredits;
        }

        public CasinoToken(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            if (this.Name == null)
                list.Add(this.LabelNumber);
            else
                list.Add(this.Name);
            list.Add(1060584, this.Amount.ToString()); // uses remaining:
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version         
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

}