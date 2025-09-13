using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a dire wolf corpse" )]
	[TypeAlias( "Server.Mobiles.Direwolf" )]
	public class DireWolf : BaseCreature
	{
		public override bool CanChew { get{return true;}}
		[Constructable]
		public DireWolf() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater dire wolf";
			Body = 225;
			BaseSoundID = 0xE5;

			SetStr( 96, 220 );
			SetDex( 81, 175 );
			SetInt( 36, 60 );

			SetHits( 58, 112 );
			SetMana( 0 );

			SetDamage( 11, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 35 );
			SetResistance( ResistanceType.Fire, 10, 30 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 57.6, 75.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 2900;
			Karma = -2500;

			VirtualArmor = 22;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 89.1;
		}
else
		{
			Name = "a dire wolf";
			Body = 225;
			BaseSoundID = 0xE5;

			SetStr( 96, 120 );
			SetDex( 81, 105 );
			SetInt( 36, 60 );

			SetHits( 58, 72 );
			SetMana( 0 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 57.6, 75.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 22;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 83.1;
		}
	}
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 7; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 4 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public DireWolf(Serial serial) : base(serial)
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