using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefGodSmithing : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Blacksmith; }
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
 
        public override string GumpTitleString
        {
            get { return "<BASEFONT Color=#FBFBFB><CENTER>MAGICAL SMITHING MENU</CENTER></BASEFONT>"; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGodSmithing();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefGodSmithing() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			if ( from.Map == Map.TerMur && from.X > 1104 && from.X < 1125 && from.Y > 1960 && from.Y < 1978 )
				return 0;

			return 501816;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x541 );
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
				return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Armor

			AddCraft( typeof( AmethystPlateArms ), "Amethyst & Emerald", "Amethyst Arms", 90.0, 125.0, typeof( AmethystIngot ), "Block of Amethyst", 18, 1042081 );
			AddCraft( typeof( AmethystPlateGloves ), "Amethyst & Emerald", "Amethyst Gauntlets", 90.0, 125.0, typeof( AmethystIngot ), "Block of Amethyst", 12, 1042081 );
			AddCraft( typeof( AmethystPlateGorget ), "Amethyst & Emerald", "Amethyst Gorget", 90.0, 125.0, typeof( AmethystIngot ), "Block of Amethyst", 10, 1042081 );
			AddCraft( typeof( AmethystPlateLegs ), "Amethyst & Emerald", "Amethyst Leggings", 90.0, 125.0, typeof( AmethystIngot ), "Block of Amethyst", 20, 1042081 );
			AddCraft( typeof( AmethystPlateChest ), "Amethyst & Emerald", "Amethyst Tunic", 90.0, 125.0, typeof( AmethystIngot ), "Block of Amethyst", 25, 1042081 );
			AddCraft( typeof( AmethystFemalePlateChest ), "Amethyst & Emerald", "Amethyst Female Tunic", 90.0, 125.0, typeof( AmethystIngot ), "Block of Amethyst", 20, 1042081 );
			AddCraft( typeof( AmethystPlateHelm ), "Amethyst & Emerald", "Amethyst Helm", 90.0, 125.0, typeof( AmethystIngot ), "Block of Amethyst", 15, 1042081 );
			AddCraft( typeof( AmethystShield ), "Amethyst & Emerald", "Amethyst Shield", 90.0, 125.0, typeof( AmethystIngot ), "Block of Amethyst", 18, 1042081 );
			index = AddCraft( typeof( OilAmethyst ), "Amethyst & Emerald", "Amethyst Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( AmethystIngot ), "Block of Amethyst", 30, 1042081 );

			AddCraft( typeof( EmeraldPlateArms ), "Amethyst & Emerald", "Emerald Arms", 90.0, 125.0, typeof( EmeraldIngot ), "Block of Emerald", 18, 1042081 );
			AddCraft( typeof( EmeraldPlateGloves ), "Amethyst & Emerald", "Emerald Gauntlets", 90.0, 125.0, typeof( EmeraldIngot ), "Block of Emerald", 12, 1042081 );
			AddCraft( typeof( EmeraldPlateGorget ), "Amethyst & Emerald", "Emerald Gorget", 90.0, 125.0, typeof( EmeraldIngot ), "Block of Emerald", 10, 1042081 );
			AddCraft( typeof( EmeraldPlateLegs ), "Amethyst & Emerald", "Emerald Leggings", 90.0, 125.0, typeof( EmeraldIngot ), "Block of Emerald", 20, 1042081 );
			AddCraft( typeof( EmeraldPlateChest ), "Amethyst & Emerald", "Emerald Tunic", 90.0, 125.0, typeof( EmeraldIngot ), "Block of Emerald", 25, 1042081 );
			AddCraft( typeof( EmeraldFemalePlateChest ), "Amethyst & Emerald", "Emerald Female Tunic", 90.0, 125.0, typeof( EmeraldIngot ), "Block of Emerald", 20, 1042081 );
			AddCraft( typeof( EmeraldPlateHelm ), "Amethyst & Emerald", "Emerald Helm", 90.0, 125.0, typeof( EmeraldIngot ), "Block of Emerald", 15, 1042081 );
			AddCraft( typeof( EmeraldShield ), "Amethyst & Emerald", "Emerald Shield", 90.0, 125.0, typeof( EmeraldIngot ), "Block of Emerald", 18, 1042081 );
			index = AddCraft( typeof( OilEmerald ), "Amethyst & Emerald", "Emerald Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( EmeraldIngot ), "Block of Emerald", 30, 1042081 );

			AddCraft( typeof( GarnetPlateArms ), "Garnet & Ice", "Garnet Arms", 90.0, 125.0, typeof( GarnetIngot ), "Block of Garnet", 18, 1042081 );
			AddCraft( typeof( GarnetPlateGloves ), "Garnet & Ice", "Garnet Gauntlets", 90.0, 125.0, typeof( GarnetIngot ), "Block of Garnet", 12, 1042081 );
			AddCraft( typeof( GarnetPlateGorget ), "Garnet & Ice", "Garnet Gorget", 90.0, 125.0, typeof( GarnetIngot ), "Block of Garnet", 10, 1042081 );
			AddCraft( typeof( GarnetPlateLegs ), "Garnet & Ice", "Garnet Leggings", 90.0, 125.0, typeof( GarnetIngot ), "Block of Garnet", 20, 1042081 );
			AddCraft( typeof( GarnetPlateChest ), "Garnet & Ice", "Garnet Tunic", 90.0, 125.0, typeof( GarnetIngot ), "Block of Garnet", 25, 1042081 );
			AddCraft( typeof( GarnetFemalePlateChest ), "Garnet & Ice", "Garnet Female Tunic", 90.0, 125.0, typeof( GarnetIngot ), "Block of Garnet", 20, 1042081 );
			AddCraft( typeof( GarnetPlateHelm ), "Garnet & Ice", "Garnet Helm", 90.0, 125.0, typeof( GarnetIngot ), "Block of Garnet", 15, 1042081 );
			AddCraft( typeof( GarnetShield ), "Garnet & Ice", "Garnet Shield", 90.0, 125.0, typeof( GarnetIngot ), "Block of Garnet", 18, 1042081 );
			index = AddCraft( typeof( OilGarnet ), "Garnet & Ice", "Garnet Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( GarnetIngot ), "Block of Garnet", 30, 1042081 );

			AddCraft( typeof( IcePlateArms ), "Garnet & Ice", "Ice Arms", 90.0, 125.0, typeof( IceIngot ), "Block of Ice", 18, 1042081 );
			AddCraft( typeof( IcePlateGloves ), "Garnet & Ice", "Ice Gauntlets", 90.0, 125.0, typeof( IceIngot ), "Block of Ice", 12, 1042081 );
			AddCraft( typeof( IcePlateGorget ), "Garnet & Ice", "Ice Gorget", 90.0, 125.0, typeof( IceIngot ), "Block of Ice", 10, 1042081 );
			AddCraft( typeof( IcePlateLegs ), "Garnet & Ice", "Ice Leggings", 90.0, 125.0, typeof( IceIngot ), "Block of Ice", 20, 1042081 );
			AddCraft( typeof( IcePlateChest ), "Garnet & Ice", "Ice Tunic", 90.0, 125.0, typeof( IceIngot ), "Block of Ice", 25, 1042081 );
			AddCraft( typeof( IceFemalePlateChest ), "Garnet & Ice", "Ice Female Tunic", 90.0, 125.0, typeof( IceIngot ), "Block of Ice", 20, 1042081 );
			AddCraft( typeof( IcePlateHelm ), "Garnet & Ice", "Ice Helm", 90.0, 125.0, typeof( IceIngot ), "Block of Ice", 15, 1042081 );
			AddCraft( typeof( IceShield ), "Garnet & Ice", "Ice Shield", 90.0, 125.0, typeof( IceIngot ), "Block of Ice", 18, 1042081 );
			index = AddCraft( typeof( OilIce ), "Garnet & Ice", "Ice Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( IceIngot ), "Block of Ice", 30, 1042081 );

			AddCraft( typeof( JadePlateArms ), "Jade & Marble", "Jade Arms", 90.0, 125.0, typeof( JadeIngot ), "Block of Jade", 18, 1042081 );
			AddCraft( typeof( JadePlateGloves ), "Jade & Marble", "Jade Gauntlets", 90.0, 125.0, typeof( JadeIngot ), "Block of Jade", 12, 1042081 );
			AddCraft( typeof( JadePlateGorget ), "Jade & Marble", "Jade Gorget", 90.0, 125.0, typeof( JadeIngot ), "Block of Jade", 10, 1042081 );
			AddCraft( typeof( JadePlateLegs ), "Jade & Marble", "Jade Leggings", 90.0, 125.0, typeof( JadeIngot ), "Block of Jade", 20, 1042081 );
			AddCraft( typeof( JadePlateChest ), "Jade & Marble", "Jade Tunic", 90.0, 125.0, typeof( JadeIngot ), "Block of Jade", 25, 1042081 );
			AddCraft( typeof( JadeFemalePlateChest ), "Jade & Marble", "Jade Female Tunic", 90.0, 125.0, typeof( JadeIngot ), "Block of Jade", 20, 1042081 );
			AddCraft( typeof( JadePlateHelm ), "Jade & Marble", "Jade Helm", 90.0, 125.0, typeof( JadeIngot ), "Block of Jade", 15, 1042081 );
			AddCraft( typeof( JadeShield ), "Jade & Marble", "Jade Shield", 90.0, 125.0, typeof( JadeIngot ), "Block of Jade", 18, 1042081 );
			index = AddCraft( typeof( OilJade ), "Jade & Marble", "Jade Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( JadeIngot ), "Block of Jade", 30, 1042081 );

			AddCraft( typeof( MarblePlateArms ), "Jade & Marble", "Marble Arms", 90.0, 125.0, typeof( MarbleIngot ), "Block of Marble", 18, 1042081 );
			AddCraft( typeof( MarblePlateGloves ), "Jade & Marble", "Marble Gauntlets", 90.0, 125.0, typeof( MarbleIngot ), "Block of Marble", 12, 1042081 );
			AddCraft( typeof( MarblePlateGorget ), "Jade & Marble", "Marble Gorget", 90.0, 125.0, typeof( MarbleIngot ), "Block of Marble", 10, 1042081 );
			AddCraft( typeof( MarblePlateLegs ), "Jade & Marble", "Marble Leggings", 90.0, 125.0, typeof( MarbleIngot ), "Block of Marble", 20, 1042081 );
			AddCraft( typeof( MarblePlateChest ), "Jade & Marble", "Marble Tunic", 90.0, 125.0, typeof( MarbleIngot ), "Block of Marble", 25, 1042081 );
			AddCraft( typeof( MarbleFemalePlateChest ), "Jade & Marble", "Marble Female Tunic", 90.0, 125.0, typeof( MarbleIngot ), "Block of Marble", 20, 1042081 );
			AddCraft( typeof( MarblePlateHelm ), "Jade & Marble", "Marble Helm", 90.0, 125.0, typeof( MarbleIngot ), "Block of Marble", 15, 1042081 );
			AddCraft( typeof( MarbleShields ), "Jade & Marble", "Marble Shield", 90.0, 125.0, typeof( MarbleIngot ), "Block of Marble", 18, 1042081 );
			index = AddCraft( typeof( OilMarble ), "Jade & Marble", "Marble Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( MarbleIngot ), "Block of Marble", 30, 1042081 );

			AddCraft( typeof( OnyxPlateArms ), "Onyx & Quartz", "Onyx Arms", 90.0, 125.0, typeof( OnyxIngot ), "Block of Onyx", 18, 1042081 );
			AddCraft( typeof( OnyxPlateGloves ), "Onyx & Quartz", "Onyx Gauntlets", 90.0, 125.0, typeof( OnyxIngot ), "Block of Onyx", 12, 1042081 );
			AddCraft( typeof( OnyxPlateGorget ), "Onyx & Quartz", "Onyx Gorget", 90.0, 125.0, typeof( OnyxIngot ), "Block of Onyx", 10, 1042081 );
			AddCraft( typeof( OnyxPlateLegs ), "Onyx & Quartz", "Onyx Leggings", 90.0, 125.0, typeof( OnyxIngot ), "Block of Onyx", 20, 1042081 );
			AddCraft( typeof( OnyxPlateChest ), "Onyx & Quartz", "Onyx Tunic", 90.0, 125.0, typeof( OnyxIngot ), "Block of Onyx", 25, 1042081 );
			AddCraft( typeof( OnyxFemalePlateChest ), "Onyx & Quartz", "Onyx Female Tunic", 90.0, 125.0, typeof( OnyxIngot ), "Block of Onyx", 20, 1042081 );
			AddCraft( typeof( OnyxPlateHelm ), "Onyx & Quartz", "Onyx Helm", 90.0, 125.0, typeof( OnyxIngot ), "Block of Onyx", 15, 1042081 );
			AddCraft( typeof( OnyxShield ), "Onyx & Quartz", "Onyx Shield", 90.0, 125.0, typeof( OnyxIngot ), "Block of Onyx", 18, 1042081 );
			index = AddCraft( typeof( OilOnyx ), "Onyx & Quartz", "Onyx Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( OnyxIngot ), "Block of Onyx", 30, 1042081 );

			AddCraft( typeof( QuartzPlateArms ), "Onyx & Quartz", "Quartz Arms", 90.0, 125.0, typeof( QuartzIngot ), "Block of Quartz", 18, 1042081 );
			AddCraft( typeof( QuartzPlateGloves ), "Onyx & Quartz", "Quartz Gauntlets", 90.0, 125.0, typeof( QuartzIngot ), "Block of Quartz", 12, 1042081 );
			AddCraft( typeof( QuartzPlateGorget ), "Onyx & Quartz", "Quartz Gorget", 90.0, 125.0, typeof( QuartzIngot ), "Block of Quartz", 10, 1042081 );
			AddCraft( typeof( QuartzPlateLegs ), "Onyx & Quartz", "Quartz Leggings", 90.0, 125.0, typeof( QuartzIngot ), "Block of Quartz", 20, 1042081 );
			AddCraft( typeof( QuartzPlateChest ), "Onyx & Quartz", "Quartz Tunic", 90.0, 125.0, typeof( QuartzIngot ), "Block of Quartz", 25, 1042081 );
			AddCraft( typeof( QuartzFemalePlateChest ), "Onyx & Quartz", "Quartz Female Tunic", 90.0, 125.0, typeof( QuartzIngot ), "Block of Quartz", 20, 1042081 );
			AddCraft( typeof( QuartzPlateHelm ), "Onyx & Quartz", "Quartz Helm", 90.0, 125.0, typeof( QuartzIngot ), "Block of Quartz", 15, 1042081 );
			AddCraft( typeof( QuartzShield ), "Onyx & Quartz", "Quartz Shield", 90.0, 125.0, typeof( QuartzIngot ), "Block of Quartz", 18, 1042081 );
			index = AddCraft( typeof( OilQuartz ), "Onyx & Quartz", "Quartz Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( QuartzIngot ), "Block of Quartz", 30, 1042081 );

			AddCraft( typeof( RubyPlateArms ), "Ruby & Sapphire", "Ruby Arms", 90.0, 125.0, typeof( RubyIngot ), "Block of Ruby", 18, 1042081 );
			AddCraft( typeof( RubyPlateGloves ), "Ruby & Sapphire", "Ruby Gauntlets", 90.0, 125.0, typeof( RubyIngot ), "Block of Ruby", 12, 1042081 );
			AddCraft( typeof( RubyPlateGorget ), "Ruby & Sapphire", "Ruby Gorget", 90.0, 125.0, typeof( RubyIngot ), "Block of Ruby", 10, 1042081 );
			AddCraft( typeof( RubyPlateLegs ), "Ruby & Sapphire", "Ruby Leggings", 90.0, 125.0, typeof( RubyIngot ), "Block of Ruby", 20, 1042081 );
			AddCraft( typeof( RubyPlateChest ), "Ruby & Sapphire", "Ruby Tunic", 90.0, 125.0, typeof( RubyIngot ), "Block of Ruby", 25, 1042081 );
			AddCraft( typeof( RubyFemalePlateChest ), "Ruby & Sapphire", "Ruby Female Tunic", 90.0, 125.0, typeof( RubyIngot ), "Block of Ruby", 20, 1042081 );
			AddCraft( typeof( RubyPlateHelm ), "Ruby & Sapphire", "Ruby Helm", 90.0, 125.0, typeof( RubyIngot ), "Block of Ruby", 15, 1042081 );
			AddCraft( typeof( RubyShield ), "Ruby & Sapphire", "Ruby Shield", 90.0, 125.0, typeof( RubyIngot ), "Block of Ruby", 18, 1042081 );
			index = AddCraft( typeof( OilRuby ), "Ruby & Sapphire", "Ruby Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( RubyIngot ), "Block of Ruby", 30, 1042081 );

			AddCraft( typeof( SapphirePlateArms ), "Ruby & Sapphire", "Sapphire Arms", 90.0, 125.0, typeof( SapphireIngot ), "Block of Sapphire", 18, 1042081 );
			AddCraft( typeof( SapphirePlateGloves ), "Ruby & Sapphire", "Sapphire Gauntlets", 90.0, 125.0, typeof( SapphireIngot ), "Block of Sapphire", 12, 1042081 );
			AddCraft( typeof( SapphirePlateGorget ), "Ruby & Sapphire", "Sapphire Gorget", 90.0, 125.0, typeof( SapphireIngot ), "Block of Sapphire", 10, 1042081 );
			AddCraft( typeof( SapphirePlateLegs ), "Ruby & Sapphire", "Sapphire Leggings", 90.0, 125.0, typeof( SapphireIngot ), "Block of Sapphire", 20, 1042081 );
			AddCraft( typeof( SapphirePlateChest ), "Ruby & Sapphire", "Sapphire Tunic", 90.0, 125.0, typeof( SapphireIngot ), "Block of Sapphire", 25, 1042081 );
			AddCraft( typeof( SapphireFemalePlateChest ), "Ruby & Sapphire", "Sapphire Female Tunic", 90.0, 125.0, typeof( SapphireIngot ), "Block of Sapphire", 20, 1042081 );
			AddCraft( typeof( SapphirePlateHelm ), "Ruby & Sapphire", "Sapphire Helm", 90.0, 125.0, typeof( SapphireIngot ), "Block of Sapphire", 15, 1042081 );
			AddCraft( typeof( SapphireShield ), "Ruby & Sapphire", "Sapphire Shield", 90.0, 125.0, typeof( SapphireIngot ), "Block of Sapphire", 18, 1042081 );
			index = AddCraft( typeof( OilSapphire ), "Ruby & Sapphire", "Sapphire Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( SapphireIngot ), "Block of Sapphire", 30, 1042081 );

			AddCraft( typeof( SilverPlateArms ), "Silver & Spinel", "Silver Arms", 90.0, 125.0, typeof( ShinySilverIngot ), "Block of Silver", 18, 1042081 );
			AddCraft( typeof( SilverPlateGloves ), "Silver & Spinel", "Silver Gauntlets", 90.0, 125.0, typeof( ShinySilverIngot ), "Block of Silver", 12, 1042081 );
			AddCraft( typeof( SilverPlateGorget ), "Silver & Spinel", "Silver Gorget", 90.0, 125.0, typeof( ShinySilverIngot ), "Block of Silver", 10, 1042081 );
			AddCraft( typeof( SilverPlateLegs ), "Silver & Spinel", "Silver Leggings", 90.0, 125.0, typeof( ShinySilverIngot ), "Block of Silver", 20, 1042081 );
			AddCraft( typeof( SilverPlateChest ), "Silver & Spinel", "Silver Tunic", 90.0, 125.0, typeof( ShinySilverIngot ), "Block of Silver", 25, 1042081 );
			AddCraft( typeof( SilverFemalePlateChest ), "Silver & Spinel", "Silver Female Tunic", 90.0, 125.0, typeof( ShinySilverIngot ), "Block of Silver", 20, 1042081 );
			AddCraft( typeof( SilverPlateHelm ), "Silver & Spinel", "Silver Helm", 90.0, 125.0, typeof( ShinySilverIngot ), "Block of Silver", 15, 1042081 );
			AddCraft( typeof( SilverShield ), "Silver & Spinel", "Silver Shield", 90.0, 125.0, typeof( ShinySilverIngot ), "Block of Silver", 18, 1042081 );
			index = AddCraft( typeof( OilSilver ), "Silver & Spinel", "Silver Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( ShinySilverIngot ), "Block of Silver", 30, 1042081 );

			AddCraft( typeof( SpinelPlateArms ), "Silver & Spinel", "Spinel Arms", 90.0, 125.0, typeof( SpinelIngot ), "Block of Spinel", 18, 1042081 );
			AddCraft( typeof( SpinelPlateGloves ), "Silver & Spinel", "Spinel Gauntlets", 90.0, 125.0, typeof( SpinelIngot ), "Block of Spinel", 12, 1042081 );
			AddCraft( typeof( SpinelPlateGorget ), "Silver & Spinel", "Spinel Gorget", 90.0, 125.0, typeof( SpinelIngot ), "Block of Spinel", 10, 1042081 );
			AddCraft( typeof( SpinelPlateLegs ), "Silver & Spinel", "Spinel Leggings", 90.0, 125.0, typeof( SpinelIngot ), "Block of Spinel", 20, 1042081 );
			AddCraft( typeof( SpinelPlateChest ), "Silver & Spinel", "Spinel Tunic", 90.0, 125.0, typeof( SpinelIngot ), "Block of Spinel", 25, 1042081 );
			AddCraft( typeof( SpinelFemalePlateChest ), "Silver & Spinel", "Spinel Female Tunic", 90.0, 125.0, typeof( SpinelIngot ), "Block of Spinel", 20, 1042081 );
			AddCraft( typeof( SpinelPlateHelm ), "Silver & Spinel", "Spinel Helm", 90.0, 125.0, typeof( SpinelIngot ), "Block of Spinel", 15, 1042081 );
			AddCraft( typeof( SpinelShield ), "Silver & Spinel", "Spinel Shield", 90.0, 125.0, typeof( SpinelIngot ), "Block of Spinel", 18, 1042081 );
			index = AddCraft( typeof( OilSpinel ), "Silver & Spinel", "Spinel Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( SpinelIngot ), "Block of Spinel", 30, 1042081 );

			AddCraft( typeof( StarRubyPlateArms ), "Star Ruby & Topaz", "Star Ruby Arms", 90.0, 125.0, typeof( StarRubyIngot ), "Block of Star Ruby", 18, 1042081 );
			AddCraft( typeof( StarRubyPlateGloves ), "Star Ruby & Topaz", "Star Ruby Gauntlets", 90.0, 125.0, typeof( StarRubyIngot ), "Block of Star Ruby", 12, 1042081 );
			AddCraft( typeof( StarRubyPlateGorget ), "Star Ruby & Topaz", "Star Ruby Gorget", 90.0, 125.0, typeof( StarRubyIngot ), "Block of Star Ruby", 10, 1042081 );
			AddCraft( typeof( StarRubyPlateLegs ), "Star Ruby & Topaz", "Star Ruby Leggings", 90.0, 125.0, typeof( StarRubyIngot ), "Block of Star Ruby", 20, 1042081 );
			AddCraft( typeof( StarRubyPlateChest ), "Star Ruby & Topaz", "Star Ruby Tunic", 90.0, 125.0, typeof( StarRubyIngot ), "Block of Star Ruby", 25, 1042081 );
			AddCraft( typeof( StarRubyFemalePlateChest ), "Star Ruby & Topaz", "Star Ruby Female Tunic", 90.0, 125.0, typeof( StarRubyIngot ), "Block of Star Ruby", 20, 1042081 );
			AddCraft( typeof( StarRubyPlateHelm ), "Star Ruby & Topaz", "Star Ruby Helm", 90.0, 125.0, typeof( StarRubyIngot ), "Block of Star Ruby", 15, 1042081 );
			AddCraft( typeof( StarRubyShield ), "Star Ruby & Topaz", "Star Ruby Shield", 90.0, 125.0, typeof( StarRubyIngot ), "Block of Star Ruby", 18, 1042081 );
			index = AddCraft( typeof( OilStarRuby ), "Star Ruby & Topaz", "Star Ruby Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( StarRubyIngot ), "Block of Star Ruby", 30, 1042081 );

			AddCraft( typeof( TopazPlateArms ), "Star Ruby & Topaz", "Topaz Arms", 90.0, 125.0, typeof( TopazIngot ), "Block of Topaz", 18, 1042081 );
			AddCraft( typeof( TopazPlateGloves ), "Star Ruby & Topaz", "Topaz Gauntlets", 90.0, 125.0, typeof( TopazIngot ), "Block of Topaz", 12, 1042081 );
			AddCraft( typeof( TopazPlateGorget ), "Star Ruby & Topaz", "Topaz Gorget", 90.0, 125.0, typeof( TopazIngot ), "Block of Topaz", 10, 1042081 );
			AddCraft( typeof( TopazPlateLegs ), "Star Ruby & Topaz", "Topaz Leggings", 90.0, 125.0, typeof( TopazIngot ), "Block of Topaz", 20, 1042081 );
			AddCraft( typeof( TopazPlateChest ), "Star Ruby & Topaz", "Topaz Tunic", 90.0, 125.0, typeof( TopazIngot ), "Block of Topaz", 25, 1042081 );
			AddCraft( typeof( TopazFemalePlateChest ), "Star Ruby & Topaz", "Topaz Female Tunic", 90.0, 125.0, typeof( TopazIngot ), "Block of Topaz", 20, 1042081 );
			AddCraft( typeof( TopazPlateHelm ), "Star Ruby & Topaz", "Topaz Helm", 90.0, 125.0, typeof( TopazIngot ), "Block of Topaz", 15, 1042081 );
			AddCraft( typeof( TopazShield ), "Star Ruby & Topaz", "Topaz Shield", 90.0, 125.0, typeof( TopazIngot ), "Block of Topaz", 18, 1042081 );
			index = AddCraft( typeof( OilTopaz ), "Star Ruby & Topaz", "Topaz Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( TopazIngot ), "Block of Topaz", 30, 1042081 );

			AddCraft( typeof( CaddellitePlateArms ), "Caddellite", "Caddellite Arms", 90.0, 125.0, typeof( CaddelliteIngot ), "Block of Caddellite", 18, 1042081 );
			AddCraft( typeof( CaddellitePlateGloves ), "Caddellite", "Caddellite Gauntlets", 90.0, 125.0, typeof( CaddelliteIngot ), "Block of Caddellite", 12, 1042081 );
			AddCraft( typeof( CaddellitePlateGorget ), "Caddellite", "Caddellite Gorget", 90.0, 125.0, typeof( CaddelliteIngot ), "Block of Caddellite", 10, 1042081 );
			AddCraft( typeof( CaddellitePlateLegs ), "Caddellite", "Caddellite Leggings", 90.0, 125.0, typeof( CaddelliteIngot ), "Block of Caddellite", 20, 1042081 );
			AddCraft( typeof( CaddellitePlateChest ), "Caddellite", "Caddellite Tunic", 90.0, 125.0, typeof( CaddelliteIngot ), "Block of Caddellite", 25, 1042081 );
			AddCraft( typeof( CaddelliteFemalePlateChest ), "Caddellite", "Caddellite Female Tunic", 90.0, 125.0, typeof( CaddelliteIngot ), "Block of Caddellite", 20, 1042081 );
			AddCraft( typeof( CaddellitePlateHelm ), "Caddellite", "Caddellite Helm", 90.0, 125.0, typeof( CaddelliteIngot ), "Block of Caddellite", 15, 1042081 );
			AddCraft( typeof( CaddelliteShield ), "Caddellite", "Caddellite Shield", 90.0, 125.0, typeof( CaddelliteIngot ), "Block of Caddellite", 18, 1042081 );
			index = AddCraft( typeof( OilCaddellite ), "Caddellite", "Caddellite Oil", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( CaddelliteIngot ), "Block of Caddellite", 30, 1042081 );

			#endregion
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DefGodSewing : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
 
        public override string GumpTitleString
        {
            get { return "<BASEFONT Color=#FBFBFB><CENTER>MAGICAL SEWING MENU</CENTER></BASEFONT>"; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGodSewing();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefGodSewing() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			if ( from.Map == Map.TerMur && from.X > 1087 && from.X < 1105 && from.Y > 1968 && from.Y < 1982 )
				return 0;

			return 501816;
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
				return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Armor

			AddCraft( typeof( SkinDemonArms ), "Demon Skin", "Demon Skin Arms", 90.0, 125.0, typeof( DemonSkin ), "Demon Skin", 4, 1042081 );
			AddCraft( typeof( SkinDemonHelm ), "Demon Skin", "Demon Skin Cap", 90.0, 125.0, typeof( DemonSkin ), "Demon Skin", 2, 1042081 );
			AddCraft( typeof( SkinDemonGloves ), "Demon Skin", "Demon Skin Gloves", 90.0, 125.0, typeof( DemonSkin ), "Demon Skin", 3, 1042081 );
			AddCraft( typeof( SkinDemonGorget ), "Demon Skin", "Demon Skin Gorget", 90.0, 125.0, typeof( DemonSkin ), "Demon Skin", 4, 1042081 );
			AddCraft( typeof( SkinDemonLegs ), "Demon Skin", "Demon Skin Leggings", 90.0, 125.0, typeof( DemonSkin ), "Demon Skin", 10, 1042081 );
			AddCraft( typeof( SkinDemonChest ), "Demon Skin", "Demon Skin Tunic", 90.0, 125.0, typeof( DemonSkin ), "Demon Skin", 12, 1042081 );

			AddCraft( typeof( SkinDragonArms ), "Dragon Skin", "Dragon Skin Arms", 90.0, 125.0, typeof( DragonSkin ), "Dragon Skin", 4, 1042081 );
			AddCraft( typeof( SkinDragonHelm ), "Dragon Skin", "Dragon Skin Cap", 90.0, 125.0, typeof( DragonSkin ), "Dragon Skin", 2, 1042081 );
			AddCraft( typeof( SkinDragonGloves ), "Dragon Skin", "Dragon Skin Gloves", 90.0, 125.0, typeof( DragonSkin ), "Dragon Skin", 3, 1042081 );
			AddCraft( typeof( SkinDragonGorget ), "Dragon Skin", "Dragon Skin Gorget", 90.0, 125.0, typeof( DragonSkin ), "Dragon Skin", 4, 1042081 );
			AddCraft( typeof( SkinDragonLegs ), "Dragon Skin", "Dragon Skin Leggings", 90.0, 125.0, typeof( DragonSkin ), "Dragon Skin", 10, 1042081 );
			AddCraft( typeof( SkinDragonChest ), "Dragon Skin", "Dragon Skin Tunic", 90.0, 125.0, typeof( DragonSkin ), "Dragon Skin", 12, 1042081 );

			AddCraft( typeof( SkinNightmareArms ), "Nightmare Skin", "Nightmare Skin Arms", 90.0, 125.0, typeof( NightmareSkin ), "Nightmare Skin", 4, 1042081 );
			AddCraft( typeof( SkinNightmareHelm ), "Nightmare Skin", "Nightmare Skin Cap", 90.0, 125.0, typeof( NightmareSkin ), "Nightmare Skin", 2, 1042081 );
			AddCraft( typeof( SkinNightmareGloves ), "Nightmare Skin", "Nightmare Skin Gloves", 90.0, 125.0, typeof( NightmareSkin ), "Nightmare Skin", 3, 1042081 );
			AddCraft( typeof( SkinNightmareGorget ), "Nightmare Skin", "Nightmare Skin Gorget", 90.0, 125.0, typeof( NightmareSkin ), "Nightmare Skin", 4, 1042081 );
			AddCraft( typeof( SkinNightmareLegs ), "Nightmare Skin", "Nightmare Skin Leggings", 90.0, 125.0, typeof( NightmareSkin ), "Nightmare Skin", 10, 1042081 );
			AddCraft( typeof( SkinNightmareChest ), "Nightmare Skin", "Nightmare Skin Tunic", 90.0, 125.0, typeof( NightmareSkin ), "Nightmare Skin", 12, 1042081 );

			AddCraft( typeof( SkinSerpentArms ), "Serpent Skin", "Serpent Skin Arms", 90.0, 125.0, typeof( SerpentSkin ), "Serpent Skin", 4, 1042081 );
			AddCraft( typeof( SkinSerpentHelm ), "Serpent Skin", "Serpent Skin Cap", 90.0, 125.0, typeof( SerpentSkin ), "Serpent Skin", 2, 1042081 );
			AddCraft( typeof( SkinSerpentGloves ), "Serpent Skin", "Serpent Skin Gloves", 90.0, 125.0, typeof( SerpentSkin ), "Serpent Skin", 3, 1042081 );
			AddCraft( typeof( SkinSerpentGorget ), "Serpent Skin", "Serpent Skin Gorget", 90.0, 125.0, typeof( SerpentSkin ), "Serpent Skin", 4, 1042081 );
			AddCraft( typeof( SkinSerpentLegs ), "Serpent Skin", "Serpent Skin Leggings", 90.0, 125.0, typeof( SerpentSkin ), "Serpent Skin", 10, 1042081 );
			AddCraft( typeof( SkinSerpentChest ), "Serpent Skin", "Serpent Skin Tunic", 90.0, 125.0, typeof( SerpentSkin ), "Serpent Skin", 12, 1042081 );

			AddCraft( typeof( SkinTrollArms ), "Troll Skin", "Troll Skin Arms", 90.0, 125.0, typeof( TrollSkin ), "Troll Skin", 4, 1042081 );
			AddCraft( typeof( SkinTrollHelm ), "Troll Skin", "Troll Skin Cap", 90.0, 125.0, typeof( TrollSkin ), "Troll Skin", 2, 1042081 );
			AddCraft( typeof( SkinTrollGloves ), "Troll Skin", "Troll Skin Gloves", 90.0, 125.0, typeof( TrollSkin ), "Troll Skin", 3, 1042081 );
			AddCraft( typeof( SkinTrollGorget ), "Troll Skin", "Troll Skin Gorget", 90.0, 125.0, typeof( TrollSkin ), "Troll Skin", 4, 1042081 );
			AddCraft( typeof( SkinTrollLegs ), "Troll Skin", "Troll Skin Leggings", 90.0, 125.0, typeof( TrollSkin ), "Troll Skin", 10, 1042081 );
			AddCraft( typeof( SkinTrollChest ), "Troll Skin", "Troll Skin Tunic", 90.0, 125.0, typeof( TrollSkin ), "Troll Skin", 12, 1042081 );

			AddCraft( typeof( SkinUnicornArms ), "Unicorn Skin", "Unicorn Skin Arms", 90.0, 125.0, typeof( UnicornSkin ), "Unicorn Skin", 4, 1042081 );
			AddCraft( typeof( SkinUnicornHelm ), "Unicorn Skin", "Unicorn Skin Cap", 90.0, 125.0, typeof( UnicornSkin ), "Unicorn Skin", 2, 1042081 );
			AddCraft( typeof( SkinUnicornGloves ), "Unicorn Skin", "Unicorn Skin Gloves", 90.0, 125.0, typeof( UnicornSkin ), "Unicorn Skin", 3, 1042081 );
			AddCraft( typeof( SkinUnicornGorget ), "Unicorn Skin", "Unicorn Skin Gorget", 90.0, 125.0, typeof( UnicornSkin ), "Unicorn Skin", 4, 1042081 );
			AddCraft( typeof( SkinUnicornLegs ), "Unicorn Skin", "Unicorn Skin Leggings", 90.0, 125.0, typeof( UnicornSkin ), "Unicorn Skin", 10, 1042081 );
			AddCraft( typeof( SkinUnicornChest ), "Unicorn Skin", "Unicorn Skin Tunic", 90.0, 125.0, typeof( UnicornSkin ), "Unicorn Skin", 12, 1042081 );

			#endregion
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DefGodBrewing : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy; }
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
 
        public override string GumpTitleString
        {
            get { return "<BASEFONT Color=#FBFBFB><CENTER>MAGICAL ALCHEMY MENU</CENTER></BASEFONT>"; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGodBrewing();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefGodBrewing() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			if ( from.Map == Map.TerMur && from.X > 1098 && from.X < 1121 && from.Y > 1908 && from.Y < 1931 )
				return 0;

			return 501816;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x242 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			Server.Gumps.MReagentGump.XReagentGump( from );

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
				return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Potions

			index = AddCraft( typeof( LesserInvisibilityPotion ), "Invisibility", "Invisibility Potion, Lesser", 70.0, 105.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( SilverSerpentVenom ), "Silver Serpent Venom", 1, 1042081 );
			AddRes( index, typeof( DragonBlood ), "Dragon Blood", 2, 1042081 );

			index = AddCraft( typeof( InvisibilityPotion ), "Invisibility", "Invisibility Potion", 80.0, 115.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( SilverSerpentVenom ), "Silver Serpent Venom", 2, 1042081 );
			AddRes( index, typeof( DragonBlood ), "Dragon Blood", 4, 1042081 );

			index = AddCraft( typeof( GreaterInvisibilityPotion ), "Invisibility", "Invisibility Potion, Greater", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( SilverSerpentVenom ), "Silver Serpent Venom", 3, 1042081 );
			AddRes( index, typeof( DragonBlood ), "Dragon Blood", 6, 1042081 );

			index = AddCraft( typeof( InvulnerabilityPotion ), "Invulnerability", "Invulnerability Potion", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( EnchantedSeaweed ), "Enchanted Seaweed", 3, 1042081 );
			AddRes( index, typeof( DragonTooth ), "Dragon Tooth", 2, 1042081 );

			index = AddCraft( typeof( LesserManaPotion ), "Mana", "Mana Potion, Lesser", 70.0, 105.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( GoldenSerpentVenom ), "Golden Serpent Venom", 1, 1042081 );
			AddRes( index, typeof( LichDust ), "Lich Dust", 2, 1042081 );

			index = AddCraft( typeof( ManaPotion ), "Mana", "Mana Potion", 80.0, 115.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( GoldenSerpentVenom ), "Golden Serpent Venom", 2, 1042081 );
			AddRes( index, typeof( LichDust ), "Lich Dust", 4, 1042081 );

			index = AddCraft( typeof( GreaterManaPotion ), "Mana", "Mana Potion, Greater", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( GoldenSerpentVenom ), "Golden Serpent Venom", 3, 1042081 );
			AddRes( index, typeof( LichDust ), "Lich Dust", 6, 1042081 );

			index = AddCraft( typeof( LesserRejuvenatePotion ), "Rejuvenate", "Rejuvenation Potion, Lesser", 70.0, 105.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( DemonClaw ), "Demon Claw", 1, 1042081 );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 1, 1042081 );

			index = AddCraft( typeof( RejuvenatePotion ), "Rejuvenate", "Rejuvenation Potion", 80.0, 115.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( DemonClaw ), "Demon Claw", 2, 1042081 );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 2, 1042081 );

			index = AddCraft( typeof( GreaterRejuvenatePotion ), "Rejuvenate", "Rejuvenation Potion, Greater", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( DemonClaw ), "Demon Claw", 3, 1042081 );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 3, 1042081 );

			index = AddCraft( typeof( SuperPotion ), "Rejuvenate", "Superior Potion", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( DemonClaw ), "Demon Claw", 3, 1042081 );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 3, 1042081 );

			index = AddCraft( typeof( AutoResPotion ), "Resurrection", "Resurrect Self Potion", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( DemigodBlood ), "Demigod Blood", 3, 1042081 );
			AddRes( index, typeof( GhostlyDust ), "Ghostly Dust", 2, 1042081 );

			index = AddCraft( typeof( ResurrectPotion ), "Resurrection", "Resurrect Others Potion", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( DemigodBlood ), "Demigod Blood", 3, 1042081 );
			AddRes( index, typeof( GhostlyDust ), "Ghostly Dust", 2, 1042081 );

			index = AddCraft( typeof( RepairPotion ), "Repair", "Repairing Potion", 90.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 3, 1042081 );
			AddRes( index, typeof( SilverSerpentVenom ), "Silver Serpent Venom", 2, 1042081 );

			/*index = AddCraft( typeof( DurabilityPotion ), "Repair", "Durability Potion", 110.0, 125.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof( GoldenSerpentVenom ), "Golden Serpent Venom", 3, 1042081 );
			AddRes( index, typeof( DragonBlood ), "Dragon Blood", 2, 1042081 );*/

			#endregion
		}
	}
}