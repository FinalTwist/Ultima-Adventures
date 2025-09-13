/* Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
	public class JewelOfUnPossession : Item
	{
		[Constructable]
        public JewelOfUnPossession()
            : base(0x1EA7)
		{
            Name = "Ancient Jewel Of UnPossession";
			Hue = 2956;

		}

        public JewelOfUnPossession(Serial serial)
            : base(serial)
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