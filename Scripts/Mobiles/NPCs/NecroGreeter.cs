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
    public class NecroGreeter : BaseNPC
	{
		private static bool m_Talked; 		
		string[] kfcsay = new string[]  
		{ 
			"Fear the woods beyond.",
		};

		[Constructable]
		public NecroGreeter() : base( )
		{
			SpeechHue = Utility.RandomRedHue();
			Direction = Direction.East;
			CantWalk = true;
			Body = 0x190; 
			Name = NameList.RandomName( "male" );
			Title = "the Grounds Keeper";
			FacialHairItemID = 0;
			FacialHairHue = 0;
			Hue = 0x83E8;
			NameHue = Utility.RandomRedHue();

			AI = AIType.AI_Citizen;
			FightMode = FightMode.None;

			MonkRobe robe = new MonkRobe( );
				robe.Hue = 0x497;
				robe.Name = "tattered robe";
				AddItem( robe );

			Sandals shoe = new Sandals( );
				shoe.Hue = 0x497;
				AddItem( shoe );

			AddItem( new Scythe() );

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );
		}

		public override bool IsEnemy( Mobile m )
		{
			return false;
		}

		public override bool OnBeforeDeath()
		{
			Say("In Vas Mani");
			this.Hits = this.HitsMax;
			this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
			this.PlaySound( 0x202 );
			return false;
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

        public NecroGreeter(Serial serial) : base(serial)
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new NecroGreeterEntry( from, this ) ); 
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
		
		public class NecroGreeterEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public NecroGreeterEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "The Island of Dracula", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "NecroGreeter" ) ));
					}
				}
            }
        }
	}  
}