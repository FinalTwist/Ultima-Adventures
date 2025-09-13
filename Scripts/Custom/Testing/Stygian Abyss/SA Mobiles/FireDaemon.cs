using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an fire slith corpse" )]
	public class FireDaemon : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ConcussionBlow;
		}

		[Constructable]
		public FireDaemon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an fire slith";
			Body = 0x310;
			BaseSoundID = 0x47D;

			SetStr( 549, 1199 );
			SetDex( 136, 206 );
			SetInt( 202, 336 );

			SetHits( 1111, 1478 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 48, 93 );
			SetResistance( ResistanceType.Fire, 60, 100 );
			SetResistance( ResistanceType.Cold, -8, 57 );
			SetResistance( ResistanceType.Poison, 30, 100 );
			SetResistance( ResistanceType.Energy, 37, 50 );

			SetSkill( SkillName.MagicResist, 98.1, 132.6 );
			SetSkill( SkillName.Tactics, 86.9, 95.5 );
			SetSkill( SkillName.Wrestling, 42.2, 98.8 );
			SetSkill( SkillName.Magery, 97.1, 100.8 );
			SetSkill( SkillName.EvalInt, 91.1, 91.8 );
			SetSkill( SkillName.Meditation, 45.4, 94.1 );

			Fame = 7000;
			Karma = -10000;

			VirtualArmor = 55;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public FireDaemon( Serial serial ) : base( serial )
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