//#define CUSTOM_TREE_GRAPHICS

using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;
using UltimaLive;
using Server.Engines.Harvest;
using Server.Misc;


namespace UltimaLive.LumberHarvest
{
  #region Small Tree Base Classes
  public abstract class SmallTree : LeafHandlingTreePhase
  {
    public virtual int[] SmallTreeTrunkGraphics{ get { return new int[] { }; } }
    public virtual int[] SmallTreeLeafGraphics { get { return new int[] { }; } }
    public override Type NextHarvestPhase { get { return typeof(SmallFallenTreeEastWest); } }
    public override Type StartingGrowthPhase { get { return typeof(SmallSaplingTreePhase1); } }
    public override Type FinalHarvestPhase { get { return typeof(SmallFallenTreeEastWest); } }
    public SmallTree() : base ()
    {
      #region Full Grown Tree Phase
      //This is a simple tree type, so there is only main Tree phase
/*
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(350, new HarvestResource(65.0, 25.0, 105.0, 1072541, typeof(OakLog)));
      PhaseResources.Add(751, new HarvestResource(80.0, 40.0, 120.0, 1072542, typeof(AshLog)));
      PhaseResources.Add(545, new HarvestResource(95.0, 55.0, 135.0, 1072543, typeof(YewLog)));
      PhaseResources.Add(436, new HarvestResource(100.0, 60.0, 140.0, 1072544, typeof(HeartwoodLog)));
      PhaseResources.Add(339, new HarvestResource(100.0, 60.0, 140.0, 1072545, typeof(BloodwoodLog)));
      PhaseResources.Add(688, new HarvestResource(100.0, 60.0, 140.0, 1072546, typeof(FrostwoodLog)));
*/
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));

      //this tree has two sets of leafs that are possible
      foreach (int treeTrunkId in SmallTreeTrunkGraphics)
      {
        BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(treeTrunkId, 0, 0) });
      }

      foreach (int leafId in SmallTreeLeafGraphics)
      {
        LeafSets.Add(new GraphicAsset[] { new GraphicAsset(leafId, 0, 0) });
      }
      #endregion
    }
  }

  public class SmallSaplingTreePhase1 : BaseTreeHarvestPhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new SmallSaplingTreePhase1()); }
    public override Type NextGrowthPhase { get { return typeof(SmallSaplingTreePhase2); } }
    public override Type StartingGrowthPhase { get { return typeof(SmallSaplingTreePhase1); } }

    public override void RecordTreeLocationAndGraphic(int mapId, int itemId, Point3D trunkLocation)
    {
      //don't record it
    }

    public SmallSaplingTreePhase1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0xCEA, 0, 0) });
    }
  }

  public class SmallSaplingTreePhase2 : BaseTreeHarvestPhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new SmallSaplingTreePhase2()); }
    public override Type StartingGrowthPhase { get { return typeof(SmallSaplingTreePhase1); } }

    public SmallSaplingTreePhase2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0xCE9, 0, 0) });
    }

    public override void RecordTreeLocationAndGraphic(int mapId, int itemId, Point3D trunkLocation)
    {
      //don't record it, if it gets harvested, we want to leave the original location that was written by the full grown tree
    }

    public override bool grow(Point3D originLocation, Map map, ref MapOperationSeries series)
    {
      bool grew = false;

      if (UltimaLiveLumberjacking.RegrowthMasterLookupTable.ContainsKey(map.MapID))
      {
        Dictionary<Point3D, int> mapRegrowthLookupTable = UltimaLiveLumberjacking.RegrowthMasterLookupTable[map.MapID];

        //if we can't find the original lookup, then we don't grow
        if (mapRegrowthLookupTable.ContainsKey(originLocation))
        {
          Teardown(originLocation, map, ref series);
          int originalItemId = mapRegrowthLookupTable[originLocation];
          MasterHarvestablePhaseLookupByItemIdList[originalItemId].Construct(originLocation, map);
        }
      }

      return grew;
    }
  }

  public class SmallFallenTreeEastWest : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new SmallFallenTreeEastWest()); }

    public SmallFallenTreeEastWest()
    {

      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));
/*
      HarvestResource logResource       = new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log));
      HarvestResource ashResource       = new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) );
      HarvestResource cherryResource       = new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) );
      HarvestResource ebonyResource       = new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) );
      HarvestResource goldenoakResource = new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) );
      HarvestResource hickoryResource = new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) );
      HarvestResource mahoganyResource = new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) );
      HarvestResource oakResource       = new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) );
      HarvestResource pineResource = new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) );
      HarvestResource rosewoodResource = new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) );
      HarvestResource walnutResource = new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) );
      HarvestResource elvenResource = new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) );

      PhaseResources.Add(0,   logResource);
      PhaseResources.Add(350, oakResource);      
      PhaseResources.Add(751, ashResource);       
      PhaseResources.Add(545, yewResource);       
      PhaseResources.Add(436, heartwoodResource); 
      PhaseResources.Add(339, bloodwoodResource);
      PhaseResources.Add(688, frostwoodResource); 
*/
      GraphicAsset asset1 = new GraphicAsset(0xCF5, -3, 0);
      asset1.HarvestResourceBaseAmount = 10;

      GraphicAsset asset2 = new GraphicAsset(0xCF6, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;

      GraphicAsset asset3 = new GraphicAsset(0xCF7, -1, 0);
      asset3.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3 });
    }
  }

  public class SmallFallenTreeNorthSouth : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new SmallFallenTreeNorthSouth()); }

    public SmallFallenTreeNorthSouth()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));

      GraphicAsset asset1 = new GraphicAsset(0xCF4, 0, -1);
      asset1.HarvestResourceBaseAmount = 10;

      GraphicAsset asset2 = new GraphicAsset(0xCF3, 0, -2);
      asset2.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2 });
    }
  }
  #endregion

  #region Leafless Trees
  public class LeafLessTree3 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 0xCCC }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { }; } }
    public static void Configure() { RegisterHarvestablePhase(new LeafLessTree3()); }
    public override Type StartingGrowthPhase { get { return typeof(LeafLessTree3); } }

  }

  public class LeafLessTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 0xCCA }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { }; } }
    public static void Configure() { RegisterHarvestablePhase(new LeafLessTree()); }
    public override Type StartingGrowthPhase { get { return typeof(LeafLessTree); } }

  }

  public class LeafLessTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 0xCCB }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { }; } }
    public static void Configure() { RegisterHarvestablePhase(new LeafLessTree2()); }
    public override Type StartingGrowthPhase { get { return typeof(LeafLessTree2); } }
  }
  #endregion

  #region OhiiTree
  public class OhiiTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 0xC9E }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { }; } }
    public static void Configure() { RegisterHarvestablePhase(new OhiiTree()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C7E; } } // bad
    public override Type NextHarvestPhase { get { return typeof(FallenOhiiTree); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOhiiTree); } }
    public override Type StartingGrowthPhase { get { return typeof(OhiiSapling1); } }
#endif
  }

  public class OhiiSapling1 : BaseTreeHarvestPhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new OhiiSapling1()); }
    public override Type NextGrowthPhase { get { return typeof(OhiiSapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(OhiiSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOhiiTree); } }

    public OhiiSapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C7F, 0, 0) }); // bad
    }
  }

  public class OhiiSapling2 : BaseTreeHarvestPhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new OhiiSapling2()); }
    public override Type NextGrowthPhase { get { return typeof(OhiiTree); } }
    public override Type StartingGrowthPhase { get { return typeof(OhiiSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOhiiTree); } }

    public OhiiSapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C80, 0, 0) }); // bad
    }
  }

  public class FallenOhiiTree : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenOhiiTree()); }

    public FallenOhiiTree()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3C83, -1, 0);// bad
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3C84, -2, 0);// bad
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3C85, -3, 0);// bad
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3C86, -4, 0);// bad
      asset4.HarvestResourceBaseAmount = 10;

      GraphicAsset asset5 = new GraphicAsset(0x3C87, -4, -1);// bad
      asset5.HarvestResourceBaseAmount = 10;
      GraphicAsset asset6 = new GraphicAsset(0x3C88, -3, -1);// bad
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3C89, -2, -1);// bad
      asset7.HarvestResourceBaseAmount = 10;

      GraphicAsset asset8 = new GraphicAsset(0x3C8A, -4, 1);// bad
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C8B, -3, 1);// bad
      asset9.HarvestResourceBaseAmount = 10;
      GraphicAsset asset10 = new GraphicAsset(0x3C8C, -2, 1);// bad
      asset10.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10 });
    }
  }
  #endregion

  #region Generic Tree 1
  public class GenericTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 0xCCD }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 0xCCE, 0xCCF }; } }
    public static void Configure() { RegisterHarvestablePhase(new GenericTree()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C09; } }// bad
    public override Type NextHarvestPhase { get { return typeof(FallenGenericTree); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTreeSapling); } }
#endif
  }

  public class GenericTreeSapling : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new GenericTreeSapling()); }
    public override Type NextGrowthPhase { get { return typeof(GenericTreeSapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTreeSapling); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree); } }

    public GenericTreeSapling()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C0A, 0, 0) });// bad
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C0B, 0, 0) });   // bad   
    }
  }

  public class GenericTreeSapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new GenericTreeSapling2()); }
    public override Type NextGrowthPhase { get { return typeof(GenericTree); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTreeSapling); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree); } }

    public GenericTreeSapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C0C, 0, 0) });// bad
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C0D, 0, 0) });// bad
    }
  }

  public class FallenGenericTree : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenGenericTree()); }

    public FallenGenericTree()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3C0E, -1, 0);// bad
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3C0F, -2, 0);// bad
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3C10, -3, 0);// bad
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3C11, -4, 0);// bad
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3C12, -5, 0);// bad
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3C13, -5, -1);// bad
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3C14, -4, -1);// bad
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3C15, -3, -1);// bad
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C16, -2, -1);// bad
      asset9.HarvestResourceBaseAmount = 10;


      GraphicAsset asset10 = new GraphicAsset(0x3C17, -3, -3);// bad
      asset10.HarvestResourceBaseAmount = 10;

      GraphicAsset asset11 = new GraphicAsset(0x3C18, -4, 1);// bad
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3C19, -3, 1);// bad
      asset12.HarvestResourceBaseAmount = 10;
      GraphicAsset asset13 = new GraphicAsset(0x3C1A, -2, 1);// bad
      asset13.HarvestResourceBaseAmount = 10;

      GraphicAsset asset14 = new GraphicAsset(0x3C1B, -3, 2);// bad
      asset14.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13, asset14 });
    }
  }
  #endregion

  #region Generic Tree 2
  public class GenericTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3280 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3281, 3282}; }}
    public static void Configure() { RegisterHarvestablePhase(new GenericTree2()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C1C; } }// bad
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree2); } }
    public override Type NextHarvestPhase { get { return typeof(FallenGenericTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTree2Sapling1); } }
#endif
  }

  public class GenericTree2Sapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new GenericTree2Sapling1()); }
    public override Type NextGrowthPhase { get { return typeof(GenericTree2Sapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree2); } }

    public GenericTree2Sapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C1D, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C1E, 0, 0) });
    }
  }

  public class GenericTree2Sapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new GenericTree2Sapling2()); }
    public override Type NextGrowthPhase { get { return typeof(GenericTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree2); } }

    public GenericTree2Sapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C1F, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C20, 0, 0) });
    }
  }

  public class FallenGenericTree2 : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenGenericTree2()); }

    public FallenGenericTree2()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3C21, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3C22, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3C23, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3C24, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3C25, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3C26, -5, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3C27, -4, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3C28, -3, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C29, -2, -1);
      asset9.HarvestResourceBaseAmount = 10;


      GraphicAsset asset10 = new GraphicAsset(0x3C2A, -5, 1);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3C2B, -4, 1);
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3C2C, -3, 1);
      asset12.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12 });
    }
  }
  #endregion

  #region GenericTree 3
  public class GenericTree3 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[]  { 3283 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3284, 3285 }; }}
    public static void Configure() { RegisterHarvestablePhase(new GenericTree3()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C2D; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree3); } }
    public override Type NextHarvestPhase { get { return typeof(FallenGenericTree3); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTree3Sapling1); } }
#endif
  }

  public class GenericTree3Sapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new GenericTree3Sapling1()); }
    public override Type NextGrowthPhase { get { return typeof(GenericTree3Sapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTree3Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree3); } }

    public GenericTree3Sapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C2E, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C2F, 0, 0) });
    }
  }

  public class GenericTree3Sapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new GenericTree3Sapling2()); }
    public override Type NextGrowthPhase { get { return typeof(GenericTree3); } }
    public override Type StartingGrowthPhase { get { return typeof(GenericTree3Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenGenericTree3); } }

    public GenericTree3Sapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C30, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C31, 0, 0) });
    }
  }

  public class FallenGenericTree3 : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenGenericTree3()); }

    public FallenGenericTree3()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3C32, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3C33, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3C34, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3C35, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3C36, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3C37, -5, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3C38, -4, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3C39, -3, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C3A, -2, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3C3B, -4, -2);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3C3C, -3, -2);
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3C3D, -2, -2);
      asset12.HarvestResourceBaseAmount = 10;

      GraphicAsset asset13 = new GraphicAsset(0x3C3E, -5, 1);
      asset13.HarvestResourceBaseAmount = 10;
      GraphicAsset asset14 = new GraphicAsset(0x3C3F, -4, 1);
      asset14.HarvestResourceBaseAmount = 10;
      GraphicAsset asset15 = new GraphicAsset(0x3C40, -3, 1);
      asset15.HarvestResourceBaseAmount = 10;
      GraphicAsset asset16 = new GraphicAsset(0x3C41, -2, 1);
      asset16.HarvestResourceBaseAmount = 10;

      GraphicAsset asset17 = new GraphicAsset(0x3C42, -2, 2);
      asset17.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13, asset14, asset15, asset16, asset17 });
    }
  }
  #endregion

  #region Cedar Tree
  public class CedarTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3286 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3287 }; }}
    public static void Configure() { RegisterHarvestablePhase(new CedarTree()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3BE7; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCedarTree); } }
    public override Type NextHarvestPhase { get { return typeof(FallenCedarTree); } }
    public override Type StartingGrowthPhase { get { return typeof(CedarTreeSapling1); } }
#endif
  }

  public class CedarTreeSapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CedarTreeSapling1()); }
    public override Type NextGrowthPhase { get { return typeof(CedarTreeSapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(CedarTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCedarTree); } }

    public CedarTreeSapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BE8, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BE9, 0, 0) });
    }
  }

  public class CedarTreeSapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CedarTreeSapling2()); }
    public override Type NextGrowthPhase { get { return typeof(CedarTree); } }
    public override Type StartingGrowthPhase { get { return typeof(CedarTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCedarTree); } }

    public CedarTreeSapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BEA, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BEB, 0, 0) });
    }
  }

  public class FallenCedarTree : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenCedarTree()); }

    public FallenCedarTree()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3BEC, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3BED, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3BEE, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3BEF, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3BF0, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;
      GraphicAsset asset6 = new GraphicAsset(0x3BF1, -6, 0);
      asset6.HarvestResourceBaseAmount = 10;

      GraphicAsset asset7 = new GraphicAsset(0x3BF2, -4, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3BF3, -3, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3BF4, -2, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3BF5, -3, 1);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3BF6, -2, 1);
      asset11.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11 });
    }
  }
  #endregion

  #region Cedar Tree 2
  public class CedarTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3288 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3289 }; }}
    public static void Configure() { RegisterHarvestablePhase(new CedarTree2()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3BF7; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCedarTree2); } }
    public override Type NextHarvestPhase { get { return typeof(FallenCedarTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(CedarTree2Sapling1); } }
#endif
  }

  public class CedarTree2Sapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CedarTree2Sapling1()); }
    public override Type NextGrowthPhase { get { return typeof(CedarTree2Sapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(CedarTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCedarTree2); } }

    public CedarTree2Sapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BF8, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BF9, 0, 0) });
    }
  }

  public class CedarTree2Sapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CedarTree2Sapling2()); }
    public override Type NextGrowthPhase { get { return typeof(CedarTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(CedarTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCedarTree2); } }

    public CedarTree2Sapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BFA, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BFB, 0, 0) });
    }
  }

  public class FallenCedarTree2 : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenCedarTree2()); }

    public FallenCedarTree2()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3BFC, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3BFD, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3BFE, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3BFF, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3C00, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;
      GraphicAsset asset6 = new GraphicAsset(0x3C01, -6, 0);
      asset6.HarvestResourceBaseAmount = 10;

      GraphicAsset asset7 = new GraphicAsset(0x3C02, -4, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3C03, -3, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C04, -2, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3C05, -3, 1);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3C06, -2, 1);
      asset11.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11 });
    }
  }
  #endregion

  #region Oak Tree 1
  public class OakTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3290 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3291, 3292 }; }}
    public static void Configure() { RegisterHarvestablePhase(new OakTree()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3BBD; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOakTree); } }
    public override Type NextHarvestPhase { get { return typeof(FallenOakTree); } }
    public override Type StartingGrowthPhase { get { return typeof(OakTreeSapling1); } }
#endif
  }

  public class OakTreeSapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new OakTreeSapling1()); }
    public override Type NextGrowthPhase { get { return typeof(OakTreeSapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(OakTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOakTree); } }

    public OakTreeSapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BBE, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BBF, 0, 0) });
    }
  }

  public class OakTreeSapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new OakTreeSapling2()); }
    public override Type NextGrowthPhase { get { return typeof(OakTree); } }
    public override Type StartingGrowthPhase { get { return typeof(OakTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOakTree); } }

    public OakTreeSapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BC0, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BC1, 0, 0) });
    }
  }

  public class FallenOakTree : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenOakTree()); }

    public FallenOakTree()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3BC2, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;

      GraphicAsset asset2 = new GraphicAsset(0x3BC3, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;

      GraphicAsset asset3 = new GraphicAsset(0x3BC4, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;

      GraphicAsset asset4 = new GraphicAsset(0x3BC5, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;

      GraphicAsset asset5 = new GraphicAsset(0x3BC6, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3BC7, -5, -1);
      asset6.HarvestResourceBaseAmount = 10;

      GraphicAsset asset7 = new GraphicAsset(0x3BC8, -4, -1);
      asset7.HarvestResourceBaseAmount = 10;

      GraphicAsset asset8 = new GraphicAsset(0x3BC9, -3, -1);
      asset8.HarvestResourceBaseAmount = 10;

      GraphicAsset asset9 = new GraphicAsset(0x3BCA, -2, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3BCB, -3, -2);
      asset10.HarvestResourceBaseAmount = 10;

      GraphicAsset asset11 = new GraphicAsset(0x3BCC, -5, 1);
      asset11.HarvestResourceBaseAmount = 10;

      GraphicAsset asset12 = new GraphicAsset(0x3BCD, -4, 1);
      asset12.HarvestResourceBaseAmount = 10;

      GraphicAsset asset13 = new GraphicAsset(0x3BCE, -3, 1);
      asset13.HarvestResourceBaseAmount = 10;

      GraphicAsset asset14 = new GraphicAsset(0x3BCF, -2, 1);
      asset14.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13, asset14 });
    }
  }
  #endregion

  #region Oak Tree 2
  public class OakTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 0x0CDD }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3294, 3295 }; } }
    public static void Configure() { RegisterHarvestablePhase(new OakTree2()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3BD0; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOakTree2); } }
    public override Type NextHarvestPhase { get { return typeof(FallenOakTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(OakTree2Sapling1); } }
#endif
  }

  public class OakTree2Sapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new OakTree2Sapling1()); }
    public override Type NextGrowthPhase { get { return typeof(OakTree2Sapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(OakTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOakTree2); } }

    public OakTree2Sapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BD1, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BD2, 0, 0) });
    }
  }

  public class OakTree2Sapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new OakTree2Sapling2()); }
    public override Type NextGrowthPhase { get { return typeof(OakTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(OakTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenOakTree2); } }

    public OakTree2Sapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BD3, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3BD4, 0, 0) });
    }
  }

  public class FallenOakTree2 : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenOakTree2()); }

    public FallenOakTree2()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3BD5, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3BD6, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3BD7, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3BD8, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;

      GraphicAsset asset5 = new GraphicAsset(0x3BD9, -4, -1);
      asset5.HarvestResourceBaseAmount = 10;
      GraphicAsset asset6 = new GraphicAsset(0x3BDA, -3, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3BDB, -2, -1);
      asset7.HarvestResourceBaseAmount = 10;

      GraphicAsset asset8 = new GraphicAsset(0x3BDC, -4, -2);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3BDD, -3, -2);
      asset9.HarvestResourceBaseAmount = 10;
      GraphicAsset asset10 = new GraphicAsset(0x3BDE, -2, -2);
      asset10.HarvestResourceBaseAmount = 10;

      GraphicAsset asset11 = new GraphicAsset(0x3BDF, -1, 1);
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3BE0, -2, 1);
      asset12.HarvestResourceBaseAmount = 10;
      GraphicAsset asset13 = new GraphicAsset(0x3BE1, -3, 1);
      asset13.HarvestResourceBaseAmount = 10;
      GraphicAsset asset14 = new GraphicAsset(0x3BE2, -4, 1);
      asset14.HarvestResourceBaseAmount = 10;

      GraphicAsset asset15 = new GraphicAsset(0x3BE3, -4, 2);
      asset15.HarvestResourceBaseAmount = 10;
      GraphicAsset asset16 = new GraphicAsset(0x3BE4, -3, 2);
      asset16.HarvestResourceBaseAmount = 10;
      GraphicAsset asset17 = new GraphicAsset(0x3BE5, -2, 2);
      asset17.HarvestResourceBaseAmount = 10;
      GraphicAsset asset18 = new GraphicAsset(0x3BE6, -1, 2);
      asset18.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13, asset14, asset15, asset16, asset17, asset18  });
    }
  }
  #endregion

  #region Walnut Tree
  public class WalnutTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3296 }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3297, 3298 }; } }
    public static void Configure() { RegisterHarvestablePhase(new WalnutTree()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C43; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWalnutTree); } }
    public override Type NextHarvestPhase { get { return typeof(FallenWalnutTree); } }
    public override Type StartingGrowthPhase { get { return typeof(WalnutTreeSapling1); } }
#endif
  }

  public class WalnutTreeSapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new WalnutTreeSapling1()); }
    public override Type NextGrowthPhase { get { return typeof(WalnutTreeSapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(WalnutTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWalnutTree); } }

    public WalnutTreeSapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C44, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C45, 0, 0) });
    }
  }

  public class WalnutTreeSapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new WalnutTreeSapling2()); }
    public override Type NextGrowthPhase { get { return typeof(WalnutTree); } }
    public override Type StartingGrowthPhase { get { return typeof(WalnutTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWalnutTree); } }

    public WalnutTreeSapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C46, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C47, 0, 0) });
    }
  }

  public class FallenWalnutTree : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenWalnutTree()); }

    public FallenWalnutTree()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3C48, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3C49, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3C4A, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3C4B, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3C4C, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;
      GraphicAsset asset6 = new GraphicAsset(0x3C4D, -6, 0);
      asset6.HarvestResourceBaseAmount = 10;

      GraphicAsset asset7 = new GraphicAsset(0x3C4E, -5, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3C4F, -4, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C50, -3, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3C51, -5, 1);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3C52, -4, 1);
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3C53, -3, 1);
      asset12.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12 });
    }
  }
  #endregion

  #region Walnut Tree 2
  public class WalnutTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3299 }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3300, 3301 }; } }
    public static void Configure() { RegisterHarvestablePhase(new WalnutTree2()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C54; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWalnutTree2); } }
    public override Type NextHarvestPhase { get { return typeof(FallenWalnutTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(WalnutTree2Sapling1); } }
#endif
  }

  public class WalnutTree2Sapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new WalnutTree2Sapling1()); }
    public override Type NextGrowthPhase { get { return typeof(WalnutTree2Sapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(WalnutTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWalnutTree2); } }

    public WalnutTree2Sapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C55, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C56, 0, 0) });
    }
  }

  public class WalnutTree2Sapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new WalnutTree2Sapling2()); }
    public override Type NextGrowthPhase { get { return typeof(WalnutTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(WalnutTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWalnutTree2); } }

    public WalnutTree2Sapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C57, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C58, 0, 0) });
    }
  }

  public class FallenWalnutTree2 : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenWalnutTree2()); }

    public FallenWalnutTree2()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3C59, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3C5A, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3C5B, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3C5C, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3C5D, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3C5E, -5, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3C5F, -4, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3C60, -3, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C61, -2, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3C62, -5, -2);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3C63, -4, -2);
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3C64, -3, -2);
      asset12.HarvestResourceBaseAmount = 10;
      GraphicAsset asset13 = new GraphicAsset(0x3C65, -2, -2);
      asset13.HarvestResourceBaseAmount = 10;

      GraphicAsset asset14 = new GraphicAsset(0x3C66, -5, 1);
      asset14.HarvestResourceBaseAmount = 10;
      GraphicAsset asset15 = new GraphicAsset(0x3C67, -4, 1);
      asset15.HarvestResourceBaseAmount = 10;
      GraphicAsset asset16 = new GraphicAsset(0x3C68, -3, 1);
      asset16.HarvestResourceBaseAmount = 10;
      GraphicAsset asset17 = new GraphicAsset(0x3C69, -2, 1);
      asset17.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13, asset14, asset15, asset16, asset17 });
    }
  }
  #endregion

  #region Willow Tree
  public class WillowTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3302 }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3303, 3304 }; }}
    public static void Configure() { RegisterHarvestablePhase(new WillowTree()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C6A; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWillowTree); } }
    public override Type NextHarvestPhase { get { return typeof(FallenWillowTree); } }
    public override Type StartingGrowthPhase { get { return typeof(WillowTreeSapling1); } }
#endif
  }

  public class WillowTreeSapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new WillowTreeSapling1()); }
    public override Type NextGrowthPhase { get { return typeof(WillowTreeSapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(WillowTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWillowTree); } }

    public WillowTreeSapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C6B, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C6C, 0, 0) });
    }
  }

  public class WillowTreeSapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new WillowTreeSapling2()); }
    public override Type NextGrowthPhase { get { return typeof(WillowTree); } }
    public override Type StartingGrowthPhase { get { return typeof(WillowTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenWillowTree); } }

    public WillowTreeSapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C6D, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C6E, 0, 0) });
    }
  }

  public class FallenWillowTree : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenWillowTree()); }

    public FallenWillowTree()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3C6F, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3C70, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3C71, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3C72, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3C73, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3C74, -4, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3C75, -3, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3C76, -2, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C77, -1, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3C78, -4, -2);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3C79, -3, -2);
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3C7A, -2, -2);
      asset12.HarvestResourceBaseAmount = 10;

      GraphicAsset asset13 = new GraphicAsset(0x3C7B, -4, 1);
      asset13.HarvestResourceBaseAmount = 10;
      GraphicAsset asset14 = new GraphicAsset(0x3C7C, -3, 1);
      asset14.HarvestResourceBaseAmount = 10;
      GraphicAsset asset15 = new GraphicAsset(0x3C7D, -2, 1);
      asset15.HarvestResourceBaseAmount = 10;


      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13, asset14, asset15 });
    }
  }
  #endregion

  #region Cypress Tree 1
  public class CypressTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3320 }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3321, 3322 }; }}
    public static void Configure() { RegisterHarvestablePhase(new CypressTree()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C8D; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree); } }
    public override Type NextHarvestPhase { get { return typeof(FallenCypressTree); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTreeSapling1); } }
#endif
  }

  public class CypressTreeSapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CypressTreeSapling1()); }
    public override Type NextGrowthPhase { get { return typeof(CypressTreeSapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree); } }

    public CypressTreeSapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C8E, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C8F, 0, 0) });
    }
  }

  public class CypressTreeSapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CypressTreeSapling2()); }
    public override Type NextGrowthPhase { get { return typeof(CypressTree); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTreeSapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree); } }

    public CypressTreeSapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C90, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3C91, 0, 0) });
    }
  }

  public class FallenCypressTree : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenCypressTree()); }

    public FallenCypressTree()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3C92, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3C93, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3C94, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3C95, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3C96, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3C97, -5, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3C98, -4, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3C99, -3, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3C9A, -2, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3C9B, -2, -2);
      asset10.HarvestResourceBaseAmount = 10;

      GraphicAsset asset11 = new GraphicAsset(0x3C9C, -4, 1);
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3C9D, -3, 1);
      asset12.HarvestResourceBaseAmount = 10;
      GraphicAsset asset13 = new GraphicAsset(0x3C9E, -2, 1);
      asset13.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13 });
    }
  }
  #endregion

  #region Cypress Tree 2
  public class CypressTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3323 }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3324, 3325 }; }}
    public static void Configure() { RegisterHarvestablePhase(new CypressTree2()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3C9F; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree2); } }
    public override Type NextHarvestPhase { get { return typeof(FallenCypressTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree2Sapling1); } }
#endif
  }

  public class CypressTree2Sapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CypressTree2Sapling1()); }
    public override Type NextGrowthPhase { get { return typeof(CypressTree2Sapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree2); } }

    public CypressTree2Sapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CA0, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CA1, 0, 0) });
    }
  }

  public class CypressTree2Sapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CypressTree2Sapling2()); }
    public override Type NextGrowthPhase { get { return typeof(CypressTree2); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree2Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree2); } }

    public CypressTree2Sapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CA2, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CA3, 0, 0) });
    }
  }

  public class FallenCypressTree2 : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenCypressTree2()); }

    public FallenCypressTree2()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3CA4, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3CA5, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3CA6, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3CA7, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3CA8, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3CA9, -4, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3CAA, -3, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3CAB, -2, -1);
      asset8.HarvestResourceBaseAmount = 10;

      GraphicAsset asset9 = new GraphicAsset(0x3CAC, -2, -2);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3CAD, -4, 1);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3CAE, -3, 1);
      asset11.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11 });
    }
  }
  #endregion

  #region Cypress Tree 3
  public class CypressTree3 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3326 }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3327, 3328 }; }}
    public static void Configure() { RegisterHarvestablePhase(new CypressTree3()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3CAF; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree3); } }
    public override Type NextHarvestPhase { get { return typeof(FallenCypressTree3); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree3Sapling1); } }
#endif
  }

  public class CypressTree3Sapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CypressTree3Sapling1()); }
    public override Type NextGrowthPhase { get { return typeof(CypressTree3Sapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree3Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree3); } }

    public CypressTree3Sapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CB0, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CB1, 0, 0) });
    }
  }

  public class CypressTree3Sapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CypressTree3Sapling2()); }
    public override Type NextGrowthPhase { get { return typeof(CypressTree3); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree3Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree3); } }

    public CypressTree3Sapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CB2, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CB3, 0, 0) });
    }
  }

  public class FallenCypressTree3 : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenCypressTree3()); }

    public FallenCypressTree3()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3CB4, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3CB5, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3CB6, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3CB7, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;

      GraphicAsset asset5 = new GraphicAsset(0x3CB8, -5, -1);
      asset5.HarvestResourceBaseAmount = 10;
      GraphicAsset asset6 = new GraphicAsset(0x3CB9, -4, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3CBA, -3, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3CBB, -2, -1);
      asset8.HarvestResourceBaseAmount = 10;

      GraphicAsset asset9 = new GraphicAsset(0x3CBC, -4, -2);
      asset9.HarvestResourceBaseAmount = 10;
      GraphicAsset asset10 = new GraphicAsset(0x3CBD, -3, -2);
      asset10.HarvestResourceBaseAmount = 10;
      GraphicAsset asset11 = new GraphicAsset(0x3CBE, -2, -2);
      asset11.HarvestResourceBaseAmount = 10;

      GraphicAsset asset12 = new GraphicAsset(0x3CBF, -4, 1);
      asset12.HarvestResourceBaseAmount = 10;
      GraphicAsset asset13 = new GraphicAsset(0x3CC0, -3, 1);
      asset13.HarvestResourceBaseAmount = 10;
      GraphicAsset asset14 = new GraphicAsset(0x3CC1, -2, 1);
      asset14.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13, asset14 });
    }
  }
  #endregion

  #region Cypress Tree 4
  public class CypressTree4 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3329 }; } }
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3330, 3331 }; }}
    public static void Configure() { RegisterHarvestablePhase(new CypressTree4()); }
#if (CUSTOM_TREE_GRAPHICS)
    public override int StumpGraphic { get { return 0x3CC2; } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree4); } }
    public override Type NextHarvestPhase { get { return typeof(FallenCypressTree4); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree4Sapling1); } }
#endif
  }

  public class CypressTree4Sapling1 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CypressTree4Sapling1()); }
    public override Type NextGrowthPhase { get { return typeof(CypressTree4Sapling2); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree4Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree4); } }

    public CypressTree4Sapling1()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CC3, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CC4, 0, 0) });
    }
  }

  public class CypressTree4Sapling2 : LeafHandlingTreePhase
  {
    public override bool AddStump { get { return false; } }
    public static void Configure() { RegisterHarvestablePhase(new CypressTree4Sapling2()); }
    public override Type NextGrowthPhase { get { return typeof(CypressTree4); } }
    public override Type StartingGrowthPhase { get { return typeof(CypressTree4Sapling1); } }
    public override Type FinalHarvestPhase { get { return typeof(FallenCypressTree4); } }

    public CypressTree4Sapling2()
      : base()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CC5, 0, 0) });
      LeafSets.Add(new GraphicAsset[] { new GraphicAsset(0x3CC6, 0, 0) });
    }
  }

  public class FallenCypressTree4 : BaseTreeHarvestPhase
  {
    public static void Configure() { RegisterHarvestablePhase(new FallenCypressTree4()); }

    public FallenCypressTree4()
    {
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ash", "", 0 ), new HarvestResource(55.0, 25.0, 90.0, "", typeof( AshLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "cherry", "", 0 ), new HarvestResource(  60.0, 30.0, 95.0, "", typeof( CherryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "ebony", "", 0 ), new HarvestResource(  65.0, 35.0, 100.0, "", typeof( EbonyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "golden oak", "", 0 ), new HarvestResource(  70.0, 40.0, 105.0, "", typeof( GoldenOakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "hickory", "", 0 ), new HarvestResource(  75.0, 45.0, 110.0, "", typeof( HickoryLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "mahogany", "", 0 ), new HarvestResource(  80.0, 50.0, 115.0, "", typeof( MahoganyLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "oak", "", 0 ), new HarvestResource(  85.0, 55.0, 120.0, "", typeof( OakLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "pine", "", 0 ), new HarvestResource(  90.0, 65.0, 125.0, "", typeof( PineLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "rosewood", "", 0 ), new HarvestResource(  95.0, 75.0, 130.0, "", typeof( RosewoodLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "walnut", "", 0 ), new HarvestResource(  100.0, 85.0, 135.0, "", typeof( WalnutLog ) ));
      PhaseResources.Add(MaterialInfo.GetMaterialColor( "elven", "", 0 ), new HarvestResource(  100.1, 95.0, 140.0, "", typeof( ElvenLog ) ));


      GraphicAsset asset1 = new GraphicAsset(0x3CC7, -1, 0);
      asset1.HarvestResourceBaseAmount = 10;
      GraphicAsset asset2 = new GraphicAsset(0x3CC8, -2, 0);
      asset2.HarvestResourceBaseAmount = 10;
      GraphicAsset asset3 = new GraphicAsset(0x3CC9, -3, 0);
      asset3.HarvestResourceBaseAmount = 10;
      GraphicAsset asset4 = new GraphicAsset(0x3CCA, -4, 0);
      asset4.HarvestResourceBaseAmount = 10;
      GraphicAsset asset5 = new GraphicAsset(0x3CCB, -5, 0);
      asset5.HarvestResourceBaseAmount = 10;

      GraphicAsset asset6 = new GraphicAsset(0x3CCC, -5, -1);
      asset6.HarvestResourceBaseAmount = 10;
      GraphicAsset asset7 = new GraphicAsset(0x3CCD, -4, -1);
      asset7.HarvestResourceBaseAmount = 10;
      GraphicAsset asset8 = new GraphicAsset(0x3CCE, -3, -1);
      asset8.HarvestResourceBaseAmount = 10;
      GraphicAsset asset9 = new GraphicAsset(0x3CCF, -2, -1);
      asset9.HarvestResourceBaseAmount = 10;

      GraphicAsset asset10 = new GraphicAsset(0x3CD0, -3, -2);
      asset10.HarvestResourceBaseAmount = 10;

      GraphicAsset asset11 = new GraphicAsset(0x3CD1, -5, 1);
      asset11.HarvestResourceBaseAmount = 10;
      GraphicAsset asset12 = new GraphicAsset(0x3CD2, -4, 1);
      asset12.HarvestResourceBaseAmount = 10;
      GraphicAsset asset13 = new GraphicAsset(0x3CD3, -3, 1);
      asset13.HarvestResourceBaseAmount = 10;
      GraphicAsset asset14 = new GraphicAsset(0x3CD4, -2, 1);
      asset14.HarvestResourceBaseAmount = 10;
      GraphicAsset asset15 = new GraphicAsset(0x3CD5, -1, 1);
      asset15.HarvestResourceBaseAmount = 10;

      BaseAssetSets.Add(new GraphicAsset[] { asset1, asset2, asset3, asset4, asset5, asset6, asset7, asset8, asset9, asset10, asset11, asset12, asset13, asset14, asset15 });
    }
  }
  #endregion

  #region Yucca Trees
  public class YuccaTree : BaseTreeHarvestPhase
  {
    public override int StumpGraphic { get { return 0x0DAC; } }
    public static void Configure() { RegisterHarvestablePhase(new YuccaTree()); }

    public override Type NextHarvestPhase { get { return typeof(SmallFallenTreeEastWest); } }
    public override Type StartingGrowthPhase { get { return typeof(SmallSaplingTreePhase1); } }
    public override Type FinalHarvestPhase { get { return typeof(SmallFallenTreeEastWest); } }


    public YuccaTree()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(3383, 0, 0) });
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
    }
  }

  public class YuccaTree2 : BaseTreeHarvestPhase
  {
    public override Type StartingGrowthPhase { get { return typeof(YuccaTree2); } }
    public override int StumpGraphic { get { return 0x0DAC; } }
    public static void Configure() { RegisterHarvestablePhase(new YuccaTree2()); }

    public YuccaTree2()
    {
      BaseAssetSets.Add(new GraphicAsset[] { new GraphicAsset(3384, 0, 0) });
      PhaseResources.Add(0, new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(Log)));
    }
  }
  #endregion

  #region Tuscany Pine Tree
  public class TuscanyPineTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 7038 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { }; }}
    public override Type StartingGrowthPhase { get { return typeof(TuscanyPineTree); } }
    public static void Configure() { RegisterHarvestablePhase(new TuscanyPineTree()); }
  }
  #endregion

  #region Fruit Trees
  public class AppleTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3476 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3477, 3478, 3479 }; }}
    public override Type StartingGrowthPhase { get { return typeof(AppleTree); } }
    public static void Configure() { RegisterHarvestablePhase(new AppleTree()); }
  }

  public class AppleTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3480 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3481, 3482, 3483 }; }}
    public override Type StartingGrowthPhase { get { return typeof(AppleTree2); } }
    public static void Configure() { RegisterHarvestablePhase(new AppleTree2()); }
  }

  public class PeachTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3484 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3485, 3486, 3487 }; }}
    public override Type StartingGrowthPhase { get { return typeof(PeachTree); } }
    public static void Configure() { RegisterHarvestablePhase(new PeachTree()); }
  }

  public class PeachTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3488 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3489, 3490, 3491 }; }}
    public override Type StartingGrowthPhase { get { return typeof(PeachTree2); } }
    public static void Configure() { RegisterHarvestablePhase(new PeachTree2()); }
  }

  public class PearTree : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3492 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3493, 3494, 3495}; }}
    public override Type StartingGrowthPhase { get { return typeof(PearTree); } }
    public static void Configure() { RegisterHarvestablePhase(new PearTree()); }
  }

  public class PearTree2 : SmallTree
  {
    public override int[] SmallTreeTrunkGraphics { get { return new int[] { 3496 }; }}
    public override int[] SmallTreeLeafGraphics { get { return new int[] { 3497, 3498, 3499 }; } }
    public override Type StartingGrowthPhase { get { return typeof(PearTree2); } }
    public static void Configure() { RegisterHarvestablePhase(new PearTree2()); }
  }
  #endregion
}