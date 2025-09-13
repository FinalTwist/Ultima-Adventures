using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a daemon corpse" )]
	public class DemonOfTheSea : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 125.0; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public DemonOfTheSea () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "daemon" );
			switch ( Utility.RandomMinMax( 0, 4 ) )
			{
				case 0: Title = "the daemon of the sea"; break;
				case 1: Title = "the daemon of the high seas"; break;
				case 2: Title = "the daemon of the ocean"; break;
				case 3: Title = "the daemon of the deep"; break;
				case 4: Title = "the daemon of the dark sea"; break;
			}
			Body = 748;
			BaseSoundID = 357;

			if ( Utility.RandomMinMax( 1, 4 ) == 1 ) // FEMALE
			{
				Name = NameList.RandomName( "goddess" );
				switch ( Utility.RandomMinMax( 0, 4 ) )
				{
					case 0: Title = "the daemoness of the sea"; break;
					case 1: Title = "the daemoness of the high seas"; break;
					case 2: Title = "the daemoness of the ocean"; break;
					case 3: Title = "the daemoness of the deep"; break;
					case 4: Title = "the daemoness of the dark sea"; break;
				}
				Body = 64;
				BaseSoundID = 0x4B0;
			}

			CanSwim = true;

			SetStr( 476, 505 );
			SetDex( 76, 95 );
			SetInt( 301, 325 );

			SetHits( 286, 303 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 70.1, 80.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 58;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 2; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public DemonOfTheSea( Serial serial ) : base( serial )
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
