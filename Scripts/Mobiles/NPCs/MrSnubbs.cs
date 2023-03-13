using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;

namespace Server.Mobiles
{
	public class MrSnubbs : BasePerson
	{
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextTalk && InRange( m, 5 ) && InLOS( m ) && m.Alive && !m.Hidden)
				{ 
					switch ( Utility.Random( 2 ))
					{
					case 0: Say("Adventurer!  Need a way off this island?"); break;
					case 1: Say("Hey there friend, do you need a way to return to Sosaria?"); break;
					};

					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
		}

		[Constructable]
		public MrSnubbs() : base( )
		{
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			NameHue = -1;
			Body = 0x190;
			Hue = 0x430;
			Name = "Mr. Snubbs";

			Server.Misc.IntelligentAction.DressUpRogues( this, "", false, 3, "" );

			SetStr( 1000, 1000 );
			SetDex( 1000, 1000 );
			SetInt( 1000, 1000 );

			SetHits( 1000,1000 );
			SetDamage( 500, 900 );

			VirtualArmor = 50;

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
			
			CantWalk = true;
		}

		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable{ get{ return true; } }

      public override bool HandlesOnSpeech( Mobile from ) 
      { 
         return true; 
      } 

		public override void OnSpeech( SpeechEventArgs e ) 
		{
			if ( !(e.Mobile is PlayerMobile) )
				return;
			
			Mobile m = e.Mobile;

			if( m.InRange( this, 5 ) && InLOS( m ) && m.Alive && !m.Hidden )
			{
				if (  Insensitive.Contains( e.Speech, "i do" ) || Insensitive.Contains( e.Speech, "aye" ) || Insensitive.Contains( e.Speech, "yes") || Insensitive.Contains( e.Speech, "sure") || Insensitive.Contains( e.Speech, "okay") )
				{
					Say("Great!  Just say 'get me out' and I'll activate the spell."); 			
				}
				else if (  Insensitive.Contains( e.Speech, "get me out of here please" ) )
				{	
					Say("Sure!"); 
					m.Kill();
					Point3D p = new Point3D(849, 1973, 0);
					Server.Mobiles.BaseCreature.TeleportPets( m, p, Map.Trammel );
					m.MoveToWorld( p, Map.Trammel );
					return;
				}
				else if (  Insensitive.Contains( e.Speech, "get me out of here" ) )
				{	
					Say("Right! That's the one, just need you to say 'get me out of here please'"); 							
				}
				else if (  Insensitive.Contains( e.Speech, "get me out" ) )
				{	
					Say("hmm... not working... try saying 'get me out of here'."); 							
				}
			}

		} 

		public MrSnubbs( Serial serial ) : base( serial )
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