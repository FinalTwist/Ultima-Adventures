using System;
using Server;

namespace Server.Items
{
  public class OrcBoneArmsofTheBarbarian : BoneArms 
   {  
      public override int ArtifactRarity{ get{ return 21; } } 

      public override int InitMinHits{ get{ return 2550; } } 
      public override int InitMaxHits{ get{ return 2550; } } 
[Constructable] 
      public OrcBoneArmsofTheBarbarian()
      {      Weight = 6.0; 
            Name = "Orc Bone Arms of The Barbarian"; 
            Hue = 2101;
        
            ArmorAttributes.SelfRepair=10;
            Attributes.BonusStr=10;
            Attributes.AttackChance=5;
            Attributes.RegenStam=10;
            ArmorAttributes.MageArmor=1;

   PhysicalBonus = 25; 
         FireBonus = 10; 
         ColdBonus = 15; 
         PoisonBonus =5; 
         EnergyBonus = 15; 
}

public OrcBoneArmsofTheBarbarian( Serial serial ) : base( serial ) 
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


