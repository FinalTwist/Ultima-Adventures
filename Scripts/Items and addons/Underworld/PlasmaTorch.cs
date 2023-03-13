using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class PlasmaTorch : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public PlasmaTorch() : base( 0x2D86 )
		{
			Name = "plasma torch";
			Stackable = true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendMessage( "What chest do you want to use the torch on?" );
				t = new UnlockTarget( this );
				from.Target = t;
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1049644, "Used to melt through most chest traps and locks" );
		}

		private class UnlockTarget : Target
		{
			private PlasmaTorch m_Key;

			public UnlockTarget( PlasmaTorch key ) : base( 1, false, TargetFlags.None )
			{
				m_Key = key;
				CheckLOS = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( !m_Key.IsChildOf( from.Backpack ) )
				{
					from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
				}
				else if ( targeted == m_Key )
				{
					from.SendMessage( "This torch is to melt locks and traps on most chests." );
				}
				else if ( targeted is BaseHouseDoor )  // house door check
				{
					from.SendMessage( "This torch is to melt locks and traps on most chests." );
				}
				else if ( targeted is BookBox )  // cursed box of books
				{
					from.SendMessage( "This torch can never penetrate the magic of this cursed box." );
				}
				else if ( targeted is UnidentifiedArtifact || targeted is UnidentifiedItem || targeted is CurseItem )
				{
					from.SendMessage( "This torch is to melt locks and traps on most chests." );
				}
				else if ( targeted is BaseDoor )
				{
					if ( Server.Items.DoorType.IsDungeonDoor( (BaseDoor)targeted ) )
					{
						if ( ((BaseDoor)targeted).Locked == false )
							from.SendMessage( "That does not need to be unlocked." );

						else
						{
							((BaseDoor)targeted).Locked = false;
							Server.Items.DoorType.UnlockDoors( (BaseDoor)targeted );
							from.RevealingAction();
							from.PlaySound( 0x227 );
							m_Key.Consume();
						}
					}
					else
						from.SendMessage( "That does not need to be unlocked." );
				}
				else if ( targeted is ILockable )
				{
					ILockable o = (ILockable)targeted;
					LockableContainer cont2 = (LockableContainer)o;
					TrapableContainer cont3 = (TrapableContainer)o;

					if ( ( o.Locked ) || ( cont3.TrapType != TrapType.None ) )
					{
						if ( o is BaseDoor && !((BaseDoor)o).UseLocks() )  // this seems to check house doors also
						{
							from.SendMessage( "This torch is to melt locks and traps on most chests." );
						}
						else if ( targeted is TreasureMapChest )
						{
							from.SendMessage( "The torch seems to have done nothing to the mechanism inside." );
							m_Key.Consume();
						}
						else if ( 100 >= cont2.RequiredSkill )
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

							if ( o is TrapableContainer )
							{
								TrapableContainer cont = (TrapableContainer)o;

								if ( cont.TrapType != TrapType.None )
									cont.TrapType = TrapType.None;
							}

							if ( targeted is Item )
							{
								Item item = (Item)targeted;
								from.SendMessage( "The torch seems to have melted the mechanism inside." );
							}

							from.RevealingAction();
							from.PlaySound( 0x227 );
							m_Key.Consume();
						}
						else if ( ( cont3.TrapType != TrapType.None ) && ( cont3.TrapLevel > 0 ) ) 
						{
							if ( o is TrapableContainer )
							{
								TrapableContainer cont = (TrapableContainer)o;

								if ( cont.TrapType != TrapType.None )
									cont.TrapType = TrapType.None;
							}

							if ( targeted is Item )
							{
								Item item = (Item)targeted;
								from.SendMessage( "The torch seems to have melted the mechanism inside." );
							}

							from.RevealingAction();
							from.PlaySound( 0x227 );
							m_Key.Consume();
						}
						else 
						{
							from.SendMessage( "The torch seems to have done nothing to the mechanism inside." );
							m_Key.Consume();
						}
					}
					else
					{
						from.SendMessage( "You don't need to use torch on that." );
					}
				}
				else
				{
					from.SendMessage( "This torch is to melt locks and traps on most chests." );
				}
			}
		}

		public PlasmaTorch( Serial serial ) : base( serial )
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