using System; 
using Server; 

namespace Server.Items 
{ 
   public class ElementTerre : Item 
   { 
      [Constructable] 
      public ElementTerre() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public ElementTerre( int amount ) : base( 0x1844 ) 
      {
	 Name = "Element of Earth";
	 Stackable = false;
	 Hue = 1501;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public ElementTerre( Serial serial ) : base( serial ) 
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