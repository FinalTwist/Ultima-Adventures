using System;

namespace Server.Items
{
	[Furniture]
	public class HalloweenPylonFire : Item
	{
		[Constructable]
		public HalloweenPylonFire() : base(0x19AF)
		{
			Weight = 1.0;
			Name = "Fire";
			Light = LightType.Circle150;
		}

		public HalloweenPylonFire(Serial serial) : base(serial)
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