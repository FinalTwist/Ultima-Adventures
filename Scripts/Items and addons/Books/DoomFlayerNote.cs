using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class DoomFlayerNote : Item
	{
		[Constructable]
		public DoomFlayerNote( ) : base( 0xE34 )
		{
			Weight = 1.0;
			Hue = 0xB98;
			Name = "a dusty scroll";
			ItemID = 0x14EE;
		}

		public class ClueGump : Gump
		{
			public ClueGump( Mobile from ): base( 100, 100 )
			{
				string sText = "The demon opened the black gate and unleashed chaos across Lodoria. Where the dwarven armies had fallen, the elven forces assembled all of their magic and sent the beast back to the void. While the world itself provided the natural forces to summon the demon, now it has diminished to the core of the world. This is where we have been searching for centuries, deep below the city of Lodoria. The drow have joined our cause and aided us in constructing our city deep below, where we can keep searching in secret. Now that my research in Doom is complete, I will return to the cemetery at night as not to be seen.";

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(37, 28, 1249);
				AddHtml( 86, 72, 303, 237, @"<BODY><BASEFONT Color=#111111><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				m.SendGump( new ClueGump( m ) );
				m.PlaySound( 0x249 );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public DoomFlayerNote(Serial serial) : base(serial)
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