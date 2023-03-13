using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an archfiend corpse" )]
	public class Archfiend : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 150.0; } }
		public override double DispelFocus{ get{ return 25.0; } }

		[Constructable]
		public Archfiend () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "demonic" );
			Title = "the archfiend";
			Body = 427;
			BaseSoundID = 357;

			if ( Utility.RandomMinMax( 1, 10 ) == 1 ) // FEMALE
			{
				Name = NameList.RandomName( "goddess" );
				Body = 436;
				Hue = 0xB01;
				BaseSoundID = 0x4B0;
			}
			else if ( Utility.RandomMinMax( 1, 10 ) == 1 )
			{
				Body = 191;
			}

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
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 95.5, 100.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 90;
		}

		public override void OnAfterSpawn()
		{
			if ( Server.Misc.Worlds.IsFireDungeon( this.Location, this.Map ) ){	this.Hue = 0xB17;	SetResistance( ResistanceType.Fire, 70, 80 );	SetResistance( ResistanceType.Cold, 10, 20 ); }
			else if ( Server.Misc.Worlds.IsIceDungeon( this.Location, this.Map ) ){	this.Hue = Utility.RandomList( 0xB33, 0xB34, 0xB35, 0xB36, 0xB37 );	SetResistance( ResistanceType.Fire, 10, 20 );	SetResistance( ResistanceType.Cold, 70, 80 ); }
			else if ( Server.Misc.Worlds.IsSeaDungeon( this.Location, this.Map ) ){	this.Hue = Utility.RandomList( 0xB3D, 0xB3E, 0xB3F, 0xB40 );	SetResistance( ResistanceType.Fire, 30, 40 );	SetResistance( ResistanceType.Cold, 30, 40 );	SetResistance( ResistanceType.Poison, 40, 50 ); }

			base.OnAfterSpawn();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
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
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 && Body == 191 )
					{
						BaseWeapon sword = new Longsword();
						sword.AccuracyLevel = WeaponAccuracyLevel.Supremely;
						sword.MinDamage = sword.MinDamage + 7;
						sword.MaxDamage = sword.MaxDamage + 12;
            			sword.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						sword.AosElementDamages.Fire = 50;
						sword.Name = "sword of " + this.Title;
						sword.Slayer = SlayerName.Repond;
						if ( Utility.RandomMinMax( 0, 100 ) > 50 ){ sword.WeaponAttributes.HitFireball = 25; }
						sword.Hue = 0xB73;
						c.DropItem( sword );
					}

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.IntelligentAction.FameBasedEvent( this ) )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						Server.Misc.ContainerFunctions.MakeDemonBox( MyChest, this );
						MyChest.Hue = 0xAFA;
						c.DropItem( MyChest );
					}
				}
			}
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 22; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public Archfiend( Serial serial ) : base( serial )
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