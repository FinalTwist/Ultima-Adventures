using System; 
using Server.Items; 

namespace Server.Items 
{ 
   public class WQStonemace : Item 
   { 
      [Constructable] 
      public WQStonemace() : base( 7963 ) 
      { 
         Hue = 1154; 
         Name = "A Wyrm's Mace Soul Stone"; 
      } 



      public WQStonemace( Serial serial ) : base( serial ) 
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