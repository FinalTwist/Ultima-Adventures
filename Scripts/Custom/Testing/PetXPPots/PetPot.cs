using System; 
using Server.Network; 
using Server.Prompts; 
using Server.Items; 
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps; 

namespace Server.Items 
{ 
   public class PimpTarget : Target 
   { 
      private PetPot m_Pot; 

      public PimpTarget( PetPot pot ) : base( 1, false, TargetFlags.None ) 
      { 
         m_Pot = pot; 
      } 

      protected override void OnTarget( Mobile from, object target ) 
      { 
         if ( target is BaseCreature ) 
         { 
            BaseCreature t = ( BaseCreature ) target; 

            if ( t.IsDeadPet == true )
            { 
               from.SendMessage( "That Being Must Be Alive!!!" );
            } 
            else if ( t.ControlMaster != from ) 
            { 
               from.SendMessage( "That is not your pet!" ); 
            } 
			else if ( t.IsBonded == false ) 
               	from.SendMessage( "The Creature Must Be Bonded To You!!!" ); 
			else if ( t.SkillsTotal >= t.SkillsCap ) 
               	from.SendMessage( "The Creature Is At It's Max Skill Level" ); 


            
         } 
         else 
         { 
            from.SendMessage( "That is not a valid traget." );  
         } 
      } 
   } 

   public class PetPot : Item // Create the item class which is derived from the base item class 
   { 
      [Constructable] 
      public PetPot() : base( 0x166E ) 
      { 
         Name = "A Pet Pot"; 
      } 

      public PetPot( Serial serial ) : base( serial ) 
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

      public override bool DisplayLootType{ get{ return false; } } 

      public override void OnDoubleClick( Mobile from ) // Override double click of the pot to call our target 
      { 
         if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack 
         { 
             from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it. 
         } 

            from.SendMessage( "What pet you want to use this on?" );  
            from.Target = new PimpTarget( this ); // Call our target 
          
      }    
   } 
}