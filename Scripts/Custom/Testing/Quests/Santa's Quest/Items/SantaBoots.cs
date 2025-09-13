using System;
using Server;

namespace Server.Items
{
  public class SantaBoots : Boots
	{

[Constructable] 
      public SantaBoots()
      {      Weight = 6.0; 
            Name = "Santa Boots"; 
            Hue = 1369;

            LootType = LootType.Blessed;

        

}

public SantaBoots( Serial serial ) : base( serial ) 
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); 
      } 
        
      public override void Deserialize(GenericReader reader) 
      { 
         base.Deserialize( reader ); 

         int version = reader.ReadInt(); 
      } 
   } 
} 


