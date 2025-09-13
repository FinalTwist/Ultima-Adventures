using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a colossus corpse" )]
	public class IceColossus : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 120.5; } }
		public override double DispelFocus{ get{ return 35.0; } }

		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 50; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 33 ); }

		[Constructable]
		public IceColossus() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ice colossus";
			Body = 753;
			Hue = 0x919;
			BaseSoundID = 268;

			SetStr( 226, 255 );
			SetDex( 126, 145 );
			SetInt( 71, 92 );

			SetHits( 236, 353 );

			SetDamage( 28 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 32;

			AddItem( new LighterSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 3 );
			AddLoot( LootPack.Gems, 2 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			RareMetals stones = new RareMetals( Utility.RandomMinMax( 10, 25 ), "mystical ice stones" );
   			c.DropItem(stones);
		}

		public override int GetAttackSound(){ return 0x626; }	// A
		public override int GetDeathSound(){ return 0x627; }	// D
		public override int GetHurtSound(){ return 0x628; }		// H

		public override bool BleedImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
			}
			else if ( from != null )
			{
				int hitback = (int)(damage/2); if (hitback > 50){ hitback = 50; }
				AOS.Damage( from, this, hitback, 100, 0, 0, 0, 0 );
			}
		}

		public IceColossus( Serial serial ) : base( serial )
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