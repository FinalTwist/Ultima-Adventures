using System;
using Server.Items;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Eighth;
//using Server.Spells.Mystic;
//using Server.Spells.Spellweaving;
using System.Collections.Generic;

namespace Server.Mobiles
{
    //TODO maybe launch a summon upon creation, or if they don't have one?
    public partial class SummonerAI : OmniAI
    {      
        //else it will just act like a regular omni mage
        public override bool m_IsSummoner { get { return true; } }
        
        //familiars don't target like other summons, and animate dead attack other mobs, so they
        //don't get used by default. 
        private bool m_UseFamiliar = false;
        private bool m_UseAnimateDead = false;
        
        //private bool m_IsEmpowered = false;
        private bool m_HasFamiliar = false;
    
        public SummonerAI(BaseCreature m, bool useDead, bool useFam) : base (m)
        {
            this.m_UseFamiliar = useFam;
            this.m_UseAnimateDead = useDead;
        }
    
        private class ExpireTimer : Timer
        {
            private readonly SummonerAI m_AI;
            public ExpireTimer(SummonerAI ai, TimeSpan delay) : base(delay)
            {
                this.m_AI = ai;
            }

            //protected override void OnTick()
            //{
                //this.m_AI.m_IsEmpowered = false;
            //}
        }
        
        public override void SummonerPower()
        {
            Spell spell = null;
            spell = this.GetSummonerSpell();
            
            if (spell != null)
                spell.Cast();
                
            return;
        }
        
        public Spell GetSummonerSpell()
        {
            List<int> skill = new List<int>();
            if (this.m_CanUseMagery)
                skill.Add(0);
            if (this.m_CanUseNecromancy)
                skill.Add(1);
            //if (this.m_CanUseMystic)
            //    skill.Add(2);
                
            //why are they a summoner again?
            if (skill.Count == 0)
                return null;
                
            //maybe should only do if a summon is already cast, ie use it as cover?
            //if (!this.m_IsEmpowered && this.m_CanUseSpellweaving)
            //{
             //   this.m_IsEmpowered = true;
             //   //TODO factor in focus level
             //   double spellweaving = this.m_Mobile.Skills[SkillName.Spellweaving].Value;
             //   TimeSpan duration = TimeSpan.FromSeconds(15 + (int)(spellweaving / 24) + 2);
             //   Timer timer = new ExpireTimer(this, duration);
             //   timer.Start();
             //   return new ArcaneEmpowermentSpell(this.m_Mobile, null);
            //}
            
            //familiars never seem to target like other summons
            if (this.m_CanUseNecromancy && !this.m_HasFamiliar && this.m_UseFamiliar)
            {
                //reset this if familar dies?
                this.m_HasFamiliar = true;
                this.CreateNecromancerFamiliar();
                return null;
            }
            
            int slots = this.m_Mobile.FollowersMax - this.m_Mobile.Followers;
            if (slots < 2)
            {
                //try to animate dead
                if (this.m_CanUseNecromancy && this.m_UseAnimateDead)
                    return new AnimateDeadSpell(this.m_Mobile, null);
                else
                    return null;
            }
            
            //if they are not slot constrained, summon something random. would get boring to fight demons
            //or rising colossi over and over
            if (slots >= 5)
            {
                List<Spell> spells = new List<Spell>();
                if (this.m_CanUseMagery)
                {
                    spells.Add(new SummonDaemonSpell(this.m_Mobile, null));
                    spells.Add(new FireElementalSpell(this.m_Mobile, null));
                    spells.Add(new AirElementalSpell(this.m_Mobile, null));
                    spells.Add(new EarthElementalSpell(this.m_Mobile, null));
                    spells.Add(new WaterElementalSpell(this.m_Mobile, null));
                    spells.Add(new EnergyVortexSpell(this.m_Mobile, null));
                }
                if (this.m_CanUseNecromancy)
                {
                    spells.Add(new VengefulSpiritSpell(this.m_Mobile, null));
                    spells.Add(new VengefulSpiritSpell(this.m_Mobile, null));
                    spells.Add(new VengefulSpiritSpell(this.m_Mobile, null));
                    if (this.m_UseAnimateDead)
                    {
                        spells.Add(new AnimateDeadSpell(this.m_Mobile, null));
                        spells.Add(new AnimateDeadSpell(this.m_Mobile, null));
                    }
                }
                //if (this.m_CanUseMystic)
                //{
                //    spells.Add(new RisingColossusSpell(this.m_Mobile, null));
                //    spells.Add(new RisingColossusSpell(this.m_Mobile, null));
                //    spells.Add(new RisingColossusSpell(this.m_Mobile, null));
                //}
            
                int picked = Utility.Random(spells.Count);
                return spells[picked];
            }
            else
            {
                int summon = 0;
                switch(slots)
                {
                    case 4:
                        summon = Utility.Random(1);
                        if (summon == 0)
                            return new SummonDaemonSpell(this.m_Mobile, null);
                        else
                            return new FireElementalSpell(this.m_Mobile, null);
                    case 3:
                        summon = Utility.Random(1);
                        if (summon == 0 && this.m_CanUseNecromancy)
                            return new VengefulSpiritSpell(this.m_Mobile, null);
                        else
                            return new WaterElementalSpell(this.m_Mobile, null);
                    case 2:
                        summon = Utility.Random(2);
                        if (summon == 0)
                            return new EarthElementalSpell(this.m_Mobile, null);
                        else if (summon == 1)
                            return new AirElementalSpell(this.m_Mobile, null);
                        else
                            return new EnergyVortexSpell(this.m_Mobile, null);
                    default:
                        return null;
                }
            }    
        }
    }
}
