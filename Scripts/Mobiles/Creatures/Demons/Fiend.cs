using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a fiend corpse" )]
	public class Fiend : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 125.0; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public Fiend () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "daemon" );
			Title = "the fiend";
			Body = Utility.RandomList( 9, 320 );
			BaseSoundID = 357;

			if ( Utility.RandomMinMax( 1, 10 ) == 1 ) // FEMALE
			{
				Name = NameList.RandomName( "goddess" );
				Body = 138;
				Hue = 0xB01;
				BaseSoundID = 0x4B0;
			}
			else if ( Utility.RandomMinMax( 1, 10 ) == 1 )
			{
				Body = Utility.RandomList( 127, 9 );
				if ( Body == 40 ){ Hue = 0xB01; }
			}

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
			ControlSlots = Core.SE ? 4 : 5;
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
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 && this.Body == 320 )
					{
						BaseWeapon sword = new Longsword();
						sword.AccuracyLevel = WeaponAccuracyLevel.Supremely;
						sword.MinDamage = sword.MinDamage + 4;
						sword.MaxDamage = sword.MaxDamage + 9;
            			sword.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						sword.AosElementDamages.Fire = 50;
						sword.Name = "sword of " + this.Title;
						if ( Utility.RandomMinMax( 0, 100 ) > 75 ){ sword.Slayer = SlayerName.Repond; }
						if ( Utility.RandomMinMax( 0, 100 ) > 75 ){ sword.WeaponAttributes.HitFireball = 10; }
						sword.Hue = 0xB17;
						c.DropItem( sword );
					}

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.IntelligentAction.FameBasedEvent( this ) )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						Server.Misc.ContainerFunctions.MakeDemonBox( MyChest, this );
						MyChest.Hue = 0xB71;
						c.DropItem( MyChest );
					}
				}
			}
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
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public Fiend( Serial serial ) : base( serial )
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