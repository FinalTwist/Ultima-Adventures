using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a tiger corpse" )]
	public class SabretoothTigerRiding : BaseMount
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public SabretoothTigerRiding() : this( "a sabretooth tiger" )
		{
		}

		[Constructable]
		public SabretoothTigerRiding( string name ) : base( name, 340, 340, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0x462;
			Hue = 0x54F;

			SetStr( 400 );
			SetDex( 300 );
			SetInt( 120 );

			SetMana( 0 );

			SetDamage( 25, 35 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 3000;
			Karma = 0;

			VirtualArmor = 50;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 90.1;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Server.Misc.MyServerSettings.ClientVersion() )
				base.OnDoubleClick( from );
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public SabretoothTigerRiding( Serial serial ) : base( serial )
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
		}
	}
}