using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a gargoyle corpse" )]
	public class GargoyleWarrior : BaseCreature
	{
		[Constructable]
		public GargoyleWarrior() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace =2;
			Name = "a gargoyle guard";
			Body = 129;
			BaseSoundID = 357;
			Hue = 0xB80;

			SetStr( 246, 375 );
			SetDex( 96, 125 );
			SetInt( 101, 125 );

			SetHits( 168, 205 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 55 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 25, 35 );

			SetSkill( SkillName.EvalInt, 80.1, 95.0 );
			SetSkill( SkillName.Magery, 80.1, 95.0 );
			SetSkill( SkillName.MagicResist, 80.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 90.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 40;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.RandomMinMax( 1, 4 ) );
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
						BaseWeapon weapon = new Scimitar();
						weapon.MinDamage = weapon.MinDamage + 6;
						weapon.MaxDamage = weapon.MaxDamage + 10;
            			weapon.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						weapon.Name = "gargoyle scimitar";
						weapon.Hue = 0xB80;
						c.DropItem( weapon );
					}
				}
			}
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public GargoyleWarrior( Serial serial ) : base( serial )
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