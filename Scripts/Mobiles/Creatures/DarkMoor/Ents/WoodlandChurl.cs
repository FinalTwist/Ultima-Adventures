using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a pile of wood" )]
	public class WoodlandChurl : BaseCreature
	{
		[Constructable]
		public WoodlandChurl() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a woodland churl";
			Body = 203;
			BaseSoundID = 442;

			SetStr( 801, 910 );
			SetDex( 151, 300 );
			SetInt( 171, 195 );

			SetHits( 200, 250 );

			SetDamage( 14, 20 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Fire, 10 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 50, 70 );
			SetResistance( ResistanceType.Cold, 35, 50 );
			SetResistance( ResistanceType.Poison, 45, 70 );
			SetResistance( ResistanceType.Energy, 45, 65 );

			SetSkill( SkillName.EvalInt, 100.1, 125.0 );
			SetSkill( SkillName.Magery, 96.1, 106.0 );
			SetSkill( SkillName.Anatomy, 85.1, 95.0 );
			SetSkill( SkillName.MagicResist, 85.1, 100.0 );
			SetSkill( SkillName.Tactics, 86.1, 101.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 12000;
			Karma = 12000;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 88.3;

			if ( Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );

			
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 3 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override bool CanAngerOnTame { get { return true; } }



		public WoodlandChurl( Serial serial ) : base( serial )
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
