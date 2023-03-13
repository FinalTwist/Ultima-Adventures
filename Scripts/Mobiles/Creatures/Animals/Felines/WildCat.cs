using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a WildCat corpse" )]
	[TypeAlias( "Server.Mobiles.Housecat" )]
	public class WildCat : BaseCreature
	{
		[Constructable]
		public WildCat() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a wild cat";
			Body = 0xC9;
			Hue = Utility.RandomAnimalHue();
			BaseSoundID = 0x69;

			SetStr( 19 );
			SetDex( 35 );
			SetInt( 5 );

			SetHits( 25 );
			SetMana( 0 );

			SetDamage( 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 10 );

			SetSkill( SkillName.MagicResist, 5.0 );
			SetSkill( SkillName.Tactics, 4.0 );
			SetSkill( SkillName.Wrestling, 25.0 );

			Fame = 0;
			Karma = 150;

			VirtualArmor = 8;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 10;
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public WildCat(Serial serial) : base(serial)
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