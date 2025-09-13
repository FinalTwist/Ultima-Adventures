using System; 
using Server.Items; 

namespace Server.Items 
{ 
   public class WQStonebow : Item 
   { 
      [Constructable] 
      public WQStonebow() : base( 7963 ) 
      { 
         Hue = 1154; 
         Name = "A Wyrm's Bow Soul Stone"; 
      } 



      public WQStonebow( Serial serial ) : base( serial ) 
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