//Made By Brad Poe
using System; 
using Server.Items; 

namespace Server.Items 
{ 
   public class BearMaskofTheBarbarian : BaseArmor 
   { 
      public override int PhysicalResistance{ get{ return 26; } } 
                public override int FireResistance{ get{ return 15; } } 
                public override int ColdResistance{ get{ return 28; } } 
                public override int PoisonResistance{ get{ return 14; } } 
                public override int EnergyResistance{ get{ return 24; } } 

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

      public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } } 

                public override int ArtifactRarity{ get{ return 25; } } 

      [Constructable] 
      public BearMaskofTheBarbarian() : base( 0x1545 ) 
      { 
         Weight = 30; 
                        Hue = 1150; 
                        Name = "Bear Mask of The Barbarian"; 
                        StrRequirement = 40; 
                        HitPoints = Utility.RandomMinMax(100, 125);
                        MaxHitPoints = Utility.RandomMinMax(126, 150);
                        Attributes.BonusStr = 12; 
                        Attributes.ReflectPhysical = 30; 
                        Attributes.AttackChance = 10;
			Attributes.WeaponDamage = 16;
			Attributes.DefendChance = 10;
  

      } 

      public BearMaskofTheBarbarian( Serial serial ) : base( serial ) 
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
 
