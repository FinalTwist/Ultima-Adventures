using System; 
using Server; 

namespace Server.Items 
{ 
   public class GlowingShipModel : Item 
   { 
      [Constructable] 
      public GlowingShipModel() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public GlowingShipModel( int amount ) : base( 0x14F4 ) 
      {
	 Name = "A Glowing Ship Model";
	 Stackable = false;
	 Hue = 1153;
         Weight = 1.0; 
         Amount = amount; 
      } 

      public GlowingShipModel( Serial serial ) : base( serial ) 
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