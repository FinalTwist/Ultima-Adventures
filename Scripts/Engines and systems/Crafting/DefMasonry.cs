using System; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Engines.Craft 
{ 
	public class DefMasonry : CraftSystem 
	{ 
		public override SkillName MainSkill 
		{ 
			get{ return SkillName.Carpentry; } 
		} 

		public override int GumpTitleNumber 
		{ 
			get{ return 1044500; } // <CENTER>MASONRY MENU</CENTER> 
		} 

		private static CraftSystem m_CraftSystem; 

		public static CraftSystem CraftSystem 
		{ 
			get 
			{ 
				if ( m_CraftSystem == null ) 
					m_CraftSystem = new DefMasonry(); 

				return m_CraftSystem; 
			} 
		} 

		public override double GetChanceAtMin( CraftItem item ) 
		{ 
			return 0.0; // 0% 
		} 

		private DefMasonry() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 ) 
		{ 
		} 

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			return true;
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckTool( tool, from ) )
				return 1048146; // If you have a tool equipped, you must use that tool.
			else if ( !(from is PlayerMobile && ((PlayerMobile)from).Masonry && from.Skills[SkillName.Carpentry].Base >= 100.0) )
				return 1044633; // You havent learned stonecraft.
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		} 

		public override void PlayCraftEffect( Mobile from ) 
		{ 
			// no effects
			//if ( from.Body.Type == BodyType.Human && !from.Mounted ) 
			//	from.Animate( 9, 5, 1, true, false, 0 ); 
			//new InternalTimer( from ).Start();

			from.PlaySound( 0x65A );
		} 

		// Delay to synchronize the sound with the hit on the anvil 
		private class InternalTimer : Timer 
		{ 
			private Mobile m_From; 

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) ) 
			{ 
				m_From = from; 
			} 

			protected override void OnTick() 
			{ 
				m_From.PlaySound( 0x23D ); 
			} 
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

			// Containers

			index = AddCraft( typeof( StoneCoffin ), "Containers", "sarcophagus, woman", 90.0, 115.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddSkill( index, SkillName.Forensics, 75.0, 80.0 );
			index = AddCraft( typeof( StoneCasket ), "Containers", "sarcophagus, man", 90.0, 115.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddSkill( index, SkillName.Forensics, 75.0, 80.0 );
			index = AddCraft( typeof( RockUrn ), "Containers", "urn", 80.0, 105.0, typeof( Granite ), 1044514, 5, 1044513 );
			index = AddCraft( typeof( RockVase ), "Containers", "vase", 80.0, 105.0, typeof( Granite ), 1044514, 5, 1044513 );
			index = AddCraft( typeof( StoneOrnateUrn ), "Containers", "urn", 90.0, 110.0, typeof( Granite ), 1044514, 6, 1044513 );
			index = AddCraft( typeof( StoneOrnateTallVase ), "Containers", "vase", 95.0, 120.0, typeof( Granite ), 1044514, 8, 1044513 );

			// Decorations

			AddCraft( typeof( StoneVase ), "Decorations", "vase", 42.5, 92.5, typeof( Granite ), 1044514, 2, 1044513 );
			AddCraft( typeof( StoneLargeVase ), "Decorations", "vase, large", 52.5, 102.5, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( StoneAmphora ), "Decorations", "amphora", 42.5, 92.5, typeof( Granite ), 1044514, 2, 1044513 );
			AddCraft( typeof( StoneLargeAmphora ), "Decorations", "amphora, large", 52.5, 102.5, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( StoneOrnateVase ), "Decorations", "vase, ornate", 52.5, 102.5, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( StoneOrnateAmphora ), "Decorations", "amphora, ornate", 52.5, 102.5, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( StoneGargoyleVase ), "Decorations", "vase, gargoyle", 62.5, 112.5, typeof( Granite ), 1044514, 6, 1044513 );

			AddCraft( typeof( StoneBuddhistSculpture ), "Decorations", "sculpture, Buddhist", 62.5, 122.5, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StoneMingSculpture ), "Decorations", "sculpture, Ming", 52.5, 122.5, typeof( Granite ), 1044514, 6, 1044513 );
			AddCraft( typeof( StoneYuanSculpture ), "Decorations", "sculpture, Yuan", 52.5, 122.5, typeof( Granite ), 1044514, 6, 1044513 );
			AddCraft( typeof( StoneQinSculpture ), "Decorations", "sculpture, Qin", 52.5, 122.5, typeof( Granite ), 1044514, 6, 1044513 );

			AddCraft( typeof( StoneMingUrn ), "Decorations", "urn, Ming", 42.5, 92.5, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneQinUrn ), "Decorations", "urn, Qin", 42.5, 92.5, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneYuanUrn ), "Decorations", "urn, Yuan", 42.5, 92.5, typeof( Granite ), 1044514, 3, 1044513 );

			AddCraft( typeof( StoneChairs ), "Furniture", 1024635, 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( StoneBenchLong ), "Furniture", "bench, long", 55.0, 105.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StoneBenchShort ), "Furniture", "bench, short", 55.0, 105.0, typeof( Granite ), 1044514, 5, 1044513 );
			AddCraft( typeof( StoneTableLong ), "Furniture", "table, long", 65.0, 115.0, typeof( Granite ), 1044514, 12, 1044513 );
			AddCraft( typeof( StoneTableShort ), "Furniture", "table, short", 65.0, 115.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddCraft( typeof( StoneWizardTable ), "Furniture", "table, wizard", 95.0, 125.0, typeof( Granite ), 1044514, 15, 1044513 );

			AddCraft( typeof( StoneSteps ), "Furniture", "steps", 55.0, 105.0, typeof( Granite ), 1044514, 5, 1044513 );
			AddCraft( typeof( StoneBlock ), "Furniture", "block", 55.0, 105.0, typeof( Granite ), 1044514, 5, 1044513 );
			AddCraft( typeof( StoneSarcophagus ), "Furniture", "sarcophagus", 65.0, 125.0, typeof( Granite ), 1044514, 10, 1044513 );

			AddCraft( typeof( StoneColumn ), "Furniture", "column", 65.0, 125.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddCraft( typeof( StoneGothicColumn ), "Furniture", "column, gothic", 85.0, 135.0, typeof( Granite ), 1044514, 20, 1044513 );
			AddCraft( typeof( StonePedestal ), "Furniture", "pedestal", 65.0, 125.0, typeof( Granite ), 1044514, 5, 1044513 );
			AddCraft( typeof( StoneFancyPedestal ), "Furniture", "pedestal, fancy", 70.0, 130.0, typeof( Granite ), 1044514, 7, 1044513 );
			AddCraft( typeof( StoneRoughPillar ), "Furniture", "pillar", 85.0, 135.0, typeof( Granite ), 1044514, 15, 1044513 );

			AddCraft( typeof( SmallStatueAngel ), "Small Statues", "angel statue", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( SmallStatueDragon ), "Small Statues", "dragon statue", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( StatueGargoyleBust ), "Small Statues", "gargoyle bust", 60.0, 110.0, typeof( Granite ), 1044514, 6, 1044513 );
			AddCraft( typeof( GargoyleStatue ), "Small Statues", "gargoyle statue", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( StatueBust ), "Small Statues", "man bust", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( SmallStatueMan ), "Small Statues", "man statue", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( SmallStatueNoble ), "Small Statues", "noble statue", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( SmallStatuePegasus ), "Small Statues", "pegasus statue", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( SmallStatueSkull ), "Small Statues", "skull idol", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( SmallStatueWoman ), "Small Statues", "woman statue", 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );

			AddCraft( typeof( StatueAdventurer ), "Medium Statues", "adventurer statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueAmazon ), "Medium Statues", "amazon statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueDemonicFace ), "Medium Statues", "demonic face", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueDruid ), "Medium Statues", "druid statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueElvenKnight ), "Medium Statues", "elf knight statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueElvenPriestess ), "Medium Statues", "elf priestess statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueElvenSorceress ), "Medium Statues", "elf sorceress statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueElvenWarrior ), "Medium Statues", "elf warrior statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueFighter ), "Medium Statues", "fighter statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueGargoyleTall ), "Medium Statues", "gargoyle statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( GargoyleFlightStatue ), "Medium Statues", "gargoyle statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueGryphon ), "Medium Statues", "gryphon statue", 65.0, 115.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddCraft( typeof( SmallStatueLion ), "Medium Statues", "lion statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( MedusaStatue ), "Medium Statues", "medusa statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueMermaid ), "Medium Statues", "mermaid statue", 65.0, 115.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddCraft( typeof( StatueNoble ), "Medium Statues", "noble statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatuePriest ), "Medium Statues", "priest statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueSeaHorse ), "Medium Statues", "sea horse statue", 65.0, 115.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddCraft( typeof( SphinxStatue ), "Medium Statues", "sphinx statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueSwordsman ), "Medium Statues", "swordsman statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueWolfWinged ), "Medium Statues", "winged wolf statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );
			AddCraft( typeof( StatueWizard ), "Medium Statues", "wizard statue", 65.0, 115.0, typeof( Granite ), 1044514, 8, 1044513 );

			AddCraft( typeof( StatueDwarf ), "Large Statues", "dwarf statue", 75.0, 125.0, typeof( Granite ), 1044514, 16, 1044513 );
			AddCraft( typeof( StatueDesertGod ), "Large Statues", "god statue", 75.0, 125.0, typeof( Granite ), 1044514, 16, 1044513 );
			AddCraft( typeof( StatueHorseRider ), "Large Statues", "horse rider", 75.0, 125.0, typeof( Granite ), 1044514, 16, 1044513 );
			AddCraft( typeof( MediumStatueLion ), "Large Statues", "lion statue", 75.0, 125.0, typeof( Granite ), 1044514, 16, 1044513 );
			AddCraft( typeof( StatueMinotaurDefend ), "Large Statues", "minotaur statue, defend", 75.0, 125.0, typeof( Granite ), 1044514, 16, 1044513 );
			AddCraft( typeof( StatueMinotaurAttack ), "Large Statues", "minotaur statue, attack", 75.0, 125.0, typeof( Granite ), 1044514, 16, 1044513 );
			AddCraft( typeof( LargePegasusStatue ), "Large Statues", "pegasus statue", 75.0, 125.0, typeof( Granite ), 1044514, 16, 1044513 );
			AddCraft( typeof( StatueWomanWarriorPillar ), "Large Statues", "woman warrior statue", 75.0, 125.0, typeof( Granite ), 1044514, 16, 1044513 );

			AddCraft( typeof( StatueAngelTall ), "Huge Statues", "angel statue", 85.0, 125.0, typeof( Granite ), 1044514, 24, 1044513 );
			AddCraft( typeof( StatueDaemon ), "Huge Statues", "daemon statue, tall", 85.0, 125.0, typeof( Granite ), 1044514, 24, 1044513 );
			AddCraft( typeof( LargeStatueLion ), "Huge Statues", "lion statue", 85.0, 125.0, typeof( Granite ), 1044514, 24, 1044513 );
			AddCraft( typeof( TallStatueLion ), "Huge Statues", "lion statue, tall", 85.0, 125.0, typeof( Granite ), 1044514, 24, 1044513 );
			AddCraft( typeof( StatueCapeWarrior ), "Huge Statues", "warrior statue", 85.0, 125.0, typeof( Granite ), 1044514, 24, 1044513 );
			AddCraft( typeof( StatueWiseManTall ), "Huge Statues", "wise man statue", 85.0, 125.0, typeof( Granite ), 1044514, 24, 1044513 );
			AddCraft( typeof( LargeStatueWolf ), "Huge Statues", "wolf statue", 85.0, 125.0, typeof( Granite ), 1044514, 24, 1044513 );
			AddCraft( typeof( StatueWomanTall ), "Huge Statues", "woman statue", 85.0, 125.0, typeof( Granite ), 1044514, 24, 1044513 );

			AddCraft( typeof( StatueGateGuardian ), "Giant Statues", "gate guardian statue", 95.0, 135.0, typeof( Granite ), 1044514, 32, 1044513 );
			AddCraft( typeof( StatueGuardian ), "Giant Statues", "guardian statue", 95.0, 135.0, typeof( Granite ), 1044514, 32, 1044513 );
			AddCraft( typeof( StatueGiantWarrior ), "Giant Statues", "warrior statue", 95.0, 135.0, typeof( Granite ), 1044514, 32, 1044513 );

			AddCraft( typeof( StoneTombStoneA ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneB ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneC ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneD ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneE ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneF ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneG ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneH ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneI ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneJ ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneK ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneL ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneM ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneN ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneO ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneP ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneQ ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneR ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneS ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneTombStoneT ), "Tombstones", "tombstone", 45.0, 95.0, typeof( Granite ), 1044514, 3, 1044513 );

			SetSubRes( typeof( Granite ), 1044525 );

			AddSubRes( typeof( Granite ),			1044525, 00.0, 1044514, 1044526 );
			AddSubRes( typeof( DullCopperGranite ),	1044023, 65.0, 1044514, 1044527 );
			AddSubRes( typeof( ShadowIronGranite ),	1044024, 70.0, 1044514, 1044527 );
			AddSubRes( typeof( CopperGranite ),		1044025, 75.0, 1044514, 1044527 );
			AddSubRes( typeof( BronzeGranite ),		1044026, 80.0, 1044514, 1044527 );
			AddSubRes( typeof( GoldGranite ),		1044027, 85.0, 1044514, 1044527 );
			AddSubRes( typeof( AgapiteGranite ),	1044028, 90.0, 1044514, 1044527 );
			AddSubRes( typeof( VeriteGranite ),		1044029, 95.0, 1044514, 1044527 );
			AddSubRes( typeof( ValoriteGranite ),	1044030, 99.0, 1044514, 1044527 );
			AddSubRes( typeof( NepturiteGranite ),	1036173, 99.0, 1044514, 1044527 );
			AddSubRes( typeof( ObsidianGranite ),	1036162, 99.0, 1044514, 1044527 );
			AddSubRes( typeof( MithrilGranite ),	1036137, 109.0, 1044514, 1044527 );
			AddSubRes( typeof( XormiteGranite ),	1034437, 109.0, 1044514, 1044527 );
			AddSubRes( typeof( DwarvenGranite ),	1036181, 110.0, 1044514, 1044527 );
		}
	}
}