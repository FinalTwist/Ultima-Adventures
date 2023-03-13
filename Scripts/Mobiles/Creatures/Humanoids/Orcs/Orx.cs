using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an orc corpse" )]
	public class Orx : BaseCreature
	{
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }

		[Constructable]
		public Orx() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace = 5;
			Name = NameList.RandomName( "orc" );
			Title = "the orx warrior";
			Body = 108;
			BaseSoundID = 0x45A;

			SetStr( 111, 145 );
			SetDex( 101, 135 );
			SetInt( 86, 110 );

			SetHits( 67, 87 );

			SetDamage( 5, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 70.1, 85.0 );
			SetSkill( SkillName.Swords, 70.1, 95.0 );
			SetSkill( SkillName.Tactics, 85.1, 100.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 34;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

		public Orx( Serial serial ) : base( serial )
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