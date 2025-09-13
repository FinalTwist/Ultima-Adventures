/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 11/16/2005
 * Time: 5:18 AM
 * 
 * Santa Claus
 */

using System; 
using Server; 
using System.Collections;
using System.Collections.Generic; 
using Server.Gumps; 
using Server.Items; 
using Server.Network; 
using Server.Targeting; 
using Server.ContextMenus; 

namespace Server.Mobiles 
{ 
   public class SantaClaus : BaseCreature
   { 
      
      public virtual bool IsInvulnerable{ get{ return true; } }
       
      [Constructable]
      public SantaClaus() : base(AIType.AI_Thief, FightMode.None, 10, 1, 0.4, 1.6 ) 
 
      { 
      	InitStats( 100, 100, 100 ); 

		Name = "Santa Claus"; 
		Female = false; 
		Body = 0x190; 
		Hue = Utility.RandomSkinHue(); 
		NameHue = 1272;  
		Blessed = true;
		AddItem( new FancyShirt(  32 )  );
      	AddItem( new Surcoat(  32 )  );
		AddItem( new LongPants(  32 )  );
      	AddItem( new FurCape(  1175 )  );
      	AddItem( new SantasElfBoots( ) );
      	
        Item hair = new Item( 0x203C );
      	hair.Hue = 1151;
      	hair.Layer = Layer.Hair;
		hair.Movable = false; 
		AddItem( hair );
		Item beard = new Item(  0x204B );
      	beard.Hue = 1151;
		beard.Layer = Layer.FacialHair;
		beard.Movable = false;
		AddItem( beard );
         
      }

	 
      private class PetSaleTarget : Target 
      { 
         private SantaClaus m_Trainer; 

         public PetSaleTarget( SantaClaus trainer ) : base( 12, false, TargetFlags.None ) 
         { 
            m_Trainer = trainer; 
         } 

         protected override void OnTarget( Mobile from, object targeted ) 
         { 
            if ( targeted is BaseCreature ) 
               m_Trainer.EndPetSale( from, ( BaseCreature )targeted ); 
            else if ( targeted == from ) 
               m_Trainer.SayTo( from, 502672 ); // HA HA HA! Sorry, I am not an inn. 
            
         } 
      } 

      public void BeginPetSale( Mobile from ) 
      { 
         if ( Deleted || !from.CheckAlive() ) 
            return; 

         SayTo( from, "Which one of my reindeer have you found?" ); 

         from.Target = new PetSaleTarget( this ); 
         
      } 

	//RUFO beginfunction
	private void SellPetForGold(Mobile from, BaseCreature pet, int goldamount)
	{
               		Item gold = new Gold(goldamount);
               		pet.ControlTarget = null; 
               		pet.ControlOrder = OrderType.None; 
               		pet.Internalize(); 
               		pet.SetControlMaster( null ); 
               		pet.SummonMaster = null;
               		pet.Delete();
               		
               		Container backpack = from.Backpack;
               		if ( backpack == null || !backpack.TryDropItem( from, gold, false ) ) 
            		{ 
            			gold.MoveToWorld( from.Location, from.Map );           			
            		}

	}
	//RUFO endfunction


      public void EndPetSale( Mobile from, BaseCreature pet ) 
      { 
         if ( Deleted || !from.CheckAlive() ) 
            return;
            
    if ( !pet.Controlled || pet.ControlMaster != from ) 
		SayTo( from, 1042562 ); // You do not own that pet! 
	else if ( pet.IsDeadPet ) 
		SayTo( from, 1049668 ); // Living pets only, please. 
	else if ( pet.Summoned ) 
		SayTo( from, 502673 ); // I can not PetSale summoned creatures. 
	else if ( pet.Body.IsHuman ) 
		SayTo( from, 502672 ); // HA HA HA! Sorry, I am not an inn.
	else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map ) 
            SayTo( from, 1042564 ); // I'm sorry.  Your pet seems to be busy. 
	else 
	{ 
		if ( pet is Rudolph )
		{
			SellPetForGold( from, pet, 250 );
		    from.AddToBackpack( new ReindeerReward1() );
			this.Say( "Thank you {0}, you have found Rudolph!",from.Name  );
		}
		else if ( pet is Dasher )
		{
			SellPetForGold(from, pet, 250 );
			from.AddToBackpack( new ReindeerReward2() );
			this.Say( "Thank you {0}, you have found Dasher!",from.Name  );
		}
		else if ( pet is Dancer )
		{
			SellPetForGold(from, pet, 250 );
			from.AddToBackpack( new ReindeerReward3() );
			this.Say( "Thank you {0}, you have found Dancer!",from.Name  );
		}
        else if ( pet is Prancer )
        {
			SellPetForGold(from, pet, 250 );
        	from.AddToBackpack( new ReindeerReward4() );
        	this.Say( "Thank you {0}, you have found Prancer!",from.Name  );
        }
        else if ( pet is Vixen )
        {
			SellPetForGold(from, pet, 250 );
        	from.AddToBackpack( new ReindeerReward5() );
        	this.Say( "Thank you {0}, you have found Vixen!",from.Name  );
        }
        else if ( pet is Comet ) 
        {
			SellPetForGold(from, pet, 250 );
        	from.AddToBackpack( new ReindeerReward6() );
        	this.Say( "Thank you {0}, you have found Comet!",from.Name  );
        }
        else if ( pet is Cupid )
        {
			SellPetForGold(from, pet, 250 );
        	from.AddToBackpack( new ReindeerReward7() );
        	this.Say( "Thank you {0}, you have found Cupid!",from.Name  );
        }
        else if ( pet is Donner )
        {
			SellPetForGold(from, pet, 250 );
        	from.AddToBackpack( new ReindeerReward8() );
        	this.Say( "Thank you {0}, you have found Donner!",from.Name  );
        }
		else if ( pet is Blitzen )
		{
			SellPetForGold( from, pet, 250 );
			from.AddToBackpack( new ReindeerReward9() );
			this.Say( "Thank you {0}, you have found Blitzen!",from.Name  );
		}
		else 
		
			this.Say( "{0}, that is not one of my lost reindeer...*sigh*.",from.Name  );
		
	}
			
      }
	     
      public override bool HandlesOnSpeech( Mobile from ) 
      { 
         return true; 
      } 

      public override void OnSpeech( SpeechEventArgs e ) 
      { 
      	if ( ( e.Speech.ToLower() == "santa" ) )
      	{
      		BeginPetSale( e.Mobile );
      	}
      	else
      	{
      		base.OnSpeech( e ); 
        }
      } 

      public SantaClaus( Serial serial ) : base( serial ) 
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
	
/* Start of the Quest Dialog */
            public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list  ) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new SantaClausEntry( from, this ) ); 
	        }
	        
	        public class SantaClausEntry : ContextMenuEntry
	        {
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SantaClausEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( SantaClausGump ) ) )
					{
						mobile.SendGump( new SantaClausGump( mobile ) );
						//mobile.AddToBackpack( new SantasQuestLog() );					
						{
							SantasQuestLog sql = mobile.Backpack.FindItemByType( typeof ( SantasQuestLog ) ) as SantasQuestLog;
							if ( sql != null )
							{
								return;
							}
							else if ( sql == null )
							{
								mobile.AddToBackpack( new SantasQuestLog() );
							}
							return;
						}
					}
				}
			}
		}

      public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			
			if ( m.Alive && m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;
			
				if ( InRange( pm, 3 ) && !InRange( oldLocation, 3 ) )
				{
					
					SantasGift2011 sg = pm.Backpack.FindItemByType( typeof ( SantasGift2011 ) ) as SantasGift2011;
		        
					if ( sg == null )
					{	
						return;
					}
					
					else if ( sg != null )
					{
						Say( "OH MY! Is that a gift for ME!? May I have it?");
						
						return;
					}
				}
			}
		}
			
		 public override bool OnDragDrop( Mobile from, Item dropped )
		 {
         	Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;
           
			if ( mobile != null)
			{
				if( dropped is SantasGift2011 )
            
         		{
         			if( dropped.Amount!=1 )
         			{
					int amount = dropped.Amount;
				    this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I have no need for that.", mobile.NetState );
				    this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I would like  the gift from my Reindeer, not "+dropped.Amount+".", mobile.NetState );
				
         				return false;
         			}
         			
         			dropped.Delete();

         			mobile.AddToBackpack( new SantasGiftBox2011() );
		            mobile.SendGump( new SantaClausFinishGump( mobile ) );
         			return true;
         		}
         		else if( dropped is SantasGift2011 )
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I have no need for that, please give me my Gift.", mobile.NetState );
     			}
			}
			return false;
		}
   
   }
}
		



   

