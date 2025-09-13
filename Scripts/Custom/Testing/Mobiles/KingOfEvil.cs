// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "King of Evil" )]
     public class KingofEvil: BaseCreature
     {
         [Constructable]
		public KingofEvil () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.5, 0.7 ) 
         {
             Name = "King of Evil";
             Body = 796;
             Hue = 1993;
             BaseSoundID = 609;
             SetStr( 850 );
             SetDex( 300 );
             SetInt( 300 );
             SetHits( 50000 );
             SetDamage( 78 );
             SetDamageType( ResistanceType.Physical, 80 );
             SetDamageType( ResistanceType.Fire, 5 );
             SetDamageType( ResistanceType.Cold, 5 );
             SetDamageType( ResistanceType.Energy, 5 );
             SetDamageType( ResistanceType.Poison, 5 );

             SetResistance( ResistanceType.Physical, 50 );
             SetResistance( ResistanceType.Fire, 50 );
             SetResistance( ResistanceType.Cold, 50 );
             SetResistance( ResistanceType.Energy, 50 );
             SetResistance( ResistanceType.Poison, 50 );
	SetSkill( SkillName.MagicResist, 40.1, 40.0 );
	SetSkill( SkillName.Tactics, 125.1, 135.0 );
	SetSkill( SkillName.Wrestling, 140.1, 145.0 );



             Fame = 10000;
             Karma = -5000;
             VirtualArmor = 10;
             ControlSlots = 2;
             MinTameSkill = 20;
}
        // public override bool HasBreath{ get{ return true; } }
             public override int Scales{ get{ return 500; } }

 public KingofEvil( Serial serial ) : base( serial )
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
