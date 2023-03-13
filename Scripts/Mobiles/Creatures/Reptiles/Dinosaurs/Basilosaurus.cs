using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dinosaur corpse" )]
	public class Basilosaurus : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 100; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 4 ); }

		[Constructable]
		public Basilosaurus() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater basilosaurus";
			Body = 150;
			BaseSoundID = 447;

			SetStr( 400, 600 );
			SetDex( 86, 205 );
			SetInt( 20, 40 );

			SetHits( 278, 395 );

			SetDamage( 12, 25 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );

			SetResistance( ResistanceType.Physical, 55, 75 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 50 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );

			Fame = 7700;
			Karma = -7000;

			VirtualArmor = 30;
			CanSwim = true;
			CantWalk = true;
		}
		else
		{
			Name = "a basilosaurus";
			Body = 150;
			BaseSoundID = 447;

			SetStr( 400, 500 );
			SetDex( 86, 105 );
			SetInt( 20, 40 );

			SetHits( 278, 295 );

			SetDamage( 12, 18 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 30;
			CanSwim = true;
			CantWalk = true;
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int Meat{ get{ return 10; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Scales{ get{ return 8; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }

		public Basilosaurus( Serial serial ) : base( serial )
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