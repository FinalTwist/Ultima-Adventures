using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Regions;
using Server.Commands;
using System.Text;

namespace Server.Mobiles
{
    public class TownHerald : BasePerson
	{
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public TownHerald() : base( )
		{
			NameHue = -1;

			InitStats( 100, 100, 25 );

			Title = "the town crier";
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			AddItem( new FancyShirt( Utility.RandomBlueHue() ) );

			Item skirt;

			switch ( Utility.Random( 2 ) )
			{
				case 0: skirt = new Skirt(); break;
				default: case 1: skirt = new Kilt(); break;
			}

			skirt.Hue = Utility.RandomGreenHue();

			AddItem( skirt );

			AddItem( new FeatheredHat( Utility.RandomGreenHue() ) );

			Item boots;

			switch ( Utility.Random( 2 ) )
			{
				case 0: boots = new Boots(); break;
				default: case 1: boots = new ThighBoots(); break;
			}

			AddItem( boots );
			AddItem( new LightCitizen( true ) );

			Utility.AssignRandomHair( this );
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			Region reg = Region.Find( this.Location, this.Map );
			if ( DateTime.UtcNow >= m_NextTalk && InRange( m, 10 ) && m is PlayerMobile )
			{

				if (AdventuresFunctions.InfectedRegions.Count > 0)
				{
					if (AetherGlobe.carrier != null && Utility.RandomDouble() > 0.70)
						Say("Ive heard word of a being named " + AetherGlobe.carrier + " spreading a mysterious infection in the lands!");
					else
						Say("Infected have been seen in the lands! Help is needed!");
				}
				else if ( LoggingFunctions.LoggingEvents() == true )
				{
					string sEvents = LoggingFunctions.LogShout();
					Say( sEvents );
				}
				else
				{
					Say( "All is well in the land!" );
				}
				m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 30 ) ));
			}
		}

		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return true; 
		} 

		public override void OnSpeech( SpeechEventArgs e ) 
		{
			if( e.Mobile.InRange( this, 4 ))
			{
				if ( Insensitive.Contains( e.Speech, "infected") )  
				{
					TalkInfection();
				}
			}
		
		} 

		public void TalkInfection()
		{
			StringBuilder sb = new StringBuilder();

			if (AdventuresFunctions.InfectedRegions == null)
                sb.Append("Thank the balance! The infected are contained.");

			AdventuresFunctions.CheckInfection();

			if (AdventuresFunctions.InfectedRegions.Count > 0 )
			{
				sb.Append("Hear Ye! Infected have been spotted in ");

				for ( int i = 0; i < AdventuresFunctions.InfectedRegions.Count; i++ ) // load static regions
				{			
					String r = (String)AdventuresFunctions.InfectedRegions[i];

					if ( r != null || r != "" || r != " " )
						sb.Append( r + ", and " );
					else
						sb.Append( "an unknown location, and ");
				}

				sb.Append("help is urgently needed!");
			}
			else
				sb.Append("Thank the balance! The infected are contained.");

			Say( sb.ToString() );
		}

        public TownHerald(Serial serial) : base(serial)
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			//list.Add( new TownHeraldEntry( from, this ) ); 
		} 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public class TownHeraldEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public TownHeraldEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( LoggingFunctions.LoggingEvents() == true )
					{
						if ( ! mobile.HasGump( typeof( LoggingGumpCrier ) ) )
						{
							mobile.SendGump(new LoggingGumpCrier( mobile, 1 ));
						}
					}
					else
					{
						m_Giver.Say("Good day to you, " + m_Mobile.Name + ".");
					}
				}
            }
        }
	}  
}