using System;
using Server;
using Server.Items;


namespace Server.Items
{
              public class ShogunEarrings: GoldEarrings
{
              
              [Constructable]
              public ShogunEarrings()
{

                          Weight = 3;
                          Name = "Shogun Earrings";
                          Hue = 2959;
              
              Attributes.BonusHits = 20;
              Attributes.BonusInt = 20;
              Attributes.BonusMana = 20;
              Attributes.EnhancePotions = 40;
              Attributes.Luck = 1000;
              Attributes.NightSight = 1;
              Attributes.ReflectPhysical = 30;
              
                  }
              public ShogunEarrings( Serial serial ) : base( serial )
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
