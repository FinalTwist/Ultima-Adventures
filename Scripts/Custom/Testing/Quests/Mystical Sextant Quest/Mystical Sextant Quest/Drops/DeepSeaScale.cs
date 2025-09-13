using System; 
using Server; 

namespace Server.Items 
{ 
   public class DeepSeaScale : Item 
   { 
      [Constructable] 
      public DeepSeaScale() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public DeepSeaScale( int amount ) : base( 0x26B4 ) 
      {
	 Name = "A Deep Sea Scale";
	 Stackable = false;
	 Hue = 288;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public DeepSeaScale( Serial serial ) : base( serial ) 
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