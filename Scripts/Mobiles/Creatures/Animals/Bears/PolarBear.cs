using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class PolarBear : BaseMount
	{
		public override bool CanChew { get{return true;}}
		[Constructable]
		public PolarBear() : this( "a polar bear" )
		{
		}

		[Constructable]
		public PolarBear( string name ) : base( name, 213, 16069, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)		
		{
			BaseSoundID = 0xA3;
Name = "a greater polar bear";
			SetStr( 116, 240 );
			SetDex( 81, 205 );
			SetInt( 26, 50 );

			SetHits( 70,154 );
			SetMana( 0 );

			SetDamage( 7, 18 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 45 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 60.1, 90.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );

			Fame = 1900;
			Karma = 0;

			VirtualArmor = 18;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 55.1;
		}		
		else
		
		{
			BaseSoundID = 0xA3;

			SetStr( 116, 140 );
			SetDex( 81, 105 );
			SetInt( 26, 50 );

			SetHits( 70, 84 );
			SetMana( 0 );

			SetDamage( 7, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 60.1, 90.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );

			Fame = 1500;
			Karma = 0;

			VirtualArmor = 18;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 35.1;
		}
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override HideType HideType{ get{ return HideType.Frozen; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.White; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }

		public PolarBear( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Hue = 0;
		}
	}
}