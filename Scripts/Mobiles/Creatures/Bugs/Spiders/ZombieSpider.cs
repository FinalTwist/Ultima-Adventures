using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a spider corpse" )]
	public class ZombieSpider : BaseCreature
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
		public ZombieSpider() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a decaying spider";
			Body = 438;
			BaseSoundID = 1170;

			SetStr( 276, 300 );
			SetDex( 96, 115 );
			SetInt( 36, 60 );

			SetHits( 246, 260 );

			SetDamage( 12, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 30 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Anatomy, 30.3, 75.0 );
			SetSkill( SkillName.Poisoning, 60.1, 80.0 );
			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 65.1, 80.0 );
			SetSkill( SkillName.Wrestling, 70.1, 85.0 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 45;

			PackItem( new SpidersSilk( 10 ) );

			Item Venom = new VenomSack();
				Venom.Name = "deadly venom sack";
				AddItem( Venom );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "spider ooze" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "spider ooze", 0x7D1, 0 );
				}
			}
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }

		public ZombieSpider( Serial serial ) : base( serial )
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