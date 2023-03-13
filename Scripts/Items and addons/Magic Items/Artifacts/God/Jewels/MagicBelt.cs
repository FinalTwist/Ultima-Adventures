using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class LevelBelt : LevelGoldRing
	{
		[Constructable]
		public LevelBelt()
		{
			Resource = CraftResource.None;
			Name = "belt";
			ItemID = 0x2790;
			Hue = RandomThings.GetRandomColor(0);
			Layer = Layer.Waist;
			Weight = 2.0;
		}

		public LevelBelt( Serial serial ) : base( serial )
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
	////////////////////////////////////////////////////////////////////////////////////////
	public class LevelLoinCloth : LevelGoldRing
	{
		[Constructable]
		public LevelLoinCloth()
		{
			Resource = CraftResource.None;
			Name = "loin cloth";
			ItemID = 0x2B68;
			Hue = RandomThings.GetRandomColor(0);
			Layer = Layer.Waist;
			Weight = 2.0;
		}

		public LevelLoinCloth( Serial serial ) : base( serial )
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