using System;
using Server;

namespace Server.Items
{
	public class MegalodonTooth : Item
	{
		[Constructable]
		public MegalodonTooth() : base( 0x5747 )
		{
			Weight = 1;
			Name = "megalodon tooth";
			Stackable = true;
		}

		public MegalodonTooth(Serial serial) : base(serial)
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