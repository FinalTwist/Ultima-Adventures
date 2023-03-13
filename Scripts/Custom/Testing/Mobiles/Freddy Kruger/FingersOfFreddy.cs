
using System;
using Server;

namespace Server.Items

{
              
              public class FingersOfFreddy : Tekagi
              {
              
                      [Constructable]
                      public FingersOfFreddy() 
                      {
                                        Weight = 0;
                                        Name = "Fingers Of Freddy";
                                        Hue = 33;

                                        WeaponAttributes.HitLeechHits = 50;
                                        WeaponAttributes.HitLeechMana = 50;
                                        WeaponAttributes.HitLightning = 50;
                                        WeaponAttributes.SelfRepair = 20;
                                        Attributes.AttackChance = 15;
                                        Attributes.BonusDex = 6;
                                        Attributes.BonusHits = 6;
                                        Attributes.BonusInt = 6;
                                        Attributes.DefendChance = 10;
                                        Attributes.ReflectPhysical = 10;
                                        Attributes.SpellChanneling = 1;
                                        Attributes.WeaponDamage = 60;
                                        Attributes.WeaponSpeed = 50;
              
                                    }
              
                      public FingersOfFreddy( Serial serial ) : base( serial )  
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
