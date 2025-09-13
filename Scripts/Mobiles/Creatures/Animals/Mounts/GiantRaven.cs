using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bird corpse" )]
	public class GiantRaven : BaseMount
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public GiantRaven() : this( "a giant raven" )
		{
		}

		[Constructable]
		public GiantRaven( string name ) : base( name, 243, 0x3E94, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Hue = 0x497;
			BaseSoundID = 0x2EE;
Name = "a greater giant raven";
			SetStr( 126, 255 );
			SetDex( 81, 185 );
			SetInt( 16, 40 );

			SetHits( 76, 193 );
			SetMana( 0 );

			SetDamage( 8, 13 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 55 );
			SetResistance( ResistanceType.Cold, 15, 35 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );

			Fame = 1700;
			Karma = -500;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 79.1;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 5 ) );
				PackItem( egg );
			}
		}
else

		{
			Hue = 0x497;
			BaseSoundID = 0x2EE;

			SetStr( 126, 155 );
			SetDex( 81, 105 );
			SetInt( 16, 40 );

			SetHits( 76, 93 );
			SetMana( 0 );

			SetDamage( 8, 13 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );

			Fame = 1000;
			Karma = -500;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 59.1;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 5 ) );
				PackItem( egg );
			}
		}
	}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			// THERE IS A SPOT IN LODOR DESERT WHERE I WANT VULTURES NEAR THE CORPSE OF AN OGRE LORD
			if ( this.Map == Map.Felucca && this.Home.X >= 1419 && this.Home.X <= 1432 && this.Home.Y >= 3110 && this.Home.Y <= 3119 )
			{
				this.Hue = 0xA77;
				this.Name = "a giant vulture";
			}
		}

		public override int TreasureMapLevel { get { return 5; } }
		public override int Meat { get { return 5; } }
		public override FoodType FavoriteFood { get { return FoodType.Meat; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 50; } }

		public GiantRaven( Serial serial ): base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
