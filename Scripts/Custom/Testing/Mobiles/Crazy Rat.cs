// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Crazy Rat" )]
     public class CrazyRat: BaseCreature
     {
         [Constructable]
		public CrazyRat () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Crazy Rat";
             Body = 143;
             Hue = 1283;
             BaseSoundID = 898;
             SetStr( 2500 );
             SetDex( 180 );
             SetInt( 140 );
             SetHits( 2000 );
             SetDamage( 45 );
             SetDamageType( ResistanceType.Physical, 80 );
             SetDamageType( ResistanceType.Fire, 5 );
             SetDamageType( ResistanceType.Cold, 5 );
             SetDamageType( ResistanceType.Energy, 5 );
             SetDamageType( ResistanceType.Poison, 5 );

             SetResistance( ResistanceType.Physical, 90 );
             SetResistance( ResistanceType.Fire, 90 );
             SetResistance( ResistanceType.Cold, 90 );
             SetResistance( ResistanceType.Energy, 90 );
             SetResistance( ResistanceType.Poison, 90 );
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

 public CrazyRat( Serial serial ) : base( serial )
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
