using System;
using Server;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
	[CorpseName( "a fleshrenderer corpse" )]
	public class FleshRenderer : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.Dismount : WeaponAbility.ParalyzingBlow;
		}

		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		[Constructable]
		public FleshRenderer() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed
			Name = "a fleshrenderer";
			Body = 999;

			SetStr( 401, 460 );
			SetDex( 201, 210 );
			SetInt( 221, 260 );

			SetHits( 4500 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 70, 80 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.MagicResist, 155.1, 160.0 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 23000;
			Karma = -23000;

			VirtualArmor = 34;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
				DemonKnight.DistributeArtifact( this );
			
			if ( Utility.RandomDouble() < 0.10 )
				switch ( Utility.Random( 20 ))		  
				{
					case 0: c.DropItem( new ArmorEnhancementDeed() ); break;
					case 1: c.DropItem( new AosEnhancementDeed() ); break;
					case 2: c.DropItem( new EnhancementDeed() ); break;
					case 3: c.DropItem( new SkillEnhancementDeed() ); break;
					case 4: c.DropItem( new WeaponEnhancementDeed() ); break;
				};
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public override int GetAttackSound()
		{
			return 0x34C;
		}

		public override int GetHurtSound()
		{
			return 0x354;
		}

		public override int GetAngerSound()
		{
			return 0x34C;
		}

		public override int GetIdleSound()
		{
			return 0x34C;
		}

		public override int GetDeathSound()
		{
			return 0x354;
		}

		public FleshRenderer( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 660 )
				BaseSoundID = -1;
		}
	}
}