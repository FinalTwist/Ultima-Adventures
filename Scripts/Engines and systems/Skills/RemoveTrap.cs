using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.SkillHandlers
{
	public class RemoveTrap
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.RemoveTrap].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.Target = new InternalTarget();

			m.SendLocalizedMessage( 502368 ); // Which trap will you attempt to disarm?

			return TimeSpan.FromSeconds( 5.0 ); // 5 second delay before being able to re-use a skill
		}

		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 2, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					from.SendLocalizedMessage( 502816 ); // You feel that such an action would be inappropriate
				}
				else if ( targeted is TrapableContainer )
				{
					TrapableContainer targ = (TrapableContainer)targeted;

					from.Direction = from.GetDirectionTo( targ );

					int nTrapLevel = targ.TrapLevel * 10;

					if ( targ.TrapType == TrapType.None )
					{
						from.SendLocalizedMessage( 502373 ); // That doesn't appear to be trapped
						return;
					}

					if ( (int)(from.Skills[SkillName.RemoveTrap].Value ) < nTrapLevel )
					{
						from.SendMessage( "This trap looks too complicated for you." );
						return;
					}

					from.PlaySound( 0x241 );

					// if ( from.CheckTargetSkill( SkillName.RemoveTrap, targ, targ.TrapPower, targ.TrapPower + 30 ) )
					nTrapLevel = nTrapLevel + 30;
					if ( from.CheckTargetSkill( SkillName.RemoveTrap, targ, 0, nTrapLevel ) )
					{
						targ.TrapPower = 0;
						targ.TrapLevel = 0;
						targ.TrapType = TrapType.None;
						from.SendLocalizedMessage( 502377 ); // You successfully render the trap harmless
					}
					else
					{
						from.SendLocalizedMessage( 502372 ); // You fail to disarm the trap... but you don't set it off
					}
				}
				else if ( targeted is Item )
				{
					if ( (Item)targeted is HiddenTrap )
					{
						from.PlaySound( 0x241 );
						
						if ( from.CheckSkill( SkillName.RemoveTrap, 0, 100 ) )
						{
							Item HideTrap = (Item)targeted;
							HideTrap.Delete();
							from.SendLocalizedMessage( 502377 ); // You successfully render the trap harmless
						}
						else
						{
							from.SendLocalizedMessage( 502372 ); // You fail to disarm the trap... but you don't set it off
						}
					}
					else { from.SendLocalizedMessage( 502373 ); }
				}
				else
				{
					
					Point3D p = new Point3D( (IPoint3D)targeted );
					IPooledEnumerable TitemsInRange = from.Map.GetItemsInRange( p, 1 );
					Item hidden = null;
					foreach ( Item item in TitemsInRange )
					{
						if ( item is HiddenTrap )
							hidden = item;
					}
					if (hidden == null)
						from.SendLocalizedMessage( 502373 ); // That does'nt appear to be trapped
					else
					{
						from.PlaySound( 0x241 );
						
						if ( from.CheckSkill( SkillName.RemoveTrap, 0, 100 ) )
						{
							hidden.Delete();
							from.SendLocalizedMessage( 502377 ); // You successfully render the trap harmless
						}
						else
						{
							from.SendLocalizedMessage( 502372 ); // You fail to disarm the trap... but you don't set it off
						}
					}
				}
			}
		}
	}
}