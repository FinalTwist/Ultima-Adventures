using System;
using Server;
using Server.Items;

namespace Server.Multis
{
	public class GalleonBarbarian : BaseBoat
	{
		public override int NorthID{ get{ return 0x18; } }
		public override int  EastID{ get{ return 0x19; } }
		public override int SouthID{ get{ return 0x1A; } }
		public override int  WestID{ get{ return 0x1B; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonBarbarian()
		{
		}

		public GalleonBarbarian( Serial serial ) : base( serial )
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

	public class GalleonRoyal : BaseBoat
	{
		public override int NorthID{ get{ return 0x24; } }
		public override int  EastID{ get{ return 0x25; } }
		public override int SouthID{ get{ return 0x26; } }
		public override int  WestID{ get{ return 0x27; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonRoyal()
		{
		}

		public GalleonRoyal( Serial serial ) : base( serial )
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

	public class GalleonExotic : BaseBoat
	{
		public override int NorthID{ get{ return 0x30; } }
		public override int  EastID{ get{ return 0x31; } }
		public override int SouthID{ get{ return 0x32; } }
		public override int  WestID{ get{ return 0x33; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonExotic()
		{
		}

		public GalleonExotic( Serial serial ) : base( serial )
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

	public class GalleonLarge : BaseBoat
	{
		public override int NorthID{ get{ return 0x40; } }
		public override int  EastID{ get{ return 0x41; } }
		public override int SouthID{ get{ return 0x42; } }
		public override int  WestID{ get{ return 0x43; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonLarge()
		{
		}

		public GalleonLarge( Serial serial ) : base( serial )
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

	public class GalleonWreckedBarbarian : BaseBoat
	{
		public override int NorthID{ get{ return 0x1C; } }
		public override int  EastID{ get{ return 0x1D; } }
		public override int SouthID{ get{ return 0x1E; } }
		public override int  WestID{ get{ return 0x1F; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonWreckedBarbarian()
		{
		}

		public GalleonWreckedBarbarian( Serial serial ) : base( serial )
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

	public class GalleonWreckedRoyal : BaseBoat
	{
		public override int NorthID{ get{ return 0x28; } }
		public override int  EastID{ get{ return 0x29; } }
		public override int SouthID{ get{ return 0x2A; } }
		public override int  WestID{ get{ return 0x2B; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonWreckedRoyal()
		{
		}

		public GalleonWreckedRoyal( Serial serial ) : base( serial )
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

	public class GalleonWreckedExotic : BaseBoat
	{
		public override int NorthID{ get{ return 0x34; } }
		public override int  EastID{ get{ return 0x35; } }
		public override int SouthID{ get{ return 0x36; } }
		public override int  WestID{ get{ return 0x37; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonWreckedExotic()
		{
		}

		public GalleonWreckedExotic( Serial serial ) : base( serial )
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

	public class GalleonWreckedLarge : BaseBoat
	{
		public override int NorthID{ get{ return 0x44; } }
		public override int  EastID{ get{ return 0x45; } }
		public override int SouthID{ get{ return 0x46; } }
		public override int  WestID{ get{ return 0x47; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonWreckedLarge()
		{
		}

		public GalleonWreckedLarge( Serial serial ) : base( serial )
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

	public class GalleonRuinedBarbarian : BaseBoat
	{
		public override int NorthID{ get{ return 0x20; } }
		public override int  EastID{ get{ return 0x21; } }
		public override int SouthID{ get{ return 0x22; } }
		public override int  WestID{ get{ return 0x23; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonRuinedBarbarian()
		{
		}

		public GalleonRuinedBarbarian( Serial serial ) : base( serial )
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

	public class GalleonRuinedRoyal : BaseBoat
	{
		public override int NorthID{ get{ return 0x2C; } }
		public override int  EastID{ get{ return 0x2D; } }
		public override int SouthID{ get{ return 0x2E; } }
		public override int  WestID{ get{ return 0x2F; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonRuinedRoyal()
		{
		}

		public GalleonRuinedRoyal( Serial serial ) : base( serial )
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

	public class GalleonRuinedExotic : BaseBoat
	{
		public override int NorthID{ get{ return 0x38; } }
		public override int  EastID{ get{ return 0x39; } }
		public override int SouthID{ get{ return 0x3A; } }
		public override int  WestID{ get{ return 0x3B; } }

		public override int HoldDistance{ get{ return 8; } }
		public override int TillerManDistance{ get{ return -8; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  4, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -4, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return null; } }

		[Constructable]
		public GalleonRuinedExotic()
		{
		}

		public GalleonRuinedExotic( Serial serial ) : base( serial )
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