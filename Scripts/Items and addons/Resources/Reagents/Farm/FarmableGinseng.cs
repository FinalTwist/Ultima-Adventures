using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableGinseng : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x18E9, 0x18EA );
		}

		public override Item GetCropObject()
		{
			Ginseng reagent = new Ginseng();
			return reagent;
		}

		public override int GetPickedID()
		{
			return 3254;
		}

		[Constructable]
		public FarmableGinseng() : base( GetCropID() )
		{
		}

		public FarmableGinseng( Serial serial ) : base( serial )
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