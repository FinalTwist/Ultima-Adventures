using System;

namespace Server.Items
{
	public abstract class BaseGiftOuterLegs : BaseGiftClothing
	{
		public BaseGiftOuterLegs( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseGiftOuterLegs( int itemID, int hue ) : base( itemID, Layer.OuterLegs, hue )
		{
		}

		public BaseGiftOuterLegs( Serial serial ) : base( serial )
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

	[Flipable( 0x230C, 0x230B )]
	public class GiftFurSarong : BaseGiftOuterLegs
	{
		[Constructable]
		public GiftFurSarong() : this( 0 )
		{
		}

		[Constructable]
		public GiftFurSarong( int hue ) : base( 0x230C, hue )
		{
			Weight = 3.0;
		}

        public GiftFurSarong(Serial serial)
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

			if ( Weight == 4.0 )
				Weight = 3.0;
		}
	}

	[Flipable( 0x1516, 0x1531 )]
	public class GiftSkirt : BaseGiftOuterLegs
	{
		[Constructable]
		public GiftSkirt() : this( 0 )
		{
		}

		[Constructable]
		public GiftSkirt( int hue ) : base( 0x1516, hue )
		{
			Weight = 4.0;
		}

        public GiftSkirt(Serial serial)
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

	[Flipable( 0x1537, 0x1538 )]
	public class GiftKilt : BaseGiftOuterLegs
	{
		[Constructable]
		public GiftKilt() : this( 0 )
		{
		}

		[Constructable]
		public GiftKilt( int hue ) : base( 0x1537, hue )
		{
			Weight = 2.0;
		}

        public GiftKilt(Serial serial)
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

	[Flipable( 0x279A, 0x27E5 )]
	public class GiftHakama : BaseGiftOuterLegs
	{
		[Constructable]
		public GiftHakama() : this( 0 )
		{
		}

		[Constructable]
		public GiftHakama( int hue ) : base( 0x279A, hue )
		{
			Weight = 2.0;
		}

        public GiftHakama(Serial serial)
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