using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class IceGhoul : BaseCreature
	{
		private bool m_Stunning;

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.MortalStrike;
		}

		[Constructable]
		public IceGhoul() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an icy ghoul";
			Body = 181;
			BaseSoundID = 471;
			Hue = 0xB78;

			SetStr( 76, 100 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetHits( 46, 60 );
			SetMana( 0 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 28;

			int[] list = new int[]
				{
					0x1CF0, 0x1CEF, 0x1CEE, 0x1CED, 0x1CE9, 0x1DA0, 0x1DAE, // pieces
					0x1CEC, 0x1CE5, 0x1CE2, 0x1CDD, 0x1AE4, 0x1DA1, 0x1DA2, 0x1DA4, 0x1DAF, 0x1DB0, 0x1DB1, 0x1DB2, // limbs
					0x1CE8, 0x1CE0, 0x1D9F, 0x1DAD // torsos
				};

			PackItem( new BodyPart( Utility.RandomList( list ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int Hides{ get{ return 2; } }
		public override HideType HideType{ get{ return HideType.Necrotic; } }

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
					defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You have been paralyzed by a necrotic claw!" );

					BaseWeapon weapon = this.Weapon as BaseWeapon;
					if ( weapon != null )
						weapon.OnHit( this, defender );

					if ( defender.Alive )
					{
						defender.Frozen = true;
						defender.PlaySound( 0x133 );
						defender.FixedParticles( 0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist );
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
				defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You feel your muscles regain control." );
			}

			m_Stunning = false;
		}

		public IceGhoul( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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