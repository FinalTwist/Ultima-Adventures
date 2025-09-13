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
	public class theStranger : Mobile
	{
		
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
		public DateTime m_NextTalk;
	
        public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public theStranger()
		{
			Body = 0x190; 
			Name = "a Stranger";
			CantWalk = true;
			Hue = Utility.RandomSkinHue();

			Item Boots = new Boots();
			Boots.Hue = 1;
      	    Boots.Name = "Dark Boots";
			Boots.Movable = false;
			AddItem( Boots ); 

			Item FancyShirt = new FancyShirt();
			FancyShirt.Hue = 1;
      	    FancyShirt.Name = "Dark Shirt";
			FancyShirt.Movable = false;
			AddItem( FancyShirt ); 

			Item LongPants = new LongPants();
			LongPants.Hue = 1;
      	    LongPants.Name = "Dark Pants";
			LongPants.Movable = false;
			AddItem( LongPants ); 

		}

		public theStranger( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
	        { 
	                base.GetContextMenuEntries( from, list ); 
					if (from.Karma < -5000)
						list.Add( new theStrangerEntry( from, this ) ); 
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

		public class theStrangerEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public theStrangerEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( theStrangersGump ) ) )
					{
						mobile.SendGump( new theStrangersGump( mobile ));
						mobile.AddToBackpack( new SlaveWhip() );
					} 
				}
			}
		}
			
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) )
			{

				if ( !m.Hidden && this.Combatant == null && DateTime.UtcNow >= m_NextTalk && m is PlayerMobile ) // check if its time to talk
				{
					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					switch (Utility.Random(9))
					{
					case 0: Say("I used to think that my life was a tragedy, but now I realize, it’s a comedy."); break;
					case 1: Say("The worst part of having a mental illness is people expect you to behave as if you don’t."); break;
					case 2: Say("For my whole life, I didn’t know if I even really existed. But I do, and people are starting to notice."); break;
					case 3: Say("What do you get when you cross a mentally ill loner with a society that abandons him and treats him like trash? You get what you fuckin’ deserve!"); break;
					case 4: Say("Comedy is subjective, "+m.Name +". Isn’t that what they say? All of you, the system that knows so much, you decide what’s right or wrong. The same way that you decide what’s funny or not."); break;
					case 5: Say("You see, in their last moments, people show you who they really are."); break;
					case 6: Say("Madness is like gravity. All it takes is a little push."); break;
					case 7: Say("The only sensible way to live in this world is without rules."); break;
					case 8: Say("Introduce a little anarchy. Upset the established order, and everything becomes chaos. I’m an agent of chaos."); break;
					}

				}
			}

		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		

			this.Say("Fuck you and your little items." );
				return false;
		}
	}
}
