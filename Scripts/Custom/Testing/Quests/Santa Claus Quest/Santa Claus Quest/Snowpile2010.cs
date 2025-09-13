using System; 
using System.Collections; 
using Server.Network; 
using Server.Items; 
using Server.Targeting; 

namespace Server.Items 
{ 

   public class Snowpile2021 : Item 
   { 
      [Constructable] 
      public Snowpile2021() : base( 0x913 ) 
      { 
         Hue = 1151; 
      	Name="snowball launcher";
      	LootType = LootType.Blessed;
	    m_NextAbilityTime = DateTime.UtcNow;
   
      } 

      public Snowpile2021( Serial serial ) : base( serial ) 
      { 
      } 
private DateTime m_NextAbilityTime;
      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); // version 
       
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 

         int version = reader.ReadInt(); 
      	m_NextAbilityTime = DateTime.UtcNow;
      } 
      
      public override void OnDoubleClick( Mobile from ) 
      { 
         if (!IsChildOf(from.Backpack)) 
         { 
            from.SendLocalizedMessage( 1042010 ); //You must have the object in your backpack to use it. 
            return; 
         } 
         else 
         { 
            
            if ( DateTime.UtcNow >= m_NextAbilityTime )
			{
               from.Target = new SnowTarget( from, this ); 
               from.SendLocalizedMessage( 1005575 );   // You carefully pack the snow into a ball... 
            } 
            else 
            { 
               from.SendLocalizedMessage( 1005574 ); 
            } 

         } 
			
    
      } 
       
      private class SnowTarget : Target 
      { 
         private Mobile m_Thrower; 
		private Snowpile2021 m_Snow;

         public SnowTarget( Mobile thrower, Snowpile2021 snow ) : base ( 10, false, TargetFlags.None ) 
         { 
         	m_Thrower = thrower;
			m_Snow=snow;
         } 
          
         protected override void OnTarget( Mobile from, object target ) 
         { 
            if( target == from ) 
               from.SendLocalizedMessage( 1005576 ); 
             
            else if( target is Mobile) 
            { 
               Mobile m = (Mobile)target; 
               from.PlaySound( 0x145 ); 
               from.Animate( 9, 1, 1, true, false, 0 ); 
               from.SendLocalizedMessage( 1010573 );   // You throw the snowball and hit the target! 
               m.SendLocalizedMessage( 1010572 ); // You have just been hit by a snowball! 
               Effects.SendMovingEffect( from, m, 0x36E4, 7, 0, false, true, 0x480, 0 ); 
               m_Snow.m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds( 5.0 ) ;
        } 

            else 
            { 
               from.SendLocalizedMessage( 1005577 ); 
            } 
         } 
          
      } 
   } 
    

       
       
} 
// created on 16/11/2002 at 19:27
