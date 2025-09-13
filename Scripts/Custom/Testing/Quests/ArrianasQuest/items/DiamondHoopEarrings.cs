using System; 
using Server; 

namespace Server.Items 
{ 
   public class DiamondHoopEarrings : Item 
   { 
      [Constructable] 
      public DiamondHoopEarrings() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public DiamondHoopEarrings( int amount ) : base( 0x1F07 ) 
      {
	 Name = "Diamond Hoop Earrings";
	 Hue = 1154;
         Weight = 0.1; 

      } 

      public DiamondHoopEarrings( Serial serial ) : base( serial ) 
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