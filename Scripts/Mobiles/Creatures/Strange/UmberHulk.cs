using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "an umber hulk corpse" )]
	public class UmberHulk : BaseCreature
	{
		private bool m_Stunning;

		[Constructable]
		public UmberHulk() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an umber hulk";
			Body = 330;
			BaseSoundID = 397;

			SetStr( 251, 280 );
			SetDex( 71, 90 );
			SetInt( 31, 62 );

			SetHits( 201, 218 );

			SetDamage( 14, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 39;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( !Server.Items.HiddenTrap.IAmAWeaponSlayer( defender, this ) )
			{
				if ( !m_Stunning && 0.3 > Utility.RandomDouble() )
				{
					m_Stunning = true;

					defender.Animate( 21, 6, 1, true, false, 0 );
					defender.PlaySound( 0x1F9 );
					defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You are confused from umber hulk's stare!" );

					BaseWeapon weapon = this.Weapon as BaseWeapon;
					if ( weapon != null )
						weapon.OnHit( this, defender );

					if ( defender.Alive )
					{
						defender.Frozen = true;
						Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( Recover_Callback ), defender );
					}
				}
			}
		}

		private void Recover_Callback( object state )
		{
			Mobile defender = state as Mobile;

			if ( defender != null )
			{
				defender.Frozen = false;
				defender.Combatant = null;
				defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You recover your senses." );
			}

			m_Stunning = false;
		}

		public override int GetAngerSound()
		{
			return 0x21D;
		}

		public override int GetIdleSound()
		{
			return 0x21D;
		}

		public override int GetAttackSound()
		{
			return 0x162;
		}

		public override int GetHurtSound()
		{
			return 0x163;
		}

		public override int GetDeathSound()
		{
			return 0x21D;
		}

		public UmberHulk( Serial serial ) : base( serial )
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