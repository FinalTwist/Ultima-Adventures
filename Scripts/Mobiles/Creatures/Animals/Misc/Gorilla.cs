using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a gorilla corpse" )]
	public class Gorilla : BaseCreature
	{
		[Constructable]
		public Gorilla() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a gorilla";
			Body = Utility.RandomList( 0x1D, 161 );
			BaseSoundID = 0x9E;

			SetStr( 53, 95 );
			SetDex( 36, 55 );
			SetInt( 36, 60 );

			SetHits( 38, 51 );
			SetMana( 0 );

			SetDamage( 4, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 43.3, 58.0 );
			SetSkill( SkillName.Wrestling, 43.3, 58.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 20;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -18.9;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 6 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public Gorilla(Serial serial) : base(serial)
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