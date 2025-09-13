using System; 
using Server;
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class YoungPlayer : BaseBlue
	{ 
		private bool m_Bandaging;
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 10.0 );
		public DateTime m_NextTalk;

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			#region Tass23/Raist
			if ( !( m is PlayerMobile ) )
				return;
			if ((m is PlayerMobile) && (m.AccessLevel == AccessLevel.Player))
			{
				if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) )
				{
					if ( !m.Frozen && DateTime.UtcNow >= m_NextResurrect && !m.Alive )
					{
						m_NextResurrect = DateTime.UtcNow + ResurrectDelay;

						if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
						{
							m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
						}
						else if ( CheckResurrect( m ) )
						{
							OfferResurrection( m );
						}
					}
					else if ( !m.Hidden && DateTime.UtcNow >= m_NextResurrect && this.HealsYoungPlayers && m.Hits < (m.HitsMax/2) && m is PlayerMobile || DateTime.UtcNow >= m_NextResurrect && m.Hits < (m.HitsMax/2) && m is BaseBlue )
					{
						OfferHeal( (PlayerMobile) m );
					}
					else if ( !m.Hidden && this.Combatant == null && DateTime.UtcNow >= m_NextTalk && m is PlayerMobile ) // check if its time to talk
					{
						m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
						switch (Utility.Random(9))
						{
							case 0: Say("Hello " + m.Name + " have you come to play?"); break;
							case 1: Say("" + m.Name + "?"); break;
							case 2: Say("" + m.Name + " where do you think your going?"); break;
							case 3: Say("Hey " + m.Name + " want to play tag?"); break;
							case 4: Say(" Hey " + m.Name + " , did you hear about Moonglow?"); break;
							case 5: Say("" + m.Name + " How are ya?"); break;
							case 6: Say("To adventure, " + m.Name + "."); break;
							case 7: Say("" + m.Name + "!!"); break;
							case 8: Say("Hi " + m.Name + "!"); break;
						}
					}
				}
			}
			#endregion
		}
		[Constructable] 
		public YoungPlayer() : base( AIType.AI_Melee, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
		{
			Title = " [YOUNG]"; 
			SpeechHue = Utility.RandomDyedHue();

			SetStr( 70, 80 );
			SetDex( 40, 50 );
			SetInt( 61, 75 );

			SetSkill( SkillName.MagicResist, 40.0, 50.0 );
			SetSkill( SkillName.Swords, 70.0, 80.0 );
			SetSkill( SkillName.Tactics, 90.0, 92.0 );
			SetSkill( SkillName.Anatomy, 40.0, 90.0 );

			AddItem( new Longsword(  ) );
			AddItem( new LongPants(  ) );
			AddItem( new Boots(  ) );
			AddItem( new LeatherGloves(  ) );
			AddItem( new Shirt(  ) );
			AddItem( new ClothNinjaHood(  ) );
			AddItem( new BodySash(  ) );
			
			switch (Utility.Random(4))
                {
                case 0: AddItem( new Longsword() ); break;
                case 1: AddItem( new Axe() ); break;
                case 2: AddItem( new Bardiche() ); break;
                case 3: AddItem( new Hatchet() ); break;
                } 

            Karma = 10;
			
			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );	
			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" ); 
			}
			
			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}
		public override bool CanRummageCorpses{ get{ return true; } }

		public override void OnThink()
		{
			base.OnThink();
			
			// Use bandages
			if ( (this.IsHurt() || this.Poisoned) && m_Bandaging == false )
			{
				Bandage m_Band = (Bandage)this.Backpack.FindItemByType( typeof ( Bandage ) );
				
				if ( m_Band != null )
				{
					m_Bandaging = true;
					
					if ( BandageContext.BeginHeal( this, this ) != null )
						m_Band.Consume();
					BandageTimer bt = new BandageTimer( this );
					bt.Start();
				}
			}
		}

		private class BandageTimer : Timer 
		{ 
			private YoungPlayer pk;

			public BandageTimer( YoungPlayer o ) : base( TimeSpan.FromSeconds( 4 ) ) 
			{ 
				pk = o;
				Priority = TimerPriority.OneSecond; 
			} 
		
 			protected override void OnTick() 
			{ 
				pk.m_Bandaging = false; 
			} 
		}

		public override bool CheckResurrect( Mobile m )
		{
			if ( m.Criminal )
			{
				Say("Look! A GHOST!!! COOL!"); // Thou art a criminal.  I shall not resurrect thee.
				return false;
			}
			else if ( m.Kills >= 5 )
			{
				Say("YOUR NAME IS RED, WHY?"); // Thou'rt not a decent and good person. I shall not resurrect thee.
				return false;
			}
			else if ( m.Karma < 0 )
			{
				Say("Let me see if I can do this..."); // Thou hast strayed from the path of virtue, but thou still deservest a second chance.
			}

			return true;
		}

		public YoungPlayer( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
}   
