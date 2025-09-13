using System;

namespace Server.Items
{
    public abstract class BaseGiftBracelet : BaseGiftJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044221; } } // star sapphire bracelet

		public BaseGiftBracelet( int itemID ) : base( itemID, Layer.Bracelet )
		{
		}

		public BaseGiftBracelet( Serial serial ) : base( serial )
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

	public class GiftGoldBracelet : BaseGiftBracelet
	{
		[Constructable]
		public GiftGoldBracelet() : base( 0x4CF1 )
		{
			Name = "bracelet";
			Weight = 0.1;
		}

        public GiftGoldBracelet(Serial serial)
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

	public class GiftSilverBracelet : BaseGiftBracelet
	{
		[Constructable]
		public GiftSilverBracelet() : base( 0x4CF2 )
		{
			Name = "bracelet";
			Weight = 0.1;
		}

        public GiftSilverBracelet(Serial serial)
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
