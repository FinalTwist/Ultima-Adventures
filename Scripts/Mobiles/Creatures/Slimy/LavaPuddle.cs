using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a puddle of lava" )]
	public class LavaPuddle : BaseCreature
	{
		[Constructable]
		public LavaPuddle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lava puddle";
			Body = 51;
			BaseSoundID = 456;

			Hue = 0xB73;

			SetStr( 122, 134 );
			SetDex( 116, 121 );
			SetInt( 16, 20 );

			SetHits( 115, 119 );

			SetDamage( 5, 15 );

			SetDamageType( ResistanceType.Fire, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Cold, 0 );

			SetSkill( SkillName.MagicResist, 65.1, 90.0 );
			SetSkill( SkillName.Tactics, 69.3, 94.0 );
			SetSkill( SkillName.Wrestling, 69.3, 94.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 18;

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }

		public LavaPuddle( Serial serial ) : base( serial )
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
