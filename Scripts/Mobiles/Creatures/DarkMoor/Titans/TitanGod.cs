// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Titan God Corpse" )]
     public class TitanGod: BaseCreature
     {
         [Constructable]
		public TitanGod () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Titan God";
             Body = 0xBD;
             Hue = 2233;
             BaseSoundID = 609;
             SetStr( 820, 950 );
             SetDex( 140, 200 );
             SetInt( 75, 200 );
             SetHits( 5000, 6000 );
             SetDamage( 20, 75 );
             SetDamageType( ResistanceType.Physical, 5 );
             SetDamageType( ResistanceType.Fire, 35 );
             SetDamageType( ResistanceType.Cold, 35 );
             SetDamageType( ResistanceType.Energy, 10 );
             SetDamageType( ResistanceType.Poison, 15 );

             SetResistance( ResistanceType.Physical, 40, 60 );
             SetResistance( ResistanceType.Fire, 30, 40 );
             SetResistance( ResistanceType.Cold, 40, 70 );
             SetResistance( ResistanceType.Energy, 75, 80 );
             SetResistance( ResistanceType.Poison, 40, 70 );

            SetSkill( SkillName.MagicResist, 105.1, 115.0 );
            SetSkill( SkillName.Tactics, 105.1, 115.0 );
            SetSkill( SkillName.Wrestling, 105.1, 115.0 );

             Fame = 10000;
             Karma = 5000;
                          ControlSlots = 2;
             MinTameSkill = 20;
             Tamable = true;

}
         public override bool HasBreath{ get{ return true; } }
             public override int Scales{ get{ return 500; } }
             public override bool CanAngerOnTame { get { return true; } }
             public override bool BardImmune{ get{ return true; } }
             public override bool Unprovokable{ get{ return true; } }
             public override bool Uncalmable{ get{ return true; } } 


        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }
        public TitanGod( Serial serial ) : base( serial )
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
