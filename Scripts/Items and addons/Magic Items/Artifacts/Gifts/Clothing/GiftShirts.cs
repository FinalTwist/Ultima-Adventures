using System;

namespace Server.Items
{
	public abstract class BaseGiftShirt : BaseGiftClothing
	{
		public BaseGiftShirt( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseGiftShirt( int itemID, int hue ) : base( itemID, Layer.Shirt, hue )
		{
		}

		public BaseGiftShirt( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x1efd, 0x1efe )]
	public class GiftFancyShirt : BaseGiftShirt
	{
		[Constructable]
		public GiftFancyShirt() : this( 0 )
		{
		}

		[Constructable]
		public GiftFancyShirt( int hue ) : base( 0x1EFD, hue )
		{
			Weight = 2.0;
		}

        public GiftFancyShirt(Serial serial)
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

	[FlipableAttribute( 0x1517, 0x1518 )]
	public class GiftShirt : BaseGiftShirt
	{
		[Constructable]
		public GiftShirt() : this( 0 )
		{
		}

		[Constructable]
		public GiftShirt( int hue ) : base( 0x1517, hue )
		{
			Weight = 1.0;
		}

        public GiftShirt(Serial serial)
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

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}

	[Flipable( 0x2794, 0x27DF )]
	public class GiftClothNinjaJacket : BaseGiftShirt
	{
		[Constructable]
		public GiftClothNinjaJacket() : this( 0 )
		{
		}

		[Constructable]
		public GiftClothNinjaJacket( int hue ) : base( 0x2794, hue )
		{
			Weight = 5.0;
			Layer = Layer.InnerTorso;
		}

        public GiftClothNinjaJacket(Serial serial)
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