using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a devil corpse" )]
	public class Satan : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 150.0; } }
		public override double DispelFocus{ get{ return 25.0; } }

		[Constructable]
		public Satan () : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "Satan";
			Title = "the devil lord";
			Body = 509;
			BaseSoundID = 0x47D;

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 900, 1100 );

			SetHits( 1592, 1711 );

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
			SetSkill( SkillName.Magery, 95.5, 110.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 25000;
			Karma = -25000;

			VirtualArmor = 90;
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
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon fork = new Pitchfork();
						fork.AccuracyLevel = WeaponAccuracyLevel.Supremely;
						fork.MinDamage = fork.MinDamage + 7;
						fork.MaxDamage = fork.MaxDamage + 12;
            			fork.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						fork.AosElementDamages.Fire = 50;
						fork.Name = "Satan's Pitchfork";
						fork.Slayer = SlayerName.Repond;
						if ( Utility.RandomMinMax( 0, 100 ) > 50 ){ fork.WeaponAttributes.HitFireball = 50; }
						fork.Hue = 0x489;
						c.DropItem( fork );
					}

					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					if ( item is OrbOfTheAbyss )
					{
						targets.Add( item );
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						OrbOfTheAbyss item = ( OrbOfTheAbyss )targets[ i ];
						if ( killer == item.owner ){ item.Delete(); } // PLAYERS ARE ONLY ALLOWED ONE ORB
					}

					OrbOfTheAbyss orb = new OrbOfTheAbyss();
					orb.owner = killer;
					int min = 25;
					int max = 100;
					int props = 2 + Utility.RandomMinMax( 0, 10 );
					BaseRunicTool.ApplyAttributesTo( (BaseJewel)orb, false, killer.Luck, props, min, max );
					killer.AddToBackpack( orb );
					killer.SendMessage( "You have obtained Satan's Orb of the Abyss!" );
					LoggingFunctions.LogGenericQuest( killer, "has obtained Satan's orb of the abyss" );
				}
			}
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public Satan( Serial serial ) : base( serial )
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