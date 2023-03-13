using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class PandaRiding : BaseMount
	{
		[Constructable]
		public PandaRiding() : this( "a panda" )
		{
		}

		[Constructable]
		public PandaRiding( string name ) : base( name, 671, 671, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0xA3;

			SetStr( 76, 100 );
			SetDex( 26, 45 );
			SetInt( 23, 47 );

			SetHits( 46, 60 );
			SetMana( 0 );

			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 30 );
			SetResistance( ResistanceType.Cold, 15, 20 );
			SetResistance( ResistanceType.Poison, 10, 15 );

			SetSkill( SkillName.MagicResist, 25.1, 35.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 41.1;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Server.Misc.MyServerSettings.ClientVersion() )
				base.OnDoubleClick( from );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 6 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }

		public PandaRiding( Serial serial ) : base( serial )
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