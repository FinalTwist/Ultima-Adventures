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
using System.Collections.Generic;
using System.Text;
using Server;

namespace UltimaLive
{
    public class CRC
    {
        //CRC caching
        //[map][block]
        public static UInt16[][] MapCRCs; 

        public static void InvalidateBlockCRC(int map, int block)
        {
            MapCRCs[map][block] = UInt16.MaxValue;
        }

        public static void Configure()
        {
            EventSink.WorldLoad += new WorldLoadEventHandler(OnLoad);
        }

        public static void OnLoad()
        {
            MapCRCs = new UInt16[256][];

            //We need CRCs for every block in every map.  
            foreach (KeyValuePair<int, MapRegistry.MapDefinition> kvp in MapRegistry.Definitions)
            {
                int blocks = Server.Map.Maps[kvp.Key].Tiles.BlockWidth * Server.Map.Maps[kvp.Key].Tiles.BlockHeight;
                MapCRCs[kvp.Key] = new UInt16[blocks];

                for (int j = 0; j < blocks; j++)
                {
                    MapCRCs[kvp.Key][j] = UInt16.MaxValue;
                }

            }
        }

        /* Thank you http://en.wikipedia.org/wiki/Fletcher%27s_checksum
         * Each sum is computed modulo 255 and thus remains less than 
         * 0xFF at all times. This implementation will thus never 
         * produce the checksum results 0x00FF, 0xFF00 or 0xFFFF.
        /**/
        public static UInt16 Fletcher16( byte[] data)
        {
           UInt16 sum1 = 0;
           UInt16 sum2 = 0;
           int index;
           for( index = 0; index < data.Length; ++index )
           {
              sum1 = (UInt16) ((sum1 + data[index]) % 255);
              sum2 = (UInt16) ((sum2 + sum1) % 255);
           }

           return (UInt16)((sum2 << 8) | sum1);
        }
    }
}
