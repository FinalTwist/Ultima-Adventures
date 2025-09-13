using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles 
{
   [CorpseName( "corpse of the royal knight" )]
   public class RoyalKnight : BaseCreature
   {
	  private static bool m_Talked; 

      string[] kfcsay = new string[] 
      { 
		 "Who dares to challenge the Royal Knight?",
		"The King Will Be Glad To Hear Of Your Death",
		"Have you any last words",
		"Muwahahahaah Royal Knights never lose",
		"Have at Thee",
		"Thou art nothing but a fool",


      }; 
      [Constructable] 
	  public RoyalKnight() : base( AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.15, 0.4 )
      { 
 
         Body = 0x190; 
		 Name = "Royal Knight[Expeditionary Leader]";

		 SetStr( 200, 210 );
         SetDex( 200, 210 ); 
         SetInt( 150, 170 ); 

         SetDamage( 30, 50 );

	 SetHits ( 750, 1000 );

         SetDamageType( ResistanceType.Energy, 100 ); 

		 SetResistance( ResistanceType.Physical, 65, 90 );
         SetResistance( ResistanceType.Fire, 30, 60 ); 
         SetResistance( ResistanceType.Cold, 40, 70 ); 
         SetResistance( ResistanceType.Poison, 60, 70 ); 
         SetResistance( ResistanceType.Energy, 60, 70 ); 

		 SetSkill( SkillName.Anatomy, 120.0 );
		 SetSkill( SkillName.MagicResist, 150.0 );
         SetSkill( SkillName.Swords, 120.0 ); 
         SetSkill( SkillName.Tactics, 120.0 );
		 SetSkill( SkillName.Wrestling, 120.0 );
         SetSkill(SkillName.Chivalry, 94.3, 100.0);

     Fame = 6000; 
	 Karma = 4000;

		Royalknightschest Chest = new Royalknightschest();
			Chest.Movable = false;
			AddItem(Chest);
			
			Royalknightsarms Arms = new Royalknightsarms();
			Arms.Movable = false;
			AddItem(Arms);
			
			Royalknightsleggings Legs = new Royalknightsleggings();
			Legs.Movable = false;
			AddItem(Legs);
			
			Royalknightsgloves Gloves = new Royalknightsgloves();
			Gloves.Movable = false;
			AddItem(Gloves);
			
			Royalknightsgorget Gorget = new Royalknightsgorget();
			Gorget.Movable = false;
			AddItem(Gorget);
			
			Royalknightshelm Helm = new Royalknightshelm();
			Helm.Movable = false;
			AddItem(Helm);
			
			Royalknightssword Sword = new Royalknightssword();
			Sword.Movable = false;
			AddItem(Sword);
			
			Royalknightsring Ring = new Royalknightsring();
			Ring.Movable = false;
			AddItem(Ring);
			
			Royalknightsbracelet Bracelet = new Royalknightsbracelet();
			Bracelet.Movable = false;
			AddItem(Bracelet);
			
			Royalknightsshield Shield = new Royalknightsshield();
			Shield.Movable = false;
			AddItem(Shield);

			switch ( Utility.Random( 20 ))
			{
	 		case 0: AddItem( new Royalknightschest() ); break; 
         		case 1: AddItem( new Royalknightsarms() ); break; 
         		case 2: AddItem( new Royalknightsgloves() ); break; 
         		case 3: AddItem( new Royalknightsgorget() ); break; 
         		case 4: AddItem( new Royalknightsleggings() ); break;
		 	case 5: AddItem( new Royalknightshelm() ); break;
			case 6: AddItem ( new Royalknightsbracelet() ); break;
			case 7: AddItem ( new Royalknightssword() ); break;
			case 8: AddItem ( new Royalknightsring() ); break;
			case 9: AddItem ( new Royalknightsshield() ); break;
		 	PackGold( 1500, 2000);
			}


      }
        protected override BaseAI ForcedAI
        {
            get
            {
                return new OmniAI(this);
            }
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
		public RoyalKnight( Serial serial ) : base( serial )
		{
		}
      private class SpamTimer : Timer 
      { 
         public SpamTimer() : base( TimeSpan.FromSeconds( 7 ) ) 
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

