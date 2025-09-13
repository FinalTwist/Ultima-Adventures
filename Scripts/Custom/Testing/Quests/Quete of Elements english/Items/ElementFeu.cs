using System; 
using Server; 

namespace Server.Items 
{ 
   public class ElementFeu : Item 
   { 
      [Constructable] 
      public ElementFeu() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public ElementFeu( int amount ) : base( 0x1844 ) 
      {
	 Name = "Element of Fire";
	 Stackable = false;
	 Hue = 1360;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public ElementFeu( Serial serial ) : base( serial ) 
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