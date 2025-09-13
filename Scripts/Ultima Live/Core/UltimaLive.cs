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
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Items;
using UltimaLive.Network;

namespace UltimaLive
{
  #region X/Y Move by Elm
  public class IncStaticYCommand : BaseCommand
  {
    public IncStaticYCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "IncSY" };
      ObjectTypes = ObjectTypes.All;
      Usage = "IncSY";
      Description = "Increases the y value of a static.";
    }
    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify an amount to change the y coord.");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {

      int change = e.GetInt32(0);

      if (obj is StaticTarget)
      {
        new IncStaticY(e.Mobile.Map.MapID, (StaticTarget)obj, change).DoOperation();
      }
    }
  }
  public class IncStaticXCommand : BaseCommand
  {
    public IncStaticXCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "IncSX" };
      ObjectTypes = ObjectTypes.All;
      Usage = "IncSX";
      Description = "Increases the x value of a static.";
    }
    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify an amount to change the x coord.");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {

      int change = e.GetInt32(0);

      if (obj is StaticTarget)
      {
        new IncStaticX(e.Mobile.Map.MapID, (StaticTarget)obj, change).DoOperation();
      }
    }
  }

  #endregion


  public class IncStaticAltCommand : BaseCommand
  {
    public IncStaticAltCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "IncStaticAlt" };
      ObjectTypes = ObjectTypes.All;
      Usage = "IncStaticAlt";
      Description = "Increases the Z value of a static.";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify an amount to change the z coord.");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {

      int change = e.GetInt32(0);

      if (obj is StaticTarget)
      {
        new IncStaticAltitude(e.Mobile.Map.MapID, (StaticTarget)obj, change).DoOperation();
      }
    }
  }

  public class SetStaticAltCommand : BaseCommand
  {
    public SetStaticAltCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      //Supports = CommandSupport.AllNPCs | CommandSupport.AllItems;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "SetStaticAlt" };
      ObjectTypes = ObjectTypes.All;
      Usage = "SetStaticAlt";
      Description = "Set the Z value of a static.";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify the z coord.");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {

      int change = e.GetInt32(0);

      if (obj is StaticTarget)
      {
        new SetStaticAltitude(e.Mobile.Map.MapID, (StaticTarget)obj, change).DoOperation();
      }
    }
  }

  public class SetStaticIDCommand : BaseCommand
  {
    public SetStaticIDCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      //Supports = CommandSupport.AllNPCs | CommandSupport.AllItems;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "SetStaticID" };
      ObjectTypes = ObjectTypes.All;
      Usage = "SetStaticID";
      Description = "Set the ID value of a static.";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify the Item ID");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      int ID = e.GetInt32(0);
      if (obj is StaticTarget)
      {
        new SetStaticID(e.Mobile.Map.MapID, (StaticTarget)obj, ID).DoOperation();
      }
    }
  }

  public class SetStaticHueCommand : BaseCommand
  {
    public SetStaticHueCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      //Supports = CommandSupport.AllNPCs | CommandSupport.AllItems;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "SetStaticHue" };
      ObjectTypes = ObjectTypes.All;
      Usage = "SetStaticHue";
      Description = "Set the hue value of a static.";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify the hue");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      int Hue = e.GetInt32(0);
      if (obj is StaticTarget)
      {
        new SetStaticHue(e.Mobile.Map.MapID, (StaticTarget)obj, Hue).DoOperation();
      }
    }
  }

  public class DelStaticCommand : BaseCommand
  {
    public DelStaticCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      //Supports = CommandSupport.AllNPCs | CommandSupport.AllItems;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "DelStatic" };
      ObjectTypes = ObjectTypes.All;
      Usage = "DelStatic";
      Description = "Delete a static.";
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      if (obj is StaticTarget)
      {
        new DeleteStatic(e.Mobile.Map.MapID, (StaticTarget)obj).DoOperation();
      }
    }
  }

  public class AddStaticCommand : BaseCommand
  {
    public AddStaticCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      //Supports = CommandSupport.AllNPCs | CommandSupport.AllItems;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "addStatic" };
      ObjectTypes = ObjectTypes.All;
      Usage = "addStatic itemId Hue [altitude]";
      Description = "Add a static.";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length < 1 || e.Length > 3)
      {
        e.Mobile.SendMessage("You must specify the Item ID and, optionally a Hue and a Z value");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      int newHue = 0;
      int newID = e.GetInt32(0);
      if(e.Length >= 2)
      {
        newHue = e.GetInt32(1);
      }

      if (obj is IPoint3D)
      {
        IPoint3D location = e.Mobile.Location;

        if (obj is IPoint3D)
        {
          location = (IPoint3D)obj;
        }

        int newZ = location.Z;
        if (e.Length == 3)
        {
          newZ = e.GetInt32(2);
        }
        new AddStatic(e.Mobile.Map.MapID, newID, newZ, location.X, location.Y, newHue).DoOperation();
      }
    }
  }

  public class MoveStaticCommand : BaseCommand
  {
    public MoveStaticCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      Supports = CommandSupport.Single;

      Commands = new string[] { "MoveStatic" };
      ObjectTypes = ObjectTypes.All;
      Usage = "MoveStatic";
      Description = "Move a static.";
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      int newID = e.GetInt32(0);

      if (obj is StaticTarget)
      {
        e.Mobile.Target = new MoveStaticDestinationTarget((StaticTarget)obj, e.Mobile.Map.MapID);
      }
    }

    private class MoveStaticDestinationTarget : Target
    {
      protected StaticTarget m_StaticTarget;
      protected int m_MapID;
      public MoveStaticDestinationTarget(StaticTarget statTarget, int mapID)
        : base(-1, true, TargetFlags.None)
      {
        m_StaticTarget = statTarget;
        m_MapID = mapID;
      }

      protected override void OnTarget(Mobile from, object o)
      {
        if (m_MapID != from.Map.MapID)
        {
          from.SendMessage("The targets must be in the same map.");
          return;
        }

        if (o is IPoint3D)
        {
          IPoint3D location = (IPoint3D)o;
          MoveStatic ms = new MoveStatic(from.Map.MapID, m_StaticTarget, location.X, location.Y);
          ms.DoOperation();
        }
      }
    }
  }

  public class IncLandAltCommand : BaseCommand
  {
    public IncLandAltCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "IncLandAlt" };
      ObjectTypes = ObjectTypes.All;
      Usage = "IncLandAlt";
      Description = "Increase / decrease the Z altitude of a land tile";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify an amount to change the z coord.");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      int change = e.GetInt32(0);

      if (obj is IPoint3D)
      {
        IPoint3D location = (IPoint3D)obj;
        new IncLandAltitude(location.X, location.Y, e.Mobile.Map.MapID, change).DoOperation();
      }
    }
  }

  public class SetLandAltCommand : BaseCommand
  {
    public SetLandAltCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "SetLandAlt" };
      ObjectTypes = ObjectTypes.All;
      Usage = "SetLandAlt";
      Description = "Set the Z altitude of a land tile";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify the z coord.");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      int change = e.GetInt32(0);

      if (obj is IPoint3D)
      {
        IPoint3D location = (IPoint3D)obj;
        new SetLandAltitude(location.X, location.Y, e.Mobile.Map.MapID, change).DoOperation();
      }
    }
  }

  public class SetLandAreaAltCommand : BaseCommand
  {
    public SetLandAreaAltCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "SetLandAreaAlt" };
      ObjectTypes = ObjectTypes.All;
      Usage = "SetLandAreaAlt";
      Description = "Set the Z altitude of a land tile for a 4 tile radius";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 2)
      {
        e.Mobile.SendMessage("You must specify the z coord (height) and radius.");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      int change = e.GetInt32(0);
      int radius = e.GetInt32(1);

      if (obj is IPoint3D && change != null && radius != null)
      {
        IPoint3D location = (IPoint3D)obj;
        int o_x = location.X;
        int o_y = location.Y;

        new SetLandAltitude(location.X, location.Y, e.Mobile.Map.MapID, change).DoOperation();

					for ( int x = -radius; x <= radius; ++x )
					{
						for ( int y = -radius; y <= radius; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= radius )
								new SetLandAltitude(o_x + x, o_y + y, e.Mobile.Map.MapID, change).DoOperation();
						}
					}
          e.Mobile.Z += change;
      }
    }
  }

  public class SetLandIDCommand : BaseCommand
  {
    public SetLandIDCommand()
    {
      AccessLevel = AccessLevel.GameMaster;
      //Supports = CommandSupport.AllNPCs | CommandSupport.AllItems;
      Supports = CommandSupport.Simple;

      Commands = new string[] { "SetLandID" };
      ObjectTypes = ObjectTypes.All;
      Usage = "SetLandID";
      Description = "Set the ID value of a land tile";
    }

    public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
    {
      bool retVal = true;
      if (e.Length != 1)
      {
        e.Mobile.SendMessage("You must specify the Item ID");
        retVal = false;
      }
      return retVal;
    }

    public override void Execute(CommandEventArgs e, object obj)
    {
      int ID = e.GetInt32(0);
      if (obj is IPoint3D)
      {
        IPoint3D location = (IPoint3D)obj;
        new SetLandID(location.X, location.Y, e.Mobile.Map.MapID, ID).DoOperation();
      }
    }
  }


  public class Live
  {
    public static void Initialize()
    {
      CommandSystem.Prefix = "[";
      Register("LiveFreeze", AccessLevel.Administrator, new CommandEventHandler(LiveFreeze_OnCommand));
            Register("DelStaticArea", AccessLevel.Administrator, new CommandEventHandler(DelStaticArea_OnCommand));
      Register("GetBlockNumber", AccessLevel.GameMaster, new CommandEventHandler(getBlockNumber_OnCommand));
      Register("QueryClientHash", AccessLevel.GameMaster, new CommandEventHandler(queryClientHash_OnCommand));
      Register("updateblock", AccessLevel.GameMaster, new CommandEventHandler(updateBlock_OnCommand));
      Register("CircularIndent", AccessLevel.GameMaster, new CommandEventHandler(circularIndent_OnCommand));
      TargetCommands.Register(new IncStaticYCommand());
      TargetCommands.Register(new IncStaticXCommand());
      TargetCommands.Register(new IncStaticAltCommand());
      TargetCommands.Register(new SetStaticHueCommand());
      TargetCommands.Register(new SetStaticAltCommand());
      TargetCommands.Register(new SetStaticIDCommand());
      TargetCommands.Register(new DelStaticCommand());
      TargetCommands.Register(new AddStaticCommand());
      TargetCommands.Register(new MoveStaticCommand());
      TargetCommands.Register(new IncLandAltCommand());
      TargetCommands.Register(new SetLandAltCommand());
      TargetCommands.Register(new SetLandAreaAltCommand());
      TargetCommands.Register(new SetLandIDCommand());

    }

    public static void Register(string command, AccessLevel access, CommandEventHandler handler)
    {
      CommandSystem.Register(command, access, handler);
    }


    #region Leveling Target
    public class RadialTarget : Target
    {
      private int m_Height;
      private int m_TType;
      private int m_Radius;

      public int Height
      {
        get { return m_Height; }
        set { m_Height = value; }
      }

      public int TType
      {
        get { return m_TType; }
        set { m_TType = value; }
      }

      public int Radius
      {
        get { return m_Radius; }
        set { m_Radius = value; }
      }

      public RadialTarget(int TType, int Radius, int Height)
        : base(-1, true, TargetFlags.None)
      {
        m_TType = TType;
        m_Radius = Radius;
        m_Height = Height;
      }

      public override Packet GetPacketFor(NetState ns)
      {
        List<TargetObject> objs = new List<TargetObject>();
        List<Point2D> circle = UltimaLiveUtility.rasterCircle(new Point2D(0, 0), m_Radius);

        foreach (Point2D p in circle)
        {
          TargetObject t = new TargetObject();
          t.ItemID = 0xA12;
          t.Hue = 35;
          t.xOffset = p.X;
          t.yOffset = p.Y;
          t.zOffset = 0;
          objs.Add(t);
        }
        return new TargetObjectList(objs, ns.Mobile, true);
      }
    }
    #endregion

    #region Custom Land Commands
    [Usage("CircularIndent")]
    [Description("Makes a circular indent in the terrain.")]
    private static void circularIndent_OnCommand(CommandEventArgs e)
    {
      if (e.Length != 2)
      {
        e.Mobile.SendMessage("You must specify a radius and a depth.");
        return;
      }
      int radius = e.GetInt32(0);
      int depth = e.GetInt32(1);

      if (radius > 0)
      {
        e.Mobile.Target = new CircularIndentTarget(radius, depth);
      }
    }
    #endregion

    #region Custom Land Targets
    private class CircularIndentTarget : BaseLandRadialTarget
    {
      private int m_Radius;
      private int m_Depth;
      public CircularIndentTarget(int radius, int depth)
        : base(1, radius, 0)
      {
        m_Radius = radius;
        m_Depth = depth;
      }

      protected override void OnTarget(Mobile from, object o)
      {
        if (base.SetupTarget(from, o))
        {
          List<Point2D> circle = UltimaLiveUtility.rasterFilledCircle(new Point2D(m_Location.X, m_Location.Y), m_Radius);

          MapOperationSeries moveSeries = new MapOperationSeries(null, from.Map.MapID);

          bool first = true;
          foreach (Point2D p in circle)
          {
            if (first)
            {
              moveSeries = new MapOperationSeries(new IncLandAltitude(p.X, p.Y, from.Map.MapID, m_Depth), from.Map.MapID);
              first = false;
            }
            else
            {
              moveSeries.Add(new IncLandAltitude(p.X, p.Y, from.Map.MapID, m_Depth));
            }
          }

          moveSeries.DoOperation();
        }
      }

    }
    #endregion

    #region Static Commands
    [Usage("LiveFreeze")]
    [Description("Makes a targeted area of dynamic items static.")]
    public static void LiveFreeze_OnCommand(CommandEventArgs e)
    {
      BoundingBoxPicker.Begin(e.Mobile, new BoundingBoxCallback(LiveFreezeBox_Callback), null);
    }

    private static Point3D NullP3D = new Point3D(int.MinValue, int.MinValue, int.MinValue);
    private class StateInfo
    {
      public Map m_Map;
      public Point3D m_Start, m_End;

      public StateInfo(Map map, Point3D start, Point3D end)
      {
        m_Map = map;
        m_Start = start;
        m_End = end;
      }
    }

    private class DeltaState
    {
      public int m_X, m_Y;
      public List<Item> m_List;
      public DeltaState(Point2D p)
      {
        m_X = p.X;
        m_Y = p.Y;
        m_List = new List<Item>();
      }
    }

    private const string LiveFreezeWarning = "Those items will be frozen into the map. Do you wish to proceed?";
    private static void LiveFreezeBox_Callback(Mobile from, Map map, Point3D start, Point3D end, object state)
    {
      SendWarning(from, "You are about to freeze a section of items.", LiveFreezeWarning, map, start, end, new WarningGumpCallback(LiveFreezeWarning_Callback));
    }

    private static void LiveFreezeWarning_Callback(Mobile from, bool okay, object state)
    {
      if (!okay)
        return;

      StateInfo si = (StateInfo)state;

      LiveFreeze(from, si.m_Map, si.m_Start, si.m_End);
    }

    public static void LiveFreeze(Mobile from, Map targetMap, Point3D start3d, Point3D end3d)
    {
      Dictionary<Point2D, List<Item>> ItemsByBlockLocation = new Dictionary<Point2D, List<Item>>();
      if (targetMap != null && start3d != NullP3D && end3d != NullP3D)
      {
        Point2D start = targetMap.Bound(new Point2D(start3d));
        Point2D end = targetMap.Bound(new Point2D(end3d));
        IPooledEnumerable eable = targetMap.GetItemsInBounds(new Rectangle2D(start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1));

        Console.WriteLine(string.Format("Invoking live freeze from {0},{1} to {2},{3}", start.X, start.Y, end.X, end.Y));

        foreach (Item item in eable)
        {
          if (item is Static || item is BaseFloor || item is BaseWall)
          {
            Map itemMap = item.Map;
            if (itemMap == null || itemMap == Map.Internal)
              continue;

            Point2D p = new Point2D(item.X >> 3, item.Y >> 3);
            if (!(ItemsByBlockLocation.ContainsKey(p)))
            {
              ItemsByBlockLocation.Add(p, new List<Item>());
            }
            ItemsByBlockLocation[p].Add(item);
          }
        }

        eable.Free();
      }
      else
      {
        from.SendMessage("That was not a proper area. Please retarget and reissue the command.");
        return;
      }

      TileMatrix matrix = targetMap.Tiles;
      foreach (KeyValuePair<Point2D, List<Item>> kvp in ItemsByBlockLocation)
      {
        StaticTile[][][] blockOfTiles = matrix.GetStaticBlock(kvp.Key.X, kvp.Key.Y);
        Dictionary<Point2D, List<StaticTile>> newBlockStatics = new Dictionary<Point2D, List<StaticTile>>();

        foreach (Item item in kvp.Value)
        {
          int xOffset = item.X - (kvp.Key.X * 8);
          int yOffset = item.Y - (kvp.Key.Y * 8);
          if (xOffset < 0 || xOffset >= 8 || yOffset < 0 || yOffset >= 8)
            continue;

          StaticTile newTile = new StaticTile((ushort)item.ItemID, (byte)xOffset, (byte)yOffset, (sbyte)item.Z, (short)item.Hue);
          Point2D refPoint = new Point2D(xOffset, yOffset);

          if (!(newBlockStatics.ContainsKey(refPoint)))
          {
            newBlockStatics.Add(refPoint, new List<StaticTile>());
          }

          newBlockStatics[refPoint].Add(newTile);
          item.Delete();
        }

        for (int i = 0; i < blockOfTiles.Length; i++)
          for (int j = 0; j < blockOfTiles[i].Length; j++)
            for (int k = 0; k < blockOfTiles[i][j].Length; k++)
            {
              Point2D refPoint = new Point2D(i, j);
              if (!(newBlockStatics.ContainsKey(refPoint)))
              {
                newBlockStatics.Add(refPoint, new List<StaticTile>());
              }

              newBlockStatics[refPoint].Add(blockOfTiles[i][j][k]);
            }

        StaticTile[][][] newblockOfTiles = new StaticTile[8][][];

        for (int i = 0; i < 8; i++)
        {
          newblockOfTiles[i] = new StaticTile[8][];
          for (int j = 0; j < 8; j++)
          {
            Point2D p = new Point2D(i, j);
            int length = 0;
            if (newBlockStatics.ContainsKey(p))
            {
              length = newBlockStatics[p].Count;
            }
            newblockOfTiles[i][j] = new StaticTile[length];
            for (int k = 0; k < length; k++)
            {
              if (newBlockStatics.ContainsKey(p))
              {
                newblockOfTiles[i][j][k] = newBlockStatics[p][k];
              }
            }
          }
        }

        matrix.SetStaticBlock(kvp.Key.X, kvp.Key.Y, newblockOfTiles);
        int blockNum = ((kvp.Key.X * matrix.BlockHeight) + kvp.Key.Y);

        List<Mobile> candidates = new List<Mobile>();
        int bX = kvp.Key.X * 8;
        int bY = kvp.Key.Y * 8;

        IPooledEnumerable eable = targetMap.GetMobilesInRange(new Point3D(bX, bY, 0));

        foreach (Mobile m in eable)
        {
          if (m.Player)
          {
            candidates.Add(m);
          }
        }
        eable.Free();

        CRC.InvalidateBlockCRC(targetMap.MapID, blockNum);
        foreach (Mobile m in candidates)
        {
          m.Send(new UpdateStaticsPacket(new Point2D(kvp.Key.X, kvp.Key.Y), m));
        }
        MapChangeTracker.MarkStaticsBlockForSave(targetMap.MapID, kvp.Key);
      }
    }

    [Usage("DelStaticArea")]
    [Description("Clear an area of statics.")]
    public static void DelStaticArea_OnCommand(CommandEventArgs e)
    {
      BoundingBoxPicker.Begin(e.Mobile, new BoundingBoxCallback(LiveClearBox_Callback), null);
    }

    private static Point3D NullP3DD = new Point3D(int.MinValue, int.MinValue, int.MinValue);
     private static void LiveClearBox_Callback(Mobile from, Map map, Point3D start, Point3D end, object state)
    {
      SendWarningg(from, "You are about to clear a section of items.", LiveClearWarning, map, start, end, new WarningGumpCallback(LiveClearWarning_Callback));
    }

    private class StateInfoo
    {
      public Map m_Map;
      public Point3D m_Start, m_End;

      public StateInfoo(Map map, Point3D start, Point3D end)
      {
        m_Map = map;
        m_Start = start;
        m_End = end;
      }
    }

    private const string LiveClearWarning = "Those items will be deleted from the map. Do you wish to proceed?";

    private static void LiveClearWarning_Callback(Mobile from, bool okay, object state)
    {
      if (!okay)
      {
        return;
      }

      StateInfoo si = (StateInfoo)state;

      LiveClear(from, si.m_Map, si.m_Start, si.m_End);
    }

    public static void LiveClear(Mobile from, Map targetMap, Point3D start3d, Point3D end3d)
    {
/*  Approach taken from extract request from architect... couldn't get it to work yet
			TileMatrix matrix = targetMap.Tiles;

			using ( FileStream idxStream = OpenFile(matrix.IndexStream) )
			{
				using ( FileStream mulStream = OpenFile(matrix.DataStream) )
				{
					if ( idxStream == null || mulStream == null )
					{
						return;
					}

					BinaryReader idxReader = new BinaryReader( idxStream );        
					for ( int x = start3d.X; x <= (end3d.X - start3d.X); ++x )
					{
						for ( int y = start3d.Y; x <= (end3d.Y - start3d.Y); ++y )
						{
							int tileCount;
							StaticTile[] staticTiles = ReadStaticBlock( idxReader, mulStream, x, y, matrix.BlockWidth, matrix.BlockHeight, out tileCount );

							if ( tileCount < 0 )
								continue;
							
							for(int i = 0; i < tileCount; ++i)
							{
								StaticTile tile = staticTiles[i];
								StaticTarget item = new StaticTarget(new Point3D(x, y, tile.Z), tile.ItemID);

                new DeleteStatic(targetMap.MapID, item.DoOperation());
							}
						}
					}
        }
      }
*/







      /* Doesn't work... doesn't delete anything - assume statics aren't read?
      //Dictionary<Point2D, List<Item>> ItemsByBlockLocation = new Dictionary<Point2D, List<Item>>();
      if (targetMap != null && start3d != NullP3DD && end3d != NullP3DD)
      {
        Console.WriteLine("1");
        Point2D start = targetMap.Bound(new Point2D(start3d));
        Point2D end = targetMap.Bound(new Point2D(end3d));
        IPooledEnumerable eable = targetMap.GetObjectsInBounds(new Rectangle2D(start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1));

       Console.WriteLine(string.Format("Invoking live clear from {0},{1} to {2},{3}", start.X, start.Y, end.X, end.Y));


        int total = 0;
        foreach (Object o in eable)
        {
          total++;
          Console.WriteLine("1.5");
          if (o is Static )
          {
            StaticTarget tg = new StaticTarget(((Static)o).Location, ((Static)o).ItemID);
            Console.WriteLine("2");
            new DeleteStatic(targetMap.MapID, tg).DoOperation();
          }
          if (o is StaticTarget )
          {
            Console.WriteLine("3");
            new DeleteStatic(targetMap.MapID, (StaticTarget)o).DoOperation();
          }
          if (o is Item)
          {
            ((Item)o).Delete();
          }
        }
        Console.WriteLine(total);

        eable.Free();
      }
      else
      {
        from.SendMessage("That was not a proper area. Please retarget and reissue the command.");
        return;
      }*/
    }

		private FileStream OpenFile( FileStream orig )
		{
			if ( orig == null )
				return null;

			try{ return new FileStream( orig.Name, FileMode.Open, FileAccess.Read, FileShare.Read ); }
			catch{ return null; }
		}

    public static void SendWarningg(Mobile m, string header, string baseWarning, Map map, Point3D start, Point3D end, WarningGumpCallback callback)
    {
      m.SendGump(new WarningGump(1060635, 30720, String.Format(baseWarning, String.Format(header, map)), 0xFFC000, 420, 400, callback, new StateInfoo(map, start, end)));
    }
    public static void SendWarning(Mobile m, string header, string baseWarning, Map map, Point3D start, Point3D end, WarningGumpCallback callback)
    {
      m.SendGump(new WarningGump(1060635, 30720, String.Format(baseWarning, String.Format(header, map)), 0xFFC000, 420, 400, callback, new StateInfo(map, start, end)));
    }

    #endregion

    #region Land Targets

    private class BaseLandRadialTarget : RadialTarget
    {
      protected IPoint3D m_Location;
      public BaseLandRadialTarget(int TType, int Radius, int Height)
        : base(TType, Radius, Height)
      {
      }

      protected override void OnTarget(Mobile from, object o)
      {
      }

      protected bool SetupTarget(Mobile from, object o)
      {
        if (!BaseCommand.IsAccessible(from, o))
        {
          from.SendMessage("That is not accessible.");
          return false;
        }

        if (!(o is IPoint3D))
          return false;
        m_Location = (IPoint3D)o;

        return true;
      }
    }

    private class BaseLandTarget : Target
    {
      protected IPoint3D m_Location;
      public BaseLandTarget()
        : base(-1, true, TargetFlags.None)
      {
      }

      protected override void OnTarget(Mobile from, object o)
      {
      }

      protected bool SetupTarget(Mobile from, object o)
      {
        if (!BaseCommand.IsAccessible(from, o))
        {
          from.SendMessage("That is not accessible.");
          return false;
        }

        if (!(o is IPoint3D))
          return false;
        m_Location = (IPoint3D)o;

        return true;
      }
    }
    #endregion

    #region Miscellaneous Commands
    [Usage("GetBlockNumber")]
    [Description("Returns the current block number")]
    private static void getBlockNumber_OnCommand(CommandEventArgs e)
    {
      int x = e.Mobile.Location.X;
      int y = e.Mobile.Location.Y;
      Map map = e.Mobile.Map;
      TileMatrix tm = map.Tiles;

      int blocknum = (((x >> 3) * tm.BlockHeight) + (y >> 3));

      e.Mobile.SendMessage(string.Format("Your block number is {0}", blocknum));
    }

    [Usage("updateblock")]
    [Description("Sends Update statics & terrain Packet to the client.")]
    public static void updateBlock_OnCommand(CommandEventArgs e)
    {
      Mobile from = e.Mobile;
      from.Send(new UltimaLive.Network.UpdateTerrainPacket(new Point2D(from.X >> 3, from.Y >> 3), from));
      from.Send(new UltimaLive.Network.UpdateStaticsPacket(new Point2D(from.X >> 3, from.Y >> 3), from));
      Console.WriteLine("Sending update statics packet");
    }

    [Usage("queryclienthash")]
    [Description("sends the client a request to hash its surrounding blocks")]
    public static void queryClientHash_OnCommand(CommandEventArgs e)
    {
      Mobile from = e.Mobile;
      from.Send(new UltimaLive.Network.QueryClientHash(from));
    }
    #endregion
  }
}