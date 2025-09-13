using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a toad corpse" )]
	public class Toad : BaseCreature
	{
		private Timer m_Timer;

		[Constructable]
		public Toad() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a toad";
			Body = 81;
			Hue = Utility.RandomList( 0x7D7, 0x7D8, 0x7D9, 0x7DA, 0x7DB, 0x7DC );
			BaseSoundID = 0x266;

			SetStr( 46, 70 );
			SetDex( 6, 25 );
			SetInt( 11, 20 );

			SetHits( 28, 42 );
			SetMana( 0 );

			SetDamage( 1, 2 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 350;
			Karma = 0;

			VirtualArmor = 6;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 23.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 4; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }

		public Toad(Serial serial) : base(serial)
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