using System;

namespace Server.Items
{
	[Furniture]
	public class HalloweenStoneSpike : Item
	{
		[Constructable]
		public HalloweenStoneSpike() : base(0x2201)
		{
			Weight = 1.0;
			Name = "Stone Spike";
			Hue = 0x322;
		}

		public HalloweenStoneSpike(Serial serial) : base(serial)
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