using System; 
using Server; 
using Server.Commands;

namespace Server.Items 
{ 
   public class GanonHead : Item 
   { 
      [Constructable] 
      public GanonHead() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public GanonHead( int amount ) : base( 7584 ) 
      {
	 Name = "Ganon's Head";
	 Stackable = false;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public GanonHead( Serial serial ) : base( serial ) 
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
