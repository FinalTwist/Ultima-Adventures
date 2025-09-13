using System;
using Server;
using Server.Items;


namespace Server.Multis
{
	public class MagicCarpetA : BaseBoat
	{
		public override int NorthID{ get{ return 0xBB; } }
		public override int  EastID{ get{ return 0xBC; } }
		public override int SouthID{ get{ return 0xBB; } }
		public override int  WestID{ get{ return 0xBC; } }


		public override int HoldDistance{ get{ return 3; } }
		public override int TillerManDistance{ get{ return -3; } }


		public override Point2D StarboardOffset{ get{ return new Point2D(  3, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -3, 0 ); } }


		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }


		public override BaseDockedBoat DockedBoat{ get{ return new MagicDockedCarpetA( this ); } }


		[Constructable]
		public MagicCarpetA()
		{
		}


		public MagicCarpetA( Serial serial ) : base( serial )
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


	public class MagicCarpetADeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetA(); } }


		[Constructable]
		public MagicCarpetADeed() : base( 0xBB, Point3D.Zero )
		{
			Name = "magic carpet";
		}


		public MagicCarpetADeed( Serial serial ) : base( serial )
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


	public class MagicDockedCarpetA : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetA(); } }


		public MagicDockedCarpetA( BaseBoat boat ) : base( 0xBB, Point3D.Zero, boat )
		{
		}


		public MagicDockedCarpetA( Serial serial ) : base( serial )
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