using System;
using Server.Targeting;
using Server.Network;
using Server.Items;
using System.Collections.Generic;
using System.Collections;

namespace Server.Spells.Second
{
	public class RemoveTrapSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Remove Trap", "An Jux",
				212,
				9001,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public RemoveTrapSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
			Caster.SendMessage( "Select a trapped container, or yourself to summon a magical wand." );
		}

		public void Target( TrapableContainer item )
		{
			if ( !Caster.CanSee( item ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckSequence() )
			{
				int nTrapLevel = item.TrapLevel * 12;

				if ( (int)(Caster.Skills[SkillName.Magery].Value ) > nTrapLevel )
				{
					Point3D loc = item.GetWorldLocation();

					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 5015, 0 );
					Effects.PlaySound( loc, item.Map, 0x1F0 );

					Caster.SendMessage( "Any traps on that container are now disabled." );

					item.TrapType = TrapType.None;
					item.TrapPower = 0;
					item.TrapLevel = 0;
				}
				else
				{
					Caster.SendMessage( "That trap seems to complicated to be affected by your magic." );
					base.DoFizzle();
				}
			}
			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private RemoveTrapSpell m_Owner;

			public InternalTarget( RemoveTrapSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is TrapableContainer )
				{
					m_Owner.Target( (TrapableContainer)o );
				}
				else if ( from == o )
				{
					if ( m_Owner.CheckSequence() )
					{
						ArrayList targets = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is TrapWand )
						{
							TrapWand myWand = (TrapWand)item;
							if ( myWand.owner == from )
							{
								targets.Add( item );
							}
						}
						for ( int i = 0; i < targets.Count; ++i )
						{
							Item item = ( Item )targets[ i ];
							item.Delete();
						}

						from.PlaySound( 0x1ED );
						from.FixedParticles( 0x376A, 9, 32, 5008, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0, EffectLayer.Waist );
						from.SendMessage( "You summon a magical orb into your pack." );
						Item iWand = new TrapWand(from);
						int nPower = (int)(from.Skills[SkillName.Magery].Value / 2 ) + 25;
						if (nPower > 100){nPower = 100;}
						TrapWand xWand = (TrapWand)iWand;
						xWand.WandPower = nPower;
						from.AddToBackpack( xWand );
					}
					m_Owner.FinishSequence();
				}
				else
				{
					from.SendMessage( "This spell has no effect on that!" );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}