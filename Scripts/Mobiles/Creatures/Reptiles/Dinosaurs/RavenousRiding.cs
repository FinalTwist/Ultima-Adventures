using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a dinosaur corpse" )]
	public class RavenousRiding : BaseMount
	{
		[Constructable]
		public RavenousRiding() : this( "a ravenous" )
		{
		}

		[Constructable]
		public RavenousRiding( string name ) : base( name, 218, 16036, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			BaseSoundID = 0x5A;
			Hue = 0x84E;
Name = "a greater ravenous";
			SetStr( 166, 290 );
			SetDex( 96, 215 );
			SetInt( 51, 60 );

			SetHits( 116, 330 );
			SetMana( 0 );

			SetDamage( 12, 39 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 55 );
			SetResistance( ResistanceType.Fire, 30, 55 );
			SetResistance( ResistanceType.Poison, 25, 45 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 4500;
			Karma = -3500;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 97.7;
		}
		else
		{
			BaseSoundID = 0x5A;
			Hue = 0x84E;

			SetStr( 166, 190 );
			SetDex( 96, 115 );
			SetInt( 51, 60 );

			SetHits( 116, 130 );
			SetMana( 0 );

			SetDamage( 12, 30 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 45 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 90.7;
		}
	}

		public override int GetAttackSound(){ return 0x622; }	// A
		public override int GetDeathSound(){ return 0x623; }	// D
		public override int GetHurtSound(){ return 0x624; }		// H

		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Meat{ get{ return 4; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }

		public RavenousRiding(Serial serial) : base(serial)
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
			if ( Body == 116 ) { ItemID = Server.Mobiles.BaseMount.MountBody(116); }
			else if ( Body == 117 ) { ItemID = Server.Mobiles.BaseMount.MountBody(117); }
			else if ( Body == 219 ) { ItemID = Server.Mobiles.BaseMount.MountBody(219); }
			else { ItemID = 16036; }
		}
	}
}
