using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ophidian corpse" )]
	[TypeAlias( "Server.Mobiles.OphidianJusticar", "Server.Mobiles.OphidianZealot" )]
	public class wOphidianArchmage : BaseCreature
	{
		private static string[] m_Names = new string[]
			{
				"an ophidian subjugated",
				"an ophidian zealot"
			};

		[Constructable]
		public wOphidianArchmage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; 
			Name = m_Names[Utility.Random( m_Names.Length )];
			Body = 85;
			BaseSoundID = 639;
			Hue = 1470;

			SetStr( 281, 305 );
			SetDex( 191, 215 );
			SetInt( 526, 750 );

			SetHits( 1469, 1983 );
			SetStam( 136, 145 );

			SetDamage( 15, 30 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 50, 70 );
			SetResistance( ResistanceType.Cold, 55, 75 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 55, 75 );

			SetSkill( SkillName.EvalInt, 95.1, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 120.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 70.2, 90.0 );

			Fame = 11500;
			Karma = -11500;

			VirtualArmor = 44;

			PackReg( 5, 15 );
			PackNecroReg( 5, 15 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

        public override void OnDeath(Container c)
        {
			WidowsLament.KillsWidow += 1;
			base.OnDeath( c );
            
		}

		public wOphidianArchmage( Serial serial ) : base( serial )
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
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; 
		}
	}
}