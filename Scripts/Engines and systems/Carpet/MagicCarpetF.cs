using System;
using Server;
using Server.Items;


namespace Server.Multis
{
	public class MagicCarpetF : BaseBoat
	{
		public override int NorthID{ get{ return 0xC5; } }
		public override int  EastID{ get{ return 0xC6; } }
		public override int SouthID{ get{ return 0xC5; } }
		public override int  WestID{ get{ return 0xC6; } }


		public override int HoldDistance{ get{ return 3; } }
		public override int TillerManDistance{ get{ return -3; } }


		public override Point2D StarboardOffset{ get{ return new Point2D(  3, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -3, 0 ); } }


		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }


		public override BaseDockedBoat DockedBoat{ get{ return new MagicDockedCarpetF( this ); } }


		[Constructable]
		public MagicCarpetF()
		{
		}


		public MagicCarpetF( Serial serial ) : base( serial )
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


	public class MagicCarpetFDeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetF(); } }


		[Constructable]
		public MagicCarpetFDeed() : base( 0xC5, Point3D.Zero )
		{
			Name = "magic carpet";
		}


		public MagicCarpetFDeed( Serial serial ) : base( serial )
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


	public class MagicDockedCarpetF : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetF(); } }


		public MagicDockedCarpetF( BaseBoat boat ) : base( 0xC5, Point3D.Zero, boat )
		{
		}


		public MagicDockedCarpetF( Serial serial ) : base( serial )
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