// Created by Script Creator
// From Aries at Revenge of the Gods
using System;
using Server;
namespace Server.Items
{
 public class BlueDragonClaws : DragonGloves
 {
 public override int InitMinHits{ get{ return 100;}}
 public override int InitMaxHits{ get{ return 100;}}
 [Constructable]
 public BlueDragonClaws()
     {
         Hue = 0x9F7;
         Name = "Blue Dragon Claws";
         ColdBonus = 35;
         EnergyBonus = 15;
         PhysicalBonus = 45;
         PoisonBonus = 25;
         FireBonus = 55;
         ArmorAttributes.SelfRepair = 5;
         Attributes.BonusHits = 25;
         Attributes.BonusMana = 15;
         Attributes.BonusStam = 30;
         Attributes.WeaponSpeed = 40;
         Attributes.BonusStr = 30;
         Attributes.WeaponDamage = 35;
         //Attributes.LowerRegCost = 0;
         Attributes.Luck = 70;
         Attributes.ReflectPhysical = 5;
         Attributes.RegenHits = 20;
         StrRequirement = 110;
         LootType = LootType.Blessed;
     }
public BlueDragonClaws( Serial serial ) : base( serial )
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
