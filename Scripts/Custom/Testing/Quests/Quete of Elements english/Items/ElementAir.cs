using System; 
using Server; 

namespace Server.Items 
{ 
   public class ElementAir : Item 
   { 
      [Constructable] 
      public ElementAir() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public ElementAir( int amount ) : base( 0x1844 ) 
      {
	 Name = "Element of Air";
	 Stackable = false;
	 Hue = 1272;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public ElementAir( Serial serial ) : base( serial ) 
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