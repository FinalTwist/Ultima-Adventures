using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a chicken corpse" )]
	public class Chicken : BaseCreature
	{
		[Constructable]
		public Chicken() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			int megachicken = Utility.Random( 200 );
			if ( megachicken == 75 )
			{
			Name = "a strong chicken";
			Body = 0xD0;
			BaseSoundID = 0x6E;

			SetStr( 5, 10 );
			SetDex( 70, 100 );
			SetInt( 20 );

			SetHits( 200, 500 );
			SetMana( 0 );

			SetDamage( 1, 3 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 40 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 80;

			Tamable = true;
			ControlSlots = 0;
			MinTameSkill = 85;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 3 ) );
				PackItem( egg );
			}
			}
			else 
			{
			Name = "a chicken";
			Body = 0xD0;
			BaseSoundID = 0x6E;

			SetStr( 5 );
			SetDex( 15 );
			SetInt( 5 );

			SetHits( 3 );
			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 1, 5 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 2;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -0.9;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 3 ) );
				PackItem( egg );
			}
			}

		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }

		public override int Feathers{ get{ return 25; } }

		public Chicken(Serial serial) : base(serial)
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
		}
	}
}