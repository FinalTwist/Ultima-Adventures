/* Copyright(c) 2016 UltimaLive
 * 
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

using System;
using System.IO;
using Server;

namespace UltimaLive
{
    class BlockUtility
    {
        public static byte[] GetLandData(Int32 blockNumber, Int32 mapNumber)
        {
            Map m = Map.Maps[mapNumber];
            TileMatrix tm = m.Tiles;
            return GetLandData( new Point2D(blockNumber / tm.BlockHeight, blockNumber % tm.BlockHeight), mapNumber);
        }

        public static byte[] GetLandData(Point2D blockCoordinates, Int32 mapNumber)
        {
            byte[] landData = new byte[192];

            Map map = Map.Maps[mapNumber];
            TileMatrix tm = map.Tiles;
            LandTile[] land = tm.GetLandBlock(blockCoordinates.X, blockCoordinates.Y);
            for(int i = 0; i < land.Length; i++) //64 * 3 = 192 bytes
            {
                landData[(i * 3)] = (byte)(((ushort)land[i].ID) & 0x00FF);
                landData[(i * 3) + 1] = (byte)((((ushort)land[i].ID) & 0xFF00) >> 8);
                landData[(i * 3) + 2] = (byte)land[i].Z;
            }
            return landData;
        }

        public static void WriteLandDataToConsole(byte[] landData)
        {
            string outString = "Land Data:\n";
            for (int i = 0; i < landData.Length; i += 3)
            {
                if (i % 12 == 0 && i != 0)
                {
                    outString += "\n";
                }
                outString += string.Format("[{0},{1},{2}]", landData[i].ToString("X2"), landData[i + 1].ToString("X2"), landData[i + 2].ToString("X2"));
            }
            Console.WriteLine(outString);
        }

        public static void WriteStaticsDataToConsole(byte[] staticsData)
        {
            string outString = string.Format("Statics Data ({0}):\n", staticsData.Length);
            for (int i = 0; i < staticsData.Length; i += 7)
            {
                if (i % 14 == 0 && i != 0)
                {
                    outString += "\n";
                }
                outString += string.Format("[{0},{1},{2},{3},{4},{5},{6}]", staticsData[i].ToString("X2"), staticsData[i + 1].ToString("X2"), staticsData[i + 2].ToString("X2"),
                  staticsData[i + 3].ToString("X2"), staticsData[i + 4].ToString("X2"), staticsData[i + 5].ToString("X2"), staticsData[i + 6].ToString("X2"));
            }
            Console.WriteLine(outString);
        }

        public static void WriteDataToConsole(byte[] anyData)
        {
            string outString = string.Format("Data ({0}):\n", anyData.Length);
            for (int i = 0; i < anyData.Length; i += 8)
            {
                if (i + 7 < anyData.Length)
                {
                    if (i % 16 == 0 && i != 0)
                    {
                        outString += "\n";
                    }
                    outString += string.Format("[{0},{1},{2},{3},{4},{5},{6},{7}]", anyData[i].ToString("X2"), anyData[i + 1].ToString("X2"),
                      anyData[i + 2].ToString("X2"), anyData[i + 3].ToString("X2"), anyData[i + 4].ToString("X2"),
                      anyData[i + 5].ToString("X2"), anyData[i + 6].ToString("X2"), anyData[i + 7].ToString("X2"));
                }
                else
                {
                    outString += "\n[";
                    for (int j = i; j < anyData.Length; j++)
                    {
                        outString += anyData[j].ToString("X2") + ",";
                    }
                    outString += "]";
                }
            }

            Console.WriteLine(outString);
        }

        public static void WriteBlockDataToConsole(byte[] anyData)
        {
            Console.WriteLine(string.Format("Block Data ({0}):\n", anyData.Length));
            if (anyData.Length >= 192)
            {
                byte[] landData = new byte[192];
                Array.Copy(anyData, 0, landData, 0, 192);
                //WriteLandDataToConsole(landData);
                if ((anyData.Length - 192) % 7 == 0)
                {
                    byte[] staticsData = new byte[anyData.Length - 192];
                    Array.Copy(anyData, 192, staticsData, 0, anyData.Length - 192);
                    WriteStaticsDataToConsole(staticsData);
                }
            }
        }

        public static byte[] GetRawStaticsData(Int32 blockNumber, Int32 mapNumber)
        {
            Map m = Map.Maps[mapNumber];
            TileMatrix tm = m.Tiles;
            return GetRawStaticsData(new Point2D(blockNumber / tm.BlockHeight, blockNumber % tm.BlockHeight), mapNumber);
        }

        public static byte[] GetRawStaticsData(Point2D blockCoordinates, Int32 mapNumber)
        {
            Map map = Map.Maps[mapNumber];
            TileMatrix tm = map.Tiles;
            StaticTile[][][] staticTiles = tm.GetStaticBlock(blockCoordinates.X, blockCoordinates.Y);

            int staticCount = 0;
            for (int k = 0; k < staticTiles.Length; k++)
            {
                for (int l = 0; l < staticTiles[k].Length; l++)
                {
                    staticCount += staticTiles[k][l].Length;
                }
            }

            byte[] blockBytes = new byte[staticCount * 7];
            int blockByteIdx = 0;
            for (int i = 0; i < staticTiles.Length; i++)
            {
                for (int j = 0; j < staticTiles[i].Length; j++)
                {
                    StaticTile[] sortedTiles = staticTiles[i][j];
                    //Array.Sort(sortedTiles, CompareStaticTiles);

                    for (int k = 0; k < sortedTiles.Length; k++)
                    {
                        blockBytes[blockByteIdx + 0] = (byte)(((ushort)sortedTiles[k].ID) & 0x00FF);
                        blockBytes[blockByteIdx + 1] = (byte)((((ushort)sortedTiles[k].ID) & 0xFF00) >> 8);
                        blockBytes[blockByteIdx + 2] = (byte)i;
                        blockBytes[blockByteIdx + 3] = (byte)j;
                        blockBytes[blockByteIdx + 4] = (byte)sortedTiles[k].Z;
                        blockBytes[blockByteIdx + 5] = (byte)(((ushort)sortedTiles[k].Hue) & 0x00FF);
                        blockBytes[blockByteIdx + 6] = (byte)((((ushort)sortedTiles[k].Hue) & 0xFF00) >> 8);
                        blockByteIdx += 7;
                    }
                }
            }
            return blockBytes;
        }

    public static int CompareStaticTiles(StaticTile b, StaticTile a)
		{
			int retVal = a.Z.CompareTo(b.Z);
      if (retVal == 0)//same Z, lower z has higher priority now, it's correct this way, tested locally
			{
				StaticTile[] sts = ClientFileExport.WorkMap.Tiles.GetStaticTiles(a.X, a.Y);
				for(int i=0; i<sts.Length; i++)
        {//we compare hashcodes for easyness, instead of comparing a bunch of properties, order has been verified to work in exportclientfiles.
					int hash=sts[i].GetHashCode();
					if(hash==a.GetHashCode())
					{
            retVal = 1;
						break;
					}
					else if(hash==b.GetHashCode())
					{
            retVal = -1;
						break;
					}
				}
			}
      //We leave this as is, but it shouldn't happen anyway if we have same Z
			if(retVal==0)
      {
				retVal = a.ID.CompareTo(b.ID);
      }

			if(retVal==0)
      {
				retVal = a.Hue.CompareTo(b.Hue);
      }

			return retVal;
		}
	}
}