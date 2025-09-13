using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a reptile corpse" )]
	public class SwampGator : BaseCreature
	{
		[Constructable]
		public SwampGator() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater swamp gator";
			Body = 206;
			Hue = Utility.RandomList( 0x7D1, 0x7D2, 0x7D3, 0x7D4, 0x7D5, 0x7D6 );
			BaseSoundID = 660;

			SetStr( 41, 171 );
			SetDex( 47, 177 );
			SetInt( 27, 57 );

			SetHits( 27, 91 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 35 );
			SetResistance( ResistanceType.Cold, 5, 10 );

			SetSkill( SkillName.MagicResist, 26.8, 44.5 );
			SetSkill( SkillName.Tactics, 29.8, 47.5 );
			SetSkill( SkillName.Wrestling, 29.8, 47.5 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 69.1;
		}
		else
		{
			Name = "a swamp gator";
			Body = 206;
			Hue = Utility.RandomList( 0x7D1, 0x7D2, 0x7D3, 0x7D4, 0x7D5, 0x7D6 );
			BaseSoundID = 660;

			SetStr( 41, 71 );
			SetDex( 47, 77 );
			SetInt( 27, 57 );

			SetHits( 27, 41 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Cold, 5, 10 );

			SetSkill( SkillName.MagicResist, 26.8, 44.5 );
			SetSkill( SkillName.Tactics, 29.8, 47.5 );
			SetSkill( SkillName.Wrestling, 29.8, 47.5 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 59.1;
		}
	}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return Utility.RandomBool() ? HideType.Regular : HideType.Horned; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public SwampGator(Serial serial) : base(serial)
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