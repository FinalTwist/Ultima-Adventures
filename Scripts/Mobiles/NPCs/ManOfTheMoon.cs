using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;

namespace Server.Mobiles
{
	public class ManOfTheMoon : BasePerson
	{
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextTalk && InRange( m, 4 ) && InLOS( m ) )
				{ 

					bool penitent = false;

					Item shirt = m.FindItemOnLayer( Layer.InnerTorso );
					Item robe = m.FindItemOnLayer( Layer.OuterTorso );
					Item glove = m.FindItemOnLayer( Layer.Gloves );
					Item pants = m.FindItemOnLayer( Layer.Pants );
					Item neck = m.FindItemOnLayer( Layer.Neck );
					Item arms = m.FindItemOnLayer( Layer.Arms );
					Item cloak = m.FindItemOnLayer( Layer.Cloak );
					Item twohanded = m.FindItemOnLayer( Layer.TwoHanded );			
					Item firstvalid = m.FindItemOnLayer( Layer.FirstValid );

					if (robe == null && shirt == null && glove == null && pants == null && neck == null && arms == null && cloak == null && twohanded == null && firstvalid == null)
						penitent = true;

					if (!penitent)
					{
						switch ( Utility.Random( 8 ))
						{
							case 0: Say("Yes... the hexagon was seen. "); break;
							case 1: Say("Soon come, soon come."); break;
							case 2: Say("Five Alternations means there will be 8!"); break;
							case 3: Say("I SEE IT!"); break;
							case 4: Say("The thing you seek is in your eyes."); break;
							case 5: Say("Forty!!  I KNEW IT WAS FORTY!  or wait... a monkey comes."); break;
							case 6: Say("Too many times, too many times."); break;
							case 7: Say("Try and break it... it must break."); break;
						};
					}
					else
					{
						switch ( Utility.Random( 3 ))
						{
							case 0: Say("Yes... I recognize a kindered soul seeking humility...  "); break;
							case 1: Say("Finally, you've come... I have been waiting for thee."); break;
							case 2: Say("Yes, you come prepared, I am ready, but are you?"); break;
						}


					}

					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
		}

		[Constructable]
		public ManOfTheMoon() : base( )
		{
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			NameHue = -1;
			Body = 0x190;
			Hue = 0x430;
			Name = "the man of the moon";

			SetStr( 3000, 3000 );
			SetDex( 3000, 3000 );
			SetInt( 3000, 3000 );

			SetHits( 6000,6000 );
			SetDamage( 500, 900 );

			VirtualArmor = 3000;

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );
			SetDamageType( ResistanceType.Energy, 60 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 35, 40 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 35, 40 );

			SetSkill( SkillName.EvalInt, 130.1, 140.0 );
			SetSkill( SkillName.Magery, 130.1, 140.0 );
			SetSkill( SkillName.Meditation, 110.1, 111.0 );
			SetSkill( SkillName.Poisoning, 110.1, 111.0 );
			SetSkill( SkillName.MagicResist, 185.2, 210.0 );
			SetSkill( SkillName.Tactics, 100.1, 110.0 );
			SetSkill( SkillName.Wrestling, 85.1, 110.0 );
			SetSkill( SkillName.Macing, 85.1, 110.0 );

			
			Karma = 0;
		}

		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable{ get{ return true; } }

		public override bool IsInvulnerable{ get{ return true; } }


      public override bool HandlesOnSpeech( Mobile from ) 
      { 
         return true; 
      } 

		public override void OnSpeech( SpeechEventArgs e ) 
		{
			if ( !(e.Mobile is PlayerMobile) )
				return;
			  
			if( e.Mobile.InRange( this, 4 ))
			{

					bool penitent = false;

					Item shirt = e.Mobile.FindItemOnLayer( Layer.InnerTorso );
					Item robe = e.Mobile.FindItemOnLayer( Layer.OuterTorso );
					Item glove = e.Mobile.FindItemOnLayer( Layer.Gloves );
					Item pants = e.Mobile.FindItemOnLayer( Layer.Pants );
					Item neck = e.Mobile.FindItemOnLayer( Layer.Neck );
					Item arms = e.Mobile.FindItemOnLayer( Layer.Arms );
					Item cloak = e.Mobile.FindItemOnLayer( Layer.Cloak );
					Item twohanded = e.Mobile.FindItemOnLayer( Layer.TwoHanded );			
					Item firstvalid = e.Mobile.FindItemOnLayer( Layer.FirstValid );

					if (robe == null && shirt == null && glove == null && pants == null && neck == null && arms == null && cloak == null && twohanded == null && firstvalid == null)
						penitent = true;


				if (!penitent && Utility.RandomBool())
					Say("Yes... "); 
				else if (!penitent)
					Say("No... ");


				if (  Insensitive.Contains( e.Speech, "humility" ) || Insensitive.Contains( e.Speech, "humble" ) && penitent )
				{
					if (penitent)
						Say("Yes... you are properly prepared... but can you tell me the word?"); 	
					else
						Say("Frozen?  The mangroves speak of orcs."); 	
				}
				else if (  Insensitive.Contains( e.Speech, "amitofu" ) && penitent)
				{
					PlayerMobile mobile = (PlayerMobile) e.Mobile;

					Item pot = mobile.Backpack.FindItemByType( typeof( HolyDraughtOfHumility ) );

					if (mobile.SkillsCap > 10000 && pot == null)
					{
						mobile.AddToBackpack ( new HolyDraughtOfHumility() );
						Say("Yes.. an ancient word... take this and be welcome in the order."); 
					}
					else if (mobile.SkillsCap == 10000 && pot == null)
					{
						Say("You already walk among us... do you not?"); 
					}
					else if (pot != null)
					{
						Say("I only give one per initiate."); 
					}
						
				}
				else if (  Insensitive.Contains( e.Speech, "purity" ) && penitent)
				{
					PlayerMobile mobile = (PlayerMobile) e.Mobile;
					Say("You wish to roam these lands in the most purest form of all?  If so, speak the word parvati while in my presence."); 

				}
				else if (  Insensitive.Contains( e.Speech, "parvati" ) && penitent)
				{
					PlayerMobile mobile = (PlayerMobile) e.Mobile;
					Say("I admire your courage!  Go forth!");
					mobile.SkillsCap = 15000; 

				}
				else
				{ 
					base.OnSpeech( e ); 
				}
			} 
		} 

		public ManOfTheMoon( Serial serial ) : base( serial )
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