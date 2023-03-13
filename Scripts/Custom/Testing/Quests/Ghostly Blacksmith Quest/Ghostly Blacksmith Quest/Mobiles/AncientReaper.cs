/* Created by Hammerhand */
using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ancient reapers corpse" )]
	public class AncientReaper : BaseCreature
	{
		[Constructable]
		public AncientReaper() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ancient reaper";
			Body = 47;
			BaseSoundID = 442;
            Hue = 2411;

			SetStr( 150, 375 );
			SetDex( 100, 175 );
			SetInt( 101, 250 );

			SetHits( 1500, 1850 );
			SetStam( 100 );

			SetDamage( 20, 43 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.1, 125.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;

            switch (Utility.Random(5))
            {
                case 0: PackItem(new SpecialCharcoal()); break;
            }

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 2; } }
		public override bool DisallowAllMoves{ get{ return true; } }

		public AncientReaper( Serial serial ) : base( serial )
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