/*	
	MageAI redone in 2022-2023 by FinalTwist
	lots of work went into this, feel free to use as you wish.
	my humble contribution to the amazing UO community.
	BTC address: 35hKrHLBnQTVRTtVbvDuPuwQzgurqgrRCc
*/
using System;
using System.Collections;
using System.Collections.Generic;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Necromancy;
using Server.Spells.Second;
using Server.Spells.Seventh;
using Server.Spells.Eighth;
using Server.Spells.Sixth;
using Server.Spells.Third;
using Server.Targeting;
using Server.Items;

namespace Server.Mobiles
{
	public class MageAI : BaseAI
	{
		private DateTime m_NextCastTime;
		private DateTime m_NextHealTime;
		private DateTime m_NextAnimateTime = DateTime.UtcNow;
		private double m_AnimateDelay = 5.0;
		private double m_AnimateFinish = 2.0;

		private Mobile m_Animated;
		public Mobile Animated
		{
			get { return m_Animated; }
			set { m_Animated = value; }
		}

		public MageAI( BaseCreature m )
			: base( m )
		{
		}

		public override bool Think()
		{
			if( m_Mobile.Deleted )
				return false;

			if( ProcessTarget() )
				return true;
			else
				return base.Think();
		}
		
		public virtual bool DoesMelee
		{
			get 
			{ 
				Mobile combatant = m_Mobile.Combatant;
				double relevantskill = m_Mobile.Skills[ SkillName.Wrestling ].Value;

				if (m_Mobile.Body.IsHuman)// may have weapons
				{
					Item onehanded = m_Mobile.FindItemOnLayer( Layer.OneHanded );
					Item twohanded = m_Mobile.FindItemOnLayer( Layer.TwoHanded );			
					Item firstvalid = m_Mobile.FindItemOnLayer( Layer.FirstValid );
					BaseWeapon bw = null;

					if (onehanded is BaseWeapon)
						bw = (BaseWeapon)onehanded;
					else if (twohanded is BaseWeapon)
						bw = (BaseWeapon)twohanded;
					else if (firstvalid is BaseWeapon)
						bw = (BaseWeapon)firstvalid;

					if (bw != null)
						relevantskill = m_Mobile.Skills[ bw.Skill ].Value; 
				}

				if (m_Mobile is BaseCreature && ((BaseCreature)m_Mobile).Controlled )
				{	
					if (m_Mobile.Str > m_Mobile.Int)
						return true;
				}
				else if ( Insensitive.Contains( m_Mobile.Name, "unicorn" ) || Insensitive.Contains( m_Mobile.Name, "steed" ) || Insensitive.Contains( m_Mobile.Name, "drake" ) || Insensitive.Contains( m_Mobile.Name, "dragon" ) || Insensitive.Contains( m_Mobile.Name, "balron" ) || Insensitive.Contains( m_Mobile.Name, "primeval" ) || Insensitive.Contains( m_Mobile.Name, "beetle" ) )
					return true;
				else if (m_Mobile.Combatant != null)
				{
					if (SmartAI && m_Mobile is BaseCreature && m_Mobile.Combatant is BaseCreature )
						return (  ((BaseCreature)m_Mobile).DamageMax > ((BaseCreature)combatant).DamageMax && m_Mobile.HitsMax > m_Mobile.Combatant.HitsMax && relevantskill > 50 );
					else if (SmartAI && m_Mobile is BaseCreature && m_Mobile.Combatant is PlayerMobile)
					{

						Item onehanded = m_Mobile.Combatant.FindItemOnLayer( Layer.OneHanded );
						Item twohanded = m_Mobile.Combatant.FindItemOnLayer( Layer.TwoHanded );			
						Item firstvalid = m_Mobile.Combatant.FindItemOnLayer( Layer.FirstValid );
						BaseWeapon bw = null;

						if (onehanded is BaseWeapon)
							bw = (BaseWeapon)onehanded;
						else if (twohanded is BaseWeapon)
							bw = (BaseWeapon)twohanded;
						else if (firstvalid is BaseWeapon)
							bw = (BaseWeapon)firstvalid;

						if (bw != null)
							return ( ((BaseCreature)m_Mobile).DamageMax > (bw.GetDamageBonus() + bw.Attributes.WeaponDamage) && m_Mobile.HitsMax > m_Mobile.Combatant.HitsMax && relevantskill > 50 );
						else //player is wrestling?  Melee it!
							return ( m_Mobile.Hits > m_Mobile.Combatant.Hits && relevantskill > 50 );
					}
						
					else 
						return ( m_Mobile.Str > (int)((double)m_Mobile.Combatant.Str * 1.20) && relevantskill > 50 );
				}
				return false; 
			}
		}

		protected const double HealChance = 0.10; // 10% chance to heal at gm magery
		protected const double TeleportChance = 0.05; // 5% chance to teleport at gm magery
		protected const double DispelChance = 0.75; // 75% chance to dispel at gm magery

		public virtual double ScaleBySkill(double v, SkillName skill)
		{
		    return v * m_Mobile.Skills[skill].Value / 100;
		}

		public virtual double ScaleByMagery( double v )
		{
			return m_Mobile.Skills[ SkillName.Magery ].Value * v * 0.01;
		}

		public virtual double ScaleByNecromancy( double v )
		{
			return m_Mobile.Skills[ SkillName.Necromancy ].Value * v * 0.1;
		}

		private readonly int[] m_ManaTable = {4, 6, 9, 11, 14, 20, 40, 50};

		public virtual bool CheckCanCastMagery(int circle)
		{
			if (circle < 1)
				circle = 1;
			if (circle > 8)
				circle = 8;

			var skill = (100.0 / 7.0) * circle;

			return m_Mobile.Mana >= m_ManaTable[circle - 1] &&
				   (Core.SA || m_Mobile.Skills[SkillName.Magery].Value >= skill - 20);
		}

		public bool CanDispel( Mobile m )
		{
			return ( m is BaseCreature && ( (BaseCreature)m ).Summoned && m_Mobile.CanBeHarmful( m, false ) && !( (BaseCreature)m ).IsAnimatedDead );
		}

        private TimeSpan CastDelay()
        // Sets random Timespan as a casting delay, depending on my magery skill:
        {
            double delay;
            if (m_Mobile.Skills[SkillName.Magery].Value > 115)
                delay = 1; // delay = 1...2 seconds
			else if (m_Mobile.Skills[SkillName.Magery].Value > 99)
                delay = 2; // delay = 1...2 seconds
			else if (m_Mobile.Skills[SkillName.Magery].Value > 90)
                delay = 3; // delay = 1...2 seconds
            if (m_Mobile.Skills[SkillName.Magery].Value > 75)
                delay = Utility.RandomMinMax(3, 4); // delay = 1...2 seconds
            else delay = Utility.RandomMinMax(4, 5); // delay = 2...4 seconds
            return TimeSpan.FromSeconds(delay);
        }


		public override bool DoActionWander()
		{
			if( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if( m_Mobile.Debug )
					m_Mobile.DebugSay( "I am going to attack {0}", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
				m_NextCastTime = DateTime.UtcNow;
			}
			else if( SmartAI && m_Mobile.Mana < m_Mobile.ManaMax )
			{
				m_Mobile.DebugSay( "I am going to meditate" );

				m_Mobile.UseSkill( SkillName.Meditation );
			}
			else
			{
				m_Mobile.DebugSay( "I am wandering" );

				m_Mobile.Warmode = false;

				base.DoActionWander();

				if( ( Utility.RandomDouble() < .05 ) )
				{
					Spell spell = CheckCastHealingSpell();

					if( spell != null )
					{
						spell.Cast();
						if( DateTime.UtcNow > m_NextAnimateTime && m_Mobile.HasBreath == false && !m_Mobile.Mounted )
						{
							m_Mobile.PlaySound( m_Mobile.GetAngerSound());
							m_Mobile.Animate( 12, 5, 1, true, false, 0 );
							m_NextAnimateTime = DateTime.UtcNow + TimeSpan.FromSeconds( m_AnimateDelay );
							NextMove = DateTime.UtcNow + TimeSpan.FromSeconds( m_AnimateFinish );
						}
					}
				}
			}

			return true;
		}

        protected virtual Spell CheckCastHealingSpell()
		{
			// If I'm poisoned, always attempt to cure.
			if( m_Mobile.Poisoned )
				return new CureSpell( m_Mobile, null );

			// Summoned creatures never heal themselves.
			if( m_Mobile.Summoned )
				return null;

			if( m_Mobile.Controlled && !(m_Mobile is HenchmanMonster) && !(m_Mobile is HenchmanArcher) && !(m_Mobile is HenchmanWizard) && !(m_Mobile is HenchmanFighter) ) // WIZARD ADDED FOR HENCHMAN
			{
				if( DateTime.UtcNow < m_NextHealTime )
					return null;
			}

			if( !SmartAI )
			{
				if (m_Mobile is BaseCreature && ((BaseCreature)m_Mobile).IsNecromancer && CastingNecro() )
				{
					if( ScaleByNecromancy( HealChance ) < Utility.RandomDouble() )
						return null;
				}
				else if( ScaleByMagery( HealChance ) < Utility.RandomDouble() )
					return null;
			}
			else
			{
				if( Utility.Random( 0, 4 + ( m_Mobile.Hits == 0 ? m_Mobile.HitsMax : ( m_Mobile.HitsMax / m_Mobile.Hits ) ) ) < 3 )
					return null;
			}

			if (!m_Mobile.Summoned && m_Mobile is BaseCreature && ((BaseCreature)m_Mobile).IsNecromancer && CastingNecro())
			{
				if ( m_Mobile.Hits < m_Mobile.HitsMax )
				{
					m_Mobile.UseSkill( SkillName.SpiritSpeak );
					m_NextHealTime = DateTime.UtcNow + TimeSpan.FromSeconds( 5 );
					return null;
				}
			}

			Spell spell = null;

			// Chance to skip healing depends on hit points:
            if (m_Mobile.Hits >= 100)
            {
                    int chance = (int)(Math.Sqrt(m_Mobile.Hits) / Math.Sqrt(m_Mobile.HitsMax / 100) * 100);
                    if (Utility.Random(1000 / chance) < 10)
                        return null;
            }

			if (SmartAI)
			{
				// If my hits are <10%, 30% chance to cast Invisibility:
                if ((m_Mobile.Hits < m_Mobile.HitsMax / 10) && (Utility.Random(3) == 0))
                {
                    spell = new InvisibilitySpell(m_Mobile, null);
                    return spell;
                }
				if (m_Mobile.Mana < 20) //can't heal if no mana
					m_Mobile.UseSkill( SkillName.Meditation );
			}

			if( m_Mobile.Hits < ( m_Mobile.HitsMax - 50 ) )
			{
				spell = new GreaterHealSpell( m_Mobile, null );

				if( spell == null )
					spell = new HealSpell( m_Mobile, null );
			}
			else if( m_Mobile.Hits < ( m_Mobile.HitsMax - 10 ) )
				spell = new HealSpell( m_Mobile, null );

			double delay;

			if( m_Mobile.Int >= 500 )
				delay = Utility.RandomMinMax( 7, 10 );
			else
				delay = Math.Sqrt( 600 - m_Mobile.Int );

			m_NextHealTime = DateTime.UtcNow + TimeSpan.FromSeconds( delay );

			return spell;
		}

		public virtual bool CanCastNecro()
		{
			return ( m_Mobile is BaseCreature  && ( (BaseCreature)m_Mobile ).IsNecromancer );
		}

		public virtual bool CastingNecro()
		{

			if (!CanCastNecro() )
				return false;

			double necrobias = 0; //10 is max

			necrobias += 0.25 * (m_Mobile.Skills[SkillName.Necromancy].Value / 120);
			necrobias += 0.25 * (m_Mobile.Skills[SkillName.SpiritSpeak].Value / 120);

			if (m_Mobile.Skills[SkillName.Magery].Value < m_Mobile.Skills[SkillName.Necromancy].Value)
				necrobias += 0.1;
			
			if (m_Mobile.Skills[SkillName.EvalInt].Value < m_Mobile.Skills[SkillName.SpiritSpeak].Value)
				necrobias += 0.1;

			if (m_Mobile.Karma < -5000)
				necrobias += 0.1;

			if (m_Mobile.Karma > 0)
				necrobias = 0;

			return ( Utility.RandomDouble() < necrobias ) ? true : false;

		}

		public virtual Spell GetRandomDamage()
		{
			return GetRandomDamage( 0, false );
		}

		public virtual Spell GetRandomDamage(int distance, bool many)
		{
			return ( CastingNecro() ) ? GetRandomDamageNecroSpell(distance, many) : GetRandomDamageSpell(distance, many);
		}

		public virtual Spell GetRandomDamageNecroSpell(int distance, bool many)
		{
			int necro = (int) Math.Min( 5, ( m_Mobile.Skills[SkillName.Necromancy].Value - 50 ) / 10 );
			int mage = (int) Math.Min( 7, m_Mobile.Skills[ SkillName.Magery ].Value / 10 );

			if ( necro >= 0 && Utility.Random( 4 ) < 3 )
			{
				if ( Utility.RandomDouble() <= 0.60 && ( m_Animated == null || !m_Animated.Alive ) && m_Mobile.Skills[ SkillName.Necromancy ].Value > 40 )
					return new AnimateDeadSpell( m_Mobile, null );

				else if (distance < 4)
				{
					if (many && Utility.RandomBool())
						return new WitherSpell( m_Mobile, null );

					switch ( Utility.Random( necro ) )
					{
						case 0: return new PoisonStrikeSpell( m_Mobile, null );
						case 1: return new WitherSpell( m_Mobile, null );
						case 2: return new StrangleSpell( m_Mobile, null );
						default: return new WitherSpell( m_Mobile, null );
					}
				}
				else
				{
					if (Utility.RandomBool())
						return new StrangleSpell( m_Mobile, null );
					else
						return new PoisonStrikeSpell( m_Mobile, null );
				}

			}
			else
			{
				switch ( Utility.Random( mage ) )
				{
					case 0: return new MagicArrowSpell( m_Mobile, null );
					case 1: return new HarmSpell( m_Mobile, null );
					case 2: return new FireballSpell( m_Mobile, null );
					case 3: return new LightningSpell( m_Mobile, null );
					case 4: return new MindBlastSpell( m_Mobile, null );
					case 5: return new EnergyBoltSpell( m_Mobile, null );
					case 6: return new ExplosionSpell( m_Mobile, null );
					default: return new FlameStrikeSpell( m_Mobile, null );
				}
			}
		}

		public static int GetMaxCircle(Mobile from)
		{
			int maxCircle = 0;
			// Determine which circle of magery (1-8) I can cast, depending on my magery skill (0-120):
            if (from.Skills[SkillName.Magery].Value < 20)
                maxCircle = 2;
            else if (from.Skills[SkillName.Magery].Value < 35)
                maxCircle = 3;
            else if (from.Skills[SkillName.Magery].Value < 50)
                maxCircle = 4;
            else if (from.Skills[SkillName.Magery].Value < 69)
                maxCircle = 5;
            else if (from.Skills[SkillName.Magery].Value < 80)
                maxCircle = 6;
            else if (from.Skills[SkillName.Magery].Value < 92)
                maxCircle = 7;
            else if (from.Skills[SkillName.Magery].Value < 105)
				maxCircle = 8;
			else if (from.Skills[SkillName.Magery].Value < 110) //for some very special specials
				maxCircle = 9;
			
			return maxCircle;
		}

		public virtual Spell GetRandomDamageSpell(int distance, bool many)
		{
			int maxCircle = GetMaxCircle(m_Mobile);
			Spell damageSpell = null;
			
			if (SmartAI)
			{
				//let's have fun
				if ( many && distance > 3 && m_Mobile.Map != null && m_Mobile.Mana > 60 && maxCircle >= 7)
                {
					damageSpell = GetRandomMassSpell();
                }
			}

			if (damageSpell != null)
				return damageSpell;

			// Determine a random spell to cast, depending on the maxCircle I can cast:
            switch ( Utility.Random( maxCircle ) * 2)
            {
                case 1: damageSpell = new MagicArrowSpell(m_Mobile, null); break;
                case 2: damageSpell = new WeakenSpell(m_Mobile, null); break;
                case 3: damageSpell = new FireballSpell(m_Mobile, null); break;
                case 4: case 5: damageSpell = new PoisonSpell(m_Mobile, null); break;
                case 6: damageSpell = new LightningSpell(m_Mobile, null); break;
                case 7: damageSpell = new CurseSpell(m_Mobile, null); break;
                case 8: case 9: case 10: damageSpell = new EnergyBoltSpell(m_Mobile, null); break;
				case 11: case 12: case 13: damageSpell = new ExplosionSpell( m_Mobile, null ); break;
                default: damageSpell = new FlameStrikeSpell(m_Mobile, null); break;
            }
			
			return damageSpell;
		}

		// Reveal uses magery and detect hidden vs. hide and stealth 
		private static bool CheckDifficulty( Mobile from, Mobile m )
		{
			// Reveal always reveals vs. invisibility spell 
			if ( !Core.AOS || InvisibilitySpell.HasTimer( m ) )
				return true;

			int magery = from.Skills[SkillName.Magery].Fixed;
			int detectHidden = from.Skills[SkillName.DetectHidden].Fixed;

			int hiding = m.Skills[SkillName.Hiding].Fixed;
			int stealth = m.Skills[SkillName.Stealth].Fixed;
			int divisor = hiding + stealth;

			int chance;
			if ( divisor > 0 )
				chance = 50 * (magery + detectHidden) / divisor;
			else
				chance = 100;

			return chance > Utility.Random( 100 );
		}

		public virtual Spell GetRandomCurse()
		{
			if (SmartAI)
			{
				if (Utility.RandomBool() && m_Mobile.Combatant != null && m_Mobile.Combatant.Mana <= (m_Mobile.Combatant.Mana / 2) && m_Mobile.Combatant.Mana > 10 )
				{
					//they are using their mana, so let's snipe their mana
					if (m_Mobile.Mana < (m_Mobile.ManaMax / 2))
						return new ManaVampireSpell(m_Mobile, null);
					else
						return new ManaDrainSpell(m_Mobile, null);
				}
			}
			return ( CastingNecro() ) ?  GetRandomNecroCurseSpell() :  GetRandomCurseSpell();
		}

		public virtual Spell GetRandomBuffSpell()
		{
		    	if (m_Mobile is BaseCreature && !((BaseCreature)m_Mobile).IsBlessed && CheckCanCastMagery(3))
				return new BlessSpell(m_Mobile, null);
			
			if (m_Mobile is BaseCreature && CheckCanCastMagery(2))
				return new CunningSpell(m_Mobile, null);
			
			if (m_Mobile is BaseCreature && CheckCanCastMagery(2))
				return new AgilitySpell(m_Mobile, null);

		    	if (!m_Mobile.Controlled && CheckCanCastMagery(6))
				return new InvisibilitySpell(m_Mobile, null);

		    return null;
		}

		public virtual Spell GetRandomSummonSpell()
		{

				if (CastingNecro() && m_Mobile.Skills[ SkillName.Necromancy ].Value > 80 )
					return new VengefulSpiritSpell(m_Mobile, null);

				if (CheckCanCastMagery(8))
		      		return new EnergyVortexSpell(m_Mobile, null);

		      	if (CheckCanCastMagery(5))
		      		return new BladeSpiritsSpell(m_Mobile, null);

		    return null;
		}

		public virtual Spell GetRandomNecroCurseSpell()
		{

			if (SmartAI)
			{
				Mobile enemy = m_Mobile.Combatant;
				if (Utility.RandomBool() && enemy.Int > enemy.Str) //magic type enemy! lets mind rot
					return new MindRotSpell( m_Mobile, null );
				if (Utility.RandomBool() && m_Mobile.PhysicalResistance > 50) //high resist char, corpse skin!
					return new CorpseSkinSpell( m_Mobile, null );
				if (Utility.RandomDouble() > 0.75 && enemy.Str > enemy.Int) //melee type char
					return new BloodOathSpell( m_Mobile, null );
			}

				switch ( Utility.Random( 5 ) )
				{
					case 0: return new CorpseSkinSpell( m_Mobile, null );
					case 1: return new EvilOmenSpell( m_Mobile, null );
					case 2: return new BloodOathSpell( m_Mobile, null );
					case 3: return new MindRotSpell( m_Mobile, null );
					default: return new EvilOmenSpell( m_Mobile, null );
				}
			
		}

		public virtual Spell GetRandomCurseSpell()
		{
			if (SmartAI && Utility.RandomBool()) // see if we should curse everyone around us?
			{
				string strategy = AssessStrategy(m_Mobile.Combatant);
				if (Insensitive.Contains(strategy, "many") && CheckCanCastMagery(6))
				{
					return new MassCurseSpell( m_Mobile, null );
				}
			}
					
			if( Utility.Random( 4 ) == 3 )
			{
				if( myMagery >= 40.0 )
				{
					return new CurseSpell( m_Mobile, null );
				}
			}

			switch( Utility.Random( 3 ) )
			{
				default:
				case 0: return new WeakenSpell( m_Mobile, null );
				case 1: return new ClumsySpell( m_Mobile, null );
				case 2: return new FeeblemindSpell( m_Mobile, null );
			}
		}

		public virtual Spell GetRandomManaDrainSpell()
		{
			if( Utility.RandomBool() )
			{
				if( myMagery >= 80.0 )
					return new ManaVampireSpell( m_Mobile, null );
			}

			return new ManaDrainSpell( m_Mobile, null );
		}

		public virtual Spell GetRandomMassSpell()
		{
			Spell damageSpell = null;
			switch ( GetMaxCircle(m_Mobile) )
			{
							default: break;
							case 7: damageSpell = new ChainLightningSpell(m_Mobile, null); break;
							case 8: damageSpell = new MeteorSwarmSpell(m_Mobile, null); break;
							case 9: damageSpell = new EarthquakeSpell(m_Mobile, null); break;
			}

			if (damageSpell != null)
				return damageSpell;

			return null;
		}

		public virtual Spell GetRandomFieldSpell()
		{
			Spell spell = null;
			switch (Utility.Random(GetMaxCircle(m_Mobile)))
			{
							case 3: spell = new WallOfStoneSpell(m_Mobile, null); break;
							case 1: case 4: spell = new FireFieldSpell(m_Mobile, null); break;
							case 2: case 5: spell = new PoisonFieldSpell(m_Mobile, null); break;
							case 6: case 7: spell = new ParalyzeFieldSpell(m_Mobile, null); break;
							case 8: case 9: spell = new EnergyFieldSpell(m_Mobile, null); break;
			}

			if (spell != null)
				return spell;

			return null;
		}

		public virtual Spell DoDispel( Mobile toDispel )
		{
			if( !SmartAI )
			{
				if( ScaleByMagery( DispelChance ) > Utility.RandomDouble() )
					return new DispelSpell( m_Mobile, null );

				return ChooseSpell( toDispel );
			}

			Spell spell = CheckCastHealingSpell();

			if( spell == null )
			{
				if( !m_Mobile.DisallowAllMoves && Utility.Random( (int)m_Mobile.GetDistanceToSqrt( toDispel ) ) == 0 && !Server.Mobiles.BasePirate.IsSailor( m_Mobile ) )
					spell = new TeleportSpell( m_Mobile, null );
				else if( Utility.Random( 3 ) == 0 && !m_Mobile.InRange( toDispel, 3 ) && !toDispel.Paralyzed && !toDispel.Frozen )
					spell = new ParalyzeSpell( m_Mobile, null );
				else
					spell = new DispelSpell( m_Mobile, null );
			}

			return spell;
		}

		public virtual double myNecro { get { return m_Mobile.Skills[ SkillName.Necromancy ].Value; } }

		public virtual double myMagery { get { return m_Mobile.Skills[ SkillName.Magery ].Value; } }

		public virtual double mySpiritSpeak { get { return m_Mobile.Skills[ SkillName.SpiritSpeak ].Value; } }

		public virtual Spell ChooseSpell( Mobile c )
		{

			// Skip healing if delay too short:
            if (DateTime.UtcNow < m_NextCastTime)
                return null;

			Spell spell = null;
			string strategy = AssessStrategy(m_Mobile.Combatant);
			bool many = Insensitive.Contains(strategy, "many");
			bool close = Insensitive.Contains(strategy, "close");
			bool distance =  Insensitive.Contains(strategy, "distance");
			int dist = (int)m_Mobile.GetDistanceToSqrt( c );
			double odds = Utility.RandomDouble();

			spell = CheckCastHealingSpell();

			if( spell != null )
				return spell;

			if (c == null)
				return null;

			if( !SmartAI ) //ordinary dumb-as nails original UO AI casting begins here
			{
				switch( Utility.Random( 16 ) )
				{
					case 0:
					case 1:	// Poison them
						{
							//m_Mobile.DebugSay( "Attempting to poison" );

							if( !c.Poisoned )
								spell = new PoisonSpell( m_Mobile, null );

							break;
						}
					case 2:	// Bless ourselves.
						{
							//m_Mobile.DebugSay( "Blessing myself" );

							spell = new BlessSpell( m_Mobile, null );
							break;
						}
					case 3:
					case 4: // Curse them.
						{
							//m_Mobile.DebugSay( "Attempting to curse" );

							spell = GetRandomCurse();
							break;
						}
					case 5:	// Paralyze them.
						{

							if( GetMaxCircle(m_Mobile) >= 5 )
								spell = new ParalyzeSpell( m_Mobile, null );

							break;
						}
					case 6: // Drain mana
						{
							//m_Mobile.DebugSay( "Attempting to drain mana" );

							spell = GetRandomManaDrainSpell();
							break;
						}
					case 7:
						{
							//m_Mobile.DebugSay( "Attempting to Invis" );

							if( spell == null )
							{
								spell = new InvisibilitySpell( m_Mobile, null );
							}

							break;
						}

					default: // Damage them.
						{
							spell = GetRandomDamage(dist, many);
							break;
						}
				}

				return spell;
			}

			//hereon only for smartai, now we let the fun truly begin
			//first start with some routines based on situation
			if (many)
			{
				//many of them: field them, mass damage them, or mass curse them if close
				if ( dist > 4 && odds < 0.50 ) 
				{
					if (Utility.RandomBool())
						spell = GetRandomMassSpell();
					else
						spell = GetRandomFieldSpell();
				}
				if (spell == null && !CurseSpell.UnderEffect( c ) && dist < 4 && m_Mobile.Mana > 40 && GetMaxCircle(m_Mobile) >= 6 && odds < 0.25)
				{
					spell = new MassCurseSpell(m_Mobile, null);
				}

			}
			if (spell == null && close && Utility.RandomBool())
			{
				//being attacked by melee
				if ( m_Mobile.Skills[ SkillName.Poisoning ].Value > 50.0 && Utility.RandomDouble() <= 0.30 && !c.Poisoned) // higher chance to poison with skill
					spell = new PoisonSpell( m_Mobile, null );
				else if ( m_Mobile.Skills[ SkillName.Poisoning ].Value <= 50.0 && Utility.RandomDouble() <= 0.10 && !c.Poisoned)
					spell = new PoisonSpell( m_Mobile, null );
				else if (Utility.RandomDouble() <= 30 && m_Mobile.Skills[ SkillName.Magery ].Value > 50.0 && !c.Paralyzed)
					spell = new ParalyzeSpell( m_Mobile, null );

				if ( spell == null && CastingNecro() && odds <= 0.45 && Utility.RandomBool() && m_Mobile.Skills[ SkillName.Necromancy ].Value > 30 && BloodOathSpell.GetBloodOath( c ) != m_Mobile )
					spell = new BloodOathSpell( m_Mobile, null );
			}

			if (spell == null && distance && Utility.RandomBool()) //getting attacked from a distance possibly single archer/mage 
			{
				if (c is PlayerMobile)
				{
					PlayerMobile player = (PlayerMobile)c;
					Item twohanded = c.FindItemOnLayer( Layer.TwoHanded );			
					Item firstvalid = c.FindItemOnLayer( Layer.FirstValid );

					//archer, first try to paralyze
                			if ( ( twohanded is BaseRanged || firstvalid is BaseRanged || twohanded is BaseMeleeWeapon || firstvalid is BaseMeleeWeapon ) && !( c.Paralyzed || c.Frozen) && GetMaxCircle(m_Mobile) >= 5 && odds > 0.55)
						spell = new ParalyzeSpell( m_Mobile, null );

					//not paralyzed, so lets blood oath if necro
					if ( spell == null && CastingNecro() && odds <= 0.45 && odds >= 0.15 && m_Mobile.Skills[ SkillName.Necromancy ].Value > 30 && BloodOathSpell.GetBloodOath( c ) != m_Mobile )
						spell = new BloodOathSpell( m_Mobile, null );
				}
				
				//lets summon some help with this one
				if ( spell == null && ( m_Mobile.Followers + 3 ) < m_Mobile.FollowersMax && m_Mobile.Mana > 50 && odds > 0.30 && odds < 0.60 )
					spell = GetRandomSummonSpell();
			}

			if ( (c.Frozen || c.Paralyzed) && m_Combo == -1 && m_Mobile.Mana > 60 && Utility.RandomBool()) //opponent is frozen, lets do a combo!
			{
				m_Combo = (Utility.RandomMinMax(1, 3));
			}

			//if necro and lots of bodies nearby
			if (spell == null && CastingNecro() && m_Mobile.Skills[ SkillName.Necromancy ].Value > 40 && ( m_Animated == null || !m_Animated.Alive ) && odds <= 0.35)
			{
				bool found = false;
				foreach ( Item item in m_Mobile.GetItemsInRange( 5 ) )
				{
					if (item is Corpse)
						found = true;
				}
				if ( found )
					spell = new AnimateDeadSpell( m_Mobile, null );
			}

			//lets drain some mana, we're low
			if (spell == null && m_Mobile.Mana < 50 && c.Mana > 50 && Utility.RandomDouble() >= 0.80 )
			{
				spell = GetRandomManaDrainSpell();
			}

			//is the player hidden?
			if (spell == null && c.Hidden && odds > 0.50 && odds < 0.70)
            		{
				bool foundhidden = false;

				//reveal them
				if (GetMaxCircle(m_Mobile) >= 6 && Utility.RandomDouble() > 0.66 && m_Mobile.InRange( c,  m_Mobile.RangePerception) )
				{
				    List<Mobile> targets = new List<Mobile>();

				    Map map = m_Mobile.Map;
				    IPoint3D p = m_Mobile.Location;

				    if ( map != null )
				    {
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 1 + (int)(m_Mobile.Skills[SkillName.Magery].Value / 20.0) );

					foreach ( Mobile m in eable )
					{
					    if ( m.Hidden && (m.AccessLevel == AccessLevel.Player || m_Mobile.AccessLevel > m.AccessLevel) && CheckDifficulty( m_Mobile, m ) )
						targets.Add( m );
					}

					eable.Free();
				    }

				    for ( int i = 0; i < targets.Count; ++i )
				    {
					Mobile m = targets[i];

					m.RevealingAction();

					m.FixedParticles( 0x375A, 9, 20, 5049, Server.Items.CharacterDatabase.GetMySpellHue( m_Mobile, 0 ), 0, EffectLayer.Head );
					m.PlaySound( 0x1FD );
								if (m.Player)
									m.SendMessage("You've been revealed!");

								foundhidden = true;
				    }
                		}
				if (foundhidden) //did an action this time
					return null;
            		}


			//small chance of blessing
			if (spell == null && odds < 0.05 )
				spell = new BlessSpell( m_Mobile, null );

			//none of the above worked, lets damage!
			if (spell == null && Utility.RandomDouble() > 0.10)
			{
				spell = GetRandomDamage(dist, many);
			}

			if (spell != null)
				return spell;

			//okay, generic function below in case nothing caught
			switch( Utility.Random( 8 ) )
			{
				default:
				case 0: // Poison them or curse them
					{
						if( !c.Poisoned )//&& !(c is BaseCreature && ((BaseCreature)c).PoisonImmune))
							spell = new PoisonSpell( m_Mobile, null );
						else
							spell = GetRandomCurse();

						break;
					}
				case 1: case 3: case 4: // Deal some damage
					{
						spell = GetRandomDamage();

						break;
					}
				case 2: case 5: // Set up a combo or special move
					{
						if( m_Mobile.Mana < 40 && m_Mobile.Mana > 15 )
						{
							if( c.Paralyzed && !c.Poisoned )
							{
								m_Mobile.DebugSay( "I am going to meditate" );

								m_Mobile.UseSkill( SkillName.Meditation );
							}
							else if( !c.Poisoned )
							{
								spell = new ParalyzeSpell( m_Mobile, null );
							}
						}
						else if(m_Mobile.Mana > 60 )
						{
							if( Utility.Random( 2 ) == 0 && !c.Paralyzed && !c.Frozen && !c.Poisoned )
							{
								m_Combo = 0;
								spell = new ParalyzeSpell( m_Mobile, null );
							}
							else
							{
								if (SmartAI && m_Mobile.Mana > 100)
									m_Combo = Utility.RandomMinMax(1,3);
								else
									m_Combo = 1;
								spell = new ExplosionSpell( m_Mobile, null );
							}
						}
						

						break;
					}
				case 6: //fields!
				{
					//are there many there?
					if (many && GetMaxCircle(m_Mobile) >= 4)
					{
						spell = GetRandomFieldSpell();
					}
					else
						spell = GetRandomDamage();
					
					break;
				} 
				case 7:
				{
					if (SmartAI && ( m_Mobile.Followers + 3 ) < m_Mobile.FollowersMax )
					{
						spell = GetRandomSummonSpell();
					}
					if (spell == null)
						spell = GetRandomDamage();
					
					break;
				}
			}

			return spell;
		}

		protected int m_Combo = -1;

		public virtual Spell DoCombo( Mobile c )
		{
			Spell spell = null;

			if( m_Combo == 0 )
			{
				spell = new ExplosionSpell( m_Mobile, null );
				++m_Combo; // Move to next spell
			}
			else if( m_Combo == 1 )
			{
				spell = new WeakenSpell( m_Mobile, null );
				++m_Combo; // Move to next spell
			}
			else if( m_Combo == 2 )
			{
				if( !c.Poisoned )
					spell = new PoisonSpell( m_Mobile, null );
				else if (CastingNecro())
					spell = new StrangleSpell( m_Mobile, null );

				++m_Combo; // Move to next spell
			}

			if( m_Combo == 3 && spell == null )
			{
				switch( Utility.Random( 4 ) )
				{
					default:
					case 0:
						{
							if( c.Int < c.Dex )
								spell = new FeeblemindSpell( m_Mobile, null );
							else
								spell = new ClumsySpell( m_Mobile, null );

							++m_Combo; // Move to next spell

							break;
						}
					case 1:
						{
							spell = new EnergyBoltSpell( m_Mobile, null );
							m_Combo = -1; // Reset combo state
							break;
						}
					case 2:
						{
							spell = new FlameStrikeSpell( m_Mobile, null );
							m_Combo = -1; // Reset combo state
							break;
						}
					case 3:
					{
						if (CastingNecro())
							spell = new PainSpikeSpell( m_Mobile, null );
						else
							spell = new FireballSpell( m_Mobile, null );

						m_Combo = -1; // Reset combo state
						break;
					}
				}
			}
			else if( m_Combo == 4 && spell == null )
			{
				spell = new MindBlastSpell( m_Mobile, null );
				m_Combo = -1;
			}

			return spell;
		}
		
		private void TeleportCombo(Mobile target) //teleported to/away from the enemy, lets do a combo because we're smart.
		{
			double odds = Utility.RandomDouble();
			Spell spell = null;
			
			if (odds <= 0.50 && !target.Frozen && !target.Paralyzed )
				spell = new ParalyzeSpell( m_Mobile, null );
			else if (odds <= 0.60)
				GetRandomFieldSpell();
			else if (odds <= 0.70 && !target.Poisoned )
				spell = new PoisonSpell( m_Mobile, null );
			else if (odds <= 0.80 && ( m_Mobile.Followers + 3 ) < m_Mobile.FollowersMax )
				spell = GetRandomSummonSpell();
			else if (CastingNecro())
				spell = GetRandomNecroCurseSpell();
			else
				spell = GetRandomCurseSpell();
			
			if( spell != null )
				spell.Cast();
		}

		private TimeSpan GetDelay()
		{
			double del = ScaleByMagery( 3.0 );
			double min = 6.0 - ( del * 0.75 );
			double max = 6.0 - ( del * 1.25 );

			return TimeSpan.FromSeconds( min + ( ( max - min ) * Utility.RandomDouble() ) );
		}

		public override bool DoActionCombat()
		{
			Mobile c = m_Mobile.Combatant;
			m_Mobile.Warmode = true;

			if( c == null || c.Deleted || !c.Alive || c.IsDeadBondedPet || !m_Mobile.CanSee( c ) || !m_Mobile.CanBeHarmful( c, false ) || c.Map != m_Mobile.Map )
			{
				// Our combatant is deleted, dead, hidden, or we cannot hurt them
				if ( c != null && !c.Deleted && c.Alive && SmartAI && !c.Hidden && !m_Mobile.CanSee( c ) ) //moved away somewhere
					FindEnemy(c);
					
				// Try to find another combatant
				if(  c != null && !c.Deleted && c.Alive && !c.Hidden && !m_Mobile.CanSee( c ) && AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					if( m_Mobile.Debug )
						m_Mobile.DebugSay( "Something happened to my combatant, so I am going to fight {0}", m_Mobile.FocusMob.Name );

						m_Mobile.Combatant = c = m_Mobile.FocusMob;
						m_Mobile.FocusMob = null;
				}
				if (c == null || c.Deleted || !c.Alive || c.IsDeadBondedPet)
				{
					m_Mobile.DebugSay( "Something happened to my combatant, and nothing is around. I am on guard." );

					Action = ActionType.Guard;
					return true;
				}
			}

			if( !m_Mobile.InLOS( c ) )
			{
				m_Mobile.DebugSay( "I can't see my target" );
				if (SmartAI && !c.Hidden)
					FindEnemy(c);

				if( !m_Mobile.InLOS( c ) && AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.DebugSay( "Nobody else is around" );
					m_Mobile.Combatant = c = m_Mobile.FocusMob;
					m_Mobile.FocusMob = null;
				}
			}
			
			DistanceCheck(c);

			if( !m_Mobile.InRange( c, m_Mobile.RangePerception ) )
			{
				// They are somewhat far away, can we find something else?

				if( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.Combatant = m_Mobile.FocusMob;
					m_Mobile.FocusMob = null;
				}
				else if( !m_Mobile.InRange( c, m_Mobile.RangePerception * 3 ) )
				{
					m_Mobile.Combatant = null;
				}

				c = m_Mobile.Combatant;

				if( c == null )
				{
					m_Mobile.DebugSay( "My combatant has fled, so I am on guard" );
					Action = ActionType.Guard;

					return true;
				}
			}

			if (SmartAI)
			{
				// no more dumb mobs being tanked by tamers
				if (m_Mobile.Combatant is BaseCreature && Utility.RandomDouble() > 0.66)
				{
					BaseCreature b = (BaseCreature)m_Mobile.Combatant;
					if ( b.ControlMaster != null && b.ControlMaster is PlayerMobile ) // tamer
					{
						m_Mobile.Combatant = b.ControlMaster;
						m_Mobile.FocusMob = b.ControlMaster;
					}
				}
				//stun if wrestling is high
				if(  m_Mobile.InRange( c, 1 ) && !m_Mobile.StunReady && m_Mobile.Skills[ SkillName.Wrestling ].Value >= 80.0 && m_Mobile.Skills[ SkillName.Anatomy ].Value >= 80.0 )
					EventSink.InvokeStunRequest( new StunRequestEventArgs( m_Mobile ) );
			}

			if( !m_Mobile.Controlled && !m_Mobile.Summoned && !m_Mobile.IsParagon && !(m_Mobile is HenchmanArcher) && !(m_Mobile is HenchmanMonster) && !(m_Mobile is HenchmanWizard) && !(m_Mobile is HenchmanFighter) && !(m_Mobile is BaseUndead) ) 
			{
				if( m_Mobile.Hits < m_Mobile.HitsMax * 20 / 100 )
				{

					// We are low on health, should we flee?

					bool flee = false;

					if( m_Mobile.Hits < c.Hits )
					{
						// We are more hurt than them

						int diff = c.Hits - m_Mobile.Hits;

						flee = ( Utility.Random( 0, 100 ) > ( 10 + diff ) ); // (10 + diff)% chance to flee
					}
					else
					{
						flee = Utility.Random( 0, 100 ) > 10; // 10% chance to flee
					}

					if( flee )
					{
						if( m_Mobile.Debug )
							m_Mobile.DebugSay( "I am going to flee from {0}", c.Name );

						Action = ActionType.Flee;
						return true;
					}
				}

				// At low Mana we should check if we should flee:
				if (SmartAI && m_Mobile.Mana < 20)
				{

					bool bFlee = false;
					if (Utility.Random(0, 100) > 20) // 80% chance to flee
						bFlee = true;

					if (bFlee)
						Action = ActionType.Flee;
				}
			}

			if( m_Mobile.Spell == null && DateTime.UtcNow > m_NextCastTime && m_Mobile.InRange( c, Core.ML ? 10 : 12 ) )
			{
				// We are ready to cast a spell
				Spell spell = null;
				Mobile toDispel = FindDispelTarget( true );

				if( m_Mobile.Poisoned ) // Top cast priority is cure
				{
					m_Mobile.DebugSay( "I am going to cure myself" );

					spell = new CureSpell( m_Mobile, null );
				}
				else if( toDispel != null ) // Something dispellable is attacking us
				{
					m_Mobile.DebugSay( "I am going to dispel {0}", toDispel );

					spell = DoDispel( toDispel );
				}
				else if( SmartAI && m_Combo != -1 ) // We are doing a spell combo
				{
					spell = DoCombo( c );
				}
				else if( SmartAI && c.Spell != null && Utility.RandomBool()  ) // They have a spell getting ready, interrupt?
				{
					if (( c.Spell is HealSpell || c.Spell is GreaterHealSpell ) && !c.Poisoned )
						spell = new PoisonSpell( m_Mobile, null );
					else if (Utility.RandomBool())
						spell = new MagicArrowSpell(m_Mobile, null);
					else
						spell = new FireballSpell(m_Mobile, null);
				}
				else
				{
					spell = ChooseSpell( c );
				}

				// Now we have a spell picked.  still chance spell == null here

				if( toDispel != null )
				{
					if( !m_Mobile.InRange( toDispel, Core.ML ? 10 : 12 ) )
						DistanceCheck( toDispel );
				}

				if( spell != null )
				{
					spell.Cast();

					if( DateTime.UtcNow > m_NextAnimateTime && m_Mobile.HasBreath == false && !m_Mobile.Mounted )
					{
						m_Mobile.PlaySound( m_Mobile.GetAngerSound());
						m_Mobile.Animate( 12, 5, 1, true, false, 0 );
						m_NextAnimateTime = DateTime.UtcNow + TimeSpan.FromSeconds( m_AnimateDelay );
						NextMove = DateTime.UtcNow + TimeSpan.FromSeconds( m_AnimateFinish );
					}
				}

				m_NextCastTime = DateTime.UtcNow + CastDelay();

			}


			return true;
		}

		public override bool DoActionGuard()
		{
			if( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "I am going to attack {0}", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				if( !m_Mobile.Controlled && !(m_Mobile is HenchmanArcher) && !(m_Mobile is HenchmanMonster) && !(m_Mobile is HenchmanWizard) && !(m_Mobile is HenchmanFighter) ) // WIZARD ADDED FOR HENCHMAN
				{
					ProcessTarget();

					Spell spell = CheckCastHealingSpell();

					if( spell != null )
					{
						spell.Cast();
						if( DateTime.UtcNow > m_NextAnimateTime && m_Mobile.HasBreath == false && !m_Mobile.Mounted )
						{
							m_Mobile.PlaySound( m_Mobile.GetAngerSound());
							m_Mobile.Animate( 12, 5, 1, true, false, 0 );
							m_NextAnimateTime = DateTime.UtcNow + TimeSpan.FromSeconds( m_AnimateDelay );
							NextMove = DateTime.UtcNow + TimeSpan.FromSeconds( m_AnimateFinish );
						}
					}
				}

				base.DoActionGuard();
			}

			return true;
		}

		public override bool DoActionFlee()
		{
			Mobile c = m_Mobile.Combatant;

			if (( m_Mobile.Mana < 20 || m_Mobile.Mana == m_Mobile.ManaMax ))
				m_Mobile.UseSkill( SkillName.Meditation );

			else if( ( m_Mobile.Mana > 20 || m_Mobile.Mana == m_Mobile.ManaMax ) && m_Mobile.Hits > ( m_Mobile.HitsMax / 2 ) )
			{
				m_Mobile.DebugSay( "I am stronger now, my guard is up" );
				Action = ActionType.Guard;
			}
			else if( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if( m_Mobile.Debug )
					m_Mobile.DebugSay( "I am scared of {0}", m_Mobile.FocusMob.Name );

				RunFrom( m_Mobile.FocusMob );
				m_Mobile.FocusMob = null;

				if( m_Mobile.Poisoned && Utility.Random( 0, 5 ) == 0 )
					new CureSpell( m_Mobile, null ).Cast();
			}
			else
			{
				m_Mobile.DebugSay( "Area seems clear, but my guard is up" );

				Action = ActionType.Guard;
				m_Mobile.Warmode = true;
			}

			return true;
		}

		public Mobile FindDispelTarget( bool activeOnly )
		{
			if( m_Mobile.Deleted || m_Mobile.Int < 95 || CanDispel( m_Mobile ) || m_Mobile.AutoDispel )
				return null;

			if( activeOnly )
			{
				List<AggressorInfo> aggressed = m_Mobile.Aggressed;
				List<AggressorInfo> aggressors = m_Mobile.Aggressors;

				Mobile active = null;
				double activePrio = 0.0;

				Mobile comb = m_Mobile.Combatant;

				if( comb != null && !comb.Deleted && comb.Alive && !comb.IsDeadBondedPet && m_Mobile.InRange( comb, Core.ML ? 10 : 12 ) && CanDispel( comb ) )
				{
					active = comb;
					activePrio = m_Mobile.GetDistanceToSqrt( comb );

					if( activePrio <= 2 )
						return active;
				}

				for( int i = 0; i < aggressed.Count; ++i )
				{
					AggressorInfo info = aggressed[ i ];
					Mobile m = info.Defender;

					if( m != comb && m.Combatant == m_Mobile && m_Mobile.InRange( m, Core.ML ? 10 : 12 ) && CanDispel( m ) )
					{
						double prio = m_Mobile.GetDistanceToSqrt( m );

						if( active == null || prio < activePrio )
						{
							active = m;
							activePrio = prio;

							if( activePrio <= 2 )
								return active;
						}
					}
				}

				for( int i = 0; i < aggressors.Count; ++i )
				{
					AggressorInfo info = aggressors[ i ];
					Mobile m = info.Attacker;

					if( m != comb && m.Combatant == m_Mobile && m_Mobile.InRange( m, Core.ML ? 10 : 12 ) && CanDispel( m ) )
					{
						double prio = m_Mobile.GetDistanceToSqrt( m );

						if( active == null || prio < activePrio )
						{
							active = m;
							activePrio = prio;

							if( activePrio <= 2 )
								return active;
						}
					}
				}

				return active;
			}
			else
			{
				Map map = m_Mobile.Map;

				if( map != null )
				{
					Mobile active = null, inactive = null;
					double actPrio = 0.0, inactPrio = 0.0;

					Mobile comb = m_Mobile.Combatant;

					if( comb != null && !comb.Deleted && comb.Alive && !comb.IsDeadBondedPet && CanDispel( comb ) )
					{
						active = inactive = comb;
						actPrio = inactPrio = m_Mobile.GetDistanceToSqrt( comb );
					}

					foreach( Mobile m in m_Mobile.GetMobilesInRange( Core.ML ? 10 : 12 ) )
					{
						if( m != m_Mobile && CanDispel( m ) )
						{
							double prio = m_Mobile.GetDistanceToSqrt( m );

							if( !activeOnly && ( inactive == null || prio < inactPrio ) )
							{
								inactive = m;
								inactPrio = prio;
							}

							if( ( m_Mobile.Combatant == m || m.Combatant == m_Mobile ) && ( active == null || prio < actPrio ) )
							{
								active = m;
								actPrio = prio;
							}
						}
					}

					return active != null ? active : inactive;
				}
			}

			return null;
		}

		public Item FindCorpseToAnimate()
		{
			foreach ( Item item in m_Mobile.GetItemsInRange( 12 ) )
			{

				Corpse c = item as Corpse;

				if ( c != null )
				{
					Type type = null;

					if ( c.Owner != null )
						type = c.Owner.GetType();

					BaseCreature owner = c.Owner as BaseCreature;

					if ( ( c.ItemID < 0xECA || c.ItemID > 0xED5 ) && m_Mobile.InLOS( c ) && !c.Channeled && type != typeof( PlayerMobile ) && type != null && ( owner == null || ( !owner.Summoned && !owner.IsBonded ) ) )
						return item;
				}
			}

			return null;
		}

		//FT movement redone
		public bool DistanceCheck(Mobile target)
		{
			if (target == null || target.Deleted || !target.Alive || target.IsDeadBondedPet )
				target = m_Mobile.Combatant;
			
			if (target == null || target.Deleted || !target.Alive || target.IsDeadBondedPet )
				return false;

			if( ( m_Mobile.Spell != null && m_Mobile.Spell.IsCasting ) || m_Mobile.Paralyzed || m_Mobile.Frozen || m_Mobile.DisallowAllMoves )
				return false; //shouldn't be moving now
				
			if (DoesMelee && m_Mobile.RangeFight > 1)
				m_Mobile.RangeFight = 1;
			if (!DoesMelee && m_Mobile.RangeFight < 5)
				m_Mobile.RangeFight = 5;
			
			bool shouldhavemoved = false;
			int distance = (int)m_Mobile.GetDistanceToSqrt(target);
			
			Point3D oldloc = m_Mobile.Location;
			
			if ( (m_Mobile is BaseCreature && ((BaseCreature)m_Mobile).Controlled) && DoesMelee && MoveTo( target, true, m_Mobile.RangeFight ) )
			{
				//pets are dumb and should just rush the enemey apparently
				m_Mobile.Direction = m_Mobile.GetDirectionTo( target );
				shouldhavemoved = true;
			}
				
				// Can i move?  e.g. every second EXCEPT fullspeed... that might be a bit much but lets see
				if (!shouldhavemoved && ( (m_Mobile is BaseCreature && ((BaseCreature)m_Mobile).AIFullSpeedActive) || SmartAI || (Core.TickCount - m_Mobile.LastMoveTime) > 1000 ) )
				{
					//check distances: is enemy in casting range??
					if (distance < 10 ) 
					{
						if (!m_Mobile.InLOS(target) && SmartAI && !target.Hidden) //behind a corner? try and find him
							FindEnemy(target);
							
						//is enemy too close?
						else if (distance < m_Mobile.RangeFight && !DoesMelee) //too close for comfort
						{
							if (!SmartAI)
								RunFrom( target );
							// Walk to get a distance of 6...9 steps between me and my enemy:
							else
								WalkMobileRange(target, 4, true, 6, 9);
							// Turn and face the enemy:
							m_Mobile.Direction = m_Mobile.GetDirectionTo(target.Location);
							shouldhavemoved = true;
						}
						else if (distance > m_Mobile.RangeFight && DoesMelee ) // need to get closer
						{
							RunTo(target);
							shouldhavemoved = true;
						}
						else
							return true; // in casting range, all is good

					}
					else if (distance > m_Mobile.RangePerception && distance < (m_Mobile.RangePerception + 4) ) //just over the needed range
					{
						if ((!SmartAI || DoesMelee) && Utility.RandomBool() )
							RunTo(target);
						//let's try to walk towards the target
						else 
							WalkMobileRange(target, 3, true, 6, 9);
						// Turn and face the enemy:
						m_Mobile.Direction = m_Mobile.GetDirectionTo(target.Location);
						shouldhavemoved = true;
					}
					else //enemy too far, or gone
					{
						// Walk back towards home (60% chance not to move, high chance to change direction, # steps to walk):
						// (Only creatures with positive karma will try to fight near their spawn point)
						if ((Utility.Random(3) == 0) && (m_Mobile.Karma >= 0) )
							WalkBack(2, 0, 7);
					
						//If enemy out of range:
						if (target != null)
						{
							if (distance > m_Mobile.RangePerception + 3)
							{
								if (AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ))
								{
									//new target
									m_Mobile.Combatant = m_Mobile.FocusMob;
									Action = ActionType.Combat;
									return true;
								}
								else
								{
									// Enemy is lost. Return to guard.
									m_Mobile.Combatant = null;
									Action = ActionType.Guard;
									return false;
								}
							}
						}
					}
					
				}
				if (shouldhavemoved &&  m_Mobile.Location == oldloc ) //didnt move in the last second but should have
				{
					OnFailedMove(); // will try to unstuck
				}
				
			return false;
		}

		public void Run( Direction d )
		{
			if( ( m_Mobile.Spell != null && m_Mobile.Spell.IsCasting ) || m_Mobile.Paralyzed || m_Mobile.Frozen || m_Mobile.DisallowAllMoves )
				return;

			m_Mobile.Direction = d | Direction.Running;

			if( !DoMove( m_Mobile.Direction, true ) )
				OnFailedMove();
		}

        // Walk back home towards spawnpoint:
        private void WalkBack(int iChanceToNotMove, int iChanceToDir, int iSteps)
        {
            if (m_Mobile.Deleted || m_Mobile.DisallowAllMoves)
                return;

            // If I'm not standing by my spawn point, take "iSteps" steps towards it...
            if (m_Mobile.Home != Point3D.Zero)
            {
                // m_Mobile.GetDistanceToSqrt(m_Mobile.Home): Distance to Spawner
                // m_Mobile.RangeHome: Defined home range of the spawner, i.e. circle around spawner that is considered "home".
                // If more than 10 steps away from home range:
                if ( (int)m_Mobile.GetDistanceToSqrt(m_Mobile.Home) > (m_Mobile.RangeHome + 15) )
                {
                    int i = 0;
                    while (i < iSteps)
                    {
                        if (Utility.Random(10) > 3)
                        {
                            DoMove(m_Mobile.GetDirectionTo(m_Mobile.Home));
                        }
                        else
                        {
                            WalkRandom(iChanceToNotMove, iChanceToDir, 1);
                        }

                        i = i + 1;
                    }
                }
            }
        }
		
		public void RunTo( Mobile m )
		{
			if( !SmartAI )
			{
				if( !MoveTo( m, true, m_Mobile.RangeFight ) )
					OnFailedMove();

				return;
			}

			if( m.Paralyzed || m.Frozen )
			{
				if( m_Mobile.InRange( m, 1 ) )
					RunFrom( m );
				else if( !m_Mobile.InRange( m, m_Mobile.RangeFight > 2 ? m_Mobile.RangeFight : 2 ) && !MoveTo( m, true, m_Mobile.RangeFight ) )
					OnFailedMove();
			}
			else
			{
				if( !m_Mobile.InRange( m, m_Mobile.RangeFight ) )
				{
					if( !MoveTo( m, true, m_Mobile.RangeFight ) )
						OnFailedMove();
				}
				else if( m_Mobile.InRange( m, m_Mobile.RangeFight - 1 ) )
				{
					RunFrom( m );
				}
			}
		}

		public void RunFrom( Mobile m )
		{
			Run( ( m_Mobile.GetDirectionTo( m ) - 4 ) & Direction.Mask );
		}

		public void OnFailedMove()
		{
			if( !m_Mobile.DisallowAllMoves && !Server.Mobiles.BasePirate.IsSailor( m_Mobile ) && ( SmartAI ? Utility.Random( 4 ) == 0 : ScaleByMagery( TeleportChance ) > Utility.RandomDouble() ) )
			{
				if( m_Mobile.Target != null )
					m_Mobile.Target.Cancel( m_Mobile, TargetCancelType.Canceled );

				new TeleportSpell( m_Mobile, null ).Cast();

				m_Mobile.DebugSay( "I am stuck, I'm going to try teleporting away" );
			}
			else if( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if( m_Mobile.Debug )
					m_Mobile.DebugSay( "My move is blocked, so I am going to attack {0}", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				WalkRandom(0, 1, 1); // Final, can no longer be cornered! will try a random move anywhere
				m_Mobile.DebugSay( "I am stuck" );
			}
		}

		private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				 0, -1,
				 0,  1,
				 1, -1,
				 1,  0,
				 1,  1,

				-2, -2,
				-2, -1,
				-2,  0,
				-2,  1,
				-2,  2,
				-1, -2,
				-1,  2,
				 0, -2,
				 0,  2,
				 1, -2,
				 1,  2,
				 2, -2,
				 2, -1,
				 2,  0,
				 2,  1,
				 2,  2
			};

		private bool FindNewEnemy()
		{
			if( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.Combatant = m_Mobile.FocusMob;
				m_Mobile.FocusMob = null;
				return true;
			}
			
			return false;
		}
			
		private bool ProcessTarget()
		{
			Target targ = m_Mobile.Target;

			if( targ == null )
				return false;

			bool isDispel = ( targ is DispelSpell.InternalTarget );
			bool isParalyze = ( targ is ParalyzeSpell.InternalTarget );
			bool isTeleport = ( targ is TeleportSpell.InternalTarget );
			bool isBeneficial = ( targ is InvisibilitySpell.InternalTarget || targ is BlessSpell.InternalTarget || targ is CunningSpell.InternalTarget || targ is AgilitySpell.InternalTarget);
			bool isAnimate = ( targ is AnimateDeadSpell.InternalTarget );
			bool isField = ( targ is FireFieldSpell.InternalTarget || targ is PoisonFieldSpell.InternalTarget || targ is WallOfStoneSpell.InternalTarget || targ is ParalyzeFieldSpell.InternalTarget || targ is EnergyFieldSpell.InternalTarget );//Smart AI for field casting
			bool teleportAway = false;

			Mobile toTarget;

			if( isBeneficial )
			{
				toTarget = m_Mobile;
			}
			else if( isDispel )
			{
				toTarget = FindDispelTarget( false );

				if( toTarget != null && !m_Mobile.InRange( toTarget, 10 ) )
					DistanceCheck( toTarget );
			}
			else if ( isTeleport ) //FT redo
			{
				toTarget = m_Mobile.Combatant;
				if( !DoesMelee && toTarget != null && m_Mobile.InRange( toTarget, 3 ) ) //they are close lets move away
				{
					teleportAway = true;
					DistanceCheck( toTarget );
				}
				else if (toTarget == null && DoesMelee) //teleport TO the enemy, find new enemy
				{
					if (!FindNewEnemy())
						toTarget = FindDispelTarget( true );
				}	
			}
			else if( isParalyze ) //FT redo
			{
				toTarget = m_Mobile.Combatant;
				
				if( toTarget == null )
				{
					toTarget = FindDispelTarget( true );
					if( toTarget != null)
						DistanceCheck( toTarget );
				}
			}
			else if (isField )
			{
				toTarget = m_Mobile.Combatant;
				
				if (SmartAI && toTarget.Map != null && !toTarget.Deleted)
				{
					//check if its a bunch of tames coming our way and are over 4 tiles away
					if ( m_Mobile.GetDistanceToSqrt( toTarget ) > 4 ) 
					{
						//it is, so put fields in between them and us
						Map map = m_Mobile.Map;

						if( map == null )
						{
							targ.Cancel( m_Mobile, TargetCancelType.Canceled );
							return true;
						}

						int rx = (m_Mobile.X - toTarget.X)/2;//half way distance
						int ry = (m_Mobile.Y - toTarget.Y)/2;
						
						int px = m_Mobile.X + rx;
						int py = m_Mobile.Y + ry;
						int pz = map.GetAverageZ(px, py);

						int teleRange = targ.Range;

						if( teleRange < 0 )
							teleRange = Core.ML ? 11 : 12;
						
						Point3D point = new Point3D(px, py, pz);
						LandTarget lt = new LandTarget( point, map );
							
						if (!(m_Mobile.GetDistanceToSqrt( toTarget ) > teleRange) && m_Mobile.InLOS( lt ) && map.CanSpawnMobile( px, py, pz ) && !SpellHelper.CheckMulti( point, map ) )
						{
								targ.Invoke( m_Mobile, lt );
								return true;
						}
					}
					else //they are on top of me just cast it on them
					{
						LandTarget lt = new LandTarget( toTarget.Location, toTarget.Map );
						Map map = toTarget.Map;
						int teleRange = targ.Range;
							
						if (!(m_Mobile.GetDistanceToSqrt( toTarget ) > teleRange) && m_Mobile.InLOS( lt ) && map.CanSpawnMobile( toTarget.X, toTarget.Y, toTarget.Z ) && !SpellHelper.CheckMulti( toTarget.Location, toTarget.Map ) )
						{
								targ.Invoke( m_Mobile, lt );
								return true;
						}
					}
						
				}
			}
			else if ( isAnimate )
			{
				Item corpse = FindCorpseToAnimate();				

				if ( corpse != null )
					targ.Invoke( m_Mobile, corpse );

				toTarget = null;
			}
			else
			{
				if (m_Mobile.Combatant == null)
					FindNewEnemy();
					
				toTarget = m_Mobile.Combatant;

				if( toTarget != null )
					DistanceCheck( toTarget );
				
			}

			if( ( targ.Flags & TargetFlags.Harmful ) != 0 && toTarget != null )
			{
				if( ( targ.Range == -1 || m_Mobile.InRange( toTarget, targ.Range ) ) && m_Mobile.CanSee( toTarget ) && m_Mobile.InLOS( toTarget ) )
				{
					targ.Invoke( m_Mobile, toTarget );
				}
				else if( isDispel )
				{
					targ.Cancel( m_Mobile, TargetCancelType.Canceled );
				}
			}
			else if( ( targ.Flags & TargetFlags.Beneficial ) != 0 )
			{
				targ.Invoke( m_Mobile, m_Mobile );
			}
			else if( isTeleport && toTarget != null )
			{
				Map map = m_Mobile.Map;

				if( map == null )
				{
					targ.Cancel( m_Mobile, TargetCancelType.Canceled );
					return true;
				}

				int px, py;

				if( teleportAway )
				{
					int rx = m_Mobile.X - toTarget.X;
					int ry = m_Mobile.Y - toTarget.Y;

					double d = m_Mobile.GetDistanceToSqrt( toTarget );

					px = toTarget.X + (int)( rx * ( 10 / d ) );
					py = toTarget.Y + (int)( ry * ( 10 / d ) );
				}
				else
				{
					px = toTarget.X;
					py = toTarget.Y;
				}

				for( int i = 0; i < m_Offsets.Length; i += 2 )
				{
					int x = m_Offsets[ i ], y = m_Offsets[ i + 1 ];

					Point3D p = new Point3D( px + x, py + y, 0 );

					LandTarget lt = new LandTarget( p, map );

					if( ( targ.Range == -1 || m_Mobile.InRange( p, targ.Range ) ) && m_Mobile.InLOS( lt ) && map.CanSpawnMobile( px + x, py + y, lt.Z ) && !SpellHelper.CheckMulti( p, map ) )
					{
						targ.Invoke( m_Mobile, lt );
						
						if (SmartAI && m_Mobile.Mana > 50 && Utility.RandomDouble() < 0.15 )
							TeleportCombo(toTarget);
							
						return true;
					}
				}

				int teleRange = targ.Range;

				if( teleRange < 0 )
					teleRange = Core.ML ? 11 : 12;

				for( int i = 0; i < 10; ++i )
				{
					Point3D randomPoint = new Point3D( m_Mobile.X - teleRange + Utility.Random( teleRange * 2 + 1 ), m_Mobile.Y - teleRange + Utility.Random( teleRange * 2 + 1 ), 0 );

					LandTarget lt = new LandTarget( randomPoint, map );

					if( m_Mobile.InLOS( lt ) && map.CanSpawnMobile( lt.X, lt.Y, lt.Z ) && !SpellHelper.CheckMulti( randomPoint, map ) )
					{
						targ.Invoke( m_Mobile, new LandTarget( randomPoint, map ) );
						
						if (SmartAI && m_Mobile.Mana > 50 && Utility.RandomDouble() < 0.15 )
							TeleportCombo(toTarget);
							
						return true;
					}
				}

				targ.Cancel( m_Mobile, TargetCancelType.Canceled );
			}
			else
			{
				targ.Cancel( m_Mobile, TargetCancelType.Canceled );
			}

			return true;
		}
	}
}
