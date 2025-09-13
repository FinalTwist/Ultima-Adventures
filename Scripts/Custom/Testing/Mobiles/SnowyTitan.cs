// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Snowy Titan Corpse" )]
     public class SnowyTitan: BaseCreature
     {
         [Constructable]
		public SnowyTitan () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Snowy Titan";
             Body = 0x4C;
             Hue = 1153;
             BaseSoundID = 609;
             SetStr( 750 );
             SetDex( 140 );
             SetInt( 75 );
             SetHits( 1700 );
             SetDamage( 37 );
             SetDamageType( ResistanceType.Physical, 5 );
             SetDamageType( ResistanceType.Fire, 5 );
             SetDamageType( ResistanceType.Cold, 80 );
             SetDamageType( ResistanceType.Energy, 5 );
             SetDamageType( ResistanceType.Poison, 5 );

             SetResistance( ResistanceType.Physical, 25 );
             SetResistance( ResistanceType.Fire, 5 );
             SetResistance( ResistanceType.Cold, 80 );
             SetResistance( ResistanceType.Energy, 35 );
             SetResistance( ResistanceType.Poison, 50 );
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

 public SnowyTitan( Serial serial ) : base( serial )
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
