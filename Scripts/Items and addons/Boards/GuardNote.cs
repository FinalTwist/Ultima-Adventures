using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class GuardNote : Item
	{
		public string ScrollText;

		[CommandProperty(AccessLevel.Owner)]
		public string Scroll_Text { get { return ScrollText; } set { ScrollText = value; InvalidateProperties(); } }

		[Constructable]
		public GuardNote( ) : base( 0x2258 )
		{
			Weight = 1.0;
			Hue = 0xB9A;
			Name = "a note";
			ItemID = Utility.RandomList( 0xE34, 0x14ED, 0x14EE, 0x14EF, 0x14F0 );
		}

		public class ReadGump : Gump
		{
			public ReadGump( Mobile from, Item parchment ): base( 100, 100 )
			{
				GuardNote scroll = (GuardNote)parchment;
				string sText = scroll.ScrollText;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(24, 25, 2080);
				AddImage(41, 62, 2081);
				AddImage(41, 132, 2082);
				AddImage(43, 341, 2083);
				AddImage(42, 202, 2081);
				AddImage(42, 271, 2082);
				AddHtml( 58, 66, 229, 262, @"<BODY><BASEFONT Color=#111111><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( !IsChildOf( e.Backpack ) ) 
			{
				e.SendMessage( "This must be in your backpack to read." );
				return;
			}
			else
			{
				e.SendGump( new ReadGump( e, this ) );
				e.PlaySound( 0x249 );
			}
		}

		public GuardNote(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( ScrollText );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            ScrollText = reader.ReadString();
		}
	}
}