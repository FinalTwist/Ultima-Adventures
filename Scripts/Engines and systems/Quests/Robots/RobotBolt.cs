using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class RobotBolt : Item
	{
		[Constructable]
		public RobotBolt() : this( 1 )
		{
		}

		[Constructable]
		public RobotBolt( int amount ) : base( 0x2021 )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Name = "robot bolt";
		}

		public RobotBolt( Serial serial ) : base( serial )
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