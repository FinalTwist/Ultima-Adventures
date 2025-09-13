using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;
using System.Linq;

namespace Server.Items
{
	public class MaterialLiquifier : Item
	{
		public int ItemCharges;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Item_Charges { get { return ItemCharges; } set { ItemCharges = value; InvalidateProperties(); } }

		[Constructable]
		public MaterialLiquifier() : base( 0x4C13 )
		{
			Weight = 5.0;
			Name = "material liquifier";
			Light = LightType.Circle150;
			ItemCharges = Utility.RandomMinMax( 10, 20 );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060741, ItemCharges.ToString() );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendSound( 0x54D );
				from.CloseGump( typeof( MaterialLiquifierGump ) );
				from.SendGump( new MaterialLiquifierGump( from ) );
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			from.PlaySound( 0x55B );
			from.RevealingAction();

			if ( from.Backpack.FindItemByType( typeof ( Bottle ) ) == null )
			{
				from.SendMessage( "You require a bottle." );
				return false;
			}
			else if ( dropped is SpaceDyes )
			{
				from.SendMessage( "The item has been destroyed." );
			}
			else if ( !GetColor( dropped, from ) )
			{
				from.SendMessage( "The item has been destroyed." );
			}

			ConsumeCharge( from );
			dropped.Delete();

			return false;
		}

		private static readonly List<string> ExtractableMaterials = new List<string>
		{
			"Beskar",
			"Carbonite",
			"Phrik",
			"Cortosis",
			"Songsteel",
			"Agrinium",
			"Durasteel",
			"Titanium",
			"Laminasteel",
			"Neuranium",
			"Promethium",
			"Quadranium",
			"Durite",
			"Farium",
			"Trimantium",
			"Xonolite",

			"Veshok",
			"Cosian",
			"Greel",
			"Teej",
			"Kyshyyyk",
			"Laroon",
			"Borl",
			"Japor",

			"Adesote",
			"Nylonite",
			"Biomesh",
			"Cerlin",
			"Polyfiber",
			"Durafiber",
			"Nylar",
			"Syncloth",
			"Hypercloth",
			"Flexicris",
			"Thermoweave",

			"Twi'lek",
			"Rodian",
			"Martian",
			"Cardassian",
			"Xindi",
			"Tusken",
			"Andorian",
			"Zabrak",
		};

		private static readonly Dictionary<string, int> ExplicitItems = new Dictionary<string, int>
		{
			{ "biohazard hood", 0xBA1 },
			{ "biohazard suit", 0xBA1 },
			{ "hazmat hood", 0x93D },
			{ "hazmat suit", 0x93D },
			{ "radiation hood", 0xBAD },
			{ "radiation suit", 0xBAD },
			{ "lab coat", 0xBB4 },
		};

		private static readonly Dictionary<int, string> DyeMap = new Dictionary<int, string>
		{
			{ 0x6F6, "Rodian Green Dye" },
			{ 0x6F8, "Veshok Gray Dye" },
			{ 0x701, "Zabrak Red Dye" },
			{ 0x705, "Kyshyyyk Gold Dye" },
			{ 0x776, "Tusken Yellow Dye" },
			{ 0x77F, "Martian Green Dye" },
			{ 0x7A9, "Durasteel Gray Dye" },
			{ 0x825, "Andorian Blue Dye" },
			{ 0x829, "Carbonite Gray Dye" },
			{ 0x82C, "Cortosis Purple Dye" },
			{ 0x870, "Neuranium Red Dye" },
			{ 0x877, "Xindi Gray Dye" },
			{ 0x8C1, "Agrinium Gray Dye" },
			{ 0x8D7, "Titanium Blue Dye" },
			{ 0xAF8, "Twi'lek Purple Dye" },
			{ 0xB42, "Songsteel White Dye" },

			// For Custom Items
			{ 0xBA1, "Biohazard Green Dye" },
			{ 0x93D, "Hazmat Orange Dye" },
			{ 0xBAD, "Radiation Yellow Dye" },
			{ 0xBB4, "Lab Coat White Dye" },
		};

		public static bool GetColor( Item item, Mobile from )
		{
			if (from == null)
				return false;
			if (item == null || from.Backpack == null )
			{
				from.SendMessage( "There's a problem with this item." );
				return false;
			}

			if (item.Name == null)
				item.Name = "";

			int color = 0;
			if (!ExplicitItems.TryGetValue(item.Name.ToLower(), out color))
			{
				string material = ExtractableMaterials.FirstOrDefault(m => item.Name.StartsWith(m));
				if (string.IsNullOrWhiteSpace(material)) { material = ExtractableMaterials.FirstOrDefault(m => item.Name.Contains(m)); }
				if (string.IsNullOrWhiteSpace(material)) { return false; }

				color = Server.Misc.MaterialInfo.GetSpaceAceColors( material );
			}

			string name;
			if (!DyeMap.TryGetValue(color, out name)) return false;

			from.RevealingAction();
			from.PlaySound( 0x23E );
			Item bottle = from.Backpack.FindItemByType( typeof ( Bottle ) );
			
			if (bottle == null )
			{
				from.SendMessage( "Do you have a bottle in your pack?" );
				return false;
			}
			if ( bottle.Amount > 1 ){ bottle.Amount = bottle.Amount - 1; } else { bottle.Delete(); }
			
			from.SendMessage( "You place a vial of " + name + " in your pack." );
			SpaceDyes vial = new SpaceDyes();
			vial.Name = name;
			vial.Hue = color;
			
			from.AddToBackpack( vial );

			return true;
		}

		public void ConsumeCharge( Mobile from )
		{
			if ( --ItemCharges < 1 )
			{
				from.SendLocalizedMessage( 1019073 ); // This item is out of charges.
				this.Delete();
			}
			else
			{
				InvalidateProperties();
			}
		}

		public MaterialLiquifier( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
			writer.Write( ItemCharges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			ItemCharges = reader.ReadInt();
		}

		public class MaterialLiquifierGump : Gump
		{
			public MaterialLiquifierGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 1242);
				AddItem(328, 33, 19475);
				AddHtml( 44, 41, 200, 20, @"<BODY><BASEFONT Color=#00FF06>MATERIAL LIQUIFIER</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 48, 76, 305, 186, @"<BODY><BASEFONT Color=#00FF06>This device is not only strange from its eerie glow, but from what it does to items you put in it. If you put any space age materials in it, they will be melted into a vial of liquid that matches the material's natural color. Make sure you have an empty bottle to put the liquid into, or the item you place in it will be wasted. This liquid can be used to coat an item of your choice, where it will become the color of the liquefied material. Space age materials are those that come from the stars, and they are found in forms of strange metals, woods, and cloths. Some examples of such metals are carbonite, durasteel, titanium, and promethium. Some examples of such wood are veshok, greel, and kyshyyyk. Lastly, we have cloth in the forms of nylonite, polyfiber, and nylar as a few examples. This device could also liquefy alien bones in the same manner. Some of the races that were crew members of this ship are twi'lek, rodian, xindi, and zabrak. Any other item one puts in this device will simply be destroyed, so be careful what you do with this device.</BASEFONT></BODY>", (bool)false, (bool)true);
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile;
				from.SendSound( 0x54D );
			}
		}
	}
}