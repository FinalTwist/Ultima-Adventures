using System; 
using Server; 

namespace Server.Items 
{ 
   public class MoldedCheese : Item 
   { 
      [Constructable] 
      public MoldedCheese() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public MoldedCheese( int amount ) : base( 0x97D ) 
      {
	 Name = "Molded Cheese";
	 Stackable = false;
	 Hue = 758;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public MoldedCheese( Serial serial ) : base( serial ) 
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