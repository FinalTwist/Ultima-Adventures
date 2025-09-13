using System;

namespace Server.Items
{
	public abstract class BaseLevelOuterLegs : BaseLevelClothing
	{
		public BaseLevelOuterLegs( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseLevelOuterLegs( int itemID, int hue ) : base( itemID, Layer.OuterLegs, hue )
		{
		}

		public BaseLevelOuterLegs( Serial serial ) : base( serial )
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
	public class LevelFurSarong : BaseLevelOuterLegs
	{
		[Constructable]
		public LevelFurSarong() : this( 0 )
		{
		}

		[Constructable]
		public LevelFurSarong( int hue ) : base( 0x230C, hue )
		{
			Weight = 3.0;
		}

        public LevelFurSarong(Serial serial)
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
	public class LevelSkirt : BaseLevelOuterLegs
	{
		[Constructable]
		public LevelSkirt() : this( 0 )
		{
		}

		[Constructable]
		public LevelSkirt( int hue ) : base( 0x1516, hue )
		{
			Weight = 4.0;
		}

        public LevelSkirt(Serial serial)
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
	public class LevelKilt : BaseLevelOuterLegs
	{
		[Constructable]
		public LevelKilt() : this( 0 )
		{
		}

		[Constructable]
		public LevelKilt( int hue ) : base( 0x1537, hue )
		{
			Weight = 2.0;
		}

        public LevelKilt(Serial serial)
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
	public class LevelHakama : BaseLevelOuterLegs
	{
		[Constructable]
		public LevelHakama() : this( 0 )
		{
		}

		[Constructable]
		public LevelHakama( int hue ) : base( 0x279A, hue )
		{
			Weight = 2.0;
		}

        public LevelHakama(Serial serial)
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