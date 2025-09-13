using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Network;

// PugilistMits
// PugilistGlove
// PugilistGloves

namespace Server.Misc
{
	class ArtifactBuilder
	{
		public static Item CreateArtifact( string arty )
		{
			Item artifact = null;
			int AddBonuses = 1;

			if ( arty == "random" )
			{
				arty = "any";
				int HighSearch = 300 + 3; // THERE ARE 300 ARTIFACTS SO CHANGE THE SECOND NUMBER TO THE NUMBER OF CATEGORIES LISTED BELOW...
				switch ( Utility.RandomMinMax( 1, HighSearch ) )
				{
					case 3:		arty = "invulnerable";	break;
				}
			}

			if ( arty == "driftwood" ) // ONLY FOR SEA TRAVELERS
			{
				artifact = CreateWoodItem();
				if ( artifact != null ){ artifact.Hue = 0x5B2; artifact.Name = "runed driftwood " + artifact.Name; }
			}
			else if ( arty == "kelp" ) // ONLY FOR SEA TRAVELERS
			{
				artifact = CreateLeatherArmor( "any" );
				if ( artifact != null ){ artifact.Hue = 0x483; artifact.Name = artifact.Name.Replace("leather", "kelp woven"); }
			}
			else if ( arty == "barnacle" ) // ONLY FOR SEA TRAVELERS
			{
				artifact = CreatePlateArmor( "any" );
					if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ artifact = CreateMetalShield(); }
				if ( artifact != null ){ artifact.Name = "barnacled " + artifact.Name; artifact.Hue = 0xB91; }
			}
			else if ( arty == "bronzed" ) // ONLY FOR SEA TRAVELERS
			{
				artifact = CreateMetalItem( "female" );
				if ( artifact != null ){ artifact.Name = "bronzed " + artifact.Name + " of the valkyrie"; artifact.Hue = 2418; }
			}
			else if ( arty == "neptune" ) // ONLY FOR SEA TRAVELERS
			{
				switch ( Utility.RandomMinMax( 0, 4 ) ) 
				{
					case 1: artifact = CreateArmorItem( "any" ); break;
					case 2: artifact = CreateBowItem(); break;
					case 3: artifact = CreateMetalShield(); break;
					case 4: artifact = CreateMetalWeapon(); break;
				}
				if ( artifact is PlateLegs ){ artifact.ItemID = 0x2B6B; }
				else if ( artifact is PlateArms ){ artifact.ItemID = 0x2B6C; }
				else if ( artifact is PlateChest ){ artifact.ItemID = 0x2B67; }
				else if ( artifact is PlateHelm ){ artifact.ItemID = 0x140E; artifact.Name = "helm"; }

				if ( artifact != null ){ artifact.Name = artifact.Name + " of Neptune"; artifact.Hue = 0x84C; }
			}
			else if ( arty == "invulnerable" )
			{
				artifact = CreateMetalProtection( "any" );
				if ( artifact != null ){ artifact.Hue = 0x4F2; artifact.Name = artifact.Name + " of invulnerability"; }
			}
			else
			{
				artifact = Loot.RandomArty();
				AddBonuses = 0;
			}

			while ( artifact == null )
			{
				artifact = Loot.RandomArty();
			}

			if ( AddBonuses == 1 )
			{
				if ( artifact is BaseWeapon ){ PowerArtifactWeapon( artifact, arty ); }
				else if ( artifact is BaseShield ){ PowerArtifactShield( artifact, arty ); }
				else if ( artifact is BaseArmor ){ PowerArtifactArmor( artifact, arty ); }
				else if ( artifact is BaseJewel ){ PowerArtifactJewel( artifact, arty ); }
			}

			if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) )
			{
				LockableContainer box = new UnidentifiedArtifact();
				box.DropItem(artifact);
				box.ItemID = artifact.ItemID;
				box.Hue = artifact.Hue;
				box.Name = RandomThings.GetOddityAdjective() + " artifact";
				return box;
			}
			return artifact;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void PowerArtifactWeapon( Item artifact, string arty )
		{
			BaseWeapon weapon = (BaseWeapon)artifact;
			ArtifactDurability( artifact );

			if ( arty == "driftwood" )
			{
				weapon.WeaponAttributes.HitLightning = 40;
				weapon.WeaponAttributes.HitLowerDefend = 40;
				weapon.Attributes.WeaponSpeed = 30;
				weapon.Attributes.WeaponDamage = 50;
			}
			else if ( arty == "bronzed" )
			{
				weapon.Attributes.BonusStr = 5;
				weapon.Attributes.BonusDex = 5;
				weapon.Attributes.BonusHits = 5;
				weapon.Attributes.BonusStam = 5;
				weapon.Attributes.RegenStam = 3;
				weapon.Attributes.LowerManaCost = 10;
			}
			else if ( arty == "neptune" )
			{
				weapon.WeaponAttributes.HitLightning = 25;
				weapon.Attributes.BonusStam = 10;
				weapon.Attributes.WeaponSpeed = 50;
				weapon.Slayer = SlayerName.NeptunesBane;
				weapon.Attributes.WeaponDamage = 75;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void PowerArtifactShield( Item artifact, string arty )
		{
			BaseShield shield = (BaseShield)artifact;
			ArtifactDurability( artifact );

			if ( arty == "driftwood" )
			{
				shield.Attributes.SpellChanneling = 1;
				shield.Attributes.ReflectPhysical = 30;
				shield.Attributes.DefendChance = 10;
				shield.PhysicalBonus = 5;
				shield.EnergyBonus = 10;
			}
			else if ( arty == "bronzed" )
			{
				shield.Attributes.BonusStr = 5;
				shield.Attributes.BonusDex = 5;
				shield.Attributes.BonusHits = 5;
				shield.Attributes.BonusStam = 5;
				shield.Attributes.RegenStam = 3;
				shield.Attributes.LowerManaCost = 10;
				shield.PhysicalBonus = 6;
				shield.FireBonus = 11;
				shield.ColdBonus = 6;
				shield.PoisonBonus = 8;
				shield.EnergyBonus = 6;
			}
			else if ( arty == "invulnerable" )
			{
				shield.PhysicalBonus = 8;
				shield.Attributes.SpellChanneling = 1;
				shield.Attributes.ReflectPhysical = 10;
				shield.Attributes.DefendChance = 15;
				shield.ArmorAttributes.LowerStatReq = 100;
				shield.MaxHitPoints = Utility.RandomMinMax(126, 150);
				shield.HitPoints = Utility.RandomMinMax(100, 125);
			}
			else if ( arty == "barnacle" )
			{
				shield.PhysicalBonus = 30;
				shield.Attributes.DefendChance = 15;
				shield.ArmorAttributes.SelfRepair = 5;
				shield.MaxHitPoints = Utility.RandomMinMax(126, 150);
				shield.HitPoints = Utility.RandomMinMax(100, 125);
			}
			else if ( arty == "neptune" )
			{
				shield.Attributes.RegenStam = 10;
				shield.FireBonus = 25;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void PowerArtifactJewel( Item artifact, string arty )
		{
			BaseJewel item = (BaseJewel)artifact;

			if ( arty == "NOT USED" )
			{
				item.Attributes.CastRecovery = 2;
				item.Attributes.CastSpeed = 2;
				item.Attributes.LowerManaCost = 20;
				item.Attributes.LowerRegCost = 20;
				item.Attributes.RegenMana = 5;
				item.Attributes.SpellDamage = 20;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void PowerArtifactArmor( Item artifact, string arty )
		{
			BaseArmor armor = (BaseArmor)artifact;
			ArtifactDurability( artifact );

			if ( arty == "driftwood" )
			{
				armor.Attributes.DefendChance = 10;
				armor.PhysicalBonus = 20;
				armor.EnergyBonus = 23;
				armor.ArmorAttributes.MageArmor = 1;
			}
			else if ( arty == "bronzed" )
			{
				armor.Attributes.BonusStr = 5;
				armor.Attributes.BonusDex = 5;
				armor.Attributes.BonusHits = 5;
				armor.Attributes.BonusStam = 5;
				armor.Attributes.RegenStam = 3;
				armor.Attributes.LowerManaCost = 10;
				armor.PhysicalBonus = 6;
				armor.FireBonus = 11;
				armor.ColdBonus = 6;
				armor.PoisonBonus = 8;
				armor.EnergyBonus = 6;
			}
			else if ( arty == "invulnerable" )
			{
				armor.PhysicalBonus = 16;
				armor.ArmorAttributes.MageArmor = 1;
				armor.Attributes.ReflectPhysical = 10;
				armor.Attributes.DefendChance = 15;
				armor.ArmorAttributes.LowerStatReq = 100;
				armor.MaxHitPoints = Utility.RandomMinMax(126, 150);
				armor.HitPoints = Utility.RandomMinMax(100, 125);
			}
			else if ( arty == "kelp" )
			{
				armor.Attributes.BonusHits = 5;
				armor.Attributes.BonusMana = 8;
				armor.Attributes.RegenMana = 2;
				armor.Attributes.SpellDamage = 8;
				armor.Attributes.LowerRegCost = 15;
				armor.PhysicalBonus = 3;
				armor.FireBonus = 9;
				armor.ColdBonus = 9;
				armor.PoisonBonus = 5;
				armor.EnergyBonus = 11;
			}
			else if ( arty == "barnacle" )
			{
				armor.PhysicalBonus = 30;
				armor.Attributes.DefendChance = 15;
				armor.ArmorAttributes.SelfRepair = 5;
				armor.MaxHitPoints = Utility.RandomMinMax(126, 150);
				armor.HitPoints = Utility.RandomMinMax(100, 125);
			}
			else if ( arty == "neptune" )
			{
				armor.Attributes.RegenStam = 10;
				armor.FireBonus = 20;
				armor.SkillBonuses.SetValues( 0, SkillName.Fishing, 10 );
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateWoodItem()
		{
			Item item = null;

			int nType = Utility.RandomMinMax( 0, 15 );

			switch ( nType )
			{
				case 0:		item = new QuarterStaff();			item.Name = "staff";				break;
				case 3:		item = new Club();					item.Name = "club";					break;
				case 4:		item = new GnarledStaff();			item.Name = "staff";				break;
				case 5:		item = new ShepherdsCrook();		item.Name = "crook";				break;
				case 6: case 7: case 8: case 9:
							item = CreateWoodenArmor();												break;
				case 10: case 11: case 12: case 13:
							item = CreateBowItem();													break;
				case 14:	item = new Nunchaku();				item.Name = "nunchaku";				break;
				case 15:	item = new Bokuto();				item.Name = "bokuto";				break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateArmorItem( string gender )
		{
			Item item = null;

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:		item = CreateMetalArmor( gender );		break;
				case 1:		item = CreateMetalArmor( gender );		break;
				case 2:		item = CreateStuddedArmor( gender );	break;
				case 3:		item = CreateLeatherArmor( gender );	break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateMetalItem( string gender )
		{
			Item item = null;

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:		item = CreateMetalWeapon();			break;
				case 1:		item = CreateMetalWeapon();			break;
				case 2:		item = CreateMetalShield();			break;
				case 3:		item = CreateMetalArmor( gender );	break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateMetalProtection( string gender )
		{
			Item item = null;

			switch ( Utility.RandomMinMax( 0, 1 ) )
			{
				case 0:		item = CreateMetalShield();			break;
				case 1:		item = CreateMetalArmor( gender );	break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateMetalWeapon()
		{
			Item item = null;

			int nType = Utility.RandomMinMax( 0, 41 );

			switch ( nType )
			{
				case 1:		item = new Cutlass();			item.Name = "cutlass";			break;
				case 2:		item = new Katana();			item.Name = "katana";			break;
				case 3:		item = new Kryss();				item.Name = "kryss";			break;
				case 4:		item = new Broadsword();		item.Name = "broadsword";		break;
				case 5:		item = new Longsword();			item.Name = "longsword";		break;
				case 6:		item = new ThinLongsword();		item.Name = "longsword";		break;
				case 7:		item = new VikingSword();		item.Name = "barbarian sword";	break;
				case 8:		item = new Scimitar();			item.Name = "scimitar";			break;
				case 9:		item = new BoneHarvester();		item.Name = "sickle";			break;
				case 10:	item = new CrescentBlade();		item.Name = "crescent blade";	break;
				case 11:	item = new DoubleBladedStaff();	item.Name = "bladed staff";		break;
				case 12:	item = new Lance();				item.Name = "lance";			break;
				case 13:	item = new Pike();				item.Name = "pike";				break;
				case 14:	item = new Scythe();			item.Name = "scythe";			break;
				case 15:	item = new Dagger();			item.Name = "dagger";			break;
				case 16:	item = new HammerPick();		item.Name = "hammer pick";		break;
				case 17:	item = new Mace();				item.Name = "mace";				break;
				case 18:	item = new Maul();				item.Name = "maul";				break;
				case 19:	item = new WarHammer();			item.Name = "war hammer";		break;
				case 20:	item = new WarMace();			item.Name = "war mace";			break;
				case 21:	item = new ExecutionersAxe();	item.Name = "great axe";		break;
				case 22:	item = new BattleAxe();			item.Name = "battle axe";		break;
				case 23:	item = new TwoHandedAxe();		item.Name = "two-handed axe";	break;
				case 24:	item = new Axe();				item.Name = "axe";				break;
				case 25:	item = new DoubleAxe();			item.Name = "double axe";		break;
				case 26:	item = new RoyalSword();		item.Name = "royal sword";		break;
				case 27:	item = new LargeBattleAxe();	item.Name = "large battle axe";	break;
				case 28:	item = new WarAxe();			item.Name = "war axe";			break;
				case 29:	item = new Bardiche();			item.Name = "bardiche";			break;
				case 30:	item = new Halberd();			item.Name = "halberd";			break;
				case 31:	item = new Pitchfork();			item.Name = "trident";			break;
				case 32:	item = new ShortSpear();		item.Name = "short spear";		break;
				case 33:	item = new Spear();				item.Name = "spear";			break;
				case 34:	item = new NoDachi();			item.Name = "no dachi";			break;
				case 35:	item = new Wakizashi();			item.Name = "wakizashi";		break;
				case 36:	item = new Tetsubo();			item.Name = "tetsubo";			break;
				case 37:	item = new Lajatang();			item.Name = "lajatang";			break;
				case 38:	item = new Daisho();			item.Name = "daisho";			break;
				case 39:	item = new Tekagi();			item.Name = "tekagi";			break;
				case 40:	item = new Kama();				item.Name = "kama";				break;
				case 41:	item = new Sai();				item.Name = "sai";				break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateMetalShield()
		{
			Item item = null;

			switch ( Utility.RandomMinMax( 0, 7 ) )
			{
				case 0:		item = new BronzeShield();		item.Name = "shield";			break;
				case 1:		item = new Buckler();			item.Name = "buckler";			break;
				case 2:		item = new MetalKiteShield();	item.Name = "kite shield";		break;
				case 3:		item = new HeaterShield();		item.Name = "large shield";		break;
				case 4:		item = new MetalShield();		item.Name = "small shield";		break;
				case 5:		item = new ChaosShield();		item.Name = "chaos shield";		break;
				case 6:		item = new OrderShield();		item.Name = "order shield";		break;
				case 7:		item = new RoyalShield();		item.Name = "royal shield";		break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateClothingItem()
		{
			Item item = null;

			switch ( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0:		item = new MagicRobe();		break;
				case 1:		item = new MagicHat();		break;
				case 2:		item = new MagicCloak();	break;
				case 3:		item = new MagicBoots();	break;
				case 4:		item = new MagicBelt();		break;
				case 5:		item = new MagicSash();		break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateLeatherArmor( string gender )
		{
			Item item = null;
			int nType = Utility.RandomMinMax( 0, 12 );

			switch ( nType )
			{
				case 0:		item = new LeatherArms();			item.Name = "leather sleeves";		break;
				case 1:		item = new LeatherChest();			item.Name = "leather tunic";
					if ( gender == "female" || Utility.RandomMinMax( 0, 3 ) == 1 )
						{ item = new FemaleLeatherChest();		item.Name = "leather armor"; }
					else if ( gender == "female" || Utility.RandomMinMax( 0, 3 ) == 1 )
						{ item = new LeatherBustierArms();		item.Name = "leather bustier"; }	break;
				case 2:		item = new LeatherGloves();			item.Name = "leather gloves";		break;
				case 3:		item = new LeatherGorget();			item.Name = "leather gorget";		break;
				case 4:	item = new LeatherLegs();				item.Name = "leather leggings";
					if ( gender == "female" || Utility.RandomMinMax( 0, 3 ) == 1 )
						{ item = new LeatherSkirt();				item.Name = "leather skirt"; }
					if ( Utility.RandomMinMax( 0, 5 ) == 1 )
						{ item = new LeatherShorts();				item.Name = "leather shorts"; }	break;
				case 5:		item = new LeatherCap();			item.Name = "leather cap";			break;
				case 6:		item = new LeatherJingasa();		item.Name = "leather jingasa";		break;
				case 7:		item = new LeatherDo();				item.Name = "leather do";			break;
				case 8:		item = new LeatherHiroSode();		item.Name = "leather hiro sode";	break;
				case 9:		item = new LeatherSuneate();		item.Name = "leather suneate";		break;
				case 10:	item = new LeatherHaidate();		item.Name = "leather haidate";		break;
				case 11:	item = new LeatherNinjaPants();		item.Name = "leather ninja pants";	break;
				case 12:	item = new LeatherNinjaJacket();	item.Name = "leather ninja jacket";	break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateStuddedArmor( string gender )
		{
			Item item = null;
			int nType = Utility.RandomMinMax( 0, 10 );

			switch ( nType )
			{
				case 0:		item = new LeatherCap();			item.Name = "leather cap";			break;
				case 1:		item = new StuddedArms();			item.Name = "studded sleeves";		break;
				case 2:		item = new StuddedGloves();			item.Name = "studded gloves";		break;
				case 3:		item = new StuddedGorget();			item.Name = "studded gorget";		break;
				case 4:		item = new StuddedLegs();			item.Name = "studded leggings";		break;
				case 5:		item = new StuddedChest();			item.Name = "studded tunic";
					if ( gender == "female" || Utility.RandomMinMax( 0, 3 ) == 1 )
							{ item = new FemaleStuddedChest();	item.Name = "studded armor"; }
					else if ( gender == "female" || Utility.RandomMinMax( 0, 3 ) == 1 )
							{ item = new StuddedBustierArms();	item.Name = "studded bustier"; }	break;
				case 6:		item = new StuddedMempo();			item.Name = "studded mempo";		break;
				case 7:		item = new StuddedDo();				item.Name = "studded do";			break;
				case 8:		item = new StuddedHiroSode();		item.Name = "studded hiro sode";	break;
				case 9:		item = new StuddedSuneate();		item.Name = "studded suneate";		break;
				case 10:	item = new StuddedHaidate();		item.Name = "studded haidate";		break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateMetalArmor( string gender )
		{
			Item item = null;

			int nType = Utility.RandomMinMax( 0, 26 );

			switch ( nType )
			{
				case 0:		item = new ChainCoif();			item.Name = "chainmail coif";		break;
				case 1:		item = new ChainChest();		item.Name = "chainmail tunic";		break;
				case 2:		item = new ChainLegs();			item.Name = "chainmail leggings";	break;
				case 3:		item = new RingmailChest();		item.Name = "ringmail tunic";		break;
				case 4:		item = new RingmailLegs();		item.Name = "ringmail leggings";	break;
				case 5:		item = new RingmailArms();		item.Name = "ringmail sleeves";		break;
				case 6:		item = new RingmailGloves();	item.Name = "ringmail gloves";		break;
				case 7:		item = new PlateGorget();		item.Name = "platemail gorget";		break;
				case 8:		item = new PlateLegs();			item.Name = "platemail leggings";	break;
				case 9:		item = new PlateArms();			item.Name = "platemail arms";		break;
				case 10:	item = new PlateGloves();		item.Name = "platemail gauntlets";	break;
				case 11:	item = new PlateChest();		item.Name = "platemail tunic";
					if ( gender == "female" || Utility.RandomMinMax( 0, 3 ) == 1 )
						{ item = new FemalePlateChest(); item.Name = "platemail tunic"; }		break;
				case 12: case 13:
					switch ( Utility.RandomMinMax( 0, 4 ) )
					{
						case 0:		item = new PlateHelm();		item.Name = "platemail helm";	break;
						case 1:		item = new CloseHelm();		item.Name = "close helm";		break;
						case 2:		item = new Helmet();		item.Name = "helmet";			break;
						case 3:		item = new NorseHelm();		item.Name = "norse helm";		break;
						case 4:		item = new Bascinet();		item.Name = "bascinet";			break;
					}
					break;
				case 14: item = new ChainHatsuburi();			item.Name = "chainmail hatsuburi";	break;
				case 15: item = new PlateHatsuburi();			item.Name = "platemail hatsuburi";	break;
				case 16: item = new LightPlateJingasa();		item.Name = "platemail jingasa";	break;
				case 17: item = new HeavyPlateJingasa();		item.Name = "platemail jingasa";	break;
				case 18: item = new SmallPlateJingasa();		item.Name = "platemail jingasa";	break;
				case 19: item = new DecorativePlateKabuto();	item.Name = "platemail kabuto";		break;
				case 20: item = new PlateBattleKabuto();		item.Name = "platemail kabuto";		break;
				case 21: item = new StandardPlateKabuto();		item.Name = "platemail kabuto";		break;
				case 22: item = new PlateDo();					item.Name = "platemail do";			break;
				case 23: item = new PlateHiroSode();			item.Name = "platemail hiro sade";	break;
				case 24: item = new PlateSuneate();				item.Name = "platemail suneate";	break;
				case 25: item = new PlateHaidate();				item.Name = "platemail haidate";	break;
				case 26: item = new ChainHatsuburi();			item.Name = "chainmail hatsuburi";	break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreatePlateArmor( string gender )
		{
			Item item = null;

			int nType = Utility.RandomMinMax( 0, 24 );

			switch ( nType )
			{
				case 0: case 1: item = new PlateGorget();		item.Name = "platemail gorget";		break;
				case 2: case 3: item = new PlateLegs();			item.Name = "platemail leggings";	break;
				case 4: case 5: item = new PlateArms();			item.Name = "platemail arms";		break;
				case 6: case 7: item = new PlateGloves();		item.Name = "platemail gauntlets";	break;
				case 8: case 9: item = new PlateChest();		item.Name = "platemail tunic";
					if ( gender == "female" || Utility.RandomMinMax( 0, 3 ) == 1 )
						{ item = new FemalePlateChest(); 		item.Name = "platemail tunic"; }	break;
				case 10: case 11: case 12: case 13:
						{ item = new PlateHelm();				item.Name = "platemail helm"; }		break;
				case 14: item = new PlateHatsuburi();			item.Name = "platemail hatsuburi";	break;
				case 15: item = new LightPlateJingasa();		item.Name = "platemail jingasa";	break;
				case 16: item = new HeavyPlateJingasa();		item.Name = "platemail jingasa";	break;
				case 17: item = new SmallPlateJingasa();		item.Name = "platemail jingasa";	break;
				case 18: item = new DecorativePlateKabuto();	item.Name = "platemail kabuto";		break;
				case 19: item = new PlateBattleKabuto();		item.Name = "platemail kabuto";		break;
				case 20: item = new StandardPlateKabuto();		item.Name = "platemail kabuto";		break;
				case 21: item = new PlateDo();					item.Name = "platemail do";			break;
				case 22: item = new PlateHiroSode();			item.Name = "platemail hiro sade";	break;
				case 23: item = new PlateSuneate();				item.Name = "platemail suneate";	break;
				case 24: item = new PlateHaidate();				item.Name = "platemail haidate";	break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateBowItem()
		{
			Item item = null;

			int nType = Utility.RandomMinMax( 0, 7 );

			switch ( nType )
			{
				case 0:		item = new Crossbow();				item.Name = "crossbow";				break;
				case 1:		item = new HeavyCrossbow();			item.Name = "heavy crossbow";		break;
				case 2:		item = new RepeatingCrossbow();		item.Name = "repeating crossbow";	break;
				case 3:		item = new CompositeBow();			item.Name = "composite bow";		break;
				case 4:		item = new Bow();					item.Name = "bow";					break;
				case 5:		item = new ElvenCompositeLongbow();	item.Name = "woodland longbow";		break;
				case 6:		item = new MagicalShortbow();		item.Name = "woodland shortbow";		break;
				case 7:		item = new Yumi();					item.Name = "yumi";					break;
			}

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item CreateWoodenArmor()
		{
			Item item = null;
			switch ( Utility.RandomMinMax( 0, 7 ) ) 
			{
				case 0:		item = new LeatherArms();		item.ItemID = 0x1410;	item.Name = "arms";			break;
				case 1:		item = new LeatherChest();		item.ItemID = 0x1415;	item.Name = "tunic";		break;
				case 2:		item = new LeatherGloves();		item.ItemID = 0x1414;	item.Name = "gauntlets";	break;
				case 3:		item = new LeatherGorget();		item.ItemID = 0x1413;	item.Name = "gorget";		break;
				case 4:		item = new LeatherLegs();		item.ItemID = 0x1411;	item.Name = "leggings";		break;
				case 5:		item = new LeatherCap();		item.ItemID = 0x1412;	item.Name = "helm";			break;
				case 6:		item = new WoodenKiteShield();							item.Name = "kite shield";	break;
				case 7:		item = new WoodenShield();								item.Name = "shield";		break;
			}
			((BaseArmor)item).Resource = CraftResource.RegularWood;

			return item;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void TurnToPlant( Item artifact )
		{
			if ( artifact is LeatherArms ){ artifact.ItemID = 0x2FC8; }
			else if ( artifact is LeatherChest ){ artifact.ItemID = 0x2FC5; }
			else if ( artifact is FemaleLeatherChest ){ artifact.ItemID = 0x2FCB; }
			else if ( artifact is LeatherBustierArms ){ artifact.ItemID = 0x2FCB; }
			else if ( artifact is LeatherGloves ){ artifact.ItemID = 0x2FC6; }
			else if ( artifact is LeatherGorget ){ artifact.ItemID = 0x2FC7; }
			else if ( artifact is LeatherLegs ){ artifact.ItemID = 0x2FC9; }
			else if ( artifact is LeatherSkirt ){ artifact.ItemID = 0x2FCA; }
			else if ( artifact is LeatherShorts ){ artifact.ItemID = 0x2FC9; }
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void ArtifactDurability( Item artifact )
		{
			int bonus = Utility.RandomMinMax( 80, 160 );

			if ( artifact is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)artifact;
				weapon.MaxHitPoints = bonus;
				weapon.HitPoints = bonus;
			}
			else if ( artifact is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)artifact;
				armor.MaxHitPoints = bonus;
				armor.HitPoints = bonus;
			}
		}
	}
}