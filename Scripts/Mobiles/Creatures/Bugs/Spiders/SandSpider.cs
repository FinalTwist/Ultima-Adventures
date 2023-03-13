using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a spider corpse" )]
	public class SandSpider : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 50; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x62A; } }
		public override int BreathEffectItemID{ get{ return 0x10D4; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 6 ); }

		[Constructable]
		public SandSpider() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sand spider";
			Body = 140;
			BaseSoundID = 0x388;
			Hue = 553;

			SetStr( 76, 100 );
			SetDex( 96, 115 );
			SetInt( 36, 60 );

			SetHits( 46, 60 );

			SetDamage( 5, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 30 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Anatomy, 30.3, 75.0 );
			SetSkill( SkillName.Poisoning, 60.1, 80.0 );
			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 65.1, 80.0 );
			SetSkill( SkillName.Wrestling, 70.1, 85.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 24;

			PackItem( new SpidersSilk( 5 ) );

			Item Venom = new VenomSack();
				Venom.Name = "deadly venom sack";
				AddItem( Venom );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }

		public override int GetAttackSound(){ return 0x601; }	// A
		public override int GetDeathSound(){ return 0x602; }	// D
		public override int GetHurtSound(){ return 0x603; }		// H

		public SandSpider( Serial serial ) : base( serial )
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