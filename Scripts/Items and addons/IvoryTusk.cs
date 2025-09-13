using System;
using Server;

namespace Server.Items
{
	public class IvoryTusk : Item
	{
		[Constructable]
		public IvoryTusk() : base( 0x0313 )
		{
			Weight = 3;
			Name = "ivory tusk";
			Stackable = true;
		}

		public IvoryTusk(Serial serial) : base(serial)
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