using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class ElderBrownBearRiding : BaseMount
	{
		public override bool CanChew { get{return true;}}
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public ElderBrownBearRiding() : this( "an elder brown bear" )
		{
		}

		[Constructable]
		public ElderBrownBearRiding( string name ) : base( name, 34, MountBody(34), AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			BaseSoundID = 0xA3;
			Name = "a greater elder brown bear";
			SetStr( 180, 355 );
			SetDex( 101, 245 );
			SetInt( 16, 40 );

			SetHits( 120, 293 );
			SetMana( 0 );

			SetDamage( 14, 29 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 65 );
			SetResistance( ResistanceType.Cold, 35, 55 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );

			Fame = 2500;
			Karma = 0;

			VirtualArmor = 35;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 79.1;
		}
		else
		{
			BaseSoundID = 0xA3;

			SetStr( 180, 255 );
			SetDex( 101, 145 );
			SetInt( 16, 40 );

			SetHits( 120, 193 );
			SetMana( 0 );

			SetDamage( 14, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );

			Fame = 1500;
			Karma = 0;

			VirtualArmor = 35;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 69.1;
		}
		}

		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 18; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }

		public ElderBrownBearRiding( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			ItemID = MountBody(34);
		}
	}
}
