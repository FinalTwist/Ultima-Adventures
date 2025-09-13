using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a sickly rat corpse" )]
	public class SicklyRat : BaseCreature
	{
		[Constructable]
		public SicklyRat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sewer rat";
			Body = 0xD7;
			BaseSoundID = 0x188;

			SetStr( 52, 94 );
			SetDex( 66, 75 );
			SetInt( 16, 30 );

			SetHits( 36, 49 );
			SetMana( 0 );

			SetDamage( 8, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Poison, 45, 55 );

			SetSkill( SkillName.MagicResist, 45.1, 50.0 );
			SetSkill( SkillName.Tactics, 49.3, 64.0 );
			SetSkill( SkillName.Wrestling, 49.3, 64.0 );

			Fame = 600;
			Karma = -600;

			VirtualArmor = 24;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override Poison HitPoison{ get{ return Poison.Regular; } }

		public SicklyRat(Serial serial) : base(serial)
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