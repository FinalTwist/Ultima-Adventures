using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Multis;
using Server.Targeting;


namespace Server.Engines.Apiculture
{
	public enum HiveHealth
	{
		Dying,
		Sickly,
		Healthy,
		Thriving
	}

	public enum HiveStatus
	{
		Empty   		= 0,
		Colonizing		= 1,
		Brooding    	= 3,
		Producing		= 5,

		Stage1			= 1,
		Stage2			= 2,
		Stage3			= 3,
		Stage4			= 4,
		Stage5			= 5
	}

	public enum ResourceStatus
	{
		None		= 0,  //red X
		VeryLow		= 1,  //red -
		Low			= 2,  //yellow -
		Normal		= 3,  //nothing
		High		= 4,  //green +
		VeryHigh	= 5,  //yellow +
		TooHigh		= 6   //red +
	}

	public enum HiveGrowthIndicator
	{
		None = 0,
		LowResources,
		NotHealthy,
		Grown,	
		PopulationUp,
		PopulationDown
	}

	

	public class BeeHiveHelper
	{
		public static readonly TimeSpan CheckDelay = TimeSpan.FromHours( 24.0 );
		//public static readonly TimeSpan CheckDelay = TimeSpan.FromSeconds( 1.0 );  //for testing

		public static void Configure()
		{
			EventSink.WorldSave += new WorldSaveEventHandler( EventSink_WorldSave );
		}

		private static void EventSink_WorldSave( WorldSaveEventArgs args)
		{
			HiveUpdateAll();
		}

		public static void HiveUpdateAll()
		{
			//loop through all the hives in the world and update them
			foreach ( Item item in World.Items.Values )
			{
				if( item is apiBeeHive )
				{
					HiveUpdate( (apiBeeHive)item );
				}
			}
		}

		public static void HiveUpdate(apiBeeHive hive)
		{
			if ( !hive.IsCheckable )
				return;

			//make sure it is time for update
			if ( DateTime.UtcNow < hive.NextCheck )
			{
				//m_GrowthIndicator = PlantGrowthIndicator.Delay;
				return;
			}

			hive.NextCheck = DateTime.UtcNow + CheckDelay; //update check timer
			hive.LastGrowth = HiveGrowthIndicator.None; //reset growth indicator
			
			hive.HiveAge++;	//update age of the hive
			hive.FindFlowersInRange(); //update flowers
			hive.FindWaterInRange();   //update water

			//apply any potions
			hive.ApplyBenefitEffects();
			
			//apply negative effects
			if( !hive.ApplyMaladiesEffects() )  //Dead
				return;

            //update stage
			hive.Grow();
			
			//update maladies
			hive.UpdateMaladies();

			hive.BeeHiveComponent.InvalidateProperties(); //to refresh beehive properties
		}

		public static int[] m_HeatSources = new int[]
		{
				0x461, 0x48E, // Sandstone oven/fireplace
				0x92B, 0x96C, // Stone oven/fireplace
				0xDE3, 0xDE9, // Campfire
				0xFAC, 0xFAC, // Firepit
				0x184A, 0x184C, // Heating stand (left)
				0x184E, 0x1850, // Heating stand (right)
				0x398C, 0x399F  // Fire field
		};

		public static bool Find( Mobile from, int[] itemIDs )
		{
			Map map = from.Map;

			if ( map == null )
				return false;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, 2 );

			foreach ( Item item in eable )
			{
				if ( (item.Z + 16) > from.Z && (from.Z + 16) > item.Z && Find( item.ItemID, itemIDs ) )
				{
					eable.Free();
					return true;
				}
			}

			eable.Free();

			for ( int x = -2; x <= 2; ++x )
			{
				for ( int y = -2; y <= 2; ++y )
				{
					int vx = from.X + x;
					int vy = from.Y + y;
					
					StaticTile[] tiles = map.Tiles.GetStaticTiles( vx, vy, true );
					
					for ( int i = 0; i < tiles.Length; ++i )
					{
						int z = tiles[i].Z;
						int id = tiles[i].ID & 0x3FFF;

						if ( (z + 16) > from.Z && (from.Z + 16) > z && Find( id, itemIDs ) )
							return true;
					}
				}
			}

			return false;
		}

		public static bool Find( int itemID, int[] itemIDs )
		{
			bool contains = false;

			for ( int i = 0; !contains && i < itemIDs.Length; i += 2 )
				contains = ( itemID >= itemIDs[i] && itemID <= itemIDs[i + 1] );

			return contains;
		}
	}
}
