// Created by David aka EvilPounder
// Server: Lords of UO

using System;
using Server.Items;

namespace Server.Mobiles

              {
              [CorpseName( " corpse of an Uber Reluse Spider" )]
              public class UberRecluseSpider : DreadSpider
              {
                                 [Constructable]
                                    public UberRecluseSpider() : base()
                            {
                                               Name = "Uber Recluse Spider";
                                               Hue = 2879;
                                               Body = 173; // Uncomment these lines and input values
                                               //BaseSoundID = ; // To use your own custom body and sound.
                                               SetStr( 1450 );
                                               SetDex( 880 );
                                               SetInt( 987 );
                                               SetHits( 15000 );
                                               SetDamage( 25, 45 );
                                               SetDamageType( ResistanceType.Physical, 65 );
                                               SetDamageType( ResistanceType.Cold, 0 );
                                               SetDamageType( ResistanceType.Fire, 0 );
                                               SetDamageType( ResistanceType.Energy, 55 );
                                               SetDamageType( ResistanceType.Poison, 100 );

                                               SetResistance( ResistanceType.Physical, 40 );
                                               SetResistance( ResistanceType.Cold, 0 );
                                               SetResistance( ResistanceType.Fire, 0 );
                                               SetResistance( ResistanceType.Energy, 40 );
                                               SetResistance( ResistanceType.Poison, 100 );
                                               Fame = -1000;
                                               Karma = -1000;
                                               VirtualArmor = 55;

                                               switch ( Utility.Random( 40 ))
                                               {                                   
            	                                   case 0: AddItem( new FangoftheRecluse() ); break;
			                           case 1: AddItem( new UberShot() ); break;
                                                   //case 2: AddItem( new () ); break;
                                               }
                                            }
            
                                            public override void GenerateLoot()
                                            {
            	                               //PackGold( 10000 );
            	                               AddLoot( LootPack.FilthyRich, 2);
            	                               AddLoot( LootPack.Gems, Utility.Random( 1, 5));
                                            }    

                                 public override bool AutoDispel{ get{ return true; } }
                                 public override bool BardImmune{ get{ return true; } }
                                 public override bool Unprovokable{ get{ return true; } }
                                 public override Poison HitPoison{ get{ return Poison. Lethal ; } }
                                 public override bool AlwaysMurderer{ get{ return true; } }

public UberRecluseSpider( Serial serial ) : base( serial )
                      {
                      }

public override void Serialize( GenericWriter writer )
                      {
                                        base.Serialize( writer );
                                        writer.Write( (int) 0 );
                      }

        public override void Deserialize( GenericReader reader )
                      {
                                        base.Deserialize( reader );
                                        int version = reader.ReadInt();
                      }
    }
}
