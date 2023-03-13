using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefWands : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Inscribe; }
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
 
        public override string GumpTitleString
        {
            get { return "<BASEFONT Color=#FBFBFB><CENTER>WAND CRAFTING MENU</CENTER></BASEFONT>"; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefWands();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefWands() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		private static Type typeofAnvil = typeof( AnvilAttribute );
		private static Type typeofForge = typeof( ForgeAttribute );

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			int say = 0;

			bool anvil, forge;
			Server.Engines.Craft.DefBlacksmithy.CheckAnvilAndForge( from, 2, out anvil, out forge );

			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
			{
				from.SendMessage( "Your book is worn out and no longer usable." );
				say = 1150845;
			}
			else if ( !BaseTool.CheckAccessible( tool, from ) )
			{
				from.SendMessage( "You need the book in your pack." );
				say = 1150845;
			}
			else if ( from.Backpack.FindItemByType( typeof ( SmithHammer ) ) == null )
			{
				from.SendMessage( "You need a blacksmith hammer to mold the metal." );
				say = 1150845;
			}
			else if ( !anvil || !forge )
			{
				return 1044267; // You must be near an anvil and a forge to smith items.;
			}

			return say;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x542 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendMessage("The book is worn out from constant use.");

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			index = AddCraft(typeof(ClumsyMagicStaff), "First Circle", "wand of clumsiness", 20.0, 50.0, typeof(VeriteIngot), "Verite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 20.0, 50.0 );
			AddRes( index, typeof( Diamond ), "Diamond", 1, 1042081 );
			AddRes( index, typeof( ClumsyScroll ), "Clumsy Scroll", 1, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 1, 1042081 );

			index = AddCraft(typeof(CreateFoodMagicStaff), "First Circle", "wand of food creation", 20.0, 50.0, typeof(BronzeIngot), "Bronze Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 20.0, 50.0 );
			AddRes( index, typeof( StarSapphire ), "Star Sapphire", 1, 1042081 );
			AddRes( index, typeof( CreateFoodScroll ), "Create Food Scroll", 1, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 1, 1042081 );

			index = AddCraft(typeof(FeebleMagicStaff), "First Circle", "wand of feeble minds", 20.0, 50.0, typeof(CopperIngot), "Copper Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 20.0, 50.0 );
			AddRes( index, typeof( Tourmaline ), "Tourmaline", 1, 1042081 );
			AddRes( index, typeof( FeeblemindScroll ), "Feeble Scroll", 1, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 1, 1042081 );

			index = AddCraft(typeof(HealMagicStaff), "First Circle", "wand of healing", 20.0, 50.0, typeof(ValoriteIngot), "Valorite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 20.0, 50.0 );
			AddRes( index, typeof( Citrine ), "Citrine", 1, 1042081 );
			AddRes( index, typeof( HealScroll ), "Heal Scroll", 1, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 1, 1042081 );

			index = AddCraft(typeof(MagicArrowMagicStaff), "First Circle", "wand of magical arrow", 20.0, 50.0, typeof(GoldIngot), "Gold Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 20.0, 50.0 );
			AddRes( index, typeof( Diamond ), "Diamond", 1, 1042081 );
			AddRes( index, typeof( MagicArrowScroll ), "Magic Arrow Scroll", 1, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 1, 1042081 );

			index = AddCraft(typeof(NightSightMagicStaff), "First Circle", "wand of night sight", 20.0, 50.0, typeof(DullCopperIngot), "Dull Copper Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 20.0, 50.0 );
			AddRes( index, typeof( StarSapphire ), "Star Sapphire", 1, 1042081 );
			AddRes( index, typeof( NightSightScroll ), "Night Sight Scroll", 1, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 1, 1042081 );

			index = AddCraft(typeof(ReactiveArmorMagicStaff), "First Circle", "wand of reactive armor", 20.0, 50.0, typeof(ShadowIronIngot), "Shadow Iron Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 20.0, 50.0 );
			AddRes( index, typeof( Emerald ), "Emerald", 1, 1042081 );
			AddRes( index, typeof( ReactiveArmorScroll ), "Reactive Armor Scroll", 1, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 1, 1042081 );

			index = AddCraft(typeof(WeaknessMagicStaff), "First Circle", "wand of weakness", 20.0, 50.0, typeof(AgapiteIngot), "Agapite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 20.0, 50.0 );
			AddRes( index, typeof( Amber ), "Amber", 1, 1042081 );
			AddRes( index, typeof( WeakenScroll ), "Weakness Scroll", 1, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 1, 1042081 );

			index = AddCraft(typeof(AgilityMagicStaff), "Second Circle", "wand of agility", 30.0, 60.0, typeof(ValoriteIngot), "Valorite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 30.0, 60.0 );
			AddRes( index, typeof( Tourmaline ), "Tourmaline", 2, 1042081 );
			AddRes( index, typeof( AgilityScroll ), "Agility Scroll", 2, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 2, 1042081 );

			index = AddCraft(typeof(CunningMagicStaff), "Second Circle", "wand of cunning", 30.0, 60.0, typeof(DullCopperIngot), "Dull Copper Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 30.0, 60.0 );
			AddRes( index, typeof( Ruby ), "Ruby", 2, 1042081 );
			AddRes( index, typeof( CunningScroll ), "Cunning Scroll", 2, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 2, 1042081 );

			index = AddCraft(typeof(CureMagicStaff), "Second Circle", "wand of curing", 30.0, 60.0, typeof(BronzeIngot), "Bronze Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 30.0, 60.0 );
			AddRes( index, typeof( Diamond ), "Diamond", 2, 1042081 );
			AddRes( index, typeof( CureScroll ), "Cure Scroll", 2, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 2, 1042081 );

			index = AddCraft(typeof(HarmMagicStaff), "Second Circle", "wand of harming", 30.0, 60.0, typeof(ShadowIronIngot), "Shadow Iron Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 30.0, 60.0 );
			AddRes( index, typeof( Sapphire ), "Sapphire", 2, 1042081 );
			AddRes( index, typeof( HarmScroll ), "Harm Scroll", 2, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 2, 1042081 );

			index = AddCraft(typeof(MagicTrapMagicStaff), "Second Circle", "wand of magical traps", 30.0, 60.0, typeof(AgapiteIngot), "Agapite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 30.0, 60.0 );
			AddRes( index, typeof( StarSapphire ), "Star Sapphire", 2, 1042081 );
			AddRes( index, typeof( MagicTrapScroll ), "Magic Trap Scroll", 2, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 2, 1042081 );

			index = AddCraft(typeof(MagicUntrapMagicStaff), "Second Circle", "wand of trap removal", 30.0, 60.0, typeof(VeriteIngot), "Verite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 30.0, 60.0 );
			AddRes( index, typeof( Sapphire ), "Sapphire", 2, 1042081 );
			AddRes( index, typeof( MagicUnTrapScroll ), "Magic Untrap Scroll", 2, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 2, 1042081 );

			index = AddCraft(typeof(ProtectionMagicStaff), "Second Circle", "wand of protection", 30.0, 60.0, typeof(CopperIngot), "Copper Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 30.0, 60.0 );
			AddRes( index, typeof( Tourmaline ), "Tourmaline", 2, 1042081 );
			AddRes( index, typeof( ProtectionScroll ), "Protection Scroll", 2, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 2, 1042081 );

			index = AddCraft(typeof(StrengthMagicStaff), "Second Circle", "wand of strength", 30.0, 60.0, typeof(ValoriteIngot), "Valorite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 30.0, 60.0 );
			AddRes( index, typeof( Amber ), "Amber", 2, 1042081 );
			AddRes( index, typeof( StrengthScroll ), "Strength Scroll", 2, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 2, 1042081 );

			index = AddCraft(typeof(BlessMagicStaff), "Third Circle", "wand of blessing", 40.0, 70.0, typeof(GoldIngot), "Gold Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 40.0, 70.0 );
			AddRes( index, typeof( Citrine ), "Citrine", 3, 1042081 );
			AddRes( index, typeof( BlessScroll ), "Bless Scroll", 3, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 3, 1042081 );

			index = AddCraft(typeof(FireballMagicStaff), "Third Circle", "wand of fireballs", 40.0, 70.0, typeof(DullCopperIngot), "Dull Copper Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 40.0, 70.0 );
			AddRes( index, typeof( Tourmaline ), "Tourmaline", 3, 1042081 );
			AddRes( index, typeof( FireballScroll ), "Fireball Scroll", 3, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 3, 1042081 );

			index = AddCraft(typeof(MagicLockMagicStaff), "Third Circle", "wand of magical locks", 40.0, 70.0, typeof(AgapiteIngot), "Agapite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 40.0, 70.0 );
			AddRes( index, typeof( Sapphire ), "Sapphire", 3, 1042081 );
			AddRes( index, typeof( MagicLockScroll ), "Magic Lock Scroll", 3, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 3, 1042081 );

			index = AddCraft(typeof(MagicUnlockMagicStaff), "Third Circle", "wand of unlocking", 40.0, 70.0, typeof(BronzeIngot), "Bronze Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 40.0, 70.0 );
			AddRes( index, typeof( Amethyst ), "Amethyst", 3, 1042081 );
			AddRes( index, typeof( UnlockScroll ), "Magic Unlock Scroll", 3, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 3, 1042081 );

			index = AddCraft(typeof(PoisonMagicStaff), "Third Circle", "wand of poisoning", 40.0, 70.0, typeof(CopperIngot), "Copper Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 40.0, 70.0 );
			AddRes( index, typeof( Ruby ), "Ruby", 3, 1042081 );
			AddRes( index, typeof( PoisonScroll ), "Poison Scroll", 3, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 3, 1042081 );

			index = AddCraft(typeof(TelekinesisMagicStaff), "Third Circle", "wand of telekinesis", 40.0, 70.0, typeof(VeriteIngot), "Verite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 40.0, 70.0 );
			AddRes( index, typeof( Emerald ), "Emerald", 3, 1042081 );
			AddRes( index, typeof( TelekinisisScroll ), "Telekinesis Scroll", 3, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 3, 1042081 );

			index = AddCraft(typeof(TeleportMagicStaff), "Third Circle", "wand of teleporting", 40.0, 70.0, typeof(ShadowIronIngot), "Shadow Iron Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 40.0, 70.0 );
			AddRes( index, typeof( Diamond ), "Diamond", 3, 1042081 );
			AddRes( index, typeof( TeleportScroll ), "Teleport Scroll", 3, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 3, 1042081 );

			index = AddCraft(typeof(WallofStoneMagicStaff), "Third Circle", "wand of stone wall", 40.0, 70.0, typeof(GoldIngot), "Gold Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 40.0, 70.0 );
			AddRes( index, typeof( Amethyst ), "Amethyst", 3, 1042081 );
			AddRes( index, typeof( WallOfStoneScroll ), "Wall Of Stone Scroll", 3, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 3, 1042081 );

			index = AddCraft(typeof(ArchCureMagicStaff), "Fourth Circle", "wand of arch curing", 50.0, 80.0, typeof(ValoriteIngot), "Valorite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 50.0, 80.0 );
			AddRes( index, typeof( Sapphire ), "Sapphire", 4, 1042081 );
			AddRes( index, typeof( ArchCureScroll ), "Arch Cure Scroll", 4, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 4, 1042081 );

			index = AddCraft(typeof(ArchProtectionMagicStaff), "Fourth Circle", "wand of arch protection", 50.0, 80.0, typeof(ShadowIronIngot), "Shadow Iron Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 50.0, 80.0 );
			AddRes( index, typeof( Diamond ), "Diamond", 4, 1042081 );
			AddRes( index, typeof( ArchProtectionScroll ), "Arch Protection Scroll", 4, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 4, 1042081 );

			index = AddCraft(typeof(CurseMagicStaff), "Fourth Circle", "wand of curses", 50.0, 80.0, typeof(BronzeIngot), "Bronze Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 50.0, 80.0 );
			AddRes( index, typeof( Amethyst ), "Amethyst", 4, 1042081 );
			AddRes( index, typeof( CurseScroll ), "Curse Scroll", 4, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 4, 1042081 );

			index = AddCraft(typeof(FireFieldMagicStaff), "Fourth Circle", "wand of fire fields", 50.0, 80.0, typeof(DullCopperIngot), "Dull Copper Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 50.0, 80.0 );
			AddRes( index, typeof( Amethyst ), "Amethyst", 4, 1042081 );
			AddRes( index, typeof( FireFieldScroll ), "Fire Field Scroll", 4, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 4, 1042081 );

			index = AddCraft(typeof(GreaterHealMagicStaff), "Fourth Circle", "wand of greater healing", 50.0, 80.0, typeof(GoldIngot), "Gold Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 50.0, 80.0 );
			AddRes( index, typeof( Emerald ), "Emerald", 4, 1042081 );
			AddRes( index, typeof( GreaterHealScroll ), "Greater Heal Scroll", 4, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 4, 1042081 );

			index = AddCraft(typeof(LightningMagicStaff), "Fourth Circle", "wand of lightning bolts", 50.0, 80.0, typeof(VeriteIngot), "Verite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 50.0, 80.0 );
			AddRes( index, typeof( Tourmaline ), "Tourmaline", 4, 1042081 );
			AddRes( index, typeof( LightningScroll ), "Lightning Scroll", 4, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 4, 1042081 );

			index = AddCraft(typeof(ManaDrainMagicStaff), "Fourth Circle", "wand of mana draining", 50.0, 80.0, typeof(AgapiteIngot), "Agapite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 50.0, 80.0 );
			AddRes( index, typeof( StarSapphire ), "Star Sapphire", 4, 1042081 );
			AddRes( index, typeof( ManaDrainScroll ), "Mana Drain Scroll", 4, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 4, 1042081 );

			index = AddCraft(typeof(RecallMagicStaff), "Fourth Circle", "wand of recalling", 50.0, 80.0, typeof(VeriteIngot), "Verite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 50.0, 80.0 );
			AddRes( index, typeof( Amethyst ), "Amethyst", 4, 1042081 );
			AddRes( index, typeof( RecallScroll ), "Recall Scroll", 4, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 4, 1042081 );

			index = AddCraft(typeof(BladeSpiritsMagicStaff), "Fifth Circle", "wand of blade spirits", 60.0, 90.0, typeof(StarRubyIngot), "Star Ruby Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 60.0, 90.0 );
			AddRes( index, typeof( Emerald ), "Emerald", 5, 1042081 );
			AddRes( index, typeof( BladeSpiritsScroll ), "Blade Spirit Scroll", 5, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 5, 1042081 );

			index = AddCraft(typeof(DispelFieldMagicStaff), "Fifth Circle", "wand of dispelling fields", 60.0, 90.0, typeof(OnyxIngot), "Onyx Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 60.0, 90.0 );
			AddRes( index, typeof( Sapphire ), "Sapphire", 5, 1042081 );
			AddRes( index, typeof( DispelFieldScroll ), "Dispel Field Scroll", 5, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 5, 1042081 );

			index = AddCraft(typeof(IncognitoMagicStaff), "Fifth Circle", "wand of disguises", 60.0, 90.0, typeof(GarnetIngot), "Garnet Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 60.0, 90.0 );
			AddRes( index, typeof( StarSapphire ), "Star Sapphire", 5, 1042081 );
			AddRes( index, typeof( IncognitoScroll ), "Incognito Scroll", 5, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 5, 1042081 );

			index = AddCraft(typeof(MagicReflectionMagicStaff), "Fifth Circle", "wand of magical reflection", 60.0, 90.0, typeof(NepturiteIngot), "Nepturite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 60.0, 90.0 );
			AddRes( index, typeof( Ruby ), "Ruby", 5, 1042081 );
			AddRes( index, typeof( MagicReflectScroll ), "Magic Reflection Scroll", 5, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 5, 1042081 );

			index = AddCraft(typeof(MindBlastMagicStaff), "Fifth Circle", "wand of mind blasting", 60.0, 90.0, typeof(AmethystIngot), "Amethyst Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 60.0, 90.0 );
			AddRes( index, typeof( Tourmaline ), "Tourmaline", 5, 1042081 );
			AddRes( index, typeof( MindBlastScroll ), "Mind Blast Scroll", 5, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 5, 1042081 );

			index = AddCraft(typeof(ParalyzeMagicStaff), "Fifth Circle", "wand of paralyzing", 60.0, 90.0, typeof(ObsidianIngot), "Obsidian Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 60.0, 90.0 );
			AddRes( index, typeof( Ruby ), "Ruby", 5, 1042081 );
			AddRes( index, typeof( ParalyzeScroll ), "Paralyze Scroll", 5, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 5, 1042081 );

			index = AddCraft(typeof(PoisonFieldMagicStaff), "Fifth Circle", "wand of poisonous fields", 60.0, 90.0, typeof(EmeraldIngot), "Emerald Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 60.0, 90.0 );
			AddRes( index, typeof( StarSapphire ), "Star Sapphire", 5, 1042081 );
			AddRes( index, typeof( PoisonFieldScroll ), "Poison Field Scroll", 5, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 5, 1042081 );

			index = AddCraft(typeof(SummonCreatureMagicStaff), "Fifth Circle", "wand of creature summoning", 60.0, 90.0, typeof(SapphireIngot), "Sapphire Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 60.0, 90.0 );
			AddRes( index, typeof( Citrine ), "Citrine", 5, 1042081 );
			AddRes( index, typeof( SummonCreatureScroll ), "Summon Creature Scroll", 5, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 5, 1042081 );

			index = AddCraft(typeof(DispelMagicStaff), "Sixth Circle", "wand of dispelling", 70.0, 100.0, typeof(MithrilIngot), "Mithril Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 70.0, 100.0 );
			AddRes( index, typeof( Amber ), "Amber", 6, 1042081 );
			AddRes( index, typeof( DispelScroll ), "Dispel Magic Scroll", 6, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 6, 1042081 );

			index = AddCraft(typeof(EnergyBoltMagicStaff), "Sixth Circle", "wand of energy bolts", 70.0, 100.0, typeof(ShinySilverIngot), "Silver Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 70.0, 100.0 );
			AddRes( index, typeof( Tourmaline ), "Tourmaline", 6, 1042081 );
			AddRes( index, typeof( EnergyBoltScroll ), "Energy Bolt Scroll", 6, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 6, 1042081 );

			index = AddCraft(typeof(ExplosionMagicStaff), "Sixth Circle", "wand of explosions", 70.0, 100.0, typeof(SpinelIngot), "Spinel Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 70.0, 100.0 );
			AddRes( index, typeof( Amethyst ), "Amethyst", 6, 1042081 );
			AddRes( index, typeof( ExplosionScroll ), "Explosion Scroll", 6, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 6, 1042081 );

			index = AddCraft(typeof(InvisibilityMagicStaff), "Sixth Circle", "wand of invisibility", 70.0, 100.0, typeof(RubyIngot), "Ruby Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 70.0, 100.0 );
			AddRes( index, typeof( Amber ), "Amber", 6, 1042081 );
			AddRes( index, typeof( InvisibilityScroll ), "Invisibility Scroll", 6, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 6, 1042081 );

			index = AddCraft(typeof(MarkMagicStaff), "Sixth Circle", "wand of marking", 70.0, 100.0, typeof(BrassIngot), "Brass Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 70.0, 100.0 );
			AddRes( index, typeof( Diamond ), "Diamond", 6, 1042081 );
			AddRes( index, typeof( MarkScroll ), "Mark Scroll", 6, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 6, 1042081 );

			index = AddCraft(typeof(MassCurseMagicStaff), "Sixth Circle", "wand of mass curses", 70.0, 100.0, typeof(SteelIngot), "Steel Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 70.0, 100.0 );
			AddRes( index, typeof( Amber ), "Amber", 6, 1042081 );
			AddRes( index, typeof( MassCurseScroll ), "Mass Curse Scroll", 6, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 6, 1042081 );

			index = AddCraft(typeof(ParalyzeFieldMagicStaff), "Sixth Circle", "wand of paralyzing fields", 70.0, 100.0, typeof(JadeIngot), "Jade Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 70.0, 100.0 );
			AddRes( index, typeof( Ruby ), "Ruby", 6, 1042081 );
			AddRes( index, typeof( ParalyzeFieldScroll ), "Paralyze Field Scroll", 6, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 6, 1042081 );

			index = AddCraft(typeof(RevealMagicStaff), "Sixth Circle", "wand of revealing", 70.0, 100.0, typeof(TopazIngot), "Topaz Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 70.0, 100.0 );
			AddRes( index, typeof( Sapphire ), "Sapphire", 6, 1042081 );
			AddRes( index, typeof( RevealScroll ), "Reveal Scroll", 6, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 6, 1042081 );

			index = AddCraft(typeof(ChainLightningMagicStaff), "Seventh Circle", "wand of chain lightning", 80.0, 110.0, typeof(QuartzIngot), "Quartz Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
			AddRes( index, typeof( Ruby ), "Ruby", 7, 1042081 );
			AddRes( index, typeof( ChainLightningScroll ), "Chain Lightning Scroll", 7, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 7, 1042081 );

			index = AddCraft(typeof(EnergyFieldMagicStaff), "Seventh Circle", "wand of energy fields", 80.0, 110.0, typeof(AmethystIngot), "Amethyst Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
			AddRes( index, typeof( StarSapphire ), "Star Sapphire", 7, 1042081 );
			AddRes( index, typeof( EnergyFieldScroll ), "Energy Field Scroll", 7, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 7, 1042081 );

			index = AddCraft(typeof(FlameStrikeMagicStaff), "Seventh Circle", "wand of flame striking", 80.0, 110.0, typeof(SteelIngot), "Steel Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
			AddRes( index, typeof( Diamond ), "Diamond", 7, 1042081 );
			AddRes( index, typeof( FlamestrikeScroll ), "Flame Strike Scroll", 7, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 7, 1042081 );

			index = AddCraft(typeof(GateTravelMagicStaff), "Seventh Circle", "wand of gate travels", 80.0, 110.0, typeof(MithrilIngot), "Mithril Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
			AddRes( index, typeof( Citrine ), "Citrine", 7, 1042081 );
			AddRes( index, typeof( GateTravelScroll ), "Gate Travel Scroll", 7, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 7, 1042081 );

			index = AddCraft(typeof(ManaVampireMagicStaff), "Seventh Circle", "wand of mana vampire", 80.0, 110.0, typeof(GarnetIngot), "Garnet Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
			AddRes( index, typeof( Citrine ), "Citrine", 7, 1042081 );
			AddRes( index, typeof( ManaVampireScroll ), "Mana Vampire Scroll", 7, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 7, 1042081 );

			index = AddCraft(typeof(MassDispelMagicStaff), "Seventh Circle", "wand of mass dispelling", 80.0, 110.0, typeof(StarRubyIngot), "Star Ruby Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
			AddRes( index, typeof( Sapphire ), "Sapphire", 7, 1042081 );
			AddRes( index, typeof( MassDispelScroll ), "Mass Dispel Scroll", 7, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 7, 1042081 );

			index = AddCraft(typeof(MeteorSwarmMagicStaff), "Seventh Circle", "wand of meteor swarms", 80.0, 110.0, typeof(SpinelIngot), "Spinel Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
			AddRes( index, typeof( Emerald ), "Emerald", 7, 1042081 );
			AddRes( index, typeof( MeteorSwarmScroll ), "Meteor Swarm Scroll", 7, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 7, 1042081 );

			index = AddCraft(typeof(PolymorphMagicStaff), "Seventh Circle", "wand of polymorphing", 80.0, 110.0, typeof(BrassIngot), "Brass Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
			AddRes( index, typeof( Emerald ), "Emerald", 7, 1042081 );
			AddRes( index, typeof( PolymorphScroll ), "Polymorph Scroll", 7, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 7, 1042081 );

			index = AddCraft(typeof(AirElementalMagicStaff), "Eighth Circle", "wand of air elementals", 90.0, 120.0, typeof(QuartzIngot), "Quartz Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 90.0, 120.0 );
			AddRes( index, typeof( Emerald ), "Emerald", 8, 1042081 );
			AddRes( index, typeof( SummonAirElementalScroll ), "Summon Air Elemental Scroll", 8, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 8, 1042081 );

			index = AddCraft(typeof(EarthElementalMagicStaff), "Eighth Circle", "wand of earth elementals", 90.0, 120.0, typeof(EmeraldIngot), "Emerald Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 90.0, 120.0 );
			AddRes( index, typeof( Diamond ), "Diamond", 8, 1042081 );
			AddRes( index, typeof( SummonEarthElementalScroll ), "Summon Earth Elemental Scroll", 8, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 8, 1042081 );

			index = AddCraft(typeof(EarthquakeMagicStaff), "Eighth Circle", "wand of earthquakes", 90.0, 120.0, typeof(SapphireIngot), "Sapphire Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 90.0, 120.0 );
			AddRes( index, typeof( Citrine ), "Citrine", 8, 1042081 );
			AddRes( index, typeof( EarthquakeScroll ), "Earthquake Scroll", 8, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 8, 1042081 );

			index = AddCraft(typeof(EnergyVortexMagicStaff), "Eighth Circle", "wand of vortex summoning", 90.0, 120.0, typeof(ShinySilverIngot), "Silver Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 90.0, 120.0 );
			AddRes( index, typeof( Citrine ), "Citrine", 8, 1042081 );
			AddRes( index, typeof( EnergyVortexScroll ), "Energy Vortex Scroll", 8, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 8, 1042081 );

			index = AddCraft(typeof(FireElementalMagicStaff), "Eighth Circle", "wand of fire elementals", 90.0, 120.0, typeof(RubyIngot), "Ruby Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 90.0, 120.0 );
			AddRes( index, typeof( Amethyst ), "Amethyst", 8, 1042081 );
			AddRes( index, typeof( SummonFireElementalScroll ), "Summon Fire Elemental Scroll", 8, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 8, 1042081 );

			index = AddCraft(typeof(ResurrectionMagicStaff), "Eighth Circle", "wand of resurrecting", 90.0, 120.0, typeof(NepturiteIngot), "Nepturite Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 90.0, 120.0 );
			AddRes( index, typeof( Amber ), "Amber", 8, 1042081 );
			AddRes( index, typeof( ResurrectionScroll ), "Resurrection Scroll", 8, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 8, 1042081 );

			index = AddCraft(typeof(SummonDaemonMagicStaff), "Eighth Circle", "wand of daemon summoning", 90.0, 120.0, typeof(ObsidianIngot), "Obsidian Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 90.0, 120.0 );
			AddRes( index, typeof( Amber ), "Amber", 8, 1042081 );
			AddRes( index, typeof( SummonDaemonScroll ), "Summon Daemon Scroll", 8, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 8, 1042081 );

			index = AddCraft(typeof(WaterElementalMagicStaff), "Eighth Circle", "wand of water elementals", 90.0, 120.0, typeof(JadeIngot), "Jade Ingot", 3, 1042081);
			AddSkill( index, SkillName.Magery, 90.0, 120.0 );
			AddRes( index, typeof( Ruby ), "Ruby", 8, 1042081 );
			AddRes( index, typeof( SummonWaterElementalScroll ), "Summon Water Elemental Scroll", 8, 1042081 );
			AddRes( index, typeof( ArcaneGem ), "Arcane Gem", 8, 1042081 );
		}
	}
}