using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a reptaur corpse" )]
	public class Reptaur : BaseCreature
	{
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Lizardman; } }

		[Constructable]
		public Reptaur() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace = 3;
			Name = NameList.RandomName( "lizardman" );
			Title = "the reptaur";
			Body = Utility.RandomList( 35, 36 );
			BaseSoundID = 417;

			SetStr( 35, 40 );
			SetDex( 30, 35 );
			SetInt( 20, 25 );

			SetDamage( 2, 6 );

			SetSkill( SkillName.Fencing, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );

			Fame = 400;
			Karma = -400;

			VirtualArmor = 28;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			// TODO: weapon
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return Utility.RandomBool() ? HideType.Regular : HideType.Horned; } }

		public Reptaur( Serial serial ) : base( serial )
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