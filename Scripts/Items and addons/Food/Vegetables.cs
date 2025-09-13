using System;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0xc77, 0xc78 )]
	public class Carrot : Food
	{
		[Constructable]
		public Carrot() : this( 1 )
		{
		}

		[Constructable]
		public Carrot( int amount ) : base( amount, 0xc78 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Carrot( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xc7b, 0xc7c )]
	public class Cabbage : Food
	{
		[Constructable]
		public Cabbage() : this( 1 )
		{
		}

		[Constructable]
		public Cabbage( int amount ) : base( amount, 0xc7b )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Cabbage( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xc6d, 0xc6e )]
	public class Onion : Food
	{
		[Constructable]
		public Onion() : this( 1 )
		{
		}

		[Constructable]
		public Onion( int amount ) : base( amount, 0xc6d )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Onion( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xc70, 0xc71 )]
	public class Lettuce : Food
	{
		[Constructable]
		public Lettuce() : this( 1 )
		{
		}

		[Constructable]
		public Lettuce( int amount ) : base( amount, 0xc70 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Lettuce( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xC6A, 0xC6B )]
	public class Pumpkin : Food
	{
		[Constructable]
		public Pumpkin() : this( 1 )
		{
		}

		[Constructable]
		public Pumpkin( int amount ) : base( amount, 0xC6A )
		{
			this.Weight = 1.0;
			this.FillFactor = 8;
		}

		public Pumpkin( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 )
			{
				if ( FillFactor == 4 )
					FillFactor = 8;

				if ( Weight == 5.0 )
					Weight = 1.0;
			}
		}
	}

	public class PumpkinLarge : Food
	{
		[Constructable]
		public PumpkinLarge() : this( 1 )
		{
		}

		[Constructable]
		public PumpkinLarge( int amount ) : base( amount, 0x5559 )
		{
			Name = "large pumpkin";
			this.Weight = 10.0;
			this.Stackable = false;
			this.FillFactor = 10;
		}

		public PumpkinLarge( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class PumpkinTall : Food
	{
		[Constructable]
		public PumpkinTall() : this( 1 )
		{
		}

		[Constructable]
		public PumpkinTall( int amount ) : base( amount, 0x5456 )
		{
			Name = "tall pumpkin";
			this.Weight = 10.0;
			this.Stackable = false;
			this.FillFactor = 10;
		}

		public PumpkinTall( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class PumpkinGreen : Food
	{
		[Constructable]
		public PumpkinGreen() : this( 1 )
		{
		}

		[Constructable]
		public PumpkinGreen( int amount ) : base( amount, 0x555B )
		{
			Name = "green pumpkin";
			this.Weight = 10.0;
			this.Stackable = false;
			this.FillFactor = 10;
		}

		public PumpkinGreen( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class PumpkinGiant : Food
	{
		[Constructable]
		public PumpkinGiant() : this( 1 )
		{
		}

		[Constructable]
		public PumpkinGiant( int amount ) : base( amount, 0x555A )
		{
			Name = "giant pumpkin";
			this.Weight = 100.0;
			this.Stackable = false;
			this.FillFactor = 20;
		}

		public PumpkinGiant( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}



	public class SmallPumpkin : Food
	{
		[Constructable]
		public SmallPumpkin() : this( 1 )
		{
		}

		[Constructable]
		public SmallPumpkin( int amount ) : base( amount, 0xC6C )
		{
			this.Weight = 1.0;
			this.FillFactor = 8;
		}

		public SmallPumpkin( Serial serial ) : base( serial )
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