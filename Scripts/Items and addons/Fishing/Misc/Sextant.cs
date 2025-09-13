using System;
using Server.Network;
using Server.Misc;
using Server.Regions;

namespace Server.Items
{
	public class Sextant : Item
	{
		[Constructable]
		public Sextant() : base( 0x1058 )
		{
			Weight = 2.0;
		}

		public Sextant( Serial serial ) : base( serial )
		{
		}

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

		public override void OnDoubleClick( Mobile from )
		{
			string world = Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y );

			int xLong = 0, yLat = 0;
			int xMins = 0, yMins = 0;
			bool xEast = false, ySouth = false;

			if ( world == "the Underworld" && !(this is MagicSextant) ){ from.SendMessage( "You will need a magical sextant to see the stars through the cavern ceiling!" ); } 
			else if ( Sextant.Format( from.Location, from.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
			{
				string location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
				from.LocalOverheadMessage( MessageType.Regular, from.SpeechHue, false, location );
			}
			else if ( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) == "Ravendark Woods" ) { from.SendMessage( "You can't use a sextant as the sun and stars are blocked by the evil darkness here!" ); }
			else if ( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) == "the Village of Ravendark" ) { from.SendMessage( "You can't use a sextant as the sun and stars are blocked by the evil darkness here!" ); }
			else if ( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) == "the Ranger Outpost" ) { from.SendMessage( "You can't use a sextant as the mountain clouds block the sky!" ); } 
			else if ( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) == "the Valley of Dark Druids" ) { from.SendMessage( "The druids mask this valley with thick clouds!" ); } 
			else { from.SendMessage( "The sextant does not seem to work here!" ); } 
		}

		public static bool ComputeMapDetails( Map map, int x, int y, out int xCenter, out int yCenter, out int xWidth, out int yHeight )
		{
			xWidth = 0;
			yHeight = 0;

			int startX = -1;
			int startY = -1;

			Point3D location = new Point3D( x, y, 0 );
			string world = Worlds.GetMyWorld( map, location, x, y );

			if ( world == "the Moon of Luna" && x >= 5801 && y >= 2716 && x <= 6125 && y <= 3034 )
			{
				xWidth = 324; yHeight = 411; startX = 5801; startY = 2716;
			}
			if ( world == "the Land of Sosaria" && x >= 0 && y >= 0 && x <= 5120 && y <= 3127 )
			{
				xWidth = 5120; yHeight = 3127; startX = 0; startY = 0;
			}
			else if ( world == "the Land of Lodoria" && x >= 0 && y >= 0 && x <= 5120 && y <= 4095 )
			{
				xWidth = 5120; yHeight = 4095; startX = 0; startY = 0;
			}
			else if ( world == "the Serpent Island" && x >= 0 && y >= 0 && x <= 1870 && y <= 2047 )
			{
				xWidth = 1870; yHeight = 2047; startX = 0; startY = 0;
			}
			else if ( world == "the Isles of Dread" && x >= 0 && y >= 0 && x <= 1447 && y <= 1447 )
			{
				xWidth = 1447; yHeight = 1447; startX = 0; startY = 0;
			}
			else if ( world == "the Savaged Empire" && x >= 136 && y >= 8 && x <= 1160 && y <= 1792 )
			{
				xWidth = 1024; yHeight = 1784; startX = 136; startY = 8;
			}
			else if ( world == "the Land of Ambrosia" && x >= 5122 && y >= 3036 && x <= 6126 && y <= 4095 )
			{
				xWidth = 1004; yHeight = 1059; startX = 5122; startY = 3036;
			}
			else if ( world == "the Island of Umber Veil" && x >= 699 && y >= 3129 && x <= 2272 && y <= 4095 )
			{
				xWidth = 1573; yHeight = 966; startX = 699; startY = 3129;
			}
			else if ( world == "the Bottle World of Kuldar" && x >= 6127 && y >= 828 && x <= 7168 && y <= 2743 )
			{
				xWidth = 1041; yHeight = 1915; startX = 6127; startY = 828;
			}
			else if ( world == "the Underworld" && x >= 0 && y >= 0 && x <= 1581 && y <= 1599 )
			{
				xWidth = 1581; yHeight = 1599; startX = 0; startY = 0;
			}

			if ( startX > -1 )
			{
				xCenter = (int)(startX+(xWidth/2));
				yCenter = (int)(startY+(yHeight/2));
			}
			else
			{
				xCenter = 0; yCenter = 0;
				return false;
			}

			return true;
		}

		public static Point3D ReverseLookup( Map map, int xLong, int yLat, int xMins, int yMins, bool xEast, bool ySouth )
		{
			if ( map == null || map == Map.Internal )
				return Point3D.Zero;

			int xCenter, yCenter;
			int xWidth, yHeight;

			if ( !ComputeMapDetails( map, 0, 0, out xCenter, out yCenter, out xWidth, out yHeight ) )
				return Point3D.Zero;

			double absLong = xLong + ((double)xMins / 60);
			double absLat  = yLat  + ((double)yMins / 60);

			if ( !xEast )
				absLong = 360.0 - absLong;

			if ( !ySouth )
				absLat = 360.0 - absLat;

			int x, y, z;

			x = xCenter + (int)((absLong * xWidth) / 360);
			y = yCenter + (int)((absLat * yHeight) / 360);

			if ( x < 0 )
				x += xWidth;
			else if ( x >= xWidth )
				x -= xWidth;

			if ( y < 0 )
				y += yHeight;
			else if ( y >= yHeight )
				y -= yHeight;

			z = map.GetAverageZ( x, y );

			return new Point3D( x, y, z );
		}

		public static bool Format( Point3D p, Map map, ref int xLong, ref int yLat, ref int xMins, ref int yMins, ref bool xEast, ref bool ySouth )
		{
			if ( map == null || map == Map.Internal )
				return false;

			int x = p.X, y = p.Y;
			int xCenter, yCenter;
			int xWidth, yHeight;

			if ( !ComputeMapDetails( map, x, y, out xCenter, out yCenter, out xWidth, out yHeight ) )
				return false;

			double absLong = (double)((x - xCenter) * 360) / xWidth;
			double absLat  = (double)((y - yCenter) * 360) / yHeight;

			if ( absLong > 180.0 )
				absLong = -180.0 + (absLong % 180.0);

			if ( absLat > 180.0 )
				absLat = -180.0 + (absLat % 180.0);

			bool east = ( absLong >= 0 ), south = ( absLat >= 0 );

			if ( absLong < 0.0 )
				absLong = -absLong;

			if ( absLat < 0.0 )
				absLat = -absLat;

			xLong = (int)absLong;
			yLat  = (int)absLat;

			xMins = (int)((absLong % 1.0) * 60);
			yMins = (int)((absLat  % 1.0) * 60);

			xEast = east;
			ySouth = south;

			return true;
		}
	}
}