//Made By Brad Poe
using System;
using Server.Items;

namespace Server.Items
{
  public class FurBootsofTheBarbarian : BaseArmor
   {  
      public override int ArtifactRarity{ get{ return 21; } } 

      public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } } 
      public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } } 

public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

[Constructable] 
      public FurBootsofTheBarbarian() : base( 0x2307 )
      {      Weight = 6.0; 
            Name = "Fur Boots of The Barbarian"; 
            Hue = 2101;
        
            ArmorAttributes.SelfRepair=15;
            Attributes.WeaponDamage=15;
            Attributes.BonusStam=15;
            ArmorAttributes.MageArmor=1;

   PhysicalBonus = 25; 
         FireBonus = 15; 
         ColdBonus = 15; 
         PoisonBonus =15; 
         EnergyBonus = 15; 
}

public FurBootsofTheBarbarian( Serial serial ) : base( serial ) 
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


