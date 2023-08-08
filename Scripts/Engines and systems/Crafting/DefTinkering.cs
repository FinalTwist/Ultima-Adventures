using System;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Engines.Craft
{
	public class DefTinkering : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tinkering; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044007; } // <CENTER>TINKERING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTinkering();

				return m_CraftSystem;
			}
		}

		private DefTinkering() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			if ( item.NameNumber == 1044258 || item.NameNumber == 1046445 ) // potion keg and faction trap removal kit
				return 0.5; // 50%

			return 0.0; // 0%
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		private static Type[] m_TinkerColorables = new Type[]
			{
				typeof( ForkLeft ), typeof( ForkRight ),
				typeof( SpoonLeft ), typeof( SpoonRight ),
				typeof( KnifeLeft ), typeof( KnifeRight ),
				typeof( Plate ),
				typeof( Goblet ), typeof( PewterMug ),
				typeof( KeyRing ),
				typeof( Candelabra ), typeof( Scales ),
				typeof( Key ), typeof( Globe ),
				typeof( Spyglass ), typeof( Lantern ),
				typeof( HeatingStand )
			};

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			if ( !type.IsSubclassOf( typeof( BaseIngot ) ) )
				return false;

			type = item.ItemType;

			bool contains = false;

			for ( int i = 0; !contains && i < m_TinkerColorables.Length; ++i )
				contains = ( m_TinkerColorables[i] == type );

			return contains;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x542 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override bool ConsumeOnFailure( Mobile from, Type resourceType, CraftItem craftItem )
		{

			return base.ConsumeOnFailure( from, resourceType, craftItem );
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Wooden Items
			AddCraft( typeof( JointingPlane ), 1044042, 1024144, 0.0, 50.0, typeof( Log ), 1015101, 4, 1044351 );
			AddCraft( typeof( MouldingPlane ), 1044042, 1024140, 0.0, 50.0, typeof( Log ), 1015101, 4, 1044351 );
			AddCraft( typeof( SmoothingPlane ), 1044042, 1024146, 0.0, 50.0, typeof( Log ), 1015101, 4, 1044351 );
			AddCraft( typeof( ClockFrame ), 1044042, 1024173, 0.0, 50.0, typeof( Log ), 1015101, 6, 1044351 );
			AddCraft( typeof( Axle ), 1044042, 1024187, -25.0, 25.0, typeof( Log ), 1015101, 2, 1044351 );
			AddCraft( typeof( RollingPin ), 1044042, 1024163, 0.0, 50.0, typeof( Log ), 1015101, 5, 1044351 );

			index = AddCraft( typeof( SawMillSouthAddonDeed ), 1044042, "saw mill (south)", 60.0, 120.0, typeof( Granite ), 1044514, 80, 1044513 );
			AddSkill( index, SkillName.Lumberjacking, 75.0, 80.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );

			index = AddCraft( typeof( SawMillEastAddonDeed ), 1044042, "saw mill (east)", 60.0, 120.0, typeof( Granite ), 1044514, 80, 1044513 );
			AddSkill( index, SkillName.Lumberjacking, 75.0, 80.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );

			if( Core.SE )
			{
				index = AddCraft( typeof( Nunchaku ), 1044042, 1030158, 70.0, 120.0, typeof( IronIngot ), 1044036, 3, 1044037 );
				AddRes( index, typeof( Log ), 1015101, 8, 1044351 );
				 
			}
			#endregion

			#region Tools
			AddCraft( typeof( Scissors ), 1044046, 1023998, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( MortarPestle ), 1044046, 1023739, 20.0, 70.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( Scorp ), 1044046, 1024327, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( TinkerTools ), 1044046, 1044164, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Hatchet ), 1044046, 1023907, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( DrawKnife ), 1044046, 1024324, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SewingKit ), 1044046, 1023997, 10.0, 70.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( GardenTool ), 1044046, "gardening shears", 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( HerbalistCauldron ), 1044046, "herbalist cauldron", 30.0, 60.0, typeof( IronIngot ), 1044036, 20, 1044037 );
			AddCraft( typeof( Saw ), 1044046, 1024148, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( DovetailSaw ), 1044046, 1024136, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Froe ), 1044046, 1024325, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Shovel ), 1044046, 1023898, 40.0, 90.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( OreShovel ), 1044046, "ore spade", 35.0, 85.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( GraveShovel ), 1044046, "grave shovel", 35.0, 85.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Hammer ), 1044046, 1024138, 30.0, 80.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Tongs ), 1044046, 1024028, 35.0, 85.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( SmithHammer ), 1044046, 1025091, 40.0, 90.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( SledgeHammer ), 1044046, 1024021, 40.0, 90.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Inshave ), 1044046, 1024326, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Pickaxe ), 1044046, 1023718, 40.0, 90.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Lockpick ), 1044046, 1025371, 45.0, 95.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Skillet ), 1044046, 1044567, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( FlourSifter ), 1044046, 1024158, 50.0, 100.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( FletcherTools ), 1044046, 1044166, 35.0, 85.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( MapmakersPen ), 1044046, 1044167, 25.0, 75.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( ScribesPen ), 1044046, 1044168, 25.0, 75.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( SkinningKnife ), 1044046, "skinning knife", 15.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SurgeonsKnife ), 1044046, "surgeons knife", 15.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( MixingCauldron ), 1044046, "mixing cauldron", 30.0, 60.0, typeof( IronIngot ), 1044036, 20, 1044037 );
			AddCraft( typeof( WaxingPot ), 1044046, "wax crafting pot", 20.0, 60.0, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddCraft( typeof( WoodworkingTools ), 1044046, "woodworking tools", 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( TrapKit ), 1044046, "trapping tools", 75.0, 110.0, typeof( IronIngot ), 1044036, 32, 1044037 );

			#endregion

			#region Parts
			AddCraft( typeof( Gears ), 1044047, 1024179, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( ClockParts ), 1044047, 1024175, 25.0, 75.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( BarrelTap ), 1044047, 1024100, 35.0, 85.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Springs ), 1044047, 1024189, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SextantParts ), 1044047, 1024185, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( BarrelHoops ), 1044047, 1024321, -15.0, 35.0, typeof( IronIngot ), 1044036, 5, 1044037 );
			AddCraft( typeof( Hinge ), 1044047, 1024181, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( BolaBall ), 1044047, 1023699, 45.0, 95.0, typeof( IronIngot ), 1044036, 10, 1044037 );
			
			#endregion

			#region Utensils
			AddCraft( typeof( ButcherKnife ), 1044048, 1025110, 25.0, 75.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SpoonLeft ), 1044048, 1044158, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( SpoonRight ), 1044048, 1044159, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Plate ), 1044048, 1022519, 0.0, 50.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( ForkLeft ), 1044048, 1044160, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( ForkRight ), 1044048, 1044161, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Cleaver ), 1044048, 1023778, 20.0, 70.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( KnifeLeft ), 1044048, 1044162, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( KnifeRight ), 1044048, 1044163, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Goblet ), 1044048, 1022458, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( PewterMug ), 1044048, 1024097, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SkinningKnife ), 1044048, 1023781, 25.0, 75.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			#endregion

			#region Misc

			index = AddCraft( typeof( CandleLarge ), 1044050, 1022598, 45.0, 85.0, typeof( IronIngot ), 1044036, 2, 1044037 );
				AddRes( index, typeof( Beeswax ), 1025154, 1, 1053098 );
			index = AddCraft( typeof( Candelabra ), 1044050, 1022599, 55.0, 195.0, typeof( IronIngot ), 1044036, 4, 1044037 );
				AddRes( index, typeof( Beeswax ), 1025154, 3, 1053098 );
			index = AddCraft( typeof( CandelabraStand ), 1044050, 1022599, 65.0, 105.0, typeof( IronIngot ), 1044036, 8, 1044037 );
				AddRes( index, typeof( Beeswax ), 1025154, 3, 1053098 );

			AddCraft( typeof( Scales ), 1044050, 1026225, 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Key ), 1044050, 1024112, 20.0, 70.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( KeyRing ), 1044050, 1024113, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Globe ), 1044050, 1024167, 55.0, 105.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Spyglass ), 1044050, "telescope", 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Lantern ), 1044050, 1022597, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( HeatingStand ), 1044050, 1026217, 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );
				index = AddCraft( typeof( WallTorch ), 1044050, "wall torch", 55.0, 105.0, typeof( IronIngot ), 1044036, 5, 1044037 );
				AddRes( index, typeof( Torch ), 1011410, 1, 1053098 );

				index = AddCraft( typeof( ColoredWallTorch ), 1044050, "colored wall torch", 85.0, 125.0, typeof( IronIngot ), 1044036, 5, 1044037 );
				AddRes( index, typeof( Torch ), 1011410, 1, 1053098 );

				index = AddCraft( typeof( ShojiLantern ), 1044050, 1029404, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1015101, 5, 1044351 );
				 

				index = AddCraft( typeof( PaperLantern ), 1044050, 1029406, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1015101, 5, 1044351 );
				 

				index = AddCraft( typeof( RoundPaperLantern ), 1044050, 1029418, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1015101, 5, 1044351 );
				 

				index = AddCraft( typeof( WindChimes ), 1044050, 1030290, 80.0, 130.0, typeof( IronIngot ), 1044036, 15, 1044037 );
				 

				index = AddCraft( typeof( FancyWindChimes ), 1044050, 1030291, 80.0, 130.0, typeof( IronIngot ), 1044036, 15, 1044037 );

			#endregion

			#region Jewelry

			AddCraft( typeof( AgapiteAmulet ), 1044049, "agapite amulet", 40.0, 90.0, typeof( AgapiteIngot ), "agapite ingot", 2, 1053098 );
			AddCraft( typeof( AgapiteBracelet ), 1044049, "agapite bracelet", 40.0, 90.0, typeof( AgapiteIngot ), "agapite ingot", 2, 1053098 );
			AddCraft( typeof( AgapiteRing ), 1044049, "agapite ring", 40.0, 90.0, typeof( AgapiteIngot ), "agapite ingot", 2, 1053098 );
			AddCraft( typeof( AgapiteEarrings ), 1044049, "agapite earrings", 40.0, 90.0, typeof( AgapiteIngot ), "agapite ingot", 2, 1053098 );
			index = AddCraft( typeof( AmberAmulet ), 1044049, "amber amulet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Amber ), "amber", 1, 1053098 );
			index = AddCraft( typeof( AmberBracelet ), 1044049, "amber bracelet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Amber ), "amber", 1, 1053098 );
			index = AddCraft( typeof( AmberRing ), 1044049, "amber ring", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Amber ), "amber", 1, 1053098 );
			index = AddCraft( typeof( AmberEarrings ), 1044049, "amber earrings", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Amber ), "amber", 1, 1053098 );
			AddCraft( typeof( AmethystAmulet ), 1044049, "amethyst amulet", 40.0, 90.0, typeof( AmethystIngot ), "amethyst ingot", 2, 1053098 );
			AddCraft( typeof( AmethystBracelet ), 1044049, "amethyst bracelet", 40.0, 90.0, typeof( AmethystIngot ), "amethyst ingot", 2, 1053098 );
			AddCraft( typeof( AmethystRing ), 1044049, "amethyst ring", 40.0, 90.0, typeof( AmethystIngot ), "amethyst ingot", 2, 1053098 );
			AddCraft( typeof( AmethystEarrings ), 1044049, "amethyst earrings", 40.0, 90.0, typeof( AmethystIngot ), "amethyst ingot", 2, 1053098 );
			AddCraft( typeof( BrassAmulet ), 1044049, "brass amulet", 40.0, 90.0, typeof( BrassIngot ), "brass ingot", 2, 1053098 );
			AddCraft( typeof( BrassBracelet ), 1044049, "brass bracelet", 40.0, 90.0, typeof( BrassIngot ), "brass ingot", 2, 1053098 );
			AddCraft( typeof( BrassRing ), 1044049, "brass ring", 40.0, 90.0, typeof( BrassIngot ), "brass ingot", 2, 1053098 );
			AddCraft( typeof( BrassEarrings ), 1044049, "brass earrings", 40.0, 90.0, typeof( BrassIngot ), "brass ingot", 2, 1053098 );
			AddCraft( typeof( BronzeAmulet ), 1044049, "bronze amulet", 40.0, 90.0, typeof( BronzeIngot ), "bronze ingot", 2, 1053098 );
			AddCraft( typeof( BronzeBracelet ), 1044049, "bronze bracelet", 40.0, 90.0, typeof( BronzeIngot ), "bronze ingot", 2, 1053098 );
			AddCraft( typeof( BronzeRing ), 1044049, "bronze ring", 40.0, 90.0, typeof( BronzeIngot ), "bronze ingot", 2, 1053098 );
			AddCraft( typeof( BronzeEarrings ), 1044049, "bronze earrings", 40.0, 90.0, typeof( BronzeIngot ), "bronze ingot", 2, 1053098 );
			AddCraft( typeof( CaddelliteAmulet ), 1044049, "caddellite amulet", 40.0, 90.0, typeof( CaddelliteIngot ), "caddellite ingot", 2, 1053098 );
			AddCraft( typeof( CaddelliteBracelet ), 1044049, "caddellite amulet", 40.0, 90.0, typeof( CaddelliteIngot ), "caddellite ingot", 2, 1053098 );
			AddCraft( typeof( CaddelliteRing ), 1044049, "caddellite amulet", 40.0, 90.0, typeof( CaddelliteIngot ), "caddellite ingot", 2, 1053098 );
			AddCraft( typeof( CaddelliteEarrings ), 1044049, "caddellite amulet", 40.0, 90.0, typeof( CaddelliteIngot ), "caddellite ingot", 2, 1053098 );
			AddCraft( typeof( CopperAmulet ), 1044049, "copper amulet", 40.0, 90.0, typeof( CopperIngot ), "copper ingot", 2, 1053098 );
			AddCraft( typeof( CopperBracelet ), 1044049, "copper bracelet", 40.0, 90.0, typeof( CopperIngot ), "copper ingot", 2, 1053098 );
			AddCraft( typeof( CopperRing ), 1044049, "copper ring", 40.0, 90.0, typeof( CopperIngot ), "copper ingot", 2, 1053098 );
			AddCraft( typeof( CopperEarrings ), 1044049, "copper earrings", 40.0, 90.0, typeof( CopperIngot ), "copper ingot", 2, 1053098 );
			index = AddCraft( typeof( DiamondAmulet ), 1044049, "diamond amulet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Diamond ), "diamond", 1, 1053098 );
			index = AddCraft( typeof( DiamondBracelet ), 1044049, "diamond bracelet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Diamond ), "diamond", 1, 1053098 );
			index = AddCraft( typeof( DiamondRing ), 1044049, "diamond ring", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Diamond ), "diamond", 1, 1053098 );
			index = AddCraft( typeof( DiamondEarrings ), 1044049, "diamond earrings", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Diamond ), "diamond", 1, 1053098 );
			AddCraft( typeof( DullCopperAmulet ), 1044049, "dull copper amulet", 40.0, 90.0, typeof( DullCopperIngot ), "dull copper ingot", 2, 1053098 );
			AddCraft( typeof( DullCopperBracelet ), 1044049, "dull copper bracelet", 40.0, 90.0, typeof( DullCopperIngot ), "dull copper ingot", 2, 1053098 );
			AddCraft( typeof( DullCopperRing ), 1044049, "dull copper ring", 40.0, 90.0, typeof( DullCopperIngot ), "dull copper ingot", 2, 1053098 );
			AddCraft( typeof( DullCopperEarrings ), 1044049, "dull copper earrings", 40.0, 90.0, typeof( DullCopperIngot ), "dull copper ingot", 2, 1053098 );


			AddCraft( typeof( DwarvenAmulet ), 1044049, "dwarven amulet", 99.0, 125.0, typeof( MithrilIngot ), "dwarven ingot", 2, 1053098 );
			AddCraft( typeof( DwarvenBracelet ), 1044049, "dwarven bracelet", 99.0, 125.0, typeof( MithrilIngot ), "dwarven ingot", 2, 1053098 );
			AddCraft( typeof( DwarvenRing ), 1044049, "dwarven ring", 99.0, 125.0, typeof( MithrilIngot ), "dwarven ingot", 2, 1053098 );
			AddCraft( typeof( DwarvenEarrings ), 1044049, "dwarven earrings", 99.0, 125.0, typeof( MithrilIngot ), "dwarven ingot", 2, 1053098 );


			index = AddCraft( typeof( EmeraldAmulet ), 1044049, "emerald amulet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Emerald ), "emerald", 1, 1053098 );
			index = AddCraft( typeof( EmeraldBracelet ), 1044049, "emerald bracelet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Emerald ), "emerald", 1, 1053098 );
			index = AddCraft( typeof( EmeraldRing ), 1044049, "emerald ring", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Emerald ), "emerald", 1, 1053098 );
			index = AddCraft( typeof( EmeraldEarrings ), 1044049, "emerald earrings", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Emerald ), "emerald", 1, 1053098 );
			AddCraft( typeof( GarnetAmulet ), 1044049, "garnet amulet", 40.0, 90.0, typeof( GarnetIngot ), "garnet ingot", 2, 1053098 );
			AddCraft( typeof( GarnetBracelet ), 1044049, "garnet bracelet", 40.0, 90.0, typeof( GarnetIngot ), "garnet ingot", 2, 1053098 );
			AddCraft( typeof( GarnetRing ), 1044049, "garnet ring", 40.0, 90.0, typeof( GarnetIngot ), "garnet ingot", 2, 1053098 );
			AddCraft( typeof( GarnetEarrings ), 1044049, "garnet earrings", 40.0, 90.0, typeof( GarnetIngot ), "garnet ingot", 2, 1053098 );
			AddCraft( typeof( GoldenAmulet ), 1044049, "golden amulet", 40.0, 90.0, typeof( GoldIngot ), "gold ingot", 2, 1053098 );
			AddCraft( typeof( GoldenBracelet ), 1044049, "golden bracelet", 40.0, 90.0, typeof( GoldIngot ), "gold ingot", 2, 1053098 );
			AddCraft( typeof( GoldenRing ), 1044049, "golden ring", 40.0, 90.0, typeof( GoldIngot ), "gold ingot", 2, 1053098 );
			AddCraft( typeof( GoldenEarrings ), 1044049, "golden earrings", 40.0, 90.0, typeof( GoldIngot ), "gold ingot", 2, 1053098 );
			AddCraft( typeof( JadeAmulet ), 1044049, "jade amulet", 40.0, 90.0, typeof( JadeIngot ), "jade ingot", 2, 1053098 );
			AddCraft( typeof( JadeBracelet ), 1044049, "jade bracelet", 40.0, 90.0, typeof( JadeIngot ), "jade ingot", 2, 1053098 );
			AddCraft( typeof( JadeRing ), 1044049, "jade ring", 40.0, 90.0, typeof( JadeIngot ), "jade ingot", 2, 1053098 );
			AddCraft( typeof( JadeEarrings ), 1044049, "jade earrings", 40.0, 90.0, typeof( JadeIngot ), "jade ingot", 2, 1053098 );
			AddCraft( typeof( MithrilAmulet ), 1044049, "mithril amulet", 40.0, 90.0, typeof( MithrilIngot ), "mithril ingot", 2, 1053098 );
			AddCraft( typeof( MithrilBracelet ), 1044049, "mithril bracelet", 40.0, 90.0, typeof( MithrilIngot ), "mithril ingot", 2, 1053098 );
			AddCraft( typeof( MithrilRing ), 1044049, "mithril ring", 40.0, 90.0, typeof( MithrilIngot ), "mithril ingot", 2, 1053098 );
			AddCraft( typeof( MithrilEarrings ), 1044049, "mithril earrings", 40.0, 90.0, typeof( MithrilIngot ), "mithril ingot", 2, 1053098 );
			AddCraft( typeof( NepturiteAmulet ), 1044049, "nepturite amulet", 40.0, 90.0, typeof( NepturiteIngot ), "nepturite ingot", 2, 1053098 );
			AddCraft( typeof( NepturiteBracelet ), 1044049, "nepturite bracelet", 40.0, 90.0, typeof( NepturiteIngot ), "nepturite ingot", 2, 1053098 );
			AddCraft( typeof( NepturiteRing ), 1044049, "nepturite ring", 40.0, 90.0, typeof( NepturiteIngot ), "nepturite ingot", 2, 1053098 );
			AddCraft( typeof( NepturiteEarrings ), 1044049, "nepturite earrings", 40.0, 90.0, typeof( NepturiteIngot ), "nepturite ingot", 2, 1053098 );
			AddCraft( typeof( ObsidianAmulet ), 1044049, "obsidian amulet", 40.0, 90.0, typeof( ObsidianIngot ), "obsidian ingot", 2, 1053098 );
			AddCraft( typeof( ObsidianBracelet ), 1044049, "obsidian bracelet", 40.0, 90.0, typeof( ObsidianIngot ), "obsidian ingot", 2, 1053098 );
			AddCraft( typeof( ObsidianRing ), 1044049, "obsidian ring", 40.0, 90.0, typeof( ObsidianIngot ), "obsidian ingot", 2, 1053098 );
			AddCraft( typeof( ObsidianEarrings ), 1044049, "obsidian earrings", 40.0, 90.0, typeof( ObsidianIngot ), "obsidian ingot", 2, 1053098 );
			AddCraft( typeof( OnyxAmulet ), 1044049, "onyx amulet", 40.0, 90.0, typeof( OnyxIngot ), "onyx ingot", 2, 1053098 );
			AddCraft( typeof( OnyxBracelet ), 1044049, "onyx bracelet", 40.0, 90.0, typeof( OnyxIngot ), "onyx ingot", 2, 1053098 );
			AddCraft( typeof( OnyxRing ), 1044049, "onyx ring", 40.0, 90.0, typeof( OnyxIngot ), "onyx ingot", 2, 1053098 );
			AddCraft( typeof( OnyxEarrings ), 1044049, "onyx earrings", 40.0, 90.0, typeof( OnyxIngot ), "onyx ingot", 2, 1053098 );
			index = AddCraft( typeof( PearlAmulet ), 1044049, "pearl amulet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( MysticalPearl ), "pearl", 1, 1053098 );
			index = AddCraft( typeof( PearlBracelet ), 1044049, "pearl bracelet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( MysticalPearl ), "pearl", 1, 1053098 );
			index = AddCraft( typeof( PearlRing ), 1044049, "pearl ring", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( MysticalPearl ), "pearl", 1, 1053098 );
			index = AddCraft( typeof( PearlEarrings ), 1044049, "pearl earrings", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( MysticalPearl ), "pearl", 1, 1053098 );
			AddCraft( typeof( QuartzAmulet ), 1044049, "quartz amulet", 40.0, 90.0, typeof( QuartzIngot ), "quartz ingot", 2, 1053098 );
			AddCraft( typeof( QuartzBracelet ), 1044049, "quartz bracelet", 40.0, 90.0, typeof( QuartzIngot ), "quartz ingot", 2, 1053098 );
			AddCraft( typeof( QuartzRing ), 1044049, "quartz ring", 40.0, 90.0, typeof( QuartzIngot ), "quartz ingot", 2, 1053098 );
			AddCraft( typeof( QuartzEarrings ), 1044049, "quartz earrings", 40.0, 90.0, typeof( QuartzIngot ), "quartz ingot", 2, 1053098 );
			index = AddCraft( typeof( RubyAmulet ), 1044049, "ruby amulet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Ruby ), "ruby", 1, 1053098 );
			index = AddCraft( typeof( RubyBracelet ), 1044049, "ruby bracelet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Ruby ), "ruby", 1, 1053098 );
			index = AddCraft( typeof( RubyRing ), 1044049, "ruby ring", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Ruby ), "ruby", 1, 1053098 );
			index = AddCraft( typeof( RubyEarrings ), 1044049, "ruby earrings", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Ruby ), "ruby", 1, 1053098 );
			index = AddCraft( typeof( SapphireAmulet ), 1044049, "sapphire amulet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Sapphire ), "sapphire", 1, 1053098 );
			index = AddCraft( typeof( SapphireBracelet ), 1044049, "sapphire bracelet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Sapphire ), "sapphire", 1, 1053098 );
			index = AddCraft( typeof( SapphireRing ), 1044049, "sapphire ring", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Sapphire ), "sapphire", 1, 1053098 );
			index = AddCraft( typeof( SapphireEarrings ), 1044049, "sapphire earrings", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Sapphire ), "sapphire", 1, 1053098 );
			AddCraft( typeof( ShadowIronAmulet ), 1044049, "shadow iron amulet", 40.0, 90.0, typeof( ShadowIronIngot ), "shadow iron ingot", 2, 1053098 );
			AddCraft( typeof( ShadowIronBracelet ), 1044049, "shadow iron bracelet", 40.0, 90.0, typeof( ShadowIronIngot ), "shadow iron ingot", 2, 1053098 );
			AddCraft( typeof( ShadowIronRing ), 1044049, "shadow iron ring", 40.0, 90.0, typeof( ShadowIronIngot ), "shadow iron ingot", 2, 1053098 );
			AddCraft( typeof( ShadowIronEarrings ), 1044049, "shadow iron earrings", 40.0, 90.0, typeof( ShadowIronIngot ), "shadow iron ingot", 2, 1053098 );
			AddCraft( typeof( SilveryAmulet ), 1044049, "silver amulet", 40.0, 90.0, typeof( ShinySilverIngot ), "silver ingot", 2, 1053098 );
			AddCraft( typeof( SilveryBracelet ), 1044049, "silver bracelet", 40.0, 90.0, typeof( ShinySilverIngot ), "silver ingot", 2, 1053098 );
			AddCraft( typeof( SilveryRing ), 1044049, "silver ring", 40.0, 90.0, typeof( ShinySilverIngot ), "silver ingot", 2, 1053098 );
			AddCraft( typeof( SilveryEarrings ), 1044049, "silver earrings", 40.0, 90.0, typeof( ShinySilverIngot ), "silver ingot", 2, 1053098 );
			AddCraft( typeof( SpinelAmulet ), 1044049, "spinel amulet", 40.0, 90.0, typeof( SpinelIngot ), "spinel ingot", 2, 1053098 );
			AddCraft( typeof( SpinelBracelet ), 1044049, "spinel bracelet", 40.0, 90.0, typeof( SpinelIngot ), "spinel ingot", 2, 1053098 );
			AddCraft( typeof( SpinelRing ), 1044049, "spinel ring", 40.0, 90.0, typeof( SpinelIngot ), "spinel ingot", 2, 1053098 );
			AddCraft( typeof( SpinelEarrings ), 1044049, "spinel earrings", 40.0, 90.0, typeof( SpinelIngot ), "spinel ingot", 2, 1053098 );
			AddCraft( typeof( StarRubyAmulet ), 1044049, "star ruby amulet", 40.0, 90.0, typeof( StarRubyIngot ), "star ruby ingot", 2, 1053098 );
			AddCraft( typeof( StarRubyBracelet ), 1044049, "star ruby bracelet", 40.0, 90.0, typeof( StarRubyIngot ), "star ruby ingot", 2, 1053098 );
			AddCraft( typeof( StarRubyRing ), 1044049, "star ruby ring", 40.0, 90.0, typeof( StarRubyIngot ), "star ruby ingot", 2, 1053098 );
			AddCraft( typeof( StarRubyEarrings ), 1044049, "star ruby earrings", 40.0, 90.0, typeof( StarRubyIngot ), "star ruby ingot", 2, 1053098 );
			index = AddCraft( typeof( StarSapphireAmulet ), 1044049, "star sapphire amulet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( StarSapphire ), "star sapphire", 1, 1053098 );
			index = AddCraft( typeof( StarSapphireBracelet ), 1044049, "star sapphire bracelet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( StarSapphire ), "star sapphire", 1, 1053098 );
			index = AddCraft( typeof( StarSapphireRing ), 1044049, "star sapphire ring", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( StarSapphire ), "star sapphire", 1, 1053098 );
			index = AddCraft( typeof( StarSapphireEarrings ), 1044049, "star sapphire earrings", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( StarSapphire ), "star sapphire", 1, 1053098 );
			AddCraft( typeof( SteelAmulet ), 1044049, "steel amulet", 40.0, 90.0, typeof( SteelIngot ), "steel ingot", 2, 1053098 );
			AddCraft( typeof( SteelBracelet ), 1044049, "steel bracelet", 40.0, 90.0, typeof( SteelIngot ), "steel ingot", 2, 1053098 );
			AddCraft( typeof( SteelRing ), 1044049, "steel ring", 40.0, 90.0, typeof( SteelIngot ), "steel ingot", 2, 1053098 );
			AddCraft( typeof( SteelEarrings ), 1044049, "steel earrings", 40.0, 90.0, typeof( SteelIngot ), "steel ingot", 2, 1053098 );
			AddCraft( typeof( TopazAmulet ), 1044049, "topaz amulet", 40.0, 90.0, typeof( TopazIngot ), "topaz ingot", 2, 1053098 );
			AddCraft( typeof( TopazBracelet ), 1044049, "topaz bracelet", 40.0, 90.0, typeof( TopazIngot ), "topaz ingot", 2, 1053098 );
			AddCraft( typeof( TopazRing ), 1044049, "topaz ring", 40.0, 90.0, typeof( TopazIngot ), "topaz ingot", 2, 1053098 );
			AddCraft( typeof( TopazEarrings ), 1044049, "topaz earrings", 40.0, 90.0, typeof( TopazIngot ), "topaz ingot", 2, 1053098 );
			index = AddCraft( typeof( TourmalineAmulet ), 1044049, "tourmaline amulet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Tourmaline ), "tourmaline", 1, 1053098 );
			index = AddCraft( typeof( TourmalineBracelet ), 1044049, "tourmaline bracelet", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Tourmaline ), "tourmaline", 1, 1053098 );
			index = AddCraft( typeof( TourmalineRing ), 1044049, "tourmaline ring", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Tourmaline ), "tourmaline", 1, 1053098 );
			index = AddCraft( typeof( TourmalineEarrings ), 1044049, "tourmaline earrings", 40.0, 90.0, typeof( IronIngot ), "iron ingot", 1, 1053098 );
				AddRes( index, typeof( Tourmaline ), "tourmaline", 1, 1053098 );
			AddCraft( typeof( ValoriteAmulet ), 1044049, "valorite amulet", 40.0, 90.0, typeof( ValoriteIngot ), "valorite ingot", 2, 1053098 );
			AddCraft( typeof( ValoriteBracelet ), 1044049, "valorite bracelet", 40.0, 90.0, typeof( ValoriteIngot ), "valorite ingot", 2, 1053098 );
			AddCraft( typeof( ValoriteRing ), 1044049, "valorite ring", 40.0, 90.0, typeof( ValoriteIngot ), "valorite ingot", 2, 1053098 );
			AddCraft( typeof( ValoriteEarrings ), 1044049, "valorite earrings", 40.0, 90.0, typeof( ValoriteIngot ), "valorite ingot", 2, 1053098 );
			AddCraft( typeof( VeriteAmulet ), 1044049, "verite amulet", 40.0, 90.0, typeof( VeriteIngot ), "verite ingot", 2, 1053098 );
			AddCraft( typeof( VeriteBracelet ), 1044049, "verite bracelet", 40.0, 90.0, typeof( VeriteIngot ), "verite ingot", 2, 1053098 );
			AddCraft( typeof( VeriteRing ), 1044049, "verite ring", 40.0, 90.0, typeof( VeriteIngot ), "verite ingot", 2, 1053098 );
			AddCraft( typeof( VeriteEarrings ), 1044049, "verite earrings", 40.0, 90.0, typeof( VeriteIngot ), "verite ingot", 2, 1053098 );

			#endregion

			#region Multi-Component Items
			// Assemblies
			index = AddCraft( typeof( AxleGears ), 1044051, 1024177, 0.0, 0.0, typeof( Axle ), 1044169, 1, 1044253 );
			AddRes( index, typeof( Gears ), 1044254, 1, 1044253 );

			index = AddCraft( typeof( ClockParts ), 1044051, 1024175, 0.0, 0.0, typeof( AxleGears ), 1044170, 1, 1044253 );
			AddRes( index, typeof( Springs ), 1044171, 1, 1044253 );

			index = AddCraft( typeof( SextantParts ), 1044051, 1024185, 0.0, 0.0, typeof( AxleGears ), 1044170, 1, 1044253 );
			AddRes( index, typeof( Hinge ), 1044172, 1, 1044253 );

			index = AddCraft( typeof( ClockRight ), 1044051, 1044257, 0.0, 0.0, typeof( ClockFrame ), 1044174, 1, 1044253 );
			AddRes( index, typeof( ClockParts ), 1044173, 1, 1044253 );

			index = AddCraft( typeof( ClockLeft ), 1044051, 1044256, 0.0, 0.0, typeof( ClockFrame ), 1044174, 1, 1044253 );
			AddRes( index, typeof( ClockParts ), 1044173, 1, 1044253 );

			AddCraft( typeof( Sextant ), 1044051, 1024183, 0.0, 0.0, typeof( SextantParts ), 1044175, 1, 1044253 );

			index = AddCraft( typeof( Bola ), 1044051, 1046441, 60.0, 80.0, typeof( BolaBall ), 1046440, 4, 1042613 );
			AddRes( index, typeof( Leather ), 1044462, 3, 1044463 );

            index = AddCraft(typeof(PotionKeg), 1044051, 1044258, 75.0, 100.0, typeof(Keg), "Keg", 1, 1044253);
            AddRes(index, typeof(Bottle), 1044250, 10, 1044253);
            AddRes(index, typeof(BarrelLid), 1044251, 1, 1044253);
            AddRes(index, typeof(BarrelTap), 1044252, 1, 1044253);

			index = AddCraft( typeof( LockpickingChest10 ), 1044051, "lockpicking chest 10", 75.0, 100.0, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 1, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

			index = AddCraft( typeof( LockpickingChest20 ), 1044051, "lockpicking chest 20", 80.0, 100.0, typeof( IronIngot ), 1044036, 15, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 3, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

			index = AddCraft( typeof( LockpickingChest30 ), 1044051, "lockpicking chest 30", 82.5, 100.0, typeof( DullCopperIngot ), 1074916, 10, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 4, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

			index = AddCraft( typeof( LockpickingChest40 ), 1044051, "lockpicking chest 40", 85.0, 100.0, typeof( ShadowIronIngot ), 1074917, 25, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 5, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

			index = AddCraft( typeof( LockpickingChest50 ), 1044051, "lockpicking chest 50", 87.5, 100.0, typeof( BronzeIngot ), 1074919, 50, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 8, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

			index = AddCraft( typeof( LockpickingChest60 ), 1044051, "lockpicking chest 60", 90.0, 100.0, typeof( GoldIngot ), 1074920, 100, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 10, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

			index = AddCraft( typeof( LockpickingChest70 ), 1044051, "lockpicking chest 70", 92.5, 100.0, typeof( AgapiteIngot ), 1074921, 250, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 15, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

			index = AddCraft( typeof( LockpickingChest80 ), 1044051, "lockpicking chest 80", 95.0, 100.0, typeof( VeriteIngot ), 1074922, 500, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 30, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

			index = AddCraft( typeof( LockpickingChest90 ), 1044051, "lockpicking chest 90", 97.5, 100.0, typeof( ValoriteIngot ), 1074923, 1000, 1044037 );
			AddRes( index, typeof( ArcaneGem ), 1114115, 45, 1044253 );
			AddRes( index, typeof( WoodenBox ), "Wooden Box", 1, 1044253 );

            // Hospitality
            index = AddCraft(typeof(AleBarrel), "Hospitality", "ale barrel", 60.0, 100.0, typeof(ArcaneGem), 1114115, 1, 1044253);
            AddRes(index, typeof(Keg), "Keg", 2, 1044253);
            AddRes(index, typeof(AxleGears), 1044170, 4, 1044253);

            index = AddCraft(typeof(CheesePress), "Hospitality", "cheese press", 60.0, 100.0, typeof(ArcaneGem), 1114115, 2, 1044253);
            AddRes(index, typeof(Keg), "Keg", 2, 1044253);
            AddRes(index, typeof(AxleGears), 1044170, 4, 1044253);

            index = AddCraft(typeof(CiderBarrel), "Hospitality", "cider barrel", 60.0, 100.0, typeof(ArcaneGem), 1114115, 1, 1044253);
            AddRes(index, typeof(Keg), "Keg", 2, 1044253);
            AddRes(index, typeof(AxleGears), 1044170, 4, 1044253);

            index = AddCraft(typeof(LiquorBarrel), "Hospitality", "liquor barrel", 95.0, 120.0, typeof(ArcaneGem), 1114115, 4, 1044253);
            AddRes(index, typeof(Keg), "Keg", 2, 1044253);
            AddRes(index, typeof(AxleGears), 1044170, 4, 1044253);

            index = AddCraft(typeof(WineBarrel), "Hospitality", "wine barrel", 80.0, 100.0, typeof(ArcaneGem), 1114115, 2, 1044253);
            AddRes(index, typeof(Keg), "Keg", 2, 1044253);
            AddRes(index, typeof(AxleGears), 1044170, 4, 1044253);

            #endregion // REMOVED THE FACTION TRAPS WIZARD

            // Set the overridable material
            SetSubRes( typeof( IronIngot ), 1044022 );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes( typeof( IronIngot ),			1044022, 00.0, 1044036, 1044267 );
			AddSubRes( typeof( DullCopperIngot ),	1044023, 65.0, 1044036, 1044268 );
			AddSubRes( typeof( ShadowIronIngot ),	1044024, 70.0, 1044036, 1044268 );
			AddSubRes( typeof( CopperIngot ),		1044025, 75.0, 1044036, 1044268 );
			AddSubRes( typeof( BronzeIngot ),		1044026, 80.0, 1044036, 1044268 );
			AddSubRes( typeof( GoldIngot ),			1044027, 85.0, 1044036, 1044268 );
			AddSubRes( typeof( AgapiteIngot ),		1044028, 90.0, 1044036, 1044268 );
			AddSubRes( typeof( VeriteIngot ),		1044029, 95.0, 1044036, 1044268 );
			AddSubRes( typeof( ValoriteIngot ),		1044030, 99.0, 1044036, 1044268 );
			AddSubRes( typeof( NepturiteIngot ),	1036173, 99.0, 1044036, 1044268 );
			AddSubRes( typeof( ObsidianIngot ),		1036162, 99.0, 1044036, 1044268 );
			AddSubRes( typeof( SteelIngot ),		1036144, 99.0, 1044036, 1044268 );
			AddSubRes( typeof( BrassIngot ),		1036152, 105.0, 1044036, 1044268 );
			AddSubRes( typeof( MithrilIngot ),		1036137, 110.0, 1044036, 1044268 );
			AddSubRes( typeof( XormiteIngot ),		1034437, 115.0, 1044036, 1044268 );
			AddSubRes( typeof( DwarvenIngot ),		1036181, 120.0, 1044036, 1044268 );

			MarkOption = true;
			Repair = true;
			CanEnhance = Core.AOS;
		}
	}

	public abstract class TrapCraft : CustomCraft
	{
		private LockableContainer m_Container;

		public LockableContainer Container{ get{ return m_Container; } }

		public abstract TrapType TrapType{ get; }

		public TrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}

		private int Verify( LockableContainer container )
		{
			if ( container == null || container.KeyValue == 0 )
				return 1005638; // You can only trap lockable chests.
			if ( From.Map != container.Map || !From.InRange( container.GetWorldLocation(), 2 ) )
				return 500446; // That is too far away.
			if ( !container.Movable )
				return 502944; // You cannot trap this item because it is locked down.
			if ( !container.IsAccessibleTo( From ) )
				return 502946; // That belongs to someone else.
			if ( container.Locked )
				return 502943; // You can only trap an unlocked object.
			if ( container.TrapType != TrapType.None )
				return 502945; // You can only place one trap on an object at a time.

			return 0;
		}

		private bool Acquire( object target, out int message )
		{
			LockableContainer container = target as LockableContainer;

			message = Verify( container );

			if ( message > 0 )
			{
				return false;
			}
			else
			{
				m_Container = container;
				return true;
			}
		}

		public override void EndCraftAction()
		{
			From.SendLocalizedMessage( 502921 ); // What would you like to set a trap on?
			From.Target = new ContainerTarget( this );
		}

		private class ContainerTarget : Target
		{
			private TrapCraft m_TrapCraft;

			public ContainerTarget( TrapCraft trapCraft ) : base( -1, false, TargetFlags.None )
			{
				m_TrapCraft = trapCraft;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int message;

				if ( m_TrapCraft.Acquire( targeted, out message ) )
					m_TrapCraft.CraftItem.CompleteCraft( m_TrapCraft.Quality, false, m_TrapCraft.From, m_TrapCraft.CraftSystem, m_TrapCraft.TypeRes, m_TrapCraft.Tool, m_TrapCraft );
				else
					Failure( message );
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
			{
				if ( cancelType == TargetCancelType.Canceled )
					Failure( 0 );
			}

			private void Failure( int message )
			{
				Mobile from = m_TrapCraft.From;
				BaseTool tool = m_TrapCraft.Tool;

				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new CraftGump( from, m_TrapCraft.CraftSystem, tool, message ) );
				else if ( message > 0 )
					from.SendLocalizedMessage( message );
			}
		}

		public override Item CompleteCraft( out int message )
		{
			message = Verify( this.Container );

			if ( message == 0 )
			{
				int trapLevel = (int)(From.Skills.Tinkering.Value / 10);

				Container.TrapType = this.TrapType;
				Container.TrapPower = trapLevel * 9;
				Container.TrapLevel = trapLevel;
				Container.TrapOnLockpick = true;

				message = 1005639; // Trap is disabled until you lock the chest.
			}

			return null;
		}
	}

	[CraftItemID( 0x1BFC )]
	public class DartTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.DartTrap; } }

		public DartTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	[CraftItemID( 0x113E )]
	public class PoisonTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.PoisonTrap; } }

		public PoisonTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	[CraftItemID( 0x370C )]
	public class ExplosionTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.ExplosionTrap; } }

		public ExplosionTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}
}