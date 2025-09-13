using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a sand vortex corpse" )]
	public class SandVortex : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 50; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x96D; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSound{ get{ return 0x654; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 15 ); }

		[Constructable]
		public SandVortex() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sand vortex";
			Body = 790;
			BaseSoundID = 263;

			SetStr( 96, 120 );
			SetDex( 171, 195 );
			SetInt( 76, 100 );

			SetHits( 51, 62 );

			SetDamage( 3, 16 );

			SetDamageType( ResistanceType.Physical, 90 );
			SetDamageType( ResistanceType.Fire, 10 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.MagicResist, 150.0 );
			SetSkill( SkillName.Tactics, 70.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 28;
			PackItem( new Bone() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 2 );
		}

		private DateTime m_NextAttack;

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Item sand = new Sand();
   			sand.Amount = Utility.RandomMinMax( 1, 2 );
   			c.DropItem(sand);
		}

		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;

			if ( DateTime.UtcNow >= m_NextAttack )
			{
				SandAttack( combatant );
				m_NextAttack = DateTime.UtcNow + TimeSpan.FromSeconds( 10.0 + (10.0 * Utility.RandomDouble()) );
			}
		}

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) && this.Fame > 4500 )
			{
				Container cont = m.Backpack;
				Item iSucked = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iSucked != null )
				{
					if ( Server.Items.HiddenTrap.IAmShielding( m, 100 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iSucked, m ) == true )
					{
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "One of your protected items was almost carried by the wind!");
					}
					else
					{
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "One of your items was carried into the wind!");
						m.PlaySound( 0x10B );
						PackItem( iSucked );
					}
				}
			}
		}

		public void SandAttack( Mobile m )
		{
			DoHarmful( m );

			m.FixedParticles( 0x36B0, 10, 25, 9540, 2413, 0, EffectLayer.Waist );

			new InternalTimer( m, this ).Start();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile, m_From;

			public InternalTimer( Mobile m, Mobile from ) : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Mobile = m;
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				m_Mobile.PlaySound( 0x4CF );
				AOS.Damage( m_Mobile, m_From, Utility.RandomMinMax( 1, 40 ), 90, 10, 0, 0, 0 );
			}
		}

		public SandVortex( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}