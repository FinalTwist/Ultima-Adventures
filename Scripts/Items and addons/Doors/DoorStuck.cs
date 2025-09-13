using System;
using Server;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
	[Flipable(0x6A7, 0x6AD)]
	public class DoorStuck : Item
	{
		[Constructable]
		public DoorStuck() : base( 0x6A7 )
		{
			Name = "door";
			Weight = 1.0;
		}

		public DoorStuck( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{
			m.SendMessage( "This door seems to be locked from the other side." );
		}

        public override void OnDoubleClickDead( Mobile m )
		{
			m.SendMessage( "This door seems to be locked from the other side." );
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