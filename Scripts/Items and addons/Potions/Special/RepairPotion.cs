using System;
using Server.Network;
using Server;
using Server.Targets;
using Server.Targeting;
using Server.Items;
namespace Server.Items
{
	public class RepairPotion : BasePotion
	{
		[Constructable]
		public RepairPotion() : base( 0x180F, PotionEffect.Repair )
		{
			Hue = 0xB7A;
			Name = "repairing potion";
		}

		public RepairPotion( Serial serial ) : base( serial )
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
				m.Target = new RepairTarget( this, m );
         	} 
         	else 
         	{ 
            	m.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that. 
         	} 
		}

		public static void ConsumeCharge( RepairPotion potion, Mobile from )
		{
			potion.Consume();
			from.RevealingAction();
			from.PlaySound( 0x23E );
		}

		private class RepairTarget : Target
		{
			private RepairPotion m_Potion;
			private Mobile m_From;

			public RepairTarget( RepairPotion potion, Mobile from ) :  base ( 1, false, TargetFlags.None )
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
						from.SendLocalizedMessage( 1044275 ); // The item must be in your backpack to repair it.
					}
					else if ( repairing.MaxHitPoints <= 0 || repairing.HitPoints == repairing.MaxHitPoints )
					{
						from.SendLocalizedMessage( 1044281 );// That item is in full repair
					}
					else
					{
						from.SendLocalizedMessage( 1044279 ); // You repair the item.
						repairing.MaxHitPoints = repairing.MaxHitPoints-1;
						repairing.HitPoints = repairing.MaxHitPoints;
						Server.Items.RepairPotion.ConsumeCharge( m_Potion, m_From );
					}
				}
				else if ( targeted is BaseWeapon )
				{
					BaseWeapon repairing2 = (BaseWeapon)targeted;
					if ( !repairing2.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1044275 ); // The item must be in your backpack to repair it.
					}
					else if ( repairing2.MaxHitPoints <= 0 || repairing2.HitPoints == repairing2.MaxHitPoints )
					{
						from.SendLocalizedMessage( 1044281 );// That item is in full repair
					}
					else
					{
						from.SendLocalizedMessage( 1044279 ); // You repair the item.
						repairing2.MaxHitPoints = repairing2.MaxHitPoints-1;
						repairing2.HitPoints = repairing2.MaxHitPoints;
						Server.Items.RepairPotion.ConsumeCharge( m_Potion, m_From );
					}
				}
				else if ( targeted is Item )
				{
					from.SendLocalizedMessage( 1044277 ); // That item cannot be repaired.
				}
				else
				{
					from.SendLocalizedMessage( 500426 ); // You can't repair that.
				}
			}
		}
	}
}
