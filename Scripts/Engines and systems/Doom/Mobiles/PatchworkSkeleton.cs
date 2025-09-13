using System;
using Server;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
	[CorpseName( "a patchwork skeletal corpse" )]
	public class PatchworkSkeleton : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

		[Constructable]
		public PatchworkSkeleton() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a patchwork skeleton";
			Body = 167;
			BaseSoundID = 0x48D;

			SetStr( 96, 120 );
			SetDex( 71, 95 );
			SetInt( 16, 40 );

			SetHits( 58, 72 );

			SetDamage( 18, 22 );

			SetDamageType( ResistanceType.Physical, 85 );
			SetDamageType( ResistanceType.Cold, 15 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 70.1, 95.0 );
			SetSkill( SkillName.Tactics, 55.1, 80.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 500;
			Karma = -500;

			VirtualArmor = 54;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			if ( Utility.RandomDouble() <= 0.02 )
				switch ( Utility.Random( 20 ))		  
				{
					case 0: c.DropItem( new ArmorEnhancementDeed() ); break;
					case 1: c.DropItem( new AosEnhancementDeed() ); break;
					case 2: c.DropItem( new EnhancementDeed() ); break;
					case 3: c.DropItem( new SkillEnhancementDeed() ); break;
					case 4: c.DropItem( new WeaponEnhancementDeed() ); break;
				};

		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public PatchworkSkeleton( Serial serial ) : base( serial )
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
