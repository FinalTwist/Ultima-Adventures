using System;
using Server;

namespace Server.Items
{
  public class OrcSkinGorgetofTheBarbarian : LeatherGorget 
   {  
      public override int ArtifactRarity{ get{ return 21; } } 

      public override int InitMinHits{ get{ return 2550; } } 
      public override int InitMaxHits{ get{ return 2550; } } 
[Constructable] 
      public OrcSkinGorgetofTheBarbarian()
      {      Weight = 6.0; 
            Name = "Orc Skin Gorget of The Barbarian"; 
            Hue = 2101;
        
            ArmorAttributes.SelfRepair=15;
            Attributes.WeaponDamage=5;
            Attributes.BonusStr=5;
            Attributes.WeaponSpeed=5;
            ArmorAttributes.MageArmor=1;

   PhysicalBonus = 10; 
         FireBonus = 7; 
         ColdBonus = 11; 
         PoisonBonus =7; 
         EnergyBonus = 17; 
}

public OrcSkinGorgetofTheBarbarian( Serial serial ) : base( serial ) 
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


