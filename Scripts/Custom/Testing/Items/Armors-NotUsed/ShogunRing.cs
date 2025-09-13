using System;
using Server;
using Server.Items;


namespace Server.Items
{
              public class ShogunRing: GoldRing
{
              public override int ArtifactRarity{ get{ return 110; } }
              [Constructable]
              public ShogunRing()
{

                          Weight = 3;
                          Name = "Shogun Ring";
                          Hue = 2729;
              
              Attributes.AttackChance = 30;
              Attributes.BonusDex = 15;
              Attributes.DefendChance = 30;
              Attributes.Luck = 1000;
              Attributes.SpellDamage = 35;
             
                  }
              public ShogunRing( Serial serial ) : base( serial )
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
