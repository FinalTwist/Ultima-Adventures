using System;
using System.Collections;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a dinosaur corpse" )]
	public class Lochasaur : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		public override int BreathPhysicalDamage{ get{ return 100; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 4 ); }

		[Constructable]
		public Lochasaur() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lochasaur";
			Body = 395;
			BaseSoundID = 447;

			SetStr( 756, 780 );
			SetDex( 226, 245 );
			SetInt( 26, 40 );

			SetHits( 454, 468 );
			SetMana( 0 );

			SetDamage( 19, 33 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 49;

			CanSwim = true;
			CantWalk = true;
		}

		public override void OnThink()
		{
			if ( VirtualArmor < 50 )
			{
				if ( Worlds.TestShore( Map, X, Y, 10 ) ){ this.Delete(); }
				VirtualArmor = 50;
			}
			base.OnThink();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override int Meat{ get{ return 18; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Dinosaur; } }

		public Lochasaur( Serial serial ) : base( serial )
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
