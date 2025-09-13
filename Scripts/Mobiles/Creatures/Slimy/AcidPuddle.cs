using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a puddle of acid" )]
	public class AcidPuddle : BaseCreature
	{
		[Constructable]
		public AcidPuddle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an acid puddle";
			Body = 51;
			BaseSoundID = 456;

			Hue = 1167;

			SetStr( 122, 134 );
			SetDex( 116, 121 );
			SetInt( 16, 20 );

			SetHits( 115, 119 );

			SetDamage( 5, 15 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 90.0 );
			SetSkill( SkillName.Tactics, 69.3, 94.0 );
			SetSkill( SkillName.Wrestling, 69.3, 94.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 18;

			PackItem( new BottleOfAcid() ); // WIZARD ADDED
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }

		public AcidPuddle( Serial serial ) : base( serial )
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
