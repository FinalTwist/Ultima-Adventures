using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a neptar corpse" )]
	public class NeptarWizard : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }

		[Constructable]
		public NeptarWizard() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a neptar wizard";
			Body = 677;
			BaseSoundID = 0x553;
			CanSwim = true;

			SetStr( 246, 275 );
			SetDex( 76, 95 );
			SetInt( 301, 325 );

			SetHits( 286, 303 );

			SetDamage( 9, 18 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 55 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 45, 55 );

			SetSkill( SkillName.EvalInt, 70.1, 80.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 7500;
			Karma = -7500;

			VirtualArmor = 46;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, Utility.RandomMinMax( 1, 4 ) );
		}

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			return base.OnBeforeDeath();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 4 ) == 1  )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "poison spit" ){ goo++; } }

				if ( goo == 0 )
				{
					attacker.PlaySound( 0x639 );
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "poison spit", 0x7D1, 0 );
				}
			}
		}

		public override int Meat{ get{ return 2; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }
		public override int Hides{ get{ return 2; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override bool BleedImmune{ get{ return true; } }

		public NeptarWizard( Serial serial ) : base( serial )
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