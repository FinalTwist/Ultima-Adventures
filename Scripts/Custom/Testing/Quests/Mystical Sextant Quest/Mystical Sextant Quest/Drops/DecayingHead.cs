using System; 
using Server; 

namespace Server.Items 
{ 
   public class DecayingHead : Item 
   { 
      [Constructable] 
      public DecayingHead() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public DecayingHead( int amount ) : base( 0x1DA0 ) 
      {
	 Name = "A Decaying Head";
	 Stackable = false;
	 Hue = 371;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public DecayingHead( Serial serial ) : base( serial ) 
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