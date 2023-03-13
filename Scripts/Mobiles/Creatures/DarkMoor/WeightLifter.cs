// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Weight Lifter" )]
     public class WeightLifter: BaseCreature
     {
         [Constructable]
		public WeightLifter () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.8 ) 
         {
             Name = "Weight Lifter";
             Body = 53;
             Hue = 33770;
             BaseSoundID = 898;
             SetStr( 1000 );
             SetDex( 180 );
             SetInt( 140 );
             SetHits( 5000 );
             SetDamage( 80 );
             SetDamageType( ResistanceType.Physical, 40 );
             SetDamageType( ResistanceType.Fire, 40 );
             SetDamageType( ResistanceType.Cold, 40 );
             SetDamageType( ResistanceType.Energy, 40 );
             SetDamageType( ResistanceType.Poison, 40 );

             SetResistance( ResistanceType.Physical, 90 );
             SetResistance( ResistanceType.Fire, 90 );
             SetResistance( ResistanceType.Cold, 90 );
             SetResistance( ResistanceType.Energy, 90 );
             SetResistance( ResistanceType.Poison, 90 );
	SetSkill( SkillName.MagicResist, 105.1, 115.0 );
	SetSkill( SkillName.Tactics, 105.1, 115.0 );
	SetSkill( SkillName.Wrestling, 105.1, 115.0 );



             Fame = 10000;
             Karma = 5000;
             VirtualArmor = 10;
             ControlSlots = 2;
             MinTameSkill = 20;
}
         public override bool HasBreath{ get{ return true; } }
             public override int Scales{ get{ return 500; } }

 public WeightLifter( Serial serial ) : base( serial )
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
