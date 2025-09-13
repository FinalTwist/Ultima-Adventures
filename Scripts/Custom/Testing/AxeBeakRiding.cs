using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an axebeak corpse" )]
	public class AxeBeakRiding : BaseMount
	{
		[Constructable]
		public AxeBeakRiding() : this( "an axebeak" )
		{
		}

		[Constructable]
		public AxeBeakRiding( string name ) : base( name, 25, 25, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0x8F;

			SetStr( 96, 120 );
			SetDex( 86, 110 );
			SetInt( 51, 75 );

			SetHits( 58, 72 );

			SetDamage( 5, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 50.1, 65.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 90.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 28;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 39.1;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 5 ) );
				PackItem( egg );
			}
		}

		public override void OnAfterSpawn()
		{
			Region reg = Region.Find( this.Location, this.Map );

			if ( reg.IsPartOf( "Dungeon Covetous" ) )
			{
				AI = AIType.AI_Melee;
				FightMode = FightMode.Closest;
				Tamable = false;
				NameHue = 0x22;
			}

			base.OnAfterSpawn();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Server.Misc.MyServerSettings.ClientVersion() )
				base.OnDoubleClick( from );
		}

		public override int Meat{ get{ return 4; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 50; } }
		public override int Hides{ get{ return 5; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }
		public override HideType HideType{ get{ return HideType.Dinosaur; } }

		public AxeBeakRiding(Serial serial) : base(serial)
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