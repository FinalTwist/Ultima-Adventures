using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a chicken lizard corpse" )]
	public class ChickenLizard : BaseCreature
	{
		[Constructable]
		public ChickenLizard() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a chicken lizard";
			Body = 716;

			SetStr( 78, 87 );
			SetDex( 87, 92 );
			SetInt( 8 );

			SetHits( 77, 82 );

			SetDamage( 2, 5 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 18, 20 );
			SetResistance( ResistanceType.Fire, 7, 14 );

			SetSkill( SkillName.MagicResist, 0.0, 28.5 );
			SetSkill( SkillName.Tactics, 0.0, 41.3 );
			SetSkill( SkillName.Wrestling, 0.0, 35.8 );

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 0.0;
		}

		public override int Meat{ get{ return 3; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public override int GetIdleSound() { return 1511; } 
		public override int GetAngerSound() { return 1508; } 
		public override int GetHurtSound() { return 1510; } 
		public override int GetDeathSound()	{ return 1509; }

		public ChickenLizard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}