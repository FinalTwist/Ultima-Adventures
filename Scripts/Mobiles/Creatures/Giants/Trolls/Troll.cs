using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a troll corpse" )]
	public class Troll : BaseCreature
	{
		[Constructable]
		public Troll () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a troll";
			Body = Utility.RandomList( 53, 439 );
			BaseSoundID = 461;

			SetStr( 176, 205 );
			SetDex( 46, 65 );
			SetInt( 46, 70 );

			SetHits( 106, 123 );

			SetDamage( 8, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 15 );
			SetResistance( ResistanceType.Energy, 5, 15 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 3500;
			Karma = -3500;

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
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && this.Body == 53 && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon axe = new LargeBattleAxe();
						axe.MinDamage = axe.MinDamage + 4;
						axe.MaxDamage = axe.MaxDamage + 8;
            			axe.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						axe.Name = "trollish battle axe";
						c.DropItem( axe );
					}
					else if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && this.Body == 439 && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon mace = new WarMace();
						mace.MinDamage = mace.MinDamage + 4;
						mace.MaxDamage = mace.MaxDamage + 8;
            			mace.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						mace.Name = "trollish war mace";
						c.DropItem( mace );
					}
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 2; } }

		public Troll( Serial serial ) : base( serial )
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