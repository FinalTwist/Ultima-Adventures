using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class RobotTransistor : Item
	{
		[Constructable]
		public RobotTransistor() : base( 0x3446 )
		{
			Weight = 1.0;
			Name = "robot transistor";
		}

		public RobotTransistor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}