using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a titans corpse" )]
	public class ElderTitan : BaseCreature
	{
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
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 14 ); }

		[Constructable]
		public ElderTitan() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "greek" );
			Title = "the elder titan";
			Body = 189;
			Hue = 0xA4B;
			BaseSoundID = 609;

			SetStr( 736, 785 );
			SetDex( 226, 245 );
			SetInt( 381, 405 );

			SetHits( 422, 451 );

			SetDamage( 15, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 105.1, 120.0 );
			SetSkill( SkillName.Magery, 105.1, 120.0 );
			SetSkill( SkillName.MagicResist, 100.2, 125.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 45;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						MyChest.Name = "ancient titan chest";
						MyChest.Hue = 0xB92;
						c.DropItem( MyChest );
					}
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int Meat{ get{ return 4; } }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 6; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Goliath; } }

		public ElderTitan( Serial serial ) : base( serial )
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