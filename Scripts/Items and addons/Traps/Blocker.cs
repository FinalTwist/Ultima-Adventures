using System;
using Server;

namespace Server.Items
{
	public class StopBlock : Item
	{
		[Constructable]
		public StopBlock() : base( 0x43CD )
		{
			Weight = 20;
			Visible = false;
			Movable = false;
			Name = "blocker";
		}

		public StopBlock(Serial serial) : base(serial)
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