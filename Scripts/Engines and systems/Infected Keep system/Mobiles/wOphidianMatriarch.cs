using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ophidian corpse" )]
	public class wOphidianMatriarch : BaseCreature
	{
		[Constructable]
		public wOphidianMatriarch() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ophidian mistress";
			Body = 87;
			BaseSoundID = 644;
			Hue = 1470;

			SetStr( 416, 505 );
			SetDex( 96, 115 );
			SetInt( 566, 855 );

			SetHits( 850, 1003 );

			SetDamage( 40, 70 );

			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 50, 70 );
			SetResistance( ResistanceType.Cold, 55, 75 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 55, 75 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 5.4, 25.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 90.1, 120.0 );

			Fame = 16000;
			Karma = -16000;

			VirtualArmor = 50;
						AIFullSpeedActive = true;
			AIFullSpeedPassive = false; 
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

       public override void OnDeath(Container c)
        {
			WidowsLament.KillsWidow += 1;
			base.OnDeath( c );
		}

		public wOphidianMatriarch( Serial serial ) : base( serial )
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