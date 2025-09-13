using System;

namespace Server.Items
{
	public abstract class BaseLevelMiddleTorso : BaseLevelClothing
	{
		public BaseLevelMiddleTorso( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseLevelMiddleTorso( int itemID, int hue ) : base( itemID, Layer.MiddleTorso, hue )
		{
		}

		public BaseLevelMiddleTorso( Serial serial ) : base( serial )
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
	public class LevelBodySash : BaseLevelMiddleTorso
	{
		[Constructable]
		public LevelBodySash() : this( 0 )
		{
		}

		[Constructable]
		public LevelBodySash( int hue ) : base( 0x1541, hue )
		{
			Weight = 1.0;
		}

        public LevelBodySash(Serial serial)
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
	public class LevelFullApron : BaseLevelMiddleTorso
	{
		[Constructable]
		public LevelFullApron() : this( 0 )
		{
		}

		[Constructable]
		public LevelFullApron( int hue ) : base( 0x153d, hue )
		{
			Weight = 4.0;
		}

        public LevelFullApron(Serial serial)
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
	public class LevelDoublet : BaseLevelMiddleTorso
	{
		[Constructable]
		public LevelDoublet() : this( 0 )
		{
		}

		[Constructable]
		public LevelDoublet( int hue ) : base( 0x1F7B, hue )
		{
			Weight = 2.0;
		}

        public LevelDoublet(Serial serial)
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
	public class LevelSurcoat : BaseLevelMiddleTorso
	{
		[Constructable]
		public LevelSurcoat() : this( 0 )
		{
		}

		[Constructable]
		public LevelSurcoat( int hue ) : base( 0x1FFD, hue )
		{
			Weight = 6.0;
		}

        public LevelSurcoat(Serial serial)
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
	public class LevelTunic : BaseLevelMiddleTorso
	{
		[Constructable]
		public LevelTunic() : this( 0 )
		{
		}

		[Constructable]
		public LevelTunic( int hue ) : base( 0x1FA1, hue )
		{
			Weight = 5.0;
		}

        public LevelTunic(Serial serial)
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
	public class LevelFormalShirt : BaseLevelMiddleTorso
	{
		[Constructable]
		public LevelFormalShirt() : this( 0 )
		{
		}

		[Constructable]
		public LevelFormalShirt( int hue ) : base( 0x2310, hue )
		{
			Weight = 1.0;
		}

        public LevelFormalShirt(Serial serial)
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
	public class LevelJesterSuit : BaseLevelMiddleTorso
	{
		[Constructable]
		public LevelJesterSuit() : this( 0 )
		{
		}

		[Constructable]
		public LevelJesterSuit( int hue ) : base( 0x1F9F, hue )
		{
			Weight = 4.0;
		}

        public LevelJesterSuit(Serial serial)
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
	public class LevelJinBaori : BaseLevelMiddleTorso
	{
		[Constructable]
		public LevelJinBaori() : this( 0 )
		{
		}

		[Constructable]
		public LevelJinBaori( int hue ) : base( 0x27A1, hue )
		{
			Weight = 3.0;
		}

        public LevelJinBaori(Serial serial)
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