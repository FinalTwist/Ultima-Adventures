using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class AssassinNote : Item
	{
		public string LetterMessage;

		[CommandProperty(AccessLevel.Owner)]
		public string Letter_Message { get { return LetterMessage; } set { LetterMessage = value; InvalidateProperties(); } }

		[Constructable]
		public AssassinNote( ) : base( 0xE34 )
		{
			Weight = 1.0;
			Hue = 0xB9A;
			Name = "a letter";
			ItemID = Utility.RandomList( 0xE34, 0x14ED, 0x14EE, 0x14EF, 0x14F0 );
		}

		public class KillGump : Gump
		{
			public KillGump( Mobile from, Item parchment ): base( 100, 100 )
			{
				AssassinNote note = (AssassinNote)parchment;
				string sText = note.LetterMessage;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(37, 28, 1249);
				AddHtml( 86, 72, 303, 237, @"<BODY><BASEFONT Color=#111111><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( KillGump ) );
				e.SendGump( new KillGump( e, this ) );
				e.PlaySound( 0x249 );
			}
		}

		public AssassinNote(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( LetterMessage );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			LetterMessage = reader.ReadString();
		}
	}
}