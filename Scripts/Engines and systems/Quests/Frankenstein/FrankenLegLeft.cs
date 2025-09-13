using System;
using Server;

namespace Server.Items
{
	public class FrankenLegLeft : FrankenItem
	{
		[Constructable]
		public FrankenLegLeft()
		{
			Name = "severed left leg";
			ItemID = 0x3E82;
		}

		public FrankenLegLeft( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}