using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableWatermelon : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x53CD, 0x53CE, 0x53CF, 0x53D0 );
		}

		public override Item GetCropObject()
		{
			Watermelon watermelon = new Watermelon();
			return watermelon;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0C5F, 0x0C60 );
		}

		[Constructable]
		public FarmableWatermelon() : base( GetCropID() )
		{
			Name = "watermelon";
		}

		public FarmableWatermelon( Serial serial ) : base( serial )
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