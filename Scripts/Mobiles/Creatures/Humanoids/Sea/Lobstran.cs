using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a giant crab corpse" )]
	public class Lobstran : BaseCreature
	{
		[Constructable]
		public Lobstran() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lobstran";
			Body = 23;
			BaseSoundID = 0x553;
			CanSwim = true;

			SetStr( 176, 205 );
			SetDex( 46, 65 );
			SetInt( 46, 70 );

			SetHits( 106, 123 );

			SetDamage( 8, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 15 );
			SetResistance( ResistanceType.Energy, 5, 15 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;

			PackItem( new Fish( ( Utility.Random( 3 )+1) ) );
		}

		public override bool BleedImmune{ get{ return true; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public Lobstran( Serial serial ) : base( serial )
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