using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableCorn : FarmableCrop
	{
		public static int GetCropID()
		{
			return 0xc7d;
		}

		public override Item GetCropObject()
		{
			Corn corn = new Corn();
			return corn;
		}

		public override int GetPickedID()
		{
			return 0xc7e;
		}

		[Constructable]
		public FarmableCorn() : base( GetCropID() )
		{
			Name = "corn";
		}

		public FarmableCorn( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xc7f, 0xc82 )]
	public class Corn : Food
	{
		[Constructable]
		public Corn() : this( 1 )
		{
		}

		[Constructable]
		public Corn( int amount ) : base( amount, 0xc7f )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Corn( Serial serial ) : base( serial )
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