/**********************************************************
	Name: Champion Spawn Monster
	Scripted By: Formosa
	Version: v1.0
	Update Date: March 10, 2013

	Notes: 
	Anyone can modify/redistribute this 
	Do Not Remove/Change This Header!!	
**********************************************************/

/* Based on AbysmalHorror, still no infos on Abyssal Abomination... Including correct body ID */
using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an abyssal abomination champion corpse" )]
	public class ChampionAbyssalAbomination : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.MortalStrike : WeaponAbility.WhirlwindAttack;
		}

		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		[Constructable]
		public ChampionAbyssalAbomination() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ChampionHue();

			Name = "an abyssal abomination";
			Body = 312;
			BaseSoundID = 0x451;

			SetStr( 401, 420 );
			SetDex( 81, 90 );
			SetInt( 401, 420 );

			SetHits( 6000 );

			SetDamage( 13, 17 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Cold, 50, 55 );
			SetResistance( ResistanceType.Poison, 60, 65 );
			SetResistance( ResistanceType.Energy, 77, 80 );

			SetSkill( SkillName.EvalInt, 200.0 );
			SetSkill( SkillName.Magery, 112.6, 117.5 );
			SetSkill( SkillName.Meditation, 200.0 );
			SetSkill( SkillName.MagicResist, 117.6, 120.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 84.1, 88.0 );

			Fame = 26000;
			Karma = -26000;

			VirtualArmor = 54;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Gems );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			//if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
			//	DemonKnight.DistributeArtifact( this );
		}

		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public virtual void ChampionHue()
		{
			switch ( Utility.Random ( 10 ) )
			{
				case 0: Hue = ( 11 ); break;
				case 1: Hue = ( 25 ); break;
				case 2: Hue = ( 40 ); break;
				case 3: Hue = ( 44 ); break;
				case 4: Hue = ( 33 ); break;
				case 5: Hue = ( 49 ); break;
				case 6: Hue = ( 47 ); break;
				case 7: Hue = ( 56 ); break;
				case 8: Hue = ( 76 ); break;
				case 9: Hue = ( 95 ); break;
			}
		}

		public ChampionAbyssalAbomination( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 357 )
				BaseSoundID = 0x451;
		}
	}
}