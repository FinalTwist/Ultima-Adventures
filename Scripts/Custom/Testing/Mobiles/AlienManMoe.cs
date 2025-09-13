// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Alien Man Moe" )]
     public class AlienManMoe: BaseCreature
     {
         [Constructable]
		public AlienManMoe () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Alien Man Moe";
             Body = 319;
             Hue = 2531;
             BaseSoundID = 898;
             SetStr( 750 );
             SetDex( 140 );
             SetInt( 75 );
             SetHits( 450 );
             SetDamage( 34 );
             SetDamageType( ResistanceType.Physical, 50 );
             SetDamageType( ResistanceType.Fire, 20 );
             SetDamageType( ResistanceType.Cold, 20 );
             SetDamageType( ResistanceType.Energy, 5 );
             SetDamageType( ResistanceType.Poison, 5 );

             SetResistance( ResistanceType.Physical, 40 );
             SetResistance( ResistanceType.Fire, 40 );
             SetResistance( ResistanceType.Cold, 50 );
             SetResistance( ResistanceType.Energy, 40 );
             SetResistance( ResistanceType.Poison, 50 );
	SetSkill( SkillName.MagicResist, 45.1, 50.0 );
	SetSkill( SkillName.Tactics, 45.1, 50.0 );
	SetSkill( SkillName.Wrestling, 45.1, 50.0 );



             Fame = 10000;
             Karma = -5000;
             VirtualArmor = 10;
             ControlSlots = 2;
             MinTameSkill = 20;
}
         public override bool HasBreath{ get{ return true; } }
             public override int Scales{ get{ return 500; } }

 public AlienManMoe( Serial serial ) : base( serial )
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
