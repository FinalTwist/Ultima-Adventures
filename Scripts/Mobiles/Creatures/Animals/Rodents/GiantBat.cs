using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bat corpse" )]
	public class GiantBat : BaseCreature
	{
		[Constructable]
		public GiantBat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a giant bat";
			Body = 317;
			BaseSoundID = 0x270;

			SetStr( 31, 50 );
			SetDex( 31, 55 );
			SetInt( 1, 5 );

			SetHits( 35, 46 );

			SetDamage( 1, 3 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 70.1, 95.0 );
			SetSkill( SkillName.Tactics, 55.1, 80.0 );
			SetSkill( SkillName.Wrestling, 30.1, 55.0 );

			Fame = 250;
			Karma = -250;

			VirtualArmor = 7;

			PackItem( new BatWing( 2 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int GetIdleSound()
		{
			return 0x29B;
		}

		public GiantBat( Serial serial ) : base( serial )
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