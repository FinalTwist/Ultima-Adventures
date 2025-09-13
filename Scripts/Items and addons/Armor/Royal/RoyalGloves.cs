using System;
using Server.Items;

namespace Server.Items
{
	public class RoyalGloves : PlateGloves
	{
		[Constructable]
		public RoyalGloves()
		{
			ItemID = 0x2B0C;
			Name = "royal bracers";

		}

		public RoyalGloves( Serial serial ) : base( serial )
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