using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	[Flipable(0x52FE, 0x52FF)]
	public class WantedMangar : Item
	{
		[Constructable]
		public WantedMangar( ) : base( 0x52FE )
		{
			Name = "Wanted!";
		}

		public class WantedMangarGump : Gump
		{
			public WantedMangarGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 154);
				AddImage(300, 0, 154);
				AddImage(0, 300, 154);
				AddImage(300, 300, 154);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 298, 129);
				AddImage(7, 9, 133);
				AddImage(179, 13, 129);
				AddImage(178, 47, 132);
				AddImage(379, 8, 134);
				AddImage(326, 325, 10896);

				AddHtml( 165, 25, 213, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Wanted: Mangar the Dark</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 102, 87, 480, 222, @"<BODY><BASEFONT Color=#FFA200><BIG>You are trapped in Skara Brae, regardless of the rumors that it was destroyed in Sosaria. It seems that Mangar has moved this village into the void for his own nefarious purposes. You can only assume that if you can find Mangar and defeat him, then you may find a way to escape this void. To do that, you will need to explore and talk to any unusual citizens. Searching the dungeons for clues, secret doors, or magic mouths may prove helpful in your quest. You may slay powerful creatures that will drop chests on the floor you can use to acquire more clues or treasure. Keep an eye on your quest log, as it will show you the steps you accomplished. You feel a bit thirsty now, however, so you may want to get some wine in the cellar below the tavern of the Scarlet Bard.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(254, 341, 7905);
				AddItem(24, 374, 577);
				AddItem(46, 398, 577);
				AddItem(94, 448, 577);
				AddItem(31, 421, 791);
				AddItem(115, 471, 579);
				AddHtml( 159, 355, 108, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Magic Mouth</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 91, 414, 108, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Secret Doors</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 233, 501, 66, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Clues</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(253, 529, 5360, 0xB98);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WantedMangarGump ) );
				e.SendGump( new WantedMangarGump( e ) );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WantedMangar(Serial serial) : base(serial)
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
		}
	}
}