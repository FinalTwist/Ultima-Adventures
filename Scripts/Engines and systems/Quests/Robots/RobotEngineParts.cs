using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class RobotEngineParts : Item
	{
		[Constructable]
		public RobotEngineParts() : base( 0x34C1 )
		{
			Weight = 1.0;
			Name = "robot engine parts";
		}

		public RobotEngineParts( Serial serial ) : base( serial )
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