using System; 
using Server; 

namespace Server.Items 
{ 
   public class EnchantedRope : Item 
   { 
      [Constructable] 
      public EnchantedRope() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public EnchantedRope( int amount ) : base( 0x14F8 ) 
      {
	 Name = "Enchanted Rope";
	 Stackable = false;
	 Hue = 1153;
	 Weight = 1.0; 
         Amount = amount; 
      } 

      public EnchantedRope( Serial serial ) : base( serial ) 
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