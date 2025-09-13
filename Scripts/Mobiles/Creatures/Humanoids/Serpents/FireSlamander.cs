using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a salamander corpse" )]
	public class FireSalamander : BaseCreature
	{
		[Constructable]
		public FireSalamander() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace = 3;
			Name = "a fire slamander";
			Body = 673;
			BaseSoundID = 634;

			SetStr( 181, 205 );
			SetDex( 191, 215 );
			SetInt( 96, 120 );

			SetHits( 109, 123 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Fire, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Cold, 5, 15 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 30;

			PackItem( new SulfurousAsh( 20 ) );

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
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
					BaseWeapon fork = new Pitchfork();
					fork.MinDamage = fork.MinDamage + 4;
					fork.MaxDamage = fork.MaxDamage + 8;
					fork.Attributes.BonusHits = 50;
					fork.AosElementDamages.Fire=100;
					fork.Hue = 0x4EC;
					fork.Name = "fire salamander trident";
					c.DropItem( fork );
				}
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "scorching ooze" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "scorching ooze", 0x496, 0 );
				}
			}
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public FireSalamander( Serial serial ) : base( serial )
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