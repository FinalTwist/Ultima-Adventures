// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Blood Titan Corpse" )]
     public class BloodTitan: BaseCreature
     {
         [Constructable]
		public BloodTitan () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Blood Titan";
             Body = 0x4C;
             Hue = 1157;
             BaseSoundID = 609;
             SetStr( 900 );
             SetDex( 140 );
             SetInt( 75 );
             SetHits( 3300 );
             SetDamage( 40 );
             SetDamageType( ResistanceType.Physical, 25 );
             SetDamageType( ResistanceType.Fire, 25 );
             SetDamageType( ResistanceType.Cold, 25 );
             SetDamageType( ResistanceType.Energy, 25 );
             SetDamageType( ResistanceType.Poison, 25 );

             SetResistance( ResistanceType.Physical, 40 );
             SetResistance( ResistanceType.Fire, 40 );
             SetResistance( ResistanceType.Cold, 40 );
             SetResistance( ResistanceType.Energy, 40 );
             SetResistance( ResistanceType.Poison, 40 );
	SetSkill( SkillName.MagicResist, 40.1, 41.0 );
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

 public BloodTitan( Serial serial ) : base( serial )
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
