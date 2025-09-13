using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Guilds;
using Server.Items;
using Server.Misc;
using Server.Regions;
using Server.Spells;
using Server.Engines.CannedEvil;

namespace Server.Multis
{
	public enum HousePlacementResult
	{
		Valid,
		BadRegion,
		BadLand,
		BadStatic,
		BadItem,
		NoSurface,
		BadRegionHidden,
		BadRegionTemp,
		InvalidCastleKeep,
	}

	public class HousePlacement
	{
		private const int YardSize = 1;

		private static int[] m_RoadIDs = new int[]
			{
				0x0071, 0x0072,
				0x0073, 0x0074,
				0x0075, 0x0076,
				0x0077, 0x0078,
				0x00E8, 0x00EB,
				0x07AE, 0x07B1,
				0x3FF4, 0x3FF4,
				0x3FF8, 0x3FFB,
				0x0442, 0x0479, // Sand stones
				0x0501, 0x0510, // Sand stones
				0x0009, 0x0015, // Furrows
				0x0150, 0x015C  // Furrows
			};

		public static HousePlacementResult Check( Mobile from, int multiID, Point3D center, out ArrayList toMove )
		{
			Region in_reg = Region.Find( from.Location, from.Map );

			// If this spot is considered valid, every item and mobile in this list will be moved under the house sign
			toMove = new ArrayList();

			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return HousePlacementResult.BadLand; // A house cannot go here

			if ( from.AccessLevel >= AccessLevel.GameMaster )
				return HousePlacementResult.Valid; // Staff can place anywhere

			NoHousingRegion noHousingRegion = (NoHousingRegion) Region.Find( center, map ).GetRegion( typeof( NoHousingRegion ) );

			if ( noHousingRegion != null )
				return HousePlacementResult.BadRegion;

			// This holds data describing the internal structure of the house
			MultiComponentList mcl = MultiData.GetComponents( multiID );

			if ( multiID >= 0x13EC && multiID < 0x1D00 )
				HouseFoundation.AddStairsTo( ref mcl ); // this is a AOS house, add the stairs

			// Location of the northwest-most corner of the house
			Point3D start = new Point3D( center.X + mcl.Min.X, center.Y + mcl.Min.Y, center.Z );

			// These are storage lists. They hold items and mobiles found in the map for further processing
			List<Item> items = new List<Item>();
			List<Mobile> mobiles = new List<Mobile>();

			// These are also storage lists. They hold location values indicating the yard and border locations.
			List<Point2D> yard = new List<Point2D>(), borders = new List<Point2D>();

			/* RULES:
			 * 
			 * 1) All tiles which are around the -outside- of the foundation must not have anything impassable.
			 * 2) No impassable object or land tile may come in direct contact with any part of the house.
			 * 3) Five tiles from the front and back of the house must be completely clear of all house tiles.
			 * 4) The foundation must rest flatly on a surface. Any bumps around the foundation are not allowed.
			 * 5) No foundation tile may reside over terrain which is viewed as a road.
			 */

			for ( int x = 0; x < mcl.Width; ++x )
			{
				for ( int y = 0; y < mcl.Height; ++y )
				{
					int tileX = start.X + x;
					int tileY = start.Y + y;

					StaticTile[] addTiles = mcl.Tiles[x][y];

					if ( addTiles.Length == 0 )
						continue; // There are no tiles here, continue checking somewhere else

					Point3D testPoint = new Point3D( tileX, tileY, center.Z );

					Region reg = Region.Find( testPoint, map );

					if ( !reg.AllowHousing( from, testPoint ) ) // Cannot place houses in dungeons, towns, treasure map areas etc
					{
						if ( reg.IsPartOf( typeof( TempNoHousingRegion ) ) || reg.IsPartOf( typeof( ChampionSpawnRegion ) ) || reg is ChampionSpawnRegion )
							return HousePlacementResult.BadRegionTemp;

						return HousePlacementResult.BadRegion;
					}

					LandTile landTile = map.Tiles.GetLandTile( tileX, tileY );
					int landID = landTile.ID & TileData.MaxLandValue;

					StaticTile[] oldTiles = map.Tiles.GetStaticTiles( tileX, tileY, true );

					Sector sector = map.GetSector( tileX, tileY );

					items.Clear();

					for ( int i = 0; i < sector.Items.Count; ++i )
					{
						Item item = sector.Items[i];

						if ( item.Visible && item.X == tileX && item.Y == tileY )
							items.Add( item );
					}

					mobiles.Clear();

					for ( int i = 0; i < sector.Mobiles.Count; ++i )
					{
						Mobile m = sector.Mobiles[i];

						if ( m.X == tileX && m.Y == tileY )
							mobiles.Add( m );
					}

					int landStartZ = 0, landAvgZ = 0, landTopZ = 0;

					map.GetAverageZ( tileX, tileY, ref landStartZ, ref landAvgZ, ref landTopZ );

					bool hasFoundation = false;

					for ( int i = 0; i < addTiles.Length; ++i )
					{
						StaticTile addTile = addTiles[i];

						if ( addTile.ID == 0x1 ) // Nodraw
							continue;

						TileFlag addTileFlags = TileData.ItemTable[addTile.ID & TileData.MaxItemValue].Flags;

						bool isFoundation = ( addTile.Z == 0 && (addTileFlags & TileFlag.Wall) != 0 );

						if ( isFoundation )
							hasFoundation = true;

						int addTileZ = center.Z + addTile.Z;
						int addTileTop = addTileZ + addTile.Height;

						if ( (addTileFlags & TileFlag.Surface) != 0 )
							addTileTop += 16;

						if ( addTileTop > landStartZ && landAvgZ > addTileZ )
							return HousePlacementResult.BadLand; // Broke rule #2

						for ( int j = 0; j < oldTiles.Length; ++j )
						{
							StaticTile oldTile = oldTiles[j];
							ItemData id = TileData.ItemTable[oldTile.ID & TileData.MaxItemValue];

							if ( (id.Impassable || (id.Surface && (id.Flags & TileFlag.Background) == 0)) && addTileTop > oldTile.Z && (oldTile.Z + id.CalcHeight) > addTileZ )
								return HousePlacementResult.BadStatic; // Broke rule #2
						}

						for ( int j = 0; j < items.Count; ++j )
						{
							Item item = items[j];
							ItemData id = item.ItemData;

							if ( addTileTop > item.Z && (item.Z + id.CalcHeight) > addTileZ )
							{
								if ( item.Movable )
									toMove.Add( item );
								else if ( (id.Impassable || (id.Surface && (id.Flags & TileFlag.Background) == 0)) )
								{
									//to allow for new house placement system FINAL
									if (item.ItemID != 3651 && item.ItemID != 6647 && item.ItemID != 6646 && item.ItemID != 4152 &&item.ItemID != 7793 &&item.ItemID != 7801 &&item.ItemID != 6643 && item.ItemID != 6645 && item.ItemID != 6641 && item.ItemID != 7806 && item.ItemID != 6651 && item.ItemID != 6652 && item.ItemID != 6649 && item.ItemID != 6650 && item.ItemID != 7799 && item.ItemID != 7129 && item.ItemID != 7134 && item.ItemID != 6642)
										return HousePlacementResult.BadItem; // Broke rule #2
								}
							}
						}

						for ( int j = 0; j < mobiles.Count; ++j )
						{
							Mobile m = mobiles[j];

							if ( addTileTop > m.Z && (m.Z + 16) > addTileZ )
								toMove.Add( m );
						}
					}

					// RAVENDARK ISLAND CAN BUILD ON DIRT
					// THERE IS DIRT AROUND 2 DESERTS THAT DESERVE HOUSING
					if ( in_reg.IsPartOf( typeof( NecromancerRegion ) ) || in_reg.IsPartOf( typeof( LunaRegion ) ) || in_reg.IsPartOf( typeof( UnderHouseRegion ) ) || 
						( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" ) || 
						( from.Map == Map.Felucca && from.X > 1105 && from.Y > 2685 && from.X < 1950 && from.Y < 3201 ) || 
						( from.Map == Map.Trammel && from.X > 1114 && from.Y > 387 && from.X < 1265 && from.Y < 597 ) ){}
					else {
						for ( int i = 0; i < m_RoadIDs.Length; i += 2 )
						{
							if ( landID >= m_RoadIDs[i] && landID <= m_RoadIDs[i + 1] )
								return HousePlacementResult.BadLand; // Broke rule #5
						}
					}

					if ( hasFoundation )
					{
						for ( int xOffset = -1; xOffset <= 1; ++xOffset )
						{
							for ( int yOffset = -YardSize; yOffset <= YardSize; ++yOffset )
							{
								Point2D yardPoint = new Point2D( tileX + xOffset, tileY + yOffset );

								if ( !yard.Contains( yardPoint ) )
									yard.Add( yardPoint );
							}
						}

						for ( int xOffset = -1; xOffset <= 1; ++xOffset )
						{
							for ( int yOffset = -1; yOffset <= 1; ++yOffset )
							{
								if ( xOffset == 0 && yOffset == 0 )
									continue;

								// To ease this rule, we will not add to the border list if the tile here is under a base floor (z<=8)

								int vx = x + xOffset;
								int vy = y + yOffset;

								if ( vx >= 0 && vx < mcl.Width && vy >= 0 && vy < mcl.Height )
								{
									StaticTile[] breakTiles = mcl.Tiles[vx][vy];
									bool shouldBreak = false;

									for ( int i = 0; !shouldBreak && i < breakTiles.Length; ++i )
									{
										StaticTile breakTile = breakTiles[i];

										if ( breakTile.Height == 0 && breakTile.Z <= 8 && TileData.ItemTable[breakTile.ID & TileData.MaxItemValue].Surface )
											shouldBreak = true;
									}

									if ( shouldBreak )
										continue;
								}

								Point2D borderPoint = new Point2D( tileX + xOffset, tileY + yOffset );

								if ( !borders.Contains( borderPoint ) )
									borders.Add( borderPoint );
							}
						}
					}
				}
			}

			for ( int i = 0; i < borders.Count; ++i )
			{
				Point2D borderPoint = borders[i];

				LandTile landTile = map.Tiles.GetLandTile( borderPoint.X, borderPoint.Y );
				int landID = landTile.ID & TileData.MaxLandValue;

				if ( (TileData.LandTable[landID].Flags & TileFlag.Impassable) != 0 )
					return HousePlacementResult.BadLand;


				// ALUCARD ISLAND CAN BUILD ON DIRT
				// THERE IS DIRT AROUND 2 DESERTS THAT DESERVE HOUSING
				if ( in_reg.IsPartOf( typeof( NecromancerRegion ) ) || in_reg.IsPartOf( typeof( LunaRegion ) ) || in_reg.IsPartOf( typeof( UnderHouseRegion ) ) || 
					( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" ) || 
					(from.Map == Map.Felucca && from.X>1105 && from.Y>2685 && from.X<1950 && from.Y<3201) || 
					(from.Map == Map.Trammel && from.X>1114 && from.Y>387 && from.X<1265 && from.Y<597) ){}
				else {
					for ( int j = 0; j < m_RoadIDs.Length; j += 2 )
					{
						if ( landID >= m_RoadIDs[j] && landID <= m_RoadIDs[j + 1] )
							return HousePlacementResult.BadLand; // Broke rule #5
					}
				}

				StaticTile[] tiles = map.Tiles.GetStaticTiles( borderPoint.X, borderPoint.Y, true );

				for ( int j = 0; j < tiles.Length; ++j )
				{
					StaticTile tile = tiles[j];
					ItemData id = TileData.ItemTable[tile.ID & TileData.MaxItemValue];

					if ( id.Impassable || (id.Surface && (id.Flags & TileFlag.Background) == 0 && (tile.Z + id.CalcHeight) > (center.Z + 2)) )
						return HousePlacementResult.BadStatic; // Broke rule #1
				}

				Sector sector = map.GetSector( borderPoint.X, borderPoint.Y );
				List<Item> sectorItems = sector.Items;

				for ( int j = 0; j < sectorItems.Count; ++j )
				{
					Item item = sectorItems[j];

					if ( item.X != borderPoint.X || item.Y != borderPoint.Y || item.Movable )
						continue;

					ItemData id = item.ItemData;

					if ( id.Impassable || (id.Surface && (id.Flags & TileFlag.Background) == 0 && (item.Z + id.CalcHeight) > (center.Z + 2)) )
						return HousePlacementResult.BadItem; // Broke rule #1
				}
			}

			List<Sector> _sectors = new List<Sector>();
			List<BaseHouse> _houses = new List<BaseHouse>();

			for ( int i = 0; i < yard.Count; i++ ) {
				Sector sector = map.GetSector( yard[i] );
				
				if ( !_sectors.Contains( sector ) ) {
					_sectors.Add( sector );
					
					if ( sector.Multis != null ) {
						for ( int j = 0; j < sector.Multis.Count; j++ ) {
							if ( sector.Multis[j] is BaseHouse ) {
								BaseHouse _house = (BaseHouse)sector.Multis[j];
								if ( !_houses.Contains( _house ) ) {
									_houses.Add( _house );
								}
							}
						}
					}
				}
			}

			for ( int i = 0; i < yard.Count; ++i )
			{
				foreach ( BaseHouse b in _houses ) {
					if ( b.Contains( yard[i] ) )
						return HousePlacementResult.BadStatic; // Broke rule #3
				}

				/*Point2D yardPoint = yard[i];

				IPooledEnumerable eable = map.GetMultiTilesAt( yardPoint.X, yardPoint.Y );

				foreach ( StaticTile[] tile in eable )
				{
					for ( int j = 0; j < tile.Length; ++j )
					{
						if ( (TileData.ItemTable[tile[j].ID & TileData.MaxItemValue].Flags & (TileFlag.Impassable | TileFlag.Surface)) != 0 )
						{
							eable.Free();
							return HousePlacementResult.BadStatic; // Broke rule #3
						}
					}
				}

				eable.Free();*/
			}

			return HousePlacementResult.Valid;
		}
	}
}