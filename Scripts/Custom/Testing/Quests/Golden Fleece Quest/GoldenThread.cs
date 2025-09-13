// Golden thread for Scottie's Quest
// By GreyWolf
// Editing help from PrplBeast
// Created Oct 6, 2007

using System;
using Server.Items;

namespace Server.Items
{
    public class GoldenThread : Item
    {
        [Constructable]
        public GoldenThread() : base(0xFA0)
        {
            Name = "Golden Spools of Thread";
            Hue = 2213;
            Stackable = true;
            Amount = 10;
        }

        public GoldenThread(Serial serial) : base(serial)
        {
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