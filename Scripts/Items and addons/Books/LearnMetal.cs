using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class LearnMetalBook : Item
	{
		[Constructable]
		public LearnMetalBook( ) : base( 0x4C5B )
		{
			Weight = 1.0;
			Name = "Scroll of Various Metals";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "A Listing Of Metals" );
		}

		public class LearnMetalGump : Gump
		{
			public LearnMetalGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);

				AddImage(0, 430, 155);
				AddImage(300, 430, 155);
				AddImage(600, 430, 155);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(600, 0, 155);
				AddImage(600, 300, 155);

				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(302, 298, 129);
				AddImage(598, 298, 129);

				AddImage(6, 7, 133);
				AddImage(230, 46, 132);
				AddImage(530, 46, 132);
				AddImage(680, 7, 134);
				AddImage(598, 428, 129);
				AddImage(298, 428, 129);
				AddImage(2, 428, 129);
				AddImage(5, 484, 142);
				AddImage(329, 693, 140);
				AddImage(573, 693, 140);
				AddImage(856, 695, 143);

				AddItem(839, 111, 4017);
				AddItem(351, 112, 3898);
				AddItem(572, 126, 5092);
				AddItem(655, 123, 7865);
				AddItem(308, 114, 6585);
				AddItem(730, 117, 4015);
				AddItem(502, 126, 3718);

				AddHtml( 170, 70, 600, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>INFORMATION ON VARIOUS TYPES OF METAL</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int i = 115;
				int o = 32;

				AddItem(100, i, 7153, 0);
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Iron</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "dull copper", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dull Copper</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Shadow Iron</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "copper", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Copper</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "bronze", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bronze</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "gold", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Gold</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "agapite", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Agapite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "verite", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Verite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "valorite", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Valorite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "nepturite", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Nepturite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "obsidian", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Obsidian</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "steel", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Steel</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "brass", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Brass</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "mithril", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mithril</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "xormite", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Xormite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7153, MaterialInfo.GetMaterialColor( "dwarven", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dwarven</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddHtml( 303, 198, 573, 466, @"<BODY><BASEFONT Color=#FCFF00><BIG><br>Mining is the skill one needs to find ore within caves and mountains. With this ore, ingot can be crafted and then blacksmiths and tinkers can then create weapons, armor, and tools. You simply need to get a pick axe or a shovel, double-click it, and then target a mountain side or caven floor. Although you will normally get regular ore, you will eventually get skilled enough to dig up other types of ore.<br><br>The many types of metal are listed here, starting up and then going down to higher quality metal. Making a shield out of valorite will be a much better shield than one made of copper, for example. The same goes for weapons forged from metal. Whatever the color of metal you use, the weapon or armor will retain the color of the metal. The same goes for many of the containers you can make from metal. A metal chest made from shadow iron will be black in color.<br><br>In order to make things from the ore, you need to turn the ore into ingots. To do this, double-click the ingots and target a forge. These forges are commonly found in blacksmith shops. Then you can begin crafting with a blacksmith hammer, or tinker with tinker tools. Keep in mind that in order to smith items, you will need to be near a forge and anvil.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( !IsChildOf( e.Backpack ) ) 
			{
				e.SendMessage( "This must be in your backpack to read." );
			}
			else
			{
				e.CloseGump( typeof( LearnMetalGump ) );
				e.SendGump( new LearnMetalGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnMetalBook(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Name = "Scroll of Various Metals";
			ItemID = 0x4C5B;
		}
	}
}