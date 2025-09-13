using System;
using Server;
using Server.Engines.Quests;

namespace Server.Mobiles
{
	[CorpseName( "a weasel corpse" )]
	public class Weasel : BaseCreature
	{
		[Constructable]
		public Weasel() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a weasel";
			Body = 0x117;
			Hue = 1705;

			SetStr( 41, 48 );
			SetDex( 55 );
			SetInt( 75 );

			SetHits( 45, 50 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 10, 14 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 21, 25 );
			SetResistance( ResistanceType.Energy, 20, 25 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 4.0 );
			SetSkill( SkillName.Wrestling, 4.0 );

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -21.3;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 1; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 1 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }

		public Weasel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
