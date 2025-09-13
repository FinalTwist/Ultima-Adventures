using System; 
using Server; 

namespace Server.Items 
{ 
   public class SturdyAxle : Item 
   { 
      [Constructable] 
      public SturdyAxle() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public SturdyAxle( int amount ) : base( 0x105B ) 
      {
	 Name = "A Sturdy Axle";
	 Stackable = false;
	 Hue = 1153;
         Weight = 1.0; 
         Amount = amount; 
      } 

      public SturdyAxle( Serial serial ) : base( serial ) 
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