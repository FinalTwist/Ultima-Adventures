using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Spells.HolyMan
{
	public class BanishEvilSpell : HolyManSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Banish", "Exilium",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 120; } }
		public override double RequiredSkill{ get{ return 60.0; } }
		public override int RequiredMana{ get{ return 30; } }

        public BanishEvilSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
			BaseCreature bc = m as BaseCreature;

            SlayerEntry undead = SlayerGroup.GetEntryByName(SlayerName.Silver);
            SlayerEntry exorcism = SlayerGroup.GetEntryByName(SlayerName.Exorcism);

            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (m is PlayerMobile)
            {
                Caster.SendMessage("Your prayers cannot banish that!");
            }
			else if (!undead.Slays(m) && !exorcism.Slays(m))
			{
                Caster.SendMessage("Your prayers cannot banish such a creature!");
            }
			else if( bc.IsBonded )
			{
                Caster.SendMessage("Your prayers cannot banish such a creature!");
			}
			else if ( exorcism.Slays(m) && !bc.IsDispellable )
			{
				m.Say("Your pitiful prayers are heard by no one, mortal!");
				double damage;
				damage = GetNewAosDamage(48, 1, 5, Caster);
				Caster.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
				Caster.PlaySound(0x208);
				SpellHelper.Damage(this, Caster, damage, 0, 100, 0, 0, 0);
			}
			else if ( m.Fame >= 23000 )
			{
				m.Say("Your pitiful prayers are heard by no one, mortal!");
				double damage;
				damage = GetNewAosDamage(48, 1, 5, Caster);
				Caster.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
				Caster.PlaySound(0x208);
				SpellHelper.Damage(this, Caster, damage, 0, 100, 0, 0, 0);
			}
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                m.FixedParticles(0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot);
                m.PlaySound(0x208);

				if (undead.Slays(m))
				{
					m.Say("No! You cannot banish me! I will return from the Underworld!");
				}
				else
				{
					m.Say("No! You cannot kill that which is dead! I will return!");
				}

                new InternalTimer(m).Start();
            }

            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            Mobile m_Owner;

            public InternalTimer(Mobile owner) : base(TimeSpan.FromSeconds(1.5))
            {
                m_Owner = owner;
            }

            protected override void OnTick()
            {
                if (m_Owner != null && m_Owner.CheckAlive())
                {
                    m_Owner.Delete();
                }
            }
        }

        private class InternalTarget : Target
        {
            private BanishEvilSpell m_Owner;

            public InternalTarget(BanishEvilSpell owner) : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
