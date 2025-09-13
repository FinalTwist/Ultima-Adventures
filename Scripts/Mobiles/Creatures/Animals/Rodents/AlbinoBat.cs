using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bat corpse" )]
	public class AlbinoBat : BaseCreature
	{
		[Constructable]
		public AlbinoBat() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "an albino bat";
			Body = 742;
			BaseSoundID = 0x270;

			SetStr( 91, 110 );
			SetDex( 91, 115 );
			SetInt( 26, 50 );

			SetHits( 55, 66 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 70.1, 95.0 );
			SetSkill( SkillName.Tactics, 55.1, 80.0 );
			SetSkill( SkillName.Wrestling, 30.1, 55.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 14;

			BatWing wings = new BatWing(2);
			wings.Hue = 0x9C2;
			PackItem( wings );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int GetIdleSound()
		{
			return 0x29B;
		}

		public AlbinoBat( Serial serial ) : base( serial )
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