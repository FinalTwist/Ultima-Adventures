using System;
using Server.Items;

namespace Server.Items
{
	public class RoyalChest : PlateChest
	{
		[Constructable]
		public RoyalChest()
		{
			ItemID = 0x2B08;
			Name = "royal tunic";

		}

		public RoyalChest( Serial serial ) : base( serial )
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

		}
	}
}