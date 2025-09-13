using System;
using Server.Prompts;
using Server.Network;

namespace Server.Items
{
	[Furniture]
	[Flipable(0x149B,0x149C)]
	public class NecromancerBanner : Item
	{
		[Constructable]
		public NecromancerBanner() : base(0x149B)
		{
			Weight = 10.0;
			Name = "deathly banner";
		}

		public NecromancerBanner(Serial serial) : base(serial)
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