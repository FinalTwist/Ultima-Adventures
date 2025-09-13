// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Ganon's corpse" )]
     public class Ganon: BaseCreature
     {
         [Constructable]
		public Ganon () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.5, 0.7 ) 
         {
             Name = "Ganon";
             Body = 796;
             BaseSoundID = 609;
             SetStr( 850 );
             SetDex( 300 );
             SetInt( 300 );
             SetHits( 3000 );
             SetDamage( 58 );
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
		
		PackItem( new GanonHead() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}

		public override bool AutoDispel{ get{ return true; } }
        

		 public Ganon( Serial serial ) : base( serial )
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
