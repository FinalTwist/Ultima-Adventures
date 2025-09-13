using System; 
using Server; 

namespace Server.Items 
{ 
   public class GraniteStone : Item 
   { 
      [Constructable] 
      public GraniteStone() : this( 1 ) 
      { 
      } 

      [Constructable] 
      public GraniteStone( int amount ) : base( 6009 ) 
      {
	 Name = "Ancient Granite Stone";
	 Stackable = true;
	 Hue = 1153;
        	 Weight = 0.1; 
         	 Amount = amount; 
      } 

      public GraniteStone( Serial serial ) : base( serial ) 
      { 
      } 

      //public override Item Dupe( int amount ) 
      //{ 
         //return base.Dupe( new GraniteStone( amount ), amount ); 
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