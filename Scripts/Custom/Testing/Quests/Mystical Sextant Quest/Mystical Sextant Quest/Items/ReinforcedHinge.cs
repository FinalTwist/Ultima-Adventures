using System; 
using Server; 

namespace Server.Items 
{ 
   public class ReinforcedHinge : Item 
   { 
      [Constructable] 
      public ReinforcedHinge() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public ReinforcedHinge( int amount ) : base( 0x1055 ) 
      {
	 Name = "A Reinforced Hinge";
	 Stackable = false;
	 Hue = 1153;
         Weight = 1.0; 
         Amount = amount; 
      } 

      public ReinforcedHinge( Serial serial ) : base( serial ) 
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