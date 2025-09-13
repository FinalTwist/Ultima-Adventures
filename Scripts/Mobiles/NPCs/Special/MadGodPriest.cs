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
using Server.Commands;

namespace Server.Mobiles
{
    public class MadGodPriest : BaseNPC
	{
		public override bool InitialInnocent{ get{ return true; } }		private static bool m_Talked; 
		
		string[] kfcsay = new string[]  
		{ 
			"All praise the Mad God.",
		};

		[Constructable]
		public MadGodPriest() : base( )
		{
			Body = 0x190; 
			Name = NameList.RandomName( "male" );
			NameHue = 0xB0C;
			Title = "the priest";
			FacialHairItemID = 0;
			FacialHairHue = 0;
			Hue = 0x83EA;
			AI = AIType.AI_Citizen;
			FightMode = FightMode.None;

			MonkRobe robe = new MonkRobe( );
				robe.Name = "priest robe";
				AddItem( robe );

			Sandals shoe = new Sandals( );
				shoe.Hue = 542;
				AddItem( shoe );

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );
		}

		public override void OnMovement( Mobile m, Point3D oldLocation ) 
		{                                                    
			if( m_Talked == false ) 
			{ 
				if ( m.InRange( this, 4 ) ) 
				{                
					m_Talked = true; 
					SayRandom( kfcsay, this ); 
					this.Move( GetDirectionTo( m.Location ) );
					chatTimer t = new chatTimer(); 
					t.Start(); 
				} 
			} 
		} 
			
		private class chatTimer : Timer 
		{ 
			public chatTimer() : base( TimeSpan.FromSeconds( 20 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
			} 
			
			protected override void OnTick() 
			{ 
				m_Talked = false; 
			} 
		} 
			
		private static void SayRandom( string[] say, Mobile m ) 
		{ 
			m.Say( say[Utility.Random( say.Length )] ); 
		}

        public MadGodPriest(Serial serial) : base(serial)
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new MadGodPriestEntry( from, this ) ); 
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
		
		public class MadGodPriestEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public MadGodPriestEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;

				if ( CharacterDatabase.GetBardsTaleQuest( m_Mobile, "BardsTaleCatacombKey" ) )
				{
					m_Giver.SayTo(m_Mobile, "Have you been meditating in the Catacombs?");
				}
				else if ( !( CharacterDatabase.GetBardsTaleQuest( m_Mobile, "BardsTaleMadGodName" ) ) )
				{
					m_Giver.SayTo(m_Mobile, "Only a true disciple knows the name of the Mad God.");
				}
				else if ( !( CharacterDatabase.GetBardsTaleQuest( m_Mobile, "BardsTaleCatacombKey" ) ) )
				{
					if ( ! m_Mobile.HasGump( typeof( SpeechGump ) ) )
					{
						CharacterDatabase.SetBardsTaleQuest( m_Mobile, "BardsTaleCatacombKey", true );
						m_Mobile.SendSound( 0x3D );
						m_Mobile.SendGump(new SpeechGump( "The Catacombs Below", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "MadGodPriest" ) ));
					}
				}
            }
        }
	}  
}