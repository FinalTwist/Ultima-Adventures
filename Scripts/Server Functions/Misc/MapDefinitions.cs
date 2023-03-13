using System;
using Server;

namespace Server.Misc
{
	public class MapDefinitions
	{
		public static void Configure()
		{
			/* Here we configure all maps. Some notes:
			 * 
			 * 1) The first 32 maps are reserved for core use.
			 * 2) Map 0x7F is reserved for core use.
			 * 3) Map 0xFF is reserved for core use.
			 * 4) Changing or removing any predefined maps may cause server instability.
			 */

			RegisterMap( 0, 0, 0, 7168, 4096, 1, "Felucca",		MapRules.FeluccaRules | MapRules.FreeMovement );//Thanimur
			RegisterMap( 1, 1, 1, 7168, 4096, 1, "Trammel",		MapRules.FeluccaRules | MapRules.FreeMovement );//Sosaria
			RegisterMap( 2, 2, 2, 2304, 1600, 1, "Ilshenar",	MapRules.FeluccaRules | MapRules.FreeMovement );
			RegisterMap( 3, 3, 3, 2560, 2048, 1, "Malas",		MapRules.FeluccaRules | MapRules.FreeMovement );//Serpent Isle
			RegisterMap( 4, 4, 4, 1448, 1448, 1, "Tokuno",		MapRules.FeluccaRules | MapRules.FreeMovement );//Isles of Dread
			RegisterMap( 5, 5, 5, 1280, 4096, 1, "TerMur",		MapRules.FeluccaRules | MapRules.FreeMovement );//Savage Empire

			//Custom Map Entries
			RegisterMap(33, 33, 33, 7168, 4096, 1, "Gaia", MapRules.FeluccaRules | MapRules.FreeMovement); //Grey World
			RegisterMap(34, 34, 34, 7168, 4096, 1, "Midland", MapRules.FeluccaRules | MapRules.FreeMovement); //the Midlands
			RegisterMap(35, 35, 35, 7168, 4096, 1, "Aszuna", MapRules.FeluccaRules | MapRules.FreeMovement); //Aszuna (Map 11)

			RegisterMap( 36, 36, 36, 7168, 4096, 1, "Underground", MapRules.FeluccaRules );

			RegisterMap( 0x7F, 0x7F, 0x7F, Map.SectorSize, Map.SectorSize, 1, "Internal", MapRules.Internal );

			/* Example of registering a custom map:
			 * RegisterMap( 32, 0, 0, 6144, 4096, 3, "Iceland", MapRules.FeluccaRules );
			 * 
			 * Defined:
			 * RegisterMap( <index>, <mapID>, <fileIndex>, <width>, <height>, <season>, <name>, <rules> );
			 *  - <index> : An unreserved unique index for this map
			 *  - <mapID> : An identification number used in client communications. For any visible maps, this value must be from 0-3
			 *  - <fileIndex> : A file identification number. For any visible maps, this value must be 0, 2, 3, or 4
			 *  - <width>, <height> : Size of the map (in tiles)
			 *  - <season> : 0,1 = Spring,Summer   2 = Fall   3 = Winter   4 = Dead
			 *  - <name> : Reference name for the map, used in props gump, get/set commands, region loading, etc
			 *  - <rules> : Rules and restrictions associated with the map. See documentation for details
			*/

			TileMatrixPatch.Enabled = false; // OSI Client Patch 6.0.0.0

			MultiComponentList.PostHSFormat = true; // OSI Client Patch 7.0.9.0
		}

		public static void RegisterMap( int mapIndex, int mapID, int fileIndex, int width, int height, int season, string name, MapRules rules )
		{
			Map newMap = new Map( mapID, mapIndex, fileIndex, width, height, season, name, rules );

			Map.Maps[mapIndex] = newMap;
			Map.AllMaps.Add( newMap );
		}
	}
}