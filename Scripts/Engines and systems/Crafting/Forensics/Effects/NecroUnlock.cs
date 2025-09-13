using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Undead
{
	public class NecroUnlockSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override double RequiredSkill{ get{ return 15.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		public NecroUnlockSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private NecroUnlockSpell m_Owner;

			public InternalTarget( NecroUnlockSpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D loc = o as IPoint3D;

				if ( loc == null )
					return;

				if ( m_Owner.CheckSequence() ) {
					SpellHelper.Turn( from, o );

					Effects.SendLocationParticles( EffectItem.Create( new Point3D( loc ), from.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5024 );

					Effects.PlaySound( loc, from.Map, 0x17E );

					if ( o is Mobile )
						from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.
					else if ( o is BaseDoor )
					{
						if ( Server.Items.DoorType.IsDungeonDoor( (BaseDoor)o ) )
						{
							if ( ((BaseDoor)o).Locked == false )
								from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.

							else
							{
								((BaseDoor)o).Locked = false;
								Server.Items.DoorType.UnlockDoors( (BaseDoor)o );
							}
						}
						else
							from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.
					}
					else if ( !( o is LockableContainer ) )
						from.SendLocalizedMessage( 501666 ); // You can't unlock that!
					else {
						LockableContainer cont = (LockableContainer)o;

						if ( Multis.BaseHouse.CheckSecured( cont ) ) 
							from.SendMessage("You cannot use this on a secure item.");
						else if ( !cont.Locked )
							from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.
						else if ( cont.LockLevel == 0 )
							from.SendLocalizedMessage( 501666 ); // You can't unlock that!
						else {
							int level = (int)(from.Skills[SkillName.Necromancy].Value);

							if ( level > 90 ){ level = 90; } // WIZARD ADDED FOR A MAXIMUM SO THIEF IS SPECIAL

							if ( level >= cont.RequiredSkill && !(cont is TreasureMapChest ) ) {
								cont.Locked = false;

								if ( cont.LockLevel == -255 )
									cont.LockLevel = cont.RequiredSkill - 10;
							}
							else
								from.PrivateOverheadMessage(MessageType.Regular, 0x3B2, false, "This does not seem to work on that lock.", from.NetState);
						}		
					}
				}

				m_Owner.FinishSequence();
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}