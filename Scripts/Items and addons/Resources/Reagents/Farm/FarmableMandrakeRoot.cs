using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmableMandrakeRoot : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x18DF, 0x18E0 );
		}

		public override Item GetCropObject()
		{
			MandrakeRoot reagent = new MandrakeRoot();
			return reagent;
		}

		public override int GetPickedID()
		{
			return 3254;
		}

		[Constructable]
		public FarmableMandrakeRoot() : base( GetCropID() )
		{
		}

		public FarmableMandrakeRoot( Serial serial ) : base( serial )
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