using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableGarlic : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x18E1, 0x18E2 );
		}

		public override Item GetCropObject()
		{
			Garlic reagent = new Garlic();
			return reagent;
		}

		public override int GetPickedID()
		{
			return 3254;
		}

		[Constructable]
		public FarmableGarlic() : base( GetCropID() )
		{
		}

		public FarmableGarlic( Serial serial ) : base( serial )
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