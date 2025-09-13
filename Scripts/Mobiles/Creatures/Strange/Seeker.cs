using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a seeker corpse" )]
	public class Seeker : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return BeholderEye( this.VirtualArmor, 1 ); } }
		public override int BreathColdDamage{ get{ return BeholderEye( this.VirtualArmor, 2 ); } }
		public override int BreathPoisonDamage{ get{ return BeholderEye( this.VirtualArmor, 3 ); } }
		public override int BreathEnergyDamage{ get{ return BeholderEye( this.VirtualArmor, 4 ); } }
		public override int BreathEffectHue{ get{ return BeholderEye( this.VirtualArmor, 5 ); } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSound{ get{ return BeholderEye( this.VirtualArmor, 7 ); } }
		public override int BreathEffectItemID{ get{ return BeholderEye( this.VirtualArmor, 6 ); } }

		public static int BeholderEye ( int cyclops, int value )
		{
			if ( value == 1 && cyclops == 48 ){ return 100; } // RETURN THE FIRE DAMAGE
			if ( value == 2 && cyclops == 49 ){ return 100; } // RETURN THE COLD DAMAGE
			if ( value == 3 && cyclops == 50 ){ return 100; } // RETURN THE POISON DAMAGE
			if ( value == 4 && cyclops == 51 ){ return 100; } // RETURN THE ENERGY DAMAGE

			if ( value == 5 && cyclops == 48 ){ return 0; } // RETURN THE FIRE HUE
			if ( value == 5 && cyclops == 49 ){ return 0x481; } // RETURN THE COLD HUE
			if ( value == 5 && cyclops == 50 ){ return 0x3F; } // RETURN THE POISON HUE
			if ( value == 5 && cyclops == 51 ){ return 0x9C2; } // RETURN THE ENERGY HUE

			if ( value == 6 && cyclops == 48 ){ return 0x36D4; } // RETURN THE FIRE ID
			if ( value == 6 && cyclops == 49 ){ return 0x36D4; } // RETURN THE COLD ID
			if ( value == 6 && cyclops == 50 ){ return 0x36D4; } // RETURN THE POISON ID
			if ( value == 6 && cyclops == 51 ){ return 0x3818; } // RETURN THE ENERGY ID

			if ( value == 7 && cyclops == 48 ){ return 0x227; } // RETURN THE FIRE SOUND
			if ( value == 7 && cyclops == 49 ){ return 0x64F; } // RETURN THE COLD SOUND
			if ( value == 7 && cyclops == 50 ){ return 0x658; } // RETURN THE POISON SOUND
			if ( value == 7 && cyclops == 51 ){ return 0x665; } // RETURN THE ENERGY SOUND

			return 0;
		}

		public override void BreathDealDamage( Mobile target, int form )
		{
			if ( this.VirtualArmor == 48 ){ form = 17; } // RETURN THE FIRE DAMAGE
			if ( this.VirtualArmor == 49 ){ form = 19; } // RETURN THE COLD DAMAGE
			if ( this.VirtualArmor == 50 ){ form = 18; } // RETURN THE POISON DAMAGE
			if ( this.VirtualArmor == 51 ){ form = 20; } // RETURN THE ENERGY DAMAGE

			base.BreathDealDamage( target, form );

			this.VirtualArmor = Utility.RandomMinMax( 48, 51 ); // THIS IS USED TO RANDOMIZE ATTACK TYPES
		}

		[Constructable]
		public Seeker () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a seeker";
			Body = 83;
			BaseSoundID = 377;

			SetStr( 296, 325 );
			SetDex( 86, 105 );
			SetInt( 291, 385 );

			SetHits( 178, 195 );

			SetDamage( 8, 19 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 62.0, 100.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 115.1, 130.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 12500;
			Karma = -12500;

			VirtualArmor = Utility.RandomMinMax( 48, 51 );
		}

		public override int TreasureMapLevel{ get{ return Core.AOS ? 4 : 0; } }
		public override int GetAttackSound(){ return 0x60E; }	// A
		public override int GetDeathSound(){ return 0x60F; }	// D
		public override int GetHurtSound(){ return 0x610; }		// H

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
		}

		public Seeker( Serial serial ) : base( serial )
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