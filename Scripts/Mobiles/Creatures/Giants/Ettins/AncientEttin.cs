using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an ettin corpse" )]
	public class AncientEttin : BaseCreature
	{
		[Constructable]
		public AncientEttin() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "giant" );
			Title = "the ancient ettin";
			Body = 732;
			BaseSoundID = 0x59D;

			SetStr( 736, 785 );
			SetDex( 226, 245 );
			SetInt( 381, 405 );

			SetHits( 622, 651 );

			SetDamage( 18, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 95.1, 110.0 );
			SetSkill( SkillName.Magery, 95.1, 110.0 );
			SetSkill( SkillName.MagicResist, 90.2, 120.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 16500;
			Karma = -16500;

			VirtualArmor = 60;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 8; } }
		public override int Hides{ get{ return 24; } }
		public override HideType HideType{ get{ return HideType.Goliath; } }

		public AncientEttin( Serial serial ) : base( serial )
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