using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Misc;
using Server.Network;

namespace Server.Engines.Harvest
{
	public class Herbalism : HarvestSystem
	{
		private static Herbalism m_System;

		public static Herbalism System
		{
			get
			{
				if ( m_System == null )
					m_System = new Herbalism();

				return m_System;
			}
		}

		private HarvestDefinition m_Flower, m_Mushroom, m_Cactus, m_Lilly, m_Leaf, m_Grass;

		public HarvestDefinition Flowers { get{ return m_Flower; } }
		public HarvestDefinition Mushroom { get{ return m_Mushroom; } }
		public HarvestDefinition Cactus { get{ return m_Cactus; } }
		public HarvestDefinition Lilly { get{ return m_Lilly; } }
		public HarvestDefinition Leaf { get{ return m_Leaf; } }
		public HarvestDefinition Grass { get{ return m_Grass; } }

		private Herbalism()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region Leaf /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			HarvestDefinition leaf = m_Leaf = new HarvestDefinition();
			leaf.BankWidth = 1;
			leaf.BankHeight = 1;
			leaf.MinTotal = 1;
			leaf.MaxTotal = 1;
			leaf.MinRespawn = TimeSpan.FromMinutes( 50.0 );
			leaf.MaxRespawn = TimeSpan.FromMinutes( 70.0 );
			leaf.Skill = SkillName.Cooking;
			leaf.Tiles = m_LeafTiles;
			leaf.MaxRange = 1;
			leaf.ConsumedPerHarvest = 1;
			leaf.ConsumedPerFeluccaHarvest = 1;
			leaf.EffectActions = new int[]{ 4 };
			leaf.EffectSounds = new int[]{ 0x248 };
			leaf.EffectCounts = new int[]{ 1 };
			leaf.EffectDelay = TimeSpan.FromSeconds( 0.0 );
			leaf.EffectSoundDelay = TimeSpan.FromSeconds( 0.1 );
			leaf.NoResourcesMessage = 1114028; // You harvest the last resources from this plant
			leaf.FailMessage = 501756; // Nothing worth taking
			leaf.OutOfRangeMessage = 500446; // That is too far away.
			leaf.PackFullMessage = 500720; // You don't have enough room in your backpack!
			leaf.ToolBrokeMessage = 1044038; // You broke your tool.

			res = new HarvestResource[]
			{
				new HarvestResource( 000.0, 000.0, 150.0, "You put an herb in your backpack",					typeof( PlantHerbalism_Leaf ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 100.0, 0.0, res[0], res[0] )
			};

			if ( Core.ML )
			{
				leaf.BonusResources = new BonusHarvestResource[] // cos this is mining after all
				{
					new BonusHarvestResource( 0, 98.0, null, null ),	//Nothing
					new BonusHarvestResource( 60, 2.0, 1074542, typeof( HomePlants_Leaf ) )
				};
			}

			leaf.RandomizeVeins = Core.ML;

			leaf.Resources = res;
			leaf.Veins = veins;

			Definitions.Add( leaf );
			#endregion

			#region Mushroom /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			HarvestDefinition mushroom = m_Mushroom = new HarvestDefinition();
			mushroom.BankWidth = 1;
			mushroom.BankHeight = 1;
			mushroom.MinTotal = 1;
			mushroom.MaxTotal = 1;
			mushroom.MinRespawn = TimeSpan.FromMinutes( 50.0 );
			mushroom.MaxRespawn = TimeSpan.FromMinutes( 70.0 );
			mushroom.Skill = SkillName.Cooking;
			mushroom.Tiles = m_MushroomTiles;
			mushroom.MaxRange = 1;
			mushroom.ConsumedPerHarvest = 1;
			mushroom.ConsumedPerFeluccaHarvest = 1;
			mushroom.EffectActions = new int[]{ 4 };
			mushroom.EffectSounds = new int[]{ 0x248 };
			mushroom.EffectCounts = new int[]{ 1 };
			mushroom.EffectDelay = TimeSpan.FromSeconds( 0.0 );
			mushroom.EffectSoundDelay = TimeSpan.FromSeconds( 0.1 );
			mushroom.NoResourcesMessage = 1114028; // You harvest the last resources from this plant
			mushroom.FailMessage = 501756; // Nothing worth taking
			mushroom.OutOfRangeMessage = 500446; // That is too far away.
			mushroom.PackFullMessage = 500720; // You don't have enough room in your backpack!
			mushroom.ToolBrokeMessage = 1044038; // You broke your tool.

			res = new HarvestResource[]
			{
				new HarvestResource( 000.0, 000.0, 150.0, "You put an herb in your backpack",					typeof( PlantHerbalism_Mushroom ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 100.0, 0.0, res[0], res[0] )
			};

			if ( Core.ML )
			{
				mushroom.BonusResources = new BonusHarvestResource[] // cos this is mining after all
				{
					new BonusHarvestResource( 0, 78.0, null, null ),	//Nothing
					new BonusHarvestResource( 60, 2.0, 1074542, typeof( HomePlants_Mushroom ) )
				};
			}

			mushroom.RandomizeVeins = Core.ML;

			mushroom.Resources = res;
			mushroom.Veins = veins;

			Definitions.Add( mushroom );
			#endregion

			#region Flower /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			HarvestDefinition flower = m_Flower = new HarvestDefinition();
			flower.BankWidth = 1;
			flower.BankHeight = 1;
			flower.MinTotal = 1;
			flower.MaxTotal = 1;
			flower.MinRespawn = TimeSpan.FromMinutes( 50.0 );
			flower.MaxRespawn = TimeSpan.FromMinutes( 70.0 );
			flower.Skill = SkillName.Cooking;
			flower.Tiles = m_FlowerTiles;
			flower.MaxRange = 1;
			flower.ConsumedPerHarvest = 1;
			flower.ConsumedPerFeluccaHarvest = 1;
			flower.EffectActions = new int[]{ 4 };
			flower.EffectSounds = new int[]{ 0x248 };
			flower.EffectCounts = new int[]{ 1 };
			flower.EffectDelay = TimeSpan.FromSeconds( 0.0 );
			flower.EffectSoundDelay = TimeSpan.FromSeconds( 0.1 );
			flower.NoResourcesMessage = 1114028; // You harvest the last resources from this plant
			flower.FailMessage = 501756; // Nothing worth taking
			flower.OutOfRangeMessage = 500446; // That is too far away.
			flower.PackFullMessage = 500720; // You don't have enough room in your backpack!
			flower.ToolBrokeMessage = 1044038; // You broke your tool.

			res = new HarvestResource[]
			{
				new HarvestResource( 000.0, 000.0, 150.0, "You put an herb in your backpack",					typeof( PlantHerbalism_Flower ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 100.0, 0.0, res[0], res[0] )
			};

			if ( Core.ML )
			{
				flower.BonusResources = new BonusHarvestResource[] // cos this is mining after all
				{
					new BonusHarvestResource( 0, 78.0, null, null ),	//Nothing
					new BonusHarvestResource( 60, 2.0, 1074542, typeof( HomePlants_Flower ) )
				};
			}

			flower.RandomizeVeins = Core.ML;

			flower.Resources = res;
			flower.Veins = veins;

			Definitions.Add( flower );
			#endregion

			#region Cactus /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			HarvestDefinition cactus = m_Cactus = new HarvestDefinition();
			cactus.BankWidth = 1;
			cactus.BankHeight = 1;
			cactus.MinTotal = 1;
			cactus.MaxTotal = 1;
			cactus.MinRespawn = TimeSpan.FromMinutes( 50.0 );
			cactus.MaxRespawn = TimeSpan.FromMinutes( 70.0 );
			cactus.Skill = SkillName.Cooking;
			cactus.Tiles = m_CactusTiles;
			cactus.MaxRange = 1;
			cactus.ConsumedPerHarvest = 1;
			cactus.ConsumedPerFeluccaHarvest = 1;
			cactus.EffectActions = new int[]{ 4 };
			cactus.EffectSounds = new int[]{ 0x248 };
			cactus.EffectCounts = new int[]{ 1 };
			cactus.EffectDelay = TimeSpan.FromSeconds( 0.0 );
			cactus.EffectSoundDelay = TimeSpan.FromSeconds( 0.1 );
			cactus.NoResourcesMessage = 1114028; // You harvest the last resources from this plant
			cactus.FailMessage = 501756; // Nothing worth taking
			cactus.OutOfRangeMessage = 500446; // That is too far away.
			cactus.PackFullMessage = 500720; // You don't have enough room in your backpack!
			cactus.ToolBrokeMessage = 1044038; // You broke your tool.

			res = new HarvestResource[]
			{
				new HarvestResource( 000.0, 000.0, 150.0, "You put an herb in your backpack",					typeof( PlantHerbalism_Cactus ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 100.0, 0.0, res[0], res[0] )
			};

			if ( Core.ML )
			{
				cactus.BonusResources = new BonusHarvestResource[] // cos this is mining after all
				{
					new BonusHarvestResource( 0, 78.0, null, null ),	//Nothing
					new BonusHarvestResource( 60, 2.0, 1074542, typeof( HomePlants_Cactus ) )
				};
			}

			cactus.RandomizeVeins = Core.ML;

			cactus.Resources = res;
			cactus.Veins = veins;

			Definitions.Add( cactus );
			#endregion

			#region Lilly /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			HarvestDefinition lilly = m_Lilly = new HarvestDefinition();
			lilly.BankWidth = 1;
			lilly.BankHeight = 1;
			lilly.MinTotal = 1;
			lilly.MaxTotal = 1;
			lilly.MinRespawn = TimeSpan.FromMinutes( 50.0 );
			lilly.MaxRespawn = TimeSpan.FromMinutes( 70.0 );
			lilly.Skill = SkillName.Cooking;
			lilly.Tiles = m_LillyTiles;
			lilly.MaxRange = 1;
			lilly.ConsumedPerHarvest = 1;
			lilly.ConsumedPerFeluccaHarvest = 1;
			lilly.EffectActions = new int[]{ 4 };
			lilly.EffectSounds = new int[]{ 0x248 };
			lilly.EffectCounts = new int[]{ 1 };
			lilly.EffectDelay = TimeSpan.FromSeconds( 0.0 );
			lilly.EffectSoundDelay = TimeSpan.FromSeconds( 0.1 );
			lilly.NoResourcesMessage = 1114028; // You harvest the last resources from this plant
			lilly.FailMessage = 501756; // Nothing worth taking
			lilly.OutOfRangeMessage = 500446; // That is too far away.
			lilly.PackFullMessage = 500720; // You don't have enough room in your backpack!
			lilly.ToolBrokeMessage = 1044038; // You broke your tool.

			res = new HarvestResource[]
			{
				new HarvestResource( 000.0, 000.0, 150.0, "You put an herb in your backpack",					typeof( PlantHerbalism_Lilly ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 100.0, 0.0, res[0], res[0] )
			};

			if ( Core.ML )
			{
				lilly.BonusResources = new BonusHarvestResource[] // cos this is mining after all
				{
					new BonusHarvestResource( 0, 78.0, null, null ),	//Nothing
					new BonusHarvestResource( 60, 2.0, 1074542, typeof( HomePlants_Lilly ) )
				};
			}

			lilly.RandomizeVeins = Core.ML;

			lilly.Resources = res;
			lilly.Veins = veins;

			Definitions.Add( lilly );

			#endregion

			#region Grass /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			HarvestDefinition grass = m_Grass = new HarvestDefinition();
			grass.BankWidth = 1;
			grass.BankHeight = 1;
			grass.MinTotal = 1;
			grass.MaxTotal = 1;
			grass.MinRespawn = TimeSpan.FromMinutes( 50.0 );
			grass.MaxRespawn = TimeSpan.FromMinutes( 70.0 );
			grass.Skill = SkillName.Cooking;
			grass.Tiles = m_GrassTiles;
			grass.MaxRange = 1;
			grass.ConsumedPerHarvest = 1;
			grass.ConsumedPerFeluccaHarvest = 1;
			grass.EffectActions = new int[]{ 4 };
			grass.EffectSounds = new int[]{ 0x248 };
			grass.EffectCounts = new int[]{ 1 };
			grass.EffectDelay = TimeSpan.FromSeconds( 0.0 );
			grass.EffectSoundDelay = TimeSpan.FromSeconds( 0.1 );
			grass.NoResourcesMessage = 1114028; // You harvest the last resources from this plant
			grass.FailMessage = 501756; // Nothing worth taking
			grass.OutOfRangeMessage = 500446; // That is too far away.
			grass.PackFullMessage = 500720; // You don't have enough room in your backpack!
			grass.ToolBrokeMessage = 1044038; // You broke your tool.

			res = new HarvestResource[]
			{
				new HarvestResource( 000.0, 000.0, 150.0, "You put an herb in your backpack",					typeof( PlantHerbalism_Grass ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 100.0, 0.0, res[0], res[0] )
			};

			if ( Core.ML )
			{
				grass.BonusResources = new BonusHarvestResource[] // cos this is mining after all
				{
					new BonusHarvestResource( 0, 78.0, null, null ),	//Nothing
					new BonusHarvestResource( 60, 2.0, 1074542, typeof( HomePlants_Grass ) )
				};
			}

			grass.RandomizeVeins = Core.ML;

			grass.Resources = res;
			grass.Veins = veins;

			Definitions.Add( grass );

			#endregion
		}

        public override Type GetResourceType( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource )
        {
            return base.GetResourceType( from, tool, def, map, loc, resource );
        }

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) )
				return false;

			if ( from.Mounted )
			{
				from.SendMessage("You cannot trim plants while riding.");
				return false;
			}
			else if ( from.IsBodyMod && !from.Body.IsHuman )
			{
				from.SendMessage("You cannot trim plants while polymorphed.");
				return false;
			}

			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) )
				return false;

			else if ( from.Mounted )
			{
				from.SendMessage("You cannot trim plants while riding.");
				return false;
			}
			else if ( from.IsBodyMod && !from.Body.IsHuman )
			{
				from.SendMessage("You cannot trim plants while polymorphed.");
				return false;
			}

			return true;
		}

		public override bool BeginHarvesting( Mobile from, Item tool )
		{
			if ( !base.BeginHarvesting( from, tool ) )
				return false;

			from.SendMessage("Which plant do you want to trim?");
			return true;
		}

		public override void OnBadHarvestTarget( Mobile from, Item tool, object toHarvest )
		{
			from.SendMessage( "That is not a plant you can trim." );
		}

		public override void OnHarvestStarted( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			base.OnHarvestStarted( from, tool, def, toHarvest );

			if ( Core.ML )
				from.RevealingAction();
		}

		public static void Initialize()
		{
			Array.Sort( m_LeafTiles );
			Array.Sort( m_FlowerTiles );
			Array.Sort( m_LillyTiles );
			Array.Sort( m_MushroomTiles );
			Array.Sort( m_CactusTiles );
			Array.Sort( m_GrassTiles );
		}

		#region Tile lists
		private static int[] m_LeafTiles = new int[]		
		{		
			0x4C93, 
			0x4C94, 
			0x4C97, 
			0x4C98, 
			0x4C9F, 
			0x4CA0, 
			0x4CA1, 
			0x4CA2, 
			0x4CA3, 
			0x4CA4, 
			0x4CA5, 
			0x4CA7,
			0x10A7, 
			0x10A8, 
			0x10A9, 
			0x10AA, 
			0x10AB
		};		

		private static int[] m_FlowerTiles = new int[]		
		{		
			0x4C37, 
			0x4C38, 
			0x4C45, 
			0x4C46, 
			0x4C47, 
			0x4C48, 
			0x4C49, 
			0x4C4A, 
			0x4C4B, 
			0x4C4C, 
			0x4C4D, 
			0x4C4E, 
			0x4C83, 
			0x4C84, 
			0x4C85, 
			0x4C86, 
			0x4C87, 
			0x4C88, 
			0x4C89, 
			0x4C8A, 
			0x4C8B, 
			0x4C8C, 
			0x4C8D, 
			0x4C8E, 
			0x4CBE, 
			0x4CBF, 
			0x4CC0, 
			0x4CC1, 
			0x4D29, 
			0x4D2B, 
			0x4D2D, 
			0x4D2F, 
			0x51CA, 
			0x51CB
		};		

		private static int[] m_LillyTiles = new int[]		
		{		
			0x4CA9, 
			0x4CB7, 
			0x4CB8, 
			0x4CB9, 
			0x4CBA, 
			0x4D04, 
			0x4D05, 
			0x4D06, 
			0x4D07, 
			0x4D08, 
			0x4D09, 
			0x4D0A, 
			0x4D0B, 
			0x4DBC, 
			0x4DBE, 
			0x4DC1, 
			0x4DC2, 
			0x4DC3
		};		

		private static int[] m_MushroomTiles = new int[]		
		{		
			0x4D0C, 
			0x4D0D, 
			0x4D0E, 
			0x4D0F, 
			0x4D10, 
			0x4D11, 
			0x4D12, 
			0x4D13, 
			0x4D14, 
			0x4D15, 
			0x4D16, 
			0x4D17, 
			0x4D18, 
			0x4D19, 
			0x622E, 
			0x622F, 
			0x6230, 
			0x6231,
			0x631A,
			0x632B,
			0x631C,
			0x631D,
			0x631E,
			0x631F,
			0x6320,
			0x6321,
			0x6322,
			0x6323,
			0x6324,
			0x6325,
			0x6326,
			0x6327,
			0x633A
		};		

		private static int[] m_CactusTiles = new int[]		
		{		
			0x4D25, 
			0x4D26, 
			0x4D27, 
			0x4D28, 
			0x4D2A, 
			0x4D2B, 
			0x4D2C, 
			0x4D2E,
			0x4D35,
			0x5E0F,
			0x5E10,
			0x5E11,
			0x5E12,
			0x5E13,
			0x5E14,
			0x6376
		};

		private static int[] m_GrassTiles = new int[]		
		{		
			0x4CAC, 
			0x4CAD, 
			0x4CAE, 
			0x4CAF, 
			0x4CB0, 
			0x4CB1, 
			0x4CB2, 
			0x4CB3, 
			0x4CB4, 
			0x4CB5, 
			0x4CB6, 
			0x4CB9, 
			0x4CBA, 
			0x4CBB, 
			0x4CBC, 
			0x4CBD, 
			0x4CC5, 
			0x4CC6, 
			0x4D32, 
			0x4D33
		};	


		#endregion
	}
}
