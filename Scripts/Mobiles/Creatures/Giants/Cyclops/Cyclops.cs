using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a cyclops corpse" )]
	public class Cyclops : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return CyclopsEye( this.Hue, 1 ); } }
		public override int BreathColdDamage{ get{ return CyclopsEye( this.Hue, 2 ); } }
		public override int BreathPoisonDamage{ get{ return CyclopsEye( this.Hue, 3 ); } }
		public override int BreathEnergyDamage{ get{ return CyclopsEye( this.Hue, 4 ); } }
		public override int BreathEffectHue{ get{ return CyclopsEye( this.Hue, 5 ); } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return EyePower( this.Hue ); } }
		public override int BreathEffectSound{ get{ return CyclopsEye( this.Hue, 7 ); } }
		public override int BreathEffectItemID{ get{ return CyclopsEye( this.Hue, 6 ); } }

		public static bool EyePower( int cyclops ){ if ( cyclops == 0xA4B || cyclops == 0xA9A || cyclops == 0x9E1 || cyclops == 0x9C4 ){ return true; } return false; }

		public static int CyclopsEye ( int cyclops, int value )
		{
			if ( value == 1 && cyclops == 0xA4B ){ return 100; } // RETURN THE FIRE DAMAGE
			if ( value == 2 && cyclops == 0xA9A ){ return 100; } // RETURN THE COLD DAMAGE
			if ( value == 3 && cyclops == 0x9E1 ){ return 100; } // RETURN THE POISON DAMAGE
			if ( value == 4 && cyclops == 0x9C4 ){ return 100; } // RETURN THE ENERGY DAMAGE

			if ( value == 5 && cyclops == 0xA4B ){ return 0; } // RETURN THE FIRE HUE
			if ( value == 5 && cyclops == 0xA9A ){ return 0x481; } // RETURN THE COLD HUE
			if ( value == 5 && cyclops == 0x9E1 ){ return 0x3F; } // RETURN THE POISON HUE
			if ( value == 5 && cyclops == 0x9C4 ){ return 0x9C2; } // RETURN THE ENERGY HUE

			if ( value == 6 && cyclops == 0xA4B ){ return 0x36D4; } // RETURN THE FIRE ID
			if ( value == 6 && cyclops == 0xA9A ){ return 0x36D4; } // RETURN THE COLD ID
			if ( value == 6 && cyclops == 0x9E1 ){ return 0x36D4; } // RETURN THE POISON ID
			if ( value == 6 && cyclops == 0x9C4 ){ return 0x3818; } // RETURN THE ENERGY ID

			if ( value == 7 && cyclops == 0xA4B ){ return 0x227; } // RETURN THE FIRE SOUND
			if ( value == 7 && cyclops == 0xA9A ){ return 0x64F; } // RETURN THE COLD SOUND
			if ( value == 7 && cyclops == 0x9E1 ){ return 0x658; } // RETURN THE POISON SOUND
			if ( value == 7 && cyclops == 0x9C4 ){ return 0x665; } // RETURN THE ENERGY SOUND

			return 0;
		}

		public override void BreathDealDamage( Mobile target, int form )
		{
			if ( this.Hue == 0xA4B ){ form = 17; } // RETURN THE FIRE DAMAGE
			if ( this.Hue == 0xA9A ){ form = 19; } // RETURN THE COLD DAMAGE
			if ( this.Hue == 0x9E1 ){ form = 18; } // RETURN THE POISON DAMAGE
			if ( this.Hue == 0x9C4 ){ form = 20; } // RETURN THE ENERGY DAMAGE

			base.BreathDealDamage( target, form );
		}

		[Constructable]
		public Cyclops() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "greek" );
			Title = "the cyclops";
			Body = Utility.RandomList( 75, 475 );
			BaseSoundID = 604;

			switch ( Utility.RandomMinMax( 1, 6 ) )
			{
				case 1: Hue = 0xA4B; break; // FIRE
				case 2: Hue = 0xA9A; break; // COLD
				case 3: Hue = 0x9E1; break; // POISON
				case 4: Hue = 0x9C4; break; // ENERGY
			}

			SetStr( 336, 385 );
			SetDex( 96, 115 );
			SetInt( 31, 55 );

			SetHits( 202, 231 );
			SetMana( 0 );

			SetDamage( 7, 23 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.3, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 48;
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
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && this.Body == 75 && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						BaseWeapon hammer = new WarHammer();
						hammer.AccuracyLevel = WeaponAccuracyLevel.Supremely;
						hammer.MinDamage = hammer.MinDamage + 6;
						hammer.MaxDamage = hammer.MaxDamage + 10;
            			hammer.DurabilityLevel = WeaponDurabilityLevel.Indestructible;
						hammer.Name = "cyclops hammer";
						c.DropItem( hammer );
					}
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override int Meat{ get{ return 4; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Goliath; } }

		public Cyclops( Serial serial ) : base( serial )
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