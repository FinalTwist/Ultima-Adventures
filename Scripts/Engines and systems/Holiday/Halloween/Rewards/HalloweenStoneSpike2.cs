using System;

namespace Server.Items
{
	[Furniture]
	public class HalloweenStoneSpike2 : Item
	{
		[Constructable]
		public HalloweenStoneSpike2() : base(0x2202)
		{
			Weight = 1.0;
			Name = "Stone Spike";
			Hue = 0x322;
		}

		public HalloweenStoneSpike2(Serial serial) : base(serial)
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