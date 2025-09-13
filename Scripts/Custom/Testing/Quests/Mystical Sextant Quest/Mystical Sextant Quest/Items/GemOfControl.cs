using System; 
using Server; 

namespace Server.Items 
{ 
   public class GemOfControl : Item 
   { 
      [Constructable] 
      public GemOfControl() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public GemOfControl( int amount ) : base( 0x1ED0 ) 
      {
	 Name = "Gem Of Control";
	 Stackable = false;
	 Hue = 1152;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public GemOfControl( Serial serial ) : base( serial ) 
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