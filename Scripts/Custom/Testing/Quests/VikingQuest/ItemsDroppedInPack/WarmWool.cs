using System; 
using Server; 

namespace Server.Items 
{ 
   public class WarmWool : Item 
   { 
      [Constructable] 
      public WarmWool() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public WarmWool( int amount ) : base( 0xDF8 ) 
      {
	 Name = "Warm Wool";
	 Hue = 0;
         Weight = 0.1; 
         Amount = amount; 
      } 

      public WarmWool( Serial serial ) : base( serial ) 
      { 
      } 

      //public override Item Dupe( int amount ) 
      //{ 
        // return base.Dupe( new FuzzyBabyBear( amount ), amount ); 
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
