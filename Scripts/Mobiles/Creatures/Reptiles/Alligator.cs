using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a reptile corpse" )]
	public class Alligator : BaseCreature
	{
		[Constructable]
		public Alligator() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an alligator";
			Body = 206;
			Hue = Utility.RandomList( 0x7D1, 0x7D2, 0x7D3, 0x7D4, 0x7D5, 0x7D6 );
			BaseSoundID = 660;

			SetStr( 76, 100 );
			SetDex( 6, 25 );
			SetInt( 11, 20 );

			SetHits( 46, 60 );
			SetStam( 46, 65 );
			SetMana( 0 );

			SetDamage( 5, 15 );

			if ( Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				Name = "a crocodile";
				Hue = Utility.RandomList( 0x7D7, 0x7D8, 0x7D9, 0x7DA, 0x7DB, 0x7DC );

				SetStr( 106, 130 );
				SetDex( 16, 35 );
				SetInt( 11, 20 );

				SetHits( 76, 90 );
				SetStam( 46, 65 );
				SetMana( 0 );

				SetDamage( 8, 18 );
			}

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 600;
			Karma = -600;

			VirtualArmor = 30;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 47.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public Alligator(Serial serial) : base(serial)
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

			if ( BaseSoundID == 0x5A )
				BaseSoundID = 660;
		}
	}
}