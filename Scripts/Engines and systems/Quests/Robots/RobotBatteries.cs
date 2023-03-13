using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class RobotBatteries : Item
	{
		public override string DefaultName
		{
			get { return "robot batteries"; }
		}

		[Constructable]
		public RobotBatteries() : base( 0x2034 )
		{
			Weight = 1.0;
		}

		public RobotBatteries( Serial serial ) : base( serial )
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
				from.SendMessage( "What do you want to put the batteries in?" );
				t = new PowerTarget( this );
				from.Target = t;
			}
		}

		private class PowerTarget : Target
		{
			private RobotBatteries m_Battery;

			public PowerTarget( RobotBatteries battery ) : base( 1, false, TargetFlags.None )
			{
				m_Battery = battery;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				Item iBattery = targeted as Item;

				if ( iBattery is RobotItem )
				{
					RobotItem xBattery = (RobotItem)iBattery;

					int myCharges = xBattery.m_Charges;

					if ( !iBattery.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this battery on robots in your pack." );
					}
					else if ( myCharges < 100 )
					{
						xBattery.m_Charges = xBattery.m_Charges + 1;

						if ( xBattery.m_Charges > 100 ){ xBattery.m_Charges = 100; }

						from.SendMessage( "You charge your robot with the battery." );
						from.RevealingAction();
						from.SendSound( 0x559 );

						xBattery.InvalidateProperties();

						m_Battery.Delete();
					}
					else
					{
						from.SendMessage( "That robot is already fully charged." );
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