using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Gumps;
using Server.Spells;
using Server.Misc;

namespace Server.Spells.Research
{
	public class ResearchEndureCold : ResearchSpell
	{
		public override int spellIndex { get { return 12; } }
		public int CirclePower = 2;
		public static int spellID = 12;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.75 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				236,
				9011
			);

		public ResearchEndureCold( Mobile caster, Item scroll) : base( caster, scroll, m_Info )
		{
		}

        public override void OnCast()
        {
			if( CheckSequence() )
			{
				bool success = false;

				ArrayList targets = new ArrayList();

				foreach ( Mobile m in Caster.GetMobilesInRange( 3 ) )
				{
					if ( Caster.CanBeBeneficial( m, false, true ) && !(m is Golem) )
						targets.Add( m );
				}

				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = (Mobile)targets[i];

					TimeSpan duration = TimeSpan.FromSeconds( (double)(DamagingSkill( Caster ) * 4) ); 
                    int amount = (int)(DamagingSkill( Caster ) / 8);

					m.SendMessage( "Your resistance to cold has increased." );
					ResistanceMod mod1 = new ResistanceMod( ResistanceType.Cold, + amount );
						
					m.AddResistanceMod( mod1 );
						
					m.PlaySound( 0x1E9 );
					m.FixedParticles( 0x375A, 9, 20, 5016, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );
						
					new ExpireTimer( m, mod1, duration ).Start();
					success = true;
				}

				if ( success ){ Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false ); }
			}
			FinishSequence();
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Mobile;
			private ResistanceMod m_Mods;

			public ExpireTimer( Mobile m, ResistanceMod mod, TimeSpan delay ) : base( delay )
			{
				m_Mobile = m;
				m_Mods = mod;
			}

			public void DoExpire()
			{
				PlayerMobile dpm = m_Mobile as PlayerMobile;
				m_Mobile.RemoveResistanceMod( m_Mods );
				
				Stop();
			}

			protected override void OnTick()
			{
				if ( m_Mobile != null )
				{
					m_Mobile.SendMessage( "The cold protection effect has worn off." );
					m_Mobile.PlaySound( 0x1F8 );
					DoExpire();
				}
			}
		}
	}
}