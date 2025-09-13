using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableTomato : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x0DEC, 0x0DEE, 0x53C7, 0x53C8 );
		}

		public override Item GetCropObject()
		{
			Tomato Tomato = new Tomato();
			return Tomato;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0C5F, 0x0C60 );
		}

		[Constructable]
		public FarmableTomato() : base( GetCropID() )
		{
			Name = "tomato";
		}

		public FarmableTomato( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x9D0, 0x9D0 )]
	public class Tomato : Food
	{
		[Constructable]
		public Tomato() : this( 1 )
		{
		}

		[Constructable]
		public Tomato( int amount ) : base( amount, 0x9D0 )
		{
			Name = "tomato";
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Tomato( Serial serial ) : base( serial )
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