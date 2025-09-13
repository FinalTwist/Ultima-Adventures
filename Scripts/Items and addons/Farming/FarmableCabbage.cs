using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableCabbage : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x53D1, 0x53D2 );
		}

		public override Item GetCropObject()
		{
			Cabbage cabbage = new Cabbage();
			return cabbage;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0CB0, 0x0CB5, 0x0CB6 );
		}

		[Constructable]
		public FarmableCabbage() : base( GetCropID() )
		{
			Name = "cabbage";
		}

		public FarmableCabbage( Serial serial ) : base( serial )
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