using System;

namespace Server.Items
{
	[Furniture]
	public class HalloweenStoneColumn : Item
	{
		[Constructable]
		public HalloweenStoneColumn() : base(0x77)
		{
			Weight = 1.0;
			Name = "Stone Column";
			Hue = 0x322;
		}

		public HalloweenStoneColumn(Serial serial) : base(serial)
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