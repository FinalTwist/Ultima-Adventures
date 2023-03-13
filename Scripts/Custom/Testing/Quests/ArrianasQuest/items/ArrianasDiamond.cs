using System; 
using Server; 

namespace Server.Items 
{ 
   public class ArrianasDiamond : Item 
   { 
      [Constructable] 
      public ArrianasDiamond() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public ArrianasDiamond( int amount ) : base( 0xF26 ) 
      {
	 Name = "Arrianas Diamond";
	 Hue = 1154;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public ArrianasDiamond( Serial serial ) : base( serial ) 
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