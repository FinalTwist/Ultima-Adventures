using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	[CorpseName( "Frankeinstein's corpse" )]
	public class Frank : BaseCreature
	{

	private static bool m_Talked;

        string[] kfcsay = new string[]
      { 
		 "Eaaah!",
		 "Frank wants food!!!",
		 "FOOOD!!!",
		 "*walks slowly, like a zombie*",
		 
      }; 
	 
	 
		[Constructable]
        public Frank()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4) 
		{ 
			SetStr( 205, 245 );
			SetDex( 100, 125 );
			SetInt( 61, 100 );
			SetHits( 1000, 1001);

			SetDamage( 10, 35 );

			SetDamageType( ResistanceType.Physical, 100 );
 			
			Name = "Frankeinstein";
			Title = "the green monster";
			Body = 400; 

			SpeechHue = Utility.RandomDyedHue(); 

			Hue = 74; 
			AddItem( new FancyShirt( 0 ) );
			AddItem( new LongPants( 73 ) );
			AddItem( new ShortHair( 2000 ) );
			NameHue = 74;
			
			
			
			PackGold( 420, 690 );
			if (Utility.RandomDouble() < .1 ) // generates random less than 1


			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Anatomy, 65.3, 83.2 ); 
			SetSkill( SkillName.Tactics, 75.3, 93.2 ); 
			SetSkill( SkillName.MagicResist, 95.3, 100.0 );  
			SetSkill( SkillName.Wrestling, 100.0, 120.0 ); 
			SetSkill( SkillName.Magery, 60.4, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );

			Fame = 2500;
			Karma = -2500;
			

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
                  				// Start timer to prevent spam 
               				SpamTimer t = new SpamTimer(); 
               				t.Start(); 
            			} 
		 			}
	  			}		

		public Frank( Serial serial ) : base( serial )
		{
		}

		private class SpamTimer : Timer 
      		{ 
         		public SpamTimer() : base( TimeSpan.FromSeconds( 3 ) ) 
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

		public override bool AlwaysMurderer{ get{ return true; } }
		
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
	} 
}