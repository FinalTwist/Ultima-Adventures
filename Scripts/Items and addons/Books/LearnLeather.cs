using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class LearnLeatherBook : Item
	{
		[Constructable]
		public LearnLeatherBook( ) : base( 0x4C60 )
		{
			Weight = 1.0;
			Name = "Scroll of Various Leather";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "A Listing Of Leather" );
		}

		public class LearnLeatherGump : Gump
		{
			public LearnLeatherGump( Mobile from ): base( 25, 25 )
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

				AddItem(717, 121, 5068);
				AddItem(409, 129, 3921);
				AddItem(506, 124, 3998);
				AddItem(616, 120, 19585);
				AddItem(308, 114, 4216);
				AddItem(833, 109, 4219);

				AddHtml( 170, 70, 600, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>INFORMATION ON VARIOUS TYPES OF LEATHER</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int i = 135;
				int o = 42;

				AddItem(100, i, 4199, 0);
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Regular</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "deep sea", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Deep Sea</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "lizard", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Lizard</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "serpent", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Serpent</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "necrotic", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Necrotic</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "volcanic", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Volcanic</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "frozen", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Frozen</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "goliath", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Goliath</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "draconic", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Draconic</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "hellish", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hellish</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "dinosaur", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dinosaur</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 4199, MaterialInfo.GetMaterialColor( "alien", "", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Alien</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddHtml( 303, 198, 573, 466, @"<BODY><BASEFONT Color=#FCFF00><BIG><br>There are various types of hides you may acquire from skinning creatures throughout the land. For some examples - snakes have serpent hides, lizardmen have lizard hides, sea serpents have deep sea hides, polar bears have frozen hides, lava lizards have volcanic hides, dragons have draconic hides, zombies have necrotic hides, demons have hellish hides, and giants have goliath hides. Each type of hide is different in color, but also has various levels of protection when creating clothing with it.<br><br>The many types of leather are listed here, starting up and then going down to higher quality leather. Making a tunic out of draconic leather will be a much better tunic than one made of lizard leather, for example. <br><br>A tailor would need a certain skill level to work with each of these types of leather. Hides can be obtained from skinning certain creatures by double clickin a bladed weapon and then targeting a corpse. These hides can then be cut with scissors and made into sheets of leather. Then a sewing kit can be used to craft the leather into various armor and bags.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
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
				e.CloseGump( typeof( LearnLeatherGump ) );
				e.SendGump( new LearnLeatherGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnLeatherBook(Serial serial) : base(serial)
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
			Name = "Scroll of Various Leather";
			ItemID = 0x4C60;
		}
	}
}