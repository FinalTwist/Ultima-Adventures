using System;

namespace Server.Items
{
	public abstract class BaseGiftNecklace : BaseGiftJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044241; } } // star sapphire necklace

		public BaseGiftNecklace( int itemID ) : base( itemID, Layer.Neck )
		{
		}

		public BaseGiftNecklace( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class GiftNecklace : BaseGiftNecklace
	{
		[Constructable]
		public GiftNecklace() : base( 0x4CFE )
		{
			Name = "beads";
			Weight = 0.1;
		}

		public GiftNecklace( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class GiftGoldNecklace : BaseGiftNecklace
	{
		[Constructable]
		public GiftGoldNecklace() : base( 0x4CFF )
		{
			Name = "amulet";
			Weight = 0.1;
		}

        public GiftGoldNecklace(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class GiftGoldBeadNecklace : BaseGiftNecklace
	{
		[Constructable]
		public GiftGoldBeadNecklace() : base( 0x4CFD )
		{
			Name = "beads";
			Weight = 0.1;
		}

        public GiftGoldBeadNecklace(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class GiftSilverNecklace : BaseGiftNecklace
	{
		[Constructable]
		public GiftSilverNecklace() : base( 0x4D00 )
		{
			Name = "amulet";
			Weight = 0.1;
			Hue = 2101;
		}

        public GiftSilverNecklace(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class GiftSilverBeadNecklace : BaseGiftNecklace
	{
		[Constructable]
		public GiftSilverBeadNecklace() : base( 0x4CFE )
		{
			Name = "beads";
			Weight = 0.1;
		}

        public GiftSilverBeadNecklace(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}