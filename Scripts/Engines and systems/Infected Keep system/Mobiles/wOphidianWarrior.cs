using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an ophidian corpse" )]
	public class wOphidianWarrior : BaseCreature
	{
		private static string[] m_Names = new string[]
			{
				"an ophidian bondservant",
				"an ophidian peon"
			};

		[Constructable]
		public wOphidianWarrior() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; 
			Name = m_Names[Utility.Random( m_Names.Length )];
			Body = 86;
			BaseSoundID = 634;
			Hue = 1470;

			SetStr( 350, 420 );
			SetDex( 94, 190 );
			SetInt( 64, 160 );

			SetHits( 528, 655 );
			SetMana( 0 );

			SetDamage( 20, 65 );

			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 50, 70 );
			SetResistance( ResistanceType.Cold, 55, 75 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 55, 75 );

			SetSkill( SkillName.MagicResist, 70.1, 85.0 );
			SetSkill( SkillName.Swords, 60.1, 85.0 );
			SetSkill( SkillName.Tactics, 75.1, 90.0 );
						SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 90.1, 120.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 36;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems );
		}

		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

       public override void OnDeath(Container c)
        {
			WidowsLament.KillsWidow += 1;
			base.OnDeath( c );
		}

		public wOphidianWarrior( Serial serial ) : base( serial )
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