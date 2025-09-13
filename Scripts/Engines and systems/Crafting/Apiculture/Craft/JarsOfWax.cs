using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class JarsOfWaxMetal : Item
	{
		[Constructable]
		public JarsOfWaxMetal( ) : base( 0x1007 )
		{
			Stackable = true;
			Weight = 1.0;
			Stackable = false;
			Name = "jar of metal wax";
			Hue = 0x967;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Adds Durability To Metal" );
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
				from.SendMessage( "What metal object do you want to rub this wax on?" );
				t = new WaxTarget( this );
				from.Target = t;
			}
		}

		private class WaxTarget : Target
		{
			private JarsOfWaxMetal m_Wax;

			public WaxTarget( JarsOfWaxMetal tube ) : base( 1, false, TargetFlags.None )
			{
				m_Wax = tube;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				Item iWax = targeted as Item;

				if ( iWax is BaseWeapon )
				{
					BaseWeapon xWax = (BaseWeapon)iWax;

					if ( !iWax.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this wax on items in your pack." );
					}
					else if ( iWax.IsChildOf( from.Backpack ) && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iWax ) )
					{
						int cBonus = xWax.WeaponAttributes.DurabilityBonus;

						if ( cBonus > 50 ){ from.SendMessage( "That metal is already in good condition." ); }
						else
						{
							xWax.WeaponAttributes.DurabilityBonus = ( cBonus + 10 );
							from.RevealingAction();
							from.PlaySound( 0x242 );
							from.AddToBackpack( new Bottle() );
							m_Wax.Consume();
						}
					}
					else
					{
						from.SendMessage( "You cannot rub this wax on that." );
					}
				}
				else if ( iWax is BaseArmor )
				{
					BaseArmor xWax = (BaseArmor)iWax;

					if ( !iWax.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this wax on items in your pack." );
					}
					else if ( iWax.IsChildOf( from.Backpack ) && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iWax ) )
					{
						int cBonus = xWax.ArmorAttributes.DurabilityBonus;

						if ( cBonus > 50 ){ from.SendMessage( "That metal is already in good condition." ); }
						else
						{
							xWax.ArmorAttributes.DurabilityBonus = ( cBonus + 10 );
							from.RevealingAction();
							from.PlaySound( 0x242 );
							from.AddToBackpack( new Bottle() );
							m_Wax.Consume();
						}
					}
					else
					{
						from.SendMessage( "You cannot rub this wax on that." );
					}
				}
				else
				{
					from.SendMessage( "You cannot rub this wax on that." );
				}
			}
		}

		public JarsOfWaxMetal( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class JarsOfWaxLeather : Item
	{
		[Constructable]
		public JarsOfWaxLeather( ) : base( 0x1007 )
		{
			Stackable = true;
			Weight = 1.0;
			Stackable = false;
			Name = "jar of leather wax";
			Hue = 0x972;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Adds Durability To Leather" );
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
				from.SendMessage( "What leather object do you want to rub this wax on?" );
				t = new WaxTarget( this );
				from.Target = t;
			}
		}

		private class WaxTarget : Target
		{
			private JarsOfWaxLeather m_Wax;

			public WaxTarget( JarsOfWaxLeather tube ) : base( 1, false, TargetFlags.None )
			{
				m_Wax = tube;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				Item iWax = targeted as Item;

				if ( iWax is BaseArmor )
				{
					BaseArmor xWax = (BaseArmor)iWax;

					if ( !iWax.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this wax on items in your pack." );
					}
					else if ( iWax.IsChildOf( from.Backpack ) && Server.Misc.MaterialInfo.IsAnyKindOfClothItem( iWax ))
                    {
						int cBonus = xWax.ArmorAttributes.DurabilityBonus;

						if ( cBonus > 50 ){ from.SendMessage( "That leather is already in good condition." ); }
						else
						{
							xWax.ArmorAttributes.DurabilityBonus = ( cBonus + 10 );
							from.RevealingAction();
							from.PlaySound( 0x242 );
							from.AddToBackpack( new Bottle() );
							m_Wax.Consume();
						}
					}
					else
					{
						from.SendMessage( "You cannot rub this wax on that." );
					}
				}
				else
				{
					from.SendMessage( "You cannot rub this wax on that." );
				}
			}
		}

		public JarsOfWaxLeather( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class JarsOfWaxInstrument : Item
	{
		[Constructable]
		public JarsOfWaxInstrument( ) : base( 0x1007 )
		{
			Stackable = true;
			Weight = 1.0;
			Stackable = false;
			Name = "jar of instrument wax";
			Hue = 0x845;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Restores Instruments" );
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
				from.SendMessage( "What instrument do you want to rub this wax on?" );
				t = new WaxTarget( this );
				from.Target = t;
			}
		}

		private class WaxTarget : Target
		{
			private JarsOfWaxInstrument m_Wax;

			public WaxTarget( JarsOfWaxInstrument tube ) : base( 1, false, TargetFlags.None )
			{
				m_Wax = tube;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				Item iWax = targeted as Item;

				if ( iWax is BaseInstrument )
				{
					BaseInstrument xWax = (BaseInstrument)iWax;

					if ( !iWax.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this wax on items in your pack." );
					}
					else
					{
						int cBonus = xWax.UsesRemaining;

						if ( cBonus > 200 ){ from.SendMessage( "That instrument is already in good condition." ); }
						else
						{
							xWax.UsesRemaining = ( cBonus + 20 );
							from.RevealingAction();
							from.PlaySound( 0x242 );
							from.AddToBackpack( new Bottle() );
							m_Wax.Consume();
						}
					}
				}
				else
				{
					from.SendMessage( "You cannot rub this wax on that." );
				}
			}
		}

		public JarsOfWaxInstrument( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}