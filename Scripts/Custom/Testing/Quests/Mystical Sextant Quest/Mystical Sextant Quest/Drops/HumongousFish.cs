using System; 
using Server; 

namespace Server.Items 
{ 
   public class HumongousFish : Item 
   { 
      [Constructable] 
      public HumongousFish() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public HumongousFish( int amount ) : base( 0x9CC ) 
      {
	 Name = "A Humongous Fish";
	 Stackable = false;
	 Hue = 951;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public HumongousFish( Serial serial ) : base( serial ) 
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