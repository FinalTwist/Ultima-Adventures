using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;
using Server.Regions;

namespace Server.Mobiles
{
	[CorpseName( "a balron corpse" )]
	public class Balron : AuraCreature
	{
		public override double DispelDifficulty{ get{ return 150.0; } }
		public override double DispelFocus{ get{ return 25.0; } }

		[Constructable]
		public Balron () : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "demonic" );
			Title = "balron"; if ( Utility.RandomBool() ){ Title = "balrog"; }
			Body = 427;
			BaseSoundID = 357;

			if ( Utility.RandomMinMax( 1, 10 ) == 1 )
			{
				Body = 191;
				Title = "balor";
			}
			MinAuraDelay = 5;
			MaxAuraDelay = 10;
			MinAuraDamage = 25;
			MaxAuraDamage = 45;
			AuraRange = 3;
			AuraMessage = "You are shaken by the creature!";
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 22; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }
		public override bool CanRummageCorpses{ get{ return true; } }

		public Balron( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( rBody );
			writer.Write( rHue );
			writer.Write( rName );
			writer.Write( rCategory );
			writer.Write( rMainColor );
			writer.Write( rFood );
			writer.Write( rDrop );
			writer.Write( rDwell );
			writer.Write( rPoison );
			writer.Write( rBlood );
			writer.Write( rBreath );
			writer.Write( rBreathPhysDmg );
			writer.Write( rBreathFireDmg );
			writer.Write( rBreathColdDmg );
			writer.Write( rBreathPoisDmg );
			writer.Write( rBreathEngyDmg );
			writer.Write( rBreathHue );
			writer.Write( rBreathSound );
			writer.Write( rBreathItemID );
			writer.Write( rBreathDelay );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			rBody = reader.ReadInt();
			rHue = reader.ReadInt();
			rName = reader.ReadString();
			rCategory = reader.ReadString();
			rMainColor = reader.ReadString();
			rFood = reader.ReadString();
			rDrop = reader.ReadString();
			rDwell = reader.ReadString();
			rPoison = reader.ReadInt();
			rBlood = reader.ReadString();
			rBreath = reader.ReadInt();
			rBreathPhysDmg = reader.ReadInt();
			rBreathFireDmg = reader.ReadInt();
			rBreathColdDmg = reader.ReadInt();
			rBreathPoisDmg = reader.ReadInt();
			rBreathEngyDmg = reader.ReadInt();
			rBreathHue = reader.ReadInt();
			rBreathSound = reader.ReadInt();
			rBreathItemID = reader.ReadInt();
			rBreathDelay = reader.ReadDouble();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 && Body == 191 )
					{
						BaseWeapon sword = new Longsword();
						sword.AccuracyLevel = WeaponAccuracyLevel.Supremely;
						sword.MinDamage = sword.MinDamage + 7;
						sword.MaxDamage = sword.MaxDamage + 12;
            			sword.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						sword.AosElementDamages.Fire = 50;
						sword.Name = "sword of " + this.Title;
						sword.Slayer = SlayerName.Repond;
						if ( Utility.RandomMinMax( 0, 100 ) > 50 ){ sword.WeaponAttributes.HitFireball = 25; }
						sword.Hue = this.Hue;
						c.DropItem( sword );
					}

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.IntelligentAction.FameBasedEvent( this ) )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						Server.Misc.ContainerFunctions.MakeDemonBox( MyChest, this );
						c.DropItem( MyChest );
					}
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			Server.Mobiles.Balron.ExplodeDeath( this, rBreath );
			return base.OnBeforeDeath();
		}

		public override void OnAfterSpawn()
		{
			Region reg = Region.Find( this.Location, this.Map );
			string category = "land";

			if ( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) ) && Utility.RandomMinMax( 1, 10 ) == 1 )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0: category = "dungeon";	break;
					case 1: category = "fire";		break;
					case 2: category = "land";		break;
					case 3: category = "dungeon";	break;
					case 4: category = "fire";		break;
				}
			}
			else //+++
			{
				if ( reg.IsPartOf( "Argentrock Castle" ) ){											category = "sky"; }
				else if ( reg.IsPartOf( "the Blood Temple" ) ){ 									category = "blood"; }
				else if ( reg.IsPartOf( "the Sanctum of Saltmarsh" ) ){								category = "swamp"; }
				else if ( reg.IsPartOf( "the Ancient Crash Site" ) || reg.IsPartOf( "the Ancient Sky Ship" ) ){ category = "radiation"; }
				else if ( Server.Misc.Worlds.IsFireDungeon( this.Location, this.Map ) ){			category = "fire"; }
				else if ( Server.Misc.Worlds.IsIceDungeon( this.Location, this.Map ) ){				category = "snow"; }
				else if ( Server.Misc.Worlds.IsSeaDungeon( this.Location, this.Map ) ){				category = "sea"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "dirt" ) 
						&& Server.Misc.Worlds.TestMountain ( this.Map, this.X, this.Y, 15 ) ){		category = "mountain"; }
				else if ( Server.Misc.Worlds.TestMountain ( this.Map, this.X, this.Y, 10 ) ){ 		category = "mountain"; }
				else if ( Server.Misc.Worlds.TestOcean ( this.Map, this.X, this.Y, 15 ) ){ 			category = "sea"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "snow" ) ){		category = "snow"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "cave" ) ){		category = "dungeon"; if ( Utility.RandomBool() ){ category = "mountain"; } }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "swamp" ) ){		category = "swamp"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "jungle" ) ){		category = "jungle"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "forest" ) ){		category = "forest"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "sand" ) ){		category = "sand"; }
				else if ( Server.Misc.Worlds.TestMountain ( this.Map, this.X, this.Y, 15 ) ){ 		category = "mountain"; }
				else if ( reg.IsPartOf( typeof( DungeonRegion ) ) ){								category = "dungeon"; }

				if ( reg.IsPartOf( "the Ancient Sky Ship" ) ){ 										category = "solar"; }
			}

			CreateBalron( category );

			if ( ( (this.Region).IsDefault || (this.Region).Name == null || (this.Region).Name == "" ) ){ this.WhisperHue = 666; this.Home = this.Location; }

			base.OnAfterSpawn();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 4 ) == 1 && !Controlled && rBlood != "" && rBlood != null && rBlood != "rust" )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter ){ goo++; } }

				if ( goo == 0 )
				{
					if ( rBlood == "glowing goo" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "glowing goo", 0xB93, 1 ); }
					else if ( rBlood == "scorching ooze" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "scorching ooze", 0x496, 0 ); }
					else if ( rBlood == "poisonous slime" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "poisonous slime", 1167, 0 ); }
					else if ( rBlood == "toxic blood" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "toxic blood", 0x48E, 1 ); }
					else if ( rBlood == "toxic goo" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "toxic goo", 0xB93, 1 ); }
					else if ( rBlood == "hot magma" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "hot magma", 0x489, 1 ); }
					else if ( rBlood == "acidic slime" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "acidic slime", 1167, 0 ); }
					else if ( rBlood == "freezing water" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "freezing water", 296, 0 ); }
					else if ( rBlood == "scorching ooze" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "scorching ooze", 0x496, 0 ); }
					else if ( rBlood == "blue slime" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "blue slime", 0x5B6, 1 ); }
					else if ( rBlood == "green blood" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "green blood", 0x7D1, 0 ); }
					else if ( rBlood == "quick silver" ){ MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "quick silver", 0xB37, 1 ); }
					else { MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "thick blood", 0x485, 0 ); }
				}
			}
		}

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) && rBlood == "rust" && m is PlayerMobile )
			{
				Container cont = m.Backpack;
				Item iRuined = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iRuined != null )
				{
					if ( Server.Items.HiddenTrap.IAmShielding( m, 100 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( m.CheckSkill( SkillName.MagicResist, 0, 100 ) )
					{
					}
					else if ( iRuined is BaseWeapon )
					{
						BaseWeapon iRusted = (BaseWeapon)iRuined;

						if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iRuined ) )
						{
							if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iRuined, m ) == true )
							{
								m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The balron almost rusted one of your protected items!");
							}
							else
							{
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "The balron rusted one of your equipped items!");
								RustyJunk broke = new RustyJunk();
								broke.ItemID = iRuined.ItemID;
								broke.Name = "rusted item";
								broke.Weight = iRuined.Weight;
								m.AddToBackpack ( broke );
								iRuined.Delete();
							}
						}
					}
					if ( iRuined is BaseArmor )
					{
						BaseArmor iRusted = (BaseArmor)iRuined;

						if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iRuined ) )
						{
							if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iRuined, m ) == true )
							{
								m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The balron almost rusted one of your protected items!");
							}
							else
							{
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "The balron rusted one of your equipped items!");
								RustyJunk broke = new RustyJunk();
								broke.ItemID = iRuined.ItemID;
								broke.Name = "rusted item";
								broke.Weight = Utility.RandomMinMax( 1, 4 );
								m.AddToBackpack ( broke );
								iRuined.Delete();
							}
						}
					}
				}
			}
		}

		public int rBody;
		[CommandProperty(AccessLevel.Owner)]
		public int r_Body { get { return rBody; } set { rBody = value; InvalidateProperties(); } }

		public int rHue;
		[CommandProperty(AccessLevel.Owner)]
		public int r_Hue { get { return rHue; } set { rHue = value; InvalidateProperties(); } }

		public string rName;
		[CommandProperty(AccessLevel.Owner)]
		public string r_Name { get { return rName; } set { rName = value; InvalidateProperties(); } }

		public string rCategory;
		[CommandProperty(AccessLevel.Owner)]
		public string r_Category { get { return rCategory; } set { rCategory = value; InvalidateProperties(); } }

		public string rMainColor;
		[CommandProperty(AccessLevel.Owner)]
		public string r_MainColor { get { return rMainColor; } set { rMainColor = value; InvalidateProperties(); } }

		public string rFood;
		[CommandProperty(AccessLevel.Owner)]
		public string r_Food { get { return rFood; } set { rFood = value; InvalidateProperties(); } }

		public string rDrop;
		[CommandProperty(AccessLevel.Owner)]
		public string r_Drop { get { return rDrop; } set { rDrop = value; InvalidateProperties(); } }

		public string rDwell;
		[CommandProperty(AccessLevel.Owner)]
		public string r_Dwell { get { return rDwell; } set { rDwell = value; InvalidateProperties(); } }

		public int rPoison;
		[CommandProperty(AccessLevel.Owner)]
		public int r_Poison { get { return rPoison; } set { rPoison = value; InvalidateProperties(); } }

		public string rBlood;
		[CommandProperty(AccessLevel.Owner)]
		public string r_Blood { get { return rBlood; } set { rBlood = value; InvalidateProperties(); } }

		public int rBreath;
		[CommandProperty(AccessLevel.Owner)]
		public int r_Breath { get { return rBreath; } set { rBreath = value; InvalidateProperties(); } }

		public int rBreathPhysDmg;
		[CommandProperty(AccessLevel.Owner)]
		public int r_BreathPhysDmg { get { return rBreathPhysDmg; } set { rBreathPhysDmg = value; InvalidateProperties(); } }

		public int rBreathFireDmg;
		[CommandProperty(AccessLevel.Owner)]
		public int r_BreathFireDmg { get { return rBreathFireDmg; } set { rBreathFireDmg = value; InvalidateProperties(); } }

		public int rBreathColdDmg;
		[CommandProperty(AccessLevel.Owner)]
		public int r_BreathColdDmg { get { return rBreathColdDmg; } set { rBreathColdDmg = value; InvalidateProperties(); } }

		public int rBreathPoisDmg;
		[CommandProperty(AccessLevel.Owner)]
		public int r_BreathPoisDmg { get { return rBreathPoisDmg; } set { rBreathPoisDmg = value; InvalidateProperties(); } }

		public int rBreathEngyDmg;
		[CommandProperty(AccessLevel.Owner)]
		public int r_BreathEngyDmg { get { return rBreathEngyDmg; } set { rBreathEngyDmg = value; InvalidateProperties(); } }

		public int rBreathHue;
		[CommandProperty(AccessLevel.Owner)]
		public int r_BreathHue { get { return rBreathHue; } set { rBreathHue = value; InvalidateProperties(); } }

		public int rBreathSound;
		[CommandProperty(AccessLevel.Owner)]
		public int r_BreathSound { get { return rBreathSound; } set { rBreathSound = value; InvalidateProperties(); } }

		public int rBreathItemID;
		[CommandProperty(AccessLevel.Owner)]
		public int r_BreathItemID { get { return rBreathItemID; } set { rBreathItemID = value; InvalidateProperties(); } }

		public double rBreathDelay;
		[CommandProperty(AccessLevel.Owner)]
		public double r_BreathDelay { get { return rBreathDelay; } set { rBreathDelay = value; InvalidateProperties(); } }

		public override FoodType FavoriteFood
		{
			get
			{
				if ( rFood == "fish" )
					return ( FoodType.Fish );

				else if ( rFood == "gold" )
					return ( FoodType.Gold );

				else if ( rFood == "fire" )
					return ( FoodType.Fire );

				else if ( rFood == "gems" )
					return ( FoodType.Gems );

				else if ( rFood == "nox" )
					return ( FoodType.Nox );

				else if ( rFood == "sea" )
					return ( FoodType.Sea );

				else if ( rFood == "moon" )
					return ( FoodType.Moon );

				else if ( rFood == "fire_meat" )
					return FoodType.Fire | FoodType.Meat; 

				else if ( rFood == "fish_sea" )
					return FoodType.Fish | FoodType.Sea; 

				else if ( rFood == "gems_fire" )
					return FoodType.Gems | FoodType.Fire; 

				else if ( rFood == "gems_gold" )
					return FoodType.Gems | FoodType.Gold; 

				else if ( rFood == "gems_meat" )
					return FoodType.Gems | FoodType.Meat; 

				else if ( rFood == "gems_moon" )
					return FoodType.Gems | FoodType.Moon; 

				else if ( rFood == "meat_nox" )
					return FoodType.Meat | FoodType.Nox; 

				else if ( rFood == "moon_fire" )
					return FoodType.Moon | FoodType.Fire; 

				else if ( rFood == "nox_fire" )
					return FoodType.Nox | FoodType.Fire; 

				return ( FoodType.Meat );
			}
		}

		public override Poison PoisonImmune
		{
			get
			{
				if ( rPoison > 0 )
					return Poison.Lethal;

				if ( rPoison == 0 )
					return Poison.Greater;

				return null;
			}
		}

		public override Poison HitPoison
		{
			get
			{
				if ( rPoison > 0 )
					return Poison.Deadly;

				return null;
			}
		}

		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, rBreath ); }

		public override int BreathPhysicalDamage{ get{ return rBreathPhysDmg; } }
		public override int BreathFireDamage{ get{ return rBreathFireDmg; } }
		public override int BreathColdDamage{ get{ return rBreathColdDmg; } }
		public override int BreathPoisonDamage{ get{ return rBreathPoisDmg; } }
		public override int BreathEnergyDamage{ get{ return rBreathEngyDmg; } }
		public override int BreathEffectHue{ get{ return rBreathHue; } }
		public override int BreathEffectSound{ get{ return rBreathSound; } }
		public override int BreathEffectItemID{ get{ return rBreathItemID; } }

		public void CreateBalron( string terrain )
		{
			if ( rHue < 1 )
			{
				rBody = Body;
				bool bright = false;

				int daemon = Utility.RandomMinMax( 1, 145 ); // 146 IS OMITTED DUE TO XORMITE RARITY
				if ( terrain == "swamp" ){ daemon = Utility.RandomMinMax( 139, 145 ); }
				else if ( terrain == "fire" ){ daemon = Utility.RandomMinMax( 74, 87 ); }
				else if ( terrain == "snow" ){ daemon = Utility.RandomMinMax( 131, 138 ); }
				else if ( terrain == "sea" )
				{
					daemon = Utility.RandomMinMax( 120, 130 ); 
					if ( Utility.RandomMinMax( 1, 20 ) == 1 ){ daemon = 16; }
				}
				else if ( terrain == "radiation" ){ daemon = Utility.RandomList( 5, 6, 7, 54, 97, 104, 106, 146 ); }
				else if ( terrain == "blood" ){ daemon = Utility.RandomList( 1, 8, 61 ); }
				else if ( terrain == "solar" ){ daemon = 104; }
				else if ( terrain == "jungle" ){ daemon = Utility.RandomList( 89, 90, 93, 95, 96 ); }
				else if ( terrain == "forest" ){ daemon = Utility.RandomMinMax( 88, 94 ); }
				else if ( terrain == "sand" ){ daemon = Utility.RandomMinMax( 112, 119 ); }
				else if ( terrain == "mountain" ){ daemon = Utility.RandomList( 109, 110, 111, 116 ); }
				else if ( terrain == "dungeon" ){ daemon = Utility.RandomMinMax( 1, 73 ); }
				else if ( terrain == "land" ){ daemon = Utility.RandomMinMax( 97, 108 ); }
				else if ( terrain == "shadow" ){ daemon = Utility.RandomMinMax( 69, 70 ); }
				else if ( terrain == "sky" ){ daemon = Utility.RandomList( 7, 22, 33, 66, 97, 99, 101, 104, 105, 106, 107 ); }

				string hell = "abyss";
				switch ( Utility.RandomMinMax( 0, 8 ) )
				{
					case 0: hell = "the abyss";		break;
					case 1: hell = "the deep";		break;
					case 2: hell = "the damned";	break;
					case 3: hell = "the veil";		break;
					case 4: hell = "the void";		break;
					case 5: hell = "the depths";	break;
					case 6: hell = "hell";			break;
					case 7: hell = "the rift";		break;
					case 8: hell = "hades";			break;
				}

				switch ( daemon )
				{
					case 1: rHue = 0x8E4; rMainColor = "red"; rName = "the bloodstone " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "thick blood"; break;
					case 2: rHue = 0xB2A; rMainColor = "white"; rName = "the mercury " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "quick silver"; break;
					case 3: rHue = 0x916; rMainColor = "red"; rName = "the scarlet " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "toxic blood"; break;
					case 4: rHue = 0xB51; rMainColor = "green"; rName = "the poison " + Title + " of " + hell; rDwell = "dungeon"; rFood = "nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = "poisonous slime"; break;
					case 5: rHue = 0x82B; rMainColor = "yellow"; rName = "the glare " + Title + " of " + hell; bright = true; rDwell = "dungeon"; rFood = "gold"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = "glowing goo"; break;
					case 6: rHue = 0x8D8; rMainColor = "white"; rName = "the glaze " + Title + " of " + hell; bright = true; rDwell = "dungeon"; rFood = "gold"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = "glowing goo"; break;
					case 7: rHue = 0x921; rMainColor = "white"; rName = "the radiant " + Title + " of " + hell; bright = true; rDwell = "dungeon"; rFood = "moon"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = "toxic goo"; break;
					case 8: rHue = 0x77C; rMainColor = "red"; rName = "the blood " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = "thick blood"; break;
					case 9: rHue = 0x871; rMainColor = "rust"; rName = "the rust " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gold"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = "rust"; break;
					case 10: rHue = 0x996; rMainColor = "blue"; rName = "the sapphire " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = "sapphire"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 11: rHue = 0xB56; rMainColor = "blue"; rName = "the azurite " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 12: rHue = 0x95B; rMainColor = "yellow"; rName = "the brass " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "brass"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 13: rHue = 0x796; rMainColor = "blue"; rName = "the cobolt " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 14: rHue = 0xB65; rMainColor = "white"; rName = "the mithril " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = "mithril"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 15: rHue = 0xB05; rMainColor = "ice"; rName = "the palladium " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 16: rHue = 0xB3B; rMainColor = "white"; rName = "the pearl " + Title + " of " + hell; rDwell = "dungeon"; rFood = "fish_sea"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 17: rHue = 0x99F; rMainColor = "blue"; rName = "the steel " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "steel"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 18: rHue = 0x98B; rMainColor = "ice"; rName = "the titanium " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 19: rHue = 0xB7C; rMainColor = "ice"; rName = "the turquoise " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 20: rHue = 0x6F7; rMainColor = "purple"; rName = "the violet " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 21: rHue = 0x7C3; rMainColor = "purple"; rName = "the amethyst " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = "amethyst"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 22: rHue = 0x7C6; rMainColor = "yellow"; rName = "the bright " + Title + " of " + hell; bright = true; rDwell = "dungeon"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 23: rHue = 0x92B; rMainColor = "yellow"; rName = "the bronze " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "bronze"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 24: rHue = 0x943; rMainColor = "green"; rName = "the cadmium " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 25: rHue = 0x8D0; rMainColor = "blue"; rName = "the cerulean " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 26: rHue = 0x8B6; rMainColor = "purple"; rName = "the darkhorned " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 27: rHue = 0xB7E; rMainColor = "white"; rName = "the diamond " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 28: rHue = 0xB1B; rMainColor = "bright"; rName = "the gilded " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gold"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 29: rHue = 0x829; rMainColor = "gray"; rName = "the grey " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 30: rHue = 0xB94; rMainColor = "green"; rName = "the jade " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "jade"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 31: rHue = 0x77E; rMainColor = "green"; rName = "the jadefire " + Title + " of " + hell; bright = true; rDwell = "dungeon"; rFood = "fire_meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 32: rHue = 0x88B; rMainColor = "black"; rName = "the murky " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 33: rHue = 0x994; rMainColor = "ice"; rName = "the platinum " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems_moon"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 34: rHue = 0x6F5; rMainColor = "purple"; rName = "the darklight " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 35: rHue = 0x869; rMainColor = "yellow"; rName = "the quartz " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = "quartz"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 36: rHue = 0xB02; rMainColor = "red"; rName = "the sanguine " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 37: rHue = 0x93E; rMainColor = "red"; rName = "the ruby " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = "ruby"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 38: rHue = 0x7CA; rMainColor = "red"; rName = "the rubystar " + Title + " of " + hell; bright = true; rDwell = "dungeon"; rFood = "gems_moon"; rDrop = "star ruby"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 39: rHue = 0x94D; rMainColor = "purple"; rName = "the spinel " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = "spinel"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 40: rHue = 0x883; rMainColor = "blue"; rName = "the topaz " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = "topaz"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 41: rHue = 0x95D; rMainColor = "blue"; rName = "the valorite " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "valorite"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 42: rHue = 0x7CB; rMainColor = "purple"; rName = "the velvet " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 43: rHue = 0x95E; rMainColor = "green"; rName = "the verite " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "verite"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 44: rHue = 0xB5A; rMainColor = "blue"; rName = "the zircon " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 45: rHue = 0x957; rMainColor = "red"; rName = "the agapite " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "agapite"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 46: rHue = 0x7C7; rMainColor = "green"; rName = "the akira " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 47: rHue = 0x7CE; rMainColor = "yellow"; rName = "the amber " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 48: rHue = 0x944; rMainColor = "blue"; rName = "the azure " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 49: rHue = 0x8DD; rMainColor = "black"; rName = "the ebony " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 50: rHue = 0x8E3; rMainColor = "purple"; rName = "the evil " + Title + " of " + hell; rDwell = "dungeon"; rFood = "fire_meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 51: rHue = 0x942; rMainColor = "white"; rName = "the iron " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "iron"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 52: rHue = 0x943; rMainColor = "green"; rName = "the garnet " + Title + " of " + hell; rDwell = "dungeon"; rFood = "nox"; rDrop = "garnet"; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 53: rHue = 0x950; rMainColor = "green"; rName = "the emerald " + Title + " of " + hell; rDwell = "dungeon"; rFood = "nox"; rDrop = "emerald"; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 54: rHue = 0x702; rMainColor = "red"; rName = "the redstar " + Title + " of " + hell; bright = true; rDwell = "dungeon"; rFood = "moon_fire"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = ""; break;
					case 55: rHue = 0xB3B; rMainColor = "white"; rName = "the marble " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gems"; rDrop = "marble"; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 56: rHue = 0x708; rMainColor = "red"; rName = "the vermillion " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 57: rHue = 0x77A; rMainColor = "red"; rName = "the ochre " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 58: rHue = 0xB5E; rMainColor = "black"; rName = "the onyx " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "onyx"; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 59: rHue = 0x95B; rMainColor = "yellow"; rName = "the umber " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 60: rHue = 0x6FB; rMainColor = "purple"; rName = "the baneful " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 61: rHue = 0x870; rMainColor = "red"; rName = "the bloodhorned " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 62: rHue = 0xA9F; rMainColor = "purple"; rName = "the corrupt " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 63: rHue = 0xBB0; rMainColor = "black"; rName = "the dark " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 64: rHue = 0x877; rMainColor = "black"; rName = "the dismal " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 65: rHue = 0x87E; rMainColor = "purple"; rName = "the drowhorned " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 66: rHue = 0x705; rMainColor = "yellow"; rName = "the gold " + Title + " of " + hell; rDwell = "dungeon"; rFood = "gold"; rDrop = "gold"; rCategory = "void"; rBreath = 23; rPoison = 0; rBlood = ""; break;
					case 67: rHue = 0x8B8; rMainColor = "black"; rName = "the grim " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 68: rHue = 0x6FD; rMainColor = "purple"; rName = "the malicious " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 69: rHue = 0x86B; rMainColor = "black"; rName = "the shadowhorned " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 70: rHue = 0x95C; rMainColor = "black"; rName = "the shadowy " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = "shadow iron"; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 71: rHue = 0x7CC; rMainColor = "purple"; rName = "the vile " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 72: rHue = 0x6FE; rMainColor = "purple"; rName = "the wicked " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 73: rHue = 0x6F9; rMainColor = "void"; rName = "the umbra " + Title + " of " + hell; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 74: rHue = 0x776; rMainColor = "red"; rName = "the burnt " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 75: rHue = 0x86C; rMainColor = "red"; rName = "the fire " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 76: rHue = 0x701; rMainColor = "red"; rName = "the firelight " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "scorching ooze"; break;
					case 77: rHue = 0xB12; rMainColor = "red"; rName = "the lava " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 78: rHue = 0xB38; rMainColor = "black"; rName = "the lavarock " + Title + " of " + hell; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 79: rHue = 0xB13; rMainColor = "red"; rName = "the magma " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 80: rHue = 0x827; rMainColor = "red"; rName = "the vulcan " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 81: rHue = 0xAB3; rMainColor = "black"; rName = "the charcoal " + Title + " of " + hell; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 82: rHue = 0xAFA; rMainColor = "red"; rName = "the cinder " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 83: rHue = 0x93D; rMainColor = "red"; rName = "the darkfire " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 84: rHue = 0xB54; rMainColor = "red"; rName = "the flare " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 85: rHue = 0x775; rMainColor = "red"; rName = "the hell " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 86: rHue = 0x779; rMainColor = "red"; rName = "the firerock " + Title + " of " + hell; bright = true; rDwell = "fire"; rFood = "fire_meat"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 87: rHue = 0xB09; rMainColor = "white"; rName = "the steam " + Title + " of " + hell; rDwell = "fire"; rFood = "meat"; rDrop = "granite"; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 88: rHue = 0x85D; rMainColor = "green"; rName = "the forest " + Title + " of " + hell; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 89: rHue = 0x6F6; rMainColor = "green"; rName = "the green " + Title + " of " + hell; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 90: rHue = 0xB28; rMainColor = "green"; rName = "the greenhorned " + Title + " of " + hell; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 91: rHue = 0xB00; rMainColor = "sea"; rName = "the evergreen " + Title + " of " + hell; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 92: rHue = 0xACC; rMainColor = "green"; rName = "the grove " + Title + " of " + hell; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 93: rHue = 0x856; rMainColor = "sea"; rName = "the moss " + Title + " of " + hell; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 94: rHue = 0x91E; rMainColor = "green"; rName = "the woodland " + Title + " of " + hell; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 95: rHue = 0x883; rMainColor = "sea"; rName = "the amazon " + Title + " of " + hell; rDwell = "jungle"; rFood = "meat_nox"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "green blood"; break;
					case 96: rHue = 0xB44; rMainColor = "green"; rName = "the jungle " + Title + " of " + hell; rDwell = "jungle"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 97: rHue = 0x706; rMainColor = "yellow"; rName = "the nova " + Title + " of " + hell; bright = true; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = "glowing goo"; break;
					case 98: rHue = 0xAF7; rMainColor = "red"; rName = "the crimson " + Title + " of " + hell; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 99: rHue = 0x86A; rMainColor = "vile"; rName = "the dusk " + Title + " of " + hell; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 100: rHue = 0xB01; rMainColor = "red"; rName = "the red " + Title + " of " + hell; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 101: rHue = 0x6FC; rMainColor = "blue"; rName = "the sky " + Title + " of " + hell; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "wind"; rBreath = 47; rPoison = 0; rBlood = ""; break;
					case 102: rHue = 0x95E; rMainColor = "green"; rName = "the spring " + Title + " of " + hell; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 103: rHue = 0x703; rMainColor = "purple"; rName = "the orchid " + Title + " of " + hell; rDwell = "land"; rFood = "meat_nox"; rDrop = ""; rCategory = "poison"; rBreath = 9; rPoison = 1; rBlood = ""; break;
					case 104: rHue = 0x981; rMainColor = "red"; rName = "the solar " + Title + " of " + hell; bright = true; rDwell = "land"; rFood = "moon_fire"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = ""; break;
					case 105: rHue = 0x6F8; rMainColor = "white"; rName = "the star " + Title + " of " + hell; bright = true; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "star"; rBreath = 45; rPoison = 0; rBlood = ""; break;
					case 106: rHue = 0x869; rMainColor = "yellow"; rName = "the sun " + Title + " of " + hell; bright = true; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = ""; break;
					case 107: rHue = 0x95D; rMainColor = "blue"; rName = "the moon " + Title + " of " + hell; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 108: rHue = 0xB9D; rMainColor = "black"; rName = "the night " + Title + " of " + hell; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 109: rHue = 0xB31; rMainColor = "black"; rName = "the mountain " + Title + " of " + hell; rDwell = "mountain"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 110: rHue = 0x99B; rMainColor = "white"; rName = "the rock " + Title + " of " + hell; rDwell = "mountain"; rFood = "meat"; rDrop = "granite"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 111: rHue = 0xB32; rMainColor = "black"; rName = "the obsidian " + Title + " of " + hell; rDwell = "mountain"; rFood = "gems_fire"; rDrop = "obsidian"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 112: rHue = 0x855; rMainColor = "blue"; rName = "the oasis " + Title + " of " + hell; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 113: rHue = 0x959; rMainColor = "red"; rName = "the copper " + Title + " of " + hell; rDwell = "sand"; rFood = "meat"; rDrop = "copper"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 114: rHue = 0x952; rMainColor = "red"; rName = "the copperish " + Title + " of " + hell; rDwell = "sand"; rFood = "meat"; rDrop = "dull copper"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 115: rHue = 0x797; rMainColor = "yellow"; rName = "the yellow " + Title + " of " + hell; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 116: rHue = 0x957; rMainColor = "yellow"; rName = "the earth " + Title + " of " + hell; rDwell = "sand"; rFood = "gems_meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 117: rHue = 0x713; rMainColor = "yellow"; rName = "the desert " + Title + " of " + hell; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "sand"; rBreath = 8; rPoison = 0; rBlood = ""; break;
					case 118: rHue = 0x8BC; rMainColor = "yellow"; rName = "the dune " + Title + " of " + hell; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "sand"; rBreath = 8; rPoison = 0; rBlood = ""; break;
					case 119: rHue = 0x712; rMainColor = "yellow"; rName = "the sand " + Title + " of " + hell; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "sand"; rBreath = 8; rPoison = 0; rBlood = ""; break;
					case 120: rHue = 0x945; rMainColor = "blue"; rName = "the nepturite " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = "nepturite"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 121: rHue = 0x8D1; rMainColor = "blue"; rName = "the storm " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "storm"; rBreath = 46; rPoison = 0; rBlood = ""; break;
					case 122: rHue = 0x8C2; rMainColor = "blue"; rName = "the tide " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 123: rHue = 0xB07; rMainColor = "sea"; rName = "the seastone " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 124: rHue = 0x707; rMainColor = "blue"; rName = "the aqua " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 125: rHue = 0xB3D; rMainColor = "blue"; rName = "the lagoon " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 126: rHue = 0x7CD; rMainColor = "blue"; rName = "the loch " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 127: rHue = 0xAE9; rMainColor = "green"; rName = "the algae " + Title + " of " + hell; rDwell = "sea"; rFood = "nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = ""; break;
					case 128: rHue = 0x854; rMainColor = "yellow"; rName = "the coastal " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "sand"; rBreath = 8; rPoison = 0; rBlood = ""; break;
					case 129: rHue = 0xB7F; rMainColor = "red"; rName = "the coral " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 130: rHue = 0xAFF; rMainColor = "plant"; rName = "the ivy " + Title + " of " + hell; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 131: rHue = 0x860; rMainColor = "ice"; rName = "the glacial " + Title + " of " + hell; rDwell = "snow"; rFood = "fish"; rDrop = "ice"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = "freezing water"; break;
					case 132: rHue = 0xAF3; rMainColor = "white"; rName = "the ice " + Title + " of " + hell; bright = true; rDwell = "snow"; rFood = "gems"; rDrop = "ice"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = "freezing water"; break;
					case 133: rHue = 0xB7A; rMainColor = "blue"; rName = "the icehorned " + Title + " of " + hell; bright = true; rDwell = "snow"; rFood = "gems"; rDrop = "ice"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = "freezing water"; break;
					case 134: rHue = 0x9C4; rMainColor = "white"; rName = "the silver " + Title + " of " + hell; rDwell = "snow"; rFood = "meat"; rDrop = "silver"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "quick silver"; break;
					case 135: rHue = 0x86D; rMainColor = "ice"; rName = "the blizzard " + Title + " of " + hell; rDwell = "snow"; rFood = "meat"; rDrop = ""; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 136: rHue = 0x87D; rMainColor = "white"; rName = "the frost " + Title + " of " + hell; rDwell = "snow"; rFood = "meat"; rDrop = "ice"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 137: rHue = 0x8BA; rMainColor = "white"; rName = "the snow " + Title + " of " + hell; rDwell = "snow"; rFood = "meat"; rDrop = ""; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 138: rHue = 0x911; rMainColor = "white"; rName = "the white " + Title + " of " + hell; rDwell = "snow"; rFood = "meat"; rDrop = ""; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 139: rHue = 0xAB1; rMainColor = "black"; rName = "the black " + Title + " of " + hell; rDwell = "swamp"; rFood = "meat_nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = "acidic slime"; break;
					case 140: rHue = 0x88D; rMainColor = "green"; rName = "the mire " + Title + " of " + hell; rDwell = "swamp"; rFood = "nox_fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 141: rHue = 0x945; rMainColor = "sea"; rName = "the moor " + Title + " of " + hell; rDwell = "swamp"; rFood = "nox_fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 142: rHue = 0x8B2; rMainColor = "green"; rName = "the bog " + Title + " of " + hell; rDwell = "swamp"; rFood = "nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = ""; break;
					case 143: rHue = 0xB27; rMainColor = "green"; rName = "the boghorned " + Title + " of " + hell; rDwell = "swamp"; rFood = "nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = ""; break;
					case 144: rHue = 0x77D; rMainColor = "green"; rName = "the swampfire " + Title + " of " + hell; bright = true; rDwell = "swamp"; rFood = "meat_nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 145: rHue = 0x8EC; rMainColor = "green"; rName = "the marsh " + Title + " of " + hell; rDwell = "swamp"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 1; rBlood = ""; break;
					case 146: rHue = 0x7C7; rMainColor = "green"; rName = "the xormite " + Title + " of " + hell; bright = true; rDwell = "dungeon"; rFood = "moon"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = ""; break;
				}

				if ( rCategory == "cold" ){ 			SetDamageType( ResistanceType.Physical, 50 );		SetDamageType( ResistanceType.Fire, 0 );		SetDamageType( ResistanceType.Cold, 50 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 0 ); }
				else if ( rCategory == "electrical" ){ 	SetDamageType( ResistanceType.Physical, 50 );		SetDamageType( ResistanceType.Fire, 0 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 50 ); }
				else if ( rCategory == "fire" ){ 		SetDamageType( ResistanceType.Physical, 50 );		SetDamageType( ResistanceType.Fire, 50 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 0 ); }
				else if ( rCategory == "poison" ){ 		SetDamageType( ResistanceType.Physical, 50 );		SetDamageType( ResistanceType.Fire, 0 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 50 );		SetDamageType( ResistanceType.Energy, 0 ); }
				else if ( rCategory == "radiation" ){ 	SetDamageType( ResistanceType.Physical, 20 );		SetDamageType( ResistanceType.Fire, 40 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 40 ); }
				else if ( rCategory == "sand" ){ 		SetDamageType( ResistanceType.Physical, 80 );		SetDamageType( ResistanceType.Fire, 20 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 0 ); }
				else if ( rCategory == "steam" ){ 		SetDamageType( ResistanceType.Physical, 40 );		SetDamageType( ResistanceType.Fire, 60 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 0 ); }
				else if ( rCategory == "void" ){ 		SetDamageType( ResistanceType.Physical, 20 );		SetDamageType( ResistanceType.Fire, 20 );		SetDamageType( ResistanceType.Cold, 20 );		SetDamageType( ResistanceType.Poison, 20 );		SetDamageType( ResistanceType.Energy, 20 ); }
				else if ( rCategory == "weed" ){ 		SetDamageType( ResistanceType.Physical, 80 );		SetDamageType( ResistanceType.Fire, 0 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 20 );		SetDamageType( ResistanceType.Energy, 0 ); }
				else if ( rCategory == "wind" ){ 		SetDamageType( ResistanceType.Physical, 100 );		SetDamageType( ResistanceType.Fire, 0 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 0 ); }
				else if ( rCategory == "storm" ){ 		SetDamageType( ResistanceType.Physical, 50 );		SetDamageType( ResistanceType.Fire, 0 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 50 ); }
				else if ( rCategory == "star" ){ 		SetDamageType( ResistanceType.Physical, 0 );		SetDamageType( ResistanceType.Fire, 50 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 50 ); }
				else { 									SetDamageType( ResistanceType.Physical, 100 );		SetDamageType( ResistanceType.Fire, 0 );		SetDamageType( ResistanceType.Cold, 0 );		SetDamageType( ResistanceType.Poison, 0 );		SetDamageType( ResistanceType.Energy, 0 ); }

				int phys = 0;
				int fire = 0;
				int cold = 0;
				int pois = 0;
				int engy = 0;

				if ( rCategory == "cold" ){ 			phys = 55;	fire = 15;  cold = 70; pois = 30; engy = 30; }
				else if ( rCategory == "electrical" ){ 	phys = 55;	fire = 30; cold = 30; pois = 15;  engy = 70; }
				else if ( rCategory == "fire" ){ 		phys = 55;	fire = 70; cold = 15;  pois = 30; engy = 30; }
				else if ( rCategory == "poison" ){ 		phys = 55;	fire = 30; cold = 30; pois = 70; engy = 15; }
				else if ( rCategory == "radiation" ){ 	phys = 55;	fire = 35; cold = 15;  pois = 35; engy = 65; }
				else if ( rCategory == "sand" ){ 		phys = 45;	fire = 65; cold = 30; pois = 15;  engy = 40; }
				else if ( rCategory == "steam" ){ 		phys = 55;	fire = 65; cold = 15;  pois = 30; engy = 30; }
				else if ( rCategory == "void" ){ 		phys = 45;	fire = 50; cold = 50; pois = 50; engy = 50; }
				else if ( rCategory == "weed" ){ 		phys = 45;	fire = 25; cold = 25; pois = 65; engy = 50; }

				else if ( rCategory == "wind" ){ 		phys = 45;	fire = 30; cold = 35; pois = 30; engy = 65; }
				else if ( rCategory == "storm" ){ 		phys = 45;	fire = 65; cold = 35; pois = 30; engy = 65; }
				else if ( rCategory == "star" ){ 		phys = 45;	fire = 30; cold = 35; pois = 30; engy = 65; }

				else { 									phys = 55;	fire = 40; cold = 40; pois = 40; engy = 40; }

				Body = rBody;
				Title = rName;
				Hue = rHue;
				YellHue = daemon;

				if ( bright ){ AddItem( new LighterSource() ); }

				SetStr( 986, 1500 );
				SetDex( 177, 255 );
				SetInt( 650, 750 );

				SetHits( 750, 1250 );

				SetDamage( 35, 55 );

				SetResistance( ResistanceType.Physical, (phys+10), (phys+20) );
				SetResistance( ResistanceType.Fire, (fire+10), (fire+20) );
				SetResistance( ResistanceType.Cold, (cold+10), (cold+20) );
				SetResistance( ResistanceType.Poison, (pois+10), (pois+20) );
				SetResistance( ResistanceType.Energy, (engy+10), (engy+20) );

				SetSkill( SkillName.Anatomy, 80.1, 90.0 );
				SetSkill( SkillName.EvalInt, 90.1, 110.0 );
				SetSkill( SkillName.Magery, 95.5, 110.0 );
				SetSkill( SkillName.Meditation, 25.1, 80.0 );
				SetSkill( SkillName.MagicResist, 100.5, 150.0 );
				SetSkill( SkillName.Tactics, 90.1, 110.0 );
				SetSkill( SkillName.Wrestling, 90.1, 110.0 );
				if ( rPoison > 0 ){ SetSkill( SkillName.Poisoning, 70.1, 99.0 ); }

				Fame = 24000;
				Karma = -24000;

				VirtualArmor = 90;

				ControlSlots = 4;

				if ( rBreath == 10 || rBreath == 18 ){ 		rBreathPhysDmg = 0;		rBreathFireDmg = 0;		rBreathColdDmg = 0;		rBreathPoisDmg = 100;	rBreathEngyDmg = 0;		rBreathHue = 0x3F;	rBreathSound = 0x658;	rBreathItemID = 0x36D4;	rBreathDelay = 1.3; }
				else if ( rBreath == 11 || rBreath == 39 ){ rBreathPhysDmg = 0;		rBreathFireDmg = 0;		rBreathColdDmg = 0;		rBreathPoisDmg = 50;	rBreathEngyDmg = 50;	rBreathHue = 0x3F;	rBreathSound = 0x227;	rBreathItemID = 0x36D4;	rBreathDelay = 0.1; }
				else if ( rBreath == 24 || rBreath == 27 ){ rBreathPhysDmg = 20;	rBreathFireDmg = 20;	rBreathColdDmg = 20;	rBreathPoisDmg = 20;	rBreathEngyDmg = 20;	rBreathHue = 0x844;	rBreathSound = 0x658;	rBreathItemID = 0x37BC;	rBreathDelay = 0.1; }
				else if ( rBreath == 12 || rBreath == 19 ){ rBreathPhysDmg = 0;		rBreathFireDmg = 0;		rBreathColdDmg = 100;	rBreathPoisDmg = 0;		rBreathEngyDmg = 0;		rBreathHue = 0x481;	rBreathSound = 0x64F;	rBreathItemID = 0x36D4;	rBreathDelay = 1.3; }
				else if ( rBreath == 13 || rBreath == 20 ){ rBreathPhysDmg = 0;		rBreathFireDmg = 0;		rBreathColdDmg = 0;		rBreathPoisDmg = 0;		rBreathEngyDmg = 100;	rBreathHue = 0x9C2;	rBreathSound = 0x665;	rBreathItemID = 0x3818;	rBreathDelay = 1.3; }
				else if ( rBreath == 16 || rBreath == 38 ){ rBreathPhysDmg = 0;		rBreathFireDmg = 100;	rBreathColdDmg = 0;		rBreathPoisDmg = 0;		rBreathEngyDmg = 0;		rBreathHue = 0x9C4;	rBreathSound = 0x108;	rBreathItemID = 0x36D4;	rBreathDelay = 0.1; }
				else if ( rBreath == 25 || rBreath == 28 ){ rBreathPhysDmg = 20;	rBreathFireDmg = 20;	rBreathColdDmg = 20;	rBreathPoisDmg = 20;	rBreathEngyDmg = 20;	rBreathHue = 0x9C1;	rBreathSound = 0x653;	rBreathItemID = 0x37BC;	rBreathDelay = 0.1; }
				else if ( rBreath == 23 || rBreath == 26 ){ rBreathPhysDmg = 20;	rBreathFireDmg = 20;	rBreathColdDmg = 20;	rBreathPoisDmg = 20;	rBreathEngyDmg = 0;		rBreathHue = 0x496;	rBreathSound = 0x658;	rBreathItemID = 0x37BC;	rBreathDelay = 0.1; }
				else if ( rBreath == 34 || rBreath == 35 ){ rBreathPhysDmg = 50;	rBreathFireDmg = 0;		rBreathColdDmg = 0;		rBreathPoisDmg = 50;	rBreathEngyDmg = 0;		rBreathHue = 0;		rBreathSound = 0x56D;	rBreathItemID = Utility.RandomList( 0xCAC, 0xCAD );	rBreathDelay = 0.1; }
				else if ( rBreath == 8 || rBreath == 40 ){ 	rBreathPhysDmg = 50;	rBreathFireDmg = 50;	rBreathColdDmg = 0;		rBreathPoisDmg = 0;		rBreathEngyDmg = 0;		rBreathHue = 0x96D;	rBreathSound = 0x654;	rBreathItemID = 0x36D4;	rBreathDelay = 1.3; }
				else if ( rBreath == 45 || rBreath == 49 ){ rBreathPhysDmg = 0;		rBreathFireDmg = 50;	rBreathColdDmg = 0;		rBreathPoisDmg = 0;		rBreathEngyDmg = 50;	rBreathHue = 0xB72;	rBreathSound = 0x227;	rBreathItemID = 0x1A84;	rBreathDelay = 1.3; }
				else if ( rBreath == 47 || rBreath == 48 ){ rBreathPhysDmg = 100;	rBreathFireDmg = 0;		rBreathColdDmg = 0;		rBreathPoisDmg = 0;		rBreathEngyDmg = 0;		rBreathHue = 0xB24;	rBreathSound = 0x654;	rBreathItemID = 0x2007;	rBreathDelay = 1.3; }
				else if ( rBreath == 46 || rBreath == 50 ){ rBreathPhysDmg = 50;	rBreathFireDmg = 0;		rBreathColdDmg = 0;		rBreathPoisDmg = 0;		rBreathEngyDmg = 50;	rBreathHue = 0x9C2;	rBreathSound = 0x665;	rBreathItemID = 0x3818;	rBreathDelay = 1.3; }
				else { 										rBreathPhysDmg = 0;		rBreathFireDmg = 100;	rBreathColdDmg = 0;		rBreathPoisDmg = 0;		rBreathEngyDmg = 0;		rBreathHue = 0;		rBreathSound = 0x227;	rBreathItemID = 0x36D4;	rBreathDelay = 1.3; }

				InvalidateProperties();
			}
		}

		public static Point3D BlastZone( int spot, int x, int y, int z )
		{
			if ( spot == 1 ){ x+=-2; y+=-2; }
			else if ( spot == 2 ){ x+=-2; y+=-1; }
			else if ( spot == 3 ){ x+=-2; y+=0; }
			else if ( spot == 4 ){ x+=-2; y+=1; }
			else if ( spot == 5 ){ x+=-2; y+=2; }
			else if ( spot == 6 ){ x+=-1; y+=-2; }
			else if ( spot == 7 ){ x+=-1; y+=-1; }
			else if ( spot == 8 ){ x+=-1; y+=0; }
			else if ( spot == 9 ){ x+=-1; y+=1; }
			else if ( spot == 10 ){ x+=-1; y+=2; }
			else if ( spot == 11 ){ x+=0; y+=-2; }
			else if ( spot == 12 ){ x+=0; y+=-1; }
			else if ( spot == 13 ){ x+=0; y+=0; }
			else if ( spot == 14 ){ x+=0; y+=1; }
			else if ( spot == 15 ){ x+=0; y+=2; }
			else if ( spot == 16 ){ x+=1; y+=-2; }
			else if ( spot == 17 ){ x+=1; y+=-1; }
			else if ( spot == 18 ){ x+=1; y+=0; }
			else if ( spot == 19 ){ x+=1; y+=1; }
			else if ( spot == 20 ){ x+=1; y+=2; }
			else if ( spot == 21 ){ x+=2; y+=-2; }
			else if ( spot == 22 ){ x+=2; y+=-1; }
			else if ( spot == 23 ){ x+=2; y+=0; }
			else if ( spot == 24 ){ x+=2; y+=1; }
			else if ( spot == 25 ){ x+=2; y+=2; }
			else if ( spot == 26 ){ x+=-4; y+=-4; }
			else if ( spot == 27 ){ x+=-4; y+=-3; }
			else if ( spot == 28 ){ x+=-4; y+=-2; }
			else if ( spot == 29 ){ x+=-4; y+=-1; }
			else if ( spot == 30 ){ x+=-4; y+=0; }
			else if ( spot == 31 ){ x+=-4; y+=1; }
			else if ( spot == 32 ){ x+=-4; y+=2; }
			else if ( spot == 33 ){ x+=-4; y+=3; }
			else if ( spot == 34 ){ x+=-4; y+=4; }
			else if ( spot == 35 ){ x+=-3; y+=-4; }
			else if ( spot == 36 ){ x+=-3; y+=-3; }
			else if ( spot == 37 ){ x+=-3; y+=-2; }
			else if ( spot == 38 ){ x+=-3; y+=-1; }
			else if ( spot == 39 ){ x+=-3; y+=0; }
			else if ( spot == 40 ){ x+=-3; y+=1; }
			else if ( spot == 41 ){ x+=-3; y+=2; }
			else if ( spot == 42 ){ x+=-3; y+=3; }
			else if ( spot == 43 ){ x+=-3; y+=4; }
			else if ( spot == 44 ){ x+=-2; y+=-4; }
			else if ( spot == 45 ){ x+=-2; y+=-3; }
			else if ( spot == 46 ){ x+=-2; y+=3; }
			else if ( spot == 47 ){ x+=-2; y+=4; }
			else if ( spot == 48 ){ x+=-1; y+=-4; }
			else if ( spot == 49 ){ x+=-1; y+=-3; }
			else if ( spot == 50 ){ x+=-1; y+=3; }
			else if ( spot == 51 ){ x+=-1; y+=4; }
			else if ( spot == 52 ){ x+=0; y+=-4; }
			else if ( spot == 53 ){ x+=0; y+=-3; }
			else if ( spot == 54 ){ x+=0; y+=3; }
			else if ( spot == 55 ){ x+=0; y+=4; }
			else if ( spot == 56 ){ x+=1; y+=-4; }
			else if ( spot == 57 ){ x+=1; y+=-3; }
			else if ( spot == 58 ){ x+=1; y+=3; }
			else if ( spot == 59 ){ x+=1; y+=4; }
			else if ( spot == 60 ){ x+=2; y+=-4; }
			else if ( spot == 61 ){ x+=2; y+=-3; }
			else if ( spot == 62 ){ x+=2; y+=3; }
			else if ( spot == 63 ){ x+=2; y+=4; }
			else if ( spot == 64 ){ x+=3; y+=-4; }
			else if ( spot == 65 ){ x+=3; y+=-3; }
			else if ( spot == 66 ){ x+=3; y+=-2; }
			else if ( spot == 67 ){ x+=3; y+=-1; }
			else if ( spot == 68 ){ x+=3; y+=0; }
			else if ( spot == 69 ){ x+=3; y+=1; }
			else if ( spot == 70 ){ x+=3; y+=2; }
			else if ( spot == 71 ){ x+=3; y+=3; }
			else if ( spot == 72 ){ x+=3; y+=4; }
			else if ( spot == 73 ){ x+=4; y+=-4; }
			else if ( spot == 74 ){ x+=4; y+=-3; }
			else if ( spot == 75 ){ x+=4; y+=-2; }
			else if ( spot == 76 ){ x+=4; y+=-1; }
			else if ( spot == 77 ){ x+=4; y+=0; }
			else if ( spot == 78 ){ x+=4; y+=1; }
			else if ( spot == 79 ){ x+=4; y+=2; }
			else if ( spot == 80 ){ x+=4; y+=3; }
			else if ( spot == 81 ){ x+=4; y+=4; }

			if ( Utility.RandomMinMax( 1, 3 ) > 1 ){ x = 0; y = 0; z = 0; }

			Point3D loc = new Point3D( x, y, z );

			return loc;
		}

		public static void ExplodeDeath( Mobile from, int form )
		{
			int radius = 25;
			if ( from is Balron ){ radius = 81; }
			int count = 0;

			if ( form == 8 ) // LARGE SAND BREATH ----------------------------------------------------------------------------------------------
			{
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x2007, 30, 10, Utility.RandomList( 0xB4D, 0xB4E ), 0 ); }
				from.PlaySound( 0x10B );
			}
			else if ( form == 9 ) // LARGE FIRE BREATH ----------------------------------------------------------------------------------------------
			{
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x3709, 30, 10 ); }
				from.PlaySound( 0x208 );
			}
			else if ( form == 10 ) // LARGE POISON BREATH -------------------------------------------------------------------------------------------
			{
				if ( Utility.RandomMinMax( 1, 2 ) == 1 )
				{
					while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x3400, 60 ); }
					Effects.PlaySound( from.Location, from.Map, 0x108 );
				}
				else
				{
					while ( radius > count ){ count++; Effects.SendLocationParticles( EffectItem.Create( BlastZone( count, from.X, from.Y, from.Z ), from.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 ); }
					Effects.PlaySound( from.Location, from.Map, 0x229 );
				}
			}
			else if ( form == 11 ) // LARGE RADIATION -----------------------------------------------------------------------------------------------
			{
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x3400, 60, 0xB96, 0 ); }
				Effects.PlaySound( from.Location, from.Map, 0x108 );
			}
			else if ( form == 12 ) // LARGE COLD BREATH ---------------------------------------------------------------------------------------------
			{
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x1A84, 30, 10, 0x9C1, 0 ); }
				from.PlaySound( 0x10B );
			}
			else if ( form == 13 ) // LARGE ELECTRICAL BREATH ---------------------------------------------------------------------------------------
			{
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 ); }
				from.PlaySound( 0x5C3 );
			}
			else if ( form == 16 ) // LARGE STEAM BREATH --------------------------------------------------------------------------------------------
			{
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x3400, 60, 10, 0x9C4, 0 ); }
				Effects.PlaySound( from.Location, from.Map, 0x108 );
			}
			else if ( form == 23 || form == 24 || form == 25 ) // LARGE VOID BREATH -----------------------------------------------------------------
			{
				int color = 0x496;
					if ( form == 24 ){ color = 0x844; }
					else if ( form == 25 ){ color = 0x9C1; }
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x3400, 60, color, 0 ); }
				Effects.PlaySound( from.Location, from.Map, 0x108 );
			}
			else if ( form == 34 ) // LARGE WEED BREATH ---------------------------------------------------------------------------------------------
			{
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x3400, 60, 0xB97, 0 ); }
				Effects.PlaySound( from.Location, from.Map, 0x64F );
			}
			else if ( form == 45 ) // STAR CREATURE ATTACK ------------------------------------------------------------------------------------------
			{
				if ( Utility.RandomBool() )
				{
					while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x3709, 30, 10 ); }
					from.PlaySound( 0x208 );
				}
				else
				{
					while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x2A4E, 30, 10 ); }
					from.PlaySound( 0x5C3 );
				}
			}
			else if ( form == 46 ) // LARGE STORM ATTACK --------------------------------------------------------------------------------------------
			{
				while ( radius > count )
				{
					count++;
					Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x2007, 30, 10, 0xB24, 0 );
					Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z+10 ), from.Map, 0x2A4E, 30, 10 );
				}
				from.PlaySound( 0x10B );
				from.PlaySound( 0x5C3 );
			}
			else if ( form == 47 ) // AIR BLOWING BREATH --------------------------------------------------------------------------------------------
			{
				while ( radius > count ){ count++; Effects.SendLocationEffect( BlastZone( count, from.X, from.Y, from.Z ), from.Map, 0x2007, 30, 10, 0xB24, 0 ); }
				from.PlaySound( 0x10B );
			}
		}
	}
}

namespace Server.Items
{
	public class DemonGate : Item 
	{ 
		[Constructable] 
		public DemonGate() : base( 0x3D5E ) 
		{ 
			Name = "demon gate"; 
			Movable = false; 
			Light = LightType.Circle300;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start();
		}
  
		public DemonGate( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize(GenericWriter writer) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
			this.Delete(); // none when the world starts 
		}

		public static void MakeDemonGate( Mobile m )
		{
			foreach ( Item gate in m.GetItemsInRange( 20 ) )
			{
				if ( gate is DemonGate )
				{
					gate.Visible = false;
				}
			}

			string color = "red";

			if ( m is Balron ){ Balron balron = (Balron)m; color = balron.rMainColor; }
			else if ( m is Daemon ){ Daemon daemon = (Daemon)m; color = daemon.rMainColor; }

			Item door = new DemonGate();
			m.PlaySound( 0x653 );
			door.ItemID = Utility.RandomList( 0x3D5E, 0x53FC );
			if ( m is Balron ){ door.ItemID = Utility.RandomList( 0x3EED, 0x53F0 ); }


			if ( color == "green" ){ door.Hue = 0xB96; }
			else if ( color == "sea" ){ door.Hue = 0xB75; }
			else if ( color == "black" ){ door.Hue = 0xB5E; }
			else if ( color == "white" ){ door.Hue = 0xBB4; }
			else if ( color == "ice" ){ door.Hue = 0xAF3; }
			else if ( color == "blue" ){ door.Hue = 0xA14; }
			else if ( color == "plant" ){ door.Hue = 0xB97; }
			else if ( color == "yellow" ){ door.Hue = 0xB54; }
			else if ( color == "purple" ){ door.Hue = 0xAF8; }
			else if ( color == "vile" ){ door.Hue = 0xB01; }
			else if ( color == "void" ){ door.Hue = 0xA6D; }
			else if ( color == "bright" ){ door.Hue = 0xB73; }
			else if ( color == "rust" ){ door.Hue = 0xB61; }
			else { door.Hue = 0x9A2; }


			int z = m.Z;
			if ( door.ItemID == 0x53FC ){ z = z + 5; }
			else if ( door.ItemID == 0x53F0 ){ z = z + 10; }


			door.MoveToWorld (new Point3D(m.X, m.Y, z), m.Map);
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromSeconds( 20.0 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			protected override void OnTick() 
			{ 
					if (( i_item != null ) && ( !i_item.Deleted )) 
						i_item.Delete(); 
			} 
		} 
	}
}