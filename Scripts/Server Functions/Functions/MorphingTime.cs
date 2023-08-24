using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Misc
{
    class MorphingTime
    {
		public static void SetGender( Mobile m )
		{
			if ( m.Body == 400 || m.Body == 605 ){ m.Female = false; }
			else if ( m.Body == 401 || m.Body == 606 ){ m.Female = true; }
		}

		public static void MakeMeAGargoyle( Mobile m, string Job )
		{
			m.Hue = 0x845;
			m.Name = NameList.RandomName( "gargoyle name" );

			Item helm = new WornHumanDeco();
				helm.Name = "gargoyle head";
				helm.ItemID = 0x2B72;
				helm.Hue = m.Hue;
				helm.Layer = Layer.Helm;
				m.AddItem( helm );

			Item wings = new WornHumanDeco();
				wings.Name = "gargoyle wings";
				wings.ItemID = 0x2FC5;
				wings.Hue = m.Hue;
				wings.Layer = Layer.Cloak;
				m.AddItem( wings );

			Item robe = new Robe();
				robe.Name = "gargoyle robe";
				robe.ItemID = 0x1F03;
				robe.Hue = 0x96C;
				m.AddItem( robe );

			if ( Utility.RandomMinMax( 1, 2 ) == 1 ) // FEMALE
			{ 
				m.Body = 401;
				m.BaseSoundID = 0x4B0;
				if ( Job == "mage" )
				{
					switch ( Utility.RandomMinMax( 0, 5 ) )
					{
						case 0: m.Title = "the gargoyle wizard"; break;
						case 1: m.Title = "the gargoyle sorcereress"; break;
						case 2: m.Title = "the gargoyle mage"; break;
						case 3: m.Title = "the gargoyle conjurer"; break;
						case 4: m.Title = "the gargoyle magician"; break;
						case 5: m.Title = "the gargoyle witch"; break;
					}
				}
			} 
			else 
			{ 
				m.Body = 400;
				m.BaseSoundID = 372;
				if ( Job == "mage" )
				{
					switch ( Utility.RandomMinMax( 0, 5 ) )
					{
						case 0: m.Title = "the gargoyle wizard"; break;
						case 1: m.Title = "the gargoyle sorcerer"; break;
						case 2: m.Title = "the gargoyle mage"; break;
						case 3: m.Title = "the gargoyle conjurer"; break;
						case 4: m.Title = "the gargoyle magician"; break;
						case 5: m.Title = "the gargoyle warlock"; break;
					}
				}
			}

			// REMOVE ANY PICK AXES
			List<Item> items = new List<Item>();
			foreach( Item i in m.Backpack.FindItemsByType( typeof( GargoylesPickaxe ), true ) ){ items.Add(i); }
			foreach ( Item item in items ){ item.Delete(); }
		}

		public static void VampireDressUp( Mobile m, int body )
		{
			int Hue1 = Utility.RandomMinMax( 2401, 2412 ); // BLACK
			int Hue2 = Utility.RandomList( 2117, 2118, 1640, 1641, 1642, 1643, 1644, 1645, 1650, 1651, 1652, 1653, 1654, 1157, 1194 ); // RED

			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				Hue1 = Utility.RandomList( 2117, 2118, 1640, 1641, 1642, 1643, 1644, 1645, 1650, 1651, 1652, 1653, 1654, 1157, 1194 ); // RED
				Hue2 = Utility.RandomMinMax( 2401, 2412 ); // BLACK
			}

			if ( body != 606 && ( Utility.RandomMinMax( 1, 2 ) == 1 || body == 605 ) ) // MALE
			{
				m.Body = 605;
				m.Name = NameList.RandomName( "dark_elf_prefix_male" ) + NameList.RandomName( "dark_elf_suffix_male" );
				m.BaseSoundID = 0x47D;
				if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ Utility.AssignRandomHair( m ); } else { m.HairItemID = 0; }

				switch ( Utility.RandomMinMax( 1, 4 ) )
				{
					case 1:
						FancyShirt m_shirt = new FancyShirt(); m_shirt.Hue = Hue1; m.AddItem( m_shirt );
						LongPants m_pant = new LongPants(); m_pant.Hue = Hue1; m.AddItem( m_pant );
						break;
					case 2:
						Shirt m_shirts = new Shirt(); m_shirts.Hue = Hue1; m.AddItem( m_shirts );
						ShortPants m_pants = new ShortPants(); m_pants.Hue = Hue1; m.AddItem( m_pants );
						break;
					case 3:
						Robe m_robe = new Robe(); m_robe.Hue = Hue1; m.AddItem( m_robe );
						break;
					case 4:
						Robe m_robes = new Robe(); m_robes.Hue = Hue1; m.AddItem( m_robes );
						break;
				}
			}
			else
			{
				m.Body = 606;
				m.Name = NameList.RandomName( "dark_elf_prefix_female" ) + NameList.RandomName( "dark_elf_suffix_female" );
				m.BaseSoundID = 0x257;
				Utility.AssignRandomHair( m );
				m.AddItem( new FancyDress(0x5B5) );

				switch ( Utility.RandomMinMax( 1, 5 ) )
				{
					case 1:
						FancyShirt f_shirt = new FancyShirt(); f_shirt.Hue = Hue1; m.AddItem( f_shirt );
						Skirt f_pant = new Skirt(); f_pant.Hue = Hue1; m.AddItem( f_pant );
						break;
					case 2:
						Shirt f_shirts = new Shirt(); f_shirts.Hue = Hue1; m.AddItem( f_shirts );
						Kilt f_pants = new Kilt(); f_pants.Hue = Hue1; m.AddItem( f_pants );
						break;
					case 3:
						PlainDress f_robe = new PlainDress(); f_robe.Hue = Hue1; m.AddItem( f_robe );
						break;
					case 4:
						PlainDress f_robes = new PlainDress(); f_robes.Hue = Hue1; m.AddItem( f_robes );
						break;
					case 5:
						FancyDress f_dress = new FancyDress(); f_dress.Hue = Hue1; m.AddItem( f_dress );
						break;
				}
			}

			if ( Utility.RandomMinMax( 1, 4 ) > 1 ){ m.AddItem( new Cloak(Hue2) ); }
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ LeatherGloves gloves = new LeatherGloves(); gloves.Hue = Hue2; m.AddItem( gloves ); }
			Boots boots = new Boots(); boots.Hue = Hue2; boots.ItemID = 12228; m.AddItem( boots );
			m.Hue = 0;
			m.HairHue = 0x497;

			BlessMyClothes( m );
		}

		public static void RemoveMyClothes( Mobile m )
		{
			if ( m.FindItemOnLayer( Layer.OuterTorso ) != null ) { m.FindItemOnLayer( Layer.OuterTorso ).Delete(); }
			if ( m.FindItemOnLayer( Layer.MiddleTorso ) != null ) { m.FindItemOnLayer( Layer.MiddleTorso ).Delete(); }
			if ( m.FindItemOnLayer( Layer.OneHanded ) != null ) { m.FindItemOnLayer( Layer.OneHanded ).Delete(); }
			if ( m.FindItemOnLayer( Layer.TwoHanded ) != null ) { m.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Bracelet ) != null ) { m.FindItemOnLayer( Layer.Bracelet ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Ring ) != null ) { m.FindItemOnLayer( Layer.Ring ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Helm ) != null ) { m.FindItemOnLayer( Layer.Helm ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Arms ) != null ) { m.FindItemOnLayer( Layer.Arms ).Delete(); }
			if ( m.FindItemOnLayer( Layer.OuterLegs ) != null ) { m.FindItemOnLayer( Layer.OuterLegs ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Neck ) != null ) { m.FindItemOnLayer( Layer.Neck ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Gloves ) != null ) { m.FindItemOnLayer( Layer.Gloves ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Talisman ) != null ) { m.FindItemOnLayer( Layer.Talisman ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Shoes ) != null ) { m.FindItemOnLayer( Layer.Shoes ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Cloak ) != null ) { m.FindItemOnLayer( Layer.Cloak ).Delete(); }
			if ( m.FindItemOnLayer( Layer.FirstValid ) != null ) { m.FindItemOnLayer( Layer.FirstValid ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Waist ) != null ) { m.FindItemOnLayer( Layer.Waist ).Delete(); }
			if ( m.FindItemOnLayer( Layer.InnerLegs ) != null ) { m.FindItemOnLayer( Layer.InnerLegs ).Delete(); }
			if ( m.FindItemOnLayer( Layer.InnerTorso ) != null ) { m.FindItemOnLayer( Layer.InnerTorso ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Pants ) != null ) { m.FindItemOnLayer( Layer.Pants ).Delete(); }
			if ( m.FindItemOnLayer( Layer.Shirt ) != null ) { m.FindItemOnLayer( Layer.Shirt ).Delete(); }
		}

		public static void ColorMyClothes( Mobile m, int hue )
		{
			if ( m.FindItemOnLayer( Layer.OuterTorso ) != null ) { if ( !( m.FindItemOnLayer( Layer.OuterTorso ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.OuterTorso ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.MiddleTorso ) != null ) { if ( !( m.FindItemOnLayer( Layer.MiddleTorso ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.MiddleTorso ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.OneHanded ) != null ) { if ( !( m.FindItemOnLayer( Layer.OneHanded ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.OneHanded ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.TwoHanded ) != null ) { if ( !( m.FindItemOnLayer( Layer.TwoHanded ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.TwoHanded ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Bracelet ) != null ) { if ( !( m.FindItemOnLayer( Layer.Bracelet ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Bracelet ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Ring ) != null ) { if ( !( m.FindItemOnLayer( Layer.Ring ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Ring ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Helm ) != null ) { if ( !( m.FindItemOnLayer( Layer.Helm ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Helm ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Arms ) != null ) { if ( !( m.FindItemOnLayer( Layer.Arms ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Arms ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.OuterLegs ) != null ) { if ( !( m.FindItemOnLayer( Layer.OuterLegs ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.OuterLegs ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Neck ) != null ) { if ( !( m.FindItemOnLayer( Layer.Neck ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Neck ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Gloves ) != null ) { if ( !( m.FindItemOnLayer( Layer.Gloves ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Gloves ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Talisman ) != null ) { if ( !( m.FindItemOnLayer( Layer.Talisman ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Talisman ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Shoes ) != null ) { if ( !( m.FindItemOnLayer( Layer.Shoes ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Shoes ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Cloak ) != null ) { if ( !( m.FindItemOnLayer( Layer.Cloak ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Cloak ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.FirstValid ) != null ) { if ( !( m.FindItemOnLayer( Layer.FirstValid ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.FirstValid ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Waist ) != null ) { if ( !( m.FindItemOnLayer( Layer.Waist ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Waist ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.InnerLegs ) != null ) { if ( !( m.FindItemOnLayer( Layer.InnerLegs ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.InnerLegs ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.InnerTorso ) != null ) { if ( !( m.FindItemOnLayer( Layer.InnerTorso ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.InnerTorso ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Pants ) != null ) { if ( !( m.FindItemOnLayer( Layer.Pants ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Pants ).Hue = hue; } }
			if ( m.FindItemOnLayer( Layer.Shirt ) != null ) { if ( !( m.FindItemOnLayer( Layer.Shirt ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Shirt ).Hue = hue; } }
		}

		public static void SailorMyClothes( Mobile m )
		{
			if ( m.FindItemOnLayer( Layer.OuterTorso ) != null ) { if ( !( m.FindItemOnLayer( Layer.OuterTorso ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.OuterTorso ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.MiddleTorso ) != null ) { if ( !( m.FindItemOnLayer( Layer.MiddleTorso ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.MiddleTorso ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Bracelet ) != null ) { if ( !( m.FindItemOnLayer( Layer.Bracelet ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Bracelet ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Ring ) != null ) { if ( !( m.FindItemOnLayer( Layer.Ring ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Ring ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Helm ) != null ) { if ( !( m.FindItemOnLayer( Layer.Helm ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Helm ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Arms ) != null ) { if ( !( m.FindItemOnLayer( Layer.Arms ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Arms ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.OuterLegs ) != null ) { if ( !( m.FindItemOnLayer( Layer.OuterLegs ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.OuterLegs ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Neck ) != null ) { if ( !( m.FindItemOnLayer( Layer.Neck ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Neck ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Gloves ) != null ) { if ( !( m.FindItemOnLayer( Layer.Gloves ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Gloves ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Talisman ) != null ) { if ( !( m.FindItemOnLayer( Layer.Talisman ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Talisman ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Shoes ) != null ) { if ( !( m.FindItemOnLayer( Layer.Shoes ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Shoes ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Cloak ) != null ) { if ( !( m.FindItemOnLayer( Layer.Cloak ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Cloak ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Waist ) != null ) { if ( !( m.FindItemOnLayer( Layer.Waist ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Waist ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.InnerLegs ) != null ) { if ( !( m.FindItemOnLayer( Layer.InnerLegs ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.InnerLegs ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.InnerTorso ) != null ) { if ( !( m.FindItemOnLayer( Layer.InnerTorso ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.InnerTorso ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Pants ) != null ) { if ( !( m.FindItemOnLayer( Layer.Pants ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Pants ).Hue = RandomThings.GetRandomColor(0); } }
			if ( m.FindItemOnLayer( Layer.Shirt ) != null ) { if ( !( m.FindItemOnLayer( Layer.Shirt ) is WornHumanDeco ) ){ m.FindItemOnLayer( Layer.Shirt ).Hue = RandomThings.GetRandomColor(0); } }
		}

		public static void BlessMyClothes( Mobile m )
		{
			if ( m.FindItemOnLayer( Layer.OuterTorso ) != null ) { m.FindItemOnLayer( Layer.OuterTorso ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.MiddleTorso ) != null ) { m.FindItemOnLayer( Layer.MiddleTorso ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.OneHanded ) != null ) { m.FindItemOnLayer( Layer.OneHanded ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.TwoHanded ) != null ) { m.FindItemOnLayer( Layer.TwoHanded ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Bracelet ) != null ) { m.FindItemOnLayer( Layer.Bracelet ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Ring ) != null ) { m.FindItemOnLayer( Layer.Ring ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Helm ) != null ) { m.FindItemOnLayer( Layer.Helm ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Arms ) != null ) { m.FindItemOnLayer( Layer.Arms ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.OuterLegs ) != null ) { m.FindItemOnLayer( Layer.OuterLegs ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Neck ) != null ) { m.FindItemOnLayer( Layer.Neck ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Gloves ) != null ) { m.FindItemOnLayer( Layer.Gloves ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Talisman ) != null ) { m.FindItemOnLayer( Layer.Talisman ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Shoes ) != null ) { m.FindItemOnLayer( Layer.Shoes ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Cloak ) != null ) { m.FindItemOnLayer( Layer.Cloak ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.FirstValid ) != null ) { m.FindItemOnLayer( Layer.FirstValid ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Waist ) != null ) { m.FindItemOnLayer( Layer.Waist ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.InnerLegs ) != null ) { m.FindItemOnLayer( Layer.InnerLegs ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.InnerTorso ) != null ) { m.FindItemOnLayer( Layer.InnerTorso ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Pants ) != null ) { m.FindItemOnLayer( Layer.Pants ).LootType = LootType.Blessed; }
			if ( m.FindItemOnLayer( Layer.Shirt ) != null ) { m.FindItemOnLayer( Layer.Shirt ).LootType = LootType.Blessed; }
		}

		public static void RenameSpaceAceItem( Item item, string word )
		{
			string name = "item";

			if ( item.Name != null && item.Name != "" ){ name = item.Name; }
			if ( name == "item" ){ name = MorphingItem.AddSpacesToSentence( (item.GetType()).Name ); }
			
			item.Name = word + " " + name;
		}

		public static void MakeSpaceAceMetalArmorWeapon( Item item, string material )
		{
			string suffix = " metal";

			switch( Utility.Random( 2 ) )
			{
				case 0: suffix = " metal"; break;
				case 1: suffix = " alloy"; break;
			}

			if ( material == null ){ material = GetSpaceAceMetalName(); }

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;

				weapon.Resource = CraftResource.None;
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ Server.Misc.MorphingItem.MorphMyItem( item, "IGNORED", ( "" + material + suffix + "" ), "IGNORED", MorphingTemplates.TemplateSpaceAce("weapons") ); }
				else { RenameSpaceAceItem( item, ( "" + material + suffix + "" ) ); }
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				armor.Resource = CraftResource.None;
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ Server.Misc.MorphingItem.MorphMyItem( item, "IGNORED", ( "" + material + suffix + "" ), "IGNORED", MorphingTemplates.TemplateSpaceAce("armors") ); }
				else { RenameSpaceAceItem( item, ( "" + material + suffix + "" ) ); }
			}
			else if ( Server.Misc.MaterialInfo.IsJewelryRing( item ) ){ item.Name = material + suffix + " Ring"; }
			else if ( item.ItemID == 0x4CFD || item.ItemID == 0x4CFE ){ item.Name = material + suffix + " Beads"; }
			else if ( Server.Misc.MaterialInfo.IsJewelryAmulet( item ) ){ item.Name = material + suffix + " Amulet"; }
			else if ( Server.Misc.MaterialInfo.IsJewelryEarrings( item ) ){ item.Name = material + suffix + " Earrings"; }
			else if ( Server.Misc.MaterialInfo.IsJewelryBracelet( item ) ){ item.Name = material + suffix + " Bracelet"; }	
			else if ( item is MagicJewelryCirclet || item.ItemID == 0x2B6F || item.ItemID == 0x3166 ){ item.Name = material + suffix + " Circlet"; }
			else
			{
				RenameSpaceAceItem( item, ( "" + material + suffix + "" ) );
			}

			if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; weapon.Resource = CraftResource.None; }
			else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; armor.Resource = CraftResource.None; }

			item.Hue = Server.Misc.MaterialInfo.GetSpaceAceColors( material );
		}

		public static void MakeSpaceAceWoodArmorWeapon( Item item, string material )
		{
			string suffix = " timber";

			switch( Utility.Random( 2 ) )
			{
				case 0: suffix = " timber"; break;
				case 1: suffix = " wood"; break;
			}

			if ( item.Name != null )
			{
				if ( (item.Name).Contains("Wood") || (item.Name).Contains("wood") ){ suffix = ""; }
			}

			if ( material == null ){ material = GetSpaceAceWoodName(); }

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;

				weapon.Resource = CraftResource.None;
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ Server.Misc.MorphingItem.MorphMyItem( item, "IGNORED", ( "" + material + suffix + "" ), "IGNORED", MorphingTemplates.TemplateSpaceAce("weapons") ); }
				else { RenameSpaceAceItem( item, ( "" + material + suffix + "" ) ); }
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				armor.Resource = CraftResource.None;
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ Server.Misc.MorphingItem.MorphMyItem( item, "IGNORED", ( "" + material + suffix + "" ), "IGNORED", MorphingTemplates.TemplateSpaceAce("armors") ); }
				else { RenameSpaceAceItem( item, ( "" + material + suffix + "" ) ); }
			}
			else
			{
				RenameSpaceAceItem( item, ( "" + material + suffix + "" ) );
			}

			if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; weapon.Resource = CraftResource.None; }
			else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; armor.Resource = CraftResource.None; }

			item.Hue = Server.Misc.MaterialInfo.GetSpaceAceColors( material );
		}

		public static void MakeSpaceAceClothArmorWeapon( Item item, string material )
		{
			string suffix = " woven";

			switch( Utility.Random( 2 ) )
			{
				case 0: suffix = " woven"; break;
				case 1: suffix = " meshed"; break;
			}

			if ( material == null ){ material = GetSpaceAceClothName(); }

			if ( item is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)item;

				weapon.Resource = CraftResource.None;
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ Server.Misc.MorphingItem.MorphMyItem( item, "IGNORED", ( "" + material + suffix + "" ), "IGNORED", MorphingTemplates.TemplateSpaceAce("weapons") ); }
				else { RenameSpaceAceItem( item, ( "" + material + suffix + "" ) ); }
			}
			else if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				armor.Resource = CraftResource.None;
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ Server.Misc.MorphingItem.MorphMyItem( item, "IGNORED", ( "" + material + suffix + "" ), "IGNORED", MorphingTemplates.TemplateSpaceAce("armors") ); }
				else { RenameSpaceAceItem( item, ( "" + material + suffix + "" ) ); }
			}
			else
			{
				RenameSpaceAceItem( item, ( "" + material + suffix + "" ) );
			}


			if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; weapon.Resource = CraftResource.None; }
			else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; armor.Resource = CraftResource.None; }

			item.Hue = Server.Misc.MaterialInfo.GetSpaceAceColors( material );
		}

		public static void MakeSpaceAceBoneArmor( Item item, string material, bool keepName )
		{
			if ( !keepName ){ item.Name = null; }

			if ( item is OrcHelm ){ item.Name = "skeletal helm"; }
			else if ( item is BoneLegs || item is BoneLegs ){ item.Name = "skeletal leggings"; item.ItemID = 0x49C2; }
			else if ( item is BoneGloves || item is SavageGloves ){ item.Name = "skeletal gauntlets"; item.ItemID = 0x499D; }
			else if ( item is BoneArms || item is SavageArms ){ item.Name = "skeletal bracers"; item.ItemID = 0x4988; }
			else if ( item is BoneChest || item is SavageChest ){ item.Name = "skeletal tunic"; item.ItemID = 0x498F; }
			else if ( item is BoneSkirt ){ item.Name = "skeletal skirt"; }
			else if ( item is BoneHelm || item is SavageHelm ){ item.Name = "skeletal helm"; item.ItemID = 0x49C1; }

			if ( material == null ){ material = GetSpaceAceBoneName(); }

			if ( item is BaseArmor )
			{
				BaseArmor armor = (BaseArmor)item;

				armor.Resource = CraftResource.None;
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ Server.Misc.MorphingItem.MorphMyItem( item, "IGNORED", material, "IGNORED", MorphingTemplates.TemplateSpaceAce("armors") ); }
				else { RenameSpaceAceItem( item, material ); }
			}
			else
			{
				RenameSpaceAceItem( item, material );
			}

			if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; weapon.Resource = CraftResource.None; }
			else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; armor.Resource = CraftResource.None; }

			item.Hue = Server.Misc.MaterialInfo.GetSpaceAceColors( material );
		}

		public static string GetSpaceAceMetalName()
		{
			string[] vMetal = new string[] { "Beskar", "Carbonite", "Phrik", "Cortosis", "Songsteel", "Agrinium", "Durasteel", "Titanium", "Laminasteel", "Neuranium", "Promethium", "Quadranium", "Durite", "Farium", "Trimantium", "Xonolite" };
			return vMetal[Utility.RandomMinMax( 0, (vMetal.Length-1) )];
		}

		public static string GetSpaceAceClothName()
		{
			string[] vCloth = new string[] { "Adesote", "Nylonite", "Biomesh", "Cerlin", "Polyfiber", "Durafiber", "Syncloth", "Hypercloth", "Flexicris", "Thermoweave", "Nylar" };
			return vCloth[Utility.RandomMinMax( 0, (vCloth.Length-1) )];
		}

		public static string GetSpaceAceBoneName()
		{
			string[] vBone = new string[] { "Twi'lek", "Rodian", "Martian", "Cardassian", "Xindi", "Tusken", "Andorian", "Zabrak" };
			return vBone[Utility.RandomMinMax( 0, (vBone.Length-1) )];
		}

		public static string GetSpaceAceWoodName()
		{
			string[] vWood = new string[] { "Veshok", "Cosian", "Greel", "Teej", "Kyshyyyk", "Laroon", "Borl", "Japor" };
			return vWood[Utility.RandomMinMax( 0, (vWood.Length-1) )];
		}

		public static void MakeSpaceAceItem( Item item, Mobile m )
		{
			if ( m != null )
			{
				Region reg = Region.Find( m.Location, m.Map );

				if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
				{
					if ( item is BaseMagicStaff || item is BaseQuiver ){}
					else if ( item is MagicTalisman || item is MagicTorch || item is MagicCandle || item is MagicLantern )
					{
						item.Name = LootPackEntry.MagicItemName( item, m, Region.Find( m.Location, m.Map ) );
					}
					else if ( item is BaseShield )
					{
						switch( Utility.RandomMinMax( 1, 4 ) )
						{
							case 1: item.ItemID = 0x1B76; item.Name = "hull plate";		break;
							case 2: item.ItemID = 0x1B76; item.Name = "deck plate";		break;
							case 3: item.ItemID = 0x1B72; item.Name = "hatch door";		break;
							case 4: item.ItemID = 0x1B7B; item.Name = "hatch cover";	break;
						}
						MakeSpaceAceMetalArmorWeapon( item, null );
					}
					else if ( item is MagicDragonLegs || item is MagicDragonGloves || item is MagicDragonArms || item is MagicDragonChest || item is MagicDragonHelm )
					{
						// DO NOTHING WITH DRAGON ARMOR
					}
					else if ( item is SkeletonsKey ){ item.Name = "minimal access card"; item.ItemID = 0x3A75; item.Hue = 0x59A; }
					else if ( item is MasterSkeletonsKey ){ item.Name = "full access card"; item.ItemID = 0x3A75; item.Hue = 0x66D; }
					else if ( item is Lockpick ){ item.Name = "security card"; item.ItemID = 0x3A75; item.Hue = 0x53C; item.Amount = Utility.RandomMinMax( 1, 3 ); }
					else if ( item is Krystal ){ item.Amount = Utility.RandomMinMax( 15, 50 ); }
					else if ( item is ChickenLeg ){ item.ItemID = 0x2023; item.Hue = 0xB51; item.Name = "soylent green"; }
					else if ( item is Ribs ){ item.ItemID = 0x2023; item.Hue = 0xB20; item.Name = "spam"; }
					else if ( item is Bolt ){ item.Amount = Utility.RandomMinMax( 15, 50 ); }
					else if ( item is Arrow ){ item.Amount = Utility.RandomMinMax( 15, 50 ); }
					else if ( Server.Items.HiddenTrap.IsJewelryItem( item ) ){ MakeSpaceAceMetalArmorWeapon( item, null ); }
					else if ( item is TinkerTools )
					{
						switch( Utility.RandomMinMax( 1, 2 ) )
						{
							case 1: item.ItemID = 0x3545; item.Name = "wrench";			break;
							case 2: item.ItemID = 0x2A2F; item.Name = "screwdriver";	break;
						}
					}
					else if ( item is Bandage ){ item.Amount = Utility.RandomMinMax( 5, 25 ); }
					else if ( item is Waterskin ){ item.ItemID = 0x4971; item.Name = "empty canteen"; }
					else if ( item is DirtyWaterskin ){ item.ItemID = 0x48E4; item.Name = "old canteen"; }
					else if ( ( item is ClothCowl || item is ClothHood || item is FancyHood || item is Robe ) && Utility.RandomBool() )
					{
						int catCloth = Utility.RandomMinMax( 1, 3 );
						if ( item is Robe ){ catCloth = Utility.RandomMinMax( 4, 7 ); }

						switch( catCloth )
						{
							case 1: ((BaseClothing)item).Resistances.Energy = Utility.RandomMinMax( 10, 30 );	item.Name = "radiation hood";		item.Hue = 0xBAD;	break;
							case 2: ((BaseClothing)item).Resistances.Poison = Utility.RandomMinMax( 10, 30 );	item.Name = "biohazard hood";		item.Hue = 0xBA1;	break;
							case 3: ((BaseClothing)item).Resistances.Energy = Utility.RandomMinMax( 5, 15 );	((BaseClothing)item).Resistances.Poison = Utility.RandomMinMax( 5, 15 );	item.Name = "hazmat hood";		item.Hue = 0x93D;	break;
							case 4: ((BaseClothing)item).Resistances.Energy = Utility.RandomMinMax( 10, 30 );	item.Name = "radiation suit";		item.Hue = 0xBAD;	break;
							case 5: ((BaseClothing)item).Resistances.Poison = Utility.RandomMinMax( 10, 30 );	item.Name = "biohazard suit";		item.Hue = 0xBA1;	break;
							case 6: ((BaseClothing)item).Resistances.Energy = Utility.RandomMinMax( 5, 15 );	((BaseClothing)item).Resistances.Poison = Utility.RandomMinMax( 5, 15 );	item.Name = "hazmat suit";		item.Hue = 0x93D;	break;
							case 7: ((BaseClothing)item).Resistances.Poison = Utility.RandomMinMax( 5, 15 );    item.Name = "lab coat";				item.Hue = 0xBB4;	break;
						}
					}
					else if ( ( item is BaseHat || item is MagicHat ) && Utility.RandomBool() ) // ONLY HALF THE HATS BECOME GOGGLES
					{
						item.ItemID = Utility.RandomList( 0x2FB8, 0x3172 );
						item.Name = "Goggles";
						switch( Utility.RandomMinMax( 0, 10 ) )
						{
							case 1: item.Name = "Pilot Goggles"; break;
							case 2: item.Name = "Medical Goggles"; break;
							case 3: item.Name = "Security Goggles"; break;
							case 4: item.Name = "Engineering Goggles"; break;
							case 5: item.Name = "Science Goggles"; break;
							case 6: item.Name = "Laboratory Goggles"; break;
							case 7: item.Name = "Safety Goggles"; break;
							case 8: item.Name = "Sun Goggles"; break;
							case 9: item.Name = "Night Goggles";
								if ( item is BaseClothing ){ ((BaseClothing)item).Attributes.NightSight = 1; }
								else if ( item is MagicHat ){ ((BaseJewel)item).Attributes.NightSight = 1; }
								break;
							case 10: item.Name = "Soldier Goggles"; break;
						}
						MakeSpaceAceMetalArmorWeapon( item, null );
					}
					else if ( item is LightSword || item is DoubleLaserSword )
					{
						// ALREADY MORPHED
					}
					else if ( item is StarSapphire ){ item.ItemID = 0xF26; item.Hue = 0x996; item.Name = "kyber crystal"; }
					else if ( item is Emerald ){ item.ItemID = 0xF25; item.Hue = 0x950; item.Name = "etaan crystal"; }
					else if ( item is Sapphire ){ item.ItemID = 0xF2D; item.Hue = 0xB40; item.Name = "trilithium crystal"; }
					else if ( item is Ruby ){ item.ItemID = 0xF16; item.Hue = 0x94F; item.Name = "lava crystal"; }
					else if ( item is Citrine ){ item.ItemID = 0xF21; item.Hue = 0xB54; item.Name = "dilithium crystal"; }
					else if ( item is Amethyst ){ item.ItemID = 0xF10; item.Hue = 0x94A; item.Name = "dantari crystal"; }
					else if ( item is Tourmaline ){ item.ItemID = 0xF19; item.Hue = 0x86C; item.Name = "vexxtal crystal"; }
					else if ( item is Amber ){ item.ItemID = 0xF13; item.Hue = 0x8FC; item.Name = "nova crystal"; }
					else if ( item is Diamond ){ item.ItemID = 0xF15; item.Hue = 0x90F; item.Name = "permafrost crystal"; }
					else if ( ( item is KilrathiHeavyGun || item is KilrathiGun ) && Utility.RandomMinMax( 1, 5 ) > 1 )
					{
						// ALREADY MORPHED, BUT DON'T GIVE IT A SPACE METAL 80% OF THE TIME
					}
					else if ( item is MagicJewelryRing || item is MagicJewelryNecklace || item is MagicJewelryEarrings || item is MagicJewelryBracelet || item is MagicJewelryCirclet )
					{
						MakeSpaceAceMetalArmorWeapon( item, null );
					}
					else if ( item is MagicBelt || item is MagicBoots || item is MagicCloak || item is MagicHat || item is MagicRobe || item is MagicSash || item is BaseClothing )
					{
						MakeSpaceAceClothArmorWeapon( item, null );
					}
					else if ( item is BaseArmor )
					{
						if ( item is BoneSkirt || item is BoneLegs || item is BoneGloves || item is BoneArms || item is BoneChest || item is OrcHelm || item is BoneHelm ){ MakeSpaceAceBoneArmor( item, null, false ); }
						else if ( item is SavageLegs || item is SavageGloves || item is SavageArms || item is SavageChest || item is SavageHelm ){ MakeSpaceAceBoneArmor( item, null, false ); }
						else if ( Server.Misc.MaterialInfo.IsWoodenItem( item ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ ((BaseArmor)item).Resource = CraftResource.PetrifiedTree; }
						else if ( Server.Misc.MaterialInfo.IsWoodenItem( item ) ){ MakeSpaceAceWoodArmorWeapon( item, null ); }
						else if ( Server.Misc.MaterialInfo.IsLeatherItem( item ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ ((BaseArmor)item).Resource = CraftResource.AlienLeather; }
						else if ( Server.Misc.MaterialInfo.IsLeatherItem( item ) ){ MakeSpaceAceClothArmorWeapon( item, null ); }
						else if ( Server.Misc.MaterialInfo.IsMetalItem( item ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ ((BaseArmor)item).Resource = CraftResource.Xormite; }
						else if ( Server.Misc.MaterialInfo.IsMetalItem( item ) ){ MakeSpaceAceMetalArmorWeapon( item, null ); }
					}
					else if ( item is BaseWeapon )
					{
						if ( Server.Misc.MaterialInfo.IsLeatherItem( item ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ ((BaseWeapon)item).Resource = CraftResource.AlienLeather; }
						else if ( Server.Misc.MaterialInfo.IsLeatherItem( item ) ){ MakeSpaceAceClothArmorWeapon( item, null ); }
						else if ( Server.Misc.MaterialInfo.IsWoodenItem( item ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ ((BaseWeapon)item).Resource = CraftResource.PetrifiedTree; }
						else if ( Server.Misc.MaterialInfo.IsWoodenItem( item ) ){ MakeSpaceAceWoodArmorWeapon( item, null ); }
						else if ( Server.Misc.MaterialInfo.IsMetalItem( item ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ ((BaseWeapon)item).Resource = CraftResource.Xormite; }
						else if ( Server.Misc.MaterialInfo.IsMetalItem( item ) ){ MakeSpaceAceMetalArmorWeapon( item, null ); }
					}
					else if ( item is Bedroll ){ item.Name = "sleeping bag"; item.Hue = Server.Misc.RandomThings.GetRandomColor(0); }
					else if ( item is UnknownLiquid ){ Server.Items.UnknownLiquid.MakeSpaceAceLiquid( item ); }
					else if ( item is BasePotion ){ Server.Items.BasePotion.MakePillBottle( item ); }
					else if ( item is UnknownReagent ){ Server.Items.UnknownReagent.MakeSpaceAceReagent( item ); }
					else if ( item is Spyglass ){ item.Name = "binoculars"; item.ItemID = 0x3562; }
					else if ( item is ArtifactManual ){ item.Name = "magnifying lense"; item.ItemID = 0x202F; item.Hue = 0; ((ArtifactManual)item).Charges = Utility.RandomMinMax( 2, 10 ); }
				}
			}
		}

		public static void MakeOrientalItem( Item item, Mobile m )
		{
			if ( m != null )
			{
				Region reg = Region.Find( m.Location, m.Map );

				if ( Server.Misc.GetPlayerInfo.OrientalPlay( m ) == true )
				{
					double ninja = m.Skills[SkillName.Ninjitsu].Base;
					double samurai = m.Skills[SkillName.Bushido].Base;

					if ( ninja <= 0 && samurai <= 0 )
					{
						switch( Utility.RandomMinMax( 0, 1 ) )
						{
							case 0: ninja = 100.0;		break;
							case 1: samurai = 100.0;	break;
						}
					}

					if ( ninja > 0 && ninja >= samurai )
					{
						if ( item is LeatherCap ) { item.ItemID = 0x278E; item.Name = "leather hood"; if ( item.Hue == 0 ){ item.Hue = 0x83D; } }
						else if ( item is LeatherChest ) { item.ItemID = 0x2793; item.Name = "leather shitagi"; if ( item.Hue == 0 ){ item.Hue = 0x83D; } }
						else if ( item is LeatherGloves ) { item.ItemID = 0x2792; item.Name = "leather yugake"; if ( item.Hue == 0 ){ item.Hue = 0x83D; } }
						else if ( item is LeatherLegs ) { item.ItemID = 0x2791; item.Name = "leather hakama"; if ( item.Hue == 0 ){ item.Hue = 0x83D; } }
						else if ( item is LeatherGorget ) { item.ItemID = 0x277A; item.Name = "leather nodowa"; if ( item.Hue == 0 ){ item.Hue = 0x83D; } }
						else if ( item is StuddedChest ) { item.ItemID = 0x2793; item.Name = "studded shitagi"; if ( item.Hue == 0 ){ item.Hue = 0x837; } }
						else if ( item is StuddedLegs ) { item.ItemID = 0x2791; item.Name = "studded hakama"; if ( item.Hue == 0 ){ item.Hue = 0x837; } }
						else if ( item is StuddedGloves ) { item.ItemID = 0x2792; item.Name = "studded yugake"; if ( item.Hue == 0 ){ item.Hue = 0x837; } }
						else if ( item is StuddedGorget ) { item.ItemID = 0x277A; item.Name = "leather nodowa"; if ( item.Hue == 0 ){ item.Hue = 0x837; } }
						else if ( item is MagicBoots ) { item.ItemID = 0x2797; item.Name = "shinobi tabi"; }
						else if ( item is MagicHat ) { item.ItemID = 0x27DA; item.Name = "shinobi hood"; }
					}
					else if ( samurai > 0 && samurai >= ninja )
					{
						if ( item is LeatherCap ) { item.ItemID = 0x2798; item.Name = "leather kasa"; }
						else if ( item is LeatherChest ) { item.ItemID = 0x277B; item.Name = "leather do"; }
						else if ( item is LeatherLegs && Utility.RandomMinMax( 1, 2 ) == 1) { item.ItemID = 0x278A; item.Name = "leather haidate"; }
						else if ( item is LeatherLegs ) { item.ItemID = 0x2786; item.Name = "leather suneate"; }
						else if ( item is LeatherGorget ) { item.ItemID = 0x277A; item.Name = "leather mempo"; }
						else if ( item is LeatherArms ) { item.ItemID = 0x277E; item.Name = "leather kote"; }
						else if ( item is StuddedChest ) { item.ItemID = 0x27C7; item.Name = "studded do"; }
						else if ( item is StuddedLegs && Utility.RandomMinMax( 1, 2 ) == 1) { item.ItemID = 0x278B; item.Name = "studded haidate"; }
						else if ( item is StuddedLegs ) { item.ItemID = 0x27D2; item.Name = "studded suneate"; }
						else if ( item is StuddedGorget ) { item.ItemID = 0x279D; item.Name = "studded mempo"; }
						else if ( item is StuddedArms ) { item.ItemID = 0x277F; item.Name = "studded kote"; }
						else if ( item is MagicBoots )
						{
							switch( Utility.RandomMinMax( 0, 2 ) )
							{
								case 0: item.ItemID = 0x2797; item.Name = "samurai tabi"; break;
								case 1: item.ItemID = 0x2796; item.Name = "waraji"; break;
								case 2: item.ItemID = 0x170B; item.Name = "kutsu"; break;
							}
						}
						else if ( item is MagicHat )
						{
							switch( Utility.RandomMinMax( 0, 1 ) )
							{
								case 0: item.ItemID = 0x2798; item.Name = "kasa"; break;
								case 1: item.ItemID = 0x1540; item.Name = "bandana"; break;
							}
						}
					}

					if ( item is ChainCoif ) { item.ItemID = 0x2774; item.Name = "chain hatsuburi"; if ( item.Hue == 0 ){ item.Hue = 0x836; } }
					else if ( item is RingmailArms ) { item.ItemID = 0x277F; item.Name = "ringmail kote"; if ( item.Hue == 0 ){ item.Hue = 0x836; } }
					else if ( item is RingmailLegs ) { item.ItemID = 0x278D; item.Name = "ringmail haidate"; if ( item.Hue == 0 ){ item.Hue = 0x836; } }
					else if ( item is ChainLegs ) { item.ItemID = 0x2788; item.Name = "chain suneate"; if ( item.Hue == 0 ){ item.Hue = 0x836; } }
					else if ( item is RingmailChest ) { item.ItemID = 0x27C7; item.Name = "ringmail do"; if ( item.Hue == 0 ){ item.Hue = 0x836; } }
					else if ( item is ChainChest ) { item.ItemID = 0x277D; item.Name = "chain do"; if ( item.Hue == 0 ){ item.Hue = 0x836; } }
					else if ( item is PlateChest ) { item.ItemID = 0x277D; item.Name = "plate do"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is RoyalChest ) { item.ItemID = 0x277D; item.Name = "shogun do"; if ( item.Hue == 0 ){ item.Hue = 0x846; } }
					else if ( item is MagicDragonHelm ) { item.ItemID = 0x2778; item.Name = "scale kabuto"; }
					else if ( item is MagicDragonChest ) { item.ItemID = 0x277D; item.Name = "scale do"; }
					else if ( item is MagicDragonLegs ) { item.ItemID = 0x278D; item.Name = "scale haidate"; }
					else if ( item is MagicDragonArms ) { item.ItemID = 0x2780; item.Name = "scale kote"; }
					else if ( item is MagicDragonGloves ) { item.Name = "scale yugake"; }
					else if ( item is PlateArms ) { item.ItemID = 0x2780; item.Name = "plate kote"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is RoyalArms ) { item.ItemID = 0x2780; item.Name = "shogun kote"; if ( item.Hue == 0 ){ item.Hue = 0x846; } }
					else if ( item is PlateGorget ) { item.ItemID = 0x2779; item.Name = "plate mempo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is RoyalGorget ) { item.ItemID = 0x2779; item.Name = "shogun mempo"; if ( item.Hue == 0 ){ item.Hue = 0x846; } }
					else if ( item is PlateLegs ) { item.ItemID = 0x2788; item.Name = "plate suneate"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is RoyalsLegs ) { item.ItemID = 0x278D; item.Name = "shogun haidate"; if ( item.Hue == 0 ){ item.Hue = 0x846; } }
					else if ( item is CloseHelm ) { item.ItemID = 0x2781; item.Name = "plate jingasa"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is Helmet ) { item.ItemID = 0x2777; item.Name = "plate jingasa"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is Bascinet ) { item.ItemID = 0x2775; item.Name = "plate hatsuburi"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is NorseHelm ) { item.ItemID = 0x2789; item.Name = "plate kabuto"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is PlateHelm ) { item.ItemID = 0x2785; item.Name = "plate kabuto"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is DreadHelm ) { item.ItemID = 0x2785; item.Name = "plate kabuto"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is RoyalHelm ) { item.ItemID = 0x2778; item.Name = "shogun kabuto"; if ( item.Hue == 0 ){ item.Hue = 0x846; } }
					else if ( item is MagicBelt )
					{
						switch( Utility.RandomMinMax( 0, 1 ) )
						{
							case 0: item.ItemID = 0x2790; item.Name = "belt"; break;
							case 1: item.ItemID = 0x27A0; item.Name = "obi"; break;
						}
					}
					else if ( item is MagicSash ){ item.Name = "himo"; }
					else if ( item is MagicCloak ){ item.Name = "horo"; }
					else if ( item is WoodenPlateLegs ) { item.ItemID = 0x2788; item.Name = "wooden suneate"; }
					else if ( item is WoodenPlateGorget ) { item.ItemID = 0x2779; item.Name = "wooden mempo"; }
					else if ( item is WoodenPlateArms ) { item.ItemID = 0x2780; item.Name = "wooden kote"; }
					else if ( item is WoodenPlateChest ) { item.ItemID = 0x277D; item.Name = "wooden do"; }
					else if ( item is WoodenPlateHelm ) { item.ItemID = 0x2785; item.Name = "wooden kabuto"; }
					else if ( item is MagicRobe || item is BaseOuterTorso )
					{
						switch( Utility.RandomMinMax( 0, 3 ) )
						{
							case 0: item.ItemID = Utility.RandomList( 0x2799, 0x27E4 ); item.Weight = 3.0; item.Name = "kamishimo"; break;
							case 1: item.ItemID = Utility.RandomList( 0x279C, 0x27E7 ); item.Weight = 3.0; item.Name = "hakama shita"; break;
							case 2: item.ItemID = Utility.RandomList( 0x2782, 0x27CD ); item.Weight = 3.0; item.Name = "kimono"; break;
							case 3: item.ItemID = Utility.RandomList( 0x2783, 0x27CE ); item.Weight = 3.0; item.Name = "kimono"; break;
						}
					}
					else if ( item is PugilistGlove ) { item.ItemID = 0x13C6; item.Name = "wushu gloves"; }
					else if ( item is PugilistGloves ) { item.ItemID = 0x13C6; item.Name = "wushu gloves"; }
					else if ( item is Crossbow ) { item.Name = "chu-ko-nu"; }
					else if ( item is ElvenCompositeLongbow ) { item.Name = "daikyu"; }
					else if ( item is QuarterStaff ) { item.Name = "bo staff"; }
					else if ( item is Pike ) { item.Name = "kumade"; }
					else if ( item is BladedStaff ) { item.Name = "naginata"; }
					else if ( item is Spear ) { item.Name = "yari"; }
					else if ( item is Axe ) { item.Name = "ono"; }
					else if ( item is ElvenMachete ) { item.Name = "sutou sabre"; }
					else if ( item is Scimitar ) { item.Name = "dao"; }	
					else if ( item is Leafblade ) { item.Name = "tanto"; }
					else if ( item is Longsword ) { item.ItemID = 0x27A8; item.Name = "daito"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is Dagger ) { item.Name = "aikuchi"; }
					else if ( item is WarMace ) { item.ItemID = 0x27A6; item.Name = "kanabo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is Mace ) { item.ItemID = 0x27A6; item.Name = "kanabo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is Maul ) { item.ItemID = 0x27A6; item.Name = "kanabo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is WarHammer ) { item.ItemID = 0x27A6; item.Name = "kanabo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is BronzeShield ) { item.ItemID = 0x1B72; item.Name = "large shirudo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is Buckler ) { item.ItemID = 0x1B73; item.Name = "small shirudo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is HeaterShield ) { item.ItemID = 0x1B76; item.Name = "infantry shirudo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
					else if ( item is MetalShield ) { item.ItemID = 0x1B7B; item.Name = "medium shirudo"; if ( item.Hue == 0 ){ item.Hue = 0x539; } }
				}
			}
		}

		public static void ChangeMaterialType( Item item, Mobile from )
		{
			if ( 	Server.Misc.MaterialInfo.IsStrangeMetalItem( item ) ||
					Server.Misc.MaterialInfo.IsStrangeWoodItem( item ) ||
					Server.Misc.MaterialInfo.IsStrangeClothItem( item ) ||
					item is MagicBoneChest || 
					item is MagicBoneHelm || 
					item is MagicBoneSkirt || 
					item is MagicBoneLegs || 
					item is MagicBoneGloves || 
					item is MagicBoneArms || 
					item is MagicDragonArms || 
					item is MagicDragonChest || 
					item is MagicDragonGloves || 
					item is MagicFurLegs || 
					item is MagicFurArms || 
					item is MagicFurChest || 
					item is MagicDragonHelm || 
					item is MagicDragonLegs )
			{
				// DON'T CHANGE
			}
			else
			{
				int chances = 0;

				int whichOne = Utility.RandomMinMax( 1, 2 );

				int originalColor = 0;
					if ( item.Hue == 0x539 || item.Hue == 0x846 || item.Hue == 0x836 || item.Hue == 0x83D || item.Hue == 0x837 ){ originalColor = item.Hue; }

				if ( Server.Misc.MaterialInfo.IsMetalItem( item ) == true )
				{
					chances = Utility.RandomMinMax( 1, 512 );

					if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;

						if ( chances >= 256 ){ weapon.Resource = CraftResource.Iron; }
						else if ( chances >= 128 ){ weapon.Resource = CraftResource.DullCopper; }
						else if ( chances >= 64 ){ weapon.Resource = CraftResource.ShadowIron; }
						else if ( chances >= 32 ){ weapon.Resource = CraftResource.Copper; }
						else if ( chances >= 16 ){ weapon.Resource = CraftResource.Bronze; }
						else if ( chances >= 8 ){ weapon.Resource = CraftResource.Gold; }
						else if ( chances >= 4 ){ weapon.Resource = CraftResource.Agapite; }
						else if ( chances >= 2 ){ weapon.Resource = CraftResource.Verite; }
						else if ( chances >= 1 ){ weapon.Resource = CraftResource.Valorite; }

						if ( chances == 1 && whichOne == 1 && Worlds.IsExploringSeaAreas( from ) == true ){ weapon.Resource = CraftResource.Nepturite; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Serpent Island" ){ weapon.Resource = CraftResource.Obsidian; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Savaged Empire" ){ weapon.Resource = CraftResource.Steel; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Island of Umber Veil" ){ weapon.Resource = CraftResource.Brass; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" && from.Map == Map.TerMur ){ weapon.Resource = CraftResource.Xormite; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" ){ weapon.Resource = CraftResource.Mithril; }

						Server.Items.Pitchfork.IronColor( weapon );
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;

						if ( chances >= 256 ){ armor.Resource = CraftResource.Iron; }
						else if ( chances >= 128 ){ armor.Resource = CraftResource.DullCopper; }
						else if ( chances >= 64 ){ armor.Resource = CraftResource.ShadowIron; }
						else if ( chances >= 32 ){ armor.Resource = CraftResource.Copper; }
						else if ( chances >= 16 ){ armor.Resource = CraftResource.Bronze; }
						else if ( chances >= 8 ){ armor.Resource = CraftResource.Gold; }
						else if ( chances >= 4 ){ armor.Resource = CraftResource.Agapite; }
						else if ( chances >= 2 ){ armor.Resource = CraftResource.Verite; }
						else if ( chances >= 1 ){ armor.Resource = CraftResource.Valorite; }

						if ( chances == 1 && whichOne == 1 && Worlds.IsExploringSeaAreas( from ) == true ){ armor.Resource = CraftResource.Nepturite; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Serpent Island" ){ armor.Resource = CraftResource.Obsidian; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Savaged Empire" ){ armor.Resource = CraftResource.Steel; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Island of Umber Veil" ){ armor.Resource = CraftResource.Brass; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" && from.Map == Map.TerMur ){ armor.Resource = CraftResource.Xormite; }
						else if ( chances == 1 && whichOne == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" ){ armor.Resource = CraftResource.Mithril; }
					}
				}
				else if ( Server.Misc.MaterialInfo.IsLeatherItem( item ) == true )
				{
					chances = Utility.RandomMinMax( 1, 1024 );

					if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;

						if ( chances >= 512 ){ weapon.Resource = CraftResource.RegularLeather; }
						else if ( chances >= 256 ){ weapon.Resource = CraftResource.HornedLeather; }
						else if ( chances >= 128 ){ weapon.Resource = CraftResource.BarbedLeather; }
						else if ( chances >= 64 ){ weapon.Resource = CraftResource.SpinedLeather; }
						else if ( chances >= 32 ){ weapon.Resource = CraftResource.NecroticLeather; }
						else if ( chances >= 16 ){ weapon.Resource = CraftResource.VolcanicLeather; }
						else if ( chances >= 8 ){ weapon.Resource = CraftResource.FrozenLeather; }
						else if ( chances >= 4 ){ weapon.Resource = CraftResource.GoliathLeather; }
						else if ( chances >= 2 ){ weapon.Resource = CraftResource.DraconicLeather; }
						else if ( chances >= 1 ){ weapon.Resource = CraftResource.HellishLeather; }

						if ( whichOne == 1 && Worlds.IsExploringSeaAreas( from ) == true ){ weapon.Resource = CraftResource.SpinedLeather; }
						else if ( whichOne == 1 && chances == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Savaged Empire" ){ weapon.Resource = CraftResource.DinosaurLeather; }
						else if ( whichOne == 1 && chances == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" && from.Map == Map.TerMur ){ weapon.Resource = CraftResource.AlienLeather; }
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;

						if ( chances >= 512 ){ armor.Resource = CraftResource.RegularLeather; }
						else if ( chances >= 256 ){ armor.Resource = CraftResource.HornedLeather; }
						else if ( chances >= 128 ){ armor.Resource = CraftResource.BarbedLeather; }
						else if ( chances >= 64 ){ armor.Resource = CraftResource.SpinedLeather; }
						else if ( chances >= 32 ){ armor.Resource = CraftResource.NecroticLeather; }
						else if ( chances >= 16 ){ armor.Resource = CraftResource.VolcanicLeather; }
						else if ( chances >= 8 ){ armor.Resource = CraftResource.FrozenLeather; }
						else if ( chances >= 4 ){ armor.Resource = CraftResource.GoliathLeather; }
						else if ( chances >= 2 ){ armor.Resource = CraftResource.DraconicLeather; }
						else if ( chances >= 1 ){ armor.Resource = CraftResource.HellishLeather; }

						if ( whichOne == 1 && Worlds.IsExploringSeaAreas( from ) == true ){ armor.Resource = CraftResource.SpinedLeather; }
						else if ( whichOne == 1 && chances == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Savaged Empire" ){ armor.Resource = CraftResource.DinosaurLeather; }
						else if ( whichOne == 1 && chances == 1 && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" && from.Map == Map.TerMur ){ armor.Resource = CraftResource.AlienLeather; }
					}
				}

				else if ( Server.Misc.MaterialInfo.IsWoodenItem( item ) == true )
				{
					chances = Utility.RandomMinMax( 1, 2048 );

					if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;

						if ( chances >= 1024 ){ weapon.Resource = CraftResource.RegularWood; }
						else if ( chances >= 512 ){ weapon.Resource = CraftResource.AshTree; }
						else if ( chances >= 256 ){ weapon.Resource = CraftResource.CherryTree; }
						else if ( chances >= 128 ){ weapon.Resource = CraftResource.EbonyTree; }
						else if ( chances >= 64 ){ weapon.Resource = CraftResource.GoldenOakTree; }
						else if ( chances >= 32 ){ weapon.Resource = CraftResource.HickoryTree; }
						else if ( chances >= 16 ){ weapon.Resource = CraftResource.MahoganyTree; }
						else if ( chances >= 8 ){ weapon.Resource = CraftResource.OakTree; }
						else if ( chances >= 4 ){ weapon.Resource = CraftResource.PineTree; }
						else if ( chances >= 2 ){ weapon.Resource = CraftResource.RosewoodTree; }
						else if ( chances >= 1 ){ weapon.Resource = CraftResource.WalnutTree; }

						if ( Worlds.IsExploringSeaAreas( from ) == true ){ weapon.Resource = CraftResource.DriftwoodTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "Ravendark Woods" && chances == 1 ){ weapon.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Island of Dracula" && chances == 1 ){ weapon.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Castle of Dracula" && chances == 1 ){ weapon.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Crypts of Dracula" && chances == 1 ){ weapon.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" && chances == 1 ){ weapon.Resource = CraftResource.PetrifiedTree; }
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;

						if ( chances >= 1024 ){ armor.Resource = CraftResource.RegularWood; }
						else if ( chances >= 512 ){ armor.Resource = CraftResource.AshTree; }
						else if ( chances >= 256 ){ armor.Resource = CraftResource.CherryTree; }
						else if ( chances >= 128 ){ armor.Resource = CraftResource.EbonyTree; }
						else if ( chances >= 64 ){ armor.Resource = CraftResource.GoldenOakTree; }
						else if ( chances >= 32 ){ armor.Resource = CraftResource.HickoryTree; }
						else if ( chances >= 16 ){ armor.Resource = CraftResource.MahoganyTree; }
						else if ( chances >= 8 ){ armor.Resource = CraftResource.OakTree; }
						else if ( chances >= 4 ){ armor.Resource = CraftResource.PineTree; }
						else if ( chances >= 2 ){ armor.Resource = CraftResource.RosewoodTree; }
						else if ( chances >= 1 ){ armor.Resource = CraftResource.WalnutTree; }

						if ( Worlds.IsExploringSeaAreas( from ) == true ){ armor.Resource = CraftResource.DriftwoodTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "Ravendark Woods" && chances == 1 ){ armor.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Island of Dracula" && chances == 1 ){ armor.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Castle of Dracula" && chances == 1 ){ armor.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Crypts of Dracula" && chances == 1 ){ armor.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" && chances == 1 ){ armor.Resource = CraftResource.PetrifiedTree; }
					}
					else if ( item is BaseInstrument )
					{
						BaseInstrument lute = (BaseInstrument)item;

						if ( chances >= 1024 ){ lute.Resource = CraftResource.RegularWood; }
						else if ( chances >= 512 ){ lute.Resource = CraftResource.AshTree; }
						else if ( chances >= 256 ){ lute.Resource = CraftResource.CherryTree; }
						else if ( chances >= 128 ){ lute.Resource = CraftResource.EbonyTree; }
						else if ( chances >= 64 ){ lute.Resource = CraftResource.GoldenOakTree; }
						else if ( chances >= 32 ){ lute.Resource = CraftResource.HickoryTree; }
						else if ( chances >= 16 ){ lute.Resource = CraftResource.MahoganyTree; }
						else if ( chances >= 8 ){ lute.Resource = CraftResource.OakTree; }
						else if ( chances >= 4 ){ lute.Resource = CraftResource.PineTree; }
						else if ( chances >= 2 ){ lute.Resource = CraftResource.RosewoodTree; }
						else if ( chances >= 1 ){ lute.Resource = CraftResource.WalnutTree; }

						if ( Worlds.IsExploringSeaAreas( from ) == true ){ lute.Resource = CraftResource.DriftwoodTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "Ravendark Woods" && chances == 1 ){ lute.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Island of Dracula" && chances == 1 ){ lute.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Castle of Dracula" && chances == 1 ){ lute.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Crypts of Dracula" && chances == 1 ){ lute.Resource = CraftResource.GhostTree; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" && chances == 1 ){ lute.Resource = CraftResource.PetrifiedTree; }
					}
				}

				if ( item.Hue == 0 ){ item.Hue = originalColor; }
			}
		}

		public static bool MakeSpecialMaterial( Item item, bool isEvil )
		{
			bool newName = false;
			if ( Utility.RandomMinMax( 1, 100 ) == 1 )
			{
				newName = true;
				string suffix = item.Name;

				int pick = Utility.RandomMinMax( 1, 22 );
					if ( isEvil == true ){ pick = Utility.RandomMinMax( 1, 6 ); }

				if ( item.ItemID == 0x2790 ){ suffix = "belt"; } 
				else if ( item.ItemID == 0x2B68 ){ suffix = "loin cloth"; } 
				else if ( item.ItemID == 0x153b ){ suffix = "apron"; } 
				else if ( item is MagicSash ){ suffix = "sash"; } 
				else if ( item is MagicLantern ){ suffix = "lantern"; }
				else if ( item is MagicCandle ){ suffix = "candle"; }
				else if ( item is MagicCloak ){ suffix = "cloak"; }
				else if ( item is MagicTalisman ){ suffix = item.Name; }
				else if ( item is MagicTorch ){ suffix = "torch"; }
				else if ( item.ItemID == 0x230E || item.ItemID == 0x230D ){ suffix = "dress"; }
				else if ( item.ItemID == 0x1F00 || item.ItemID == 0x1EFF ){ suffix = "dress"; }
				else if ( item.ItemID == 0x1f01 || item.ItemID == 0x1f02 ){ suffix = "dress"; }
				else if ( item.ItemID == 0x170d || item.ItemID == 0x170e ){ suffix = "sandals"; }
				else if ( item.ItemID == 0x170f || item.ItemID == 0x1710 ){ suffix = "shoes"; }
				else if ( item.ItemID == 5914 ){ suffix = "feathered hat"; } 
				else if ( item.ItemID == 5916 ){ suffix = "jester hat"; } 
				else if ( item.ItemID == 5911 ){ suffix = "straw hat"; } 
				else if ( item.ItemID == 5910 ){ suffix = "tall straw hat"; } 
				else if ( item.ItemID == 5908 ){ suffix = "wide brim hat"; } 
				else if ( item.ItemID == 5912 ){ suffix = "wizard hat"; } 
				else if ( item.ItemID == 5915 ){ suffix = "tricorne hat"; } 
				else if ( item.ItemID == 5907 ){ suffix = "floppy hat"; } 
				else if ( item.ItemID == 5907 ){ suffix = "floppy hat"; } 
				else if ( item.ItemID == 5444 ){ suffix = "skullcap"; } 
				else if ( item.ItemID == 5909 ){ suffix = "bonnet"; } 
				else if ( item.ItemID == 0x2B71 ){ suffix = "hood"; } 
				else if ( item.ItemID == 0x3176 ){ suffix = "cowl"; } 
				else if ( item.ItemID == 0x4D09 ){ suffix = "fancy hood"; } 
				else if ( item.ItemID == 0x4CDA ){ suffix = "Syth hood"; } 
				else if ( item.ItemID == 0x4CDC ){ suffix = "Syth cowl"; } 
				else if ( item.ItemID == 0x4CDB ){ suffix = "reaper hood"; } 
				else if ( item.ItemID == 0x4CDD ){ suffix = "reaper cowl"; } 
				else if ( item.ItemID == 0x2FC3 ){ suffix = "witch hat"; } 
				else if ( item.ItemID == 0x2FBC ){ suffix = "pirate hat"; } 
				else if ( item.ItemID == 0x2B6D ){ suffix = "wolfskin cap"; } 
				else if ( item.ItemID == 0x278F ){ suffix = "executioners hood"; } 
				else if ( item.ItemID == 0x1540 ){ suffix = "bandana"; } 
				else if ( item.ItemID == 0x1549 ){ suffix = "shaman mask"; } 
				else if ( item.ItemID == 0x154B ){ suffix = "tribal mask"; } 
				else if ( item.ItemID == 5445 ){ suffix = "bearskin cap"; } 
				else if ( item.ItemID == 5447 ){ suffix = "dearskin cap"; } 
				else if ( item is MagicRobe ){ suffix = "robe"; }
				else if ( item is MagicBoots){ suffix = "boots"; }
				else if ( item is MagicHat){ suffix = "hat"; }

				switch( pick )
				{
					case 1: item.Name = "shadow " + suffix;		item.Hue = 0x966;	break;
					case 2: item.Name = "ruby " + suffix;		item.Hue = MaterialInfo.GetMaterialColor( "ruby", "classic", 0 );		break;
					case 3: item.Name = "sapphire " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 );	break;
					case 4: item.Name = "obsidian " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 );	break;
					case 5: item.Name = "violet " + suffix;		item.Hue = 0x486;	break;
					case 6: item.Name = "silver " + suffix;		item.Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 );		break;
					case 7: item.Name = "jade " + suffix;		item.Hue = MaterialInfo.GetMaterialColor( "jade", "classic", 0 );		break;
					case 8: item.Name = "azurite " + suffix;	item.Hue = 0x5B6;	break;
					case 9: item.Name = "emerald " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "emerald", "classic", 0 );	break;
					case 10: item.Name = "pearl " + suffix;		item.Hue = 0x47E;	break;
					case 11: item.Name = "turquoise " + suffix;	item.Hue = 0x495;	break;
					case 12: item.Name = "amethyst " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "amethyst", "classic", 0 );	break;
					case 13: item.Name = "golden " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 );		break;
					case 14: item.Name = "copper " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 );		break;
					case 15: item.Name = "bronze " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 );		break;
					case 16: item.Name = "agapite " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 );	break;
					case 17: item.Name = "verite " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 );		break;
					case 18: item.Name = "valorite " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 );	break;
					case 19: item.Name = "steel " + suffix;		item.Hue = MaterialInfo.GetMaterialColor( "steel", "classic", 0 );		break;
					case 20: item.Name = "brass " + suffix;		item.Hue = MaterialInfo.GetMaterialColor( "brass", "classic", 0 );		break;
					case 21: item.Name = "mithril " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 );	break;
					case 22: item.Name = "nepturite " + suffix;	item.Hue = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 );	break;
				}
			}

			return newName;
		}

		public static void CheckMorph( Mobile from )
		{
			if ( from is EpicCharacter || from is Citizens )
				return;

			if ( CheckGargoyle( from ) )
				return;

			if ( CheckRavendark( from ) )
				return;

			if ( CheckNecromancer( from ) )
				return;

			if ( CheckBarbarian( from ) )
				return;

			if ( CheckOrk( from ) )
				return;

			if ( CheckPirate( from ) )
				return;

			CheckElf( from );
		}

		public static bool CheckOrk( Mobile from )
		{
			if ( from is OrkMonks || from is OrkRogue || from is OrkMage || from is OrkWarrior )
			{
				TurnToOrk( from );
				return true;
			}

			Map map = from.Map;

			if ( map != Map.TerMur )
				return false;

			if ( from.Region.IsPartOf( "the Cimmeran Mines" ) )
				return false;

			if ( from.Region.IsPartOf( "the Enchanted Pass" ) )
				return false;

			if ( from.Region.IsPartOf( "the Hedge Maze" ) )
				return false;

				TurnToOrk( from );

			return true;
		}

		public static bool CheckElf( Mobile from )
		{
			Map map = from.Map;

			if ( ( map != Map.Felucca ) && ( !from.Region.IsPartOf( "the Enchanted Pass" ) ) )
				return false;

			// BARD'S TALE
			if ( from.Region.IsPartOf( typeof( BardTownRegion ) ) )
				return false;

			if ( 
				from.Region.IsPartOf( "the Ethereal Plane" ) || 
				from.Region.IsPartOf( "the Ranger Outpost" ) || 
				from.Region.IsPartOf( "the Glowing Pond" ) || 
				from.Region.IsPartOf( "the Altar of Golden Rangers" ) || 
				from.Region.IsPartOf( "the Weary Camper Tavern" )
			)
				return false;

				TurnToElf( from );

			return true;
		}

		public static bool CheckGargoyle( Mobile from )
		{
			Map map = from.Map;

			if ( map != Map.Malas )
				return false;

				TurnToGargoyle( from );

			return true;
		}

		public static bool CheckBarbarian( Mobile from )
		{
			Map map = from.Map;

			if ( ( map != Map.Tokuno ) && ( !from.Region.IsPartOf( "the Cimmeran Mines" ) ) )
				return false;

			if ( from.Region.IsPartOf( "the Forgotten Lighthouse" ) )
				return false;

				TurnToBarbarian( from );

			return true;
		}

		public static bool CheckPirate( Mobile from )
		{
			if ( !from.Region.IsPartOf( "the Forgotten Lighthouse" ) )
				return false;

				TurnToPirate( from );

			return true;
		}

		public static bool CheckNecromancer( Mobile from )
		{
			Map map = from.Map;

			if ( 
				( from.Region.IsPartOf( "the Undercity of Umbra" ) || from.Region.IsPartOf( "the Black Magic Guild" ) || from.Region.IsPartOf( "the Island of Dracula" ) || from.Region.IsPartOf( "the Village of Ravendark" ) || from.Region.IsPartOf( "Ravendark Woods" ) 
				) && ( from.Hue != 0x83E8 ) )
			{
				TurnToNecromancer( from );
				return true;
			}

			return false;
		}


		public static bool CheckNecro( Mobile from )
		{
			Map map = from.Map;


			if ( from.Region.IsPartOf( "the Undercity of Umbra" ) || from.Region.IsPartOf( "the Black Magic Guild" ) || from.Region.IsPartOf( "the Island of Dracula" ) || from.Region.IsPartOf( "the Village of Ravendark" ) || from.Region.IsPartOf( "Ravendark Woods" ) )
				return true;


			return false;
		}


		public static bool CheckRavendark( Mobile from )
		{
			Map map = from.Map;

			if ( from.Region.IsPartOf( typeof( NecromancerRegion ) ) )
			{
				if ( from is Citizens ){ TurnToNecromancer( from ); }
				else { TurnToRavendark( from ); }
				return true;
			}

			return false;
		}

		public static int GetRandomNecromancerHue()
		{
			return Utility.RandomList(1476, 2342, 2056, 2944, 2817, 2915, 2906, 2875, 1790, 1779, 1909, 2085, 2092, 2089, 2796, 2338, 2380, 1989, 2845, 2379, 1484, 1489, 1995, 2167, 2928, 1470, 1939, 2227, 1141, 1157, 1158, 1175, 1254, 1509, 2118, 2224, 1105, 0xB80, 0xB5E, 0xB39, 0xB3A, 0xA9F, 0x99E, 0x997, 0x8D9, 0x8DA, 0x8DB, 0x8DC, 0x8B9, 2117, 2118, 1640, 1641, 1642, 1643, 1644, 1645, 1650, 1651, 1652, 1653, 1654, 1157, 1194, 2401, 2412);
		}

		public static void TurnToNecromancer( Mobile from )
		{
			int mainColor = GetRandomNecromancerHue();
			int armorColor = GetRandomNecromancerHue();
			int hairColor = Utility.RandomList( 0, 0x497 );

			if ( !(from is TownGuards) )
			{
				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is BaseShoes )
						item.Hue = GetRandomNecromancerHue();
					else if ( item is BaseClothing )
						item.Hue = mainColor;
					else if ( item is BaseArmor )
						item.Hue = armorColor;
					else if ( item is BaseWeapon || item is BaseTool )
						item.Hue = GetRandomNecromancerHue();
				}
			}

			from.HairHue = hairColor;
			from.FacialHairHue = hairColor;

			from.Hue = 0x83E8;
		}

		public static void TurnToRavendark( Mobile from )
		{
			RemoveMyClothes( from );

			int color = 0x497;

			switch ( Utility.Random( 7 ) )
			{
				case 0: from.AddItem( new NecromancerRobe( color ) ); break;
				case 1: from.AddItem( new AssassinRobe( color ) ); break;
				case 2: from.AddItem( new MagistrateRobe( color ) ); break;
				case 3: from.AddItem( new OrnateRobe( color ) ); break;
				case 4: from.AddItem( new SorcererRobe( color ) ); break;
				case 5: from.AddItem( new SpiderRobe( color ) ); break;
				case 6: from.AddItem( new VagabondRobe( color ) ); break;
			}

			switch ( Utility.Random( 3 ) )
			{
				case 0: from.AddItem( new ClothHood( color ) ); break;
				case 1: from.AddItem( new ClothCowl( color ) ); break;
				case 2: from.AddItem( new FancyHood( color ) ); break;
			}

			from.AddItem( new Boots() );
			from.HairHue = 0;
			from.FacialHairHue = 0;
			from.HairItemID = 0;
			from.FacialHairItemID = 0;
			from.Hue = 0;
			//from.Blessed = true;
			from.NameHue = Utility.RandomOrangeHue();
		}

		public static void TurnToBarbarian( Mobile from )
		{
			for ( int i = 0; i < from.Items.Count; ++i )
			{
				Item item = from.Items[i];

				if ( item is Hair || item is Beard )
				{
					item.Hue = 0x455;
				}
				else if ( ( ( item is BasePants ) || ( item is BaseOuterLegs ) ) && ( !(from is TownGuards) ) )
				{
					item.Delete();
					from.AddItem( new Kilt(Utility.RandomYellowHue()) );
				}
				else if ( ( item is BaseClothing || item is BaseWeapon || item is BaseArmor || item is BaseTool ) && ( !(from is TownGuards) ) )
				{
					item.Hue = Utility.RandomYellowHue();
				}
			}

			from.HairHue = 0x455;
			from.FacialHairHue = 0x455;

			if ( from.Female )
			{
				from.Name = NameList.RandomName( "barb_female" );
			}
			else
			{
				from.Name = NameList.RandomName( "barb_male" );
			}
		}

		public static void TurnToOrk( Mobile from )
		{
			if ( from.Hue == 0x1C4 || from.Hue == 0x1C5 || from.Hue == 0x1C6 || from.Hue == 0x1C7 || from.Hue == 0x1C9 || from.Hue == 0x1CA || from.Hue == 0x1CB || from.Hue == 0x1CC || from.Hue == 0x1CE || from.Hue == 0x1CF || from.Hue == 0x1D0 || from.Hue == 0x1D1 )
			{
				// THEY ARE ALREADY AN ORK
			}
			else
			{
				if ( !( from is TownGuards || from is OrkMonks || from is OrkRogue || from is OrkMage || from is OrkWarrior ) )
				{
					for ( int i = 0; i < from.Items.Count; ++i )
					{
						Item item = from.Items[i];

						if ( item is BaseClothing || item is BaseWeapon || item is BaseArmor || item is BaseTool )
							item.Hue = Utility.RandomYellowHue();
					}
				}

				from.Hue = Utility.RandomList( 0x1C4, 0x1C5, 0x1C6, 0x1C7, 0x1C9, 0x1CA, 0x1CB, 0x1CC, 0x1CE, 0x1CF, 0x1D0, 0x1D1 );
				from.BaseSoundID = 0x45A;
				from.HairItemID = 0;
				from.FacialHairItemID = 0;

				if ( from.Female )
				{
					from.Name = NameList.RandomName( "ork_female" );
				}
				else
				{
					from.Name = NameList.RandomName( "ork_male" );
				}

				if ( from.Region.IsPartOf( "the Azure Castle" ) )
				{
					from.Title = from.Title.Replace("the ork ", "");
				}
				else if ( from.Title != null && from.Title != "" )
				{
					from.Title = from.Title.Replace("the ork ", "the ");
					from.Title = from.Title.Replace("the ", "the ork ");
				}

				if ( from.FindItemOnLayer( Layer.Helm ) != null && !(from is TownGuards) ) { (from.FindItemOnLayer( Layer.Helm )).Delete(); }

				if ( !(from is TownGuards) )
				{
					Item helm = new WornHumanDeco();
						helm.Name = "orkish face";
						helm.ItemID = 0x141B;
						helm.Hue = from.Hue;
						helm.Layer = Layer.Helm;
						from.AddItem( helm );
				}
			}
		}

		public static void TurnToPirate( Mobile from )
		{
			switch( Utility.RandomMinMax( 1, 3 ) )
			{
				case 1: from.AddItem( new SkullCap(Utility.RandomYellowHue()) );	break;
				case 2: from.AddItem( new TricorneHat(Utility.RandomYellowHue()) );	break;
				case 3: from.AddItem( new PirateHat(Utility.RandomYellowHue()) );	break;
			}
		}

		public static void TurnToElf( Mobile from )
		{
			for ( int i = 0; i < from.Items.Count; ++i )
			{
				Item item = from.Items[i];

				if ( item is Hair || item is Beard )
					item.Delete();
			}

			from.Race = Race.Elf;

			int hairHue = Utility.RandomHairHue();
			Utility.AssignRandomHair( from, hairHue );
			from.FacialHairItemID = 0;
			from.Hue = Server.Misc.RandomThings.GetRandomSkinColor(); 

			if ( from.Female )
			{
				from.Name = NameList.RandomName( "elf_female" );
				from.Body = 606;
			}
			else
			{
				from.Name = NameList.RandomName( "elf_male" );
				from.Body = 605;
			}

			if ( from.Title != null && from.Title != "" )
			{
				from.Title = from.Title.Replace("the elf ", "the ");
				from.Title = from.Title.Replace("the ", "the elf ");
			}
		}

		public static void TurnToGargoyle( Mobile from )
		{
			if ( from.Hue != 0x845 )
			{
				int colorClothes = Utility.RandomMinMax( 1, 3 );

				if ( !( from is TownGuards ) )
				{
					for ( int i = 0; i < from.Items.Count; ++i )
					{
						Item item = from.Items[i];

						if ( item is BaseClothing || item is BaseWeapon || item is BaseArmor || item is BaseTool )
						{
							if ( colorClothes == 1 ){ item.Hue = Utility.RandomBlueHue(); }
							else if ( colorClothes == 2 ){ item.Hue = Utility.RandomRedHue(); }
							else { item.Hue = Utility.RandomGreenHue(); }
						}
					}
				}

				from.Hue = 0x845;
				from.Name = NameList.RandomName( "gargoyle name" );
				from.BaseSoundID = 372;
				from.HairItemID = 0;
				from.FacialHairItemID = 0;

				if ( from.Female )
				{
					from.Name = NameList.RandomName( "ork_female" );
				}
				else
				{
					from.Name = NameList.RandomName( "ork_male" );
				}

				if ( from.Title != null && from.Title != "" )
				{
					from.Title = from.Title.Replace("the gargoyle ", "the ");
					from.Title = from.Title.Replace("the ", "the gargoyle ");
				}

				if ( from.FindItemOnLayer( Layer.Cloak ) != null ) { (from.FindItemOnLayer( Layer.Cloak )).Delete(); }
				if ( from.FindItemOnLayer( Layer.Helm ) != null && !(from is TownGuards) ) { (from.FindItemOnLayer( Layer.Helm )).Delete(); }
				if ( from.FindItemOnLayer( Layer.OuterTorso ) != null )
				{
					(from.FindItemOnLayer( Layer.OuterTorso )).Delete();
					Item robe = new WornHumanDeco();
						robe.Name = "gargoyle robe";
						robe.ItemID = 0x1F03;
						robe.Hue = Utility.RandomBlueHue();
						robe.Layer = Layer.MiddleTorso;
						from.AddItem( robe );
				}

				if ( !(from is TownGuards) )
				{
					Item helm = new WornHumanDeco();
						helm.Name = "gargoyle head";
						helm.ItemID = 0x2B72;
						helm.Hue = from.Hue;
						helm.Layer = Layer.Helm;
						from.AddItem( helm );
				}

				Item wings = new WornHumanDeco();
					wings.Name = "gargoyle wings";
					wings.ItemID = 0x2FC5;
					wings.Hue = from.Hue;
					wings.Layer = Layer.Cloak;
					from.AddItem( wings );
			}
		}

		public static void TurnToSomethingOnDeath( Mobile from )
		{
			if (from is BaseCreature && AdventuresFunctions.IsPuritain((object)from) && from.Body.IsHuman)
			{
				if (((BaseCreature)from).midrace == 2) // garg
					from.Body = 4; // GARGOYLE
				if (((BaseCreature)from).midrace == 3) // lizard
					from.Body = 35; 
				if (((BaseCreature)from).midrace == 4) // pirate
					from.Body = Utility.RandomList( 57, 168, 170, 327 );
				if (((BaseCreature)from).midrace == 3) // orc
					from.Body = 17; 
			
			}
			else if ( from.Hue == 0x1C4 || from.Hue == 0x1C5 || from.Hue == 0x1C6 || from.Hue == 0x1C7 || from.Hue == 0x1C9 || from.Hue == 0x1CA || from.Hue == 0x1CB || from.Hue == 0x1CC || from.Hue == 0x1CE || from.Hue == 0x1CF || from.Hue == 0x1D0 || from.Hue == 0x1D1 )
			{
				from.Body = 17; // ORC
			}
			else if ( from.Hue == 0x845 )
			{
				from.Body = 4; // GARGOYLE
			}
		}

		public static void CapitalizeTitle( Mobile from )
		{
			string title = from.Title;

			if ( title == null )
				return;

			string[] split = title.Split( ' ' );

			for ( int i = 0; i < split.Length; ++i )
			{
				if ( Insensitive.Equals( split[i], "the" ) )
					continue;

				if ( split[i].Length > 1 )
					split[i] = Char.ToUpper( split[i][0] ) + split[i].Substring( 1 );
				else if ( split[i].Length > 0 )
					split[i] = Char.ToUpper( split[i][0] ).ToString();
			}

			from.Title = String.Join( " ", split );
		}
	}
}