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
				from.SendMessage( "The item has been destroyed." );
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

		public static bool GetColor( Item item, Mobile from )
		{
			if (from == null)
				return false;
			if (item == null || from.Backpack == null )
			{
				from.SendMessage( "There's a problem with this item." );
				return false;
			}
from.SendMessage( "1" );
			if (item.Name == null)
				item.Name = "";

			bool machineWorked = false;
			string name = "";
			int color = 0;

			string material = "";

			if ( (item.Name).Contains("Beskar") ){ material = "Beskar"; }
			else if ( (item.Name).Contains("Carbonite") ){ material = "Carbonite"; }
			else if ( (item.Name).Contains("Phrik") ){ material = "Phrik"; }
			else if ( (item.Name).Contains("Cortosis") ){ material = "Cortosis"; }
			else if ( (item.Name).Contains("Songsteel") ){ material = "Songsteel"; }
			else if ( (item.Name).Contains("Agrinium") ){ material = "Agrinium"; }
			else if ( (item.Name).Contains("Durasteel") ){ material = "Durasteel"; }
			else if ( (item.Name).Contains("Titanium") ){ material = "Titanium"; }
			else if ( (item.Name).Contains("Laminasteel") ){ material = "Laminasteel"; }
			else if ( (item.Name).Contains("Neuranium") ){ material = "Neuranium"; }
			else if ( (item.Name).Contains("Promethium") ){ material = "Promethium"; }
			else if ( (item.Name).Contains("Quadranium") ){ material = "Quadranium"; }
			else if ( (item.Name).Contains("Durite") ){ material = "Durite"; }
			else if ( (item.Name).Contains("Farium") ){ material = "Farium"; }
			else if ( (item.Name).Contains("Trimantium") ){ material = "Trimantium"; }
			else if ( (item.Name).Contains("Xonolite") ){ material = "Xonolite"; }

			else if ( (item.Name).Contains("Veshok") ){ material = "Veshok"; }
			else if ( (item.Name).Contains("Cosian") ){ material = "Cosian"; }
			else if ( (item.Name).Contains("Greel") ){ material = "Greel"; }
			else if ( (item.Name).Contains("Teej") ){ material = "Teej"; }
			else if ( (item.Name).Contains("Kyshyyyk") ){ material = "Kyshyyyk"; }
			else if ( (item.Name).Contains("Laroon") ){ material = "Laroon"; }
			else if ( (item.Name).Contains("Borl") ){ material = "Borl"; }
			else if ( (item.Name).Contains("Japor") ){ material = "Japor"; }

			else if ( (item.Name).Contains("Adesote") ){ material = "Adesote"; }
			else if ( (item.Name).Contains("Nylonite") ){ material = "Nylonite"; }
			else if ( (item.Name).Contains("Biomesh") ){ material = "Biomesh"; }
			else if ( (item.Name).Contains("Cerlin") ){ material = "Cerlin"; }
			else if ( (item.Name).Contains("Polyfiber") ){ material = "Polyfiber"; }
			else if ( (item.Name).Contains("Durafiber") ){ material = "Durafiber"; }
			else if ( (item.Name).Contains("Syncloth") ){ material = "Syncloth"; }
			else if ( (item.Name).Contains("Hypercloth") ){ material = "Hypercloth"; }
			else if ( (item.Name).Contains("Flexicris") ){ material = "Flexicris"; }
			else if ( (item.Name).Contains("Thermoweave") ){ material = "Thermoweave"; }
			else if ( (item.Name).Contains("Nylar") ){ material = "Nylar"; }

			else if ( (item.Name).Contains("Twi'lek") ){ material = "Twi'lek"; }
			else if ( (item.Name).Contains("Rodian") ){ material = "Rodian"; }
			else if ( (item.Name).Contains("Martian") ){ material = "Martian"; }
			else if ( (item.Name).Contains("Cardassian") ){ material = "Cardassian"; }
			else if ( (item.Name).Contains("Xindi") ){ material = "Xindi"; }
			else if ( (item.Name).Contains("Tusken") ){ material = "Tusken"; }
			else if ( (item.Name).Contains("Andorian") ){ material = "Andorian"; }
			else if ( (item.Name).Contains("Zabrak") ){ material = "Zabrak"; }
from.SendMessage( "2" );
			color = Server.Misc.MaterialInfo.GetSpaceAceColors( material );

			if ( color == 0x6F6 ){ name = "Rodian Green Dye"; machineWorked = true; }
			else if ( color == 0x6F8 ){ name = "Veshok Gray Dye"; machineWorked = true; }
			else if ( color == 0x701 ){ name = "Zabrak Red Dye"; machineWorked = true; }
			else if ( color == 0x705 ){ name = "Kyshyyyk Gold Dye"; machineWorked = true; }
			else if ( color == 0x776 ){ name = "Tusken Yellow Dye"; machineWorked = true; }
			else if ( color == 0x77F ){ name = "Martian Green Dye"; machineWorked = true; }
			else if ( color == 0x7A9 ){ name = "Durasteel Gray Dye"; machineWorked = true; }
			else if ( color == 0x825 ){ name = "Andorian Blue Dye"; machineWorked = true; }
			else if ( color == 0x829 ){ name = "Carbonite Gray Dye"; machineWorked = true; }
			else if ( color == 0x82C ){ name = "Cortosis Purple Dye"; machineWorked = true; }
			else if ( color == 0x870 ){ name = "Neuranium Red Dye"; machineWorked = true; }
			else if ( color == 0x877 ){ name = "Xindi Gray Dye"; machineWorked = true; }
			else if ( color == 0x8C1 ){ name = "Agrinium Gray Dye"; machineWorked = true; }
			else if ( color == 0x8D7 ){ name = "Titanium Blue Dye"; machineWorked = true; }
			else if ( color == 0xAF8 ){ name = "Twi'lek Purple Dye"; machineWorked = true; }
			else if ( color == 0xB42 ){ name = "Songsteel White Dye"; machineWorked = true; }
from.SendMessage( "3" );
			if ( machineWorked )
			{
				from.RevealingAction();
				from.PlaySound( 0x23E );
				Item bottle = from.Backpack.FindItemByType( typeof ( Bottle ) );
				from.SendMessage( "4" );
				if (bottle == null )
				{
					from.SendMessage( "Do you have a bottle in your pack?" );
					return false;
				}
				if ( bottle.Amount > 1 ){ bottle.Amount = bottle.Amount - 1; } else { bottle.Delete(); }
				from.SendMessage( "5" );
				from.SendMessage( "You place a vial of " + name + " in your pack." );
				SpaceDyes vial = new SpaceDyes();
				vial.Name = name;
				vial.Hue = color;
				vial.vialHue = color;
				from.SendMessage( "6" );
				from.AddToBackpack( vial );
			}
from.SendMessage( "7" );
			return machineWorked;
		}

		public void ConsumeCharge( Mobile from )
		{
			--ItemCharges;

			if ( ItemCharges < 1 )
			{
				from.SendLocalizedMessage( 1019073 ); // This item is out of charges.
				this.Delete();
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