using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Accounting;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "the Stranger corpse" )]
	public class DarkMoorGreeter : Mobile
	{
		
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
		public DateTime m_NextTalk;
	
        public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public DarkMoorGreeter()
		{
			Name = "Aamon Lore-keeper of the Darklord";
			CantWalk = true;
			Body = 970;
			Hue = 1975;
		}

		public DarkMoorGreeter( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
	        { 
	                base.GetContextMenuEntries( from, list ); 
					if (from.Karma < -5000)
						list.Add( new DarkMoorGreeterEntry( from, this ) ); 
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

		public class DarkMoorGreeterEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public DarkMoorGreeterEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( DarkMoorGreeterGump ) ) )
					{
						mobile.SendGump( new DarkMoorGreeterGump( mobile ));
						
					} 
				}
			}
		}
			
			
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if (m == null || m.Map == null)
				return;
			
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) && m is PlayerMobile && !m.Hidden && DateTime.UtcNow >= m_NextTalk)
			{
				if (m.Karma < -500)
				{
					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					switch (Utility.Random(9))
					{
					case 0: Say("Greetings fellow shadow Dweller, Welcome to the reclaimed lands of DarkMoor."); break;
					case 1: Say("Our hold on these lands is precarious... we can use all the help we can get."); break;
					case 2: Say("Help us rid these lands of this carebear scum."); break;
					case 3: Say("I can tell thee of the tales of how we once ruled these lands."); break;
					case 4: Say("Welcome to reclaimed Praetoria.  Watch your back."); break;
					case 5: Say("We once claimed all these lands with the great one's army... it was glorious."); break;
					case 6: Say("Hail, help us shed innocent blood."); break;
					case 7: Say("Hail, and glory to the Dark One!"); break;
					case 8: Say("Beware of the insidious people of these lands lest they corrupt your dark soul."); break;
					}
				}
				else
				{
					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					switch (Utility.Random(3))
					{
					case 0: Say("You do not belong here!  Begone!"); break;
					case 1: Say("Praetors!  A traitor is in our Midst!"); break;
					case 2: Say("Honorae!  Come slay this impostor!"); break;
					}
				}

				
			}

		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		

			this.Say("Shove that." );
				return false;
		}
	}
}
