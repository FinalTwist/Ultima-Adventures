using System;

namespace Server.Items
{
	[Furniture]
	public class HalloweenColumn : Item
	{
		[Constructable]
		public HalloweenColumn() : base(0x196)
		{
			Weight = 1.0;
			Name = "Column";
			Hue = 0x322;
		}

		public HalloweenColumn(Serial serial) : base(serial)
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