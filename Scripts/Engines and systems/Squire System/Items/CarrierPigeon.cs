using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class CarrierPigeon : Item
	{
		private Squire squire;
		private bool m_KillTheSquire;

		public CarrierPigeon( Serial serial ) : base( serial )
		{
		}

		public CarrierPigeon( Squire s )
		{
			m_KillTheSquire = true;
			squire = s;
			
			Name = ( "Carrier Pigeon: " + s.Name );
			Weight = 3;
			Hue = 1001;
			ItemID = 0x211D;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if( from.InRange( this.GetWorldLocation(), 2 ) == false )
			{
				from.SendLocalizedMessage( 500486 );	//That is too far away.
				return;
			}
			else if ( squire == null || squire.Deleted )
			{
				from.SendMessage( "Your squire has been sold into slavery." );
				return;
			}
			else if( !from.CheckAlive() )
			{
				from.SendLocalizedMessage( 1060190 );	//You cannot do that while dead!
			}
			else if ( from.Followers + squire.ControlSlots > from.FollowersMax )
			{
				from.SendMessage( "You have too many followers to call your squire." );
				return;
			}
			else
			{
				bool alreadyOwned = squire.Owners.Contains( from );
				if (!alreadyOwned)
				{
					squire.Owners.Add( from );
				}

				//Make the Squire belong to their master again.
				squire.SetControlMaster( from );
				m_KillTheSquire = false;

				//Bring the Squire to their master.
				squire.Location = from.Location;
				squire.Map = from.Map;

				//Set the Squire to follow their master.
				squire.ControlTarget = from;
				squire.ControlOrder = OrderType.Follow;
				
				//Just in case someone messed with the system.
				if ( squire.Summoned )
					squire.SummonMaster = from;

				this.Delete();
			}



		}
		public override void OnDelete()
		{
			try 
			{
				if ( m_KillTheSquire )
				{
					squire.Delete();
				}
			} 
			catch
			{
				Console.Write( "Error with calling squire back: {0} is being Deleted.  ItemID of:  {1}.", this.Name, this.ItemID );
			}
			base.OnDelete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( squire );
			writer.Write( m_KillTheSquire );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			squire = ( Squire )reader.ReadMobile();
			m_KillTheSquire = reader.ReadBool();
		}
	}
}
