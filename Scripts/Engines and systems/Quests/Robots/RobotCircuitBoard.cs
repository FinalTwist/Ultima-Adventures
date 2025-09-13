using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class RobotCircuitBoard : Item
	{
		[Constructable]
		public RobotCircuitBoard() : base( 0x346D )
		{
			Weight = 1.0;
			Name = "robot circuit board";
		}

		public RobotCircuitBoard( Serial serial ) : base( serial )
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