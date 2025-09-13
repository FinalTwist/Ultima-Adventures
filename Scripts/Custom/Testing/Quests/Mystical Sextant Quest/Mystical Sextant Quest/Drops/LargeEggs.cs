using System; 
using Server; 

namespace Server.Items 
{ 
   public class LargeEggs : Item 
   { 
      [Constructable] 
      public LargeEggs() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public LargeEggs( int amount ) : base( 0x9B5 ) 
      {
	 Name = "Large Eggs";
	 Stackable = false;
	 Hue = 542;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public LargeEggs( Serial serial ) : base( serial ) 
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