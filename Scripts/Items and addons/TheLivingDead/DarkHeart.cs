using System;

namespace Server.Items
{
	[Furniture]
	public class DarkHeart : Item
	{
		[Constructable]
		public DarkHeart() : base(0xF91)
		{
			Weight = 1.0;
			Name = "dark heart";
			Hue = 0x386;
		}

		public DarkHeart(Serial serial) : base(serial)
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
			Weight = 1.0;
		}
	}
}