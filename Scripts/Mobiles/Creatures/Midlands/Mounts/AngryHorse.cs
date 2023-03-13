using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a horse corpse" )]
	public class AngryHorse : BaseMount
	{
		private static int[] m_IDs = new int[]
			{
				0xC8, 0x3E9F,
				0xE2, 0x3EA0,
				0xE4, 0x3EA1,
				0xE2, 0x3EA0
			};

		[Constructable]
		public AngryHorse() : this( "an angry horse" )
		{
		}

		[Constructable]
		public AngryHorse( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			int random = Utility.Random( 4 );

			Body = m_IDs[random * 2];
			ItemID = m_IDs[random * 2 + 1];
			Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );
			BaseSoundID = 0xA8;

			SetStr( 105, 255 );
			SetDex( 105, 345 );
			SetInt( 6, 100 );

			SetHits( 100, 250 );
			SetMana( 0 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 70 );
			SetResistance( ResistanceType.Fire, 10, 55 );
			SetResistance( ResistanceType.Poison, 20, 55 );
			SetResistance( ResistanceType.Energy, 20, 55 );

			SetSkill( SkillName.MagicResist, 75.1, 80.0 );
			SetSkill( SkillName.Tactics, 79.3, 94.0 );
			SetSkill( SkillName.Wrestling, 79.3, 94.0 );

			Fame = 1500;
			Karma = -1500;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 85.1;
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

		public AngryHorse( Serial serial ) : base( serial )
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