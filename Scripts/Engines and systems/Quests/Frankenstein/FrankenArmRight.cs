using System;
using Server;

namespace Server.Items
{
	public class FrankenArmRight : FrankenItem
	{
		[Constructable]
		public FrankenArmRight()
		{
			Name = "severed right arm";
			ItemID = 0x3A8C;
		}

		public FrankenArmRight( Serial serial ) : base( serial )
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