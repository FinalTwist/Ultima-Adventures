using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a hook horror corpse" )]
	public class HookHorror : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public HookHorror() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a hook horror";
			Body = 366;
			Hue = 0xB7E;
			BaseSoundID = 0x56F;

			SetStr( 251, 280 );
			SetDex( 271, 290 );
			SetInt( 31, 62 );

			SetHits( 201, 218 );

			SetDamage( 14, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 39;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
		}

		public HookHorror( Serial serial ) : base( serial )
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