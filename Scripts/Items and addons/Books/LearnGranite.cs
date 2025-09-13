using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class LearnGraniteBook : Item
	{
		[Constructable]
		public LearnGraniteBook( ) : base( 0x4C5C )
		{
			Weight = 1.0;
			Name = "Scroll of Sand and Stone";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "The Basics of Stone and Glass Crafting" );
		}

		public class LearnGraniteGump : Gump
		{
			public LearnGraniteGump( Mobile from ): base( 25, 25 )
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

				AddItem(575, 122, 2859);
				AddItem(346, 120, 3898);
				AddItem(663, 131, 3854);
				AddItem(834, 101, 4632);
				AddItem(308, 122, 6009);
				AddItem(752, 130, 4787);
				AddItem(486, 131, 3718);

				AddHtml( 170, 70, 600, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>CRAFTING WITH SAND AND STONE</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int i = 135;
				int o = 36;

				AddItem(100, i, 6011, 0);
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Regular</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dull Copper</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Shadow Iron</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "copper", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Copper</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bronze Oak</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "gold", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Gold</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Agapite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "verite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Verite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Valorite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Nepturite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Obsidian</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mithril</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Xormite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dwarven</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddHtml( 303, 198, 573, 466, @"<BODY><BASEFONT Color=#FCFF00><BIG><br>Mining is the skill one needs to find granite within caves and mountains. With this granite, stone crafters can make stone furniture and statues. You simply need to get a pick axe or a shovel, double-click it, and then target a mountain side or caven floor. Although you will normally get regular granite, you will eventually get skilled enough to dig up other types of granite.<br><br>The many types of granite are listed here, but only their color makes them unique. So making a statue from shadow iron granite will make a blackened statue.<br><br>In order to make things from the granite, you need to first learn how to dig for it. Legends tell of the gargoyles, and how they can teach the likes of men these secrets. Then if you carpentry skill is good enough, you can begin crafting with a mallet and chisel.<br><br>Mining is also the skill one needs to find sand on beaches and desert sands. With this sand, glass blowers can make items such as bottles and jars. You simply need to get a pick axe or a shovel, double-click it, and then target a the sand at your feet. Sand comes in piles and an alchemist can use a blow pipe to create bottles, for example. This artful crafting is said to also be taught by the gargoyles.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
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
				e.CloseGump( typeof( LearnGraniteGump ) );
				e.SendGump( new LearnGraniteGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnGraniteBook(Serial serial) : base(serial)
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
			Name = "Scroll of Sand and Stone";
			ItemID = 0x4C5C;
		}
	}
}