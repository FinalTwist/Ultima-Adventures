using System;
using Server;

namespace Server.Items
{
  public class SantaGloves : LeatherGloves 
   {  
      public override int ArtifactRarity{ get{ return 4; } } 

      public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } } 
      public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } } 
[Constructable] 
      public SantaGloves()
      {      Weight = 6.0; 
            Name = "Santa Gloves"; 
            Hue = 33;
        
            ArmorAttributes.SelfRepair=3;
            Attributes.WeaponDamage=10;
            Attributes.WeaponSpeed=5;
            Attributes.ReflectPhysical=5;
	    Attributes.LowerRegCost=5;

   PhysicalBonus = 9; 
         FireBonus = 9; 
         ColdBonus = 5; 
         PoisonBonus =5; 
         EnergyBonus = 4; 
}

public SantaGloves( Serial serial ) : base( serial ) 
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


