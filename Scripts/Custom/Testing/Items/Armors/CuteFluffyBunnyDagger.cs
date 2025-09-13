//Mangled by Revid >:)
using System;
using Server;
using Server.Items;

namespace Server.Items

{
              
              public class CuteFluffyBunnyDagger : Dagger
              {
              public override int AosMinDamage{ get{ return 20; } }
              public override int AosMaxDamage{ get{ return 30; } }
              
                      [Constructable]
                      public CuteFluffyBunnyDagger() 
                      {
                       Weight = 1;
                       Name = "Cute Fluffy Bunny Carrot Peeler";
                       Hue = 2264;
              
                       WeaponAttributes.HitLightning = 80;
              
                       Attributes.AttackChance = 33;
                       Attributes.DefendChance = 10;
                       Attributes.SpellChanneling = 1;
                       Attributes.SpellDamage = 15;
                       Attributes.WeaponDamage = 30;
                       Attributes.WeaponSpeed = 50;
              
                                    }
              
                      public CuteFluffyBunnyDagger( Serial serial ) : base( serial )  
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
