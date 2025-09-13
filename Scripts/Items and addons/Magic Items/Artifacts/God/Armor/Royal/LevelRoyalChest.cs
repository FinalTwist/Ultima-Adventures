using System;
using Server.Items;

namespace Server.Items
{
	public class LevelRoyalChest : LevelPlateChest
	{
		[Constructable]
		public LevelRoyalChest()
		{
			ItemID = 0x2B08;
			Name = "royal tunic";
			Weight = 10.0;
		}

		public LevelRoyalChest( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 10.0;
		}
	}
}