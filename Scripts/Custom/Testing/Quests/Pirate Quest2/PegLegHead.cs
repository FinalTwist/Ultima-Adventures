using System; 
using Server; 

namespace Server.Items 
{ 
   public class PegLegHead : Item 
   { 
      [Constructable] 
      public PegLegHead() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public PegLegHead( int amount ) : base( 7584 ) 
      {
	 Name = "Peg Leg's Head";
	 Stackable = false;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public PegLegHead( Serial serial ) : base( serial ) 
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