using System;
using Server;
using Server.Items;


namespace Server.Items
{
              public class ShogunNecklace: Necklace
{
              
              [Constructable]
              public ShogunNecklace()
{

                          Weight = 3;
                          Name = "Shogun Necklace";
                          Hue = 2729;
              
              Attributes.CastRecovery = 20;
              Attributes.CastSpeed = 15;
              Attributes.DefendChance = 20;
              Attributes.LowerManaCost = 30;
              Attributes.LowerRegCost = 40;
                  }
              public ShogunNecklace( Serial serial ) : base( serial )
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
