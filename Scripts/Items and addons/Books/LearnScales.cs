using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class LearnScalesBook : Item
	{
		[Constructable]
		public LearnScalesBook( ) : base( 0x4C60 )
		{
			Weight = 1.0;
			Name = "Scroll of Reptile Scales";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "A Listing Of Reptile Scales" );
		}

		public class LearnScalesGump : Gump
		{
			public LearnScalesGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 298, 129);
				AddImage(265, 564, 140);
				AddImage(19, 23, 161);
				AddImage(275, 46, 132);
				AddImage(7, 355, 142);
				AddImage(7, 8, 150);
				AddImage(556, 566, 143);
				AddImage(20, 318, 157);
				AddImage(362, 7, 138);
				AddImage(246, 43, 159);
				AddImage(202, 32, 143);

				AddHtml( 187, 80, 316, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>TYPES OF REPTILE SCALES</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 88, 123, 402, 372, @"<BODY><BASEFONT Color=#FCFF00><BIG>Use a bladed item, like a dagger or knife, on a corpse by double-clicking the bladed item and then selecting the corpse. If there is something to be skinned from it, it will appear in their pack. If they have scales, then those will also appear in their pack.<br><br>Different types of scales can be found on many creatures like dragons and dinosaurs. You can use these scales to make different types of items by using a blacksmith hammer and making scalemail armor. There are 7 different types of scales:<br><br>-Red<br>-Yellow<br>-Black<br>-Green<br>-White<br>-Blue<br>-Dinosaur</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(514, 383, 9905, 1645);
				AddItem(514, 425, 9905, 2216);
				AddItem(514, 470, 9905, 1109);
				AddItem(514, 526, 9905, 2129);
				AddItem(457, 526, 9905, 2301);
				AddItem(392, 526, 9905, 2224);
				AddItem(327, 526, 9905, 1072);
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
				e.CloseGump( typeof( LearnScalesGump ) );
				e.SendGump( new LearnScalesGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnScalesBook(Serial serial) : base(serial)
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
			Name = "Scroll of Reptile Scales";
			ItemID = 0x4C60;
		}
	}
}