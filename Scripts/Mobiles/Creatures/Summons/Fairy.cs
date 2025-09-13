using System;
using Server;
using Server.Misc;
using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a Fairy corpse" )]
	public class Fairy : BaseCreature
	{
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		[Constructable]
		public Fairy() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fairy";
			Body = 58;
			Hue = 0x4FE;
			BaseSoundID = 466;

			SetStr( 196, 225 );
			SetDex( 196, 225 );
			SetInt( 196, 225 );

			SetHits( 118, 135 );

			SetDamage( 17, 18 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 20, 40 );
			SetResistance( ResistanceType.Cold, 10, 30 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 50, 70 );

			SetSkill( SkillName.EvalInt, 80.0 );
			SetSkill( SkillName.Magery, 80.0 );
			SetSkill( SkillName.MagicResist, 80.0 );
			SetSkill( SkillName.Tactics, 80.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			Fame = 4000;
			Karma = 0;

			VirtualArmor = 40;

			AddItem( new LightSource() );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Wisp; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public Fairy( Serial serial ) : base( serial )
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