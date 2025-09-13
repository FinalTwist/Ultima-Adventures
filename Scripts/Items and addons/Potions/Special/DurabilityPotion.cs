using System;
using Server.Network;
using Server;
using Server.Targets;
using Server.Targeting;

namespace Server.Items
{
	public class DurabilityPotion : BasePotion
	{
		[Constructable]
		public DurabilityPotion() : base( 0x180F, PotionEffect.Durability )
		{
			Hue = 0xB7D;
			Name = "durability potion";
		}

		public DurabilityPotion( Serial serial ) : base( serial )
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
	  
	  	public override void Drink( Mobile m )
      	{ 
         	if ( m.InRange( this.GetWorldLocation(), 1 ) ) 
         	{ 
				m.SendMessage( "What would you like to pour this on!" );
				m.Target = new DurabilityTarget( this, m );
         	} 
         	else 
         	{ 
            	m.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that. 
         	} 
		}

		public static void ConsumeCharge( DurabilityPotion potion, Mobile from )
		{
			potion.Consume();
			from.RevealingAction();
			from.PlaySound( 0x23E );
		}

		private class DurabilityTarget : Target
		{
			private DurabilityPotion m_Potion;
			private Mobile m_From;

			public DurabilityTarget( DurabilityPotion potion, Mobile from ) :  base ( 1, false, TargetFlags.None )
			{
				m_Potion = potion;
				m_From = from;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseArmor )
				{
					BaseArmor repairing = (BaseArmor)targeted;
					if ( !repairing.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "The item must be in your backpack to use that potion on it!" );
					}
					else if ( repairing.MaxHitPoints >= 50 )
					{
						from.SendMessage( "This item is already too durable to be affected!" );
					}
					else
					{
						from.SendMessage( "You add to the durability of the item!" );
						repairing.MaxHitPoints += 10;
						Server.Items.DurabilityPotion.ConsumeCharge( m_Potion, m_From );
					}
				}
				else if ( targeted is BaseWeapon )
				{
					BaseWeapon repairing2 = (BaseWeapon)targeted;
					if ( !repairing2.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "The item must be in your backpack to use that potion on it!" );
					}
					else if ( repairing2.MaxHitPoints >= 50 )
					{
						from.SendMessage( "This item is already too durable to be affected!" );
					}
					else
					{
						from.SendMessage( "You add to the durability of the item!" );
						repairing2.MaxHitPoints += 10;
						Server.Items.DurabilityPotion.ConsumeCharge( m_Potion, m_From );
					}
				}
				else if ( targeted is Item )
				{
					from.SendMessage( "This item cannot be altered!" );
				}
				else
				{
					from.SendMessage( "You cannot do that!" );
				}
			}
		}
	}
}
