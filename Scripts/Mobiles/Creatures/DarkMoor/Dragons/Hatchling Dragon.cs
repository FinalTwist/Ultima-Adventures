using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "a dead hatchling dragon" )]
     public class HatchlingDragon: BaseCreature
     {
         [Constructable]
	      public HatchlingDragon() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
         {
             Name = "Hatchling Dragon";
             Body = 269;
             BaseSoundID = 362;
             SetStr( 250 );
             SetDex( 100 );
             SetInt( 150 );
             SetHits( 150 );
             SetDamage( 40 );
             SetDamageType( ResistanceType.Physical, 50 );
             SetDamageType( ResistanceType.Fire, 50 );
             SetDamageType( ResistanceType.Cold, 0 );
             SetDamageType( ResistanceType.Energy, 0 );
             SetDamageType( ResistanceType.Poison, 0 );

                SetResistance( ResistanceType.Physical, 69 );
                SetResistance( ResistanceType.Fire, 69 );
                SetResistance( ResistanceType.Cold, 69 );
                SetResistance( ResistanceType.Energy, 40 );
                SetResistance( ResistanceType.Poison, 69 );

		SetSkill( SkillName.EvalInt, 80.1, 100.0 );
		SetSkill( SkillName.Magery, 80.1, 100.0 );
		SetSkill( SkillName.Meditation, 52.5, 75.0 );
		SetSkill( SkillName.MagicResist, 100.5, 150.0 );
		SetSkill( SkillName.Tactics, 97.6, 100.0 );
		SetSkill( SkillName.Wrestling, 97.6, 100.0 );

             Fame = 5000;
             Karma = 5000;
             VirtualArmor = 1;
             ControlSlots = 2;
             MinTameSkill = 20;
             Tamable = true;

}
             public override bool HasBreath{ get{ return true; } }
             public override bool BardImmune{ get{ return true; } }
             public override bool Unprovokable{ get{ return true; } }
             public override bool Uncalmable{ get{ return true; } }
             public override int Hides{ get{ return 10; } }  
             public override HideType HideType{ get{ return HideType.Barbed; } }
             public override bool CanAngerOnTame { get { return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }

		

        public HatchlingDragon( Serial serial ) : base( serial )
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
