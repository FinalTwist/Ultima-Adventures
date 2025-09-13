using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class LearnWoodBook : Item
	{
		[Constructable]
		public LearnWoodBook( ) : base( 0x4C5E )
		{
			Weight = 1.0;
			Name = "Scroll of Various Wood";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "A Listing of Wood" );
		}

		public class LearnWoodBookGump : Gump
		{
			public LearnWoodBookGump( Mobile from ): base( 25, 25 )
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

				AddItem(717, 121, 7980);
				AddItem(409, 129, 4148);
				AddItem(506, 124, 4142);
				AddItem(602, 126, 4138);
				AddItem(308, 114, 3670);
				AddItem(808, 98, 1928);
				AddItem(831, 121, 1928);
				AddItem(807, 83, 4528);

				AddHtml( 170, 70, 600, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>INFORMATION ON VARIOUS TYPES OF WOOD</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int i = 135;
				int o = 32;

				AddItem(100, i, 7137, 0);
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Regular</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "ash", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ash</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "cherry", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cherry</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "ebony", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ebony</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "golden oak", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Golden Oak</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "hickory", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hickory</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "mahogany", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mahogany</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "oak", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Oak</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "pine", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pine</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "ghostwood", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ghostwood</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "rosewood", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Rosewood</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "walnut", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Walnut</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "petrified", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Petrified</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "driftwood", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Driftwood</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 7137, MaterialInfo.GetMaterialColor( "elven", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Elven</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddHtml( 303, 198, 573, 466, @"<BODY><BASEFONT Color=#FCFF00><BIG><br>Lumberjacking is a task carried out long before the mining of ore. You simply need to get an axe, double-click it, and then target a tree to begin chopping. Although you will normally get regular wood, you will eventually get skilled enough to chop other types of wood. With wood you can make arrows, bows, and crossbows with the fletching skill. You can also make furniture, weapons, and armor with the carpentry skill.<br><br>The many types of wood are listed here, starting up and then going down to higher quality wood. Making a shield out of walnut will be a much better shield than one made of ash, for example. The same goes for bows, crossbows, and other weapons made of wood. Whatever the color of wood you use, the weapon, armor, or instrument will retain the color of the wood. The same goes for many of the furniture and containers you can make from wood. A wooden chest made from cherry wood will be red in color.<br><br>In order to make things from the wood, you need to turn the logs into boards. To do this, double-click the logs and target a saw mill. These mills are commonly found in carpenter shops. Then you can begin crafting with a carpentry tool, or fletching with bowyer tools.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
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
				e.CloseGump( typeof( LearnWoodBookGump ) );
				e.SendGump( new LearnWoodBookGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnWoodBook(Serial serial) : base(serial)
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
			Name = "Scroll of Various Wood";
			ItemID = 0x4C5E;
		}
	}
}