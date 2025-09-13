using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a gargoyle corpse" )]
	public class StygianGargoyleLord : BaseCreature
	{
		[Constructable]
		public StygianGargoyleLord() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace =2;
			Name = "a gargoyle lord";
			Body = 257;
			BaseSoundID = 357;

			SetStr( 346, 475 );
			SetDex( 96, 125 );
			SetInt( 181, 205 );

			SetHits( 268, 305 );

			SetDamage( 12, 18 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 55 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 25, 35 );

			SetSkill( SkillName.EvalInt, 80.1, 95.0 );
			SetSkill( SkillName.Magery, 80.1, 95.0 );
			SetSkill( SkillName.MagicResist, 80.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 90.0 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 45;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
			AddLoot( LootPack.Gems, Utility.RandomMinMax( 1, 4 ) );
		}

		public override int TreasureMapLevel{ get{ return 2; } }
		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public StygianGargoyleLord( Serial serial ) : base( serial )
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