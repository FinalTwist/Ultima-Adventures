using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class LearnStealingBook : Item
	{
		[Constructable]
		public LearnStealingBook( ) : base( 0x4C5C )
		{
			Weight = 1.0;
			Name = "The Art of Thievery";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "What To Steal For Better Profit" );
		}

		public class LearnStealingGump : Gump
		{
			public LearnStealingGump( Mobile from ): base( 25, 25 )
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

				AddHtml( 187, 80, 316, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>THE ART OF THIEVERY</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 88, 123, 402, 372, @"<BODY><BASEFONT Color=#FCFF00><BIG>For those skilled in the art of snooping and stealing, the search for ancient artifacts can be a profitable venture. Searching some of the crypts, tombs, and dungeons...you may find pedestals with ornately crafted boxes and bags that might contain something of great value. It may be a rare item, a fine piece of art, or an ancient weapon. The finely crafted bags and boxes can be kept for oneself, or they may be given to a thief in the guild where they will gladly pay 500 gold for each one. These are highly collectible and they have guild contacts to resell them to royalty, art dealers, or collectors. When you come across these pedestals, and there is an item upon it, double click it to attempt to steal the item. If you are not well trained in snooping, you may set off a deadly trap. Having a good trap removing skill may avoid the effects of such traps. Once the trap is avoided, then your skill in stealing will be put to the test. If you succeed at getting the item, look inside and claim your prize.<br><br>Many people in town are looking for rare artifacts, and may pay handsomely for them.<br><br>There are also footlockers, chests, bags, and boxes that contain treasure in these places. You can attempt to steal these containers. Make sure to take what you want from them before stealing them, as you will empty the container on your escape. A thief in the guild may also pay money for these containers by giving it to them, as they are also collectible to others and they may fetch a good price. If you want to take one of these dungeon containers, use your stealing skill and then target the container. Maybe you will be quick enough.<br><br>Although you can also seek gold by picking the pockets of merchants, you can also steal gold from their coffers. You can snoop the coffers to see how much gold is in it, and then you can use your stealing skill on the coffer to try and take the gold. This may practice your skill, but it is a tricky maneuver if you are caught.<br><br></BIG></BASEFONT></BODY>", (bool)false, (bool)true);

				AddItem(521, 398, 4643);
				AddItem(524, 500, 13042);
				AddItem(326, 516, 13111);
				AddItem(398, 527, 5373);
				AddItem(522, 504, 3712);
				AddItem(457, 517, 7183);
				AddItem(521, 397, 3702);
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
				e.CloseGump( typeof( LearnStealingGump ) );
				e.SendGump( new LearnStealingGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnStealingBook(Serial serial) : base(serial)
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
			Name = "The Art of Thievery";
			ItemID = 0x4C5C;
		}
	}
}