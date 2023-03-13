using System; 
using Server; 

namespace Server.Items 
{ 
   public class KegOfBritishAle : Item 
   { 
      [Constructable] 
      public KegOfBritishAle() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public KegOfBritishAle( int amount ) : base( 0x1940 ) 
      {
	 Name = "A Keg of British Ale";
	 Stackable = false;
	 Hue = 411;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public KegOfBritishAle( Serial serial ) : base( serial ) 
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