using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class MasterSkeletonsKey : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public MasterSkeletonsKey() : base( 0x410B )
		{
			Name = "master skeleton key";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;
			int number;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendMessage( "What locked container do you want to use the key on?" );
				t = new UnlockTarget( this );
				from.Target = t;
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1049644, "Used to open any locked container" );
		}

		private class UnlockTarget : Target
		{
			private MasterSkeletonsKey m_Key;

			public UnlockTarget( MasterSkeletonsKey key ) : base( 1, false, TargetFlags.None )
			{
				m_Key = key;
				CheckLOS = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int number = -1;

				if ( !m_Key.IsChildOf( from.Backpack ) )
				{
					from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
				}
				else if ( targeted == m_Key )
				{
					from.SendMessage( "This key is to unlock almost any container." );
				}
				else if ( targeted is BaseHouseDoor )  // house door check
				{
					from.SendMessage( "This key is to unlock almost any container." );
				}
				else if ( targeted is BookBox )  // cursed box of books
				{
					from.SendMessage( "This key can never unlock this cursed box." );
				}
				else if ( targeted is UnidentifiedArtifact || targeted is UnidentifiedItem || targeted is CurseItem )
				{
					from.SendMessage( "This key is to unlock almost any container." );
				}
				else if ( targeted is BaseDoor )
				{
					if ( Server.Items.DoorType.IsSpaceshipDoor( (BaseDoor)targeted ) && m_Key.ItemID != 0x3A75 )
					{
						from.SendMessage( "This doesn't have a key hole, but it does have a card slot." );
					}
					else if ( !(Server.Items.DoorType.IsSpaceshipDoor( (BaseDoor)targeted )) && m_Key.ItemID == 0x3A75 )
					{
						from.SendMessage( "This doesn't have a card slot, but it does have a key hole." );
					}
					else if ( Server.Items.DoorType.IsSpaceshipDoor( (BaseDoor)targeted ) && m_Key.ItemID == 0x3A75 )
					{
						if ( ((BaseDoor)targeted).Locked == false )
							from.SendMessage( "That does not need to be unlocked." );

						else
						{
							((BaseDoor)targeted).Locked = false;
							Server.Items.DoorType.UnlockDoors( (BaseDoor)targeted );
							from.RevealingAction();
							from.PlaySound( 0x54B );
							m_Key.Consume();
						}
					}
					else if ( Server.Items.DoorType.IsDungeonDoor( (BaseDoor)targeted ) && m_Key.ItemID != 0x3A75 )
					{
						if ( ((BaseDoor)targeted).Locked == false )
							from.SendMessage( "That does not need to be unlocked." );

						else
						{
							((BaseDoor)targeted).Locked = false;
							((BaseDoor)targeted).KeyValue = 0;
							Server.Items.DoorType.UnlockDoors( (BaseDoor)targeted );
							from.RevealingAction();
							from.PlaySound( 0x241 );
							m_Key.Consume();
						}
					}
					else
						from.SendMessage( "That does not need to be unlocked." );
				}
				else if ( targeted is ILockable )
				{
					ILockable o = (ILockable)targeted;
					LockableContainer cont2 = o as LockableContainer;
					if (cont2 == null)
					{
						Plank plank = o as Plank;
						if (plank != null && plank.Boat != null)
						{
							if (plank.Boat.DecayLevel == Multis.BoatDecayLevel.IDOC)
							{
								if (from.Skills[SkillName.Lockpicking].Value < 95)
								{
									from.PrivateOverheadMessage( 0, 1150, false,  "You don't see how that lock can be manipulated.", from.NetState );
									return;
								} 
							
								if (from.CheckTargetSkill( SkillName.Lockpicking, this, 90, 100 ) )
								{
									from.PlaySound( 0x4A );
									plank.Boat.TakeOwnership(from);
									m_Key.Consume();
								}
								else 
								{
									if ( Utility.Random( 4 ) == 0 )
									{
										from.PlaySound( 0x3A4 );
										from.PrivateOverheadMessage( 0, 1150, false,  "You break the key.", from.NetState ); 
										m_Key.Consume();
									}
                                    else 
									{
										from.PrivateOverheadMessage( 0, 1150, false,  "The key fails to fit the lock.", from.NetState ); 
									}
								}

								return;
							}
						}

						from.SendLocalizedMessage( 501666 ); // You can't unlock that!
						return;
					}

					if ( Multis.BaseHouse.CheckSecured( cont2 ) ) 
						from.SendLocalizedMessage( 503098 ); // You cannot cast this on a secure item.
					else if ( !cont2.Locked )
						from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503101 ); // That did not need to be unlocked.
					else if ( cont2.LockLevel == 0 )
						from.SendLocalizedMessage( 501666 ); // You can't unlock that!
					else if ( o.Locked )
					{
						if ( o is BaseDoor && !((BaseDoor)o).UseLocks() )  // this seems to check house doors also
						{
							from.SendMessage( "This key is to unlock any container." );
						}
						else if ( Server.Misc.ContainerFunctions.IsSpaceCrate( ((Item)o).ItemID ) && ((ILockpickable)targeted).Locked && m_Key.ItemID != 0x3A75 )
						{
							from.SendMessage( "This doesn't have a key hole, but it does have a card slot." );
						}
						else if ( !(Server.Misc.ContainerFunctions.IsSpaceCrate( ((Item)o).ItemID )) && ((ILockpickable)targeted).Locked && m_Key.ItemID == 0x3A75 )
						{
							from.SendMessage( "This doesn't have a card slot, but it does have a key hole." );
						}
						else if ( Server.Misc.ContainerFunctions.IsSpaceCrate( ((Item)o).ItemID ) && o.Locked && m_Key.ItemID == 0x3A75 )
						{
							o.Locked = false;

							if ( o is LockableContainer )
							{
								LockableContainer cont = (LockableContainer)o;
								if ( cont.LockLevel == -255 )
								{
									cont.LockLevel = cont.RequiredSkill - 10;
									if ( cont.LockLevel == 0 )
										cont.LockLevel = -1;
								}

								cont.Picker = from;  // sets "lockpicker" to the user.
							}

							if ( targeted is Item )
							{
								Item item = (Item)targeted;
								from.SendMessage( "You swipe the key card to open the lock, but also wearing it out from further use." );
							}

							from.RevealingAction();
							from.PlaySound( 0x54B );
							m_Key.Consume();
						}
						else if ( o.Locked && m_Key.ItemID != 0x3A75 )
						{
							o.Locked = false;

							if ( o is LockableContainer )
							{
								LockableContainer cont = (LockableContainer)o;
								if ( cont.LockLevel == -255 )
								{
									cont.LockLevel = cont.RequiredSkill - 10;
									if ( cont.LockLevel == 0 )
										cont.LockLevel = -1;
								}

								cont.Picker = from;  // sets "lockpicker" to the user.
							}

							if ( targeted is Item )
							{
								Item item = (Item)targeted;
								from.SendMessage( "The key opens the lock, wearing the key out from further use." );
							}

							from.RevealingAction();
							from.PlaySound( 0x241 );
							m_Key.Consume();
						}
					}
					else
					{
						from.SendMessage( "You don't need to use this key on that." );
					}
				}
				else
				{
					from.SendMessage( "This key is to unlock any container." );
				}
			}
		}

		public MasterSkeletonsKey( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}