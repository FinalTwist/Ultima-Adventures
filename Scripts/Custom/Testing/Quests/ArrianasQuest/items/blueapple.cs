using System; 
using Server; 

namespace Server.Items 
{ 
   public class blueapple : Item 
   { 
      [Constructable] 
      public blueapple() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public blueapple( int amount ) : base( 0x9D0 ) 
      {
	 Name = "blue apple";
	 Hue = 6;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public blueapple( Serial serial ) : base( serial ) 
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