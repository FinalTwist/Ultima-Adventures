// Custom Enhanced MAGE AI (by Hawkeye Pike, Feb 02 2010)
//
// Add the new AIType to the list in ChangeAIType( AIType NewAI ) in BaseCreature.cs
// Add the new AIType to the namespace Server.Mobiles in BaseAI.cs
// Assign the AIType to the class of the creature (e.g. public OrcishMage() : base(AIType.AI_Mage, ...)
// Enter into the Spawner, for example, "OrcishMage/ai/AI_Mage2"
//
// Features:
// - Improved pathfinding (keeps distance to enemy, tries to return to home zone)
// - Improved spells (casts higher level spells, more intelligent casting)
// - Intelligent self medication (curing and healing)

using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Misc;
using Server.Regions;
using Server.SkillHandlers;

namespace Server.Mobiles
{
	public class Mage2AI : BaseAI
	{
        public Mage2AI(BaseCreature m) : base(m)
		{
		}

        // Setting variables:
        private DateTime m_NextCastTime;
        Spell myspell;
        String remark;

        public override bool Think()
        {
            if (m_Mobile.Deleted)
                return false;

            if (ProcessTarget())
                return true;
            else
                return base.Think();
        }



        //-------------------Declaration of Functions-------------------



        private int RNG(int Max, int Current)
        // Returns random numbers between 1 and 70.
        // The numbers decrease more rapidly, the closer "Current" gets to 0.
        // This works, no matter how high the Max value is.
        // I.e. this function can be used as a random trigger for current skill, mana, hits etc.
        {
            int tempvar;
            tempvar = (int)(100 * System.Math.Sqrt(Current) / System.Math.Sqrt(3 * Max - Current)); // Yields numbers 70...0
            return (Utility.Random(tempvar)+1);
        }

        private TimeSpan CastDelay()
        // Sets random Timespan as a casting delay, depending on my magery skill:
        {
            double delay;
            if (m_Mobile.Skills[SkillName.Magery].Value > 100)
                delay = 1; // delay = 1...2 seconds
            if (m_Mobile.Skills[SkillName.Magery].Value > 80)
                delay = Utility.RandomMinMax(1, 2); // delay = 1...2 seconds
            else delay = Utility.RandomMinMax(2, 4); // delay = 2...4 seconds
            return TimeSpan.FromSeconds(delay);
        }



        // Healing back up when wandering or on guard:
        private Spell HealBackUp()
        {
            Spell healSpell = null;

            // Skip healing if delay too short:
            if (DateTime.UtcNow < m_NextCastTime)
                return null;

            // If I'm poisoned, always attempt to cure:
            if (m_Mobile.Poisoned)
            {
                healSpell = new CureSpell(m_Mobile, null);
            }
            else if ( (m_Mobile.Hits < m_Mobile.HitsMax) && (m_Mobile.Mana > 20) && !m_Mobile.Controlled )
            {
                healSpell = new GreaterHealSpell(m_Mobile, null);
                // If for any reason (e.g. low Magery skill) GreaterHeal can't be chosen, use Heal:
                if (healSpell == null)
                    healSpell = new HealSpell(m_Mobile, null);
            }

            // Healing delay in seconds:
            m_NextCastTime = DateTime.UtcNow + CastDelay();

            return healSpell;
        }



        // Check if I need healing or curing:
		private Spell GetHealingSpell()
		{
            Spell healSpell = null;

            // Skip healing if delay too short:
            if (DateTime.UtcNow < m_NextCastTime)
                return null;

            // If I'm poisoned, always attempt to cure:
            if (m_Mobile.Poisoned)
            {
                healSpell = new CureSpell(m_Mobile, null);
            }

            // If my hits are >30, enemy's hits <30, my magery skill > 90, 60% chance to skip healing and cast damage spell instead:
            else if ( (m_Mobile.Combatant != null) && (!m_Mobile.Combatant.Deleted) && (m_Mobile.Combatant.Alive) &&
                (m_Mobile.Combatant.Hits < 30) && (m_Mobile.Hits > 30) && (m_Mobile.Skills[SkillName.Magery].Value > 90) && (Utility.Random(3) == 0))
            {
                switch (Utility.Random(3))
                {
                    case 0: healSpell = new EnergyBoltSpell(m_Mobile, null); break;
                    case 1: healSpell = new ExplosionSpell(m_Mobile, null); break;
                    default: healSpell = new FlameStrikeSpell(m_Mobile, null); break;
                }
            }

            // Assign Heal spell:
            else
            {
                // If I'm a summoned creature never heal myself:
                if (m_Mobile.Summoned)
                return null;

                // If my hits are <10%, 30% chance to cast Invisibility:
                if ((m_Mobile.Hits < m_Mobile.HitsMax / 10) && (Utility.Random(3) == 0))
                {
                    healSpell = new InvisibilitySpell(m_Mobile, null);
                    return healSpell;
                }

                // Chance to skip healing depends on hit points:
                if (m_Mobile.Hits >= 100)
                {
                    int chance = (int)(Math.Sqrt(m_Mobile.Hits) / Math.Sqrt(m_Mobile.HitsMax / 100) * 100);
                    if (Utility.Random(1000 / chance) < 10)
                        return null;
                }

                // At health < 100, chance to skip healing is calculated differently (linear):
                if ( m_Mobile.Hits < 100 )
                {
                    int chance = ( (m_Mobile.Hits + 1) /2 ); // +1 added by Falkor to prevent div by zero potential

			if (chance < 1) 
				{ 
				chance = 1; 
				} // Added to make doubly sure there is no zero division happening

                    if (Utility.Random(1000/chance) < 10)
                        return null;
                }

                // If Hit Points not full, heal:
                if (m_Mobile.Hits < (m_Mobile.HitsMax - 5))
                {
                    healSpell = new GreaterHealSpell(m_Mobile, null);
                    // If for any reason (e.g. low Magery skill) GreaterHeal can't be chosen, use Heal:
                    if (healSpell == null)
                        healSpell = new HealSpell(m_Mobile, null);
                }
            }

            // Healing delay in seconds:
			m_NextCastTime = DateTime.UtcNow + CastDelay();

            return healSpell;
		}



        // Select damage spell to cast:
		private Spell GetDamageSpell()
		{
            int maxCircle = 1;
            Spell damageSpell = null;

            // Skip healing if delay too short:
            if (DateTime.UtcNow < m_NextCastTime)
                return null;
            
            // Determine which circle of magery (1-8) I can cast, depending on my magery skill (0-120):
            if (m_Mobile.Skills[SkillName.Magery].Value < 10)
                maxCircle = 2;
            else if (m_Mobile.Skills[SkillName.Magery].Value < 24)
                maxCircle = 3;
            else if (m_Mobile.Skills[SkillName.Magery].Value < 38)
                maxCircle = 4;
            else if (m_Mobile.Skills[SkillName.Magery].Value < 52)
                maxCircle = 5;
            else if (m_Mobile.Skills[SkillName.Magery].Value < 66)
                maxCircle = 6;
            else if (m_Mobile.Skills[SkillName.Magery].Value < 88)
                maxCircle = 7;
            else maxCircle = 8;

            // Determine a random spell to cast, depending on the maxCircle I can cast:
            switch (Utility.Random( maxCircle ) * 2)
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

            if (m_Mobile.Combatant is PlayerMobile)
            {
                PlayerMobile player = (PlayerMobile)m_Mobile.Combatant;
                Item twohanded = m_Mobile.Combatant.FindItemOnLayer( Layer.TwoHanded );			
			    Item firstvalid = m_Mobile.Combatant.FindItemOnLayer( Layer.FirstValid );

                if ( ( twohanded is BaseRanged || firstvalid is BaseRanged || twohanded is BaseMeleeWeapon || firstvalid is BaseMeleeWeapon ) && !( m_Mobile.Combatant.Paralyzed || m_Mobile.Combatant.Frozen) && maxCircle >= 5 && Utility.RandomDouble() > 0.55)
                    damageSpell = new ParalyzeSpell(m_Mobile, null); 

                else if ( player.Hidden && maxCircle >= 6 && Utility.RandomDouble() > 0.66 && m_Mobile.InRange( player,  m_Mobile.RangePerception) )
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
                    }
                }


            }
            
            // Casting delay in seconds:
            m_NextCastTime = DateTime.UtcNow + CastDelay();

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
        
        // Dispel selected enemy:
        private Spell DoDispel(Mobile toDispel)
        {
            // If my magery skill is > 90, 50% chance to dispel, else 25% chance.
            if ((m_Mobile.Skills[SkillName.Magery].Value > 90) && (Utility.Random(2) == 0))
                return new DispelSpell(m_Mobile, null);
            else if (Utility.Random(4) == 0)
                return new DispelSpell(m_Mobile, null);
            else
                return null;
        }



        // Define what kinds of creatures can be dispeled:
        public bool CanDispel(Mobile m)
        {
            return (m is BaseCreature && ((BaseCreature)m).Summoned && m_Mobile.CanBeHarmful(m, false) && !((BaseCreature)m).IsAnimatedDead);
        }



        // Find the target that is to be dispeled:
        public Mobile FindDispelTarget(bool activeOnly)
        {
            // Check if I cannot dispel enemy:
            if (m_Mobile.Deleted || m_Mobile.Int < 95 || CanDispel(m_Mobile) || m_Mobile.AutoDispel)
                return null;

            if (activeOnly)
            {
                List<AggressorInfo> aggressed = m_Mobile.Aggressed;
                List<AggressorInfo> aggressors = m_Mobile.Aggressors;

                Mobile active = null;
                double activePrio = 0.0;

                // Check if enemy still is there and no pet:
                if (m_Mobile.Combatant != null && !m_Mobile.Combatant.Deleted && m_Mobile.Combatant.Alive && !m_Mobile.Combatant.IsDeadBondedPet && m_Mobile.InRange(m_Mobile.Combatant, Core.ML ? 10 : 12) && CanDispel(m_Mobile.Combatant))
                {
                    active = m_Mobile.Combatant;
                    activePrio = m_Mobile.GetDistanceToSqrt(m_Mobile.Combatant);

                    if (activePrio <= 2)
                        return active;
                }

                for (int i = 0; i < aggressed.Count; ++i)
                {
                    AggressorInfo info = aggressed[i];
                    Mobile m = info.Defender;

                    if (m != m_Mobile.Combatant && m.Combatant == m_Mobile && m_Mobile.InRange(m, Core.ML ? 10 : 12) && CanDispel(m))
                    {
                        double prio = m_Mobile.GetDistanceToSqrt(m);

                        if (active == null || prio < activePrio)
                        {
                            active = m;
                            activePrio = prio;

                            if (activePrio <= 2)
                                return active;
                        }
                    }
                }

                for (int i = 0; i < aggressors.Count; ++i)
                {
                    AggressorInfo info = aggressors[i];
                    Mobile m = info.Attacker;

                    if (m != m_Mobile.Combatant && m.Combatant == m_Mobile && m_Mobile.InRange(m, Core.ML ? 10 : 12) && CanDispel(m))
                    {
                        double prio = m_Mobile.GetDistanceToSqrt(m);

                        if (active == null || prio < activePrio)
                        {
                            active = m;
                            activePrio = prio;

                            if (activePrio <= 2)
                                return active;
                        }
                    }
                }

                return active;
            }
            else
            {
                Map map = m_Mobile.Map;

                if (map != null)
                {
                    Mobile active = null, inactive = null;
                    double actPrio = 0.0, inactPrio = 0.0;

                    if (m_Mobile.Combatant != null && !m_Mobile.Combatant.Deleted && m_Mobile.Combatant.Alive && !m_Mobile.Combatant.IsDeadBondedPet && CanDispel(m_Mobile.Combatant))
                    {
                        active = inactive = m_Mobile.Combatant;
                        actPrio = inactPrio = m_Mobile.GetDistanceToSqrt(m_Mobile.Combatant);
                    }

                    foreach (Mobile m in m_Mobile.GetMobilesInRange(Core.ML ? 10 : 12))
                    {
                        if (m != m_Mobile && CanDispel(m))
                        {
                            double prio = m_Mobile.GetDistanceToSqrt(m);

                            if (!activeOnly && (inactive == null || prio < inactPrio))
                            {
                                inactive = m;
                                inactPrio = prio;
                            }

                            if ((m_Mobile.Combatant == m || m.Combatant == m_Mobile) && (active == null || prio < actPrio))
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



        // Choosing the target for my spell:
        private bool ProcessTarget()
        {
            Target targ;
            // Setting target to current target:
            targ = m_Mobile.Target;

            // No target:
            if (targ == null)
                return false;

            Mobile makeTarget;

            makeTarget = m_Mobile.Combatant;
            // If there is a valid target...
            if ( ((targ.Flags & TargetFlags.Harmful) != 0) && (makeTarget != null) )
            {
                // If enemy is close enough, and if I can see it, and if it is in line of sight, make it a target:
                if ((targ.Range == -1 || m_Mobile.InRange(makeTarget, targ.Range)) && m_Mobile.CanSee(makeTarget) && m_Mobile.InLOS(makeTarget))
                {
                    targ.Invoke(m_Mobile, makeTarget);
                }
                else
                {
                    targ.Cancel(m_Mobile, TargetCancelType.Canceled);
                }
            }
            else if ((targ.Flags & TargetFlags.Beneficial) != 0)
            {
                targ.Invoke(m_Mobile, m_Mobile);
            }
            else
            {
                targ.Cancel(m_Mobile, TargetCancelType.Canceled);
            }
            return true;
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




        //------------------Definition of Activities-------------------



        // Define what to do when wandering around:
        public override bool DoActionWander()
        {
            m_Mobile.Warmode = false;

            // If creature is an enemy, set it to Combatant and trigger Combat action:
            if (AcquireFocusMob(m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true))
            {
				/*
                // Psychological warfare:
                switch (Utility.Random(50))
                {
                    case 0: case 1: remark = "Have at thee!"; break;
                    case 2: case 3: remark = "Thou shalt be slain!"; break;
                    case 4: case 5: remark = "Thou wilt feel my wrath!"; break;
                    case 6: remark = "Surrender, or I shall burn thee, fry thee, cook thee, and feed thee to the wolves!"; break;
                    default: remark = ""; break;
                }
                if (remark != "")
                    m_Mobile.Say(remark);*/

                m_Mobile.Combatant = m_Mobile.FocusMob;
                Action = ActionType.Combat;
            }

            // Else if my hit points not at maximum, heal:
            else if ( (m_Mobile.Hits < m_Mobile.HitsMax) )
            {
                myspell = HealBackUp();
                if (myspell != null)
                    myspell.Cast();
            }

            // Else if my mana not at maximum, meditate:
            else if (m_Mobile.Mana < m_Mobile.ManaMax)
                m_Mobile.UseSkill(SkillName.Meditation);

            // Else go out of war mode and continue wandering:
            else
            {
                m_Mobile.Warmode = false;
                return base.DoActionWander();
            }

            return true;
        }



        // What happens during combat:
        public override bool DoActionCombat()
        {
            // Go into war mode:
			m_Mobile.Warmode = true;

			// If enemy is deleted, dead, invisible or can't be harmed, go back on guard:
            if (m_Mobile.Combatant == null || m_Mobile.Combatant.Deleted || !m_Mobile.Combatant.Alive || m_Mobile.Combatant.IsDeadBondedPet)
            {
                // Psychological warfare:
               /* switch (Utility.Random(50))
                {
                    case 0: remark = "Thou didst get what thou deservest."; break;
                    case 1: case 2: remark = "Was that all thou couldst come up with, thou wimp?"; break;
                    case 3: case 4: remark = "Fighting thee almost takes the honor out of victory!"; break;
                    case 5: remark = "So thou didst die. It matters not."; break;
                    case 6: remark = "May thy remains rot, thy bones be stolen by dogs, and thy guts be eaten by rats!"; break;
                    default: remark = ""; break;
                }
                if (remark != "")
                    m_Mobile.Say(remark);*/

                Action = ActionType.Guard;
                return true;
            }

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

            // If enemy has moved recently...:
            if ((Core.TickCount - m_Mobile.LastMoveTime) > 1000 || m_Mobile.AIFullSpeedActive)
            {
                // Walk back towards home (60% chance not to move, high chance to change direction, # steps to walk):
                // (Only creatures with positive karma will try to fight near their spawn point)
                if ((Utility.Random(3) == 0) && (m_Mobile.Karma >= 0) )
                    WalkBack(2, 0, 7);
                
                // Walk to get a distance of 8...10 steps between me and my enemy:
                if (WalkMobileRange(m_Mobile.Combatant, 2, true, 7, 9))
                {
                    // Turn and face the enemy:
                    m_Mobile.Direction = m_Mobile.GetDirectionTo(m_Mobile.Combatant.Location);

                }
                else
                {
                    //If enemy out of range:
                    if (m_Mobile.Combatant != null)
                    {
                        if ((int)m_Mobile.GetDistanceToSqrt(m_Mobile.Combatant) > m_Mobile.RangePerception + 1)
                        {
                            // Enemy is lost. Return to guard.
                            m_Mobile.Combatant = null;
                            Action = ActionType.Guard;
                            return true;
                        }
						else 
							WalkRandom(0, 1, 1); // Final, can no longer be cornered!
                    }
                }
            }

            // Healing/Curing:
            myspell = GetHealingSpell();
            if (myspell != null)
            {
                myspell.Cast();
            }

            // Prepare casting if it is time and the enemy is in range:
            if (m_Mobile.Spell == null && DateTime.UtcNow > m_NextCastTime && m_Mobile.InRange(m_Mobile.Combatant, Core.ML ? 10 : 12))
            {

                Mobile toDispel = FindDispelTarget(true);

                // If a summoned creature is attacking me, try to dispel:
                if (toDispel != null)
                    myspell = DoDispel(toDispel);
                // Else choose damage spell:
                else
                    ProcessTarget();
                    myspell = GetDamageSpell();

                if (myspell != null)
                {
                    myspell.Cast();
                }

                m_NextCastTime = DateTime.UtcNow + CastDelay();
            }

            // At low Mana we should check if we should flee:
            if (m_Mobile.Mana < 20)
            {
                bool bFlee = false;
                if (Utility.Random(0, 100) > 10) // 90% chance to flee
                    bFlee = true;

                if (bFlee)
                    Action = ActionType.Flee;
            }

            // At 20% Health we should check if we should flee:
            if (m_Mobile.Hits < m_Mobile.HitsMax * 0.2)
            {
                bool bFlee = false;

                // If my current hit points are more than my opponent, and I'm above 50HP, low chance to flee:
                if (( m_Mobile.Combatant != null) && (m_Mobile.Hits >= m_Mobile.Combatant.Hits) && (m_Mobile.Hits > 50) && (RNG(m_Mobile.HitsMax, m_Mobile.Hits) < 3 ) )
                        bFlee = true;

                // Else, flee:
                else if ((m_Mobile.Combatant != null))
                {
                    if (Utility.Random(0, 100) > 10) // 90% chance to flee
                        bFlee = true;
                }

                if (bFlee)
                    Action = ActionType.Flee;
            }
			return true;
		}



        public override bool DoActionGuard()
		{
			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				if ( !m_Mobile.Controlled )
				{
                    myspell = HealBackUp();
                    if (myspell != null)
                        myspell.Cast();
                }
				base.DoActionGuard();
			}
			return true;
		}


    }
}
