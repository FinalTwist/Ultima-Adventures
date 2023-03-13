using System;

namespace Server.Items
{
	[Furniture]
	[Flipable(0x1245,0x125E)]
	public class HalloweenChopper : Item
	{
		[Constructable]
		public HalloweenChopper() : base(0x1245)
		{
			Weight = 1.0;
			Name = "Guillotine";
		}

		public HalloweenChopper(Serial serial) : base(serial)
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
		}
	}
}