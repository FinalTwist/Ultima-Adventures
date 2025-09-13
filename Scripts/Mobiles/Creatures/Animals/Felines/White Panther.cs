using System;
using Server.Items;
using Server.Mobiles;
using Server;

namespace Server.Mobiles
{
	[CorpseName( "a feline corpse" )]
	public class WhitePanther : BaseCreature
	{
		public override bool CanChew { get{return true;}}
			public override int BreathPhysicalDamage{ get{ return 20; } }
			public override int BreathFireDamage{ get{ return 20; } }
			public override int BreathColdDamage{ get{ return 20; } }
			public override int BreathPoisonDamage{ get{ return 20; } }
			public override int BreathEnergyDamage{ get{ return 20; } }
			public override int BreathEffectHue{ get{ return 0xB71; } }
			public override int BreathEffectSound{ get{ return 0x655; } }
			public override int BreathEffectItemID{ get{ return 0x3039; } }
			public override bool ReacquireOnMovement{ get{ return !Controlled; } }
			public override bool HasBreath{ get{ return true; } }
			public override double BreathEffectDelay{ get{ return 0.3; } }
			public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 25 ); }

		[Constructable]
		public WhitePanther() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{

				Name = "a White Panther";
				Body = 885;
				BaseSoundID = 0x3EE;
				Hue = 0x47E;

				SetStr( 200, 270 );
				SetDex( 320, 490 );
				SetInt( 50, 76 );

				SetHits( 264, 388 );
				SetMana( 0 );

				SetDamage( 28, 36 );

				SetDamageType( ResistanceType.Physical, 50 );
				SetDamageType( ResistanceType.Energy, 50 );

				SetResistance( ResistanceType.Physical, 50, 70 );
				SetResistance( ResistanceType.Fire, 25, 40 );
				SetResistance( ResistanceType.Cold, 35, 65 );
				SetResistance( ResistanceType.Poison, 20, 30 );

				SetSkill( SkillName.MagicResist, 65.1, 80.0 );
				SetSkill( SkillName.Tactics, 65.1, 90.0 );
				SetSkill( SkillName.Wrestling, 65.1, 80.0 );

				Fame = 8000;
				Karma = 0;

				VirtualArmor = 40;

				Tamable = true;
				ControlSlots = 2;
				MinTameSkill = 91.1;
				AIFullSpeedActive = true; // Force full speed
				
				ActiveSpeed = 0;
				PassiveSpeed = 0;

		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 10; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 5 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public WhitePanther(Serial serial) : base(serial)
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
			AIFullSpeedActive = true;
		}
	}
}