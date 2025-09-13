using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;
using UltimaLive;
using System.IO;
using Server.Engines.Harvest;
using UltimaLive.LumberHarvest;
using Server.Regions;

namespace Server.Engines.Harvest
{
  public class UltimaLiveLumberjacking :  HarvestSystem
  {

    //List contains an entry for each map, each entry contains all the locations where trees have been chopped down
    public static Dictionary<int, Dictionary<Point3D, int>> RegrowthMasterLookupTable = new Dictionary<int, Dictionary<Point3D, int>>();
    public static DateTime LastGrowth = DateTime.UtcNow;
    

    //This is the minimum time between regrowths, but the regrowths will only happen on world saves
    public static TimeSpan TimeBetweenRegrowth = TimeSpan.FromMinutes(20);

    public static void Initialize()
    {

      List<int> tiles = new List<int>(UltimaLive.LumberHarvest.BaseTreeHarvestPhase.MasterHarvestablePhaseLookupByItemIdList.Keys);
      for (int i = 0; i < tiles.Count; ++i)
      {
        tiles[i] += 0x4000;
      }

      int[] tileNums = tiles.ToArray();
      Array.Sort(tileNums);
      HarvestDefinition lumber = new HarvestDefinition();
      lumber.Tiles = tileNums;

      // Players must be within 2 tiles to harvest
      lumber.MaxRange = 2;

      // Skill checking is done on the Lumberjacking skill
      lumber.Skill = SkillName.Lumberjacking;

      // The chopping effect
      lumber.EffectActions = new int[] { 13 };
      lumber.EffectSounds = new int[] { 0x13E };
      lumber.EffectCounts = (Core.AOS ? new int[] { 1 } : new int[] { 1, 2, 2, 2, 3 });
      lumber.EffectDelay = TimeSpan.FromSeconds(1.6);
      lumber.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

      lumber.NoResourcesMessage = 500493; // There's not enough wood here to harvest.
      lumber.FailMessage = 500495; // You hack at the tree for a while, but fail to produce any useable wood.
      lumber.OutOfRangeMessage = 500446; // That is too far away.
      lumber.PackFullMessage = 500497; // You can't place any wood into your backpack!
      lumber.ToolBrokeMessage = 500499; // You broke your axe.

      //not used
      lumber.RaceBonus = Core.ML;
      lumber.RandomizeVeins = Core.ML;
      lumber.BankWidth = 4;
      lumber.BankHeight = 3;
      lumber.MinTotal = 20;
      lumber.MaxTotal = 45;
      lumber.Resources = new HarvestResource[] { new HarvestResource(00.0, 00.0, 100.0, 500498, typeof(Log)) };
      lumber.BonusResources = new BonusHarvestResource[] { new BonusHarvestResource(0, 83.9, null, null) };
      lumber.Veins = new HarvestVein[] { new HarvestVein(49.0, 0.0, lumber.Resources[0], null) };
      System.Definitions.Add(lumber);
    }

    public static void Configure()
    {
      foreach (Map m in Map.AllMaps)
      {
        RegrowthMasterLookupTable.Add(m.MapID, new Dictionary<Point3D, int>());
      }

      EventSink.WorldSave += new WorldSaveEventHandler(OnSave);
      EventSink.WorldLoad += new WorldLoadEventHandler(OnLoad);
    }

    public static void OnLoad()
    {
      for (int mapId = 0; mapId < RegrowthMasterLookupTable.Count; ++mapId)
      {
        string filename = Path.Combine(UltimaLiveSettings.LumberHarvestFallenTreeSaveLocation, "TreeLocations." + mapId);
        FileInfo treeFileInfo = new FileInfo(filename);

        if (treeFileInfo.Exists)
        {
          using (FileStream fs = new FileStream(filename, FileMode.Open))
          {
            using (BinaryReader br = new BinaryReader(fs))
            {
              GenericReader reader = new BinaryFileReader(br);

              while (fs.Position < fs.Length)
              {
                Point3D p = reader.ReadPoint3D();
                int itemId = reader.ReadInt();
                if (!RegrowthMasterLookupTable[mapId].ContainsKey(p))
                {
                  RegrowthMasterLookupTable[mapId].Add(p, itemId);
                }
              }
            }
          }
        }
      }
    }

    public static void OnSave(WorldSaveEventArgs e)
    {
      if (!Directory.Exists(UltimaLiveSettings.LumberHarvestFallenTreeSaveLocation))
      {
        Directory.CreateDirectory(UltimaLiveSettings.LumberHarvestFallenTreeSaveLocation);
      }

      bool updateRegrowthTime = false;

      //foreach map in the lookup table
      foreach (Map m in Map.AllMaps)
      {
        if (RegrowthMasterLookupTable.ContainsKey(m.MapID))
        {
        #region Regrowth
        if (DateTime.UtcNow > LastGrowth + TimeBetweenRegrowth)
        {
          updateRegrowthTime = true;
          Dictionary<Point3D, int> mapLookupTable = RegrowthMasterLookupTable[m.MapID];
        MapOperationSeries mapOperations = null;

        List<Point3D> locationsToRemove = new List<Point3D>();

        //for each tree location in the lookup table
            foreach (KeyValuePair<Point3D, int> treeLocationKvp in mapLookupTable)
        {
          if (BaseHarvestablePhase.MasterHarvestablePhaseLookupByItemIdList.ContainsKey(treeLocationKvp.Value))
          {
            //look up the current phase
            bool existingTileFound = false;
                foreach (StaticTile tile in m.Tiles.GetStaticTiles(treeLocationKvp.Key.X, treeLocationKvp.Key.Y))
            {
              if (tile.Z == treeLocationKvp.Key.Z) // if the z altitude matches
              {
                if (BaseHarvestablePhase.MasterHarvestablePhaseLookupByItemIdList.ContainsKey(tile.ID)) //if the item id is linked to a phase
                {

                  BaseHarvestablePhase currentPhase = BaseHarvestablePhase.MasterHarvestablePhaseLookupByItemIdList[tile.ID];

                  foreach (GraphicAsset[] assetSet in currentPhase.BaseAssetSets)
                  {
                    if (assetSet.Length > 0 && assetSet[0].ItemID == tile.ID)
                    {
                      existingTileFound = true;
                    }
                  }

                      if (existingTileFound && !currentPhase.grow(treeLocationKvp.Key, m, ref mapOperations))
                  {
                    locationsToRemove.Add(treeLocationKvp.Key);
                  }

                  break;
                }
              }
            }

            //nothing to grow at this location, so construct the starting phase
            if (!existingTileFound)
            {
              //lookup original phase that was saved
              BaseHarvestablePhase grownPhase = BaseHarvestablePhase.MasterHarvestablePhaseLookupByItemIdList[treeLocationKvp.Value];

              //cleanup final harvest graphics
              if (grownPhase.FinalHarvestPhase != null && BaseHarvestablePhase.MasterHarvestablePhaseLookupByTypeList.ContainsKey(grownPhase.FinalHarvestPhase))
              {
                    BaseHarvestablePhase.MasterHarvestablePhaseLookupByTypeList[grownPhase.FinalHarvestPhase].Teardown(treeLocationKvp.Key, m, ref mapOperations);
              }

              if (grownPhase.StartingGrowthPhase != null && BaseHarvestablePhase.MasterHarvestablePhaseLookupByTypeList.ContainsKey(grownPhase.StartingGrowthPhase))
              {
                //remove stump
                  BaseTreeHarvestPhase maturePhase = grownPhase as BaseTreeHarvestPhase;
                  if (maturePhase != null)
                {
                  if (mapOperations != null)
                  {
                      mapOperations.Add(new DeleteStatic(m.MapID, new StaticTarget(treeLocationKvp.Key, maturePhase.StumpGraphic)));
                  }
                  else
                  {
                      mapOperations = new MapOperationSeries(new DeleteStatic(m.MapID, new StaticTarget(treeLocationKvp.Key, maturePhase.StumpGraphic)), m.MapID);
                  }
                }

                    BaseHarvestablePhase.MasterHarvestablePhaseLookupByTypeList[grownPhase.StartingGrowthPhase].Construct(treeLocationKvp.Key, m, ref mapOperations);
              }
            }
          }
        }

        if (mapOperations != null)
        {
          mapOperations.DoOperation();
        }

        foreach (Point3D p in locationsToRemove)
        {
            RegrowthMasterLookupTable[m.MapID].Remove(p);
        }
        }
        #endregion

          GenericWriter writer = new BinaryFileWriter(Path.Combine(UltimaLiveSettings.LumberHarvestFallenTreeSaveLocation, "TreeLocations." + m.MapID), true);

          foreach (KeyValuePair<Point3D, int> kvp in RegrowthMasterLookupTable[m.MapID])
        {
          writer.Write(kvp.Key);
          writer.Write(kvp.Value);
        }
        writer.Close();
        }
      }

      if (updateRegrowthTime)
      {
        LastGrowth = DateTime.UtcNow;
      }
    }

    private static UltimaLiveLumberjacking m_System;

    public static UltimaLiveLumberjacking System
    {
      get
      {
        if (m_System == null)
          m_System = new UltimaLiveLumberjacking();

        return m_System;
      }
    }

    public override void FinishHarvesting(Mobile from, Item tool, HarvestDefinition def, object toHarvest, object locked)
    {
      from.EndAction(locked);

      if (!CheckHarvest(from, tool))
        return;

      int tileID;
      Map map;
      Point3D loc;

      if (!GetHarvestDetails(from, tool, toHarvest, out tileID, out map, out loc))
      {
        OnBadHarvestTarget(from, tool, toHarvest);
        return;
      }
      else if (!def.Validate(tileID))
      {
        OnBadHarvestTarget(from, tool, toHarvest);
        return;
      }

      if (!CheckRange(from, tool, def, map, loc, true))
        return;
      else if (!CheckHarvest(from, tool, def, toHarvest))
        return;

      double skillBase = from.Skills[def.Skill].Base;
      double skillValue = from.Skills[def.Skill].Value;

      StaticTarget harvestTarget = toHarvest as StaticTarget;

      if (harvestTarget != null)
      {
        if (from.CheckSkill(def.Skill, 0, 120))
        {
          if (tool is IUsesRemaining)
          {
            IUsesRemaining toolWithUses = (IUsesRemaining)tool;

            toolWithUses.ShowUsesRemaining = true;

            if (toolWithUses.UsesRemaining > 0)
            {
              --toolWithUses.UsesRemaining;
            }
            if (toolWithUses.UsesRemaining < 1)
            {
              tool.Delete();
              def.SendMessageTo(from, def.ToolBrokeMessage);
            }
          }
        }

        BaseHarvestablePhase hTreePhase = BaseHarvestablePhase.LookupPhase(harvestTarget.ItemID);

        if (hTreePhase != null && hTreePhase is BaseTreeHarvestPhase)
        {
          hTreePhase.Harvest(from, harvestTarget.ItemID, harvestTarget.Location, map);
        }
      }
    }
  }
}