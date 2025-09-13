using System;
using Server;
using Server.Items;
using Server.Regions;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class Spirit : BaseCreature
	{
		[Constructable]
		public Spirit() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a spirit";
			Body = 84;
			BaseSoundID = 0x482;
			Hue = 0x47E;

			SetStr( 106, 130 );
			SetDex( 96, 115 );
			SetInt( 66, 90 );

			SetHits( 96, 120 );

			SetDamage( 9, 15 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 10, 20 );

			SetSkill( SkillName.EvalInt, 75.1, 90.0 );
			SetSkill( SkillName.Magery, 75.1, 90.0 );
			SetSkill( SkillName.MagicResist, 75.1, 90.0 );
			SetSkill( SkillName.Tactics, 65.1, 80.0 );
			SetSkill( SkillName.Wrestling, 65.1, 75.0 );

			Fame = 5500;
			Karma = -5500;

			VirtualArmor = 30;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;
			return base.OnBeforeDeath();
		}

		public override bool BleedImmune{ get{ return true; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public Spirit( Serial serial ) : base( serial )
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