//Made By Brad Poe
using System;
using Server.Items;

namespace Server.Items
{
  public class FurCapeofTheBarbarian : BaseArmor
   {  
      public override int ArtifactRarity{ get{ return 21; } } 

      public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } } 
      public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } } 

public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

[Constructable] 
      public FurCapeofTheBarbarian() : base( 0x2309 ) 
      {      Weight = 6.0; 
            Name = "Fur Cape of The Barbarian"; 
            Hue = 1150;
        
            ArmorAttributes.SelfRepair=10;
            Attributes.WeaponDamage=5;
            Attributes.AttackChance=15;
            Attributes.WeaponSpeed=5;
            ArmorAttributes.MageArmor=1;
            Attributes.DefendChance=10;

   PhysicalBonus = 17; 
         FireBonus = 10; 
         ColdBonus = 17; 
         PoisonBonus =10; 
         EnergyBonus = 27; 
}

public FurCapeofTheBarbarian( Serial serial ) : base( serial ) 
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


