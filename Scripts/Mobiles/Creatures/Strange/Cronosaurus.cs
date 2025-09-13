using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a sea creature corpse" )]
	public class Cronosaurus : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 10 ); }

		[Constructable]
		public Cronosaurus() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a cronosaurus";
			Body = 166;
			BaseSoundID = 362;

			SetStr( 656, 680 );
			SetDex( 226, 245 );
			SetInt( 26, 40 );

			SetHits( 454, 468 );
			SetMana( 0 );

			SetDamage( 15, 29 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 50;

			CanSwim = true;
			CantWalk = true;

			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: PackItem( new SeaweedLegs() ); break;
					case 1: PackItem( new SeaweedGloves() ); break;
					case 2: PackItem( new SeaweedGorget() ); break;
					case 3: PackItem( new SeaweedArms() ); break;
					case 4: PackItem( new SeaweedChest() ); break;
					case 5: PackItem( new SeaweedHelm() ); break;
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Blue ); } }
		public override int Meat{ get{ return 19; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }

		public Cronosaurus( Serial serial ) : base( serial )
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
