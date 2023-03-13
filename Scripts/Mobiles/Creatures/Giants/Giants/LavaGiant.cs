using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a giant corpse" )]
	public class LavaGiant : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 100; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

		[Constructable]
		public LavaGiant() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "giant" );
			Title = "of the molten core";
			Body = 485;
			Hue = 0xAFA;
			BaseSoundID = 609;

			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );

			SetHits( 478, 495 );

			SetDamage( 16, 22 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 60 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 5, 15 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.MagicResist, 80.2, 110.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 40.1, 50.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 40;

			AddItem( new LighterSource() );
		}

		public override int GetAttackSound(){ return 0x5F8; }	// A
		public override int GetDeathSound(){ return 0x5F9; }	// D
		public override int GetHurtSound(){ return 0x5FA; }		// H

		public override void OnDeath( Container c )
		{
			Region reg = Region.Find( this.Location, this.Map );

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
						MyChest.Name = "molten chest";
						MyChest.Hue = 0x496;
						c.DropItem( MyChest );
					}
				}
			}

			base.OnDeath( c );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override int Meat{ get{ return 4; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ if ( Utility.RandomBool() ){ return HideType.Volcanic; } else { return HideType.Goliath; } } }

		public LavaGiant( Serial serial ) : base( serial )
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