using System;
using Server;

namespace Server.Items
{
  public class SantaTunic : LeatherChest 
   {  
      public override int ArtifactRarity{ get{ return 4; } } 

      public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } } 
      public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } } 
[Constructable] 
      public SantaTunic()  
      {      Weight = 6.0; 
            Name = "Santa Tunic"; 
            Hue = 33;
        
            ArmorAttributes.SelfRepair=1;
            Attributes.SpellDamage=5;
            Attributes.LowerRegCost=10;
            Attributes.DefendChance=7;

   PhysicalBonus = 4; 
         FireBonus = 8; 
         ColdBonus = 8; 
         PoisonBonus =5; 
         EnergyBonus = 4; 
}

public SantaTunic( Serial serial ) : base( serial ) 
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


