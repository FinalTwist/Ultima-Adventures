using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a demon corpse" )]
	public class Demon : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 110.0; } }
		public override double DispelFocus{ get{ return 55.0; } }

		[Constructable]
		public Demon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a demon";
			Body = Utility.RandomList( 195, 137, 353 );
			BaseSoundID = 357;

			if ( Utility.RandomMinMax( 1, 10 ) == 1 ) // FEMALE
			{
				Body = 131;
				BaseSoundID = 0x4B0;
			}

			SetStr( 276, 305 );
			SetDex( 46, 65 );
			SetInt( 201, 225 );

			SetHits( 186, 203 );

			SetDamage( 4, 10 );

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

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 40;
			ControlSlots = Core.SE ? 4 : 5;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Average, 1 );
			AddLoot( LootPack.MedScrolls, 1 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 2; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 8; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public Demon( Serial serial ) : base( serial )
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
