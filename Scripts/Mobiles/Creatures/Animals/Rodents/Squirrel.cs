using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a squirrel corpse" )]	
	public class Squirrel : BaseCreature
	{
		[Constructable]
		public Squirrel() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a squirrel";
			Body = 0x116;
			Hue = Utility.RandomList( 0, 0, 0x966 );

			SetStr( 44, 50 );
			SetDex( 35 );
			SetInt( 5 );

			SetHits( 42, 50 );

			SetDamage( 1, 2 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 34 );
			SetResistance( ResistanceType.Fire, 10, 14 );
			SetResistance( ResistanceType.Cold, 30, 35 );
			SetResistance( ResistanceType.Poison, 20, 25 );
			SetResistance( ResistanceType.Energy, 20, 25 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 4.0 );
			SetSkill( SkillName.Wrestling, 4.0 );

			Tamable = true;	
			ControlSlots = 1;
			MinTameSkill = -21.3;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item acorn = new Acorn( Utility.RandomMinMax( 1, 3 ) );
				PackItem( acorn );
			}
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 1; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 1 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }

		public Squirrel( Serial serial ) : base( serial )
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
