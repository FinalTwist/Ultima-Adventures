using System;

namespace Server.Items
{
	[Furniture]
	public class HalloweenTree6 : Item
	{
		[Constructable]
		public HalloweenTree6() : base(0xCCD)
		{
			Weight = 1.0;
			Name = "Tree";
			Hue = 0x2C1;
		}

		public HalloweenTree6(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
}