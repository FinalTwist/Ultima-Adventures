using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class DeathBear : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public DeathBear() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an undead bear";
			Body = 34;
			BaseSoundID = 0xA3;
			Hue = 0xB97;

			SetStr( 126, 155 );
			SetDex( 81, 105 );
			SetInt( 16, 40 );

			SetHits( 76, 93 );
			SetMana( 0 );

			SetDamage( 8, 13 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 24;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Necrotic; } }

		public DeathBear( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}