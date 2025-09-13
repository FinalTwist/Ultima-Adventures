// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Snowy Titan Corpse" )]
     public class PoisonousTitan: BaseCreature
     {
         [Constructable]
		public PoisonousTitan () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Poisonous Titan";
             Body = 0x4C;
             Hue = 1267;
             BaseSoundID = 609;
             SetStr( 750 );
             SetDex( 140 );
             SetInt( 75 );
             SetHits( 1500 );
             SetDamage( 37 );
             SetDamageType( ResistanceType.Physical, 5 );
             SetDamageType( ResistanceType.Fire, 5 );
             SetDamageType( ResistanceType.Cold, 5 );
             SetDamageType( ResistanceType.Energy, 5 );
             SetDamageType( ResistanceType.Poison, 80 );

             SetResistance( ResistanceType.Physical, 5 );
             SetResistance( ResistanceType.Fire, 5 );
             SetResistance( ResistanceType.Cold, 5 );
             SetResistance( ResistanceType.Energy, 5 );
             SetResistance( ResistanceType.Poison, 80 );

	SetSkill( SkillName.MagicResist, 50.1, 56.0 );
	SetSkill( SkillName.Tactics, 105.1, 115.0 );
	SetSkill( SkillName.Wrestling, 105.1, 115.0 );



             Fame = 10000;
             Karma = -5000;
             VirtualArmor = 10;
             ControlSlots = 2;
             MinTameSkill = 20;
             Tamable = true;
}
         public override bool HasBreath{ get{ return true; } }
             public override int Scales{ get{ return 500; } }

 public PoisonousTitan( Serial serial ) : base( serial )
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
