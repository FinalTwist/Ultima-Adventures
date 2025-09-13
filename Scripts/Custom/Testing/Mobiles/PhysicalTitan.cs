// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Metal Titan Corpse" )]
     public class MetalTitan: BaseCreature
     {
         [Constructable]
		public MetalTitan () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Metal Titan";
             Body = 0x4C;
             Hue = 1953;
             BaseSoundID = 609;
             SetStr( 750 );
             SetDex( 140 );
             SetInt( 75 );
             SetHits( 2000 );
             SetDamage( 45 );
             SetDamageType( ResistanceType.Physical, 80 );
             SetDamageType( ResistanceType.Fire, 5 );
             SetDamageType( ResistanceType.Cold, 5 );
             SetDamageType( ResistanceType.Energy, 5 );
             SetDamageType( ResistanceType.Poison, 5 );

             SetResistance( ResistanceType.Physical, 90 );
             SetResistance( ResistanceType.Fire, 5 );
             SetResistance( ResistanceType.Cold, 5 );
             SetResistance( ResistanceType.Energy, 45 );
             SetResistance( ResistanceType.Poison, 30 );
	SetSkill( SkillName.MagicResist, 50.1, 51.0 );
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

 public MetalTitan( Serial serial ) : base( serial )
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
