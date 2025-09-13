using System;
using Server;

namespace Server.Items

{
              
              public class BusterSword : VikingSword
              {
              public override int ArtifactRarity{ get{ return 19; } } 
              public override int AosMinDamage{ get{ return 20; } }
              public override int AosMaxDamage{ get{ return 25; } }
              
                      [Constructable]
                      public BusterSword() 
                      {
                                        Weight = 5;
                                        Name = "Buster Sword";
                                        Hue = 1175;
					LootType = LootType.Blessed;
              
                                        WeaponAttributes.DurabilityBonus = 20;
                                       	WeaponAttributes.HitColdArea = 25;
                                        WeaponAttributes.HitEnergyArea = 25;
                                        WeaponAttributes.HitFireArea = 25;
                                        WeaponAttributes.HitHarm = 25;
                                        WeaponAttributes.HitLeechHits = 30;
                                        WeaponAttributes.HitLightning = 25;
                                        WeaponAttributes.HitLowerAttack = 5;
                                        WeaponAttributes.SelfRepair = 5;
              
                                        Attributes.AttackChance = 5;
                                        Attributes.BonusDex = 10;
                                        Attributes.BonusHits = 10;
                                        Attributes.BonusInt = 10;
                                        Attributes.BonusMana = 10 ;
                                        Attributes.BonusStam = 10;
                                       Attributes.ReflectPhysical = 5;
                                        Attributes.SpellChanneling = 1;
              
                                    }
              
                      public BusterSword( Serial serial ) : base( serial )  
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
