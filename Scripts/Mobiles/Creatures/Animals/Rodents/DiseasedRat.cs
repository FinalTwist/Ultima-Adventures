using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a sickly rat corpse" )]
	public class DiseasedRat : BaseCreature
	{
		[Constructable]
		public DiseasedRat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a diseased rat";
			Body = 0xD7;
			BaseSoundID = 0x188;

			SetStr( 52, 94 );
			SetDex( 66, 85 );
			SetInt( 16, 30 );

			SetHits( 46, 59 );
			SetMana( 0 );

			SetDamage( 4, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Poison, 25, 35 );

			SetSkill( SkillName.MagicResist, 45.1, 50.0 );
			SetSkill( SkillName.Tactics, 49.3, 64.0 );
			SetSkill( SkillName.Wrestling, 49.3, 64.0 );

			Fame = 600;
			Karma = -600;

			VirtualArmor = 18;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 32.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies | FoodType.Eggs; } }

		public DiseasedRat(Serial serial) : base(serial)
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