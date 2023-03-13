using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Fifth
{
	public class MagicReflectSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Magic Reflection", "In Jux Sanct",
				242,
				9012,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		public MagicReflectSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
				return false;
			}
			else if ( Caster.Backpack.FindItemByType( typeof ( Diamond ) ) == null )
			{
				Caster.SendMessage( "You need a diamond to cast this spell!" );
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
			}
			else if ( Caster.Backpack.FindItemByType( typeof ( Diamond ) ) == null )
			{
				Caster.SendMessage( "You need a diamond to cast this spell!" );
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
				{
					int value = (int)( ( Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.EvalInt].Value ) / 4 );
					Caster.MagicDamageAbsorb = value;
					Item diamond = Caster.Backpack.FindItemByType( typeof ( Diamond ) );
					if ( diamond != null ){ diamond.Consume(); }
					Caster.PlaySound( 0x1ED );
					Caster.FixedParticles( 0x375A, 10, 15, 5037, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );
				}
				else
				{
					Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
				}

				FinishSequence();
			}
		}
	}
}