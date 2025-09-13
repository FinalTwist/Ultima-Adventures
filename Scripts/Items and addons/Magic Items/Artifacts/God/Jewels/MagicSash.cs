using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class LevelSash : LevelGoldRing
	{
		[Constructable]
		public LevelSash()
		{
			Resource = CraftResource.None;
			Name = "sash";
			ItemID = 0x1541;
			Hue = RandomThings.GetRandomColor(0);
			Layer = Layer.MiddleTorso;
			Weight = 2.0;
		}

		public LevelSash( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}