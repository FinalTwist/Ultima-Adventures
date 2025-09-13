using System;
using Server;

namespace Server.Items
{
  public class SantaLegs : LeatherLegs
   {  
      public override int ArtifactRarity{ get{ return 4; } } 

      public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } } 
      public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } } 
[Constructable] 
      public SantaLegs()
      {      Weight = 6.0; 
            Name = "Santa Legs"; 
            Hue = 33;
        
            ArmorAttributes.SelfRepair=2;
            Attributes.WeaponDamage=10;
            Attributes.SpellDamage=5;
            Attributes.LowerRegCost=12;

   PhysicalBonus = 8; 
         FireBonus = 4; 
         ColdBonus = 8; 
         PoisonBonus =4; 
         EnergyBonus = 4; 
}

public SantaLegs( Serial serial ) : base( serial ) 
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


