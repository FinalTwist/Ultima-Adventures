using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "an ostard corpse" )]
	public class FrenziedOstard : BaseMount
	{
		[Constructable]
		public FrenziedOstard() : this( "a frenzied ostard" )
		{
		}

		[Constructable]
		public FrenziedOstard( string name ) : base( name, 0xDB, 0x3EA3, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			
			if (Utility.RandomDouble() > 0.85)
				{
			Name = "a greater frenzied ostard";
			Hue = Utility.RandomHairHue() | 0x8000;

			BaseSoundID = 0x275;

			SetStr( 185, 305 );
			SetDex( 105, 445 );
			SetInt( 6, 100 );

			SetHits( 200, 350 );
			SetMana( 0 );

			SetDamage( 23, 30 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 80 );
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
			MinTameSkill = 90.1;
				}
		
		else
							{
			Name = "a frenzied ostard";
			Hue = Utility.RandomHairHue() | 0x8000;

			BaseSoundID = 0x275;

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
		}
		


		public override int Meat{ get{ return 3; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.FruitsAndVegies; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Ostard; } }

		public FrenziedOstard( Serial serial ) : base( serial )
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
		}
	}
}