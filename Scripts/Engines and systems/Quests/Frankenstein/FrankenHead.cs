using System;
using Server;

namespace Server.Items
{
	public class FrankenHead : FrankenItem
	{
		[Constructable]
		public FrankenHead()
		{
			Name = "severed head";
			ItemID = 0x3E01;
		}

		public FrankenHead( Serial serial ) : base( serial )
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