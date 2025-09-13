using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a hag's corpse" )]
	public class SeaHag : BaseCreature
	{
		[Constructable]
		public SeaHag() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Title = "the sea hag";
			Name = NameList.RandomName( "evil witch" );
			Body = 24;
			BaseSoundID = 0x4B0;
			Hue = 2121;
			CanSwim = true;

			SetStr( 171, 200 );
			SetDex( 126, 145 );
			SetInt( 276, 305 );

			SetHits( 103, 120 );

			SetDamage( 24, 26 );

			SetDamageType( ResistanceType.Physical, 10 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 55, 65 );
			SetResistance( ResistanceType.Energy, 40, 50 );


			SetSkill( SkillName.Necromancy, 89, 99.1 );
			SetSkill( SkillName.SpiritSpeak, 90.0, 99.0 );

			SetSkill( SkillName.EvalInt, 100.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.Meditation, 85.1, 95.0 );
			SetSkill( SkillName.MagicResist, 80.1, 100.0 );
			SetSkill( SkillName.Tactics, 70.1, 90.0 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 50;
			PackItem( new GnarledStaff() );
			PackReg( Utility.RandomMinMax( 2, 10 ) );
			PackReg( Utility.RandomMinMax( 2, 10 ) );
			PackReg( Utility.RandomMinMax( 2, 10 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			return base.OnBeforeDeath();
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override bool BleedImmune{ get{ return true; } }

		public SeaHag( Serial serial ) : base( serial )
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