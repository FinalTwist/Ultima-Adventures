using System; 
using Server; 

namespace Server.Items 
{ 
   public class ArrianasClips : Item 
   { 
      [Constructable] 
      public ArrianasClips() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public ArrianasClips( int amount ) : base( 0xF8F ) 
      {
	 Name = "Arrianas Clip";
	 Hue = 1154;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public ArrianasClips( Serial serial ) : base( serial ) 
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