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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Items;

namespace UltimaLive
{
  public class UltimaLiveSettings
  {
    public const string UNIQUE_SHARD_IDENTIFIER = "Ultima Adventures"; //Must be 28 characters or less

    public const string ULTIMA_LIVE_ROOT_FOLDER_NAME = "UltimaLive";
    public const string ULTIMA_LIVE_MAP_CHANGES_FOLDER_NAME = "ClientFiles";
    public const string ULTIMA_LIVE_LUMBER_HARVEST_FOLDER_NAME = "LumberHarvest";
    public const string ULTIMA_LIVE_CLIENT_EXPORT_FOLDER_NAME = "ClientExport";
    public static string UltimaLiveClientExportPath
    {
      get
      {
        return Path.Combine(UltimaLiveRootPath, ULTIMA_LIVE_CLIENT_EXPORT_FOLDER_NAME);
      }
    }

    public static string UltimaLiveMapChangesSavePath
    {
      get
      {
        return Path.Combine(UltimaLiveRootPath, ULTIMA_LIVE_MAP_CHANGES_FOLDER_NAME);
      }
    }

    public static string UltimaLiveRootPath
    {
      get
      {
        return Path.Combine(Core.BaseDirectory, ULTIMA_LIVE_ROOT_FOLDER_NAME);
      }
    }

    public static string LumberHarvestFallenTreeSaveLocation
    {
      get
      {
        return Path.Combine(UltimaLiveRootPath, ULTIMA_LIVE_LUMBER_HARVEST_FOLDER_NAME);
      }
    }
  }
}