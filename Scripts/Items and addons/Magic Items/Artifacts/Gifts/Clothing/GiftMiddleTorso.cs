using System;

namespace Server.Items
{
	public abstract class BaseGiftMiddleTorso : BaseGiftClothing
	{
		public BaseGiftMiddleTorso( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseGiftMiddleTorso( int itemID, int hue ) : base( itemID, Layer.MiddleTorso, hue )
		{
		}

		public BaseGiftMiddleTorso( Serial serial ) : base( serial )
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

	[Flipable( 0x1541, 0x1542 )]
	public class GiftBodySash : BaseGiftMiddleTorso
	{
		[Constructable]
		public GiftBodySash() : this( 0 )
		{
		}

		[Constructable]
		public GiftBodySash( int hue ) : base( 0x1541, hue )
		{
			Weight = 1.0;
		}

        public GiftBodySash(Serial serial)
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

	[Flipable( 0x153d, 0x153e )]
	public class GiftFullApron : BaseGiftMiddleTorso
	{
		[Constructable]
		public GiftFullApron() : this( 0 )
		{
		}

		[Constructable]
		public GiftFullApron( int hue ) : base( 0x153d, hue )
		{
			Weight = 4.0;
		}

        public GiftFullApron(Serial serial)
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

	[Flipable( 0x1f7b, 0x1f7c )]
	public class GiftDoublet : BaseGiftMiddleTorso
	{
		[Constructable]
		public GiftDoublet() : this( 0 )
		{
		}

		[Constructable]
		public GiftDoublet( int hue ) : base( 0x1F7B, hue )
		{
			Weight = 2.0;
		}

        public GiftDoublet(Serial serial)
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

	[Flipable( 0x1ffd, 0x1ffe )]
	public class GiftSurcoat : BaseGiftMiddleTorso
	{
		[Constructable]
		public GiftSurcoat() : this( 0 )
		{
		}

		[Constructable]
		public GiftSurcoat( int hue ) : base( 0x1FFD, hue )
		{
			Weight = 6.0;
		}

        public GiftSurcoat(Serial serial)
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

			if ( Weight == 3.0 )
				Weight = 6.0;
		}
	}

	[Flipable( 0x1fa1, 0x1fa2 )]
	public class GiftTunic : BaseGiftMiddleTorso
	{
		[Constructable]
		public GiftTunic() : this( 0 )
		{
		}

		[Constructable]
		public GiftTunic( int hue ) : base( 0x1FA1, hue )
		{
			Weight = 5.0;
		}

        public GiftTunic(Serial serial)
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

	[Flipable( 0x2310, 0x230F )]
	public class GiftFormalShirt : BaseGiftMiddleTorso
	{
		[Constructable]
		public GiftFormalShirt() : this( 0 )
		{
		}

		[Constructable]
		public GiftFormalShirt( int hue ) : base( 0x2310, hue )
		{
			Weight = 1.0;
		}

        public GiftFormalShirt(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			if ( Weight == 2.0 )
				Weight = 1.0;
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	[Flipable( 0x1f9f, 0x1fa0 )]
	public class GiftJesterSuit : BaseGiftMiddleTorso
	{
		[Constructable]
		public GiftJesterSuit() : this( 0 )
		{
		}

		[Constructable]
		public GiftJesterSuit( int hue ) : base( 0x1F9F, hue )
		{
			Weight = 4.0;
		}

        public GiftJesterSuit(Serial serial)
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

	[Flipable( 0x27A1, 0x27EC )]
	public class GiftJinBaori : BaseGiftMiddleTorso
	{
		[Constructable]
		public GiftJinBaori() : this( 0 )
		{
		}

		[Constructable]
		public GiftJinBaori( int hue ) : base( 0x27A1, hue )
		{
			Weight = 3.0;
		}

        public GiftJinBaori(Serial serial)
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