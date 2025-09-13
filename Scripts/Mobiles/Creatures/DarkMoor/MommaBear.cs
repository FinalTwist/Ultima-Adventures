//Created by Wild Joker aka Abracadabra for Ethereal Mountains
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a momma bear corpse" )]
	public class MommaBear : BaseCreature
	{
		[Constructable]
		public MommaBear() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Momma Bear";
			Body = 0xA7;
			BaseSoundID = 427;

			SetStr( 336, 385 );
			SetDex( 96, 115 );
			SetInt( 31, 55 );

			SetHits( 202, 231 );
			SetMana( 0 );

			SetDamage( 7, 23 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.3, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 15000;
			Karma = 15000;

			VirtualArmor = 48;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override int Meat{ get{ return 4; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override int Hides{ get{ return 12; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }

		public MommaBear( Serial serial ) : base( serial )
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