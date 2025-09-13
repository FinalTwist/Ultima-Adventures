using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a lesser elemental corpse" )]
	public class WaterSpawn : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 60.5; } }
		public override double DispelFocus{ get{ return 20.0; } }

		[Constructable]
		public WaterSpawn () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a water spawn";
			Body = 51;
			BaseSoundID = 456;
			Hue = 296;

			SetStr( 200, 250 );
			SetDex( 40, 60 );
			SetInt( 113, 175 );

			SetHits( 158, 180 );

			SetDamage( 8, 13 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );

			SetResistance( ResistanceType.Physical, 30, 45 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.EvalInt, 40.1, 50.0 );
			SetSkill( SkillName.Magery, 40.1, 50.0 );
			SetSkill( SkillName.Meditation, 5.4, 25.0 );
			SetSkill( SkillName.MagicResist, 40.1, 50.0 );
			SetSkill( SkillName.Tactics, 40.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 50.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 30;

			CanSwim = true;

			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ PackItem( new WaterFlask( ) ); }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override bool BleedImmune{ get{ return true; } }

		public WaterSpawn( Serial serial ) : base( serial )
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