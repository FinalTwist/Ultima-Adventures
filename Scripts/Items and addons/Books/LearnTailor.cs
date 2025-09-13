using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class LearnTailorBook : Item
	{
		[Constructable]
		public LearnTailorBook( ) : base( 0x4C5E )
		{
			Weight = 1.0;
			Name = "Scroll of Tailoring";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "A Basic Guide to Tailoring" );
		}

		public class LearnTailorGump : Gump
		{
			public LearnTailorGump( Mobile from ): base( 25, 25 )
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

				AddHtml( 187, 80, 316, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>BASIC GUIDE TO TAILORING</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 88, 123, 402, 372, @"<BODY><BASEFONT Color=#FCFF00><BIG>You can sheer sheep with a bladed weapon to get wool by double clicking the weapon and then targeting the sheep. You can also find gardens that grow cotton and flax. You can gather these by double clicking the plants and then the plants will be placed in your pack. Once gathered, you can use them on a spinning wheel to make yarn and spools of thread.<br><br>Once you have the yarn or thread, you can use those on a loom to make bolts of cloth by double clicking the spool or thread and then selecting the loom. Taking scissors to these bolts of cloth will produce sheets of cloth that you can then use with a sewing kit to make clothing. You can also cut the cloth down further into bandages if you need them.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(516, 371, 4117);
				AddItem(531, 430, 4192);
				AddItem(443, 520, 19585);
				AddItem(305, 508, 3990);
				AddItem(503, 470, 4191);
				AddItem(463, 525, 3998);
				AddItem(393, 523, 3576);
				AddItem(356, 523, 3567);
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
				e.CloseGump( typeof( LearnTailorGump ) );
				e.SendGump( new LearnTailorGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnTailorBook(Serial serial) : base(serial)
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
			Name = "Scroll of Tailoring";
			ItemID = 0x4C5E;
		}
	}
}