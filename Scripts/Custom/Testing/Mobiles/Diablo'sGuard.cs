//Created by David AKA EvilPounder
//Shard: Lords of UO
using System;
using Server;
using Server.Items;

namespace Server.Items
{

              public class DiablosGuard: MetalKiteShield
{
              
              [Constructable]
              public DiablosGuard()
{

                          Weight = 2;
                          Name = "Diablo's Guard";
                          Hue = 2244;
              
              Attributes.BonusDex = 10;
              Attributes.BonusHits = 10;
              Attributes.BonusMana = 10;
              Attributes.CastRecovery = 1;
              Attributes.CastSpeed = 1;
              Attributes.DefendChance = 60;
              Attributes.LowerManaCost = 20;
              Attributes.Luck = 1500;
              Attributes.NightSight = 1;
              Attributes.SpellChanneling = 1;
              Attributes.WeaponDamage = 20;
              ArmorAttributes.DurabilityBonus = 100;
              ArmorAttributes.LowerStatReq = 100;
              ArmorAttributes.SelfRepair = 55;
              StrBonus = 10; 
                  }
              public DiablosGuard( Serial serial ) : base( serial )
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
