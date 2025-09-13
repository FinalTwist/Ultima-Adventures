using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a lizard corpse" )]
	public class GiantLizard : BaseCreature
	{
		[Constructable]
		public GiantLizard() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater giant lizard";
			Body = 206;
			Hue = Utility.RandomList( 0x7D1, 0x7D2, 0x7D3, 0x7D4, 0x7D5, 0x7D6, 0x7D7, 0x7D8, 0x7D9, 0x7DA, 0x7DB, 0x7DC );
			BaseSoundID = 0x5A;

			SetStr( 30, 80 );
			SetDex( 10, 20 );
			SetInt( 5, 8 );

			SetHits( 29, 78 );
			SetMana( 0 );

			SetDamage( 3, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Poisoning, 70.1, 100.0 );
			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 650;
			Karma = -350;

			VirtualArmor = 10;
		}
		else
		{
			Name = "a giant lizard";
			Body = 206;
			Hue = Utility.RandomList( 0x7D1, 0x7D2, 0x7D3, 0x7D4, 0x7D5, 0x7D6, 0x7D7, 0x7D8, 0x7D9, 0x7DA, 0x7DB, 0x7DC );
			BaseSoundID = 0x5A;

			SetStr( 30, 40 );
			SetDex( 10, 20 );
			SetInt( 5, 8 );

			SetHits( 29, 38 );
			SetMana( 0 );

			SetDamage( 3, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Poisoning, 70.1, 100.0 );
			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 350;
			Karma = -350;

			VirtualArmor = 10;
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 2; } }
		public override HideType HideType{ get{ return HideType.Horned; } }

		public GiantLizard(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}