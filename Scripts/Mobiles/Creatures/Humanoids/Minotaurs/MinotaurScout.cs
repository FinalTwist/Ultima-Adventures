using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a minotaur corpse" )]
	public class MinotaurScout : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ParalyzingBlow;
		}

		[Constructable]
		public MinotaurScout() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a minotaur warrior";
			Body = 280;
			BaseSoundID = 0x54E;

			SetStr( 256, 280 );
			SetDex( 116, 135 );
			SetInt( 61, 80 );

			SetHits( 196, 220 );
			SetStam( 76, 95 );
			SetMana( 0 );

			SetDamage( 12, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.6, 67.5 );
			SetSkill( SkillName.Tactics, 86.9, 103.6 );
			SetSkill( SkillName.Wrestling, 85.6, 104.5 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 40;
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
						BaseWeapon blade = new RuneBlade();
						blade.MinDamage = blade.MinDamage + 4;
						blade.MaxDamage = blade.MaxDamage + 8;
            			blade.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						blade.Name = "minotaur war blades";
						c.DropItem( blade );
					}
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public MinotaurScout( Serial serial ) : base( serial )
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
