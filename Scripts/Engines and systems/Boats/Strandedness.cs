using System;
using System.IO;
using Server;
using Server.Mobiles;
using Server.Regions;

namespace Server.Misc
{
	public class Strandedness
	{
		private static Point2D[] m_Felucca = new Point2D[]
			{
				new Point2D( 615, 390 ), new Point2D( 1662, 789 ), new Point2D( 919, 1241 ),
				new Point2D( 1357, 1570 ), new Point2D( 1122, 2829 ), new Point2D( 1863, 3227 ),
				new Point2D( 2415, 3343 ), new Point2D( 1991, 3659 ), new Point2D( 3409, 3554 ),
				new Point2D( 3068, 2837 ), new Point2D( 3701, 2619 ), new Point2D( 4416, 3241 ),
				new Point2D( 3468, 1503 ), new Point2D( 2057, 1472 ), new Point2D( 2088, 379 ),
				new Point2D( 3271, 522 ), new Point2D( 4303, 435 ), new Point2D( 4401, 1075 )
			};
		private static Point2D[] m_Trammel = new Point2D[]
			{
				new Point2D( 4526, 1367 ), new Point2D( 3764, 1339 ), new Point2D( 4116, 2899 ),
				new Point2D( 1546, 2683 ), new Point2D( 1749, 2015 ), new Point2D( 842, 1378 ),
				new Point2D( 864, 873 ), new Point2D( 732, 216 ), new Point2D( 3199, 492 )
			};
		private static Point2D[] m_Ilshenar = new Point2D[]
			{
				new Point2D( 585, 1029 ), new Point2D( 922, 1003 ), 
				new Point2D( 784, 1387 ), new Point2D( 326, 1378 )
			};
		private static Point2D[] m_Malas = new Point2D[]
			{
				new Point2D( 121, 918 ), new Point2D( 73, 1330 ), new Point2D( 484, 378 ),
				new Point2D( 1327, 505 ), new Point2D( 1387, 1015 ), new Point2D( 1352, 1789 )
			};
		private static Point2D[] m_Tokuno = new Point2D[]
			{
				new Point2D( 649, 909 ), new Point2D( 148, 1243 ), new Point2D( 453, 1284 ),
				new Point2D( 277, 948 )
			};
		private static Point2D[] m_TerMur = new Point2D[]
			{
				new Point2D( 706, 116 ), new Point2D( 856, 664 ), new Point2D( 881, 976 ),
				new Point2D( 346, 805 ), new Point2D( 376, 307 ), new Point2D( 625, 209 )
			};
		private static Point2D[] m_Umber = new Point2D[]
			{
				new Point2D( 1161, 3205 ), new Point2D( 933, 3469 ), new Point2D( 1128, 3766 ),
				new Point2D( 1564, 3998 ), new Point2D( 1970, 3812 ), new Point2D( 2073, 3477 ),
				new Point2D( 1681, 3303 ), new Point2D( 1428, 3328 )
			};
		private static Point2D[] m_Bottle = new Point2D[]
			{
				new Point2D( 6599, 1066 ), new Point2D( 6500, 1502 ), new Point2D( 6540, 2020 ),
				new Point2D( 6701, 2299 ), new Point2D( 6931, 2056 ), new Point2D( 6918, 1770 ),
				new Point2D( 6837, 1262 )
			};
		private static Point2D[] m_Ambrosia = new Point2D[]
			{
				new Point2D( 5930, 3571 ), new Point2D( 5775, 3975 )
			};

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( EventSink_Login );
		}

		private static bool IsStranded( Mobile from )
		{
			Map map = from.Map;

			if ( map == null )
				return false;

			if (	Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) ) || 
					from.Region.IsPartOf( typeof( OutDoorRegion ) ) || 
					from.Region.IsPartOf( typeof( OutDoorBadRegion ) ) || 
					from.Region.IsPartOf( typeof( VillageRegion ) ) )
			{
				object surface = map.GetTopSurface( from.Location );

				if ( surface is LandTile ) {
					int id = ((LandTile)surface).ID;

					return (id >= 168 && id <= 171)
						|| (id >= 310 && id <= 311);
				} else if ( surface is StaticTile ) {
					int id = ((StaticTile)surface).ID;

					return (id >= 0x1796 && id <= 0x17B2);
				}
			}

			return false;
		}

		public static void EventSink_Login( LoginEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !IsStranded( from ) )
				return;

			Map map = from.Map;

			Point2D[] list;

			if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Land of Lodoria" )
				list = m_Felucca;
			else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Serpent Island" )
				list = m_Malas;
			else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Isles of Dread" )
				list = m_Tokuno;
			else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Savaged Empire" )
				list = m_TerMur;
			else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Bottle World of Kuldar" )
				list = m_Bottle;
			else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Island of Umber Veil" )
				list = m_Umber;
			else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Land of Ambrosia" )
				list = m_Ambrosia;
			else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" )
				list = m_Ilshenar;
			else
				list = m_Trammel;

			Point2D p = Point2D.Zero;
			double pdist = double.MaxValue;

			for ( int i = 0; i < list.Length; ++i )
			{
				double dist = from.GetDistanceToSqrt( list[i] );

				if ( dist < pdist )
				{
					p = list[i];
					pdist = dist;
				}
			}

			int x = p.X, y = p.Y;
			int z;
			bool canFit = false;

			z = map.GetAverageZ( x, y );
			canFit = map.CanSpawnMobile( x, y, z );

			for ( int i = 1; !canFit && i <= 40; i += 2 )
			{
				for ( int xo = -1; !canFit && xo <= 1; ++xo )
				{
					for ( int yo = -1; !canFit && yo <= 1; ++yo )
					{
						if ( xo == 0 && yo == 0 )
							continue;

						x = p.X + (xo * i);
						y = p.Y + (yo * i);
						z = map.GetAverageZ( x, y );
						canFit = map.CanSpawnMobile( x, y, z );
					}
				}
			}

			if ( canFit )
				from.Location = new Point3D( x, y, z );
		}

		public static void CabinGoneByeBye( Mobile from, string world )
		{
			Map map = from.Map;

			Point2D[] list;

			if ( world == "the Land of Lodoria" ){ list = m_Felucca; map = Map.Felucca; }
			else if ( world == "the Serpent Island" ){ list = m_Malas; map = Map.Malas; }
			else if ( world == "the Isles of Dread" ){ list = m_Tokuno; map = Map.Tokuno; }
			else if ( world == "the Savaged Empire" ){ list = m_TerMur; map = Map.TerMur; }
			else if ( world == "the Bottle World of Kuldar" ){ list = m_Bottle; map = Map.Trammel; }
			else if ( world == "the Island of Umber Veil" ){ list = m_Umber; map = Map.Trammel; }
			else if ( world == "the Land of Ambrosia" ){ list = m_Ambrosia; map = Map.Trammel; }
			else if ( world == "the Underworld" ){ list = m_Ilshenar; map = Map.Ilshenar; }
			else{ list = m_Trammel; map = Map.Trammel; }

			Point2D p = Point2D.Zero;
			double pdist = double.MaxValue;

			for ( int i = 0; i < list.Length; ++i )
			{
				double dist = from.GetDistanceToSqrt( list[i] );

				if ( dist < pdist )
				{
					p = list[i];
					pdist = dist;
				}
			}

			int x = p.X, y = p.Y;
			int z;
			bool canFit = false;

			z = map.GetAverageZ( x, y );
			canFit = map.CanSpawnMobile( x, y, z );

			for ( int i = 1; !canFit && i <= 40; i += 2 )
			{
				for ( int xo = -1; !canFit && xo <= 1; ++xo )
				{
					for ( int yo = -1; !canFit && yo <= 1; ++yo )
					{
						if ( xo == 0 && yo == 0 )
							continue;

						x = p.X + (xo * i);
						y = p.Y + (yo * i);
						z = map.GetAverageZ( x, y );
						canFit = map.CanSpawnMobile( x, y, z );
					}
				}
			}

			if ( canFit )
			{
				Point3D loc = new Point3D( x, y, z );
				BaseCreature.TeleportPets( from, loc, map, false );
				from.MoveToWorld ( loc, map );
			}
		}
	}
}