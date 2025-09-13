using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an goblin corpse" )]
	public class ReownedGrayGoblinMage : BaseCreature
	{
		//public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }

		[Constructable]
		public ReownedGrayGoblinMage() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Gray Goblin Mage [Reowned]";
			Body = 157;
			BaseSoundID = 0x45A;
			Hue = 651;

			SetStr( 527, 527 );
			SetDex( 71, 71 );
			SetInt( 572, 572 );

			SetHits( 1090, 1090 );
			SetStam( 71, 71 );
			SetMana( 572, 572 );

			SetDamage( 5, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 31, 31 );
			SetResistance( ResistanceType.Fire, 51, 51 );
			SetResistance( ResistanceType.Cold, 40, 40 );
			SetResistance( ResistanceType.Poison, 47, 47 );
			SetResistance( ResistanceType.Energy, 22, 22 );

			SetSkill( SkillName.MagicResist, 143.9, 143.9 );
			SetSkill( SkillName.Tactics, 99.6, 99.6 );
			SetSkill( SkillName.Anatomy, 91.4, 91.4 );
			SetSkill( SkillName.Wrestling, 95.4, 95.4 );

			SetSkill( SkillName.Magery, 106.3, 106.3 );
			SetSkill( SkillName.EvalInt, 113.1, 113.1 );
			SetSkill( SkillName.Meditation, 106.7, 106.7 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 28;

			// Loot - 300-350gold, magicitem,scroll,reg,goblin blood, essence control
			switch ( Utility.Random( 20 ) )
			{
				case 0: PackItem( new Scimitar() ); break;
				case 1: PackItem( new Katana() ); break;
				case 2: PackItem( new WarMace() ); break;
				case 3: PackItem( new WarHammer() ); break;
				case 4: PackItem( new Kryss() ); break;
				case 5: PackItem( new Pitchfork() ); break;
			}

			PackItem( new ThighBoots() );

			switch ( Utility.Random( 3 ) )
			{
				case 0: PackItem( new Ribs() ); break;
				case 1: PackItem( new Shaft() ); break;
				case 2: PackItem( new Candle() ); break;
			}

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new BolaBall() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 1; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.SavagesAndOrcs; }
		}

		//public override bool IsEnemy( Mobile m )
		//{
		//	if ( m.Player && m.FindItemOnLayer( Layer.Helm ) is OrcishKinMask )
		//		return false;

		//	return base.IsEnemy( m );
		//}

		//public override void AggressiveAction( Mobile aggressor, bool criminal )
		//{
			//base.AggressiveAction( aggressor, criminal );

			//Item item = aggressor.FindItemOnLayer( Layer.Helm );

			//if ( item is OrcishKinMask )
			//{
			//	AOS.Damage( aggressor, 50, 0, 100, 0, 0, 0 );
			//	item.Delete();
			//	aggressor.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
			//	aggressor.PlaySound( 0x307 );
			//}
		//}

		public ReownedGrayGoblinMage( Serial serial ) : base( serial )
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
