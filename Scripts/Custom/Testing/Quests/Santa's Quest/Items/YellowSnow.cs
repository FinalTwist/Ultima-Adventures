using System; 
using Server; 

namespace Server.Items 
{ 
   public class YellowSnow : Item 
   { 
      [Constructable] 
      public YellowSnow() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public YellowSnow( int amount ) : base( 0x26B8 ) 
      {
	 Name = "Yellow Snow";
	 Stackable = true;
	 Hue = 55;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public YellowSnow( Serial serial ) : base( serial ) 
      { 
      } 

      //public override Item Dupe( int amount ) 
      //{ 
         //return base.Dupe( new YellowSnow( amount ), amount ); 
      //} 

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