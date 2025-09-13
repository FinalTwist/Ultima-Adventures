using System;
using Server;
using Server.Items;


namespace Server.Multis
{
	public class MagicCarpetD : BaseBoat
	{
		public override int NorthID{ get{ return 0xC1; } }
		public override int  EastID{ get{ return 0xC2; } }
		public override int SouthID{ get{ return 0xC1; } }
		public override int  WestID{ get{ return 0xC2; } }


		public override int HoldDistance{ get{ return 3; } }
		public override int TillerManDistance{ get{ return -3; } }


		public override Point2D StarboardOffset{ get{ return new Point2D(  3, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -3, 0 ); } }


		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }


		public override BaseDockedBoat DockedBoat{ get{ return new MagicDockedCarpetD( this ); } }


		[Constructable]
		public MagicCarpetD()
		{
		}


		public MagicCarpetD( Serial serial ) : base( serial )
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


	public class MagicCarpetDDeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetD(); } }


		[Constructable]
		public MagicCarpetDDeed() : base( 0xC1, Point3D.Zero )
		{
			Name = "magic carpet";
		}


		public MagicCarpetDDeed( Serial serial ) : base( serial )
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


	public class MagicDockedCarpetD : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetD(); } }


		public MagicDockedCarpetD( BaseBoat boat ) : base( 0xC1, Point3D.Zero, boat )
		{
		}


		public MagicDockedCarpetD( Serial serial ) : base( serial )
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