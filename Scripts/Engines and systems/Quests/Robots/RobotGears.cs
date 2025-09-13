using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class RobotGears : Item
	{
		[Constructable]
		public RobotGears() : this( 1 )
		{
		}

		[Constructable]
		public RobotGears( int amount ) : base( 0x202E )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Name = "robot gears";
		}

		public RobotGears( Serial serial ) : base( serial )
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