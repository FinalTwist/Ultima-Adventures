using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Deadly Wyrm" )]
     public class DeadlyWyrm: BaseCreature
     {
         [Constructable]
	      public DeadlyWyrm() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
         {
             Name = "Deadly Wyrm";
             Body = 46;
             Hue = 2614;
             BaseSoundID = 362;
             SetStr( 1600 );
             SetDex( 179 );
             SetInt( 750 );
             SetHits( 1500 );
             SetDamage( 45 );
             SetDamageType( ResistanceType.Physical, 50 );
             SetDamageType( ResistanceType.Fire, 50 );
             SetDamageType( ResistanceType.Cold, 0 );
             SetDamageType( ResistanceType.Energy, 0 );
             SetDamageType( ResistanceType.Poison, 0 );

                SetResistance( ResistanceType.Physical, 75 );
                SetResistance( ResistanceType.Fire, 75 );
                SetResistance( ResistanceType.Cold, 77 );
                SetResistance( ResistanceType.Energy, 85 );
                SetResistance( ResistanceType.Poison, 75 );

		SetSkill( SkillName.EvalInt, 80.1, 100.0 );
		SetSkill( SkillName.Magery, 80.1, 100.0 );
		SetSkill( SkillName.Meditation, 52.5, 75.0 );
		SetSkill( SkillName.MagicResist, 100.5, 150.0 );
		SetSkill( SkillName.Tactics, 97.6, 100.0 );
		SetSkill( SkillName.Wrestling, 97.6, 100.0 );

             Fame = 5000;
             Karma = 5000;
             VirtualArmor = 1;
             ControlSlots = 5;
             Tamable = true;
}
             public override bool HasBreath{ get{ return true; } }
             public override bool BardImmune{ get{ return true; } }
             public override bool Unprovokable{ get{ return true; } }
             public override bool Uncalmable{ get{ return true; } }
             public override int Hides{ get{ return 50; } }  
             public override HideType HideType{ get{ return HideType.Barbed; } }


 public DeadlyWyrm( Serial serial ) : base( serial )
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
