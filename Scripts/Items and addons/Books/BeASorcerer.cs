using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class BeASorcerer : Item
	{
		[Constructable]
		public BeASorcerer( ) : base( 0x4C5C )
		{
			Weight = 1.0;
			Name = "Guide to wielding true magic";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "How to become a sorcerer" );
		}

		public class SorcererHowToGump : Gump
		{
			public SorcererHowToGump( Mobile from ): base( 25, 25 )
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

				AddHtml( 187, 80, 316, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>WIELDING TRUE MAGIC</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 88, 123, 402, 372, @"<BODY><BASEFONT Color=#FCFF00><BIG>I have heard of rumors throughout my life of certain persons who were able to wield magic in a different way than the mages in the mages guild.  For many years I studied the arts but alas  I could only learn what was already in practice.  On an specially dark evening, I chose to put away traditional magic practices... I put down my spell channeling shield, I took off my magic renegation armor, and I put down my mage-weapon mace... I stood there, completely naked, begging for a way to wield pure, brutal, powerful energies.  Being drunk at the time, and wanting to tuck away my spellbook, I put it in my talisman holder... and thats when it happened.... My mind cleared, I could see visions from lands far away, I knew the purpose of life... I BECAME A TRUE SORCERER!  No longer needing armor and items which other mages rely on, I can now defeat the strongest foes with complete impunity!  LET MY REIGN BEGIN!<br><br></BIG></BASEFONT></BODY>", (bool)false, (bool)true);

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
				e.CloseGump( typeof( SorcererHowToGump ) );
				e.SendGump( new SorcererHowToGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public BeASorcerer(Serial serial) : base(serial)
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
			Name = "How to become a sorcerer";
			ItemID = 0x4C5C;
		}
	}
}
