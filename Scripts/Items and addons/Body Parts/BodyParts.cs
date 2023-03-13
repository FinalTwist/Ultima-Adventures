using System;
using Server;

namespace Server.Items
{
	public class BodyPart : Item
	{
		[Constructable]
		public BodyPart( int itemID ) : base( itemID )
		{
			Movable = true;
			Weight = 2.0;
		}

		public BodyPart( Serial serial ) : base( serial )
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