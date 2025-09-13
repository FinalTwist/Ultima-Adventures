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
  public class BaseTreeHarvestPhase : BaseHarvestablePhase
  {
    public const int LEAF_HUE = 0;

    public virtual int StumpGraphic { get { return 0xE57; } }
    public virtual bool AddStump { get { return true; } }

    public List<GraphicAsset[]> LeafSets = new List<GraphicAsset[]>();

    public override void RegisterBasicPhaseTiles()
    {
      base.RegisterBasicPhaseTiles();

      //Add all the leaf graphics for this phase
      foreach (GraphicAsset[] leafSet in LeafSets)
      {
        RegisterAssetSet(leafSet);
      }
    }

    public override Point3D LookupOriginLocation(Point3D harvestTargetLocation, int harvestTargetItemId)
    {
      Point3D originLocation = base.LookupOriginLocation(harvestTargetLocation, harvestTargetItemId);

      if (originLocation == Point3D.Zero)
      {
        bool found = false;
        foreach (GraphicAsset[] leafSet in LeafSets)
        {
          foreach (GraphicAsset asset in leafSet)
          {
            if (asset.ItemID == harvestTargetItemId)
            {
              originLocation = new Point3D(harvestTargetLocation.X + asset.XOffset, harvestTargetLocation.Y + asset.YOffset, harvestTargetLocation.Z - TileData.ItemTable[harvestTargetItemId].CalcHeight);
              found = true;
              break;
            }
          }
          if (found)
          {
            break;
          }
        }
      }

      return originLocation;
    }

    public override int Construct(Point3D location, Map map, ref MapOperationSeries mapOperationSeries)
    {
      int trunkHue = base.Construct(location, map, ref mapOperationSeries);

      GraphicAsset[] leafSet = null;

      if (LeafSets != null && LeafSets.Count > 0)
      {
        //leafSet = LeafSets[Utility.Random(LeafSets.Count)];
        leafSet = LeafSets[0];
      }

      if (leafSet != null)
      {
        foreach (GraphicAsset asset in leafSet)
        {
          //come back and add a usestatic boolean to this class and add it into the constructor too
          AddStatic addStatic = new AddStatic(map.MapID, asset.ItemID, location.Z, location.X + asset.XOffset, location.Y + asset.YOffset, LEAF_HUE);
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

      return trunkHue;
    }

    public override void Teardown(Point3D originLocation, Map map, ref MapOperationSeries mapActions)
    {
      base.Teardown(originLocation, map, ref mapActions);
      List<KeyValuePair<int, GraphicAsset>> leavesRemoved = RemoveAssets(map, originLocation, ref mapActions, LeafSets);
    }

    public override bool Harvest(Mobile from, int itemId, Point3D harvestTargetLocation, Map map)
    {
      bool successfulHarvest = false;
      MapOperationSeries mapActions = null;

      successfulHarvest = Harvest(from, itemId, harvestTargetLocation, map, ref mapActions);

      if (mapActions != null)
      {
        mapActions.DoOperation();
      }
      return successfulHarvest;
    }


    //This only gets called if the NextHarvestPhase is not null
    public virtual void RecordTreeLocationAndGraphic(int mapId, int itemId, Point3D trunkLocation)
    {
      if (!UltimaLiveLumberjacking.RegrowthMasterLookupTable.ContainsKey(mapId))
      {
        UltimaLiveLumberjacking.RegrowthMasterLookupTable.Add(mapId, new Dictionary<Point3D, int>());
      }

      if (!UltimaLiveLumberjacking.RegrowthMasterLookupTable[mapId].ContainsKey(trunkLocation))
      {
        UltimaLiveLumberjacking.RegrowthMasterLookupTable[mapId].Add(trunkLocation, itemId);
      }
      else
      {
      UltimaLiveLumberjacking.RegrowthMasterLookupTable[mapId][trunkLocation] = itemId;
    }
    }

    public bool Harvest(Mobile from, int itemId, Point3D harvestTargetLocation, Map map, ref MapOperationSeries operationSeries)
    {
      Point3D trunkLocation = LookupOriginLocation(harvestTargetLocation, itemId);

      List<KeyValuePair<int, GraphicAsset>> phasePiecesRemoved = null;

      //If the next phase is not null, tear it all down and construct the next phase
      if (NextHarvestPhase != null)
      {
        phasePiecesRemoved = RemoveAssets(map, trunkLocation, ref operationSeries, BaseAssetSets);
        int constructedTreeHue = 0;

        if (MasterHarvestablePhaseLookupByTypeList.ContainsKey(NextHarvestPhase))
        {
          constructedTreeHue = MasterHarvestablePhaseLookupByTypeList[NextHarvestPhase].Construct(trunkLocation, map, ref operationSeries);
        }

        if (operationSeries != null)
        {
          RecordTreeLocationAndGraphic(map.MapID, BaseAssetSets[0][0].ItemID, trunkLocation);

          if (AddStump)
          {
            operationSeries.Add(new AddStatic(map.MapID, StumpGraphic, trunkLocation.Z, trunkLocation.X, trunkLocation.Y, constructedTreeHue));
          }
        }

      }
      else //the next phase is not null, so destroy one asset at a time
      {
        GraphicAsset asset = LookupAsset(itemId);
        List<GraphicAsset[]> assetsToRemove = new List<GraphicAsset[]>();
        assetsToRemove.Add(new GraphicAsset[] { asset });
        phasePiecesRemoved = RemoveAssets(map, trunkLocation, ref operationSeries, assetsToRemove);
      }

      int hue = 0;

      //give out phase resource for each graphic asset removed
      foreach (KeyValuePair<int, GraphicAsset> assetPair in phasePiecesRemoved)
      {
        hue = assetPair.Key;

        Item itm = this.ReapResource(assetPair.Key, from, assetPair.Value.HarvestResourceBaseAmount);

        if (itm != null)
        {
          from.AddToBackpack(itm);
        }
      }

      //give out asset bonus resources
      foreach (Item itm in this.ReapBonusResources(hue, from))
      {
        from.AddToBackpack(itm);
      }

      bool returnValue = false;

      if (operationSeries != null)
      {
        returnValue = true;
      }

      return returnValue;
    }
  }
}