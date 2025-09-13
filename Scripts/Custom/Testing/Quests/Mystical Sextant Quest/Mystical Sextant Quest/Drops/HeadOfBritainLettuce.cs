using System; 
using Server; 

namespace Server.Items 
{ 
   public class HeadOfBritainLettuce : Item 
   { 
      [Constructable] 
      public HeadOfBritainLettuce() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public HeadOfBritainLettuce( int amount ) : base( 0xC70 ) 
      {
	 Name = "A Head of Britain Lettuce";
	 Stackable = false;
	 Hue = 267;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public HeadOfBritainLettuce( Serial serial ) : base( serial ) 
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