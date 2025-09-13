using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a water weird corpse" )]
	public class WaterWeird : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 125.5; } }
		public override double DispelFocus{ get{ return 40.0; } }

		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 50; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x012; } }
		public override int BreathEffectItemID{ get{ return 0x1A85; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 30 ); }

		[Constructable]
		public WaterWeird () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a water weird";
			Body = 16;
			BaseSoundID = 278;
			Hue = 296;

			CanSwim = true;

			SetStr( 226, 355 );
			SetDex( 166, 185 );
			SetInt( 201, 225 );

			SetHits( 176, 193 );

			SetDamage( 10, 16 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 20, 35 );
			SetResistance( ResistanceType.Cold, 20, 35 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.EvalInt, 80.1, 95.0 );
			SetSkill( SkillName.Magery, 80.1, 95.0 );
			SetSkill( SkillName.MagicResist, 120.1, 125.0 );
			SetSkill( SkillName.Tactics, 70.1, 90.0 );
			SetSkill( SkillName.Wrestling, 70.1, 90.0 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 40;

			PackItem( new WaterFlask( ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Potions );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 4 ) == 1 &&  ( this.Fame > 6500 || this.WhisperHue == 999 ) )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "freezing water" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "freezing water", 296, 0 );
				}
			}
		}

		public WaterWeird( Serial serial ) : base( serial )
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