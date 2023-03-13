using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a horse corpse" )]
	[TypeAlias( "Server.Mobiles.BrownHorse", "Server.Mobiles.DirtyHorse", "Server.Mobiles.GrayHorse", "Server.Mobiles.TanHorse" )]
	public class Horse : BaseMount
	{
		private static int[] m_IDs = new int[]
			{
				0xC8, 0x3E9F,
				0xE2, 0x3EA0,
				0xE4, 0x3EA1,
				0xE2, 0x3EA0
			};

		[Constructable]
		public Horse() : this( "a horse" )
		{
		}

		[Constructable]
		public Horse( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.95)
				
					{
			int random = Utility.Random( 4 );
Name = "a greater horse";
			Body = m_IDs[random * 2];
			ItemID = m_IDs[random * 2 + 1];
			Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );
			BaseSoundID = 0xA8;

			SetStr( 22, 168 );
			SetDex( 56, 125 );
			SetInt( 6, 10 );

			SetHits( 28, 95 );
			SetMana( 0 );

			SetDamage( 3, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 35 );

			SetSkill( SkillName.MagicResist, 25.1, 30.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 600;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 49.1;

			if ( Utility.RandomMinMax( 0, 100 ) == 1 )
			{
				MinTameSkill = 49.1;
				Fame = 500;
				Hue = 0x9C2;
				if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ Hue = 0x497; }
			}
		}
		else
		{
			int random = Utility.Random( 4 );

			Body = m_IDs[random * 2];
			ItemID = m_IDs[random * 2 + 1];
			Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );
			BaseSoundID = 0xA8;

			SetStr( 22, 98 );
			SetDex( 56, 75 );
			SetInt( 6, 10 );

			SetHits( 28, 45 );
			SetMana( 0 );

			SetDamage( 3, 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );

			SetSkill( SkillName.MagicResist, 25.1, 30.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 29.1;

			if ( Utility.RandomMinMax( 0, 100 ) == 1 )
			{
				MinTameSkill = 49.1;
				Fame = 500;
				Hue = 0x9C2;
				if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ Hue = 0x497; }
			}
		}
		}

		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 5 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public override bool OnBeforeDeath()
		{
			Server.Items.HorseArmor.DropArmor( this );
			return base.OnBeforeDeath();
		}

		public Horse( Serial serial ) : base( serial )
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

			if ( !Server.Misc.MyServerSettings.ClientVersion() && Body == 587 && ItemID == 587 )
			{
				Body = 0xE2;
				ItemID = 0x3EA0;
			}
		}
	}
}