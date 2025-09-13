using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class GargoyleShade : BaseCreature
	{
		[Constructable]
		public GargoyleShade() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a gargoyle shade";
			Body = 722;
			Hue = 0x4001;
			BaseSoundID = 0x482;

			SetStr( 76, 78 );
			SetDex( 76, 81 );
			SetInt( 36, 48 );

			SetHits( 60, 64 );
			SetStam( 80, 81 );
			SetMana( 75, 78 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 41 );
			SetResistance( ResistanceType.Fire, 40, 44 );
			SetResistance( ResistanceType.Cold, 10, 16 );
			SetResistance( ResistanceType.Poison, 20, 35 );

			SetSkill( SkillName.EvalInt, 57.1, 65.5 );
			SetSkill( SkillName.Magery, 60.6, 70.1 );
			SetSkill( SkillName.MagicResist, 52.6, 70.0 );
			SetSkill( SkillName.Tactics, 52.7, 60.0 );
			SetSkill( SkillName.Wrestling, 47.7, 55.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 28;

			PackReg( 10 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool BleedImmune{ get{ return true; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public GargoyleShade( Serial serial ) : base( serial )
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