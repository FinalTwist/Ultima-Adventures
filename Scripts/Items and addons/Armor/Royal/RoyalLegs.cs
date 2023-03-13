using System;
using Server.Items;

namespace Server.Items
{
	public class RoyalsLegs : PlateLegs
	{
		[Constructable]
		public RoyalsLegs()
		{
			ItemID = 0x2B06;
			Name = "royal leggings";

		}

		public RoyalsLegs( Serial serial ) : base( serial )
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