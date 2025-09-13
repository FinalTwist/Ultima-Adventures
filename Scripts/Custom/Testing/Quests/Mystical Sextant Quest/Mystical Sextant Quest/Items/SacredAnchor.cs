using System; 
using Server; 

namespace Server.Items 
{ 
   public class SacredAnchor : Item 
   { 
      [Constructable] 
      public SacredAnchor() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public SacredAnchor( int amount ) : base( 0x14F9 ) 
      {
	 Name = "A Sacred Anchor";
	 Stackable = false;
	 Hue = 683;
         Weight = 1.0; 
         Amount = amount; 
      } 

      public SacredAnchor( Serial serial ) : base( serial ) 
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