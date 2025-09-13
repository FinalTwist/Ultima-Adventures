using System;

namespace Server.Items
{
	[Furniture]
	[Flipable(0x1A03,0x1A04)]
	public class HalloweenTortSkel : Item
	{
		[Constructable]
		public HalloweenTortSkel() : base(0x1A03)
		{
			Weight = 1.0;
			Name = "Skeleton";
		}

		public HalloweenTortSkel(Serial serial) : base(serial)
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