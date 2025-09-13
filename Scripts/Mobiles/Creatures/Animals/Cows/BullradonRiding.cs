using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bullradon corpse" )]
	public class BullradonRiding : BaseMount
	{
		[Constructable]
		public BullradonRiding() : this( "a bullradon" )
		{
		}

		[Constructable]
		public BullradonRiding( string name ) : base( name, 0x11C, 0x3E92, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SetStr( 146, 175 );
			SetDex( 111, 150 );
			SetInt( 46, 60 );

			SetHits( 131, 160 );
			SetMana( 0 );

			SetDamage( 6, 11 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Cold, 30, 50 );
			SetResistance( ResistanceType.Poison, 40, 60 );
			SetResistance( ResistanceType.Energy, 30, 50 );

			SetSkill( SkillName.MagicResist, 37.6, 42.5 );
			SetSkill( SkillName.Tactics, 70.6, 83.0 );
			SetSkill( SkillName.Wrestling, 50.1, 57.5 );

			Fame = 2000;
			Karma = -2000;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 63.9;
		}

		public override int GetAngerSound()
		{
			return 0x4F8;
		}

		public override int GetIdleSound()
		{
			return 0x4F7;
		}

		public override int GetAttackSound()
		{
			return 0x4F6;
		}

		public override int GetHurtSound()
		{
			return 0x4F9;
		}

		public override int GetDeathSound()
		{
			return 0x4F5;
		}

		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 15; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }

		public BullradonRiding( Serial serial ) : base( serial )
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
