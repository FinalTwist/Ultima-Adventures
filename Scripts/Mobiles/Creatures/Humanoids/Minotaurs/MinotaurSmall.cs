using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a minotaur corpse" )]
	public class MinotaurSmall : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ParalyzingBlow;
		}

		[Constructable]
		public MinotaurSmall() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) // NEED TO CHECK
		{
			Name = "a minotaur";
			Body = Utility.RandomList( 78, 357 );
			BaseSoundID = 0x54E;

			SetStr( 156, 180 );
			SetDex( 16, 35 );
			SetInt( 11, 20 );

			SetHits( 96, 120 );
			SetStam( 46, 65 );
			SetMana( 0 );

			SetDamage( 9, 18 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 56.1, 64.0 );
			SetSkill( SkillName.Tactics, 93.3, 97.8 );
			SetSkill( SkillName.Wrestling, 90.4, 92.1 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 28;

			if ( Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				Body = 650;

				SetStr( 256, 280 );
				SetDex( 116, 135 );
				SetInt( 61, 80 );

				SetHits( 196, 220 );
				SetStam( 76, 95 );
				SetMana( 0 );

				SetDamage( 12, 24 );

				Fame = 4000;
				Karma = -4000;

				VirtualArmor = 40;
			}
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
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && this.Body == 78 && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon axe = new Axe();
						axe.MinDamage = axe.MinDamage + 4;
						axe.MaxDamage = axe.MaxDamage + 8;
            			axe.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						axe.Name = "minotaur axe";
						c.DropItem( axe );
					}
					else if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && this.Body == 650 && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon axe = new DoubleAxe();
						axe.MinDamage = axe.MinDamage + 5;
						axe.MaxDamage = axe.MaxDamage + 9;
            			axe.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						axe.Name = "minotaur battle axe";
						c.DropItem( axe );
					}
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public MinotaurSmall( Serial serial ) : base( serial )
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
