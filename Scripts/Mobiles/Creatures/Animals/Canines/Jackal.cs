using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a jackal corpse" )]
	[TypeAlias( "Server.Mobiles.Timberwolf" )]
	public class Jackal : BaseCreature
	{
		public override bool CanChew { get{return true;}}
		[Constructable]
		public Jackal() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater jackal";
			Body = 225;
			BaseSoundID = 0xE5;
			Hue = 1705;

			SetStr( 56, 140 );
			SetDex( 56, 125 );
			SetInt( 11, 25 );

			SetHits( 34, 98 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 30 );
			SetResistance( ResistanceType.Fire, 5, 20 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 27.6, 45.0 );
			SetSkill( SkillName.Tactics, 30.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 850;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 33.1;
		}
		else
				{
			Name = "a jackal";
			Body = 225;
			BaseSoundID = 0xE5;
			Hue = 1705;

			SetStr( 56, 80 );
			SetDex( 56, 75 );
			SetInt( 11, 25 );

			SetHits( 34, 48 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 27.6, 45.0 );
			SetSkill( SkillName.Tactics, 30.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 23.1;
		}
		}
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 3 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public Jackal(Serial serial) : base(serial)
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