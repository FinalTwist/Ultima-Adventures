using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class BardsTaleNote : Item
	{
		public string ScrollMessage;

		[CommandProperty(AccessLevel.Owner)]
		public string Scroll_Message { get { return ScrollMessage; } set { ScrollMessage = value; InvalidateProperties(); } }

		[Constructable]
		public BardsTaleNote( ) : base( 0xE34 )
		{
			Weight = 1.0;
			Hue = 0xB98;
			Name = "an old parchment";

			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0:	Name = "parchment";	break;
				case 1:	Name = "note";		break;
				case 2:	Name = "scroll";		break;
			}

			switch ( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0:	Name = "an old" + " " + Name;		break;
				case 1:	Name = "a weathered" + " " + Name;	break;
				case 2:	Name = "a worn" + " " + Name;		break;
				case 3:	Name = "a scribbled" + " " + Name;	break;
				case 4:	Name = "an unusual" + " " + Name;	break;
				case 5:	Name = "a strange" + " " + Name;	break;
			}

			ItemID = Utility.RandomList( 0xE34, 0x14ED, 0x14EE, 0x14EF, 0x14F0 );

			int amnt = Utility.RandomMinMax( 1, 18 );

			switch ( amnt )
			{
				case 1:		ScrollMessage = "The magician named Kylearan is really behind the fate of Skara Brae."; break;
				case 2:		ScrollMessage = "The crystal sword can cut through the void."; break;
				case 3:		ScrollMessage = "The crystal statue is indestructible."; break;
				case 4:		ScrollMessage = "There is a way to escape the void from the sewers."; break;
				case 5:		ScrollMessage = "There is a secret passage in the cellar below the Scarlet Bard."; break;
				case 6:		ScrollMessage = "One could get into the catacombs if they know the name of the mad god."; break;
				case 7:		ScrollMessage = "There is more than one way into Mangar's tower."; break;
				case 8:		ScrollMessage = "There is a secret passage in the deer hunter's room."; break;
				case 9:		ScrollMessage = "There is a passageway behind Harkyn's throne."; break;
				case 10:	ScrollMessage = "The gray dragon holds the key to escape."; break;
				case 11:	ScrollMessage = "Some believe that a deal was struck between Kylearan and Mangar"; break;
				case 12:	ScrollMessage = "There is a cave where Garth gets his ore."; break;
				case 13:	ScrollMessage = "There was a mad god that left ruins of Skara Brae in Sosaria."; break;
				case 14:	ScrollMessage = "There are three silver shapes that are needed to enter Mangar's room."; break;
				case 15:	ScrollMessage = "Some have seen a wizard that would go into his dungeon cells and disappear."; break;
				case 16:	ScrollMessage = "There is a statue of the mad god is on top of Harkyn's tower."; break;
				case 17:	ScrollMessage = "Some believe that the key to Mangar's tower was seen in Kylearn's tower."; break;
				case 18:	ScrollMessage = "Long ago, a crystal statue was carved with a jade box inside."; break;
			}
		}

		public class ClueGump : Gump
		{
			public ClueGump( Mobile from, Item parchment ): base( 100, 100 )
			{
				BardsTaleNote scroll = (BardsTaleNote)parchment;
				string sText = scroll.ScrollMessage;

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
			if ( !IsChildOf( e.Backpack ) ) 
			{
				e.SendMessage( "This must be in your backpack to read." );
			}
			else
			{
				e.CloseGump( typeof( ClueGump ) );
				e.SendGump( new ClueGump( e, this ) );
				e.PlaySound( 0x249 );
			}
		}

		public BardsTaleNote(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( ScrollMessage );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			ScrollMessage = reader.ReadString();
		}
	}
}