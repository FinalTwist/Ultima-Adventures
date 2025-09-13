using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Multis
{
	public class Pyramid : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -15, -15, 32, 32 ) };

		public override int DefaultPrice{ get{ return 366500; } }

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 3, 16, 0 ); } }

		public Pyramid( Mobile owner ) : base( 0x48, owner, 1, 1 )
		{
			uint keyValue = CreateKeys( owner );

			AddSouthDoors( false, 0, 13, 5, keyValue );

			SetSign( 3, 16, 5 );

			BaseDoor door = MakeDoor( false, DoorFacing.NorthCCW );

			if ( door is BaseHouseDoor )
				((BaseHouseDoor)door).Facing = DoorFacing.NorthCCW;

			AddDoor( door, -4, -7, 5 );

			AddEastDoor( false, -4, -6, 5 );
		}

		public Pyramid( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class LargeTent : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -12, -13, 25, 26 ) };

		public override int DefaultPrice{ get{ return 160500; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 1, 13, 0 ); } }

		public LargeTent( Mobile owner ) : base( 0x49, owner, 1576,	788 )
		{
			SetSign( 1, 12, 8 );
		}

		public LargeTent( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class CastleTower : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -6, -7, 13, 14 ) };

		public override int DefaultPrice{ get{ return 160500; } }

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 5, 7, 0 ); } }

		public CastleTower( Mobile owner ) : base( 0x4A, owner, 1576, 788 )
		{
			uint keyValue = CreateKeys( owner );

			AddSouthDoors( false, 3, 5, 5, keyValue );

			SetSign( 5, 6, 9 );
		}

		public CastleTower( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class Fortress : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -15, -16, 32, 33 ) };

		public override int DefaultPrice{ get{ return 1165250; } }

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 4, 16, 0 ); } }

		public Fortress( Mobile owner ) : base( 0x4B, owner, 5688, 3344 )
		{
			uint keyValue = CreateKeys( owner );

			AddSouthDoors( false, 0, 11, 5, keyValue );

			SetSign( 4, 16, 0 );

			AddSouthDoors( false, -10, -6, 5, keyValue );
			AddSouthDoors( false, 0, -6, 5, keyValue );
			AddSouthDoors( false, 10, -6, 5, keyValue );
			AddSouthDoors( false, -10, 7, 5, keyValue );
			AddSouthDoors( false, 10, 7, 5, keyValue );
			AddSouthDoors( false, 10, -5, 27, keyValue );
			AddSouthDoors( false, 10, 2, 27, keyValue );
			AddSouthDoors( false, -10, -5, 27, keyValue );
			AddSouthDoors( false, -7, -4, 49, keyValue );
			AddSouthDoors( false, 7, -4, 49, keyValue );
		}

		public Fortress( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStorySandstoneHouse : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -5, 14, 10 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 7, -4, 0 ); } }

		public NewTwoStorySandstoneHouse( Mobile owner ) : base( 0x4C, owner, 1, 1 )
		{
			SetSignFace( 7, -4, 0, 0xBD1 );

			BaseDoor door1 = AddHouseDoor( 6, 0, 6, 0x6AD );
			BaseDoor door2 = AddHouseDoor( 6, -1, 6, 0x6AF );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStorySandstoneHouse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewBrickHouseWithSteeple : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -6, -6, 13, 13 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 6, 4, 0 ); } }

		public NewBrickHouseWithSteeple( Mobile owner ) : base( 0x4D, owner, 1, 1 )
		{
			SetSignFace( 6, 4, 0, 0xBD1 );

			AddHouseDoor( -3, 5, 5, 0x6A5 );
			AddHouseDoor( -2, -3, 5, 0x6AF );

			BaseDoor door1 = AddHouseDoor( 5, -1, 5, 0x6AD );
			BaseDoor door2 = AddHouseDoor( 5, -2, 5, 0x6AF );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewBrickHouseWithSteeple( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStoryBrickHouse : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -5, 11, 12 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -4, 5, 0 ); } }

		public NewTwoStoryBrickHouse( Mobile owner ) : base( 0x4E, owner, 1, 1 )
		{
			SetSignFace( -4, 5, 0, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 5, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( 2, 5, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStoryBrickHouse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewPlasterHousePictureWindow : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -7, 14, 16 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 6, -6, 0 ); } }

		public NewPlasterHousePictureWindow( Mobile owner ) : base( 0x4F, owner, 1, 1 )
		{
			SetSignFace( 6, -6, 0, 0xBD1 );

			BaseDoor door1 = AddHouseDoor( 5, -3, 5, 0x6AD );
			BaseDoor door2 = AddHouseDoor( 5, -4, 5, 0x6AF );

			door1.Link = door2;
			door2.Link = door1;

			BaseDoor door3 = AddHouseDoor( -2, -1, 5, 0x6A5 );
			BaseDoor door4 = AddHouseDoor( -1, -1, 5, 0x6A7 );

			door3.Link = door4;
			door4.Link = door3;
		}

		public NewPlasterHousePictureWindow( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class Wagon : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -7, 11, 15 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 6, 8, 0 ); } }

		public Wagon( Mobile owner ) : base( 0x94, owner, 1, 1 )
		{
			SetSignFace( 4, 5, 5, 0x0BD2 );
			AddHouseDoor( 0, 4, 5, 0x06A5 );
		}

		public Wagon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStoryBrickHome : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -8, 14, 17 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -3, 7, 0 ); } }

		public NewTwoStoryBrickHome( Mobile owner ) : base( 0x50, owner, 1, 1 )
		{
			SetSignFace( -3, 7, 0, 0xBD2 );

			AddHouseDoor( 2, -2, 5, 0x6A7 );

			BaseDoor door1 = AddHouseDoor( 1, 5, 5, 0x6A7 );
			BaseDoor door2 = AddHouseDoor( 0, 5, 5, 0x6A5 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStoryBrickHome( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStoryWoodenHomeWithPorch : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -5, 14, 12 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 5, 4, 5 ); } }

		public NewTwoStoryWoodenHomeWithPorch( Mobile owner ) : base( 0x51, owner, 1, 1 )
		{
			SetSignFace( 5, 4, 5, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( -4, 3, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -3, 3, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;

			BaseDoor door3 = AddHouseDoor( 0, 2, 25, 0x6AD );
			BaseDoor door4 = AddHouseDoor( 0, 1, 25, 0x6AF );

			door3.Link = door4;
			door4.Link = door3;
		}

		public NewTwoStoryWoodenHomeWithPorch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallStoneShoppe : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -5, 14, 12 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -6, 6, 5 ); } }

		public NewSmallStoneShoppe( Mobile owner ) : base( 0x52, owner, 1, 1 )
		{
			SetSignFace( -6, 6, 5, 0xBD2 );

			AddHouseDoor( -4, -1, 5, 0x6A5 );
			AddHouseDoor( -3, -3, 5, 0x6AF );

			BaseDoor door1 = AddHouseDoor( -3, 5, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -2, 5, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewSmallStoneShoppe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewWoodenHomePorch : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -8, -5, 17, 10 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 2, 4, 0 ); } }

		public NewWoodenHomePorch( Mobile owner ) : base( 0x53, owner, 1, 1 )
		{
			SetSignFace( 2, 4, 0, 0xBD2 );

			AddHouseDoor( 0, 1, 5, 0x6A5 );
		}

		public NewWoodenHomePorch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallStoneTower : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -6, 11, 13 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -2, 6, 2 ); } }

		public NewSmallStoneTower( Mobile owner ) : base( 0x54, owner, 1, 1 )
		{
			SetSignFace( -2, 6, 2, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 5, 5, 0x675 );
			BaseDoor door2 = AddHouseDoor( 0, 5, 5, 0x677 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewSmallStoneTower( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewThreeStoryStoneVilla : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -7, 16, 14 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -6, 6, 5 ); } }

		public NewThreeStoryStoneVilla( Mobile owner ) : base( 0x55, owner, 1, 1 )
		{
			SetSignFace( -6, 6, 5, 0xBD2 );

			AddHouseDoor( -3, -1, 25, 0x6A5 );
			AddHouseDoor( 0, -2, 25, 0x6AF );

			BaseDoor door1 = AddHouseDoor( -4, -1, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -3, -1, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewThreeStoryStoneVilla( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStorySmallStoneHome : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -4, -4, 9, 9 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 3, 3, 5 ); } }

		public NewTwoStorySmallStoneHome( Mobile owner ) : base( 0x56, owner, 1, 1 )
		{
			SetSignFace( 3, 3, 5, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 2, 2, 5, 0x6A7 );
			BaseDoor door2 = AddHouseDoor( 1, 2, 5, 0x6A5 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStorySmallStoneHome( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStorySmallStoneHouse : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -4, -4, 9, 9 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 3, 3, 5 ); } }

		public NewTwoStorySmallStoneHouse( Mobile owner ) : base( 0x57, owner, 1, 1 )
		{
			SetSignFace( 3, 3, 5, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 2, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( 2, 2, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStorySmallStoneHouse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStorySmallStoneDwelling : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -4, -4, 9, 9 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 3, 3, 5 ); } }

		public NewTwoStorySmallStoneDwelling( Mobile owner ) : base( 0x58, owner, 1, 1 )
		{
			SetSignFace( 3, 3, 5, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 2, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( 2, 2, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStorySmallStoneDwelling( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStorySmallWoodenDwelling : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -4, -4, 9, 9 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 3, 3, 5 ); } }

		public NewTwoStorySmallWoodenDwelling( Mobile owner ) : base( 0x59, owner, 1, 1 )
		{
			SetSignFace( 3, 3, 5, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 2, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( 2, 2, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStorySmallWoodenDwelling( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewWoodenMansion : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -7, 15, 14 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 6, 6, 5 ); } }

		public NewWoodenMansion( Mobile owner ) : base( 0x5A, owner, 1, 1 )
		{
			SetSignFace( 6, 6, 5, 0xBD1 );

			AddHouseDoor( -1, -1, 25, 0x6A5 );
			AddHouseDoor( 3, -1, 25, 0x6A7 );
			AddHouseDoor( 0, -2, 25, 0x6AF );
			AddHouseDoor( 1, -2, 5, 0x6AF );

			BaseDoor door1 = AddHouseDoor( 0, -1, 45, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( 1, -1, 45, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;

			BaseDoor door3 = AddHouseDoor( 1, 1, 5, 0x6AF );
			BaseDoor door4 = AddHouseDoor( 1, 2, 5, 0x6AD );

			door3.Link = door4;
			door4.Link = door3;
		}

		public NewWoodenMansion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallStoneStoreFront : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -3, 12, 8 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 0, 4, 6 ); } }

		public NewSmallStoneStoreFront( Mobile owner ) : base( 0x5B, owner, 1, 1 )
		{
			SetSignFace( 0, 4, 6, 0xBD2 );

			AddHouseDoor( 0, -1, 7, 0x6AF );

			BaseDoor door1 = AddHouseDoor( -3, 3, 7, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -2, 3, 7, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewSmallStoneStoreFront( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallStoneHomeEast : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -3, -3, 8, 8 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 4, -2, 0 ); } }

		public NewSmallStoneHomeEast( Mobile owner ) : base( 0x5C, owner, 1, 1 )
		{
			SetSignFace( 4, -2, 0, 0xBD1 );

			AddHouseDoor( 3, 0, 7, 0x6AF );
		}

		public NewSmallStoneHomeEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewFancyStoneWoodHome : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -5, 12, 11 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -4, 5, 0 ); } }

		public NewFancyStoneWoodHome( Mobile owner ) : base( 0x5D, owner, 1, 1 )
		{
			SetSignFace( -4, 5, 0, 0xBD2 );

			AddHouseDoor( 0, 4, 7, 0x6A5 );
		}

		public NewFancyStoneWoodHome( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewFancyWoodenStoneHouse : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -7, 12, 16 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 6, -4, 0 ); } }

		public NewFancyWoodenStoneHouse( Mobile owner ) : base( 0x5E, owner, 1, 1 )
		{
			SetSignFace( 6, -4, 0, 0xBD1 );

			AddHouseDoor( -2, 2, 27, 0x6A5 );
			AddHouseDoor( 2, 4, 27, 0x6AD );
			AddHouseDoor( 1, 0, 7, 0x6AF );
			AddHouseDoor( -1, 0, 27, 0x6AF );
			AddHouseDoor( -2, -3, 27, 0x6A7 );
			AddHouseDoor( -1, -2, 7, 0x6A5 );
			AddHouseDoor( -1, 2, 7, 0x6A5 );
		}

		public NewFancyWoodenStoneHouse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallStoneHouseEast : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -3, -3, 8, 8 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 4, -2, 0 ); } }

		public NewSmallStoneHouseEast( Mobile owner ) : base( 0x5F, owner, 1, 1 )
		{
			SetSignFace( 4, -2, 0, 0xBD1 );

			AddHouseDoor( 3, 0, 7, 0x6AF );
		}

		public NewSmallStoneHouseEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallWoodenShackPorch : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -5, 10, 11 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -4, 4, 4 ); } }

		public NewSmallWoodenShackPorch( Mobile owner ) : base( 0x60, owner, 1, 1 )
		{
			SetSignFace( -4, 4, 4, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( -1, 1, 6, 0x6AD );
			BaseDoor door2 = AddHouseDoor( -1, 0, 6, 0x6AF );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewSmallWoodenShackPorch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewPlainPlasterHouse : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -5, 14, 11 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -6, 4, 0 ); } }

		public NewPlainPlasterHouse( Mobile owner ) : base( 0x61, owner, 1, 1 )
		{
			SetSignFace( -6, 4, 0, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( -1, 4, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( 0, 4, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewPlainPlasterHouse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewPlainStoneHouse : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -6, -5, 13, 12 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -5, 6, 5 ); } }

		public NewPlainStoneHouse( Mobile owner ) : base( 0x62, owner, 1, 1 )
		{
			SetSignFace( -5, 6, 5, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( -4, 5, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -3, 5, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewPlainStoneHouse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewPlasterHomeDirtDeck : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -7, 11, 14 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -2, 7, 0 ); } }

		public NewPlasterHomeDirtDeck( Mobile owner ) : base( 0x63, owner, 1, 1 )
		{
			SetSignFace( -2, 7, 0, 0xBD2 );

			AddHouseDoor( 0, 1, 25, 0x6A5 );
			AddHouseDoor( 0, 3, 5, 0x6A5 );
		}

		public NewPlasterHomeDirtDeck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewWoodenHomeUpperDeck : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -5, 14, 10 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -5, 4, 0 ); } }

		public NewWoodenHomeUpperDeck( Mobile owner ) : base( 0x80, owner, 1, 1 )
		{
			SetSignFace( -5, 4, 0, 0xBD2 );

			AddHouseDoor( 3, 0, 5, 0x6AF );
			AddHouseDoor( 1, 0, 25, 0x6AF );

			BaseDoor door1 = AddHouseDoor( -3, 3, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -2, 3, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewWoodenHomeUpperDeck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStoryStoneVilla : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -8, 11, 17 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 4, 8, 0); } }

		public NewTwoStoryStoneVilla( Mobile owner ) : base( 0x81, owner, 1, 1 )
		{
			SetSignFace( 4, 8, 0, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( -2, 1, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -1, 1, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStoryStoneVilla( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewTwoStorySmallPlasterDwelling : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -4, -4, 9, 9 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 3, 3, 8 ); } }

		public NewTwoStorySmallPlasterDwelling( Mobile owner ) : base( 0x82, owner, 1, 1 )
		{
			SetSignFace( 3, 3, 8, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 2, 7, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( 2, 2, 7, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewTwoStorySmallPlasterDwelling( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallStoneTemple : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -5, -4, 11, 9 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 4, -3, 0 ); } }

		public NewSmallStoneTemple( Mobile owner ) : base( 0x83, owner, 1, 1 )
		{
			SetSignFace( 4, -3, 0, 0xBD1 );
		}

		public NewSmallStoneTemple( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallSandstoneWorkshop : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -3, -4, 8, 9 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 4, 4, 11 ); } }

		public NewSmallSandstoneWorkshop( Mobile owner ) : base( 0x84, owner, 1, 1 )
		{
			SetSignFace( 4, 4, 11, 0xBD2 );
		}

		public NewSmallSandstoneWorkshop( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewStoneHomeWithEnclosedPatio : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -7, 14, 15 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 6, 0, 0 ); } }

		public NewStoneHomeWithEnclosedPatio( Mobile owner ) : base( 0x85, owner, 1, 1 )
		{
			SetSignFace( 6, 0, 0, 0xBD1 );

			AddHouseDoor( -1, 3, 5, 0x6A5 );

			BaseDoor door1 = AddHouseDoor( 5, -2, 5, 0x6AD );
			BaseDoor door2 = AddHouseDoor( 5, -3, 5, 0x6AF );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewStoneHomeWithEnclosedPatio( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewLogCabin : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -8, -5, 17, 10 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 2, 4, 0 ); } }

		public NewLogCabin( Mobile owner ) : base( 0x86, owner, 1, 1 )
		{
			SetSignFace( 2, 4, 0, 0xBD2 );

			AddHouseDoor( 0, 1, 5, 0x6A5 );
		}

		public NewLogCabin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallLogCabinWithDeck : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -4, 15, 9 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 1, 4, 10 ); } }

		public NewSmallLogCabinWithDeck( Mobile owner ) : base( 0x88, owner, 1, 1 )
		{
			SetSignFace( 1, 4, 10, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 0, 10, 0x6AD );
			BaseDoor door2 = AddHouseDoor( 1, -1, 10, 0x6AF );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewSmallLogCabinWithDeck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewRaisedBrickHome : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -6, -7, 13, 14 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 3, 6, 0 ); } }

		public NewRaisedBrickHome( Mobile owner ) : base( 0x89, owner, 1, 1 )
		{
			SetSignFace( 3, 6, 0, 0xBD1 );

			BaseDoor door1 = AddHouseDoor( 2, -3, 25, 0x6AF );
			BaseDoor door2 = AddHouseDoor( 2, -2, 25, 0x6AD );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewRaisedBrickHome( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewBrickArena : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -9, -11, 18, 22 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -9, 10, 0 ); } }

		public NewBrickArena( Mobile owner ) : base( 0x8A, owner, 1, 1 )
		{
			SetSignFace( -9, 10, 0, 0xBD2 );

			AddHouseDoor( 0, 9, 6, 0x675 );
		}

		public NewBrickArena( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewStoneFort : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -8, -7, 17, 15 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -5, 7, 7 ); } }

		public NewStoneFort( Mobile owner ) : base( 0x8B, owner, 1, 1 )
		{
			SetSignFace( -5, 7, 7, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 6, 7, 0x675 );
			BaseDoor door2 = AddHouseDoor( 0, 6, 7, 0x677 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewStoneFort( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewOldStoneHomeShoppe : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -7, 16, 14 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 8, -6, 5 ); } }

		public NewOldStoneHomeShoppe( Mobile owner ) : base( 0x8E, owner, 1, 1 )
		{
			SetSignFace( 8, -6, 5, 0xBD1 );

			BaseDoor door1 = AddHouseDoor( 7, -3, 5, 0x6AD );
			BaseDoor door2 = AddHouseDoor( 7, -4, 5, 0x6AF );

			door1.Link = door2;
			door2.Link = door1;

			BaseDoor door3 = AddHouseDoor( 0, 1, 5, 0x6AF );
			BaseDoor door4 = AddHouseDoor( 0, 2, 5, 0x6AD );

			door3.Link = door4;
			door4.Link = door3;
		}

		public NewOldStoneHomeShoppe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallBrickCastle : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -6, 14, 13 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -5, 6, 0 ); } }

		public NewSmallBrickCastle( Mobile owner ) : base( 0x8F, owner, 1, 1 )
		{
			SetSignFace( -6, 6, 0, 0xBD2 );

			AddHouseDoor( 0, 0, 25, 0x675 );
		}

		public NewSmallBrickCastle( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewSmallWizardTower : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -8, -6, 17, 13 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -2, 6, 0 ); } }

		public NewSmallWizardTower( Mobile owner ) : base( 0x90, owner, 1, 1 )
		{
			SetSignFace( -2, 6, 0, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 0, 5, 5, 0x677 );
			BaseDoor door2 = AddHouseDoor( 1, 5, 5, 0x675 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewSmallWizardTower( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewBrickHomeWithFrontDeck : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -7, 15, 14 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( 0, 7, 0 ); } }

		public NewBrickHomeWithFrontDeck( Mobile owner ) : base( 0x91, owner, 1, 1 )
		{
			SetSignFace( 0, 6, 0, 0xBD2 );

			BaseDoor door1 = AddHouseDoor( 1, 3, 5, 0x6AD );
			BaseDoor door2 = AddHouseDoor( 1, 2, 5, 0x6AF );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewBrickHomeWithFrontDeck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewMarbleShoppe : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -6, -5, 13, 12 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -5, 6, 1 ); } }

		public NewMarbleShoppe( Mobile owner ) : base( 0x92, owner, 1, 1 )
		{
			SetSignFace( -5, 6, 1, 0xBD2 );

			AddHouseDoor( -2, -3, 5, 0x6AF );
			AddHouseDoor( -3, -1, 5, 0x6A5 );

			BaseDoor door1 = AddHouseDoor( -2, 5, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -1, 5, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewMarbleShoppe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////
	public class NewBrickHomeWithLargePorch : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -7, -7, 16, 14 ) };

		public override HousePlacementEntry ConvertEntry{ get{ return HousePlacementEntry.ThreeStoryFoundations[37]; } }
		public override int ConvertOffsetY{ get{ return -1; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }
		public override Point3D BaseBanLocation{ get{ return new Point3D( -6, 5, 0 ); } }

		public NewBrickHomeWithLargePorch( Mobile owner ) : base( 0x93, owner, 1, 1 )
		{
			SetSignFace( -6, 5, 0, 0xBD2 );

			AddHouseDoor( 4, -3, 5, 0x6AF );

			BaseDoor door1 = AddHouseDoor( -4, 4, 5, 0x6A5 );
			BaseDoor door2 = AddHouseDoor( -3, 4, 5, 0x6A7 );

			door1.Link = door2;
			door2.Link = door1;
		}

		public NewBrickHomeWithLargePorch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}