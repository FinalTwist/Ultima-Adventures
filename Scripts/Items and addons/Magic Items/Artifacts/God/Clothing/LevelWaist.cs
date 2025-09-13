using System;

namespace Server.Items
{

	public abstract class BaseLevelWaist : BaseLevelClothing
	{
		public BaseLevelWaist( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseLevelWaist( int itemID, int hue ) : base( itemID, Layer.Waist, hue )
		{
		}

		public BaseLevelWaist( Serial serial ) : base( serial )
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
	public class LevelHalfApron : BaseLevelWaist
	{
		[Constructable]
		public LevelHalfApron() : this( 0 )
		{
		}

		[Constructable]
		public LevelHalfApron( int hue ) : base( 0x153b, hue )
		{
			Weight = 2.0;
		}

        public LevelHalfApron(Serial serial)
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
	public class LevelObi : BaseLevelWaist
	{
		[Constructable]
		public LevelObi() : this( 0 )
		{
		}

		[Constructable]
		public LevelObi( int hue ) : base( 0x27A0, hue )
		{
			Weight = 1.0;
		}

        public LevelObi(Serial serial)
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
	public class LevelWoodlandBelt : BaseLevelWaist
	{
		[Constructable]
		public LevelWoodlandBelt() : this( 0 )
		{
		}

		[Constructable]
		public LevelWoodlandBelt( int hue ) : base( 0x2B68, hue )
		{
			Weight = 4.0;
			Name = "loin cloth";
		}

        public LevelWoodlandBelt(Serial serial)
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
