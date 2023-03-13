using System;
using Server.Items;

namespace Server.Mobiles

              {
              [CorpseName( " corpse of a Mondain The Wizard" )]
              public class MondainTheWizard : BaseCreature
              {
                                 [Constructable]
                                    public MondainTheWizard() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
                            {
                                               Name = "Mondain";
						Title = "The Wizard";
                                               Hue = 0;
                                               Body = 400;
                                               BaseSoundID = 1154;
                                               SetStr( 200 );
                                               SetDex( 200 );
                                               SetInt( 200 );
                                               SetHits( 15000 );
                                               SetDamage( 35 );
                                               SetDamageType( ResistanceType.Physical, 69 );
                                               SetDamageType( ResistanceType.Cold, 95 );
                                               SetDamageType( ResistanceType.Fire, 98 );
                                               SetDamageType( ResistanceType.Energy, 86 );
                                               SetDamageType( ResistanceType.Poison, 84 );

                                               SetResistance( ResistanceType.Physical, 50 );
                                               SetResistance( ResistanceType.Cold, 58 );
                                               SetResistance( ResistanceType.Fire, 55 );
                                               SetResistance( ResistanceType.Energy, 55 );
                                               SetResistance( ResistanceType.Poison, 56 );
                                               Fame = 12000;
                                               Karma = 12000;
                                               VirtualArmor = 48;
						new EtherealCuSidhe().Rider = this;

						AddItem( new Robe( 1153 ) );
						AddItem( new FancyShirt( 1153 ) );
						AddItem( new Doublet( 1153 ) ); 
         					AddItem( new Cloak( 1153 ) ); 
         					AddItem( new Sandals( 1153 ) );
						AddItem( new HolySword() );
     
                                               PackGold( 5000, 6000 );
						PackItem( new HeadOfMondain() );

                            }


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Gems, 5 );			
		}

                                 public override bool BardImmune{ get{ return true; } }
                                 public override Poison HitPoison{ get{ return Poison. Lethal ; } }
                                 public override bool AlwaysMurderer{ get{ return true; } }

public MondainTheWizard( Serial serial ) : base( serial )
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
