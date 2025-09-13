using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class GiftTalismanLeather : GiftGoldRing
	{
		[Constructable]
		public GiftTalismanLeather()
		{
			ItemID = 0x2F58;
			Resource = CraftResource.None;
			Name = "talisman";
			Layer = Layer.Talisman;
			Weight = 1.0;
			Hue = RandomThings.GetRandomColor(0);
		}

		public GiftTalismanLeather( Serial serial ) : base( serial )
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
	//////////////////////////////////////////////////////////////////////
	public class GiftTalismanSnake : GiftGoldRing
	{
		[Constructable]
		public GiftTalismanSnake()
		{
			ItemID = 0x2F59;
			Resource = CraftResource.None;
			Name = "talisman";
			Layer = Layer.Talisman;
			Weight = 1.0;
			Hue = RandomThings.GetRandomColor(0);
		}

		public GiftTalismanSnake( Serial serial ) : base( serial )
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
	//////////////////////////////////////////////////////////////////////
	public class GiftTalismanTotem : GiftGoldRing
	{
		[Constructable]
		public GiftTalismanTotem()
		{
			ItemID = 0x2F5A;
			Resource = CraftResource.None;
			Name = "talisman";
			Layer = Layer.Talisman;
			Weight = 1.0;
			Hue = RandomThings.GetRandomColor(0);
		}

		public GiftTalismanTotem( Serial serial ) : base( serial )
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
	//////////////////////////////////////////////////////////////////////
	public class GiftTalismanHoly : GiftGoldRing
	{
		[Constructable]
		public GiftTalismanHoly()
		{
			ItemID = 0x2F5B;
			Resource = CraftResource.None;
			Name = "talisman";
			Layer = Layer.Talisman;
			Weight = 1.0;
			Hue = RandomThings.GetRandomColor(0);
		}

		public GiftTalismanHoly( Serial serial ) : base( serial )
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