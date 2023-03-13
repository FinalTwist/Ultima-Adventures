//Created by Peoharen
using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Song;
using Server.Targeting;
using Server.SkillHandlers;

namespace Server.Mobiles
{
    public partial class OmniAI : BaseAI
    {
        private DateTime m_NextAnimateTime = DateTime.UtcNow;
        private double m_AnimateDelay = 5.0;
        private double m_AnimateFinish = 2.0;
        public void BardPower()
        {
            if (this.m_Mobile.Debug)
                this.m_Mobile.Say(1162, "");

            this.CheckInstrument();           
            switch( Utility.Random(3) )
            {
                case 0:
                    this.UseDiscord();
                    break;
                case 1:
                    this.UseProvocation();
                    break;
                case 2:
                    this.UsePeacemaking();
                    break;
                default:
                     // this.UseSongSpell();
                    break;
            }

            return;
        }

        public bool CheckInstrument()
        {
            BaseInstrument inst = BaseInstrument.GetInstrument(this.m_Mobile);

            if (inst != null)
                return true;

            if (this.m_Mobile.Debug)
                this.m_Mobile.Say(1162, "I need an instrument, fixing my problem.");

            if (this.m_Mobile.Backpack == null)
                return false;

            inst = (BaseInstrument)this.m_Mobile.Backpack.FindItemByType(typeof(BaseInstrument));

            if (inst == null)
            {
                inst = new Harp();
                inst.SuccessSound = 0x58B;
                inst.FailureSound = 0x58C;
                // Got Better Music?
                // inst.DiscordSound = inst.PeaceSound = 0x58B;
                // inst.ProvocationSound = 0x58A;
            }

            BaseInstrument.SetInstrument(this.m_Mobile, inst);
            return true;
        }

        #region songspell
        public void UseSongSpell() {
            Spell spell = this.GetRandomBardSpell();
            spell.Cast();
            this.m_NextCastTime = DateTime.UtcNow + spell.GetCastDelay() + spell.GetCastRecovery();
            return;
        }
        protected virtual Spell CheckCastArmysPaeonSong()
        {
            // If I'm poisoned, always attempt to cure.
            if( m_Mobile.Poisoned )
                return null;
            else if ( DateTime.UtcNow < m_NextHealTime )
                return null;

            Spell spell = null;

            if( m_Mobile.Hits < ( m_Mobile.HitsMax - 50 ) )
            {
                spell = new ArmysPaeonSong( m_Mobile, null );
            }

            double delay;

            if( m_Mobile.Int >= 500 )
                delay = Utility.RandomMinMax( 7, 10 );
            else
                delay = Math.Sqrt( 600 - m_Mobile.Int );

            m_NextHealTime = DateTime.UtcNow + TimeSpan.FromSeconds( delay );

            return spell;
        }
        public virtual Spell GetRandomBardSpell()
        {
            // WORK IN PROGRESS, Bard AI cant use the new bard spells yet, new targeting requirements
            Spell armysPaeonSong = this.CheckCastArmysPaeonSong();
            if (armysPaeonSong != null) {
                return armysPaeonSong;
            }
            int possibles = 10;
            switch( Utility.Random( possibles ) )
            {
                default: m_Mobile.DebugSay( "Foe Requiem" ); return new FoeRequiemSong( m_Mobile, null );
                case 0: m_Mobile.DebugSay( "Energy Threnody" ); return new EnergyThrenodySong( m_Mobile, null );
                case 1: m_Mobile.DebugSay( "Fire Threnody" ); return new FireThrenodySong( m_Mobile, null );
                case 2: m_Mobile.DebugSay( "Ice Threnody" ); return new IceThrenodySong( m_Mobile, null );
                case 3: m_Mobile.DebugSay( "Poison Threnody" ); return new PoisonThrenodySong( m_Mobile, null );
                case 4: m_Mobile.DebugSay( "Poison Carol" ); return new FireCarolSong( m_Mobile, null );
                case 5: m_Mobile.DebugSay( "Ice Carol" ); return new IceCarolSong( m_Mobile, null );
                case 6: m_Mobile.DebugSay( "Fire Carol" ); return new FireCarolSong( m_Mobile, null );
                case 7: m_Mobile.DebugSay( "Energy Carol" ); return new EnergyCarolSong( m_Mobile, null );            
             }
        }
        #endregion

        #region discord
        public void UseDiscord()
        {
            Mobile target = this.m_Mobile.Combatant;

            if (target == null)
                return;

            if (!this.m_Mobile.UseSkill(SkillName.Discordance))
                return;

            if (this.m_Mobile.Debug)
                this.m_Mobile.Say(1162, "Discording");

            // Discord's target flag is harmful so the AI already targets it's combatant.
            // However players are immune to Discord hence the following.
            if (target is PlayerMobile)
            {
                Item pants = target.FindItemOnLayer( Layer.Pants );
                if (pants != null && pants is LegsOfMusicalPanache)
                    return;

                TimeSpan duration = TimeSpan.FromSeconds(this.m_Mobile.Skills[SkillName.Discordance].Value / 2);
                ResistanceMod[] mods = 
                {
                    new ResistanceMod( ResistanceType.Physical, Discordance.ReduceValue(this.m_Mobile, target, target.GetResistance(ResistanceType.Physical)) ),
                    new ResistanceMod( ResistanceType.Cold, Discordance.ReduceValue(this.m_Mobile, target, target.GetResistance(ResistanceType.Cold)) ),
                    new ResistanceMod( ResistanceType.Poison, Discordance.ReduceValue(this.m_Mobile, target, target.GetResistance(ResistanceType.Poison)) ),
                    new ResistanceMod( ResistanceType.Energy, Discordance.ReduceValue(this.m_Mobile, target, target.GetResistance(ResistanceType.Energy)) ),
                    new ResistanceMod( ResistanceType.Fire, Discordance.ReduceValue(this.m_Mobile, target, target.GetResistance(ResistanceType.Fire)) )
                };
                TimedResistanceMod.AddMod(target, "Discordance", mods, duration);
                target.AddStatMod(new StatMod(StatType.Str, "DiscordanceStr", Discordance.ReduceValue(this.m_Mobile, target, target.RawStr), duration));
                target.AddStatMod(new StatMod(StatType.Int, "DiscordanceInt", Discordance.ReduceValue(this.m_Mobile, target, target.RawInt), duration));
                target.AddStatMod(new StatMod(StatType.Dex, "DiscordanceDex", Discordance.ReduceValue(this.m_Mobile, target, target.RawDex), duration));
                new DiscordEffectTimer(target, duration).Start();
            }
        }

        public class DiscordEffectTimer : Timer
        {
            public Mobile Mob;
            public int Count;
            public int MaxCount;

            public DiscordEffectTimer(Mobile mob, TimeSpan duration)
                : base(TimeSpan.FromSeconds(1.25), TimeSpan.FromSeconds(1.25))
            {
                this.Mob = mob;
                this.Count = 0;
                this.MaxCount = (int)((double)duration.TotalSeconds / 1.25);
            }

            protected override void OnTick()
            {
                if (this.Count >= this.MaxCount)
                    this.Stop();
                else
                {
                    this.Mob.FixedEffect(0x376A, 1, 32);
                    this.Count++;
                }
            }
        }
        #endregion

        public bool UseProvocation()
        {
            if (!this.m_Mobile.UseSkill(SkillName.Provocation))
                return false;
            else if (this.m_Mobile.Target != null)
                this.m_Mobile.Target.Cancel(this.m_Mobile, TargetCancelType.Canceled);

            Mobile target = this.m_Mobile.Combatant;

            if (this.m_Mobile.Combatant is BaseCreature)
            {
                BaseCreature bc = this.m_Mobile.Combatant as BaseCreature;
                target = bc.GetMaster();

                if (target != null && bc.CanBeHarmful(target))
                {
                    if (this.m_Mobile.Debug)
                        this.m_Mobile.Say(1162, "Provocation: Pet to Master");

                    bc.Provoke(this.m_Mobile, target, true);
                    return true;
                }
            }

            List<BaseCreature> list = new List<BaseCreature>();

            foreach (Mobile m in this.m_Mobile.GetMobilesInRange(5))
            {
                if (m != null && m is BaseCreature && m != this.m_Mobile)
                {
                    BaseCreature bc = m as BaseCreature;

                    if (this.m_Mobile.Controlled != bc.Controlled)
                        continue;

                    if (this.m_Mobile.Summoned != bc.Summoned)
                        continue;

                    list.Add(bc);
                }
            }

            if (list.Count == 0)
                return false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CanBeHarmful(target))
                {
                    if (this.m_Mobile.Debug)
                        this.m_Mobile.Say(1162, "Provocation: " + list[i].Name + " to " + target.Name);

                    list[i].Provoke(this.m_Mobile, target, true);
                    return true;
                }
            }

            return false;
        }

        public void UsePeacemaking()
        {
            if (!this.m_Mobile.UseSkill(SkillName.Peacemaking))
                return;

            if (this.m_Mobile.Combatant is PlayerMobile )
            {
                if (this.m_Mobile.Debug)
                    this.m_Mobile.Say(1162, "Peacemaking: Player");

                PlayerMobile pm = this.m_Mobile.Combatant as PlayerMobile;

                if ( pm.Troubadour())
                {
                    Item pants = pm.FindItemOnLayer( Layer.Pants );
                    if (pants != null && pants is LegsOfMusicalPanache)
                        return;
                }

                    if (pm.PeacedUntil <= DateTime.UtcNow)
                    {
                        pm.PeacedUntil = DateTime.UtcNow + (TimeSpan.FromSeconds((int)(this.m_Mobile.Skills[SkillName.Peacemaking].Value / 6)));
                        pm.SendLocalizedMessage(500616); // You hear lovely music, and forget to continue battling!                 
                    }
                    if (pm.PeacedUntil > (DateTime.UtcNow + TimeSpan.FromSeconds( 30 )) )
                        pm.PeacedUntil = DateTime.UtcNow + TimeSpan.FromSeconds( 30 );
            }
            else if (this.m_Mobile.Target != null)
            {
                if (this.m_Mobile.Debug)
                    this.m_Mobile.Say(1162, "Peacemaking");

                this.m_Mobile.Target.Invoke(this.m_Mobile, this.m_Mobile.Combatant);
            }
        }
    }
}
