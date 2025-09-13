// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Alien Man Beau" )]
     public class AlienManBeau: BaseCreature
     {
         [Constructable]
		public AlienManBeau () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Alien Man Beau";
             Body = 319;
             Hue = 2527;
             BaseSoundID = 898;
             SetStr( 750 );
             SetDex( 140 );
             SetInt( 75 );
             SetHits( 550 );
             SetDamage( 34 );
             SetDamageType( ResistanceType.Physical, 20 );
             SetDamageType( ResistanceType.Fire, 20 );
             SetDamageType( ResistanceType.Cold, 60 );
             SetDamageType( ResistanceType.Energy, 0 );
             SetDamageType( ResistanceType.Poison, 0 );

             SetResistance( ResistanceType.Physical, 40 );
             SetResistance( ResistanceType.Fire, 40 );
             SetResistance( ResistanceType.Cold, 50 );
             SetResistance( ResistanceType.Energy, 40 );
             SetResistance( ResistanceType.Poison, 50 );
	SetSkill( SkillName.MagicResist, 65.1, 70.0 );
	SetSkill( SkillName.Tactics, 65.1, 70.0 );
	SetSkill( SkillName.Wrestling, 65.1, 70.0 );


             Fame = 10000;
             Karma = -5000;
             VirtualArmor = 10;
             ControlSlots = 2;
             MinTameSkill = 20;
}
         public override bool HasBreath{ get{ return true; } }
             public override int Scales{ get{ return 500; } }

 public AlienManBeau( Serial serial ) : base( serial )
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
