using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Mobiles 
{
	[CorpseName( "a huge corpse" )] 
	public class FrankenFighter : BaseCreature
	{
		private bool m_Stunning;
		
		private bool m_Threwpot;

		public int FighterLevel;
		[CommandProperty(AccessLevel.Administrator)]
		public int Fighter_Level{ get { return FighterLevel; } set { FighterLevel = value; InvalidateProperties(); } }

		private int m_lesspots;
		[CommandProperty( AccessLevel.GameMaster )]
        public int LessPots
        {
            get{ return m_lesspots; }
            set{ m_lesspots = value; }
        }

		private int m_pots;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Pots
        {
            get{ return m_pots; }
            set{ m_pots = value; }
        }

		private int m_greatpots;
		[CommandProperty( AccessLevel.GameMaster )]
        public int GreatPots
        {
            get{ return m_greatpots; }
            set{ m_greatpots = value; }
        }

		private double m_decay;
		[CommandProperty( AccessLevel.GameMaster )]
        public double Decay
        {
            get{ return m_decay; }
            set{ m_decay = value; }
        }

		private DateTime m_NextTalking;
		public DateTime NextTalking{ get{ return m_NextTalking; } set{ m_NextTalking = value; } }
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.UtcNow >= m_NextTalking && InRange( m, 20 ) )
			{
				this.Loyalty = 100;
				m_NextTalking = (DateTime.UtcNow + TimeSpan.FromSeconds( 300 ));
			}
		}

		[Constructable] 
		public FrankenFighter( ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			m_NextTalking = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 ));

			Name = "a reanimation";
			Body = 69;
			BaseSoundID = 684;
			ControlSlots = 3;
			ActiveSpeed = 0.1;
			PassiveSpeed = 0.2;
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool DeleteOnRelease{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override bool IsDispellable { get { return false; } }
		public override bool IsBondable{ get{ return false; } }
		public override bool CanBeRenamedBy( Mobile from ){ return true; }
		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public FrankenFighter( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void OnAfterSpawn()
		{
			double scalar = 1.0;

			if ( FighterLevel < 10 ){ scalar = 1; }
			else if ( FighterLevel < 15 ){ scalar = 1.1; }
			else if ( FighterLevel < 20 ){ scalar = 1.2; }
			else if ( FighterLevel < 25 ){ scalar = 1.3; }
			else if ( FighterLevel < 30 ){ scalar = 1.4; }
			else if ( FighterLevel < 35 ){ scalar = 1.5; }
			else if ( FighterLevel < 40 ){ scalar = 1.6; }
			else if ( FighterLevel < 45 ){ scalar = 1.7; }
			else if ( FighterLevel < 50 ){ scalar = 1.8; }
			else if ( FighterLevel < 55 ){ scalar = 1.9; }
			else if ( FighterLevel < 60 ){ scalar = 2; }
			else if ( FighterLevel < 65 ){ scalar = 2.1; }
			else if ( FighterLevel < 70 ){ scalar = 2.2; }
			else if ( FighterLevel < 75 ){ scalar = 2.3; }
			else if ( FighterLevel < 80 ){ scalar = 2.4; }
			else if ( FighterLevel < 85 ){ scalar = 2.5; }
			else if ( FighterLevel < 90 ){ scalar = 2.6; }
			else if ( FighterLevel < 95 ){ scalar = 2.7; }
			else { scalar = 2.8; }

			if ( FighterLevel >= 65 ){ Body = 999; }

			scalar -= m_decay;
			if (scalar < 0.1 )
				scalar = 0.1;

			double chemist = 1;

			if (this.ControlMaster != null && this.ControlMaster is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)ControlMaster;
				if (pm.Alchemist())
					chemist = pm.AlchemistBonus();
			}

			SetStr( (int)(200*scalar*chemist) );
			SetDex( (int)(100*scalar*chemist) );
			SetInt( (int)(60*scalar*chemist) );

			SetHits( (int)(210*scalar*(chemist)) );

			SetDamage( (int)(5*scalar*chemist), (int)(10*scalar*chemist) );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, (int)(26*scalar) );
			SetResistance( ResistanceType.Fire, (int)(20*scalar) );
			SetResistance( ResistanceType.Cold, (int)(20*scalar) );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, (int)(20*scalar) );

			SetSkill( SkillName.MagicResist, (50.0*scalar) );
			SetSkill( SkillName.Tactics, (50.0*scalar) );
			SetSkill( SkillName.Wrestling, (50.0*scalar) );
		}

		public override bool OnBeforeDeath()
		{
			if (Utility.RandomDouble() > 0.75)
				m_decay += 0.10;

			Effects.SendLocationEffect(this.Location, this.Map, 0x36B0, 9, 10, 0, 0);
			this.PlaySound( 0x1DB );
			this.AIObject.DoOrderRelease();
			return false;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{  
			if (dropped == null || from == null || !(from is PlayerMobile) || this.ControlMaster == null)
				return false;

			if (dropped is BaseExplosionPotion && this.ControlMaster is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)ControlMaster;
				if (!pm.Alchemist())
				{
					from.SendMessage("Only Chemists may direct their reanimation to use potions.");
					return false;
				}

				if (dropped is GreaterExplosionPotion)
					m_greatpots += dropped.Amount;
				else if (dropped is LesserExplosionPotion)
					m_lesspots += dropped.Amount;
				else if (dropped is ExplosionPotion)
					m_pots += dropped.Amount;

				dropped.Delete();
				return true;

			}

			return base.OnDragDrop(from, dropped);
		}

		public override void OnThink()
		{
			base.OnThink();

			if (!m_Threwpot && ( m_lesspots > 0 || m_greatpots > 0 || m_pots > 0 ) && ControlMaster != null && ControlMaster is PlayerMobile && ((PlayerMobile)ControlMaster).Alchemist() && Utility.RandomDouble() > 0.85)
			{
				if (this.Combatant != null && InLOS( this.Combatant ) && (int)GetDistanceToSqrt( this.Combatant ) > 3 && (int)GetDistanceToSqrt( this.Combatant ) < 15 )
				{
						if (m_potTimer == null )
						{
							this.Frozen = true;
							m_Threwpot = true;
							Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( Throwpot ), new object[]{ this } );
						}
				}
			}
		}


		private Timer m_potTimer;		
		public void Throwpot( object state )
		{
			m_Threwpot = false;

			if (this.Deleted || this == null )
				return;

			this.Frozen = false;
			object[] states = (object[])state;

			FrankenFighter from = (FrankenFighter)states[0];

			if (from == null || from.Map == null || from.Combatant == null )
				return;

			Map map = from.Map;
			Mobile target = from.Combatant;
			Map targetmap = target.Map;
			Point3D targetloc = target.Location;

			if (  target == null || targetmap == Map.Internal || targetmap == null )
				return;

		    BaseExplosionPotion explosion = null;

			if (m_lesspots > 0 && Utility.RandomBool())
			{
				explosion = new LesserExplosionPotion();
				m_lesspots --;
			}
			else if (m_pots > 0 && Utility.RandomBool())
			{
				explosion = new ExplosionPotion();
				m_pots --;
			}
			else if (m_greatpots > 0)
			{
				explosion = new GreaterExplosionPotion();
				m_greatpots --;
			}

		    if( explosion != null )
		    {

				explosion.Internalize();

				Effects.SendMovingEffect( from, target, 0x2408, 7, 0, false, false, 0, 0 );

				Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( Reposition_OnTick ), new object[]{ explosion, targetloc, targetmap } );
				
			}
		}

		private void Reposition_OnTick( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Item pot = (Item)states[0];
			Point3D p = (Point3D)states[1];
			Map map = (Map)states[2];

			pot.MoveToWorld( p, map );

			m_potTimer = Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.25 ), 5, new TimerStateCallback( Detonate_OnTick ), new object[]{ pot, p, map, 3 } ); // 3.6 seconds explosion delay
		}

		private void Detonate_OnTick( object state )
		{

			object[] states = (object[])state;
			BaseExplosionPotion pot = (BaseExplosionPotion)states[0];
			Point3D loc = (Point3D)states[1];
			Map map = (Map)states[2];
			int timer = (int)states[3];

			if (pot.Deleted)
				return;

			if ( timer == 0 )
			{
				if ( pot.Deleted )
					return;

				pot.Consume();

				if ( map == null )
					return;

				Effects.PlaySound(loc, map, 0x307);

				Effects.SendLocationEffect(loc, map, 0x36B0, 9, 10, 0, 0);

				IPooledEnumerable eable =  map.GetObjectsInRange( loc, 2 ) ;
				ArrayList toExplode = new ArrayList();

				int toDamage = 0;

				foreach ( object o in eable )
				{
					if ( o is Mobile && !(o is BaseRed) ) 
					{
						toExplode.Add( o );
						++toDamage;
					}
				}

				eable.Free();

				for ( int i = 0; i < toExplode.Count; ++i )
				{
					object o = toExplode[i];

					if ( o is Mobile )
					{
						Mobile m = (Mobile)o;

						int damage = Utility.RandomMinMax( 20, 80 );

						AOS.Damage( true, m, this, damage, 0, 100, 0, 0, 0 );
					}
					else if ( o is BaseExplosionPotion )
					{
						BaseExplosionPotion pots = (BaseExplosionPotion)o;

						pots.Explode( this, false, pots.GetWorldLocation(), pots.Map );
					}
				}

				m_potTimer = null;
			}
			else
			{
				pot.PublicOverheadMessage( MessageType.Regular, 0x22, false, timer.ToString() );
				states[3] = timer - 1;
			}
		}


		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version
            writer.Write( FighterLevel );
			Loyalty = 100;
			writer.Write( m_lesspots );
			writer.Write( m_pots );
			writer.Write( m_greatpots );
			writer.Write( m_decay );
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
					this.PlaySound( 0xEE );
					defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You have been knocked senseless!" );

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

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt();
			FighterLevel = reader.ReadInt();

			if (version >= 1)
			{
				m_lesspots = reader.ReadInt();
				m_pots = reader.ReadInt();
				m_greatpots = reader.ReadInt();
				m_decay = reader.ReadDouble();
			}


			LeaveNowTimer thisTimer = new LeaveNowTimer( this ); 
			thisTimer.Start(); 
		} 
	}
}
