using System;

namespace Server.Items
{
	public abstract class BasePants : BaseClothing
	{
		public BasePants( int itemID ) : this( itemID, 0 )
		{
		}

		public BasePants( int itemID, int hue ) : base( itemID, Layer.Pants, hue )
		{
		}

		public BasePants( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x152e, 0x152f )]
	public class ShortPants : BasePants
	{
		[Constructable]
		public ShortPants() : this( 0 )
		{
		}

		[Constructable]
		public ShortPants( int hue ) : base( 0x152E, hue )
		{
			Weight = 2.0;
		}

		public ShortPants( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x1539, 0x153a )]
	public class LongPants : BasePants
	{
		[Constructable]
		public LongPants() : this( 0 )
		{
		}

		[Constructable]
		public LongPants( int hue ) : base( 0x1539, hue )
		{
			Weight = 2.0;
		}

		public LongPants( Serial serial ) : base( serial )
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

	[Flipable( 0x279B, 0x27E6 )]
	public class TattsukeHakama : BasePants
	{
		[Constructable]
		public TattsukeHakama() : this( 0 )
		{
		}

		[Constructable]
		public TattsukeHakama( int hue ) : base( 0x279B, hue )
		{
			Weight = 2.0;
		}

		public TattsukeHakama( Serial serial ) : base( serial )
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

	public class SailorPants : BasePants
	{
		[Constructable]
		public SailorPants() : this( 0 )
		{
		}

		[Constructable]
		public SailorPants( int hue ) : base( 0x309, hue )
		{
			Name = "sailor pants";
			Weight = 2.0;
		}

		public SailorPants( Serial serial ) : base( serial )
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

	public class PiratePants : BasePants
	{
		[Constructable]
		public PiratePants() : this( 0 )
		{
		}

		[Constructable]
		public PiratePants( int hue ) : base( 0x404, hue )
		{
			Name = "pirate pants";
			Weight = 2.0;
		}

		public PiratePants( Serial serial ) : base( serial )
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