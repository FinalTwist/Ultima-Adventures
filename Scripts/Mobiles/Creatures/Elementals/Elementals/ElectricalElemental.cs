using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an elemental corpse" )]
	public class ElectricalElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 21 ); }

		[Constructable]
		public ElectricalElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an electrical elemental";
			Body = 199;
			BaseSoundID = 838;

			SetStr( 126, 155 );
			SetDex( 166, 185 );
			SetInt( 101, 125 );

			SetHits( 76, 93 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Energy, 75 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Energy, 60, 80 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Fire, 30, 40 );

			SetSkill( SkillName.EvalInt, 80.1, 105.0 );
			SetSkill( SkillName.Magery, 80.1, 105.0 );
			SetSkill( SkillName.MagicResist, 75.2, 105.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 5500;
			Karma = -5500;

			VirtualArmor = 45;

			AddItem( new LighterSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public ElectricalElemental( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 274 )
				BaseSoundID = 838;
		}
	}
}
