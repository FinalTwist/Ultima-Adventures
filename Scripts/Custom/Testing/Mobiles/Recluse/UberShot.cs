// Created by David aka EvilPounder
// Server: Lords of UO
using System;
using Server;
namespace Server.Items
{
 public class UberShot : Bow
 {
 public override float MlSpeed{ get{ return 3.75f; } }
 public override int InitMinHits{ get{ return 225;}}
 public override int InitMaxHits{ get{ return 225;}}
 [Constructable]
 public UberShot()
     {
         Name = "Fang Bow";
         Attributes.WeaponDamage = 75;
         Attributes.AttackChance = 20;
         Attributes.DefendChance = 20;
         //WeaponAttributes.HitColdArea = 50;
         WeaponAttributes.HitDispel = 50;
         WeaponAttributes.HitLeechHits = 35;
         WeaponAttributes.HitLeechMana = 35;
         WeaponAttributes.HitLightning = 25;
         WeaponAttributes.HitMagicArrow = 25;
         WeaponAttributes.HitFireball = 25;
         //WeaponAttributes.SelfRepair = 75;
         //WeaponAttributes.UseBestSkill = 1;
         Attributes.BonusDex = 10;
         //Attributes.BonusHits = 30;
         //Attributes.BonusInt = 20;
         //Attributes.BonusMana = 20;
         //Attributes.BonusStam = 75;
         Attributes.Luck = 175;
         //Attributes.NightSight = 1;
         Attributes.ReflectPhysical = 15;
         //Attributes.RegenHits = 20;
         //Attributes.RegenMana = 20;
         //Attributes.RegenStam = 20;
         Attributes.SpellChanneling = 1;
         Attributes.WeaponSpeed = 60;
         //LootType = LootType.Blessed;
         //Slayer = SlayerName.OrcSlaying;
Hue = 1197;
     }
public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
    {
            phys = fire = cold = pois = chaos = direct = 7;
			nrgy = 20;
     }
public UberShot( Serial serial ) : base( serial )
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
