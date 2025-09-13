using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;


namespace Server.Items
{
	public class CarpetBuild : Item
	{
		[Constructable]
		public CarpetBuild() : base( 0x1A97 )
		{
			Weight = 2.0;
			Hue = 0x95B;
			Name = "The Carpet of Aladdin";


			if ( Weight > 1.0 )
			{
				Weight = 1.0;
				HaveGold = 0;
				HaveCloth = 0;
			}
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);


			int needGold = 20000 - HaveGold;
				if ( needGold < 0 ){ needGold = 0; }
			int needCloth = 10000 - HaveCloth;
				if ( needCloth < 0 ){ needCloth = 0; }


			int carpetDone = needGold + needCloth;


			if ( carpetDone > 0 )
			{
				list.Add( 1070722, "Drop The Items Needed On This Book" );
				list.Add( 1049644, "Need " + needGold.ToString() + " Gold Coins, " + needCloth.ToString() + " Cloth" );
			}
			else
			{
				list.Add( 1070722, "Read the Book to Conjure" );
			}
        }


		public override void OnDoubleClick( Mobile from )
		{
			int needGold = 20000 - HaveGold;
			int needCloth = 10000 - HaveCloth;


			int carpetDone = needGold + needCloth;


			if ( carpetDone > 0 )
			{
				from.SendMessage( "You need to gather more items before you can conjure this!" );
				from.SendSound( 0x55 );
				from.CloseGump( typeof( RugGump ) );
				from.SendGump( new RugGump( HaveGold, HaveCloth ) );
			}
			else
			{
				int builder = 0;


				foreach ( Mobile m in this.GetMobilesInRange( 20 ) )
				{
					if ( m is Mage || m is Witches || m is Necromancer || m is NecroMage || m is HolyMage || m is MageGuildmaster || m is NecromancerGuildmaster )
						++builder;
				}


				if ( builder < 1 )
				{
					from.SendMessage( "You need to be near a wizard to conjure that!" );
					from.SendSound( 0x55 );
					from.CloseGump( typeof( RugGump ) );
					from.SendGump( new RugGump( HaveGold, HaveCloth ) );
				}
				else
				{
					from.SendMessage( "You read the book and it transforms into a magic carpet." );
					from.PlaySound( 0x243 );
					from.AddToBackpack ( new Multis.MagicCarpetADeed() );
					this.Delete();
				}
			}
		}


		private class RugGump : Gump
		{
			public RugGump( int gold, int cloth ) : base( 20, 20 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;


				if ( gold > 20000 ){ gold = 20000; }
				if ( cloth > 10000 ){ cloth = 10000; }


				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(300, 0, 153);
				AddImage(2, 2, 163);
				AddImage(181, 2, 163);
				AddImage(302, 2, 163);
				AddImage(7, 54, 142);
				AddImage(284, 264, 140);
				AddImage(263, 129, 165);
				AddImage(564, 266, 143);
				AddHtml( 13, 12, 574, 26, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>The Carpet of Aladdin</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 86, 51, 495, 142, @"<BODY><BASEFONT Color=#FCFF00><BIG>This book tells the tale of Aladdin's magic carpet. Within these pages, you have learned the secrets to have your own magic carpet conjured. Centuries ago, legends tell of Aladdin gliding over the seas on his magic carpet. If you want to have such an item created for yourself, you will need to collect 10,000 cloth and then read the magic words from this book while near a local wizard as you will surely need their help. For their services, the wizard will require 20,000 gold. To collect these items, place them on this book as you acquire them. Once you collect the needed cloth and gold, find a local wizard and read the book while with them. The book should then transform into a magic carpet.<br><br>Magic carpets work much like sailing vessels, except they can be launched from any shore and they do not have a lower deck to rest. Perhaps the book, Skulls and Shackles, will help you as the navigation and maintenance is the same for both sailing vessels and magic carpets. Unlike ships with planks to board, magic carpets have extended cloth that you can board and disembark from. You will also have a magic key for which to protect your carpet from unwanted visitors. They do not have a hold like a ship, but instead a magic bag that you can store items in. They do not have a tillerman, but instead a magic lamp to follow your commands. If you ever want to change the decorative design of your carpet, simply give it to a tailor and they will change it to one of nine designs. You can simply use the carpet to see the preview of what the design looks like after it is altered. If you named your carpet prior to alteration, you will have to rename it if you gave it a name previously. If you want to change the color of your carpet, you can simply dye it a different color.</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
				AddItem(253, 220, 21536);
				AddItem(542, 220, 21537);
				AddHtml( 308, 212, 48, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Gold:</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 308, 245, 46, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Cloth:</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 367, 212, 70, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + gold + "</BIG></BASEFONT></BODY></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 367, 245, 66, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + cloth + "</BIG></BASEFONT></BODY></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 463, 212, 70, 20, @"<BASEFONT Color=#FBFBFB><BIG>20000</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 463, 245, 70, 20, @"<BASEFONT Color=#FBFBFB><BIG>10000</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 442, 212, 11, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>/</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 442, 245, 11, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>/</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}


		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
			Container pack = from.Backpack;
			int iAmount = 0;


			int needGold = 20000 - HaveGold;
			int needCloth = 10000 - HaveCloth;


			if ( from != null )
			{
				iAmount = dropped.Amount;


				if ( dropped is Gold && needGold > 0 )
				{
					HaveGold = HaveGold + iAmount;
					from.SendMessage( "You added " + iAmount.ToString() + " gold." );
					dropped.Delete();
					this.InvalidateProperties();
					return true;
				}
				else if ( ( dropped is UncutCloth || dropped is Cloth ) && needCloth > 0 )
				{
					HaveCloth = HaveCloth + iAmount;
					from.SendMessage( "You added " + iAmount.ToString() + " cloth." );
					dropped.Delete();
					this.InvalidateProperties();
					return true;
				}
				else if ( dropped is BoltOfCloth && needCloth > 0 )
				{
					HaveCloth = HaveCloth + ( iAmount * 50 );
					from.SendMessage( "You added " + iAmount.ToString() + " cloth" );
					dropped.Delete();
					this.InvalidateProperties();
					return true;
				}
			}


			return false;
		}


		public CarpetBuild( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version


			writer.Write( HaveGold );
			writer.Write( HaveCloth );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();


			HaveGold = reader.ReadInt();
			HaveCloth = reader.ReadInt();
		}


		public int HaveGold;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveGold { get{ return HaveGold; } set{ HaveGold = value; } }


		public int HaveCloth;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveCloth { get{ return HaveCloth; } set{ HaveCloth = value; } }
	}
}