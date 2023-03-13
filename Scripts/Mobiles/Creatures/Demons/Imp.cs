using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an impish corpse" )]
	public class Imp : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 110.0; } }
		public override double DispelFocus{ get{ return 50.0; } }

		[Constructable]
		public Imp() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "imp" );

			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: Title = "the imp"; break;
				case 1: Title = "the mephit"; break;
				case 2: Title = "the quasit"; break;
			}

			Body = Utility.RandomList( 202, 359 );
			BaseSoundID = 594;
			Hue = Utility.RandomList( 0xB88, 0xB8C, 0xB85, 0x846, 0x84C, 0x84E, 0x4001, 0x5B7, 0x5B6, 0x550, 0x497, 0x48D, 0x482, 0x47E, 0x4AA, 0 );

			SetStr( 91, 115 );
			SetDex( 61, 80 );
			SetInt( 86, 105 );

			SetHits( 55, 70 );

			SetDamage( 10, 14 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 20.1, 30.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 30.1, 50.0 );
			SetSkill( SkillName.Tactics, 42.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 44.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 30;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 83.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Daemon; } }

		public Imp( Serial serial ) : base( serial )
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