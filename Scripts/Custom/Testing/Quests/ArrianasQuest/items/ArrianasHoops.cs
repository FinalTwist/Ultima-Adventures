using System; 
using Server; 

namespace Server.Items 
{ 
   public class ArrianasHoops : Item 
   { 
      [Constructable] 
      public ArrianasHoops() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public ArrianasHoops( int amount ) : base( 0x1F09 ) 
      {
	 Name = "Arrianas Hoops";
	 Hue = 1154;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public ArrianasHoops( Serial serial ) : base( serial ) 
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