using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefTailoring : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044005; } // <CENTER>TAILORING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTailoring();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefTailoring() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
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

		public static bool IsNonColorable(Type type)
		{
			for (int i = 0; i < m_TailorNonColorables.Length; ++i)
			{
				if (m_TailorNonColorables[i] == type)
				{
					return true;
				}
			}

			return false;
		}

		private static Type[] m_TailorNonColorables = new Type[]
			{
				//typeof( OrcHelm )
			};

		private static Type[] m_TailorColorables = new Type[]
			{
				typeof( GozaMatEastDeed ), typeof( GozaMatSouthDeed ),
				typeof( SquareGozaMatEastDeed ), typeof( SquareGozaMatSouthDeed ),
				typeof( BrocadeGozaMatEastDeed ), typeof( BrocadeGozaMatSouthDeed ),
				typeof( BrocadeSquareGozaMatEastDeed ), typeof( BrocadeSquareGozaMatSouthDeed )
			};

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			if ( type != typeof( Cloth ) && type != typeof( UncutCloth ) )
				return false;

			type = item.ItemType;

			bool contains = false;

			for ( int i = 0; !contains && i < m_TailorColorables.Length; ++i )
				contains = ( m_TailorColorables[i] == type );

			return contains;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x248 );
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

			#region Furs
			AddCraft( typeof( FurCap ), "Furs", "fur cap", 0.0, 25.0, typeof( Furs ), "Fur", 2, 1042081 );
			AddCraft( typeof( FurBoots ), "Furs", "fur boots", 33.1, 58.1, typeof( Furs ), "Fur", 8, 1042081 );
			AddCraft( typeof( FurArms ), "Furs", "fur arms", 53.9, 78.9, typeof( Furs ), "Fur", 4, 1042081 );
			AddCraft( typeof( FurLegs ), "Furs", "fur leggings", 66.3, 91.3, typeof( Furs ), "Fur", 10, 1042081 );
			AddCraft( typeof( FurSarong ), "Furs", "fur sarong", 35.0, 60.0, typeof( Furs ), "Fur", 13, 1042081 );
			AddCraft( typeof( FurTunic ), "Furs", "fur tunic", 70.5, 95.5, typeof( Furs ), "Fur", 12, 1042081 );
			AddCraft( typeof( FurCape ), "Furs", "fur cape", 35.0, 60.0, typeof( Furs ), "Fur", 13, 1042081 );
			AddCraft( typeof( FurRobe ), "Furs", "fur robe", 55.0, 80.0, typeof( Furs ), "Fur", 16, 1042081 );
			AddCraft( typeof( WhiteFurCap ), "Furs", "white fur cap", 0.0, 25.0, typeof( FursWhite ), "White Fur", 2, 1042081 );
			AddCraft( typeof( WhiteFurBoots ), "Furs", "white fur boots", 33.1, 58.1, typeof( FursWhite ), "White Fur", 8, 1042081 );
			AddCraft( typeof( WhiteFurArms ), "Furs", "white fur arms", 53.9, 78.9, typeof( FursWhite ), "White Fur", 4, 1042081 );
			AddCraft( typeof( WhiteFurLegs ), "Furs", "white fur leggings", 66.3, 91.3, typeof( FursWhite ), "White Fur", 10, 1042081 );
			AddCraft( typeof( WhiteFurSarong ), "Furs", "white fur sarong", 35.0, 60.0, typeof( FursWhite ), "White Fur", 13, 1042081 );
			AddCraft( typeof( WhiteFurTunic ), "Furs", "white fur tunic", 70.5, 95.5, typeof( FursWhite ), "White Fur", 12, 1042081 );
			AddCraft( typeof( WhiteFurCape ), "Furs", "white fur cape", 35.0, 60.0, typeof( FursWhite ), "White Fur", 13, 1042081 );
			AddCraft( typeof( WhiteFurRobe ), "Furs", "white fur robe", 55.0, 80.0, typeof( FursWhite ), "White Fur", 16, 1042081 );
			#endregion

			#region Hats
			AddCraft( typeof( SkullCap ), 1011375, 1025444, 0.0, 25.0, typeof( Cloth ), 1044286, 2, 1044287 );
			AddCraft( typeof( Bandana ), 1011375, 1025440, 0.0, 25.0, typeof( Cloth ), 1044286, 2, 1044287 );
			AddCraft( typeof( FloppyHat ), 1011375, 1025907, 6.2, 31.2, typeof( Cloth ), 1044286, 11, 1044287 );
			AddCraft( typeof( Cap ), 1011375, 1025909, 6.2, 31.2, typeof( Cloth ), 1044286, 11, 1044287 );
			AddCraft( typeof( WideBrimHat ), 1011375, 1025908, 6.2, 31.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( StrawHat ), 1011375, 1025911, 6.2, 31.2, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( TallStrawHat ), 1011375, 1025910, 6.7, 31.7, typeof( Cloth ), 1044286, 13, 1044287 );
			AddCraft( typeof( WizardsHat ), 1011375, 1025912, 7.2, 32.2, typeof( Cloth ), 1044286, 15, 1044287 );
			AddCraft( typeof( WitchHat ), 1011375, "witch hat", 7.2, 32.2, typeof( Cloth ), 1044286, 15, 1044287 );
			AddCraft( typeof( Bonnet ), 1011375, 1025913, 6.2, 31.2, typeof( Cloth ), 1044286, 11, 1044287 );
			AddCraft( typeof( FeatheredHat ), 1011375, 1025914, 6.2, 31.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( TricorneHat ), 1011375, 1025915, 6.2, 31.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( PirateHat ), 1011375, "pirate hat", 6.2, 31.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( JesterHat ), 1011375, 1025916, 7.2, 32.2, typeof( Cloth ), 1044286, 15, 1044287 );
			AddCraft( typeof( ClothHood ), 1011375, "cloth hood", 7.2, 32.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( FancyHood ), 1011375, "fancy hood", 7.2, 32.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( ClothCowl ), 1011375, "cloth cowl", 7.2, 32.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( WizardHood ), 1011375, "wizard hood", 7.2, 32.2, typeof( Cloth ), 1044286, 12, 1044287 );
			index = AddCraft( typeof( DeadMask ), 1011375, "mask of the dead", 7.2, 32.2, typeof( Cloth ), 1044286, 12, 1044287 );
				AddRes( index, typeof( PolishedSkull ), "Polished Skull", 1, 1049063 );
			AddCraft( typeof( FurCap ), 1011375, "fur cap", 12.0, 48.0, typeof( Furs ), "Fur", 9, 1042081 );
			AddCraft( typeof( WhiteFurCap ), 1011375, "white fur cap", 12.0, 48.0, typeof( FursWhite ), "White Fur", 9, 1042081 );
			AddCraft( typeof( FlowerGarland ), 1011375, 1028965, 10.0, 35.0, typeof( Cloth ), 1044286, 5, 1044287 );
			AddCraft( typeof( ClothNinjaHood ), 1011375, 1030202, 80.0, 105.0, typeof( Cloth ), 1044286, 13, 1044287 );
			AddCraft( typeof( Kasa ), 1011375, 1030211, 60.0, 85.0, typeof( Cloth ), 1044286, 12, 1044287 );	
			#endregion

			#region Shirts
			AddCraft( typeof( Doublet ), 1015269, 1028059, 0, 25.0, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Shirt ), 1015269, 1025399, 20.7, 45.7, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( BeggarVest ), 1015269, "beggar vest", 20.7, 45.7, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( RoyalVest ), 1015269, "royal vest", 20.7, 45.7, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( RusticVest ), 1015269, "rustic vest", 20.7, 45.7, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Tunic ), 1015269, 1028097, 00.0, 25.0, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( Surcoat ), 1015269, 1028189, 8.2, 33.2, typeof( Cloth ), 1044286, 14, 1044287 );
			AddCraft( typeof( PlainDress ), 1015269, 1027937, 12.4, 37.4, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( FancyDress ), 1015269, 1027935, 33.1, 58.1, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( GildedDress ), 1015269, 1028973, 37.5, 62.5, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( Cloak ), 1015269, 1025397, 41.4, 66.4, typeof( Cloth ), 1044286, 14, 1044287 );
			AddCraft( typeof( RoyalCape ), 1015269, "royal cloak", 91.4, 120.4, typeof( Cloth ), 1044286, 14, 1044287 );
			AddCraft( typeof( Robe ), 1015269, 1027939, 53.9, 78.9, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( ArchmageRobe ), 1015269, "archmage robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( AssassinRobe ), 1015269, "assassin robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( AssassinShroud ), 1015269, "assassin shroud", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( ChaosRobe ), 1015269, "chaos robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( CultistRobe ), 1015269, "cultist robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( DragonRobe ), 1015269, "dragon robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( ElegantRobe ), 1015269, "elegant robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( ExquisiteRobe ), 1015269, "exquisite robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( FancyRobe ), 1015269, "fancy robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( FoolsCoat ), 1015269, "fool's coat", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( FormalRobe ), 1015269, "formal robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( GildedRobe ), 1015269, "gilded robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( GildedDarkRobe ), 1015269, "gilded dark robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( GildedLightRobe ), 1015269, "gilded light robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( JesterGarb ), 1015269, "jester garb", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( JesterSuit ), 1015269, 1028095, 8.2, 33.2, typeof( Cloth ), 1044286, 24, 1044287 );
			AddCraft( typeof( JokerRobe ), 1015269, "jester coat", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( MagistrateRobe ), 1015269, "magistrate robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( NecromancerRobe ), 1015269, "necromancer robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( OrnateRobe ), 1015269, "ornate robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( PirateCoat ), 1015269, "pirate coat", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( PriestRobe ), 1015269, "priest robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( ProphetRobe ), 1015269, "prophet robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( RoyalRobe ), 1015269, "royal robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( SageRobe ), 1015269, "sage robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( SorcererRobe ), 1015269, "sorcerer robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( SpiderRobe ), 1015269, "spider robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( VagabondRobe ), 1015269, "vagabond robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( VampireRobe ), 1015269, "vampire robe", 70.0, 95.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( FancyShirt ), 1015269, 1027933, 24.8, 49.8, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( FormalShirt ), 1015269, 1028975, 26.0, 51.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( FormalCoat ), 1015269, "formal coat", 26.0, 51.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( RoyalCoat ), 1015269, "royal coat", 26.0, 51.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( RoyalShirt ), 1015269, "royal shirt", 26.0, 51.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( RusticShirt ), 1015269, "rustic shirt", 26.0, 51.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( SquireShirt ), 1015269, "squire shirt", 26.0, 51.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( WizardShirt ), 1015269, "wizard shirt", 26.0, 51.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( ClothNinjaJacket ), 1015269, 1030207, 75.0, 100.0, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( Kamishimo ), 1015269, 1030212, 75.0, 100.0, typeof( Cloth ), 1044286, 15, 1044287 );
			AddCraft( typeof( HakamaShita ), 1015269, 1030215, 40.0, 65.0, typeof( Cloth ), 1044286, 14, 1044287 );
			AddCraft( typeof( MaleKimono ), 1015269, 1030189, 50.0, 75.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( FemaleKimono ), 1015269, 1030190, 50.0, 75.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( JinBaori ), 1015269, 1030220, 30.0, 55.0, typeof( Cloth ), 1044286, 12, 1044287 );
			#endregion

			#region Pants
			AddCraft( typeof( ShortPants ), 1015279, 1025422, 24.8, 49.8, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( LongPants ), 1015279, 1025433, 24.8, 49.8, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( SailorPants ), 1015279, "sailor pants", 24.8, 49.8, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( PiratePants ), 1015279, "pirate pants", 24.8, 49.8, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Kilt ), 1015279, 1025431, 20.7, 45.7, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Skirt ), 1015279, 1025398, 29.0, 54.0, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( RoyalSkirt ), 1015279, "royal skirt", 20.7, 45.7, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( RoyalLongSkirt ), 1015279, "royal long skirt", 29.0, 54.0, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( FurSarong ), 1015279, 1028971, 35.0, 60.0, typeof( Furs ), "Fur", 12, 1042081 );
			AddCraft( typeof( WhiteFurSarong ), 1015279, "white fur sarong", 35.0, 60.0, typeof( FursWhite ), "White Fur", 12, 1042081 );
			AddCraft( typeof( Hakama ), 1015279, 1030213, 50.0, 75.0, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( TattsukeHakama ), 1015279, 1030214, 50.0, 75.0, typeof( Cloth ), 1044286, 16, 1044287 );
			#endregion

			#region Misc
			AddCraft( typeof( BodySash ), 1015283, 1025441, 4.1, 29.1, typeof( Cloth ), 1044286, 4, 1044287 );
			AddCraft( typeof( LoinCloth ), 1015283, "loin cloth", 20.7, 45.7, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( HalfApron ), 1015283, 1025435, 20.7, 45.7, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( FullApron ), 1015283, 1025437, 29.0, 54.0, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( PugilistMits ), 1015283, "pugilist gloves", 32.9, 57.9, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( ThrowingGloves ), 1015283, "throwing gloves", 32.9, 57.9, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( Obi ), 1015283, 1030219, 20.0, 45.0, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( HarpoonRope ), 1015283, "harpoon rope", 0.0, 40.0, typeof( Cloth ), 1044286, 1, 1044287 );
			AddCraft( typeof( OilCloth ), 1015283, 1041498, 74.6, 99.6, typeof( Cloth ), 1044286, 1, 1044287 );
			AddCraft( typeof( GozaMatEastDeed ), 1015283, 1030404, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
			AddCraft( typeof( GozaMatSouthDeed ), 1015283, 1030405, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
			AddCraft( typeof( SquareGozaMatEastDeed ), 1015283, 1030407, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
			AddCraft( typeof( SquareGozaMatSouthDeed ), 1015283, 1030406, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
			AddCraft( typeof( BrocadeGozaMatEastDeed ), 1015283, 1030408, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
			AddCraft( typeof( BrocadeGozaMatSouthDeed ), 1015283, 1030409, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
			AddCraft( typeof( BrocadeSquareGozaMatEastDeed ), 1015283, 1030411, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
			AddCraft( typeof( BrocadeSquareGozaMatSouthDeed ), 1015283, 1030410, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
			#endregion

			#region Footwear
			AddCraft( typeof( FurBoots ), 1015288, 1028967, 50.0, 75.0, typeof( Furs ), "Fur", 12, 1042081 );
			AddCraft( typeof( WhiteFurBoots ), 1015288, "white fur boots", 50.0, 75.0, typeof( FursWhite ), "White Fur", 12, 1042081 );
			AddCraft( typeof( NinjaTabi ), 1015288, 1030210, 70.0, 95.0, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( SamuraiTabi ), 1015288, 1030209, 20.0, 45.0, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( Sandals ), 1015288, 1025901, 12.4, 37.4, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( Shoes ), 1015288, 1025904, 16.5, 41.5, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( Boots ), 1015288, 1025899, 33.1, 58.1, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( ThighBoots ), 1015288, 1025906, 41.4, 66.4, typeof( Leather ), 1044462, 10, 1044463 );
			AddCraft( typeof( BarbarianBoots ), 1015288, "barbarian boots", 50.0, 75.0, typeof( Furs ), "Fur", 12, 1042081 );
			AddCraft( typeof( LeatherSandals ), 1015288, "leather sandals", 42.4, 67.4, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( LeatherShoes ), 1015288, "leather shoes", 56.5, 71.5, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( LeatherBoots ), 1015288, "leather boots", 63.1, 88.1, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( LeatherThighBoots ), 1015288, "leather thigh boots", 71.4, 96.4, typeof( Leather ), 1044462, 10, 1044463 );
			AddCraft( typeof( LeatherSoftBoots ), 1015288, "soft leather boots", 81.4, 106.4, typeof( Leather ), 1044462, 8, 1044463 );
			#endregion

			#region Leather Armor
			AddCraft( typeof( LeatherGorget ), 1015293, 1025063, 53.9, 78.9, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( LeatherCap ), 1015293, 1027609, 6.2, 31.2, typeof( Leather ), 1044462, 2, 1044463 );
			AddCraft( typeof( LeatherGloves ), 1015293, 1025062, 51.8, 76.8, typeof( Leather ), 1044462, 3, 1044463 );
			AddCraft( typeof( LeatherArms ), 1015293, 1025061, 53.9, 78.9, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( LeatherLegs ), 1015293, 1025067, 66.3, 91.3, typeof( Leather ), 1044462, 10, 1044463 );
			AddCraft( typeof( LeatherChest ), 1015293, 1025068, 70.5, 95.5, typeof( Leather ), 1044462, 12, 1044463 );
			AddCraft( typeof( LeatherCloak ), 1015293, "leather cloak", 66.3, 91.3, typeof( Leather ), 1044462, 10, 1044463 );
			AddCraft( typeof( LeatherRobe ), 1015293, "leather robe", 76.3, 101.3, typeof( Leather ), 1044462, 18, 1044463 );
			AddCraft( typeof( LeatherShorts ), 1015293, 1027168, 62.2, 87.2, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( LeatherSkirt ), 1015293, 1027176, 58.0, 83.0, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( LeatherBustierArms ), 1015293, 1027178, 58.0, 83.0, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( FemaleLeatherChest ), 1015293, 1027174, 62.2, 87.2, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( LeatherJingasa ), 1015293, 1030177, 45.0, 70.0, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( LeatherMempo ), 1015293, 1030181, 80.0, 105.0, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( LeatherDo ), 1015293, 1030182, 75.0, 100.0, typeof( Leather ), 1044462, 12, 1044463 );
			AddCraft( typeof( LeatherHiroSode ), 1015293, 1030185, 55.0, 80.0, typeof( Leather ), 1044462, 5, 1044463 );
			AddCraft( typeof( LeatherSuneate ), 1015293, 1030193, 68.0, 93.0, typeof( Leather ), 1044462, 12, 1044463 );
			AddCraft( typeof( LeatherHaidate ), 1015293, 1030197, 68.0, 93.0, typeof( Leather ), 1044462, 12, 1044463 );
			AddCraft( typeof( LeatherNinjaPants ), 1015293, 1030204, 80.0, 105.0, typeof( Leather ), 1044462, 13, 1044463 );
			AddCraft( typeof( LeatherNinjaJacket ), 1015293, 1030206, 85.0, 110.0, typeof( Leather ), 1044462, 13, 1044463 );
			AddCraft( typeof( LeatherNinjaBelt ), 1015293, 1030203, 50.0, 75.0, typeof( Leather ), 1044462, 5, 1044463 );
			AddCraft( typeof( LeatherNinjaMitts ), 1015293, 1030205, 65.0, 90.0, typeof( Leather ), 1044462, 12, 1044463 );
			AddCraft( typeof( LeatherNinjaHood ), 1015293, 1030201, 90.0, 115.0, typeof( Leather ), 1044462, 14, 1044463 );
			#endregion

			#region Studded Armor
			AddCraft( typeof( StuddedGorget ), 1015300, 1025078, 78.8, 103.8, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( StuddedGloves ), 1015300, 1025077, 82.9, 107.9, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( StuddedArms ), 1015300, 1025076, 87.1, 112.1, typeof( Leather ), 1044462, 10, 1044463 );
			AddCraft( typeof( StuddedLegs ), 1015300, 1025082, 91.2, 116.2, typeof( Leather ), 1044462, 12, 1044463 );
			AddCraft( typeof( StuddedSkirt ), 1015300, "studded skirt", 91.2, 116.2, typeof( Leather ), 1044462, 12, 1044463 );
			AddCraft( typeof( StuddedChest ), 1015300, 1025083, 94.0, 119.0, typeof( Leather ), 1044462, 14, 1044463 );
			AddCraft( typeof( StuddedBustierArms ), 1015300, 1027180, 82.9, 107.9, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( FemaleStuddedChest ), 1015300, 1027170, 87.1, 112.1, typeof( Leather ), 1044462, 10, 1044463 );
			AddCraft( typeof( StuddedMempo ), 1015300, 1030216, 80.0, 105.0, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( StuddedDo ), 1015300, 1030183, 95.0, 120.0, typeof( Leather ), 1044462, 14, 1044463 );
			AddCraft( typeof( StuddedHiroSode ), 1015300, 1030186, 85.0, 110.0, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( StuddedSuneate ), 1015300, 1030194, 92.0, 117.0, typeof( Leather ), 1044462, 14, 1044463 );
			AddCraft( typeof( StuddedHaidate ), 1015300, 1030198, 92.0, 117.0, typeof( Leather ), 1044462, 14, 1044463 );
			#endregion

			#region Bone Armor
			index = AddCraft( typeof( BoneHelm ), 1049149, 1025206, 85.0, 110.0, typeof( Leather ), 1044462, 4, 1044463 );
				AddRes( index, typeof( PolishedSkull ), "Polished Skull", 1, 1049063 );
				AddRes( index, typeof( PolishedBone ), "Polished Bone", 1, 1049063 );
			index = AddCraft( typeof( BoneGloves ), 1049149, 1025205, 89.0, 114.0, typeof( Leather ), 1044462, 6, 1044463 );
				AddRes( index, typeof( PolishedBone ), "Polished Bone", 2, 1049063 );
			index = AddCraft( typeof( BoneArms ), 1049149, 1025203, 92.0, 117.0, typeof( Leather ), 1044462, 8, 1044463 );
				AddRes( index, typeof( PolishedBone ), "Polished Bone", 4, 1049063 );
			index = AddCraft( typeof( BoneLegs ), 1049149, 1025202, 95.0, 120.0, typeof( Leather ), 1044462, 10, 1044463 );
				AddRes( index, typeof( PolishedBone ), "Polished Bone", 6, 1049063 );
			index = AddCraft( typeof( BoneSkirt ), 1049149, "bone skirt", 95.0, 120.0, typeof( Leather ), 1044462, 10, 1044463 );
				AddRes( index, typeof( PolishedBone ), "Polished Bone", 6, 1049063 );
			index = AddCraft( typeof( BoneChest ), 1049149, 1025199, 96.0, 121.0, typeof( Leather ), 1044462, 12, 1044463 );
				AddRes( index, typeof( PolishedBone ), "Polished Bone", 10, 1049063 );
			index = AddCraft(typeof(OrcHelm), 1049149, "horned helm", 90.0, 115.0, typeof(Leather), 1044462, 6, 1044463);
				AddRes( index, typeof( PolishedSkull ), "Polished Skull", 1, 1049063 );
				AddRes( index, typeof( PolishedBone ), "Polished Bone", 3, 1049063 );
			#endregion

			#region Bags
			AddCraft( typeof( Backpack ), "Bags", "backpack", 8.2, 33.2, typeof( Leather ), 1044462, 3, 1044463 );
			AddCraft( typeof( RuggedBackpack ), "Bags", "backpack, rugged", 10.7, 40.7, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( Pouch ), "Bags", "pouch", 0.0, 25.0, typeof( Leather ), 1044462, 2, 1044463 );
			AddCraft( typeof( Bag ), "Bags", "bag", 0.0, 25.0, typeof( Leather ), 1044462, 3, 1044463 );
			AddCraft( typeof( LargeBag ), "Bags", "bag, large", 16.5, 41.5, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( GiantBag ), "Bags", "bag, giant", 26.0, 51.0, typeof( Leather ), 1044462, 9, 1044463 );
			AddCraft( typeof( LargeSack ), "Bags", "rucksack", 20.7, 45.7, typeof( Leather ), 1044462, 7, 1044463 );
			AddCraft(typeof(AlchemyPouch), "Bags", "Alchemy Rucksack", 90.0, 115.0, typeof(GoliathLeather), 1044462, 25, 1049311);
			AddCraft(typeof(MinersPouch), "Bags", "Miners Rucksack", 90.0, 115.0, typeof(GoliathLeather), 1044462, 50, 1049311);
			AddCraft(typeof(LumberjackPouch), "Bags", "Lumberjacks Rucksack", 90.0, 115.0, typeof(GoliathLeather), 1044462, 50, 1049311);
			AddCraft(typeof(CoinPouch), "Bags", "Coin Pouch", 90.0, 115.0, typeof(GoliathLeather), 1044462, 25, 1049311);


			#endregion

			// Set the overridable material
			SetSubRes( typeof( Leather ), 1049150 );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes( typeof( Leather ),			1049150, 00.0, 1044462, 1049311 );
			AddSubRes( typeof( HornedLeather ),		1049152, 60.0, 1044462, 1049311 );
			AddSubRes( typeof( BarbedLeather ),		1049153, 65.0, 1044462, 1049311 );
			AddSubRes( typeof( NecroticLeather ),	1034403, 70.0, 1044462, 1049311 );
			AddSubRes( typeof( VolcanicLeather ),	1034414, 75.0, 1044462, 1049311 );
			AddSubRes( typeof( FrozenLeather ),		1034425, 80.0, 1044462, 1049311 );
			AddSubRes( typeof( SpinedLeather ),		1049151, 85.0, 1044462, 1049311 );
			AddSubRes( typeof( GoliathLeather ),	1034370, 90.0, 1044462, 1049311 );
			AddSubRes( typeof( DraconicLeather ),	1034381, 95.0, 1044462, 1049311 );
			AddSubRes( typeof( HellishLeather ),	1034392, 99.0, 1044462, 1049311 );
			AddSubRes( typeof( DinosaurLeather ),	1036104, 105.0, 1044462, 1049311 );
			AddSubRes( typeof( AlienLeather ),		1034444, 120.0, 1044462, 1049311 );

			MarkOption = true;
			Repair = Core.AOS;
			CanEnhance = Core.AOS;
		}
	}
}