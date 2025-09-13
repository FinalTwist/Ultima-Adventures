using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bird corpse" )]
	public class Bird : BaseCreature
	{
		[Constructable]
		public Bird() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Hue = Utility.RandomBirdHue();
			Name = NameList.RandomName( "bird" );
			Body = 6;
			BaseSoundID = 0x1B;

			VirtualArmor = Utility.RandomMinMax( 0, 6 );

			SetStr( 10 );
			SetDex( 25, 35 );
			SetInt( 10 );

			SetDamage( 0 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Wrestling, 4.2, 6.4 );
			SetSkill( SkillName.Tactics, 4.0, 6.0 );
			SetSkill( SkillName.MagicResist, 4.0, 5.0 );

			Fame = 150;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -6.9;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( 1 );
				PackItem( egg );
			}
		}

		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Meat{ get{ return 1; } }
		public override int Feathers{ get{ return 25; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public override void OnAfterSpawn()
		{
			if ( Server.Misc.Worlds.TestOcean ( this.Map, this.X, this.Y, 15 ) )
			{
				this.Hue = 0xB78;
				this.Name =	"a seagull";
			}
			else if ( Utility.RandomMinMax( 1, 10 ) == 1 )
			{
				this.Hue = 0x901;
				switch ( Utility.Random( 2 ) )
				{
					case 0: this.Name = "a crow"; break;
					case 1: this.Name = "a raven"; break;
				}
			}
			else if ( Utility.RandomMinMax( 1, 10 ) == 1 )
			{
				this.Hue = 0xBB4;
				this.Name = "a dove";
			}
			if ( Utility.RandomMinMax( 1, 100 ) == 1 )
			{
				this.Hue = 0xB7A;
				this.Name = "a carrier pigeon";
				PackItem( new SomeRandomNote() );
			}

			base.OnAfterSpawn();
		}

		public Bird( Serial serial ) : base( serial )
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

			if ( Hue == 0 )
				Hue = Utility.RandomBirdHue();
		} 
	}
}