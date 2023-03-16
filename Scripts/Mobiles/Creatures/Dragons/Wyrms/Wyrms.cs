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
	[CorpseName( "a dragon corpse" )]
	public class Wyrms : BaseCreature
	{
		[Constructable]
		public Wyrms () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}

		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 9; } }
		public override bool CanAngerOnTame { get { return true; } }
		public override bool CanChew { get{return true;}}

		public Wyrms( Serial serial ) : base( serial )
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
			writer.Write( rScales );
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
			rScales = reader.ReadString();
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

			Body = Server.Misc.MyServerSettings.WyrmBody();
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
			else
			{
				if ( this.Map == Map.Felucca && this.Home.X == 6385 && this.Home.Y == 364 ){ 		category = "fire"; }		// SPECIAL SPAWN - BLACK NIGHT VAULT
				else if ( reg.IsPartOf( "Argentrock Castle" ) ){									category = "sky"; }
				else if ( reg.IsPartOf( "the Sanctum of Saltmarsh" ) ){								category = "swamp"; }
				else if ( reg.IsPartOf( "Dungeon Destard" ) )
				{
					if ( this.X >= 5319 && this.Y >= 892 && this.X <= 5361 && this.Y<= 922 ){ 		category = "sea"; }
					else if ( this.X >= 5131 && this.Y >= 956 && this.X <= 5152 && this.Y<= 976 ){ 	category = "fire"; }
					else if ( this.X >= 5217 && this.Y >= 901 && this.X <= 5241 && this.Y<= 927 ){ 	category = "sea"; }
					else { category = "dungeon"; }
				}
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
			}

			CreateDragon( category );

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
								m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The dragon almost rusted one of your protected items!");
							}
							else
							{
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "The dragon rusted one of your equipped items!");
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
								m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The dragon almost rusted one of your protected items!");
							}
							else
							{
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "The dragon rusted one of your equipped items!");
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

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Region reg = Region.Find( this.Location, this.Map );

			if ( !this.Controlled && rDrop != null && rDrop != "" )
			{
				int SpecialScales = Utility.RandomMinMax( 10, 50 );

				string metal = rDrop.ToUpper();

				if ( rDrop == "granite" )
				{
					Item granite = new Granite();
					granite.Amount = Utility.RandomMinMax( 6, 18 );
					c.DropItem(granite);
				}
				else
				{
					Item scale = new HardScales( SpecialScales, rDrop + " scales" );
					c.DropItem(scale);
				}

				Mobile killer = this.LastKiller;
				if ( killer != null )
				{
					if ( killer is BaseCreature )
						killer = ((BaseCreature)killer).GetMaster();

					if ( killer is PlayerMobile )
					{
						Server.Mobiles.Dragons.DropSpecial( this, killer, rDrop, metal, this.Name + " " + this.Title, c, 25, 0 );

						if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomBool() && reg.IsPartOf( "the Vault of the Black Knight" ) && rDwell == "fire" && !this.Controlled )
						{
							switch ( Utility.RandomMinMax( 0, 5 ) )
							{
								case 0: c.DropItem( new LavaSkinLegs() ); break;
								case 1: c.DropItem( new LavaSkinGloves() ); break;
								case 2: c.DropItem( new LavaSkinGorget() ); break;
								case 3: c.DropItem( new LavaSkinArms() ); break;
								case 4: c.DropItem( new LavaSkinChest() ); break;
								case 5: c.DropItem( new LavaSkinHelm() ); break;
							}
						}
						if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomBool() && reg.IsPartOf( "the Glacial Scar" ) && !this.Controlled )
						{
							switch ( Utility.RandomMinMax( 0, 5 ) )
							{
								case 0: c.DropItem( new IcySkinLegs() ); break;
								case 1: c.DropItem( new IcySkinGloves() ); break;
								case 2: c.DropItem( new IcySkinGorget() ); break;
								case 3: c.DropItem( new IcySkinArms() ); break;
								case 4: c.DropItem( new IcySkinChest() ); break;
								case 5: c.DropItem( new IcySkinHelm() ); break;
							}
						}

						if ( Utility.RandomMinMax( 1, 100 ) == 1 && !this.Controlled )
						{
							DragonEgg egg = new DragonEgg();
							egg.DragonType = this.YellHue;
							egg.DragonBody = 59;
							egg.Hue = this.Hue;
							egg.Name = "egg of " + this.Title;
							egg.NeedGold = 100000;
							c.DropItem( egg );
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

		public string rScales;
		[CommandProperty(AccessLevel.Owner)]
		public string r_Scales { get { return rScales; } set { rScales = value; InvalidateProperties(); } }

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

		public override ScaleType ScaleType
		{
			get
			{
				if ( rScales == "blue" )
					return ( ScaleType.Blue );

				else if ( rScales == "yellow" )
					return ( ScaleType.Yellow );

				else if ( rScales == "black" )
					return ( ScaleType.Black );

				else if ( rScales == "green" )
					return ( ScaleType.Green );

				else if ( rScales == "white" )
					return ( ScaleType.White );

				else if ( rScales == "purple" )
					return Utility.RandomBool() ? ScaleType.Red : ScaleType.Blue;

				else if ( rScales == "sea" )
					return Utility.RandomBool() ? ScaleType.Green : ScaleType.Blue;

				else if ( rScales == "gray" )
					return Utility.RandomBool() ? ScaleType.White : ScaleType.Black;

				else if ( rScales == "ice" )
					return Utility.RandomBool() ? ScaleType.White : ScaleType.Blue;

				else if ( rScales == "rust" )
					return Utility.RandomBool() ? ScaleType.Red : ScaleType.Yellow;

				else if ( rScales == "plant" )
					return Utility.RandomBool() ? ScaleType.Green : ScaleType.Yellow;

				else if ( rScales == "void" )
					return Utility.RandomBool() ? ScaleType.Black : ScaleType.Blue;

				else if ( rScales == "vile" )
					return Utility.RandomBool() ? ScaleType.Black : ScaleType.Red;

				else if ( rScales == "bright" )
					return Utility.RandomBool() ? ScaleType.White : ScaleType.Yellow;

				return ( ScaleType.Red );
			}
		}

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
				return Poison.Greater;
			}
		}

		public override Poison HitPoison
		{
			get
			{
				return Poison.Greater;
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

		public void CreateDragon( string terrain )
		{
			if ( rHue < 1 )
			{
				bool bright = false;

				rBody = Body = Server.Misc.MyServerSettings.WyrmBody();

				int dragon = Utility.RandomMinMax( 1, 145 ); // 146 IS OMITTED DUE TO XORMITE RARITY
				if ( terrain == "swamp" ){ dragon = Utility.RandomMinMax( 139, 145 ); }
				else if ( terrain == "fire" ){ dragon = Utility.RandomMinMax( 74, 87 ); }
				else if ( terrain == "snow" ){ dragon = Utility.RandomMinMax( 131, 138 ); }
				else if ( terrain == "sea" )
				{
					dragon = Utility.RandomMinMax( 120, 130 ); 
					if ( Utility.RandomMinMax( 1, 20 ) == 1 ){ dragon = 16; }
				}
				else if ( terrain == "radiation" ){ dragon = Utility.RandomList( 5, 6, 7, 54, 97, 104, 106, 146 ); }
				else if ( terrain == "jungle" ){ dragon = Utility.RandomList( 89, 90, 93, 95, 96 ); }
				else if ( terrain == "forest" ){ dragon = Utility.RandomMinMax( 88, 94 ); }
				else if ( terrain == "sand" ){ dragon = Utility.RandomMinMax( 112, 119 ); }
				else if ( terrain == "mountain" ){ dragon = Utility.RandomList( 109, 110, 111, 116 ); }
				else if ( terrain == "dungeon" ){ dragon = Utility.RandomMinMax( 1, 73 ); }
				else if ( terrain == "land" ){ dragon = Utility.RandomMinMax( 97, 108 ); }
				else if ( terrain == "sky" ){ dragon = Utility.RandomList( 7, 22, 33, 66, 97, 99, 101, 104, 105, 106, 107 ); }

				switch ( dragon )
				{
					case 1: rHue = 0x8E4; rScales = "red"; rName = "the bloodstone wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "thick blood"; break;
					case 2: rHue = 0xB2A; rScales = "white"; rName = "the mercury wyrm"; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "quick silver"; break;
					case 3: rHue = 0x916; rScales = "red"; rName = "the scarlet wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "toxic blood"; break;
					case 4: rHue = 0xB51; rScales = "green"; rName = "the poison wyrm"; rDwell = "dungeon"; rFood = "nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = "poisonous slime"; break;
					case 5: rHue = 0x82B; rScales = "yellow"; rName = "the glare wyrm"; bright = true; rDwell = "dungeon"; rFood = "gold"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = "glowing goo"; break;
					case 6: rHue = 0x8D8; rScales = "white"; rName = "the glaze wyrm"; bright = true; rDwell = "dungeon"; rFood = "gold"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = "glowing goo"; break;
					case 7: rHue = 0x921; rScales = "white"; rName = "the radiant wyrm"; bright = true; rDwell = "dungeon"; rFood = "moon"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = "toxic goo"; break;
					case 8: rHue = 0x77C; rScales = "red"; rName = "the blood wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = "thick blood"; break;
					case 9: rHue = 0x871; rScales = "rust"; rName = "the rust wyrm"; rDwell = "dungeon"; rFood = "gold"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = "rust"; break;
					case 10: rHue = 0x996; rScales = "blue"; rName = "the sapphire wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = "sapphire"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 11: rHue = 0xB56; rScales = "blue"; rName = "the azurite wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 12: rHue = 0x95B; rScales = "yellow"; rName = "the brass wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "brass"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 13: rHue = 0x796; rScales = "blue"; rName = "the cobolt wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 14: rHue = 0xB65; rScales = "white"; rName = "the mithril wyrm"; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = "mithril"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 15: rHue = 0xB05; rScales = "ice"; rName = "the palladium wyrm"; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 16: rHue = 0xB3B; rScales = "white"; rName = "the pearl wyrm"; rDwell = "dungeon"; rFood = "fish_sea"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 17: rHue = 0x99F; rScales = "blue"; rName = "the steel wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "steel"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 18: rHue = 0x98B; rScales = "ice"; rName = "the titanium wyrm"; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 19: rHue = 0xB7C; rScales = "ice"; rName = "the turquoise wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 20: rHue = 0x6F7; rScales = "purple"; rName = "the violet wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 21: rHue = 0x7C3; rScales = "purple"; rName = "the amethyst wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = "amethyst"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 22: rHue = 0x7C6; rScales = "yellow"; rName = "the bright wyrm"; bright = true; rDwell = "dungeon"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 23: rHue = 0x92B; rScales = "yellow"; rName = "the bronze wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "bronze"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 24: rHue = 0x943; rScales = "green"; rName = "the cadmium wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 25: rHue = 0x8D0; rScales = "blue"; rName = "the cerulean wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 26: rHue = 0x8B6; rScales = "purple"; rName = "the darkscale wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 27: rHue = 0xB7E; rScales = "white"; rName = "the diamond wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 28: rHue = 0xB1B; rScales = "bright"; rName = "the gilded wyrm"; rDwell = "dungeon"; rFood = "gold"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 29: rHue = 0x829; rScales = "gray"; rName = "the grey wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 30: rHue = 0xB94; rScales = "green"; rName = "the jade wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "jade"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 31: rHue = 0x77E; rScales = "green"; rName = "the jadefire wyrm"; bright = true; rDwell = "dungeon"; rFood = "fire_meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 32: rHue = 0x88B; rScales = "black"; rName = "the murky wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 33: rHue = 0x994; rScales = "ice"; rName = "the platinum wyrm"; rDwell = "dungeon"; rFood = "gems_moon"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 34: rHue = 0x6F5; rScales = "purple"; rName = "the darklight wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 35: rHue = 0x869; rScales = "yellow"; rName = "the quartz wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = "quartz"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 36: rHue = 0xB02; rScales = "red"; rName = "the rosescale wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 37: rHue = 0x93E; rScales = "red"; rName = "the ruby wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = "ruby"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 38: rHue = 0x7CA; rScales = "red"; rName = "the rubystar wyrm"; bright = true; rDwell = "dungeon"; rFood = "gems_moon"; rDrop = "star ruby"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 39: rHue = 0x94D; rScales = "purple"; rName = "the spinel wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = "spinel"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 40: rHue = 0x883; rScales = "blue"; rName = "the topaz wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = "topaz"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 41: rHue = 0x95D; rScales = "blue"; rName = "the valorite wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "valorite"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 42: rHue = 0x7CB; rScales = "purple"; rName = "the velvet wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 43: rHue = 0x95E; rScales = "green"; rName = "the verite wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "verite"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 44: rHue = 0xB5A; rScales = "blue"; rName = "the zircon wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 45: rHue = 0x957; rScales = "red"; rName = "the agapite wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "agapite"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 46: rHue = 0x7C7; rScales = "green"; rName = "the akira wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 47: rHue = 0x7CE; rScales = "yellow"; rName = "the amber wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 48: rHue = 0x944; rScales = "blue"; rName = "the azure wyrm"; rDwell = "dungeon"; rFood = "gems_gold"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 49: rHue = 0x8DD; rScales = "black"; rName = "the ebony wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 50: rHue = 0x8E3; rScales = "purple"; rName = "the evil wyrm"; rDwell = "dungeon"; rFood = "fire_meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 51: rHue = 0x942; rScales = "white"; rName = "the iron wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "iron"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 52: rHue = 0x943; rScales = "green"; rName = "the garnet wyrm"; rDwell = "dungeon"; rFood = "nox"; rDrop = "garnet"; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 53: rHue = 0x950; rScales = "green"; rName = "the emerald wyrm"; rDwell = "dungeon"; rFood = "nox"; rDrop = "emerald"; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 54: rHue = 0x702; rScales = "red"; rName = "the redstar wyrm"; bright = true; rDwell = "dungeon"; rFood = "moon_fire"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = ""; break;
					case 55: rHue = 0xB3B; rScales = "white"; rName = "the marble wyrm"; rDwell = "dungeon"; rFood = "gems"; rDrop = "marble"; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 56: rHue = 0x708; rScales = "red"; rName = "the vermillion wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 57: rHue = 0x77A; rScales = "red"; rName = "the ochre wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 58: rHue = 0xB5E; rScales = "black"; rName = "the onyx wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "onyx"; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 59: rHue = 0x95B; rScales = "yellow"; rName = "the umber wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 60: rHue = 0x6FB; rScales = "purple"; rName = "the baneful wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 61: rHue = 0x870; rScales = "red"; rName = "the bloodscale wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 62: rHue = 0xA9F; rScales = "purple"; rName = "the corrupt wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 63: rHue = 0xBB0; rScales = "black"; rName = "the dark wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 64: rHue = 0x877; rScales = "black"; rName = "the dismal wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 65: rHue = 0x87E; rScales = "purple"; rName = "the drowscale wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 66: rHue = 0x705; rScales = "yellow"; rName = "the gold wyrm"; rDwell = "dungeon"; rFood = "gold"; rDrop = "gold"; rCategory = "void"; rBreath = 23; rPoison = 0; rBlood = ""; break;
					case 67: rHue = 0x8B8; rScales = "black"; rName = "the grim wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 68: rHue = 0x6FD; rScales = "purple"; rName = "the malicious wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 69: rHue = 0x86B; rScales = "black"; rName = "the shadowscale wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 70: rHue = 0x95C; rScales = "black"; rName = "the shadowy wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = "shadow iron"; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 71: rHue = 0x7CC; rScales = "purple"; rName = "the vile wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 72: rHue = 0x6FE; rScales = "purple"; rName = "the wicked wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 73: rHue = 0x6F9; rScales = "void"; rName = "the umbra wyrm"; rDwell = "dungeon"; rFood = "meat"; rDrop = ""; rCategory = "void"; rBreath = 24; rPoison = 0; rBlood = ""; break;
					case 74: rHue = 0x776; rScales = "red"; rName = "the burnt wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 75: rHue = 0x86C; rScales = "red"; rName = "the fire wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 76: rHue = 0x701; rScales = "red"; rName = "the firelight wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "scorching ooze"; break;
					case 77: rHue = 0xB12; rScales = "red"; rName = "the lava wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 78: rHue = 0xB38; rScales = "black"; rName = "the lavarock wyrm"; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 79: rHue = 0xB13; rScales = "red"; rName = "the magma wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 80: rHue = 0x827; rScales = "red"; rName = "the vulcan wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "hot magma"; break;
					case 81: rHue = 0xAB3; rScales = "black"; rName = "the charcoal wyrm"; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 82: rHue = 0xAFA; rScales = "red"; rName = "the cinder wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 83: rHue = 0x93D; rScales = "red"; rName = "the darkfire wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 84: rHue = 0xB54; rScales = "red"; rName = "the flare wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 85: rHue = 0x775; rScales = "red"; rName = "the hell wyrm"; bright = true; rDwell = "fire"; rFood = "fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 86: rHue = 0x779; rScales = "red"; rName = "the firerock wyrm"; bright = true; rDwell = "fire"; rFood = "fire_meat"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 87: rHue = 0xB09; rScales = "white"; rName = "the steam wyrm"; rDwell = "fire"; rFood = "meat"; rDrop = "granite"; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 88: rHue = 0x85D; rScales = "green"; rName = "the forest wyrm"; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 89: rHue = 0x6F6; rScales = "green"; rName = "the green wyrm"; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 90: rHue = 0xB28; rScales = "green"; rName = "the greenscale wyrm"; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 91: rHue = 0xB00; rScales = "sea"; rName = "the evergreen wyrm"; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 92: rHue = 0xACC; rScales = "green"; rName = "the grove wyrm"; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 93: rHue = 0x856; rScales = "sea"; rName = "the moss wyrm"; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 94: rHue = 0x91E; rScales = "green"; rName = "the woodland wyrm"; rDwell = "forest"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 95: rHue = 0x883; rScales = "sea"; rName = "the amazon wyrm"; rDwell = "jungle"; rFood = "meat_nox"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "green blood"; break;
					case 96: rHue = 0xB44; rScales = "green"; rName = "the jungle wyrm"; rDwell = "jungle"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 97: rHue = 0x706; rScales = "yellow"; rName = "the nova wyrm"; bright = true; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = "glowing goo"; break;
					case 98: rHue = 0xAF7; rScales = "red"; rName = "the crimson wyrm"; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 99: rHue = 0x86A; rScales = "vile"; rName = "the dusk wyrm"; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 100: rHue = 0xB01; rScales = "red"; rName = "the red wyrm"; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 101: rHue = 0x6FC; rScales = "blue"; rName = "the sky wyrm"; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "wind"; rBreath = 47; rPoison = 0; rBlood = ""; break;
					case 102: rHue = 0x95E; rScales = "green"; rName = "the spring wyrm"; rDwell = "land"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 103: rHue = 0x703; rScales = "purple"; rName = "the orchid wyrm"; rDwell = "land"; rFood = "meat_nox"; rDrop = ""; rCategory = "poison"; rBreath = 9; rPoison = 1; rBlood = ""; break;
					case 104: rHue = 0x981; rScales = "red"; rName = "the solar wyrm"; bright = true; rDwell = "land"; rFood = "moon_fire"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = ""; break;
					case 105: rHue = 0x6F8; rScales = "white"; rName = "the star wyrm"; bright = true; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "star"; rBreath = 45; rPoison = 0; rBlood = ""; break;
					case 106: rHue = 0x869; rScales = "yellow"; rName = "the sun wyrm"; bright = true; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = ""; break;
					case 107: rHue = 0x95D; rScales = "blue"; rName = "the moon wyrm"; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 108: rHue = 0xB9D; rScales = "black"; rName = "the night wyrm"; rDwell = "land"; rFood = "moon"; rDrop = ""; rCategory = "void"; rBreath = 25; rPoison = 0; rBlood = ""; break;
					case 109: rHue = 0xB31; rScales = "black"; rName = "the mountain wyrm"; rDwell = "mountain"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 110: rHue = 0x99B; rScales = "white"; rName = "the rock wyrm"; rDwell = "mountain"; rFood = "meat"; rDrop = "granite"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 111: rHue = 0xB32; rScales = "black"; rName = "the obsidian wyrm"; rDwell = "mountain"; rFood = "gems_fire"; rDrop = "obsidian"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 112: rHue = 0x855; rScales = "blue"; rName = "the blue wyrm"; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 113: rHue = 0x959; rScales = "red"; rName = "the copper wyrm"; rDwell = "sand"; rFood = "meat"; rDrop = "copper"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 114: rHue = 0x952; rScales = "red"; rName = "the copperish wyrm"; rDwell = "sand"; rFood = "meat"; rDrop = "dull copper"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 115: rHue = 0x797; rScales = "yellow"; rName = "the yellow wyrm"; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 116: rHue = 0x957; rScales = "yellow"; rName = "the earth wyrm"; rDwell = "sand"; rFood = "gems_meat"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 117: rHue = 0x713; rScales = "yellow"; rName = "the desert wyrm"; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "sand"; rBreath = 8; rPoison = 0; rBlood = ""; break;
					case 118: rHue = 0x8BC; rScales = "yellow"; rName = "the dune wyrm"; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "sand"; rBreath = 8; rPoison = 0; rBlood = ""; break;
					case 119: rHue = 0x712; rScales = "yellow"; rName = "the sand wyrm"; rDwell = "sand"; rFood = "meat"; rDrop = ""; rCategory = "sand"; rBreath = 8; rPoison = 0; rBlood = ""; break;
					case 120: rHue = 0x945; rScales = "blue"; rName = "the nepturite wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = "nepturite"; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 121: rHue = 0x8D1; rScales = "blue"; rName = "the storm wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "storm"; rBreath = 46; rPoison = 0; rBlood = ""; break;
					case 122: rHue = 0x8C2; rScales = "blue"; rName = "the tide wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "electrical"; rBreath = 13; rPoison = 0; rBlood = ""; break;
					case 123: rHue = 0xB07; rScales = "sea"; rName = "the seastone wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 124: rHue = 0x707; rScales = "blue"; rName = "the aqua wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 125: rHue = 0xB3D; rScales = "blue"; rName = "the lagoon wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 126: rHue = 0x7CD; rScales = "blue"; rName = "the loch wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 127: rHue = 0xAE9; rScales = "green"; rName = "the algae wyrm"; rDwell = "sea"; rFood = "nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = ""; break;
					case 128: rHue = 0x854; rScales = "yellow"; rName = "the coastal wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "sand"; rBreath = 8; rPoison = 0; rBlood = ""; break;
					case 129: rHue = 0xB7F; rScales = "red"; rName = "the coral wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "steam"; rBreath = 16; rPoison = 0; rBlood = ""; break;
					case 130: rHue = 0xAFF; rScales = "plant"; rName = "the ivy wyrm"; rDwell = "sea"; rFood = "fish_sea"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 0; rBlood = ""; break;
					case 131: rHue = 0x860; rScales = "ice"; rName = "the glacial wyrm"; rDwell = "snow"; rFood = "fish"; rDrop = "ice"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = "freezing water"; break;
					case 132: rHue = 0xAF3; rScales = "white"; rName = "the ice wyrm"; bright = true; rDwell = "snow"; rFood = "gems"; rDrop = "ice"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = "freezing water"; break;
					case 133: rHue = 0xB7A; rScales = "blue"; rName = "the icescale wyrm"; bright = true; rDwell = "snow"; rFood = "gems"; rDrop = "ice"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = "freezing water"; break;
					case 134: rHue = 0x9C4; rScales = "white"; rName = "the silver wyrm"; rDwell = "snow"; rFood = "meat"; rDrop = "silver"; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = "quick silver"; break;
					case 135: rHue = 0x86D; rScales = "ice"; rName = "the blizzard wyrm"; rDwell = "snow"; rFood = "meat"; rDrop = ""; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 136: rHue = 0x87D; rScales = "white"; rName = "the frost wyrm"; rDwell = "snow"; rFood = "meat"; rDrop = "ice"; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 137: rHue = 0x8BA; rScales = "white"; rName = "the snow wyrm"; rDwell = "snow"; rFood = "meat"; rDrop = ""; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 138: rHue = 0x911; rScales = "white"; rName = "the white wyrm"; rDwell = "snow"; rFood = "meat"; rDrop = ""; rCategory = "cold"; rBreath = 12; rPoison = 0; rBlood = ""; break;
					case 139: rHue = 0xAB1; rScales = "black"; rName = "the black wyrm"; rDwell = "swamp"; rFood = "meat_nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = "acidic slime"; break;
					case 140: rHue = 0x88D; rScales = "green"; rName = "the mire wyrm"; rDwell = "swamp"; rFood = "nox_fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 141: rHue = 0x945; rScales = "sea"; rName = "the moor wyrm"; rDwell = "swamp"; rFood = "nox_fire"; rDrop = ""; rCategory = "fire"; rBreath = 9; rPoison = 0; rBlood = ""; break;
					case 142: rHue = 0x8B2; rScales = "green"; rName = "the bog wyrm"; rDwell = "swamp"; rFood = "nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = ""; break;
					case 143: rHue = 0xB27; rScales = "green"; rName = "the bogscale wyrm"; rDwell = "swamp"; rFood = "nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 1; rBlood = ""; break;
					case 144: rHue = 0x77D; rScales = "green"; rName = "the swampfire wyrm"; bright = true; rDwell = "swamp"; rFood = "meat_nox"; rDrop = ""; rCategory = "poison"; rBreath = 10; rPoison = 0; rBlood = ""; break;
					case 145: rHue = 0x8EC; rScales = "green"; rName = "the marsh wyrm"; rDwell = "swamp"; rFood = "meat"; rDrop = ""; rCategory = "weed"; rBreath = 34; rPoison = 1; rBlood = ""; break;
					case 146: rHue = 0x7C7; rScales = "green"; rName = "the xormite wyrm"; bright = true; rDwell = "dungeon"; rFood = "moon"; rDrop = ""; rCategory = "radiation"; rBreath = 11; rPoison = 0; rBlood = ""; break;
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

				if ( rCategory == "cold" ){ 			phys = 45;	fire = 5;  cold = 70; pois = 20; engy = 20; }
				else if ( rCategory == "electrical" ){ 	phys = 45;	fire = 20; cold = 20; pois = 5;  engy = 70; }
				else if ( rCategory == "fire" ){ 		phys = 45;	fire = 70; cold = 5;  pois = 20; engy = 20; }
				else if ( rCategory == "poison" ){ 		phys = 45;	fire = 20; cold = 20; pois = 70; engy = 5; }
				else if ( rCategory == "radiation" ){ 	phys = 45;	fire = 25; cold = 5;  pois = 25; engy = 60; }
				else if ( rCategory == "sand" ){ 		phys = 35;	fire = 60; cold = 20; pois = 5;  engy = 30; }
				else if ( rCategory == "steam" ){ 		phys = 45;	fire = 60; cold = 5;  pois = 20; engy = 20; }
				else if ( rCategory == "void" ){ 		phys = 35;	fire = 40; cold = 40; pois = 40; engy = 40; }
				else if ( rCategory == "weed" ){ 		phys = 35;	fire = 15; cold = 15; pois = 60; engy = 40; }


				else if ( rCategory == "wind" ){ 		phys = 35;	fire = 20; cold = 25; pois = 20; engy = 60; }
				else if ( rCategory == "storm" ){ 		phys = 35;	fire = 60; cold = 25; pois = 20; engy = 60; }
				else if ( rCategory == "star" ){ 		phys = 35;	fire = 20; cold = 25; pois = 20; engy = 60; }


				else { 									phys = 45;	fire = 30; cold = 30; pois = 30; engy = 30; }

				Body = rBody;
				Title = rName;
				Hue = rHue;
				YellHue = dragon;

				if ( bright ){ AddItem( new LighterSource() ); }

				SetStr( 721, 760 );
				SetDex( 101, 130 );
				SetInt( 386, 425 );

				SetHits( 433, 456 );

				SetDamage( 17, 25 );

				SetResistance( ResistanceType.Physical, (phys+10), (phys+20) );
				SetResistance( ResistanceType.Fire, (fire+10), (fire+20) );
				SetResistance( ResistanceType.Cold, (cold+10), (cold+20) );
				SetResistance( ResistanceType.Poison, (pois+10), (pois+20) );
				SetResistance( ResistanceType.Energy, (engy+10), (engy+20) );

				SetSkill( SkillName.EvalInt, 99.1, 100.0 );
				SetSkill( SkillName.Magery, 99.1, 100.0 );
				SetSkill( SkillName.MagicResist, 99.1, 100.0 );
				SetSkill( SkillName.Tactics, 97.6, 100.0 );
				SetSkill( SkillName.Wrestling, 90.1, 100.0 );

				Fame = 18000;
				Karma = -18000;

				VirtualArmor = 64;

				Tamable = true;
				ControlSlots = 3;
				MinTameSkill = 96.3;

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
	}
}
