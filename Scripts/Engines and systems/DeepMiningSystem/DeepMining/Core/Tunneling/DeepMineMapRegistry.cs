/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 26/09/2014
 */

/// <summary>
/// Register here all Black Maps to be parsed into DeepMines
/// </summary>

using System;
using Server;

namespace Server.DeepMine
{
	public static class DeepMineMapRegistry
	{
		public static MapRegister[] MapEntries = new MapRegister[]
		{
			new MapRegister(36,8,6)//Map.MapID, number of mines, number of levels
		};
		
		public static void Register()
		{
			foreach(MapRegister entry in MapEntries)
			{
				DeepMineParsing.ParseMap(entry);
			}
		}
	}
	
	public class MapRegister
	{
		public int MapID;
		public int Mines;
		public int Levels;
		
		public MapRegister(int mapid, int mines, int levels)
		{
			MapID = mapid;
			Mines = mines;
			Levels = levels;
		}
	}
}