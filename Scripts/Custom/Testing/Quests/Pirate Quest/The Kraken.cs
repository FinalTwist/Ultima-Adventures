using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "The Kraken's Corpse" )]
	public class TheKraken : Kraken
	{
		[Constructable]
		public TheKraken()
		{
			Name = "The Kraken";
			Hue = 1155;
			BaseSoundID = 1149;

			SetHits( 1000 );

			SetDamage( 16, 25 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 60;

			PackItem( new CaptainsCutlass() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
		}

		public TheKraken( Serial serial ) : base( serial )
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
