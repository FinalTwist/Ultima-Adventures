using System;
using Server;
using Server.Items;

namespace Server.Multis
{
	public class MediumBoat : BaseBoat
	{
		public override int NorthID{ get{ return 0x8; } }
		public override int  EastID{ get{ return 0x9; } }
		public override int SouthID{ get{ return 0xA; } }
		public override int  WestID{ get{ return 0xB; } }

		public override int HoldDistance{ get{ return 4; } }
		public override int TillerManDistance{ get{ return -5; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  2, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -2, 0 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return new MediumDockedBoat( this ); } }

		[Constructable]
		public MediumBoat()
		{
		}

		public MediumBoat( Serial serial ) : base( serial )
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

	public class MediumBoatDeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MediumBoat(); } }

		[Constructable]
		public MediumBoatDeed() : base( 0x8, Point3D.Zero )
		{
			Name = "medium sized ship";
		}

		public MediumBoatDeed( Serial serial ) : base( serial )
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

	public class MediumDockedBoat : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MediumBoat(); } }

		public MediumDockedBoat( BaseBoat boat ) : base( 0x8, Point3D.Zero, boat )
		{
		}

		public MediumDockedBoat( Serial serial ) : base( serial )
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