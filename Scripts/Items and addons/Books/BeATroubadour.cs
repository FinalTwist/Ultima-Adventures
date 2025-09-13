using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class BeATroubadour : Item
	{
		[Constructable]
		public BeATroubadour( ) : base( 0x4C5C )
		{
			Weight = 1.0;
			Name = "Secrets of the Troubadour";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "How to become a troubadour" );
		}

		public class TroubadourHowToGump : Gump
		{
			public TroubadourHowToGump( Mobile from ): base( 25, 25 )
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

				AddHtml( 187, 80, 316, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>THE SOUND OF POWER</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 88, 123, 402, 372, @"<BODY><BASEFONT Color=#FCFF00><BIG>I only wanted to please others all my life.  As a child, I learnt songs, dances, juggling - it made others laugh and I devoted most of my childhood bettering my skills.  One day, I did see the most beautiful of all maidens in a small home near Lamut.  Eyes of opals, teeth of pearls and hair of gold, I wondered how her charms I may get a hold.  I approached, I sang, I danced, I juggled - she laughed, and my hopes soared! <br><br>Get thee away from me, you fool!  You cannot even slay a imp!  Leave me be - I only wish to speak to REAL MEN!<br><br>My hopes dashed, I drank, I cried, I sorrowed.  She was right!  My entire life was a complete waste!  I found a corner to lay in, took off all my armor, my gloves, my weapons, my shield, and only kept my faithful instrument in my talisman pouch... and I sang.<br><br> To my complete amazement my song shattered a tree!  <br><br> What in the nine muses happened!  I was a new man!!  With new powers I rose, put my armor on to go show my maiden my newfound powers!  <br><br> In back in the building, I began a song which they would never forget!  <br><br>Alas, I could not even shatter a mug when I tried... What had happened to my powers?  Were they all a dream?  Shamed, I left these lands, leaving only this note as a record of my existence.<br>Jetson the Amateur<br><br></BIG></BASEFONT></BODY>", (bool)false, (bool)true);

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
				e.CloseGump( typeof( TroubadourHowToGump ) );
				e.SendGump( new TroubadourHowToGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public BeATroubadour(Serial serial) : base(serial)
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
			Name = "Secrets of the Troubadour";
			ItemID = 0x4C5C;
		}
	}
}
