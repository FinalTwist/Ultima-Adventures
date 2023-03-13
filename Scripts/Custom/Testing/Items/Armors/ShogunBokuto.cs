using System;
using Server;

namespace Server.Items

{
              
              public class ShogunBokuto : Bokuto
              {
              public override int AosMinDamage{ get{ return 20; } }
              public override int AosMaxDamage{ get{ return 30; } }
              
              public override int ArtifactRarity{ get{ return 110; } }
                      [Constructable]
                      public ShogunBokuto() 
                      {
                                        Weight = 1;
                                        Name = "Shogun Bokuto";
                                        Hue = 2729;
              
                                        WeaponAttributes.DurabilityBonus = 255;
                                        WeaponAttributes.HitDispel = 50;
                                        WeaponAttributes.HitHarm = 80;
                                        WeaponAttributes.HitLeechHits = 80;
                                        WeaponAttributes.HitMagicArrow = 30;
                                        WeaponAttributes.SelfRepair = 5;
              
                                        Attributes.AttackChance = 50;
                                        Attributes.DefendChance = 10;
                                        Attributes.SpellChanneling = 1;
                                        Attributes.WeaponDamage = 30;
                                        Attributes.WeaponSpeed = 30;
              
                                    }
              
                      public ShogunBokuto( Serial serial ) : base( serial )  
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
