using System;
using Server.Items;

namespace Server.Items
{
	public class RoyalGorget : PlateGorget
	{
		[Constructable]
		public RoyalGorget()
		{
			ItemID = 0x2B0E;
			Name = "royal gorget";

		}

		public RoyalGorget( Serial serial ) : base( serial )
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