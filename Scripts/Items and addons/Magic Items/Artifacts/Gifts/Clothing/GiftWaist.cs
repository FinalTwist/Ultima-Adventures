using System;

namespace Server.Items
{

	public abstract class BaseGiftWaist : BaseGiftClothing
	{
		public BaseGiftWaist( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseGiftWaist( int itemID, int hue ) : base( itemID, Layer.Waist, hue )
		{
		}

		public BaseGiftWaist( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x153b, 0x153c )]
	public class GiftHalfApron : BaseGiftWaist
	{
		[Constructable]
		public GiftHalfApron() : this( 0 )
		{
		}

		[Constructable]
		public GiftHalfApron( int hue ) : base( 0x153b, hue )
		{
			Weight = 2.0;
		}

        public GiftHalfApron(Serial serial)
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

	[Flipable( 0x27A0, 0x27EB )]
	public class GiftObi : BaseGiftWaist
	{
		[Constructable]
		public GiftObi() : this( 0 )
		{
		}

		[Constructable]
		public GiftObi( int hue ) : base( 0x27A0, hue )
		{
			Weight = 1.0;
		}

        public GiftObi(Serial serial)
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

	[FlipableAttribute( 0x2B68, 0x315F )]
	public class GiftWoodlandBelt : BaseGiftWaist
	{
		[Constructable]
		public GiftWoodlandBelt() : this( 0 )
		{
		}

		[Constructable]
		public GiftWoodlandBelt( int hue ) : base( 0x2B68, hue )
		{
			Weight = 4.0;
			Name = "loin cloth";
		}

        public GiftWoodlandBelt(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
