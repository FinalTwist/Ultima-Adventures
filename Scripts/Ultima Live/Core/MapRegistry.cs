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
using System.Net;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace UltimaLive
{
  public class MapRegistry
  {
    public struct MapDefinition
    {
      public int FileIndex;
      public Point2D Dimensions;
      public Point2D WrapAroundDimensions;
      public MapDefinition(int index, Point2D dimension, Point2D wraparound)
      {
        FileIndex = index;
        Dimensions = dimension;
        WrapAroundDimensions = wraparound;
      }
    }

    private static Dictionary<int, MapDefinition> m_Definitions = new Dictionary<int, MapDefinition>();
    public static Dictionary<int, MapDefinition> Definitions
    {
      get { return m_Definitions; }
    }
    private static Dictionary<int, List<int>> m_MapAssociations = new Dictionary<int, List<int>>();
    public static Dictionary<int, List<int>> MapAssociations
    {
      get { return m_MapAssociations; }
    }


    public static void AddMapDefinition(int index, int associated, Point2D dimensions, Point2D wrapDimensions)
    {
      if (!m_Definitions.ContainsKey(index))
      {
        m_Definitions.Add(index, new MapDefinition(associated, dimensions, wrapDimensions));
        if (m_MapAssociations.ContainsKey(associated))
        {
          m_MapAssociations[associated].Add(index);
        }
        else
        {
          m_MapAssociations[associated] = new List<int>();
          m_MapAssociations[associated].Add(index);
        }
      }
    }

    public static void Configure()
    {
      AddMapDefinition(0, 0, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
      AddMapDefinition(1, 1, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
      AddMapDefinition(2, 2, new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
      AddMapDefinition(3, 3, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
      AddMapDefinition(4, 4, new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
      AddMapDefinition(5, 5, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur

            //those are sample maps that use same original map...
            //AddMapDefinition(32, 0, new Point2D(7168, 4096), new Point2D(5120, 4096));
            //AddMapDefinition(33, 0, new Point2D(7168, 4096), new Point2D(5120, 4096));
            //AddMapDefinition(34, 1, new Point2D(7168, 4096), new Point2D(5120, 4096));

       

            AddMapDefinition(33, 33,  new Point2D(7168, 4096), new Point2D(7168, 4096)); //Grey World
            AddMapDefinition(34, 34,  new Point2D(7168, 4096), new Point2D(7168, 4096)); //The Night Expanse
            AddMapDefinition(35, 35,  new Point2D(7168, 4096), new Point2D(7168, 4096)); //Aszuna

            AddMapDefinition(36, 36, new Point2D(7168, 4096), new Point2D(5120, 4096)); // mining map
            


            EventSink.ServerList += new ServerListEventHandler(EventSink_OnServerList);
      EventSink.Login += new LoginEventHandler(EventSink_Login);
    }

    private static void EventSink_OnServerList(ServerListEventArgs args)
    {
      args.State.Send(new UltimaLive.Network.LoginComplete());
      args.State.Send(new UltimaLive.Network.MapDefinitions());
    }

    private static void EventSink_Login(LoginEventArgs args)
    {
      args.Mobile.Send(new UltimaLive.Network.QueryClientHash(args.Mobile));
    }
  }
}