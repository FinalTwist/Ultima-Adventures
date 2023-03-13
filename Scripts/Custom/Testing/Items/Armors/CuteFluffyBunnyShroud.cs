using System; 
using Server.Items; 

namespace Server.Items 
{ 
   public class CuteFluffyBunnyShroud : BaseArmor
   { 
      public override int PhysicalResistance{ get{ return 5; } } 
      public override int FireResistance{ get{ return 5; } } 
      public override int ColdResistance{ get{ return 5; } } 
      public override int PoisonResistance{ get{ return 5; } } 
      public override int EnergyResistance{ get{ return 5; } } 

      public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

                public override int ArtifactRarity{ get{ return 13; } } 

      [Constructable] 
      public CuteFluffyBunnyShroud() : base( 0x2684 ) 
      { 
			Name = "Cute Fluffy Bunny Suit";
			Hue = 2264;
			Weight = 3.0;

			Attributes.CastSpeed = 1;
			Attributes.CastRecovery = 1;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 5;
			Attributes.SpellDamage = 5;
			Attributes.LowerManaCost = 5;
			Attributes.LowerRegCost = 25;
			Attributes.BonusStr = 5;
			ArmorAttributes.MageArmor = 1;

      } 

      public CuteFluffyBunnyShroud( Serial serial ) : base( serial ) 
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
 
