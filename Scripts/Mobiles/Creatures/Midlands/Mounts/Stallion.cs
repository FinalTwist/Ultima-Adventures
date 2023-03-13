using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a stallion corpse" )]
	public class Stallion : BaseMount
	{
		private static int[] m_IDs = new int[]
			{
				0xC8, 0x3E9F,
				0xE2, 0x3EA0,
				0xE4, 0x3EA1,
				0xE2, 0x3EA0
			};

		[Constructable]
		public Stallion() : this( "a stallion" )
		{
		}

		[Constructable]
		public Stallion( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			int random = Utility.Random( 4 );

			Body = m_IDs[random * 2];
			ItemID = m_IDs[random * 2 + 1];
			Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );
			BaseSoundID = 0xA8;

			SetStr( 377, 418 );
			SetDex( 87, 103 );
			SetInt( 25, 30 );

			SetHits( 475, 566 );
			SetStam( 87, 103 );
			SetMana( 0 );

			SetDamage( 20, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 67.1, 74.5 );
			SetSkill( SkillName.Anatomy, 96.5, 104.0 );
			SetSkill( SkillName.Tactics, 95.8, 102.6 );
			SetSkill( SkillName.Wrestling, 100.5, 111.4 );

			Fame = 5000;
			Karma = 0;

			VirtualArmor = 32;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 67.1;
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

		public Stallion( Serial serial ) : base( serial )
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