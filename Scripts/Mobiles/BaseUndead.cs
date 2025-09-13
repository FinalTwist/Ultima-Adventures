using System;
using System.Collections;
using Server.Items;
using Server.Regions;

namespace Server.Mobiles
{
    public class BaseUndead : BaseCreature
    {
        public bool sneaking = false;
        private bool ForcedActive = false;
		
        public override bool AlwaysMurderer { get { return true; } }

			public override bool BleedImmune { get { if (this.CanInfect) {return true;} else {return false;} } }
			public override Poison PoisonImmune { get { if (this.CanInfect) { return Poison.Lethal; }  else { return null; } } }
			public override Poison HitPoison { get { if (this.CanInfect) { return Poison.Lethal; } else {return null;} } }
        public override bool CanRummageCorpses { get { return true; } }
        private RotTimer m_Timer;


        public override bool ClickTitle { get { return false; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Spirit { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool LeaveCorpse { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Nocturnal { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SemiVisible { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool LifeDrain { get; set; }

        public BaseUndead() : base(AIType.AI_Melee, FightMode.Closest, 25, 1, 0.2, 0.4)
        { }

        public BaseUndead(AIType ai, FightMode mode, int iRangePerception, int iRangeFight, double dActiveSpeed, double dPassiveSpeed) : base(ai, mode, iRangePerception, iRangeFight, dActiveSpeed, dPassiveSpeed)
        {
            LeaveCorpse = true;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
            {
                if (!ForcedActive && Blessed)
                {
                    ForcedActive = true;
                    Blessed = false;
                    Hidden = false;
                    sneaking = false;
                    if (NameMod == null) NameMod = Name + " (Forced)";
                }
                else if (ForcedActive)
                {
                    ForcedActive = false;
                    NameMod = null;
                }
                else base.OnDoubleClick(from);
            }
            else base.OnDoubleClick(from);
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            base.AlterMeleeDamageFrom(from, ref damage);
            if (Spirit)
            {
                damage = 0;
            }
        }

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
        {
            if (to is PlayerMobile)
            {
                if (to.AccessLevel >= AccessLevel.GameMaster)
                    damage = 0;
            }
            base.AlterMeleeDamageTo(to, ref damage);
        }

        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
        }

        public override void AlterSpellDamageTo(Mobile to, ref int damage)
        {
            if (to is PlayerMobile)
            {
                if (to.AccessLevel >= AccessLevel.GameMaster)
                    damage = 0;
            }
            base.AlterSpellDamageTo(to, ref damage);
        }

        protected override bool OnMove(Direction d)
        {
            if (!sneaking)
                RevealingAction();
            return base.OnMove(d);
        }

		public override void OnGotMeleeAttack( Mobile attacker )
        {
            base.OnGotMeleeAttack( attacker );

            if ( Utility.RandomMinMax( 1, 4 ) == 1 )
            {
                int goo = 0;

                string Goo = "Poison Splash";
                int Color = 0x3F;

                foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == Goo ){ goo++; } }

                if ( goo == 0 )
                {
                    MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, Goo, Color, 0 );
                }
            }
			if ( (((BaseCreature)this).CanInfect) && 0.10 >= Utility.RandomDouble() )
				ProjectFear();

				if (attacker is BaseCreature)
				{
					BaseCreature mob = (BaseCreature)attacker;
					if (mob.Controlled && mob.ControlMaster is PlayerMobile && !mob.Poisoned && !mob.Hidden && InRange(mob, 3) && InLOS(mob)&& Utility.RandomDouble()> 0.66)
						mob.ApplyPoison( mob, Poison.Deadly );
				}
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if ( (defender is BaseCreature || defender is PlayerMobile )&& Utility.RandomDouble() > 0.75)
            {
                defender.Stam -= defender.Stam / 6;
            }
			
			if ( this.CanInfect && 0.10 >= Utility.RandomDouble() )
				ProjectFear();

				if (defender is BaseCreature)
				{
					BaseCreature mob = (BaseCreature)defender;
					if (mob.Controlled && mob.ControlMaster is PlayerMobile && !mob.Poisoned && !mob.Hidden && InRange(mob, 3) && InLOS(mob)&& Utility.RandomDouble() > 0.66)
						mob.ApplyPoison( mob, Poison.Deadly );
				}
		
        }
		
		public void ProjectFear()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{

					DoHarmful( m );

					m.PlaySound(0x204);
					m.FixedEffect(0x376A, 6, 1);

					int duration = 1;//Utility.RandomMinMax(1, 2);
					m.Paralyze(TimeSpan.FromSeconds(duration));
                    
					m.SendMessage( "The Contageon goes Airborne and tries to take hold!" );
					if (Utility.RandomDouble() < 0.33)
					{
						m.ApplyPoison( this, Poison.Deadly );
						m.SendMessage( "and you get infected!" );
                        if ( (m.Stam - (m.Stam / 2) ) > 0 )
                            m.Stam -= m.Stam / 2;
                        else 
                            m.Stam = 1;
					}
					else
					{
					m.SendMessage( "and you resist the virus!" );
					}
			}
		}
		
        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (!m.Hidden && InRange(m, 8) && InLOS(m))
            {
                if (IsEnemy(m) && m.AccessLevel < AccessLevel.Counselor)
                {
                    Combatant = m;
                    Warmode = true;
                }
            }
            //	//This deals with the Zombie's calls to other zombiex's in the area
            if (((m is BaseCreature && ((BaseCreature)m).CanInfect) || m is Zombiex) && Combatant != null)
            {
                if (m.Combatant == null)
                {
                    m.Combatant = Combatant;
                    m.Say("*Moan*");  // what does the zombie getting called say
                    Say("*MMooaann*"); // what does the zombie doing the calling say
                }
            }

            base.OnMovement(m, oldLocation);
            return;
        }

        public override bool IsEnemy(Mobile m)
        { 
	        if (m is PlayerMobile)
            {
				if (m.AccessLevel >= AccessLevel.GameMaster)
					return false;
				else if (this.Combatant == m || m.Combatant is Zombiex || m.Combatant is DeepDweller || m.Combatant is FailedExperiment)
					return true;
				else if ( ((PlayerMobile)m).IsZen && (m.Direction & Direction.Running) == 0 )
					return false;
				else if ((m == this.SummonMaster) || (m == this.ControlMaster))
					return false;
				else
					return true;
            }
            else if (m is BaseCreature)
            {
                BaseCreature mm = (BaseCreature)m;
                if ( !mm.Summoned && ( mm is BaseUndead || mm.CanInfect || mm is wOphidianWarrior || mm is AcidSlug || mm is wOphidianMatriarch || mm is wOphidianMage || mm is wOphidianKnight || mm is wOphidianArchmage || mm is OphidianWarrior || mm is OphidianMatriarch || mm is OphidianMage || mm is OphidianKnight || mm is OphidianArchmage || mm is MonsterNestEntity || mm is AncientLich || mm is Bogle || mm is LichLord || mm is Shade || mm is Spectre || mm is Wraith || mm is BoneKnight || mm is ZenMorgan || mm is Ghoul || mm is Mummy || mm is SkeletalKnight || mm is Skeleton || mm is Zombie || mm is RevenantLion || mm is RottingCorpse || mm is SkeletalDragon || mm is AirElemental || mm is IceElemental || mm is ToxicElemental || mm is PoisonElemental || mm is FireElemental || mm is WaterElemental || mm is EarthElemental || mm is Efreet || mm is SnowElemental || mm is AgapiteElemental || mm is BronzeElemental || mm is CopperElemental || mm is DullCopperElemental || mm is GoldenElemental || mm is ShadowIronElemental || mm is ValoriteElemental || mm is VeriteElemental || mm is BloodElemental ))
                    return false;
                if ( mm.Summoned && (mm.SummonMaster is PlayerMobile && !IsEnemy(mm.SummonMaster) ) ) 
                    return false;
                if ( mm.Controlled && (mm.ControlMaster is PlayerMobile && !IsEnemy(mm.ControlMaster) ) ) 
                    return false;
            }
            return true;
            
        }

        public virtual void CheckNocturnal()
        {
            _LastActiveCheck = DateTime.UtcNow;

            int hours, minutes;
            if (!(Region is DungeonRegion))
            {
                Clock.GetTime(Map, X, Y, out hours, out minutes);

                // 00:00 AM - 00:59 AM : Witching hour
                // 01:00 AM - 03:59 AM : Middle of night
                // 04:00 AM - 07:59 AM : Early morning
                // 08:00 AM - 11:59 AM : Late morning
                // 12:00 PM - 12:59 PM : Noon
                // 01:00 PM - 03:59 PM : Afternoon
                // 04:00 PM - 07:59 PM : Early evening
                // 08:00 PM - 11:59 AM : Late at night

                if ((hours >= 18) || (hours <= 06))
                {
                    Blessed = false;
                    Hidden = false;
                    sneaking = false;
                    NameMod = null;
                }
                else if ((hours < 18) && (hours > 06) && (Combatant == null))
                {
                    Blessed = true;
                    Hidden = true;
                    sneaking = true;
                    if (NameMod == null) NameMod = Name + " (Resting)";
                }
            }
        }

        private DateTime _LastActiveCheck = DateTime.UtcNow;
        private TimeSpan _ActiveCheckDelay = TimeSpan.FromSeconds(5.0);

        public bool CanCheckActive()
        {
            if (_LastActiveCheck.Add(_ActiveCheckDelay) < DateTime.UtcNow)
                return true;
            return false;
        }
/*
        public override void OnThink()
        {
            if ((ControlMaster == null) && (SummonMaster == null))
            {
                if (Nocturnal && !ForcedActive && CanCheckActive()) CheckNocturnal();

                if (SemiVisible && !ForcedActive)
                {
                    if (Combatant != null)
                    {
                        Hidden = false;
                        sneaking = false;
                        NameMod = null;
                    }
                    else
                    {
                        Hidden = true;
                        sneaking = true;
                        if (NameMod == null) NameMod = Name + " (Waiting)";
                    }
                }

                if ((LifeDrain == true) && (Combatant != null) && (0.1 >= Utility.RandomDouble())) DrainLife();
            }
            else  // how a summoned undead should act
            {
                Blessed = false;
                Hidden = false;
                sneaking = false;
                Spirit = false;
                Nocturnal = false;
                SemiVisible = false;
                LifeDrain = false;
                LeaveCorpse = true;
            }

            base.OnThink();
        }*/
		public override void OnThink()
		{
			base.OnThink();
			
        }


        private class RotTimer : Timer
        {
            private Mobile m_Mobile;

            //Change timespan to change the rate of decay

            public RotTimer(Mobile m) : base(TimeSpan.FromMinutes(5.0))
            {
                m_Mobile = m;
            }

            protected override void OnTick()
            {
                RotTimer rotTimer = new RotTimer(m_Mobile);
                if (m_Mobile.Str < 2)
                {
                    m_Mobile.Kill();
                    Stop();
                }
                else
                {
                    if (m_Mobile.Hits > m_Mobile.HitsMax / 2)
                    {
                        m_Mobile.Hits -= m_Mobile.HitsMax / 100;
                        //m_Mobile.Say(" losing 1% Hits");
                    }

                    if (m_Mobile.Hits <= m_Mobile.HitsMax / 2)
                    {
                        m_Mobile.Hits -= 1;
                        //m_Mobile.Say(" losing 1 Hitpoint");
                    }

                    if (m_Mobile.Hits < m_Mobile.HitsMax / 4)
                    {
                        m_Mobile.CantWalk = true;
                        //m_Mobile.Say(" Cant Walk Anymore");
                    }

                    if (m_Mobile.Str > 100)
                    {
                        m_Mobile.Str -= m_Mobile.Str / 30;
                        //m_Mobile.Say(" losing 3.33% Str");
                    }

                    if (m_Mobile.Str <= 100)
                    {
                        m_Mobile.Str -= 1;
                        //m_Mobile.Say(" losing 1 Str point");
                    }
                    rotTimer.Start();
                }
            }
        }

        public void DrainLife()
        {
            ArrayList list = new ArrayList();

            foreach (Mobile m in GetMobilesInRange(2))
            {
                if (m == this || !CanBeHarmful(m))
                    continue;

                if (m.Player || m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != Team))
                    list.Add(m);
            }

            foreach (Mobile m in list)
            {
                DoHarmful(m);

                m.FixedParticles(0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist);
                m.PlaySound(0x231);

                m.SendMessage("You feel the life drain out of you!");

                int toDrain = Utility.RandomMinMax(1, 25);

                Hits += toDrain;
                m.Damage(toDrain, this);
            }
            list.Clear();
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            if (!LeaveCorpse)
                c.Delete();
        }

        public BaseUndead(Serial serial) : base(serial)
        { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version
            writer.Write(Spirit);
            writer.Write(LeaveCorpse);
            writer.Write(Nocturnal);
            writer.Write(SemiVisible);
            writer.Write(LifeDrain);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            switch (version)
            {
                case 1:
                {
                    Spirit = reader.ReadBool();
                    LeaveCorpse = reader.ReadBool();
                    Nocturnal = reader.ReadBool();
                    SemiVisible = reader.ReadBool();
                    LifeDrain = reader.ReadBool();
                    goto case 0;
                }
                case 0:
                    break;
            }
        }
    }
}
