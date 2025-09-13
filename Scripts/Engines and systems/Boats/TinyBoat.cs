using System;
using Server;
using Server.Items;

namespace Server.Multis
{
	public class TinyBoat : BaseBoat
	{
		public override int NorthID{ get{ return 0x3C; } }
		public override int  EastID{ get{ return 0x3D; } }
		public override int SouthID{ get{ return 0x3E; } }
		public override int  WestID{ get{ return 0x3F; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  2, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -2, 0 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }

		[Constructable]
		public TinyBoat()
		{
		}

		public TinyBoat( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );
		}
	}
}