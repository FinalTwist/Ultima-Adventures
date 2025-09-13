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
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Server;
using Server.Accounting;
using Server.Gumps;
using Server.Targeting;
using Server.Items;
using Server.Menus;
using Server.Mobiles;
using Server.Movement;
using Server.Prompts;
using Server.HuePickers;
using Server.ContextMenus;
using Server.Diagnostics;
using CV = Server.ClientVersion;
using Server.Network;


namespace UltimaLive
{
  public class UltimaLivePacketHandlers
  {

    public static void Configure()
    {
      EventSink.Disconnected += new DisconnectedEventHandler(EventSink_Disconnected);
    }

    static void EventSink_Disconnected(DisconnectedEventArgs e)
    {
      if (e.Mobile != null && e.Mobile is PlayerMobile)
      {
        PlayerMobile player = (PlayerMobile)e.Mobile;
        Console.WriteLine("Reseting UltimaLive Major and Minor version for " + player.Name);
        player.UltimaLiveMajorVersion = 0;
        player.UltimaLiveMinorVersion = 0;
      }
    }

    public static void Initialize()
    {
      PacketHandlers.Register(0x3F, 0, true, new OnPacketReceive(UltimaLivePacketHandlers.ReceiveUltimaLiveCommand));
    }

    public static void ReceiveUltimaLiveCommand(NetState state, PacketReader pvSrc)
    {
      pvSrc.Seek(13, SeekOrigin.Begin);
      byte ultimaLiveCommand = pvSrc.ReadByte();

      switch (ultimaLiveCommand)
      {
        case 0xFF: //block query response
          {
            HandleBlockQueryReply(state, pvSrc);
          }
          break;

        case 0xFE: //read client version of UltimaLive
          {
            pvSrc.Seek(15, SeekOrigin.Begin);
            UInt16 majorVersion = pvSrc.ReadUInt16();
            UInt16 minorVersion = pvSrc.ReadUInt16();
            Console.WriteLine(String.Format("Received UltimaLive version packet: {0}.{1} from {2}", majorVersion, minorVersion, state.Mobile.Name));
            if (state != null && state.Mobile is PlayerMobile)
            {
              PlayerMobile player = (PlayerMobile)state.Mobile;
              player.UltimaLiveMajorVersion = majorVersion;
              player.UltimaLiveMinorVersion = minorVersion;
            }
          }
          break;

        // Need to write in functionality for direct server edit commands
        // from players with accesslevel that is high enough.
        // This will be for future enhancements on the client end (Client Overlay Editor,
        // Pandora's Box Plugin, etc)
        default:
          {
          }
          break;
      }
    }

    /*
     * Whenever the client moves out of a block, we will ask the client to 
     * provide us with a list of blocks around it and their corresponding
     * block versions.  If any of them are different than the server's 
     * block versions, we'll know the client needs to be updated, and 
     * we'll send the appropriate blocks.
    /**/
    public static void HandleBlockQueryReply(NetState state, PacketReader pvSrc)
    {
      Mobile from = state.Mobile;
      //byte 000              -  cmd
      //byte 001 through 002  -  packet size
      pvSrc.Seek(3, SeekOrigin.Begin);            //byte 003 through 006  -  central block number for the query (block that player is standing in)
      UInt32 blocknum = pvSrc.ReadUInt32();
      UInt32 count = pvSrc.ReadUInt32();          //byte 007 through 010  -  number of statics in the packet (8 for a query response)
      //byte 011 through 012  -  UltimaLive sequence number - we sent one out, did we get it back?
      //byte 013              -  UltimaLive command (0xFF is a block Query Response)
      pvSrc.Seek(14, SeekOrigin.Begin);           //byte 014              -  UltimaLive mapnumber
      Int32 mapID = (Int32)pvSrc.ReadByte();

      if (mapID != from.Map.MapID)
      {
        Console.WriteLine(string.Format("Received a block query response from {0} for map {1} but that player is on map {2}",
            from.Name, mapID, from.Map.MapID));
        return;
      }

      UInt16[] receivedCRCs = new UInt16[25];     //byte 015 through 64   -  25 block CRCs
      for (int i = 0; i < 25; i++)
      {
        receivedCRCs[i] = pvSrc.ReadUInt16();
      }

      //TODO: see if sequence numbers are valid

      PushBlockUpdates((int)blocknum, (int)mapID, receivedCRCs, from);
    }

    public static UInt16 GetBlockCrc(Point2D blockCoords, int mapID, ref byte[] landDataOut, ref byte[] staticsDataOut)
    {
      if (blockCoords.X < 0 || blockCoords.Y < 0 || (blockCoords.X) >= Map.Maps[mapID].Tiles.BlockWidth || (blockCoords.Y) >= Map.Maps[mapID].Tiles.BlockHeight)
      {
        return (UInt16)0;
      }

      landDataOut = BlockUtility.GetLandData(blockCoords, mapID);


      staticsDataOut = BlockUtility.GetRawStaticsData(blockCoords, mapID);
      byte[] blockData = new byte[landDataOut.Length + staticsDataOut.Length];
      Array.Copy(landDataOut, 0, blockData, 0, landDataOut.Length);
      Array.Copy(staticsDataOut, 0, blockData, landDataOut.Length, staticsDataOut.Length);
      return CRC.Fletcher16(blockData);
    }

    public static void PushBlockUpdates(int block, int mapID, UInt16[] recievedCRCs, Mobile from)
    {
      //Console.WriteLine("------------------------------------------Push Block Updates----------------------------------------");
      //Console.WriteLine("Map: " + mapID);
      //Console.WriteLine("Block: " + block);
      //Console.WriteLine("----------------------------------------------------------------------------------------------------");

      if (!MapRegistry.Definitions.ContainsKey(mapID))
      {
        Console.WriteLine("Received query for an invalid map.");
        return;
      }

      ushort[] Hashes = new ushort[25];
      TileMatrix tm = Map.Maps[mapID].Tiles;

      int blockX = ((block / tm.BlockHeight));
      int blockY = ((block % tm.BlockHeight));
      Int32 wrapWidthInBlocks = MapRegistry.Definitions[mapID].WrapAroundDimensions.X >> 3;
      Int32 wrapHeightInBlocks = MapRegistry.Definitions[mapID].WrapAroundDimensions.Y >> 3;
      Int32 mapWidthInBlocks = MapRegistry.Definitions[mapID].Dimensions.X >> 3;
      Int32 mapHeightInBlocks = MapRegistry.Definitions[mapID].Dimensions.Y >> 3;
      //Console.WriteLine("BlockX: " + blockX + " BlockY: " + blockY);
      //Console.WriteLine("Map Height in blocks: " + mapHeightInBlocks);
      //Console.WriteLine("Map Width in blocks: " + mapWidthInBlocks);
      //Console.WriteLine("Wrap Height in blocks: " + wrapHeightInBlocks);
      //Console.WriteLine("Wrap Width in blocks: " + wrapWidthInBlocks);

      if (block < 0 || block >= mapWidthInBlocks * mapHeightInBlocks)
      {
        return;
      }

      byte[] buf = new byte[2];

      for (int x = -2; x <= 2; x++)
      {
        int xBlockItr = 0;
        if (blockX < wrapWidthInBlocks)
        {
          xBlockItr = (blockX + x) % wrapWidthInBlocks;
          if (xBlockItr < 0)
          {
            xBlockItr += wrapWidthInBlocks;
          }
        }
        else
        {
          xBlockItr = (blockX + x) % mapWidthInBlocks;
          if (xBlockItr < 0)
          {
            xBlockItr += mapWidthInBlocks;
          }
        }
  
        for (int y = -2; y <= 2; y++)
        {
          int yBlockItr = 0;
          if (blockY < wrapHeightInBlocks)
          {
            yBlockItr = (blockY + y) % wrapHeightInBlocks;
            if (yBlockItr < 0)
            {
              yBlockItr += wrapHeightInBlocks;
            }
          }
          else
          {
            yBlockItr = (blockY + y) % mapHeightInBlocks;
            if (yBlockItr < 0)
            {
              yBlockItr += mapHeightInBlocks;
            }
          }

          Int32 blocknum = (xBlockItr * mapHeightInBlocks) + yBlockItr;

          //CRC caching
          UInt16 crc = CRC.MapCRCs[mapID][blocknum];

          byte[] landData = new byte[0];
          byte[] staticsData = new byte[0];
          Point2D blockPosition = new Point2D(xBlockItr, yBlockItr);
          if (crc == UInt16.MaxValue)
          {
            crc = GetBlockCrc(blockPosition, mapID, ref landData, ref staticsData);
            CRC.MapCRCs[mapID][blocknum] = crc;
          }

          //Console.WriteLine(crc.ToString("X4") + " vs " + recievedCRCs[((x + 2) * 5) + y + 2].ToString("X4"));
          //Console.WriteLine(String.Format("({0},{1})", blockPosition.X, blockPosition.Y));
          if (crc != recievedCRCs[((x + 2) * 5) + (y + 2)])
          {
            if (landData.Length < 1)
            {
              from.Send(new UltimaLive.Network.UpdateTerrainPacket(blockPosition, from));
              from.Send(new UltimaLive.Network.UpdateStaticsPacket(blockPosition, from));
            }
            else
            {
              from.Send(new UltimaLive.Network.UpdateTerrainPacket(landData, blocknum, from.Map.MapID));
              from.Send(new UltimaLive.Network.UpdateStaticsPacket(staticsData, blocknum, from.Map.MapID));
            }
          }
        }
      }

      //if (refreshClientView)
      //{
      //  from.Send(new RefreshClientView());
      //}
    }
  }
}