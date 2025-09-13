using System;

namespace Server.Items
{
	[Furniture]
	public class HalloweenMaiden : Item
	{
		[Constructable]
		public HalloweenMaiden() : base(0x124B)
		{
			Weight = 1.0;
			Name = "Iron Maiden";
		}

		public HalloweenMaiden(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.ItemID == 0x124B ){ this.ItemID = 0x1249; }
			else { this.ItemID = 0x124B; }
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