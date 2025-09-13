using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a demonic corpse" )]
	public class Marilith : BaseCreature
	{
		[Constructable]
		public Marilith() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "goddess" );
			Title = "the marilith";
			Body = 63;
			BaseSoundID = 0x4B0;

			SetStr( 436, 485 );
			SetDex( 196, 215 );
			SetInt( 131, 155 );

			SetHits( 402, 431 );
			SetMana( 0 );

			SetDamage( 11, 27 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.3, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 9500;
			Karma = -9500;

			VirtualArmor = 50;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			Server.Misc.IntelligentAction.LeapToAttacker( this, from );
			base.OnDamage( amount, from, willKill );
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
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon sword = new Scimitar();
						sword.AccuracyLevel = WeaponAccuracyLevel.Supremely;
						sword.MinDamage = sword.MinDamage + 7;
						sword.MaxDamage = sword.MaxDamage + 12;
            			sword.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						sword.AosElementDamages.Fire = 50;
						sword.Name = "scimitar of " + this.Title;
						sword.Slayer = SlayerName.Repond;
						if ( Utility.RandomMinMax( 0, 100 ) > 50 ){ sword.WeaponAttributes.HitFireball = 25; }
						sword.Hue = 0x54B;
						c.DropItem( sword );
					}
				}
			}
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			reflect = true; // Every spell is reflected back to the caster
		}

		public Marilith( Serial serial ) : base( serial )
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