using System;
using Server; 
using Server.Targeting;
using Server.Network;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Misc;

namespace Server.Spells.Second
{
	public class MagicTrapSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Magic Trap", "In Jux",
				212,
				9001,
				Reagent.Garlic,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public MagicTrapSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( TrapableContainer item )
		{
			if ( !Caster.CanSee( item ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( item.TrapType != TrapType.None && item.TrapType != TrapType.MagicTrap )
			{
				base.DoFizzle();
			}
			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, item );

				item.TrapType = TrapType.MagicTrap;
				//item.TrapPower = Core.AOS ? Utility.RandomMinMax( 10, 50 ) : 1;
				item.TrapPower = (int)(Caster.Skills[SkillName.Magery].Value);
				item.TrapLevel = (int)(Caster.Skills[SkillName.Magery].Value);
					if ( item.TrapLevel > 90 ){ item.TrapLevel = 90; }
					if ( item.TrapLevel < 0 ){ item.TrapLevel = 0; }

				Point3D loc = item.GetWorldLocation();

				Effects.SendLocationParticles( EffectItem.Create( new Point3D( loc.X + 1, loc.Y, loc.Z ), item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 10, 9502 );
				Effects.SendLocationParticles( EffectItem.Create( new Point3D( loc.X, loc.Y - 1, loc.Z ), item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 10, 9502 );
				Effects.SendLocationParticles( EffectItem.Create( new Point3D( loc.X - 1, loc.Y, loc.Z ), item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 10, 9502 );
				Effects.SendLocationParticles( EffectItem.Create( new Point3D( loc.X, loc.Y + 1, loc.Z ), item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 10, 9502 );
				Effects.SendLocationParticles( EffectItem.Create( new Point3D( loc.X, loc.Y,     loc.Z ), item.Map, EffectItem.DefaultDuration ), 0, 0, 0, 5014 );

				Effects.PlaySound( loc, item.Map, 0x1EF );
			}

			FinishSequence();
		}

		public void MTarget( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				int traps = 0;

				foreach ( Item m in Caster.GetItemsInRange( 10 ) )
				{
					if ( m is SpellTrap )
						++traps;
				}

				if ( traps > 2 )
				{
					Caster.SendMessage( "There are too many magical traps in the area!" );
				}
				else if ( !Caster.Region.AllowHarmful( Caster, Caster ) )
				{
					Caster.SendMessage( "That doesn't feel like a good idea." ); 
					return;
				}
				else
				{
					SpellHelper.Turn( Caster, p );
					SpellHelper.GetSurfaceTop( ref p );

					Point3D loc = new Point3D( p.X, p.Y, p.Z );

					int TrapPower = (int)(Caster.Skills[SkillName.Magery].Value/2);
					SpellTrap mtrap = new SpellTrap( Caster, TrapPower ); 
					mtrap.Map = Caster.Map; 
					mtrap.Location = loc;

					Effects.SendLocationParticles( EffectItem.Create( loc, Caster.Map, EffectItem.DefaultDuration ), 0x376A, 9, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 9502, 0 );
					Effects.PlaySound( loc, Caster.Map, 0x1EF );
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private MagicTrapSpell m_Owner;

			public InternalTarget( MagicTrapSpell owner ) : base( Core.ML ? 10 : 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is TrapableContainer )
				{
					m_Owner.Target( (TrapableContainer)o );
				}
				else if ( o is IPoint3D )
				{
					m_Owner.MTarget( (IPoint3D)o );
				}
				else
				{
					from.SendMessage( "You can't trap that!" );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}