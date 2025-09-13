using System;

namespace Server.Items
{
	[Furniture]
	[Flipable(0x122A,0x122D)]
	public class HalloweenBlood : Item
	{
		[Constructable]
		public HalloweenBlood() : base(0x122A)
		{
			Weight = 1.0;
			Name = "Blood";
		}

		public HalloweenBlood(Serial serial) : base(serial)
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