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
  public class LeafHandlingTreePhase : BaseTreeHarvestPhase
  {
    public const int FALLING_LEAF_HUE = 1436;

    public LeafHandlingTreePhase()
      : base()
    {
    }

    public override bool Harvest(Mobile from, int itemId, Point3D harvestTargetLocation, Map map)
    {
      Point3D trunkLocation = LookupOriginLocation(harvestTargetLocation, itemId);

      //search for leaves
      MapOperationSeries mapActions = null;
      List<KeyValuePair<int, GraphicAsset>> leavesRemoved = RemoveAssets(map, trunkLocation, ref mapActions, LeafSets);

      if (mapActions != null) 
      {
        //handle falling leaves

        int numLeaves = Utility.Random(5);
        Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(trunkLocation.X, trunkLocation.Y, trunkLocation.Z + 20), map),
                  new Entity(Serial.Zero, new Point3D(trunkLocation.X, trunkLocation.Y, trunkLocation.Z), map),
                  0x1B1F, 1, 0, false, false, FALLING_LEAF_HUE, 0, 0, 1, 0, EffectLayer.Waist, 0x486);

        for(int i = 0; i < numLeaves + 9; i++)
        {
          new FadingLeaf().MoveToWorld(new Point3D(trunkLocation.X + Utility.Random(4) - 2, trunkLocation.Y + Utility.Random(4) - 2, trunkLocation.Z), map);
          Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(trunkLocation.X + Utility.Random(4) - 2, trunkLocation.Y + Utility.Random(4) - 2, trunkLocation.Z + 20), map),
                            new Entity(Serial.Zero, new Point3D(trunkLocation.X + Utility.Random(4) - 2, trunkLocation.Y + Utility.Random(4) - 2, trunkLocation.Z), map),
                            0x1B1F, 1, 0, false, false, FALLING_LEAF_HUE, 0, 0, 1, 0, EffectLayer.Waist, 0x486);
        }

        //give out leaf bonus asset resources
        foreach (KeyValuePair<int, GraphicAsset> assetPair in leavesRemoved)
        {         
          foreach (Item itm in assetPair.Value.ReapBonusResources(assetPair.Key, from))
          {
            from.AddToBackpack(itm);
          }
        }
      }
      else //if there are no leaves left, then remove the trunk pieces and call nextPhase.construct
      {
        base.Harvest(from, itemId, harvestTargetLocation, map, ref mapActions);
      }

      if (mapActions != null)
      {
        mapActions.DoOperation();
      }

      return false;
    }
  }
}
