using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefCarpentry : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Carpentry;	}
		}

		public override int GumpTitleNumber
		{
			get { return 1044004; } // <CENTER>CARPENTRY MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefCarpentry();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefCarpentry() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );

			from.PlaySound( 0x23D );
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

		public override void InitCraftList()
		{
			int index = -1;

			//index =	AddCraft( typeof( Board ),			1044294, 1027127,	0.0,   0.0,	typeof( BaseLog ), 1044466,  1, 1044465 );
			//SetUseAllRes( index, true );

			AddCraft( typeof( Kindling ), 1044294, 1023553, 0.0, 00.0, typeof( Log ), 1015101, 1, 1044351 );

			index = AddCraft( typeof( Kindling ), 1044294, "batch of kindling", 0.0, 00.0, typeof( Log ), 1015101, 1, 1044351 );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( BarkFragment ),	1044294, "bark fragment",	40.0, 70.0, typeof( Board ), 1015101, 1, 1044465 );
			SetUseAllRes( index, true );

			AddCraft( typeof( BarrelStaves ),				1044294, 1027857,	00.0,  25.0,	typeof( Board ), 1015101,  5, 1044351 );
			AddCraft( typeof( BarrelLid ),					1044294, 1027608,	11.0,  36.0,	typeof( Board ), 1015101,  4, 1044351 );
			AddCraft( typeof( ShortMusicStand ),			1044294, 1044313,	78.9, 103.9,	typeof( Board ), 1015101, 15, 1044351 );
			AddCraft( typeof( TallMusicStand ),				1044294, 1044315,	81.5, 106.5,	typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( Easle ),						1044294, 1044317,	86.8, 111.8,	typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( MixingSpoon ),				1044294, "cauldron mixer",	30.0,  60.0,	typeof( Board ), 1015101,  5, 1044351 );

				index = AddCraft( typeof( RedHangingLantern ), 1044294, 1029412, 65.0, 90.0, typeof( Board ), 1015101, 5, 1044351 );
				AddRes( index, typeof( BlankScroll ), 1044377, 10, 1044378 );
				 

				index = AddCraft( typeof( WhiteHangingLantern ), 1044294, 1029416, 65.0, 90.0, typeof( Board ), 1015101, 5, 1044351 );
				AddRes( index, typeof( BlankScroll ), 1044377, 10, 1044378 );
				 

				index = AddCraft( typeof( ShojiScreen ), 1044294, 1029423, 80.0, 105.0, typeof( Board ), 1015101, 75, 1044351 );
				AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
				AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
				 

				index = AddCraft( typeof( BambooScreen ), 1044294, 1029428, 80.0, 105.0, typeof( Board ), 1015101, 75, 1044351 );
				AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
				AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
				 
				index = AddCraft( typeof( FishingPole ), 1044294, 1023519, 68.4, 93.4, typeof( Board ), 1015101, 5, 1044351 ); //This is in the categor of Other during AoS
				AddSkill( index, SkillName.Tailoring, 40.0, 45.0 );
				AddRes( index, typeof( Cloth ), 1044286, 5, 1044287 );

			// Furniture
			AddCraft( typeof( FootStool ),					1044291, 1022910,	11.0,  36.0,	typeof( Board ), 1015101,  9, 1044351 );
			AddCraft( typeof( Stool ),						1044291, 1022602,	11.0,  36.0,	typeof( Board ), 1015101,  9, 1044351 );
			AddCraft( typeof( BambooChair ),				1044291, 1044300,	21.0,  46.0,	typeof( Board ), 1015101, 13, 1044351 );
			AddCraft( typeof( WoodenChair ),				1044291, 1044301,	21.0,  46.0,	typeof( Board ), 1015101, 13, 1044351 );
			AddCraft( typeof( FancyWoodenChairCushion ),	1044291, 1044302,	42.1,  67.1,	typeof( Board ), 1015101, 15, 1044351 );
			AddCraft( typeof( WoodenChairCushion ),			1044291, 1044303,	42.1,  67.1,	typeof( Board ), 1015101, 13, 1044351 );
			AddCraft( typeof( WoodenBench ),				1044291, 1022860,	52.6,  77.6,	typeof( Board ), 1015101, 17, 1044351 );
			AddCraft( typeof( WoodenThrone ),				1044291, 1044304,	52.6,  77.6,	typeof( Board ), 1015101, 17, 1044351 );
			AddCraft( typeof( Throne ),						1044291, 1044305,	73.6,  98.6,	typeof( Board ), 1015101, 19, 1044351 );
			AddCraft( typeof( Nightstand ),					1044291, 1044306,	42.1,  67.1,	typeof( Board ), 1015101, 17, 1044351 );
			AddCraft( typeof( WritingTable ),				1044291, 1022890,	63.1,  88.1,	typeof( Board ), 1015101, 17, 1044351 );
			AddCraft( typeof( YewWoodTable ),				1044291, 1044307,	63.1,  88.1,	typeof( Board ), 1015101, 23, 1044351 );
			AddCraft( typeof( LargeTable ),					1044291, 1044308,	84.2, 109.2,	typeof( Board ), 1015101, 27, 1044351 );

				index = AddCraft( typeof( ElegantLowTable ),	1044291, 1030265,	80.0, 105.0,	typeof( Log ), 1044041, 35, 1044351 );

				index = AddCraft( typeof( PlainLowTable ),		1044291, 1030266,	80.0, 105.0,	typeof( Log ), 1044041, 35, 1044351 );

				index = AddCraft( typeof( OrnateElvenChair ), 1044291, 1072870, 80.0, 105.0, typeof( Log ), 1044041, 30, 1044351 );

			AddCraft( typeof( WoodenBox ),					1044292, 1023709,	21.0,  46.0,	typeof( Log ), 1044041, 10, 1044351 );
			AddCraft( typeof( SmallCrate ),					1044292, 1044309,	10.0,  35.0,	typeof( Log ), 1044041, 8 , 1044351 );
			AddCraft( typeof( MediumCrate ),				1044292, 1044310,	31.0,  56.0,	typeof( Log ), 1044041, 15, 1044351 );
			AddCraft( typeof( LargeCrate ),					1044292, 1044311,	47.3,  72.3,	typeof( Log ), 1044041, 18, 1044351 );
			AddCraft( typeof( WoodenChest ),				1044292, 1023650,	73.6,  98.6,	typeof( Log ), 1044041, 20, 1044351 );
			AddCraft( typeof( EmptyBookcase ),				1044292, 1022718,	31.5,  56.5,	typeof( Log ), 1044041, 25, 1044351 );
			AddCraft( typeof( FancyArmoire ),				1044292, 1044312,	84.2, 109.2,	typeof( Log ), 1044041, 35, 1044351 );
			AddCraft( typeof( Armoire ),					1044292, 1022643,	84.2, 109.2,	typeof( Log ), 1044041, 35, 1044351 );

			index = AddCraft( typeof( ElegantLowTable ),	1044291, 1030265,	80.0, 105.0,	typeof( Board ), 1015101, 35, 1044351 );
			 

			index = AddCraft( typeof( PlainLowTable ),		1044291, 1030266,	80.0, 105.0,	typeof( Board ), 1015101, 35, 1044351 );
			 

				index = AddCraft( typeof( PlainWoodenChest ),	1044292, 1030251, 90.0, 115.0,	typeof( Log ), 1044041, 30, 1044351 );

				index = AddCraft( typeof( FinishedWoodenChest ),1044292, 1030259, 90.0, 115.0,	typeof( Log ), 1044041, 30, 1044351 );

				index = AddCraft( typeof( TallCabinet ),	1044292, 1030261, 90.0, 115.0,	typeof( Log ), 1044041, 35, 1044351 );


				index = AddCraft( typeof( ShortCabinet ),	1044292, 1030263, 90.0, 115.0,	typeof( Log ), 1044041, 35, 1044351 );

			

			// Barrels
			index = AddCraft( typeof( Keg ), "Barrels", 1023711, 57.8, 82.8, typeof( BarrelStaves ), 1044288, 3, 1044253 );
			AddRes( index, typeof( BarrelHoops ), 1044289, 1, 1044253 );
			AddRes( index, typeof( BarrelLid ), 1044251, 1, 1044253 );

			index = AddCraft( typeof( AlchemyTub ), "Barrels", "alchemy tub", 87.8, 102.8, typeof( BarrelStaves ), 1044288, 3, 1044253 );
			AddRes( index, typeof( BarrelHoops ), 1044289, 1, 1044253 );

			// Staves and Shields
			AddCraft( typeof( WildStaff ), 1044295, "druid staff", 78.9, 103.9, typeof( Board ), 1015101, 7, 1044351 );
			AddCraft( typeof( ShepherdsCrook ), 1044295, 1023713, 78.9, 103.9, typeof( Board ), 1015101, 7, 1044351 );
			AddCraft( typeof( QuarterStaff ), 1044295, 1023721, 73.6, 98.6, typeof( Board ), 1015101, 6, 1044351 );
			AddCraft( typeof( GnarledStaff ), 1044295, 1025112, 78.9, 103.9, typeof( Board ), 1015101, 7, 1044351 );
			AddCraft( typeof( WoodenShield ), 1044295, 1027034, 52.6, 77.6, typeof( Board ), 1015101, 9, 1044351 );

				index = AddCraft( typeof( Bokuto ), 1044295, 1030227, 70.0, 95.0, typeof( Board ), 1015101, 6, 1044351 );
				 

				index = AddCraft( typeof( Fukiya ), 1044295, 1030229, 60.0, 85.0, typeof( Board ), 1015101, 6, 1044351 );
				 

				index = AddCraft( typeof( Tetsubo ), 1044295, 1030225, 80.0, 140.3, typeof( Board ), 1015101, 10, 1044351 );

			index = AddCraft( typeof( WoodenPlateArms ), 1044295, "wooden arms", 66.3, 116.3, typeof( ReaperOil ), "Reaper Oil", 2, 1042081 );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 2, 1042081 );
			AddRes( index, typeof( Board ), 1015101, 18, 1044351 );
			index = AddCraft( typeof( WoodenPlateHelm ), 1044295, "wooden helm", 62.6, 112.6, typeof( ReaperOil ), "Reaper Oil", 1, 1042081 );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 1, 1042081 );
			AddRes( index, typeof( Board ), 1015101, 15, 1044351 );
			index = AddCraft( typeof( WoodenPlateGloves ), 1044295, "wooden gauntlets", 58.9, 108.9, typeof( ReaperOil ), "Reaper Oil", 1, 1042081 );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 1, 1042081 );
			AddRes( index, typeof( Board ), 1015101, 12, 1044351 );
			index = AddCraft( typeof( WoodenPlateGorget ), 1044295, "wooden gorget", 56.4, 106.4, typeof( ReaperOil ), "Reaper Oil", 1, 1042081 );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 1, 1042081 );
			AddRes( index, typeof( Board ), 1015101, 10, 1044351 );
			index = AddCraft( typeof( WoodenPlateLegs ), 1044295, "wooden leggings", 68.8, 118.8, typeof( ReaperOil ), "Reaper Oil", 3, 1042081 );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 3, 1042081 );
			AddRes( index, typeof( Board ), 1015101, 20, 1044351 );
			index = AddCraft( typeof( WoodenPlateChest ), 1044295, "wooden tunic", 75.0, 125.0, typeof( ReaperOil ), "Reaper Oil", 3, 1042081 );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 3, 1042081 );
			AddRes( index, typeof( Board ), 1015101, 25, 1044351 );

			// Instruments
			index = AddCraft( typeof( LapHarp ), 1044293, 1023762, 63.1, 88.1, typeof( Board ), 1015101, 20, 1044351 );
			AddSkill( index, SkillName.Musicianship, 45.0, 50.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 5, 1044037 );

			index = AddCraft( typeof( Harp ), 1044293, 1023761, 78.9, 103.9, typeof( Board ), 1015101, 35, 1044351 );
			AddSkill( index, SkillName.Musicianship, 45.0, 50.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );

			index = AddCraft( typeof( Drums ), 1044293, 1023740, 57.8, 82.8, typeof( Board ), 1015101, 20, 1044351 );
			AddSkill( index, SkillName.Musicianship, 45.0, 50.0 );
			AddRes( index, typeof( Cloth ), 1044286, 10, 1044287 );

			index = AddCraft( typeof( Lute ), 1044293, 1023763, 68.4, 93.4, typeof( Board ), 1015101, 25, 1044351 );
			AddSkill( index, SkillName.Musicianship, 45.0, 50.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 5, 1044037 );

			index = AddCraft( typeof( Tambourine ), 1044293, 1023741, 57.8, 82.8, typeof( Board ), 1015101, 15, 1044351 );
			AddSkill( index, SkillName.Musicianship, 45.0, 50.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 5, 1044037 );

			index = AddCraft( typeof( TambourineTassel ), 1044293, 1044320, 57.8, 82.8, typeof( Board ), 1015101, 15, 1044351 );
			AddSkill( index, SkillName.Musicianship, 45.0, 50.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 5, 1044037 );
			AddRes( index, typeof( Cloth ), 1044286, 5, 1044287 );

			index = AddCraft( typeof( BambooFlute ), 1044293, 1030247, 80.0, 105.0, typeof( Board ), 1015101, 15, 1044351 );
			AddSkill( index, SkillName.Musicianship, 45.0, 50.0 );
			 

			// Misc
			/*index = AddCraft( typeof( SmallBedSouthDeed ), 1044290, 1044321, 94.7, 119.8, typeof( Board ), 1015101, 100, 1044351 );
			AddSkill( index, SkillName.Tailoring, 75.0, 80.0 );
			AddRes( index, typeof( Cloth ), 1044286, 100, 1044287 );
			index = AddCraft(typeof(SmallBedEastDeed), 1044290, 1044322, 94.7, 119.8, typeof(Log), 1015101, 100, 1044351);
			AddSkill( index, SkillName.Tailoring, 75.0, 80.0 );
			AddRes( index, typeof( Cloth ), 1044286, 100, 1044287 );
			index = AddCraft(typeof(LargeBedSouthDeed), 1044290, 1044323, 94.7, 119.8, typeof(Log), 1015101, 150, 1044351);
			AddSkill( index, SkillName.Tailoring, 75.0, 80.0 );
			AddRes( index, typeof( Cloth ), 1044286, 150, 1044287 );
			index = AddCraft(typeof(LargeBedEastDeed), 1044290, 1044324, 60.7, 90.8, typeof(Log), 1015101, 150, 1044351);
			AddSkill( index, SkillName.Tailoring, 75.0, 80.0 );
			AddRes( index, typeof( Cloth ), 1044286, 150, 1044287 );*/
			//beds 
			index = AddCraft( typeof( SleeperSmallSouthAddonDeed ), 1044290, "sleeper small east", 30.0, 60.0, typeof( Board ), 1015101, 100, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 40, "cloth" );
			index = AddCraft( typeof( SleeperSmallEWAddonDeed ), 1044290, "sleeper small south", 30.0, 60.0, typeof( Board ), 1015101, 100, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 40, "cloth" );
			index = AddCraft( typeof( SleeperFutonEWAddonDeed ), 1044290, "sleeper futon east", 40.0, 70.0, typeof( Board ), 1015101, 10, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 200, "cloth" );
			index = AddCraft( typeof( SleeperFutonNSAddonDeed ), 1044290, "sleeper futon south", 40.0, 70.0, typeof( Board ), 1015101, 10, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 200, "cloth" );
			index = AddCraft( typeof( SleeperEWAddonDeed ), 1044290, "sleeper bed east", 60.0, 90.0, typeof( Board ), 1015101, 200, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 80, "cloth" );
			index = AddCraft( typeof( SleeperNSAddonDeed ), 1044290, "sleeper bed south", 60.0, 90.0, typeof( Board ), 1015101, 200, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 80, "cloth" );
			index = AddCraft( typeof( SleeperElvenEWAddonDeed ), 1044290, "sleeper elven east", 70.0, 100.0, typeof( Board ), 1015101, 500, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 150, "cloth" );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 5, 1042081 );
			AddRes( index, typeof( IronIngot ), 1044036, 20, 1044037 );
			index = AddCraft( typeof( SleeperElvenSouthAddonDeed ), 1044290, "sleeper elven south", 70.0, 100.0, typeof( Board ), 1015101, 500, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 150, "cloth" );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 5, 1042081 );
			AddRes( index, typeof( IronIngot ), 1044036, 20, 1044037 );
			index = AddCraft( typeof( SleeperTallElvenEastAddonDeed ), 1044290, "sleeper tall elven east", 90.0, 119.0, typeof( Board ), 1015101, 1000, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 300, "cloth" );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 10, 1042081 );
			AddRes( index, typeof( IronIngot ), 1044036, 50, 1044037 );
			index = AddCraft( typeof( SleeperTallElvenSouthAddonDeed ), 1044290, "sleeper tall elven south", 90.0, 119.0, typeof( Board ), 1015101, 1000, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 300, "cloth" );
			AddRes( index, typeof( MysticalTreeSap ), "Mystical Tree Sap", 10, 1042081 );
			AddRes( index, typeof( IronIngot ), 1044036, 50, 1044037 );

			AddCraft( typeof( DartBoardSouthDeed ), 1044290, 1044325, 15.7, 40.7, typeof( Board ), 1015101, 5, 1044351 );
			AddCraft( typeof( DartBoardEastDeed ), 1044290, 1044326, 15.7, 40.7, typeof( Board ), 1015101, 5, 1044351 );
			AddCraft( typeof( BallotBoxDeed ), 1044290, 1044327, 47.3, 72.3, typeof( Board ), 1015101, 5, 1044351 );
			index = AddCraft( typeof( PentagramDeed ), 1044290, 1044328, 100.0, 125.0, typeof( Board ), 1015101, 100, 1044351 );
			AddSkill( index, SkillName.Magery, 75.0, 80.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 40, 1044037 );
			index = AddCraft( typeof( AbbatoirDeed ), 1044290, 1044329, 100.0, 125.0, typeof( Board ), 1015101, 100, 1044351 );
			AddSkill( index, SkillName.Magery, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 40, 1044037 );

			index = AddCraft( typeof( AbbatoirDeed ), 1044290, 1044329, 100.0, 125.0, typeof( Board ), 1015101, 100, 1044351 );
			AddSkill( index, SkillName.Magery, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 40, 1044037 );

				AddCraft( typeof( PlayerBBEast ), 1044290, 1062420, 85.0, 110.0, typeof( Board ), 1015101, 50, 1044351 );
				AddCraft( typeof( PlayerBBSouth ), 1044290, 1062421, 85.0, 110.0, typeof( Board ), 1015101, 50, 1044351 );

			// Blacksmithy
			index = AddCraft( typeof( SmallForgeDeed ), 1044296, 1044330, 73.6, 98.6, typeof( Board ), 1015101, 5, 1044351 );
			AddSkill( index, SkillName.Blacksmith, 75.0, 80.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 75, 1044037 );
			index = AddCraft( typeof( LargeForgeEastDeed ), 1044296, 1044331, 78.9, 103.9, typeof( Board ), 1015101, 5, 1044351 );
			AddSkill( index, SkillName.Blacksmith, 80.0, 85.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 100, 1044037 );
			index = AddCraft( typeof( LargeForgeSouthDeed ), 1044296, 1044332, 78.9, 103.9, typeof( Board ), 1015101, 5, 1044351 );
			AddSkill( index, SkillName.Blacksmith, 80.0, 85.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 100, 1044037 );
			index = AddCraft( typeof( AnvilEastDeed ), 1044296, 1044333, 73.6, 98.6, typeof( Board ), 1015101, 5, 1044351 );
			AddSkill( index, SkillName.Blacksmith, 75.0, 80.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 150, 1044037 );
			index = AddCraft( typeof( AnvilSouthDeed ), 1044296, 1044334, 73.6, 98.6, typeof( Board ), 1015101, 5, 1044351 );
			AddSkill( index, SkillName.Blacksmith, 75.0, 80.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 150, 1044037 );

			// Training
			index = AddCraft( typeof( TrainingDummyEastDeed ), 1044297, 1044335, 68.4, 93.4, typeof( Board ), 1015101, 55, 1044351 );
			AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
			AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
			index = AddCraft( typeof( TrainingDummySouthDeed ), 1044297, 1044336, 68.4, 93.4, typeof( Board ), 1015101, 55, 1044351 );
			AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
			AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
			index = AddCraft( typeof( PickpocketDipEastDeed ), 1044297, 1044337, 73.6, 98.6, typeof( Board ), 1015101, 65, 1044351 );
			AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
			AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
			index = AddCraft( typeof( PickpocketDipSouthDeed ), 1044297, 1044338, 73.6, 98.6, typeof( Board ), 1015101, 65, 1044351 );
			AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
			AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );

			// Tailoring
			index = AddCraft( typeof( Dressform ), 1002155, 1044339, 63.1, 88.1, typeof( Board ), 1015101, 25, 1044351 );
			AddSkill( index, SkillName.Tailoring, 65.0, 70.0 );
			AddRes( index, typeof( Cloth ), 1044286, 10, 1044287 );
			index = AddCraft( typeof( SpinningwheelEastDeed ), 1002155, 1044341, 73.6, 98.6, typeof( Board ), 1015101, 75, 1044351 );
			AddSkill( index, SkillName.Tailoring, 65.0, 70.0 );
			AddRes( index, typeof( Cloth ), 1044286, 25, 1044287 );
			index = AddCraft( typeof( SpinningwheelSouthDeed ), 1002155, 1044342, 73.6, 98.6, typeof( Board ), 1015101, 75, 1044351 );
			AddSkill( index, SkillName.Tailoring, 65.0, 70.0 );
			AddRes( index, typeof( Cloth ), 1044286, 25, 1044287 );
			index = AddCraft( typeof( LoomEastDeed ), 1002155, 1044343, 84.2, 109.2, typeof( Board ), 1015101, 85, 1044351 );
			AddSkill( index, SkillName.Tailoring, 65.0, 70.0 );
			AddRes( index, typeof( Cloth ), 1044286, 25, 1044287 );
			index = AddCraft( typeof( LoomSouthDeed ), 1002155, 1044344, 84.2, 109.2, typeof( Board ), 1015101, 85, 1044351 );
			AddSkill( index, SkillName.Tailoring, 65.0, 70.0 );
			AddRes( index, typeof( Cloth ), 1044286, 25, 1044287 );

			// Cooking
			index = AddCraft( typeof( StoneOvenEastDeed ), 1044299, 1044345, 68.4, 93.4, typeof( Board ), 1015101, 85, 1044351 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 125, 1044037 );
			index = AddCraft( typeof( StoneOvenSouthDeed ), 1044299, 1044346, 68.4, 93.4, typeof( Board ), 1015101, 85, 1044351 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 125, 1044037 );
			index = AddCraft( typeof( FlourMillEastDeed ), 1044299, 1044347, 94.7, 119.7, typeof( Board ), 1015101, 100, 1044351 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 50, 1044037 );
			index = AddCraft( typeof( FlourMillSouthDeed ), 1044299, 1044348, 94.7, 119.7, typeof( Board ), 1015101, 100, 1044351 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 50, 1044037 );
			AddCraft( typeof( WaterTroughEastDeed ), 1044299, 1044349, 94.7, 119.7, typeof( Board ), 1015101, 150, 1044351 );
			AddCraft( typeof( WaterTroughSouthDeed ), 1044299, 1044350, 94.7, 119.7, typeof( Board ), 1015101, 150, 1044351 );

			Repair = true;
			MarkOption = true;
			CanEnhance = Core.AOS;

			SetSubRes( typeof( Board ), 1072643 );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material	TODO: Verify the required skill amount
			AddSubRes( typeof( Board ), 1072643, 00.0, 1015101, 1072652 );
			AddSubRes( typeof( AshBoard ), 1095379, 65.0, 1015101, 1072652 );
			AddSubRes( typeof( CherryBoard ), 1095380, 70.0, 1015101, 1072652 );
			AddSubRes( typeof( EbonyBoard ), 1095381, 75.0, 1015101, 1072652 );
			AddSubRes( typeof( GoldenOakBoard ), 1095382, 80.0, 1015101, 1072652 );
			AddSubRes( typeof( HickoryBoard ), 1095383, 85.0, 1015101, 1072652 );
			AddSubRes( typeof( MahoganyBoard ), 1095384, 90.0, 1015101, 1072652 );
			AddSubRes( typeof( OakBoard ), 1095385, 95.0, 1015101, 1072652 );
			AddSubRes( typeof( PineBoard ), 1095386, 100.0, 1015101, 1072652 );
			AddSubRes( typeof( RosewoodBoard ), 1095387, 100.0, 1015101, 1072652 );
			AddSubRes( typeof( WalnutBoard ), 1095388, 100.0, 1015101, 1072652 );
			AddSubRes( typeof( DriftwoodBoard ), 1095409, 105.0, 1015101, 1072652 );
			AddSubRes( typeof( GhostBoard ), 1095511, 110.0, 1015101, 1072652 );
			AddSubRes( typeof( PetrifiedBoard ), 1095532, 115.0, 1015101, 1072652 );
			AddSubRes( typeof( ElvenBoard ), 1095535, 120.0, 1015101, 1072652 );
		}
	}
}
