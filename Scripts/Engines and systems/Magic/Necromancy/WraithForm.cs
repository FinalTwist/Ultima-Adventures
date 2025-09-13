using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Necromancy
{
	public class WraithFormSpell : TransformationSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Wraith Form", "Rel Xen Um",
				203,
				9031,
				Reagent.NoxCrystal,
				Reagent.PigIron
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		public override double RequiredSkill{ get{ return 20.0; } }
		public override int RequiredMana{ get{ return 17; } }

		public override int Body{ get{ return 310; } }
		public override int Hue{ get{ return Caster.Female ? 0 : 0x4001; } }

		public override int PhysResistOffset{ get{ return +25; } }
		public override int FireResistOffset{ get{ return -5; } }
		public override int ColdResistOffset{ get{ return  0; } }
		public override int PoisResistOffset{ get{ return  0; } }
		public override int NrgyResistOffset{ get{ return -15; } }

		public WraithFormSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void DoEffect( Mobile m )
		{
			if ( m is PlayerMobile )
				((PlayerMobile)m).IgnoreMobiles = true;
			
			m.PlaySound( 0x17F );
			m.FixedParticles( 0x374A, 1, 15, 9902, 1108, 4, EffectLayer.Waist );
			
			int manadrain = Math.Max(8, 5 + (int)(0.16 * m.Skills.SpiritSpeak.Value));

			BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.WraithForm, 1060524, 1153829, string.Format("15\t5\t5\t{0}", manadrain)));
		}
		
		public override void RemoveEffect( Mobile m )
		{
			if ( m is PlayerMobile && m.AccessLevel == AccessLevel.Player )
				((PlayerMobile)m).IgnoreMobiles = false;
				
			BuffInfo.RemoveBuff(m, BuffIcon.WraithForm);
		}
	}
}
