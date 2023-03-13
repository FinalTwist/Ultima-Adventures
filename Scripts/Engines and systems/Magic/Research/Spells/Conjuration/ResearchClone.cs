using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;

namespace Server.Spells.Research
{
	public class ResearchClone : ResearchSpell
	{
		public override int spellIndex { get { return 17; } }
		public int CirclePower = 3;
		public static int spellID = 17;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				230,
				9041
			);

		private static Dictionary<Mobile, int> m_CloneCount = new Dictionary<Mobile, int>();

		public static bool HasClone( Mobile m )
		{
			return m_CloneCount.ContainsKey( m );
		}

		public static void AddClone( Mobile m )
		{
			if ( m == null )
				return;

			if ( m_CloneCount.ContainsKey( m ) )
				m_CloneCount[m]++;
			else
				m_CloneCount[m] = 1;
		}

		public static void RemoveClone( Mobile m )
		{
			if ( m == null )
				return;

			if ( m_CloneCount.ContainsKey( m ) )
			{
				m_CloneCount[m]--;

				if ( m_CloneCount[m] == 0 )
					m_CloneCount.Remove( m );
			}
		}

		public ResearchClone( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Caster.Mounted )
			{
				Caster.SendLocalizedMessage( 1063132 ); // You cannot use this ability while mounted.
				return false;
			}
			else if ( (Caster.Followers + 1) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1063133 ); // You cannot summon a mirror image because you have too many followers.
				return false;
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster, typeof( HorrificBeastSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1061091 ); // You cannot cast that spell in this form.
				return false;
			}

			return base.CheckCast( caster );
		}

		public override void OnBeginCast()
		{
			base.OnBeginCast();

			Caster.SendLocalizedMessage( 1063134 ); // You begin to summon a mirror image of yourself.
		}

		public override void OnCast()
		{
			if ( Caster.Mounted )
			{
				Caster.SendLocalizedMessage( 1063132 ); // You cannot use this ability while mounted.
			}
			else if ( (Caster.Followers + 1) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1063133 ); // You cannot summon a mirror image because you have too many followers.
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster, typeof( HorrificBeastSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1061091 ); // You cannot cast that spell in this form.
			}
			else if ( CheckSequence() )
			{
				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 0, 0, 5042, 0 );
				Effects.PlaySound( Caster, Caster.Map, 0x201 );
				Caster.Hidden = true;
				new Clone( Caster ).MoveToWorld( Caster.Location, Caster.Map );
				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false );
			}

			FinishSequence();
		}
	}
}