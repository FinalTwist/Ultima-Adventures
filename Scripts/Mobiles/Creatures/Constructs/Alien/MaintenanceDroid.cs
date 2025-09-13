using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a broken machine" )]
	public class MaintenanceDroid : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 30; } }
		public override int BreathFireDamage{ get{ return 30; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 40; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathDamageScalar{ get{ return 0.35; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSound{ get{ return 0x54A; } }
		public override int BreathEffectItemID{ get{ return 0x28EF; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 0 ); }

		[Constructable]
		public MaintenanceDroid() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = Server.Misc.RandomThings.GetRandomRobot(1);
			Title = "the maintenance droid";
			Body = 756;
			BaseSoundID = 1368;
			Hue = Utility.RandomList( 0xB70, 0xB65, 0xB54, 0xB04, 0xB05, 0xB06, 0xB07, 0xB08, 0xB09, 0xB17, 0xB5C, 0xAFA, 0xAFB, 0xB57 );

			SetStr( 261, 350 );
			SetDex( 76, 95 );
			SetInt( 61, 90 );

			SetHits( 131, 190 );

			SetDamage( 9, 14 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 60.2, 78.0 );
			SetSkill( SkillName.Tactics, 60.2, 78.0 );
			SetSkill( SkillName.Wrestling, 60.2, 78.0 );

			Fame = 8000;
			Karma = -8000;
			VirtualArmor = 30;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return true; } }
		public override bool Unprovokable { get { return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public MaintenanceDroid( Serial serial ) : base( serial )
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