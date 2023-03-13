using System; 
using Server; 

namespace Server.Items 
{ 
   public class WarmPirateJacket : Item 
   { 
      [Constructable] 
      public WarmPirateJacket() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public WarmPirateJacket( int amount ) : base( 0x279C ) 
      {
	 Name = "Warm Pirate Jacket";
	 Hue = 1708;
         Weight = 0.1; 
           ItemID = 10140;   
         Amount = amount; 
      } 

      public  WarmPirateJacket( Serial serial ) : base( serial ) 
      { 
      } 

     // public override Item Dupe( int amount ) 
     // { 
       //  return base.Dupe( new GoldilocksMap( amount ), amount ); 
       

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
