using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class EmbalmingFluid : Item
	{
		[Constructable]
		public EmbalmingFluid() : this( 1 )
		{
		}

		[Constructable]
		public EmbalmingFluid( int amount ) : base( 0xE0F )
		{
			Name = "embalming fluid";
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Hue = 0xBA1;
		}

		public EmbalmingFluid( Serial serial ) : base( serial )
		{
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
				from.SendMessage( "What do you want to use the embalming fluid on?" );
				t = new FluidTarget( this );
				from.Target = t;
			}
		}

		private class FluidTarget : Target
		{
			private EmbalmingFluid m_Embalming;

			public FluidTarget( EmbalmingFluid fluid ) : base( 1, false, TargetFlags.None )
			{
				m_Embalming = fluid;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				Item iFluid = targeted as Item;

				if ( iFluid is FrankenPorterItem )
				{
					FrankenPorterItem xFluid = (FrankenPorterItem)iFluid;

					int myCharges = xFluid.m_Charges;

					if ( !iFluid.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this fluid on items in your pack." );
					}
					else if ( myCharges < 100 )
					{
						int UpMe = 5;
						if ( xFluid.PorterType > 0 ){ UpMe = 1; }

						xFluid.m_Charges = xFluid.m_Charges + UpMe;

						if ( xFluid.m_Charges > 100 ){ xFluid.m_Charges = 100; }

						from.SendMessage( "You preserve your reanimation with the embalming fluid." );

						xFluid.InvalidateProperties();

						from.RevealingAction();
						from.PlaySound( 0x23E );
						from.AddToBackpack( new Bottle() );
						m_Embalming.Consume();
					}
					else
					{
						from.SendMessage( "That reanimation is already full of fluid." );
					}
				}
				else
				{
					from.SendMessage( "You don't think that will really do anything." );
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}