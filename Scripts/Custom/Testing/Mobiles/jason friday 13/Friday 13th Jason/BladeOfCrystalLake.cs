
using System;
using Server;

namespace Server.Items

{
              
              public class BladeOfCrystalLake : RadiantScimitar
              {
              
                      [Constructable]
                      public BladeOfCrystalLake() 
                      {
                                        Weight = 0;
                                        Name = "Blade Of Crystal Lake";
                                        Hue = 43;
              
                                        WeaponAttributes.DurabilityBonus = 100;
                                        WeaponAttributes.HitLeechHits = 50;
                                        WeaponAttributes.HitLeechMana = 50;
                                        WeaponAttributes.HitLightning = 50;
                                        WeaponAttributes.SelfRepair = 20;
                                        WeaponAttributes.UseBestSkill = 1;
              
                                        Attributes.AttackChance = 15;
                                        Attributes.BonusDex = 10;
                                        Attributes.BonusHits = 20;
                                        Attributes.BonusInt = 10;
                                       Attributes.CastRecovery = 4;
                                        Attributes.CastSpeed = 3;
                                        Attributes.DefendChance = 15;
                                        Attributes.Luck = 200;
                                        Attributes.NightSight = 1;
                                       Attributes.ReflectPhysical = 15;
                                        Attributes.SpellChanneling = 1;
                                        Attributes.WeaponDamage = 60;
                                        Attributes.WeaponSpeed = 50;
              
                                    }
              
                      public BladeOfCrystalLake( Serial serial ) : base( serial )  
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
