using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableNightshade : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x18E5, 0x18E6 );
		}

		public override Item GetCropObject()
		{
			Nightshade reagent = new Nightshade();
			return reagent;
		}

		public override int GetPickedID()
		{
			return 3254;
		}

		[Constructable]
		public FarmableNightshade() : base( GetCropID() )
		{
		}

		public FarmableNightshade( Serial serial ) : base( serial )
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