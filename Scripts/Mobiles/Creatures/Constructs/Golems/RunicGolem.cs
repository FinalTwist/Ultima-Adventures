using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a broken golem" )]
	public class RunicGolem : BaseCreature
	{
		private bool m_Stunning;

		[Constructable]
		public RunicGolem() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a runic golem";
			Body = 754;
			BaseSoundID = 268;

			SetStr( 251, 280 );
			SetDex( 71, 90 );
			SetInt( 31, 62 );

			SetHits( 201, 218 );

			SetDamage( 18, 26 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 45 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 75, 95 );

			SetSkill( SkillName.MagicResist, 125.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 6000;
			Karma = -6000;

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new ArcaneGem() );

			VirtualArmor = 42;

			AddItem( new LighterSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			c.DropItem( Server.Items.DungeonLoot.RandomRuneMagic() );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( !Server.Items.HiddenTrap.IAmAWeaponSlayer( defender, this ) )
			{
				if ( !m_Stunning && 0.3 > Utility.RandomDouble() )
				{
					m_Stunning = true;

					defender.Animate( 21, 6, 1, true, false, 0 );
					this.PlaySound( 0xEE );
					defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You have been stunned by a devastating blow!" );

					BaseWeapon weapon = this.Weapon as BaseWeapon;
					if ( weapon != null )
						weapon.OnHit( this, defender );

					if ( defender.Alive )
					{
						defender.BoltEffect( 0 );
						defender.Frozen = true;
						defender.Mana = 0;
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

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( Controlled || Summoned )
			{
				Mobile master = ( this.ControlMaster );

				if ( master == null )
					master = this.SummonMaster;

				if ( !Server.Items.HiddenTrap.IAmAWeaponSlayer( from, this ) && master != null && master.Player && master.Map == this.Map && master.InRange( Location, 20 ) )
				{
					if ( master.Mana >= amount )
					{
						master.Mana -= amount;
					}
					else
					{
						amount -= master.Mana;
						master.Mana = 0;
						master.Damage( amount );
					}
				}
			}

			base.OnDamage( amount, from, willKill );
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ reflect = true; } // 50% spells are reflected back to the caster
			else { reflect = false; }
		}

		public RunicGolem( Serial serial ) : base( serial )
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