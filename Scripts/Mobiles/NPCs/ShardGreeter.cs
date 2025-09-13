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
using Server.Regions;
using Server.Engines.Soulbound;

namespace Server.Mobiles
{
    public class ShardGreeter : BasePerson
	{
		public override bool ClickTitle{ get{ return false; } }
		public override bool IsInvulnerable{ get{ return true; } }

		private static bool m_SpokeBenefits;
		private static bool m_SpokePerils;
		private static bool m_SpokeSoulbound;
		
		private static bool m_Talked; 

		string[] kfcsay = new string[]  
		{ 
			"Welcome to these lands.  I encourage thee to stare into the globe before you, for you must make an important choice.  You may ask me of the 'benefits' or 'perils' of this choice.",
		};

		[Constructable]
		public ShardGreeter() : base( )
		{
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			NameHue = -1;
			Body = 0x190;
			Hue = 0x430;
			m_Talked = false;

			Name = "the Time Lord";

			AddItem( new Sandals() );
			AddItem( new ClothCowl() );
			AddItem( new SorcererRobe() );

			Direction = Direction.South;
			CantWalk = true;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation ) 
		{                                                    
			if( m_Talked == false ) 
			{ 
				if ( m.InRange( this, 4 )) 
				{   
					m_Talked = true;
					this.SayTo(m, "Welcome to these lands.  I encourage thee to stare into the globe before you, for you must make an important choice.  You can ask me about the 'benefits' and 'perils' of this choice if you need more information.");
					this.SayTo(m, "Lastly, I wish to talk to you about choosing a more difficult life, meant for those who truly live each day to its fullest.  For more on this, say 'soulbound'.");
					//SayRandom( kfcsay, this ); 
					this.Move( GetDirectionTo( m.Location ) );
					chatTimer t = new chatTimer(); 
					t.Start(); 
				} 
			} 
		} 

		private class chatTimer : Timer 
		{ 
			public chatTimer() : base( TimeSpan.FromSeconds( 25 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
			} 

			protected override void OnTick() 
			{ 
				m_Talked = false; 
				m_SpokeBenefits = false;
				m_SpokeSoulbound = false;
				m_SpokePerils = false; 
			} 
		} 

		public void UpdateNewCharacterItems(Mobile mobile, bool normalOnly) {
			//foreach (Item item in mobile.Backpack.Items) {
			//	item.IsNormalOnly = normalOnly;
			//} 
		}

		private static void SayRandom( string[] say, Mobile m ) 
		{ 
			m.Say( say[Utility.Random( say.Length )] ); 
		}

		public ShardGreeter(Serial serial) : base(serial)
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new ShardGreeterEntry( from, this ) ); 
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

			m_Talked = false;
		}

		public class ShardGreeterEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;

			public ShardGreeterEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				if( !( m_Mobile is PlayerMobile ) )
					return;

				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
				{
					mobile.SendGump(new SpeechGump( "Welcome Brave Adventurer", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "ShardGreeter" ) ));
				}
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

	    if (  Insensitive.Contains( e.Speech, "benefits" ) )//was sellpet
	    {
			this.SayTo(e.Mobile, "The world is governed by a Mystical Balance - those brave enough to enter without restrictions will have greater impact on this balance, and may be crowned champions.  They also benefit from bonuses on their daily adventures and a higher stat cap.");
	    }
	    else if (  Insensitive.Contains( e.Speech, "perils" ) )
	    {
			this.SayTo(e.Mobile, "Those who seek to play without restrictions may lose some of their skill when travelling the Aether or after being resurrected.");
	    }
	    else if (  Insensitive.Contains( e.Speech, "soulbound" ) )
	    {
			this.SayTo(e.Mobile, "Aaah yes... for the soulbound, death is permanent.  If you die, you loose all memories and are born again as a new person each time.  This is a truly challenging way to live... but to those who persist, comes great reward, adventure and items not available to other regular souls.");
			this.SayTo(e.Mobile, "If you wish to choose this life, speak the words 'unam animam'.  Once spoken, I will hand you information on where your journey begins.");
	    }
	    else if (  Insensitive.Contains( e.Speech, "unam animam" ) )
	    {
			this.SayTo(e.Mobile, "As you wish.... Here are four maps from different lands that indicate a common entrance to where you need to go next.");
			Mobile m = e.Mobile;
			if ( !(m is PlayerMobile) )
				return;
			PlayerMobile p = (PlayerMobile)m;
			p.SoulBound = true;
			p.SoulForce = 0;
 			p.SoulBoundDate = DateTime.UtcNow;
 			p.OriginalBody = p.Body;
 			BloodRune rune = new BloodRune();
 			GuardNote note = new GuardNote();
 			string text = "As a soulbound player, make it a priority to locate the Hall of Legends. There are four entrances across the lands. Inside, a being can teach you further.<br>Below are the co-ordinates by land:<br>";
 			for(int i = 0; i < 4; ++i) {
 				switch(i) {
 					case 0:
 						Point3D malas = rune.MalasEntrance;
					 	text += Worlds.GetMyWorld(Map.Malas, malas, malas.X,  malas.Y ) + " :" + malas.X + "," + malas.Y + "<br>";
 					break;
 					case 1:
 						Point3D trammel = rune.TrammelEntrance;
					 	text += Worlds.GetMyWorld(Map.Trammel, trammel, trammel.X,  trammel.Y ) + " :" + trammel.X + "," + trammel.Y + "<br>";
 					break;
 					case 2:
 						Point3D felucca= rune.FeluccaEntrance;
					 	text += Worlds.GetMyWorld(Map.Felucca, felucca, felucca.X,  felucca.Y ) + " :"  + felucca.X + "," + felucca.Y + "<br>";
 					break;
 					case 3:
 						Point3D termur = rune.TerMurEntrance;
					 	text += Worlds.GetMyWorld(Map.TerMur, termur, termur.X,  termur.Y ) + " :"  + termur.X + "," + termur.Y + "<br>";
 					break; 
 				}
 				note.Scroll_Text = text;
			 	if (p.Backpack != null) {
			 		p.Backpack.DropItem(note);
			 	}
 			}
 			rune.Delete();
 			this.UpdateNewCharacterItems(p, false);
	    }
	    else 
	    { 
		base.OnSpeech( e ); 
	    }
      	}
      }
	}
}