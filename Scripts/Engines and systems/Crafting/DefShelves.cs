using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefShelves : CraftSystem
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
					m_CraftSystem = new DefShelves();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefShelves() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
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

			// 1
			AddCraft( typeof( FancyArmoire ),		"Armoires", 1044312, 51.5, 76.5,	typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( Armoire ),			"Armoires", 1022643, 51.5, 76.5,	typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( RedArmoire ),			"Armoires", 1030328, 90.0, 115.0,	typeof( Board ), 1015101, 40, 1044351 );
			AddCraft( typeof( ElegantArmoire ),		"Armoires", 1030330, 90.0, 115.0,	typeof( Board ), 1015101, 40, 1044351 );
			AddCraft( typeof( MapleArmoire ),		"Armoires", 1030332, 90.0, 115.0,	typeof( Board ), 1015101, 40, 1044351 );
			AddCraft( typeof( CherryArmoire ),		"Armoires", 1030334, 90.0, 115.0,	typeof( Board ), 1015101, 40, 1044351 );
			AddCraft( typeof( NewArmoireA ), 		"Armoires", "bamboo armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireB ), 		"Armoires", "bamboo armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireC ), 		"Armoires", "bamboo armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireD ), 		"Armoires", "armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireE ),		"Armoires", "empty armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireF ), 		"Armoires", "open armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireG ), 		"Armoires", "armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireH ), 		"Armoires", "empty armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireI ), 		"Armoires", "open armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmoireJ ), 		"Armoires", "open armoire", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredArmoireA ),	"Armoires", "fancy amoire*", 51.5, 76.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredArmoireB ),	"Armoires", "tall fancy armoire*", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );

			// 2
			AddCraft( typeof( TallCabinet ),	"Cabinets", 1030261, 90.0, 115.0,	typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ShortCabinet ),	"Cabinets", 1030263, 90.0, 115.0,	typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredCabinetA ), "Cabinets", "book cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredCabinetB ), "Cabinets", "dish cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredCabinetC ), "Cabinets", "medium cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredCabinetD ), "Cabinets", "narrow book cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredCabinetE ), "Cabinets", "short cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredCabinetF ), "Cabinets", "short elegant cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredCabinetG ), "Cabinets", "short locker*", 51.5, 76.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredCabinetH ), "Cabinets", "storage cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredCabinetI ), "Cabinets", "tall fancy cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredCabinetJ ), "Cabinets", "tall medium cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredCabinetK ), "Cabinets", "tall narrow cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredCabinetL ), "Cabinets", "tall wide cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredCabinetM ), "Cabinets", "tall wide locker*", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredCabinetN ), "Cabinets", "wide medium cabinet*", 51.5, 76.5, typeof( Board ), 1015101, 35, 1044351 );

			// 3
			AddCraft( typeof( WoodenBox ),				"Chests", 1023709,	21.0,  46.0,	typeof( Board ), 1015101, 10, 1044351 );
			AddCraft( typeof( WoodenChest ),			"Chests", 1023650,	73.6,  98.6,	typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( PlainWoodenChest ),		"Chests", 1030251, 90.0, 115.0,	typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( OrnateWoodenChest ),		"Chests", 1030253, 90.0, 115.0,	typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( GildedWoodenChest ),		"Chests", 1030255, 90.0, 115.0,	typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( WoodenFootLocker ),		"Chests", 1030257, 90.0, 115.0,	typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( FinishedWoodenChest ),	"Chests", 1030259, 90.0, 115.0,	typeof( Board ), 1015101, 30, 1044351 );
			index = AddCraft( typeof( WoodenCoffin ), 	"Chests", "coffin", 90.0, 115.0, typeof( Board ), 1015101, 40, 1044351 );
			AddSkill( index, SkillName.Forensics, 75.0, 80.0 );
			index = AddCraft( typeof( WoodenCasket ), 	"Chests", "casket", 90.0, 115.0, typeof( Board ), 1015101, 40, 1044351 );
			AddSkill( index, SkillName.Forensics, 75.0, 80.0 );

			// 4
			AddCraft( typeof( SmallCrate ),			"Crates", 1044309,	10.0,  35.0,	typeof( Board ), 1015101, 8 , 1044351 );
			AddCraft( typeof( MediumCrate ),		"Crates", 1044310,	31.0,  56.0,	typeof( Board ), 1015101, 15, 1044351 );
			AddCraft( typeof( LargeCrate ),			"Crates", 1044311,	47.3,  72.3,	typeof( Board ), 1015101, 18, 1044351 );
			AddCraft( typeof( AdventurerCrate ), 	"Crates", "adventurer crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( AlchemyCrate ), 		"Crates", "alchemy crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( ArmsCrate ), 			"Crates", "arms crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( BakerCrate ), 		"Crates", "baker crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( BeekeeperCrate ), 	"Crates", "beekeeper crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( BlacksmithCrate ), 	"Crates", "blacksmith crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( BowyerCrate ), 		"Crates", "bowyer crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( ButcherCrate ), 		"Crates", "butcher crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( CarpenterCrate ), 	"Crates", "carpenter crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( FletcherCrate ), 		"Crates", "fletcher crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( HealerCrate ), 		"Crates", "healer crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( HugeCrate ), 			"Crates", "huge crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( JewelerCrate ), 		"Crates", "jeweler crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( LibrarianCrate ), 	"Crates", "librarian crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( MusicianCrate ), 		"Crates", "musician crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( NecromancerCrate ), 	"Crates", "necromancer crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( ProvisionerCrate ), 	"Crates", "provisioner crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( SailorCrate ), 		"Crates", "sailor crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( StableCrate ), 		"Crates", "stable crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( SupplyCrate ), 		"Crates", "supply crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( TailorCrate ), 		"Crates", "tailor crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( TavernCrate ), 		"Crates", "tavern crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( TinkerCrate ), 		"Crates", "tinker crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( TreasureCrate ), 		"Crates", "treasure crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );
			AddCraft( typeof( WizardryCrate ), 		"Crates", "wizardry crate", 47.3,  72.3, typeof( Board ), 1015101, 20, 1044351 );

			// 5
			AddCraft( typeof( NewDrawersA ), "Dressers", "dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersB ), "Dressers", "open dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersC ), "Dressers", "open dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersD ), "Dressers", "open ", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersE ), "Dressers", "nightstand", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersF ), "Dressers", "dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersG ), "Dressers", "open dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersH ), "Dressers", "open dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersI ), "Dressers", "open dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersJ ), "Dressers", "nightstand", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( Drawer ), "Dressers", "dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersL ), "Dressers", "open dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersM ), "Dressers", "open dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersN ), "Dressers", "open dresser", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( NewDrawersK ), "Dressers", "nightstand", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredDresserA ), "Dressers", "dresser*", 31.5, 56.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredDresserB ), "Dressers", "fancy dresser*", 31.5, 56.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredDresserC ), "Dressers", "medium dresser*", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredDresserD ), "Dressers", "medium dresser*", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredDresserE ), "Dressers", "short elegant dresser*", 31.5, 56.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredDresserF ), "Dressers", "short narrow dresser*", 31.5, 56.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredDresserG ), "Dressers", "short wide dresser*", 31.5, 56.5, typeof( Board ), 1015101, 27, 1044351 );
			AddCraft( typeof( ColoredDresserH ), "Dressers", "standing dresser*", 31.5, 56.5, typeof( Board ), 1015101, 27, 1044351 );
			AddCraft( typeof( ColoredDresserI ), "Dressers", "trinket dresser*", 31.5, 56.5, typeof( Board ), 1015101, 30, 1044351 );
			AddCraft( typeof( ColoredDresserJ ), "Dressers", "wide medium dresser*", 31.5, 56.5, typeof( Board ), 1015101, 35, 1044351 );

			// 6
			AddCraft( typeof( NewShelfB ), "Bamboo Shelves", "shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShelfA ), "Bamboo Shelves", "shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewWizardShelfC ), "Bamboo Shelves", "alchemy shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmorShelfB ), "Bamboo Shelves", "armor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBakerShelfA ), "Bamboo Shelves", "baker shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfB ), "Bamboo Shelves", "book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfC ), "Bamboo Shelves", "book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfD ), "Bamboo Shelves", "book shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBowyerShelfA ), "Bamboo Shelves", "bowyer shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTannerShelfB ), "Bamboo Shelves", "carpenter shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewClothShelfA ), "Bamboo Shelves", "cloth shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewClothShelfB ), "Bamboo Shelves", "cloth shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShoeShelfA ), "Bamboo Shelves", "cobbler shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewDrinkShelfA ), "Bamboo Shelves", "drink shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewHunterShelf ), "Bamboo Shelves", "hunter shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewKitchenShelfB ), "Bamboo Shelves", "kitchen shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewSorcererShelfA ), "Bamboo Shelves", "sorcerer shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTailorShelfA ), "Bamboo Shelves", "tailor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTannerShelfA ), "Bamboo Shelves", "supply shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTavernShelfC ), "Bamboo Shelves", "tavern shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBlacksmithShelfB ), "Bamboo Shelves", "tinker shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );

			// 7
			AddCraft( typeof( NewShelfC ), "Redwood Shelves", "shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShelfD ), "Redwood Shelves", "shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewWizardShelfD ), "Redwood Shelves", "alchemy shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmorShelfC ), "Redwood Shelves", "armor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBakerShelfC ), "Redwood Shelves", "baker shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfE ), "Redwood Shelves", "book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfF ), "Redwood Shelves", "book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfG ), "Redwood Shelves", "book shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBowyerShelfB ), "Redwood Shelves", "bowyer shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewCarpenterShelfA ), "Redwood Shelves", "carpenter shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewClothShelfC ), "Redwood Shelves", "cloth shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewClothShelfD ), "Redwood Shelves", "cloth shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShoeShelfB ), "Redwood Shelves", "cobbler shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewDrinkShelfB ), "Redwood Shelves", "drink shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBakerShelfB ), "Redwood Shelves", "kitchen shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBlacksmithShelfC ), "Redwood Shelves", "smith shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewSorcererShelfB ), "Redwood Shelves", "sorcerer shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewSupplyShelfA ), "Redwood Shelves", "supply shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTailorShelfB ), "Redwood Shelves", "tailor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTavernShelfD ), "Redwood Shelves", "tavern shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTinkerShelfA ), "Redwood Shelves", "tinker shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );

			// 8
			AddCraft( typeof( NewShelfF ), "Rustic Shelves", "shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShelfE ), "Rustic Shelves", "shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewWizardShelfE ), "Rustic Shelves", "alchemy shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmorShelfD ), "Rustic Shelves", "armor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBakerShelfE ), "Rustic Shelves", "baker shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfH ), "Rustic Shelves", "book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfI ), "Rustic Shelves", "book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfJ ), "Rustic Shelves", "book shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBowyerShelfC ), "Rustic Shelves", "bowyer shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewCarpenterShelfB ), "Rustic Shelves", "carpenter shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewClothShelfE ), "Rustic Shelves", "cloth shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewClothShelfF ), "Rustic Shelves", "cloth shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShoeShelfC ), "Rustic Shelves", "cobbler shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewDrinkShelfC ), "Rustic Shelves", "drink shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBakerShelfD ), "Rustic Shelves", "kitchen shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBlacksmithShelfD ), "Rustic Shelves", "smith shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewSorcererShelfC ), "Rustic Shelves", "sorcerer shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewSupplyShelfB ), "Rustic Shelves", "supply shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTailorShelfC ), "Rustic Shelves", "tailor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTavernShelfE ), "Rustic Shelves", "tavern shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTinkerShelfB ), "Rustic Shelves", "tinker shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );

			// 9
			AddCraft( typeof( ColoredShelf1 ), "Stained Shelf", "alchemy shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelf2 ), "Stained Shelf", "armor shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelf3 ), "Stained Shelf", "baker shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelf4 ), "Stained Shelf", "barkeep shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelf5 ), "Stained Shelf", "book shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelf6 ), "Stained Shelf", "bowyer shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelf7 ), "Stained Shelf", "carpenter shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelf8 ), "Stained Shelf", "cloth shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfA ), "Stained Shelf", "cobbler shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfN ), "Stained Shelf", "cobbler shelf*", 41.5, 66.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredShelfB ), "Stained Shelf", "drink shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfC ), "Stained Shelf", "empty shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfD ), "Stained Shelf", "food shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfE ), "Stained Shelf", "kitchen shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfF ), "Stained Shelf", "library shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfG ), "Stained Shelf", "liquor shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfH ), "Stained Shelf", "necromancer shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfI ), "Stained Shelf", "plain cloth shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfJ ), "Stained Shelf", "provisions shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( SailorShelf ), "Stained Shelf", "sailor shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfK ), "Stained Shelf", "short book shelf*", 41.5, 66.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredShelfL ), "Stained Shelf", "short empty shelf*", 41.5, 66.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredShelfM ), "Stained Shelf", "short kitchen shelf*", 41.5, 66.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( ColoredShelfO ), "Stained Shelf", "smith shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfP ), "Stained Shelf", "storage shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfQ ), "Stained Shelf", "supply shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfR ), "Stained Shelf", "tailor shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfS ), "Stained Shelf", "tall supply shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfT ), "Stained Shelf", "tall wizard shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfU ), "Stained Shelf", "tamer shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfV ), "Stained Shelf", "tavern shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfW ), "Stained Shelf", "tinker shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfX ), "Stained Shelf", "tome shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfY ), "Stained Shelf", "weaver shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( ColoredShelfZ ), "Stained Shelf", "wizard shelf*", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );

			// 10
			AddCraft( typeof( EmptyBookcase ), "Wood Shelves", 1022718, 41.5, 66.5, typeof( Board ), 1015101, 25, 1044351 );
			AddCraft( typeof( FullBookcase ), "Wood Shelves", 1022711,	41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShelfG ), "Wood Shelves", "shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShelfH ), "Wood Shelves", "shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewWizardShelfF ), "Wood Shelves", "alchemy shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmorShelfA ), "Wood Shelves", "armor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewArmorShelfE ), "Wood Shelves", "armor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBakerShelfG ), "Wood Shelves", "baker shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfK ), "Wood Shelves", "book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfL ), "Wood Shelves", "book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfM ), "Wood Shelves", "book shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBookShelfA ), "Wood Shelves", "book shelf, tall", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBowyerShelfD ), "Wood Shelves", "bowyer shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewCarpenterShelfC ), "Wood Shelves", "carpenter shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewClothShelfG ), "Wood Shelves", "cloth shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewClothShelfH ), "Wood Shelves", "cloth shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewShoeShelfD ), "Wood Shelves", "cobbler shelf, small", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewDarkBookShelfA ), "Wood Shelves", "dark book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewDarkBookShelfB ), "Wood Shelves", "dark book shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewDarkShelf ), "Wood Shelves", "dark shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewDrinkShelfD ), "Wood Shelves", "drink shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewHelmShelf ), "Wood Shelves", "helm shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBakerShelfF ), "Wood Shelves", "kitchen shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewKitchenShelfA ), "Wood Shelves", "kitchen shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewDrinkShelfE ), "Wood Shelves", "liquor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewOldBookShelf ), "Wood Shelves", "old book shelf",	41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewPotionShelf ), "Wood Shelves", "potion shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewRuinedBookShelf ), "Wood Shelves", "ruined book shelf",	41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewBlacksmithShelfE ), "Wood Shelves", "smith shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewSorcererShelfD ), "Wood Shelves", "sorcerer shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewSupplyShelfC ), "Wood Shelves", "supply shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTailorShelfD ), "Wood Shelves", "tailor shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTavernShelfF ), "Wood Shelves", "tavern shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTinkerShelfC ), "Wood Shelves", "tinker shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewTortureShelf ), "Wood Shelves", "torture shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewWizardShelfB ), "Wood Shelves", "wizard shelf", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );
			AddCraft( typeof( NewWizardShelfA ), "Wood Shelves", "wizard shelf, tall", 41.5, 66.5, typeof( Board ), 1015101, 35, 1044351 );



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
			AddSubRes( typeof( DriftwoodBoard ), 1095409, 90.0, 1015101, 1072652 );
			AddSubRes( typeof( OakBoard ), 1095385, 95.0, 1015101, 1072652 );
			AddSubRes( typeof( PineBoard ), 1095386, 100.0, 1015101, 1072652 );
			AddSubRes( typeof( GhostBoard ), 1095511, 100.0, 1015101, 1072652 );
			AddSubRes( typeof( RosewoodBoard ), 1095387, 100.0, 1015101, 1072652 );
			AddSubRes( typeof( WalnutBoard ), 1095388, 100.0, 1015101, 1072652 );
			AddSubRes( typeof( PetrifiedBoard ), 1095532, 100.0, 1015101, 1072652 );
			AddSubRes( typeof( ElvenBoard ), 1095535, 100.1, 1015101, 1072652 );
		}
	}
}