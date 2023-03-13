using System;

namespace Server.Items
{
	[Furniture]
	public class HalloweenPylon : Item
	{
		[Constructable]
		public HalloweenPylon() : base(0x1ECB)
		{
			Weight = 1.0;
			Name = "Pylon";
			Hue = 0x322;
		}

		public HalloweenPylon(Serial serial) : base(serial)
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