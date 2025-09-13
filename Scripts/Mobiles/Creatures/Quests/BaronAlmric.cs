using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;

namespace Server.Mobiles
{
	[CorpseName( "Baron Almric's corpse" )]
	public class BaronAlmric : BaseCreature
	{
		[Constructable]
		public BaronAlmric () : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Almric";
			Title = "the Baron";
			Body = 65;
			BaseSoundID = 0x47D;

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 151, 250 );

			SetHits( 592, 711 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Item skull = new SkullOfBaronAlmric();
			c.DropItem( skull );

			Mobile killer = this.LastKiller;
			if ( killer != null && this.Body == 353 )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						switch( Utility.RandomMinMax( 0, 5 ) )
						{
							case 0:
								BaseWeapon sword = new Longsword();
								sword.AccuracyLevel = WeaponAccuracyLevel.Supremely;
								sword.MinDamage = sword.MinDamage + 7;
								sword.MaxDamage = sword.MaxDamage + 12;
								sword.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
								sword.Slayer = SlayerName.DragonSlaying;
								sword.Name = "sword of Baron Almric";
								sword.Hue = 0x9C4;
								c.DropItem( sword );	
								break;
							case 1: MagicDragonLegs legs = new MagicDragonLegs(); legs.DragonKiller = "Slain by Baron Almric"; c.DropItem( legs ); break;
							case 2: MagicDragonGloves gloves = new MagicDragonGloves(); gloves.DragonKiller = "Slain by Baron Almric"; c.DropItem( gloves ); break;
							case 3: MagicDragonArms arms = new MagicDragonArms(); arms.DragonKiller = "Slain by Baron Almric"; c.DropItem( arms ); break;
							case 4: MagicDragonChest chest = new MagicDragonChest(); chest.DragonKiller = "Slain by Baron Almric"; c.DropItem( chest ); break;
							case 5: MagicDragonHelm helm = new MagicDragonHelm(); helm.DragonKiller = "Slain by Baron Almric"; c.DropItem( helm ); break;
						}
					}
				}
			}
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Necrotic; } }
		public override bool BardImmune { get { return true; } }

		public BaronAlmric( Serial serial ) : base( serial )
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