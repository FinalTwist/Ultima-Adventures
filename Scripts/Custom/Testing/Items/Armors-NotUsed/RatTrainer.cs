using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a rat training corpse" )]
	public class RatTrainer : BaseCreature
	{
		[Constructable]
		public RatTrainer() : base( AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a rat trainer";
			Body = 238;
			BaseSoundID = 0xCC;

			SetStr( 9 );
			SetDex( 35 );
			SetInt( 5 );

			SetHits( 50000 );
			SetMana( 0 );

			SetDamage( 1, 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 4.0 );
			SetSkill( SkillName.Wrestling, 4.0 );

			Fame = 150;
			Karma = -150;

			VirtualArmor = 6;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = -0.9;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.GrainsAndHay; } }

		public RatTrainer(Serial serial) : base(serial)
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
