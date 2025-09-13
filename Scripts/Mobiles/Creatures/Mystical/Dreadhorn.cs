using System;
using Server.Mobiles;
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dreadhorn corpse" )]
	public class Dreadhorn : BaseMount
	{
		public override int BreathPhysicalDamage{ get{ return 20; } }
		public override int BreathFireDamage{ get{ return 20; } }
		public override int BreathColdDamage{ get{ return 20; } }
		public override int BreathPoisonDamage{ get{ return 20; } }
		public override int BreathEnergyDamage{ get{ return 20; } }
		public override int BreathEffectHue{ get{ return 0x496; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override int BreathEffectItemID{ get{ return 0x37BC; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 26 ); }

		[Constructable]
		public Dreadhorn() : this( "a dreadhorn" )
		{
		}

		[Constructable]
		public Dreadhorn( string name ) : base( name, 354, MountBody(354), AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0x4BC;

			SetStr( 496, 525 );
			SetDex( 86, 105 );
			SetInt( 186, 225 );

			SetHits( 298, 315 );

			SetDamage( 16, 22 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 40 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 10.4, 50.0 );
			SetSkill( SkillName.Magery, 10.4, 50.0 );
			SetSkill( SkillName.MagicResist, 85.3, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 80.5, 92.5 );

			Fame = 14000;
			Karma = -14000;

			VirtualArmor = 60;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 95.1;

			PackItem( new BlackPearl( Utility.RandomMinMax( 3, 5 ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Potions );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 5 ); } }
		public override FurType FurType{ get{ return FurType.White; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public Dreadhorn( Serial serial ) : base( serial )
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

			ItemID = MountBody(354);
		}
	}
}
