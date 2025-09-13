using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a daemon corpse" )]
	public class AbysmalDaemon : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Disarm;
		}

		public override double DispelDifficulty{ get{ return 125.0; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public AbysmalDaemon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "demonic" );
			Title = "the abysmal archfiend";
			Body = 88;
			Hue = 0xA63;
			BaseSoundID = 372;

			SetStr( 476, 505 );
			SetDex( 76, 95 );
			SetInt( 301, 325 );

			SetHits( 286, 303 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 70.1, 80.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 58;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 1 );
			AddLoot( LootPack.FilthyRich, 1 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			int staff = 0;

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon harv = new BladedStaff();
						harv.AccuracyLevel = WeaponAccuracyLevel.Supremely;
						harv.MinDamage = harv.MinDamage + 10;
						harv.MaxDamage = harv.MaxDamage + 20;
            			harv.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						harv.AosElementDamages.Energy=50;
						harv.Name = "staff of the abyss";
						harv.Hue = 0x490;
						c.DropItem( harv );
						staff = 0;
					}
				}
			}

			if ( staff == 0 ){ c.DropItem( new BladedStaff() ); }
		}

		public AbysmalDaemon( Serial serial ) : base( serial )
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