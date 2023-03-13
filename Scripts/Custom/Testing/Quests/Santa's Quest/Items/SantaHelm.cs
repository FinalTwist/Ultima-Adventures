using System;
using Server;

namespace Server.Items
{
  public class SantaHelm : LeatherCap 
   {  
      public override int ArtifactRarity{ get{ return 4; } } 

      public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } } 
      public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } } 
[Constructable] 
      public SantaHelm()
      {      Weight = 6.0; 
            Name = "Santa Cap"; 
            Hue = 33;

            Attributes.RegenMana=2;

   PhysicalBonus = 12; 
         FireBonus = 12; 
         ColdBonus = 12; 
         PoisonBonus =12; 
         EnergyBonus = 12; 
}

public SantaHelm( Serial serial ) : base( serial ) 
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


