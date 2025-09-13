using System;
using Server;

namespace Server.Items
{
	public class FrankenLegRight : FrankenItem
	{
		[Constructable]
		public FrankenLegRight()
		{
			Name = "severed right leg";
			ItemID = 0x3E99;
		}

		public FrankenLegRight( Serial serial ) : base( serial )
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