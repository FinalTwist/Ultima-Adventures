using System; 
using Server; 

namespace Server.Items 
{ 
   public class ElementEau : Item 
   { 
      [Constructable] 
      public ElementEau() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public ElementEau( int amount ) : base( 0x1844 ) 
      {
	 Name = "Element of Water";
	 Stackable = false;
	 Hue = 1266;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public ElementEau( Serial serial ) : base( serial ) 
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