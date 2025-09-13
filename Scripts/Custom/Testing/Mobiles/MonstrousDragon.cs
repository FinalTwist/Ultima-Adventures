// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Monstrous Dragon" )]
     public class MonstrousDragon: BaseCreature
     {
         [Constructable]
        public MonstrousDragon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Monstrous Dragon";
             Body = 12;
             Hue = 2613;
             BaseSoundID = 0;
             SetStr( 700 );
             SetDex( 120 );
             SetInt( 200 );
             SetHits( 570 );
             SetDamage( 30 );
             SetDamageType( ResistanceType.Physical, 50 );
             SetDamageType( ResistanceType.Fire, 0 );
             SetDamageType( ResistanceType.Cold, 0 );
             SetDamageType( ResistanceType.Energy, 25 );
             SetDamageType( ResistanceType.Poison, 25 );
		SetSkill( SkillName.EvalInt, 80.1, 100.0 );
		SetSkill( SkillName.Magery, 80.1, 100.0 );
		SetSkill( SkillName.Meditation, 52.5, 75.0 );
		SetSkill( SkillName.MagicResist, 100.5, 150.0 );
		SetSkill( SkillName.Tactics, 97.6, 100.0 );
		SetSkill( SkillName.Wrestling, 97.6, 100.0 );
             SetResistance( ResistanceType.Physical, 60 );
             SetResistance( ResistanceType.Fire, 60 );
             SetResistance( ResistanceType.Cold, 60 );
             SetResistance( ResistanceType.Energy, 60 );
             SetResistance( ResistanceType.Poison, 60 );


             Fame = 5000;
             Karma = -5000;
             VirtualArmor = 10;
             ControlSlots = 1;
             MinTameSkill = 500;
}
         public override bool HasBreath{ get{ return true; } }
         public override bool BardImmune{ get{ return true; } }
         public override bool Unprovokable{ get{ return true; } }
             public override bool Uncalmable{ get{ return true; } }
             public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
             public override HideType HideType{ get{ return HideType.Barbed; } }

 public MonstrousDragon( Serial serial ) : base( serial )
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
