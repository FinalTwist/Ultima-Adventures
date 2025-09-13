using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class GiftBelt : GiftGoldRing
	{
		[Constructable]
		public GiftBelt()
		{
			Resource = CraftResource.None;
			Name = "belt";
			ItemID = 0x2790;
			Hue = RandomThings.GetRandomColor(0);
			Layer = Layer.Waist;
			Weight = 2.0;
		}

		public GiftBelt( Serial serial ) : base( serial )
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
	public class GiftLoinCloth : GiftGoldRing
	{
		[Constructable]
		public GiftLoinCloth()
		{
			Resource = CraftResource.None;
			Name = "loin cloth";
			ItemID = 0x2B68;
			Hue = RandomThings.GetRandomColor(0);
			Layer = Layer.Waist;
			Weight = 2.0;
		}

		public GiftLoinCloth( Serial serial ) : base( serial )
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