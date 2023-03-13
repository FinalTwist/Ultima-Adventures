using System; 
using Server; 

namespace Server.Items 
{ 
   public class SteelGears : Item 
   { 
      [Constructable] 
      public SteelGears() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public SteelGears( int amount ) : base( 0x1053 ) 
      {
	 Name = "Steel Gears";
	 Stackable = false;
	 Hue = 1153;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public SteelGears( Serial serial ) : base( serial ) 
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