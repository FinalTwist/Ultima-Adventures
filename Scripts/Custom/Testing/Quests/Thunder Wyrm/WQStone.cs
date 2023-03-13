using System; 
using Server.Items; 

namespace Server.Items 
{ 
   public class WQStone : Item 
   { 
      [Constructable] 
      public WQStone() : base( 7963 ) 
      { 
         Hue = 1154; 
         Name = "A Wyrm's Soul Stone";
	 Movable = true; 
      } 



      public WQStone( Serial serial ) : base( serial ) 
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