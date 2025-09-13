using System;
using Server;
using Server.Items;


namespace Server.Multis
{
	public class MagicCarpetH : BaseBoat
	{
		public override int NorthID{ get{ return 0xC9; } }
		public override int  EastID{ get{ return 0xCA; } }
		public override int SouthID{ get{ return 0xC9; } }
		public override int  WestID{ get{ return 0xCA; } }


		public override int HoldDistance{ get{ return 3; } }
		public override int TillerManDistance{ get{ return -3; } }


		public override Point2D StarboardOffset{ get{ return new Point2D(  3, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -3, 0 ); } }


		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }


		public override BaseDockedBoat DockedBoat{ get{ return new MagicDockedCarpetH( this ); } }


		[Constructable]
		public MagicCarpetH()
		{
		}


		public MagicCarpetH( Serial serial ) : base( serial )
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


	public class MagicCarpetHDeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetH(); } }


		[Constructable]
		public MagicCarpetHDeed() : base( 0xC9, Point3D.Zero )
		{
			Name = "magic carpet";
		}


		public MagicCarpetHDeed( Serial serial ) : base( serial )
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


	public class MagicDockedCarpetH : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetH(); } }


		public MagicDockedCarpetH( BaseBoat boat ) : base( 0xC9, Point3D.Zero, boat )
		{
		}


		public MagicDockedCarpetH( Serial serial ) : base( serial )
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