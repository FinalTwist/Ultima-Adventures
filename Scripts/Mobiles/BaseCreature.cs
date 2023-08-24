using System;
using System.Collections.Generic;
using System.Collections;
using Server.Regions;
using Server.Targeting;
using Server.Network;
using Server.Multis;
using Server.Spells;
using Server.Items;
using Server.ContextMenus;
using Server.Engines.Quests;
using Server.Engines.PartySystem;
using Server.Spells.Bushido;
using Server.Spells.Necromancy;
using Server.Engines.CannedEvil;
using Server.Misc;
using Server.Custom;
using System.Text;
using Server;
using System.IO;
using Custom.Jerbal.Jako;
using Server.OneTime.Events;
using Felladrin.Automations;

namespace Server.Mobiles
{
	#region Enums
	/// <summary>
	/// Summary description for MobileAI.
	/// </summary>
	///
	public enum FightMode
	{
		None,			// Never focus on others
		Aggressor,		// Only attack aggressors
		Strongest,		// Attack the strongest
		Weakest,		// Attack the weakest
		Closest, 		// Attack the closest
		Evil,			// Only attack aggressor -or- negative karma
		Good,			// Only attack aggressor -or- positive karma // WIZARD ADDED
		CharmMonster,
		CharmAnimal
	}

	public enum OrderType
	{
		None,			//When no order, let's roam
		Come,			//"(All/Name) come"  Summons all or one pet to your location.
		Drop,			//"(Name) drop"  Drops its loot to the ground (if it carries any).
		Follow,			//"(Name) follow"  Follows targeted being.
						//"(All/Name) follow me"  Makes all or one pet follow you.
		Friend,			//"(Name) friend"  Allows targeted player to confirm resurrection.
		Unfriend,		// Remove a friend
		Guard,			//"(Name) guard"  Makes the specified pet guard you. Pets can only guard their owner.
						//"(All/Name) guard me"  Makes all or one pet guard you.
		Attack,			//"(All/Name) kill",
						//"(All/Name) attack"  All or the specified pet(s) currently under your control attack the target.
		Patrol,			//"(Name) patrol"  Roves between two or more guarded targets.
		Release,		//"(Name) release"  Releases pet back into the wild (removes "tame" status).
		Stay,			//"(All/Name) stay" All or the specified pet(s) will stop and stay in current spot.
		Stop,			//"(All/Name) stop Cancels any current orders to attack, guard or follow.
		Transfer		//"(Name) transfer" Transfers complete ownership to targeted player.
	}

	[Flags]
	public enum FoodType
	{
		None			= 0x0000,
		Meat			= 0x0001,
		FruitsAndVegies	= 0x0002,
		GrainsAndHay	= 0x0004,
		Fish			= 0x0008,
		Eggs			= 0x0010,
		Gold			= 0x0020,
		Fire			= 0x0040,
		Gems			= 0x0080,
		Nox				= 0x0100,
		Sea				= 0x0200,
		Moon			= 0x0400
	}

	[Flags]
	public enum PackInstinct
	{
		None			= 0x0000,
		Canine			= 0x0001,
		Ostard			= 0x0002,
		Feline			= 0x0004,
		Arachnid		= 0x0008,
		Daemon			= 0x0010,
		Bear			= 0x0020,
		Equine			= 0x0040,
		Bull			= 0x0080
	}

	public enum ScaleType
	{
		Red,
		Yellow,
		Black,
		Green,
		White,
		Blue,
		Dinosaur,
		All
	}

	public enum MeatType
	{
		Ribs,
		Bird,
		LambLeg,
		Fish
	}

	public enum FurType
	{
		Regular,
		White
	}

	public enum HideType
	{
		Regular,
		Spined,
		Horned,
		Barbed,
		Necrotic,
		Volcanic,
		Frozen,
		Goliath,
		Draconic,
		Hellish,
		Dinosaur,
		Alien
	}

	#endregion

	public class DamageStore : IComparable
	{
		public Mobile m_Mobile;
		public int m_Damage;
		public bool m_HasRight;

		public DamageStore( Mobile m, int damage )
		{
			m_Mobile = m;
			m_Damage = damage;
		}

		public int CompareTo( object obj )
		{
			DamageStore ds = (DamageStore)obj;

			return ds.m_Damage - m_Damage;
		}
	}

	[AttributeUsage( AttributeTargets.Class )]
	public class FriendlyNameAttribute : Attribute
	{
		//future use: Talisman 'Protection/Bonus vs. Specific Creature
		private TextDefinition m_FriendlyName;

		public TextDefinition FriendlyName
		{
			get
			{
				return m_FriendlyName;
			}
		}

		public FriendlyNameAttribute( TextDefinition friendlyName )
		{
			m_FriendlyName = friendlyName;
		}

		public static TextDefinition GetFriendlyNameFor( Type t )
		{
			if( t.IsDefined( typeof( FriendlyNameAttribute ), false ) )
			{
				object[] objs = t.GetCustomAttributes( typeof( FriendlyNameAttribute ), false );

				if( objs != null && objs.Length > 0 )
				{
					FriendlyNameAttribute friendly = objs[0] as FriendlyNameAttribute;

					return friendly.FriendlyName;
				}
			}

			return t.Name;
		}
	}

	public class BaseCreature : Mobile, IHonorTarget
	{
		public const int MaxLoyalty = 100;

        private bool m_IsHitchStabled;

        #region Var declarations
        private BaseAI	m_AI;					// THE AI

		private AIType	m_CurrentAI;			// The current AI
		private AIType	m_DefaultAI;			// The default AI

		private Mobile	m_FocusMob;				// Use focus mob instead of combatant, maybe we don't whan to fight
		private FightMode m_FightMode;			// The style the mob uses

		private int		m_iRangePerception;		// The view area
		private int		m_iRangeFight;			// The fight distance

		private bool	m_bDebugAI;				// Show debug AI messages
		private bool 	m_goferal;
		private int 	m_special;

		private int		m_iTeam;				// Monster Team

		private double	m_dActiveSpeed;			// Timer speed when active
		private double	m_dPassiveSpeed;		// Timer speed when not active
		private double	m_dCurrentSpeed;		// The current speed, lets say it could be changed by something;

		private Point3D m_pHome;				// The home position of the creature, used by some AI
		private int		m_iRangeHome = 10;		// The home range of the creature

		List<Type>		m_arSpellAttack;		// List of attack spell/power
		List<Type>		m_arSpellDefense;		// List of defensive spell/power

		private bool		m_bControlled;		// Is controlled
		private Mobile		m_ControlMaster;	// My master
		private Mobile		m_ControlTarget;	// My target mobile
		private Point3D		m_ControlDest;		// My target destination (patrol)
		private OrderType	m_ControlOrder;		// My order

		private int			m_Loyalty;

		private double		m_dMinTameSkill;
		private bool		m_bTamable;

		private bool		m_bSummoned = false;
		private DateTime	m_SummonEnd;
		private int			m_iControlSlots;

		private bool		m_bBardProvoked = false;
		private bool		m_bBardPacified = false;
		private Mobile		m_bBardMaster = null;
		private Mobile		m_bBardTarget = null;
		private DateTime	m_timeBardEnd;
		private WayPoint	m_CurrentWayPoint = null;
		private IPoint2D	m_TargetLocation = null;

		private Mobile		m_SummonMaster;

		private int			m_HitsMax = -1;
		private	int			m_StamMax = -1;
		private int			m_ManaMax = -1;
		private int			m_DamageMin = -1;
		private int			m_DamageMax = -1;

		private int			m_PhysicalResistance, m_PhysicalDamage = 100;
		private int			m_FireResistance, m_FireDamage;
		private int			m_ColdResistance, m_ColdDamage;
		private int			m_PoisonResistance, m_PoisonDamage;
		private int			m_EnergyResistance, m_EnergyDamage;
		private int			m_ChaosDamage;
		private int			m_DirectDamage;

		//adding for the new aging mechanic
		public int 		m_oldstr;
		public int 		m_oldint;
		public int 		m_olddex;
		public int 		m_oldmin;
		public int 		m_oldmax;
		public int 		m_oldphys;
		public int 		m_oldfire;
		public int 		m_oldcold;
		public int 		m_oldpois;
		public int 		m_oldener;

		private List<Mobile> m_Owners;
		private List<Mobile> m_Friends;

		private bool		m_IsStabled;

		private bool		m_HasGeneratedLoot; // have we generated our loot yet?

		private bool		m_Paragon;

		private bool		m_IsPrisoner;
		
		//private bool		IsSleeping;
		private bool 		Nearby;
		public bool IsBlessed;

		private bool m_IsSleeping;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool IsSleeping
        {
            get{ return m_IsSleeping; }
            set{ m_IsSleeping = value; }
        }

		private bool m_AdvancedAI;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool AdvancedAI
        {
            get{ return m_AdvancedAI; }
            set{ m_AdvancedAI = value; }
        }

		private bool m_breeding;

		[CommandProperty( AccessLevel.GameMaster )]
        public bool Breeding
        {
            get{ return m_breeding; }
            set{ m_breeding = value; }
        }

		private int m_injuries;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Injuries
        {
            get{ return m_injuries; }
            set{ m_injuries = value; }
        }

		private bool m_SpawnerSpawned;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool SpawnerSpawned
        {
            get{ return m_SpawnerSpawned; }
            set{ m_SpawnerSpawned = value; }
        }
		
		//Start Zombiex edit
		private bool m_CanInfect = false;
		[CommandProperty( AccessLevel.GameMaster )]
		public virtual bool CanInfect
		{
			get{return m_CanInfect;}
			set{m_CanInfect = value;}
		}
		//End Zombiex edit

		//Start Gadget2013 edit for AI fast run mode
		private bool m_FullSpeedPassiveAI = false;
		private bool m_FullSpeedActiveAI = false;
		private double dOrigPassiveSpeed = 0.0;
		private double dOrigActiveSpeed = 0.0;

		[CommandProperty( AccessLevel.GameMaster )]
		public virtual bool AIFullSpeedActive
		{
		    get 
		    { 
				if (dOrigActiveSpeed == 0.0 && ActiveSpeed != 0.0)
				{
					dOrigActiveSpeed = ActiveSpeed;
				}
				return m_FullSpeedActiveAI; 
		    }
		    set
		    {
				m_FullSpeedActiveAI = value;
				if (m_FullSpeedActiveAI)
				{
					dOrigActiveSpeed = ActiveSpeed;
				}
				else
				{
					ActiveSpeed = dOrigActiveSpeed;
				}
		    }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public virtual bool AIFullSpeedPassive
		{
		    get 
		    { 
			if (dOrigPassiveSpeed == 0.0 && PassiveSpeed != 0.0)
			{
			    dOrigPassiveSpeed = PassiveSpeed;
			}
			return m_FullSpeedPassiveAI; 
		    }
		    set
		    {
			m_FullSpeedPassiveAI = value;
			if (m_FullSpeedPassiveAI)
			{
			    dOrigPassiveSpeed = PassiveSpeed;
			}
			else
			{
			    PassiveSpeed = dOrigPassiveSpeed;
			}
		    }
		}
		//End Gadget2013 edit for AI fast run mode

		#endregion

		public virtual InhumanSpeech SpeechType{ get{ return null; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsHitchStabled
        {
            get
            {
                return m_IsHitchStabled;
            }
            set
            {
                m_IsHitchStabled = value;
            }

        }

        [CommandProperty( AccessLevel.GameMaster, AccessLevel.Administrator )]
		public bool IsStabled
		{
			get{ return m_IsStabled; }
			set
			{
				m_IsStabled = value;
				if ( m_IsStabled )
					StopDeleteTimer();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsPrisoner
		{
			get{ return m_IsPrisoner; }
			set{ m_IsPrisoner = value; }
		}

		protected DateTime SummonEnd
		{
			get { return m_SummonEnd; }
			set { m_SummonEnd = value; }
		}


		#region Bonding
		public const bool BondingEnabled = true;

		public virtual bool IsNecromancer { get { 
			return ( Skills[ SkillName.Necromancy ].Value > 50 && Karma < -1000 && Skills[ SkillName.SpiritSpeak ].Value > 50); 
		} }

		public virtual bool IsBondable{ get{ return ( BondingEnabled && !Summoned ); } }
		public virtual TimeSpan BondingDelay{ get{ return TimeSpan.FromDays( 7.0 ); } }
		public virtual TimeSpan BondingAbandonDelay{ get{ return TimeSpan.FromDays( 1.0 ); } }

		public override bool CanRegenHits{ get{ return !m_IsDeadPet && base.CanRegenHits; } }
		public override bool CanRegenStam{ get{ return !m_IsDeadPet && base.CanRegenStam; } }
		public override bool CanRegenMana{ get{ return !m_IsDeadPet && base.CanRegenMana; } }

		public override bool IsDeadBondedPet{ get{ return m_IsDeadPet; } }

		private bool m_IsBonded;
		private bool m_IsDeadPet;
		private DateTime m_BondingBegin;
		private DateTime m_OwnerAbandonTime;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile LastOwner
		{
			get
			{
				if ( m_Owners == null || m_Owners.Count == 0 )
					return null;

				return m_Owners[m_Owners.Count - 1];
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsBonded
		{
			get{ return m_IsBonded; }
			set{ m_IsBonded = value; InvalidateProperties(); }
		}

		public bool IsDeadPet
		{
			get{ return m_IsDeadPet; }
			set{ m_IsDeadPet = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime BondingBegin
		{
			get{ return m_BondingBegin; }
			set{ m_BondingBegin = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime OwnerAbandonTime
		{
			get{ return m_OwnerAbandonTime; }
			set{ m_OwnerAbandonTime = value; }
		}
		private int m_SecondsSoulTouched;
		public int SecondsSoulTouched {
			get { return m_SecondsSoulTouched; }
			set { m_SecondsSoulTouched = value;}
		}
		private List<SkillMod> m_SoulSkillMods;
		public List<SkillMod> SoulSkillMods {
			get { return m_SoulSkillMods; }
			set { m_SoulSkillMods = value;}
		}

		private int m_midrace;

		[CommandProperty(AccessLevel.GameMaster)]
		public int midrace {
			get { return m_midrace; }
			set { m_midrace = value;}
		}

		//1 humans
		//2 gargoyles
		//3 lizards
		//4 pirates
		//5 orcs

		private bool m_isdiscorded;

		public bool IsDiscorded {
			get { return m_isdiscorded; }
			set { m_isdiscorded = value;}
		}

		private bool m_superspeed;


		#endregion

		#region Delete Previously Tamed Timer
		private DeleteTimer		m_DeleteTimer;

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan DeleteTimeLeft
		{
			get
			{
				if ( m_DeleteTimer != null && m_DeleteTimer.Running )
					return m_DeleteTimer.Next - DateTime.UtcNow;

				return TimeSpan.Zero;
			}
		}

		private class DeleteTimer : Timer
		{
			private Mobile m;

			public DeleteTimer( Mobile creature, TimeSpan delay ) : base( delay )
			{
				m = creature;
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				m.Delete();
			}
		}

		public void BeginDeleteTimer()
		{
			if ( !Summoned && !Deleted && !IsStabled )
			{
				StopDeleteTimer();
				m_DeleteTimer = new DeleteTimer( this, TimeSpan.FromDays( 3.0 ) );
				m_DeleteTimer.Start();
			}
		}

		public void StopDeleteTimer()
		{
			if ( m_DeleteTimer != null )
			{
				m_DeleteTimer.Stop();
				m_DeleteTimer = null;
			}
		}

		#endregion

		public virtual double WeaponAbilityChance{ get{ return 0.4; } }

		public virtual WeaponAbility GetWeaponAbility()
		{
			return null;
		}

		#region Elemental Resistance/Damage

		public override int BasePhysicalResistance{ get{ return m_PhysicalResistance; } }
		public override int BaseFireResistance{ get{ return m_FireResistance; } }
		public override int BaseColdResistance{ get{ return m_ColdResistance; } }
		public override int BasePoisonResistance{ get{ return m_PoisonResistance; } }
		public override int BaseEnergyResistance{ get{ return m_EnergyResistance; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PhysicalResistanceSeed{ get{ return m_PhysicalResistance; } set{ m_PhysicalResistance = value; UpdateResistances(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int FireResistSeed{ get{ return m_FireResistance; } set{ m_FireResistance = value; UpdateResistances(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ColdResistSeed{ get{ return m_ColdResistance; } set{ m_ColdResistance = value; UpdateResistances(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PoisonResistSeed{ get{ return m_PoisonResistance; } set{ m_PoisonResistance = value; UpdateResistances(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int EnergyResistSeed{ get{ return m_EnergyResistance; } set{ m_EnergyResistance = value; UpdateResistances(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PhysicalDamage{ get{ return m_PhysicalDamage; } set{ m_PhysicalDamage = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int FireDamage{ get{ return m_FireDamage; } set{ m_FireDamage = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ColdDamage{ get{ return m_ColdDamage; } set{ m_ColdDamage = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PoisonDamage{ get{ return m_PoisonDamage; } set{ m_PoisonDamage = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int EnergyDamage{ get{ return m_EnergyDamage; } set{ m_EnergyDamage = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ChaosDamage{ get{ return m_ChaosDamage; } set{ m_ChaosDamage = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int DirectDamage{ get{ return m_DirectDamage; } set{ m_DirectDamage = value; } }

		#endregion

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsParagon
		{
			get{ return m_Paragon; }
			set
			{
				if ( m_Paragon == value )
					return;
				else if ( value )
					Paragon.Convert( this );
				else
					Paragon.UnConvert( this );

				m_Paragon = value;

				InvalidateProperties();
			}
		}

		public virtual FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public virtual PackInstinct PackInstinct{ get{ return PackInstinct.None; } }

		public List<Mobile> Owners { get { return m_Owners; } }

		public virtual bool AllowMaleTamer{ get{ return true; } }
		public virtual bool AllowFemaleTamer{ get{ return true; } }
		public virtual bool SubdueBeforeTame{ get{ return false; } }
		public virtual bool StatLossAfterTame{ get{ return SubdueBeforeTame; } }
		public virtual bool ReduceSpeedWithDamage{ get{ return true; } }
		public virtual bool IsSubdued{ get{ return SubdueBeforeTame && ( Hits < ( HitsMax / 10 ) ); } }

		public virtual bool Commandable{ get{ return true; } }

		public virtual Poison HitPoison{ get{ return null; } }
		public virtual double HitPoisonChance{ get{ return 0.5; } }
		public virtual Poison PoisonImmune{ get{ return null; } }

		public virtual bool BardImmune{ get{ return false; } }
		public virtual bool Unprovokable{ get{ return BardImmune || m_IsDeadPet; } }
		public virtual bool Uncalmable{ get{ return BardImmune || m_IsDeadPet; } }
		public virtual bool AreaPeaceImmune { get { return BardImmune || m_IsDeadPet; } }

		public virtual bool BleedImmune{ get{ return false; } }
		public virtual double BonusPetDamageScalar{ get{ return 1.0; } }

		public virtual bool DeathAdderCharmable{ get{ return false; } }

		//TODO: Find the pub 31 tweaks to the DispelDifficulty and apply them of course.
		public virtual double DispelDifficulty{ get{ return 0.0; } } // at this skill level we dispel 50% chance
		public virtual double DispelFocus{ get{ return 20.0; } } // at difficulty - focus we have 0%, at difficulty + focus we have 100%
		public virtual bool DisplayWeight{ get{ return Backpack is StrongBackpack; } }

		#region Breath ability, like dragon fire breath
		private DateTime m_NextBreathTime;

		// Must be overriden in subclass to enable
		public virtual bool HasBreath{ get{ return false; } }

		// Base damage given is: CurrentHitPoints * BreathDamageScalar
		public virtual double BreathDamageScalar{ get{ return 0.25; } }

		// Min/max seconds until next breath
		public virtual double BreathMinDelay{ get{ return 10.0; } }
		public virtual double BreathMaxDelay{ get{ return 15.0; } }

		// Creature stops moving for 1.0 seconds while breathing
		public virtual double BreathStallTime{ get{ return 1.0; } }

		// Effect is sent 1.3 seconds after BreathAngerSound and BreathAngerAnimation is played
		public virtual double BreathEffectDelay{ get{ return 1.3; } }

		// Damage is given 1.0 seconds after effect is sent
		public virtual double BreathDamageDelay{ get{ return 1.0; } }

		public virtual int BreathRange{ get{ return RangePerception; } }

		// Damage types
		public virtual int BreathPhysicalDamage{ get{ return 0; } }
		public virtual int BreathFireDamage{ get{ return 100; } }
		public virtual int BreathColdDamage{ get{ return 0; } }
		public virtual int BreathPoisonDamage{ get{ return 0; } }
		public virtual int BreathEnergyDamage{ get{ return 0; } }

		// Is immune to breath damages
		public virtual bool BreathImmune{ get{ return false; } }

		// Effect details and sound
		public virtual int BreathEffectItemID{ get{ return 0x36D4; } }
		public virtual int BreathEffectSpeed{ get{ return 5; } }
		public virtual int BreathEffectDuration{ get{ return 0; } }
		public virtual bool BreathEffectExplodes{ get{ return false; } }
		public virtual bool BreathEffectFixedDir{ get{ return false; } }
		public virtual int BreathEffectHue{ get{ return 0; } }
		public virtual int BreathEffectRenderMode{ get{ return 0; } }

		public virtual int BreathEffectSound{ get{ return 0x227; } }

		// Anger sound/animations
		public virtual int BreathAngerSound{ get{ return GetAngerSound(); } }
		public virtual int BreathAngerAnimation{ get{ return 12; } }

		public virtual void BreathStart( Mobile target )
		{
			BreathStallMovement();
			BreathPlayAngerSound();
			BreathPlayAngerAnimation();

			this.Direction = this.GetDirectionTo( target );

			Timer.DelayCall( TimeSpan.FromSeconds( BreathEffectDelay ), new TimerStateCallback( BreathEffect_Callback ), target );
		}

		public virtual void BreathStallMovement()
		{
			if ( m_AI != null )
				m_AI.NextMove = DateTime.UtcNow + TimeSpan.FromSeconds( BreathStallTime );
		}

		public virtual void BreathPlayAngerSound()
		{
			PlaySound( BreathAngerSound );
		}

		public virtual void BreathPlayAngerAnimation()
		{
			Animate( BreathAngerAnimation, 5, 1, true, false, 0 );
		}

		public virtual void BreathEffect_Callback( object state )
		{
			Mobile target = (Mobile)state;

			if ( !target.Alive || !CanBeHarmful( target ) )
				return;
			
			if ( target is PlayerMobile && this.Controlled && this.ControlMaster == target && this.Combatant != target) // FInal, tamed dragons were harming their owners with breath.
				return;

			BreathPlayEffectSound();
			if ( BreathEffectItemID > 0 ){ BreathPlayEffect( target ); }

			Timer.DelayCall( TimeSpan.FromSeconds( BreathDamageDelay ), new TimerStateCallback( BreathDamage_Callback ), target );
		}

		public virtual void BreathPlayEffectSound()
		{
			PlaySound( BreathEffectSound );
		}

		public virtual void BreathPlayEffect( Mobile target )
		{
			Effects.SendMovingEffect( this, target, BreathEffectItemID,
				BreathEffectSpeed, BreathEffectDuration, BreathEffectFixedDir,
				BreathEffectExplodes, BreathEffectHue, BreathEffectRenderMode );
		}

		public virtual void BreathDamage_Callback( object state )
		{
			Mobile target = (Mobile)state;

			if ( target is BaseCreature && ((BaseCreature)target).BreathImmune )
				return;
			
			if ( target is PlayerMobile && this.Controlled && this.ControlMaster == target && this.Combatant != target) // FInal, tamed dragons were harming their owners with breath.
				return;

			if ( CanBeHarmful( target ) )
			{
				DoHarmful( target );
				BreathDealDamage( target, 0 );
			}
		}

		public virtual void BreathDealDamage( Mobile target, int form )
		{
			if( Evasion.CheckSpellEvasion( target ) )
				return;

			DoFinalBreathAttack( target, form, true );
		}

		public void DoFinalBreathAttack( Mobile target, int form, bool cycle )
		{
			int physDamage = BreathPhysicalDamage;
			int fireDamage = BreathFireDamage;
			int coldDamage = BreathColdDamage;
			int poisDamage = BreathPoisonDamage;
			int nrgyDamage = BreathEnergyDamage;
			int BreathDistance = 0;

			Point3D blast1 = new Point3D( ( target.X ), ( target.Y ), target.Z );
			Point3D blast2 = new Point3D( ( target.X-1 ), ( target.Y ), target.Z );
			Point3D blast3 = new Point3D( ( target.X+1 ), ( target.Y ), target.Z );
			Point3D blast4 = new Point3D( ( target.X ), ( target.Y-1 ), target.Z );
			Point3D blast5 = new Point3D( ( target.X ), ( target.Y+1 ), target.Z );

			Point3D blast1z = new Point3D( ( target.X ), ( target.Y ), target.Z+10 );
			Point3D blast2z = new Point3D( ( target.X-1 ), ( target.Y ), target.Z+10 );
			Point3D blast3z = new Point3D( ( target.X+1 ), ( target.Y ), target.Z+10 );
			Point3D blast4z = new Point3D( ( target.X ), ( target.Y-1 ), target.Z+10 );
			Point3D blast5z = new Point3D( ( target.X ), ( target.Y+1 ), target.Z+10 );

			Point3D blast1w = new Point3D( ( target.X ), ( target.Y ), target.Z );
			Point3D blast2w = new Point3D( ( target.X-2 ), ( target.Y ), target.Z );
			Point3D blast3w = new Point3D( ( target.X+2 ), ( target.Y ), target.Z );
			Point3D blast4w = new Point3D( ( target.X ), ( target.Y-2 ), target.Z );
			Point3D blast5w = new Point3D( ( target.X ), ( target.Y+2 ), target.Z );

			AOS.Damage( target, this, BreathComputeDamage(), physDamage, fireDamage, coldDamage, poisDamage, nrgyDamage );

			if ( form == 1 ) // CRYSTAL DRAGONS -----------------------------------------------------------------------------------------------------
			{
				int bColor = Utility.RandomList( 0x48D, 0x48E, 0x48F, 0x490, 0x491 );
				Effects.SendLocationEffect( blast1, target.Map, 0x3709, 30, 10, bColor, 0 );
				Effects.SendLocationEffect( blast2, target.Map, 0x3709, 30, 10, bColor, 0 );
				Effects.SendLocationEffect( blast3, target.Map, 0x3709, 30, 10, bColor, 0 );
				Effects.SendLocationEffect( blast4, target.Map, 0x3709, 30, 10, bColor, 0 );
				Effects.SendLocationEffect( blast5, target.Map, 0x3709, 30, 10, bColor, 0 );
				target.PlaySound( 0x208 );
				BreathDistance = 3;
			}
			else if ( form == 2 ) // POTIONS THROWN -------------------------------------------------------------------------------------------------
			{
				if ( BreathEffectHue == 0x488 )
				{
					Effects.SendLocationEffect( blast1, target.Map, 0x3709, 30, 10 );
					target.PlaySound( 0x208 );
					target.PlaySound( 0x38D );
				}
				else if ( BreathEffectHue == 0xB92 )
				{
					Effects.SendLocationParticles( EffectItem.Create( blast1, target.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					Effects.PlaySound( target.Location, target.Map, 0x229 );

					if ( !(Server.Items.HiddenTrap.SavingThrow( target, "Poison", false )) )
					{
						switch( Utility.RandomMinMax( 1, 2 ) )
						{
							case 1: target.ApplyPoison( target, Poison.Lesser );	break;
							case 2: target.ApplyPoison( target, Poison.Regular );	break;
						}
					}
					target.PlaySound( 0x38D );
				}
				else if ( form == 0x5B5 )
				{
					Point3D vortex = new Point3D( ( target.X+1 ), ( target.Y+1 ), target.Z );
					Effects.SendLocationEffect( vortex, target.Map, 0x37CC, 30, 10, 0x481, 0 );
					target.PlaySound( 0x10B );
					target.PlaySound( 0x38D );
				}
				else
				{
					target.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
					target.PlaySound( 0x307 );
				}

				this.YellHue = Utility.RandomMinMax( 0, 3 ); // THIS IS USED TO RANDOMIZE POTION TYPES
			}
			else if ( form == 3 ) // DAGGERS OR STARS THROWN ----------------------------------------------------------------------------------------
			{
				if ( target is PlayerMobile )
				{
					Server.Misc.IntelligentAction.CryOut( target );

					Blood blood = new Blood(); blood.MoveToWorld( blast2, this.Map );
						  blood = new Blood(); blood.MoveToWorld( blast3, this.Map );
						  blood = new Blood(); blood.MoveToWorld( blast4, this.Map );
						  blood = new Blood(); blood.MoveToWorld( blast5, this.Map );
				}

				if ( BreathEffectItemID == 0x406C ) // ASSASSIN STAR
				{
					if ( !(Server.Items.HiddenTrap.SavingThrow( target, "Poison", false )) )
					{
						switch( Utility.RandomMinMax( 1, 2 ) )
						{
							case 1: target.ApplyPoison( target, Poison.Lesser );	break;
							case 2: target.ApplyPoison( target, Poison.Regular );	break;
						}
					}
				}
			}
			else if ( form == 4 ) // DINOSAUR ROAR --------------------------------------------------------------------------------------------------
			{
				target.SendMessage( "You are hit by the force of the mighty roar!" );
				target.PlaySound( 0x63F );
				BreathDistance = 5;
			}
			else if ( form == 5 ) // MANTICORE ------------------------------------------------------------------------------------------------------
			{
				target.SendMessage( "You are hit by a manticore thorn!" );
				if ( !(Server.Items.HiddenTrap.SavingThrow( target, "Poison", false )) )
				{
					target.ApplyPoison( target, Poison.Lethal );
				}
				Server.Misc.IntelligentAction.CryOut( target );
			}
			else if ( form == 6 ) // SPIDERS --------------------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x10D3, 30, 10, 0, 0 );
				target.PlaySound( 0x62D );
				double webbed = ((double)(this.Fame/200));
					if ( webbed > 15.0 ){ webbed = 15.0; }
				target.Paralyze( TimeSpan.FromSeconds( webbed ) );
			}
			else if ( form == 7 ) // GIANT STONES AND LOGS ------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x36B0, 30, 10, 0x837, 0 );
				target.PlaySound( 0x664 );
				BreathDistance = 2;
			}
			else if ( form == 8 ) // LARGE SAND BREATH ----------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1w, target.Map, 0x2007, 30, 10, Utility.RandomList( 0xB4D, 0xB4E ), 0 );
				Effects.SendLocationEffect( blast2w, target.Map, 0x2007, 30, 10, Utility.RandomList( 0xB4D, 0xB4E ), 0 );
				Effects.SendLocationEffect( blast3w, target.Map, 0x2007, 30, 10, Utility.RandomList( 0xB4D, 0xB4E ), 0 );
				Effects.SendLocationEffect( blast4w, target.Map, 0x2007, 30, 10, Utility.RandomList( 0xB4D, 0xB4E ), 0 );
				Effects.SendLocationEffect( blast5w, target.Map, 0x2007, 30, 10, Utility.RandomList( 0xB4D, 0xB4E ), 0 );
				target.PlaySound( 0x10B );
				BreathDistance = 3;
			}
			else if ( form == 9 ) // LARGE FIRE BREATH ----------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3709, 30, 10 );
				Effects.SendLocationEffect( blast2, target.Map, 0x3709, 30, 10 );
				Effects.SendLocationEffect( blast3, target.Map, 0x3709, 30, 10 );
				Effects.SendLocationEffect( blast4, target.Map, 0x3709, 30, 10 );
				Effects.SendLocationEffect( blast5, target.Map, 0x3709, 30, 10 );
				target.PlaySound( 0x208 );
				BreathDistance = 3;
			}
			else if ( form == 10 ) // LARGE POISON BREATH -------------------------------------------------------------------------------------------
			{
				if ( Utility.RandomMinMax( 1, 2 ) == 1 )
				{
					Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60 );
					Effects.SendLocationEffect( blast2, target.Map, 0x3400, 60 );
					Effects.SendLocationEffect( blast3, target.Map, 0x3400, 60 );
					Effects.SendLocationEffect( blast4, target.Map, 0x3400, 60 );
					Effects.SendLocationEffect( blast5, target.Map, 0x3400, 60 );
					Effects.PlaySound( target.Location, target.Map, 0x108 );
				}
				else
				{
					Effects.SendLocationParticles( EffectItem.Create( blast1, target.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					Effects.SendLocationParticles( EffectItem.Create( blast2, target.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					Effects.SendLocationParticles( EffectItem.Create( blast3, target.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					Effects.SendLocationParticles( EffectItem.Create( blast4, target.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					Effects.SendLocationParticles( EffectItem.Create( blast5, target.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					Effects.PlaySound( target.Location, target.Map, 0x229 );
				}
				BreathDistance = 3;

				if ( !(Server.Items.HiddenTrap.SavingThrow( target, "Poison", false )) )
				{
					switch( Utility.RandomMinMax( 1, 2 ) )
					{
						case 1: target.ApplyPoison( target, Poison.Greater );	break;
						case 2: target.ApplyPoison( target, Poison.Deadly );	break;
					}
				}
			}
			else if ( form == 11 ) // LARGE RADIATION -----------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60, 0xB96, 0 );
				Effects.SendLocationEffect( blast2, target.Map, 0x3400, 60, 0xB96, 0 );
				Effects.SendLocationEffect( blast3, target.Map, 0x3400, 60, 0xB96, 0 );
				Effects.SendLocationEffect( blast4, target.Map, 0x3400, 60, 0xB96, 0 );
				Effects.SendLocationEffect( blast5, target.Map, 0x3400, 60, 0xB96, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x108 );
				BreathDistance = 3;
			}
			else if ( form == 12 ) // LARGE COLD BREATH ---------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x1A84, 30, 10, 0x9C1, 0 );
				Effects.SendLocationEffect( blast2, target.Map, 0x1A84, 30, 10, 0x9C1, 0 );
				Effects.SendLocationEffect( blast3, target.Map, 0x1A84, 30, 10, 0x9C1, 0 );
				Effects.SendLocationEffect( blast4, target.Map, 0x1A84, 30, 10, 0x9C1, 0 );
				Effects.SendLocationEffect( blast5, target.Map, 0x1A84, 30, 10, 0x9C1, 0 );
				target.PlaySound( 0x10B );
				BreathDistance = 3;
			}
			else if ( form == 13 ) // LARGE ELECTRICAL BREATH ---------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				Effects.SendLocationEffect( blast2, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				Effects.SendLocationEffect( blast3, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				Effects.SendLocationEffect( blast4, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				Effects.SendLocationEffect( blast5, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				target.PlaySound( 0x5C3 );
				BreathDistance = 3;
			}
			else if ( form == 14 ) // TITAN LIGHTNING BOLT ------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				Effects.SendLocationEffect( blast2, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				Effects.SendLocationEffect( blast3, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				Effects.SendLocationEffect( blast4, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				Effects.SendLocationEffect( blast5, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				target.PlaySound( 0x5C3 );
				target.BoltEffect( 0 );
				BreathDistance = 3;
			}
			else if ( form == 15 ) // SPHINX --------------------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x2007, 30, 10, Utility.RandomList( 0xB4D, 0xB4E ), 0 );
				target.PlaySound( 0x10B );
				BreathDistance = 3;
			}
			else if ( form == 16 ) // LARGE STEAM BREATH --------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60, 10, 0x9C4, 0 );
				Effects.SendLocationEffect( blast2, target.Map, 0x3400, 60, 10, 0x9C4, 0 );
				Effects.SendLocationEffect( blast3, target.Map, 0x3400, 60, 10, 0x9C4, 0 );
				Effects.SendLocationEffect( blast4, target.Map, 0x3400, 60, 10, 0x9C4, 0 );
				Effects.SendLocationEffect( blast5, target.Map, 0x3400, 60, 10, 0x9C4, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x108 );
				BreathDistance = 3;
			}
			else if ( form == 17 ) // SMALL FIRE BREATH ---------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3709, 30, 10 );
				target.PlaySound( 0x208 );
			}
			else if ( form == 18 ) // SMALL POISON BREATH -------------------------------------------------------------------------------------------
			{
				if ( Utility.RandomMinMax( 1, 2 ) == 1 )
				{
					Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60 );
					Effects.PlaySound( target.Location, target.Map, 0x108 );
				}
				else
				{
					Effects.SendLocationParticles( EffectItem.Create( blast1, target.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					Effects.PlaySound( target.Location, target.Map, 0x229 );
				}

				if ( !(Server.Items.HiddenTrap.SavingThrow( target, "Poison", false )) )
				{
					switch( Utility.RandomMinMax( 1, 2 ) )
					{
						case 1: target.ApplyPoison( target, Poison.Lesser );	break;
						case 2: target.ApplyPoison( target, Poison.Regular );	break;
					}
				}
				BreathDistance = 2;
			}
			else if ( form == 19 ) // SMALL COLD BREATH ---------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x1A84, 30, 10, 0x9C1, 0 );
				target.PlaySound( 0x10B );
				BreathDistance = 2;
			}
			else if ( form == 20 ) // SMALL ENERGY BREATH -------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				target.PlaySound( 0x5C3 );
				BreathDistance = 2;
			}
			else if ( form == 21 ) // SMALL ENERGY WITH BOLT ----------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
				target.PlaySound( 0x5C3 );
				target.BoltEffect( 0 );
				BreathDistance = 2;
			}
			else if ( form == 22 ) // MISC ELEMENTAL ------------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x36B0, 30, 10, 0x840, 0 );
				target.PlaySound( 0x65A );
			}
			else if ( form == 23 || form == 24 || form == 25 ) // LARGE VOID BREATH -----------------------------------------------------------------
			{
				int color = 0x496;
					if ( form == 24 ){ color = 0x844; }
					else if ( form == 25 ){ color = 0x9C1; }
				Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60, color, 0 );
				Effects.SendLocationEffect( blast2, target.Map, 0x3400, 60, color, 0 );
				Effects.SendLocationEffect( blast3, target.Map, 0x3400, 60, color, 0 );
				Effects.SendLocationEffect( blast4, target.Map, 0x3400, 60, color, 0 );
				Effects.SendLocationEffect( blast5, target.Map, 0x3400, 60, color, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x108 );
				BreathDistance = 3;

				int drain = ((int)(this.Fame/500));

				target.Mana = target.Mana - drain;
					if ( target.Mana < 0 ){ target.Mana = 0; }

				target.Stam = target.Stam - drain;
					if ( target.Stam < 0 ){ target.Stam = 0; }

				target.SendMessage( "You feel your soul draining!" );
			}
			else if ( form == 26 || form == 27 || form == 28 ) // SMALL VOID BREATH -----------------------------------------------------------------
			{
				int color = 0x496;
					if ( form == 27 ){ color = 0x844; }
					else if ( form == 28 ){ color = 0x9C1; }
				Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60, color, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x108 );
				BreathDistance = 2;

				int drain = ((int)(this.Fame/500));

				target.Mana = target.Mana - drain;
					if ( target.Mana < 0 ){ target.Mana = 0; }

				target.Stam = target.Stam - drain;
					if ( target.Stam < 0 ){ target.Stam = 0; }

				target.SendMessage( "You feel your soul draining!" );
			}
			else if ( form == 29 ) // STONE HANDS FROM THE GROUND -----------------------------------------------------------------------------------
			{
				Point3D hands = new Point3D( ( target.X ), ( target.Y ), ( target.Z+5 ) );
				Effects.SendLocationEffect( hands, target.Map, 0x3837, 23, 10, this.Hue, 0 );
				target.PlaySound( 0x65A );
			}
			else if ( form == 30 ) // WATER SPLASH --------------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x1A84, 30, 10, BreathEffectHue, 0 );
				target.PlaySound( 0x026 );
				BreathDistance = 2;
			}
			else if ( form == 31 ) // WATER SPLASH --------------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x1A84, 30, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast2, target.Map, 0x23B2, 16, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast3, target.Map, 0x23B2, 16, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast4, target.Map, 0x23B2, 16, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast5, target.Map, 0x23B2, 16, BreathEffectHue, 0 );
				target.PlaySound( 0x026 );
				BreathDistance = 4;
			}
			else if ( form == 32 ) // SMALL FALLING ICE ---------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x656 );
				BreathDistance = 2;
			}
			else if ( form == 33 ) // BIG FALLING ICE -----------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast2, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast3, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast4, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast5, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x658 );

				BreathDistance = 3;
			}
			else if ( form == 34 ) // LARGE WEED BREATH ---------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60, 0xB97, 0 );
				Effects.SendLocationEffect( blast2, target.Map, 0x3400, 60, 0xB97, 0 );
				Effects.SendLocationEffect( blast3, target.Map, 0x3400, 60, 0xB97, 0 );
				Effects.SendLocationEffect( blast4, target.Map, 0x3400, 60, 0xB97, 0 );
				Effects.SendLocationEffect( blast5, target.Map, 0x3400, 60, 0xB97, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x64F );
				BreathDistance = 3;

				double weed = ((double)(this.Fame/200));
					if ( weed > 15.0 ){ weed = 15.0; }
				target.Paralyze( TimeSpan.FromSeconds( weed ) );
			}
			else if ( form == 35 ) // SMALL WEED BREATH ---------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60, 0xB97, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x64F );
				BreathDistance = 2;

				double weed = ((double)(this.Fame/200));
					if ( weed > 15.0 ){ weed = 15.0; }
				target.Paralyze( TimeSpan.FromSeconds( weed ) );
			}
			else if ( form == 36 ) // ACID SPLASH ---------------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x1A84, 30, 10, BreathEffectHue, 1167 );
				Effects.SendLocationEffect( blast2, target.Map, 0x23B2, 16, BreathEffectHue, 1167 );
				Effects.SendLocationEffect( blast3, target.Map, 0x23B2, 16, BreathEffectHue, 1167 );
				Effects.SendLocationEffect( blast4, target.Map, 0x23B2, 16, BreathEffectHue, 1167 );
				Effects.SendLocationEffect( blast5, target.Map, 0x23B2, 16, BreathEffectHue, 1167 );
				target.PlaySound( 0x026 );
				BreathDistance = 3;
			}
			else if ( form == 37 ) // MUMMY WRAP ----------------------------------------------------------------------------------------------------
			{
				Point3D wrapped = new Point3D( ( target.X ), ( target.Y ), (target.Z+2) );
				Effects.SendLocationEffect( wrapped, target.Map, 0x23AF, 30, 10, 0, 0 );
				target.PlaySound( 0x5D2 );
				target.Paralyze( TimeSpan.FromSeconds( 5.0 ) );
			}
			else if ( form == 38 ) // SMALL STEAM BREATH --------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60, 10, 0x9C4, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x108 );
				BreathDistance = 2;
			}
			else if ( form == 39 ) // SMALL RADIATION -----------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3400, 60, 0xB96, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x108 );
				BreathDistance = 2;
			}
			else if ( form == 40 ) // SMALL SAND BREATH ---------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x2007, 30, 10, Utility.RandomList( 0xB4D, 0xB4E ), 0 );
				target.PlaySound( 0x10B );
				BreathDistance = 2;
			}
			else if ( form == 41 ) // TITAN OF EARTH ATTACK -----------------------------------------------------------------------------------------
			{
				Point3D hands = new Point3D( ( target.X ), ( target.Y ), ( target.Z+5 ) );
				Effects.SendLocationEffect( hands, target.Map, 0x3837, 23, 10, BreathEffectHue, 0 );

				Effects.SendLocationEffect( blast1z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast2z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast3z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast4z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast5z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x658 );

				BreathDistance = 6;
			}
			else if ( form == 42 ) // TITAN OF FIRE ATTACK ------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x3709, 30, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast2, target.Map, 0x3709, 30, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast3, target.Map, 0x3709, 30, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast4, target.Map, 0x3709, 30, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast5, target.Map, 0x3709, 30, 10, BreathEffectHue, 0 );
				target.PlaySound( 0x208 );

				Effects.SendLocationEffect( blast1z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast2z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast3z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast4z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast5z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.PlaySound( target.Location, target.Map, 0x15F );

				BreathDistance = 6;
			}
			else if ( form == 43 ) // TITAN OF WATER ATTACK -----------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1, target.Map, 0x23B2, 16 );
				Effects.SendLocationEffect( blast2, target.Map, 0x23B2, 16 );
				Effects.SendLocationEffect( blast3, target.Map, 0x23B2, 16 );
				Effects.SendLocationEffect( blast4, target.Map, 0x23B2, 16 );
				Effects.SendLocationEffect( blast5, target.Map, 0x23B2, 16 );
				target.PlaySound( 0x026 );

				Effects.SendLocationEffect( blast1z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast2z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast3z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast4z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );
				Effects.SendLocationEffect( blast5z, target.Map, Utility.RandomList( 0x384E, 0x3859 ), 85, 10, BreathEffectHue, 0 );

				BreathDistance = 6;
			}
			else if ( form == 44 ) // TITAN OF AIR ATTACK -----------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast2w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast3w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast4w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast5w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				target.PlaySound( 0x10B );

				if ( target is PlayerMobile && Utility.RandomBool() )
				{
					IMount mount = target.Mount;

					if ( mount != null )
					{
						target.SendLocalizedMessage( 1062315 ); // You fall off your mount!
						Server.Mobiles.EtherealMount.EthyDismount( target, true );
						mount.Rider = null;
					}
					target.Animate( 22, 5, 1, true, false, 0 );
				}
				BreathDistance = 6;
			}
			else if ( form == 45 ) // STAR CREATURE ATTACK ------------------------------------------------------------------------------------------
			{
				if ( Utility.RandomBool() )
				{
					Effects.SendLocationEffect( blast1, target.Map, 0x3709, 30, 10 );
					Effects.SendLocationEffect( blast2, target.Map, 0x3709, 30, 10 );
					Effects.SendLocationEffect( blast3, target.Map, 0x3709, 30, 10 );
					Effects.SendLocationEffect( blast4, target.Map, 0x3709, 30, 10 );
					Effects.SendLocationEffect( blast5, target.Map, 0x3709, 30, 10 );
					target.PlaySound( 0x208 );
				}
				else
				{
					Effects.SendLocationEffect( blast1z, target.Map, 0x2A4E, 30, 10 );
					Effects.SendLocationEffect( blast2z, target.Map, 0x2A4E, 30, 10 );
					Effects.SendLocationEffect( blast3z, target.Map, 0x2A4E, 30, 10 );
					Effects.SendLocationEffect( blast4z, target.Map, 0x2A4E, 30, 10 );
					Effects.SendLocationEffect( blast5z, target.Map, 0x2A4E, 30, 10 );
					target.PlaySound( 0x5C3 );
				}
				BreathDistance = 3;
			}
			else if ( form == 46 ) // LARGE STORM ATTACK --------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast2w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast3w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast4w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast5w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				target.PlaySound( 0x10B );

				Effects.SendLocationEffect( blast1z, target.Map, 0x2A4E, 30, 10 );
				Effects.SendLocationEffect( blast2z, target.Map, 0x2A4E, 30, 10 );
				Effects.SendLocationEffect( blast3z, target.Map, 0x2A4E, 30, 10 );
				Effects.SendLocationEffect( blast4z, target.Map, 0x2A4E, 30, 10 );
				Effects.SendLocationEffect( blast5z, target.Map, 0x2A4E, 30, 10 );
				target.PlaySound( 0x5C3 );

				if ( target is PlayerMobile && Utility.RandomMinMax( 1, 5 ) == 1 )
				{
					IMount mount = target.Mount;

					if ( mount != null )
					{
						target.SendLocalizedMessage( 1062315 ); // You fall off your mount!
						Server.Mobiles.EtherealMount.EthyDismount( target, true );
						mount.Rider = null;
					}
					target.Animate( 22, 5, 1, true, false, 0 );
				}

				BreathDistance = 3;
			}
			else if ( form == 47 ) // AIR BLOWING BREATH --------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast2w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast3w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast4w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				Effects.SendLocationEffect( blast5w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				target.PlaySound( 0x10B );

				if ( target is PlayerMobile && Utility.RandomBool() )
				{
					IMount mount = target.Mount;

					if ( mount != null )
					{
						target.SendLocalizedMessage( 1062315 ); // You fall off your mount!
						Server.Mobiles.EtherealMount.EthyDismount( target, true );
						mount.Rider = null;
					}
					target.Animate( 22, 5, 1, true, false, 0 );
				}
				BreathDistance = 3;
			}
			else if ( form == 48 ) // SMALL AIR BLOWING BREATH --------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				target.PlaySound( 0x10B );

				if ( target is PlayerMobile && Utility.RandomBool() )
				{
					IMount mount = target.Mount;

					if ( mount != null )
					{
						target.SendLocalizedMessage( 1062315 ); // You fall off your mount!
						Server.Mobiles.EtherealMount.EthyDismount( target, true );
						mount.Rider = null;
					}
					target.Animate( 22, 5, 1, true, false, 0 );
				}
				BreathDistance = 2;
			}
			else if ( form == 49 ) // SMALL STAR CREATURE ATTACK ------------------------------------------------------------------------------------
			{
				if ( Utility.RandomBool() )
				{
					Effects.SendLocationEffect( blast1, target.Map, 0x3709, 30, 10 );
					target.PlaySound( 0x208 );
				}
				else
				{
					Effects.SendLocationEffect( blast1w, target.Map, 0x2A4E, 30, 10 );
					target.PlaySound( 0x5C3 );
				}
				BreathDistance = 2;
			}
			else if ( form == 50 ) // SMALL STORM ATTACK --------------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( blast1w, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				target.PlaySound( 0x10B );

				Effects.SendLocationEffect( blast1w, target.Map, 0x2A4E, 30, 10 );
				target.PlaySound( 0x5C3 );

				if ( target is PlayerMobile && Utility.RandomMinMax( 1, 5 ) == 1 )
				{
					IMount mount = target.Mount;

					if ( mount != null )
					{
						target.SendLocalizedMessage( 1062315 ); // You fall off your mount!
						Server.Mobiles.EtherealMount.EthyDismount( target, true );
						mount.Rider = null;
					}
					target.Animate( 22, 5, 1, true, false, 0 );
				}

				BreathDistance = 3;
			}
			else if ( form == 51 ) // SMALL AIR ATTACK -----------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( target.Location, target.Map, 0x2007, 30, 10, 0xB24, 0 );
				target.PlaySound( 0x10B );

				if ( target is PlayerMobile && Utility.RandomMinMax( 1, 5 ) == 1 )
				{
					IMount mount = target.Mount;

					if ( mount != null )
					{
						target.SendLocalizedMessage( 1062315 ); // You fall off your mount!
						Server.Mobiles.EtherealMount.EthyDismount( target, true );
						mount.Rider = null;
					}
					target.Animate( 22, 5, 1, true, false, 0 );
				}
				BreathDistance = 2;
			}
			else if ( form == 52 ) // SMALL UNICORN ATTACK -------------------------------------------------------------------------------------
			{
				Effects.SendLocationEffect( target.Location, target.Map, 0x3039, 30, 10, 0xB71, 0 );
				target.PlaySound( 0x20B );
				BreathDistance = 2;
			}

			if ( BreathDistance > 0 && cycle )
			{
				List<Mobile> targets = new List<Mobile>();

				Map map = this.Map;

				if ( map != null && target != null )
				{
					foreach ( Mobile m in target.GetMobilesInRange( BreathDistance ) )
					{
						if ( m != this && m != target && this.InLOS( m ) && m is PlayerMobile && m.Alive && CanBeHarmful( m ) && !m.Blessed )
							targets.Add( m );
						if ( m != this && m != target && this.InLOS( m ) && m is BaseCreature && m.Alive && CanBeHarmful( m ) && !m.Blessed )
						{
							if ( ((BaseCreature)m).Summoned || ((BaseCreature)m).Controlled )
								targets.Add( m );
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = targets[i];
						DoFinalBreathAttack( m, form, false );
					}
				}
			}
		}

		public virtual int BreathComputeDamage()
		{
			
			double damage = ((double)(DamageMin + DamageMax)/2)/BreathDamageScalar;

			Region reg = Region.Find( this.Location, this.Map );
			if ( reg.Name == "the Land of Sosaria"  )
				damage /= 1.5;

			if ( IsParagon )
				damage *= 2;
			
			AdventuresFunctions.DiminishingReturns((int)damage, 250, 8);

			return (int)damage;
		}

		#endregion

        #region Jako Taming
        private uint m_level = 1;
        private uint m_realLevel = 1;
        private uint m_experience = 0;
        private uint m_maxLevel = (uint)Utility.RandomMinMax(8, 17);
        private uint m_traits = 0;
        private DateTime m_nextMate = DateTime.UtcNow;
        protected virtual TimeSpan NextMateDelay(uint atLevel) 
			{ 
			if ( Female ){
				return (atLevel == AbsMaxLevel ? TimeSpan.FromDays(7) : TimeSpan.FromDays(3)); }
			else {
				return (atLevel == AbsMaxLevel ? TimeSpan.FromDays(3) : TimeSpan.FromDays(2)); }			
			}
			
        public virtual uint ExpNeeded(uint atLevel) 
		{ 
			double baseexp = 1;
			
			if (this.Fame < 80)
				baseexp = 1;
			else
				baseexp = (double)this.Fame/80;
			
			uint baseexpuint = Convert.ToUInt32(baseexp);
			
			return (uint)(baseexpuint * Math.Pow(atLevel, 2) + 50); 
		}
		
        public virtual uint TraitsGiven(uint atLevel) { return (atLevel == 10) ? (uint)3 : (uint)1; }
        public string SexString { get { return (Female ? "Female" : "Male"); } }
        public JakoAttributes m_jakoAttributes = new JakoAttributes();


        #region GM Commands & Getters/Setters

        [CommandProperty(AccessLevel.GameMaster)]
        public JakoAttributes JakoAttributes
        {
            get { return m_jakoAttributes; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public uint Level
        {
            get { return m_level; }
            set { setLevel(value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public uint RealLevel
        {
            get { return m_realLevel; }
            set { m_realLevel = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public uint MaxLevel
        {
            get { return m_maxLevel; }
            set { m_maxLevel = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual uint AbsMaxLevel
        {
            get { return 55; }
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public uint Experience
        {
            get { return m_experience; }
            set { m_experience = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public uint Traits
        {
            get { return m_traits; }
            set { m_traits = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextMate
        {
            get { return m_nextMate; }
            set { m_nextMate = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int MatingLevel
        {
            get { return 10; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool JakoIsEnabled { get { return true; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual double ExpDecayPerc { get { return .05; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual uint ExpGiven 
		{ get 
			{ 
				double exper = (Math.Abs((double)Fame/35)) * (m_Loyalty / MaxLoyalty);
				uint experu = Convert.ToUInt32(exper);
				return experu; 
			} 
		}
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual uint ExpToNextLevel { get { return ExpNeeded(m_level); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual TimeSpan NextMateIn { get { return NextMateDelay(m_level); } }
        #endregion
        /// <summary>
        /// Informs both the friends of the creature and the owner of a pet's death by calling DeathNitification(controller,friend).  Overridable to change defaults.</summary>
        public virtual void DeathNotification()
        {
            DeathNotification(true, true);
        }

        /// <summary>
        /// Preforms the SendMessage notification informing the controller/friends that this pet is dead.</summary>
        /// <param name="controller">Inform the ControlMaster of the death.</param>
        /// <param name="friend">Inform the Friend(s) of the death.</param>
        public virtual void DeathNotification(bool controller, bool friend)
        {
            if (controller && ControlMaster != null)
	    {
		if (this is HenchmanFighter || this is HenchmanArcher || this is HenchmanWizard || this is HenchmanMonster)
		{
		    ControlMaster.SendMessage("Your henchman {0} has died!", Name);
		}
		else if (this is Squire)
		{
		    ControlMaster.SendMessage("Your squire {0} has died!", Name);
		}
		else
		{
		    ControlMaster.SendMessage("Your pet {0} has died!", Name);
		}
	    }

            if (!friend || Friends == null)
                return;
            foreach (PlayerMobile f in Friends)
            {
		if (this is HenchmanFighter || this is HenchmanArcher || this is HenchmanWizard || this is HenchmanMonster)
		{
		    f.SendMessage("Your friend {0}'s henchman {1} has died!", ControlMaster, Name);
		}
		else if (this is Squire)
		{
		    f.SendMessage("Your friend {0}'s squire {1} has died!", ControlMaster, Name);
		}
		else
		{
		    f.SendMessage("Your friend {0}'s pet {1} has died!", ControlMaster, Name);
		}
            }
        }

        /// <summary>
        /// Increase the Pet's experience and tells owner.  If above exp to next level, increase the level.</summary>
        /// <param name="exp">The amount of Experience Gained.</param>
        public virtual void GainExp(Mobile killed, uint exp)
        {
            GainExp(killed, exp, true);
        }

        /// <summary>
        /// Increase the Pet's experience.  If above exp to next level, increase the level.</summary>
        /// <param name="killer">The Mobile who was killed.</param>
        /// <param name="exp">The amount of Experience Gained.</param>
        /// <param name="tellOwner">SendMessage to Owner about the changes.</param>
        public virtual void GainExp(Mobile killed, uint exp, bool tellOwner)
        {
			if (this.Summoned || this is HenchmanArcher || this is HenchmanFighter || this is HenchmanMonster || this is HenchmanWizard || this is Squire || !this.JakoIsEnabled || (killed is BaseCreature && (((BaseCreature)killed).Controlled && ((BaseCreature)killed).ControlMaster != null) || !((BaseCreature)killed).JakoIsEnabled) || Level == MaxLevel)
				return;
				
			if (tellOwner && ControlMaster != null)
				ControlMaster.SendMessage("Your pet {0} has gained {1} experience!", Name, exp);
			if ((m_experience + exp) < ExpToNextLevel)
			{
				m_experience += exp;
				return;
			}
			else if ((m_experience + exp) > ExpToNextLevel)
			{
				uint oldExp = setLevel(m_level + 1, tellOwner);
				m_experience = oldExp + exp - ExpNeeded(m_level - 1);
			}
        }

        /// <summary>
        /// Decrease's Pet's Experience and inform the user.  If the exp is less then the current earned, decrease the level.</summary>
        /// <param name="exp">The last mobile to kill this.</param>
        /// <param name="exp">The amount of Experience Lost.</param>
        public virtual void LoseExp(Mobile killer, uint exp)
        {
            LoseExp(killer, exp, true);
        }

        /// <summary>
        /// Decrease's Pet's Experience.  If the exp is less then the current earned, decrease the level.</summary>
        /// <param name="exp">The amount of Experience Lost.</param>
        /// <param name="tellOwner">SendMessage to Owner about the loss.</param>
        public virtual void LoseExp(Mobile killer, uint exp, bool tellOwner)
        {
            if (killer == null || killer is PlayerMobile || this is HenchmanArcher || this is HenchmanFighter || this is HenchmanMonster || this is HenchmanWizard || this is Squire || !this.JakoIsEnabled || (killer is BaseCreature && ((BaseCreature)killer).Controlled && ((BaseCreature)killer).ControlMaster != null || !((BaseCreature)killer).JakoIsEnabled))
                return;

            if (tellOwner && ControlMaster != null)
                ControlMaster.SendMessage("Your pet {0} has lost {1} experience!", Name, exp);
            if (exp < m_experience)
            {
                m_experience -= exp;
                return;
            }

            if (m_level == 1)
            {
                m_experience = 0;
                return;
            }

            uint oldExp = setLevel(m_level - 1, tellOwner);
            m_experience = oldExp + ExpNeeded(m_level + 1) - exp;
        }

        /// <summary>
        /// Calculate the distrubution of attackers and ExpGiven, and increase the Pet's Exp.</summary>
        /// <param name="m">The creature to calcaulte the EXP From (typically dead).</param>
        private void CalculateExpDist(Mobile m)
        {
            if (!(m is BaseCreature) || ((BaseCreature)m).ExpGiven == 0 || !((BaseCreature)m).JakoIsEnabled || ((BaseCreature)m).Summoned)
                return;

			double herdingbonus = 1;
			if (this.Controlled && this.ControlMaster != null)
			{
				if (this.ControlMaster is PlayerMobile)
				{	
					PlayerMobile owner = (PlayerMobile)this.ControlMaster;
					herdingbonus += owner.Skills[SkillName.Herding].Value / 500; // max of 25%
				}
			}

            List<DamageEntry> rights = m.DamageEntries;
            foreach (DamageEntry entry in rights)
            {
                if (entry.Damager is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)entry.Damager;
                    if (bc.Controlled == true && bc.ControlMaster != null)
					{
						uint exp = (uint)( (double)(((BaseCreature)m).ExpGiven / rights.Count) * herdingbonus);
						Region region = Region.Find( this.Location, this.Map );

						if ( region.IsPartOf( typeof( ChampionSpawnRegion ) )|| region is ChampionSpawnRegion ) 						
						{
							exp /= 3;
						}	
						bc.GainExp(m, exp, true);
					}
                        
                }
            }

        }

        /// <summary>
        /// Decrease the Experience by ExpDecayPerc.  This function is called OnBeforeDeath.</summary>
        /// <returns>The amount of experience the mobile had before the change.</returns>
        public virtual void DecayExperience(Mobile killer)
        {
            uint expLost = (uint)(m_experience * ExpDecayPerc);
            LoseExp(killer, expLost);
        }

        /// <summary>
        /// Increase the traits of the Pet, and informs the owner.</summary>
        /// <param name="bonus">The number of traits to increase by.</param>
        public void increaseTraits(uint bonus)
        {
            increaseTraits(bonus, true);
        }

        /// <summary>
        /// Increases the traits of the Pet.</summary>
        /// <param name="bonus">The number of traits to increase by.</param>
        /// <param name="tellOwner">SendMessage to Owner about the loss.</param>
        public void increaseTraits(uint bonus, bool tellOwner)
        {
            if (tellOwner && ControlMaster != null)
                ControlMaster.SendMessage("Your pet has gained {0} trait{1}!", bonus, (bonus == 1 ? "" : "s"));
            m_traits += bonus;
        }

        /// <summary>
        /// Sets the Mobile's Level to the level given and resets the current Experience and tells the owner.  Increases the traits correctly.</summary>
        /// <param name="newLevel">The level the mobile will now be.</param>
        /// <returns>The amount of experience the mobile had before the change.</returns>        
        public uint setLevel(uint newLevel)
        {
            return setLevel(newLevel, true);
        }

        /// <summary>
        /// Sets the Mobile's Level to the level given and resets the current Experience.</summary>
        /// <param name="newLevel">The level the mobile will now be.</param>
        /// <param name="tellOwner">SendMessage to Owner about the loss.</param>
        /// <returns>The amount of experience the mobile had before the change.</returns>
        public uint setLevel(uint newLevel, bool tellOwner)
        {
            uint oldExp = m_experience;
            if (newLevel < m_level)
            {
				
                if (tellOwner && ControlMaster != null)
                    ControlMaster.SendMessage("Your pet has decreased in level!");
                m_level = newLevel;

            }
            else if (newLevel > m_realLevel)
            {
                for (uint x = m_level + 1; x <= newLevel && x <= m_maxLevel; x++)
                {
                    increaseTraits(TraitsGiven(x), false);
                }

				if (m_realLevel >= 5 && !this.IsBonded)
					this.IsBonded = true;
				
				if (m_realLevel < 25 && newLevel >= 25 && this.ControlSlots >= 2)
					this.ControlSlots -= 1;

				if (m_realLevel < 50 && newLevel >= 50 && this.ControlSlots >= 2)
					this.ControlSlots -= 1;

				if (Utility.RandomDouble() <= ((double)newLevel / 80) && this.MinTameSkill > 5)
					this.MinTameSkill -= 1;

				int intlevel =  Convert.ToInt32(newLevel);
				double levelodds = ((double)intlevel / 200);
				if  ( intlevel > 10 && Utility.RandomDouble() < levelodds && Utility.RandomMinMax(1, ( (int)(200 / Convert.ToInt32(newLevel)) )  ) == 3 && Utility.RandomDouble() > 0.98 && m_special == 0 && !Body.IsHuman )
				//((Convert.ToDouble(newLevel) / 8000) > Utility.RandomDouble() && Utility.RandomBool() && m_special == 0 && !Body.IsHuman)//Special added! this doesn't work since it gives specials when newlevel is 0-10
				{

					ControlMaster.SendMessage("Your pet has gained a new special ability!");

					bool newspecialgiven = false;
					while (!newspecialgiven) 
					{
						switch (Utility.RandomMinMax( 1, 9 ))
						{
							case 1:
							{
								m_special = 1;
								ControlMaster.SendMessage("It has learnt to be more ferocious."); 
								newspecialgiven = true;
							}
							break;
							case 2:
							{
								m_special = 2;
								ControlMaster.SendMessage("It has been granted the regenerative trait."); 
								newspecialgiven = true;
							}
							break;
							case 3:
							{
								m_special = 3;
								ControlMaster.SendMessage("It has been granted the mystical trait.");
								newspecialgiven = true;
							}
							break;
							case 4:
							{
								m_special = 4;
								ControlMaster.SendMessage("It has been granted the firebreather trait.");
								newspecialgiven = true;
							}
							break;
							case 5:
							{
								m_special = 5;
								ControlMaster.SendMessage("It has been granted Thor's touch.");
								newspecialgiven = true;
							}
							break;
							case 6:
							{
								m_special = 6;
								ControlMaster.SendMessage("It has been granted the tactical trait.");
								newspecialgiven = true;
							}
							break;
							case 7:
							{
								m_special = 7;
								ControlMaster.SendMessage("It has been granted the shamanic trait.");
								newspecialgiven = true;
							}
							break;
							case 8:
							{
								m_special = 8;
								ControlMaster.SendMessage("It has been granted the chaotic trait.");
								newspecialgiven = true;
							}
							break;
							case 9:
							{
								m_special = 9;
								ControlMaster.SendMessage("It has been granted the poisonous trait.");
								newspecialgiven = true;

								Skill skill = Skills[SkillName.Poisoning];
								if (skill.Base == 0)
									SetSkill(SkillName.Poisoning, 10, 30);
							}
							break;
						}
						if (newspecialgiven)
							break;
					}
				}

                m_level = m_realLevel = newLevel;
            }

            Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x20F6, 10, 5, 5023);
            if (tellOwner && ControlMaster != null)
                ControlMaster.SendMessage("Your pet is now level {0}.", newLevel);
            m_experience = 0;
            InvalidateProperties();

			if (this.Level != null && ControlMaster != null)
			{
				if (this.ControlMaster is PlayerMobile)
				{
					int amount = Convert.ToInt32(this.Level) * Utility.RandomMinMax(1,4);
					double difficulty = this.MinTameSkill * ((double)this.Loyalty/100);

					while (amount > 0)
					{
						ControlMaster.CheckTargetSkill( SkillName.AnimalTaming, this, difficulty - 25.0, difficulty + 25.0 );
						amount -= 1;
					}
				}
			}

            return oldExp;
        }
        #endregion

		#region Spill Acid

		public void SpillAcid( int Amount )
		{
			SpillAcid( null, Amount );
		}

		public void SpillAcid( Mobile target, int Amount )
		{
			if ( (target != null && target.Map == null) || this.Map == null )
				return;

			for ( int i = 0; i < Amount; ++i )
			{
				Point3D loc = this.Location;
				Map map = this.Map;
				Item acid = NewHarmfulItem();

				if ( target != null && target.Map != null && Amount == 1 )
				{
					loc = target.Location;
					map = target.Map;
				} 
				else
				{
					bool validLocation = false;
					for ( int j = 0; !validLocation && j < 10; ++j )
					{
						loc = new Point3D(
							loc.X+(Utility.Random(0,3)-2),
							loc.Y+(Utility.Random(0,3)-2),
							loc.Z );
						loc.Z = map.GetAverageZ( loc.X, loc.Y );
						validLocation = map.CanFit( loc, 16, false, false ) ;
					}
				}
				acid.MoveToWorld( loc, map );
			}
		}

		/*
			Solen Style, override me for other mobiles/items:
			kappa+acidslime, grizzles+whatever, etc.
		*/

		public virtual Item NewHarmfulItem()
		{
			return new PoolOfAcid( TimeSpan.FromSeconds(10), 30, 30 );
		}

		#endregion

		#region Flee!!!
		private DateTime m_EndFlee;

		public DateTime EndFleeTime
		{
			get{ return m_EndFlee; }
			set{ m_EndFlee = value; }
		}

		public virtual void StopFlee()
		{
			m_EndFlee = DateTime.MinValue;
		}

		public virtual bool CheckFlee()
		{
			if ( m_EndFlee == DateTime.MinValue )
				return false;

			if ( DateTime.UtcNow >= m_EndFlee )
			{
				StopFlee();
				return false;
			}

			return true;
		}

		public virtual void BeginFlee( TimeSpan maxDuration )
		{
			m_EndFlee = DateTime.UtcNow + maxDuration;
		}

		#endregion

		public BaseAI AIObject{ get{ return m_AI; } }

		public const int MaxOwners = 5;

		public virtual OppositionGroup OppositionGroup
		{
			get{ return null; }
		}

		#region Friends
		public List<Mobile> Friends { get { return m_Friends; } }

		public virtual bool AllowNewPetFriend
		{
			get{ return ( m_Friends == null || m_Friends.Count < 5 ); }
		}

		public virtual bool IsPetFriend( Mobile m )
		{
			return ( m_Friends != null && m_Friends.Contains( m ) );
		}

		public virtual void AddPetFriend( Mobile m )
		{
			if ( m_Friends == null )
				m_Friends = new List<Mobile>();

			m_Friends.Add( m );
		}

		public virtual void RemovePetFriend( Mobile m )
		{
			if ( m_Friends != null )
				m_Friends.Remove( m );
		}

		public virtual bool IsFriend( Mobile m )
		{
			OppositionGroup g = this.OppositionGroup;

			if ( g != null && g.IsEnemy( this, m ) )
				return false;

			if ( !(m is BaseCreature) )
				return false;

			BaseCreature c = (BaseCreature)m;

			return ( m_iTeam == c.m_iTeam && ( (m_bSummoned || m_bControlled) == (c.m_bSummoned || c.m_bControlled) )/* && c.Combatant != this */);
		}

		#endregion

		public double Agility() //for dodge and parry
		{
			double calc = 0;
			double resists = ( ((double)this.PoisonResistSeed + (double)this.PhysicalResistanceSeed + (double)this.FireResistSeed + (double)this.ColdResistSeed + (double)this.EnergyResistSeed) );
			// logic here is that for mobs, more resists = more armored = less agile

			calc = (resists / ( ((double)Str/3) + ((double)Dex) ) ); 
			//this will be all over the place - mobs can have 100-600 dex

			if (calc > 1 )
				calc = 0.99;

			calc = 1 - calc;
			return calc;
		}

		public virtual bool IsEnemy( Mobile m )
		{
		
			//adding special here for animate dead cast by mage ai mobs
			if (this is SummonedCorpse && ((SummonedCorpse)this).MobSummon && ( m is PlayerMobile || ( m is BaseCreature && ((BaseCreature)m).Controlled && ((BaseCreature)m).ControlMaster is PlayerMobile ) ) )
				return true;
			else if (this is SummonedCorpse && ((SummonedCorpse)this).MobSummon)
				return false;

			if (Controlled && !m_goferal && m is BaseCreature && !((BaseCreature)m).GoFeral && ((BaseCreature)m).ControlMaster == ControlMaster && ((BaseCreature)m).Combatant != this )
				return false;
			
			if (AdventuresFunctions.IsPuritain((object)this))
			{
				if (this.midrace > 0)
					return IntelligentAction.RaceCheck(this, m);
				if (Summoned && SummonMaster != null) 
					return IntelligentAction.RaceCheck(SummonMaster, m);
				if (Controlled && ControlMaster!= null)
					return IntelligentAction.RaceCheck(ControlMaster, m);
			} 

			if ( (this is BaseBlue || this is BaseRed || this is BasePerson || this is BaseVendor) && !m.Criminal && ( DisguiseTimers.IsDisguised( m ) || Server.Spells.Fifth.IncognitoSpell.m_Timers.Contains(m)))
				return false;
				
			Region reg = Region.Find( this.Location, this.Map );

			if ( reg.IsPartOf( "the Basement" ))
				return false;

			if (this is GraveDigger && (m is PlayerMobile || m is BaseCreature) )
				return false;
			
			if (m is GraveDigger)
				return false;
			
			if ( m is PlayerMobile )
			{
				/*if ( ((PlayerMobile)m).Profession == 1 )
				{
					m.Criminal = true;
					if ( m.Kills < 1 ){ m.Kills = 1; }
				}*/

				if ((this is DoomedLich || this is LichKing || this is Lich) && m.Name == "Slippery Pete" && Combatant != m)
					return false;

				SlayerEntry undead_creatures = SlayerGroup.GetEntryByName( SlayerName.Silver );
				if ( undead_creatures.Slays(this) )
				{
					Item item = m.FindItemOnLayer( Layer.Helm );
					if ( item is DeathlyMask )
					{
						return false;
					}
				}
			}

			if ( WhisperHue == 999 && Hidden && m is PlayerMobile && !Server.Mobiles.BasePirate.IsSailor( this ) ) // SURFACE FROM WATER AND ATTACK
			{
				this.Home = this.Location; // SO THEY KNOW WHERE TO GO BACK TO

				if ( m.Z < 0 ) // JUMP NEAR A BOAT
				{
					Point3D loc = Server.Misc.Worlds.GetBoatWater( m.X, m.Y, m.Map, 4 );
					this.Location = loc;
					this.PlaySound( 0x026 );
					Effects.SendLocationEffect( this.Location, this.Map, 0x23B2, 16 );
				}
				else if ( !(CanOnlyMoveOnSea( this )) ) // JUMP OUT OF WATER AND WALK TO SHORE
				{
					this.PlaySound( 0x026 );
					Effects.SendLocationEffect( this.Location, this.Map, 0x23B2, 16 );
				}
				this.Warmode = true;
				this.Combatant = m;
				this.CantWalk = false;	if ( !(CanOnlyMoveOnSea( this )) ){ this.CantWalk = true; }
				this.CanSwim = true;
				this.Hidden = false;
				return true;
			}

			// DRACULA ISLAND SPECIAL REACTIONS
			SlayerEntry undead = SlayerGroup.GetEntryByName( SlayerName.Silver );
			SlayerEntry exorcism = SlayerGroup.GetEntryByName( SlayerName.Exorcism );
			if ( reg.IsPartOf( typeof( NecromancerRegion ) ) && this.ControlSlots != 666 && ( m is BaseVendor || GetPlayerInfo.EvilPlayer( m ) ) && (this is Bat || this is DiseasedRat || this is DarkHound || undead.Slays(this) || exorcism.Slays(this) || this is EvilMage) )
				return false;
			
			// region FINAL edits 
			
			if ( reg.IsPartOf( "Blood Dungeon" ) && m is PlayerMobile && m.Guild != null && !m.Guild.Disbanded && ( m.Guild.Abbreviation == "BEAK" || m.Guild.Abbreviation == "beak" || m.Guild.Abbreviation == "[BEAK]" || m.Guild.Abbreviation == "[beak]" ) && this.Combatant != m)
			{
				if (this is BloodDemigod)
					Say ("We server the same Master, bow before me Mortal.");
				
				return false;
			}

			if ( this.Map == Map.Ilshenar && this.X <= 1007 && this.Y <= 1280) // Final darkmoor has different enemy rules
			{
				if ( m is BaseCreature && ((BaseCreature)m).ControlMaster != null && m.Combatant != this)
					{
					Mobile owner = ((BaseCreature)m).ControlMaster;
					if ( this.Karma < 0 && owner is PlayerMobile && owner.Karma < 0)
						return false;
					else if (this.Karma >= 0 && owner is PlayerMobile && owner.Karma >= 0)
						return false;
					else 
						return true;
					} 
				else if (this.Karma > 25)
				{
					if (m.Karma >= 0)
						return false;
					else 
						return true;
				}
				else if (this.Karma < 0)
				{
					if (m.Karma <= 0)
						return false;
					else if (m.Karma > 25)
						return true;
					else 
						return false;
				}
				else
					return false;
			}
			
			if (m is BaseBlue && this.Karma < 0) { return true; }

			if (m is BaseRed && !(this is BaseRed) && this.Karma < 0) { return true; }
			
			if (m is BaseCursed && this.Karma < -5000) { return false; }
			
			if (m is BaseChild && this.Karma < 0 && !(this is BaseVendor) && !(this is Citizens) && !(this is Townsperson) && !(reg.IsPartOf( typeof( TownRegion ) ))) { return true; }
			//end
	
			Mobile mastah = m;

			if (m is BaseCreature && ((BaseCreature)m).Controlled && ((BaseCreature)m).ControlMaster != null)
				mastah = ((BaseCreature)m).ControlMaster;
	

			if ( !(this is BasePerson) && !(this is Citizens) && !(this is BaseVendor) && this.Karma > 100 && mastah.Karma > 1000) 
				{return false;}
			if (!(this is BasePerson) && !(this is Citizens) && !(this is BaseVendor) && this.Karma > 100 && mastah.Karma < 1000) {return true;}			

			
			OppositionGroup g = this.OppositionGroup;

			if ( g != null && g.IsEnemy( this, m ) )
				return true;
			
			if (!(m is BaseCreature))
				return true;

			BaseCreature c = (BaseCreature)m;

			return ( m_iTeam != c.m_iTeam || ( (m_bSummoned || m_bControlled) != (c.m_bSummoned || c.m_bControlled) )/* || c.Combatant == this*/ );
		}

		public static bool AlwaysInvulnerable( Mobile m )
		{
			if ( m is PackBear ){ return true; }
			else if ( m is PackMule ){ return true; }
			else if ( m is PackStegosaurus ){ return true; }
			else if ( m is PackTurtle ){ return true; }
			else if ( m is HenchmanFamiliar ){ return true; }
			else if ( m is AerialServant ){ return true; }
			else if ( m is PackBeast ){ return true; }
			else if ( m is FrankenPorter ){ return true; }
			else if ( m is GolemPorter ){ return true; }
			else if ( m is EtherealDealer ){ return true; }
			else if ( m is TavernPatronEast ){ return true; }
			else if ( m is TavernPatronNorth ){ return true; }
			else if ( m is TavernPatronSouth ){ return true; }
			else if ( m is TavernPatronWest ){ return true; }
			else if ( m is AdventurerEast ){ return true; }
			else if ( m is AdventurerNorth ){ return true; }
			else if ( m is AdventurerSouth ){ return true; }
			else if ( m is AdventurerWest ){ return true; }
			else if ( m is Citizens ){ return true; }
			else if ( m is EpicPet ){ return true; }
			else if ( m is EpicCharacter ){ return true; }
			else if ( m is DeathKnightDemon ){ return true; }
			else if ( m is DraculaBride ){ return true; }
			else if ( m is GodOfLegends ){ return true; }
			else if ( m is NecroGreeter ){ return true; }
			else if ( m is Priest ){ return true; }
			else if ( m is BaseNPC ){ return true; }
			else if ( m is GauntletMaster ){ return true; }
			else if ( m is CloneCharacterOnLogout.CharacterClone ){ return true; }
			else if ( m is DoomVarietyDealer ){ return true; }

			return false;
		}

		public static bool CanOnlyMoveOnSea( Mobile m )
		{
			if ( m is Dolphin ){ return true; }
			else if ( m is Basilosaurus ){ return true; }
			else if ( m is GreatWhite ){ return true; }
			else if ( m is Lochasaur ){ return true; }
			else if ( m is Megalodon ){ return true; }
			else if ( m is SeaHorses ){ return true; }
			else if ( m is Shark ){ return true; }
			else if ( m is Calamari ){ return true; }
			else if ( m is GiantEel ){ return true; }
			else if ( m is GiantLamprey ){ return true; }
			else if ( m is GiantSquid ){ return true; }
			else if ( m is Kraken ){ return true; }
			else if ( m is Leviathan ){ return true; }
			else if ( m is Slitheran ){ return true; }
			else if ( m is DeepSeaSerpent ){ return true; }
			else if ( m is Jormungandr ){ return true; }
			else if ( m is SeaSerpent ){ return true; }
			else if ( m is OilSlick ){ return true; }
			else if ( m is Cronosaurus ){ return true; }
			else if ( m is RottingSquid ){ return true; }

			return false;
		}

		public override string ApplyNameSuffix( string suffix )
		{
			if ( IsParagon )
			{
				if ( suffix.Length == 0 )
					suffix = "(cursed)";
				else
					suffix = String.Concat( suffix, " (cursed)" );
			}

			return base.ApplyNameSuffix( suffix );
		}

		public virtual bool CheckControlChance( Mobile m )
		{
			if ( GetControlChance( m ) > Utility.RandomDouble() )
			{
				if (Utility.RandomDouble() > 0.85 && Controlled && ControlMaster != null && ControlMaster is PlayerMobile)
				{
					PlayerMobile pm = (PlayerMobile)(ControlMaster);

					pm.CheckSkill( SkillName.Herding, MinTameSkill - 25, MinTameSkill + 25 );
				}
				//Loyalty += 1;
				return true;
			}

			PlaySound( GetAngerSound() );

			if ( Body.IsAnimal )
				Animate( 10, 5, 1, true, false, 0 );
			else if ( Body.IsMonster )
				Animate( 18, 5, 1, true, false, 0 );

			Loyalty -= 3;
			return false;
		}

		public virtual bool CanBeControlledBy( Mobile m )
		{
			return ( GetControlChance( m ) > 0.0 );
		}

		public double GetControlChance( Mobile m )
		{
			return GetControlChance( m, false );
		}

		public virtual double GetControlChance( Mobile m, bool useBaseSkill )
		{
			if ( m_dMinTameSkill <= 29.1 || m_bSummoned || m.AccessLevel >= AccessLevel.GameMaster )
				return 1.0;

			double dMinTameSkill = m_dMinTameSkill;

			if ( dMinTameSkill > -24.9 && Server.SkillHandlers.AnimalTaming.CheckMastery( m, this ) )
				dMinTameSkill = -24.9;

			int taming = (int)((useBaseSkill ? m.Skills[SkillName.AnimalTaming].Base : m.Skills[SkillName.AnimalTaming].Value ) * 10);
			int lore = (int)((useBaseSkill ? m.Skills[SkillName.AnimalLore].Base : m.Skills[SkillName.AnimalLore].Value )* 10);
			int bonus = 0, chance = 700;

			if( Core.ML )
			{
				int SkillBonus = taming - (int)(dMinTameSkill * 10);
				int LoreBonus = lore - (int)(dMinTameSkill * 10);

				int SkillMod = 6, LoreMod = 6;

				if( SkillBonus < 0 )
					SkillMod = 28;
				if( LoreBonus < 0 )
					LoreMod = 14;

				SkillBonus *= SkillMod;
				LoreBonus *= LoreMod;

				bonus = (SkillBonus + LoreBonus ) / 2;
			}
			else
			{
				int difficulty = (int)(dMinTameSkill * 10);
				int weighted = ((taming * 4) + lore) / 5;
				bonus = weighted - difficulty;

				if ( bonus <= 0 )
					bonus *= 14;
				else
					bonus *= 6;
			}

			chance += bonus;

			if ( chance >= 0 && chance < 200 )
				chance = 200;
			else if ( chance > 990 )
				chance = 990;

			chance -= (MaxLoyalty - m_Loyalty) * 10;

			return ( (double)chance / 1000 );
		}

		private static Type[] m_AnimateDeadTypes = new Type[]
			{
				typeof( HellSteed ), typeof( SkeletalMount ),
				typeof( WailingBanshee ), typeof( Wraith ), typeof( SkeletalDragon ),
				typeof( LichLord ), typeof( FleshGolem ), typeof( Lich ),
				typeof( SkeletalKnight ), typeof( BoneKnight ), typeof( Mummy ),
				typeof( SkeletalMage ), typeof( BoneMagi )
			};

		public virtual bool IsAnimatedDead
		{
			get
			{
				if ( this is SummonedCorpse )
					return true;

				return false;
			}
		}

		public virtual bool IsNecroFamiliar
		{
			get
			{
				if ( !Summoned )
					return false;

				if ( m_ControlMaster != null && SummonFamiliarSpell.Table.Contains( m_ControlMaster ) )
					return SummonFamiliarSpell.Table[ m_ControlMaster ] == this;

				return false;
			}
		}

		public override void Damage( int amount, Mobile from )
		{ //damage received
			int oldHits = this.Hits;

			if ( Core.AOS && !this.Summoned && this.Controlled && 0.2 > Utility.RandomDouble() )
			{
				amount = (int)(amount * BonusPetDamageScalar);
				if (from is BaseCreature && ((BaseCreature)from).Special == 1) // new pet abilities
					amount =(int)((double)amount* 1.25);
			}

			if ( from is BaseCreature && !((BaseCreature)from).Summoned && !((BaseCreature)from).Controlled && IsPet( this ) && MyServerSettings.DamageToPets() > 1.0 )
			{
				amount = (int)(amount * MyServerSettings.DamageToPets());
			}

			if ( from is BaseCreature && !((BaseCreature)from).Summoned && !((BaseCreature)from).Controlled && IsPet( this ) && MyServerSettings.CriticalToPets() >= Utility.RandomMinMax( 1, 100 ) )
			{
				amount = amount * 2;
			}

			if ( Spells.Necromancy.EvilOmenSpell.TryEndEffect( this ) )
				amount = (int)(amount * 1.25);

			Mobile oath = Spells.Necromancy.BloodOathSpell.GetBloodOath( from );

			if ( oath == this )
			{
				amount = (int)(amount * 1.1);
				from.Damage( amount, from );
			}

			base.Damage( amount, from );

			if ( SubdueBeforeTame && !Controlled )
			{
				if ( (oldHits > (this.HitsMax / 10)) && (this.Hits <= (this.HitsMax / 10)) )
					PublicOverheadMessage( MessageType.Regular, 0x3B2, false, "* The creature has been beaten into subjugation! *" );
			}
		}

		public virtual bool DeleteCorpseOnDeath
		{
			get
			{
				return m_bSummoned;
			}
		}

		public override void SetLocation( Point3D newLocation, bool isTeleport )
		{
			base.SetLocation( newLocation, isTeleport );

			if ( isTeleport && m_AI != null )
				m_AI.OnTeleported();
		}

		public override void OnBeforeSpawn( Point3D location, Map m )
		{
			if ( ( Paragon.CheckConvert( this, location, m ) ) && ( this.Karma < -999 ) && ( this.EmoteHue != 123 ) && !( this.Region is GargoyleRegion ) && !( this.Region.IsPartOf( "the Castle of the Black Knight" ) ) )
				IsParagon = true;

			base.OnBeforeSpawn( location, m );
		}

		public static void BeefUp( BaseCreature bc, int up )
		{
			int HitPointIncrease = up;

			// WE DON'T WANT THE VERY POWERFUL CREATURES TO BE IMPOSSIBLE SO WE CAP THEM BASED ON FAME
			if ( bc.Fame >= 20000 ){ up = 0; }
			else if ( bc.Fame >= 18000 && up > 1 ){ up = 1; }
			else if ( bc.Fame >= 15000 && up > 2 ){ up = 2; }
			else if ( bc.Fame >= 10000 && up > 3 ){ up = 3; }

			// Buffs
			double GoldBuff   = 0.1 * up;
			double HitsBuff   = 0.1 * up;
			double StrBuff    = 0.1 * up;
			double IntBuff    = 0.3 * up;
			double DexBuff    = 0.3 * up;
			double SkillsBuff = 0.3 * up;
			double FameBuff   = 0.1 * up;
			double KarmaBuff  = 0.1 * up;
			int    DamageBuff = up;

			if ( bc.IsParagon )
				return;

			if ( HitPointIncrease > 0 )
			{
				if ( up > 0 )
				{
					if ( bc.HitsMaxSeed >= 0 )
						bc.HitsMaxSeed = (int)( bc.HitsMaxSeed + ( bc.HitsMaxSeed * HitsBuff ) );
					
					bc.RawStr = (int)( bc.RawStr + ( bc.RawStr * StrBuff ) );
					bc.RawInt = (int)( bc.RawInt + ( bc.RawInt * IntBuff ) );
					bc.RawDex = (int)( bc.RawDex + ( bc.RawDex * DexBuff ) );

					bc.Hits = bc.HitsMax;
					bc.Mana = bc.ManaMax;
					bc.Stam = bc.StamMax;
				}

				Server.Misc.MyServerSettings.AdditionalHitPoints( bc, HitPointIncrease );

				if ( up > 0 )
				{
					for( int i = 0; i < bc.Skills.Length; i++ )
					{
						Skill skill = (Skill)bc.Skills[i];

						if ( skill.Base > 0.0 )
							skill.Base = skill.Base + ( skill.Base * SkillsBuff );
					}

					bc.DamageMin += DamageBuff;
					bc.DamageMax += DamageBuff;

					if ( bc.Fame > 0 )
						bc.Fame = (int)( bc.Fame + ( bc.Fame * FameBuff ) );

					if ( bc.Fame > 40000 )
						bc.Fame = 40000;

					if ( bc.Karma != 0 )
					{
						bc.Karma = (int)( bc.Karma + ( bc.Karma * KarmaBuff ) );

						if( Math.Abs( bc.Karma ) > 40000 )
							bc.Karma = 40000 * Math.Sign( bc.Karma );
					}

				}

			}
			Region reg = Region.Find( bc.Location, bc.Map );
			if ( !reg.IsPartOf( typeof( BardDungeonRegion ) ) && ( reg.IsPartOf( typeof( OutDoorBadRegion ) ) || reg.IsPartOf( typeof( DungeonRegion ) )) && !bc.Tamable && bc.SpawnerSpawned && up != 0 && !(bc is BasePerson) && !(bc is BaseVendor) && !(bc is AnimalTrainerLord) && !(bc is Townsperson) && !(bc is PlayerVendor) && !(bc.Blessed) && bc.FightMode != FightMode.None && !(bc is Citizens))//new section to make sure mobs are on par with dungeon difficulty
				{
					
					bc.DynamicFameKarma();
					if (up == 4 && bc.Fame < 10000)
						DynamicBeefUp(bc, 10000);
					else if (up == 3 && bc.Fame < 5000)
						DynamicBeefUp(bc, 5000);
					else if (up == 2 && bc.Fame < 1500)
						DynamicBeefUp(bc, 1500);
					else if (up == 1 && bc.Fame < 500)
						DynamicBeefUp(bc, 500);

				}


			if ( Server.Misc.MyServerSettings.CreaturesDetectHidden() )
			{
				double detectHidden = (double)(Server.Misc.IntelligentAction.GetCreatureLevel( (Mobile)bc ) + 10);
				if ( bc.Skills[SkillName.DetectHidden].Value > 10 ){} // DON'T MODIFY THOSE THAT ALREADY HAVE THE SKILL
				else { bc.SetSkill( SkillName.DetectHidden, detectHidden ); }
			}
		}

		public static void DynamicBeefUp( BaseCreature bc, double minfame )
		{
			//randomize minfame for a bit of variety
			minfame *= 1+ (Utility.RandomDouble()/5); // max 20% variance

			while (bc.Fame < minfame) // loop that increases stats until minfame is reached
			{
					bc.HitsMaxSeed = (int)( bc.HitsMaxSeed + (int)( (double)bc.HitsMaxSeed * 0.15 ) );
					
					bc.RawStr = (int)( bc.RawStr + (int)( (double)bc.RawStr * 0.15 ) );
					bc.RawInt = (int)( bc.RawInt + (int)( (double)bc.RawInt * 0.15 ) );
					bc.RawDex = (int)( bc.RawDex + (int)( (double)bc.RawDex * 0.15 ) );

					for( int i = 0; i < bc.Skills.Length; i++ )
					{
						Skill skill = (Skill)bc.Skills[i];

						if ( skill.Base > 0.0 && skill.Base < 120.0 )
							skill.Base = skill.Base + ( skill.Base * 0.2 );
					}

					int DamageBuff = (int)( (double)bc.DamageMax * 0.33 );
					if (DamageBuff < 1)
						DamageBuff = 1;

					bc.DamageMin += DamageBuff;
					bc.DamageMax += DamageBuff;

					if (bc.PhysicalResistanceSeed < 80)
						bc.SetResistance( ResistanceType.Physical, (int)((double)bc.PhysicalResistanceSeed *1.2) );
					if (bc.FireResistSeed < 80)
						bc.SetResistance( ResistanceType.Fire, (int)((double)bc.FireResistSeed *1.2) );
					if (bc.ColdResistSeed < 80)
						bc.SetResistance( ResistanceType.Cold, (int)((double)bc.ColdResistSeed *1.2) );
					if (bc.PoisonResistSeed < 80)
						bc.SetResistance( ResistanceType.Poison, (int)((double)bc.PoisonResistSeed *1.2) );
					if (bc.EnergyResistSeed < 80)
						bc.SetResistance( ResistanceType.Energy, (int)((double)bc.EnergyResistSeed *1.2) );

					bc.DynamicFameKarma();
			
			}

			bc.Hits = bc.HitsMax;
			bc.Mana = bc.ManaMax;
			bc.Stam = bc.StamMax;

		}


		public static void BeefDown( BaseCreature bc, int down )
		{

			int HitPointIncrease = down;
			
			if ( bc.Fame >= 20000 ){ down = 0; }
			else if ( bc.Fame >= 18000 && down > 1 ){ down = 1; }
			else if ( bc.Fame >= 15000 && down > 2 ){ down = 2; }
			else if ( bc.Fame >= 10000 && down > 3 ){ down = 3; }

			double GoldBuff   = 0.1 * down;
			double HitsBuff   = 0.1 * down;
			double StrBuff    = 0.1 * down;
			double IntBuff    = 0.3 * down;
			double DexBuff    = 0.3 * down;
			double SkillsBuff = 0.3 * down;
			double FameBuff   = 0.1 * down;
			double KarmaBuff  = 0.1 * down;
			int    DamageBuff = down;

			if ( HitPointIncrease > 0 )
			{
				if ( down > 0 )
				{
					if ( bc.HitsMaxSeed >= 0 )
						bc.HitsMaxSeed = (int)( bc.HitsMaxSeed / ( 1+ (HitsBuff/1.5) ) );
					
					bc.RawStr = (int)( bc.RawStr / ( 1+ (StrBuff/1.5) ) );
					bc.RawInt = (int)( bc.RawInt / ( 1+ (IntBuff/1.5) ) );
					bc.RawDex = (int)( bc.RawDex / ( 1+ (DexBuff/1.5) ) );

					bc.Hits = bc.HitsMax;
					bc.Mana = bc.ManaMax;
					bc.Stam = bc.StamMax;
				}

				Server.Misc.MyServerSettings.LessHitPoints( bc, HitPointIncrease );

				if ( down > 0 )
				{
					for( int i = 0; i < bc.Skills.Length; i++ )
					{
						Skill skill = (Skill)bc.Skills[i];

						if ( skill.Base > 0.0 )
							skill.Base = skill.Base / ( 1+ (SkillsBuff/1.5) );
					}

					bc.DamageMin -= DamageBuff;
					bc.DamageMax -= DamageBuff;

					bc.DynamicFameKarma();

				}
			}
			
		}

		public static void BeefUpLoot( BaseCreature bc, int up )
		{
			if ( bc.IsParagon || up < 1 )
				return;

			if ( bc.Backpack != null )
			{
				if ( up >= Utility.Random( 7 ) )
				{
					if ( bc.Fame < 1250 )
						bc.AddLoot( LootPack.Meager );
					else if ( bc.Fame < 2500 )
						bc.AddLoot( LootPack.Average );
					else if ( bc.Fame < 5000 )
						bc.AddLoot( LootPack.Rich );
					else if ( bc.Fame < 10000 )
						bc.AddLoot( LootPack.FilthyRich );
					else
						bc.AddLoot( LootPack.UltraRich );
				}
			}
		}

		public override void OnAfterSpawn()
		{

			int Heat = MyServerSettings.GetDifficultyLevel( this.Location, this.Map );

			Heat = Server.Misc.SummonQuests.SummonCarriers( this, this, Heat );

			Region reg = Region.Find( this.Location, this.Map );

			/*//special considerations apply for champion mobs to reduce lag.
			if (this is ChampionGreaterMongbat || this is ChampionImp || this is ChampionGargoyle || this is ChampionHarpy || this is ChampionScorpion || this is ChampionGiantSpider || this is ChampionTerathanDrone || this is ChampionTerathanWarrior || this is ChampionLizardman || this is ChampionSnake || this is ChampionLavaLizard || this is ChampionOphidianWarrior || this is ChampionPixie || this is ChampionShadowWisp || this is ChampionKirin || this is ChampionWisp || this is ChampionGiantRat || this is ChampionSlime || this is ChampionDireWolf || this is ChampionRatman || this is ChampionDeathwatchBeetleHatchling || this is Champion2Lizardman || this is ChampionDeathwatchBeetle || this is ChampionKappa || this is Champion2Pixie || this is Champion2ShadowWisp || this is ChampionPlagueSpawn || this is ChampionBogling )
			{
				this.DeleteCorpseOnDeath = true;
				return; // skip rest of the method for these
			}*/

			if ( reg.IsPartOf( typeof( MBRegion ) ) || (this.Map == Map.Trammel && (this.X > 3631 && this.X < 3642) && ( this.Y > 2085 && this.Y < 2095))) 
			{
				m_NoKillAwards = true;
			}

			if ( (m_midrace != 0 || this is Townsperson || this is BaseVendor) && AdventuresFunctions.IsPuritain((object)this))
			{
				if (this is Townsperson || (this is BaseVendor && m_midrace == 0))
				{
					IntelligentAction.AssignMidlandRace( this);
				}

				IntelligentAction.MidlandRace(this);

				if (this is MidlandGuard && Utility.RandomDouble() < 0.15)
				{
				//	GuardDog dog = new GuardDog();
					//dog.MoveToWorld(this.Location, this.Map);
					//dog.OnAfterSpawn();
				}
			}

			if ( (reg.IsPartOf( "DarkMoor" ) || reg.IsPartOf( "the Temple of Praetoria" )) && !(this is BaseCursed) && !(this is BaseVendor) && !(this is Citizens) && !(this is BasePerson) && !(this is BaseRed) && !(this is BaseQuester) &&!(this is TownHerald) && this.Karma < -50)
				this.Karma = Math.Abs(this.Karma);

			if ( this is Xurtzar || this is Surtaz || this is Vulcrum || this is Arachnar || this is CaddelliteDragon ){ Heat = 4; } // TIME LORD TRIAL CREATURES GET HP BUFF
			
			// BARDS TALE TWEAKS
			if ( reg.IsPartOf( "Mangar's Tower" ) && this.Fame >= 5000 ){ Heat = 1; }
			else if ( reg.IsPartOf( "Mangar's Chamber" ) && this.Fame >= 5000 ){ Heat = 1; }
			else if ( reg.IsPartOf( "Kylearan's Tower" ) && this.Fame >= 5000 ){ Heat = 1; }

			if ( this.Name == "a vampire" ){ this.Title = null; }
			else if ( this.Name == "a young vampire" ){ this.Title = null; }
			else if ( this.Name == "a vampire lord" ){ this.Title = null; }
			else if ( this.Name == "a vampire prince" ){ this.Title = null; }

			if ( this.Map == Map.Tokuno && Utility.RandomMinMax( 1, 4 ) == 1 ) // SOME ANIMALS ARE AGGRESSIVE ON THE ISLES OF DREAD
			{
				if (	this is WhiteTiger || 
						this is WhiteTigerRiding || 
						this is PolarBear || 
						this is WhiteWolf || 
						this is SnowLeopard || 
						this is Mammoth || 
						this is Boar || 
						this is Panda || 
						this is PandaRiding || 
						this is Bull || 
						this is Gorilla || 
						this is Panther || 
						this is GreyWolf ){

					AI = AIType.AI_Melee;
					FightMode = FightMode.Closest;
					Karma = 0 - Fame;
					Tamable = false;
				}
			}

			if ( this.Map == Map.Felucca && this.Z > 10 && this.X >= 1975 && this.Y >= 2201 && this.X <= 2032 && this.Y <= 2247 ) // ZOO ONLY HAS FRIENDLY ANIMALS
			{
				AI = AIType.AI_Melee;
				FightMode = FightMode.Aggressor;
				Karma = 0;
				Fame = 0;
				Tamable = false;
			}

			if ( Region.IsPartOf( typeof( PirateRegion ) ) ) // GHOST PIRATE SHIP ////////////////////////////////////////
			{
				if ( this is SkeletalMage ){ this.Name = "a dead pirate"; }
				else if ( this is SkeletalKnight ){ this.Name = "a skeletal pirate"; }
				else if ( this is Ghoul ){ this.Name = "a ghoulish pirate"; }
				else if ( this is Zombie ){ this.Name = "a rotting pirate"; }
				else if ( this is Spectre ){ this.Name = "a spectral pirate"; }
				else if ( this is AncientLich )
				{
					this.Name = NameList.RandomName( "evil mage" );
					this.Title = "the Captain of the Dead";
					PirateChest MyChest = new PirateChest(10,null);
					MyChest.ContainerOwner = "Treasure Chest of " + this.Name + " " + this.Title + "";
					MyChest.Hue = 0x47E;
					this.PackItem( MyChest );
				}
			}

			if ( Utility.RandomMinMax( 1, 4 ) == 1 && Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) ) ) // FOR HORSE RIDERS
			{
				if (	this is Adventurers || 
						this is Berserker || 
						this is Minstrel || 
						this is Rogue || 
						this is EvilMage || 
						this is EvilMageLord || 
						this is Brigand || 
						this is Executioner || 
						this is Monks || 
						this is ElfBerserker || 
						this is ElfMage || 
						this is ElfRogue || 
						this is ElfMinstrel || 
						this is ElfMonks || 
						this is OrkWarrior || 
						this is OrkMage || 
						this is OrkRogue || 
						this is OrkMonks )
				{
					BaseMount steed = new EvilMount();

					if ( Utility.RandomMinMax( 1, 5 ) > 1 )
					{
						if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" )
						{
							switch ( Utility.RandomMinMax( 1, 4 ) )
							{
								case 1: steed.Body = 793;		steed.ItemID = 0x3EBB;	break;
								case 2: steed.Body = 0x11C;		steed.ItemID = 0x3E92;	break;
								case 3: steed.Body = 219;		steed.ItemID = 16036;	steed.Hue = 0xA70;	break;
								case 4: steed.Body = 116;		steed.ItemID = 16036;	steed.Hue = 0xB77;	break;
							}
						}
						else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Isles of Dread" )
						{
							switch ( Utility.RandomMinMax( 1, 2 ) )
							{
								case 1: steed.Body = 218;		steed.ItemID = 16036;	break;
								case 2: steed.Body = 117;		steed.ItemID = 16036;	steed.Hue = 0xB77;	break;
							}
						}
						else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Land of Lodoria" )
						{
							switch ( Utility.RandomMinMax( 1, 8 ) )
							{
								case 1: steed.Body = 0xD2;		steed.ItemID = 0x3EA3;	steed.Hue = Utility.RandomList( 0x89f, 0xBB4, 1701 );	break;
								case 2: steed.Body = 0xD2;		steed.ItemID = 0x3EA3;	steed.Hue = Utility.RandomList( 0x89f, 0xBB4, 1701 );	break;
								case 3: steed.Body = 0xD2;		steed.ItemID = 0x3EA3;	steed.Hue = Utility.RandomList( 0x89f, 0xBB4, 1701 );	break;
								case 4: steed.Body = 0xD2;		steed.ItemID = 0x3EA3;	steed.Hue = Utility.RandomList( 0x89f, 0xBB4, 1701 );	break;
								case 5: steed.Body = 0xD2;		steed.ItemID = 0x3EA3;	steed.Hue = Utility.RandomList( 0x89f, 0xBB4, 1701 );	break;
								case 6: steed.Body = 0xC8;		steed.ItemID = 0x3E9F;	steed.Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );	break;
								case 7: steed.Body = 0xE2;		steed.ItemID = 0x3EA0;	steed.Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );	break;
								case 8: steed.Body = 0xE4;		steed.ItemID = 0x3EA1;	steed.Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );	break;
							}
						}
						else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" )
						{
							switch ( Utility.RandomMinMax( 1, 1 ) )
							{
								case 1: steed.Body = 0x31A;		steed.ItemID = 0x3EBD;	break;
							}
						}
						else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Savaged Empire" )
						{
							switch ( Utility.RandomMinMax( 1, 5 ) )
							{
								case 1: steed.Body = 0x31A;		steed.ItemID = 0x3EBD;	break;
								case 2: steed.Body = 0x11C;		steed.ItemID = 0x3E92;	steed.Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );	break;
								case 3: steed.Body = 0x31A;		steed.ItemID = 0x3EBD;	steed.Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );	break;
								case 4: steed.Body = 0x11C;		steed.ItemID = 0x3E92;	steed.Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );	break;
								case 5: steed.Body = 0x11C;		steed.ItemID = 0x3E92;	break;
							}
						}
						else
						{
							switch ( Utility.RandomMinMax( 1, 3 ) )
							{
								case 1: steed.Body = 0xC8;		steed.ItemID = 0x3E9F;	steed.Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );	break;
								case 2: steed.Body = 0xE2;		steed.ItemID = 0x3EA0;	steed.Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );	break;
								case 3: steed.Body = 0xE4;		steed.ItemID = 0x3EA1;	steed.Hue = Utility.RandomList( 0, 0, 0, 0, 0, 0x780, 0x781, 0x782, 0x783, 0x8FD, 0x8FE, 0x8FF, 0x900, 0x901, 0x902, 0x903, 0x904, 0x905, 0x906, 0x907, 0x908, Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue(), Utility.RandomNeutralHue() );	break;
							}
						}
					}
					else
					{
						switch ( Utility.RandomMinMax( 1, 9 ) )
						{
							case 1: steed.Body = 213;		steed.ItemID = 16069;	steed.Hue = Utility.RandomList( 0x797, 0x5BC, 0x789, 0x908, 0xAB1, 0, 0x92B, 0xAC0, 0xAB1 );	break;
							case 2: steed.Body = 277;		steed.ItemID = 16017;	steed.Hue = Utility.RandomList( 1899, 0xBB4, 0xAF3, 0xB01 );	break;
							case 3: steed.Body = 226;		steed.ItemID = 0x3EA0;	steed.Hue = Utility.RandomList( 0xB78, 1109, 0xB73, 1470 );	break;
							case 4: steed.Body = 187;		steed.ItemID = 0x3EBA;	steed.Hue = Utility.RandomList( 0, 0xB4D );	break;
							case 5: steed.Body = 243;		steed.ItemID = 0x3E94;	steed.Hue = Utility.RandomList( 2708, 0x497, 0, 0xB73 );	break;
							case 6: steed.Body = 0x31F;		steed.ItemID = 0x3EBE;	break;
							case 7: steed.Body = 188;		steed.ItemID = 0x3EB8;	break;
							case 8: steed.Body = 0x31A;		steed.ItemID = 0x3EBD;	steed.Hue = 0x991;	break;
							case 9: steed.Body = 0xA9;		steed.ItemID = 0x3E95;	steed.Hue = Utility.RandomList( 0xB16, Utility.RandomList( 0xB71, 0xB17, 0xB73 ), Utility.RandomList( 0xB33, 0xB34, 0xB35, 0xB36, 0xB37 ), 1167, 0, 0xB75 );	break;
						}
					}

					steed.Rider = this;
					ActiveSpeed = 0.1;
					PassiveSpeed = 0.2;
				}
			}

			// BARD'S TALE ///////////////////////////////////////////////////////////////////////////////////////
			if ( reg.IsPartOf( typeof( BardDungeonRegion ) ) )
			{
				if ( reg.IsPartOf( "the Sewers" ) )
				{
					if ( this is GiantSpider ){ this.Name = "a spinner"; }
				}
				else if ( reg.IsPartOf( "the Catacombs" ) || reg.IsPartOf( "the Lower Catacombs" ) )
				{
					if ( this is Spectre ){ this.Name = "a shadow"; }
					if ( this is Spirit ){ this.Name = "a ghost"; }
				}
				else if ( reg.IsPartOf( "Harkyn's Castle" ) )
				{
					if ( this is Gazer ){ this.Name = "a seeker"; }
					if ( this is MadDog ){ this.Name = "a wolf"; }
					if ( this is StoneElemental ){ this.Name = "a stone golem"; }
					if ( this is FrostGiant ){ this.Title = "the ice giant"; }
				}
				else if ( reg.IsPartOf( "Kylearan's Tower" ) )
				{
					if ( this is Gazer ){ this.Body = 674; this.BaseSoundID = 0x47D; this.Name = NameList.RandomName( "drakkul" ); this.Title = "the beholder"; }
					if ( this is MadDog ){ this.Name = "a wolf"; }
					if ( this is StoneElemental ){ this.Name = "a stone elemental"; }
					if ( this is LowerDemon ){ this.Name = "a demon"; this.Hue = 0x5B5; this.Body = 4; }
				}
				else if ( reg.IsPartOf( "Mangar's Tower" ) )
				{
					if ( this is Gazer ){ this.Name = "an evil eye"; this.Body = 83; }
					if ( this is LowerDemon ){ this.Name = "a demon lord"; this.Title = ""; this.Body = 102; }
					if ( this is Demon ){ this.Name = "a greater demon"; this.Title = ""; this.Hue = 0; this.Body = 137; }
					if ( this is Daemon ){ this.Name = "a balrog"; this.Title = ""; this.Body = 38; }
					if ( this is StormGiant && Utility.RandomMinMax( 1, 2 ) == 1 )
					{
						this.Title = "the cloud giant";
						Item lootchest = this.Backpack.FindItemByType( typeof ( LootChest ) );
						if ( lootchest != null )
						{
							lootchest.Hue = 0x835;
							lootchest.Name = "silver chest";
						}
					}
				}
				else if ( this is Orc ){ this.Body = Utility.RandomList( 7, 17, 41 ); }
			}

			if ( reg.IsPartOf( "the Castle of the Black Knight" ) )
			{
				if ( this is Gargoyle )
				{
					MorphingTime.MakeMeAGargoyle( this, "mage" );
				}
				else if ( this is EvilMage )
				{
					this.Title = "the dark wizard";
					MorphingTime.ColorMyClothes( this, 0x497 );
				}
			}

			if ( reg.IsPartOf( "the Blood Temple" ) )
			{
				if ( this is Devil )
				{
					switch ( Utility.Random( 5 ) )
					{
						case 0: this.Title = "the devil of blood"; 			break;
						case 1: this.Title = "the bleeding devil"; 			break;
						case 2: this.Title = "the blood devil"; 			break;
						case 3: this.Title = "the devil of bloody hell"; 	break;
						case 4: this.Title = "the blood moon devil"; 		break;
					}
					Hue = Utility.RandomList( 0xB01, 0x870 );
				}
			}

			if ( reg.IsPartOf( "the Daemon's Crag" ) )
			{
				if ( this is Daemon )
				{
					Name = NameList.RandomName( "demonic" );
					Title = "the daemon";
					Body = Utility.RandomList( 9, 320 );
					Hue = 0;
					BaseSoundID = 357;
				}
				else if ( this is Balron )
				{
					Name = NameList.RandomName( "demonic" );
					Title = "the daemon lord";
					Body = Utility.RandomList( 191, 427 );
					Hue = 0;
					BaseSoundID = 357;
				}
				else if ( this is EvilMageLord || this is EvilMage )
				{
					MorphingTime.RemoveMyClothes( this );

					Item robe = new AssassinRobe();
						robe.Name = "sorcerer robe";
						robe.Hue = 2411;
						AddItem( robe );

					Item boots = new Boots();
						boots.Name = "boots";
						boots.Hue = 2411;
						AddItem( boots );

					Item hat = new ClothHood();
						hat.Name = "sorcerer hood";
						hat.Hue = 2411;
						AddItem( hat );

					Item staff = new BlackStaff();
						staff.Name = "sorcerer staff";
						AddItem( staff );

					Body = 0x190; 
					Title = "the sorcerer";
					BaseSoundID = 0x47D;

					if ( this is EvilMageLord )
					{
						Name = "Malchir";
						Title = "the master sorcerer";
					}
					else if ( this is EvilMage && Home.X == 6277 && Home.Y == 2099 )
					{
						Body = 0x191; 
						Name = "Bane";
						Title = "the sorceress";
						BaseSoundID = 0x4B0;
					}
					else if ( this is EvilMage && Home.X == 6398 && Home.Y == 1966 )
					{
						Name = "Vardion";
					}
					else if ( this is EvilMage && Home.X == 6398 && Home.Y == 1966 )
					{
						Name = "Beren";
					}
					else if ( this is EvilMage && Home.X == 6398 && Home.Y == 1966 )
					{
						Name = "Gorgrond";
					}

					Utility.AssignRandomHair( this );
					FacialHairItemID = 0;

					if ( this is EvilMageLord )
					{
						SetStr( 216, 305 );
						SetDex( 96, 115 );
						SetInt( 966, 1045 );
						SetHits( 560, 595 );
						SetDamage( 15, 27 );

						SetDamageType( ResistanceType.Physical, 20 );
						SetDamageType( ResistanceType.Cold, 40 );
						SetDamageType( ResistanceType.Energy, 40 );

						SetResistance( ResistanceType.Physical, 55, 65 );
						SetResistance( ResistanceType.Fire, 25, 30 );
						SetResistance( ResistanceType.Cold, 50, 60 );
						SetResistance( ResistanceType.Poison, 50, 60 );
						SetResistance( ResistanceType.Energy, 25, 30 );

						SetSkill( SkillName.EvalInt, 120.1, 130.0 );
						SetSkill( SkillName.Magery, 120.1, 130.0 );
						SetSkill( SkillName.Meditation, 100.1, 101.0 );
						SetSkill( SkillName.Poisoning, 100.1, 101.0 );
						SetSkill( SkillName.MagicResist, 175.2, 200.0 );
						SetSkill( SkillName.Tactics, 90.1, 100.0 );
						SetSkill( SkillName.Wrestling, 75.1, 100.0 );

						Fame = 23000;
						Karma = -23000;

						VirtualArmor = 60;
					}
					else
					{
						SetStr( 416, 505 );
						SetDex( 146, 165 );
						SetInt( 566, 655 );

						SetHits( 250, 303 );

						SetDamage( 11, 13 );

						SetDamageType( ResistanceType.Physical, 0 );
						SetDamageType( ResistanceType.Cold, 60 );
						SetDamageType( ResistanceType.Energy, 40 );

						SetResistance( ResistanceType.Physical, 40, 50 );
						SetResistance( ResistanceType.Fire, 30, 40 );
						SetResistance( ResistanceType.Cold, 50, 60 );
						SetResistance( ResistanceType.Poison, 50, 60 );
						SetResistance( ResistanceType.Energy, 40, 50 );

						SetSkill( SkillName.Necromancy, 90, 110.0 );
						SetSkill( SkillName.SpiritSpeak, 90.0, 110.0 );

						SetSkill( SkillName.EvalInt, 90.1, 100.0 );
						SetSkill( SkillName.Magery, 90.1, 100.0 );
						SetSkill( SkillName.MagicResist, 150.5, 200.0 );
						SetSkill( SkillName.Tactics, 50.1, 70.0 );
						SetSkill( SkillName.Wrestling, 60.1, 80.0 );

						Fame = 18000;
						Karma = -18000;

						VirtualArmor = 50;
					}
				}
			}

			if ( reg.IsPartOf( "the Mines of Morinia" ) )
			{
				if ( this is PoisonElemental )
				{
					this.Body = 16;
					this.BaseSoundID = 278;
				}
				else if ( this is CrystalElemental )
				{
					this.Hue = Utility.RandomList( 0x48D, 0x48E, 0x48F, 0x490, 0x491 );
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Poison, 0 );
					this.SetDamageType( ResistanceType.Physical, 20 );
					this.SetDamageType( ResistanceType.Energy, 80 );
					this.AddItem( new LightSource() );
				}
			}

			if ( reg.IsPartOf( "the Fires of Hell" ) )
			{
				if ( this is Gargoyle )
				{
					this.Name = "an ashen gargoyle";
					this.Hue = 0xB85;
				}
				else if ( this is BoneMagi )
				{
					this.Name = "a skeletal fire mage";
					this.Hue = Utility.RandomList( 0xB73, 0xB71, 0xB17, 0xAFA, 0xAC8, 0x986 );
					this.AddItem( new LightSource() );
				}
				else if ( this is SkeletalMage )
				{
					this.Name = "an undead pyromancer";
					this.Hue = Utility.RandomList( 0xB73, 0xB71, 0xB17, 0xAFA, 0xAC8, 0x986 );
					this.AddItem( new LightSource() );
				}
				else if ( this is BoneKnight )
				{
					this.Name = "a skeletal guard";
					this.Hue = Utility.RandomList( 0xB73, 0xB71, 0xB17, 0xAFA, 0xAC8, 0x986 );
					this.AddItem( new LightSource() );
				}
			}

			if ( reg.IsPartOf( "the City of Embers" ) )
			{
				if ( this is DreadSpider )
				{
					this.Name = "a vulrachnid";
					this.Hue = 0xB73;
					this.Body = 99;
					this.AddItem( new LightSource() );
				}
				else if ( this is BoneMagi )
				{
					this.Name = "an undead flamecaster";
					this.Hue = Utility.RandomList( 0xB73, 0xB71, 0xB17, 0xAFA, 0xAC8, 0x986 );
					this.AddItem( new LightSource() );
				}
				else if ( this is SkeletalMage )
				{
					this.Name = "a skeletal pyromancer";
					this.Hue = Utility.RandomList( 0xB73, 0xB71, 0xB17, 0xAFA, 0xAC8, 0x986 );
					this.AddItem( new LightSource() );
				}
				else if ( this is BoneKnight )
				{
					this.Name = "a firebone warrior";
					this.Hue = Utility.RandomList( 0xB73, 0xB71, 0xB17, 0xAFA, 0xAC8, 0x986 );
					this.AddItem( new LightSource() );
				}
			}

			if ( reg.IsPartOf( "Dungeon Hythloth" ) )
			{
				if ( this is LichLord )
				{
					this.Title = "the high pharaoh";
					this.Hue = 0x9C4;
					this.Body = 125;
				}
				else if ( this is Lich )
				{
					this.Title = "the pharaoh";
					this.Hue = 0x9DF;
					this.Body = 125;
				}
				else if ( this is Gazer )
				{
					this.Name = "a watcher";
					this.Hue = 0x96D;
				}
				else if ( this is ElderGazer )
				{
					this.Name = "a tomb watcher";
					this.Hue = 0x9D1;
					this.Body = 674;
				}
				else if ( this is Gargoyle )
				{
					this.Name = "a sand gargoyle";
					this.Hue = 0x96D;
					this.PackItem( new Sand( Utility.RandomMinMax( 1, 2 ) ) );
				}
			}

			if ( reg.IsPartOf( "the Ruins of the Black Blade" ) )
			{
				if ( this is Gazer ){ this.Name = "a seeker"; }
				if ( this is StoneElemental ){ this.Name = "a stone golem"; }
			}

			if ( reg.IsPartOf( "the Ancient Crash Site" ) || reg.IsPartOf( "the Ancient Sky Ship" ) )
			{
				if ( this is Fungal ){ this.Name = "a mushroom man"; this.Hue=0x48F; this.AddItem( new LightSource() ); }
				else if ( this is FungalMage ){ this.Name = "a psychic mushroom"; this.Hue=0xABA; this.AddItem( new LightSource() ); }
				else if ( this is ToxicElemental ){ this.Name = "a toxic waste elemental"; this.Hue=0xBA1; this.AddItem( new LighterSource() ); this.Body = 707;}
				else if ( this is PoisonBeetleRiding ){ this.Name = "a rad beetle"; this.Hue=0xB07; this.AddItem( new LighterSource() ); }
				else if ( this is ElectricalElemental ){ this.Name = "a plasma elemental"; this.Hue=0xB53; }
				else if ( this is Stirge ){ this.Name = "a mynock"; this.Body = 742; }
				else if ( this is BloodElemental )
				{
					this.AddItem( new LighterSource() ); 
					if ( this.X > 954 && this.Y > 3771 && this.X < 976 && this.Y < 3793 ) { this.Name = "a coolant elemental"; this.Hue = 0xB73; }
					else { this.Name = "a contaminated elemental"; this.Hue = 0xBAD; }
				}
			}

			if ( reg.IsPartOf( "the Forgotten Halls" ) )
			{
				if ( this is Daemon && Server.Misc.SummonQuests.IsInLocation( this.Home.X, this.Home.Y, this.Map, 409, 3670, Map.TerMur ) )
				{
					switch ( Utility.Random( 5 ) )
					{
						case 0: this.Title = "the daemon of filth"; break;
						case 1: this.Title = "the daemon of crud"; break;
						case 2: this.Title = "the daemon of grime"; break;
						case 3: this.Title = "the daemon of sludge"; break;
						case 4: this.Title = "the daemon of the putrid"; break;
					}
				}
				if ( this is ToxicElemental ){ this.Name = "a sewage elemental"; this.Hue = Hue = 0xB97; }
				if ( this is ForestGiant ){ this.Hue = Hue = 0xB97; this.Title = "the sludge giant"; }
				if ( this is AncientLich )
				{
					this.Hue = Hue = 0x967;
					this.Title = "the shadow lich";

					if ( Utility.Random( 3 ) == 1 )
					{
						switch ( Utility.Random( 5 ) )
						{
							case 0: BoneArms radarm = new BoneArms(); 		radarm.Name = "shadow lich arms"; 		radarm.Hue = 0x497; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radarm, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); 	this.PackItem( radarm ); 	break;
							case 1: BoneChest radchest = new BoneChest(); 	radchest.Name = "shadow lich chest"; 	radchest.Hue = 0x497; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radchest, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); this.PackItem( radchest ); 	break;
							case 2: BoneGloves radglove = new BoneGloves(); radglove.Name = "shadow lich gloves"; 	radglove.Hue = 0x497; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radglove, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); this.PackItem( radglove ); 	break;
							case 3: BoneLegs radleg = new BoneLegs(); 		radleg.Name = "shadow lich leggings"; 	radleg.Hue = 0x497; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radleg, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); 	this.PackItem( radleg ); 	break;
							case 4: BoneHelm radhelm = new BoneHelm(); 		radhelm.Name = "shadow lich helm"; 		radhelm.Hue = 0x497; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radhelm, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); 	this.PackItem( radhelm ); 	break;
						}
					}
				}
				if ( this is SkeletalKnight )
				{
					this.Name = "a rotting skeleton";
					this.Hue = 0xB97;
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Physical, 60 );
					this.SetDamageType( ResistanceType.Energy, 0 );
					this.SetDamageType( ResistanceType.Poison, 40 );
					this.Body = Utility.RandomList( 57, 50, 56 );

					List<Item> belongings = new List<Item>();
					foreach( Item i in this.Backpack.Items )
					{
						belongings.Add(i);
					}
					foreach ( Item stuff in belongings )
					{
						stuff.Delete();
					}

					GenerateLoot( true );

					if ( Utility.Random( 4 ) == 1 )
					{
						switch ( Utility.Random( 5 ) )
						{
							case 0: BoneArms radarm = new BoneArms(); 		radarm.Name = "rotting bone arms"; 		radarm.Hue = 0xB97; 	radarm.PoisonBonus = 10; 	this.PackItem( radarm ); 	break;
							case 1: BoneChest radchest = new BoneChest(); 	radchest.Name = "rotting bone chest"; 	radchest.Hue = 0xB97; 	radchest.PoisonBonus = 10; 	this.PackItem( radchest ); 	break;
							case 2: BoneGloves radglove = new BoneGloves(); radglove.Name = "rotting bone gloves"; 	radglove.Hue = 0xB97; 	radglove.PoisonBonus = 10; 	this.PackItem( radglove ); 	break;
							case 3: BoneLegs radleg = new BoneLegs(); 		radleg.Name = "rotting bone leggings"; 	radleg.Hue = 0xB97; 	radleg.PoisonBonus = 10; 	this.PackItem( radleg ); 	break;
							case 4: BoneHelm radhelm = new BoneHelm(); 		radhelm.Name = "rotting bone helm"; 	radhelm.Hue = 0xB97; 	radhelm.PoisonBonus = 10; 	this.PackItem( radhelm ); 	break;
						}
					}

					if ( this.Body == 56 || this.Body == 168 ){ BattleAxe radaxe = new BattleAxe(); radaxe.Name = "rusty battle axe"; radaxe.Hue = 0xB97; radaxe.AosElementDamages.Poison=50; this.PackItem( radaxe ); }
					if ( this.Body == 57 ){ Scimitar radsim = new Scimitar(); radsim.Name = "rusty scimitar"; radsim.Hue = 0xB97;	radsim.AosElementDamages.Poison=50; this.PackItem( radsim ); }
					if ( this.Body == 170 || this.Body == 327 ){ Longsword radswd = new Longsword(); radswd.Name = "rusty longsword"; radswd.Hue = 0xB97;	radswd.AosElementDamages.Poison=50; this.PackItem( radswd ); }
					if ( this.Body == 57 || this.Body == 168 || this.Body == 170 ){ WoodenShield radshield = new WoodenShield(); radshield.Name = "rotting shield"; radshield.Hue = 0xB97; radshield.PoisonBonus = 5; this.PackItem( radshield ); }
				}
			}

			if ( reg.IsPartOf( "the Tomb of Kazibal" ) )
			{
				if ( this is AncientLich )
				{
					this.Hue = Hue = 0x83B;
					this.Name = "Kazibal";
					this.Title = "the unearthed";

					if ( Utility.Random( 3 ) == 1 )
					{
						switch ( Utility.Random( 5 ) )
						{
							case 0: BoneArms radarm = new BoneArms(); 		radarm.Name = "Kazibal bone arms"; 		radarm.Hue = 0x83B; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radarm, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); 	this.PackItem( radarm ); 	break;
							case 1: BoneChest radchest = new BoneChest(); 	radchest.Name = "Kazibal bone chest"; 	radchest.Hue = 0x83B; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radchest, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); this.PackItem( radchest ); 	break;
							case 2: BoneGloves radglove = new BoneGloves(); radglove.Name = "Kazibal bone gloves"; 	radglove.Hue = 0x83B; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radglove, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); this.PackItem( radglove ); 	break;
							case 3: BoneLegs radleg = new BoneLegs(); 		radleg.Name = "Kazibal bone leggings"; 	radleg.Hue = 0x83B; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radleg, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); 	this.PackItem( radleg ); 	break;
							case 4: BoneHelm radhelm = new BoneHelm(); 		radhelm.Name = "Kazibal bone helm"; 	radhelm.Hue = 0x83B; 	BaseRunicTool.ApplyAttributesTo( (BaseArmor)radhelm, false, 1000, Utility.RandomMinMax( 4, 8 ), 50, 125 ); 	this.PackItem( radhelm ); 	break;
						}
					}
				}
			}

			if ( reg.IsPartOf( "the Stygian Abyss" ) )
			{
				if ( this is SerpynSorceress )
				{
					this.Hue = 0xB79;
					this.Body = 306;
					this.BaseSoundID = 639;
					this.Name = NameList.RandomName( "lizardman" );
					this.Title = "the silisk sorcerer";
				}
				else if ( this is Sleestax )
				{
					this.Title = "the silisk";
				}
				else if ( this is Grathek )
				{
					this.Title = "the silisk guard";
				}
			}

			if ( reg.IsPartOf( "the Sanctum of Saltmarsh" ) )
			{
				if ( this is Tyranasaur )
				{
					this.Hue = 0xB51;
				}
				else if ( this is Raptor )
				{
					this.Hue = 0xB51;
				}
				else if ( this is Stegosaurus )
				{
					this.Name = "a scalosaur";
					this.Hue = 0xB18;
					AI = AIType.AI_Melee;
					FightMode = FightMode.Closest;
					Karma = 0 - Fame;
				}
			}

			if ( reg.IsPartOf( "the Hall of the Mountain King" ) )
			{
				if ( this is StygianGargoyleLord )
				{
					this.Name = "a gargoyle";
				}
				else if ( this is Sleestax )
				{
					this.Title = "the silisk";
				}
			}

			if ( reg.IsPartOf( "Argentrock Castle" ) )
			{
				if ( this is ElderTitan )
				{
					this.Title = "the ancient titan";
				}
				else if ( this is StygianGargoyleLord )
				{
					this.Name = "an elder gargoyle";
				}
				else if ( this is StygianGargoyle )
				{
					this.Name = "a gargoyle";
				}
				else if ( this is HarpyElder )
				{
					this.Name = "a harpy";
				}
				else if ( this is GriffonRiding )
				{
					AI = AIType.AI_Melee;
					FightMode = FightMode.Closest;
					Karma = 0 - Fame;
					Tamable = false;
				}
				else if ( this is AnyStatue )
				{
					Body = 303;
				}
			}

			if ( reg.IsPartOf( "the Undersea Castle" ) )
			{
				CantWalk = false; // SOME OF THESE SETTINGS KEEP SWIMMERS ON THE STONE AND OFF THE WATER IN THESE DUNGEONS
				CanSwim = false;

				Location = Home;
			}

			if ( reg.IsPartOf( "the Depths of Carthax Lake" ) )
			{
				CantWalk = false; // SOME OF THESE SETTINGS KEEP SWIMMERS ON THE STONE AND OFF THE WATER IN THESE DUNGEONS
				CanSwim = false;

				if ( this is WaterBeetleRiding )
				{
					this.Body = 0xF4;
					this.Hue = 0xB48;
				}
				else if ( this is Sleestax )
				{
					this.Title = "the silisk";
				}
				else if ( this is GiantEel )
				{
					this.Body = 21;
					this.Hue = 0xB9D;
				}
				else if ( this is GiantSquid )
				{
					this.Title = "squid tentacles";
					this.Hue = 0xB75;
				}

				Location = Home;
			}

			if ( reg.IsPartOf( "the Dragon's Maw" ) )
			{
				if ( this is BoneKnight )
				{
					this.Name = "a skeleton";
					this.Hue = 0x48F;
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Physical, 20 );
					this.SetDamageType( ResistanceType.Energy, 40 );
					this.SetDamageType( ResistanceType.Poison, 40 );
					this.Body = Utility.RandomList( 57, 50, 56 );
					this.AddItem( new LightSource() );

					List<Item> belongings = new List<Item>();
					foreach( Item i in this.Backpack.Items )
					{
						belongings.Add(i);
					}
					foreach ( Item stuff in belongings )
					{
						stuff.Delete();
					}

					GenerateLoot( true );

					if ( Utility.Random( 4 ) == 1 )
					{
						switch ( Utility.Random( 5 ) )
						{
							case 0: BoneArms radarm = new BoneArms(); 		radarm.Name = "irradiated bone arms"; 		radarm.Hue = 0x48F; 	radarm.PoisonBonus = 10; 	this.PackItem( radarm ); 	break;
							case 1: BoneChest radchest = new BoneChest(); 	radchest.Name = "irradiated bone chest"; 	radchest.Hue = 0x48F; 	radchest.PoisonBonus = 10; 	this.PackItem( radchest ); 	break;
							case 2: BoneGloves radglove = new BoneGloves(); radglove.Name = "irradiated bone gloves"; 	radglove.Hue = 0x48F; 	radglove.PoisonBonus = 10; 	this.PackItem( radglove ); 	break;
							case 3: BoneLegs radleg = new BoneLegs(); 		radleg.Name = "irradiated bone leggings"; 	radleg.Hue = 0x48F; 	radleg.PoisonBonus = 10; 	this.PackItem( radleg ); 	break;
							case 4: BoneHelm radhelm = new BoneHelm(); 		radhelm.Name = "irradiated bone helm"; 		radhelm.Hue = 0x48F; 	radhelm.PoisonBonus = 10; 	this.PackItem( radhelm ); 	break;
						}
					}

					if ( this.Body == 56 || this.Body == 168 ){ BattleAxe radaxe = new BattleAxe(); radaxe.Name = "irradiated battle axe"; radaxe.Hue = 0x48F; radaxe.AosElementDamages.Poison=50; this.PackItem( radaxe ); }
					if ( this.Body == 57 ){ Scimitar radsim = new Scimitar(); radsim.Name = "irradiated scimitar"; radsim.Hue = 0x48F;	radsim.AosElementDamages.Poison=50; this.PackItem( radsim ); }
					if ( this.Body == 170 || this.Body == 327 ){ Longsword radswd = new Longsword(); radswd.Name = "irradiated longsword"; radswd.Hue = 0x48F;	radswd.AosElementDamages.Poison=50; this.PackItem( radswd ); }
					if ( this.Body == 57 || this.Body == 168 || this.Body == 170 ){ WoodenShield radshield = new WoodenShield(); radshield.Name = "irradiated shield"; radshield.Hue = 0x48F; radshield.PoisonBonus = 5; this.PackItem( radshield ); }
				}
				else if ( this is CrystalElemental )
				{
					this.Hue = Utility.RandomList( 0x48D, 0x48E, 0x48F, 0x490, 0x491 );
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Poison, 0 );
					this.SetDamageType( ResistanceType.Physical, 20 );
					this.SetDamageType( ResistanceType.Energy, 80 );
					this.AddItem( new LightSource() );
				}
				else if ( this is FloatingEye )
				{
					this.Hue = 0x494;
					this.Name = "an eye of the void";
				}
			}

			if ( reg.IsPartOf( "the Catacombs of Azerok" ) )
			{
				if ( this is BoneKnight )
				{
					this.Name = "a rotting skeleton";
					this.Hue = 0xB97;
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Physical, 60 );
					this.SetDamageType( ResistanceType.Energy, 0 );
					this.SetDamageType( ResistanceType.Poison, 40 );
					this.Body = Utility.RandomList( 57, 50, 56 );

					List<Item> belongings = new List<Item>();
					foreach( Item i in this.Backpack.Items )
					{
						belongings.Add(i);
					}
					foreach ( Item stuff in belongings )
					{
						stuff.Delete();
					}

					GenerateLoot( true );

					if ( Utility.Random( 4 ) == 1 )
					{
						switch ( Utility.Random( 5 ) )
						{
							case 0: BoneArms radarm = new BoneArms(); 		radarm.Name = "rotting bone arms"; 		radarm.Hue = 0xB97; 	radarm.PoisonBonus = 10; 	this.PackItem( radarm ); 	break;
							case 1: BoneChest radchest = new BoneChest(); 	radchest.Name = "rotting bone chest"; 	radchest.Hue = 0xB97; 	radchest.PoisonBonus = 10; 	this.PackItem( radchest ); 	break;
							case 2: BoneGloves radglove = new BoneGloves(); radglove.Name = "rotting bone gloves"; 	radglove.Hue = 0xB97; 	radglove.PoisonBonus = 10; 	this.PackItem( radglove ); 	break;
							case 3: BoneLegs radleg = new BoneLegs(); 		radleg.Name = "rotting bone leggings"; 	radleg.Hue = 0xB97; 	radleg.PoisonBonus = 10; 	this.PackItem( radleg ); 	break;
							case 4: BoneHelm radhelm = new BoneHelm(); 		radhelm.Name = "rotting bone helm"; 	radhelm.Hue = 0xB97; 	radhelm.PoisonBonus = 10; 	this.PackItem( radhelm ); 	break;
						}
					}

					if ( this.Body == 56 || this.Body == 168 ){ BattleAxe radaxe = new BattleAxe(); radaxe.Name = "rusty battle axe"; radaxe.Hue = 0xB97; radaxe.AosElementDamages.Poison=50; this.PackItem( radaxe ); }
					if ( this.Body == 57 ){ Scimitar radsim = new Scimitar(); radsim.Name = "rusty scimitar"; radsim.Hue = 0xB97;	radsim.AosElementDamages.Poison=50; this.PackItem( radsim ); }
					if ( this.Body == 170 || this.Body == 327 ){ Longsword radswd = new Longsword(); radswd.Name = "rusty longsword"; radswd.Hue = 0xB97;	radswd.AosElementDamages.Poison=50; this.PackItem( radswd ); }
					if ( this.Body == 57 || this.Body == 168 || this.Body == 170 ){ WoodenShield radshield = new WoodenShield(); radshield.Name = "rotting shield"; radshield.Hue = 0xB97; radshield.PoisonBonus = 5; this.PackItem( radshield ); }
				}
			}

			if ( reg.IsPartOf( "the Tower of Brass" ) )
			{
				if ( this is BloodDemon )
				{
					this.Hue = 0x480;
					this.Body = 9;
					switch ( Utility.RandomMinMax( 0, 5 ) )
					{
						case 0: this.Title = "the ice daemon";			break;
						case 1: this.Title = "the daemon of ice";		break;
						case 2: this.Title = "of the icy veil";			break;
						case 3: this.Title = "of the frozen void";		break;
						case 4: this.Title = "of the frozen wastes";	break;
						case 5: this.Title = "of the icy depths";		break;
					}
				}
				else if ( this is CrystalElemental )
				{
					this.Hue = Utility.RandomList( 0x54B, 0x54C, 0x54D, 0x54E, 0x54F, 0x550 );
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 50 );
					this.SetDamageType( ResistanceType.Poison, 0 );
					this.SetDamageType( ResistanceType.Physical, 50 );
					this.SetDamageType( ResistanceType.Energy, 0 );
					this.AddItem( new LightSource() );
				}
				else if ( this is Brigand )
				{
					for ( int i = 0; i < this.Items.Count; ++i )
					{
						Item item = this.Items[i];

						if ( item is Hair || item is Beard )
						{
							item.Hue = 0x455;
						}
						else if ( ( item is BasePants ) || ( item is BaseOuterLegs ) )
						{
							item.Delete();
							AddItem( new Kilt(Utility.RandomYellowHue()) );
						}
						else if ( item is BaseClothing || item is BaseWeapon || item is BaseArmor || item is BaseTool )
						{
							item.Hue = Utility.RandomYellowHue();
						}
					}

					if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.FindItemOnLayer( Layer.OneHanded ).Delete(); }
					if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }

					this.AddItem( new Pickaxe() );
					this.PackItem ( new CopperOre( Utility.RandomMinMax( 1, 3 ) ) );
					this.HairHue = 0x455;
					this.FacialHairHue = 0x455;
					this.Title = "the miner";
				}
				else if ( this is IronCobra )
				{
					this.Name = "a brass serpent";
					this.Hue = MaterialInfo.GetMaterialColor( "brass", "monster", 0 );
				}

				if ( this.Backpack != null && !(this is Brigand) && !(this is GhostWarrior) && !(this is GhostWizard) )
				{
					for ( int i = 0; i < this.Items.Count; ++i )
					{
						Item item = this.Items[i];

						if ( item is BaseWeapon )
						{
							BaseWeapon iweapon = (BaseWeapon)item;
							if ( Server.Misc.MaterialInfo.IsMetalItem( item ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ iweapon.Resource = CraftResource.Brass; iweapon.Hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); }
							else { item.Hue = Utility.RandomYellowHue(); }
						}
						else if ( item is BaseArmor )
						{
							BaseArmor iarmor = (BaseArmor)item;
							if ( Server.Misc.MaterialInfo.IsMetalItem( item ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ iarmor.Resource = CraftResource.Brass; iarmor.Hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); }
							else { item.Hue = Utility.RandomYellowHue(); }
						}
						else if ( ( item is BasePants ) || ( item is BaseOuterLegs ) )
						{
							item.Delete();
							AddItem( new Kilt(Utility.RandomYellowHue()) );
						}
						else if ( item is BaseClothing || item is BaseTool )
						{
							item.Hue = Utility.RandomYellowHue();
						}
					}

					List<Item> brasses = new List<Item>();
					foreach( Item i in this.Backpack.Items )
					{
						if ( i is BaseWeapon && Utility.RandomMinMax( 1, 20 ) == 1 )
						{
							BaseWeapon iweapon = (BaseWeapon)i;
							if ( Server.Misc.MaterialInfo.IsMetalItem( i ) ){ iweapon.Resource = CraftResource.Brass; }
						}
						else if ( i is BaseArmor && Utility.RandomMinMax( 1, 20 ) == 1 )
						{
							BaseArmor iarmor = (BaseArmor)i;
							if ( Server.Misc.MaterialInfo.IsMetalItem( i ) ){ iarmor.Resource = CraftResource.Brass; }
						}
						else if ( i is IronIngot )
						{
							brasses.Add(i);
						}
					}
					foreach ( Item brs in brasses )
					{
						this.PackItem ( new BrassIngot( brs.Amount ) );
						brs.Delete();
					}
				}
			}
			if ( reg.IsPartOf( "the Castle of Dracula" ) )
			{
				if ( this is OrcCaptain )
				{
					this.Title = "the orc miner";
					this.Body = 17;

					List<Item> belongings = new List<Item>();
					foreach( Item i in this.Backpack.Items )
					{
						belongings.Add(i);
					}
					foreach ( Item stuff in belongings )
					{
						stuff.Delete();
					}

					switch ( Utility.RandomMinMax( 0, 1 ) )
					{
						case 0: this.PackItem( new Pickaxe() ); break;
						case 1: this.PackItem( new Shovel() ); break;
					}

					this.PackItem ( new IronOre( Utility.RandomMinMax( 1, 3 ) ) );

					if ( Utility.RandomMinMax( 1, 10 ) > 3 )
					{
						switch ( Utility.RandomMinMax( 0, 5 ) )
						{
							case 0: this.PackItem( new BreadLoaf( Utility.RandomMinMax( 1, 3 ) ) ); break;
							case 1: this.PackItem( new CheeseWheel( Utility.RandomMinMax( 1, 3 ) ) ); break;
							case 2: this.PackItem( new Ribs( Utility.RandomMinMax( 1, 3 ) ) ); break;
							case 3: this.PackItem( new Apple( Utility.RandomMinMax( 1, 3 ) ) ); break;
							case 4: this.PackItem( new CookedBird( Utility.RandomMinMax( 1, 3 ) ) ); break;
							case 5: this.PackItem( new LambLeg( Utility.RandomMinMax( 1, 3 ) ) ); break;
						}
					}
					if ( Utility.RandomMinMax( 1, 10 ) > 3 )
					{
						switch ( Utility.RandomMinMax( 0, 4 ) )
						{
							case 0: this.PackItem( new BeverageBottle( BeverageType.Ale ) ); break;
							case 1: this.PackItem( new BeverageBottle( BeverageType.Wine ) ); break;
							case 2: this.PackItem( new BeverageBottle( BeverageType.Liquor ) ); break;
							case 3: this.PackItem( new Jug( BeverageType.Cider ) ); break;
							case 4: this.PackItem( new Waterskin() ); break;
						}
					}
					if ( Utility.RandomMinMax( 1, 10 ) > 3 )
					{
						switch ( Utility.RandomMinMax( 0, 2 ) )
						{
							case 0: this.PackItem( new Torch() ); break;
							case 1: this.PackItem( new Candle() ); break;
							case 2: this.PackItem( new Lantern() ); break;
						}
					}
				}
				else if ( this is BoneKnight && this.X >= 6978 && this.Y >= 1670 && this.X <= 6998 && this.Y <= 1697 )
				{
					this.Name = "a skeletal jailor";
				}
				else if ( this is LivingStoneStatue )
				{
					this.Name = "a giant statue";
					this.Body = 325;
					this.Hue = 0x847;
					BeefUp( this, 3 );
				}
				else if ( this is LivingIronStatue )
				{
					this.Hue = 0x6DF;
				}
			}
			if ( reg.IsPartOf( "Stonegate Castle" ) )
			{
				if ( this is Daemon && this.X >= 6512 && this.X <= 6551 && this.Y >= 2782 && this.Y <= 2836 )
				{
					this.Name = "Balinor";
					this.Title = "the Guardian of Stonegate";
					this.EmoteHue = 123;
					this.Body = 779;
					this.BaseSoundID = 357;
				}
				else if ( this is Daemon && this.X >= 6756 && this.X <= 6878 && this.Y>= 2464 && this.Y<= 2544 )
				{
					if ( Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						Item bonearmor = new BoneChest(); bonearmor.Delete();
						int reflect = 0;

						switch ( Utility.RandomMinMax( 0, 4 ) )
						{
							case 0: bonearmor = new BoneChest(); bonearmor.Name = "daemon ash bone tunic";							reflect = 12; break;
							case 1: bonearmor = new BoneArms(); bonearmor.Name = "daemon ash bone bracers";							reflect = 8; break;
							case 2: bonearmor = new BoneLegs(); bonearmor.Name = "daemon ash bone leggings";						reflect = 10; break;
							case 3: bonearmor = new BoneGloves(); bonearmor.Name = "daemon ash bone gauntlets";						reflect = 6; break;
							case 4: bonearmor = new BoneHelm(); bonearmor.ItemID = 0x1F0C; bonearmor.Name = "daemon ash bone helm";	reflect = 6; break;
						}

						BaseArmor bones = (BaseArmor)bonearmor;
						bones.Durability = ArmorDurabilityLevel.Indestructible;
						bones.ProtectionLevel = ArmorProtectionLevel.Invulnerability; 
						bones.FireBonus = 8;
						bones.Attributes.ReflectPhysical = reflect;
						bones.Hue = 0xB85;
						PackItem( bones );
					}
				}
				else if ( this is CrystalElemental )
				{
					this.Name = "a gem elemental";
					this.AddLoot( LootPack.Gems, Utility.RandomMinMax( 7, 12 ) );
					this.Hue = Utility.RandomList( 0x48D, 0x48E, 0x48F, 0x490, 0x491 );
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Poison, 0 );
					this.SetDamageType( ResistanceType.Physical, 20 );
					this.SetDamageType( ResistanceType.Energy, 80 );
					this.AddItem( new LightSource() );
				}
				else if ( this is MonstrousSpider )
				{
					this.Name = "an ash crawler";
					this.Hue = 0x774;
				}
				else if ( this is CaveLizard )
				{
					this.Name = "a stone lizard";
				}
				else if ( this is MinotaurScout )
				{
					this.Name = "a minotaur berserker";
				}
				else if ( this is SeaTroll )
				{
					this.Name = "a deep water troll";
				}
				else if ( this is OrcishLord )
				{
					this.Title = "an orc barbarian";
				}
				else if ( this is BoneKnight )
				{
					this.Name = "a burnt skeleton";
					this.Hue = 0xA78;
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 60 );
					this.SetDamageType( ResistanceType.Physical, 40 );
					this.SetDamageType( ResistanceType.Energy, 0 );
					this.SetDamageType( ResistanceType.Poison, 0 );
					this.Body = Utility.RandomList( 57, 50, 56 );

					List<Item> belongings = new List<Item>();
					foreach( Item i in this.Backpack.Items )
					{
						belongings.Add(i);
					}
					foreach ( Item stuff in belongings )
					{
						stuff.Delete();
					}

					GenerateLoot( true );

					if ( this.Body == 50 || this.Body == 57 || ( this.Body == 56 && Utility.Random( 4 ) == 1 ) )
					{
						switch ( Utility.Random( 5 ) )
						{
							case 0: BoneArms asharm = new BoneArms(); 		asharm.Name = "burnt bone arms"; 		asharm.Hue = 0xA78; 	asharm.FireBonus = 10; 		this.PackItem( asharm ); 	break;
							case 1: BoneChest ashchest = new BoneChest(); 	ashchest.Name = "burnt bone chest"; 	ashchest.Hue = 0xA78; 	ashchest.FireBonus = 10; 	this.PackItem( ashchest ); 	break;
							case 2: BoneGloves ashglove = new BoneGloves(); ashglove.Name = "burnt bone gloves"; 	ashglove.Hue = 0xA78; 	ashglove.FireBonus = 10; 	this.PackItem( ashglove ); 	break;
							case 3: BoneLegs ashleg = new BoneLegs(); 		ashleg.Name = "burnt bone leggings"; 	ashleg.Hue = 0xA78; 	ashleg.FireBonus = 10; 		this.PackItem( ashleg ); 	break;
							case 4: BoneHelm ashhelm = new BoneHelm(); 		ashhelm.Name = "burnt bone helm"; 		ashhelm.Hue = 0xA78; 	ashhelm.FireBonus = 10; 	this.PackItem( ashhelm ); 	break;
						}
					}

					if ( this.Body == 56 || this.Body == 168 ){ BattleAxe radaxe = new BattleAxe(); radaxe.Name = "burnt battle axe"; radaxe.Hue = 0xA78; radaxe.AosElementDamages.Fire=50; this.PackItem( radaxe ); }
					if ( this.Body == 57 ){ Scimitar radsim = new Scimitar(); radsim.Name = "burnt scimitar"; radsim.Hue = 0xA78;	radsim.AosElementDamages.Fire=50; this.PackItem( radsim ); }
					if ( this.Body == 170 || this.Body == 327 ){ Longsword radswd = new Longsword(); radswd.Name = "burnt longsword"; radswd.Hue = 0xA78;	radswd.AosElementDamages.Fire=50; this.PackItem( radswd ); }
					if ( this.Body == 57 || this.Body == 168 || this.Body == 170 ){ WoodenShield radshield = new WoodenShield(); radshield.Name = "burnt shield"; radshield.Hue = 0xA78; radshield.FireBonus = 5; this.PackItem( radshield ); }
				}
			}
			if ( reg.IsPartOf( "the Ancient Elven Mine" ) || reg.IsPartOf( "the Undersea Pass" ) )
			{
				if ( this is ShamanicCyclops )
				{
					this.Name = NameList.RandomName( "giant" );
					this.Title = "the warlord";
				}
				if ( this is Urk )
				{
					this.Hue = 0x8A4;

					MorphingTime.RemoveMyClothes( this );

					Item helm = new WornHumanDeco();
						helm.Name = "orcish face";
						helm.ItemID = 0x141B;
						helm.Hue = 0x8A4;
						helm.Layer = Layer.Helm;
						AddItem( helm );

					Item boots = new Boots();
						boots.Name = "orcish boots";
						boots.Hue = 0x97D;
						AddItem( boots );

					int DressUpAs = Utility.RandomMinMax( 1, 4 );

					if ( DressUpAs == 1 )
					{
						LeatherArms drowarms = new LeatherArms();
							drowarms.Name = "drow skin arms";
							drowarms.Attributes.BonusDex = 4;
							drowarms.SkillBonuses.SetValues( 0, SkillName.MagicResist, 6 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ drowarms.LootType = LootType.Blessed; }
							drowarms.Hue = 0x966;
							AddItem( drowarms );

						LeatherChest drowchest = new LeatherChest();
							drowchest.Name = "drow skin tunic";
							drowchest.Attributes.BonusDex = 5;
							drowchest.SkillBonuses.SetValues( 0, SkillName.MagicResist, 8 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ drowchest.LootType = LootType.Blessed; }
							drowchest.Hue = 0x966;
							AddItem( drowchest );

						LeatherGloves drowgloves = new LeatherGloves();
							drowgloves.Name = "drow skin gloves";
							drowgloves.Attributes.BonusDex = 3;
							drowgloves.SkillBonuses.SetValues( 0, SkillName.MagicResist, 5 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ drowgloves.LootType = LootType.Blessed; }
							drowgloves.Hue = 0x966;
							AddItem( drowgloves );

						LeatherGorget drowgorget = new LeatherGorget();
							drowgorget.Name = "drow skin gorget";
							drowgorget.Attributes.BonusDex = 2;
							drowgorget.SkillBonuses.SetValues( 0, SkillName.MagicResist, 4 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ drowgorget.LootType = LootType.Blessed; }
							drowgorget.Hue = 0x966;
							AddItem( drowgorget );

						LeatherLegs drowlegs = new LeatherLegs();
							drowlegs.Name = "drow skin leggings";
							drowlegs.Attributes.BonusDex = 4;
							drowlegs.SkillBonuses.SetValues( 0, SkillName.MagicResist, 7 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ drowlegs.LootType = LootType.Blessed; }
							drowlegs.Hue = 0x966;
							AddItem( drowlegs );
					}
					else if ( DressUpAs == 2 )
					{
						BoneChest bonechest = new BoneChest();
							bonechest.Name = "drow bone chest piece";
							bonechest.Attributes.BonusDex = 4;
							bonechest.SkillBonuses.SetValues( 0, SkillName.MagicResist, 8 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ bonechest.LootType = LootType.Blessed; }
							bonechest.Hue = 0x966;
							AddItem( bonechest );

						BoneArms bonearms = new BoneArms();
							bonearms.Name = "drow bone bracers";
							bonearms.Attributes.BonusDex = 3;
							bonearms.SkillBonuses.SetValues( 0, SkillName.MagicResist, 6 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ bonearms.LootType = LootType.Blessed; }
							bonearms.Hue = 0x966;
							AddItem( bonearms );

						BoneLegs bonelegs = new BoneLegs();
							bonelegs.Name = "drow bone leggings";
							bonelegs.Attributes.BonusDex = 3;
							bonelegs.SkillBonuses.SetValues( 0, SkillName.MagicResist, 7 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ bonelegs.LootType = LootType.Blessed; }
							bonelegs.Hue = 0x966;
							AddItem( bonelegs );

						BoneGloves bonegloves = new BoneGloves();
							bonegloves.Name = "drow bone gauntlets";
							bonegloves.Attributes.BonusDex = 2;
							bonegloves.SkillBonuses.SetValues( 0, SkillName.MagicResist, 5 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ bonegloves.LootType = LootType.Blessed; }
							bonegloves.Hue = 0x966;
							AddItem( bonegloves );
					}
					else // MINER
					{
						LeatherGloves minegloves = new LeatherGloves();
							minegloves.Name = "miner gloves";
							minegloves.SkillBonuses.SetValues( 0, SkillName.Mining, 5 );
							if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ minegloves.LootType = LootType.Blessed; }
							minegloves.Hue = 0x97D;
							AddItem( minegloves );

						Item cloth5 = new LoinCloth();
							cloth5.Hue = 0x97D;
							cloth5.Name = "orcish loin cloth";
							AddItem( cloth5 );

						AddItem( new Pickaxe() );
						this.Title = "the orc miner";
					}

					if ( DressUpAs < 3 )
					{
						BaseWeapon weapon = new BattleAxe();

						switch ( Utility.Random( 28 ))
						{
							case 0: weapon = new BattleAxe(); weapon.Name = "battle axe"; break;
							case 1: weapon = new VikingSword(); weapon.Name = "great sword"; break;
							case 2: weapon = new Halberd(); weapon.Name = "halberd"; break;
							case 3: weapon = new DoubleAxe(); weapon.Name = "double axe"; break;
							case 4: weapon = new ExecutionersAxe(); weapon.Name = "great axe"; break;
							case 5: weapon = new WarAxe(); weapon.Name = "war axe"; break;
							case 6: weapon = new TwoHandedAxe(); weapon.Name = "two handed axe"; break;
							case 7: weapon = new Cutlass(); weapon.Name = "cutlass"; break;
							case 8: weapon = new Katana(); weapon.Name = "katana"; break;
							case 9: weapon = new Kryss(); weapon.Name = "kryss"; break;
							case 10: weapon = new Broadsword(); weapon.Name = "broadsword"; break;
							case 11: weapon = new Longsword(); weapon.Name = "longsword"; break;
							case 12: weapon = new ThinLongsword(); weapon.Name = "longsword"; break;
							case 13: weapon = new Scimitar(); weapon.Name = "scimitar"; break;
							case 14: weapon = new BoneHarvester(); weapon.Name = "sickle"; break;
							case 15: weapon = new CrescentBlade(); weapon.Name = "crescent blade"; break;
							case 16: weapon = new DoubleBladedStaff(); weapon.Name = "double bladed staff"; break;
							case 17: weapon = new Pike(); weapon.Name = "pike"; break;
							case 18: weapon = new Scythe(); weapon.Name = "scythe"; break;
							case 19: weapon = new Pitchfork(); weapon.Name = "trident"; break;
							case 20: weapon = new ShortSpear(); weapon.Name = "short spear"; break;
							case 21: weapon = new Spear(); weapon.Name = "spear"; break;
							case 22: weapon = new Club(); weapon.Name = "club"; break;
							case 23: weapon = new HammerPick(); weapon.Name = "hammer pick"; break;
							case 24: weapon = new Mace(); weapon.Name = "mace"; break;
							case 25: weapon = new Maul(); weapon.Name = "maul"; break;
							case 26: weapon = new WarHammer(); weapon.Name = "war hammer"; break;
							case 27: weapon = new WarMace(); weapon.Name = "war mace"; break;
						}

						weapon.Name = "orcish " + weapon.Name;
						weapon.Hue = 0x97D;
						weapon.MinDamage = weapon.MinDamage + 3;
						weapon.MaxDamage = weapon.MaxDamage + 5;
						AddItem( weapon );

						switch ( Utility.RandomMinMax( 0, 5 ) )
						{
							case 0: this.Title = "the orc warrior"; break;
							case 1: this.Title = "the orc savage"; break;
							case 2: this.Title = "the orc barbarian"; break;
							case 3: this.Title = "the orc fighter"; break;
							case 4: this.Title = "the orc gladiator"; break;
							case 5: this.Title = "the orc berserker"; break;
						}
					}
				}
			}
			if ( reg.IsPartOf( "the Caverns of Poseidon" ) )
			{
				if ( this is DeepSeaSerpent )
				{
					this.Name = "a great serpent";
					this.Hue = 21;
					this.Body = 0x67;
					switch ( Utility.RandomMinMax( 0, 5 ) )
					{
						case 0: this.Title = "from the frozen deep";		break;
						case 1: this.Title = "of the darkest sea";			break;
						case 2: this.Title = "from the deepest depths";		break;
						case 3: this.Title = "of the cold sea";				break;
						case 4: this.Title = "of the icy waves";			break;
						case 5: this.Title = "of the icy sea";				break;
					}
					this.AddItem( new LightSource() );
				}
				else if ( this is JadeSerpent )
				{
					this.Name = "a coldwater serpent";
					this.Hue = 0x48D;
					this.SetDamageType( ResistanceType.Cold, 50 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Poison, 0 );
					this.SetDamageType( ResistanceType.Physical, 50 );
					this.SetDamageType( ResistanceType.Energy, 0 );
					this.AddItem( new LightSource() );
				}
				else if ( this is PirateLand || this is PirateCaptain )
				{
					for ( int i = 0; i < this.Items.Count; ++i )
					{
						Item item = this.Items[i];

						if ( item is BaseClothing || item is BaseArmor )
						{
							item.Hue = Utility.RandomBlueHue();
						}
					}
				}
				else if ( this is SeaGiant )
				{
					this.Name = NameList.RandomName( "drakkul" );
					this.Title = "the gate keeper";
				}
				else if ( this is SwampTentacle )
				{
					this.Name = "a kelp fiend";
				}
				else if ( this is BloodSnake )
				{
					this.Name = "a sea viper";
					this.Hue = 0x555;
				}
				else if ( this is LichLord )
				{
					this.Title = "the lich of the deep";
					this.Hue = 0x48D;
					this.AddItem( new LightSource() );
				}
				else if ( this is CrystalElemental )
				{
					this.Name = "a nox elemental";
					this.Hue = 0x48F;
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Poison, 50 );
					this.SetDamageType( ResistanceType.Physical, 50 );
					this.SetDamageType( ResistanceType.Energy, 0 );
					this.AddItem( new LightSource() );
				}
				else if ( this is Devil )
				{
					this.Hue = 0x48F;
					this.AddItem( new LightSource() );
					this.SetDamageType( ResistanceType.Cold, 0 );
					this.SetDamageType( ResistanceType.Fire, 0 );
					this.SetDamageType( ResistanceType.Poison, 60 );
					this.SetDamageType( ResistanceType.Physical, 40 );
					this.SetDamageType( ResistanceType.Energy, 0 );
					switch ( Utility.RandomMinMax( 0, 5 ) )
					{
						case 0: this.Title = "the nox devil";			break;
						case 1: this.Title = "the shard devil";			break;
						case 2: this.Title = "of the poison veil";		break;
						case 3: this.Title = "of the venomous void";	break;
						case 4: this.Title = "of the foul wastes";		break;
						case 5: this.Title = "of the crystal depths";	break;
					}
				}
				else if ( this is BoneKnight )
				{
					this.Name = "a skeletal pirate";
				}
				else if ( this is AquaticGhoul )
				{
					this.Name = "a ghoulish pirate";
				}
			}

			if ( reg.IsPartOf( "the Vault of the Black Knight" ) )
			{
				if ( this is LivingShadowIronStatue ){ this.Body = 485; }
				if ( this is LivingGoldStatue ){ this.Body = 485; }
				if ( this is LivingBronzeStatue ){ this.Body = 485; }
			}

			Server.Mobiles.PremiumSpawner.SpreadOut( this );

			if ( this is BasePerson || this is BaseVendor || this is BluePlayer || this is Townsperson || m_bSummoned || m_bControlled )
			{
				Heat = 0;
			}

			if ( reg.IsPartOf( "the Barge of the Dead" ) && !(this is BaseVendor) ) // NO STOWAWAYS //////////////
			{
				this.Delete();
			}

			if ( this.Backpack != null )
			{
				int cashgrab = this.TotalGold;

				List<Item> pockets = new List<Item>();
				foreach( Item i in this.Backpack.Items )
				{
					if ( i is Gold )
					{
						pockets.Add(i);
					}
				}
				foreach ( Item coins in pockets )
				{
					coins.Delete();
				}

				if ( cashgrab > 0 )
				{
					if (	(Region.Find( this.Location, this.Map )).IsPartOf( "the Ancient Crash Site" ) || 
							(Region.Find( this.Location, this.Map )).IsPartOf( "the Ancient Sky Ship" ) )
					{
						int xormite = (int)(cashgrab/3);
						PackItem( new DDXormite( xormite ) );
					}
					else if ( (Region.Find( this.Location, this.Map )).IsPartOf( "the Mines of Morinia" ) && Utility.RandomMinMax( 1, 5 ) == 1 )
					{
						int crystals = (int)(cashgrab/5);
						PackItem( new Crystals( crystals ) );
					}
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" )
					{
						int jewels = (int)(cashgrab/2);
						PackItem( new DDJewels( jewels ) );
					}
					else
					{
						cashgrab = cashgrab * 10;

						if (Utility.RandomMinMax( 1, 100 ) > 99)
						{
							int nGm = 20;
							int nGms = (int)Math.Floor((decimal)(cashgrab/nGm));
							if (nGms > 0)
							{
								int nGemstones = Utility.RandomMinMax( 1, nGms );
									if ( nGemstones < 10 ){ nGemstones = Utility.RandomMinMax( 10, 15 ); }
								PackItem( new DDGemstones( nGemstones ) );
								cashgrab = cashgrab - (nGemstones * nGm);
							}
						}
						if (Utility.RandomMinMax( 1, 100 ) > 95)
						{
							int nGs = 10;
							int nGps = (int)Math.Floor((decimal)(cashgrab/nGs));
							if (nGps > 0)
							{
								int nNuggets = Utility.RandomMinMax( 1, nGps );
									if ( nNuggets < 10 ){ nNuggets = Utility.RandomMinMax( 10, 15 ); }
								PackItem( new DDGoldNuggets( nNuggets ) );
								cashgrab = cashgrab - (nNuggets * nGs);
							}
						}
						if (Utility.RandomMinMax( 1, 100 ) > 66)
						{
							int nGp = 10;
							int nGpp = (int)Math.Floor((decimal)(cashgrab/nGp));
							if (nGpp > 0)
							{
								int nGold = Utility.RandomMinMax( 1, nGpp );
									if ( nGold < 10 ){ nGold = Utility.RandomMinMax( 10, 15 ); }
								PackItem( new Gold( nGold ) );
								cashgrab = cashgrab - (nGold * nGp);
							}
						}
						if (Utility.RandomMinMax( 1, 100 ) > 33)
						{
							int nSp = 5;
							int nSpp = (int)Math.Floor((decimal)(cashgrab/nSp));
							if (nSpp > 0)
							{
								int nSilver = Utility.RandomMinMax( 1, nSpp );
									if ( nSilver < 10 ){ nSilver = Utility.RandomMinMax( 10, 15 ); }
								PackItem( new DDSilver( nSilver ) );
								cashgrab = cashgrab - (nSilver * nSp);
							}
						}
						if (cashgrab > 0){ if ( cashgrab < 10 ){ cashgrab = Utility.RandomMinMax( 10, 15 ); } PackItem( new DDCopper( cashgrab ) ); }
					}
					
				}
				
			}

			if ( this.Body != 400 && this.Body != 401 ){ this.TithingPoints = this.Body; } // STORE THE BODY VALUE IN AN UNUSED VARIABLE FOR NECRO ANIMATE

			if (Utility.RandomDouble() < (AetherGlobe.BalanceLevel / 150000) && Utility.RandomBool() && this.Karma < 0 && !(this is Zombiex) && !(this is BaseUndead) &&!this.Tamable && !(this is BaseVendor) && !(this is BaseCursed) && !(this is BasePerson) && !(this is BaseRed) ) // FINAL evil curse bump in stats
			{
				this.RawStr *= 2;
				this.RawDex *= 2;
				this.RawInt *= 2;
				this.Hue = 1989;
				this.Title = this.Title + " *Enraged*";
				this.HitsMaxSeed *= 2;
				this.Hits = this.HitsMaxSeed;
				this.AIFullSpeedActive = true;
				if ( Utility.RandomDouble() <  ( 0.20 *  ((double)Math.Abs(this.Karma) / 20000) ) )
					this.PackItem ( new BalanceSpike() );
			}
			else if (Utility.RandomDouble() < ( (100000-AetherGlobe.BalanceLevel) / 150000) && Utility.RandomBool() && this.Karma > 0 && !this.Tamable && !(this is BaseVendor) && !(this is Zombiex) && !(this is BaseUndead) && !(this is BasePerson) && !(this is BaseBlue) && !(this is BaseChild))  // FINAL evil curse bump in stats
			{
				this.RawStr *= 2;
				this.RawDex *= 2;
				this.RawInt *= 2;
				this.Hue = 1985;
				this.Title = this.Title + " *Righteous*";
				this.HitsMaxSeed *= 2;
				this.Hits = this.HitsMaxSeed;
				this.AIFullSpeedActive = true;
				if ( Utility.RandomDouble() <  ( 0.20 *  ((double)Math.Abs(this.Karma) / 20000) ) )
					this.PackItem ( new BalanceSpike() );
			}

			if (this.Tamable && !(this is Zombiex) && !(this is BaseUndead) && !this.Controlled)
			{


					int rarity = 3; //randomize new pets on world generation
					int chance = Utility.RandomMinMax(1, 200);
					if (chance == 69)  // 0.5% chance
					    rarity = 6;
					else if (chance <= 10) // 5% chance
					    rarity = 5;
					else if (chance <= 20) // 10% chance
					    rarity = 4;
					else if (chance >= 195) // 2.5% chance
					    rarity = 1;
					else if (chance >= 180)  // 10% chance
					    rarity = 2;
					 

					if (rarity > 3) // 10% chance being stronger
					{
					    this.RawStr = (int)((double)this.RawStr*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    this.RawDex = (int)((double)this.RawDex*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    this.RawInt = (int)((double)this.RawInt*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    this.HitsMaxSeed = (int)((double)this.HitsMaxSeed*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));

					    this.Hits = this.HitsMaxSeed;
					    this.DamageMax = (int)((double)this.DamageMax*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    this.DamageMin = (int)((double)this.DamageMin*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    if (this.DamageMin > this.DamageMax)
						this.DamageMin = this.DamageMax -1;
					}
					else if (rarity < 3) // 10% chance of being weaker
					{
					    this.RawStr = (int)((double)this.RawStr*( ((double)(Utility.RandomMinMax(1, rarity) * 0.40))));
					    this.RawDex = (int)((double)this.RawDex*( ((double)(Utility.RandomMinMax(1, rarity) * 0.40))));
					    this.RawInt = (int)((double)this.RawInt*( ((double)(Utility.RandomMinMax(1, rarity) * 0.40))));
					    this.HitsMaxSeed = (int)((double)this.HitsMaxSeed*( ((double)(Utility.RandomMinMax(1, rarity) * 0.40))));

					    this.Hits = this.HitsMaxSeed;
					    this.DamageMax = (int)((double)this.DamageMax*( ((double)(Utility.RandomMinMax(1, rarity) * 0.40))));
					    this.DamageMin = (int)((double)this.DamageMin*( ((double)(Utility.RandomMinMax(1, rarity) * 0.40))));
					    if (this.DamageMin > this.DamageMax)
						this.DamageMin = this.DamageMax -1;
					}
					else if (rarity == 3) // 80% chance of small change only
					{
					    this.RawStr += (int)((double)this.RawStr*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    this.RawDex += (int)((double)this.RawDex*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    this.RawInt += (int)((double)this.RawInt*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    this.HitsMaxSeed += (int)((double)this.HitsMaxSeed*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));

					    this.Hits = this.HitsMaxSeed;
					    this.DamageMax += (int)((double)this.DamageMax*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    this.DamageMin += (int)((double)this.DamageMin*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    if (this.DamageMin > this.DamageMax)
						this.DamageMin = this.DamageMax -1;
					}

			}

			CheckBasicSkills();
			DynamicFameKarma();
			DynamicTaming(true);
			DynamicGold();
			NameColor();

			if (AdventuresFunctions.IsPuritain((object)this) && this.midrace > 0 && ( !(this is MidlandVendor) || !(this is TownHerald)) && this.Fame < 10000)
				DynamicBeefUp(this, (this.Fame + Utility.RandomMinMax(2000, 5000)));

			else if ( Heat > 0 && !IsParagon && !(this is Zombiex) && !(this is BaseUndead) )
			{
				BeefUp( this, Heat );
				DynamicFameKarma();
			}

			// make enemies more challenging by giving them speed
			if (!(this is BasePerson) && !(this is BaseVendor) && !(this is BasePerson) && !(this is Townsperson) && !AIFullSpeedActive && !AIFullSpeedPassive)
			{
				double famy = (double)this.Fame;
				if (famy >= 30000)
					famy = 25000;
				double final = (famy / 30000) * 0.05;
				if (Utility.RandomDouble() <= final)
				{
					m_superspeed = true;
					AIFullSpeedActive = true;
					AIFullSpeedPassive = false;
				}		
			}

			this.InvalidateProperties();

			if (Home.X == 0 && Home.Y == 0 && Home.Z == 0)
			{
				Home = this.Location;
			}

		}

		public void DynamicFameKarma() //final - no more arbitrary fame/karma values! Set Karma/Fame standard based on an algo
		{

			//darkmoor vendors
			if (this.Map == Map.Ilshenar && this.X <= 1007 && this.Y <= 1280 && ( this is BasePerson || this is BaseGuildmaster || this is TownHerald || this is BaseVendor || this is AnimalTrainerLord || this is Townsperson || this is PlayerVendor || this.Blessed || this.FightMode == FightMode.None || this is Citizens) )
				this.Karma = -(Utility.RandomMinMax(50, 250));

			else if ( !(this is BasePerson) && !(this is BaseVendor) && !(this is AnimalTrainerLord) && !(this is Townsperson) && !(this is PlayerVendor) && !(this.Blessed) && this.FightMode != FightMode.None && !(this is Citizens) )	
				{
						

					double basecalc = 1;
					double modifier = 1;

					// start with base stats and multiply by damage for a base value
					double resists = ( ((double)this.PoisonResistSeed + (double)this.PhysicalResistanceSeed + (double)this.FireResistSeed + (double)this.ColdResistSeed + (double)this.EnergyResistSeed) )/1.1;
					basecalc = ( ( ((double)(this.RawStr + this.RawDex + this.RawInt)/3)+ resists + (this.HitsMax/3)) * (((double)this.DamageMin + (double)this.DamageMax )/1.3) ); 

					//change this value based on relevant skills (over 60 raises, below lowers)	

					if (this.AI == AIType.AI_Mage )
						basecalc *= 0.45 + (( this.Skills[SkillName.Magery].Value + this.Skills[SkillName.EvalInt].Value ) / 125.0);
					else if  (this.AI == AIType.AI_Archer || this.AI == AIType.AI_Archer2)
						basecalc *= (( this.Skills[SkillName.Archery].Value + this.Skills[SkillName.Tactics].Value ) / 125.0);
					else 
					{
						basecalc *= (( this.Skills[SkillName.Wrestling].Value + this.Skills[SkillName.Tactics].Value ) / 125.0);
					}
					// animals are less of a challenge
					if (this.AI == AIType.AI_Animal)
						basecalc /= 1.5;

					// modify based on attributes and abilities
								
					if (this.CanHeal)
						modifier += this.Skills[SkillName.Healing].Value / 200.0;
					
					if (this.HasBreath)
						modifier += (double)this.BreathComputeDamage() / 300.0;
						
					if (this.HitPoison != null)
					{
						if (this.HitPoison == Poison.Lethal)
							modifier += 0.5;
						else if (this.HitPoison == Poison.Deadly)
							modifier += 0.25;
						else if (this.HitPoison == Poison.Greater)
							modifier += 0.15;
						else 
							modifier += .1;
					}
					
					if (this is BullFrog || this is FireToad || this is GiantToad || this is IceToad || this is PoisonFrog ) //have a special latch ability
						modifier += 0.10;

					if (Backpack is StrongBackpack)
						modifier += 0.25;

					if (this.AIFullSpeedActive)
						modifier += 0.35;
					
					if (this is BaseMount)
						modifier += 0.25;
					
					if (this.IsParagon)
						modifier += 1;
					
					if (this.CanInfect)
						modifier += 0.25;

					if (this.CanChew)
						modifier += 0.1;

					if (this.CanAngerOnTame)
						modifier += 0.1;

					if (this is AuraCreature)
					{
						double auradamage = ( ((AuraCreature)this).MaxAuraDamage + ((AuraCreature)this).MinAuraDamage) /2;
						double auradelay = ( ((AuraCreature)this).MaxAuraDelay + ((AuraCreature)this).MaxAuraDelay) /2;
						
						modifier *= 1+ ( ( (auradamage / 200) + (1 / auradelay) ));
					}	
					
					if ( this is BaseBlue || this is BaseRed || this is BaseCursed || this is Widow || this is OphidianGeneral ) //harder mob types
						modifier *=1.5;
					
					if ( this is DemonKnight || this is BaseChampion || this is Widow ) // for bosses
						modifier *= 2.5;	
					
					// apply the attributes
					basecalc *= modifier;
					basecalc /= 3; // arbitrary balancing value				
							
					//diminishing returns to prevent 60,000 fame mobs
					double final = 0;
					double step = 500;
					double diminish = 0.95;
					if (basecalc < step)
						final = basecalc;						
					else 
					{	
						while ( basecalc > 0 )
						{
							if (basecalc > step)
							{
								basecalc -= step;
								final += step;
								
								if (final >22000)
									basecalc *= diminish * ((35000 - final)/13000); // limits to 35k karma
								else
									basecalc *= diminish;
							}
							else
							{
								final += basecalc * diminish;
								basecalc = 0;
							}
						}
					}

					if (this is Zombiex) // strange bug where zombiex had 20k+ fame and can't find out why
						final /= 3;

					// couple debug checks here
					if (final < 0 )
						final = Math.Abs(final);
					if (final == 0 && this.Karma != 0)
						final = (double)this.Karma;
					// apply based on karma duality
					if (this.Karma < 0 && final != 0)
						this.Karma = -((int)final);
					else if (this.Karma > 0 && final != 0)
						this.Karma = (int)final;
					else if (this.Karma == 0 && final != 0)
						this.Karma = 0;
					
					this.Fame = (int)final;	
					
					AdvancedAI = false; //i know this is useless since it is false by default, but im just making sure
					
					Region region = Region.Find( this.Location, this.Map );

					if (this is BaseBlue || this is BaseRed || this is BaseCursed || this is BaseChampion) //always default to smart
						AdvancedAI = true;
					else if ( !region.IsPartOf( typeof( ChampionSpawnRegion ) ) && !(region is ChampionSpawnRegion) ) 						
					{
						this.AdvancedAI = AdventuresFunctions.CheckAdvancedAI(this);	
					}
						
				}					
		}
		
		public void DynamicTaming(bool slotchange) //final - no more arbitrary taming min value!
		{
			
			if ( this.Tamable && !(this.Controlled)  )	
				{
					double finalcompute = this.Fame;

					if (!this.CanAngerOnTame && finalcompute >= 5000)
					{
						if (finalcompute <=  10000 && Utility.RandomBool())
							this.m_anger = true;
						else if (finalcompute > 10000)
							this.m_anger = true;
					}

					if (slotchange)
					{
						m_iControlSlots = (int)(finalcompute / 3250); // estimate max of 8 slots
						if (m_iControlSlots < 1)
							m_iControlSlots = 1;

						if (m_iControlSlots > 1 && this.Tamable)
							finalcompute /= 1+ (m_iControlSlots * .05);
					}

					//diminishing returns to prevent 125+ min taming required
					double final = 0;

					double karmatotame = 50; // 
					double diminish = 0.85; // 
					double step = 500;

					if (finalcompute < step)
						final = finalcompute / karmatotame;	

					else 
					{	
						while ( finalcompute > 0 )
						{
							if (finalcompute > step)
							{
								finalcompute -= step;
								final += step / karmatotame;
								
								if (final < 105)
									finalcompute *= diminish;
								else
									finalcompute *= (diminish * ((125 - final) / 20)); // makes sure max is 122 skill
							}
							else
							{
								final += (finalcompute/karmatotame) * diminish;
								finalcompute = 0;
							}
						}
					}			

					//if (this is Elephant || this is Mammoth)
					//	final += 10;

					m_dMinTameSkill = (int)Math.Ceiling(final) + (15-(15*(final/125))); // 15 addition only applies to taming value under 100


				}
		}

		public void DynamicGold ()
		{
			if ( !(this is BaseCreature) || this == null )
				return;
				
			Region reg = Region.Find( this.Location, this.Map );

			int amount = 0;
			if (this.Fame <= 200)
				return;
			else if (this.Fame <= 500)
				amount = Utility.RandomMinMax(5, 16);
			else if (this.Fame <= 1000)
				amount = Utility.RandomMinMax(10, 32);
			else if (this.Fame <= 5000)
				amount = Utility.RandomMinMax(20, 64);
			else if (this.Fame <= 10000)
				amount = Utility.RandomMinMax(40, 120);
			else if (this.Fame <= 15000)
				amount = Utility.RandomMinMax(80, 240);
			else if (this.Fame <= 20000)
				amount = Utility.RandomMinMax(160, 480);
			else if (this.Fame <= 25000)
				amount = Utility.RandomMinMax(320, 960);
			else if (this.Fame <= 30000)
				amount = Utility.RandomMinMax(640, 1920);
			else if (this.Fame >= 30001)
				amount = Utility.RandomMinMax(1280, 3840);

			if (AdvancedAI && this.AI == AIType.AI_Mage)
				amount = (int)((double)amount * 1.15);
				
			if ( ( ( reg.IsPartOf( typeof( ChampionSpawnRegion ) ) || reg is ChampionSpawnRegion ) && this is BaseChampion ) || this is DemonKnight || this is Widow || this is Sataness) 
				amount = Utility.RandomMinMax(15000, 25000);

			int diff = MyServerSettings.GetDifficultyLevel( this.Location, this.Map );
			//string wrld = Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y );
			

			if (reg.IsPartOf("the Land of Sosaria"))
				diff -=2;

			if (reg.IsPartOf("the Island of Umber Veil") || reg.IsPartOf("the Isles of Dread") || reg.IsPartOf("the Land of Lodoria"))
				diff --;

			//no change for serps, darkmoor
				
			if (reg.IsPartOf("the Land of Ambrosia") || reg.IsPartOf("the Bottle World of Kuldar") || reg.IsPartOf("the Savaged Empire"))
				diff +=1;
			
			if (reg.IsPartOf("the Underworld"))
				diff += 2;

			if (diff > 1)
				amount *= diff;

			else if (diff == 1)
				amount = (int)((double)amount*1.5);
			
			else if (diff == -1)
				amount = (int)((double)amount/1.5);

			else if (diff == -2)
				amount = (int)((double)amount/2);

			// animals shouldn't have gold
			if (this.AI == AIType.AI_Animal)
				amount = 0;

			if ( this.Backpack != null )
			{
				Item g = this.Backpack.FindItemByType(typeof(Gold));
				int JetsonWantsGold = amount;

				if (g != null)
				{
					if (JetsonWantsGold == 0)
						g.Delete();
					else if (JetsonWantsGold > 60000)
					{
						g.Amount = 60000;
						JetsonWantsGold -= 60000;
					}
					else 
					{
						g.Amount = JetsonWantsGold;
						JetsonWantsGold = 0;
					}
				}
				
				if (JetsonWantsGold != 0 && JetsonWantsGold > 60000)
				{
					while (JetsonWantsGold > 60000)
					{
						this.AddToBackpack( new Gold( 60000 ) );
						JetsonWantsGold -= 60000;
					}
					this.AddToBackpack( new Gold( JetsonWantsGold ) );
				}
				else if (JetsonWantsGold != 0 )
					this.AddToBackpack( new Gold( JetsonWantsGold ) );
			}

		}

		public void CheckBasicSkills() //final - ensure mobs have the correct skills
		{
			double skillbase = Utility.RandomMinMax(25, 45) + ((this.Fame / 35000) * 80);
			if (this.AI == AIType.AI_Mage || this.AI == AIType.AI_Mage )
			{
				if (this.Skills[SkillName.Magery].Value == 0 ||  this.Skills[SkillName.EvalInt].Value == 0)
				{
					SetSkill( SkillName.Magery, (skillbase -5), (skillbase +5) );
					SetSkill( SkillName.EvalInt, (skillbase -5), (skillbase +5) );
				}
				if ( this.AI == AIType.AI_Mage && (this.Skills[SkillName.Necromancy].Value == 0|| this.Skills[SkillName.SpiritSpeak].Value == 0 ) )
				{		
					SetSkill( SkillName.Necromancy, (skillbase -5), (skillbase +5) );
					SetSkill( SkillName.SpiritSpeak, (skillbase -5), (skillbase +5) );		
				}
			}
			else if  (this.AI == AIType.AI_Archer || this.AI == AIType.AI_Archer2)
			{
				if (this.Skills[SkillName.Archery].Value == 0 ||  this.Skills[SkillName.Tactics].Value == 0)
				{
					SetSkill( SkillName.Archery, (skillbase -5), (skillbase +5) );
					SetSkill( SkillName.Tactics, (skillbase -5), (skillbase +5) );	
					SetSkill( SkillName.Anatomy, (skillbase -5), (skillbase +5) );
				}
			}
			else 
			{
				if (this.Skills[SkillName.Wrestling].Value == 0 || this.Skills[SkillName.Tactics].Value == 0)
				{
					SetSkill( SkillName.Wrestling, (skillbase -5), (skillbase +5) );
					SetSkill( SkillName.Tactics, (skillbase -5), (skillbase +5) );	
					SetSkill( SkillName.Anatomy, (skillbase -5), (skillbase +5) );
				}
			}
			
			Region reg = Region.Find( this.Location, this.Map );
			if ( reg.IsPartOf( typeof( BardDungeonRegion ) ) )
			{
				double detect = 120 * ((double)this.Fame / 30000);
				if (detect > 110)
					detect = 110;
				
				if ( this.Skills[SkillName.DetectHidden].Value < detect )
					SetSkill( SkillName.DetectHidden, (detect -10), (detect +10) );
			}
		}

		public override ApplyPoisonResult ApplyPoison( Mobile from, Poison poison )
		{
			if ( !Alive || IsDeadPet )
				return ApplyPoisonResult.Immune;

			if ( Spells.Necromancy.EvilOmenSpell.TryEndEffect( this ) )
				poison = PoisonImpl.IncreaseLevel( poison );

			ApplyPoisonResult result = base.ApplyPoison( from, poison );

			if ( from != null && result == ApplyPoisonResult.Poisoned && PoisonTimer is PoisonImpl.PoisonTimer )
				(PoisonTimer as PoisonImpl.PoisonTimer).From = from;

			return result;
		}

		public override bool CheckPoisonImmunity( Mobile from, Poison poison )
		{
			if ( base.CheckPoisonImmunity( from, poison ) )
				return true;

			Poison p = this.PoisonImmune;

			return ( p != null && p.Level >= poison.Level );
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Loyalty
		{
			get
			{
				return m_Loyalty;
			}
			set
			{
				m_Loyalty = Math.Min( Math.Max( value, 0 ), MaxLoyalty );
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WayPoint CurrentWayPoint
		{
			get
			{
				return m_CurrentWayPoint;
			}
			set
			{
				m_CurrentWayPoint = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public IPoint2D TargetLocation
		{
			get
			{
				return m_TargetLocation;
			}
			set
			{
				m_TargetLocation = value;
			}
		}

		public virtual Mobile ConstantFocus{ get{ return null; } }

		public virtual bool DisallowAllMoves
		{
			get
			{
				return false;
			}
		}

		public virtual bool InitialInnocent
		{
			get
			{
				return false;
			}
		}

		public virtual bool AlwaysMurderer
		{
			get
			{
				return false;
			}
		}

		public virtual bool AlwaysAttackable
		{
			get
			{
				return false;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public virtual int DamageMin{ get{ return m_DamageMin; } set{ m_DamageMin = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public virtual int DamageMax{ get{ return m_DamageMax; } set{ m_DamageMax = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax
		{
			get
			{
				if ( m_HitsMax > 0 ) {
					int value = m_HitsMax + GetStatOffset( StatType.Str );

					if( value < 1 )
						value = 1;
					else if( value > 65000 )
						value = 65000;

					return value;
				}

				return Str;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HitsMaxSeed
		{
			get{ return m_HitsMax; }
			set{ m_HitsMax = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override int StamMax
		{
			get
			{
				if ( m_StamMax > 0 ) {
					int value = m_StamMax + GetStatOffset( StatType.Dex );

					if( value < 1 )
						value = 1;
					else if( value > 65000 )
						value = 65000;

					return value;
				}

				return Dex;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StamMaxSeed
		{
			get{ return m_StamMax; }
			set{ m_StamMax = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override int ManaMax
		{
			get
			{
				if ( m_ManaMax > 0 ) {
					int value = m_ManaMax + GetStatOffset( StatType.Int );

					if( value < 1 )
						value = 1;
					else if( value > 65000 )
						value = 65000;

					return value;
				}

				return Int;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ManaMaxSeed
		{
			get{ return m_ManaMax; }
			set{ m_ManaMax = value; }
		}

		public virtual bool CanOpenDoors
		{
			get
			{
				return !this.Body.IsAnimal && !this.Body.IsSea;
			}
		}

		public virtual bool CanMoveOverObstacles
		{
			get
			{
				return Core.AOS || this.Body.IsMonster;
			}
		}

		public virtual bool CanDestroyObstacles
		{
			get
			{
				// to enable breaking of furniture, 'return CanMoveOverObstacles;'
				return false;
			}
		}

		public void Unpacify()
		{
			BardEndTime = DateTime.UtcNow;
			BardPacified = false;
		}

		private HonorContext m_ReceivedHonorContext;

		public HonorContext ReceivedHonorContext{ get{ return m_ReceivedHonorContext; } set{ m_ReceivedHonorContext = value; } }

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{	
			
			if ( BardPacified && (HitsMax - Hits) * 0.001 > Utility.RandomDouble() )
				Unpacify();

			// OCEAN MONSTERS SHOULD NOT GET ARROWS JUST HITTING THEM BECAUSE THEY CANNOT NAVIGATE THE SHORE
			// SO THEY WILL LEAP AT AN ATTACKER ON SHORE IF THEY GET HURT FROM THEM
			if ( !(CanOnlyMoveOnSea( this )) && this.WhisperHue == 999 && this.Z < -2 && from.Z >=0 && from is PlayerMobile && from.InRange( this.Location, 15 ) && from.Alive && from.Map == this.Map && CanSee( from ) && !Server.Mobiles.BasePirate.IsSailor( this ) )
			{
				this.Home = this.Location; // SO THEY KNOW WHERE TO GO BACK TO
				from.PlaySound( 0x026 );
				Effects.SendLocationEffect( from.Location, from.Map, 0x23B2, 16 );
				this.Location = from.Location;
				this.Warmode = true;
				this.Combatant = from;
				this.CantWalk = false;
				this.CanSwim = true;
				this.Hidden = false;
			}

			int disruptThreshold;
			//NPCs can use bandages too!
			if( !Core.AOS )
				disruptThreshold = 0;
			else if( from != null && from.Player )
				disruptThreshold = 18;
			else
				disruptThreshold = 25;

			if( amount > disruptThreshold )
			{
				BandageContext c = BandageContext.GetContext( this );

				if( c != null )
					c.Slip();
			}

			if( Confidence.IsRegenerating( this ) )
				Confidence.StopRegenerating( this );

			WeightOverloading.FatigueOnDamage( this, amount );

			InhumanSpeech speechType = this.SpeechType;

			if ( speechType != null && !willKill )
				speechType.OnDamage( this, amount );

			if ( !Summoned && willKill )
			{
				Mobile leveler = from;

				if ( leveler is BaseCreature )
					leveler = ((BaseCreature)leveler).GetMaster();

				if ( leveler is PlayerMobile )
				{
					LevelItemManager.CheckItems( leveler, this );
				}
			}

			if ( m_ReceivedHonorContext != null )
				m_ReceivedHonorContext.OnTargetDamaged( from, amount );

			if ( willKill && from is PlayerMobile )
				Timer.DelayCall( TimeSpan.FromSeconds( 10 ), new TimerCallback( ((PlayerMobile) from).RecoverAmmo ) );

			// FINAL add chance that a mob attacked by a tamed pet will actually attack the controlmaster to make things interesting
			if ( from is BaseCreature && (this.Summoned || this.Controlled) && ((BaseCreature)from).ControlMaster is PlayerMobile && ((BaseCreature)from).ControlMaster != ControlMaster && Utility.RandomDouble() <= 0.05)
			{
					m_FocusMob = ((BaseCreature)from).ControlMaster;
					Combatant = ((BaseCreature)from).ControlMaster;
			}

			//loyalty loss on damage taken
			if (!this.Summoned && this.Controlled && this.ControlMaster != null)
			{
				if ( Utility.RandomDouble() < ((double)amount / (double)this.HitsMax)  )
					this.Loyalty -= Utility.RandomMinMax(0, 2);
			}
			
			//new injury system for tamed and controlled pets
			
			
			if (willKill && this.Tamable && this.Controlled && !this.IsParagon && !this.Summoned ) //only pets who are going to die may earn injury
			{
				double halflife = (double)(Convert.ToInt32(this.m_maxLevel) /2); //uint doesnt like being converted using (int) or (double) for some weird reason
				double lvl = (double)(Convert.ToInt32(this.Level));
				double maxlvl = (double)(Convert.ToInt32(this.m_maxLevel));
				
				if (lvl > halflife) //pets under half of max levels won't get injured.
				{
					double odds = 0.15; // start at 15% odds of an injury
					odds *= ( (lvl - halflife) / (maxlvl - halflife) ); //reduce the odds by the percentage difference to max level (max level pets get 15%, half level pets get 0%)
					
					int severity = 1; //set severity of the injury
					
					if ( Utility.RandomDouble() < ( ( ( (double)amount / (double)this.HitsMax) + (lvl / maxlvl) ) /2) && Utility.RandomBool() ) //big hits means more severity
						severity = Utility.RandomMinMax(1, 3);

					if (Utility.RandomDouble() < odds) //apply the injury
						GainInjury(severity);
				}
			}

			if ( from != null && from is PlayerMobile && ((PlayerMobile)from).BodyMod == 318 && ((double)this.Hits / (double)this.HitsMax) <= 0.80 ) // dark father morph
			{
				if (Utility.RandomDouble() <= 0.13)
				{
					MovingEffect( this, 0xECA, 10, 0, false, false, 0, 0 );
					PlaySound( 0x491 );		

					AOS.Damage( this, from, 5000, 20, 20, 20, 20, 20 );
				}
			}


			base.OnDamage( amount, from, willKill );
		}

		public virtual void OnDamagedBySpell( Mobile from )
		{
		}

		public virtual void OnHarmfulSpell( Mobile from )
		{
		}

		#region Alter[...]Damage From/To

		public virtual void AlterDamageScalarFrom( Mobile caster, ref double scalar )
		{
		}

		public virtual void AlterDamageScalarTo( Mobile target, ref double scalar )
		{
		}

		public virtual void AlterSpellDamageFrom( Mobile from, ref int damage )
		{
			foreach (Item item in GetItemsInRange(0))
			{
				if (item is HouseSign) // being attacked by a player in a home, thats not fair is it?
				{
					this.MoveToWorld(m_pHome, this.Map);
					from.Hits /= 2;
					from.SendMessage("That's a cheap tactic isn't it?");
				}	
			}
		}

		public virtual void AlterSpellDamageTo( Mobile to, ref int damage )
		{
		}

		public virtual void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			foreach (Item item in GetItemsInRange(0))
			{
				if (item is HouseSign) // being attacked by a player in a home, thats not fair is it?
				{
					this.MoveToWorld(m_pHome, this.Map);
					from.Hits /= 2;
					from.SendMessage("That's a cheap tactic isn't it?");
				}	
			}	
		}

		public virtual void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
		}

		#endregion

		public virtual void CheckReflect( Mobile caster, ref bool reflect )
		{
		}

		public virtual void OnCarve( Mobile from, Corpse corpse, Item with )
		{
			int feathers = Feathers;
			int wool = Wool;
			int meat = Meat;
			int hides = Hides;
			int scales = Scales;
			int furs = Furs;

			if ( (feathers == 0 && wool == 0 && meat == 0 && hides == 0 && scales == 0 && furs == 0) || Summoned || IsBonded || corpse.Animated )
			{
				if ( corpse.Animated ) 
					corpse.SendLocalizedMessageTo( from, 500464 );	// Use this on corpses to carve away meat and hide
				else
				{
					from.SendLocalizedMessage( 500485 ); // You see nothing useful to carve from the corpse.
					Console.WriteLine("basecr");
				}
			}
			else
			{
				if ( corpse.Map == Map.TerMur )
				{
					feathers *= 2;
					wool *= 2;
					hides *= 2;
					meat *= 2;
					scales *= 2;
					furs *= 2;
				}
				if ( corpse.Map == Map.Trammel )
				{
					feathers /= 2;
					wool /= 2;
					hides /= 2;
					meat /= 2;
					scales /= 2;
					furs /= 2;
				}
				if ( ( from.CheckSkill( SkillName.Forensics, 0, 100 ) ) && ( from.Skills[SkillName.Forensics].Base >= 5.0 ) ) // WIZARD ADDED TO MAKE FORENSICS USEFUL
				{
					if (feathers > 0){ feathers = feathers + (int)(from.Skills[SkillName.Forensics].Value/25) + (int)(from.Skills[SkillName.Anatomy].Value/25); }
					if (wool > 0){ wool = wool + (int)(from.Skills[SkillName.Forensics].Value/25) + (int)(from.Skills[SkillName.Anatomy].Value/25); }
					if (hides > 0){ hides = hides + (int)(from.Skills[SkillName.Forensics].Value/25) + (int)(from.Skills[SkillName.Anatomy].Value/25); }
					if (meat > 0){ meat = meat + (int)(from.Skills[SkillName.Forensics].Value/25) + (int)(from.Skills[SkillName.Anatomy].Value/25); }
					if (scales > 0){ scales = scales + (int)(from.Skills[SkillName.Forensics].Value/25) + (int)(from.Skills[SkillName.Anatomy].Value/25); }
					if (furs > 0){ furs = furs + (int)(from.Skills[SkillName.Forensics].Value/25) + (int)(from.Skills[SkillName.Anatomy].Value/25); }
				}

				if (Insensitive.Contains(this.Name, "greater"))
				{
					feathers *= 2;
					wool *= 2;
					hides *= 2;
					meat *= 2;
					scales *= 2;
					furs *= 2;
				}					

				new Blood( 0x122D ).MoveToWorld( corpse.Location, corpse.Map );

				if ( feathers != 0 )
				{
					corpse.AddCarvedItem( new Feather( feathers ), from );
					from.SendLocalizedMessage( 500479 ); // You pluck the bird. The feathers are now on the corpse.
				}

				if ( wool != 0 )
				{
					corpse.AddCarvedItem( new TaintedWool( wool ), from );
					from.SendLocalizedMessage( 500483 ); // You shear it, and the wool is now on the corpse.
				}

				if ( meat != 0 )
				{
					if ( MeatType == MeatType.Ribs )
						corpse.AddCarvedItem( new RawRibs( meat ), from );
					else if ( MeatType == MeatType.Bird )
						corpse.AddCarvedItem( new RawBird( meat ), from );
					else if ( MeatType == MeatType.LambLeg )
						corpse.AddCarvedItem( new RawLambLeg( meat ), from );
					else if ( MeatType == MeatType.Fish )
						corpse.AddCarvedItem( new RawFishSteak( meat ), from );

					from.SendLocalizedMessage( 500467 ); // You carve some meat, which remains on the corpse.
				}

				if ( furs != 0 )
				{
					if ( FurType == FurType.Regular )
						corpse.AddCarvedItem( new Furs( furs ), from );
					else if ( FurType == FurType.White )
						corpse.AddCarvedItem( new FursWhite( furs ), from );

					from.SendLocalizedMessage( 500475 );
				}

				if ( hides != 0 )
				{
					Item holding = from.Weapon as Item;
					if ( Core.AOS && ( holding is SkinningKnife /* TODO: || holding is ButcherWarCleaver || with is ButcherWarCleaver */ ) )
					{
						Item leather = null;

						switch ( HideType )
						{
							case HideType.Regular: leather = new Leather( hides ); break;
							case HideType.Spined: leather = new SpinedLeather( hides ); break;
							case HideType.Horned: leather = new HornedLeather( hides ); break;
							case HideType.Barbed: leather = new BarbedLeather( hides ); break;
							case HideType.Necrotic: leather = new NecroticLeather( hides ); break;
							case HideType.Volcanic: leather = new VolcanicLeather( hides ); break;
							case HideType.Frozen: leather = new FrozenLeather( hides ); break;
							case HideType.Goliath: leather = new GoliathLeather( hides ); break;
							case HideType.Draconic: leather = new DraconicLeather( hides ); break;
							case HideType.Hellish: leather = new HellishLeather( hides ); break;
							case HideType.Dinosaur: leather = new DinosaurLeather( hides ); break;
							case HideType.Alien: leather = new AlienLeather( hides ); break;
						}

						if ( leather != null )
						{
							if ( !from.PlaceInBackpack( leather ) )
							{
								corpse.DropItem( leather );
								from.SendLocalizedMessage( 500471 ); // You skin it, and the hides are now in the corpse.
							}
							else
								from.SendLocalizedMessage( 1073555 ); // You skin it and place the cut-up hides in your backpack.
						}
					}
					else
					{
						if ( HideType == HideType.Regular )
							corpse.DropItem( new Hides( hides ) );
						else if ( HideType == HideType.Spined )
							corpse.DropItem( new SpinedHides( hides ) );
						else if ( HideType == HideType.Horned )
							corpse.DropItem( new HornedHides( hides ) );
						else if ( HideType == HideType.Barbed )
							corpse.DropItem( new BarbedHides( hides ) );
						else if ( HideType == HideType.Necrotic )
							corpse.DropItem( new NecroticHides( hides ) );
						else if ( HideType == HideType.Volcanic )
							corpse.DropItem( new VolcanicHides( hides ) );
						else if ( HideType == HideType.Frozen )
							corpse.DropItem( new FrozenHides( hides ) );
						else if ( HideType == HideType.Goliath )
							corpse.DropItem( new GoliathHides( hides ) );
						else if ( HideType == HideType.Draconic )
							corpse.DropItem( new DraconicHides( hides ) );
						else if ( HideType == HideType.Hellish )
							corpse.DropItem( new HellishHides( hides ) );
						else if ( HideType == HideType.Dinosaur )
							corpse.DropItem( new DinosaurHides( hides ) );
						else if ( HideType == HideType.Alien )
							corpse.DropItem( new AlienHides( hides ) );

						from.SendLocalizedMessage( 500471 ); // You skin it, and the hides are now in the corpse.
					}
				}

				if ( scales != 0 )
				{
					ScaleType sc = this.ScaleType;

					switch ( sc )
					{
						case ScaleType.Red:     	corpse.AddCarvedItem( new RedScales( scales ), from ); break;
						case ScaleType.Yellow: 	 	corpse.AddCarvedItem( new YellowScales( scales ), from ); break;
						case ScaleType.Black:  	 	corpse.AddCarvedItem( new BlackScales( scales ), from ); break;
						case ScaleType.Green:   	corpse.AddCarvedItem( new GreenScales( scales ), from ); break;
						case ScaleType.White:   	corpse.AddCarvedItem( new WhiteScales( scales ), from ); break;
						case ScaleType.Blue:    	corpse.AddCarvedItem( new BlueScales( scales ), from ); break;
						case ScaleType.Dinosaur:    corpse.AddCarvedItem( new DinosaurScales( scales ), from ); break;
						case ScaleType.All:
						{
							corpse.AddCarvedItem( new RedScales( scales ), from );
							corpse.AddCarvedItem( new YellowScales( scales ), from );
							corpse.AddCarvedItem( new BlackScales( scales ), from );
							corpse.AddCarvedItem( new GreenScales( scales ), from );
							corpse.AddCarvedItem( new WhiteScales( scales ), from );
							corpse.AddCarvedItem( new BlueScales( scales ), from );
							corpse.AddCarvedItem( new DinosaurScales( scales ), from );
							break;
						}
					}

					from.SendMessage( "You cut away some scales, but they remain on the corpse." );
				}

				corpse.Carved = true;

				if ( corpse.IsCriminalAction( from ) )
					from.CriminalAction( true );
			}
		}

		public const int DefaultRangePerception = 16;
		public const int OldRangePerception = 10;

		public BaseCreature(AIType ai,
			FightMode mode,
			int iRangePerception,
			int iRangeFight,
			double dActiveSpeed,
			double dPassiveSpeed)
		{
			if ( iRangePerception == OldRangePerception )
				iRangePerception = DefaultRangePerception;

			m_Loyalty = MaxLoyalty; // Wonderfully Happy
			m_SoulSkillMods = new List<SkillMod>();
			m_SecondsSoulTouched = 0;
			m_CurrentAI = ai;
			m_DefaultAI = ai;

			m_iRangePerception = iRangePerception;
			m_iRangeFight = iRangeFight;

			m_FightMode = mode;

			m_iTeam = 0;

			SpeedInfo.GetSpeeds( this, ref dActiveSpeed, ref dPassiveSpeed );

			m_dActiveSpeed = dActiveSpeed;
			m_dPassiveSpeed = dPassiveSpeed;
			m_dCurrentSpeed = dPassiveSpeed;

			m_bDebugAI = false;
			m_goferal = false;
			m_special = 0;
			m_iControlSlots = 1;
			m_anger = false;

			m_arSpellAttack = new List<Type>();
			m_arSpellDefense = new List<Type>();
			
			m_bControlled = false;
			m_ControlMaster = null;
			m_ControlTarget = null;
			m_ControlOrder = OrderType.None;

			m_bTamable = false;

			m_Owners = new List<Mobile>();

			m_NextReacquireTime = DateTime.UtcNow + ReacquireDelay;

			ChangeAIType(AI);

			InhumanSpeech speechType = this.SpeechType;

			if ( speechType != null )
				speechType.OnConstruct( this );

			GenerateLoot( true );
			
			
			#region Jako Taming Added
            Female = Utility.RandomBool();
            #endregion
			
			if ( this.Region.IsPartOf( typeof( DungeonRegion ) ) || this.Region.IsPartOf( typeof( BardDungeonRegion ) ) )
			{
				m_IsSleeping = true;
			}

			m_midrace = 0;
		}

		public BaseCreature( Serial serial ) : base( serial )
		{
			m_arSpellAttack = new List<Type>();
			m_arSpellDefense = new List<Type>();

			m_bDebugAI = false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 30 ); // version, breeding check
			writer.Write( (int)m_CurrentAI );
			writer.Write( (int)m_DefaultAI );

			writer.Write( (int)m_iRangePerception );
			writer.Write( (int)m_iRangeFight );

			writer.Write( (int)m_iTeam );

			writer.Write( (double)m_dActiveSpeed );
			writer.Write( (double)m_dPassiveSpeed );
			writer.Write( (double)m_dCurrentSpeed );

			writer.Write( (int) m_pHome.X );
			writer.Write( (int) m_pHome.Y );
			writer.Write( (int) m_pHome.Z );

			// Version 1
			writer.Write( (int) m_iRangeHome );

			int i=0;

			writer.Write( (int) m_arSpellAttack.Count );
			for ( i=0; i< m_arSpellAttack.Count; i++ )
			{
				writer.Write( m_arSpellAttack[i].ToString() );
			}

			writer.Write( (int) m_arSpellDefense.Count );
			for ( i=0; i< m_arSpellDefense.Count; i++ )
			{
				writer.Write( m_arSpellDefense[i].ToString() );
			}

			// Version 2
			writer.Write( (int) m_FightMode );

			writer.Write( (bool) m_bControlled );
			writer.Write( (Mobile) m_ControlMaster );
			writer.Write( (Mobile) m_ControlTarget );
			writer.Write( (Point3D) m_ControlDest );
			writer.Write( (int) m_ControlOrder );
			writer.Write( (double) m_dMinTameSkill );
			// Removed in version 9
			//writer.Write( (double) m_dMaxTameSkill );
			writer.Write( (bool) m_bTamable );
			writer.Write( (bool) m_bSummoned );

			if ( m_bSummoned )
				writer.WriteDeltaTime( m_SummonEnd );

			writer.Write( (int) m_iControlSlots );

			// Version 3
			writer.Write( (int)m_Loyalty );

			// Version 4
			writer.Write( m_CurrentWayPoint );

			// Verison 5
			writer.Write( m_SummonMaster );

			// Version 6
			writer.Write( (int) m_HitsMax );
			writer.Write( (int) m_StamMax );
			writer.Write( (int) m_ManaMax );
			writer.Write( (int) m_DamageMin );
			writer.Write( (int) m_DamageMax );

			// Version 7
			writer.Write( (int) m_PhysicalResistance );
			writer.Write( (int) m_PhysicalDamage );

			writer.Write( (int) m_FireResistance );
			writer.Write( (int) m_FireDamage );

			writer.Write( (int) m_ColdResistance );
			writer.Write( (int) m_ColdDamage );

			writer.Write( (int) m_PoisonResistance );
			writer.Write( (int) m_PoisonDamage );

			writer.Write( (int) m_EnergyResistance );
			writer.Write( (int) m_EnergyDamage );

			// Version 8
			writer.Write( m_Owners, true );

			// Version 10
			writer.Write( (bool) m_IsDeadPet );
			writer.Write( (bool) m_IsBonded );
			writer.Write( (DateTime) m_BondingBegin );
			writer.Write( (DateTime) m_OwnerAbandonTime );

			// Version 11
			writer.Write( (bool) m_HasGeneratedLoot );

			// Version 12
			writer.Write( (bool) m_Paragon );

			// Version 13
			writer.Write( (bool) ( m_Friends != null && m_Friends.Count > 0 ) );

			if ( m_Friends != null && m_Friends.Count > 0 )
				writer.Write( m_Friends, true );

			// Version 14
			writer.Write( (bool)m_RemoveIfUntamed );
			writer.Write( (int)m_RemoveStep );

			// Version 17
			if ( IsStabled || ( Controlled && ControlMaster != null ) )
				writer.Write( TimeSpan.Zero );
			else
				writer.Write( DeleteTimeLeft );
			
			
			//Start Zombiex edit version 18
			writer.Write( (bool) m_CanInfect);
			//End Zombitx edit			

			// Version 19 Jako
            if (Tamable)
            {
                writer.Write(m_level);
                writer.Write(m_realLevel);
                writer.Write(m_experience);
                writer.Write(m_maxLevel);
                writer.Write(m_traits);
            }

            // vers 20
            writer.Write(m_IsHitchStabled);

			//ver 22
			writer.Write( m_special);

			//ver 23
			writer.Write ( m_anger );

			//24
			writer.Write ( m_FullSpeedPassiveAI );
			writer.Write ( m_FullSpeedActiveAI );
			writer.Write ( m_SpawnerSpawned );

			//25
			writer.Write (m_midrace);

			//26
			writer.Write (m_superspeed);
			
			//27
			writer.Write (m_injuries);

			//28
			writer.Write (m_oldstr);
			writer.Write (m_oldint);
			writer.Write (m_olddex);
			writer.Write (m_oldmin);
			writer.Write (m_oldmax);
			writer.Write (m_oldphys);
			writer.Write (m_oldfire);
			writer.Write (m_oldcold);
			writer.Write (m_oldpois);
			writer.Write (m_oldener);

			//29
			writer.Write (m_AdvancedAI);

			//30
			writer.Write(m_breeding);


        }

		private static double[] m_StandardActiveSpeeds = new double[]
			{
				0.175, 0.1, 0.15, 0.2, 0.25, 0.3, 0.4, 0.5, 0.6, 0.8
			};

		private static double[] m_StandardPassiveSpeeds = new double[]
			{
				0.350, 0.2, 0.4, 0.5, 0.6, 0.8, 1.0, 1.2, 1.6, 2.0
			};

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();


			if (version >= 21) {
				m_SecondsSoulTouched = 0;
				m_SoulSkillMods = new List<SkillMod>();
			}
			m_CurrentAI = (AIType)reader.ReadInt();
			m_DefaultAI = (AIType)reader.ReadInt();

			m_iRangePerception = reader.ReadInt();
			m_iRangeFight = reader.ReadInt();

			m_iTeam = reader.ReadInt();

			m_dActiveSpeed = reader.ReadDouble();
			m_dPassiveSpeed = reader.ReadDouble();
			m_dCurrentSpeed = reader.ReadDouble();

			if ( m_iRangePerception == OldRangePerception )
				m_iRangePerception = DefaultRangePerception;

			m_pHome.X = reader.ReadInt();
			m_pHome.Y = reader.ReadInt();
			m_pHome.Z = reader.ReadInt();

			if ( version >= 1 )
			{
				m_iRangeHome = reader.ReadInt();

				int i, iCount;

				iCount = reader.ReadInt();
				for ( i=0; i< iCount; i++ )
				{
					string str = reader.ReadString();
					Type type = Type.GetType( str );

					if ( type != null )
					{
						m_arSpellAttack.Add( type );
					}
				}

				iCount = reader.ReadInt();
				for ( i=0; i< iCount; i++ )
				{
					string str = reader.ReadString();
					Type type = Type.GetType( str );

					if ( type != null )
					{
						m_arSpellDefense.Add( type );
					}
				}
			}
			else
			{
				m_iRangeHome = 0;
			}

			if ( version >= 2 )
			{
				m_FightMode = ( FightMode )reader.ReadInt();

				m_bControlled = reader.ReadBool();
				m_ControlMaster = reader.ReadMobile();
				m_ControlTarget = reader.ReadMobile();
				m_ControlDest = reader.ReadPoint3D();
				m_ControlOrder = (OrderType) reader.ReadInt();

				m_dMinTameSkill = reader.ReadDouble();

				if ( version < 9 )
					reader.ReadDouble();

				m_bTamable = reader.ReadBool();
				m_bSummoned = reader.ReadBool();

				if ( m_bSummoned )
				{
					m_SummonEnd = reader.ReadDeltaTime();
					new UnsummonTimer( m_ControlMaster, this, m_SummonEnd - DateTime.UtcNow ).Start();
				}

				m_iControlSlots = reader.ReadInt();
			}
			else
			{
				m_FightMode = FightMode.Closest;

				m_bControlled = false;
				m_ControlMaster = null;
				m_ControlTarget = null;
				m_ControlOrder = OrderType.None;
			}

			if ( version >= 3 )
				m_Loyalty = reader.ReadInt();
			else
				m_Loyalty = MaxLoyalty; // Wonderfully Happy

			if ( version >= 4 )
				m_CurrentWayPoint = reader.ReadItem() as WayPoint;

			if ( version >= 5 )
				m_SummonMaster = reader.ReadMobile();

			if ( version >= 6 )
			{
				m_HitsMax = reader.ReadInt();
				m_StamMax = reader.ReadInt();
				m_ManaMax = reader.ReadInt();
				m_DamageMin = reader.ReadInt();
				m_DamageMax = reader.ReadInt();
			}

			if ( version >= 7 )
			{
				m_PhysicalResistance = reader.ReadInt();
				m_PhysicalDamage = reader.ReadInt();

				m_FireResistance = reader.ReadInt();
				m_FireDamage = reader.ReadInt();

				m_ColdResistance = reader.ReadInt();
				m_ColdDamage = reader.ReadInt();

				m_PoisonResistance = reader.ReadInt();
				m_PoisonDamage = reader.ReadInt();

				m_EnergyResistance = reader.ReadInt();
				m_EnergyDamage = reader.ReadInt();
			}

			if ( version >= 8 )
				m_Owners = reader.ReadStrongMobileList();
			else
				m_Owners = new List<Mobile>();

			if ( version >= 10 )
			{
				m_IsDeadPet = reader.ReadBool();
				m_IsBonded = reader.ReadBool();
				m_BondingBegin = reader.ReadDateTime();
				m_OwnerAbandonTime = reader.ReadDateTime();
			}

			if ( version >= 11 )
				m_HasGeneratedLoot = reader.ReadBool();
			else
				m_HasGeneratedLoot = true;

			if ( version >= 12 )
				m_Paragon = reader.ReadBool();
			else
				m_Paragon = false;

			if ( version >= 13 && reader.ReadBool() )
				m_Friends = reader.ReadStrongMobileList();
			else if ( version < 13 && m_ControlOrder >= OrderType.Unfriend )
				++m_ControlOrder;

			//if ( version < 16 && Loyalty != MaxLoyalty ) // final why was this increasing on server load?
			//	Loyalty *= 10;

			double activeSpeed = m_dActiveSpeed;
			double passiveSpeed = m_dPassiveSpeed;

			SpeedInfo.GetSpeeds( this, ref activeSpeed, ref passiveSpeed );

			bool isStandardActive = false;
			for ( int i = 0; !isStandardActive && i < m_StandardActiveSpeeds.Length; ++i )
				isStandardActive = ( m_dActiveSpeed == m_StandardActiveSpeeds[i] );

			bool isStandardPassive = false;
			for ( int i = 0; !isStandardPassive && i < m_StandardPassiveSpeeds.Length; ++i )
				isStandardPassive = ( m_dPassiveSpeed == m_StandardPassiveSpeeds[i] );

			if ( isStandardActive && m_dCurrentSpeed == m_dActiveSpeed )
				m_dCurrentSpeed = activeSpeed;
			else if ( isStandardPassive && m_dCurrentSpeed == m_dPassiveSpeed )
				m_dCurrentSpeed = passiveSpeed;

			if ( isStandardActive && !m_Paragon )
				m_dActiveSpeed = activeSpeed;

			if ( isStandardPassive && !m_Paragon )
				m_dPassiveSpeed = passiveSpeed;

			if ( version >= 14 )
			{
				m_RemoveIfUntamed = reader.ReadBool();
				m_RemoveStep = reader.ReadInt();
			}

			TimeSpan deleteTime = TimeSpan.Zero;

			if ( version >= 17 )
				deleteTime = reader.ReadTimeSpan();
			
			//Start Zombiex edit
			if (version >= 18) m_CanInfect = reader.ReadBool();
			//End Zombiex edit			

			//jako deserialize
			if (version >= 19 & Tamable)
             {
                 m_level = reader.ReadUInt();
                 m_realLevel = reader.ReadUInt();
                 m_experience = reader.ReadUInt();
                 m_maxLevel = reader.ReadUInt();
                 m_traits = reader.ReadUInt();
             }

            if (version >= 20)
            {
                m_IsHitchStabled = reader.ReadBool();
            }

			if (version >= 22)
				m_special = reader.ReadInt();

			if (version >= 23)
				m_anger = reader.ReadBool();

			if (version >= 24)
			{
				m_FullSpeedPassiveAI = reader.ReadBool();
				m_FullSpeedActiveAI = reader.ReadBool();
				m_SpawnerSpawned = reader.ReadBool();
			}
			if (version >= 25)
				m_midrace = reader.ReadInt();
			if (version >= 26)
			{
				m_superspeed = reader.ReadBool();
				if (m_superspeed)
				{
					AIFullSpeedActive = true;
					AIFullSpeedPassive = false;
				}
			}
			if (version >= 27)
				m_injuries = reader.ReadInt();

			if (version >= 28)
			{
				m_oldstr = reader.ReadInt();
				m_oldint = reader.ReadInt();
				m_olddex = reader.ReadInt();
				m_oldmin = reader.ReadInt();
				m_oldmax = reader.ReadInt();
				m_oldphys = reader.ReadInt();
				m_oldfire = reader.ReadInt();
				m_oldcold = reader.ReadInt();
				m_oldpois = reader.ReadInt();
				m_oldener = reader.ReadInt();
			}

			if (version >= 29)
				m_AdvancedAI = reader.ReadBool();

			if (version >= 30)
				m_breeding = reader.ReadBool();

            if ( deleteTime > TimeSpan.Zero || LastOwner != null && !Controlled && !IsStabled )
			{
				if ( deleteTime == TimeSpan.Zero )
					deleteTime = TimeSpan.FromDays( 3.0 );

				m_DeleteTimer = new DeleteTimer( this, deleteTime );
				m_DeleteTimer.Start();
			}

			if( version <= 14 && m_Paragon && Hue == 0x31 )
			{
				Hue = Paragon.Hue; //Paragon hue fixed, should now be 0x501.
			}


			CheckStatTimers();

			ChangeAIType(m_CurrentAI);

			AddFollowers();

			//if ( IsAnimatedDead )
			//	Spells.Necromancy.AnimateDeadSpell.Register( m_SummonMaster, this );

			if ( FightMode == FightMode.CharmMonster ){ FightMode = FightMode.Closest; }
			else if ( FightMode == FightMode.CharmAnimal ){ FightMode = FightMode.Aggressor; }
			
			if (RawInt <= 100) //magery ai now summons so this is now relevant for basecreatures
				FollowersMax = 2;
			else if (RawInt <= 400) 
				FollowersMax = 3;
			else if (RawInt <= 700) 
				FollowersMax = 5;
			else if (RawInt <= 1000) 
				FollowersMax = 7;
			else if (RawInt > 1000) 
				FollowersMax = 9;
		}

		public virtual bool IsHumanInTown()
		{
			return false;
		}

		public virtual bool CheckGold( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
				return OnGoldGiven( from, (Gold)dropped );

			return false;
		}

		public virtual bool OnGoldGiven( Mobile from, Gold dropped )
		{
			if ( CheckTeachingMatch( from ) )
			{

				if (this is CloneCharacterOnLogout.CharacterClone)
				{
					dropped.Amount /= 300;
					if (dropped.Amount <1 || dropped.Amount == 0)
					{
						from.SendMessage( "You need to pay at least 300 gp to learn from this character.");
						return false;
					}

				}
				if ( Teach( m_Teaching, from, dropped.Amount, true ) )
				{
					if (this is CloneCharacterOnLogout.CharacterClone)
					{
						Mobile playr = ((CloneCharacterOnLogout.CharacterClone)this).Original;
						if (playr != null )
							Banker.Deposit( playr, (dropped.Amount*150) );
					}

					dropped.Delete();
					return true;
				}
			}
			else if ( IsHumanInTown() )
			{
				Direction = GetDirectionTo( from );

				int oldSpeechHue = this.SpeechHue;

				this.SpeechHue = 0x23F;
				SayTo( from, "Thou art giving me gold?" );

				if ( dropped.Amount >= 400 )
					SayTo( from, "'Tis a noble gift." );
				else
					SayTo( from, "Money is always welcome." );

				this.SpeechHue = 0x3B2;
				SayTo( from, 501548 ); // I thank thee.

				this.SpeechHue = oldSpeechHue;

				dropped.Delete();
				return true;
			}

			return false;
		}

		public override bool ShouldCheckStatTimers{ get{ return false; } }

		#region Food
		private static Type[] m_Eggs = new Type[]
			{
				typeof( FriedEggs ), typeof( Eggs ), typeof( FairyEgg )
			};

		private static Type[] m_Fish = new Type[]
			{
				typeof( FishSteak ), typeof( RawFishSteak ), typeof( NewFish )
			};

		private static Type[] m_GrainsAndHay = new Type[]
			{
				typeof( BreadLoaf ), typeof( FrenchBread ), typeof( SheafOfHay )
			};

		private static Type[] m_Meat = new Type[]
			{
				/* Cooked */
				typeof( Bacon ), typeof( CookedBird ), typeof( Sausage ),
				typeof( Ham ), typeof( Ribs ), typeof( LambLeg ), typeof( FoodDriedBeef ), 
				typeof( ChickenLeg ), typeof( FoodBeefJerky ), 

				/* Uncooked */
				typeof( RawBird ), typeof( RawRibs ), typeof( RawLambLeg ),
				typeof( RawChickenLeg ), typeof( TastyHeart), 

				/* Body Parts */
				typeof( Head ), typeof( LeftArm ), typeof( LeftLeg ),
				typeof( Torso ), typeof( RightArm ), typeof( RightLeg ), typeof( BodyPart )
			};

		private static Type[] m_FruitsAndVegies = new Type[]
			{
				typeof( HoneydewMelon ), typeof( YellowGourd ), typeof( GreenGourd ),
				typeof( Banana ), typeof( Bananas ), typeof( Lemon ), typeof( Lime ),
				typeof( Dates ), typeof( Grapes ), typeof( Peach ), typeof( Pear ),
				typeof( Apple ), typeof( Watermelon ), typeof( Squash ), typeof ( SmallWatermelon ), 
				typeof( Cantaloupe ), typeof( Carrot ), typeof( Cabbage ), typeof ( FoodImpBerry ), 
				typeof( Onion ), typeof( Lettuce ), typeof( Pumpkin ), typeof( FoodToadStool ), 
				typeof( Tomato ), typeof( FoodPotato ), typeof( Corn ), typeof( Acorn )
			};

		private static Type[] m_Gold = new Type[]
			{
				typeof( Gold ), typeof( GoldBricks ), typeof( GoldIngot )
			};

		private static Type[] m_Fire = new Type[]
			{
				typeof( Brimstone ), typeof( SulfurousAsh )
			};

		private static Type[] m_Gems = new Type[]
			{
				typeof( Ruby ), typeof( Amber ), typeof( Amethyst ), typeof( Citrine ),
				typeof( Emerald ), typeof( Diamond ), typeof( Sapphire ), typeof( StarSapphire ),
				typeof( Tourmaline ), typeof( DDRelicGem )
			};

		private static Type[] m_Nox = new Type[]
			{
				typeof( Nightshade ), typeof( NoxCrystal ), typeof( SwampBerries )
			};

		private static Type[] m_Sea = new Type[]
			{
				typeof( SeaSalt ), typeof( EnchantedSeaweed ), typeof( SpecialSeaweed )
			};

		private static Type[] m_Moon = new Type[]
			{
				typeof( MoonCrystal )
			};

		public virtual bool CheckFoodPreference( Item f )
		{
			if ( CheckFoodPreference( f, FoodType.Eggs, m_Eggs ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.Fish, m_Fish ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.GrainsAndHay, m_GrainsAndHay ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.Meat, m_Meat ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.FruitsAndVegies, m_FruitsAndVegies ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.Gold, m_Gold ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.Fire, m_Fire ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.Gems, m_Gems ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.Nox, m_Nox ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.Sea, m_Sea ) )
				return true;

			if ( CheckFoodPreference( f, FoodType.Moon, m_Moon ) )
				return true;

			return false;
		}

		public virtual bool CheckFoodPreference( Item fed, FoodType type, Type[] types )
		{
			if ( (FavoriteFood & type) == 0 )
				return false;

			Type fedType = fed.GetType();
			bool contains = false;

			for ( int i = 0; !contains && i < types.Length; ++i )
				contains = ( fedType == types[i] );

			return contains;
		}

		public static bool IsPet( Mobile m )
		{
			if ( m is PlayerMobile )
				return false;

			if ( m is BaseCreature )
			{
				if ( m is FrankenFighter || m is Robot || m is GolemFighter || m is HenchmanMonster || m is HenchmanWizard || m is HenchmanArcher || m is HenchmanFighter )
					return false;

				BaseCreature bc = (BaseCreature)m;

				if ( bc.Summoned || bc.Controlled )
					return true;
			}

			return false;
		}

		public virtual bool CheckFeed( Mobile from, Item dropped )
		{
			if ( 	!IsDeadPet && 
					!( this is FrankenPorter ) && 
					!( this is FrankenFighter ) && 
					!( this is GolemPorter ) && 
					!( this is AerialServant ) && 
					!( this is Robot ) && 
					!( this is Robot ) && 
					!( this is PackBeast ) && 
					!( this is HenchmanFamiliar ) && 
					!( this is HenchmanFighter ) && 
					!( this is HenchmanMonster ) && 
					!( this is HenchmanWizard ) && 
					!( this is HenchmanArcher ) && 
					Controlled && 
					( ControlMaster == from || IsPetFriend( from ) )
				)
			{
				Item f = dropped;

				if ( CheckFoodPreference( f ) )
				{
					int amount = f.Amount;

					if ( amount >= 1 )
					{
						int stamGain;

						if ( f is Gold )
							stamGain = amount - 50;
						else
							stamGain = (amount * 15) - 50;

						if ( stamGain > 0 )
							Stam += stamGain;

						//if ( Core.SE )
						//{
						//	if ( m_Loyalty < MaxLoyalty )
						//	{
						//		m_Loyalty = MaxLoyalty;
						//	}
						//}
						//else
						//{
						if (amount > 1)
						{
							for ( int i = 0; i < amount; ++i )
							{
								if ( m_Loyalty < MaxLoyalty  )
								{
									m_Loyalty += Utility.RandomMinMax(3, 10);
									dropped.Amount -= 1;
								}
							}
						}
						else
						{
							m_Loyalty += Utility.RandomMinMax(3, 10);
						}

						
							this.InvalidateProperties();
						//}

						/* if ( happier )*/	// looks like in OSI pets say they are happier even if they are at maximum loyalty
							SayTo( from, 502060 ); // Your pet looks happier.

						if ( Body.IsAnimal )
							Animate( 3, 5, 1, true, false, 0 );
						else if ( Body.IsMonster )
							Animate( 17, 5, 1, true, false, 0 );

						if ( IsBondable && !IsBonded )
						{
							Mobile master = m_ControlMaster;

							if ( master != null && master == from )	//So friends can't start the bonding process
							{
								if ( m_dMinTameSkill <= 29.1 || master.Skills[SkillName.AnimalTaming].Base >= m_dMinTameSkill || OverrideBondingReqs() || (Core.ML && master.Skills[SkillName.AnimalTaming].Value >= m_dMinTameSkill) )
								{
									if ( BondingBegin == DateTime.MinValue )
									{
										BondingBegin = DateTime.UtcNow;
									}
									else if ( (BondingBegin + BondingDelay) <= DateTime.UtcNow )
									{
										IsBonded = true;
										BondingBegin = DateTime.MinValue;
										from.SendLocalizedMessage( 1049666 ); // Your pet has bonded with you!
									}
								}
								else if( Core.ML )
								{
									from.SendLocalizedMessage( 1075268 ); // Your pet cannot form a bond with you until your animal taming ability has risen.
								}
							}
						}

						if (dropped != null && !dropped.Deleted && dropped.Amount == 1 && dropped.ParentEntity != null) // to prevent stacks of food on ground from being deleted - feeding from ground
							dropped.Delete();

						return true;
					}
				}
			}

			return false;
		}

		#endregion

		public virtual bool OverrideBondingReqs()
		{
			return false;
		}

		private bool m_anger;
		public virtual bool CanAngerOnTame{ get{ 
			if (m_anger)
				return true;

			return false; } }


		#region OnAction[...]

		public virtual void OnActionWander()
		{
		}

		public virtual void OnActionCombat()
		{
		}

		public virtual void OnActionGuard()
		{
		}

		public virtual void OnActionFlee()
		{
		}

		public virtual void OnActionInteract()
		{
		}

		public virtual void OnActionBackoff()
		{
		}

		#endregion

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( CheckFeed( from, dropped ) )
				return true;
			else if ( CheckGold( from, dropped ) )
				return true;

			return base.OnDragDrop( from, dropped );
		}

		protected virtual BaseAI ForcedAI { get { return null; } }

		public  void ChangeAIType( AIType NewAI )
		{
			if ( m_AI != null )
				m_AI.m_Timer.Stop();

			if( ForcedAI != null )
			{
				m_AI = ForcedAI;
				return;
			}

			m_AI = null;

			switch ( NewAI )
			{
				case AIType.AI_Melee:
					m_AI = new MeleeAI(this);
					break;
				case AIType.AI_Animal:
					m_AI = new AnimalAI(this);
					break;
				case AIType.AI_Berserk:
					m_AI = new BerserkAI(this);
					break;
				case AIType.AI_Archer:
					m_AI = new ArcherAI(this);
					break;
				case AIType.AI_Healer:
					m_AI = new HealerAI(this);
					break;
				case AIType.AI_Vendor:
					m_AI = new VendorAI(this);
					break;
				case AIType.AI_Mage:
					m_AI = new MageAI(this);
					break;
				//FINAL
				case AIType.AI_Archer2:
					m_AI = new Archer2AI(this);
					break;
				case AIType.AI_Paladin:
					m_AI = new PaladinAI(this);
					break;
				case AIType.AI_PlayActor:
					m_AI = new PlayActorAI(this);
					break;
				//end
				case AIType.AI_Predator:
					//m_AI = new PredatorAI(this);
					m_AI = new MeleeAI(this);
					break;
				case AIType.AI_Thief:
					m_AI = new ThiefAI(this);
					break;
				case AIType.AI_Fearful:
					m_AI = new FearfulAI(this);
					break;
			}
		}

		public void ChangeAIToDefault()
		{
			ChangeAIType(m_DefaultAI);
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AIType AI
		{
			get
			{
				return m_CurrentAI;
			}
			set
			{
				m_CurrentAI = value;

				if (m_CurrentAI == AIType.AI_Use_Default)
				{
					m_CurrentAI = m_DefaultAI;
				}

				ChangeAIType(m_CurrentAI);
			}
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool GoFeral
		{
			get
			{
				return m_goferal;
			}
			set
			{
				m_goferal = value;
			}
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Special
		{
			get
			{
				return m_special;
			}
			set
			{
				m_special = value;
			}
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool Debug
		{
			get
			{
				return m_bDebugAI;
			}
			set
			{
				m_bDebugAI = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Team
		{
			get
			{
				return m_iTeam;
			}
			set
			{
				m_iTeam = value;

				OnTeamChange();
			}
		}

		public virtual void OnTeamChange()
		{
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile FocusMob
		{
			get
			{
				return m_FocusMob;
			}
			set
			{
				m_FocusMob = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public FightMode FightMode
		{
			get
			{
				return m_FightMode;
			}
			set
			{
				m_FightMode = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RangePerception
		{
			get
			{
				return m_iRangePerception;
			}
			set
			{
				m_iRangePerception = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RangeFight
		{
			get
			{
				return m_iRangeFight;
			}
			set
			{
				m_iRangeFight = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RangeHome
		{
			get
			{
				return m_iRangeHome;
			}
			set
			{
				m_iRangeHome = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double ActiveSpeed
		{
			get
			{
				return m_dActiveSpeed;
			}
			set
			{
				m_dActiveSpeed = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double PassiveSpeed
		{
			get
			{
				return m_dPassiveSpeed;
			}
			set
			{
				m_dPassiveSpeed = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double CurrentSpeed
		{
			get
			{
				if ( m_TargetLocation != null )
					return 0.3;

				return m_dCurrentSpeed;
			}
			set
			{
				if ( m_dCurrentSpeed != value )
				{
					m_dCurrentSpeed = value;

					if (m_AI != null)
						m_AI.OnCurrentSpeedChanged();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Home
		{
			get
			{
				return m_pHome;
			}
			set
			{
				m_pHome = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Controlled
		{
			get
			{
				return m_bControlled;
			}
			set
			{
				if ( m_bControlled == value )
					return;

				m_bControlled = value;
				Delta( MobileDelta.Noto );

				InvalidateProperties();
			}
		}

		public override void RevealingAction()
		{
			if ( WhisperHue == 999 && Hidden && CantWalk && !CanSwim && !Server.Mobiles.BasePirate.IsSailor( this ) )
			{
				this.PlaySound( 0x026 );
				Effects.SendLocationEffect( this.Location, this.Map, 0x23B2, 16 );
				this.CantWalk = false;
				this.CanSwim = true;
				this.Hidden = false;
			}
			else if ( WhisperHue == 666 && Hidden && CantWalk )
			{
				Server.Items.DemonGate.MakeDemonGate( this );
				this.CantWalk = false;
				this.Hidden = false;
			}
			if ( CanOnlyMoveOnSea( this ) ){ this.CantWalk = true; }

			Spells.Sixth.InvisibilitySpell.RemoveTimer( this );

			base.RevealingAction();
		}

		public void RemoveFollowers()
		{
			if ( m_ControlMaster != null )
			{
				m_ControlMaster.Followers -= m_iControlSlots;
				if( m_ControlMaster is PlayerMobile )
				{
					((PlayerMobile)m_ControlMaster).AllFollowers.Remove( this );
					if( ((PlayerMobile)m_ControlMaster).AutoStabled.Contains( this ) )
						((PlayerMobile)m_ControlMaster).AutoStabled.Remove( this );
				}
			}
			else if ( m_SummonMaster != null )
			{
				m_SummonMaster.Followers -= m_iControlSlots;
				if( m_SummonMaster is PlayerMobile )
				{
					((PlayerMobile)m_SummonMaster).AllFollowers.Remove( this );
				}
			}

			if ( m_ControlMaster != null && m_ControlMaster.Followers < 0 )
				m_ControlMaster.Followers = 0;

			if ( m_SummonMaster != null && m_SummonMaster.Followers < 0 )
				m_SummonMaster.Followers = 0;
		}

		public void AddFollowers()
		{
			if ( m_ControlMaster != null )
			{
				m_ControlMaster.Followers += m_iControlSlots;
				if( m_ControlMaster is PlayerMobile )
				{
					((PlayerMobile)m_ControlMaster).AllFollowers.Add( this );
				}
			}
			else if ( m_SummonMaster != null )
			{
				m_SummonMaster.Followers += m_iControlSlots;
				if( m_SummonMaster is PlayerMobile )
				{
					((PlayerMobile)m_SummonMaster).AllFollowers.Add( this );
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile ControlMaster
		{
			get
			{
				return m_ControlMaster;
			}
			set
			{
				if ( m_ControlMaster == value || this == value )
					return;

				RemoveFollowers();
				m_ControlMaster = value;
				AddFollowers();
				if ( m_ControlMaster != null )
					StopDeleteTimer();

				Delta( MobileDelta.Noto );
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile SummonMaster
		{
			get
			{
				return m_SummonMaster;
			}
			set
			{
				if ( m_SummonMaster == value || this == value )
					return;

				RemoveFollowers();
				m_SummonMaster = value;
				AddFollowers();

				Delta( MobileDelta.Noto );
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile ControlTarget
		{
			get
			{
				return m_ControlTarget;
			}
			set
			{
				m_ControlTarget = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D ControlDest
		{
			get
			{
				return m_ControlDest;
			}
			set
			{
				m_ControlDest = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public OrderType ControlOrder
		{
			get
			{
				return m_ControlOrder;
			}
			set
			{
				m_ControlOrder = value;

				if ( m_AI != null )
					m_AI.OnCurrentOrderChanged();

				InvalidateProperties();

				if ( m_ControlMaster != null )
					m_ControlMaster.InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool BardProvoked
		{
			get
			{
				return m_bBardProvoked;
			}
			set
			{
				m_bBardProvoked = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool BardPacified
		{
			get
			{
				return m_bBardPacified;
			}
			set
			{
				m_bBardPacified = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile BardMaster
		{
			get
			{
				return m_bBardMaster;
			}
			set
			{
				m_bBardMaster = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile BardTarget
		{
			get
			{
				return m_bBardTarget;
			}
			set
			{
				m_bBardTarget = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime BardEndTime
		{
			get
			{
				return m_timeBardEnd;
			}
			set
			{
				m_timeBardEnd = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double MinTameSkill
		{
			get
			{
				return m_dMinTameSkill;
			}
			set
			{
				m_dMinTameSkill = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Tamable
		{
			get
			{
				if (m_Paragon && m_bControlled)
					return m_bTamable;
				
				return m_bTamable && !m_Paragon;
			}
			set
			{
				m_bTamable = value;
			}
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool Summoned
		{
			get
			{
				return m_bSummoned;
			}
			set
			{
				if ( m_bSummoned == value )
					return;

				m_NextReacquireTime = DateTime.UtcNow;

				m_bSummoned = value;
				Delta( MobileDelta.Noto );

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int ControlSlots
		{
			get
			{
				return m_iControlSlots;
			}
			set
			{
				m_iControlSlots = value;
			}
		}

		public virtual bool NoHouseRestrictions{ get{ return false; } }
		public virtual bool IsHouseSummonable{ get{ return false; } }

		#region Corpse Resources
		public virtual int Feathers{ get{ return 0; } }
		public virtual int Wool{ get{ return 0; } }

		public virtual MeatType MeatType{ get{ return MeatType.Ribs; } }
		public virtual int Meat{ get{ return 0; } }

		public virtual FurType FurType{ get{ return FurType.Regular; } }
		public virtual int Furs{ get{ return 0; } }

		public virtual int Hides{ get{ return 0; } }
		public virtual HideType HideType{ get{ return HideType.Regular; } }

		public virtual int Scales{ get{ return 0; } }
		public virtual ScaleType ScaleType{ get{ return ScaleType.Red; } }
		#endregion

		public virtual bool AutoDispel{ get{ return false; } }
		public virtual double AutoDispelChance{ get { return ((Core.SE) ? .10 : 1.0); } }

		public virtual bool IsScaryToPets{ get{ return false; } }
		public virtual bool IsScaredOfScaryThings{ get{ return true; } }

		public virtual bool CanRummageCorpses{ get{ return false; } }

		public virtual void OnGotMeleeAttack( Mobile attacker )
		{	
			if ( AutoDispel && attacker is BaseCreature && ((BaseCreature)attacker).IsDispellable )
			{
				BaseCreature mob = attacker as BaseCreature;
				PlayerMobile owner = null;
				
				if (mob.Summoned && mob.ControlMaster != null && mob.ControlMaster is PlayerMobile)
					owner = (PlayerMobile)mob.ControlMaster;
					
				if (owner != null && owner.Sorcerer() && (AutoDispelChance/4) > Utility.RandomDouble() )
					Dispel( attacker );
				else if (AutoDispelChance > Utility.RandomDouble() )
					Dispel( attacker );
			}
						//ppl kiting mobs from homes
				BaseHouse house = BaseHouse.FindHouseAt( attacker.Location, attacker.Map, 16 );
				BaseHouse house2 = BaseHouse.FindHouseAt( Location, Map, 16 );
				if (Alive && !Deleted && Map != null && house != null && house2 == null && attacker is PlayerMobile)
				{
					if (this is BaseChampion)
						attacker.Hits /= 2;
					else
						attacker.Hits /= 4;

					attacker.SendMessage("Tisk tisk!");
					attacker.Map = this.Map;
					attacker.Location = this.Location;
					//attacker.MoveToWorld(Location, Map);  causes crash
				}

		}

		public virtual void Dispel( Mobile m )
		{
			Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
			Effects.PlaySound( m, m.Map, 0x201 );

			m.Delete();
		}

		public virtual bool DeleteOnRelease{ get{ return m_bSummoned; } }

		public virtual void OnGaveMeleeAttack( Mobile defender )
		{
			Poison p = HitPoison;
			if (m_level == 9)
				p = Poison.Greater;

			if ( m_Paragon )
				p = PoisonImpl.IncreaseLevel( p );

			if ( p != null && ( HitPoisonChance >= Utility.RandomDouble() || (m_special == 9 && 0.15 > Utility.RandomDouble() ) )  ) {
				defender.ApplyPoison( this, p );
				if ( this.Controlled )
					this.CheckSkill(SkillName.Poisoning, 0, this.Skills[SkillName.Poisoning].Cap);
			}

			if( AutoDispel && defender is BaseCreature && ((BaseCreature)defender).IsDispellable && AutoDispelChance > Utility.RandomDouble() )
				Dispel( defender );

			Server.Misc.IntelligentAction.SaySomethingWhenAttacking( this, defender );

			if ( AI == AIType.AI_Archer )
			{
				int sound = 0;

				if ( FindItemOnLayer( Layer.OneHanded ) is BaseMeleeWeapon ) { sound = ( ( BaseMeleeWeapon )( FindItemOnLayer( Layer.OneHanded ) ) ).DefHitSound; }
				else if ( FindItemOnLayer( Layer.TwoHanded ) is BaseMeleeWeapon ) { sound = ( ( BaseMeleeWeapon )( FindItemOnLayer( Layer.TwoHanded ) ) ).DefHitSound; }

				if ( sound > 0 ){ PlaySound( sound ); }
			}
			else if ( AI == AIType.AI_Mage )
			{
				int sound = 0;

				if ( FindItemOnLayer( Layer.OneHanded ) is BaseWizardStaff ) { sound = ( ( BaseMeleeWeapon )( FindItemOnLayer( Layer.OneHanded ) ) ).DefHitSound; }
				else if ( FindItemOnLayer( Layer.TwoHanded ) is BaseWizardStaff ) { sound = ( ( BaseMeleeWeapon )( FindItemOnLayer( Layer.TwoHanded ) ) ).DefHitSound; }

				if ( sound > 0 ){ PlaySound( sound ); }
			}



		}

		public override void OnAfterDelete()
		{
			if ( m_AI != null )
			{
				if ( m_AI.m_Timer != null )
					m_AI.m_Timer.Stop();

				m_AI = null;
			}

			if ( m_DeleteTimer != null )
			{
				m_DeleteTimer.Stop();
				m_DeleteTimer = null;
			}

			FocusMob = null;

			//if ( IsAnimatedDead )
			//	Spells.Necromancy.AnimateDeadSpell.Unregister( m_SummonMaster, this );

			base.OnAfterDelete();
		}

		public void DebugSay( string text )
		{
			if ( m_bDebugAI )
				this.PublicOverheadMessage( MessageType.Regular, 41, false, text );
		}

		public void DebugSay( string format, params object[] args )
		{
			if ( m_bDebugAI )
				this.PublicOverheadMessage( MessageType.Regular, 41, false, String.Format( format, args ) );
		}

		/*
		 * This function can be overriden.. so a "Strongest" mobile, can have a different definition depending
		 * on who check for value
		 * -Could add a FightMode.Prefered
		 *
		 */

		public virtual double GetFightModeRanking( Mobile m, FightMode acqType, bool bPlayerOnly )
		{
			if ( ( bPlayerOnly && (m.Player || m is BaseBlue || m is BaseRed) ) ||  !bPlayerOnly )
			{
				switch( acqType )
				{
					case FightMode.Strongest :
						return (m.Skills[SkillName.Tactics].Value + m.Str); //returns strongest mobile

					case FightMode.Weakest :
						return -m.Hits; // returns weakest mobile

					default :
						return -GetDistanceToSqrt( m ); // returns closest mobile
				}
			}
			else
			{
				return double.MinValue;
			}
		}

		// Turn, - for left, + for right
		// Basic for now, needs work
		public virtual void Turn(int iTurnSteps)
		{
			int v = (int)Direction;

			Direction = (Direction)((((v & 0x7) + iTurnSteps) & 0x7) | (v & 0x80));
		}

		public virtual void TurnInternal(int iTurnSteps)
		{
			int v = (int)Direction;

			SetDirection( (Direction)((((v & 0x7) + iTurnSteps) & 0x7) | (v & 0x80)) );
		}

		public bool IsHurt()
		{
			return ( Hits != HitsMax );
		}

		public double GetHomeDistance()
		{
			return GetDistanceToSqrt( m_pHome );
		}

		public virtual int GetTeamSize(int iRange)
		{
			int iCount = 0;

			foreach ( Mobile m in this.GetMobilesInRange( iRange ) )
			{
				if (m is BaseCreature)
				{
					if ( ((BaseCreature)m).Team == Team )
					{
						if ( !m.Deleted )
						{
							if ( m != this )
							{
								if ( CanSee( m ) )
								{
									iCount++;
								}
							}
						}
					}
				}
			}

			return iCount;
		}

		private class TameEntry : ContextMenuEntry
		{
			private BaseCreature m_Mobile;

			public TameEntry( Mobile from, BaseCreature creature ) : base( 6130, 6 )
			{
				m_Mobile = creature;

				Enabled = Enabled && ( from.Female ? creature.AllowFemaleTamer : creature.AllowMaleTamer );
			}

			public override void OnClick()
			{
				if ( !Owner.From.CheckAlive() )
					return;

				Owner.From.TargetLocked = true;
				SkillHandlers.AnimalTaming.DisableMessage = true;

				if ( Owner.From.UseSkill( SkillName.AnimalTaming ) )
					Owner.From.Target.Invoke( Owner.From, m_Mobile );

				SkillHandlers.AnimalTaming.DisableMessage = false;
				Owner.From.TargetLocked = false;
			}
		}

		#region Teaching
		public virtual bool CanTeach{ get{ return false; } }

		public virtual bool CheckTeach( SkillName skill, Mobile from )
		{
			if ( !CanTeach )
				return false;

			if( skill == SkillName.Stealth && from.Skills[SkillName.Hiding].Base < ((Core.SE) ? 50.0 : 80.0) )
				return false;

			return true;
		}

		public enum TeachResult
		{
			Success,
			Failure,
			KnowsMoreThanMe,
			KnowsWhatIKnow,
			SkillNotRaisable,
			NotEnoughFreePoints
		}

		public virtual TeachResult CheckTeachSkills( SkillName skill, Mobile m, int maxPointsToLearn, ref int pointsToLearn, bool doTeach )
		{

			if ( !CheckTeach( skill, m ) || !m.CheckAlive() )
				return TeachResult.Failure;

			Skill ourSkill = Skills[skill];
			Skill theirSkill = m.Skills[skill];

			if ( ourSkill == null || theirSkill == null )
				return TeachResult.Failure;

			int baseToSet = 0;

			if (this is CloneCharacterOnLogout.CharacterClone)
			{
				if (ourSkill.BaseFixedPoint >= 1000)
					baseToSet = 800;
				else
					return TeachResult.Failure;
			}
			else
			{
				baseToSet = ourSkill.BaseFixedPoint / 3;
				if ( baseToSet > 420 )
					baseToSet = 420;
				else if ( baseToSet < 200 )
					return TeachResult.Failure;
			}
			
			if ( baseToSet > theirSkill.CapFixedPoint )
				baseToSet = theirSkill.CapFixedPoint;

			pointsToLearn = baseToSet - theirSkill.BaseFixedPoint;

			if ( maxPointsToLearn > 0 && pointsToLearn > maxPointsToLearn )
			{
				pointsToLearn = maxPointsToLearn;
				baseToSet = theirSkill.BaseFixedPoint + pointsToLearn;
			}

			if ( pointsToLearn < 0 )
				return TeachResult.KnowsMoreThanMe;

			if ( pointsToLearn == 0 )
				return TeachResult.KnowsWhatIKnow;

			if ( theirSkill.Lock != SkillLock.Up )
				return TeachResult.SkillNotRaisable;

			int freePoints = m.Skills.Cap - m.Skills.Total;
			int freeablePoints = 0;

			if ( freePoints < 0 )
				freePoints = 0;

			for ( int i = 0; (freePoints + freeablePoints) < pointsToLearn && i < m.Skills.Length; ++i )
			{
				Skill sk = m.Skills[i];

				if ( sk == theirSkill || sk.Lock != SkillLock.Down )
					continue;

				freeablePoints += sk.BaseFixedPoint;
			}

			if ( (freePoints + freeablePoints) == 0 )
				return TeachResult.NotEnoughFreePoints;

			if ( (freePoints + freeablePoints) < pointsToLearn )
			{
				pointsToLearn = freePoints + freeablePoints;
				baseToSet = theirSkill.BaseFixedPoint + pointsToLearn;
			}

			if ( doTeach )
			{
				int need = pointsToLearn - freePoints;

				for ( int i = 0; need > 0 && i < m.Skills.Length; ++i )
				{
					Skill sk = m.Skills[i];

					if ( sk == theirSkill || sk.Lock != SkillLock.Down )
						continue;

					if ( sk.BaseFixedPoint < need )
					{
						need -= sk.BaseFixedPoint;
						sk.BaseFixedPoint = 0;
					}
					else
					{
						sk.BaseFixedPoint -= need;
						need = 0;
					}
				}

				/* Sanity check */
				if ( baseToSet > theirSkill.CapFixedPoint || (m.Skills.Total - theirSkill.BaseFixedPoint + baseToSet) > m.Skills.Cap )
					return TeachResult.NotEnoughFreePoints;

				theirSkill.BaseFixedPoint = baseToSet;
			}

			return TeachResult.Success;
		}

		public virtual bool CheckTeachingMatch( Mobile m )
		{
			if ( m_Teaching == (SkillName)(-1) )
				return false;

			if ( m is PlayerMobile )
				return ( ((PlayerMobile)m).Learning == m_Teaching );

			return true;
		}

		private SkillName m_Teaching = (SkillName)(-1);

		public virtual bool Teach( SkillName skill, Mobile m, int maxPointsToLearn, bool doTeach )
		{



			int pointsToLearn = 0;

			TeachResult res = CheckTeachSkills( skill, m, maxPointsToLearn, ref pointsToLearn, doTeach );

			switch ( res )
			{
				case TeachResult.KnowsMoreThanMe:
				{
					Say( 501508 ); // I cannot teach thee, for thou knowest more than I!
					break;
				}
				case TeachResult.KnowsWhatIKnow:
				{
					Say( 501509 ); // I cannot teach thee, for thou knowest all I can teach!
					break;
				}
				case TeachResult.NotEnoughFreePoints:
				case TeachResult.SkillNotRaisable:
				{
					m.SendMessage( "Make sure this skill is marked to raise. If you are near the skill cap you may need to lose some points in another skill first.");
					break;
				}
				case TeachResult.Success:
				{
					if ( doTeach )
					{
						Say( 501539 ); // Let me show thee something of how this is done.
						m.SendLocalizedMessage( 501540 ); // Your skill level increases.

						m_Teaching = (SkillName)(-1);

						if ( m is PlayerMobile )
							((PlayerMobile)m).Learning = (SkillName)(-1);
					}
					else
					{
						if (this is CloneCharacterOnLogout.CharacterClone)
							Say( 1019077, AffixType.Append, String.Format( " {0}", (pointsToLearn * 300) ), "" ); // I will teach thee all I know, if paid the amount in full.  The price is:
						else
							Say( 1019077, AffixType.Append, String.Format( " {0}", pointsToLearn ), "" );
						Say( 1043108 ); // For less I shall teach thee less.

						m_Teaching = skill;

						if ( m is PlayerMobile )
							((PlayerMobile)m).Learning = skill;
					}

					Server.Gumps.SkillListingGump.RefreshSkillList( m );

					return true;
				}
			}

			return false;
		}

		#endregion

		public override void AggressiveAction( Mobile aggressor, bool criminal )
		{
			base.AggressiveAction( aggressor, criminal );

			if ( this.ControlMaster != null )
				if ( NotorietyHandlers.CheckAggressor( this.ControlMaster.Aggressors, aggressor ) )
					aggressor.Aggressors.Add( AggressorInfo.Create( this, aggressor, true ) );

			OrderType ct = m_ControlOrder;

			if ( m_AI != null )
			{
				if( !Core.ML || ( ct != OrderType.Follow && ct != OrderType.Stop ) )
				{
					m_AI.OnAggressiveAction( aggressor );
				}
				else
				{
					DebugSay( "I'm being attacked but my master told me not to fight." );
					Warmode = false;
					return;
				}
			}

			StopFlee();

			ForceReacquire();

			SlayerEntry undead_creatures = SlayerGroup.GetEntryByName( SlayerName.Silver );
			if ( undead_creatures.Slays(this) && aggressor is PlayerMobile )
			{
				Item item = aggressor.FindItemOnLayer( Layer.Helm );
				if ( item is DeathlyMask )
				{
					item.Delete();
					aggressor.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, "The mask of death has vanished.");
					aggressor.PlaySound( 0x1F0 );
				}
			}

			if ( aggressor.ChangingCombatant && (m_bControlled || m_bSummoned) && (ct == OrderType.Come || ( !Core.ML && ct == OrderType.Stay ) || ct == OrderType.Stop || ct == OrderType.None || ct == OrderType.Follow) )
			{
				ControlTarget = aggressor;
				ControlOrder = OrderType.Attack;
			}
			else if ( Combatant == null && !m_bBardPacified )
			{
				Warmode = true;
				Combatant = aggressor;
			}
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is BaseCreature && !((BaseCreature)m).Controlled )
				return ( !Alive || !m.Alive || IsDeadBondedPet || m.IsDeadBondedPet ) || ( Hidden && m.AccessLevel > AccessLevel.Player );

			return base.OnMoveOver( m );
		}

		public virtual void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

		public virtual bool CanDrop { get { return IsBonded; } }

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
            if (m_IsHitchStabled)
                return;

            base.GetContextMenuEntries( from, list );

			if ( m_AI != null && Commandable )
				m_AI.GetContextMenuEntries( from, list );

			if ( m_bTamable && !m_bControlled && from.Alive )
				list.Add( new TameEntry( from, this ) );

			AddCustomContextEntries( from, list );

			if ( CanTeach && from.Alive && !from.Criminal )
			{
				Skills ourSkills = this.Skills;
				Skills theirSkills = from.Skills;

				for ( int i = 0; i < ourSkills.Length && i < theirSkills.Length; ++i )
				{
					Skill skill = ourSkills[i];
					Skill theirSkill = theirSkills[i];

					if ( skill != null && theirSkill != null && CheckTeach( skill.SkillName, from ) )
					{
						if ( ( this is CloneCharacterOnLogout.CharacterClone && skill.Base >= 100.0 ) || ( !(this is CloneCharacterOnLogout.CharacterClone) && skill.Base >= 60.0 ))
						{
							double toTeach = 0;
							if (this is CloneCharacterOnLogout.CharacterClone)
							{
								toTeach = 80.0;
							}
							else
							{
								toTeach = skill.Base / 3.0;

								if ( toTeach > 42.0 )
									toTeach = 42.0;

							}

							if (toTeach > 0)
								list.Add( new TeachEntry( (SkillName)i, this, from, ( toTeach > theirSkill.Base ) ) );
						}

					}
				}
			}
		}

		public void CheckOldStats()
		{
			if (m_oldstr == 0 || this.RawStr > m_oldstr)
				m_oldstr = this.RawStr;
			
			if (m_oldint == 0 || this.RawInt > m_oldint)
				m_oldint = this.RawInt;

			if (m_olddex == 0 || this.RawDex > m_olddex)
				m_olddex = this.RawDex;

			if (m_oldmin == 0 || this.DamageMin > m_oldmin)
				m_oldmin = this.DamageMin;

			if (m_oldmax == 0 || this.DamageMax > m_oldmax)
				m_oldmax = this.DamageMax;

			if (m_oldphys == 0 || this.PhysicalResistanceSeed > m_oldphys)
				m_oldphys = this.PhysicalResistanceSeed;

			if (m_oldfire == 0 || this.FireResistSeed > m_oldfire)
				m_oldfire = this.FireResistSeed;

			if (m_oldcold == 0 || this.ColdResistSeed > m_oldcold)
				m_oldcold = this.ColdResistSeed;

			if (m_oldpois == 0 || this.PoisonResistSeed > m_oldpois)
				m_oldpois = this.PoisonResistSeed;

			if (m_oldener == 0 || this.EnergyResistSeed > m_oldener)
				m_oldener = this.EnergyResistSeed;

		}

		public void GainInjury( int severity) // max severity of 3 please
		{
			if (!this.Tamable || !this.Controlled || this.Deleted || this.Summoned || this.IsParagon)
				return;
			
			if (severity < 1 )
				severity = 1;

			CheckOldStats(); //keep old stats or higher stats for breeding purposes

			this.HitsMaxSeed = (int)( this.HitsMaxSeed - ( (int)( (double)this.HitsMaxSeed * 0.04 ) * severity ) ); // hitpoints go down for each injury
			
			int which = Utility.RandomMinMax(1, 4); // which stat will be reduced, 1/4 chance of all 3 reducing
			
			if (which == 1 || which ==4)
				this.RawStr = (int)( this.RawStr - ( (int)( (double)this.RawStr * 0.04 ) * severity ) );
			else if (which == 2 || which ==4)
				this.RawInt = (int)( this.RawInt - ( (int)( (double)this.RawInt * 0.04 ) * severity ) );
			else if (which == 3 || which ==4)
				this.RawDex = (int)( this.RawDex - ( (int)( (double)this.RawDex * 0.04 ) * severity ) );

			int DamageBuff = (int)( (double)this.DamageMax * 0.07 ) * severity; // damage will reduce for all injuries
			
			if (DamageBuff < 1)
				DamageBuff = 1;

			this.DamageMin -= DamageBuff;
			this.DamageMax -= DamageBuff;

			int whichresist = Utility.RandomMinMax(1, 6); // which resist will be reduced, 1/6 chance of all 5 being reduced 
			
			if (this.PhysicalResistanceSeed > 5 && (whichresist == 1 || whichresist == 6))
				this.SetResistance( ResistanceType.Physical, (this.PhysicalResistanceSeed - (int)( ( (double)this.PhysicalResistanceSeed * 0.05) * severity) ) );
			if (this.FireResistSeed > 5 && (whichresist == 2 || whichresist == 6))
				this.SetResistance( ResistanceType.Fire, (this.FireResistSeed - (int)( ( (double)this.FireResistSeed * 0.05) * severity) ) );
			if (this.ColdResistSeed > 5 && (whichresist == 3 || whichresist == 6))
				this.SetResistance( ResistanceType.Cold, (this.ColdResistSeed - (int)( ( (double)this.ColdResistSeed * 0.05) * severity) ) );
			if (this.PoisonResistSeed > 5 && (whichresist == 4 || whichresist == 6))
				this.SetResistance( ResistanceType.Poison, (this.PoisonResistSeed - (int)( ( (double)this.PoisonResistSeed * 0.05) * severity) ) );
			if (this.EnergyResistSeed > 5 && (whichresist == 5 || whichresist == 6))
				this.SetResistance( ResistanceType.Energy, (this.EnergyResistSeed - (int)( ( (double)this.EnergyResistSeed * 0.05) * severity) ) );
				
			PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "*Receives an injury!*" ) ); 
			
			m_injuries ++;
		
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			InhumanSpeech speechType = this.SpeechType;

			if ( speechType != null && (speechType.Flags & IHSFlags.OnSpeech) != 0 && from.InRange( this, 3 ) )
				return true;

			return ( m_AI != null && m_AI.HandlesOnSpeech( from ) && from.InRange( this, m_iRangePerception ) );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			InhumanSpeech speechType = this.SpeechType;

			Mobile m = e.Mobile;
			if (m != null && AdventuresFunctions.IsPuritain((object)this) && midrace > 0 && !m.Hidden && CanSee(m) && InRange( m, 7 ) )
			{
				if (IntelligentAction.RaceCheck(this, m)) // means this is an enemy
				{
					FocusMob = m;
					Combatant = m;	

					if (this is MidlandVendor)				
					{
						Say("Guards!! Guards!!");
						foreach ( Mobile mm in this.GetMobilesInRange( 10 ) )
						{
							if ( mm is MidlandGuard )
							{
								((BaseCreature)mm).FocusMob = m;
								mm.Combatant = m;
								mm.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Stop! You there!" ) ); 
							}

						}				
					}
				}
			}

			if ( speechType != null && speechType.OnSpeech( this, e.Mobile, e.Speech ) )
				e.Handled = true;
			else if ( !e.Handled && m_AI != null && e.Mobile.InRange( this, m_iRangePerception ) )
				m_AI.OnSpeech( e );
		}

		public override bool IsHarmfulCriminal( Mobile target )
		{
			if ( (Controlled && target == m_ControlMaster) || (Summoned && target == m_SummonMaster) )
				return false;

			if ( target is BaseCreature && ((BaseCreature)target).InitialInnocent && !((BaseCreature)target).Controlled )
				return false;

			if ( target is PlayerMobile && ((PlayerMobile)target).PermaFlags.Count > 0 )
				return false;

			return base.IsHarmfulCriminal( target );
		}

		public override void CriminalAction( bool message )
		{
			base.CriminalAction( message );

			if ( Controlled || Summoned )
			{
				if ( m_ControlMaster != null && m_ControlMaster.Player )
					m_ControlMaster.CriminalAction( false );
				else if ( m_SummonMaster != null && m_SummonMaster.Player )
					m_SummonMaster.CriminalAction( false );
			}
		}

		public override void DoHarmful( Mobile target, bool indirect )
		{
			base.DoHarmful( target, indirect );

			if ( target == this || target == m_ControlMaster || target == m_SummonMaster || (!Controlled && !Summoned) )
				return;

			List<AggressorInfo> list = this.Aggressors;

			for ( int i = 0; i < list.Count; ++i )
			{
				AggressorInfo ai = list[i];

				if ( ai.Attacker == target )
					return;
			}

			list = this.Aggressed;

			for ( int i = 0; i < list.Count; ++i )
			{
				AggressorInfo ai = list[i];

				if ( ai.Defender == target )
				{
					if ( m_ControlMaster != null && m_ControlMaster.Player && m_ControlMaster.CanBeHarmful( target, false ) )
						m_ControlMaster.DoHarmful( target, true );
					else if ( m_SummonMaster != null && m_SummonMaster.Player && m_SummonMaster.CanBeHarmful( target, false ) )
						m_SummonMaster.DoHarmful( target, true );

					return;
				}
			}
		}

		private static Mobile m_NoDupeGuards;

		public void ReleaseGuardDupeLock()
		{
			m_NoDupeGuards = null;
		}

		public void ReleaseGuardLock()
		{
			EndAction( typeof( GuardedRegion ) );
		}

		private DateTime m_IdleReleaseTime;

		public virtual bool CheckIdle()
		{
			if ( Combatant != null )
				return false; // in combat.. not idling

			if ( m_IdleReleaseTime > DateTime.MinValue )
			{
				// idling...

				if ( DateTime.UtcNow >= m_IdleReleaseTime )
				{
					m_IdleReleaseTime = DateTime.MinValue;
					return false; // idle is over
				}

				return true; // still idling
			}

			if ( 95 > Utility.Random( 100 ) )
				return false; // not idling, but don't want to enter idle state

			m_IdleReleaseTime = DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 25 ) );

			if ( Body.IsHuman && !Mounted )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: Animate( 5, 5, 1, true,  true, 1 ); break;
					case 1: Animate( 6, 5, 1, true, false, 1 ); break;
				}
			}
			else if ( Body.IsAnimal )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: Animate(  3, 3, 1, true, false, 1 ); break;
					case 1: Animate(  9, 5, 1, true, false, 1 ); break;
					case 2: Animate( 10, 5, 1, true, false, 1 ); break;
				}
			}
			else if ( Body.IsMonster )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: Animate( 17, 5, 1, true, false, 1 ); break;
					case 1: Animate( 18, 5, 1, true, false, 1 ); break;
				}
			}

			PlaySound( GetIdleSound() );
			return true; // entered idle state
		}

		protected override void OnLocationChange( Point3D oldLocation )
		{
			Map map = this.Map;

			if ( PlayerRangeSensitive && m_AI != null && map != null && map.GetSector( this.Location ).Active )
				m_AI.Activate();

			base.OnLocationChange( oldLocation );
		}

/*		public void InitCheckSoulTouchedAuraTimer() {
			OneTimeSecEvent.SecTimerTick += CheckSoulTouchedAura;
		}
		public void CheckSoulTouchedAura(object sender, EventArgs e) {
			if (SecondsSoulTouched == 0) {
				RemoveStatMod("SoulTouchedInt");
				RemoveStatMod("SoulTouchedDex");
				RemoveStatMod("SoulTouchedStr");
				if (SoulSkillMods != null) {
					foreach(SkillMod skillMod in SoulSkillMods) {
						RemoveSkillMod(skillMod);
					}
				}
				Hits = HitsMax;
				OneTimeSecEvent.SecTimerTick -= CheckSoulTouchedAura;
			} else {
				bool loseTick = true;
				foreach ( Mobile mobile in this.GetMobilesInRange( 17 ) )
				{
					if (mobile is PlayerMobile && ( (((PlayerMobile)mobile).SoulBound && mobile.Party != null) || ((PlayerMobile)mobile).InGauntlet) ) {
						loseTick = false;
					}
				}
				// if no soulbound party around anymore
				if (loseTick) {
					--SecondsSoulTouched;
				}
			}
		}*/

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			base.OnMovement( m, oldLocation );

			if (AdventuresFunctions.IsPuritain((object)this) && midrace > 0 && !m.Hidden && CanSee(m) && InRange( m, 5 ) && !InRange( oldLocation, 5 ))
			{
				if (IntelligentAction.RaceCheck(this, m)) // means this is an enemy
				{
					FocusMob = m;
					Combatant = m;	

					if (this is MidlandVendor)				
					{
						Say("Guards!! Guards!!");
						foreach ( Mobile mm in this.GetMobilesInRange( 10 ) )
						{
							if ( mm is MidlandGuard )
							{
								((BaseCreature)mm).FocusMob = m;
								mm.Combatant = m;
								mm.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Stop! You there!" ) ); 
							}

						}				
					}
				}
			}

			if ( ReacquireOnMovement || m_Paragon )
				ForceReacquire();

			InhumanSpeech speechType = this.SpeechType;

			if ( speechType != null )
				speechType.OnMovement( this, m, oldLocation );

			/* Begin notice sound */
			if ( !m_IsSleeping && (!m.Hidden || m.AccessLevel == AccessLevel.Player) && m.Player && m_FightMode != FightMode.Aggressor && m_FightMode != FightMode.None && Combatant == null && !Controlled && !Summoned )
			{
				// If this creature defends itself but doesn't actively attack (animal) or
				// doesn't fight at all (vendor) then no notice sounds are played..
				// So, players are only notified of aggressive monsters

				// Monsters that are currently fighting are ignored

				// Controlled or summoned creatures are ignored

				if ( InRange( m.Location, 18 ) && !InRange( oldLocation, 18 ) && IsEnemy( m ) && CanSee( m ) && InLOS( m ) )
				{
					if ( Body.IsMonster )
						Animate( 11, 5, 1, true, false, 1 );

					PlaySound( GetAngerSound() );
				}
			}
			/* End notice sound */
			if ( m_NoDupeGuards == m )
				return;

			if ( !Body.IsHuman || AlwaysMurderer || AlwaysAttackable || !m.InRange( Location, 12 ) || !m.Alive )
				return;

		}

		public void AddSpellAttack( Type type )
		{
			m_arSpellAttack.Add ( type );
		}

		public void AddSpellDefense( Type type )
		{
			m_arSpellDefense.Add ( type );
		}

		public Spell GetAttackSpellRandom()
		{
			if ( m_arSpellAttack.Count > 0 )
			{
				Type type = m_arSpellAttack[Utility.Random(m_arSpellAttack.Count)];

				object[] args = {this, null};
				return Activator.CreateInstance( type, args ) as Spell;
			}
			else
			{
				return null;
			}
		}

		public Spell GetDefenseSpellRandom()
		{
			if ( m_arSpellDefense.Count > 0 )
			{
				Type type = m_arSpellDefense[Utility.Random(m_arSpellDefense.Count)];

				object[] args = {this, null};
				return Activator.CreateInstance( type, args ) as Spell;
			}
			else
			{
				return null;
			}
		}

		public Spell GetSpellSpecific( Type type )
		{
			int i;

			for( i=0; i< m_arSpellAttack.Count; i++ )
			{
				if( m_arSpellAttack[i] == type )
				{
					object[] args = { this, null };
					return Activator.CreateInstance( type, args ) as Spell;
				}
			}

			for ( i=0; i< m_arSpellDefense.Count; i++ )
			{
				if ( m_arSpellDefense[i] == type )
				{
					object[] args = {this, null};
					return Activator.CreateInstance( type, args ) as Spell;
				}
			}

			return null;
		}

		#region Set[...]

		public void SetDamage( int val )
		{
			m_DamageMin = val;
			m_DamageMax = val;
		}

		public void SetDamage( int min, int max )
		{
			m_DamageMin = min;
			m_DamageMax = max;
		}

		public void SetHits( int val )
		{
			if ( val < 1000 && !Core.AOS )
				val = (val * 100) / 60;

			m_HitsMax = val;
			Hits = HitsMax;
		}

		public void SetHits( int min, int max )
		{
			if ( min < 1000 && !Core.AOS )
			{
				min = (min * 100) / 60;
				max = (max * 100) / 60;
			}

			m_HitsMax = Utility.RandomMinMax( min, max );
			Hits = HitsMax;
		}

		public void SetStam( int val )
		{
			m_StamMax = val;
			Stam = StamMax;
		}

		public void SetStam( int min, int max )
		{
			m_StamMax = Utility.RandomMinMax( min, max );
			Stam = StamMax;
		}

		public void SetMana( int val )
		{
			m_ManaMax = val;
			Mana = ManaMax;
		}

		public void SetMana( int min, int max )
		{
			m_ManaMax = Utility.RandomMinMax( min, max );
			Mana = ManaMax;
		}

		public void SetStr( int val )
		{
			RawStr = val;
			Hits = HitsMax;
		}

		public void SetStr( int min, int max )
		{
			RawStr = Utility.RandomMinMax( min, max );
			Hits = HitsMax;
		}

		public void SetDex( int val )
		{
			RawDex = val;
			Stam = StamMax;
		}

		public void SetDex( int min, int max )
		{
			RawDex = Utility.RandomMinMax( min, max );
			Stam = StamMax;
		}

		public void SetInt( int val )
		{
			RawInt = val;
			Mana = ManaMax;
		}

		public void SetInt( int min, int max )
		{
			RawInt = Utility.RandomMinMax( min, max );
			Mana = ManaMax;
		}

		public void SetDamageType( ResistanceType type, int min, int max )
		{
			SetDamageType( type, Utility.RandomMinMax( min, max ) );
		}

		public void SetDamageType( ResistanceType type, int val )
		{
			switch ( type )
			{
				case ResistanceType.Physical: m_PhysicalDamage = val; break;
				case ResistanceType.Fire: m_FireDamage = val; break;
				case ResistanceType.Cold: m_ColdDamage = val; break;
				case ResistanceType.Poison: m_PoisonDamage = val; break;
				case ResistanceType.Energy: m_EnergyDamage = val; break;
			}
		}

		public void SetResistance( ResistanceType type, int min, int max )
		{
			SetResistance( type, Utility.RandomMinMax( min, max ) );
		}

		public void SetResistance( ResistanceType type, int val )
		{
			switch ( type )
			{
				case ResistanceType.Physical: m_PhysicalResistance = val; break;
				case ResistanceType.Fire: m_FireResistance = val; break;
				case ResistanceType.Cold: m_ColdResistance = val; break;
				case ResistanceType.Poison: m_PoisonResistance = val; break;
				case ResistanceType.Energy: m_EnergyResistance = val; break;
			}

			UpdateResistances();
		}

		public void SetSkill( SkillName name, double val )
		{
			Skills[name].BaseFixedPoint = (int)(val * 10);

			if ( Skills[name].Base > Skills[name].Cap )
			{
				if ( Core.SE )
					this.SkillsCap += ( Skills[name].BaseFixedPoint - Skills[name].CapFixedPoint );

				Skills[name].Cap = Skills[name].Base;
			}
		}

		public void SetSkill( SkillName name, double min, double max )
		{
			int minFixed = (int)(min * 10);
			int maxFixed = (int)(max * 10);

			Skills[name].BaseFixedPoint = Utility.RandomMinMax( minFixed, maxFixed );

			if ( Skills[name].Base > Skills[name].Cap )
			{
				if ( Core.SE )
					this.SkillsCap += ( Skills[name].BaseFixedPoint - Skills[name].CapFixedPoint );

				Skills[name].Cap = Skills[name].Base;
			}
		}

		public void SetFameLevel( int level )
		{
			switch ( level )
			{
				case 1: Fame = Utility.RandomMinMax(     0,  1249 ); break;
				case 2: Fame = Utility.RandomMinMax(  1250,  2499 ); break;
				case 3: Fame = Utility.RandomMinMax(  2500,  4999 ); break;
				case 4: Fame = Utility.RandomMinMax(  5000,  9999 ); break;
				case 5: Fame = Utility.RandomMinMax( 10000, 10000 ); break;
			}
		}

		public void SetKarmaLevel( int level )
		{
			switch ( level )
			{
				case 0: Karma = -Utility.RandomMinMax(     0,   624 ); break;
				case 1: Karma = -Utility.RandomMinMax(   625,  1249 ); break;
				case 2: Karma = -Utility.RandomMinMax(  1250,  2499 ); break;
				case 3: Karma = -Utility.RandomMinMax(  2500,  4999 ); break;
				case 4: Karma = -Utility.RandomMinMax(  5000,  9999 ); break;
				case 5: Karma = -Utility.RandomMinMax( 10000, 10000 ); break;
			}
		}

		#endregion

		public static void Cap( ref int val, int min, int max )
		{
			if ( val < min )
				val = min;
			else if ( val > max )
				val = max;
		}

		#region Pack & Loot

		public void PackPotion()
		{
			PackItem( Loot.RandomPotion() );
		}

		public void PackArcanceScroll( double chance )
		{
			if ( !Core.ML || chance <= Utility.RandomDouble() )
				return;

			PackItem( Loot.Construct( Loot.ArcaneScrollTypes ) );
		}

		public void PackNecroScroll( int index )
		{
			if ( !Core.AOS || 0.05 <= Utility.RandomDouble() )
				return;

			PackItem( Loot.Construct( Loot.NecromancyScrollTypes, index ) );
		}

		public void PackScroll( int minCircle, int maxCircle )
		{
			PackScroll( Utility.RandomMinMax( minCircle, maxCircle ) );
		}

		public void PackScroll( int circle )
		{
			int min = (circle - 1) * 8;

			PackItem( Loot.RandomScroll( min, min + 7, SpellbookType.Regular ) );
		}

		public void PackMagicItems( int minLevel, int maxLevel )
		{
			PackMagicItems( minLevel, maxLevel, 0.30, 0.15 );
		}

		public void PackMagicItems( int minLevel, int maxLevel, double armorChance, double weaponChance )
		{
			if ( !PackArmor( minLevel, maxLevel, armorChance ) )
				PackWeapon( minLevel, maxLevel, weaponChance );
		}

		public virtual void DropBackpack()
		{
			if ( Backpack != null )
			{
				if( Backpack.Items.Count > 0 )
				{
					Backpack b = new CreatureBackpack( Name ); // was CreatureBackpack

					List<Item> list = new List<Item>( Backpack.Items );
					foreach ( Item item in list )
					{
						b.DropItem( item );
					}

					b.Movable = true;

					BaseHouse house = BaseHouse.FindHouseAt( this );
					if ( house  != null )
						b.MoveToWorld( house.BanLocation, house.Map );
					else
						b.MoveToWorld( Location, Map );
				}
			}
		}

		protected bool m_Spawning;
		protected int m_KillersLuck;

		public virtual void GenerateLoot( bool spawning )
		{
			m_Spawning = spawning;

			if ( !spawning )
				m_KillersLuck = LootPack.GetLuckChanceForKiller( this );

			GenerateLoot();

			if ( m_Paragon )
			{
				if ( Fame < 1250 )
					AddLoot( LootPack.Meager );
				else if ( Fame < 2500 )
					AddLoot( LootPack.Average );
				else if ( Fame < 5000 )
					AddLoot( LootPack.Rich );
				else if ( Fame < 10000 )
					AddLoot( LootPack.FilthyRich );
				else
					AddLoot( LootPack.UltraRich );
			}

			m_Spawning = false;
			m_KillersLuck = 0;
		}

		public virtual void GenerateLoot()
		{
		}

		public virtual void AddLoot( LootPack pack, int amount )
		{
			
//Final: standardize lootpack				

			if (pack == LootPack.Poor || pack == LootPack.Meager || pack == LootPack.Average || pack == LootPack.Rich || pack == LootPack.FilthyRich || pack == LootPack.UltraRich || pack == LootPack.SuperBoss || pack == LootPack.AosPoor || pack == LootPack.AosMeager || pack == LootPack.AosAverage || pack == LootPack.AosRich || pack == LootPack.AosFilthyRich || pack == LootPack.AosUltraRich || pack == LootPack.AosSuperBoss  )
			{
				DynamicFameKarma();
				
				if (this.Fame <= 750)
				{
					pack = LootPack.Poor;
					if (Utility.RandomDouble() < 0.05 )
						amount = 3;
					else if (Utility.RandomDouble() < 0.25 )
						amount = 2;
					else
						amount = 1;
				}
					
				else if (this.Fame <= 1500)
				{
					pack = LootPack.Meager;
					if (Utility.RandomDouble() < 0.05 )
						amount = 3;
					else if (Utility.RandomDouble() < 0.25 )
						amount = 2;
					else
						amount = 1;
				}
				else if (this.Fame <= 3000)
				{
					pack = LootPack.Average;
					if (Utility.RandomDouble() < 0.05 )
						amount = 3;
					else if (Utility.RandomDouble() < 0.25 )
						amount = 2;
					else
						amount = 1;
				}
				else if (this.Fame <= 6000)
				{
					pack = LootPack.Rich;
					if (Utility.RandomDouble() < 0.05 )
						amount = 3;
					else if (Utility.RandomDouble() < 0.25 )
						amount = 2;
					else
						amount = 1;
				}
				else if (this.Fame <= 12000)
				{
					pack = LootPack.FilthyRich;
					if (Utility.RandomDouble() < 0.05 )
						amount = 3;
					else if (Utility.RandomDouble() < 0.25 )
						amount = 2;
					else
						amount = 1;
				}
				else if (this.Fame <= 24000)
				{
					pack = LootPack.UltraRich;
					if (Utility.RandomDouble() < 0.05 )
						amount = 3;
					else if (Utility.RandomDouble() < 0.25 )
						amount = 2;
					else
						amount = 1;
				}		
				else if (this.Fame > 24000)
				{
					pack = LootPack.SuperBoss;
					if (Utility.RandomDouble() < 0.05 )
						amount = 3;
					else if (Utility.RandomDouble() < 0.25 )
						amount = 2;
					else
						amount = 1;
				}		
			}
			
			for ( int i = 0; i < amount; ++i )
				AddLoot( pack );
		}

		public virtual void AddLoot( LootPack pack )
		{
			if ( Summoned )
				return;
			Container backpack = Backpack;

			if ( backpack == null )
			{
				backpack = new Backpack();

				backpack.Movable = false;

				AddItem( backpack );
			}

			pack.Generate( this, backpack, m_Spawning, m_KillersLuck );
		}

		public bool PackArmor( int minLevel, int maxLevel )
		{
			return PackArmor( minLevel, maxLevel, 1.0 );
		}

		public bool PackArmor( int minLevel, int maxLevel, double chance )
		{
			if ( chance <= Utility.RandomDouble() )
				return false;

			Cap( ref minLevel, 0, 5 );
			Cap( ref maxLevel, 0, 5 );

			Item item = Loot.RandomArmorOrShieldOrJewelry();

			if ( item == null )
				return false;

			int attributeCount, min, max;
			GetRandomAOSStats( minLevel, maxLevel, out attributeCount, out min, out max );

			if ( item is BaseArmor )
			{
				Server.Misc.MorphingTime.ChangeMaterialType( item, this );
				BaseRunicTool.ApplyAttributesTo( (BaseArmor)item, attributeCount, min, max );
				item.Name = LootPackEntry.MagicItemName( item, this, Region.Find( this.Location, this.Map ) );
			}
			else if ( item is BaseJewel )
			{
				BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, attributeCount, min, max );
				item.Name = LootPackEntry.MagicItemName( item, this, Region.Find( this.Location, this.Map ) );
			}

			PackItem( item );

			return true;
		}

		public static void GetRandomAOSStats( int minLevel, int maxLevel, out int attributeCount, out int min, out int max )
		{
			int v = RandomMinMaxScaled( minLevel, maxLevel );

			if ( v >= 5 )
			{
				attributeCount = Utility.RandomMinMax( 2, 6 );
				min = 20; max = 70;
			}
			else if ( v == 4 )
			{
				attributeCount = Utility.RandomMinMax( 2, 4 );
				min = 20; max = 50;
			}
			else if ( v == 3 )
			{
				attributeCount = Utility.RandomMinMax( 2, 3 );
				min = 20; max = 40;
			}
			else if ( v == 2 )
			{
				attributeCount = Utility.RandomMinMax( 1, 2 );
				min = 10; max = 30;
			}
			else
			{
				attributeCount = 1;
				min = 10; max = 20;
			}
		}

		public static int RandomMinMaxScaled( int min, int max )
		{
			if ( min == max )
				return min;

			if ( min > max )
			{
				int hold = min;
				min = max;
				max = hold;
			}

			/* Example:
			 *    min: 1
			 *    max: 5
			 *  count: 5
			 *
			 * total = (5*5) + (4*4) + (3*3) + (2*2) + (1*1) = 25 + 16 + 9 + 4 + 1 = 55
			 *
			 * chance for min+0 : 25/55 : 45.45%
			 * chance for min+1 : 16/55 : 29.09%
			 * chance for min+2 :  9/55 : 16.36%
			 * chance for min+3 :  4/55 :  7.27%
			 * chance for min+4 :  1/55 :  1.81%
			 */

			int count = max - min + 1;
			int total = 0, toAdd = count;

			for ( int i = 0; i < count; ++i, --toAdd )
				total += toAdd*toAdd;

			int rand = Utility.Random( total );
			toAdd = count;

			int val = min;

			for ( int i = 0; i < count; ++i, --toAdd, ++val )
			{
				rand -= toAdd*toAdd;

				if ( rand < 0 )
					break;
			}

			return val;
		}

		public bool PackSlayer()
		{
			return PackSlayer( 0.05 );
		}

		public virtual void CallOthers( Mobile target )
		{
			foreach ( Mobile mob in this.GetMobilesInRange( 10 ) )
			{

				Region region = Region.Find( this.Location, this.Map );

				if ( mob is BaseCreature && InLOS( mob ) && mob.Combatant == null && this.GetType() == mob.GetType() && !((BaseCreature)mob).Summoned && !((BaseCreature)mob).Controlled && !((BaseCreature)mob).IsHitchStabled && !((BaseCreature)mob).IsStabled && !(region.IsPartOf( typeof( ChampionSpawnRegion ) ) ) && !(region is ChampionSpawnRegion))
				{
					mob.Combatant = target;
					mob.Warmode = true;
				}
			}	
		}

		public bool PackSlayer( double chance )
		{
			if ( chance <= Utility.RandomDouble() )
				return false;

			if ( Utility.RandomBool() )
			{
				BaseInstrument instrument = Loot.RandomInstrument();

				if ( instrument != null )
				{
					instrument.Slayer = SlayerGroup.GetLootSlayerType( GetType() );
					PackItem( instrument );
				}
			}
			else if ( !Core.AOS )
			{
				BaseWeapon weapon = Loot.RandomWeapon();

				if ( weapon != null )
				{
					weapon.Slayer = SlayerGroup.GetLootSlayerType( GetType() );
					PackItem( weapon );
				}
			}

			return true;
		}

		public bool PackWeapon( int minLevel, int maxLevel )
		{
			return PackWeapon( minLevel, maxLevel, 1.0 );
		}

		public bool PackWeapon( int minLevel, int maxLevel, double chance )
		{
			if ( chance <= Utility.RandomDouble() )
				return false;

			Cap( ref minLevel, 0, 5 );
			Cap( ref maxLevel, 0, 5 );

			if ( Core.AOS )
			{
				Item item = Loot.RandomWeaponOrJewelry();

				if ( item == null )
					return false;

				int attributeCount, min, max;
				GetRandomAOSStats( minLevel, maxLevel, out attributeCount, out min, out max );

				if ( item is BaseWeapon )
				{
					Server.Misc.MorphingTime.ChangeMaterialType( item, this );
					BaseRunicTool.ApplyAttributesTo( (BaseWeapon)item, attributeCount, min, max );
					item.Name = LootPackEntry.MagicItemName( item, this, Region.Find( this.Location, this.Map ) );
				}
				else if ( item is BaseJewel )
				{
					BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, attributeCount, min, max );
					item.Name = LootPackEntry.MagicItemName( item, this, Region.Find( this.Location, this.Map ) );
				}

				PackItem( item );
			}
			else
			{
				BaseWeapon weapon = Loot.RandomWeapon();

				if ( weapon == null )
					return false;

				if ( 0.05 > Utility.RandomDouble() )
					weapon.Slayer = SlayerName.Silver;

				weapon.DamageLevel = (WeaponDamageLevel)RandomMinMaxScaled( minLevel, maxLevel );
				weapon.AccuracyLevel = (WeaponAccuracyLevel)RandomMinMaxScaled( minLevel, maxLevel );
				weapon.DurabilityLevel = (WeaponDurabilityLevel)RandomMinMaxScaled( minLevel, maxLevel );

				PackItem( weapon );
			}

			return true;
		}

		public void PackGold( int amount )
		{
			if ( amount > 0 )
				PackItem( new Gold( amount ) );
		}

		public void PackGold( int min, int max )
		{
			PackGold( Utility.RandomMinMax( min, max ) );
		}

		public void PackStatue( int min, int max )
		{
			PackStatue( Utility.RandomMinMax( min, max ) );
		}

		public void PackStatue( int amount )
		{
			for ( int i = 0; i < amount; ++i )
				PackStatue();
		}

		public void PackStatue()
		{
			PackItem( Loot.RandomStatue() );
		}

		public void PackGem()
		{
			PackGem( 1 );
		}

		public void PackGem( int min, int max )
		{
			PackGem( Utility.RandomMinMax( min, max ) );
		}

		public void PackGem( int amount )
		{
			if ( amount <= 0 )
				return;

			Item gem = Loot.RandomGem();

			gem.Amount = amount;

			PackItem( gem );
		}

		public void PackNecroReg( int min, int max )
		{
			int amount = Utility.RandomMinMax( min, max );

			if ( Utility.RandomMinMax( 1 , 4 ) == 1 )
			{
				for ( int i = 0; i < amount; ++i )
					PackItem( Loot.RandomNecromancyReagent() );
			}
			else
			{
				Item wizreg = Loot.RandomSecretReagent();
				if ( Server.Misc.Worlds.IsOnSpaceship( this.Location, this.Map ) ){ Server.Misc.MorphingTime.MakeSpaceAceItem( wizreg, this ); }
				UnknownReagent myreg = (UnknownReagent)wizreg;
				myreg.RegAmount = Utility.RandomMinMax( (amount * 1), (amount * 2) );
				PackItem( myreg );
			}
		}

		public void PackNecroReg( int amount )
		{
			if ( Utility.RandomMinMax( 1 , 4 ) == 1 )
			{
				for ( int i = 0; i < amount; ++i )
					PackItem( Loot.RandomNecromancyReagent() );
			}
			else
			{
				Item wizreg = Loot.RandomSecretReagent();
				UnknownReagent myreg = (UnknownReagent)wizreg;
				if ( Server.Misc.Worlds.IsOnSpaceship( this.Location, this.Map ) ){ Server.Misc.MorphingTime.MakeSpaceAceItem( wizreg, this ); }
				myreg.RegAmount = Utility.RandomMinMax( (amount * 1), (amount * 4) );
				PackItem( myreg );
			}
		}

		public void PackNecroReg()
		{
			if ( Utility.RandomMinMax( 1 , 4 ) == 1 ){ PackItem( Loot.RandomNecromancyReagent() ); }
			else
			{
				Item wizreg = Loot.RandomSecretReagent();
				UnknownReagent myreg = (UnknownReagent)wizreg;
				if ( Server.Misc.Worlds.IsOnSpaceship( this.Location, this.Map ) ){ Server.Misc.MorphingTime.MakeSpaceAceItem( wizreg, this ); }
				PackItem( myreg );
			}
		}

		public void PackReg( int min, int max )
		{
			PackReg( Utility.RandomMinMax( min, max ) );
		}

		public void PackReg( int amount )
		{
			if ( amount <= 0 )
				return;

			Item reg = Loot.RandomReagent();

			reg.Amount = amount;

			if ( Utility.RandomMinMax( 1 , 4 ) == 1 ){ PackItem( reg ); }
			else
			{
				Item wizreg = Loot.RandomSecretReagent();
				UnknownReagent myreg = (UnknownReagent)wizreg;
				if ( Server.Misc.Worlds.IsOnSpaceship( this.Location, this.Map ) ){ Server.Misc.MorphingTime.MakeSpaceAceItem( wizreg, this ); }
				myreg.RegAmount = amount; PackItem( myreg );
			}
		}

		public override void Delete()
		{
			if (m_breeding)
				return;
			
			base.Delete();
		}

		public void PackItem( Item item )
		{
			if ( Summoned || item == null )
			{
				if ( item != null )
					item.Delete();

				return;
			}

			Container pack = Backpack;

			if ( pack == null )
			{
				pack = new Backpack();

				pack.Movable = false;

				AddItem( pack );
			}

			if ( !item.Stackable || !pack.TryDropItem( this, item, false ) ) // try stack
				pack.DropItem( item ); // failed, drop it anyway
		}

		#endregion

		public override void OnDoubleClick( Mobile from )
		{
            if ( from.AccessLevel >= AccessLevel.GameMaster && !Body.IsHuman )
			{
				Container pack = this.Backpack;

				if ( pack != null )
					pack.DisplayTo( from );
			}

			if ( this.DeathAdderCharmable && from.CanBeHarmful( this, false ) )
			{
				DeathAdder da = Spells.Necromancy.SummonFamiliarSpell.Table[from] as DeathAdder;

				if ( da != null && !da.Deleted )
				{
					from.SendAsciiMessage( "You charm the snake.  Select a target to attack." );
					from.Target = new DeathAdderCharmTarget( this );
				}
			}

			base.OnDoubleClick( from );
		}

		private class DeathAdderCharmTarget : Target
		{
			private BaseCreature m_Charmed;

			public DeathAdderCharmTarget( BaseCreature charmed ) : base( -1, false, TargetFlags.Harmful )
			{
				m_Charmed = charmed;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( !m_Charmed.DeathAdderCharmable || m_Charmed.Combatant != null || !from.CanBeHarmful( m_Charmed, false ) )
					return;

				DeathAdder da = Spells.Necromancy.SummonFamiliarSpell.Table[from] as DeathAdder;
				if ( da == null || da.Deleted )
					return;

				Mobile targ = targeted as Mobile;
				if ( targ == null || !from.CanBeHarmful( targ, false ) )
					return;

				from.RevealingAction();
				from.DoHarmful( targ, true );

				m_Charmed.Combatant = targ;

				if ( m_Charmed.AIObject != null )
					m_Charmed.AIObject.Action = ActionType.Combat;
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( Core.ML )
			{
				if ( DisplayWeight && Controlled )
					list.Add( TotalWeight == 1 ? 1072788 : 1072789, TotalWeight.ToString() ); // Weight: ~1_WEIGHT~ stones

				if ( m_ControlOrder == OrderType.Guard )
					list.Add( 1080078 ); // guarding
			}

			if ( this is JediMirage || this is SythProjection || this is Clown || this is Clone ){} // NO WORDS
			else if ( this is HenchmanFamiliar )
				list.Add( "(familiar)" );
			else if ( this is PackBeast )
				list.Add( "(Pack Animal)" );
			else if ( this is GolemPorter || this is GolemFighter )
				list.Add( "(automaton)" );
			else if ( this is Robot )
				list.Add( "(robot)" );
			else if ( this is FrankenPorter || this is FrankenFighter )
				list.Add( "(reanimation)" );
			else if ( Summoned && !IsAnimatedDead && !IsNecroFamiliar )
				list.Add( 1049646 ); // (summoned)
			else if ( Controlled && Commandable && !(this is FrankenFighter) && !(this is AerialServant) && !(this is FrankenPorter) && !(this is Robot) && !(this is GolemFighter) && !(this is GolemPorter) && !(this is PackBeast) && !(this is HenchmanMonster) && !(this is HenchmanFighter) && !(this is HenchmanWizard) && !(this is HenchmanArcher) && !(this is HenchmanFamiliar) && !(this is BaseChild))
			{
				if (this is Squire)
						list.Add("(pledged)");
				else if ( IsBonded )	//Intentional difference (showing ONLY bonded when bonded instead of bonded & tame)
				{
					if ( ((Mobile)this).Karma > 100 )
						list.Add("(dominated)");
					else
						list.Add( 1049608 ); // (bonded)
				}
				else
				{
					if ( ((Mobile)this).Karma > 100 )
						list.Add("(enslaved)");
					else 
						list.Add( 502006 ); // (tame)
				}

                list.Add(1060662, String.Format("Loyalty Rating\t{0}%", Loyalty.ToString())); // ADD THIS
				
				#region Jako Taming
                if (!Summoned && JakoIsEnabled)
                    list.Add("Level {0} {1}", m_realLevel, SexString);
                #endregion

				if ( m_special != 0 ) 
				{
					if (m_special == 1)
						list.Add( "*Ferocious*" ); //damage increase done
					else if (m_special == 2)
						list.Add( "*Regenerative*" ); //life leech done
					else if (m_special == 3)
						list.Add( "*Mystical*" ); //hit harm done
					else if (m_special == 4)
						list.Add( "*Firebreather*" ); //hit fireball done
					else if (m_special == 5)
						list.Add( "*Thor's touch*" ); //hit lightning done
					else if (m_special == 6)
						list.Add( "*Tactical*" ); //Armor ignore done
					else if (m_special == 7)
						list.Add( "*Shamanic*" ); //can heal/res player done
					else if (m_special == 8)
						list.Add( "*Chaotic*" ); //Chaos damage done
					else if (m_special == 9)
						list.Add( "*Poisonous*" ); //hit poison (greater) done

				}
			if (m_injuries >0)
				list.Add( "This creature has suffered " + m_injuries + " injuries" );
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Controlled && Commandable )
			{
				int number;

				if ( Summoned )
					number = 1049646; // (summoned)
				else if ( IsBonded )
					number = 1049608; // (bonded)
				else
					number = 502006; // (tame)

				PrivateOverheadMessage( MessageType.Regular, 0x3B2, number, from.NetState );
			}

			base.OnSingleClick( from );
		}

		public virtual double TreasureMapChance{ get{ return TreasureMap.LootChance; } }
		public virtual int TreasureMapLevel{ get{ return -1; } }

		public virtual bool IgnoreYoungProtection { get { return false; } }

		public override void Kill()
		{
			if (this is ChampionGreaterMongbat || this is ChampionImp || this is ChampionGargoyle || this is ChampionHarpy || this is ChampionScorpion || this is ChampionGiantSpider || this is ChampionTerathanDrone || this is ChampionTerathanWarrior || this is ChampionLizardman || this is ChampionSnake || this is ChampionLavaLizard || this is ChampionOphidianWarrior || this is ChampionPixie || this is ChampionShadowWisp || this is ChampionKirin || this is ChampionWisp || this is ChampionGiantRat || this is ChampionSlime || this is ChampionDireWolf || this is ChampionRatman || this is ChampionDeathwatchBeetleHatchling || this is Champion2Lizardman || this is ChampionDeathwatchBeetle || this is ChampionKappa || this is Champion2Pixie || this is Champion2ShadowWisp || this is ChampionPlagueSpawn || this is ChampionBogling )
			{
				if (Utility.RandomDouble() > 0.20)// reduce lag in champ spawns
				{
					this.Delete();	
				}	
			}

			Region reg = Region.Find( this.Location, this.Map );

			if ( reg.IsPartOf( typeof( MBRegion ) ) || (this.Map == Map.Trammel && (this.X > 3631 && this.X < 3642) && ( this.Y > 2085 && this.Y < 2095))) 
			{
				this.Delete();
			}

			if (!Deleted)
				base.Kill();
		}

		public override bool OnBeforeDeath()
		{
			if (this == null || this.Deleted)
				return true;

			Region reg = Region.Find( this.Location, this.Map );
			int Heat = MyServerSettings.GetDifficultyLevel( this.Location, this.Map );

			SlayerEntry undead = SlayerGroup.GetEntryByName( SlayerName.Silver );
			SlayerEntry exorcism = SlayerGroup.GetEntryByName( SlayerName.Exorcism );

			Mobile slayer = this.FindMostRecentDamager(true);

			if (slayer is BaseCreature)
			{
					BaseCreature bc_killer = (BaseCreature)slayer;
					if(bc_killer.Summoned)
					{
						if(bc_killer.SummonMaster != null)
							slayer = bc_killer.SummonMaster;
					}
					else if(bc_killer.Controlled)
					{
						if(bc_killer.ControlMaster != null)
							slayer=bc_killer.ControlMaster;
					}
					else if(bc_killer.BardProvoked)
					{
						if(bc_killer.BardMaster != null)
							slayer=bc_killer.BardMaster;
					}
			}

			Mobile murderer = this.LastKiller;

			if (murderer is BaseCreature)
			{
					BaseCreature bc_killer = (BaseCreature)murderer;
					if(bc_killer.Summoned)
					{
						if(bc_killer.SummonMaster != null)
							murderer = bc_killer.SummonMaster;
					}
					else if(bc_killer.Controlled)
					{
						if(bc_killer.ControlMaster != null)
							murderer=bc_killer.ControlMaster;
					}
					else if(bc_killer.BardProvoked)
					{
						if(bc_killer.BardMaster != null)
							murderer=bc_killer.BardMaster;
					}
			}

			if ( AI == AIType.AI_Citizen )
			{
				if ( slayer is PlayerMobile )
				{
					slayer.Criminal = true;
					slayer.Kills = murderer.Kills + 1;
					Server.Items.DisguiseTimers.RemoveDisguise( slayer );
				}
				string bSay = "Help! Guards!";
				this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( bSay ) ); 
			}

			//Mobile slayer = ;

			/*if ( !this.IsBonded && ( slayer is BasePerson || ( slayer is BaseCreature && ((BaseCreature)slayer).FightMode == FightMode.Evil ) ) )
			{
				this.Delete();
			}
			else if ( !this.IsBonded && this is BaseCreature && this.FightMode == FightMode.Evil )
			{
				if ( slayer is PlayerMobile ){}
				else if ( slayer is BaseCreature && ((BaseCreature)slayer).ControlMaster != null && ((BaseCreature)slayer).Controlled ){}
				else { this.Delete(); }
			}
			else */if ( Heat > 0 && IsParagon == false )
			{
				BeefUpLoot( this, Heat );
			}

			if ( slayer is PlayerMobile )
			{

				if (AdventuresFunctions.IsPuritain((object)slayer))
					((PlayerMobile)slayer).AdjustReputation( this );

				///////////////////////////////////////////////////////////////////////////////////////

				Server.Misc.IntelligentAction.SaySomethingOnDeath( this, this.LastKiller );

				Server.Misc.HoardPile.MakeHoard( this ); // SEE IF A HOARD DROPS NEARBY

				Server.Misc.SummonQuests.WellTheyDied( this, this );

				///////////////////////////////////////////////////////////////////////////////////////

				if ( reg.IsPartOf( typeof( NecromancerRegion ) ) && GetPlayerInfo.EvilPlayer( slayer ) && slayer.Skills[SkillName.Necromancy].Base >= 25 )
				{
					if ( undead.Slays(this) || exorcism.Slays(this) )
					{
						switch ( Utility.Random( 7 ) )
						{
							case 0: PackItem( new BatWing( Utility.RandomMinMax( 1, 10 ) ) ); break;
							case 1: PackItem( new NoxCrystal( Utility.RandomMinMax( 1, 10 ) ) ); break;
							case 2: PackItem( new GraveDust( Utility.RandomMinMax( 1, 10 ) ) ); break;
							case 3: PackItem( new PigIron( Utility.RandomMinMax( 1, 10 ) ) ); break;
							case 4: PackItem( new DaemonBlood( Utility.RandomMinMax( 1, 10 ) ) ); break;
						}
					}
					else if ( this is EvilMage )
					{
						PackItem( new BatWing( Utility.RandomMinMax( 1, 10 ) ) );
						PackItem( new NoxCrystal( Utility.RandomMinMax( 1, 10 ) ) );
						PackItem( new GraveDust( Utility.RandomMinMax( 1, 10 ) ) );
						PackItem( new PigIron( Utility.RandomMinMax( 1, 10 ) ) );
						PackItem( new DaemonBlood( Utility.RandomMinMax( 1, 10 ) ) );
					}
				}

				Server.Misc.IntelligentAction.DropReagent( slayer, this );

				if ( slayer.Skills[SkillName.Forensics].Value >= Utility.RandomMinMax( 30, 150 ) )
				{
					if ( 	this is MummyGiant || this is FleshGolem || this is ReanimatedDragon || this is AncientFleshGolem || this is SkinGolem || 
							this is Ghoul || this is AquaticGhoul || this is DiseasedMummy || this is Mummy || this is MummyLord || this is RottingCorpse || 
							this is WalkingCorpse || this is ZombieGiant || this is FrozenCorpse || this is ZombieGargoyle || this is SeaZombie || 
							this is ZombieMage || this is Zombie )
					{
						PackItem( new EmbalmingFluid() );
					}
				}
				// run gold find check for soulbound
				if (slayer is PlayerMobile && ((PlayerMobile)slayer).SoulBound) {
					Phylactery phylactery = ((PlayerMobile)slayer).FindPhylactery();
					if (phylactery != null) {
						int extraGold = phylactery.CalculateExtraGold(this.TotalGold);
						if (extraGold > 0) {
							this.Backpack.DropItem(new Gold(extraGold));
						}
					}
				}

			}

			///////////////////////////////////////////////////////////////////////////////////////

			SlayerEntry spreaddeath = SlayerGroup.GetEntryByName( SlayerName.Repond );

			//Mobile deathknight = this.LastKiller;										// DEATH KNIGHT HOLDING SOUL LANTERNS
			if ( spreaddeath.Slays(this) && murderer != null && this.TotalGold > 0 )	// TURNS THE MONEY TO SOUL COUNT
			{
				if ( murderer is BaseCreature )
					murderer = ((BaseCreature)murderer).GetMaster();

				if ( murderer is PlayerMobile )
				{
					Item lantern = murderer.FindItemOnLayer( Layer.TwoHanded );

					if ( lantern is SoulLantern )
					{
						SoulLantern souls = (SoulLantern)lantern;
						souls.TrappedSouls = souls.TrappedSouls + this.TotalGold;
						if ( souls.TrappedSouls > 100000 ){ souls.TrappedSouls = 100000; }
						souls.InvalidateProperties();

						Item deathpack = this.FindItemOnLayer( Layer.Backpack );
						if ( deathpack != null )
						{
							Item dtcoins = this.Backpack.FindItemByType( typeof( Gold ) );
							dtcoins.Delete();
							murderer.SendMessage( "A soul has been claimed." );
							Effects.SendLocationParticles( EffectItem.Create( murderer.Location, murderer.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
							Effects.PlaySound( murderer.Location, murderer.Map, 0x1ED );
						}
					}
				}

			}

			///////////////////////////////////////////////////////////////////////////////////////

			SlayerEntry holyundead = SlayerGroup.GetEntryByName( SlayerName.Silver );
			SlayerEntry holydemons = SlayerGroup.GetEntryByName( SlayerName.Exorcism );

			//Mobile holyman = this.LastKiller;																		// HOLY MANY HOLDING HOLY SYMBOL
			if ( ( holyundead.Slays(this) || holydemons.Slays(this) ) && murderer != null && this.TotalGold > 0 )	// TURNS THE MONEY TO BANISH COUNT
			{
				if ( murderer is BaseCreature )
					murderer = ((BaseCreature)murderer).GetMaster();

				if ( murderer is PlayerMobile )
				{
					Item symbol = murderer.FindItemOnLayer( Layer.Talisman );

					if ( symbol is HolySymbol )
					{
						HolySymbol banish = (HolySymbol)symbol;
						banish.BanishedEvil = banish.BanishedEvil + this.TotalGold;
						if ( banish.BanishedEvil > 100000 ){ banish.BanishedEvil = 100000; }
						banish.InvalidateProperties();

						Item deathpack = this.FindItemOnLayer( Layer.Backpack );
						if ( deathpack != null )
						{
							Item dtcoins = this.Backpack.FindItemByType( typeof( Gold ) );
							dtcoins.Delete();
							murderer.SendMessage( "Evil has been banished." );
							murderer.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Waist );
							murderer.PlaySound( 0x1EA );
						}
					}
				}
			}

			///////////////////////////////////////////////////////////////////////////////////////

			// GOLDEN FEATHERS FOR THE RANGERS OUTPOST ALTAR
			if ( this is Harpy || this is StoneHarpy || this is SnowHarpy || this is Phoenix || this is HarpyElder || this is HarpyHen )
			{
				//Mobile FeatherGetter = this.LastKiller;

				if ( murderer is BaseCreature )
					murderer = ((BaseCreature)murderer).GetMaster();

				if ( murderer is PlayerMobile )
				{
					Item RangerBook = murderer.Backpack.FindItemByType( typeof( GoldenRangers ) );
					if ( RangerBook != null && ( murderer.Skills[SkillName.Camping].Base >= 90 || murderer.Skills[SkillName.Tracking].Base >= 90 ) )
					{
						int FeatherChance = 5;
						if ( this is Phoenix ){ FeatherChance = 25; }

						if ( FeatherChance >= Utility.RandomMinMax( 1, 100 ) )
						{
							ArrayList targets = new ArrayList();
							foreach ( Item item in World.Items.Values )
							if ( item is GoldenFeathers )
							{
								GoldenFeathers goldfeather = (GoldenFeathers)item;
								if ( goldfeather.owner == murderer )
								{
									targets.Add( item );
								}
							}
							for ( int i = 0; i < targets.Count; ++i )
							{
								Item item = ( Item )targets[ i ];
								item.Delete();
							}
							murderer.AddToBackpack( new GoldenFeathers( murderer ) );
							murderer.SendSound( 0x3D );
							murderer.PrivateOverheadMessage(MessageType.Regular, 1150, false, "The goddess has given you golden feathers.", murderer.NetState);
						}
					}
				}
			}

			int treasureLevel = TreasureMapLevel;

			if ( treasureLevel == 1 && this.Map == Map.Trammel )
			{
				//Mobile killer = this.LastKiller;

				if ( murderer is BaseCreature )
					murderer = ((BaseCreature)murderer).GetMaster();

				if ( murderer is PlayerMobile && ((PlayerMobile)murderer).Young )
					treasureLevel = 0;
			}

			if ( !Summoned && !NoKillAwards && !IsBonded && treasureLevel >= 0 )
			{
				if ( m_Paragon && Paragon.ChestChance > Utility.RandomDouble() )
					PackItem( new ParagonChest( this.Name, this.Title, treasureLevel, this ) );
				
				//if ( m_Paragon && Paragon.DeedChance > Utility.RandomDouble() && Utility.RandomBool() )
				//	PackItem( new ParagonPetDeed() );
					
				else if ( TreasureMap.LootChance >= Utility.RandomDouble() )
				{
					PackItem( new TreasureMap( treasureLevel, this.Map, this.Location, this.X, this.Y ) );
				}
			}

			if ( !Summoned && !NoKillAwards && !m_HasGeneratedLoot )
			{
				m_HasGeneratedLoot = true;
				GenerateLoot( false );
			}

			if ( IsAnimatedDead )
				Effects.SendLocationEffect( Location, Map, 0x3728, 13, 1, 0x461, 4 );

			InhumanSpeech speechType = this.SpeechType;

			if ( speechType != null )
				speechType.OnDeath( this );

			if ( m_ReceivedHonorContext != null )
				m_ReceivedHonorContext.OnTargetKilled();
		
           //Start Zombiex edit
            if (murderer is Zombiex || murderer is WanderingConcubine || (murderer is BaseCreature && ((BaseCreature)murderer).CanInfect))
            {
				Region region = Region.Find( this.Location, this.Map );

				if ( !(region.IsPartOf( typeof( ChampionSpawnRegion ) ) ) && !(region is ChampionSpawnRegion ) ) 
				{
					Zombiex zomb = new Zombiex();
					zomb.NewZombie(this);
				}

            }
            //End Zombiex edit
	
            else if (murderer is PlayerMobile) // Final: Widow's morphing armor addition
            {
				PlayerMobile pmkiller = (PlayerMobile)murderer;
				if (pmkiller.BodyMod == 84 && Utility.RandomBool() ) 
				{
					WidowSpawn spawn = new WidowSpawn();
					spawn.NewSpawn(this, LastKiller);
				}
            }	

            #region Jako Taming
            if (IsBonded && JakoIsEnabled)
                DecayExperience(LastKiller);
            if (ControlMaster != null && JakoIsEnabled)
                DeathNotification();
            else if (!Summoned && JakoIsEnabled)
                CalculateExpDist(this);
            #endregion

			if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" && Utility.RandomMinMax(1, 500) == 69)
			{
				PackItem( new DoomGuy());
			}

				//Halloween bags!  
				
				string CurrentMonth = DateTime.UtcNow.ToString("MM");
				
				if ( CurrentMonth == "10" )
				{
					if ( Utility.RandomBool() && Server.Misc.GetPlayerInfo.LuckyPlayer( ((Mobile)this.LastKiller).Luck, ((Mobile)this).LastKiller ) )
					{
						PackItem( new HalloweenBag());
					}
				}

			// final - Doom drops
			if (!NoKillAwards && ( Region.IsPartOf("Doom") || Region.IsPartOf("Doom Gauntlet") ) )
			{
				
				int bones = Engines.Quests.Doom.TheSummoningQuest.GetDaemonBonesFor(this);

				if (bones > 0)
					PackItem(new DaemonBone(bones));
				
				int dropchance = 0;
				
				if (this.Fame <= 5000)
					dropchance = 1000;
				else if (this.Fame <= 10000)
					dropchance = 500;	
				else if (this.Fame <= 15000)
					dropchance = 250;
				else if (this.Fame <= 20000)
					dropchance = 100;				
				else if (this.Fame >= 20001)
					dropchance = 60;

				if (this is DemonKnight)
					dropchance = 25;

				if (Utility.Random(dropchance) == 15)
				{
				    PackItem(new EnhancementDeed());
				}

				if (Region.IsPartOf("Doom Gauntlet"))
				{
					switch (Utility.Random( (dropchance * 4) ) )
					{
						case 0:
							PackItem(new DarkFatherMorphArms());
							break;
						case 1:
							PackItem(new DarkFatherMorphChest());
							break;
						case 2:
							PackItem(new DarkFatherMorphGloves());
							break;
						case 3:
							PackItem(new DarkFatherMorphGorget());
							break;
						case 4:
							PackItem(new DarkFatherMorphHelm());
							break;
						case 5:
							PackItem(new DarkFatherMorphLegs());
							break;
					}	
				}					
			}

			double changechange = (double)Math.Abs(this.Fame / 300);

			if (changechange > 0 && Utility.RandomDouble() < (1 / changechange) )
				HealthOrb.Drop(this);
			else if (changechange > 0 && Utility.RandomDouble() < ((changechange / 125)) )
				SkillOrb.DropSkill(this);
			
			if (LastKiller != null && ( LastKiller is BlueGuard || LastKiller is MidlandGuard) )
			{

				ArrayList delete = new ArrayList();
				if (this.Backpack != null)
				{
					foreach (Item item in this.Backpack.Items)
					{

						if (item != null )
						{

							if ( (item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
							{

								if (item is Gold || item is DDSilver || item is DDCopper)
								{
									delete.Add( item );
								}
									
								else if (Utility.RandomDouble() > 0.66)
								{
									delete.Add( item );

								}
							}
						}
					}
			
					if (delete.Count > 0)
					{
						for ( int i = 0; i < delete.Count; ++i )
						{
							Item items = ( Item )delete[ i ];
							items.Delete();
						}
						LastKiller.Say("We shall take these items for our Coffers!");
					}
				}
				
			}


			if (AdventuresFunctions.IsPuritain((object)this) && Body.IsHuman && midrace > 0 )
				Server.Misc.MorphingTime.TurnToSomethingOnDeath( this );

			return base.OnBeforeDeath();
		}

		private bool m_NoKillAwards;

		public bool NoKillAwards
		{
			get{ return m_NoKillAwards; }
			set{ m_NoKillAwards = value; }
		}

		public int ComputeBonusDamage( List<DamageEntry> list, Mobile m )
		{
			int bonus = 0;

			for ( int i = list.Count - 1; i >= 0; --i )
			{
				DamageEntry de = list[i];

				if ( de.Damager == m || !(de.Damager is BaseCreature) )
					continue;

				BaseCreature bc = (BaseCreature)de.Damager;
				Mobile master = null;

				master = bc.GetMaster();

				if ( master == m )
					bonus += de.DamageGiven;
			}

			return bonus;
		}

		public Mobile GetMaster()
		{
			if ( Controlled && ControlMaster != null )
				return ControlMaster;
			else if ( Summoned && SummonMaster != null )
				return SummonMaster;

			return null;
		}

		private class FKEntry
		{
			public Mobile m_Mobile;
			public int m_Damage;

			public FKEntry( Mobile m, int damage )
			{
				m_Mobile = m;
				m_Damage = damage;
			}
		}

		public static List<DamageStore> GetLootingRights( List<DamageEntry> damageEntries, int hitsMax )
		{
			List<DamageStore> rights = new List<DamageStore>();

			for ( int i = damageEntries.Count - 1; i >= 0; --i )
			{
				if ( i >= damageEntries.Count )
					continue;

				DamageEntry de = damageEntries[i];

				if ( de.HasExpired )
				{
					damageEntries.RemoveAt( i );
					continue;
				}

				int damage = de.DamageGiven;

				List<DamageEntry> respList = de.Responsible;

				if ( respList != null )
				{
					for ( int j = 0; j < respList.Count; ++j )
					{
						DamageEntry subEntry = respList[j];
						Mobile master = subEntry.Damager;

						if ( master == null || master.Deleted || !master.Player )
							continue;

						bool needNewSubEntry = true;

						for ( int k = 0; needNewSubEntry && k < rights.Count; ++k )
						{
							DamageStore ds = rights[k];

							if ( ds.m_Mobile == master )
							{
								ds.m_Damage += subEntry.DamageGiven;
								needNewSubEntry = false;
							}
						}

						if ( needNewSubEntry )
							rights.Add( new DamageStore( master, subEntry.DamageGiven ) );

						damage -= subEntry.DamageGiven;
					}
				}

				Mobile m = de.Damager;

				if ( m == null || m.Deleted || !m.Player )
					continue;

				if ( damage <= 0 )
					continue;

				bool needNewEntry = true;

				for ( int j = 0; needNewEntry && j < rights.Count; ++j )
				{
					DamageStore ds = rights[j];

					if ( ds.m_Mobile == m )
					{
						ds.m_Damage += damage;
						needNewEntry = false;
					}
				}

				if ( needNewEntry )
					rights.Add( new DamageStore( m, damage ) );
			}

			if ( rights.Count > 0 )
			{
				rights[0].m_Damage = (int)(rights[0].m_Damage * 1.25);	//This would be the first valid person attacking it.  Gets a 25% bonus.  Per 1/19/07 Five on Friday

				if ( rights.Count > 1 )
					rights.Sort(); //Sort by damage

				int topDamage = rights[0].m_Damage;
				int minDamage;

				if ( hitsMax >= 3000 )
					minDamage = topDamage / 16;
				else if ( hitsMax >= 1000 )
					minDamage = topDamage / 8;
				else if ( hitsMax >= 200 )
					minDamage = topDamage / 4;
				else
					minDamage = topDamage / 2;

				for ( int i = 0; i < rights.Count; ++i )
				{
					DamageStore ds = rights[i];

					ds.m_HasRight = ( ds.m_Damage >= minDamage );
				}
			}

			return rights;
		}

		public virtual void OnKilledBy( Mobile mob )
		{
			if ( m_Paragon && Paragon.CheckArtifactChance( mob, this ) )
				Paragon.GiveArtifactTo( mob );
		}

		public override void OnDeath( Container c )
		{

			if (this.LastKiller == null) // final added to prevent null crash
			{
				base.OnDeath( c );
				return;
			}
				
			if (this is ChampionGreaterMongbat || this is ChampionImp || this is ChampionGargoyle || this is ChampionHarpy || this is ChampionScorpion || this is ChampionGiantSpider || this is ChampionTerathanDrone || this is ChampionTerathanWarrior || this is ChampionLizardman || this is ChampionSnake || this is ChampionLavaLizard || this is ChampionOphidianWarrior || this is ChampionPixie || this is ChampionShadowWisp || this is ChampionKirin || this is ChampionWisp || this is ChampionGiantRat || this is ChampionSlime || this is ChampionDireWolf || this is ChampionRatman || this is ChampionDeathwatchBeetleHatchling || this is Champion2Lizardman || this is ChampionDeathwatchBeetle || this is ChampionKappa || this is Champion2Pixie || this is Champion2ShadowWisp || this is ChampionPlagueSpawn || this is ChampionBogling )
			{
				if (Utility.RandomBool())// reduce lag in champ spawns
				{
					for ( int i = 0; i < this.Items.Count; ++i )
					{
						Item item = this.Items[i];
						item.Delete();
					}
				}	
			}
					

			Mobile killer = this.LastKiller;
			Map grave = killer.Map;

			Region reg = Region.Find( this.Location, this.Map );

			QuestTake.DropChest( this );

			// RESET SPAWNERS TIME TO MAX FOR SPAWNS WITH ONLY ONE CREATURE AND A NEARBY RANGE /////////////////////////////////////////////////////////////////////////////
			// THIS KEEPS TIMERS FROM COUNTING DOWN WHILE THE SPAWNER WAITS AND THUS KEEPS A   /////////////////////////////////////////////////////////////////////////////
			// TRUE TIME TO RESPAWN AGAIN ONCE THE MONSTER IS KILLED                           /////////////////////////////////////////////////////////////////////////////

			if ( this.Controlled == false && this.ControlMaster == null && this.Home.X > 0 && this.Home.Y > 0 && grave != null )
			{
				IPooledEnumerable eable = grave.GetItemsInRange( this.Home, 0 );

				foreach ( Item item in eable )
				{
					if ( item is PremiumSpawner )
					{
						PremiumSpawner spwn = (PremiumSpawner)item;

						if ( spwn.SpawnID == 99999 || ( spwn.Count + spwn.CountA + spwn.CountB + spwn.CountC + spwn.CountD + spwn.CountD ) == 1 && spwn.HomeRange < 10 && spwn.WalkingRange < 10 )
						{
							int minSeconds = (int)(spwn.MinDelay).TotalSeconds;
							int maxSeconds = (int)(spwn.MaxDelay).TotalSeconds;
							TimeSpan sDelay = TimeSpan.FromSeconds( Utility.RandomMinMax( minSeconds, maxSeconds ) );
							spwn.DoTimer( sDelay );
						}
					}
				}

				eable.Free();
			}

			if (killer is BaseCreature)
			{
				BaseCreature bc_killer = (BaseCreature)killer;
				if(bc_killer.Summoned)
				{
					if(bc_killer.SummonMaster != null)
						killer = bc_killer.SummonMaster;
				}
				else if(bc_killer.Controlled )
				{
					if(bc_killer.ControlMaster != null)
						killer=bc_killer.ControlMaster;

					if (Utility.RandomDouble() >= 0.99)
						bc_killer.Loyalty -= 1;

					if ( bc_killer.Loyalty <= 35 && ( ( (double)bc_killer.Loyalty / 100 ) < Utility.RandomDouble() ) )
					{
						bc_killer.PublicOverheadMessage( MessageType.Emote, EmoteHue, false, "*This animal has gone feral!*" );
						bc_killer.GoFeral = true;
					}

				}
				else if(bc_killer.BardProvoked)
				{
					if(bc_killer.BardMaster != null)
					{
						killer=bc_killer.BardMaster;

						if (killer is PlayerMobile)
						{
									double diff = BaseInstrument.GetBaseDifficulty( this );

									int amount = 1;
									if (this.Fame > 20000)
										amount = 10;
									else if (this.Fame > 15000)
										amount = 7;
									else if (this.Fame > 10000)
										amount = 4;
									else if (this.Fame > 5000)
										amount = 2;

									while (amount > 0)
									{
										killer.CheckTargetSkill( SkillName.Provocation, this, diff-25.0, diff+25.0 ); 
										amount -= 1;
									}
						}
					}
				}
			}

			if ( ( killer is PlayerMobile ) && (killer.AccessLevel < AccessLevel.GameMaster) && !(this is Zombiex) && !(this is BaseUndead) && this.Name != null )
			{
				LoggingFunctions.LogBattles( killer, this );
			}

			if ( killer is PlayerMobile )
			{
				
				if (((PlayerMobile)killer).Avatar && Utility.RandomMinMax(1,200) == 69 && Utility.RandomDouble() > 0.90)
				{
					OneRing ring = new OneRing();
					((PlayerMobile)killer).Backpack.DropItem(ring);
					ring.OnAfterSpawn();
					killer.SendMessage("You feel the true ring of power nearby!");
				}

				AssassinFunctions.CheckTarget( killer, this );
				StandardQuestFunctions.CheckTarget( killer, this, null );
				FishingQuestFunctions.CheckTarget( killer, this, null );
				if ( killer.Backpack.FindItemByType( typeof ( MuseumBook ) ) != null && this.Fame >= 18000 )
				{
					MuseumBook.FoundItem( killer, 1 );
				}
				if ( killer.Backpack.FindItemByType( typeof ( QuestTome ) ) != null && this.Fame >= 18000 )
				{
					QuestTome.FoundItem( killer, 1, null );
				}

			}

			Server.Misc.DropRelic.DropSpecialItem( this, killer, c ); // SOME DROP RARE ITEMS

			if ( IsBonded )
			{
				int sound = this.GetDeathSound();

				if ( sound >= 0 )
					Effects.PlaySound( this, this.Map, sound );

				Warmode = false;

				Poison = null;
				Combatant = null;

				Hits = 0;
				Stam = 0;
				Mana = 0;

				IsDeadPet = true;
				ControlTarget = ControlMaster;
				ControlOrder = OrderType.Follow;

				ProcessDeltaQueue();
				SendIncomingPacket();
				SendIncomingPacket();

				List<AggressorInfo> aggressors = this.Aggressors;

				for ( int i = 0; i < aggressors.Count; ++i )
				{
					AggressorInfo info = aggressors[i];

					if ( info.Attacker.Combatant == this )
						info.Attacker.Combatant = null;
				}

				List<AggressorInfo> aggressed = this.Aggressed;

				for ( int i = 0; i < aggressed.Count; ++i )
				{
					AggressorInfo info = aggressed[i];

					if ( info.Defender.Combatant == this )
						info.Defender.Combatant = null;
				}

				Mobile owner = this.ControlMaster;

				if ( owner == null || owner.Deleted || owner.Map != this.Map || !owner.InRange( this, 12 ) || !this.CanSee( owner ) || !this.InLOS( owner ) )
				{
					if ( this.OwnerAbandonTime == DateTime.MinValue )
						this.OwnerAbandonTime = DateTime.UtcNow;
				}
				else
				{
					this.OwnerAbandonTime = DateTime.MinValue;
				}

				CheckStatTimers();
			}
			else
			{
				if ( !Summoned && !m_NoKillAwards )
				{
					int totalFame = Fame / 100;
					int totalKarma = -Karma / 100;

					List<DamageStore> list = GetLootingRights( this.DamageEntries, this.HitsMax );
					List<Mobile> titles = new List<Mobile>();
					List<int> fame = new List<int>();
					List<int> soulForce = new List<int>();
					List<int> karma = new List<int>();

					bool givenQuestKill = false;
					bool givenToTKill = false;

					for ( int i = 0; i < list.Count; ++i )
					{
						DamageStore ds = list[i];

						if ( !ds.m_HasRight )
							continue;

						Party party = Engines.PartySystem.Party.Get( ds.m_Mobile );

						if ( party != null)
						{
							int divedFame = totalFame / party.Members.Count;
							int divedKarma = totalKarma / party.Members.Count;							
							if (party.IsSoulBound() && SecondsSoulTouched > 0) {
								// reset the fame divide and award them
								int soulboundFameBonus = (int)(totalFame*party.Members.Count);
								divedFame = totalFame + soulboundFameBonus;
							}

							for ( int j = 0; j < party.Members.Count; ++j )
							{
								PartyMemberInfo info = party.Members[ j ] as PartyMemberInfo;

								if ( info != null && info.Mobile != null )
								{
									int index = titles.IndexOf( info.Mobile );
									if ( index == -1 )
									{
										titles.Add( info.Mobile );
										fame.Add( divedFame );
										karma.Add( divedKarma );
									}
									else
									{
										fame[ index ] += divedFame;
										karma[ index ] += divedKarma;
									}
								}
							}
						}
						else
						{
							titles.Add( ds.m_Mobile );
							fame.Add( totalFame );
							karma.Add( totalKarma );
						}

						OnKilledBy( ds.m_Mobile );

						Region region = ds.m_Mobile.Region;

						if ( givenQuestKill )
							continue;

						PlayerMobile pm = ds.m_Mobile as PlayerMobile;

						if ( pm != null )
						{
							QuestSystem qs = pm.Quest;

							if ( qs != null )
							{
								qs.OnKill( this, c );
								givenQuestKill = true;
							}
						}
					}
					for ( int i = 0; i < titles.Count; ++i )
					{
						Titles.AwardFame( titles[ i ], fame[ i ], true );
						Titles.AwardKarma( titles[ i ], karma[ i ], true );
					}
				}
				
				//final balancelevel

				if (this is BaseChild)
				{
					if (killer is PlayerMobile)
						((PlayerMobile)killer).BalanceEffect += 5;
					AetherGlobe.ChangeCurse( 5 ); // killing children adds to the curse
					killer.CriminalAction( true );

					if (killer.Karma < 0) 
						Titles.AwardFame( killer,5, true );

					Titles.AwardKarma( killer, -50, true );
				}
				else 
				{
					
					Mobile killera = killer;
					if (killer is BaseCreature)
					{
						if ( ((BaseCreature)killer).Controlled && ((BaseCreature)killer).ControlMaster != null && ((BaseCreature)killer).ControlMaster is PlayerMobile )
							killera = ((BaseCreature)killer).ControlMaster; // tamed pet killed this foe
						else if (((BaseCreature)killer).BardProvoked && ((BaseCreature)killer).BardMaster is PlayerMobile)
							killera = ((BaseCreature)killer).BardMaster; //killed was provoked
						else // npc's killing other npc's doesn't count towards balance.
						{
							base.OnDeath( c );
							return;
						}
					}

					if (killera is PlayerMobile) 
					{
						double changechange = 0;
						if ( ((PlayerMobile)killera).BalanceStatus != 0)
							changechange = ((double)this.Karma / 2500) ;//* ((double)Math.Abs(killer.Karma) / 10000);
						else 
							changechange = ((double)this.Karma / 5000) ;//* ((double)Math.Abs(killer.Karma) / 10000);
						
						Region region = Region.Find( this.Location, this.Map );

						if ( region.IsPartOf( typeof( ChampionSpawnRegion ) )|| region is ChampionSpawnRegion ) 						
						{
							changechange /= 3;
						}	

						if ( !((PlayerMobile)killera).Avatar )
							changechange /= 10;

						double karmaeffectneg = 0;
						double karmaeffectpos = 0;
						
						if ( killera.Karma < 0)
						{
							karmaeffectneg = (double)Math.Abs(killera.Karma) / 15000;
							if (karmaeffectneg >= 1)
								karmaeffectpos = 0;
							else
								karmaeffectpos = 1- karmaeffectneg;
						}
						else if (killera.Karma > 0)
						{
							karmaeffectpos = (double)killera.Karma / 15000;
							karmaeffectneg = 0;

						}						

						double changeevil = Math.Abs(changechange) * karmaeffectneg;	
						double changegood = changechange * karmaeffectpos;	

						if (((PlayerMobile)killera).Avatar)
						{//sanity checks
							if (changeevil < 0)
								changeevil = Math.Abs(changeevil);
							if (changegood > 0)
								changegood = changegood * -1;	
						}
								
						changechange = changeevil + changegood;
						
						if (killera.Kills >0 && changechange >0) // really evil people have more influence on evil
							changechange *= 1+ (killera.Kills / 200);
						
						else if (killera.Kills >0 && changechange <0)
							changechange /= 1+ (killera.Kills / 200);
						
						if (Math.Abs(changechange) < 1 && Utility.RandomDouble() < 0.20 )
						{
							if (changechange < 0)
								changechange = -1;
							else 
								changechange = 1;
						}
						else if (Math.Abs(changechange) < 1)// someone killed something small and it won't count.
						{
							base.OnDeath( c );
							return;
						}
						double yo_sup = 0.75;
						if (((PlayerMobile)killera).SoulBound)
							yo_sup = 1.0;

						if (OneRing.wearer == killera)
							yo_sup = 1.25;
							
						((PlayerMobile)killera).BalanceEffect += Convert.ToInt32((changechange*yo_sup));
						AetherGlobe.ChangeCurse( Convert.ToInt32(changechange) );

					}

				}

				if (Utility.RandomDouble() > 0.97 && !(this is BaseRed) && !(this is BaseBlue) && !(this is BaseVendor) && !(this is BaseChild) && !(this is BaseCursed) && !(this is BaseChampion) && !(this is Zombiex))
					c.AddItem( new EssenceBones(this.GetType()));

				base.OnDeath( c );

				if ( DeleteCorpseOnDeath || ( ( this.Name == "a follower" || this.Name == "a sailor" || this.Name == "a pirate" ) && this.EmoteHue > 0 ) )
					c.Delete();
			}
			
		}

		/* To save on cpu usage, RunUO creatures only reacquire creatures under the following circumstances:
		 *  - 10 seconds have elapsed since the last time it tried
		 *  - The creature was attacked
		 *  - Some creatures, like dragons, will reacquire when they see someone move
		 *
		 * This functionality appears to be implemented on OSI as well
		 */

		private DateTime m_NextReacquireTime;

		public DateTime NextReacquireTime{ get{ return m_NextReacquireTime; } set{ m_NextReacquireTime = value; } }

		public virtual TimeSpan ReacquireDelay{ get{ return TimeSpan.FromSeconds( 7.0 ); } }
		public virtual bool ReacquireOnMovement{ get{ return false; } }

		public void ForceReacquire()
		{
			m_NextReacquireTime = DateTime.MinValue;
		}

		public override void OnDelete()
		{
			Mobile m = m_ControlMaster;

			SetControlMaster( null );
			SummonMaster = null;

			if ( m_ReceivedHonorContext != null )
				m_ReceivedHonorContext.Cancel();

			if ( this is HenchmanFamiliar )
			{
				ArrayList bagitems = new ArrayList(this.Backpack.Items);
				foreach (Item item in bagitems)
				{
					if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
					{
						item.MoveToWorld(this.Location, this.Map);
					}
				}
			}
			else if ( this is PackBeast )
			{
				ArrayList bagitems = new ArrayList(this.Backpack.Items);
				foreach (Item item in bagitems)
				{
					if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
					{
						item.MoveToWorld(this.Location, this.Map);
					}
				}
			}
			else if ( this is GolemPorter )
			{
				ArrayList bagitems = new ArrayList(this.Backpack.Items);
				foreach (Item item in bagitems)
				{
					if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
					{
						item.MoveToWorld(this.Location, this.Map);
					}
				}
			}
			else if ( this is FrankenPorter )
			{
				ArrayList bagitems = new ArrayList(this.Backpack.Items);
				foreach (Item item in bagitems)
				{
					if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
					{
						item.MoveToWorld(this.Location, this.Map);
					}
				}
			}
			else if ( this is AerialServant )
			{
				ArrayList bagitems = new ArrayList(this.Backpack.Items);
				foreach (Item item in bagitems)
				{
					if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
					{
						item.MoveToWorld(this.Location, this.Map);
					}
				}
			}

			base.OnDelete();

			if ( m != null )
				m.InvalidateProperties();
		}

		public override bool CanBeHarmful( Mobile target, bool message, bool ignoreOurBlessedness )
		{

			if ( (target is BaseVendor && ((BaseVendor)target).IsInvulnerable) || target is PlayerVendor || target is PlayerBarkeeper )
			{
				if ( message )
				{
					if ( target.Title == null )
						SendMessage( "{0} the vendor cannot be harmed.", target.Name );
					else
						SendMessage( "{0} {1} cannot be harmed.", target.Name, target.Title );
				}

				return false;
			}
			

			return base.CanBeHarmful( target, message, ignoreOurBlessedness );
		}

		public override bool CanBeRenamedBy( Mobile from )
		{
			bool ret = base.CanBeRenamedBy( from );

			if ( Controlled && from == ControlMaster && !from.Region.IsPartOf( typeof( Jail ) ) )
				ret = true;

			return ret;
		}

		public bool SetControlMaster( Mobile m )
		{
			if ( m == null )
			{
				ControlMaster = null;
				Controlled = false;
				ControlTarget = null;
				ControlOrder = OrderType.None;
				Guild = null;

				Delta( MobileDelta.Noto );
			}
			else
			{
				ISpawner se = this.Spawner;
				if ( se != null && se.UnlinkOnTaming )
				{
					this.Spawner.Remove( this );
					this.Spawner = null;
				}

				if ( m.Followers + ControlSlots > m.FollowersMax )
				{
					m.SendLocalizedMessage( 1049607 ); // You have too many followers to control that creature.
					return false;
				}

				CurrentWayPoint = null;//so tamed animals don't try to go back

				ControlMaster = m;
				Controlled = true;
				ControlTarget = null;
				ControlOrder = OrderType.Come;
				Guild = null;
				Criminal = false;
				Warmode = false;
				Combatant = null;

				if (AdvancedAI)
					AdvancedAI = false;

				if ( m_DeleteTimer != null )
				{
					m_DeleteTimer.Stop();
					m_DeleteTimer = null;
				}

				Delta( MobileDelta.Noto );
			}

			InvalidateProperties();

			return true;
		}

		public override void OnRegionChange( Region Old, Region New )
		{
			base.OnRegionChange( Old, New );

			if ( this.Controlled )
			{
				SpawnEntry se = this.Spawner as SpawnEntry;

				if ( se != null && !se.UnlinkOnTaming && ( New == null || !New.AcceptsSpawnsFrom( se.Region ) ) )
				{
					this.Spawner.Remove( this );
					this.Spawner = null;
				}
			}
		}

		private static bool m_Summoning;

		public static bool Summoning
		{
			get{ return m_Summoning; }
			set{ m_Summoning = value; }
		}

		public static bool Summon( BaseCreature creature, Mobile caster, Point3D p, int sound, TimeSpan duration )
		{
			return Summon( creature, true, caster, p, sound, duration );
		}

		public static bool Summon( BaseCreature creature, bool controlled, Mobile caster, Point3D p, int sound, TimeSpan duration )
		{
			if ( caster.Followers + creature.ControlSlots > caster.FollowersMax )
			{
				caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				creature.Delete();
				return false;
			}

			m_Summoning = true;

			if ( controlled )
				creature.SetControlMaster( caster );

			creature.RangeHome = 10;
			creature.Summoned = true;

			creature.SummonMaster = caster;


			if (	creature is SummonedAirElemental || 
					creature is SummonedAirElementalGreater || 
					creature is SummonedDaemon || 
					creature is SummonedDaemonGreater || 
					creature is SummonedEarthElemental || 
					creature is SummonedEarthElementalGreater || 
					creature is SummonedFireElemental || 
					creature is SummonedFireElementalGreater || 
					creature is SummonedWaterElemental || 
					creature is SummonedWaterElementalGreater || 
					creature is AncientFleshGolem || 
					creature is FleshGolem || 
					creature is BloodElemental || 
					creature is ElectricalElemental || 
					creature is GemElemental || 
					creature is IceElemental || 
					creature is MudElemental || 
					creature is PoisonElemental || 
					creature is WeedElemental || 
					creature is ToxicElemental || 
					creature is SummonedTreefellow || 
					creature is BoneKnight || 
					creature is Devil || 
					creature is BlackBear || 
					creature is BrownBear || 
					creature is DireWolf || 
					creature is Panther || 
					creature is Tiger || 
					creature is TigerRiding || 
					creature is TimberWolf || 
					creature is Scorpion || 
					creature is GiantSpider || 
					creature is HugeLizard || 
					creature is GiantToad || 
					creature is Slime )
			{
				creature.ControlOrder = OrderType.Guard;
			}


			Container pack = creature.Backpack;

			if ( pack != null )
			{
				for ( int i = pack.Items.Count - 1; i >= 0; --i )
				{
					if ( i >= pack.Items.Count )
						continue;

					pack.Items[i].Delete();
				}
			}

			new UnsummonTimer( caster, creature, duration ).Start();
			creature.m_SummonEnd = DateTime.UtcNow + duration;

			creature.MoveToWorld( p, caster.Map );

			Effects.PlaySound( p, creature.Map, sound );

			m_Summoning = false;

			return true;
		}

		private static Type[] m_MinorArtifactsMl = new Type[]
		{
			typeof( AegisOfGrace ), typeof( BladeDance ), typeof( Bonesmasher ),
			typeof( Boomstick ), typeof( FeyLeggings ), typeof( FleshRipper ),
			typeof( HelmOfSwiftness ), typeof( PadsOfTheCuSidhe ), typeof( QuiverOfRage ),
			typeof( QuiverOfElements ), typeof( RaedsGlory ), typeof( RighteousAnger ),
			typeof( RobeOfTheEclipse ), typeof( RobeOfTheEquinox ), typeof( SoulSeeker ),
			typeof( TalonBite ), typeof( WildfireBow ), typeof( Windsong ),
			// TODO: Brightsight lenses, Bloodwood spirit, Totem of the void
		};

		public static Type[] MinorArtifactsMl
		{
			get { return m_MinorArtifactsMl; }
		}

		private static bool EnableRummaging = true;

		private const double ChanceToRummage = 0.5; // 50%

		private const double MinutesToNextRummageMin = 1.0;
		private const double MinutesToNextRummageMax = 4.0;

		private const double MinutesToNextChanceMin = 0.25;
		private const double MinutesToNextChanceMax = 0.75;

		private DateTime m_NextRummageTime;

		public virtual bool CanBreath { get { return HasBreath && !Summoned; } }
		public virtual bool IsDispellable { get { return ( Summoned || DispelDifficulty > 0 ) && !IsAnimatedDead; } }

		public virtual bool CanChew { get { return false; } }

		#region Healing
		public virtual bool CanHeal { get { return false; } }
		public virtual bool CanHealOwner { get { return false; } }
		public virtual double HealScalar { get { return 1.0; } }

		public virtual int HealSound { get { return 0x57; } }
		public virtual int HealStartRange { get { return 2; } }
		public virtual int HealEndRange { get { return RangePerception; } }
		public virtual double HealTrigger { get { return 0.78; } }
		public virtual double HealDelay { get { return 6.5; } }
		public virtual double HealInterval { get { return 0.0; } }
		public virtual bool HealFully { get { return true; } }
		public virtual double HealOwnerTrigger { get { return 0.78; } }
		public virtual double HealOwnerDelay { get { return 6.5; } }
		public virtual double HealOwnerInterval { get { return 30.0; } }
		public virtual bool HealOwnerFully { get { return false; } }

		private DateTime m_NextHealTime = DateTime.UtcNow;
		private DateTime m_NextHealOwnerTime = DateTime.UtcNow;
		private Timer m_HealTimer = null;

		public bool IsHealing { get { return ( m_HealTimer != null ); } }

		public virtual void HealStart( Mobile patient )
		{
			bool onSelf = ( patient == this );

			//DoBeneficial( patient );

			RevealingAction();

			if ( !onSelf )
			{
				patient.RevealingAction();
				patient.SendLocalizedMessage( 1008078, false, Name ); //  : Attempting to heal you.
			}

			double seconds = ( onSelf ? HealDelay : HealOwnerDelay ) + ( patient.Alive ? 0.0 : 5.0 );

			m_HealTimer = Timer.DelayCall( TimeSpan.FromSeconds( seconds ), new TimerStateCallback( Heal_Callback ), patient );
		}

		private void Heal_Callback( object state )
		{
			if ( state is Mobile )
				Heal( (Mobile)state );
		}

		public virtual void Heal( Mobile patient )
		{
			if ( !Alive || this.Map == Map.Internal || !CanBeBeneficial( patient, true, true ) || patient.Map != this.Map || !InRange( patient, HealEndRange ) )
			{
				StopHeal();
				return;
			}

			bool onSelf = ( patient == this );

			if ( !patient.Alive )
			{
			}
			else if ( patient.Poisoned )
			{
				int poisonLevel = patient.Poison.Level;

				double healing = Skills.Healing.Value;
				double anatomy = Skills.Anatomy.Value;
				double chance = ( healing - 30.0 ) / 50.0 - poisonLevel * 0.1;

				if ( ( healing >= 60.0 && anatomy >= 60.0 ) && chance > Utility.RandomDouble() )
				{
					if ( patient.CurePoison( this ) )
					{
						patient.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.

						CheckSkill( SkillName.Healing, 0.0, 60.0 + poisonLevel * 10.0 ); // TODO: Verify formula
						CheckSkill( SkillName.Anatomy, 0.0, 100.0 );
					}
				}
			}
			else if ( BleedAttack.IsBleeding( patient ) )
			{
				patient.SendLocalizedMessage( 1060167 ); // The bleeding wounds have healed, you are no longer bleeding!
				BleedAttack.EndBleed( patient, false );
			}
			else
			{
				double healing = Skills.Healing.Value;
				double anatomy = Skills.Anatomy.Value;
				double chance = ( healing + 10.0 ) / 100.0;

				if ( chance > Utility.RandomDouble() )
				{
					double min, max;

					min = ( anatomy / 10.0 ) + ( healing / 6.0 ) + 4.0;
					max = ( anatomy / 8.0 ) + ( healing / 3.0 ) + 4.0;

					if ( onSelf )
						max += 10;

					double toHeal = min + ( Utility.RandomDouble() * ( max - min ) );

					toHeal *= HealScalar;

					patient.Heal( (int)toHeal );

					CheckSkill( SkillName.Healing, 0.0, 90.0 );
					CheckSkill( SkillName.Anatomy, 0.0, 100.0 );
				}
			}

			HealEffect( patient );

			StopHeal();

			if ( ( onSelf && HealFully && Hits >= HealTrigger * HitsMax && Hits < HitsMax ) || ( !onSelf && HealOwnerFully && patient.Hits >= HealOwnerTrigger * patient.HitsMax && patient.Hits < patient.HitsMax ) )
				HealStart( patient );
		}

		public virtual void StopHeal()
		{
			if ( m_HealTimer != null )
				m_HealTimer.Stop();

			m_HealTimer = null;
		}

		public virtual void HealEffect( Mobile patient )
		{
			patient.PlaySound( HealSound );
		}

		#endregion

		public void NameColor()
		{
			if ( this is HenchmanMonster || this is HenchmanFighter || this is HenchmanWizard || this is HenchmanArcher )
			{
				NameHue = 1154;
			}
			else if ( this.Map == Map.Ilshenar && this.X <= 1007 && this.Y <= 1280)
			{
				if (this is DarkMoorGreeter)
					NameHue = 44;
				
				else if (this.Karma < 0 && this.Kills < 1)
					NameHue = 44;
				else if (this.Karma > 25 || this is BaseChild)
					NameHue = 100;
				else if (this is Citizens)
					NameHue = -1;
				else 
					NameHue = -1;
			}
			else if ( this.Map == Map.Tokuno && ( this is WhiteTiger || this is PolarBear || this is WhiteWolf || this is SnowLeopard || this is Mammoth || this is Boar || this is Panda || this is Bull || this is Gorilla || this is Panther || this is GreyWolf ) )
			{
				NameHue = -1;
			}
			else if ( this.Map == Map.Felucca && this.Z > 10 && this.X >= 1975 && this.Y >= 2201 && this.X <= 2032 && this.Y <= 2247 ) // ZOO
			{
				NameHue = -1;
			}
			else if ( this.Kills > 0 )
			{
				NameHue = 0x22;
			}
				
			else if ( m_FightMode == FightMode.Closest && !( this is BasePerson || this is Townsperson || this is BaseBlue || this is BaseVendor || this is PlayerVendor || this is PlayerBarkeeper ) )
			{
				NameHue = 44;
			}
			else if ( m_FightMode == FightMode.Evil )
			{
				NameHue = 0x92E;
			}

			if ( GetMaster() is PlayerMobile && NameHue > 0 && !( this is HenchmanMonster || this is HenchmanFighter || this is HenchmanWizard || this is HenchmanArcher ) ){ NameHue = -1; }
		}

		public virtual void OnThink()
		{
			NameColor();
			Server.Misc.MaterialInfo.IsNoHairHat( 0, this );				

			if (this.Map == null)
				return;

			EndBarding();

			//to prevent strange issue where pets are going grey for no reason i can see
			if ( this.Controlled && this.ControlMaster != null && !((BaseCreature)this).ControlMaster.Criminal && this.Criminal && !this.GoFeral )
			{
				Mobile masta = this.ControlMaster;
				if ( masta.InRange( this.Location, RangePerception ) && CanSee( masta ) && InLOS( masta ) )
				{
					this.Criminal = false;
				}
			}

			if (this.Loyalty < 0) //went over or below for some reason
				this.Loyalty = 0;
			if (this.Loyalty > 100)
				this.Loyalty = 100;

			//pets now feed themselves when walking over their food type
			if ( this.Controlled && this.ControlMaster != null && this.Loyalty <100 && !this.IsStabled)
			{
				foreach ( Item item in GetItemsInRange( 1 ) )
				{
	    			if ( item.Movable && CheckFeed( this.ControlMaster, item ) )
	    			{
						this.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, string.Format ( "*Eats something on the ground*"  ) );
	    			}
				}
			}

			//feral mobs
			if ( !IsDeadPet && m_goferal && this.Loyalty < 35 && this.Controlled && this.ControlMaster != null && this.ControlMaster is PlayerMobile && !this.IsStabled && !this.IsHitchStabled  )
			{
				Mobile master = this.ControlMaster;
				m_ControlOrder = OrderType.None; 

				if (this.Combatant != master || this.Combatant == null)
				{

					Mobile crazytarget = null;

					foreach ( Mobile friend in GetMobilesInRange( RangePerception ) )
					{
						if ( !friend.Blessed && friend.InRange( this.Location, RangePerception ) && CanSee( friend ) && InLOS( friend ) && CanBeHarmful( friend, false ))
						{
							if (friend is BaseCreature && ((BaseCreature)friend).Controlled && ((BaseCreature)friend).ControlMaster != null && ((BaseCreature)friend).ControlMaster is PlayerMobile )
							{ // another pet nearby
								BaseCreature bc = (BaseCreature)friend;
								if ( !bc.GoFeral && ((double)bc.Loyalty / 100) < Utility.RandomDouble()  ) // feral is contageous
								{
									bc.Loyalty = 30;
									bc.GoFeral = true;
									bc.PublicOverheadMessage( MessageType.Emote, 123, false, "*This animal has gone feral!*" );
									continue;
								}
							}
							else if (friend is BaseCreature && ((BaseCreature)friend).GoFeral)
								continue;
							else if ( crazytarget == null )
								crazytarget = friend;
						}
					}

					if ( (master.InRange( this.Location, 13 ) && CanSee( master ) && InLOS( master ) ) )
					{	
						if (!this.Warmode)	
							this.Warmode = true;
						if ( !(this.Combatant == master) )
							this.Combatant = master;
					}
					else if (this.Combatant == null && crazytarget != null)
					{
						if (!this.Warmode)
							this.Warmode = true;

						this.Combatant = crazytarget;
					}
				}

			}
			else if ( !IsDeadPet && m_goferal && this.Loyalty > 35)
			{
				m_goferal = false;
				if (this.Combatant != null )
					this.Combatant = null;

				this.Warmode = false;
				this.FocusMob = null;
			}
			else if (IsDeadPet && m_goferal ) 
			{
				m_goferal = false;
				if (this.Combatant != null )
					this.Combatant = null;
				this.FocusMob = null;
			}

			//try to fix bug where pets go grey and attack owner
			if (this.Combatant != null && this.Controlled && this.ControlMaster == Combatant && !this.BardProvoked && (ControlOrder == OrderType.Guard || ControlOrder == OrderType.Follow || ControlOrder == OrderType.Come ) )
			{
				this.Warmode = false;
				this.Combatant = null;
			}

			if ( ( this.Region.IsPartOf( typeof( DungeonRegion ) ) || this.Region.IsPartOf( typeof( BardDungeonRegion ) ) ) && !this.Controlled )
			{

				this.Nearby = false;
				
				foreach ( NetState state in NetState.Instances )
				{
					if (state != null)
					{
						Mobile m = state.Mobile;

						if ( m is PlayerMobile && m.Map != null )
						{
							if ( (m.InRange( this.Location, 13 ) && CanSee( m ) && InLOS( m ) ) || this.Combatant == m || (m.Hidden && m.InRange( this.Location, 8 ) && InLOS( m )) )
							{
							this.Nearby = true;
							}

							else if( m.InRange( this.Location, 10 ) && Utility.RandomDouble() <= ( m.Skills[SkillName.Tracking].Value / 600 ))
							{
								this.Nearby = true;
							}	
						}
					}
						
				}
				
				if (this.Nearby)
					m_IsSleeping = false;
				else 
					m_IsSleeping = true;
				
				
				if (m_IsSleeping && !this.Hidden && this.Combatant == null) // Final, mobs don't appear immediately in dungeons.
				{
					this.Hidden = true;
					this.Frozen = true;
				}
				else if (!m_IsSleeping && this.Hidden)
				{
					this.Hidden = false;
					this.Frozen = false;
				}
				

			}			

			Mobile tamer = GetMaster(); // FOR INVULNERABLE POTIONS AND SEANCE SPELLS
			if ( tamer is PlayerMobile )
			{
				if ( tamer.Blessed )
				{
					Blessed = true;
				}
				else if ( !AlwaysInvulnerable( this ) && Blessed )
				{
					Blessed = false;
				}
			}
			if ( !Controlled && !AlwaysInvulnerable( this ) && Blessed )
			{
				Blessed = false;
			}

			if ( this is HenchmanMonster || this is HenchmanFighter || this is HenchmanWizard || this is HenchmanArcher || MyServerSettings.FastFriends( this ) )
			{
				Mobile leader = GetMaster();
				if ( leader != null )
				{
					if ( Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( leader.Location, leader.Map ) ) && Server.Mobiles.AnimalTrainer.AllowMagicSpeed( leader, Region.Find( leader.Location, leader.Map ) ) )
					{
						Server.Misc.HenchmanFunctions.DismountHenchman( leader );
					}
					else if ( Region.Find( this.Location, this.Map ) is HouseRegion )
					{
						Server.Misc.HenchmanFunctions.DismountHenchman( leader );
					}
					else if ( Server.Mobiles.AnimalTrainer.IsBeingFast( leader ) && this.ActiveSpeed >= 0.2 )
					{
						Server.Misc.HenchmanFunctions.MountHenchman( leader );
					}
					else if ( !Server.Mobiles.AnimalTrainer.IsBeingFast( leader ) && this.ActiveSpeed <= 0.1 )
					{
						Server.Misc.HenchmanFunctions.DismountHenchman( leader );
					}
				}
				else
				{
					Server.Misc.HenchmanFunctions.ForceSlow( this );
				}
			}

			if ( EnableRummaging && CanRummageCorpses && !Summoned && !Controlled && DateTime.UtcNow >= m_NextRummageTime )
			{
				double min, max;

				if ( ChanceToRummage > Utility.RandomDouble() && Rummage() )
				{
					min = MinutesToNextRummageMin;
					max = MinutesToNextRummageMax;
				}
				else
				{
					min = MinutesToNextChanceMin;
					max = MinutesToNextChanceMax;
				}

				double delay = min + (Utility.RandomDouble() * (max - min));
				m_NextRummageTime = DateTime.UtcNow + TimeSpan.FromMinutes( delay );
			}

			if ( CanBreath && DateTime.UtcNow >= m_NextBreathTime ) // tested: controlled dragons do breath fire, what about summoned skeletal dragons?
			{
				Mobile target = this.Combatant;
				Mobile owner = this.ControlMaster;
				
				if( target != null && target.Alive && !target.IsDeadBondedPet && CanBeHarmful( target ) && target.Map == this.Map && !IsDeadBondedPet && target.InRange( this, BreathRange ) && InLOS( target ) && !BardPacified )
				{

					if( ( DateTime.UtcNow - m_NextBreathTime ) < TimeSpan.FromSeconds( 30 ) ) 
					{
						bool chk = false;
						if (owner != null && target == owner )
							chk = true;
						else if (owner != null && target is BaseCreature && ((BaseCreature)target).Controlled && ((BaseCreature)target).ControlMaster == owner )
							chk = true;
						
						if (chk)
						{
							if ( Utility.RandomDouble() < ((double)this.m_Loyalty /100))
								BreathStart( target );
						}
						else
							BreathStart( target );
					}

					m_NextBreathTime = DateTime.UtcNow + TimeSpan.FromSeconds( BreathMinDelay + ( Utility.RandomDouble() * BreathMaxDelay ) );
				}
			}

			if ( ( ( CanHeal || CanHealOwner ) || (m_special == 7 )) && Alive && !IsHealing && !BardPacified )
			{
				Mobile owner = this.ControlMaster;

				if ( owner != null && ( CanHealOwner || (m_special == 7 )) && DateTime.UtcNow >= m_NextHealOwnerTime && CanBeBeneficial( owner, true, true ) && owner.Map == this.Map && InRange( owner, HealStartRange ) && InLOS( owner ) && owner.Hits < HealOwnerTrigger * owner.HitsMax )
				{
					HealStart( owner );
					if (Utility.RandomDouble() > 0.95 )
						m_Loyalty -= Utility.RandomMinMax(1, 2);

					m_NextHealOwnerTime = DateTime.UtcNow + TimeSpan.FromSeconds( HealOwnerInterval );
				}
				else if ( ( CanHeal  || (m_special == 7 )) && DateTime.UtcNow >= m_NextHealTime && CanBeBeneficial( this ) && ( Hits < HealTrigger * HitsMax || Poisoned ) )
				{
					HealStart( this );
					if (Utility.RandomDouble() > 0.85 )
						m_Loyalty -= Utility.RandomMinMax(1, 5);

					m_NextHealTime = DateTime.UtcNow + TimeSpan.FromSeconds( HealInterval );
				}
			}


			if ( (  this is TownHerald || ( this is BaseVendor && this.WhisperHue != 999 && !(this is PlayerVendor) && !(this is PlayerBarkeeper) ) ) // GUARDS/MERCHANTS SHOULD MOVE BACK TO THEIR POST
				&& 
				( Math.Abs( this.X-this.Home.X ) > 8 || Math.Abs( this.Y-this.Home.Y ) > 8 || Math.Abs( this.Z-this.Home.Z ) > 8 )
				&& 
				Combatant == null )
			{
				this.Location = this.Home;
				Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( this, this.Map, 0x201 );
			}
			else if ( WhisperHue == 999 && CanSwim && !(CanOnlyMoveOnSea( this )) && !CantWalk && Combatant == null && !Hidden && !Server.Mobiles.BasePirate.IsSailor( this ) ) // DIVE UNDER WATER AND WAIT FOR VICTIM
			{
				bool dive = true;

				foreach ( NetState state in NetState.Instances )
				{
					Mobile m = state.Mobile;

					if ( m is PlayerMobile && m.InRange( this.Location, 20 ) && m.Alive && m.Map == this.Map )
					{
						if ( m.AccessLevel == AccessLevel.Player ){ dive = false; }
					}
				}

				if ( dive )
				{
					Point3D loc = Server.Misc.Worlds.GetBoatWater( this.X, this.Y, this.Map, 8 );

					if ( loc.X == 0 && loc.Y == 0 && loc.Z == 0 )
					{
						this.Location = this.Home;
						Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						this.PlaySound( 0x1FE );
					}
					else
					{
						this.Location = loc;
						this.PlaySound( 0x026 );
						Effects.SendLocationEffect( this.Location, this.Map, 0x23B2, 16 );
					}

					this.Warmode = false;
					this.CantWalk = true;
					this.CanSwim = false;
					this.Hidden = true;
				}
			}
			else if ( WhisperHue == 666 ) // ENTER A DEMON GATE
			{
				if ( !CantWalk && Combatant == null && !Hidden )
				{
					bool escape = true;

					foreach ( NetState state in NetState.Instances )
					{
						Mobile m = state.Mobile;

						if ( m is PlayerMobile && m.InRange( this.Location, 20 ) && m.Alive && m.Map == this.Map )
						{
							if ( m.AccessLevel == AccessLevel.Player ){ escape = false; }
						}
					}

					if ( escape )
					{
						Server.Items.DemonGate.MakeDemonGate( this );
						this.Location = this.Home;
						this.Warmode = false;
						this.CantWalk = true;
						this.Hidden = true;
					}
				}
				else if ( Hidden && CantWalk )
				{
					bool appear = false;

					foreach ( NetState state in NetState.Instances )
					{
						Mobile m = state.Mobile;

						if ( m is PlayerMobile && m.InRange( this.Location, 8 ) && m.Alive && m.Map == this.Map )
						{
							if ( m.AccessLevel == AccessLevel.Player ){ appear = true; }
						}
					}

					if ( appear )
					{
						this.Home = this.Location;
						Server.Items.DemonGate.MakeDemonGate( this );
						this.CantWalk = false;
						this.Hidden = false;
					}
				}
			}

		}

		public virtual bool Rummage()
		{
			Corpse toRummage = null;

			foreach ( Item item in this.GetItemsInRange( 2 ) )
			{
				if ( item is Corpse && item.Items.Count > 0 )
				{
					toRummage = (Corpse)item;
					break;
				}
			}

			if ( toRummage == null )
				return false;

			Container pack = this.Backpack;

			if ( pack == null )
				return false;

			List<Item> items = toRummage.Items;

			bool rejected;
			LRReason reason;

			for ( int i = 0; i < items.Count; ++i )
			{
				Item item = items[Utility.Random( items.Count )];

				Lift( item, item.Amount, out rejected, out reason );

				if ( !rejected && Drop( this, new Point3D( -1, -1, 0 ) ) )  
				{
					// *rummages through a corpse and takes an item*
					PublicOverheadMessage( MessageType.Emote, 0x3B2, 1008086 );
					this.PackItem( item); 
					return true;
				}
			}

			return false;
		}

		public void Pacify( Mobile master, DateTime endtime )
		{
			BardPacified = true;
			BardEndTime = endtime;
		}

		public override Mobile GetDamageMaster( Mobile damagee )
		{
			if ( m_bBardProvoked && damagee == m_bBardTarget )
				return m_bBardMaster;
			else if ( m_bControlled && m_ControlMaster != null )
				return m_ControlMaster;
			else if ( m_bSummoned && m_SummonMaster != null )
				return m_SummonMaster;

			return base.GetDamageMaster( damagee );
		}

		public void EndBarding( )
		{

			if ( (BardPacified || BardProvoked) && DateTime.UtcNow > this.BardEndTime )
			{
				if (BardPacified)
					BardPacified = false;
				else if (BardProvoked)
				{
					BardProvoked = false;
					Combatant = null;
					Warmode = false;
				}
				BardMaster = null;
				BardTarget = null;
				Criminal = false;
			}
			
			

		}

		public void Provoke( Mobile master, Mobile target, bool bSuccess )
		{
			BardProvoked = true;

			if ( !Core.ML )
			{
				this.PublicOverheadMessage( MessageType.Emote, EmoteHue, false, "*looks furious*" );
			}

			if ( bSuccess )
			{
				PlaySound( GetIdleSound() );

				BardMaster = master;
				BardTarget = target;
				Combatant = target;
				BardEndTime = DateTime.UtcNow + TimeSpan.FromSeconds( 30.0 );

				if ( target is BaseCreature )
				{
					BaseCreature t = (BaseCreature)target;

					if ( t.Unprovokable || (t.IsParagon && BaseInstrument.GetBaseDifficulty( t ) >= 160.0) )
						return;

					t.BardProvoked = true;

					t.BardMaster = master;
					t.BardTarget = this;
					t.Combatant = this;
					t.BardEndTime = DateTime.UtcNow + TimeSpan.FromSeconds( 30.0 );
				}
			}
			else
			{
				PlaySound( GetAngerSound() );

				BardMaster = master;
				BardTarget = target;
			}
		}

		public bool FindMyName( string str, bool bWithAll )
		{
			int i, j;

			string name = this.Name;

			if( name == null || str.Length < name.Length )
				return false;

			string[] wordsString = str.Split(' ');
			string[] wordsName = name.Split(' ');

			for ( j=0 ; j < wordsName.Length; j++ )
			{
				string wordName = wordsName[j];

				bool bFound = false;
				for ( i=0 ; i < wordsString.Length; i++ )
				{
					string word = wordsString[i];

					if ( Insensitive.Equals( word, wordName ) )
						bFound = true;

					if ( bWithAll && Insensitive.Equals( word, "all" ) )
						return true;
				}

				if ( !bFound )
					return false;
			}

			return true;
		}

		public static void TeleportPets( Mobile master, Point3D loc, Map map )
		{
			TeleportPets( master, loc, map, false );
		}

		public static void TeleportPets( Mobile master, Point3D loc, Map map, bool onlyBonded )
		{
			List<Mobile> move = new List<Mobile>();

			foreach ( Mobile m in master.GetMobilesInRange( 10 ) )
			{
				if ( m is BaseCreature )
				{
					BaseCreature pet = (BaseCreature)m;

					if ( pet.Controlled && pet.ControlMaster == master && !(m is BaseChild) )
					{
						if ( !onlyBonded || pet.IsBonded )
						{
							if ( pet.ControlOrder == OrderType.Guard || pet.ControlOrder == OrderType.Follow || pet.ControlOrder == OrderType.Come )
								move.Add( pet );
						}
						else if ( pet is HenchmanFamiliar || pet is AerialServant || pet is PackBeast || pet is Robot || pet is GolemPorter || pet is GolemFighter || pet is FrankenPorter || pet is FrankenFighter ){ move.Add( pet ); }
					}
				}
			}

			foreach ( Mobile m in move )
				m.MoveToWorld( loc, map );
		}

		public virtual void ResurrectPet()
		{
			if ( !IsDeadPet )
				return;

			OnBeforeResurrect();

			Poison = null;

			Warmode = false;

			Hits = 10;
			Stam = 10;
			Mana = 0;

			if (this.Loyalty > 10)
				this.Loyalty -= Utility.RandomMinMax(1, 5); // lose some loyalty on death

			ProcessDeltaQueue();

			IsDeadPet = false;

			Effects.SendPacket( Location, Map, new BondedStatus( 0, this.Serial, 0 ) );

			this.SendIncomingPacket();
			this.SendIncomingPacket();

			OnAfterResurrect();

			Mobile owner = this.ControlMaster;

			if ( owner == null || owner.Deleted || owner.Map != this.Map || !owner.InRange( this, 12 ) || !this.CanSee( owner ) || !this.InLOS( owner ) )
			{
				if ( this.OwnerAbandonTime == DateTime.MinValue )
					this.OwnerAbandonTime = DateTime.UtcNow;
			}
			else
			{
				this.OwnerAbandonTime = DateTime.MinValue;
			}

			CheckStatTimers();
		}

		public override bool CanBeDamaged()
		{
			if ( IsDeadPet )
				return false;

			return base.CanBeDamaged();
		}

		public virtual bool PlayerRangeSensitive{ get{ return (this.CurrentWayPoint == null); } }	//If they are following a waypoint, they'll continue to follow it even if players aren't around

		public override void OnSectorDeactivate()
		{
			if ( PlayerRangeSensitive && m_AI != null )
				m_AI.Deactivate();

			base.OnSectorDeactivate();
		}

		public override void OnSectorActivate()
		{
			if ( PlayerRangeSensitive && m_AI != null )
				m_AI.Activate();

			base.OnSectorActivate();
		}

		private bool m_RemoveIfUntamed;

		// used for deleting untamed creatures [in houses]
		private int m_RemoveStep;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RemoveIfUntamed{ get{ return m_RemoveIfUntamed; } set{ m_RemoveIfUntamed = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int RemoveStep { get { return m_RemoveStep; } set { m_RemoveStep = value; } }
	}

	public class LoyaltyTimer : Timer
	{
		private static TimeSpan InternalDelay = TimeSpan.FromMinutes( 5.0 );

		public static void Initialize()
		{
			new LoyaltyTimer().Start();
		}

		public LoyaltyTimer() : base( InternalDelay, InternalDelay )
		{
			m_NextHourlyCheck = DateTime.UtcNow + TimeSpan.FromHours( 1.0 );
			Priority = TimerPriority.FiveSeconds;
		}

		private DateTime m_NextHourlyCheck;

		protected override void OnTick()
		{
			if ( DateTime.UtcNow >= m_NextHourlyCheck )
				m_NextHourlyCheck = DateTime.UtcNow + TimeSpan.FromHours( 1.0 );
			else
				return;

			List<BaseCreature> toRelease = new List<BaseCreature>();

			// added array for wild creatures in house regions to be removed
			List<BaseCreature> toRemove = new List<BaseCreature>();

			foreach ( Mobile m in World.Mobiles.Values )
			{
				if ( m is BaseMount && ((BaseMount)m).Rider != null )
				{
					((BaseCreature)m).OwnerAbandonTime = DateTime.MinValue;
					continue;
				}

				if ( m is BaseCreature )
				{
					BaseCreature c = (BaseCreature)m;

					if ( c.IsDeadPet )
					{
						Mobile owner = c.ControlMaster;

						if (  !c.IsHitchStabled &&  owner == null || owner.Deleted || owner.Map != c.Map || !owner.InRange( c, 12 ) || !c.CanSee( owner ) || !c.InLOS( owner ) )
						{
							if ( c.OwnerAbandonTime == DateTime.MinValue )
								c.OwnerAbandonTime = DateTime.UtcNow;
							else if ( (c.OwnerAbandonTime + c.BondingAbandonDelay) <= DateTime.UtcNow )
								toRemove.Add( c );
						}
						else
						{
							c.OwnerAbandonTime = DateTime.MinValue;
						}
					}
					else if ( c.Controlled && c.Commandable && !c.Summoned && !c.IsDeadBondedPet && !c.IsStabled) 
                    {
						c.OwnerAbandonTime = DateTime.MinValue;

						if ( c.Map != Map.Internal )
						{
							if (c.IsHitchStabled && Utility.RandomDouble() > 0.99)
								c.Loyalty -= 1;
							else if (Utility.RandomDouble() > 0.90)
								c.Loyalty -= Utility.RandomMinMax(1, 2);

							if( c.Loyalty < (BaseCreature.MaxLoyalty / 10) )
							{
								c.Say( 1043270, c.Name ); // * ~1_NAME~ looks around desperately *
								c.PlaySound( c.GetIdleSound() );
							}

							if ( c.Loyalty <= 0 && !c.IsBonded)
							{
								toRelease.Add( c );
							}
							
							c.InvalidateProperties();
						}
					}

					// added lines to check if a wild creature in a house region has to be removed or not
					// WIZARD CHANGED TO CLEAN THE WORLD OF TAMED CREATURES
					//if ( (!c.Controlled && ( c.Region.IsPartOf( typeof( HouseRegion ) ) && c.CanBeDamaged()) || ( c.RemoveIfUntamed && c.Spawner == null )) )
					if ( !c.Controlled && c.LastOwner != null && !c.IsStabled && !c.IsHitchStabled && c.CanBeDamaged() && c.Map != Map.Internal && !(c.Region is HouseRegion) )
					{
						c.RemoveStep++;

						if ( c.RemoveStep >= 1 )
						{
							toRemove.Add( c );
							
						}
					}
					else
					{
						c.RemoveStep = 0;
					}
				}
			}

			foreach ( BaseCreature c in toRelease )
			{

							if (c.IsHitchStabled)
							{
								c.IsHitchStabled = false;
								c.Blessed = false;
								c.CantWalk = false;

								foreach (Item item in World.Items.Values)
								{
									if (item is HitchingPost)
									{
										if (((HitchingPost)item).StabledTable != null && ((HitchingPost)item).StabledTable.Count > 0)
										{
											Mobile ownerpost = null;
											foreach( DictionaryEntry de in ((HitchingPost)item).StabledTable )
											{
												if (de.Value is BaseCreature)								
												{

													if ( (BaseCreature)de.Value == c)
													{
														ownerpost = (Mobile)de.Key;

													}
												}
											}
											if (ownerpost != null)
												((HitchingPost)item).removeowner(ownerpost); 
										}
									}
								}
							}
							

				c.Say( 1043255, c.Name ); // ~1_NAME~ appears to have decided that is better off without a master!
				c.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy
				c.IsBonded = false;
				c.BondingBegin = DateTime.MinValue;
				c.OwnerAbandonTime = DateTime.MinValue;
				c.ControlTarget = null;
				
				//c.ControlOrder = OrderType.Release;
				c.AIObject.DoOrderRelease(); // this will prevent no release of creatures left alone with AI disabled (and consequent bug of Followers)
				//c.DropBackpack();
			}

			// added code to handle removing of wild creatures in house regions
			foreach ( BaseCreature c in toRemove )
			{
				c.Delete();
			}
		}
	}
}
