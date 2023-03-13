using System;
using Server.Items;

namespace Server.Mobiles

              {
              [CorpseName( " corpse of a ArcaneDaemonFromHell" )]
              public class ArcaneDaemonFromHell : ArcaneDaemon
              {
                                 [Constructable]
                                    public ArcaneDaemonFromHell() : base()
                            {
			AIFullSpeedActive = true; // Force full speed
			AIFullSpeedPassive = false;
                                               Name = "ArcaneDaemon";
                                               Title = "of B&T's Most Excelent Adventure";
						
                                               Hue = 0;
                                               Body = 113; // Uncomment these lines and input values
                                               //BaseSoundID = ; // To use your own custom body and sound.
                                               SetStr( 580 );
                                               SetDex( 210 );
                                               SetInt( 100 );
                                               SetHits( 800 );
                                               SetDamage( 35 );
                                               SetDamageType( ResistanceType.Physical, 100 );
                                               SetDamageType( ResistanceType.Cold, 0 );
                                               SetDamageType( ResistanceType.Fire, 5 );
                                               SetDamageType( ResistanceType.Energy, 3 );
                                               SetDamageType( ResistanceType.Poison, 3 );

                                               SetResistance( ResistanceType.Physical, 100 );
                                               SetResistance( ResistanceType.Cold, 2 );
                                               SetResistance( ResistanceType.Fire, 20 );
                                               SetResistance( ResistanceType.Energy, 20 );
                                               SetResistance( ResistanceType.Poison, 3 );
                                               Fame = 1000;
                                               Karma = -9000;
                                               VirtualArmor = 30;
     
                                               PackGold( 2000 );
                                               PackItem( new Longsword() ); 
                                               PackItem( new HellKey2() ); 

                            }
                                 public override bool HasBreath{ get{ return true ; } }
                                 public override Poison HitPoison{ get{ return Poison. Greater ; } }
                                 public override bool AlwaysMurderer{ get{ return true; } }

public ArcaneDaemonFromHell( Serial serial ) : base( serial )
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
			AIFullSpeedActive = true; // Force full speed
			AIFullSpeedPassive = false;
                      }
    }
}
