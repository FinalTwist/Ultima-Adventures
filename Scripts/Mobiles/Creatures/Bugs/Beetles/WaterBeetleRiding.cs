using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a beetle corpse" )]
	public class WaterBeetleRiding : BaseMount
	{
		[Constructable]
		public WaterBeetleRiding() : this( "a water beetle" )
		{
		}

		[Constructable]
		public WaterBeetleRiding( string name ) : base( name, 0xA9, 0x3E95, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.25, 0.5 )
		{
			Hue = 0xB75;

			SetStr( 96, 120 );
			SetDex( 86, 105 );
			SetInt( 6, 10 );

			CanSwim = true;

			SetHits( 80, 110 );

			SetDamage( 3, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 75.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 39.1;
		}

		public override bool BleedImmune{ get{ return true; } }

		public override int GetAngerSound()
		{
			return 0x21D;
		}

		public override int GetIdleSound()
		{
			return 0x21D;
		}

		public override int GetAttackSound()
		{
			return 0x162;
		}

		public override int GetHurtSound()
		{
			return 0x163;
		}

		public override int GetDeathSound()
		{
			return 0x21D;
		}

		public WaterBeetleRiding( Serial serial ) : base( serial )
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
			Hue = 0xB75;
		}
	}
}