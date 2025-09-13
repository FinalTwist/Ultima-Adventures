using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class Reptalon : BaseMount
	{
		[Constructable]
		public Reptalon() : base( "a dragon", 0x114, 0x3E90, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.35 )
		{
			BaseSoundID = 0x16A; // TODO check

			Hue = Utility.RandomGreenHue();

			SetStr( 1001, 1025 );
			SetDex( 152, 164 );
			SetInt( 251, 289 );

			SetHits( 833, 931 );

			SetDamage( 21, 28 );
			
			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Poison, 25 );
			SetDamageType( ResistanceType.Energy, 75 );

			SetResistance( ResistanceType.Physical, 53, 64 );
			SetResistance( ResistanceType.Fire, 35, 45 );
			SetResistance( ResistanceType.Cold, 36, 45 );
			SetResistance( ResistanceType.Poison, 52, 63 );
			SetResistance( ResistanceType.Energy, 71, 83 );

			SetSkill( SkillName.Wrestling, 101.5, 118.2 );
			SetSkill( SkillName.Tactics, 101.7, 108.2 );
			SetSkill( SkillName.MagicResist, 76.4, 89.9 );
			SetSkill( SkillName.Anatomy, 56.4, 59.7 );
			
			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 101.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ParalyzingBlow;
		}

		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 10; } }
		public override bool CanBreath{ get{ return true; } }
		public override bool CanAngerOnTame{ get { return true; } }
		public override bool StatLossAfterTame{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public Reptalon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
