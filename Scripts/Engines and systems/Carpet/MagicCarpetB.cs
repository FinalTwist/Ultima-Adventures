using System;
using Server;
using Server.Items;


namespace Server.Multis
{
	public class MagicCarpetB : BaseBoat
	{
		public override int NorthID{ get{ return 0xBD; } }
		public override int  EastID{ get{ return 0xBE; } }
		public override int SouthID{ get{ return 0xBD; } }
		public override int  WestID{ get{ return 0xBE; } }


		public override int HoldDistance{ get{ return 3; } }
		public override int TillerManDistance{ get{ return -3; } }


		public override Point2D StarboardOffset{ get{ return new Point2D(  3, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -3, 0 ); } }


		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }


		public override BaseDockedBoat DockedBoat{ get{ return new MagicDockedCarpetB( this ); } }


		[Constructable]
		public MagicCarpetB()
		{
		}


		public MagicCarpetB( Serial serial ) : base( serial )
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


	public class MagicCarpetBDeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetB(); } }


		[Constructable]
		public MagicCarpetBDeed() : base( 0xBD, Point3D.Zero )
		{
			Name = "magic carpet";
		}


		public MagicCarpetBDeed( Serial serial ) : base( serial )
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


	public class MagicDockedCarpetB : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetB(); } }


		public MagicDockedCarpetB( BaseBoat boat ) : base( 0xBD, Point3D.Zero, boat )
		{
		}


		public MagicDockedCarpetB( Serial serial ) : base( serial )
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