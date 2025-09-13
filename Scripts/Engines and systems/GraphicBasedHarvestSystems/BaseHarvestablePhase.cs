using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;
using UltimaLive;
using Server.Engines.Harvest;


namespace UltimaLive.LumberHarvest
{
  /*
  +---------------------------+              +---------------------------+
  | GraphicAsset              +-------+      | BaseHarvestablePhase      |
  |---------------------------|       |      |---------------------------|
  | ItemId                    |       |      | FinalHarvestPhase         |
  | xOffset                   |       |      | StartingGrowthPhase       |
  | yOffset                   |       |      | NextHarvestPhase          |
  | HarvestResourceBaseAmount |       |      | NextGrowthPhase           |
  | BonusResourceBaseAmount   |       |      |                           |
  | BonusResources            |       |      | HarvestResourceBaseAmount |
  |                           |       |      | BonusResourceBaseAmount   |
  | +ReapBonusResources       |       |      | HarvestSkill              |
  +---------------------------+       |      |                           |
                                      |      | PhaseResources            |
                                      |      | BonusResources            |
                                      +-----+| BaseAssetSets             |
                                             |                           |
                                             | +Construct                |
                                             | +TearDown                 |
                                             | +Harvest                  |
                                             | +Grow                     |
                                             |                           |
                                             | +ReapResource             |
                                             | +ReapBonusResources       |
                                             |                           |
                                             | +LookupOriginLocation     |
                                             | +LookupAsset              |
                                             | +LookupPhase              |
                                             | +LookupStaticHue          |
                                             | +LookupStaticTile         |
                                             |                           |
                                             +---------------------------+

                            Grow                     Grow                      Grow

         +----------------+       +----------------+       +-----------------+      +-----------------+
         | SaplingPhase1  +------>| SaplingPhase2  +------>| GrownTreePhase1 +----->| GrownTreePhase2 |
         +----------------+       +----------------+       +-----------------+      +-----------------+


                           Harvest                   Harvest

         +-----------------+      +------------------+      +------------------+
         | GrownTreePhase2 +----->| FallenTreePhase1 +----->| FallenTreePhase2 +-------+
         +-----------------+      +------------------+      +------------------+       |
                                                                     ^                 |
                                                                     |     Harvest     |
                                                                     |                 |
                                                                     +-----------------+
    
                         +----------------------+
                         | BaseHarvestablePhase |
                         +----------------------+
                                     ^ ^
                                     | |
                                     | +-----------------------------------------------------------------+
                                     |                                                                   |
                           +---------+--------+                                               +---------------------+
                           | TreeHarvestPhase |                                               | RockMiningBasePhase |
                           +------------------+                                               +---------------------+
                                   ^ ^ ^                                                                 ^  ^
                                   | | |                                                                 |  |
                                   | | |                                                                 |  +--------------+
                                   | | |                                                                 |                 |
          +------------------------+ | +--------------------------+                               +------+------+  +-------+-------+
          |                          |                            |                               | MoltenPhase |  | HardenedPhase |
 +--------+---------+      +---------+----------+       +---------+-------+                       +-------------+  +---------------+
 | SaplingTreePhase |      | FullGrownTreePhase |       | FallenTreePhase |                                              ^ ^
 +------------------+      +--------------------+       +-----------------+                                              | |
                                     ^                            ^  ^                                    +--------------+ |
                                     |                            |  |                                    |                |
                             +-------+--------+                   |  +--------------+                +----+-----+    +-----+------+
                             | SmallTreePhase |                   |                 |                | GoldVein |    | CopperVein |
                             +----------------+         +---------+-------+ +---------------+        +----------+    +------------+
                                   ^ ^ ^                | SmallFallenTree | | FallenOakTree |
                                   | | |                +-----------------+ +---------------+
                                   | | |
                       +-----------+ | +------------+
                       |             |              |
                 +-----+-----+ +-----+-----+ +------+-----+
                 | OakTree   | | CedarTree | | WalnutTree |
                 +-----------+ +-----------+ +------------+ 
    
   */


  public abstract class BaseHarvestablePhase
  {
    public const int STATIC_TILE_SEARCH_Z_TOLERANCE = 5;
    public virtual Type FinalHarvestPhase { get { return null; } }
    public virtual Type StartingGrowthPhase { get { return null; } }
    public virtual Type NextHarvestPhase { get { return null; } }
    public virtual Type NextGrowthPhase { get { return null; } }
    public virtual bool ConstructUsingHues { get { return false; } }
    public virtual int HarvestResourceBaseAmount { get { return 0; } }
    public virtual int BonusResourceBaseAmount { get { return 0; } }
    public virtual SkillName HarvestSkill { get { return SkillName.Lumberjacking; } }

    public Dictionary<int, HarvestResource> PhaseResources = new Dictionary<int, HarvestResource>();
    public Dictionary<int, BonusHarvestResource[]> BonusResources = new Dictionary<int, BonusHarvestResource[]>();
    public List<GraphicAsset[]> BaseAssetSets = new List<GraphicAsset[]>();

    public BaseHarvestablePhase()
    {
    }

    #region Registration and Asset Lookup
    public static Dictionary<int, GraphicAsset> MasterHarvestableAssetlookup = new Dictionary<int, GraphicAsset>();
    public static Dictionary<int, BaseHarvestablePhase> MasterHarvestablePhaseLookupByItemIdList = new Dictionary<int, BaseHarvestablePhase>();
    public static Dictionary<Type, BaseHarvestablePhase> MasterHarvestablePhaseLookupByTypeList = new Dictionary<Type, BaseHarvestablePhase>();

    public static bool RegisterHarvestablePhase(BaseHarvestablePhase phase)
    {
      bool alreadyRegistered = true;
      if (!MasterHarvestablePhaseLookupByTypeList.ContainsKey(phase.GetType()))
      {
        MasterHarvestablePhaseLookupByTypeList.Add(phase.GetType(), phase);
        alreadyRegistered = false;
        phase.RegisterBasicPhaseTiles();

        //send out update tiles event
        if (UpdatedTilesEvent != null)
        {
          List<int> tiles = new List<int>(UltimaLive.LumberHarvest.BaseTreeHarvestPhase.MasterHarvestablePhaseLookupByItemIdList.Keys);
          UpdatedTilesEvent(tiles.ToArray());
        }
      }
      else
      {
        Console.WriteLine("NOT Registering " + phase.GetType());
      }

      return alreadyRegistered;
    }

    public delegate void UpdateTilesEventHandler(int[] newTiles);
    public static event UpdateTilesEventHandler UpdatedTilesEvent;

    public virtual void RegisterAssetSet(GraphicAsset[] assetSet)
    {
      foreach (GraphicAsset treeAsset in assetSet)
      {
        if (!MasterHarvestablePhaseLookupByItemIdList.ContainsKey(treeAsset.ItemID))
        {
          MasterHarvestablePhaseLookupByItemIdList.Add(treeAsset.ItemID, this);
        }
        else
        {
          Console.WriteLine(this.GetType().ToString() + " tried to add a graphic 0x" + treeAsset.ItemID.ToString("X") + " to the MasterHarvestablePhaseLookupByItemIdList table that is already in use.");
        }

        if (!MasterHarvestableAssetlookup.ContainsKey(treeAsset.ItemID))
        {
          MasterHarvestableAssetlookup.Add(treeAsset.ItemID, treeAsset);
        }
        else
        {
          Console.WriteLine(this.GetType().ToString() + " tried to add a graphic 0x" + treeAsset.ItemID.ToString("X") + " to the MasterHarvestableAssetlookup table that is already in use.");
        }

      }
    }

    public virtual void RegisterBasicPhaseTiles()
    {
      //Add all the trunk graphics for this phase
      foreach (GraphicAsset[] assetSet in BaseAssetSets)
      {
        RegisterAssetSet(assetSet);
      }
    }

    #endregion

    #region Lookups
    public virtual Point3D LookupOriginLocation(Point3D harvestTargetLocation, int harvestTargetItemId)
    {
      Point3D trunkLocation = Point3D.Zero;

      bool found = false;

      foreach (GraphicAsset[] trunkSet in BaseAssetSets)
      {
        foreach (GraphicAsset asset in trunkSet)
        {
          if (asset.ItemID == harvestTargetItemId)
          {
            trunkLocation = new Point3D(harvestTargetLocation.X - asset.XOffset, harvestTargetLocation.Y - asset.YOffset, harvestTargetLocation.Z - TileData.ItemTable[harvestTargetItemId].CalcHeight);
            found = true;
            break;
          }
        }

        if (found)
        {
          break;
        }
      }

      return trunkLocation;
    }

    public static GraphicAsset LookupAsset(int itemId)
    {
      GraphicAsset asset = null;

      if (MasterHarvestableAssetlookup.ContainsKey(itemId))
      {
        asset = MasterHarvestableAssetlookup[itemId];
      }

      return asset;
    }
    
    public static BaseHarvestablePhase LookupPhase(int itemId)
    {
      BaseHarvestablePhase phase = null;

      if (MasterHarvestablePhaseLookupByItemIdList.ContainsKey(itemId))
      {
        phase = MasterHarvestablePhaseLookupByItemIdList[itemId];
      }

      return phase;
    }
    
    public static int LookupStaticHue(Map map, Point3D staticLocation, int itemId)
    {
      int hue = 0;
      foreach (StaticTile tile in map.Tiles.GetStaticTiles(staticLocation.X, staticLocation.Y))
      {
        if (tile.Z >= staticLocation.Z - STATIC_TILE_SEARCH_Z_TOLERANCE && tile.Z <= staticLocation.Z + STATIC_TILE_SEARCH_Z_TOLERANCE)
        {
          if (tile.ID == itemId)
          {
            hue = tile.Hue;

            continue;
          }
        }
      }
      return hue;
    }

    public static bool LookupStaticTile(Map map, Point3D staticLocation, int itemId, ref StaticTile tileResult)
    {
      bool found = false;

      foreach (StaticTile tile in map.Tiles.GetStaticTiles(staticLocation.X, staticLocation.Y))
      {
        if (tile.Z >= staticLocation.Z - STATIC_TILE_SEARCH_Z_TOLERANCE && tile.Z <= staticLocation.Z + STATIC_TILE_SEARCH_Z_TOLERANCE)
        {
          if (tile.ID == itemId)
          {
            tileResult = tile;
            found = true;
            continue;
          }
        }
      }
      return found;
    }

    #endregion

    #region Construction / Teardown
    public static List<KeyValuePair<int, GraphicAsset>> RemoveAssets(Map map, Point3D trunkLocation, ref MapOperationSeries deleteTreePartsOperationSeries, List<GraphicAsset[]> assetList)
    {
      List<KeyValuePair<int, GraphicAsset>> treePartsRemoved = new List<KeyValuePair<int, GraphicAsset>>();

      foreach (GraphicAsset[] assetSet in assetList)
      {
        foreach (GraphicAsset treeAsset in assetSet)
        {
          int leafX = trunkLocation.X + treeAsset.XOffset;
          int leafY = trunkLocation.Y + treeAsset.YOffset;

          foreach (StaticTile tile in map.Tiles.GetStaticTiles(leafX, leafY))
          {
            if (tile.Z >= trunkLocation.Z - STATIC_TILE_SEARCH_Z_TOLERANCE && tile.Z <= trunkLocation.Z + STATIC_TILE_SEARCH_Z_TOLERANCE)
            {
              if (tile.ID == treeAsset.ItemID)
              {
                Point3D leafLocation = new Point3D(leafX, leafY, tile.Z);

                if (deleteTreePartsOperationSeries == null)
                {
                  deleteTreePartsOperationSeries = new MapOperationSeries(new DeleteStatic(map.MapID, new StaticTarget(leafLocation, treeAsset.ItemID)), map.MapID);
                }
                else
                {
                  deleteTreePartsOperationSeries.Add(new DeleteStatic(map.MapID, new StaticTarget(leafLocation, treeAsset.ItemID)));
                }

                treePartsRemoved.Add(new KeyValuePair<int, GraphicAsset>(tile.Hue, treeAsset));

                continue;
              }
            }
          }
        }
      }

      return treePartsRemoved;
    }

    public void Construct(Point3D location, Map map)
    {
      MapOperationSeries series = null;
      Construct(location, map, ref series);
      if (series != null)
      {
        series.DoOperation();
      }
    }

    public virtual void onBeforeConstruct(Point3D location, Map map, ref MapOperationSeries mapOperationSeries)
    {
    }

    /*
     * Return value is the HUE
     */
    public virtual int Construct(Point3D location, Map map, ref MapOperationSeries mapOperationSeries)
    {
      onBeforeConstruct(location, map, ref mapOperationSeries);

      GraphicAsset[] trunkSet = null;

      if (BaseAssetSets != null && BaseAssetSets.Count > 0)
      {
        trunkSet = BaseAssetSets[Utility.Random(BaseAssetSets.Count)];
      }

      int hue = 0;

      List<int> possibleHues = new List<int>(PhaseResources.Keys);

      if (ConstructUsingHues && possibleHues.Count > 0)
      {
        hue = possibleHues[Utility.Random(possibleHues.Count)];
      }

      if (trunkSet != null)
      {
        foreach (GraphicAsset asset in trunkSet)
        {
          //come back and add a usestatic boolean to this class and add it into the constructor too
          AddStatic addStatic = new AddStatic(map.MapID, asset.ItemID, location.Z, location.X + asset.XOffset, location.Y + asset.YOffset, hue);
          if (mapOperationSeries == null)
          {
            mapOperationSeries = new MapOperationSeries(addStatic, map.MapID);
          }
          else
          {
            mapOperationSeries.Add(addStatic);
          }
        }
      }

      return hue;
    }

    public void Teardown(Point3D originLocation, Map map)
    {
      MapOperationSeries series = null;
      Teardown(originLocation, map, ref series);
      if (series != null)
      {
        series.DoOperation();
      }
    }

    public virtual void Teardown(Point3D originLocation, Map map, ref MapOperationSeries mapActions)
    {
      List<KeyValuePair<int, GraphicAsset>> basePiecesRemoved = RemoveAssets(map, originLocation, ref mapActions, BaseAssetSets);
    }

    #endregion

    #region Resource Reap Methods
    public virtual Item ReapResource(int hue, Mobile from, int amount)
    {
      Item resourceItem = null;
      HarvestResource resource = null;

      if (amount > 0)
      {
        if (PhaseResources.ContainsKey(hue))
        {
          resource = PhaseResources[hue];
        }
        else if (PhaseResources.ContainsKey(0))
        {
          resource = PhaseResources[0];
        }

        if (resource != null)
        {
          double skillBase = from.Skills[HarvestSkill].Base;

          if (skillBase >= resource.ReqSkill)
          {
            try
            {

              Type type = resource.Types[Utility.Random(resource.Types.Length)];
              Item item = Activator.CreateInstance(type) as Item;
              if (item != null)
              {
                item.Amount = amount;
                resourceItem = item;
              }
            }
            catch
            {
              Console.WriteLine("exception caught when trying to create bonus resource");
            }
          }
          else
          {
            //TODO: Inform player they don't have enough skill using a cliloc to do it
            from.SendMessage("you don't have enough skill to harvest that!");
          }
        }
      }

      return resourceItem;
    }

    public virtual List<Item> ReapBonusResources(int hue, Mobile from)
    {
      return GraphicAsset.ReapBonusResources(hue, from, BonusResourceBaseAmount, BonusResources);
    }

    #endregion

    #region Harvest and Grow

    /* This is the harvest function for a phase.  If a next harvest phase is defined, this function tears down all the assets associated
     * with the phase and constructs the next growth phase.  It also optionally gives any phase resources out according to the base 
     * amount specified. It then optionally gives out any bonus resources according to the amount specified.
     * 
     * If the next harvest phase has not been defined, it removes the asset that was targetted and gives out an amount of phase 
     * resources specified in the graphic asset. It then gives out any bonus resources associated with the graphic.
     * 
     *                      Harvest                   Harvest
     *
     * +-----------------+      +------------------+      +------------------+
     * | GrownTreePhase2 +----->| FallenTreePhase1 +----->| FallenTreePhase2 +-------+
     * +-----------------+      +------------------+      +------------------+       |
     *                                                             ^                 |
     *                                                             |     Harvest     |
     *                                                             |                 |
     *                                                             +-----------------+
     * 
     */
    public virtual bool Harvest(Mobile from, int itemId, Point3D location, Map map)
    {
      return false;
    }


    /* This is the growth function for a phase. If a next growth phase is defined, this function tears down all the assets associated with the
     * phase and constructs the next growth phase. If the next phase has not been defined, this function assumes that the phase has finished  
     * growing, and does nothing.
     * 
     *                       Grow                     Grow                      Grow                     Grow
     *
     *    +----------------+       +----------------+       +-----------------+      +-----------------+
     *    | SaplingPhase1  +------>| SaplingPhase2  +------>| GrownTreePhase1 +----->| GrownTreePhase2 |---------->( Stop )
     *    +----------------+       +----------------+       +-----------------+      +-----------------+
     *    
     */
    public virtual bool Grow(Point3D originLocation, Map map)
    {
      MapOperationSeries series = null;
      bool grew = grow(originLocation, map, ref series);
      if (series != null)
      {
        series.DoOperation();
      }

      return grew;
    }

    public virtual bool grow(Point3D originLocation, Map map, ref MapOperationSeries series)
    {
      bool grew = false;

      if (NextGrowthPhase != null)
      {
        Teardown(originLocation, map, ref series);
        MasterHarvestablePhaseLookupByTypeList[NextGrowthPhase].Construct(originLocation, map); //this is the transition to the next phase
        grew = true;
      }

      return grew;
    }
    #endregion

  }
}
