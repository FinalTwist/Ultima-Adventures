/* Created by Hammerhand & Milva */

using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;

namespace Server.Engines.Harvest
{
	public class GoldPanning : HarvestSystem
	{
		private static GoldPanning m_System;

		public static GoldPanning System
		{
			get
			{
				if ( m_System == null )
					m_System = new GoldPanning();

				return m_System;
			}
		}

		private HarvestDefinition m_Definition;

		public HarvestDefinition Definition
		{
			get{ return m_Definition; }
		}

        private GoldPanning()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

            #region GoldPanning
            HarvestDefinition nugget = new HarvestDefinition();

			// Resource banks are every 8x8 tiles
            nugget.BankWidth = 8;
            nugget.BankHeight = 8;

			// Every bank holds from 5 to 10 nuggets
            nugget.MinTotal = 5;
            nugget.MaxTotal = 10;

			// A resource bank will respawn its content every 10 to 20 minutes
            nugget.MinRespawn = TimeSpan.FromMinutes(10.0);
            nugget.MaxRespawn = TimeSpan.FromMinutes(20.0);

			// Skill checking is done on the Mining skill
            nugget.Skill = SkillName.Mining;

			// Set the list of harvestable tiles
            nugget.Tiles = m_WaterTiles;
            nugget.RangedTiles = true;

			// Players must be within 2 tiles to harvest
            nugget.MaxRange = 2;

			// One nugget per harvest action
            nugget.ConsumedPerHarvest = 1;
            nugget.ConsumedPerFeluccaHarvest = 1;

			// The panning
            nugget.EffectActions = new int[] { 32 };
            nugget.EffectSounds = new int[0];
            nugget.EffectCounts = new int[] { 1 };
            nugget.EffectDelay = TimeSpan.Zero;
            nugget.EffectSoundDelay = TimeSpan.FromSeconds(8.0);

            nugget.NoResourcesMessage = "There doesn't seem to be any nuggets left here."; // There doesn't seem to be any nuggets left here.
            nugget.FailMessage = "You pan for a while, but fail to find any nuggets."; // You pan for a while, but fail to find any nuggets.
            nugget.TimedOutOfRangeMessage = "You need to be closer to the water for panning!"; // You need to be closer to the water for panning!
            nugget.OutOfRangeMessage = "You need to be closer to the water for panning!"; // You need to be closer to the water for panning!
            nugget.PackFullMessage = "You dont have room in your pack for another nugget."; // You dont have room in your pack for another nugget.
            nugget.ToolBrokeMessage = "You wore out your gold pan."; // You wore out your gold pan.

			res = new HarvestResource[]
				{
					new HarvestResource( 75.0, 70.0, 125.0, "You put a small gold nugget in your pack.", typeof( SmallGoldNugget ) ),
                    new HarvestResource( 90.0, 75.0, 135.0, "You put a medium gold nugget in your pack.", typeof( MediumGoldNugget ) ),
                    new HarvestResource( 105.0, 90.0, 145.0, "You put a large gold nugget in your pack.", typeof( LargeGoldNugget ) )
                    
				};

			veins = new HarvestVein[]
				{
					new HarvestVein( 70.0, 0.0, res[0], null ),
                    new HarvestVein( 20.0, 0.5, res[1], res[0] ),
                    new HarvestVein( 10.0, 0.5, res[2], res[0] )
				};

            nugget.Resources = res;
            nugget.Veins = veins;

            if (Core.ML)
            {
                nugget.BonusResources = new BonusHarvestResource[]
				{
					new BonusHarvestResource( 0, 88.0, null, null ),	//Nothing
					new BonusHarvestResource( 100, 2.0, 1072562, typeof( BlueDiamond ) ),
					new BonusHarvestResource( 100, 2.0, 1072567, typeof( DarkSapphire ) ),
					new BonusHarvestResource( 100, 2.0, 1072570, typeof( EcruCitrine ) ),
					new BonusHarvestResource( 100, 2.0, 1072564, typeof( FireRuby ) ),
					new BonusHarvestResource( 100, 2.0, 1072566, typeof( PerfectEmerald ) ),
					new BonusHarvestResource( 100, 2.0, 1072568, typeof( Turquoise ) ),
					new BonusHarvestResource( 100, 2.0, 1113349, typeof( DelicateScales ) ),
					new BonusHarvestResource( 100, 2.0, 1026256, typeof( BrilliantAmber ) ),
					new BonusHarvestResource( 100, 2.0, 1032694, typeof( WhitePearl ) )
				};
            }

            m_Definition = nugget;
            Definitions.Add(nugget);
			#endregion
		}

		public override void OnConcurrentHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
            from.SendMessage("You are already panning."); // You are already panning.
		}

		private static Map SafeMap( Map map )
		{
			if ( map == null || map == Map.Internal )
				return Map.Trammel;

			return map;
		}

		public override void OnHarvestStarted( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			base.OnHarvestStarted( from, tool, def, toHarvest );

			int tileID;
			Map map;
			Point3D loc;

			if ( GetHarvestDetails( from, tool, toHarvest, out tileID, out map, out loc ) )
				Timer.DelayCall( TimeSpan.FromSeconds( 1.5 ), 
					delegate
					{
						if( Core.ML )
							from.RevealingAction();

						Effects.SendLocationEffect( loc, map, 0x352D, 16, 4 );
						Effects.PlaySound( loc, map, 0x364 );
					} );
		}

		public override void OnHarvestFinished( Mobile from, Item tool, HarvestDefinition def, HarvestVein vein, HarvestBank bank, HarvestResource resource, object harvested )
		{
			base.OnHarvestFinished( from, tool, def, vein, bank, resource, harvested );

			if ( Core.ML )
				from.RevealingAction();
		}

		public override object GetLock( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			return this;
		}

		public override bool BeginHarvesting( Mobile from, Item tool )
		{
			if ( !base.BeginHarvesting( from, tool ) )
				return false;

			from.SendMessage( "What water do you want to pan in?" ); // What water do you want to pan in?
			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) )
				return false;

			if ( from.Mounted )
			{
                from.SendMessage("You can't pan for gold while riding!"); // You can't pan for gold while riding!
				return false;
			}

			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) )
				return false;

			if ( from.Mounted )
			{
                from.SendMessage("You can't pan for gold while riding!"); // You can't pan for gold while riding!
				return false;
			}

			return true;
		}

		private static int[] m_WaterTiles = new int[]
			{
				0x00A8, 0x00AB,
				0x0136, 0x0137,
				0x5797, 0x579C,
				0x746E, 0x7485,
				0x7490, 0x74AB,
				0x74B5, 0x75D5
			};
	}
}