using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class BottleOfAcid : Item
	{
		public override int Hue{ get { return 1167; } }

		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public BottleOfAcid() : base( 0x180F )
		{
			Name = "bottle of acid";
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
				from.SendMessage( "What chest do you want to use the acid on?" );
				t = new UnlockTarget( this );
				from.Target = t;
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1049644, "Used to dissolve most chest traps and locks" );
		}

		private class UnlockTarget : Target
		{
			private BottleOfAcid m_Key;

			public UnlockTarget( BottleOfAcid key ) : base( 1, false, TargetFlags.None )
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
					from.SendMessage( "This acid is to dissolve locks and traps on most chests." );
				}
				else if ( targeted is BaseHouseDoor )  // house door check
				{
					from.SendMessage( "This acid is to dissolve locks and traps on most chests." );
				}
				else if ( targeted is BookBox )  // cursed box of books
				{
					from.SendMessage( "This acid can never penetrate the magic of this cursed box." );
				}
				else if ( targeted is UnidentifiedArtifact || targeted is UnidentifiedItem || targeted is CurseItem )
				{
					from.SendMessage( "This acid is to dissolve locks and traps on most chests." );
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
							from.PlaySound( 0x231 );
							if ( m_Key.ItemID == 0x1007 ){ from.AddToBackpack( new Jar() ); } else { from.AddToBackpack( new Bottle() ); }
							m_Key.Consume();
						}
					}
					else
						from.SendMessage( "That does not need to be unlocked." );
				}
				else if ( targeted is Head )
				{
					if ( ((Item)targeted).ItemID == 7584 || ((Item)targeted).ItemID == 7393 )
					{
						from.RevealingAction();
						from.PlaySound( 0x231 );
						if ( m_Key.ItemID == 0x1007 ){ from.AddToBackpack( new Jar() ); } else { from.AddToBackpack( new Bottle() ); }
						m_Key.Consume();
						((Item)targeted).ItemID = 0x1AE0;
						if ( (((Item)targeted).Name).Contains(" head ") ){ (((Item)targeted).Name) = (((Item)targeted).Name).Replace(" head ", " skull "); }
						from.SendMessage( "The acid melts the skin away, leaving only a skull." );
					}
					else
					{
						from.SendMessage( "Someone already used acid to melt the skin away." );
					}
				}
				else if ( targeted is ILockable && targeted is LockableContainer)
				{
					ILockable o = (ILockable)targeted;
					TrapableContainer cont3 = (TrapableContainer)o;

					if ( ( o.Locked ) || ( cont3.TrapType != TrapType.None ) )
					{
						LockableContainer cont2 = (LockableContainer)o;
						if ( o is BaseDoor && !((BaseDoor)o).UseLocks() )  // this seems to check house doors also
						{
							from.SendMessage( "This acid is to dissolve locks and traps on most chests." );
						}
						else if ( targeted is TreasureMapChest )
						{
							from.SendMessage( "The acid seems to have done nothing to the mechanism inside." );
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
								from.SendMessage( "The acid seems to have eaten away at the mechanism inside." );
							}

							from.RevealingAction();
							from.PlaySound( 0x231 );
							if ( m_Key.ItemID == 0x1007 ){ from.AddToBackpack( new Jar() ); } else { from.AddToBackpack( new Bottle() ); }
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
								from.SendMessage( "The acid seems to have eaten away at the mechanism inside." );
							}

							from.RevealingAction();
							from.PlaySound( 0x231 );
							if ( m_Key.ItemID == 0x1007 ){ from.AddToBackpack( new Jar() ); } else { from.AddToBackpack( new Bottle() ); }
							m_Key.Consume();
						}
						else 
						{
							from.SendMessage( "The acid seems to have done nothing to the mechanism inside." );
							m_Key.Consume();
						}
					}
					else
					{
						from.SendMessage( "You don't need to use acid on that." );
					}
				}
				else
				{
					from.SendMessage( "This acid is to dissolve locks and traps on most chests." );
				}
			}
		}

		public BottleOfAcid( Serial serial ) : base( serial )
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