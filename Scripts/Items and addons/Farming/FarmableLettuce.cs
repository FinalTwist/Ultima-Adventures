using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableLettuce : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x53D3, 0x53D4 );
		}

		public override Item GetCropObject()
		{
			Lettuce lettuce = new Lettuce();
			return lettuce;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0CB0, 0x0CB5, 0x0CB6 );
		}

		[Constructable]
		public FarmableLettuce() : base( GetCropID() )
		{
			Name = "lettuce";
		}

		public FarmableLettuce( Serial serial ) : base( serial )
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