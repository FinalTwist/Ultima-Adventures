using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a worm corpse" )]
	public class CarcassWorm : BaseCreature
	{
		[Constructable]
		public CarcassWorm () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a carcass worm";
			Body = 321;
			BaseSoundID = 898;

			SetStr( 38, 52 );
			SetDex( 12, 17 );
			SetInt( 1, 5 );

			SetHits( 19, 25 );

			SetDamage( 2, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 450;
			Karma = -450;

			VirtualArmor = 4;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor, 2 );
		}

		public override int Meat{ get{ return 4; } }

		public CarcassWorm( Serial serial ) : base( serial )
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