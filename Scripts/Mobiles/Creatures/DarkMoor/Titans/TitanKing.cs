// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Titan King Corpse" )]
     public class TitanKing: BaseCreature
     {
         [Constructable]
		public TitanKing () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Titan King";
             Body = 0x4C;
             Hue = 2030;
             BaseSoundID = 609;
             SetStr( 780, 900 );
             SetDex( 140, 200 );
             SetInt( 75 );
             SetHits( 1750, 2500 );
             SetDamage( 42, 65 );
             SetDamageType( ResistanceType.Physical, 5 );
             SetDamageType( ResistanceType.Fire, 35 );
             SetDamageType( ResistanceType.Cold, 35 );
             SetDamageType( ResistanceType.Energy, 10 );
             SetDamageType( ResistanceType.Poison, 15 );

             SetResistance( ResistanceType.Physical, 65 );
             SetResistance( ResistanceType.Fire, 65 );
             SetResistance( ResistanceType.Cold, 70 );
             SetResistance( ResistanceType.Energy, 75 );
             SetResistance( ResistanceType.Poison, 70 );
	SetSkill( SkillName.MagicResist, 105.1, 115.0 );
	SetSkill( SkillName.Tactics, 105.1, 115.0 );
	SetSkill( SkillName.Wrestling, 105.1, 115.0 );



             Fame = 10000;
             Karma = 5000;
             VirtualArmor = 10;
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

        public TitanKing( Serial serial ) : base( serial )
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
