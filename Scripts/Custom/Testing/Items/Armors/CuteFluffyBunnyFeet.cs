
using System;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x170d, 0x170e)]
    public class CuteFluffyBunnyFeet : BaseShoes
    {
        public override int ArtifactRarity { get { return 13; } }

        [Constructable]
        public CuteFluffyBunnyFeet()
            : this(0)
        {
        }

        [Constructable]
        public CuteFluffyBunnyFeet(int hue)
            : base(0x170D, hue)
        {
            Weight = 1.0;
            Name = "Cute Fluffy Bunny Feet";
            Hue = 2264;

            Attributes.RegenMana = 5;
            Attributes.RegenHits = 5;
            Attributes.LowerRegCost = 5;
            Attributes.RegenHits = 5;
            Attributes.CastSpeed = 1;
            Attributes.CastRecovery = 1;
            

        }

        public CuteFluffyBunnyFeet(Serial serial)
            : base(serial)
        {
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
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
