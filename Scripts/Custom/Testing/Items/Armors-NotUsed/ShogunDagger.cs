//Created by Dudus
using System;
using Server;

namespace Server.Items

{
              
              public class ShogunDagger : Dagger
              {
              public override int AosMinDamage{ get{ return 20; } }
              public override int AosMaxDamage{ get{ return 30; } }
              
              public override int ArtifactRarity{ get{ return 110; } }

                      [Constructable]
                      public ShogunDagger() 
                      {
                                        Weight = 1;
                                        Name = "Shogun Dagger";
                                        Hue = 2729;
              
                                        WeaponAttributes.HitLightning = 80;
              
                                        Attributes.AttackChance = 33;
                                        Attributes.DefendChance = 10;
                                        Attributes.SpellChanneling = 1;
                                        Attributes.SpellDamage = 15;
                                        Attributes.WeaponDamage = 30;
                                        Attributes.WeaponSpeed = 50;
              
                                    }
              
                      public ShogunDagger( Serial serial ) : base( serial )  
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
