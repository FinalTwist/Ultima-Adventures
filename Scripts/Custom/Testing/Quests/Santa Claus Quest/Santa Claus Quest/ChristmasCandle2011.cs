/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 11/30/2005
 * Time: 7:27 PM
 *  You can change the message on the candle on lines 22 thru 27
 * ChristmasCandle2008
 */
using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x2373, 0x236E )]
	public class ChristmasCandle2011 : Item, IDyable
	{
		public static string GetRandomTitle()
		{
			string[] titles = new string[]
				{
					/*  1 */ "Happy Holidays 2011, from Crow",
					/* 2 */ "Happy Holidays 2011, from Kegan",
					/* 3 */ "Seasons Greetings 2011, from Crows Staff",
					/* 4 */ "Bah Humbug!*muhaha*",
					/* 5 */ "Wishing you a wonderful Holiday Season!",
					/* 6 */ "Were you naughty this year? *tisk tisk*",
					/* 7 */ "Happy Holidays 2011, from Goddess",
					/* 8 */ "Happy Holidays 2011, from Bane",


				};

			if ( titles.Length > 0 )
				return titles[Utility.Random( titles.Length )];

			return null;
		}

		private string m_Title;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Title
		{
			get{ return m_Title; }
			set{ m_Title = value; InvalidateProperties(); }
		}

		[Constructable]
		public ChristmasCandle2011() : this( Utility.RandomDyedHue(), GetRandomTitle() )
		{
		}

		[Constructable]
		public ChristmasCandle2011( int hue ) : this( hue, GetRandomTitle() )
		{
		}

		[Constructable]
		public ChristmasCandle2011( string title ) : this( Utility.RandomBirdHue(), title )
		{
		}

		[Constructable]
		public ChristmasCandle2011( int hue, string title ) : base( 0x236E )
		{
		        
			Weight = 3.0;
			Hue = hue;
			LootType = LootType.Blessed;
            Light = LightType.Circle300;
			m_Title = title;
		}

        public override int LabelNumber{ get{ return 1070875; } } 

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Title != null )
				list.Add( m_Title ); 
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public ChristmasCandle2011( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (string) m_Title );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Title = reader.ReadString();
					break;
				}
			}
		}
	}
}
