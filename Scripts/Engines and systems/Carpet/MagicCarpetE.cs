using System;
using Server;
using Server.Items;


namespace Server.Multis
{
	public class MagicCarpetE : BaseBoat
	{
		public override int NorthID{ get{ return 0xC3; } }
		public override int  EastID{ get{ return 0xC4; } }
		public override int SouthID{ get{ return 0xC3; } }
		public override int  WestID{ get{ return 0xC4; } }


		public override int HoldDistance{ get{ return 3; } }
		public override int TillerManDistance{ get{ return -3; } }


		public override Point2D StarboardOffset{ get{ return new Point2D(  3, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -3, 0 ); } }


		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }


		public override BaseDockedBoat DockedBoat{ get{ return new MagicDockedCarpetE( this ); } }


		[Constructable]
		public MagicCarpetE()
		{
		}


		public MagicCarpetE( Serial serial ) : base( serial )
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


	public class MagicCarpetEDeed : BaseBoatDeed
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetE(); } }


		[Constructable]
		public MagicCarpetEDeed() : base( 0xC3, Point3D.Zero )
		{
			Name = "magic carpet";
		}


		public MagicCarpetEDeed( Serial serial ) : base( serial )
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


	public class MagicDockedCarpetE : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new MagicCarpetE(); } }


		public MagicDockedCarpetE( BaseBoat boat ) : base( 0xC3, Point3D.Zero, boat )
		{
		}


		public MagicDockedCarpetE( Serial serial ) : base( serial )
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