using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a dog corpse" )]
	public class MadDog : BaseCreature
	{
		public override bool CanChew { get{return true;}}
		[Constructable]
		public MadDog() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a mad dog";
			Body = 225;
			BaseSoundID = 0xE5;

			SetStr( 56, 80 );
			SetDex( 56, 75 );
			SetInt( 31, 55 );

			SetHits( 34, 48 );
			SetMana( 0 );

			SetDamage( 3, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Poison, 10, 15 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 20.1, 35.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 53.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 3 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public MadDog(Serial serial) : base(serial)
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