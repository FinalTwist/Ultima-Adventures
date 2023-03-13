using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Prith corpse" )]
	public class PrithShaman : BaseCreature
	{
		public override InhumanSpeech SpeechType { get { return InhumanSpeech.Lizardman; } }

		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ if ( YellHue < 2 ){ return 100; } else { return 0; } } }
		public override int BreathColdDamage{ get{ if ( YellHue == 3 ){ return 100; } else { return 0; } } }
		public override int BreathPoisonDamage{ get{ if ( YellHue == 2 ){ return 100; } else { return 0; } } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ if ( YellHue == 1 ){ return 0x488; } else if ( YellHue == 2 ){ return 0xB92; } else if ( YellHue == 3 ){ return 0x5B5; } else { return 0x4FD; } } }
		public override int BreathEffectSound{ get{ return 0x238; } }
		public override int BreathEffectItemID{ get{ return 0x1005; } } // EXPLOSION POTION
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 2 ); }
		public override double BreathDamageScalar{ get{ return 0.4; } }

		[Constructable]
		public PrithShaman () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "lizardman" );
			Title = "the Prith shaman";
			Body = 324;
			BaseSoundID = 417;

			SetStr( 136, 165 );
			SetDex( 56, 75 );
			SetInt( 131, 155 );

			SetHits( 82, 99 );

			SetDamage( 7, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 80.1, 92.5 );
			SetSkill( SkillName.Magery, 80.1, 92.5 );
			SetSkill( SkillName.MagicResist, 80.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 95.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 4000;
			Karma = 4000;

			VirtualArmor = 50;

			PackReg( 16 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Potions );
		}

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			return base.OnBeforeDeath();
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override int Scales{ get{ return 1; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Blue ); } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }

		public PrithShaman( Serial serial ) : base( serial )
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
