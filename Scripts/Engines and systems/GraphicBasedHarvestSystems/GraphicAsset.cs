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
  public class GraphicAsset
  {
    public int ItemID;
    public Int16 XOffset;
    public Int16 YOffset;
    public int HarvestResourceBaseAmount = 0;
    public int BonusResourceBaseAmount = 0;

    //Graphics are linked to bonus resources only. Regular harvest resources should be assigned at the phase level.
    public Dictionary<int, BonusHarvestResource[]> BonusResources = new Dictionary<int, BonusHarvestResource[]>();

    public GraphicAsset(int itemId, Int16 xOff, Int16 yOff)
    {
      ItemID = itemId;
      XOffset = xOff;
      YOffset = yOff;
    }

    public virtual List<Item> ReapBonusResources(int hue, Mobile from)
    {
      return ReapBonusResources(hue, from, BonusResourceBaseAmount, BonusResources);
    }

    //The bonus resource list can be specified at the phase level by using this overloaded method.
    public static List<Item> ReapBonusResources(int hue, Mobile from, int bonusResourceAmount, Dictionary<int, BonusHarvestResource[]> bonusList)
    {
      List<Item> bonusItems = new List<Item>();
      BonusHarvestResource[] bonusResourceList = null;

      if (bonusResourceAmount > 0)
      {
        if (bonusList.ContainsKey(hue))
        {
          bonusResourceList = bonusList[hue];
        }
        else if (bonusList.ContainsKey(0))
        {
          bonusResourceList = bonusList[0];
        }

        if (bonusResourceList != null && bonusResourceList.Length > 0)
        {
          double skillBase = from.Skills[SkillName.Lumberjacking].Base;

          foreach (BonusHarvestResource resource in bonusResourceList)
          {
            if (skillBase >= resource.ReqSkill && Utility.RandomDouble() <= resource.Chance)
            {
              try
              {
                Item item = Activator.CreateInstance(resource.Type) as Item;
                if (item != null)
                {
                  item.Amount = bonusResourceAmount;
                  bonusItems.Add(item);
                }
              }
              catch
              {
                Console.WriteLine("exception caught when trying to create bonus resource");
              }
            }
          }
        }
      }

      return bonusItems;
    }
  }
}
