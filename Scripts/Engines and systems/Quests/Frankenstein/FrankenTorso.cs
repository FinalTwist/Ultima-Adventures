using System;
using Server;

namespace Server.Items
{
	public class FrankenTorso : FrankenItem
	{
		[Constructable]
		public FrankenTorso()
		{
			Name = "severed torso";
			ItemID = 0x3A9B;
		}

		public FrankenTorso( Serial serial ) : base( serial )
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