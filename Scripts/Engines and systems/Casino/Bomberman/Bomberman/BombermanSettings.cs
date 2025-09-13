using System;
using Server;

namespace Solaris.BoardGames
{
	public enum BombermanStyle
	{
		Default = 0,
		Rocky = 1,
		Woodland = 2,
		Warehouse = 3,
		Ruins,
		Graveyard,
		Crystal,
	}
	
	//all the default settings for the bomberman game, in a convenient place
	public class BombermanSettings
	{
		//min, max, and default bomberman playing field settings
		public const int MAX_BOARD_SIZE = 65;
		public const int MIN_BOARD_SIZE = 11;
		public const int DEFAULT_BOARD_SIZE = 21;

		//chance that a destructable wall will be generated when the gameboard is being built
		public const double OBSTACLE_CHANCE = 0.7;
		
		
		//delay before explosion, in seconds
		public const int EXPLODE_DELAY = 3;

		//propagation delay of bomb blast, in milliseconds
		public const int BLAST_DELAY = 50;

		//chance that a demolished wall will spawn an upgrade
		public const double UPGRADE_SPAWN_CHANCE = .2;
		
		
		public const int KILL_SCORE = 1;
		public const int DEATH_SCORE = -1;
		
		public const int WIN_SCORE = 2;
		public const int SUICIDE_SCORE = -2;

		
		//time until an upgrade will disappear, in seconds
		public const int UPGRADE_DECAY_DELAY = 20;		
		

		public static int GetDestructableWallID( BombermanStyle style )
		{
			switch( style )
			{
				default:
				case BombermanStyle.Default:
				{
					return 1900;
				}
				case BombermanStyle.Rocky:
				{
					return Utility.RandomMinMax( 0x1363, 0x136D );
				}
				case BombermanStyle.Woodland:
				{
					return Utility.RandomMinMax( 0xCC8, 0xCC9 );
				}
				case BombermanStyle.Warehouse:
				{
					return Utility.RandomMinMax( 0xE3C, 0xE3F );
					
				}
				case BombermanStyle.Ruins:
				{
					return Utility.RandomMinMax( 0x3B7, 0x3BD );
				}
				case BombermanStyle.Graveyard:
				{
					return Utility.RandomMinMax( 0x1AD8, 0x1ADC );
				}
				case BombermanStyle.Crystal:
				{
					return Utility.RandomMinMax( 0x2224, 0x222C );
				}
			}
		}
		
		public static int GetIndestructableWallID( BombermanStyle style )
		{
			switch( style )
			{
				default:
				case BombermanStyle.Default:
				{
					return 1801;
				}
				case BombermanStyle.Rocky:
				{
					return 0x177A;
				}
				case BombermanStyle.Woodland:
				{
					return Utility.RandomList( new int[]{ 0xE57, 0xE59 } );
				}
				case BombermanStyle.Warehouse:
				{
					return Utility.RandomList( new int[]{ 0x720, 0x721 } );
				}
				case BombermanStyle.Ruins:
				{
					return Utility.RandomMinMax( 0x3BE, 0x3C1 );
				}
				case BombermanStyle.Graveyard:
				{
					return Utility.RandomMinMax( 0x1165, 0x1184 );
				}
				case BombermanStyle.Crystal:
				{
					return Utility.RandomList( new int[]{ 0x35EB, 0x35EC, 0x35EF, 0x35F6, 0x35F7 } );
				}
			}
		}
		
		public static int GetFloorTileID( BombermanStyle style )
		{
			switch( style )
			{
				default:
				case BombermanStyle.Default:
				{
					return 0x496;
				}
				case BombermanStyle.Rocky:
				{
					return Utility.RandomMinMax( 0x53B, 0x53F );
				}
				case BombermanStyle.Woodland:
				{
					return Utility.RandomMinMax( 0x177D, 0x1781 );
				}
				case BombermanStyle.Warehouse:
				{
					return Utility.RandomMinMax( 0x4A9, 0x4AC );
				}
				case BombermanStyle.Ruins:
				{
					return Utility.RandomMinMax( 0x525, 0x528 );
				}
				case BombermanStyle.Graveyard:
				{
					return Utility.RandomMinMax( 0x515, 0x518 );
				}
				case BombermanStyle.Crystal:
				{
					return Utility.RandomMinMax( 0x579, 0x57E );
					
				}
			}
			
		}
		
	
	}
}