using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a reptile corpse" )]
	public class Toraxen : BaseCreature
	{
		[Constructable]
		public Toraxen() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a toraxen";
			Body = 206;
			Hue = 0x96D;
			BaseSoundID = 660;

			SetStr( 106, 130 );
			SetDex( 6, 25 );
			SetInt( 11, 20 );

			SetHits( 66, 96 );
			SetStam( 46, 65 );
			SetMana( 0 );

			SetDamage( 9, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 50;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 57.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public Toraxen(Serial serial) : base(serial)
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