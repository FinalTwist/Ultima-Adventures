using System;
using Server;
using Server.Items;


namespace Server.Multis
{
	public class MagicCarpetI : BaseBoat
	{
		public override int NorthID{ get{ return 0xCB; } }
		public override int  EastID{ get{ return 0xCC; } }
		public override int SouthID{ get{ return 0xCB; } }
		public override int  WestID{ get{ return 0xCC; } }


		public override int HoldDistance{ get{ return 3; } }
		public override int TillerManDistance{ get{ return -3; } }


		public override Point2D StarboardOffset{ get{ return new Point2D(  3, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -3, 0 ); } }


		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }


		public override BaseDockedBoat DockedBoat{ get{ return new MagicDockedCarpetI( this ); } }


		[Constructable]
		public MagicCarpetI()
		{
		}


		public MagicCarpetI( Serial serial ) : base( serial )
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


	public class MagicCarpetIDeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetI(); } }


		[Constructable]
		public MagicCarpetIDeed() : base( 0xCB, Point3D.Zero )
		{
			Name = "magic carpet";
		}


		public MagicCarpetIDeed( Serial serial ) : base( serial )
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


	public class MagicDockedCarpetI : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetI(); } }


		public MagicDockedCarpetI( BaseBoat boat ) : base( 0xCB, Point3D.Zero, boat )
		{
		}


		public MagicDockedCarpetI( Serial serial ) : base( serial )
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