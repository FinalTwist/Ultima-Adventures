using System;
using Server;

namespace Server.Items
{
  public class SantaArms : LeatherArms 
   {  
      public override int ArtifactRarity{ get{ return 4; } } 

      public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } } 
      public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } } 
[Constructable] 
      public SantaArms()
      {      Weight = 6.0; 
            Name = "Santa Arms"; 
            Hue = 33;
        
            Attributes.AttackChance=5;
            Attributes.RegenMana=1;
	    Attributes.RegenHits=1;
	    Attributes.RegenStam=1;
	    Attributes.LowerRegCost=15;

   PhysicalBonus = 3; 
         FireBonus = 4; 
         ColdBonus = 9; 
         PoisonBonus =6; 
         EnergyBonus = 9; 
}

public SantaArms( Serial serial ) : base( serial ) 
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


