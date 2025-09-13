using System;
using Server;

namespace Server.Items
{
  public class SantaGorget : LeatherGorget
   {  
      public override int ArtifactRarity{ get{ return 4; } } 

      public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } } 
      public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } } 
[Constructable] 
      public SantaGorget()
      {      Weight = 6.0; 
            Name = "Santa Gorget"; 
            Hue = 33;
        
            ArmorAttributes.SelfRepair=2;
            Attributes.WeaponDamage=10;
            Attributes.SpellDamage=5;
            Attributes.LowerRegCost=12;

   PhysicalBonus = 4; 
         FireBonus = 8; 
         ColdBonus = 4; 
         PoisonBonus =4; 
         EnergyBonus = 8; 
}

public SantaGorget( Serial serial ) : base( serial ) 
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


