using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a durgar corpse" )]
	public class Durgar : BaseCreature
	{
		[Constructable]
		public Durgar() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a durgar";
			Body = 349;
			BaseSoundID = 427;

			SetStr( 156, 185 );
			SetDex( 111, 135 );
			SetInt( 46, 70 );

			SetHits( 206, 223 );
			SetMana( 0 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 85, 95 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );

			Fame = 2000;
			Karma = -2000;

			VirtualArmor = 30;

			PackItem( new Club() );
		}

		public override int Meat{ get{ return 2; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public Durgar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}