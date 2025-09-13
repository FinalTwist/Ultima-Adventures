using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a cyclops corpse" )]
	public class ShamanicCyclops : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 20 ); }

		[Constructable]
		public ShamanicCyclops() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "greek" );
			Title = "the shamanic cyclops";
			Body = 75;
			BaseSoundID = 604;
			Hue = 0xA1F;
				if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ Hue = 0xA4B; }

			SetStr( 336, 385 );
			SetDex( 96, 115 );
			SetInt( 236, 285 );

			SetDamage( 12, 23 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.MagicResist, 80.2, 110.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 48;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && this.Body == 757 && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon hammer = new WarHammer();
						hammer.AccuracyLevel = WeaponAccuracyLevel.Supremely;
						hammer.MinDamage = hammer.MinDamage + 6;
						hammer.MaxDamage = hammer.MaxDamage + 10;
            			hammer.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						hammer.Name = "magical cyclops hammer";
						hammer.AosElementDamages.Energy = 25;
						hammer.AosElementDamages.Physical = 75;
						hammer.WeaponAttributes.HitLightning = 10;
						c.DropItem( hammer );
					}
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override int Meat{ get{ return 4; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Goliath; } }

		public ShamanicCyclops( Serial serial ) : base( serial )
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