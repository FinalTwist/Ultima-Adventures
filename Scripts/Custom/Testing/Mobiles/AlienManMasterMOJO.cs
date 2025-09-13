// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Alien Man Master MOJO" )]
     public class AlienManMasterMOJO: BaseCreature
     {
         [Constructable]
		public AlienManMasterMOJO () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Alien Man Master MOJO";
             Body = 319;
             Hue = 2523;
             BaseSoundID = 898;
             SetStr( 750 );
             SetDex( 140 );
             SetInt( 75 );
             SetHits( 880 );
             SetDamage( 45 );
             SetDamageType( ResistanceType.Physical, 5 );
             SetDamageType( ResistanceType.Fire, 35 );
             SetDamageType( ResistanceType.Cold, 35 );
             SetDamageType( ResistanceType.Energy, 10 );
             SetDamageType( ResistanceType.Poison, 15 );

             SetResistance( ResistanceType.Physical, 55 );
             SetResistance( ResistanceType.Fire, 50 );
             SetResistance( ResistanceType.Cold, 65 );
             SetResistance( ResistanceType.Energy, 75 );
             SetResistance( ResistanceType.Poison, 70 );
	SetSkill( SkillName.MagicResist, 105.1, 115.0 );
	SetSkill( SkillName.Tactics, 105.1, 115.0 );
	SetSkill( SkillName.Wrestling, 105.1, 115.0 );



             Fame = 10000;
             Karma = -5000;
             VirtualArmor = 10;
             ControlSlots = 2;
             MinTameSkill = 20;
}
         public override bool HasBreath{ get{ return true; } }
             public override int Scales{ get{ return 500; } }

 public AlienManMasterMOJO( Serial serial ) : base( serial )
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
