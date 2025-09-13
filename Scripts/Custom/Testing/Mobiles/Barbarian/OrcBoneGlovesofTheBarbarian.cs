//Made By Brad Poe
using System;
using Server;

namespace Server.Items
{
  public class OrcBoneGlovesofTheBarbarian : BoneGloves 
   {  
      public override int ArtifactRarity{ get{ return 21; } } 

      public override int InitMinHits{ get{ return 2550; } } 
      public override int InitMaxHits{ get{ return 2550; } } 
[Constructable] 
      public OrcBoneGlovesofTheBarbarian()
      {      Weight = 6.0; 
            Name = "Orc Bone Gloves of The Barbarian"; 
            Hue = 2101;
        
            ArmorAttributes.SelfRepair=15;
            Attributes.WeaponDamage=5;
            Attributes.WeaponSpeed=5;
            ArmorAttributes.MageArmor=1;

   PhysicalBonus = 15; 
         FireBonus = 15; 
         ColdBonus = 15; 
         PoisonBonus =15; 
         EnergyBonus = 15; 
}

public OrcBoneGlovesofTheBarbarian( Serial serial ) : base( serial ) 
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


