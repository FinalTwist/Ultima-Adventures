using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a fox corpse" )]
	public class Fox : BaseCreature
	{
		[Constructable]
		public Fox() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a fox";
			Body = 121;
			BaseSoundID = 0x85;

			SetStr( 27, 37 );
			SetDex( 28, 43 );
			SetInt( 29, 37 );

			SetHits( 17, 22 );
			SetMana( 0 );

			SetDamage( 4, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 15 );

			SetSkill( SkillName.MagicResist, 22.1, 47.0 );
			SetSkill( SkillName.Tactics, 19.2, 31.0 );
			SetSkill( SkillName.Wrestling, 19.2, 31.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 12;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -15.3;
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public Fox(Serial serial) : base(serial)
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