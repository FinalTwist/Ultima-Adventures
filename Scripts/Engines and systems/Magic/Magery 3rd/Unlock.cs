using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Third
{
	public class UnlockSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Unlock Spell", "Ex Por",
				215,
				9001,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		public UnlockSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private UnlockSpell m_Owner;

			public InternalTarget( UnlockSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
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

					Effects.SendLocationParticles( EffectItem.Create( new Point3D( loc ), from.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0, 5024, 0 );

					Effects.PlaySound( loc, from.Map, 0x1FF );

					if ( o is Mobile )
					{
						from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.
					}
					else if ( o is BaseHouseDoor )  // house door check
					{
						from.SendMessage( "This spell is to unlock certain containers and other types of doors." );
					}
					else if ( o is BookBox )  // cursed box of books
					{
						from.SendMessage( "This spell can never unlock this cursed box." );
					}
					else if ( o is UnidentifiedArtifact || o is UnidentifiedItem || o is CurseItem )
					{
						from.SendMessage( "This spell is used to unlock any container." );
					}
					else if ( o is BaseDoor )
					{
						if ( Server.Items.DoorType.IsDungeonDoor( (BaseDoor)o ) )
						{
							if ( ((BaseDoor)o).Locked == false )
								from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.

							else
							{
								if (((BaseDoor)o).KeyValue <= 65 && Utility.RandomDouble() < (double)( (100- ((BaseDoor)o).KeyValue) / 100) )
								{	
									((BaseDoor)o).Locked = false;
									((BaseDoor)o).KeyValue = 0;
									Server.Items.DoorType.UnlockDoors( (BaseDoor)o );
								}
								else 
									from.SendMessage( "The lock is too complex for a simple spell." );
							}
						}
						else
							from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.
					}
					else if ( !( o is LockableContainer ) )
					{
						from.SendLocalizedMessage( 501666 ); // You can't unlock that!
					}
					else {
						LockableContainer cont = (LockableContainer)o;

						if ( Multis.BaseHouse.CheckSecured( cont ) ) 
							from.SendLocalizedMessage( 503098 ); // You cannot cast this on a secure item.
						else if ( !cont.Locked )
							from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.
						else if ( cont.LockLevel == 0 )
							from.SendLocalizedMessage( 501666 ); // You can't unlock that!
						else if ( this is TreasureMapChest )
						{
							from.SendMessage( "A magical aura on this long lost treasure seems to negate your spell." );
						}
						else if ( this is ParagonChest )
						{
							from.SendMessage( "A magical aura on this long lost treasure seems to negate your spell." );
						}
						else if ( this is PirateChest )
						{
							from.SendMessage( "This seems to be protected from magic, but maybe a thief can get it open." );
						}
						else {
							int level = (int)(from.Skills[SkillName.Magery].Value) + 20; // WIZARD CHANGED FOR WANDS AND SUCH

							if ( level > 50 ){ level = 50; } // WIZARD ADDED FOR A MAXIMUM SO THIEF IS SPECIAL

							if ( level >= cont.RequiredSkill && !(cont is TreasureMapChest && ((TreasureMapChest)cont).Level > 2) ) {
								cont.Locked = false;

								if ( cont.LockLevel == -255 )
									cont.LockLevel = cont.RequiredSkill - 10;
							}
							else
								from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503099 ); // My spell does not seem to have an effect on that lock.
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