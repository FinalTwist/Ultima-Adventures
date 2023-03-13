using System;
using Server;
using System.Collections;
using Server.Misc;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Items;

namespace Server.Misc
{
    class MaterialInfo
    {
		public static string GetMaterialName( Item item )
		{
			string material = "";

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;

				if ( weapon.Resource == CraftResource.DullCopper ){ material = "dull copper"; }
				else if ( weapon.Resource == CraftResource.ShadowIron ){ material = "shadow iron"; }
				else if ( weapon.Resource == CraftResource.Copper ){ material = "copper"; }
				else if ( weapon.Resource == CraftResource.Bronze ){ material = "bronze"; }
				else if ( weapon.Resource == CraftResource.Gold ){ material = "golden"; }
				else if ( weapon.Resource == CraftResource.Agapite ){ material = "agapite"; }
				else if ( weapon.Resource == CraftResource.Verite ){ material = "verite"; }
				else if ( weapon.Resource == CraftResource.Valorite ){ material = "valorite"; }
				else if ( weapon.Resource == CraftResource.Steel ){ material = "steel"; }
				else if ( weapon.Resource == CraftResource.Brass ){ material = "brass"; }
				else if ( weapon.Resource == CraftResource.Mithril ){ material = "mithril"; }
				else if ( weapon.Resource == CraftResource.Xormite ){ material = "xormite"; }
				else if ( weapon.Resource == CraftResource.Obsidian ){ material = "obsidian"; }
				else if ( weapon.Resource == CraftResource.Nepturite ){ material = "nepturite"; }
				else if ( weapon.Resource == CraftResource.Dwarven ){ material = "dwarven"; }
				//else if ( weapon.Resource == CraftResource.Iron ){ material = "iron"; }
				//else if ( weapon.Resource == CraftResource.RegularLeather ){ material = "leather"; }
				else if ( weapon.Resource == CraftResource.SpinedLeather ){ material = "deep sea"; }
				else if ( weapon.Resource == CraftResource.HornedLeather ){ material = "lizard"; }
				else if ( weapon.Resource == CraftResource.BarbedLeather ){ material = "serpent"; }
				else if ( weapon.Resource == CraftResource.NecroticLeather ){ material = "necrotic"; }
				else if ( weapon.Resource == CraftResource.VolcanicLeather ){ material = "volcanic"; }
				else if ( weapon.Resource == CraftResource.FrozenLeather ){ material = "frozen"; }
				else if ( weapon.Resource == CraftResource.GoliathLeather ){ material = "goliath"; }
				else if ( weapon.Resource == CraftResource.DraconicLeather ){ material = "draconic"; }
				else if ( weapon.Resource == CraftResource.HellishLeather ){ material = "hellish"; }
				else if ( weapon.Resource == CraftResource.DinosaurLeather ){ material = "dinosaur"; }
				else if ( weapon.Resource == CraftResource.AlienLeather ){ material = "alien"; }
				//else if ( weapon.Resource == CraftResource.RegularWood ){ material = "wooden"; }
				else if ( weapon.Resource == CraftResource.AshTree ){ material = "ash"; }
				else if ( weapon.Resource == CraftResource.CherryTree ){ material = "cherry"; }
				else if ( weapon.Resource == CraftResource.EbonyTree ){ material = "ebony"; }
				else if ( weapon.Resource == CraftResource.GoldenOakTree ){ material = "golden oak"; }
				else if ( weapon.Resource == CraftResource.HickoryTree ){ material = "hickory"; }
				else if ( weapon.Resource == CraftResource.MahoganyTree ){ material = "mahogany"; }
				else if ( weapon.Resource == CraftResource.OakTree ){ material = "oak"; }
				else if ( weapon.Resource == CraftResource.PineTree ){ material = "pine"; }
				else if ( weapon.Resource == CraftResource.RosewoodTree ){ material = "rosewood"; }
				else if ( weapon.Resource == CraftResource.DriftwoodTree ){ material = "driftwood"; }
				else if ( weapon.Resource == CraftResource.WalnutTree ){ material = "walnut"; }
				else if ( weapon.Resource == CraftResource.ElvenTree ){ material = "elven"; }
				else if ( weapon.Resource == CraftResource.GhostTree ){ material = "ghostwood"; }
				else if ( weapon.Resource == CraftResource.PetrifiedTree ){ material = "petrified"; }
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				if ( armor.Resource == CraftResource.DullCopper ){ material = "dull copper"; }
				else if ( armor.Resource == CraftResource.ShadowIron ){ material = "shadow iron"; }
				else if ( armor.Resource == CraftResource.Copper ){ material = "copper"; }
				else if ( armor.Resource == CraftResource.Bronze ){ material = "bronze"; }
				else if ( armor.Resource == CraftResource.Gold ){ material = "golden"; }
				else if ( armor.Resource == CraftResource.Agapite ){ material = "agapite"; }
				else if ( armor.Resource == CraftResource.Verite ){ material = "verite"; }
				else if ( armor.Resource == CraftResource.Valorite ){ material = "valorite"; }
				else if ( armor.Resource == CraftResource.Steel ){ material = "steel"; }
				else if ( armor.Resource == CraftResource.Brass ){ material = "brass"; }
				else if ( armor.Resource == CraftResource.Mithril ){ material = "mithril"; }
				else if ( armor.Resource == CraftResource.Xormite ){ material = "xormite"; }
				else if ( armor.Resource == CraftResource.Obsidian ){ material = "obsidian"; }
				else if ( armor.Resource == CraftResource.Nepturite ){ material = "nepturite"; }
				else if ( armor.Resource == CraftResource.Dwarven ){ material = "dwarven"; }
				//else if ( armor.Resource == CraftResource.Iron ){ material = "iron"; }
				//else if ( armor.Resource == CraftResource.RegularLeather ){ material = "leather"; }
				else if ( armor.Resource == CraftResource.SpinedLeather ){ material = "deep sea"; }
				else if ( armor.Resource == CraftResource.HornedLeather ){ material = "lizard"; }
				else if ( armor.Resource == CraftResource.BarbedLeather ){ material = "serpent"; }
				else if ( armor.Resource == CraftResource.NecroticLeather ){ material = "necrotic"; }
				else if ( armor.Resource == CraftResource.VolcanicLeather ){ material = "volcanic"; }
				else if ( armor.Resource == CraftResource.FrozenLeather ){ material = "frozen"; }
				else if ( armor.Resource == CraftResource.GoliathLeather ){ material = "goliath"; }
				else if ( armor.Resource == CraftResource.DraconicLeather ){ material = "draconic"; }
				else if ( armor.Resource == CraftResource.HellishLeather ){ material = "hellish"; }
				else if ( armor.Resource == CraftResource.DinosaurLeather ){ material = "dinosaur"; }
				else if ( armor.Resource == CraftResource.AlienLeather ){ material = "alien"; }
				//else if ( armor.Resource == CraftResource.RegularWood ){ material = "wooden"; }
				else if ( armor.Resource == CraftResource.AshTree ){ material = "ash"; }
				else if ( armor.Resource == CraftResource.CherryTree ){ material = "cherry"; }
				else if ( armor.Resource == CraftResource.EbonyTree ){ material = "ebony"; }
				else if ( armor.Resource == CraftResource.GoldenOakTree ){ material = "golden oak"; }
				else if ( armor.Resource == CraftResource.HickoryTree ){ material = "hickory"; }
				else if ( armor.Resource == CraftResource.MahoganyTree ){ material = "mahogany"; }
				else if ( armor.Resource == CraftResource.OakTree ){ material = "oak"; }
				else if ( armor.Resource == CraftResource.PineTree ){ material = "pine"; }
				else if ( armor.Resource == CraftResource.RosewoodTree ){ material = "rosewood"; }
				else if ( armor.Resource == CraftResource.DriftwoodTree ){ material = "driftwood"; }
				else if ( armor.Resource == CraftResource.WalnutTree ){ material = "walnut"; }
				else if ( armor.Resource == CraftResource.ElvenTree ){ material = "elven"; }
				else if ( armor.Resource == CraftResource.GhostTree ){ material = "ghostwood"; }
				else if ( armor.Resource == CraftResource.PetrifiedTree ){ material = "petrified"; }
			}

			return material;
		}

		public static bool IsMetalItem( Item item )
		{
			if ( item == null )
				return false;

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;

				if ( weapon is BaseMagicStaff )
					return false;

				if ( weapon.Resource == CraftResource.DullCopper || 
					weapon.Resource == CraftResource.ShadowIron || 
					weapon.Resource == CraftResource.Copper || 
					weapon.Resource == CraftResource.Bronze || 
					weapon.Resource == CraftResource.Gold || 
					weapon.Resource == CraftResource.Agapite || 
					weapon.Resource == CraftResource.Verite || 
					weapon.Resource == CraftResource.Valorite || 
					weapon.Resource == CraftResource.Steel || 
					weapon.Resource == CraftResource.Brass || 
					weapon.Resource == CraftResource.Mithril || 
					weapon.Resource == CraftResource.Xormite || 
					weapon.Resource == CraftResource.Obsidian || 
					weapon.Resource == CraftResource.Nepturite || 
					weapon.Resource == CraftResource.Dwarven || 
					weapon.Resource == CraftResource.Iron ){ return true; }
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				if ( armor.Resource == CraftResource.DullCopper || 
					armor.Resource == CraftResource.ShadowIron || 
					armor.Resource == CraftResource.Copper || 
					armor.Resource == CraftResource.Bronze || 
					armor.Resource == CraftResource.Gold || 
					armor.Resource == CraftResource.Agapite || 
					armor.Resource == CraftResource.Verite || 
					armor.Resource == CraftResource.Valorite || 
					armor.Resource == CraftResource.Steel || 
					armor.Resource == CraftResource.Brass || 
					armor.Resource == CraftResource.Mithril || 
					armor.Resource == CraftResource.Xormite || 
					armor.Resource == CraftResource.Obsidian || 
					armor.Resource == CraftResource.Nepturite || 
					armor.Resource == CraftResource.Dwarven || 
					armor.Resource == CraftResource.Iron ){ return true; }
			}

			return false;
		}

		public static bool IsAnyKindOfMetalItem( Item item )
		{
			if ( item == null )
				return false;

			if ( IsMetalItem( item ) )
				return true;

			if ( IsStrangeMetalItem( item ) )
				return true;

			return false;
		}

		public static bool IsAnyKindOfWoodItem( Item item )
		{
			if ( item == null )
				return false;

			if ( IsWoodenItem( item ) )
				return true;

			if ( IsStrangeWoodItem( item ) )
				return true;

			return false;
		}

		public static bool IsAnyKindOfClothItem( Item item )
		{
			if ( item == null )
				return false;

			if ( IsLeatherItem( item ) )
				return true;

			if ( IsStrangeClothItem( item ) )
				return true;

			return false;
		}

		public static bool IsStrangeMetalItem( Item item )
		{
			if ( item == null )
				return false;
			else if ( item.Name == null )
				return false;

			if ( item is BaseWeapon || item is BaseArmor )
			{
				if ( item is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)item;
					if ( weapon.Resource != CraftResource.None ){ return false; }
				}
				if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;
					if ( armor.Resource != CraftResource.None ){ return false; }
				}

				if ( (item.Name).Contains("Ice") ){ return true; }
				else if ( (item.Name).Contains("Garnet") ){ return true; }
				else if ( (item.Name).Contains("Emerald") ){ return true; }
				else if ( (item.Name).Contains("Caddellite") ){ return true; }
				else if ( (item.Name).Contains("Amethyst") ){ return true; }
				else if ( (item.Name).Contains("Topaz") ){ return true; }
				else if ( (item.Name).Contains("Star Ruby") ){ return true; }
				else if ( (item.Name).Contains("Spinel") ){ return true; }
				else if ( (item.Name).Contains("Silver") ){ return true; }
				else if ( (item.Name).Contains("Quartz") ){ return true; }
				else if ( (item.Name).Contains("Onyx") ){ return true; }
				else if ( (item.Name).Contains("Marble") ){ return true; }
				else if ( (item.Name).Contains("Jade") ){ return true; }
				else if ( (item.Name).Contains("Ruby") ){ return true; }

				else if ( (item.Name).Contains("Beskar") ){ return true; }
				else if ( (item.Name).Contains("Carbonite") ){ return true; }
				else if ( (item.Name).Contains("Phrik") ){ return true; }
				else if ( (item.Name).Contains("Cortosis") ){ return true; }
				else if ( (item.Name).Contains("Songsteel") ){ return true; }
				else if ( (item.Name).Contains("Agrinium") ){ return true; }
				else if ( (item.Name).Contains("Durasteel") ){ return true; }
				else if ( (item.Name).Contains("Titanium") ){ return true; }
				else if ( (item.Name).Contains("Laminasteel") ){ return true; }
				else if ( (item.Name).Contains("Neuranium") ){ return true; }
				else if ( (item.Name).Contains("Promethium") ){ return true; }
				else if ( (item.Name).Contains("Quadranium") ){ return true; }
				else if ( (item.Name).Contains("Durite") ){ return true; }
				else if ( (item.Name).Contains("Farium") ){ return true; }
				else if ( (item.Name).Contains("Trimantium") ){ return true; }
				else if ( (item.Name).Contains("Xonolite") ){ return true; }
			}

			return false;
		}

		public static bool IsStrangeWoodItem( Item item )
		{
			if ( item == null )
				return false;
			else if ( item.Name == null )
				return false;

			if ( item is BaseWeapon || item is BaseArmor )
			{
				if ( item is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)item;
					if ( weapon.Resource != CraftResource.None ){ return false; }
				}
				if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;
					if ( armor.Resource != CraftResource.None ){ return false; }
				}

				if ( (item.Name).Contains("Veshok") ){ return true; }
				else if ( (item.Name).Contains("Cosian") ){ return true; }
				else if ( (item.Name).Contains("Greel") ){ return true; }
				else if ( (item.Name).Contains("Teej") ){ return true; }
				else if ( (item.Name).Contains("Kyshyyyk") ){ return true; }
				else if ( (item.Name).Contains("Laroon") ){ return true; }
				else if ( (item.Name).Contains("Borl") ){ return true; }
				else if ( (item.Name).Contains("Japor") ){ return true; }
			}

			return false;
		}

		public static bool IsStrangeClothItem( Item item )
		{
			if ( item == null )
				return false;
			else if ( item.Name == null )
				return false;

			if ( 	item is BaseWeapon || 
					item is BaseArmor || 
					item is MagicBelt || 
					item is MagicBoots || 
					item is MagicCloak || 
					item is MagicHat || 
					item is MagicRobe || 
					item is MagicSash || 
					item is BaseClothing )
			{
				if ( item is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)item;
					if ( weapon.Resource != CraftResource.None ){ return false; }
				}
				if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;
					if ( armor.Resource != CraftResource.None ){ return false; }
				}

				if ( (item.Name).Contains("Adesote") ){ return true; }
				else if ( (item.Name).Contains("Nylonite") ){ return true; }
				else if ( (item.Name).Contains("Biomesh") ){ return true; }
				else if ( (item.Name).Contains("Cerlin") ){ return true; }
				else if ( (item.Name).Contains("Polyfiber") ){ return true; }
				else if ( (item.Name).Contains("Durafiber") ){ return true; }
				else if ( (item.Name).Contains("Syncloth") ){ return true; }
				else if ( (item.Name).Contains("Hypercloth") ){ return true; }
				else if ( (item.Name).Contains("Flexicris") ){ return true; }
				else if ( (item.Name).Contains("Thermoweave") ){ return true; }
				else if ( (item.Name).Contains("Nylar") ){ return true; }

				// BONE
				else if ( (item.Name).Contains("Twi'lek") ){ return true; }
				else if ( (item.Name).Contains("Rodian") ){ return true; }
				else if ( (item.Name).Contains("Martian") ){ return true; }
				else if ( (item.Name).Contains("Cardassian") ){ return true; }
				else if ( (item.Name).Contains("Xindi") ){ return true; }
				else if ( (item.Name).Contains("Tusken") ){ return true; }
				else if ( (item.Name).Contains("Andorian") ){ return true; }
				else if ( (item.Name).Contains("Zabrak") ){ return true; }
			}

			return false;
		}

		public static string GetSpecialMaterialName( Item i )
		{
			string str = "";

			if ( i == null )
				return str;

			if ( IsStrangeWoodItem( i ) || IsStrangeMetalItem( i ) || IsStrangeClothItem( i ) )
			{
				if ( (i.Name).Contains("Ice") ){ str = "Ice Alloy "; }
				else if ( (i.Name).Contains("Garnet") ){ str = "Garnet Alloy "; }
				else if ( (i.Name).Contains("Emerald") ){ str = "Emerald Alloy "; }
				else if ( (i.Name).Contains("Caddellite") ){ str = "Caddellite Alloy "; }
				else if ( (i.Name).Contains("Amethyst") ){ str = "Amethyst Alloy "; }
				else if ( (i.Name).Contains("Topaz") ){ str = "Topaz Alloy "; }
				else if ( (i.Name).Contains("Star Ruby") ){ str = "Star Ruby Alloy "; }
				else if ( (i.Name).Contains("Spinel") ){ str = "Spinel Alloy "; }
				else if ( (i.Name).Contains("Silver") ){ str = "Silver Alloy "; }
				else if ( (i.Name).Contains("Quartz") ){ str = "Quartz Alloy "; }
				else if ( (i.Name).Contains("Onyx") ){ str = "Onyx Alloy "; }
				else if ( (i.Name).Contains("Marble") ){ str = "Marble Alloy "; }
				else if ( (i.Name).Contains("Jade") ){ str = "Jade Alloy "; }
				else if ( (i.Name).Contains("Ruby") ){ str = "Ruby Alloy "; }
				else if ( (i.Name).Contains("Beskar") ){ str = "Beskar Alloy "; }
				else if ( (i.Name).Contains("Carbonite") ){ str = "Carbonite Alloy "; }
				else if ( (i.Name).Contains("Phrik") ){ str = "Phrik Alloy "; }
				else if ( (i.Name).Contains("Cortosis") ){ str = "Cortosis Alloy "; }
				else if ( (i.Name).Contains("Songsteel") ){ str = "Songsteel Alloy "; }
				else if ( (i.Name).Contains("Agrinium") ){ str = "Agrinium Alloy "; }
				else if ( (i.Name).Contains("Durasteel") ){ str = "Durasteel Alloy "; }
				else if ( (i.Name).Contains("Titanium") ){ str = "Titanium Alloy "; }
				else if ( (i.Name).Contains("Laminasteel") ){ str = "Laminasteel Alloy "; }
				else if ( (i.Name).Contains("Neuranium") ){ str = "Neuranium Alloy "; }
				else if ( (i.Name).Contains("Promethium") ){ str = "Promethium Alloy "; }
				else if ( (i.Name).Contains("Quadranium") ){ str = "Quadranium Alloy "; }
				else if ( (i.Name).Contains("Durite") ){ str = "Durite Alloy "; }
				else if ( (i.Name).Contains("Farium") ){ str = "Farium Alloy "; }
				else if ( (i.Name).Contains("Trimantium") ){ str = "Trimantium Alloy "; }
				else if ( (i.Name).Contains("Xonolite") ){ str = "Xonolite Alloy "; }
				else if ( (i.Name).Contains("Veshok") ){ str = "Veshok Timber "; }
				else if ( (i.Name).Contains("Cosian") ){ str = "Cosian Timber "; }
				else if ( (i.Name).Contains("Greel") ){ str = "Greel Timber "; }
				else if ( (i.Name).Contains("Teej") ){ str = "Teej Timber "; }
				else if ( (i.Name).Contains("Kyshyyyk") ){ str = "Kyshyyyk Timber "; }
				else if ( (i.Name).Contains("Laroon") ){ str = "Laroon Timber "; }
				else if ( (i.Name).Contains("Borl") ){ str = "Borl Timber "; }
				else if ( (i.Name).Contains("Japor") ){ str = "Japor Timber "; }
				else if ( (i.Name).Contains("Adesote") ){ str = "Adesote Woven "; }
				else if ( (i.Name).Contains("Nylonite") ){ str = "Nylonite Woven "; }
				else if ( (i.Name).Contains("Biomesh") ){ str = "Biomesh Woven "; }
				else if ( (i.Name).Contains("Cerlin") ){ str = "Cerlin Woven "; }
				else if ( (i.Name).Contains("Polyfiber") ){ str = "Polyfiber Woven "; }
				else if ( (i.Name).Contains("Durafiber") ){ str = "Durafiber Woven "; }
				else if ( (i.Name).Contains("Syncloth") ){ str = "Syncloth Woven "; }
				else if ( (i.Name).Contains("Hypercloth") ){ str = "Hypercloth Woven "; }
				else if ( (i.Name).Contains("Flexicris") ){ str = "Flexicris Woven "; }
				else if ( (i.Name).Contains("Thermoweave") ){ str = "Thermoweave Woven "; }
				else if ( (i.Name).Contains("Nylar") ){ str = "Nylar Woven "; }
				else if ( (i.Name).Contains("Twi'lek") ){ str = "Twi'lek Skeletal "; }
				else if ( (i.Name).Contains("Rodian") ){ str = "Rodian Skeletal "; }
				else if ( (i.Name).Contains("Martian") ){ str = "Martian Skeletal "; }
				else if ( (i.Name).Contains("Cardassian") ){ str = "Cardassian Skeletal "; }
				else if ( (i.Name).Contains("Xindi") ){ str = "Xindi Skeletal "; }
				else if ( (i.Name).Contains("Tusken") ){ str = "Tusken Skeletal "; }
				else if ( (i.Name).Contains("Andorian") ){ str = "Andorian Skeletal "; }
				else if ( (i.Name).Contains("Zabrak") ){ str = "Zabrak Skeletal "; }
			}
			return str;
		}

		public static bool IsLeatherItem( Item item )
		{
			if ( item == null )
				return false;

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;

				if ( weapon.Resource == CraftResource.RegularLeather || 
					weapon.Resource == CraftResource.SpinedLeather || 
					weapon.Resource == CraftResource.HornedLeather || 
					weapon.Resource == CraftResource.BarbedLeather || 
					weapon.Resource == CraftResource.NecroticLeather || 
					weapon.Resource == CraftResource.VolcanicLeather || 
					weapon.Resource == CraftResource.FrozenLeather || 
					weapon.Resource == CraftResource.GoliathLeather || 
					weapon.Resource == CraftResource.DraconicLeather || 
					weapon.Resource == CraftResource.HellishLeather || 
					weapon.Resource == CraftResource.DinosaurLeather || 
					weapon.Resource == CraftResource.AlienLeather ){ return true; }
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				if ( armor.Resource == CraftResource.RegularLeather || 
					armor.Resource == CraftResource.SpinedLeather || 
					armor.Resource == CraftResource.HornedLeather || 
					armor.Resource == CraftResource.BarbedLeather || 
					armor.Resource == CraftResource.NecroticLeather || 
					armor.Resource == CraftResource.VolcanicLeather || 
					armor.Resource == CraftResource.FrozenLeather || 
					armor.Resource == CraftResource.GoliathLeather || 
					armor.Resource == CraftResource.DraconicLeather || 
					armor.Resource == CraftResource.HellishLeather || 
					armor.Resource == CraftResource.DinosaurLeather || 
					armor.Resource == CraftResource.AlienLeather ){ return true; }
			}

			return false;
		}

		public static bool IsWoodenItem( Item item )
		{
			if ( item == null )
				return false;

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;

				if ( weapon.Resource == CraftResource.RegularWood || 
					weapon.Resource == CraftResource.AshTree || 
					weapon.Resource == CraftResource.CherryTree || 
					weapon.Resource == CraftResource.EbonyTree || 
					weapon.Resource == CraftResource.GoldenOakTree || 
					weapon.Resource == CraftResource.HickoryTree || 
					weapon.Resource == CraftResource.MahoganyTree || 
					weapon.Resource == CraftResource.OakTree || 
					weapon.Resource == CraftResource.PineTree || 
					weapon.Resource == CraftResource.RosewoodTree || 
					weapon.Resource == CraftResource.DriftwoodTree || 
					weapon.Resource == CraftResource.WalnutTree || 
					weapon.Resource == CraftResource.ElvenTree || 
					weapon.Resource == CraftResource.GhostTree || 
					weapon.Resource == CraftResource.PetrifiedTree ){ return true; }
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				if ( armor.Resource == CraftResource.RegularWood || 
					armor.Resource == CraftResource.AshTree || 
					armor.Resource == CraftResource.CherryTree || 
					armor.Resource == CraftResource.EbonyTree || 
					armor.Resource == CraftResource.GoldenOakTree || 
					armor.Resource == CraftResource.HickoryTree || 
					armor.Resource == CraftResource.MahoganyTree || 
					armor.Resource == CraftResource.OakTree || 
					armor.Resource == CraftResource.PineTree || 
					armor.Resource == CraftResource.RosewoodTree || 
					armor.Resource == CraftResource.DriftwoodTree || 
					armor.Resource == CraftResource.WalnutTree || 
					armor.Resource == CraftResource.ElvenTree || 
					armor.Resource == CraftResource.GhostTree || 
					armor.Resource == CraftResource.PetrifiedTree ){ return true; }
			}
			else if ( item is BaseInstrument )
			{
				BaseInstrument lute = (BaseInstrument)item;

				if ( lute.Resource == CraftResource.RegularWood || 
					lute.Resource == CraftResource.AshTree || 
					lute.Resource == CraftResource.CherryTree || 
					lute.Resource == CraftResource.EbonyTree || 
					lute.Resource == CraftResource.GoldenOakTree || 
					lute.Resource == CraftResource.HickoryTree || 
					lute.Resource == CraftResource.MahoganyTree || 
					lute.Resource == CraftResource.OakTree || 
					lute.Resource == CraftResource.PineTree || 
					lute.Resource == CraftResource.RosewoodTree || 
					lute.Resource == CraftResource.DriftwoodTree || 
					lute.Resource == CraftResource.WalnutTree || 
					lute.Resource == CraftResource.ElvenTree || 
					lute.Resource == CraftResource.GhostTree || 
					lute.Resource == CraftResource.PetrifiedTree ){ return true; }
			}

			return false;
		}

		public static bool IsNotPlain( Item item )
		{
			if ( item == null )
				return false;

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;

				if ( weapon is BaseMagicStaff )
					return false;

				if ( weapon.Resource == CraftResource.DullCopper || 
					weapon.Resource == CraftResource.ShadowIron || 
					weapon.Resource == CraftResource.Copper || 
					weapon.Resource == CraftResource.Bronze || 
					weapon.Resource == CraftResource.Gold || 
					weapon.Resource == CraftResource.Agapite || 
					weapon.Resource == CraftResource.Verite || 
					weapon.Resource == CraftResource.Valorite || 
					weapon.Resource == CraftResource.Steel || 
					weapon.Resource == CraftResource.Brass || 
					weapon.Resource == CraftResource.Mithril || 
					weapon.Resource == CraftResource.Xormite || 
					weapon.Resource == CraftResource.Obsidian || 
					weapon.Resource == CraftResource.Nepturite || 
					weapon.Resource == CraftResource.Dwarven || 
					weapon.Resource == CraftResource.SpinedLeather || 
					weapon.Resource == CraftResource.HornedLeather || 
					weapon.Resource == CraftResource.BarbedLeather || 
					weapon.Resource == CraftResource.NecroticLeather || 
					weapon.Resource == CraftResource.VolcanicLeather || 
					weapon.Resource == CraftResource.FrozenLeather || 
					weapon.Resource == CraftResource.GoliathLeather || 
					weapon.Resource == CraftResource.DraconicLeather || 
					weapon.Resource == CraftResource.HellishLeather || 
					weapon.Resource == CraftResource.DinosaurLeather || 
					weapon.Resource == CraftResource.AlienLeather || 
					weapon.Resource == CraftResource.AshTree || 
					weapon.Resource == CraftResource.CherryTree || 
					weapon.Resource == CraftResource.EbonyTree || 
					weapon.Resource == CraftResource.GoldenOakTree || 
					weapon.Resource == CraftResource.HickoryTree || 
					weapon.Resource == CraftResource.MahoganyTree || 
					weapon.Resource == CraftResource.OakTree || 
					weapon.Resource == CraftResource.PineTree || 
					weapon.Resource == CraftResource.RosewoodTree || 
					weapon.Resource == CraftResource.DriftwoodTree || 
					weapon.Resource == CraftResource.WalnutTree || 
					weapon.Resource == CraftResource.ElvenTree || 
					weapon.Resource == CraftResource.GhostTree || 
					weapon.Resource == CraftResource.PetrifiedTree ){ return true; }
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				if ( armor.Resource == CraftResource.DullCopper || 
					armor.Resource == CraftResource.ShadowIron || 
					armor.Resource == CraftResource.Copper || 
					armor.Resource == CraftResource.Bronze || 
					armor.Resource == CraftResource.Gold || 
					armor.Resource == CraftResource.Agapite || 
					armor.Resource == CraftResource.Verite || 
					armor.Resource == CraftResource.Valorite || 
					armor.Resource == CraftResource.Steel || 
					armor.Resource == CraftResource.Brass || 
					armor.Resource == CraftResource.Mithril || 
					armor.Resource == CraftResource.Xormite || 
					armor.Resource == CraftResource.Obsidian || 
					armor.Resource == CraftResource.Nepturite || 
					armor.Resource == CraftResource.Dwarven || 
					armor.Resource == CraftResource.SpinedLeather || 
					armor.Resource == CraftResource.HornedLeather || 
					armor.Resource == CraftResource.BarbedLeather || 
					armor.Resource == CraftResource.NecroticLeather || 
					armor.Resource == CraftResource.VolcanicLeather || 
					armor.Resource == CraftResource.FrozenLeather || 
					armor.Resource == CraftResource.GoliathLeather || 
					armor.Resource == CraftResource.DraconicLeather || 
					armor.Resource == CraftResource.HellishLeather || 
					armor.Resource == CraftResource.DinosaurLeather || 
					armor.Resource == CraftResource.AlienLeather || 
					armor.Resource == CraftResource.AshTree || 
					armor.Resource == CraftResource.CherryTree || 
					armor.Resource == CraftResource.EbonyTree || 
					armor.Resource == CraftResource.GoldenOakTree || 
					armor.Resource == CraftResource.HickoryTree || 
					armor.Resource == CraftResource.MahoganyTree || 
					armor.Resource == CraftResource.OakTree || 
					armor.Resource == CraftResource.PineTree || 
					armor.Resource == CraftResource.RosewoodTree || 
					armor.Resource == CraftResource.DriftwoodTree || 
					armor.Resource == CraftResource.WalnutTree || 
					armor.Resource == CraftResource.ElvenTree || 
					armor.Resource == CraftResource.GhostTree || 
					armor.Resource == CraftResource.PetrifiedTree ){ return true; }
			}
			else if ( item is BaseInstrument )
			{
				BaseInstrument lute = (BaseInstrument)item;

				if ( lute.Resource == CraftResource.DullCopper || 
					lute.Resource == CraftResource.ShadowIron || 
					lute.Resource == CraftResource.Copper || 
					lute.Resource == CraftResource.Bronze || 
					lute.Resource == CraftResource.Gold || 
					lute.Resource == CraftResource.Agapite || 
					lute.Resource == CraftResource.Verite || 
					lute.Resource == CraftResource.Valorite || 
					lute.Resource == CraftResource.Steel || 
					lute.Resource == CraftResource.Brass || 
					lute.Resource == CraftResource.Mithril || 
					lute.Resource == CraftResource.Xormite || 
					lute.Resource == CraftResource.Obsidian || 
					lute.Resource == CraftResource.Nepturite || 
					lute.Resource == CraftResource.Dwarven || 
					lute.Resource == CraftResource.SpinedLeather || 
					lute.Resource == CraftResource.HornedLeather || 
					lute.Resource == CraftResource.BarbedLeather || 
					lute.Resource == CraftResource.NecroticLeather || 
					lute.Resource == CraftResource.VolcanicLeather || 
					lute.Resource == CraftResource.FrozenLeather || 
					lute.Resource == CraftResource.GoliathLeather || 
					lute.Resource == CraftResource.DraconicLeather || 
					lute.Resource == CraftResource.HellishLeather || 
					lute.Resource == CraftResource.DinosaurLeather || 
					lute.Resource == CraftResource.AlienLeather || 
					lute.Resource == CraftResource.AshTree || 
					lute.Resource == CraftResource.CherryTree || 
					lute.Resource == CraftResource.EbonyTree || 
					lute.Resource == CraftResource.GoldenOakTree || 
					lute.Resource == CraftResource.HickoryTree || 
					lute.Resource == CraftResource.MahoganyTree || 
					lute.Resource == CraftResource.OakTree || 
					lute.Resource == CraftResource.PineTree || 
					lute.Resource == CraftResource.RosewoodTree || 
					lute.Resource == CraftResource.DriftwoodTree || 
					lute.Resource == CraftResource.WalnutTree || 
					lute.Resource == CraftResource.ElvenTree || 
					lute.Resource == CraftResource.GhostTree || 
					lute.Resource == CraftResource.PetrifiedTree ){ return true; }
			}

			return false;
		}

		public static bool IsMagicLight( Item item )
		{
			if ( item == null )
				return false;

			if ( item is GoldRing ){ return true; }
			else if ( item is LevelGoldRing ){ return true; }
			else if ( item is GiftGoldRing ){ return true; }
			else if ( item is BaseEquipableLight ){ return true; }

			return false;
		}

		public static bool IsCowlHood( Item item )
		{
			if ( item.ItemID == 0x4D09 || item.ItemID == 0x141B || item.ItemID == 0x141C || item.ItemID == 0x4D01 || item.ItemID == 0x4D02 || item.ItemID == 0x4D03 || item.ItemID == 0x4D04 || item.ItemID == 0x3176 || item.ItemID == 0x3177 || item.ItemID == 0x2B71 || item.ItemID == 0x3168 || item.ItemID == 0x4CDA || item.ItemID == 0x4CDB || item.ItemID == 0x4CDC || item.ItemID == 0x4CDD )
				return true;

			return false;
		}

		public static void IsNoHairHat( int item, Mobile m )
		{
			if ( m.HairItemID > 0 )
			{
				if ( item == 0 && m.FindItemOnLayer( Layer.Helm ) != null ){ item = m.FindItemOnLayer( Layer.Helm ).ItemID; }

				if (item == 0x1543 || // SKULLCAPS
					item == 0x1544 ||
					item == 0x26A3 || // NINJA HOODS
					item == 0x26A4 ||
					item == 0x278E ||
					item == 0x278F ||
					item == 0x27D9 ||
					item == 0x27DA ||
					item == 0x13BB || // CHAIN COIF
					item == 0x13C0 )
				{
					if ( m is BaseCreature ){ m.HairItemID = 0; } // A DUMB FUNCTION TO HELP REMOVE HAIR FROM NPCS BECAUSE IT STICKS OUT OF THESE HATS SOMETIMES
				}
			}
		}

		public static bool IsJewelry( Item item )
		{
			if ( !( item is BaseJewel ) )
				return false;

			if ( item.ItemID == 0x4CEB || item.ItemID == 0x4CEC || item.ItemID == 0x4CED || item.ItemID == 0x4CEE || item.ItemID == 0x4CEF || item.ItemID == 0x4CF0 || item.ItemID == 0x4CF1 || item.ItemID == 0x4CF2 || item.ItemID == 0x4CF3 || item.ItemID == 0x4CF4 || item.ItemID == 0x4CF5 || item.ItemID == 0x4CF6 || item.ItemID == 0x4CF7 || item.ItemID == 0x4CF8 || item.ItemID == 0x4CF9 || item.ItemID == 0x4CFA || item.ItemID == 0x4CFC || item.ItemID == 0x4CFD || item.ItemID == 0x4CFD || item.ItemID == 0x4CFE || item.ItemID == 0x4CFF || item.ItemID == 0x4D00 || item.ItemID == 0x5650 || item.ItemID == 0x1085 || item.ItemID == 0x1086 || item.ItemID == 0x1087 || item.ItemID == 0x1088 || item.ItemID == 0x1089 || item.ItemID == 0x108A || item.ItemID == 0x1F05 || item.ItemID == 0x1F06 || item.ItemID == 0x1F07 || item.ItemID == 0x1F08 || item.ItemID == 0x1F09 || item.ItemID == 0x1F0A )
				return true;

			return false;
		}

		public static bool IsJewelryRing( Item item )
		{
			if ( !( item is BaseJewel ) )
				return false;

			if ( item.ItemID == 0x4CF3 || item.ItemID == 0x4CF4 || item.ItemID == 0x4CF5 || item.ItemID == 0x4CF6 || item.ItemID == 0x4CF7 || item.ItemID == 0x4CF8 || item.ItemID == 0x4CF9 || item.ItemID == 0x4CFA )
				return true;

			return false;
		}

		public static bool IsJewelryAmulet( Item item )
		{
			if ( !( item is BaseJewel ) )
				return false;

			if ( item.ItemID == 0x4CFF || item.ItemID == 0x4D00 || item.ItemID == 0x5650 )
				return true;

			return false;
		}

		public static bool IsJewelryBracelet( Item item )
		{
			if ( !( item is BaseJewel ) )
				return false;

			if ( item.ItemID == 0x4CEB || item.ItemID == 0x4CEC || item.ItemID == 0x4CED || item.ItemID == 0x4CEE || item.ItemID == 0x4CEF || item.ItemID == 0x4CF0 || item.ItemID == 0x4CF1 || item.ItemID == 0x4CF2 )
				return true;

			return false;
		}

		public static bool IsJewelryEarrings( Item item )
		{
			if ( !( item is BaseJewel ) )
				return false;

			if ( item.ItemID == 0x4CFC || item.ItemID == 0x4CFD )
				return true;

			return false;
		}

		public static void UpgradeJewelry( Item item )
		{
			if ( item is BaseJewel )
			{
				if ( item.ItemID == 0x1085 ){ item.ItemID = 0x4CFE; if ( item.Name == null ){ item.Name = "beads"; } }
				else if ( item.ItemID == 0x1086 ){ item.ItemID = 0x4CF1; if ( item.Name == null ){ item.Name = "bracelet"; } }
				else if ( item.ItemID == 0x1087 ){ item.ItemID = 0x4CFB; if ( item.Name == null ){ item.Name = "earrings"; } }
				else if ( item.ItemID == 0x1088 ){ item.ItemID = 0x4CFF; if ( item.Name == null ){ item.Name = "amulet"; } }
				else if ( item.ItemID == 0x1089 ){ item.ItemID = 0x4CFD; if ( item.Name == null ){ item.Name = "beads"; } }
				else if ( item.ItemID == 0x108A ){ item.ItemID = 0x4CFA; if ( item.Name == null ){ item.Name = "ring"; } }
				else if ( item.ItemID == 0x1F05 ){ item.ItemID = 0x4CFE; if ( item.Name == null ){ item.Name = "beads"; } }
				else if ( item.ItemID == 0x1F06 ){ item.ItemID = 0x4CF2; if ( item.Name == null ){ item.Name = "bracelet"; } }
				else if ( item.ItemID == 0x1F07 ){ item.ItemID = 0x4CFC; if ( item.Name == null ){ item.Name = "earrings"; } }
				else if ( item.ItemID == 0x1F08 ){ item.ItemID = 0x4D00; if ( item.Name == null ){ item.Name = "amulet"; } }
				else if ( item.ItemID == 0x1F09 ){ item.ItemID = 0x4CF4; if ( item.Name == null ){ item.Name = "ring"; } }
				else if ( item.ItemID == 0x1F0A ){ item.ItemID = 0x4CFE; if ( item.Name == null ){ item.Name = "beads"; } }
			}
		}

		public static bool IsMagicTalisman( Item item )
		{
			if ( item == null )
				return false;

			if ( item is MagicTalisman ){ return true; }
			else if ( item is GiftTalismanLeather ){ return true; }
			else if ( item is GiftTalismanSnake ){ return true; }
			else if ( item is GiftTalismanTotem ){ return true; }
			else if ( item is GiftTalismanHoly ){ return true; }
			else if ( item is LevelTalismanLeather ){ return true; }
			else if ( item is LevelTalismanSnake ){ return true; }
			else if ( item is LevelTalismanTotem ){ return true; }
			else if ( item is LevelTalismanHoly ){ return true; }

			return false;
		}

		public static bool IsMagicTorch( Item item )
		{
			if ( item == null )
				return false;

			if ( item is MagicTorch ){ return true; }
			else if ( item is LevelTorch ){ return true; }
			else if ( item is GiftTorch ){ return true; }

			return false;
		}

		public static bool IsMagicCandle( Item item )
		{
			if ( item == null )
				return false;

			if ( item is MagicCandle ){ return true; }
			else if ( item is LevelCandle ){ return true; }
			else if ( item is GiftCandle ){ return true; }

			return false;
		}

		public static bool IsMagicLantern( Item item )
		{
			if ( item == null )
				return false;

			if ( item is MagicLantern ){ return true; }
			else if ( item is GiftLantern ){ return true; }
			else if ( item is LevelLantern ){ return true; }

			return false;
		}

		public static bool IsMagicSash( Item item )
		{
			if ( item == null )
				return false;

			if ( item is MagicSash ){ return true; }
			else if ( item is LevelSash ){ return true; }
			else if ( item is GiftSash ){ return true; }

			return false;
		}

		public static bool IsMagicBelt( Item item )
		{
			if ( item == null )
				return false;

			if ( item is MagicBelt ){ return true; }
			else if ( item is LevelBelt ){ return true; }
			else if ( item is GiftBelt ){ return true; }

			return false;
		}

		public static void TransmuteNormal( Item item )
		{
			if ( ( IsStrangeMetalItem( item ) || IsStrangeWoodItem( item ) || IsStrangeClothItem( item ) ) && item.Name != null )
			{
				if ( (item.Name).Contains("Ice") ){ item.Name = (item.Name).Replace("Ice ", ""); }
				else if ( (item.Name).Contains("Garnet") ){ item.Name = (item.Name).Replace("Garnet ", ""); }
				else if ( (item.Name).Contains("Emerald") ){ item.Name = (item.Name).Replace("Emerald ", ""); }
				else if ( (item.Name).Contains("Caddellite") ){ item.Name = (item.Name).Replace("Caddellite ", ""); }
				else if ( (item.Name).Contains("Amethyst") ){ item.Name = (item.Name).Replace("Amethyst ", ""); }
				else if ( (item.Name).Contains("Topaz") ){ item.Name = (item.Name).Replace("Topaz ", ""); }
				else if ( (item.Name).Contains("Star Ruby") ){ item.Name = (item.Name).Replace("Star Ruby ", ""); }
				else if ( (item.Name).Contains("Spinel") ){ item.Name = (item.Name).Replace("Spinel ", ""); }
				else if ( (item.Name).Contains("Silver") ){ item.Name = (item.Name).Replace("Silver ", ""); }
				else if ( (item.Name).Contains("Quartz") ){ item.Name = (item.Name).Replace("Quartz ", ""); }
				else if ( (item.Name).Contains("Onyx") ){ item.Name = (item.Name).Replace("Onyx ", ""); }
				else if ( (item.Name).Contains("Marble") ){ item.Name = (item.Name).Replace("Marble ", ""); }
				else if ( (item.Name).Contains("Jade") ){ item.Name = (item.Name).Replace("Jade ", ""); }
				else if ( (item.Name).Contains("Ruby") ){ item.Name = (item.Name).Replace("Ruby ", ""); }
				else if ( (item.Name).Contains("Beskar") ){ item.Name = (item.Name).Replace("Beskar ", ""); }
				else if ( (item.Name).Contains("Carbonite") ){ item.Name = (item.Name).Replace("Carbonite ", ""); }
				else if ( (item.Name).Contains("Phrik") ){ item.Name = (item.Name).Replace("Phrik ", ""); }
				else if ( (item.Name).Contains("Cortosis") ){ item.Name = (item.Name).Replace("Cortosis ", ""); }
				else if ( (item.Name).Contains("Songsteel") ){ item.Name = (item.Name).Replace("Songsteel ", ""); }
				else if ( (item.Name).Contains("Agrinium") ){ item.Name = (item.Name).Replace("Agrinium ", ""); }
				else if ( (item.Name).Contains("Durasteel") ){ item.Name = (item.Name).Replace("Durasteel ", ""); }
				else if ( (item.Name).Contains("Titanium") ){ item.Name = (item.Name).Replace("Titanium ", ""); }
				else if ( (item.Name).Contains("Laminasteel") ){ item.Name = (item.Name).Replace("Laminasteel ", ""); }
				else if ( (item.Name).Contains("Neuranium") ){ item.Name = (item.Name).Replace("Neuranium ", ""); }
				else if ( (item.Name).Contains("Promethium") ){ item.Name = (item.Name).Replace("Promethium ", ""); }
				else if ( (item.Name).Contains("Quadranium") ){ item.Name = (item.Name).Replace("Quadranium ", ""); }
				else if ( (item.Name).Contains("Durite") ){ item.Name = (item.Name).Replace("Durite ", ""); }
				else if ( (item.Name).Contains("Farium") ){ item.Name = (item.Name).Replace("Farium ", ""); }
				else if ( (item.Name).Contains("Trimantium") ){ item.Name = (item.Name).Replace("Trimantium ", ""); }
				else if ( (item.Name).Contains("Xonolite") ){ item.Name = (item.Name).Replace("Xonolite ", ""); }
				else if ( (item.Name).Contains("Veshok") ){ item.Name = (item.Name).Replace("Veshok ", ""); }
				else if ( (item.Name).Contains("Cosian") ){ item.Name = (item.Name).Replace("Cosian ", ""); }
				else if ( (item.Name).Contains("Greel") ){ item.Name = (item.Name).Replace("Greel ", ""); }
				else if ( (item.Name).Contains("Teej") ){ item.Name = (item.Name).Replace("Teej ", ""); }
				else if ( (item.Name).Contains("Kyshyyyk") ){ item.Name = (item.Name).Replace("Kyshyyyk ", ""); }
				else if ( (item.Name).Contains("Laroon") ){ item.Name = (item.Name).Replace("Laroon ", ""); }
				else if ( (item.Name).Contains("Borl") ){ item.Name = (item.Name).Replace("Borl ", ""); }
				else if ( (item.Name).Contains("Japor") ){ item.Name = (item.Name).Replace("Japor ", ""); }
				else if ( (item.Name).Contains("Adesote") ){ item.Name = (item.Name).Replace("Adesote ", ""); }
				else if ( (item.Name).Contains("Nylonite") ){ item.Name = (item.Name).Replace("Nylonite ", ""); }
				else if ( (item.Name).Contains("Biomesh") ){ item.Name = (item.Name).Replace("Biomesh ", ""); }
				else if ( (item.Name).Contains("Cerlin") ){ item.Name = (item.Name).Replace("Cerlin ", ""); }
				else if ( (item.Name).Contains("Polyfiber") ){ item.Name = (item.Name).Replace("Polyfiber ", ""); }
				else if ( (item.Name).Contains("Durafiber") ){ item.Name = (item.Name).Replace("Durafiber ", ""); }
				else if ( (item.Name).Contains("Syncloth") ){ item.Name = (item.Name).Replace("Syncloth ", ""); }
				else if ( (item.Name).Contains("Hypercloth") ){ item.Name = (item.Name).Replace("Hypercloth ", ""); }
				else if ( (item.Name).Contains("Flexicris") ){ item.Name = (item.Name).Replace("Flexicris ", ""); }
				else if ( (item.Name).Contains("Thermoweave") ){ item.Name = (item.Name).Replace("Thermoweave ", ""); }
				else if ( (item.Name).Contains("Nylar") ){ item.Name = (item.Name).Replace("Nylar ", ""); }
				else if ( (item.Name).Contains("Twi'lek") ){ item.Name = (item.Name).Replace("Twi'lek ", ""); }
				else if ( (item.Name).Contains("Rodian") ){ item.Name = (item.Name).Replace("Rodian ", ""); }
				else if ( (item.Name).Contains("Martian") ){ item.Name = (item.Name).Replace("Martian ", ""); }
				else if ( (item.Name).Contains("Cardassian") ){ item.Name = (item.Name).Replace("Cardassian ", ""); }
				else if ( (item.Name).Contains("Xindi") ){ item.Name = (item.Name).Replace("Xindi ", ""); }
				else if ( (item.Name).Contains("Tusken") ){ item.Name = (item.Name).Replace("Tusken ", ""); }
				else if ( (item.Name).Contains("Andorian") ){ item.Name = (item.Name).Replace("Andorian ", ""); }
				else if ( (item.Name).Contains("Zabrak") ){ item.Name = (item.Name).Replace("Zabrak ", ""); }
			}
		}

		public static void ColorMetal( Item item, int color )
		{
			if ( color < 1 ){ color = Utility.RandomMinMax( 0, 37 ); }

			switch ( color ) 
			{
				case 1: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ); item.Name = "star ruby " + item.Name;		break;
				case 2: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ); item.Name = "spinel " + item.Name;			break;
				case 3: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ); item.Name = "silver " + item.Name;			break;
				case 4: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ); item.Name = "sapphire " + item.Name;		break;
				case 5: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ); item.Name = "ruby " + item.Name;			break;
				case 6: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ); item.Name = "quartz " + item.Name;			break;
				case 7: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ); item.Name = "onyx " + item.Name;			break;
				case 8: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ); item.Name = "jade " + item.Name;			break;
				case 9: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ); item.Name = "garnet " + item.Name;			break;
				case 10: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ); item.Name = "emerald " + item.Name;		break;
				case 11: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ); item.Name = "amethyst " + item.Name;		break;
				case 12: item.Hue = 0x47E; item.Name = "pearl " + item.Name;		break;
				case 13: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); item.Name = "obsidian " + item.Name;		break;
				case 14: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); item.Name = "dull copper " + item.Name;	break;
				case 15: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); item.Name = "shadow iron " + item.Name;	break;
				case 16: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "copper", "", 0 ); item.Name = "copper " + item.Name;		break;
				case 17: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "bronze", "", 0 ); item.Name = "bronze " + item.Name;		break;
				case 18: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "gold", "", 0 ); item.Name = "gold " + item.Name;			break;
				case 19: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "agapite", "", 0 ); item.Name = "agapite " + item.Name;		break;
				case 20: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "verite", "", 0 ); item.Name = "verite " + item.Name;		break;
				case 21: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "valorite", "", 0 ); item.Name = "valorite " + item.Name;		break;
				case 22: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "steel", "", 0 ); item.Name = "steel " + item.Name;		break;
				case 23: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "brass", "", 0 ); item.Name = "brass " + item.Name;		break;
				case 24: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); item.Name = "nepturite " + item.Name;	break;
				case 25: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); item.Name = "shadow " + item.Name;		break;
				case 26: item.Hue = 0x486; item.Name = "violet " + item.Name;		break;
				case 27: item.Hue = 0x5B6; item.Name = "azurite " + item.Name;		break;
				case 28: item.Hue = 0x495; item.Name = "turquoise " + item.Name;	break;
				case 29: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "mithril", "", 0 ); item.Name = "mithril " + item.Name;		break;
				case 30: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 ); item.Name = "caddellite " + item.Name;	break;
				case 31: item.Hue = 0x71B; item.Name = "wooden " + item.Name;		break;
				case 32: item.Hue = 0xB92; item.Name = "bone " + item.Name;			break;
				case 33: item.Hue = 0xA61; item.Name = "diamond " + item.Name;		break;
				case 34: item.Hue = 0x54F; item.Name = "amber " + item.Name;		break;
				case 35: item.Hue = 0x550; item.Name = "tourmaline " + item.Name;	break;
				case 36: item.Hue = 0x4F2; item.Name = "star sapphire " + item.Name;break;
				case 37: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ); item.Name = "topaz " + item.Name;			break;
			}
		}

		public static void ColorPlainMetal( Item item )
		{
			switch ( Utility.RandomMinMax( 0, 13 ) ) 
			{
				case 0: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ); item.Name = "silver " + item.Name;			break;
				case 1: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); item.Name = "dull copper " + item.Name;	break;
				case 2: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); item.Name = "shadow iron " + item.Name;	break;
				case 3: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "copper", "", 0 ); item.Name = "copper " + item.Name;		break;
				case 4: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "bronze", "", 0 ); item.Name = "bronze " + item.Name;		break;
				case 5: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "gold", "", 0 ); item.Name = "gold " + item.Name;			break;
				case 6: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "agapite", "", 0 ); item.Name = "agapite " + item.Name;		break;
				case 7: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "verite", "", 0 ); item.Name = "verite " + item.Name;		break;
				case 8: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "valorite", "", 0 ); item.Name = "valorite " + item.Name;		break;
				case 9: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "steel", "", 0 ); item.Name = "steel " + item.Name;			break;
				case 10: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "brass", "", 0 ); item.Name = "brass " + item.Name;		break;
				case 11: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); item.Name = "nepturite " + item.Name;	break;
				case 12: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "mithril", "", 0 ); item.Name = "mithril " + item.Name;		break;
				case 13: item.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 ); item.Name = "caddellite " + item.Name;	break;
			}
		}

		public static bool IsPotion( Item item )
		{
			if ( item == null )
				return false;

			if ( 
					item is BasePotion || 
					item is AutoResPotion || 
					item is ShieldOfEarthPotion || 
					item is WoodlandProtectionPotion || 
					item is ProtectiveFairyPotion || 
					item is HerbalHealingPotion || 
					item is GraspingRootsPotion || 
					item is BlendWithForestPotion || 
					item is SwarmOfInsectsPotion || 
					item is VolcanicEruptionPotion || 
					item is TreefellowPotion || 
					item is StoneCirclePotion || 
					item is DruidicRunePotion || 
					item is LureStonePotion || 
					item is NaturesPassagePotion || 
					item is MushroomGatewayPotion || 
					item is RestorativeSoilPotion || 
					item is FireflyPotion || 
					item is HellsGateScroll || 
					item is ManaLeechScroll || 
					item is NecroCurePoisonScroll || 
					item is NecroPoisonScroll || 
					item is NecroUnlockScroll || 
					item is PhantasmScroll || 
					item is RetchedAirScroll || 
					item is SpectreShadowScroll || 
					item is UndeadEyesScroll || 
					item is VampireGiftScroll || 
					item is WallOfSpikesScroll || 
					item is BloodPactScroll || 
					item is GhostlyImagesScroll || 
					item is GhostPhaseScroll || 
					item is GraveyardGatewayScroll || 
					item is HellsBrandScroll || 
					item is MagicalDyes || 
					item is UnusualDyes || 
					item is BottleOfAcid || 
					item is CrystallineJar || 
					item is NecroSkinPotion || 
					item is OilWood || 
					item is OilAmethyst || 
					item is OilCaddellite || 
					item is OilEmerald || 
					item is OilGarnet || 
					item is OilIce || 
					item is OilJade || 
					item is OilLeather || 
					item is OilMarble || 
					item is OilMetal || 
					item is OilOnyx || 
					item is OilQuartz || 
					item is OilRuby || 
					item is OilSapphire || 
					item is OilSilver || 
					item is OilSpinel || 
					item is OilStarRuby || 
					item is OilTopaz || 
					item is OilWood || 
					item is BottleOfParts || 
					item is PotionOfWisdom || 
					item is PotionOfDexterity || 
					item is PotionOfMight 
			)
			return true;

			return false;
		}

		public static bool IsBodyPart( Item item )
		{
			if ( item == null )
				return false;

			if ( 
					item is LeftLeg || 
					item is RightLeg || 
					item is TastyHeart || 
					item is BodyPart || 
					item is Head || 
					item is LeftArm || 
					item is RightArm || 
					item is Torso || 
					item is Bone || 
					item is RibCage || 
					item is BonePile || 
					item is Bones 
			)
			return true;

			return false;
		}

		public static bool IsReagent( Item item )
		{
			if ( item == null )
				return false;

			if ( 
					item is BlackPearl || 
					item is Bloodmoss || 
					item is Garlic || 
					item is Ginseng || 
					item is MandrakeRoot || 
					item is Nightshade || 
					item is SpidersSilk || 
					item is SulfurousAsh || 
					item is BatWing || 
					item is GraveDust || 
					item is DaemonBlood || 
					item is PigIron || 
					item is NoxCrystal || 
					item is SilverSerpentVenom || 
					item is DragonBlood || 
					item is EnchantedSeaweed || 
					item is DragonTooth || 
					item is GoldenSerpentVenom || 
					item is LichDust || 
					item is DemonClaw || 
					item is PegasusFeather || 
					item is PhoenixFeather || 
					item is UnicornHorn || 
					item is DemigodBlood || 
					item is GhostlyDust || 
					item is BottleOfParts || 
					item is EyeOfToad || 
					item is FairyEgg || 
					item is GargoyleEar || 
					item is BeetleShell || 
					item is MoonCrystal || 
					item is PixieSkull || 
					item is RedLotus || 
					item is SeaSalt || 
					item is SilverWidow || 
					item is SwampBerries || 
					item is Brimstone || 
					item is ButterflyWings || 
					item is PlantHerbalism_Leaf || 
					item is PlantHerbalism_Flower || 
					item is PlantHerbalism_Mushroom || 
					item is PlantHerbalism_Lilly || 
					item is PlantHerbalism_Cactus || 
					item is PlantHerbalism_Grass || 
					item is reagents_magic_jar1 || 
					item is reagents_magic_jar2 || 
					item is reagents_magic_jar3 
			)
			return true;

			return false;
		}

		public static int GetSpaceAceColors( string item )
		{
			int color = 0;

			if ( item == "Beskar" ){ color = 0x6F8; }
			else if ( item == "Carbonite" ){ color = 0x829; }
			else if ( item == "Phrik" ){ color = 0xAF8; }
			else if ( item == "Cortosis" ){ color = 0x82C; }
			else if ( item == "Songsteel" ){ color = 0xB42; }
			else if ( item == "Agrinium" ){ color = 0x8C1; }
			else if ( item == "Durasteel" ){ color = 0x7A9; }
			else if ( item == "Titanium" ){ color = 0x8D7; }
			else if ( item == "Laminasteel" ){ color = 0x77F; }
			else if ( item == "Neuranium" ){ color = 0x870; }
			else if ( item == "Promethium" ){ color = 0x6F6; }
			else if ( item == "Quadranium" ){ color = 0x705; }
			else if ( item == "Durite" ){ color = 0x877; }
			else if ( item == "Farium" ){ color = 0x776; }
			else if ( item == "Trimantium" ){ color = 0x825; }
			else if ( item == "Xonolite" ){ color = 0x701; }

			else if ( item == "Adesote" ){ color = 0xAF8; }
			else if ( item == "Nylonite" ){ color = 0x6F8; }
			else if ( item == "Biomesh" ){ color = 0x829; }
			else if ( item == "Cerlin" ){ color = 0x8D7; }
			else if ( item == "Polyfiber" ){ color = 0x6F6; }
			else if ( item == "Durafiber" ){ color = 0x8C1; }
			else if ( item == "Syncloth" ){ color = 0x7A9; }
			else if ( item == "Hypercloth" ){ color = 0x77F; }
			else if ( item == "Flexicris" ){ color = 0x705; }
			else if ( item == "Thermoweave" ){ color = 0x776; }
			else if ( item == "Nylar" ){ color = 0x701; }

			else if ( item == "Veshok" ){ color = 0x6F8; }
			else if ( item == "Cosian" ){ color = 0x77F; }
			else if ( item == "Greel" ){ color = 0x870; }
			else if ( item == "Teej" ){ color = 0x6F6; }
			else if ( item == "Kyshyyyk" ){ color = 0x705; }
			else if ( item == "Laroon" ){ color = 0x877; }
			else if ( item == "Borl" ){ color = 0x776; }
			else if ( item == "Japor" ){ color = 0x825; }

			else if ( item == "Twi'lek" ){ color = 0xAF8; }
			else if ( item == "Rodian" ){ color = 0x77F; }
			else if ( item == "Martian" ){ color = 0x6F6; }
			else if ( item == "Cardassian" ){ color = 0x705; }
			else if ( item == "Xindi" ){ color = 0x877; }
			else if ( item == "Tusken" ){ color = 0x776; }
			else if ( item == "Andorian" ){ color = 0x825; }
			else if ( item == "Zabrak" ){ color = 0x701; }

			return color;
		}

		public static bool ShinyArmor() // DO YOU WANT SHINY METAL ARMOR
		{
			return true; // IF YOU CHANGE THIS, DELETE THE INFO/colors.set FILE AND RESTART THE SERVER, IT WILL TAKE A BIT TO LOAD WHILE IT UPDATES COLORS
		}

		public static bool LeatherColor() // DO YOU WANT NEWER LEATHER COLORS
		{
			return true; // IF YOU CHANGE THIS, DELETE THE INFO/colors.set FILE AND RESTART THE SERVER, IT WILL TAKE A BIT TO LOAD WHILE IT UPDATES COLORS
		}

		public static int GetMaterialColor( string color, string style, int based )
		{
			int hue = based;
			string leather = style;

			if ( style == "alter" )
			{
				if ( based == 99998 )
				{
					leather = "classic";
					style = "classic";
					hue = 0;
				}
				else
				{
					leather = "newer";
					style = "newer";
					hue = 0;
				}
			}
			else
			{
				if ( style == "" && !ShinyArmor() ){ style = "classic"; }
				if ( !LeatherColor() ){ leather = "classic"; }
			}

			if ( style == "classic" )
			{
				if ( color == "agapite" ){ hue = 0x979; }
				else if ( color == "brass" ){ hue = 0xA5D; }
				else if ( color == "bronze" ){ hue = 0x972; }
				else if ( color == "copper" ){ hue = 0x96D; }
				else if ( color == "dull copper" ){ hue = 0x973; }
				else if ( color == "dwarven" ){ hue = 0xA1D; }
				else if ( color == "gold" ){ hue = 0x8A5; }
				else if ( color == "mithril" ){ hue = 0x9C2; }
				else if ( color == "nepturite" ){ hue = 0x58E; }
				else if ( color == "obsidian" ){ hue = 0x497; }
				else if ( color == "shadow iron" ){ hue = 0x966; }
				else if ( color == "steel" ){ hue = 0x515; }
				else if ( color == "valorite" ){ hue = 0x8AB; }
				else if ( color == "verite" ){ hue = 0x89F; }
				else if ( color == "xormite" ){ hue = 0xB96; }
				else if ( color == "jade" ){ hue = 0xB94; }
				else if ( color == "marble" ){ hue = 0xB92; }
				else if ( color == "onyx" ){ hue = 0xB63; }
				else if ( color == "quartz" ){ hue = 0x4AC; }
				else if ( color == "ruby" ){ hue = 0x845; }
				else if ( color == "sapphire" ){ hue = 0x84C; }
				else if ( color == "spinel" ){ hue = 0x48B; }
				else if ( color == "star ruby" ){ hue = 0x48E; }
				else if ( color == "topaz" ){ hue = 0x488; }
				else if ( color == "amethyst" ){ hue = 0x492; }
				else if ( color == "caddellite" ){ hue = 0x4AB; }
				else if ( color == "emerald" ){ hue = 0x5B4; }
				else if ( color == "garnet" ){ hue = 0x48F; }
				else if ( color == "ice" ){ hue = 0x480; }
				else if ( color == "silver" ){ hue = 0x9C4; }
			}
			else if ( style == "monster" )
			{
				if ( color == "agapite" ){ hue = 0x957; }
				else if ( color == "brass" ){ hue = 0x95B; }
				else if ( color == "bronze" ){ hue = 0x958; }
				else if ( color == "copper" ){ hue = 0x959; }
				else if ( color == "dull copper" ){ hue = 0x952; }
				else if ( color == "dwarven" ){ hue = 0x6FC; }
				else if ( color == "gold" ){ hue = 0x941; }
				else if ( color == "mithril" ){ hue = 0xB74; }
				else if ( color == "nepturite" ){ hue = 0x945; }
				else if ( color == "obsidian" ){ hue = 0x8B9; }
				else if ( color == "shadow iron" ){ hue = 0x95C; }
				else if ( color == "steel" ){ hue = 0x99F; }
				else if ( color == "valorite" ){ hue = 0x95D; }
				else if ( color == "verite" ){ hue = 0x95E; }
				else if ( color == "xormite" ){ hue = 0x7C7; }
				else if ( color == "jade" ){ hue = 0xB94; }
				else if ( color == "marble" ){ hue = 0xB3B; }
				else if ( color == "onyx" ){ hue = 0xB5E; }
				else if ( color == "quartz" ){ hue = 0x869; }
				else if ( color == "ruby" ){ hue = 0x982; }
				else if ( color == "sapphire" ){ hue = 0x996; }
				else if ( color == "spinel" ){ hue = 0x94D; }
				else if ( color == "star ruby" ){ hue = 0x7CA; }
				else if ( color == "topaz" ){ hue = 0x883; }
				else if ( color == "amethyst" ){ hue = 0x8D5; }
				else if ( color == "caddellite" ){ hue = 0x99D; }
				else if ( color == "emerald" ){ hue = 0x950; }
				else if ( color == "garnet" ){ hue = 0x943; }
				else if ( color == "ice" ){ hue = 0xAF3; }
				else if ( color == "silver" ){ hue = 0xB2A; }
			}
			else
			{
				if ( color == "agapite" ){ hue = 0xAEA; }
				else if ( color == "brass" ){ hue = 0x993; }
				else if ( color == "bronze" ){ hue = 0x8BC; }
				else if ( color == "copper" ){ hue = 0xB18; }
				else if ( color == "dull copper" ){ hue = 0xAB5; }
				else if ( color == "dwarven" ){ hue = 0x6FC; }
				else if ( color == "gold" ){ hue = 0x99A; }
				else if ( color == "mithril" ){ hue = 0xB70; }
				else if ( color == "nepturite" ){ hue = 0x948; }
				else if ( color == "obsidian" ){ hue = 0x7C2; }
				else if ( color == "shadow iron" ){ hue = 0xAB3; }
				else if ( color == "steel" ){ hue = 0x99F; }
				else if ( color == "valorite" ){ hue = 0x95D; }
				else if ( color == "verite" ){ hue = 0x85D; }
				else if ( color == "xormite" ){ hue = 0x7C7; }
				else if ( color == "jade" ){ hue = 0xB0C; }
				else if ( color == "marble" ){ hue = 0xB3B; }
				else if ( color == "onyx" ){ hue = 0xB5E; }
				else if ( color == "quartz" ){ hue = 0x869; }
				else if ( color == "ruby" ){ hue = 0x982; }
				else if ( color == "sapphire" ){ hue = 0x5CE; }
				else if ( color == "spinel" ){ hue = 0x7CB; }
				else if ( color == "star ruby" ){ hue = 0x7CA; }
				else if ( color == "topaz" ){ hue = 0x883; }
				else if ( color == "amethyst" ){ hue = 0x8D5; }
				else if ( color == "caddellite" ){ hue = 0x99D; }
				else if ( color == "emerald" ){ hue = 0x950; }
				else if ( color == "garnet" ){ hue = 0x8C4; }
				else if ( color == "ice" ){ hue = 0x8E2; }
				else if ( color == "silver" ){ hue = 0x911; }
			}

			if ( leather == "classic" )
			{
				if ( color == "frozen" ){ hue = 0x47E; }
				else if ( color == "volcanic" ){ hue = 0x4EB; }
				else if ( color == "dinosaur" ){ hue = 0x430; }
				else if ( color == "serpent" ){ hue = 0x7D1; }
				else if ( color == "lizard" ){ hue = 0x586; }
				else if ( color == "deep sea" ){ hue = 0x555; }
				else if ( color == "draconic" ){ hue = 0x846; }
				else if ( color == "hellish" ){ hue = 0x5B5; }
				else if ( color == "goliath" ){ hue = 0x6DF; }
				else if ( color == "alien" ){ hue = 0xB93; }
				else if ( color == "necrotic" ){ hue = 0xB97; }
			}
			else
			{
				if ( color == "frozen" ){ hue = 0xB5B; }
				else if ( color == "volcanic" ){ hue = 0xB39; }
				else if ( color == "dinosaur" ){ hue = 0x91D; }
				else if ( color == "serpent" ){ hue = 0xB19; }
				else if ( color == "lizard" ){ hue = 0xAB0; }
				else if ( color == "deep sea" ){ hue = 0xABB; }
				else if ( color == "draconic" ){ hue = 0xAB4; }
				else if ( color == "hellish" ){ hue = 0xAFA; }
				else if ( color == "goliath" ){ hue = 0x86A; }
				else if ( color == "alien" ){ hue = 0x8FC; }
				else if ( color == "necrotic" ){ hue = 0x7B0; }
			}

			if ( color == "ash" ){ hue = 0x4A7; }
			else if ( color == "cherry" ){ hue = 0x747; }
			else if ( color == "ebony" ){ hue = 0x96C; }
			else if ( color == "golden oak" ){ hue = 0x7DA; }
			else if ( color == "hickory" ){ hue = 0x415; }
			else if ( color == "mahogany" ){ hue = 0x908; }
			else if ( color == "oak" ){ hue = 0x712; }
			else if ( color == "pine" ){ hue = 0x1CD; }
			else if ( color == "ghostwood" ){ hue = 0x9C2; }
			else if ( color == "rosewood" ){ hue = 0x843; }
			else if ( color == "walnut" ){ hue = 0x750; }
			else if ( color == "petrified" ){ hue = 0xA94; }
			else if ( color == "driftwood" ){ hue = 0x973; }
			else if ( color == "elven" ){ hue = 0xA3A; }

			else if ( color == "icy skin" ){ hue = 0xB7A; }
			else if ( color == "lava skin" ){ hue = 0xB17; }
			else if ( color == "seaweed" ){ hue = 0x98D; }
			else if ( color == "demon skin" ){ hue = 0xB1E; }
			else if ( color == "dragon skin" ){ hue = 0x960; }
			else if ( color == "nightmare skin" ){ hue = 0xB80; }
			else if ( color == "serpent skin" ){ hue = 0xB79; }
			else if ( color == "troll skin" ){ hue = 0xB4C; }
			else if ( color == "unicorn skin" ){ hue = 0xBB4; }
			else if ( color == "dead skin" ){ hue = 0xB4A; }

			if ( color != "" && hue == 0 ){ Console.WriteLine( "Defined Color Mismatch For Search: " + color + "!" ); }

			return hue;
		}

		public static void ConvertMaterial( Item item )
		{
			if ( LeatherColor() ) // NEWER COLORS
			{
				if ( item.Hue == GetMaterialColor( "frozen", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "frozen", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "volcanic", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "volcanic", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "dinosaur", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "dinosaur", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "serpent", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "serpent", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "lizard", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "lizard", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "deep sea", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "deep sea", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "draconic", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "draconic", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "hellish", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "hellish", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "goliath", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "goliath", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "alien", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "alien", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "necrotic", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "necrotic", "alter", 99999 ); }
			}
			else if ( !LeatherColor() ) // CLASSIC COLORS
			{
				if ( item.Hue == GetMaterialColor( "frozen", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "frozen", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "volcanic", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "volcanic", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "dinosaur", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "dinosaur", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "serpent", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "serpent", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "lizard", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "lizard", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "deep sea", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "deep sea", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "draconic", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "draconic", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "hellish", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "hellish", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "goliath", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "goliath", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "alien", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "alien", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "necrotic", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "necrotic", "alter", 99998 ); }
			}

			if ( ShinyArmor() && !(item is BaseGranite) && !(item is Container) ) // NEWER COLORS
			{
				if ( item.Hue == GetMaterialColor( "agapite", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "agapite", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "brass", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "brass", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "bronze", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "bronze", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "copper", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "copper", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "dull copper", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "dull copper", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "dwarven", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "dwarven", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "gold", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "gold", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "mithril", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "mithril", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "nepturite", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "nepturite", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "obsidian", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "obsidian", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "shadow iron", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "shadow iron", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "steel", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "steel", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "valorite", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "valorite", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "verite", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "verite", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "xormite", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "xormite", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "jade", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "jade", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "marble", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "marble", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "onyx", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "onyx", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "quartz", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "quartz", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "ruby", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "ruby", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "sapphire", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "sapphire", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "spinel", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "spinel", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "star ruby", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "star ruby", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "topaz", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "topaz", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "amethyst", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "amethyst", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "caddellite", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "caddellite", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "emerald", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "emerald", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "garnet", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "garnet", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "ice", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "ice", "alter", 99999 ); }
				else if ( item.Hue == GetMaterialColor( "silver", "alter", 99998 ) ){ item.Hue = GetMaterialColor( "silver", "alter", 99999 ); }
			}
			else if ( !ShinyArmor() || item is BaseGranite || item is Container ) // CLASSIC COLORS
			{
				if ( item.Hue == GetMaterialColor( "agapite", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "agapite", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "brass", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "brass", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "bronze", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "bronze", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "copper", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "copper", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "dull copper", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "dull copper", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "dwarven", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "dwarven", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "gold", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "gold", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "mithril", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "mithril", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "nepturite", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "nepturite", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "obsidian", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "obsidian", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "shadow iron", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "shadow iron", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "steel", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "steel", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "valorite", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "valorite", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "verite", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "verite", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "xormite", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "xormite", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "jade", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "jade", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "marble", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "marble", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "onyx", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "onyx", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "quartz", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "quartz", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "ruby", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "ruby", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "sapphire", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "sapphire", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "spinel", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "spinel", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "star ruby", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "star ruby", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "topaz", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "topaz", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "amethyst", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "amethyst", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "caddellite", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "caddellite", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "emerald", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "emerald", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "garnet", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "garnet", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "ice", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "ice", "alter", 99998 ); }
				else if ( item.Hue == GetMaterialColor( "silver", "alter", 99999 ) ){ item.Hue = GetMaterialColor( "silver", "alter", 99998 ); }
			}
		}

	}
}
