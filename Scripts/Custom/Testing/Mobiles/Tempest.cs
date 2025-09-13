using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "the remains of Tempest" )]
	public class Tempest : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public Tempest () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Tempest";
			Body = 13;
			Hue = 1175;
			BaseSoundID = 263;

			SetStr( 116, 135 );
			SetDex( 166, 185 );
			SetInt( 101, 125 );

			SetHits( 602 );

			SetDamage( 18, 20 );  // Erica's

			SetDamageType( ResistanceType.Energy, 80 );
			SetDamageType( ResistanceType.Cold, 20 );

			SetResistance( ResistanceType.Physical, 46 );
			SetResistance( ResistanceType.Fire, 39 );
			SetResistance( ResistanceType.Cold, 33 );
			SetResistance( ResistanceType.Poison, 36 );
			SetResistance( ResistanceType.Energy, 58 );

			SetSkill( SkillName.EvalInt, 99.6 );
			SetSkill( SkillName.Magery, 101.0 );
			SetSkill( SkillName.MagicResist, 104.6 );
			SetSkill( SkillName.Tactics, 111.8 );
			SetSkill( SkillName.Wrestling, 116.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 40;
			ControlSlots = 2;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public Tempest( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 655;
		}
	}
}
