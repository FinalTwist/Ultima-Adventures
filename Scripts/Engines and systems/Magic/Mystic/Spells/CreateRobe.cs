using System;
using Server.Items;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;

namespace Server.Spells.Mystic
{
	public class CreateRobe : MysticSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Create Robe", "Ra Beh Cah Summ",
				269,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 150; } }
		public override double RequiredSkill{ get{ return 25.0; } }
		public override int RequiredMana{ get{ return 20; } }
		public override int MysticSpellCircle{ get{ return 1; } }

		public CreateRobe( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				if ( item is MysticMonkRobe )
				{
					if ( ((MysticMonkRobe)item).m_Owner == Caster )
						targets.Add( item );
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}

				MysticMonkRobe robe = new MysticMonkRobe( 2422 );
				robe.m_Owner = Caster;
				robe.m_Gifter = "Mystical Monk's Robe";
				robe.m_How = "Belongs to";
				robe.m_Points = (int)(Caster.Skills[SkillName.Wrestling].Value * 2);
				Caster.AddToBackpack( robe );

				Caster.FixedParticles( 0x376A, 1, 62, 9923, 3, 3, EffectLayer.Waist );
				Caster.PlaySound( 0x5C9 );
			}

			FinishSequence();
		}
	}
}