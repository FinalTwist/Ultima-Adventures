using System;
using Server;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "an Ice Cat's corpse" )]
	public class IceCat : Cougar
	{
		[Constructable]
		public IceCat()
		{
			Name = "an Ice Cat";
			Hue = 1152;
			SetStr( 250, 500 );
			SetDex( 150, 200 );
			SetInt( 100, 250 );

			SetHits( 760, 1000 );
			SetStam( 150, 150 );
			SetMana( 150, 150 );

			SetSkill( SkillName.Wrestling, 100, 200 );
			SetSkill( SkillName.Magery, 100, 200 );
			SetSkill( SkillName.MagicResist, 100, 200 );
			SetSkill( SkillName.Meditation, 100, 200 );
			SetSkill( SkillName.EvalInt, 100, 200 );
			SetSkill( SkillName.Tactics, 100, 200 );
			SetSkill( SkillName.Anatomy, 100, 200 );

			SetResistance( ResistanceType.Physical, 50, 50 );
			SetResistance( ResistanceType.Fire, 50, 50 );
			SetResistance( ResistanceType.Cold, 50, 50 );
			SetResistance( ResistanceType.Poison, 50, 50 );
			SetResistance( ResistanceType.Energy, 50, 50 );
			Karma = 1;

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
		}

		public IceCat( Serial serial ) : base( serial )
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
