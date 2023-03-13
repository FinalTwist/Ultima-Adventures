using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class SpaceDyes : Item
	{
		public int vialHue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int vial_Hue { get { return vialHue; } set { vialHue = value; InvalidateProperties(); } }

		[Constructable]
		public SpaceDyes() : this( 1 )
		{
		}

		[Constructable]
		public SpaceDyes( int amount ) : base( 0x1FDD )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Name = "space-age dye"; 
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Used To Dye Almost Anything" );
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
				from.SendMessage( "What do you want to use the dye on?" );
				t = new DyeTarget( this );
				from.Target = t;
			}
		}

		private class DyeTarget : Target
		{
			private SpaceDyes m_Dye;

			public DyeTarget( SpaceDyes tube ) : base( 1, false, TargetFlags.None )
			{
				m_Dye = tube;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item iDye = targeted as Item;

					if ( !iDye.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only dye things in your pack." );
					}
					else if ( ( iDye.Stackable == true ) || ( iDye.ItemID == 8702 ) || ( iDye.ItemID == 4011 ) )
					{
						from.SendMessage( "You cannot dye that." );
					}
					else if ( iDye.IsChildOf( from.Backpack ) )
					{
						iDye.Hue = m_Dye.vialHue;
						from.RevealingAction();
						from.PlaySound( 0x23E );
						from.AddToBackpack( new Bottle() );
						m_Dye.Consume();
					}
					else
					{
						from.SendMessage( "You cannot dye that with this." );
					}
				}
				else
				{
					from.SendMessage( "You cannot dye that with this." );
				}
			}
		}

		public SpaceDyes( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
            writer.Write( vialHue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            vialHue = reader.ReadInt();
		}
	}
}