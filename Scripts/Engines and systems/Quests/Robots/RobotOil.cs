using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class RobotOil : Item
	{
		[Constructable]
		public RobotOil() : this( 1 )
		{
		}

		[Constructable]
		public RobotOil( int amount ) : base( 0x3543 )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Name = "robot oil";
		}

		public RobotOil( Serial serial ) : base( serial )
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