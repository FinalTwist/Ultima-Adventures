using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a tangling root corpse" )]
	public class TanglingRoots : BaseCreature
	{
		[Constructable]
		public TanglingRoots() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a tangling root";
			Body = 743;
			BaseSoundID = 684;

			SetStr( 150, 157 );
			SetDex( 55, 60 );
			SetInt( 30, 35 );

			SetHits( 200, 232 );
			SetStam( 55, 60 );

			SetDamage( 10, 23 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 20, 30 );

			SetSkill( SkillName.MagicResist, 15.1, 18.5 );
			SetSkill( SkillName.Tactics, 45.1, 59.5 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 18;

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new Board( 10 ) );
			else
				PackItem( new Log( 10 ) );

			PackItem( new MandrakeRoot( 3 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }
		public override bool DisallowAllMoves{ get{ return true; } }

		public TanglingRoots( Serial serial ) : base( serial )
		{
		}
            public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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

			if ( BaseSoundID == 352 )
				BaseSoundID = 684;
		}
	}
}
