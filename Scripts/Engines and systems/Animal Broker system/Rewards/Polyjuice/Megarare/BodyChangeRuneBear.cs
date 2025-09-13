using System; 
using Server.Network; 
using Server.Prompts; 
using Server.Items; 
using Server.Mobiles;
using Server.Targeting; 

namespace Server.Items 
{ 
   public class BodyChangeRuneBear : Target //FINAL To create a new item, just replace ALL instances of "RuneBear" with the new body type in the entire script Find>replace
   { 
      private BodyChangeRuneBearStatue m_Deed; 

      public BodyChangeRuneBear( BodyChangeRuneBearStatue deed ) : base( 1, false, TargetFlags.None ) 
      { 
         m_Deed = deed; 
      } 

      protected override void OnTarget( Mobile from, object target ) 
      { 
         if ( target != null && target is BaseCreature ) 
         { 
            BaseCreature t = ( BaseCreature ) target; 

            if ( t.ControlMaster != from ) 
            { 
               from.SendMessage( "That is not your pet!" ); 
            } 
            else  
             
               { 
                  t.Body = 190; // FINAL this is the body id the pet will take
                  m_Deed.Delete(); // Delete the deed 

               } 
            
         } 
         else 
         { 
            from.SendMessage( "That is not a valid traget." );  
         } 
      } 
   } 

   public class BodyChangeRuneBearStatue : Item 
   { 
      [Constructable] 
      public BodyChangeRuneBearStatue() : base(0x42B4) // FINAL 0xxxxx number this is the item ID for the item player double clicks 
      { 
         Weight = 1.0; 
         Name = "A Rune Bear Polyjuice"; 
         Stackable = false;
	      //Hue = 1176;
      } 

      public BodyChangeRuneBearStatue( Serial serial ) : base( serial ) 
      { 
      } 

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			
				list.Add("Changes a pet's body." ); 
		}

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); // version 
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         //LootType = LootType.Blessed; 

         int version = reader.ReadInt(); 
      } 

      public override bool DisplayLootType{ get{ return false; } } 

      public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target 
      { 
         if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack 
         { 
             from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it. 
         } 
         else 
         { 
            from.SendMessage( "Choose the pet you wish to change." ); 
            from.Target = new BodyChangeRuneBear( this ); // Call our target 
          } 
      }    
   } 
}