using System;
using Server; 

namespace Server.Items
{
	public class MeetingSpots : Item
	{
		[Constructable]
		public MeetingSpots() : base( 0x1B72 )
		{
			Name = "meeting spot";
			Visible = false;
			Movable = false;
		}

		public MeetingSpots( Serial serial ) : base( serial )
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