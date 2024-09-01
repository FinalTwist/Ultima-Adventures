using System;
using Server;
using Server.Items;


namespace Server.Items
{
              public class ShogunBracelet: GoldBracelet
{
              public override int ArtifactRarity{ get{ return 110; } }

              [Constructable]
              public ShogunBracelet()
{

                          Weight = 3;
                          Name = "Shogun Bracelet";
                          Hue = 2959;
              
              Attributes.AttackChance = 25;
              Attributes.BonusDex = 10;
              Attributes.CastRecovery = 4;
              Attributes.CastSpeed = 2;
              Attributes.DefendChance = 40;
              Attributes.LowerManaCost = 20;
              Attributes.LowerRegCost = 25;
              Attributes.SpellDamage = 30;
              Attributes.BonusStr = 10;
                  }
              public ShogunBracelet( Serial serial ) : base( serial )
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
