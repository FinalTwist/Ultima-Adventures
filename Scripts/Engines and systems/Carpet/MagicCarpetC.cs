using System;
using Server;
using Server.Items;


namespace Server.Multis
{
	public class MagicCarpetC : BaseBoat
	{
		public override int NorthID{ get{ return 0xBF; } }
		public override int  EastID{ get{ return 0xC0; } }
		public override int SouthID{ get{ return 0xBF; } }
		public override int  WestID{ get{ return 0xC0; } }


		public override int HoldDistance{ get{ return 3; } }
		public override int TillerManDistance{ get{ return -3; } }


		public override Point2D StarboardOffset{ get{ return new Point2D(  3, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -3, 0 ); } }


		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }


		public override BaseDockedBoat DockedBoat{ get{ return new MagicDockedCarpetC( this ); } }


		[Constructable]
		public MagicCarpetC()
		{
		}


		public MagicCarpetC( Serial serial ) : base( serial )
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


	public class MagicCarpetCDeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetC(); } }


		[Constructable]
		public MagicCarpetCDeed() : base( 0xBF, Point3D.Zero )
		{
			Name = "magic carpet";
		}


		public MagicCarpetCDeed( Serial serial ) : base( serial )
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


	public class MagicDockedCarpetC : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetC(); } }


		public MagicDockedCarpetC( BaseBoat boat ) : base( 0xBF, Point3D.Zero, boat )
		{
		}


		public MagicDockedCarpetC( Serial serial ) : base( serial )
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