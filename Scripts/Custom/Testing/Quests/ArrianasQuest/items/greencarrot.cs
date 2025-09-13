using System; 
using Server; 

namespace Server.Items 
{ 
   public class greencarrot : Item 
   { 
      [Constructable] 
      public greencarrot() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public greencarrot( int amount ) : base( 0xC78 ) 
      {
	 Name = "green carrot";
	 Hue = 462;
         Weight = 0.1; 
        
      } 

      public greencarrot( Serial serial ) : base( serial ) 
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