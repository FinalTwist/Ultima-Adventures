using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis.Deeds;

namespace Server.Multis.Deeds
{
	public class BlueTentDeed : HouseDeed
	{
		public override int LabelNumber{ get{ return 1041217; } } // deed to a blue tent
		[Constructable]
		public BlueTentDeed() : base( 0x70, new Point3D( 0, 0, 0 ) )
		{
		}

		public BlueTentDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new BlueTent( owner );
		}

		public override Rectangle2D[] Area{ get{ return BlueTent.AreaArray; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}

namespace Server.Multis
{
	public class BlueTent : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -3, -3, 8, 8 )};

		public override int DefaultPrice{ get{ return 40000; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }

		public override Point3D BaseBanLocation{ get{ return new Point3D( 1, 4, 0 ); } }

		public BlueTent( Mobile owner ) : base( 0x70, owner, 500, 4 )
		{
			Price = 12000;
			uint keyValue = CreateKeys( owner );

			SetSign( -1, 5, 9 );
			// Turn sign
			ChangeSignType(0x0bd1);

		}

		public BlueTent ( Serial serial ) : base( serial )
		{
		}


		public override bool IsInside( Point3D p, int height )
		{
			if ( Deleted )
				return false;

			foreach(Rectangle2D rect in Area)
			{
				if(rect.Contains(new Point2D( p.X - X, p.Y - Y )))
					return true;
			}

			return false;
		}

		public override HouseDeed GetDeed() { return new BlueTentDeed(); }

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

namespace Server.Multis.Deeds
{
	public class GreenTentDeed : HouseDeed
	{
		public override int LabelNumber{ get{ return 1041218; } } // deed to a green tent
		[Constructable]
		public GreenTentDeed() : base( 0x72, new Point3D( 0, 0, 0 ) )
		{
		}

		public GreenTentDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new GreenTent( owner );
		}

		public override Rectangle2D[] Area{ get{ return SmallOldHouse.AreaArray; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}

namespace Server.Multis
{
	public class GreenTent : BaseHouse
	{
		public static Rectangle2D[] AreaArray = new Rectangle2D[]{ new Rectangle2D( -3, -3, 8, 8 )};

		public override int DefaultPrice{ get{ return 40000; } }

		public override Rectangle2D[] Area{ get{ return AreaArray; } }

		public override Point3D BaseBanLocation{ get{ return new Point3D( 1, 4, 0 ); } }

		public GreenTent( Mobile owner ) : base( 0x72, owner, 500, 4 )
		{
			Price = 12000;
			uint keyValue = CreateKeys( owner );

			SetSign( -1, 5, 9 );
			// Turn sign
			ChangeSignType(0x0bd1);
		}

		public GreenTent ( Serial serial ) : base( serial )
		{
		}


		public override bool IsInside( Point3D p, int height )
		{
			if ( Deleted )
				return false;

			foreach(Rectangle2D rect in Area)
			{
				if(rect.Contains(new Point2D( p.X - X, p.Y - Y )))
					return true;
			}

			return false;
		}

		public override HouseDeed GetDeed() { return new GreenTentDeed(); }

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