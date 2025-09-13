using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a giant nasty boar corpse" )] // stupid corpse name
	public class NastyBoar : BaseCreature
	{
		[Constructable]
		public NastyBoar() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a nasty boar";
			Body =  0x122;
                                                Hue = 1108;
			BaseSoundID = 0xC4; // TODO: validate

			SetStr( 110, 115 );
			SetDex( 100, 105 );
			SetInt( 95, 100 );

			SetHits( 500 );

			SetDamage( 30, 40 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 90, 100 );
			SetResistance( ResistanceType.Poison, 100, 110 );
			SetResistance( ResistanceType.Energy, 100, 120 );

			SetSkill( SkillName.Anatomy, 80.3, 85.0 );
			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 105.1, 110.0 );
			SetSkill( SkillName.Tactics, 85.1, 90.0 );
			SetSkill( SkillName.Wrestling, 90.1, 95.0 );

			
                                                  Fame = 10000;
			Karma = 0;
		
			VirtualArmor = 40;

			
			PackGold( 100, 200 );
			PackItem( new TastyBoarMeat() );
			
			
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }

		public  NastyBoar( Serial serial ) : base( serial )
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
		}
	}
}